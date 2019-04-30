using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Conciliacion.RunTime.ReglasDeNegocio;
using Conciliacion.RunTime.DatosSQL;
using System.Configuration;
using Conciliacion.RunTime;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using CrystalDecisions.Shared.Json;
using Locker;

public partial class Conciliacion_FormasConciliar_CantidadConcuerda : System.Web.UI.Page
{
    #region "Propiedades Globales"
    private SeguridadCB.Public.Operaciones operaciones;
    private SeguridadCB.Public.Usuario usuario;
    private SeguridadCB.Public.Parametros parametros;
    #endregion
    #region "Propiedades Privadas"
    private List<ListaCombo> listFormasConciliacion = new List<ListaCombo>();
    private List<ListaCombo> listSucursales = new List<ListaCombo>();
    private List<ListaCombo> listStatusConcepto = new List<ListaCombo>();



    private DataTable tblReferenciasAConciliar;
    private DataTable tblTransaccionesConciliadas;

    public ReferenciaNoConciliada tranDesconciliar;

    public List<ReferenciaNoConciliada> listaTransaccionesConciliadas = new List<ReferenciaNoConciliada>();
    public List<ReferenciaConciliada> listaReferenciaConciliada = new List<ReferenciaConciliada>();
    public List<ReferenciaConciliadaPedido> listaReferenciaConciliadaPedidos = new List<ReferenciaConciliadaPedido>();
    public List<DatosArchivo> listArchivosInternos = new List<DatosArchivo>();

    public DataTable tblDetalleTransaccionConciliada;
    public List<ListaCombo> listCamposDestino = new List<ListaCombo>();
    public string DiferenciaDiasMaxima, DiferenciaDiasMinima, DiferenciaCentavosMaxima, DiferenciaCentavosMinima;
    public int corporativoConciliacion, añoConciliacion, folioConciliacion, folioExterno, sucursalConciliacion;
    public short mesConciliacion, tipoConciliacion, grupoConciliacion;

    private List<ListaCombo> listTipoFuenteInformacionExternoInterno = new List<ListaCombo>();
    public List<ListaCombo> listFoliosInterno = new List<ListaCombo>();


    private DataTable tblDestinoDetalleInterno;
    private List<DatosArchivoDetalle> listaDestinoDetalleInterno = new List<DatosArchivoDetalle>();

    private bool activepaging = true;
    public bool ActivePaging
    {
        get { return activaPaginacion(); }
        //set { activepaging = value; }
    }

    public int indiceExternoSeleccionado = 0;

    #endregion

    public bool activaPaginacion()
    {
        SeguridadCB.Public.Parametros parametros;
        parametros = (SeguridadCB.Public.Parametros)HttpContext.Current.Session["Parametros"];
        AppSettingsReader settings = new AppSettingsReader();
        bool activar;

        activar = parametros.ValorParametro(Convert.ToSByte(settings.GetValue("Modulo", typeof(sbyte))), "ESTADOPAGINADORES") == "1";
        usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
        if (usuario.Area == 8) //el usuario es de metropoli
            activar = parametros.ValorParametro(Convert.ToSByte(settings.GetValue("Modulo", typeof(sbyte))), "METROPOLIPAGINADORES") == "1";

        return activar;
    }

    protected override void OnPreInit(EventArgs e)
    {
        if (HttpContext.Current.Session["Operaciones"] == null)
            Response.Redirect("~/Acceso/Acceso.aspx", true);
        else
            operaciones = (SeguridadCB.Public.Operaciones)HttpContext.Current.Session["Operaciones"];
    }
    private short ObtieneFormaConciliacion()
    {
        short formaConciliacion = Convert.ToSByte(Request.QueryString["FormaConciliacion"]);
        if (formaConciliacion == 0)
        {
            formaConciliacion = 1;
        }
        return formaConciliacion;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["BLOQUEO_ORIGEN"] = "CANTIDADCONCUERDA";
        short _FormaConciliacion = ObtieneFormaConciliacion();
        Conciliacion.RunTime.App.ImplementadorMensajes.ContenedorActual = this;
        try
        {
            Conciliacion.RunTime.App.ImplementadorMensajes.ContenedorActual = this;

            if (HttpContext.Current.Request.UrlReferrer != null)
            {
                if ((!HttpContext.Current.Request.UrlReferrer.AbsoluteUri.Contains("SitioConciliacion")) ||
                    (HttpContext.Current.Request.UrlReferrer.AbsoluteUri.Contains("Acceso.aspx")))
                {
                    HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
                    HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
                }
            }
            if (true)
                LlenaGridViewDestinoDetalleInterno();

            wucBuscadorPagoEstadoCuenta.Contenedor = mpeBuscadorPagoEdoCta;

            if (!Page.IsPostBack)
            {
                usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];

                corporativoConciliacion = Convert.ToInt32(Request.QueryString["Corporativo"]);
                sucursalConciliacion = Convert.ToInt16(Request.QueryString["Sucursal"]);
                añoConciliacion = Convert.ToInt32(Request.QueryString["Año"]);
                folioConciliacion = Convert.ToInt32(Request.QueryString["Folio"]);
                mesConciliacion = Convert.ToSByte(Request.QueryString["Mes"]);
                tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);
                grupoConciliacion = Convert.ToSByte(Request.QueryString["GrupoConciliacion"]);

                SolicitudConciliacion objSolicitdConciliacion = new SolicitudConciliacion();
                objSolicitdConciliacion.TipoConciliacion = tipoConciliacion;
                objSolicitdConciliacion.FormaConciliacion = _FormaConciliacion;

                activepaging = activaPaginacion();
                CargarRangoDiasDiferenciaGrupo(grupoConciliacion);

                Carga_SucursalCorporativo(corporativoConciliacion);
                Carga_FormasConciliacion(tipoConciliacion);
                Carga_StatusConcepto(Consultas.ConfiguracionStatusConcepto.ConEtiquetas);

                LlenarBarraEstado();
                Consulta_ConciliarPorCantidad(corporativoConciliacion, sucursalConciliacion, añoConciliacion,
                    mesConciliacion, folioConciliacion, tipoConciliacion, Convert.ToSByte(txtDias.Text),
                    Convert.ToDecimal(txtDiferencia.Text), Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value));

                //CARGAR LAS TRANSACCIONES CONCILIADAS POR EL CRITERIO DE AUTOCONCILIACIÓN
                if (ddlCriteriosConciliacion.SelectedValue != "")
                    Consulta_TransaccionesConciliadas(corporativoConciliacion, sucursalConciliacion, añoConciliacion,
                                                      mesConciliacion, folioConciliacion, Convert.ToInt32(ddlCriteriosConciliacion.SelectedValue));
                GenerarTablaConciliados();
                LlenaGridViewConciliadas();
                /*if (tipoConciliacion == 2 || tipoConciliacion == 6)
                {

                    lblPedidos.Visible = true;
                    //tdExportar.Attributes.Add("class", "iconoOpcion bg-color-grisClaro02");
                    txtDias.Enabled = tdEtiquetaMontoIn.Visible = tdMontoIn.Visible = false; //=imgExportar.Enabled
                    btnActualizarConfig.ValidationGroup = "CantidadPedidos";
                    rvDiferencia.ValidationGroup = "CantidadPedidos";
                    rfvDiferenciaVacio.ValidationGroup = "CantidadPedidos";
                    GenerarTablaReferenciasAConciliarPedidos();
                }
                else
                {
                    GenerarTablaReferenciasAConciliarInternos();
                    lblArchivosInternos.Visible = true;
                    btnActualizarConfig.ValidationGroup = "CantidadArchivos";
                }*/

                if (objSolicitdConciliacion.ConsultaPedido())
                {
                    lblPedidos.Visible = true;
                    tdEtiquetaMontoIn.Visible = tdMontoIn.Visible = false; //=imgExportar.Enabled
                    btnActualizarConfig.ValidationGroup = "CantidadPedidos";
                    rvDiferencia.ValidationGroup = "CantidadPedidos";
                    rfvDiferenciaVacio.ValidationGroup = "CantidadPedidos";
                    GenerarTablaReferenciasAConciliarPedidos();
                }

                if (objSolicitdConciliacion.ConsultaArchivo())
                {
                    GenerarTablaReferenciasAConciliarInternos();
                    lblArchivosInternos.Visible = true;
                    btnActualizarConfig.ValidationGroup = "CantidadArchivos";
                    HttpContext.Current.Session["SolicitdConciliacionConsultaArchivo"] = 1;
                }
                else
                    HttpContext.Current.Session["SolicitdConciliacionConsultaArchivo"] = 0;

                txtDias.Enabled = true;

                LlenaGridViewReferenciasConciliadas(tipoConciliacion);
                activarImportacion(tipoConciliacion);
            }
        }
        catch (SqlException ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);

            if (ex.Class >= 20)
            {
                Response.Redirect("~/Inicio.aspx", true);
            }
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

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
    //Limpiar las variables de Session
    public void limpiarVariablesSession()
    {
        //Eliminar las variables de Session utilizadas en la Vista
        HttpContext.Current.Session["CONCILIADAS"] = null;
        HttpContext.Current.Session["TAB_CONCILIADAS"] = null;
        HttpContext.Current.Session["TAB_REF_CONCILIAR"] = null;
        HttpContext.Current.Session["POR_CONCILIAR"] = null;
        HttpContext.Current.Session["RepDoc"] = null;
        HttpContext.Current.Session["ParametrosReporte"] = null;
        HttpContext.Current.Session["NUEVOS_INTERNOS"] = null;
        HttpContext.Current.Session["DETALLEINTERNO"] = null;

        HttpContext.Current.Session.Remove("CONCILIADAS");
        HttpContext.Current.Session.Remove("TAB_CONCILIADAS");
        HttpContext.Current.Session.Remove("TAB_REF_CONCILIAR");
        HttpContext.Current.Session.Remove("POR_CONCILIAR");
        HttpContext.Current.Session.Remove("RepDoc");
        HttpContext.Current.Session.Remove("ParametrosReporte");
        HttpContext.Current.Session.Remove("NUEVOS_INTERNOS");
        HttpContext.Current.Session.Remove("DETALLEINTERNO");
    }


    //Cargar Rango DiasMaximo-Minimio-Default
    public void CargarRangoDiasDiferenciaGrupo(short grupoC)
    {
        try
        {
            GrupoConciliacionDiasDiferencia gcd = App.GrupoConciliacionDias(grupoC);
            if (!gcd.CargarDatos())
            {
                App.ImplementadorMensajes.MostrarMensaje("Conflicto al leer Grupo Conciliación");
                return;
            }
            txtDias.Text = Convert.ToString(gcd.DiferenciaDiasDefault);
            txtDiferencia.Text = Convert.ToString(gcd.DiferenciaCentavosDefault);

            DiferenciaDiasMaxima = Convert.ToString(gcd.DiferenciaDiasMaxima);
            DiferenciaDiasMinima = Convert.ToString(gcd.DiferenciaDiasMinima);
            DiferenciaCentavosMaxima = Convert.ToString(gcd.DiferenciaCentavosMaxima);
            DiferenciaCentavosMinima = Convert.ToString(gcd.DiferenciaCentavosMinima);

            rvDias.MaximumValue = DiferenciaDiasMaxima;
            rvDias.MinimumValue = DiferenciaDiasMinima;
            rvDias.ErrorMessage = "[Dias entre " + DiferenciaDiasMinima + " - " + DiferenciaDiasMaxima + "]";

            rvDiferencia.MaximumValue = DiferenciaCentavosMaxima;
            rvDiferencia.MinimumValue = DiferenciaCentavosMinima;
            rvDiferencia.ErrorMessage = "[Diferencia entre " + DiferenciaCentavosMinima + " - " + DiferenciaCentavosMaxima + " pesos]";
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    //Colocar el DropDown de Criterios de Evaluacion en la Actual
    public void ActualizarCriterioEvaluacion()
    {
        try
        {
            ddlCriteriosConciliacion.SelectedValue = ddlCriteriosConciliacion.Items.FindByText("CANTIDAD CONCUERDA").Value;
        }
        catch (NullReferenceException ex)
        {
            if (ddlCriteriosConciliacion.Items.Count > 0)
            {
                ddlCriteriosConciliacion.SelectedIndex = 0;
            }
            else
            {
                throw new Exception("No existen elementos en el combo de formas de conciliación.");
            }
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
            lblMontoTotalExterno.Text = c.MontoTotalExterno.ToString("C2");
            lblMontoTotalInterno.Text = c.MontoTotalInterno.ToString("C2");
            lblStatusConciliacion.Text = c.StatusConciliacion;
            imgStatusConciliacion.ImageUrl = c.UbicacionIcono;

        }
        catch (SqlException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    /// <summary>
    /// Llena el Combo de Formas de Conciliacion
    /// </summary>
    public void Carga_FormasConciliacion(short tipoConciliacion)
    {
        try
        {
            //listFormasConciliacion = Conciliacion.RunTime.App.Consultas.ConsultaFormaConciliacion(tipoConciliacion);
            Enrutador objEnrutador = new Enrutador();
            listFormasConciliacion = objEnrutador.CargarFormaConciliacion(Convert.ToSByte(Request.QueryString["TipoConciliacion"]));
            this.ddlCriteriosConciliacion.DataSource = listFormasConciliacion;
            this.ddlCriteriosConciliacion.DataValueField = "Identificador";
            this.ddlCriteriosConciliacion.DataTextField = "Descripcion";
            this.ddlCriteriosConciliacion.DataBind();
            this.ddlCriteriosConciliacion.Dispose();
            ActualizarCriterioEvaluacion();
        }
        catch (SqlException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// Llena el Combo de Sucurales según Corporativo
    /// </summary>
    public void Carga_SucursalCorporativo(int corporativo)
    {
        try
        {
            listSucursales = Conciliacion.RunTime.App.Consultas.ConsultaSucursales(Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionIden0.Sin0, corporativo);
            this.ddlSucursal.DataSource = this.ddlSucursalInterno.DataSource = listSucursales;
            this.ddlSucursal.DataValueField = this.ddlSucursalInterno.DataValueField = "Identificador";
            this.ddlSucursal.DataTextField = this.ddlSucursalInterno.DataTextField = "Descripcion";

            this.ddlSucursal.DataBind();
            this.ddlSucursal.Dispose();
            this.ddlSucursalInterno.DataBind();
            this.ddlSucursalInterno.Dispose();

        }
        catch (SqlException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// Llena el Combo de Sucurales según Corporativo
    /// </summary>
    public void Carga_StatusConcepto(Consultas.ConfiguracionStatusConcepto cConcepto)
    {
        try
        {
            listStatusConcepto = Conciliacion.RunTime.App.Consultas.ConsultaStatusConcepto(cConcepto);
            this.ddlStatusConcepto.DataSource = listStatusConcepto;
            this.ddlStatusConcepto.DataValueField = "Identificador";
            this.ddlStatusConcepto.DataTextField = "Descripcion";
            this.ddlStatusConcepto.DataBind();
            this.ddlStatusConcepto.Dispose();
        }
        catch (SqlException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// Cargar campos de Filtro y Busqueda externo
    /// </summary>
    public void cargar_ComboCampoFiltroDestino(int tConciliacion, string filtrarEn)
    {
        try
        {
            listCamposDestino = filtrarEn.Equals("Externos") || filtrarEn.Equals("Conciliados")
                                            ? Conciliacion.RunTime.App.Consultas.ConsultaDestino()
                                            : (tConciliacion != 2
                                                   ? Conciliacion.RunTime.App.Consultas.ConsultaDestino()
                                                   : Conciliacion.RunTime.App.Consultas.ConsultaDestinoPedido());


        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }

    }
    //Enlazar Campo a Filtrar
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

    //Consulta transacciones conciliadas
    public
    void Consulta_TransaccionesConciliadas(int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion, int formaconciliacion)
    {
        SeguridadCB.Seguridad seguridad = new SeguridadCB.Seguridad();
        System.Data.SqlClient.SqlConnection Connection = seguridad.Conexion;
        if (Connection.State == ConnectionState.Closed)
        {
            seguridad.Conexion.Open();
            Connection = seguridad.Conexion;
        }
        try
        {
            listaTransaccionesConciliadas = Conciliacion.RunTime.App.Consultas.ConsultaTransaccionesConciliadas(
                                            corporativoconciliacion,
                                            sucursalconciliacion,
                                            añoconciliacion,
                                            mesconciliacion,
                                            folioconciliacion,
                                            Convert.ToInt16(ddlCriteriosConciliacion.SelectedValue));
            //Guardar en Session la Lista de TransaccionesConciliadas para la Forma: CANTIDAD CONCUERDA
            Session["CONCILIADAS"] = listaTransaccionesConciliadas;
        }
        catch (SqlException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //Genera la tabla de transacciones Conciliadas
    public void GenerarTablaConciliados()
    {
        try
        {
            tblTransaccionesConciliadas = new DataTable("TransaccionesConciladas");
            tblTransaccionesConciliadas.Columns.Add("FolioExt", typeof(int));
            tblTransaccionesConciliadas.Columns.Add("FolioConciliacion", typeof(int));
            tblTransaccionesConciliadas.Columns.Add("CorporativoConciliacion", typeof(int));
            tblTransaccionesConciliadas.Columns.Add("SucursalConciliacion", typeof(int));
            tblTransaccionesConciliadas.Columns.Add("AñoConciliacion", typeof(int));
            tblTransaccionesConciliadas.Columns.Add("MesConciliacion", typeof(short));
            tblTransaccionesConciliadas.Columns.Add("Secuencia", typeof(int));
            tblTransaccionesConciliadas.Columns.Add("RFCTercero", typeof(string));
            tblTransaccionesConciliadas.Columns.Add("Retiro", typeof(decimal));
            tblTransaccionesConciliadas.Columns.Add("Referencia", typeof(string));
            tblTransaccionesConciliadas.Columns.Add("NombreTercero", typeof(string));
            tblTransaccionesConciliadas.Columns.Add("Descripcion", typeof(string));
            tblTransaccionesConciliadas.Columns.Add("Deposito", typeof(decimal));
            tblTransaccionesConciliadas.Columns.Add("Cheque", typeof(string));
            tblTransaccionesConciliadas.Columns.Add("FMovimiento", typeof(DateTime));
            tblTransaccionesConciliadas.Columns.Add("FOperacion", typeof(DateTime));
            tblTransaccionesConciliadas.Columns.Add("MontoConciliado", typeof(decimal));
            tblTransaccionesConciliadas.Columns.Add("Concepto", typeof(string));
            tblTransaccionesConciliadas.Columns.Add("SerieFactura", typeof(string));
            tblTransaccionesConciliadas.Columns.Add("ClienteReferencia", typeof(string));

            foreach (ReferenciaNoConciliada rc in listaTransaccionesConciliadas)
            {
                tblTransaccionesConciliadas.Rows.Add(
                    rc.Folio,
                    rc.FolioConciliacion,
                    rc.Corporativo,
                    rc.Sucursal,
                    rc.Año,
                    rc.MesConciliacion,
                    rc.Secuencia,
                    rc.RFCTercero,
                    rc.Retiro,
                    rc.Referencia,
                    rc.NombreTercero,
                    rc.Descripcion,
                    rc.Deposito,
                    rc.Cheque,
                    rc.FMovimiento,
                    rc.FOperacion,
                    rc.MontoConciliado,
                    rc.Concepto,
                    rc.SerieFactura,
                    rc.ClienteReferencia);
            }

            HttpContext.Current.Session["TAB_CONCILIADAS"] = tblTransaccionesConciliadas;
            ViewState["TAB_CONCILIADAS"] = tblTransaccionesConciliadas;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //Llena el Gridview Transacciones Concilidadas
    private void LlenaGridViewConciliadas()
    {
        try
        {
            DataTable tablaConciliadas = (DataTable)HttpContext.Current.Session["TAB_CONCILIADAS"];
            grvConciliadas.DataSource = tablaConciliadas;
            grvConciliadas.DataBind();
        }
        catch (Exception ex)
        {

            throw ex;
        }


    }
    //Llena el Gridview Transacciones Concilidadas
    private void LlenaGridViewConciliadasAjustado()
    {
        DataTable tablaConciliadas = ViewState["TAB_CONCILIADAS"] as DataTable;
        grvConciliadas.DataSource = tablaConciliadas;
        grvConciliadas.DataBind();

    }
    //Genera la tabla DetalleTransaccionesConciliadas
    public void GeneraTablaDetalleArchivosInternos(ReferenciaNoConciliada trConciliada)
    {
        try
        {
            tblDetalleTransaccionConciliada = new DataTable("DetalleTransaccionConciliada");
            if (trConciliada.ConInterno)
            {
                tblDetalleTransaccionConciliada.Columns.Add("SecuenciaInterno", typeof(int));
                tblDetalleTransaccionConciliada.Columns.Add("FolioInterno", typeof(int));
                tblDetalleTransaccionConciliada.Columns.Add("FMovimientoInt", typeof(DateTime));
                tblDetalleTransaccionConciliada.Columns.Add("FOperacionInt", typeof(DateTime));
                tblDetalleTransaccionConciliada.Columns.Add("MontoInterno", typeof(Decimal));
                tblDetalleTransaccionConciliada.Columns.Add("ConceptoInterno", typeof(string));
            }
            else
            {
                tblDetalleTransaccionConciliada.Columns.Add("Pedido", typeof(int));
                tblDetalleTransaccionConciliada.Columns.Add("PedidoReferencia", typeof(string));
                tblDetalleTransaccionConciliada.Columns.Add("AñoPed", typeof(int));
                tblDetalleTransaccionConciliada.Columns.Add("Celula", typeof(int));
                tblDetalleTransaccionConciliada.Columns.Add("Cliente", typeof(string));
                tblDetalleTransaccionConciliada.Columns.Add("Nombre", typeof(string));
                tblDetalleTransaccionConciliada.Columns.Add("Total", typeof(decimal));
                tblDetalleTransaccionConciliada.Columns.Add("ConceptoPedido", typeof(string));
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    public void ConsultaDetalleTransaccionConciliada(ReferenciaNoConciliada trConciliada)
    {
        try
        {
            if (trConciliada.ConInterno)
            {
                foreach (ReferenciaConciliada r in trConciliada.ListaReferenciaConciliada)
                {
                    tblDetalleTransaccionConciliada.Rows.Add(
                        r.SecuenciaInterno,
                        r.FolioInterno,
                        r.FMovimientoInt,
                        r.FOperacionInt,
                        r.MontoInterno,
                        r.ConceptoInterno);
                }
            }
            else
            {
                foreach (ReferenciaConciliadaPedido r in trConciliada.ListaReferenciaConciliada)
                {
                    tblDetalleTransaccionConciliada.Rows.Add(
                        r.Pedido,
                        r.PedidoReferencia,
                        r.AñoPedido,
                        r.CelulaPedido,
                        r.Cliente,
                        r.Nombre,
                        r.Total,
                        r.ConceptoPedido
                        );
                }
            }
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }
    public void LlenarGridDetalleInterno(ReferenciaNoConciliada trConciliada)
    {
        try
        {
            if (trConciliada.ConInterno)
            {
                grvDetalleArchivoInterno.DataSource = tblDetalleTransaccionConciliada;
                grvDetalleArchivoInterno.DataBind();
            }
            else
            {
                grvDetallePedidoInterno.DataSource = tblDetalleTransaccionConciliada;
                grvDetallePedidoInterno.DataBind();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    public void GenerarTablaReferenciasAConciliarInternos()//Genera la tabla Referencias a Conciliar Internos
    {
        try
        {
            tblReferenciasAConciliar = new DataTable("ReferenciasAConciliarInternas");
            //Campos Externos
            tblReferenciasAConciliar.Columns.Add("Selecciona", typeof(bool));
            tblReferenciasAConciliar.Columns.Add("FolioExt", typeof(int));
            tblReferenciasAConciliar.Columns.Add("SecuenciaExt", typeof(int));
            tblReferenciasAConciliar.Columns.Add("RFCTerceroExt", typeof(string));
            tblReferenciasAConciliar.Columns.Add("RetiroExt", typeof(decimal));
            tblReferenciasAConciliar.Columns.Add("ReferenciaExt", typeof(string));
            tblReferenciasAConciliar.Columns.Add("NombreTerceroExt", typeof(string));
            tblReferenciasAConciliar.Columns.Add("DescripcionExt", typeof(string));
            tblReferenciasAConciliar.Columns.Add("DepositoExt", typeof(decimal));
            tblReferenciasAConciliar.Columns.Add("ChequeExt", typeof(string));
            tblReferenciasAConciliar.Columns.Add("FMovimientoExt", typeof(DateTime));
            tblReferenciasAConciliar.Columns.Add("FOperacionExt", typeof(DateTime));
            tblReferenciasAConciliar.Columns.Add("MontoConciliadoExt", typeof(decimal));
            tblReferenciasAConciliar.Columns.Add("ConceptoExt", typeof(string));
            //Campos Internos
            tblReferenciasAConciliar.Columns.Add("SecuenciaInt", typeof(int));
            tblReferenciasAConciliar.Columns.Add("FolioInt", typeof(int));
            tblReferenciasAConciliar.Columns.Add("RFCTerceroInt", typeof(string));
            tblReferenciasAConciliar.Columns.Add("RetiroInt", typeof(decimal));
            tblReferenciasAConciliar.Columns.Add("ReferenciaInt", typeof(string));
            tblReferenciasAConciliar.Columns.Add("NombreTerceroInt", typeof(string));
            tblReferenciasAConciliar.Columns.Add("DescripcionInt", typeof(string));
            tblReferenciasAConciliar.Columns.Add("DepositoInt", typeof(decimal));
            tblReferenciasAConciliar.Columns.Add("ChequeInt", typeof(string));
            tblReferenciasAConciliar.Columns.Add("FMovimientoInt", typeof(DateTime));
            tblReferenciasAConciliar.Columns.Add("FOperacionInt", typeof(DateTime));
            tblReferenciasAConciliar.Columns.Add("MontoInt", typeof(decimal));
            tblReferenciasAConciliar.Columns.Add("ConceptoInt", typeof(string));
            tblReferenciasAConciliar.Columns.Add("SerieFactura", typeof(string));
            tblReferenciasAConciliar.Columns.Add("ClienteReferencia", typeof(string));

            foreach (ReferenciaConciliada rc in listaReferenciaConciliada)
            {
                tblReferenciasAConciliar.Rows.Add(
                    rc.Selecciona,
                    rc.Folio,
                    rc.Secuencia,
                    rc.RFCTercero,
                    rc.Retiro,
                    rc.Referencia,
                    rc.NombreTercero,
                    rc.Descripcion,
                    rc.Deposito,
                    rc.Cheque,
                    rc.FMovimiento,
                    rc.FOperacion,
                    rc.MontoConciliado,
                    rc.Concepto,
                    rc.SecuenciaInterno,
                    rc.FolioInterno,
                    rc.RFCTerceroInterno,
                    rc.RetiroInterno,
                    rc.ReferenciaInterno,
                    rc.NombreTerceroInterno,
                    rc.DescripcionInterno,
                    rc.DepositoInterno,
                    rc.ChequeInterno,
                    rc.FMovimientoInt,
                    rc.FOperacionInt,
                    rc.MontoInterno,
                    rc.ConceptoInterno,
                    rc.SerieFactura,
                    rc.ClienteReferencia);
            }
            HttpContext.Current.Session["TAB_REF_CONCILIAR"] = tblReferenciasAConciliar;
            ViewState["TAB_REF_CONCILIAR"] = tblReferenciasAConciliar;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void GenerarTablaReferenciasAConciliarPedidos()//Genera la tabla Referencias a Conciliar Internos
    {
        try
        {
            tblReferenciasAConciliar = new DataTable("ReferenciasAConciliarPedidos");
            //Campos Externos
            tblReferenciasAConciliar.Columns.Add("Selecciona", typeof(bool));
            tblReferenciasAConciliar.Columns.Add("Secuencia", typeof(int));
            tblReferenciasAConciliar.Columns.Add("FolioExt", typeof(int));
            tblReferenciasAConciliar.Columns.Add("RFCTercero", typeof(string));
            tblReferenciasAConciliar.Columns.Add("Retiro", typeof(decimal));
            tblReferenciasAConciliar.Columns.Add("Referencia", typeof(string));
            tblReferenciasAConciliar.Columns.Add("NombreTercero", typeof(string));
            tblReferenciasAConciliar.Columns.Add("Deposito", typeof(decimal));
            tblReferenciasAConciliar.Columns.Add("Cheque", typeof(string));
            tblReferenciasAConciliar.Columns.Add("FMovimiento", typeof(DateTime));
            tblReferenciasAConciliar.Columns.Add("FOperacion", typeof(DateTime));
            tblReferenciasAConciliar.Columns.Add("MontoConciliado", typeof(decimal));
            tblReferenciasAConciliar.Columns.Add("Concepto", typeof(string));
            tblReferenciasAConciliar.Columns.Add("Descripcion", typeof(string));
            //Campos Pedidos
            tblReferenciasAConciliar.Columns.Add("Pedido", typeof(int));
            tblReferenciasAConciliar.Columns.Add("PedidoReferencia", typeof(string));
            tblReferenciasAConciliar.Columns.Add("AñoPed", typeof(int));
            tblReferenciasAConciliar.Columns.Add("Celula", typeof(int));
            tblReferenciasAConciliar.Columns.Add("Cliente", typeof(string));
            tblReferenciasAConciliar.Columns.Add("Nombre", typeof(string));
            tblReferenciasAConciliar.Columns.Add("Total", typeof(decimal));
            tblReferenciasAConciliar.Columns.Add("ConceptoPedido", typeof(string));
            tblReferenciasAConciliar.Columns.Add("SerieFactura", typeof(string));
            tblReferenciasAConciliar.Columns.Add("ClienteReferencia", typeof(string));

            foreach (ReferenciaConciliadaPedido rc in listaReferenciaConciliadaPedidos)
            {
                tblReferenciasAConciliar.Rows.Add(
                    rc.Selecciona,
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
                    rc.Pedido,
                    rc.PedidoReferencia,
                    rc.AñoPedido,
                    rc.CelulaPedido,
                    rc.Cliente,
                    rc.Nombre,
                    rc.Total,
                    rc.ConceptoPedido,
                    rc.FolioSat + rc.SerieSat, //"HOLA FACTURA",
                    rc.Cliente//"CLIENTE REF"
                    );
            }
            HttpContext.Current.Session["TAB_REF_CONCILIAR"] = tblReferenciasAConciliar;
            ViewState["TAB_REF_CONCILIAR"] = tblReferenciasAConciliar;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void LlenaGridViewReferenciasConciliadas(short tipoConciliacion)//Llena el gridview con las conciliaciones antes leídas
    {
        SolicitudConciliacion objSolicitdConciliacion = new SolicitudConciliacion();
        objSolicitdConciliacion.TipoConciliacion = tipoConciliacion;
        objSolicitdConciliacion.FormaConciliacion = ObtieneFormaConciliacion();
        try
        {
            DataTable tablaReferenacias = (DataTable)HttpContext.Current.Session["TAB_REF_CONCILIAR"];
            if (tipoConciliacion == 2)
            {
                grvCantidadConcuerdanPedidos.DataSource = tablaReferenacias;
                grvCantidadConcuerdanPedidos.DataBind();
            }
            else
            {
                grvCantidadConcuerdanArchivos.DataSource = tablaReferenacias;
                grvCantidadConcuerdanArchivos.DataBind();
            }
            if (objSolicitdConciliacion.ConsultaPedido())
            {
                UnCheckBloquedos(grvCantidadConcuerdanPedidos);
                bloqueaTodoLoSeleccionado(grvCantidadConcuerdanPedidos);
            }
            if (objSolicitdConciliacion.ConsultaArchivo())
            {
                UnCheckBloquedos(grvCantidadConcuerdanArchivos);
                bloqueaTodoLoSeleccionado(grvCantidadConcuerdanArchivos);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public void Consulta_ConciliarPorCantidad(int corporativo, int sucursal, int año, short mes, int folio, int tipoConciliacion, short dias, decimal centavos, int statusConcepto)
    {
        try
        {
            SeguridadCB.Seguridad seguridad = new SeguridadCB.Seguridad();
            System.Data.SqlClient.SqlConnection Connection = seguridad.Conexion;
            if (Connection.State == ConnectionState.Closed)
            {
                seguridad.Conexion.Open();
                Connection = seguridad.Conexion;
            }
            if (tipoConciliacion == 2)
            {
                listaReferenciaConciliadaPedidos =
                    Conciliacion.RunTime.App.Consultas.ConsultaConciliarPedidoCantidad(corporativo, sucursal, año, mes,
                        folio, centavos, statusConcepto);
                Session["POR_CONCILIAR"] = listaReferenciaConciliadaPedidos;
            }

            else
            {
                listaReferenciaConciliada =
                    Conciliacion.RunTime.App.Consultas.ConsultaConciliarArchivosCantidad(corporativo, sucursal, año, mes,
                        folio, dias, centavos,
                        statusConcepto);
                Session["POR_CONCILIAR"] = listaReferenciaConciliada;
            }
        }
        catch (SqlException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// Verifica si Existe algún CheckBox Selecionado dentro del GridView[Copiar]
    /// </summary>
    public List<GridViewRow> leerReferenciasSeleccionadasPagina(int tipoConciliacion)
    {
        return tipoConciliacion == 2
                   ? grvCantidadConcuerdanPedidos.Rows.Cast<GridViewRow>().Where(fila => fila.RowType == DataControlRowType.DataRow && (fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked)).ToList()
                   : grvCantidadConcuerdanArchivos.Rows.Cast<GridViewRow>().Where(fila => fila.RowType == DataControlRowType.DataRow && (fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked)).ToList();
    }
    protected void OnCheckedChangedArchivos(object sender, EventArgs e)
    {
        CheckBox chk = (sender as CheckBox);
        listaReferenciaConciliada = Session["POR_CONCILIAR"] as List<ReferenciaConciliada>;
        if (chk.ID == "chkAllFolios")
        {
            foreach (GridViewRow fila in grvCantidadConcuerdanArchivos.Rows.Cast<GridViewRow>()
                                                 .Where(fila => fila.RowType == DataControlRowType.DataRow))
                fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked = chk.Checked;
            UnCheckBloquedos(grvCantidadConcuerdanArchivos);
            bloqueaTodoLoSeleccionado(grvCantidadConcuerdanArchivos);
        }
    }
    protected void OnCheckedChangedPedidos(object sender, EventArgs e)
    {
        CheckBox chk = (sender as CheckBox);
        listaReferenciaConciliadaPedidos = Session["POR_CONCILIAR"] as List<ReferenciaConciliadaPedido>;
        if (chk.ID == "chkAllFolios")
            foreach (GridViewRow fila in grvCantidadConcuerdanPedidos.Rows)
                if (fila.RowType == DataControlRowType.DataRow)
                    fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked = chk.Checked;
        UnCheckBloquedos(grvCantidadConcuerdanPedidos);
        bloqueaTodoLoSeleccionado(grvCantidadConcuerdanPedidos);
    }
    //Metodos Busqueda
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
    public string pintarBusqueda(Match m)
    {
        return "<span class=marcarBusqueda>" + m.Value + "</span>";
    }
    //Activar los textbox segun sea el tipo de campa seleccionado en el filtro
    //public void activarControles(string tipoCampo)
    //{
    //    switch (tipoCampo)
    //    {
    //        case "Numero":
    //            this.txtValorNumericoFiltro.Visible = true;
    //            this.txtValorCadenaFiltro.Visible = false;
    //            this.txtValorFechaFiltro.Visible = false;
    //            this.txtValorNumericoFiltro.Text = "0";
    //            this.txtValorCadenaFiltro.Text = String.Empty;
    //            this.txtValorFechaFiltro.Text = String.Empty;
    //            break;
    //        case "Fecha":
    //            this.txtValorFechaFiltro.Visible = true;
    //            this.txtValorNumericoFiltro.Visible = false;
    //            this.txtValorCadenaFiltro.Visible = false;
    //            this.txtValorFechaFiltro.Text = String.Empty;
    //            this.txtValorNumericoFiltro.Text = String.Empty;
    //            this.txtValorCadenaFiltro.Text = String.Empty;
    //            break;
    //        case "Cadena":
    //            this.txtValorCadenaFiltro.Visible = true;
    //            this.txtValorNumericoFiltro.Visible = false;
    //            this.txtValorFechaFiltro.Visible = false;
    //            this.txtValorCadenaFiltro.Text = String.Empty;
    //            this.txtValorNumericoFiltro.Text = String.Empty;
    //            this.txtValorFechaFiltro.Text = String.Empty;
    //            break;
    //    }

    //}
    public string tipoCampoSeleccionado()
    {
        return listCamposDestino[ddlCampoFiltrar.SelectedIndex].Campo1;
    }
    public string valorFiltro(string tipoCampo)
    {
        try
        {
            switch (tipoCampo)
            {
                case "Cadena":
                    return txtValor.Text;
                    break;
                case "Numero":
                    decimal num = Convert.ToDecimal(txtValor.Text);
                    return num.ToString();
                    break;
                case "Fecha":
                    DateTime fecha = Convert.ToDateTime(txtValor.Text);
                    return fecha.ToString();
                    break;
            }

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Verifique:\n- Valor no valido por tipo de Campo seleccionado.");
        }
        return "";
        //return tipoCampo.Equals("Cadena")
        //           ? txtValorCadenaFiltro.Text
        //           : (tipoCampo.Equals("Fecha") ? txtValorFechaFiltro.Text : txtValorNumericoFiltro.Text);

    }
    public void InicializarControlesFiltro()
    {
        ddlCampoFiltrar.SelectedIndex = ddlOperacion.SelectedIndex = 0;
        txtValor.Text = String.Empty;
    }
    private void FiltrarCampo(string valorFiltro, string filtroEn)
    {
        try
        {
            DataTable dt = filtroEn.Equals("Conciliados")
                               ? (DataTable)HttpContext.Current.Session["TAB_CONCILIADAS"]
                               : (DataTable)HttpContext.Current.Session["TAB_REF_CONCILIAR"];

            DataView dv = new DataView(dt);
            string SearchExpression = String.Empty;
            if (!String.IsNullOrEmpty(valorFiltro))
            {
                SearchExpression = string.Format(
                    ddlOperacion.SelectedItem.Value == "LIKE"
                        ? !filtroEn.Equals("Conciliados") && tipoConciliacion != 2
                            ? filtroEn.Equals("Externos")
                                ? "{0}Ext {1} '%{2}%'"
                                : "{0}Int {1} '%{2}%'"
                            : "{0} {1} '%{2}%'"
                         : !filtroEn.Equals("Conciliados") && tipoConciliacion != 2
                            ? filtroEn.Equals("Externos")
                                ? "{0}Ext {1} '{2}'"
                                : "{0}Int {1} '{2}'"
                            : "{0} {1} '{2}'", ddlCampoFiltrar.SelectedItem.Text,
                    ddlOperacion.SelectedItem.Value, valorFiltro);
            }
            if (dv.Count <= 0) return;
            dv.RowFilter = SearchExpression;

            if (filtroEn.Equals("Conciliados"))
            {
                ViewState["TAB_CONCILIADAS"] = dv.ToTable();
                //grvConciliadas.DataSource = ViewState["TAB_CONCILIADAS"] as DataTable;
                //grvConciliadas.DataBind();
                LlenaGridViewConciliadasAjustado();
            }
            else
            {
                if (tipoConciliacion == 2)
                {
                    ViewState["TAB_REF_CONCILIAR"] = dv.ToTable();
                    grvCantidadConcuerdanPedidos.DataSource = ViewState["TAB_REF_CONCILIAR"] as DataTable;
                    grvCantidadConcuerdanPedidos.DataBind();
                    //LlenaGridViewRefConciliarAjustado();
                }
                else
                {
                    ViewState["TAB_REF_CONCILIAR"] = dv.ToTable();
                    grvCantidadConcuerdanArchivos.DataSource = ViewState["TAB_REF_CONCILIAR"] as DataTable;
                    grvCantidadConcuerdanArchivos.DataBind();
                    //LlenaGridViewRefConciliarAjustado();
                }
            }
            mpeFiltrar.Hide();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Verifique:\na.-Valor no valido por tipo de Campo seleccionado.");
            mpeFiltrar.Hide();
        }

    }
    private string getSortDirectionString(string columna)
    {
        string sortDirection = "ASC";

        string sortExpression = ViewState["SortExpression"] as string;

        if (sortExpression != null)
        {
            if (sortExpression == columna)
            {
                string lastDirection = ViewState["SortDirection"] as string;
                if ((lastDirection != null) && (lastDirection == "ASC"))
                {
                    sortDirection = "DESC";
                }
            }
        }

        ViewState["SortDirection"] = sortDirection;
        ViewState["SortExpression"] = columna;
        return sortDirection;
    }


    //Crea la paginacion para Concilidos
    protected void grvConciliadas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager || (grvConciliadas.DataSource == null)) return;
        //TRAE EL TOTAL DE PAGINAS
        Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumPaginas");
        _TotalPags.Text = grvConciliadas.PageCount.ToString();

        //LLENA LA LISTA CON EL NUMERO DE PAGINAS
        DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownListConciliadas");
        for (int i = 1; i <= Convert.ToInt32(grvConciliadas.PageCount); i++)
        {
            list.Items.Add(i.ToString());
        }
        list.SelectedValue = (grvConciliadas.PageIndex + 1).ToString();
    }
    //Comando para desconciliar
    protected void grvConciliadas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (!e.CommandName.Equals("DESCONCILIAR")) return;
            Button imgDesconciliar = e.CommandSource as Button;
            GridViewRow gRowConciliado = (GridViewRow)(imgDesconciliar).Parent.Parent;

            int corporativoC = Convert.ToInt32(grvConciliadas.DataKeys[gRowConciliado.RowIndex].Values["CorporativoConciliacion"]);
            int sucursalC = Convert.ToInt32(grvConciliadas.DataKeys[gRowConciliado.RowIndex].Values["SucursalConciliacion"]);
            int añoC = Convert.ToInt32(grvConciliadas.DataKeys[gRowConciliado.RowIndex].Values["AñoConciliacion"]);
            int mesC = Convert.ToInt32(grvConciliadas.DataKeys[gRowConciliado.RowIndex].Values["MesConciliacion"]);
            int folioC = Convert.ToInt32(grvConciliadas.DataKeys[gRowConciliado.RowIndex].Values["FolioConciliacion"]);
            int folioEx = Convert.ToInt32(grvConciliadas.DataKeys[gRowConciliado.RowIndex].Values["FolioExt"]);
            int secuenciaEx = Convert.ToInt32(grvConciliadas.DataKeys[gRowConciliado.RowIndex].Values["Secuencia"]);
            //Leer la variable de Session : CONCILIADAS
            listaTransaccionesConciliadas = Session["CONCILIADAS"] as List<ReferenciaNoConciliada>;

            tranDesconciliar = listaTransaccionesConciliadas.Single(
                x => x.Corporativo == corporativoC &&
                     x.Sucursal == sucursalC &&
                     x.Año == añoC &&
                     x.MesConciliacion == mesC &&
                     x.FolioConciliacion == folioC &&
                     x.Folio == folioEx &&
                     x.Secuencia == secuenciaEx);

            tranDesconciliar.DesConciliar();
            //Leer Variables URL 
            cargarInfoConciliacionActual();


            Consulta_TransaccionesConciliadas(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, Convert.ToInt32(ddlCriteriosConciliacion.SelectedValue));
            GenerarTablaConciliados();
            LlenaGridViewConciliadas();
            LlenarBarraEstado();
            //Cargo de nuevo las ReferenciasCantidadConcuerda
            Consulta_ConciliarPorCantidad(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, tipoConciliacion, Convert.ToSByte(txtDias.Text),
                                                                Convert.ToDecimal(txtDiferencia.Text), Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value));
            if (tipoConciliacion == 2)
                GenerarTablaReferenciasAConciliarPedidos();
            else
                GenerarTablaReferenciasAConciliarInternos();
            LlenaGridViewReferenciasConciliadas(tipoConciliacion);
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }
    //Ver el detalle de la Transaccion Conciliada
    protected void grvConciliadas_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            int corporativoConcilacion = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["CorporativoConciliacion"]);
            int sucursalConciliacion = Convert.ToInt16(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["SucursalConciliacion"]);
            int añoConciliacion = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["AñoConciliacion"]);
            short mesConciliacion = Convert.ToSByte(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["MesConciliacion"]);
            int folioConciliacion = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["FolioConciliacion"]);
            int folioExterno = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["FolioExt"]);
            int secuenciaExterno = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["Secuencia"]);

            //Leer la variable de Session : CONCILIADAS
            listaTransaccionesConciliadas = Session["CONCILIADAS"] as List<ReferenciaNoConciliada>;
            ReferenciaNoConciliada tConciliada = listaTransaccionesConciliadas.Single(
                        x => x.Corporativo == corporativoConcilacion &&
                        x.Sucursal == sucursalConciliacion &&
                        x.Año == añoConciliacion &&
                        x.MesConciliacion == mesConciliacion &&
                        x.FolioConciliacion == folioConciliacion &&
                        x.Folio == folioExterno &&
                        x.Secuencia == secuenciaExterno);

            GeneraTablaDetalleArchivosInternos(tConciliada);
            ConsultaDetalleTransaccionConciliada(tConciliada);
            LlenarGridDetalleInterno(tConciliada);
            mpeLanzarDetalle.Show();
        }
        catch (Exception)
        {
            throw;
        }
    }
    //Paginacion de los Concilidos
    protected void grvConciliadas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvConciliadas.PageIndex = e.NewPageIndex;
            //LlenaGridViewConciliadas();
            LlenaGridViewConciliadasAjustado();
        }
        catch (Exception ex)
        {
            //App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }
    }
    //Llena el dropDown de  paginacion para Conciliados
    protected void paginasDropDownListConciliadas_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList oIraPag = sender as DropDownList;
        int iNumPag = 0;

        grvConciliadas.PageIndex = int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 &&
                                   iNumPag <= grvConciliadas.PageCount
                                       ? iNumPag - 1
                                       : 0;
        //LlenaGridViewConciliadas();
        LlenaGridViewConciliadasAjustado();
    }
    protected void grvCantidadConcuerdanArchivos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvCantidadConcuerdanArchivos.PageIndex = e.NewPageIndex;
            LlenaGridViewReferenciasConciliadas(tipoConciliacion);
        }
        catch (Exception)
        {

        }
    }
    protected void paginasDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag = 0;

        if (int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 && iNumPag <= grvCantidadConcuerdanArchivos.PageCount)

            grvCantidadConcuerdanArchivos.PageIndex = iNumPag - 1;

        else

            grvCantidadConcuerdanArchivos.PageIndex = 0;

        LlenaGridViewReferenciasConciliadas(tipoConciliacion);
    }

    protected void grvCantidadConcuerdanPedidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvCantidadConcuerdanPedidos.PageIndex = e.NewPageIndex;
            LlenaGridViewReferenciasConciliadas(tipoConciliacion);
        }
        catch (Exception)
        {

        }
    }
    protected void paginasDropDownListPedidos_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag = 0;

        if (int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 && iNumPag <= grvCantidadConcuerdanPedidos.PageCount)
        {
            grvCantidadConcuerdanPedidos.PageIndex = iNumPag - 1;
        }
        else
        {
            grvCantidadConcuerdanPedidos.PageIndex = 0;
        }


        LlenaGridViewReferenciasConciliadas(tipoConciliacion);
    }
    protected void btnIrBuscar_Click(object sender, EventArgs e)
    {
        if (ddlBuscarEn.SelectedItem.Value.Equals("Conciliados"))
        {
            //grvConciliadas.DataSource = ViewState["TAB_CONCILIADAS"] as DataTable;
            //grvConciliadas.DataBind();
            LlenaGridViewConciliadasAjustado();
        }
        else
        {
            if (tipoConciliacion == 2)
            {
                grvCantidadConcuerdanPedidos.DataSource = ViewState["TAB_REF_CONCILIAR"] as DataTable;
                grvCantidadConcuerdanPedidos.DataBind();
            }
            else
            {
                grvCantidadConcuerdanArchivos.DataSource = ViewState["TAB_REF_CONCILIAR"] as DataTable;
                grvCantidadConcuerdanArchivos.DataBind();
            }
        }
        mpeBuscar.Hide();
    }
    protected void grvCantidadConcuerdanArchivos_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='bg-color-grisClaro02'");
            e.Row.Attributes.Add("onmouseout", "this.className='bg-color-blanco'");
        }
    }
    protected void grvCantidadConcuerdanPedidos_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='bg-color-grisClaro02'");
            e.Row.Attributes.Add("onmouseout", "this.className='bg-color-blanco'");

        }
    }
    protected void grvConciliadas_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;

        e.Row.Attributes["onmouseover"] = "this.className='filaEncima '";
        e.Row.Attributes["onmouseout"] = "this.className='filaNormal'";
        e.Row.Attributes["onclick"] = "this.className='filaSeleccionada'";

    }

    //protected void ddlCampoFiltrar_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //activarControles(tipoCampoSeleccionado());
    //    //mpeFiltrar.Show();
    //}
    protected void btnIrFiltro_Click(object sender, EventArgs e)
    {
        //Leer el tipoConciliacion URL
        tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);

        cargar_ComboCampoFiltroDestino(tipoConciliacion, ddlFiltrarEn.SelectedItem.Value);

        FiltrarCampo(valorFiltro(tipoCampoSeleccionado()), ddlFiltrarEn.SelectedItem.Value);
        mpeFiltrar.Hide();
    }
    protected void ddlCriteriosConciliacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string criterioConciliacion = ddlCriteriosConciliacion.SelectedItem.Text.Equals("CANTIDAD CONCUERDA")
        //                                  ? "CantidadConcuerda" : ddlCriteriosConciliacion.SelectedItem.Text.Equals("CANTIDAD Y REFERENCIA CONCUERDA") ? "CantidadYRefernciaConcuerdan" :
        //                                                              ddlCriteriosConciliacion.SelectedItem.Text.Equals("UNO A VARIOS") ? "UnoAVarios" : "CopiaDeConciliacion";

        //Response.Redirect("~/Conciliacion/FormasConciliar/" + criterioConciliacion +
        //                              ".aspx?Folio=" + folioConciliacion + "&Corporativo=" + corporativoConciliacion +
        //                              "&Sucursal=" + sucursalConciliacion + "&Año=" + añoConciliacion + "&Mes=" +
        //                              mesConciliacion + "&TipoConciliacion=" + tipoConciliacion);
    }
    protected void grvCantidadConcuerdanArchivos_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtSortTable = ViewState["TAB_REF_CONCILIAR"] as DataTable;
        //DataTable dtSortTable = (DataTable)grvPedidos.DataSource;

        if (dtSortTable == null) return;
        string order = getSortDirectionString(e.SortExpression);
        dtSortTable.DefaultView.Sort = e.SortExpression + " " + order;
        //HttpContext.Current.Session["TAB_REF_CONCILIAR"] = dtSortTable;
        grvCantidadConcuerdanArchivos.DataSource = dtSortTable;
        grvCantidadConcuerdanArchivos.DataBind();
    }
    //protected void imgBuscar_Click(object sender, ImageClickEventArgs e)
    //{
    //    txtBuscar.Text = String.Empty;
    //    mpeBuscar.Show();
    //}
    protected void grvCantidadConcuerdanPedidos_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtSortTable = ViewState["TAB_REF_CONCILIAR"] as DataTable;
        if (dtSortTable == null) return;
        string order = getSortDirectionString(e.SortExpression);
        dtSortTable.DefaultView.Sort = e.SortExpression + " " + order;
        grvCantidadConcuerdanPedidos.DataSource = dtSortTable;
        grvCantidadConcuerdanPedidos.DataBind();
    }
    protected void btnIrFormaConciliacion_Click(object sender, EventArgs e)
    {

    }
    protected void grvConciliadas_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtSortTable = ViewState["TAB_CONCILIADAS"] as DataTable;
        //DataTable dtSortTable = (DataTable)grvPedidos.DataSource;

        if (dtSortTable == null) return;
        string order = getSortDirectionString(e.SortExpression);
        dtSortTable.DefaultView.Sort = e.SortExpression + " " + order;
        //HttpContext.Current.Session["TAB_CONCILIADAS"] = dtSortTable;
        ViewState["TAB_CONCILIADAS"] = dtSortTable;
        //grvConciliadas.DataSource = dtSortTable;
        //grvConciliadas.DataBind();
        LlenaGridViewConciliadasAjustado();
        //LlenaGridViewConciliadas();
    }
    protected void btnActualizarConfig_Click(object sender, ImageClickEventArgs e)
    {
        //Leer Variables URL 
        cargarInfoConciliacionActual();

        Consulta_ConciliarPorCantidad(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, tipoConciliacion, Convert.ToSByte(txtDias.Text), Convert.ToDecimal(txtDiferencia.Text), Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value));
        if (tipoConciliacion != 2)
            GenerarTablaReferenciasAConciliarInternos();
        else
            GenerarTablaReferenciasAConciliarPedidos();
        LlenaGridViewReferenciasConciliadas(tipoConciliacion);
    }
    protected void imgAutomatica_Click(object sender, ImageClickEventArgs e)
    {
        //Leer Variables URL 
        cargarInfoConciliacionActual();

        Enrutador objEnrutador = new Enrutador();
        string criterioConciliacion = "";

        criterioConciliacion = objEnrutador.ObtieneURLSolicitud(new SolicitudEnrutador(Convert.ToSByte(Request.QueryString["TipoConciliacion"]),
                                                                                       Convert.ToSByte(ddlCriteriosConciliacion.SelectedValue)));

        HttpContext.Current.Session["criterioConciliacion"] = criterioConciliacion;
        //Eliminar las variables de Session utilizadas en la Vista
        limpiarVariablesSession();

        Response.Redirect("~/Conciliacion/FormasConciliar/" + criterioConciliacion +
                                      ".aspx?Folio=" + folioConciliacion + "&Corporativo=" + corporativoConciliacion +
                                      "&Sucursal=" + sucursalConciliacion + "&Año=" + añoConciliacion + "&Mes=" +
                                      mesConciliacion + "&TipoConciliacion=" + tipoConciliacion + "&FormaConciliacion=" + Convert.ToSByte(ddlCriteriosConciliacion.SelectedValue));
    }

    private bool hayBloqueados(GridView grv)
    {
        bool Existen = false;
        if (LockerExterno.ExternoBloqueado != null)
        {
            SolicitudConciliacion objSolicitdConciliacion = new SolicitudConciliacion();
            tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);
            short _FormaConciliacion = ObtieneFormaConciliacion();
            objSolicitdConciliacion.TipoConciliacion = tipoConciliacion;
            objSolicitdConciliacion.FormaConciliacion = _FormaConciliacion;
            int filaindex = 0;
            foreach (GridViewRow fila in grv.Rows)
            {
                if (fila.RowType == DataControlRowType.DataRow)
                {
                    if (objSolicitdConciliacion.ConsultaPedido())  
                    {
                        listaReferenciaConciliadaPedidos[filaindex].Selecciona = fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                        if (fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked)
                        {
                            Existen = LockerExterno.ExternoBloqueado.Exists(x => x.Corporativo == listaReferenciaConciliadaPedidos[filaindex].Corporativo &&
                                                                                x.Sucursal == listaReferenciaConciliadaPedidos[filaindex].Sucursal &&
                                                                                x.Año == listaReferenciaConciliadaPedidos[filaindex].Año &&
                                                                                x.Folio == listaReferenciaConciliadaPedidos[filaindex].Folio &&
                                                                                x.Secuencia == listaReferenciaConciliadaPedidos[filaindex].Secuencia);
                        }
                    }
                    else
                    {
                        listaReferenciaConciliada[filaindex].Selecciona = fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                        if (fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked)
                        {
                            Existen = LockerExterno.ExternoBloqueado.Exists(x => x.Corporativo == listaReferenciaConciliada[filaindex].Corporativo &&
                                                                                x.Sucursal == listaReferenciaConciliada[filaindex].Sucursal &&
                                                                                x.Año == listaReferenciaConciliada[filaindex].Año &&
                                                                                x.Folio == listaReferenciaConciliada[filaindex].Folio &&
                                                                                x.Secuencia == listaReferenciaConciliada[filaindex].Secuencia);
                        }
                    }
                    if (Existen)
                        break;
                    filaindex++;
                }
            }
        }
        return Existen;
    }

    private void UnCheckBloquedos(GridView grv)
    {
        bool Existen = false;
        if (LockerExterno.ExternoBloqueado != null)
        {
            SolicitudConciliacion objSolicitdConciliacion = new SolicitudConciliacion();
            tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);
            short _FormaConciliacion = ObtieneFormaConciliacion();
            objSolicitdConciliacion.TipoConciliacion = tipoConciliacion;
            objSolicitdConciliacion.FormaConciliacion = _FormaConciliacion;
            int filaindex = 0;
            foreach (GridViewRow fila in grv.Rows)
            {
                if (fila.RowType == DataControlRowType.DataRow)
                {
                    if (objSolicitdConciliacion.ConsultaPedido())
                    {
                        listaReferenciaConciliadaPedidos[filaindex].Selecciona = fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                        if (fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked)
                        { 
                            Existen = LockerExterno.ExternoBloqueado.Exists(x => x.SessionID != Session.SessionID && 
                                                                            x.Corporativo == listaReferenciaConciliadaPedidos[filaindex].Corporativo &&
                                                                            x.Sucursal == listaReferenciaConciliadaPedidos[filaindex].Sucursal &&
                                                                            x.Año == listaReferenciaConciliadaPedidos[filaindex].Año &&
                                                                            x.Folio == listaReferenciaConciliadaPedidos[filaindex].Folio &&
                                                                            x.Secuencia == listaReferenciaConciliadaPedidos[filaindex].Secuencia);
                            fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked = !Existen;
                        }
                    }
                    else
                    {
                        listaReferenciaConciliada[filaindex].Selecciona = fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                        if (fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked)
                        { 
                            Existen = LockerExterno.ExternoBloqueado.Exists(x => x.SessionID != Session.SessionID &&  
                                                                            x.Corporativo == listaReferenciaConciliada[filaindex].Corporativo &&
                                                                            x.Sucursal == listaReferenciaConciliada[filaindex].Sucursal &&
                                                                            x.Año == listaReferenciaConciliada[filaindex].Año &&
                                                                            x.Folio == listaReferenciaConciliada[filaindex].Folio &&
                                                                            x.Secuencia == listaReferenciaConciliada[filaindex].Secuencia);
                            fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked = !Existen;
                        }
                    }
                    filaindex++;
                }
            }
        }
    }

    private void bloqueaTodoLoSeleccionado(GridView grv)
    {
        AppSettingsReader settings = new AppSettingsReader();
        SeguridadCB.Public.Parametros parametros = (SeguridadCB.Public.Parametros)HttpContext.Current.Session["Parametros"];
        string BloqueoEdoCTA = parametros.ValorParametro(Convert.ToSByte(settings.GetValue("Modulo", typeof(sbyte))), "BloqueoEdoCTA");
        
        if (BloqueoEdoCTA == "1")
        {
            int Corporativo;
            int Sucursal;
            int Año;
            int Folio;
            int Secuencia;
            string Descripcion;
            decimal Monto;

            SolicitudConciliacion objSolicitdConciliacion = new SolicitudConciliacion();
            tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);
            short _FormaConciliacion = ObtieneFormaConciliacion();
            objSolicitdConciliacion.TipoConciliacion = tipoConciliacion;
            objSolicitdConciliacion.FormaConciliacion = _FormaConciliacion;            

            SeguridadCB.Public.Usuario usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
            if (LockerExterno.ExternoBloqueado == null)
                LockerExterno.ExternoBloqueado = new List<RegistroExternoBloqueado>();
            else
                LockerExterno.EliminarBloqueos(Session.SessionID);

            int filaindex = 0;
            foreach (GridViewRow fila in grv.Rows) //grvCantidadConcuerdanPedidos
            {
                if (fila.RowType == DataControlRowType.DataRow)
                {
                    if (objSolicitdConciliacion.ConsultaPedido()) //if (tipoConciliacion == 2)
                    {
                        listaReferenciaConciliadaPedidos[filaindex].Selecciona = fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                        Corporativo = listaReferenciaConciliadaPedidos[filaindex].Corporativo;
                        Sucursal = listaReferenciaConciliadaPedidos[filaindex].Sucursal;
                        Año = listaReferenciaConciliadaPedidos[filaindex].Año;
                        Folio = listaReferenciaConciliadaPedidos[filaindex].Folio;
                        Secuencia = listaReferenciaConciliadaPedidos[filaindex].Secuencia;
                        Descripcion = listaReferenciaConciliadaPedidos[filaindex].Descripcion;
                        Monto = listaReferenciaConciliadaPedidos[filaindex].Total; //monto
                    }
                    else 
                    {
                        listaReferenciaConciliada[filaindex].Selecciona = fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                        Corporativo = listaReferenciaConciliada[filaindex].Corporativo;
                        Sucursal = listaReferenciaConciliada[filaindex].Sucursal;
                        Año = listaReferenciaConciliada[filaindex].Año;
                        Folio = listaReferenciaConciliada[filaindex].Folio;
                        Secuencia = listaReferenciaConciliada[filaindex].Secuencia;
                        Descripcion = listaReferenciaConciliada[filaindex].Descripcion;
                        Monto = listaReferenciaConciliada[filaindex].MontoConciliado; //monto
                    }
                    if (fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked)
                    {
                        LockerExterno.ExternoBloqueado.Add(new RegistroExternoBloqueado
                        {
                            FormaConciliacion = "CANTIDADCONCUERDA",
                            SessionID = Session.SessionID,
                            Corporativo = Corporativo,
                            Sucursal = Sucursal,
                            Año = Año,
                            Folio = Folio,
                            Secuencia = Secuencia,
                            Usuario = usuario.IdUsuario.ToString(),
                            InicioBloqueo = DateTime.Now,
                            Descripcion = Descripcion,
                            Monto = Monto //monto
                        });
                    }
                    filaindex++;
                }
            }
        }
    }

    private void desBloqueaTodo()
    {
        try
        {
            if (Locker.LockerExterno.ExternoBloqueado != null)
            {
                int J = Locker.LockerExterno.ExternoBloqueado.Count;
                for (int i = 0; i <= J - 1; i++)
                {
                    Locker.LockerExterno.ExternoBloqueado.Remove(Locker.LockerExterno.ExternoBloqueado.Where<Locker.RegistroExternoBloqueado>(s => s.SessionID == Session.SessionID).ToList()[0]);
                }
            }
        }
        catch (Exception)
        {
        }
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        bool resultado = false;
        bool AlgunChequeado = false;
        //Leer el tipoConciliacion URL
        tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);
        if (tipoConciliacion == 2)
        {
            //Leer la lista de Referencias por Conciliar : Tipo Conciliacion = 2
            listaReferenciaConciliadaPedidos = Session["POR_CONCILIAR"] as List<ReferenciaConciliadaPedido>;

            if (listaReferenciaConciliadaPedidos != null && listaReferenciaConciliadaPedidos.Count > 0)
            {
                try
                {
                    int filaindex = 0;
                    foreach (GridViewRow fila in grvCantidadConcuerdanPedidos.Rows)
                        if (fila.RowType == DataControlRowType.DataRow)
                        {
                            listaReferenciaConciliadaPedidos[filaindex].Selecciona = fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                            if (fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked)
                            {
                                AlgunChequeado = true;
                                resultado = listaReferenciaConciliadaPedidos[filaindex].Guardar();
                                if (!resultado)
                                    break;
                            }
                            filaindex++;
                        }
                }
                finally
                {
                    desBloqueaTodo();
                }
            }
            else
                App.ImplementadorMensajes.MostrarMensaje("Lista de Referencias a Conciliar esta Vacia");
        }
        else
        {
            //Leer la lista de Referencias por Conciliar : Tipo Conciliacion = 2
            listaReferenciaConciliada = Session["POR_CONCILIAR"] as List<ReferenciaConciliada>;
            if (listaReferenciaConciliada != null && listaReferenciaConciliada.Count > 0)
            {
                try
                {
                    int filaindex = 0;
                    foreach (GridViewRow fila in grvCantidadConcuerdanArchivos.Rows)
                        if (fila.RowType == DataControlRowType.DataRow)
                        {
                            listaReferenciaConciliada[filaindex].Selecciona = fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                            if (fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked)
                            {
                                AlgunChequeado = true;
                                resultado = listaReferenciaConciliada[filaindex].Guardar();
                                if (!resultado)
                                    break;
                            }
                            filaindex++;
                        }
                }
                finally
                {
                    desBloqueaTodo();
                }
            }
            else
                App.ImplementadorMensajes.MostrarMensaje("Lista de Referencias a Conciliar esta Vacia");
        }
        if (! AlgunChequeado)
            App.ImplementadorMensajes.MostrarMensaje("Seleccione al menos uno para continuar.");

        if (AlgunChequeado && ! resultado)
            App.ImplementadorMensajes.MostrarMensaje("Ocurrieron errores al guardar. Verifique");

        if (resultado)
        {
            //Leer Variables URL 
            cargarInfoConciliacionActual();

            LlenarBarraEstado();
            Consulta_TransaccionesConciliadas(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, Convert.ToInt32(ddlCriteriosConciliacion.SelectedValue));
            GenerarTablaConciliados();
            LlenaGridViewConciliadas();
            Consulta_ConciliarPorCantidad(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, tipoConciliacion, Convert.ToSByte(txtDias.Text),
                                                                Convert.ToDecimal(txtDiferencia.Text), Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value));
            if (tipoConciliacion == 2)
                GenerarTablaReferenciasAConciliarPedidos();
            else
                GenerarTablaReferenciasAConciliarInternos();
            LlenaGridViewReferenciasConciliadas(tipoConciliacion);
        }

    }

    protected void Nueva_Ventana(string Pagina, string Titulo, int Ancho, int Alto, int X, int Y)
    {
        ScriptManager.RegisterClientScriptBlock(this.upBarraHerramientas,
                                          upBarraHerramientas.GetType(),
                                          "ventana",
                                          "ShowWindow('" + Pagina + "','" + Titulo + "'," + Ancho + "," + Alto + "," + X + "," + Y + ")",
                                          true);
    }
    protected void imgExportar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            AppSettingsReader settings = new AppSettingsReader();

            //Leer Variables URL 
            cargarInfoConciliacionActual();

            string strReporte;
            
            strReporte = Server.MapPath("~/") + settings.GetValue(tipoConciliacion == 2 ? "RutaReporteRemanentesConciliacion" : "RutaReporteConciliacionTesoreria", typeof(string));


            if (!File.Exists(strReporte)) return;
            try
            {
                string strServer = settings.GetValue("Servidor", typeof(string)).ToString();
                string strDatabase = settings.GetValue("Base", typeof(string)).ToString();

                usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
                string strUsuario = usuario.IdUsuario.Trim();
                string strPW = usuario.ClaveDesencriptada;
                ArrayList Par = new ArrayList();

                Par.Add("@Corporativo=" + corporativoConciliacion);
                Par.Add("@Sucursal=" + sucursalConciliacion);
                Par.Add("@AñoConciliacion=" + añoConciliacion);
                Par.Add("@MesConciliacion=" + mesConciliacion);
                Par.Add("@FolioConciliacion=" + folioConciliacion);
                ClaseReporte Reporte = new ClaseReporte(strReporte, Par, strServer, strDatabase, strUsuario, strPW);
                //Reporte.Imprimir_Reporte();
                HttpContext.Current.Session["RepDoc"] = Reporte.RepDoc;
                HttpContext.Current.Session["ParametrosReporte"] = Par;
                Nueva_Ventana("../../Reporte/Reporte.aspx", "Carta", 0, 0, 0, 0);
                //if (Reporte.Hay_Error) Mensaje("Error.", Reporte.Error);
                Reporte = null;
            }
            catch (Exception ex)
            {
                App.ImplementadorMensajes.MostrarMensaje(ex.Message);
            }
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }
    }
    protected void imgCerrarConciliacion_Click(object sender, ImageClickEventArgs e)
    {
        usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
        //Leer Variables URL 
        cargarInfoConciliacionActual();

        cConciliacion c = App.Consultas.ConsultaConciliacionDetalle(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion);
        if (c.CerrarConciliacion(usuario.IdUsuario))
        {
            App.ImplementadorMensajes.MostrarMensaje("CONCILIACIÓN CERRADA EXITOSAMENTE");
            System.Threading.Thread.Sleep(3000);
            Response.Redirect("~/Conciliacion/DetalleConciliacion.aspx?Folio=" + folioConciliacion + "&Corporativo=" + corporativoConciliacion +
                                    "&Sucursal=" + sucursalConciliacion + "&Año=" + añoConciliacion + "&Mes=" +
                                    mesConciliacion + "&TipoConciliacion=" + tipoConciliacion);
        }
        else
        {
            App.ImplementadorMensajes.MostrarMensaje("ERRORES AL CERRAR LA CONCILIACIÓN");
        }
    }
    protected void imgCancelarConciliacion_Click(object sender, ImageClickEventArgs e)
    {
        usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
        //Leer Variables URL 
        cargarInfoConciliacionActual();

        cConciliacion c = App.Consultas.ConsultaConciliacionDetalle(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion);
        if (c.CancelarConciliacion(usuario.IdUsuario))
        {
            App.ImplementadorMensajes.MostrarMensaje("CONCILIACIÓN CANCELADA EXITOSAMENTE");
            System.Threading.Thread.Sleep(3000);
            Response.Redirect("../../Inicio.aspx");
        }
        else
        {
            App.ImplementadorMensajes.MostrarMensaje("ERRORES AL CANCELAR LA CONCILIACIÓN");
        }
    }
    protected void imgFiltrar_Click(object sender, ImageClickEventArgs e)
    {
        //Leer el tipoConciliacion URL
        tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);

        cargar_ComboCampoFiltroDestino(tipoConciliacion, ddlFiltrarEn.SelectedItem.Value);
        enlazarCampoFiltrar();
        InicializarControlesFiltro();
        mpeFiltrar.Show();
    }
    //------------------------------INICIO MODULO "AGREGAR NUEVO INTERNO"---------------------------------
    protected void imgImportar_Click(object sender, ImageClickEventArgs e)
    {

        Carga_TipoFuenteInformacionInterno(Consultas.ConfiguracionTipoFuente.TipoFuenteInformacionInterno);
        limpiarVistaImportarInterno();

        LlenaGridViewFoliosAgregados();
        popUpImportarArchivos.Show();
    }
    public void limpiarVistaImportarInterno()
    {
        Session["NUEVOS_INTERNOS"] = null;
        Session.Remove("NUEVOS_INTERNOS");

        ddlTipoFuenteInfoInterno.SelectedIndex = ddlSucursalInterno.SelectedIndex = 0;
    }
    /// <summary>
    /// Llena el Combo de Tipo Fuente Informacion Externo e Interno
    /// </summary>
    public void Carga_TipoFuenteInformacionInterno(Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionTipoFuente tipo)
    {
        try
        {

            listTipoFuenteInformacionExternoInterno = Conciliacion.RunTime.App.Consultas.ConsultaTipoInformacionDatos(tipo);
            this.ddlTipoFuenteInfoInterno.DataSource = listTipoFuenteInformacionExternoInterno;
            this.ddlTipoFuenteInfoInterno.DataValueField = "Identificador";
            this.ddlTipoFuenteInfoInterno.DataTextField = "Descripcion";
            this.ddlTipoFuenteInfoInterno.DataBind();
            this.ddlTipoFuenteInfoInterno.Dispose();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    protected void ddlTipoFuenteInfoInterno_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Leer Variables URL 
        cargarInfoConciliacionActual();

        Carga_FoliosInternos(
                       corporativoConciliacion,
                       Convert.ToInt32(ddlSucursalInterno.SelectedItem.Value),
                       añoConciliacion,
                       mesConciliacion,
                       lblCuenta.Text,
                       Convert.ToSByte(ddlTipoFuenteInfoInterno.SelectedItem.Value)
                       );
        enlazarComboFolioInterno();

    }
    protected void ddlSucursalInterno_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Leer Variables URL 
        cargarInfoConciliacionActual();

        Carga_FoliosInternos(
                              corporativoConciliacion,
                              Convert.ToInt32(ddlSucursalInterno.SelectedItem.Value),
                              añoConciliacion,
                              mesConciliacion,
                              lblCuenta.Text,
                              Convert.ToSByte(ddlTipoFuenteInfoInterno.SelectedItem.Value)
                              );
        enlazarComboFolioInterno();
    }

    /// <summary>
    /// Consulta los Folios Internos según parametros de filtro. Agregar Interno
    /// </summary>
    public void Carga_FoliosInternos(int corporativo, int sucursal, int añoF, short mesF, string cuentabancaria, short tipofuenteinformacion)
    {
        try
        {
            listFoliosInterno = Conciliacion.RunTime.App.Consultas.ConsultaFoliosTablaDestino(corporativo, sucursal, añoF, mesF, cuentabancaria, tipofuenteinformacion);
            //HttpContext.Current.Session["listFoliosInterno"] = listFoliosInterno;
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }
    /// <summary>
    /// Llena el Combo de Folios Internos para Agregar Nuevo Interno
    /// </summary>
    public void enlazarComboFolioInterno()
    {
        this.ddlFolioInterno.DataSource = listFoliosInterno;
        this.ddlFolioInterno.DataValueField = "Identificador";
        this.ddlFolioInterno.DataTextField = "Descripcion";
        this.ddlFolioInterno.DataBind();
        this.ddlFolioInterno.Dispose();
    }
    protected void ddlFolioInterno_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Leer Variables URL 
        cargarInfoConciliacionActual();

        Carga_FoliosInternos(
                       corporativoConciliacion,
                       Convert.ToInt32(ddlSucursalInterno.SelectedItem.Value),
                       añoConciliacion,
                       mesConciliacion,
                       lblCuenta.Text,
                       Convert.ToSByte(ddlTipoFuenteInfoInterno.SelectedItem.Value)
                       );

        this.lblStatusFolioInterno.Text = listFoliosInterno[ddlFolioInterno.SelectedIndex].Campo1;
        this.lblUsuarioAltaEx.Text = listFoliosInterno[ddlFolioInterno.SelectedIndex].Campo3;

    }
    protected void ddlFolioInterno_DataBound(object sender, EventArgs e)
    {
        try
        {
            if (ddlFolioInterno.Items.Count <= 0)
            {
                lblUsuarioAltaEx.Text = lblStatusFolioInterno.Text = String.Empty;
                return;
            }
            this.lblStatusFolioInterno.Text = listFoliosInterno[ddlFolioInterno.SelectedIndex].Campo1;
            this.lblUsuarioAltaEx.Text = listFoliosInterno[ddlFolioInterno.SelectedIndex].Campo3;
        }
        catch (Exception)
        {
            throw;
        }

    }
    protected void btnAñadirFolio_Click(object sender, ImageClickEventArgs e)
    {
        listArchivosInternos = Session["NUEVOS_INTERNOS"] != null ?
                               Session["NUEVOS_INTERNOS"] as List<DatosArchivo> :
                               new List<DatosArchivo>();

        if (listArchivosInternos != null && listArchivosInternos.Exists(x => x.Folio == Convert.ToInt32(ddlFolioInterno.SelectedItem.Value)))
        {
            App.ImplementadorMensajes.MostrarMensaje("Este Folio Interno ya esta Agregado");
        }
        else
        {
            //Leer Variables URL 
            cargarInfoConciliacionActual();

            cConciliacion conciliacion = App.Consultas.ConsultaConciliacionDetalle(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion);
            DatosArchivo datosArchivoInterno = App.DatosArchivo.CrearObjeto();//new DatosArchivoDatos(App.ImplementadorMensajes); //App.DatosArchivo

            datosArchivoInterno.FolioConciliacion = conciliacion.Folio;
            datosArchivoInterno.SucursalConciliacion = conciliacion.Sucursal;
            datosArchivoInterno.Folio = Convert.ToInt32(ddlFolioInterno.SelectedItem.Value);
            datosArchivoInterno.Corporativo = conciliacion.Corporativo;
            datosArchivoInterno.Sucursal = Convert.ToInt32(ddlSucursalInterno.SelectedItem.Value);
            datosArchivoInterno.Año = conciliacion.Año;
            datosArchivoInterno.MesConciliacion = conciliacion.Mes;
            listArchivosInternos.Add(datosArchivoInterno);
            //Guardar los arhivos interno que se estan agregando
            Session["NUEVOS_INTERNOS"] = listArchivosInternos;
            LlenaGridViewFoliosAgregados();
        }


    }
    /// <summary>
    /// Llena el GridView de Folios Internos Agregados
    /// </summary>
    private void LlenaGridViewFoliosAgregados()
    {
        listArchivosInternos = Session["NUEVOS_INTERNOS"] != null ?
                               Session["NUEVOS_INTERNOS"] as List<DatosArchivo> :
                               new List<DatosArchivo>();
        this.grvAgregados.DataSource = listArchivosInternos;
        this.grvAgregados.DataBind();
        //this.grvAgregados.Dispose();
    }
    public void activarImportacion(int tipoConciliacion)
    {
        if (tipoConciliacion == 2)
        {
            tdImportar.Attributes.Add("class", "iconoOpcion bg-color-grisClaro02");
            imgImportar.Enabled = false;
        }
    }
    protected void btnGuardarInterno_Click(object sender, EventArgs e)
    {
        bool resultado = false;
        listArchivosInternos = Session["NUEVOS_INTERNOS"] != null ?
                               Session["NUEVOS_INTERNOS"] as List<DatosArchivo> :
                               listArchivosInternos;

        if (listArchivosInternos != null && listArchivosInternos.Count > 0)
        {
            //Leer Variables URL 
            cargarInfoConciliacionActual();

            cConciliacion conciliacion = App.Consultas.ConsultaConciliacionDetalle(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion);
            listArchivosInternos.ForEach(x => resultado = conciliacion.AgregarArchivo(x, cConciliacion.Operacion.Edicion));

            if (resultado)
            {
                //ACTUALIZAR GRID CANTIDAD CONCUERDA
                LlenarBarraEstado();
                Consulta_ConciliarPorCantidad(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, tipoConciliacion, Convert.ToSByte(txtDias.Text), Convert.ToDecimal(txtDiferencia.Text), Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value));
                if (tipoConciliacion != 2)
                    GenerarTablaReferenciasAConciliarInternos();
                else
                    GenerarTablaReferenciasAConciliarPedidos();
                LlenaGridViewReferenciasConciliadas(tipoConciliacion);

                App.ImplementadorMensajes.MostrarMensaje("Agregado de Folios Internos exitoso.");
            }
            else
                App.ImplementadorMensajes.MostrarMensaje("Ocurrieron problemas al agregar el nuevo Folio");
            //Limpiar Remover Variable (Session) de Internos 
            limpiarVistaImportarInterno();

            popUpImportarArchivos.Hide();
            popUpImportarArchivos.Dispose();

        }
        else
        {
            App.ImplementadorMensajes.MostrarMensaje("No se ha agregado un nuevo Archivo Interno");
        }
    }
    protected void grvAgregados_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        listArchivosInternos = Session["NUEVOS_INTERNOS"] != null ?
                               Session["NUEVOS_INTERNOS"] as List<DatosArchivo> :
                               listArchivosInternos;

        int folioInterno = Convert.ToInt32(grvAgregados.DataKeys[e.RowIndex].Value);
        listArchivosInternos.RemoveAll(x => x.Folio == folioInterno);
        Session["NUEVOS_INTERNOS"] = listArchivosInternos;
        LlenaGridViewFoliosAgregados();
    }
    protected void grvAgregados_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try { grvAgregados.PageIndex = e.NewPageIndex; LlenaGridViewFoliosAgregados(); }
        catch (Exception ex) { }
    }
    protected void ddlTipoFuenteInfoInterno_DataBound(object sender, EventArgs e)
    {
        //Leer Variables URL 
        cargarInfoConciliacionActual();

        Carga_FoliosInternos(
                       corporativoConciliacion,
                       Convert.ToInt32(ddlSucursalInterno.SelectedItem.Value),
                       añoConciliacion,
                       mesConciliacion,
                       lblCuenta.Text,
                       Convert.ToSByte(ddlTipoFuenteInfoInterno.SelectedItem.Value)
                       );
        enlazarComboFolioInterno();
    }

    /// <summary>
    ///Consulta el detalle del Folio Interno
    /// </summary>
    public void Consulta_TablaDestinoDetalleInterno(Consultas.Configuracion configuracion, int empresa, int sucursal, int año, int folioInterno)
    {
        SeguridadCB.Seguridad seguridad = new SeguridadCB.Seguridad();
        System.Data.SqlClient.SqlConnection Connection = seguridad.Conexion;
        if (Connection.State == ConnectionState.Closed)
        {
            seguridad.Conexion.Open();
            Connection = seguridad.Conexion;
        }
        try
        {
            listaDestinoDetalleInterno = Conciliacion.RunTime.App.Consultas.ConsultaTablaDestinoDetalle(
                        configuracion,
                        empresa,
                        sucursal,
                        año,
                        folioInterno);
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error al Consultar Detalle.\r\nError" + ex.Message);
        }
    }
    /// <summary>
    /// Llena el VistaRapida[TablaDestinoDetalleExterno] x Folio
    /// </summary>
    public void GenerarTablaDestinoDetalleInterno()
    {
        tblDestinoDetalleInterno = new DataTable("DetalleInterno");
        tblDestinoDetalleInterno.Columns.Add("Folio", typeof(int));
        tblDestinoDetalleInterno.Columns.Add("FOperacion", typeof(DateTime));
        tblDestinoDetalleInterno.Columns.Add("FMovimiento", typeof(DateTime));
        tblDestinoDetalleInterno.Columns.Add("Referencia", typeof(string));
        tblDestinoDetalleInterno.Columns.Add("Descripcion", typeof(string));
        tblDestinoDetalleInterno.Columns.Add("Deposito", typeof(float));
        tblDestinoDetalleInterno.Columns.Add("Retiro", typeof(float));
        tblDestinoDetalleInterno.Columns.Add("Concepto", typeof(string));

        foreach (DatosArchivoDetalle da in listaDestinoDetalleInterno)
        {
            tblDestinoDetalleInterno.Rows.Add(
                da.Folio,
                da.FOperacion.ToShortDateString(),
                da.FMovimiento.ToShortDateString(),
                da.Referencia,
                da.Descripcion,
                da.Deposito,
                da.Retiro,
                da.Concepto);
        }
        HttpContext.Current.Session["DETALLEINTERNO"] = tblDestinoDetalleInterno;
    }
    /// <summary>
    ///Genera la tabla de destino detalle[Vista Rapida]
    ///  
    private void LlenaGridViewDestinoDetalleInterno()
    {
        DataTable tablaDestinoDetalleInterno = (DataTable)HttpContext.Current.Session["DETALLEINTERNO"];
        this.grvVistaRapidaInterno.DataSource = tblDestinoDetalleInterno;
        this.grvVistaRapidaInterno.DataBind();
    }
    protected void btnVerDatalleInterno_Click(object sender, ImageClickEventArgs e)
    {
        //Leer Variables URL 
        cargarInfoConciliacionActual();

        Consulta_TablaDestinoDetalleInterno(Consultas.Configuracion.Previo,
                                            corporativoConciliacion,
                                            Convert.ToInt16(ddlSucursalInterno.SelectedItem.Value),
                                            añoConciliacion, Convert.ToInt32(ddlFolioInterno.SelectedItem.Value));
        GenerarTablaDestinoDetalleInterno();
        LlenaGridViewDestinoDetalleInterno();
        lblFolioInterno.Text = ddlFolioInterno.SelectedItem.Value;
        grvVistaRapidaInterno_ModalPopupExtender.Show();
        upDetalleInterno.Update();
    }

    //---FIN MODULO "AGREGAR NUEVO INTERNO"
    protected void imgGuardarParcial_Click(object sender, ImageClickEventArgs e)
    {
        //Leer Variables URL 
        cargarInfoConciliacionActual();

        bool resultado = false;
        List<GridViewRow> referenciasVistaSeleccionados = leerReferenciasSeleccionadasPagina(tipoConciliacion); ;
        if (referenciasVistaSeleccionados.Count > 0)
        {
            if (tipoConciliacion == 2)
            {
                ReferenciaConciliadaPedido rcp;
                foreach (GridViewRow un in referenciasVistaSeleccionados)
                {
                    int secuenciaEx = Convert.ToInt32(grvCantidadConcuerdanPedidos.DataKeys[un.RowIndex].Values["Secuencia"]);
                    int folioExt = Convert.ToInt32(grvCantidadConcuerdanPedidos.DataKeys[un.RowIndex].Values["FolioExt"]);
                    int pedido = Convert.ToInt32(grvCantidadConcuerdanPedidos.DataKeys[un.RowIndex].Values["Pedido"]);
                    int celula = Convert.ToInt32(grvCantidadConcuerdanPedidos.DataKeys[un.RowIndex].Values["Celula"]);

                    //Leer la lista de Referencias por Conciliar : Tipo Conciliacion = 2
                    listaReferenciaConciliadaPedidos = Session["POR_CONCILIAR"] as List<ReferenciaConciliadaPedido>;

                    rcp = listaReferenciaConciliadaPedidos.Single(x => x.Secuencia == secuenciaEx && x.Folio == folioExt && x.Pedido == pedido && x.CelulaPedido == celula);
                    resultado = rcp.Guardar();
                }
                App.ImplementadorMensajes.MostrarMensaje(resultado
                                                                      ? "Guardado Exitosamente"
                                                                      : "Error al Guardar");
            }
            else
            {
                ReferenciaConciliada rcp;
                foreach (GridViewRow un in referenciasVistaSeleccionados)
                {

                    int secuenciaExt = Convert.ToInt32(grvCantidadConcuerdanArchivos.DataKeys[un.RowIndex].Values["SecuenciaExt"]);
                    int folioExt = Convert.ToInt32(grvCantidadConcuerdanArchivos.DataKeys[un.RowIndex].Values["FolioExt"]);
                    int secuenciaInt = Convert.ToInt32(grvCantidadConcuerdanArchivos.DataKeys[un.RowIndex].Values["SecuenciaInt"]);
                    int folioInt = Convert.ToInt32(grvCantidadConcuerdanArchivos.DataKeys[un.RowIndex].Values["FolioInt"]);

                    //Leer la lista de Referencias por Conciliar : Tipo Conciliacion != 2
                    listaReferenciaConciliada = Session["POR_CONCILIAR"] as List<ReferenciaConciliada>;

                    rcp = listaReferenciaConciliada.Single(
                            x =>
                            x.Secuencia == secuenciaExt && x.Folio == folioExt && x.FolioInterno == folioInt &&
                            x.SecuenciaInterno == secuenciaInt);
                    resultado = rcp.Guardar();
                }
                App.ImplementadorMensajes.MostrarMensaje(resultado ? "Guardado Exitosamente"
                                                                      : "Error al Guardar");
            }
        }
        else
            App.ImplementadorMensajes.MostrarMensaje("No existe ninguna referencia seleccionada. Verifique");


        LlenarBarraEstado();
        Consulta_TransaccionesConciliadas(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, Convert.ToInt32(ddlCriteriosConciliacion.SelectedValue));
        GenerarTablaConciliados();
        LlenaGridViewConciliadas();
        Consulta_ConciliarPorCantidad(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, tipoConciliacion, Convert.ToSByte(txtDias.Text),
                                                            Convert.ToDecimal(txtDiferencia.Text), Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value));

        if (tipoConciliacion == 2)
            GenerarTablaReferenciasAConciliarPedidos();
        else
            GenerarTablaReferenciasAConciliarInternos();

        LlenaGridViewReferenciasConciliadas(tipoConciliacion);
    }


    protected void imgCerrarImportar_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void grvCantidadConcuerdanArchivos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            if (int.Parse(HttpContext.Current.Session["SolicitdConciliacionConsultaArchivo"].ToString()) == 1)
            {
                if (e.Row.Cells.Count >= 15)
                    e.Row.Cells[15].Visible = false;
                if (e.Row.Cells.Count >= 16)
                    e.Row.Cells[16].Visible = false;
            }
            else
            {
                if (e.Row.Cells.Count >= 15)
                    e.Row.Cells[15].Visible = true;
                if (e.Row.Cells.Count >= 16)
                    e.Row.Cells[16].Visible = true;
            }
        }
    }

    protected void grvVistaRapidaInterno_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grvVistaRapidaInterno.DataSource = (DataTable)HttpContext.Current.Session["DETALLEINTERNO"]; //tblDestinoDetalleInterno;
        this.grvVistaRapidaInterno.PageIndex = e.NewPageIndex;
        this.grvVistaRapidaInterno.DataBind();
    }

    private void BloqueaUnSeleccionado(ReferenciaConciliada rfEx)
    {
        AppSettingsReader settings = new AppSettingsReader();
        SeguridadCB.Public.Parametros parametros = (SeguridadCB.Public.Parametros)HttpContext.Current.Session["Parametros"];
        string BloqueoEdoCTA = parametros.ValorParametro(Convert.ToSByte(settings.GetValue("Modulo", typeof(sbyte))), "BloqueoEdoCTA");

        if (BloqueoEdoCTA == "1")
        {
            int Corporativo;
            int Sucursal;
            int Año;
            int Folio;
            int Secuencia;
            string Descripcion;
            decimal Monto;

            SolicitudConciliacion objSolicitdConciliacion = new SolicitudConciliacion();
            tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);
            short _FormaConciliacion = ObtieneFormaConciliacion();
            objSolicitdConciliacion.TipoConciliacion = tipoConciliacion;
            objSolicitdConciliacion.FormaConciliacion = _FormaConciliacion;

            SeguridadCB.Public.Usuario usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
            if (LockerExterno.ExternoBloqueado == null)
                LockerExterno.ExternoBloqueado = new List<RegistroExternoBloqueado>();

            if (objSolicitdConciliacion.ConsultaPedido())
            {
                Corporativo = rfEx.Corporativo;
                Sucursal = rfEx.Sucursal;
                Año = rfEx.Año;
                Folio = rfEx.Folio;
                Secuencia = rfEx.Secuencia;
                Descripcion = "";
                Monto = rfEx.MontoConciliado;
            }
            else
            {
                Corporativo = rfEx.Corporativo;
                Sucursal = rfEx.Sucursal;
                Año = rfEx.Año;
                Folio = rfEx.Folio;
                Secuencia = rfEx.Secuencia;
                Descripcion = rfEx.Descripcion;
                Monto = rfEx.MontoConciliado;
            }
            LockerExterno.ExternoBloqueado.Add(new RegistroExternoBloqueado
            {
                FormaConciliacion = "CANTIDADCONCUERDA",
                SessionID = Session.SessionID,
                Corporativo = Corporativo,
                Sucursal = Sucursal,
                Año = Año,
                Folio = Folio,
                Secuencia = Secuencia,
                Usuario = usuario.IdUsuario.ToString(),
                InicioBloqueo = DateTime.Now,
                Descripcion = Descripcion,
                Monto = Monto
            });

        }
    }

    private void DesBloquea(ReferenciaConciliada rfEx)
    {
        try
        {
            if (Locker.LockerExterno.ExternoBloqueado != null && Locker.LockerExterno.ExternoBloqueado.Count > 0)
            {
                LockerExterno.ExternoBloqueado.Remove(
                       Locker.LockerExterno.ExternoBloqueado.Where<Locker.RegistroExternoBloqueado>(x => x.Corporativo == rfEx.Corporativo &&
                                                                   x.Sucursal == rfEx.Sucursal &&
                                                                   x.Año == rfEx.Año &&
                                                                   x.Folio == rfEx.Folio &&
                                                                   x.Secuencia == rfEx.Secuencia).ToList()[0]
                    );
            }
        }
        catch (Exception)
        {
        }
    }

    public ReferenciaConciliada leerReferenciaExternaSeleccionada()
    {
        listaReferenciaConciliada = Session["POR_CONCILIAR"] as List<ReferenciaConciliada>;
        int secuenciaExterno = Convert.ToInt32(grvCantidadConcuerdanArchivos.DataKeys[indiceExternoSeleccionado].Values["SecuenciaExt"]);
        int folioExterno = Convert.ToInt32(grvCantidadConcuerdanArchivos.DataKeys[indiceExternoSeleccionado].Values["FolioExt"]);
        return listaReferenciaConciliada.Single(x => x.Secuencia == secuenciaExterno && x.Folio == folioExterno);
    }

    private bool ExisteExternoBloqueado()
    {
        SeguridadCB.Public.Parametros parametros;
        parametros = (SeguridadCB.Public.Parametros)HttpContext.Current.Session["Parametros"];
        AppSettingsReader settings = new AppSettingsReader();
        string bloqueo = parametros.ValorParametro(Convert.ToSByte(settings.GetValue("Modulo", typeof(sbyte))), "BloqueoEdoCTA").Trim();
        bool BloqueoEdoCTA = false;
        BloqueoEdoCTA = bloqueo == "1" ? true : false;
        if (BloqueoEdoCTA)
        {
            if (LockerExterno.ExternoBloqueado == null)
                return false;

            ReferenciaConciliada rfEx = leerReferenciaExternaSeleccionada();
            return LockerExterno.ExternoBloqueado.Exists(x => x.Corporativo == rfEx.Corporativo &&
                                                                x.Sucursal == rfEx.Sucursal &&
                                                                x.Año == rfEx.Año &&
                                                                x.Folio == rfEx.Folio &&
                                                                x.Secuencia == rfEx.Secuencia);
        }
        else
            return false;
    }

    protected void chkFolio_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = sender as CheckBox;
        GridViewRow grv = (GridViewRow)chk.Parent.Parent;

        int respaldoIndiceSeleccionado = indiceExternoSeleccionado;
        indiceExternoSeleccionado = grv.RowIndex;
        ReferenciaConciliada rfEx = leerReferenciaExternaSeleccionada();

        if (chk.Checked)
        {
            rfEx.Selecciona = false;//Es solo para guardar la REFERENCIA SELECCIONADA..FALSE porq se hace un ! negacion..al cargar el Externos..para no modificar otra cosa.
            //GenerarTablaReferenciasAConciliarInternos();
            if (ExisteExternoBloqueado())
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Mensaje",
                    @"alertify.alert('Conciliaci&oacute;n bancaria','El registro no se puede Guardar, el externo seleccionado ya ha sido conciliado por otro usuario.', function(){ });", true);
                indiceExternoSeleccionado = respaldoIndiceSeleccionado;
                CheckBox checkbox = sender as CheckBox;
                checkbox.Checked = false;
                return;
            }
            else
                BloqueaUnSeleccionado(rfEx);
        }
        else
        {
            rfEx.Selecciona = true;
            //GenerarTablaReferenciasAConciliarInternos();
            DesBloquea(rfEx);
        }

    }
}