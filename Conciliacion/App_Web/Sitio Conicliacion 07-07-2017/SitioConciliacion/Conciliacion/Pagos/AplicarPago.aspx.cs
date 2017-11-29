using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Conciliacion.RunTime.ReglasDeNegocio;
using Conciliacion.RunTime;
using System.Configuration;
using SeguridadCB.Public;
using Conciliacion.RunTime.DatosSQL;

public partial class Conciliacion_Pagos_AplicarPago : System.Web.UI.Page
{
    #region "Propiedades Globales"
    private SeguridadCB.Public.Operaciones operaciones;
    private SeguridadCB.Public.Usuario usuario;
    private SeguridadCB.Public.Parametros parametros;
    public List<ReferenciaConciliadaPedido> listaReferenciaConciliadaPagos = new List<ReferenciaConciliadaPedido>();
    private List<ListaCombo> listFormasConciliacion = new List<ListaCombo>();
    private DataTable tblReferenciasAPagar;
    public List<ListaCombo> listCamposDestino = new List<ListaCombo>();
    public int corporativoConciliacion, añoConciliacion, folioConciliacion, folioExterno, sucursalConciliacion;
    public short mesConciliacion, tipoConciliacion;
    public MovimientoCajaDatos movimientoCajaAlta = null;
    #endregion

    #region Eventos de la Forma
    protected override void OnPreInit(EventArgs e)
    {
        if (HttpContext.Current.Session["Operaciones"] == null)
            Response.Redirect("~/Acceso/Acceso.aspx", true);
        else
            operaciones = (SeguridadCB.Public.Operaciones)HttpContext.Current.Session["Operaciones"];
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        Conciliacion.RunTime.App.ImplementadorMensajes.ContenedorActual = this;
        try
        {
            if (HttpContext.Current.Request.UrlReferrer != null)
            {
                if ((!HttpContext.Current.Request.UrlReferrer.AbsoluteUri.Contains("SitioConciliacion")) || (HttpContext.Current.Request.UrlReferrer.AbsoluteUri.Contains("Acceso.aspx")))
                {
                    HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
                    HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
                }
            }
            if (!Page.IsPostBack)
            {
                usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
                parametros = (SeguridadCB.Public.Parametros)HttpContext.Current.Session["Parametros"];

                corporativoConciliacion = Convert.ToInt32(Request.QueryString["Corporativo"]);
                sucursalConciliacion = Convert.ToInt16(Request.QueryString["Sucursal"]);
                añoConciliacion = Convert.ToInt32(Request.QueryString["Año"]);
                folioConciliacion = Convert.ToInt32(Request.QueryString["Folio"]);
                mesConciliacion = Convert.ToSByte(Request.QueryString["Mes"]);
                tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);
                LlenarBarraEstado();
                Carga_FormasConciliacion(tipoConciliacion);
                cargar_ComboCampoFiltroDestino(tipoConciliacion, rdbFiltrarEn.SelectedItem.Value);
                enlazarCampoFiltrar();
                activarControles(tipoCampoSeleccionado());
                //CARGAR LAS TRANSACCIONES CONCILIADAS POR EL CRITERIO DE AUTOCONCILIACIÓN
                Consulta_MovimientoCaja(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion);
                Consulta_TransaccionesAPagar(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion);
                GenerarTablaReferenciasAPagarPedidos();
                LlenaGridViewReferenciasPagos();

                tdExportar.Attributes.Add("class", "iconoOpcion bg-color-grisClaro02");
                imgExportar.Enabled = false;
            }

        }
        catch (Exception ex) { App.ImplementadorMensajes.MostrarMensaje(ex.Message); }
    }

    #endregion

    #region Funciones Privadas
    //Cargar InfoConciliacion Actual
    public void cargarInfoConciliacionActual()
    {
        corporativoConciliacion = Convert.ToInt32(Request.QueryString["Corporativo"]);
        sucursalConciliacion = Convert.ToInt16(Request.QueryString["Sucursal"]);
        añoConciliacion = Convert.ToInt32(Request.QueryString["Año"]);
        folioConciliacion = Convert.ToInt32(Request.QueryString["Folio"]);
        mesConciliacion = Convert.ToSByte(Request.QueryString["Mes"]);
        tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);
    }

    /// <summary>
    /// Leer Movimiento Caja Alta
    /// </summary>
    public void Consulta_MovimientoCaja(int corporativoConciliacion, int sucursalConciliacion, int añoConciliacion, short mesConciliacion, int folioConciliacion)
    {
        try
        {
            movimientoCajaAlta = Conciliacion.RunTime.App.Consultas.ConsultaMovimientoCajaAlta(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion);

            //short consecutivo = movimientoCajaAlta.Consecutivo;
            //int folio = movimientoCajaAlta.Folio;
            //short caja = movimientoCajaAlta.Caja;
            //string FOperacion = Convert.ToString(movimientoCajaAlta.FOperacion);
            HttpContext.Current.Session["MovimientoCaja"] = movimientoCajaAlta;
        }
        catch (Exception ex) { App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.StackTrace + "\nMensaje: " + ex.Message); }

    }
    /// <summary>
    /// Carga las Formas de Conciliación
    /// </summary>
    public void Carga_FormasConciliacion(short tipoConciliacion)
    {
        try
        {
            listFormasConciliacion = Conciliacion.RunTime.App.Consultas.ConsultaFormaConciliacion(tipoConciliacion);
            this.ddlCriteriosConciliacion.DataSource = listFormasConciliacion;
            this.ddlCriteriosConciliacion.DataValueField = "Identificador";
            this.ddlCriteriosConciliacion.DataTextField = "Descripcion";
            this.ddlCriteriosConciliacion.DataBind();
            this.ddlCriteriosConciliacion.Dispose();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }
    }
    /// <summary>
    /// Leer Pedidos --> Movimiento Caja Alta
    /// </summary>
    public void Consulta_TransaccionesAPagar(int corporativoC, int sucursalC, int añoC, short mesC, int folioC)
    {
        try
        {
            // movimientoCajaAlta = HttpContext.Current.Session["MovimientoCaja"] as MovimientoCaja;
            listaReferenciaConciliadaPagos = Conciliacion.RunTime.App.Consultas.ConsultaPagosPorAplicar(corporativoC, sucursalC, añoC, mesC, folioC);
            HttpContext.Current.Session["LIST_REF_PAGAR"] = listaReferenciaConciliadaPagos;
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }
    /// <summary>
    /// Genera la tabla Referencias a Pagar Pedidos
    /// </summary>
    public void GenerarTablaReferenciasAPagarPedidos()
    {
        try
        {
            tblReferenciasAPagar = new DataTable("ReferenciasAConciliarPedidos");
            //Campos Externos
            tblReferenciasAPagar.Columns.Add("Secuencia", typeof(int));
            tblReferenciasAPagar.Columns.Add("FolioExt", typeof(int));
            tblReferenciasAPagar.Columns.Add("RFCTercero", typeof(string));
            tblReferenciasAPagar.Columns.Add("Retiro", typeof(decimal));
            tblReferenciasAPagar.Columns.Add("Referencia", typeof(string));
            tblReferenciasAPagar.Columns.Add("NombreTercero", typeof(string));
            tblReferenciasAPagar.Columns.Add("Deposito", typeof(decimal));
            tblReferenciasAPagar.Columns.Add("Cheque", typeof(string));
            tblReferenciasAPagar.Columns.Add("FMovimiento", typeof(DateTime));
            tblReferenciasAPagar.Columns.Add("FOperacion", typeof(DateTime));
            tblReferenciasAPagar.Columns.Add("MontoConciliado", typeof(decimal));
            tblReferenciasAPagar.Columns.Add("Concepto", typeof(string));
            tblReferenciasAPagar.Columns.Add("Descripcion", typeof(string));
            tblReferenciasAPagar.Columns.Add("Diferencia", typeof(decimal));
            tblReferenciasAPagar.Columns.Add("StatusMovimiento", typeof(string));
            //Campos Pedidos
            tblReferenciasAPagar.Columns.Add("Pedido", typeof(int));
            tblReferenciasAPagar.Columns.Add("PedidoReferencia", typeof(string));
            tblReferenciasAPagar.Columns.Add("SeriePedido", typeof(string));
            tblReferenciasAPagar.Columns.Add("RemisionPedido", typeof(string));
            tblReferenciasAPagar.Columns.Add("FolioSat", typeof(string));
            tblReferenciasAPagar.Columns.Add("SerieSat", typeof(string));
            tblReferenciasAPagar.Columns.Add("AñoPed", typeof(int));
            tblReferenciasAPagar.Columns.Add("Celula", typeof(int));
            tblReferenciasAPagar.Columns.Add("Cliente", typeof(string));
            tblReferenciasAPagar.Columns.Add("Nombre", typeof(string));
            tblReferenciasAPagar.Columns.Add("Total", typeof(decimal));
            tblReferenciasAPagar.Columns.Add("ConceptoPedido", typeof(string));

            foreach (ReferenciaConciliadaPedido rc in listaReferenciaConciliadaPagos)
            {
                tblReferenciasAPagar.Rows.Add(
                   rc.Secuencia,
                    rc.Folio,
                    rc.RFCTercero,
                    rc.Retiro,
                    rc.Referencia,
                    rc.NombreTercero,
                    rc.Deposito,
                    rc.Cheque,
                    rc.FMovimiento,
                    rc.FOperacion,
                    rc.MontoConciliado,
                    rc.Concepto,
                    rc.Descripcion,
                    rc.Diferencia,
                    rc.StatusMovimiento,
                    rc.Pedido,
                    rc.PedidoReferencia,
                    rc.SeriePedido,
                    rc.RemisionPedido,
                    rc.FolioSat,
                    rc.SerieSat,
                    rc.AñoPedido,
                    rc.CelulaPedido,
                    rc.Cliente,
                    rc.Nombre,
                    rc.Total,
                    rc.ConceptoPedido
                    );
            }
            HttpContext.Current.Session["TAB_REF_PAGAR"] = tblReferenciasAPagar;
            ViewState["TAB_REF_PAGAR"] = tblReferenciasAPagar;
        }
        catch (Exception e)
        {

        }
    }
    /// <summary>
    /// Llena el gridview con las conciliaciones antes leídas
    /// </summary>
    private void LlenaGridViewReferenciasPagos()//
    {
        try
        {
            DataTable tablaReferenacias = (DataTable)HttpContext.Current.Session["TAB_REF_PAGAR"];
            grvPagos.DataSource = tablaReferenacias;
            grvPagos.DataBind();
        }
        catch (Exception e)
        {
            App.ImplementadorMensajes.MostrarMensaje(e.Message);
        }

    }
    //Llenar Barra de Estado
    public void LlenarBarraEstado()
    {
        try
        {
            cConciliacion c = App.Consultas.ConsultaConciliacionDetalle(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion);
            lblFolio.Text = c.Folio.ToString();
            lblBanco.Text = c.BancoStr;
            lblCuenta.Text = c.CuentaBancaria;
            lblGrupoCon.Text = c.GrupoConciliacionStr;
            lblSucursal.Text = c.SucursalDes;
            lblTipoCon.Text = c.TipoConciliacionStr;
            lblMesAño.Text = c.Mes + "/" + c.Año;
            lblConciliadasExt.Text = c.ConciliadasExternas.ToString();
            lblConciliadasInt.Text = c.ConciliadasInternas.ToString();
            lblStatusConciliacion.Text = c.StatusConciliacion;
            imgStatusConciliacion.ImageUrl = c.UbicacionIcono;
        }
        catch (Exception)
        {

            throw;
        }

    }
    /// <summary>
    /// Cargar campos de Filtro y Busqueda externo
    /// </summary>
    public void cargar_ComboCampoFiltroDestino(int tConciliacion, string filtrarEn)
    {
        try
        {
            listCamposDestino = filtrarEn.Equals("Externos")
                                            ? Conciliacion.RunTime.App.Consultas.ConsultaDestino()
                                            : Conciliacion.RunTime.App.Consultas.ConsultaDestinoPedido();


        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }

    }

    //Enlazar Campo a Filtrar/Busqueda
    public void enlazarCampoFiltrar()
    {
        try
        {
            this.ddlCampoFiltrar.DataSource = listCamposDestino;
            this.ddlCampoFiltrar.DataValueField = "Identificador";
            this.ddlCampoFiltrar.DataTextField = "Descripcion";
            this.ddlCampoFiltrar.DataBind();
            this.ddlCampoFiltrar.Dispose();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    /// <summary>
    /// Activar los textbox segun sea el tipo de campa seleccionado en el filtro
    /// </summary>
    public void activarControles(string tipoCampo)
    {
        switch (tipoCampo)
        {
            case "Numero":
                // this.rfvValorExterno.ControlToValidate = "txtValorNumericoExterno";
                this.txtValorNumericoFiltro.Visible = true;
                this.txtValorCadenaFiltro.Visible = false;
                this.txtValorFechaFiltro.Visible = false;
                this.txtValorNumericoFiltro.Text = "0";
                this.txtValorCadenaFiltro.Text = String.Empty;
                this.txtValorFechaFiltro.Text = String.Empty;
                break;
            case "Fecha":
                //this.rfvValorExterno.ControlToValidate = "txtValorFechaExterno";
                this.txtValorFechaFiltro.Visible = true;
                this.txtValorNumericoFiltro.Visible = false;
                this.txtValorCadenaFiltro.Visible = false;
                this.txtValorFechaFiltro.Text = String.Empty;
                this.txtValorNumericoFiltro.Text = String.Empty;
                this.txtValorCadenaFiltro.Text = String.Empty;
                break;
            case "Cadena":
                //  this.rfvValorExterno.ControlToValidate = "txtValorCadenaExterno";
                this.txtValorCadenaFiltro.Visible = true;
                this.txtValorNumericoFiltro.Visible = false;
                this.txtValorFechaFiltro.Visible = false;
                this.txtValorCadenaFiltro.Text = String.Empty;
                this.txtValorNumericoFiltro.Text = String.Empty;
                this.txtValorFechaFiltro.Text = String.Empty;
                break;
        }

    }
    /// <summary>
    /// Lee el tipo de campo Seleccionado
    /// </summary>
    public string tipoCampoSeleccionado()
    {
        return listCamposDestino[ddlCampoFiltrar.SelectedIndex].Campo1;
    }
    /// <summary>
    /// Lee el valor del TextBox por tipo de Campo Seleccionado
    /// </summary>
    public string valorFiltro(string tipoCampo)
    {
        return tipoCampo.Equals("Cadena")
                   ? txtValorCadenaFiltro.Text
                   : (tipoCampo.Equals("Fecha") ? txtValorFechaFiltro.Text : txtValorNumericoFiltro.Text);
    }
    /// <summary>
    /// Ejecutar el Filtro por Valor y Campo
    /// </summary>
    private void FiltrarCampo(string valorFiltro, string filtroEn)
    {
        try
        {
            DataTable dt = (DataTable)HttpContext.Current.Session["TAB_REF_PAGAR"];

            DataView dv = new DataView(dt);
            string SearchExpression = String.Empty;
            if (!String.IsNullOrEmpty(valorFiltro))
            {
                SearchExpression = string.Format(
                    ddlOperacion.SelectedItem.Value == "LIKE"
                        ? "{0} {1} '%{2}%'"
                        : "{0} {1} '{2}'", ddlCampoFiltrar.SelectedItem.Text,
                    ddlOperacion.SelectedItem.Value, valorFiltro);
            }
            if (dv.Count <= 0) return;
            dv.RowFilter = SearchExpression;
            ViewState["TAB_REF_PAGAR"] = dv.ToTable();
            grvPagos.DataSource = ViewState["TAB_REF_PAGAR"] as DataTable;
            grvPagos.DataBind();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }
    }
    /// <summary>
    /// Obtiene la direccion de ordebamiento ASC o DESC
    /// </summary>
    private string direccionOrdenarCadena(string columna)
    {
        string direccionOrden = "ASC";
        string expresionOrden = ViewState["ExpresionOrden"] as string;
        if (expresionOrden != null)
            if (expresionOrden == columna)
            {
                string direccionAnterior = ViewState["DireccionOrden"] as string;
                if ((direccionAnterior != null) && (direccionAnterior == "ASC"))
                    direccionOrden = "DESC";
            }

        ViewState["DireccionOrden"] = direccionOrden;
        ViewState["ExpresionOrden"] = columna;

        return direccionOrden;
    }
    /// <summary>
    /// Metodos Busqueda
    ///</summary>
    public string resaltarBusqueda(string entradaTexto)
    {
        if (!txtBuscar.Text.Equals(""))
        {
            string strBuscar = txtBuscar.Text;
            Regex RegExp = new Regex(strBuscar.Replace(" ", "|").Trim(),
                           RegexOptions.IgnoreCase);
            return RegExp.Replace(entradaTexto, pintarBusqueda);
        }
        return entradaTexto;
    }
    /// <summary>
    /// Metodo Pintar
    ///</summary>
    public string pintarBusqueda(Match m)
    {
        return "<span class=marcarBusqueda>" + m.Value + "</span>";
    }
    #endregion

    #region Funciones de las Formas
    protected void btnFiltrar_Click(object sender, ImageClickEventArgs e)
    {
        //Leer el tipoConciliacion URL
        tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);
        cargar_ComboCampoFiltroDestino(tipoConciliacion, rdbFiltrarEn.SelectedItem.Value);

        FiltrarCampo(valorFiltro(tipoCampoSeleccionado()), rdbFiltrarEn.SelectedItem.Value);
    }
    protected void ddlCampoFiltrar_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Leer el tipoConciliacion URL
        tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);
        cargar_ComboCampoFiltroDestino(tipoConciliacion, rdbFiltrarEn.SelectedItem.Value);

        activarControles(tipoCampoSeleccionado());
    }
    protected void imgBuscar_Click(object sender, ImageClickEventArgs e)
    {
        txtBuscar.Text = String.Empty;
        mpeBuscar.Show();
    }
    protected void btnIrBuscar_Click(object sender, EventArgs e)
    {
        grvPagos.DataSource = ViewState["TAB_REF_PAGAR"] as DataTable;
        grvPagos.DataBind();
        mpeBuscar.Hide();
    }

    protected void Nueva_Ventana(string Pagina, string Titulo, int Ancho, int Alto, int X, int Y)
    {

        ScriptManager.RegisterClientScriptBlock(this.upAplicarPagos,
                                         upAplicarPagos.GetType(),
                                            "ventana",
                                            "ShowWindow('" + Pagina + "','" + Titulo + "'," + Ancho + "," + Alto + "," + X + "," + Y + ")",
                                            true);

    }

    public void lanzarReporteComprobanteDeCaja(MovimientoCaja mc)
    {
        //short consecutivo = mc.Consecutivo;
        //int folio = mc.Folio;
        //short caja = mc.Caja;
        //string FOperacion = Convert.ToString(mc.FOperacion);
        AppSettingsReader settings = new AppSettingsReader();

        string strReporte = Server.MapPath("~/") + settings.GetValue("RutaComprobanteDeCaja", typeof(string));
        if (!File.Exists(strReporte)) return;
        try
        {
            string strServer = settings.GetValue("Servidor", typeof(string)).ToString();
            string strDatabase = settings.GetValue("Base", typeof(string)).ToString();
            usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];

            string strUsuario = usuario.IdUsuario.Trim();
            string strPW = usuario.ClaveDesencriptada;
            ArrayList Par = new ArrayList();

            Par.Add("@Consecutivo=" + mc.Consecutivo);
            Par.Add("@Folio=" + mc.Folio);
            Par.Add("@Caja=" + mc.Caja);
            Par.Add("@FOperacion=" + mc.FOperacion);

            ClaseReporte Reporte = new ClaseReporte(strReporte, Par, strServer, strDatabase, strUsuario, strPW);
            HttpContext.Current.Session["RepDoc"] = Reporte.RepDoc;
            HttpContext.Current.Session["ParametrosReporte"] = Par;
            Nueva_Ventana("../../Reporte/Reporte.aspx", "Carta", 0, 0, 0, 0);
            Reporte = null;
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error: " + ex.Message);
        }
    }

    public void lanzarReporteCobranza(int Cobranza)
    {
        AppSettingsReader settings = new AppSettingsReader();

        string strReporte = Server.MapPath("~/") + settings.GetValue("RutaCobranza", typeof(string));
        if (!File.Exists(strReporte)) return;
        try
        {
            string strServer = settings.GetValue("Servidor", typeof(string)).ToString();
            string strDatabase = settings.GetValue("Base", typeof(string)).ToString();
            usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];

            string strUsuario = usuario.IdUsuario.Trim();
            string strPW = usuario.ClaveDesencriptada;
            ArrayList Par = new ArrayList();

            Par.Add("@Cobranza=" + Cobranza);

            ClaseReporte Reporte = new ClaseReporte(strReporte, Par, strServer, strDatabase, strUsuario, strPW);
            HttpContext.Current.Session["RepDoc"] = Reporte.RepDoc;
            HttpContext.Current.Session["ParametrosReporte"] = Par;
            Nueva_Ventana("../../Reporte/ReporteAlternativo.aspx", "Carta", 0, 0, 0, 0);
            Reporte = null;
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error: " + ex.Message);
        }
    }



    protected void imgExportar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //lanzarReporteComprobanteDeCaja();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.StackTrace);
        }
    }
    protected void btnActualizar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //Leer la InfoActual Conciliacion
            cargarInfoConciliacionActual();
            Consulta_TransaccionesAPagar(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion);
            GenerarTablaReferenciasAPagarPedidos();
            LlenaGridViewReferenciasPagos();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }
    }
    protected void btnAplicarPagos_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Parametros p = Session["Parametros"] as Parametros;
            AppSettingsReader settings = new AppSettingsReader();
            short modulo = Convert.ToSByte(settings.GetValue("Modulo", typeof(string)));

            movimientoCajaAlta = HttpContext.Current.Session["MovimientoCaja"] as MovimientoCajaDatos;
            int MaxDocumentos = Convert.ToInt16(p.ValorParametro(modulo, "NumeroDocumentosTRANSBAN"));
            TransBan objTransBan = new TransBan();

            cargarInfoConciliacionActual();

            List<MovimientoCaja> lstMovimientoCaja = objTransBan.ReorganizaTransban(movimientoCajaAlta, MaxDocumentos);

            Conexion conexion = new Conexion();


            int corporativoConciliacion = 0;
            Int16 sucursalConciliacion = 0;
            int añoConciliacion = 0;
            int folioConciliacion = 0;
            short mesConciliacion = 0;
            short tipoConciliacion = 0;

            foreach (MovimientoCaja objMovimientoCaja in lstMovimientoCaja)
            {
                conexion = new Conexion();
                conexion.AbrirConexion(true);


                if (objMovimientoCaja.Guardar(conexion))
                {

                    corporativoConciliacion = Convert.ToInt32(Request.QueryString["Corporativo"]);
                    sucursalConciliacion = Convert.ToInt16(Request.QueryString["Sucursal"]);
                    añoConciliacion = Convert.ToInt32(Request.QueryString["Año"]);
                    folioConciliacion = Convert.ToInt32(Request.QueryString["Folio"]);
                    mesConciliacion = Convert.ToSByte(Request.QueryString["Mes"]);
                    tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);


                    MovimientoCajaConciliacion objMCC = new MovimientoCajaConciliacionDatos(objMovimientoCaja.Caja,objMovimientoCaja.FOperacion,objMovimientoCaja.Consecutivo,objMovimientoCaja.Folio,
                        corporativoConciliacion,sucursalConciliacion,añoConciliacion,mesConciliacion,folioConciliacion,"ABIERTO",new MensajeImplemantacionForm());
                    objMCC.Guardar(conexion);

                    Boolean HasBoveda = p.ValorParametro(modulo, "BovedaExiste").Equals("1");

                    RelacionCobranzaException rCobranzaE = null;
                    try
                    {
                        RelacionCobranza rCobranza = App.RelCobranza.CrearObjeto(objMovimientoCaja, HasBoveda);
                        rCobranza.CadenaConexion = App.CadenaConexion;
                        rCobranzaE = rCobranza.CreaRelacionCobranza(conexion);

                        if (!rCobranzaE.DetalleExcepcion.VerificacionValida)
                        {
                            App.ImplementadorMensajes.MostrarMensaje("Error: " + rCobranzaE.DetalleExcepcion.Mensaje + ", Código: " + rCobranzaE.DetalleExcepcion.CodigoError);
                        }
                    }
                    catch (Exception ex)
                    {
                        rCobranzaE.DetalleExcepcion.CodigoError = 201;
                        rCobranzaE.DetalleExcepcion.Mensaje = rCobranzaE.DetalleExcepcion.Mensaje + " " + ex.Message;
                        rCobranzaE.DetalleExcepcion.VerificacionValida = false;
                        throw new Exception("Error: " + rCobranzaE.DetalleExcepcion.Mensaje + ", Código: " + rCobranzaE.DetalleExcepcion.CodigoError);
                    }

                    lanzarReporteComprobanteDeCaja(objMovimientoCaja);

                    Consulta_MovimientoCaja(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion);
                    Consulta_TransaccionesAPagar(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion);
                    GenerarTablaReferenciasAPagarPedidos();
                    LlenaGridViewReferenciasPagos();

                    parametros = (SeguridadCB.Public.Parametros)HttpContext.Current.Session["Parametros"];
                    string aplicacobranza = parametros.ValorParametro(30, "AplicaCobranza");
                    if (aplicacobranza == "1")
                    {
                        usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
                        string strUsuario = usuario.IdUsuario.Trim();

                        List<ReferenciaConciliadaPedido> _listaReferenciaConciliadaPagos = (List<ReferenciaConciliadaPedido>)HttpContext.Current.Session["LIST_REF_PAGAR"];

                        Cobranza cobranza = Conciliacion.RunTime.App.Cobranza.CrearObjeto();
                        cobranza.FCobranza = DateTime.Now;
                        cobranza.UsuarioCaptura = strUsuario;
                        cobranza.ListaReferenciaConciliadaPedido = _listaReferenciaConciliadaPagos;
                        int idCobranza = cobranza.GuardarProcesoCobranza();
                        lanzarReporteCobranza(idCobranza);
                    }
                }
                else
                    App.ImplementadorMensajes.MostrarMensaje("Error al aplicar el pago de los pedidos, por favor verifique.");
            }

            conexion.AbrirConexion(true);
            FacturasComplemento objFacturasComplemento = App.FacturasComplemento;
            objFacturasComplemento.CorporativoConciliacion = corporativoConciliacion;
            objFacturasComplemento.SucursalConciliacion = sucursalConciliacion;
            objFacturasComplemento.AnioConciliacion = añoConciliacion;
            objFacturasComplemento.MesConciliacion = mesConciliacion;
            objFacturasComplemento.FolioConciliacion = folioConciliacion;
            objFacturasComplemento.Guardar(conexion);



            App.ImplementadorMensajes.MostrarMensaje("El registro se guardó con éxito.");

            /* if (movimientoCajaAlta != null && movimientoCajaAlta.Caja != 0)
            {
                if (grvPagos.Rows.Count > 0)
                {
                    if (movimientoCajaAlta.Guardar())
                    {

                        Boolean HasBoveda = p.ValorParametro(modulo, "BovedaExiste").Equals("SI");

                        RelacionCobranzaException rCobranzaE = null;
                        try
                        {
                            RelacionCobranza rCobranza = App.RelCobranza.CrearObjeto(movimientoCajaAlta, HasBoveda);
                            rCobranza.CadenaConexion = App.CadenaConexion;
                            Conciliacion.RunTime.DatosSQL.Conexion conexion = new Conciliacion.RunTime.DatosSQL.Conexion();
                            conexion.AbrirConexion(true);
                            rCobranzaE = rCobranza.CreaRelacionCobranza(conexion);

                            if (rCobranzaE.DetalleExcepcion.VerificacionValida)
                            {
                                App.ImplementadorMensajes.MostrarMensaje(rCobranzaE.DetalleExcepcion.Mensaje);
                            }
                            else
                            {
                                App.ImplementadorMensajes.MostrarMensaje("Error: " + rCobranzaE.DetalleExcepcion.Mensaje + ", Codigo: " + rCobranzaE.DetalleExcepcion.CodigoError);
                            }
                        }
                        catch (Exception ex)
                        {
                            rCobranzaE.DetalleExcepcion.CodigoError = 201;
                            rCobranzaE.DetalleExcepcion.Mensaje = rCobranzaE.DetalleExcepcion.Mensaje + " " + ex.Message;
                            rCobranzaE.DetalleExcepcion.VerificacionValida = false;
                            throw new Exception("Error: " + rCobranzaE.DetalleExcepcion.Mensaje + ", Codigo: " + rCobranzaE.DetalleExcepcion.CodigoError);
                        }

                        lanzarReporteComprobanteDeCaja(movimientoCajaAlta);
                        Consulta_MovimientoCaja(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion);
                        Consulta_TransaccionesAPagar(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion);
                        GenerarTablaReferenciasAPagarPedidos();
                        LlenaGridViewReferenciasPagos();

                        parametros = (SeguridadCB.Public.Parametros)HttpContext.Current.Session["Parametros"];
                        string aplicacobranza = parametros.ValorParametro(30, "AplicaCobranza");
                        if (aplicacobranza == "1")
                        {
                            usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
                            string strUsuario = usuario.IdUsuario.Trim();

                            List<ReferenciaConciliadaPedido>_listaReferenciaConciliadaPagos =(List<ReferenciaConciliadaPedido>) HttpContext.Current.Session["LIST_REF_PAGAR"];

                            Cobranza cobranza = Conciliacion.RunTime.App.Cobranza.CrearObjeto();
                            /*Charcar si quedaran como constantes
                            cobranza.FCobranza = DateTime.Now;
                            cobranza.UsuarioCaptura = strUsuario;
                            cobranza.ListaReferenciaConciliadaPedido = _listaReferenciaConciliadaPagos;
                            int idCobranza = cobranza.GuardarProcesoCobranza();
                            lanzarReporteCobranza(idCobranza);
                        }
                    }
                    else
                        App.ImplementadorMensajes.MostrarMensaje("Error al aplicar el pago de los pedidos. Verifique");
                }
                else
                    App.ImplementadorMensajes.MostrarMensaje("No existe ningun pedidos para aplicar pagos. Verifique");

            }
            else
                App.ImplementadorMensajes.MostrarMensaje("No puede aplicar pagos. Verifique: Caja 0 no existe");*/
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }
    }
    protected void grvPagos_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='bg-color-grisClaro02'");
            e.Row.Attributes.Add("onmouseout", "this.className='bg-color-blanco'");
        }
    }
    protected void grvPagos_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtSortTable = ViewState["TAB_REF_PAGAR"] as DataTable;
        if (dtSortTable == null) return;
        string order = direccionOrdenarCadena(e.SortExpression);
        dtSortTable.DefaultView.Sort = e.SortExpression + " " + order;
        grvPagos.DataSource = dtSortTable;
        grvPagos.DataBind();
    }
    protected void rdbFiltrarEn_SelectedIndexChanged(object sender, EventArgs e)
    {

        //Leer el tipoConciliacion URL
        tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);
        cargar_ComboCampoFiltroDestino(tipoConciliacion, rdbFiltrarEn.SelectedItem.Value);
        enlazarCampoFiltrar();
        activarControles(tipoCampoSeleccionado());
    }
    #endregion
    protected void grvPagos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Image imgSM = e.Row.FindControl("imgStatusMovimiento") as Image;
            if (imgSM.AlternateText.Equals("PENDIENTE"))
                imgSM.CssClass = "icono bg-color-grisClaro02";
            else
            {
                imgSM.CssClass = "icono bg-color-verdeClaro";
                btnAplicarPagos.Enabled = false;
                tdAplicarPagos.Attributes.Add("class", "iconoOpcion bg-color-grisClaro02");
            }
        }
    }
}