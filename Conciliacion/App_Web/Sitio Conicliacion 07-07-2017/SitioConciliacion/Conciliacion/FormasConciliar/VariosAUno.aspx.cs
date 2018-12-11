using System;
using System.Activities.Statements;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Conciliacion.RunTime;
using Conciliacion.RunTime.ReglasDeNegocio;
using AjaxControlToolkit;
using Conciliacion.RunTime.DatosSQL;

public partial class Conciliacion_FormasConciliar_VariosAUno : System.Web.UI.Page
{
    #region "Propiedades Globales"
    private SeguridadCB.Public.Operaciones operaciones;
    private SeguridadCB.Public.Usuario usuario;

    public List<ReferenciaNoConciliada> listaReferenciaExternas = new List<ReferenciaNoConciliada>();
    public List<ReferenciaNoConciliada> listaReferenciaArchivosInternos = new List<ReferenciaNoConciliada>();
    public List<ReferenciaNoConciliada> listaReferenciaInternasAgregadas = new List<ReferenciaNoConciliada>();

    public List<ReferenciaNoConciliada> listaTransaccionesConciliadas = new List<ReferenciaNoConciliada>();
    public List<ReferenciaNoConciliadaPedido> listaReferenciaPedidos = new List<ReferenciaNoConciliadaPedido>();

    private List<ListaCombo> listSucursales = new List<ListaCombo>();
    private List<ListaCombo> listCelulas = new List<ListaCombo>();
    private List<ListaCombo> listStatusConcepto = new List<ListaCombo>();
    private List<ListaCombo> listFormasConciliacion = new List<ListaCombo>();
    private List<ListaCombo> listMotivosNoConciliados = new List<ListaCombo>();
    private Dictionary<int, string> dcBusquedaPedidos = new Dictionary<int, string>();
    
    private DataTable tblTransaccionesConciliadas;
    private DataTable tblReferenciaExternas;
    private DataTable tblReferenciaInternas;
    public List<ListaCombo> listCamposDestino = new List<ListaCombo>();
    public DataTable tblDetalleTransaccionConciliada;
    public DataTable tblReferenciaAgregadasExternas;

    #endregion

    private string DiferenciaDiasMaxima, DiferenciaDiasMinima, DiferenciaCentavosMaxima, DiferenciaCentavosMinima;
    public int corporativo, año, folio, sucursal;
    public short mes, tipoConciliacion, grupoConciliacion, formaConciliacion;
    public int indiceExternoSeleccionado = 0;
    public int indiceInternoSeleccionado = 0;
    public ReferenciaNoConciliada tranDesconciliar;
    public ReferenciaNoConciliada tranExternaAnteriorSeleccionada;

    private DatosArchivo datosArchivoInterno;
    private List<ListaCombo> listTipoFuenteInformacionExternoInterno = new List<ListaCombo>();
    public List<ListaCombo> listFoliosInterno = new List<ListaCombo>();
    public List<DatosArchivo> listArchivosInternos = new List<DatosArchivo>();

    private DataTable tblDestinoDetalleInterno;
    private List<DatosArchivoDetalle> listaDestinoDetalleInterno = new List<DatosArchivoDetalle>();
    private string _URLGateway;

    protected override void OnPreInit(EventArgs e)
    {
        if (HttpContext.Current.Session["Operaciones"] == null)
            Response.Redirect("~/Acceso/Acceso.aspx", true);
        else
            operaciones = (SeguridadCB.Public.Operaciones)HttpContext.Current.Session["Operaciones"];
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        wucBuscaClientesFacturas.HtmlIdGridRelacionado = "ctl00_contenidoPrincipal_grvInternos";
        Conciliacion.RunTime.App.ImplementadorMensajes.ContenedorActual = this;
        short _FormaConciliacion = Convert.ToSByte(Request.QueryString["FormaConciliacion"]);
        if (_FormaConciliacion == 0)
        {
            _FormaConciliacion = 6;
        }

        try
        {
            Conciliacion.RunTime.App.ImplementadorMensajes.ContenedorActual = this;
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
                //Leer variables de URL
                corporativo = Convert.ToInt32(Request.QueryString["Corporativo"]);
                sucursal = Convert.ToInt16(Request.QueryString["Sucursal"]);
                año = Convert.ToInt32(Request.QueryString["Año"]);
                folio = Convert.ToInt32(Request.QueryString["Folio"]);
                mes = Convert.ToSByte(Request.QueryString["Mes"]);
                tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);
                grupoConciliacion = Convert.ToSByte(Request.QueryString["GrupoConciliacion"]);
                formaConciliacion = Convert.ToInt16(Request.QueryString["FormaConciliacion"]);

                SolicitudConciliacion objSolicitdConciliacion = new SolicitudConciliacion();
                objSolicitdConciliacion.TipoConciliacion = tipoConciliacion;
                objSolicitdConciliacion.FormaConciliacion = _FormaConciliacion;

                HabilitarBusquedaPedidos(objSolicitdConciliacion);

                CargarRangoDiasDiferenciaGrupo(grupoConciliacion);
                Carga_StatusConcepto(Consultas.ConfiguracionStatusConcepto.ConEtiquetas);
                Carga_FormasConciliacion(tipoConciliacion);
                cargar_ComboMotivosNoConciliado();
                Carga_SucursalCorporativo(corporativo);
                if (objSolicitdConciliacion.ConsultaPedido())
                    Carga_ComboTiposDeCobro();
                else
                {
                    lblTiposdeCobro.Visible = false;
                    ddlTiposDeCobro.Visible = false;
                }
                LlenarBarraEstado();
                //CARGAR LAS TRANSACCIONES CONCILIADAS POR EL CRITERIO DE CONCILIACION
                //Consulta_TransaccionesConciliadas(corporativo, sucursal, año, mes, folio, Convert.ToInt32(ddlCriteriosConciliacion.SelectedValue));
                Consulta_TransaccionesConciliadas(corporativo, sucursal, año, mes, folio, formaConciliacion);
                GenerarTablaConciliados();
                LlenaGridViewConciliadas();
                Consulta_Externos(corporativo, sucursal, año, mes, folio, Convert.ToDecimal(txtDiferencia.Text), tipoConciliacion, Convert.ToInt32(ddlStatusConcepto.SelectedValue), EsDepositoRetiro());
                GenerarTablaExternos();
                LlenaGridViewExternos();
                ocultarOpciones("INTERNO");

                if (objSolicitdConciliacion.ConsultaPedido())
                { 
                    HttpContext.Current.Session["wucBuscaClientesFacturasVisible"] = 1;
                    btnFiltraCliente.Visible = true;
                }
                else
                { 
                    HttpContext.Current.Session["wucBuscaClientesFacturasVisible"] = 0;
                    btnFiltraCliente.Visible = false;
                }

                if (tipoConciliacion == 2)
                {   //PENDIENTE
                    if (Convert.ToString(HttpContext.Current.Session["criterioConciliacion"]) == "VariosAUno")
                    {
                        //wucBuscaClientesFacturas.HtmlIdGridRelacionado = "ctl00_contenidoPrincipal_grvPedidos";
                        wucBuscaClientesFacturas.HtmlIdGridRelacionado = "ctl00_contenidoPrincipal_grvAgregadosPedidos";
                        wucBuscaClientesFacturas.HtmlIdGridCeldaID = "1";
                    }
                }
                else
                {
                    if (Convert.ToString(HttpContext.Current.Session["criterioConciliacion"]) == "VariosAUno")
                    {
                        wucBuscaClientesFacturas.HtmlIdGridRelacionado = "ctl00_contenidoPrincipal_grvInternos";
                        wucBuscaClientesFacturas.HtmlIdGridCeldaID = "3";
                    }
                }

                if (objSolicitdConciliacion.ConsultaPedido())
                {
                    lblSucursalCelula.Text = "Celula Interna";
                    ddlCelula.Visible = lblPedidos.Visible = lblVer.Visible = rdbTodosMenoresIn.Visible = true;
                    //btnENPROCESOINTERNO.Visible = btnCANCELARINTERNO.Visible = txtDias.CausesValidation = txtDias.Enabled = ddlSucursal.Enabled = imgExportar.Enabled = false;
                    btnENPROCESOINTERNO.Visible = btnCANCELARINTERNO.Visible = txtDias.CausesValidation = txtDias.Enabled = ddlSucursal.Enabled = false;//imgExportar.Enabled = 
                    //tdExportar.Attributes.Add("class", "iconoOpcion bg-color-grisClaro02");
                    tdEtiquetaMontoIn.Visible = tdMontoIn.Visible = false;
                    Carga_CelulaCorporativo(corporativo);
                    btnActualizarConfig.ValidationGroup = "VariosUnoPedidos";
                    rfvDiferenciaVacio.ValidationGroup = "VariosUnoPedidos";
                    rvDiferencia.ValidationGroup = "VariosUnoPedidos";
                }

                if (objSolicitdConciliacion.ConsultaArchivo())
                {
                    lblSucursalCelula.Text = "Sucursal Interna";
                    btnActualizarConfig.ValidationGroup = "VariosUno";
                    ddlSucursal.Visible = lblArchivosInternos.Visible = true;
                    Carga_SucursalCorporativo(corporativo);
                }
                txtDias.Enabled = true;
                btnGuardar.Enabled = false;
                ocultarFiltroFechas(tipoConciliacion);

                Carga_TipoFuenteInformacionInterno(Consultas.ConfiguracionTipoFuente.TipoFuenteInformacionInterno);
                activarImportacion(tipoConciliacion);

                ListItem selectedListItem = ddlCriteriosConciliacion.Items.FindByValue(_FormaConciliacion.ToString());
                ddlCriteriosConciliacion.ClearSelection();
                if (selectedListItem != null)
                {
                    selectedListItem.Selected = true;
                }
            }
            dvExpera.Visible = grvPedidos.Rows.Count == 0;//RRV
            if (int.Parse(HttpContext.Current.Session["wucBuscaClientesFacturasVisible"].ToString()) == 1)
                btnFiltraCliente.Visible = true;
            else
                btnFiltraCliente.Visible = false;

            //if (wucClienteDatosBancarios.ClienteSeleccionado != "")
            //{
            //    Cliente objCliente = Conciliacion.RunTime.App.Cliente.CrearObjeto();
            //    Conexion conexion = new Conexion();
            //    conexion.AbrirConexion(true);
            //    grvPedidos.DataSource = objCliente.ObtienePedidosCliente(int.Parse(wucClienteDatosBancarios.ClienteSeleccionado), conexion);
            //    grvPedidos.DataBind();
            //}
            //
            //;
            
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

    /// <summary>
    /// Habilita los controles para buscar pedidos cuando la forma de conciliación
    /// es la 9: "Varios a uno pedidos"
    /// </summary>
    /// <param name="obSolicitud"></param>
    private void HabilitarBusquedaPedidos(SolicitudConciliacion obSolicitud)
    {
        if (obSolicitud.ConsultaPedido())
        {
            CargarDDLBusquedaPedidos();

            tdPedidosLinea.Visible =
                lblBusquedaPedidos.Visible =
                ddlBusquedaPedidos.Visible =
                txtBusquedaPedidos.Visible =
                imbBusquedaPedidos.Visible = true;
        }
        else
        {
            tdPedidosLinea.Visible =
                lblBusquedaPedidos.Visible =
                ddlBusquedaPedidos.Visible =
                txtBusquedaPedidos.Visible =
                imbBusquedaPedidos.Visible = false;
        }
    }

    /// <summary>
    /// Agrega valores al diccionario dcBusquedaPedidos y asigna éste como 
    /// fuente de datos del control ddlBusquedaPedidos
    /// </summary>
    private void CargarDDLBusquedaPedidos()
    {
        dcBusquedaPedidos.Add(1, "Cuenta bancaria");
        dcBusquedaPedidos.Add(2, "Clabe bancaria");
        dcBusquedaPedidos.Add(3, "RFC");
        dcBusquedaPedidos.Add(4, "Referencia de pago");

        ddlBusquedaPedidos.DataSource = dcBusquedaPedidos;
        ddlBusquedaPedidos.DataValueField = "Key";
        ddlBusquedaPedidos.DataTextField = "Value";
        ddlBusquedaPedidos.DataBind();
        ddlBusquedaPedidos.Dispose();
    }

    //Cargar InfoConciliacion Actual
    public void cargarInfoConciliacionActual()
    {
        //Leer variables de URL
        corporativo = Convert.ToInt32(Request.QueryString["Corporativo"]);
        sucursal = Convert.ToInt16(Request.QueryString["Sucursal"]);
        año = Convert.ToInt32(Request.QueryString["Año"]);
        folio = Convert.ToInt32(Request.QueryString["Folio"]);
        mes = Convert.ToSByte(Request.QueryString["Mes"]);
        tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);

    }
    //Limpian variables de Session
    public void limpiarVariablesSession()
    {
        //Eliminar las variables de Session utilizadas en la Vista
        HttpContext.Current.Session["CONCILIADAS"] = null;
        HttpContext.Current.Session["TAB_CONCILIADAS"] = null;
        HttpContext.Current.Session["TAB_INTERNOS"] = null;
        HttpContext.Current.Session["POR_CONCILIAR_INTERNO"] = null;
        HttpContext.Current.Session["POR_CONCILIAR_EXTERNO"] = null;
        HttpContext.Current.Session["POR_CONCILIAR_PEDIDO"] = null;
        HttpContext.Current.Session["TAB_EXTERNOS"] = null;
        HttpContext.Current.Session["TAB_EXTERNOS_01"] = null;
        HttpContext.Current.Session["TAB_INTER_RESP"] = null;
        HttpContext.Current.Session["TAB_INTERNOS"] = null;
        HttpContext.Current.Session["RepDoc"] = null;
        HttpContext.Current.Session["ParametrosReporte"] = null;
        HttpContext.Current.Session["NUEVOS_INTERNOS"] = null;
        HttpContext.Current.Session["DETALLEINTERNO"] = null;

        HttpContext.Current.Session.Remove("CONCILIADAS");
        HttpContext.Current.Session.Remove("TAB_CONCILIADAS");
        HttpContext.Current.Session.Remove("TAB_INTERNOS");
        HttpContext.Current.Session.Remove("POR_CONCILIAR_INTERNO");
        HttpContext.Current.Session.Remove("POR_CONCILIAR_EXTERNO");
        HttpContext.Current.Session.Remove("TAB_EXTERNOS");
        HttpContext.Current.Session.Remove("TAB_EXTERNOS_01");
        HttpContext.Current.Session.Remove("TAB_INTER_RESP");
        HttpContext.Current.Session.Remove("TAB_INTERNOS");
        HttpContext.Current.Session.Remove("RepDoc");
        HttpContext.Current.Session.Remove("ParametrosReporte");
        HttpContext.Current.Session.Remove("NUEVOS_INTERNOS");
        HttpContext.Current.Session.Remove("DETALLEINTERNO");

    }
    public void ocultarOpciones(string config)
    {
        if (config.Equals("EXTERNO"))
            chkReferenciaEx.Enabled = btnENPROCESOEXTERNO.Visible = btnCANCELAREXTERNO.Visible = rdbVerDepositoRetiro.Enabled = false;
        else
            chkReferenciaIn.Enabled = rdbTodosMenoresIn.Enabled = btnENPROCESOINTERNO.Visible = btnCANCELARINTERNO.Visible = false;
    }
    public void encenderOpciones(string config)
    {
        if (config.Equals("EXTERNO"))
            chkReferenciaEx.Enabled = btnENPROCESOEXTERNO.Visible = btnCANCELAREXTERNO.Visible = rdbVerDepositoRetiro.Enabled = true;
        else
            chkReferenciaIn.Enabled = rdbTodosMenoresIn.Enabled = btnENPROCESOINTERNO.Visible = btnCANCELARINTERNO.Visible = true;
    }
    public DateTime obtenerFechaMaximaExterna()
    {
        try
        {
            List<ReferenciaNoConciliada> extSeleccionados = filasSeleccionadasExternos("EN PROCESO DE CONCILIACION");
            return extSeleccionados.OrderByDescending(e => e.FMovimiento).ToList()[0].FMovimiento;
        }
        catch (Exception ex) { return DateTime.Today; }
    }
    public DateTime obtenerFechaMinimaExterna()
    {
        List<ReferenciaNoConciliada> extSeleccionados = filasSeleccionadasExternos("EN PROCESO DE CONCILIACION");
        return extSeleccionados.OrderBy(e => e.FMovimiento).ToList()[0].FMovimiento;
    }
    public bool EsDepositoRetiro()
    {
        return rdbVerDepositoRetiro.SelectedValue.Equals("DEPOSITOS");
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

    /// <summary>
    /// Cargar campos de Filtro y Busqueda externo
    /// </summary>
    public void cargar_ComboCampoFiltroDestino(int tConciliacion, string filtrarEn)
    {
        listCamposDestino = filtrarEn.Equals("Externos") || filtrarEn.Equals("Conciliados")
                                ? Conciliacion.RunTime.App.Consultas.ConsultaDestino()
                                : (tConciliacion != 2
                                       ? Conciliacion.RunTime.App.Consultas.ConsultaDestino()
                                       : Conciliacion.RunTime.App.Consultas.ConsultaDestinoPedido());
    }

    /// <summary>
    /// Cargar campos de Filtro y Busqueda externo
    /// </summary>
    public void enlazarComboCampoFiltrarDestino()
    {

        this.ddlCampoFiltrar.DataSource = listCamposDestino;
        this.ddlCampoFiltrar.DataValueField = "Identificador";
        this.ddlCampoFiltrar.DataTextField = "Descripcion";
        this.ddlCampoFiltrar.DataBind();
        this.ddlCampoFiltrar.Dispose();
    }

    /// <summary>
    /// Cargar Combo de Motivos por lo q no se Cancela la Tranasaccion
    /// </summary>
    public void cargar_ComboMotivosNoConciliado()
    {
        try
        {
            listMotivosNoConciliados = Conciliacion.RunTime.App.Consultas.ConsultaMotivoNoConciliado();
            this.ddlMotivosNoConciliado.DataSource = listMotivosNoConciliados;
            this.ddlMotivosNoConciliado.DataValueField = "Identificador";
            this.ddlMotivosNoConciliado.DataTextField = "Descripcion";
            this.ddlMotivosNoConciliado.DataBind();
            this.ddlMotivosNoConciliado.Dispose();

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
    /// Llena la barra de estado.
    /// </summary>
    public void LlenarBarraEstado()
    {
        try
        {
            cConciliacion c = App.Consultas.ConsultaConciliacionDetalle(corporativo, sucursal, año, mes, folio);
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
    /// Llena el Combo de Formas de Conciliacion
    /// </summary>
    public void Carga_CelulaCorporativo(int corporativo)
    {
        try
        {
        listCelulas = Conciliacion.RunTime.App.Consultas.ConsultaCelula(corporativo);
        this.ddlCelula.DataSource = listCelulas;
        this.ddlCelula.DataValueField = "Identificador";
        this.ddlCelula.DataTextField = "Descripcion";
        this.ddlCelula.DataBind();
        this.ddlCelula.Dispose();
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

    public void Carga_ComboTiposDeCobro()
    {
        try
        {
            IDictionary<int, string> dictTiposDeCobro = new Dictionary<int, string>
            {
                { 10, "Transferencia" },
                { 5, "Efectivo" },
                { 3, "Cheques" },
                { 6, "Tarjeta de Crédito" },
                { 19, "Tarjeta de Débito" }
            };
            this.ddlTiposDeCobro.DataSource = dictTiposDeCobro;
            this.ddlTiposDeCobro.DataTextField = "Value";
            this.ddlTiposDeCobro.DataValueField = "Key";
            this.ddlTiposDeCobro.DataBind();
        }
        catch (Exception)
        {
            throw;
        }
    }

    //Colocar el DropDown de Criterios de Evaluacion en la Actual
    public void ActualizarCriterioEvaluacion()
    {
        try
        {
            ddlCriteriosConciliacion.SelectedValue = ddlCriteriosConciliacion.Items.FindByText("VARIOS A UNO").Value;
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
    /// <summary>
    /// Llena el Combo de Formas de Conciliacion
    /// </summary>
    public void Carga_FormasConciliacion(short tipoConciliacion)
    {
        try
        {
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
    //Crea la paginacion para Concilidos
    protected void grvConciliadas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager && (grvConciliadas.DataSource != null))
        {
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
    }
    //Asignar Valores Css de cada Row
    protected void grvConciliadas_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='bg-color-grisClaro02'");
            e.Row.Attributes.Add("onmouseout", "this.className='bg-color-blanco'");
        }
    }
    //Paginacion de los Concilidos
    protected void grvConciliadas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvConciliadas.PageIndex = e.NewPageIndex;
            LlenaGridViewConciliadas();
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

        grvConciliadas.PageIndex = int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 && iNumPag <= grvConciliadas.PageCount ? iNumPag - 1 : 0;

        LlenaGridViewConciliadas();
    }
    //Consulta transacciones conciliadas
    public void Consulta_TransaccionesConciliadas(int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion, int formaconciliacion)
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
            listaTransaccionesConciliadas = Conciliacion.RunTime.App.Consultas.ConsultaTransaccionesConciliadas(corporativoconciliacion, sucursalconciliacion, añoconciliacion, mesconciliacion, folioconciliacion, formaconciliacion);
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

    private string TipoCobroDescripcion(int tipoCobro)
    {
        if (tipoCobro == 10) //Transferencia
            return "Transferencia";
        else
        if (tipoCobro == 5) //Efectivo, ID: 
            return "Efectivo";
        else
        if (tipoCobro == 3) //c) Cheques, ID: 3
            return "Cheques";
        else
        if (tipoCobro == 6) //d) Tarjeta de crédito, ID: 
            return "Tarjeta de crédito";
        else
        if (tipoCobro == 19) //e) Tarjeta de débito, ID: 
            return "Tarjeta de débito";
        else
            return "";
    }

    //Genera la tabla de transacciones Conciliadas
    public void GenerarTablaConciliados()
    {
        try
        {
            tblTransaccionesConciliadas = new DataTable("TransaccionesConciladas");
            tblTransaccionesConciliadas.Columns.Add("CorporativoConciliacion", typeof(int));
            tblTransaccionesConciliadas.Columns.Add("SucursalConciliacion", typeof(int));
            tblTransaccionesConciliadas.Columns.Add("AñoConciliacion", typeof(int));
            tblTransaccionesConciliadas.Columns.Add("MesConciliacion", typeof(int));
            tblTransaccionesConciliadas.Columns.Add("FolioConciliacion", typeof(int));
            tblTransaccionesConciliadas.Columns.Add("SecuenciaExterno", typeof(int));

            tblTransaccionesConciliadas.Columns.Add("FolioExterno", typeof(int));
            tblTransaccionesConciliadas.Columns.Add("RFCTercero", typeof(string));
            tblTransaccionesConciliadas.Columns.Add("Referencia", typeof(string));
            tblTransaccionesConciliadas.Columns.Add("NombreTercero", typeof(string));
            tblTransaccionesConciliadas.Columns.Add("FMovimiento", typeof(DateTime));
            tblTransaccionesConciliadas.Columns.Add("FOperacion", typeof(DateTime));
            tblTransaccionesConciliadas.Columns.Add("MontoConciliado", typeof(decimal));
            tblTransaccionesConciliadas.Columns.Add("Retiro", typeof(decimal));
            tblTransaccionesConciliadas.Columns.Add("Deposito", typeof(decimal));
            tblTransaccionesConciliadas.Columns.Add("Cheque", typeof(string));
            tblTransaccionesConciliadas.Columns.Add("Concepto", typeof(string));
            tblTransaccionesConciliadas.Columns.Add("Descripcion", typeof(string));
            tblTransaccionesConciliadas.Columns.Add("TipoCobro", typeof(string));

            foreach (ReferenciaNoConciliada rc in listaTransaccionesConciliadas)
            {
                tblTransaccionesConciliadas.Rows.Add(
                    rc.Corporativo,
                    rc.Sucursal,
                    rc.Año,
                    rc.MesConciliacion,
                    rc.FolioConciliacion,
                    rc.Secuencia,
                    rc.Folio,
                    rc.RFCTercero,
                    rc.Referencia,
                    rc.NombreTercero,
                    rc.FMovimiento,
                    rc.FOperacion,
                    rc.Monto,
                    rc.Retiro,
                    rc.Deposito,
                    rc.Cheque,
                    rc.Concepto,
                    rc.Descripcion,
                    TipoCobroDescripcion(rc.TipoCobro) );
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
    //Genera la tabla DetalleTransaccionesConciliadas
    public void GeneraTablaDetalleArchivosInternos(ReferenciaNoConciliada trConciliada)
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
    public string consultaClienteCRM(int cliente)
    {
        RTGMGateway.RTGMGateway Gateway;
        RTGMGateway.SolicitudGateway Solicitud;
        RTGMCore.DireccionEntrega DireccionEntrega = new RTGMCore.DireccionEntrega();
        try
        {
            if (_URLGateway != string.Empty)
            {
                AppSettingsReader settings = new AppSettingsReader();
                SeguridadCB.Public.Usuario usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
                byte modulo = byte.Parse(settings.GetValue("Modulo", typeof(string)).ToString());
                Gateway = new RTGMGateway.RTGMGateway(modulo, App.CadenaConexion);
                Gateway.URLServicio = _URLGateway;
                Solicitud = new RTGMGateway.SolicitudGateway();
                Solicitud.IDCliente = cliente;
                DireccionEntrega = Gateway.buscarDireccionEntrega(Solicitud);
            }
        }
        catch (Exception ex)
        {
            //throw ex;
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
        if (DireccionEntrega != null && DireccionEntrega.Nombre != null)
            return DireccionEntrega.Nombre.Trim();
        else
            return "No encontrado";
    }
    private string ObtieneNombreCliente(List<Cliente> lstClientes, int numCliente, string NombreClienteBD)
    {
        string NombreCliente = "";
        if (_URLGateway != "")
        {
            Cliente cliente = lstClientes.Find(x => x.NumCliente == numCliente);
            if (cliente == null)
            {
                NombreCliente = consultaClienteCRM(numCliente);
                cliente = App.Cliente.CrearObjeto();
                cliente.NumCliente = numCliente;
                cliente.Nombre = NombreCliente;
                lstClientes.Add(cliente);
            }
            else
            {
                NombreCliente = cliente.Nombre;
            }
        }
        else
            NombreCliente = NombreClienteBD;
        return NombreCliente;
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
                SeguridadCB.Public.Parametros parametros;
                parametros = (SeguridadCB.Public.Parametros)HttpContext.Current.Session["Parametros"];
                AppSettingsReader settings = new AppSettingsReader();
                _URLGateway = parametros.ValorParametro(Convert.ToSByte(settings.GetValue("Modulo", typeof(sbyte))), "URLGateway").Trim();
                string NombreCliente = "";
                List<Cliente> lstClientes = new List<Cliente>();
                foreach (ReferenciaConciliadaPedido r in trConciliada.ListaReferenciaConciliada)
                {
                    NombreCliente = ObtieneNombreCliente(lstClientes, r.Cliente, r.Nombre);
                    tblDetalleTransaccionConciliada.Rows.Add(
                        r.Pedido,
                        r.PedidoReferencia,
                        r.AñoPedido,
                        r.CelulaPedido,
                        r.Cliente,
                        NombreCliente, //r.Nombre,
                        r.Total,
                        r.ConceptoPedido
                        );
                }
            }
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }
    }
    public void LlenarGridDetalleInterno(ReferenciaNoConciliada trConciliada)
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
    protected void grvExternos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                CheckBox chkExterno = ((CheckBox)(e.Row.FindControl("chkExterno")));
                if (chkExterno.Checked)
                {
                    e.Row.CssClass = "bg-color-azulClaro01";
                    e.Row.CssClass = "bg-color-azulClaro01 fg-color-blanco";
                    e.Row.Cells[0].CssClass = "bg-color-azulClaro01";
                    e.Row.Cells[1].CssClass = "bg-color-azulClaro01";
                    e.Row.Cells[2].CssClass = "bg-color-azulClaro01";
                }
            }
            catch (Exception ex) { App.ImplementadorMensajes.MostrarMensaje("Error\n" + ex.Message); }
        }
    }

    public void pintarFilaSeleccionadaExterno(int fila)
    {
        grvExternos.Rows[fila].CssClass = "bg-color-azulClaro01 fg-color-blanco";
        grvExternos.Rows[fila].Cells[0].CssClass = "bg-color-azulClaro01";
        grvExternos.Rows[fila].Cells[1].CssClass = "bg-color-azulClaro01";
        grvExternos.Rows[fila].Cells[2].CssClass = "bg-color-azulClaro01";

    }
    public void pintarFilaSeleccionadaArchivoInterno(int fila)
    {
        grvInternos.Rows[fila].CssClass = "bg-color-amarillo";
        grvInternos.Rows[fila].Cells[0].CssClass = "bg-color-amarillo";
        grvInternos.Rows[fila].Cells[1].CssClass = "bg-color-amarillo";
        grvInternos.Rows[fila].Cells[2].CssClass = "bg-color-amarillo";
        grvInternos.Rows[fila].Cells[3].CssClass = "bg-color-amarillo";
    }
    public void pintarFilaSeleccionadaPedido(int fila)
    {
        grvPedidos.Rows[fila].CssClass = "bg-color-amarillo";
        grvPedidos.Rows[fila].Cells[0].CssClass = "bg-color-amarillo";
    }
    public void despintarFilaSeleccionadaExterno(int fila)
    {

        grvExternos.Rows[fila].CssClass = "bg-color-blanco fg-color-negro";
        grvExternos.Rows[fila].Cells[0].CssClass = "bg-color-grisClaro03";
        grvExternos.Rows[fila].Cells[1].CssClass = "bg-color-grisClaro03";
        grvExternos.Rows[fila].Cells[2].CssClass = "bg-color-grisClaro03";

    }
    public void despintarFilaSeleccionadaArchivoInterno(int fila)
    {
        grvInternos.Rows[fila].CssClass = "bg-color-blanco";
        grvInternos.Rows[fila].Cells[0].CssClass = "bg-color-grisClaro03";
        grvInternos.Rows[fila].Cells[1].CssClass = "bg-color-grisClaro03";
        grvInternos.Rows[fila].Cells[2].CssClass = "bg-color-grisClaro03";
        grvInternos.Rows[fila].Cells[3].CssClass = "bg-color-grisClaro03";
    }
    public void despintarFilaSeleccionadaPedido(int fila)
    {
        grvPedidos.Rows[fila].CssClass = "bg-color-blanco";
        grvPedidos.Rows[fila].Cells[0].CssClass = "bg-color-grisClaro03";
    }
    public Consultas.BusquedaPedido obtenerConfiguracionPedido()
    {
        return chkReferenciaIn.Checked
                   ? (rdbTodosMenoresIn.SelectedValue.Equals("TODOS")
                          ? Consultas.BusquedaPedido.ConReferenciaTodos
                          : Consultas.BusquedaPedido.ConReferenciaMenores)
                   : (rdbTodosMenoresIn.SelectedValue.Equals("TODOS")
                          ? Consultas.BusquedaPedido.Todos
                          : Consultas.BusquedaPedido.SinReferenciaMenores);
    }
    //Obtener la configuracion del la consulta de Internos
    public Consultas.ConciliacionInterna obtenerConfiguracionInterno()
    {
        return chkReferenciaIn.Checked
                   ? Consultas.ConciliacionInterna.ConReferencia
                   : Consultas.ConciliacionInterna.SinReferencia;
    }

    public void LimpiarTotalesAgregadosExternos()
    {
        lblMontoAcumuladoExterno.Text = Decimal.Round(0, 2).ToString();
        lblAgregadosExternos.Text = "0";
    }
    protected void grvInternos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (grvInternos.Rows.Count > 0)
                {
                    indiceInternoSeleccionado = 0;
                    (grvInternos.Rows[0].FindControl("rdbSecuenciaIn") as RadioButton).Checked = true;
                    pintarFilaSeleccionadaArchivoInterno(indiceInternoSeleccionado);
                    LimpiarExternosTodos();
                    agregarArchivoInternoExterno();
                }
            }
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error referencias internas:\n" + ex.Message);
        }
    }
    protected void grvInternos_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Attributes.Add("onmouseover", "this.className='bg-color-grisClaro02'");
        //    e.Row.Attributes.Add("onmouseout", "this.className='bg-color-blanco'");
        //}
    }
    protected void grvPedidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvPedidos.PageIndex = e.NewPageIndex;
            DataTable dtSortTable = HttpContext.Current.Session["TAB_INTER_RESP"] as DataTable;
            grvPedidos.DataSource = dtSortTable;
            grvPedidos.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnGuardarVariosAUno_Click(object sender, EventArgs e)
    {
        bool resultado = false;
        try
        {
            hfTipoCobroSeleccionado.Value = ddlTiposDeCobro.SelectedValue;
            //Leer info actual de la conciliación.
            cargarInfoConciliacionActual();

            List<ReferenciaNoConciliada> extSeleccionados = filasSeleccionadasExternos("EN PROCESO DE CONCILIACION");
            /*          Asignar forma de conciliación "Varios a Uno" o  "Varios a Uno Pedidos"           */
            short formaConciliacion = Convert.ToInt16(ddlCriteriosConciliacion.SelectedValue);
            
            SolicitudConciliacion objSolicitdConciliacion = new SolicitudConciliacion();
            objSolicitdConciliacion.TipoConciliacion = tipoConciliacion;
            objSolicitdConciliacion.FormaConciliacion = Convert.ToSByte(Request.QueryString["FormaConciliacion"]);

            if (extSeleccionados.Count == 0)
            {
                throw new Exception(" Seleccione transacciones válidas para continuar.");
            }

            if (objSolicitdConciliacion.ConsultaArchivo()) //if (formaConciliacion == 6)
            {
                foreach (ReferenciaNoConciliada rfNC in extSeleccionados)
                {
                    rfNC.FormaConciliacion = formaConciliacion;
                    foreach (ReferenciaConciliada rfC in rfNC.ListaReferenciaConciliada)
                    {
                        if (rfC.StatusConciliacion == "CONCILIACION CANCELADA")
                        {
                            throw new Exception(rfC.StatusConciliacion + ".Seleccione otra para continuar.");
                        }
                        rfC.FormaConciliacion = formaConciliacion;
                    }
                }
            }
            if (objSolicitdConciliacion.ConsultaPedido())  //if (formaConciliacion == 9)
            {
                foreach (ReferenciaNoConciliada rfNC in extSeleccionados)
                {
                    rfNC.FormaConciliacion = formaConciliacion;
                    foreach (ReferenciaConciliadaPedido rfC in rfNC.ListaReferenciaConciliada)
                    {
                        if (rfC.StatusConciliacion == "CONCILIACION CANCELADA")
                        {
                            throw new Exception(rfC.StatusConciliacion + ".Seleccione otra para continuar.");
                        }
                        rfC.FormaConciliacion = formaConciliacion;
                    }
                }
            }

            //int numRegInter = tipoConciliacion == 4 ? grvPedidos.Rows.Count : grvInternos.Rows.Count;
            int numRegInter = 0;
            if (objSolicitdConciliacion.ConsultaPedido())
                numRegInter = grvPedidos.Rows.Count;
            if (objSolicitdConciliacion.ConsultaArchivo())
                numRegInter = grvInternos.Rows.Count;
            if (extSeleccionados[0].MontoConciliado > 0 && numRegInter > 0)
            {
                foreach (ReferenciaNoConciliada ex in extSeleccionados)
                {
                    if (objSolicitdConciliacion.ConsultaPedido())
                        ex.ConInterno = false;
                    if (objSolicitdConciliacion.ConsultaArchivo())
                        ex.ConInterno = true;
                    ex.TipoCobro = int.Parse(ddlTiposDeCobro.SelectedValue.ToString());

                    //throw new Exception("STOP");

                    if (ex.GuardarReferenciaConciliada())
                    {
                        Consulta_Externos(corporativo, sucursal, año, mes, folio, Convert.ToDecimal(txtDiferencia.Text), tipoConciliacion, Convert.ToInt32(ddlStatusConcepto.SelectedValue), EsDepositoRetiro());
                        GenerarTablaExternos();
                        LlenaGridViewExternos();

                        Consulta_TransaccionesConciliadas(corporativo, sucursal, año, mes, folio, formaConciliacion);
                        GenerarTablaConciliados();
                        LlenaGridViewConciliadas();
                        LlenarBarraEstado();

                        dvExpera.Visible = grvPedidos.Rows.Count == 0;
                        btnRegresarExternos.Visible = false;
                        if (tipoConciliacion == 2)
                        {
                            grvPedidos.Visible = false;
                            grvPedidos.DataSource = null;
                            grvPedidos.DataBind();
                        }
                        else
                        {
                            grvInternos.Visible = false;
                            grvInternos.DataSource = null;
                            grvInternos.DataBind();
                        }
                        tdVerINEX.Attributes.Add("class", "etiqueta lineaVertical centradoMedio bg-color-naranja");
                        lblAgregadosExternos.Text = lblMontoAcumuladoExterno.Text = Convert.ToString(0);
                        lblMontoInterno.Text = "$0.00";

                        ocultarOpciones("INTERNO");
                        encenderOpciones("EXTERNO");
                        grvPedidos.DataSource = null;
                        grvPedidos.DataBind();
                        resultado = true;
                    }
                    else
                        App.ImplementadorMensajes.MostrarMensaje("Ocurrieron conflictos al guardar. Recargue e intente de nuevo.");
                }
            }
            else
                App.ImplementadorMensajes.MostrarMensaje("No se ha seleccionado una referencia interna de forma correcta.");
        }
        catch (Exception Ex)
        {
            resultado = false;  App.ImplementadorMensajes.MostrarMensaje("Error\nVerifique su selección. DEtalles:"+Ex.Message);
        }
        if (resultado)
            App.ImplementadorMensajes.MostrarMensaje("Transacciones guardadas correctamente.");
    }

    public void GenerarTablaArchivosInternos()//Genera la tabla Referencias a Conciliar de Archivos Internos
    {
        tblReferenciaInternas = new DataTable("ReferenciasInternas");
        tblReferenciaInternas.Columns.Add("Secuencia", typeof(int));
        tblReferenciaInternas.Columns.Add("Folio", typeof(int));
        tblReferenciaInternas.Columns.Add("Sucursal", typeof(int));
        tblReferenciaInternas.Columns.Add("Año", typeof(int));
        tblReferenciaInternas.Columns.Add("FMovimiento", typeof(DateTime));
        tblReferenciaInternas.Columns.Add("FOperacion", typeof(DateTime));
        tblReferenciaInternas.Columns.Add("Retiro", typeof(decimal));
        tblReferenciaInternas.Columns.Add("Deposito", typeof(decimal));
        tblReferenciaInternas.Columns.Add("Referencia", typeof(string));
        tblReferenciaInternas.Columns.Add("Descripcion", typeof(string));
        tblReferenciaInternas.Columns.Add("Monto", typeof(decimal));
        tblReferenciaInternas.Columns.Add("Concepto", typeof(string));
        tblReferenciaInternas.Columns.Add("RFCTercero", typeof(string));
        tblReferenciaInternas.Columns.Add("NombreTercero", typeof(string));
        tblReferenciaInternas.Columns.Add("Cheque", typeof(string));
        tblReferenciaInternas.Columns.Add("StatusConciliacion", typeof(string));
        tblReferenciaInternas.Columns.Add("UbicacionIcono", typeof(string));
        tblReferenciaInternas.Columns.Add("Cliente", typeof(int));

        foreach (ReferenciaNoConciliada rc in listaReferenciaArchivosInternos)
        {
            tblReferenciaInternas.Rows.Add(
                rc.Secuencia,
                rc.Folio,
                rc.Sucursal,
                rc.Año,
                rc.FMovimiento,
                rc.FOperacion,
                rc.Retiro,
                rc.Deposito,
                rc.Referencia,
                rc.Descripcion,
                rc.Monto,
                rc.Concepto,
                rc.RFCTercero,
                rc.NombreTercero,
                rc.Cheque,
                rc.StatusConciliacion,
                rc.UbicacionIcono,
                1
                );
        }

        HttpContext.Current.Session["TAB_INTERNOS"] = tblReferenciaInternas;
        HttpContext.Current.Session["TAB_INTER_RESP"] = tblReferenciaInternas;
    }
    private void LlenaGridViewArchivosInternos()//Llena el gridview Referencias Internas
    {
        DataTable tablaReferenciasAi = (DataTable)HttpContext.Current.Session["TAB_INTERNOS"];
        grvInternos.DataSource = tablaReferenciasAi;
        grvInternos.DataBind();
        Session["TABLADEINTERNOS"] = grvInternos;
    }
    private void LlenaGridViewArchivosInternosTemporal()//Llena el gridview con las Referencias Externas
    {
        DataTable tablaReferenciasAi = (DataTable)HttpContext.Current.Session["TAB_INTER_RESP"];
        grvInternos.DataSource = tablaReferenciasAi;
        grvInternos.DataBind();
    }
    public void Consulta_ArchivosInternos(int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion, int sucursalinterno, short dias, decimal diferencia, int statusConcepto, bool esDepositoRetiro, decimal montoExterno, DateTime fmaxima, DateTime fminima)
    {
        SeguridadCB.Seguridad seguridad = new SeguridadCB.Seguridad();
        System.Data.SqlClient.SqlConnection connection = seguridad.Conexion;
        if (connection.State == ConnectionState.Closed)
        {
            seguridad.Conexion.Open();
        }
        try
        {
            //if (hdfInternosControl.Value.Equals("PENDIENTES"))
            //{
            listaReferenciaArchivosInternos =
            Conciliacion.RunTime.App.Consultas.ConsultaDetalleInternoPendienteDeposito(
            obtenerConfiguracionInterno(),
            corporativoconciliacion,
            sucursalconciliacion,
            añoconciliacion,
            mesconciliacion,
            folioconciliacion,
            sucursalinterno,
            dias, diferencia,
            statusConcepto,
            montoExterno,
            esDepositoRetiro,
            fminima, fmaxima
            );

            Session["POR_CONCILIAR_INTERNO"] = listaReferenciaArchivosInternos;
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    public void Consulta_Pedidos(int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion, decimal diferencia, int celula)
    {
        SeguridadCB.Seguridad seguridad = new SeguridadCB.Seguridad();
        System.Data.SqlClient.SqlConnection connection = seguridad.Conexion;
        if (connection.State == ConnectionState.Closed)
        {
            seguridad.Conexion.Open();
        }
        try
        {
            listaReferenciaPedidos = App.Consultas.ConciliacionBusquedaPedidoVariosUno(Consultas.BusquedaPedido.Todos, corporativoconciliacion, sucursalconciliacion, añoconciliacion, mesconciliacion, folioconciliacion, 0, 0, diferencia, celula);
            //Session["POR_CONCILIAR_INTERNO"] = listaReferenciaPedidos;
            Session["POR_CONCILIAR_PEDIDO"] = listaReferenciaPedidos;
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.StackTrace);
        }
    }
    public void GenerarTablaPedidos()//Genera la tabla Referencias a Conciliar de Pedidos.
    {
        tblReferenciaInternas = new DataTable("ReferenciasInternas");
        tblReferenciaInternas.Columns.Add("Pedido", typeof(int));
        tblReferenciaInternas.Columns.Add("PedidoReferencia", typeof(int));
        tblReferenciaInternas.Columns.Add("AñoPed", typeof(int));
        tblReferenciaInternas.Columns.Add("Celula", typeof(int));
        tblReferenciaInternas.Columns.Add("Cliente", typeof(int));
        tblReferenciaInternas.Columns.Add("Nombre", typeof(string));
        tblReferenciaInternas.Columns.Add("FSuministro", typeof(DateTime));
        tblReferenciaInternas.Columns.Add("Total", typeof(decimal));
        tblReferenciaInternas.Columns.Add("Concepto", typeof(string));

        //ReferenciaNoConciliada externoSelec = leerReferenciaExternaSeleccionada();
        //foreach (ReferenciaNoConciliadaPedido rc in listaReferenciaPedidos.Where(rc => !externoSelec.ExisteReferenciaConciliadaPedido(rc.Pedido, rc.CelulaPedido, rc.AñoPedido)))
        foreach (ReferenciaNoConciliadaPedido rc in listaReferenciaPedidos)
        {
            tblReferenciaInternas.Rows.Add(
                rc.Pedido,
                rc.PedidoReferencia,
                rc.AñoPedido,
                rc.CelulaPedido,
                rc.Cliente,
                rc.Nombre,
                rc.FMovimiento,
                rc.Total,
                rc.Concepto
                );
        }
        HttpContext.Current.Session["TAB_INTERNOS"] = tblReferenciaInternas;
        HttpContext.Current.Session["TAB_INTER_RESP"] = tblReferenciaInternas;
    }
    private void LlenaGridViewPedidos()//Llena el gridview dePedidos
    {
        DataTable tablaReferenciasP = (DataTable)HttpContext.Current.Session["TAB_INTERNOS"];
        grvPedidos.PageIndex = 0;
        grvPedidos.DataSource = tablaReferenciasP;
        grvPedidos.DataBind();
    }

    //public void Consulta_ExternosPendientesCancelados(int corporativoconciliacion, short sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion, short sucursalInterno, int folioInterno, int secuenciaInterno, decimal diferencia, int statusConcepto)
    //{
    //    System.Data.SqlClient.SqlConnection connection = SeguridadCB.Seguridad.Conexion;
    //    if (connection.State == ConnectionState.Closed)
    //    {
    //        SeguridadCB.Seguridad.Conexion.Open();
    //    }
    //    try
    //    {
    //        listaReferenciaExternas =
    //            tipoConciliacion == 2
    //                ? Conciliacion.RunTime.App.Consultas.ConsultaDetalleExternoCanceladoPendiente
    //                      (chkReferenciaEx.Checked
    //                           ? Consultas.ConsultaExterno.DepositosConReferenciaPedido
    //                           : Consultas.ConsultaExterno.DepositosPedido,
    //                       corporativoconciliacion, sucursalconciliacion, añoconciliacion, mesconciliacion,
    //                       folioconciliacion, sucursalInterno, folioInterno, secuenciaInterno, statusConcepto, diferencia)
    //                : Conciliacion.RunTime.App.Consultas.ConsultaDetalleExternoCanceladoPendiente
    //                      (chkReferenciaEx.Checked
    //                           ? Consultas.ConsultaExterno.ConReferenciaInterno
    //                           : Consultas.ConsultaExterno.TodoInterno,
    //                       corporativoconciliacion, sucursalconciliacion, añoconciliacion, mesconciliacion,
    //                       folioconciliacion, sucursalInterno, folioInterno, secuenciaInterno, statusConcepto, diferencia);
    //    }
    //    catch (Exception ex)
    //    {
    //        App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
    //    }
    //}
    //Limpian ListasRefencias
    public void LimpiarExternosReferencia(ReferenciaNoConciliada rExterna)
    {
        try
        {
            listaReferenciaExternas = Session["POR_CONCILIAR_EXTERNO"] as List<ReferenciaNoConciliada>;
            listaReferenciaExternas.Where(x => x.Secuencia != rExterna.Secuencia && x.Folio == rExterna.Folio && x.Sucursal == rExterna.Sucursal && x.Año == rExterna.Año)
                                        .Where(x => !x.Completo)
                                        .ToList()
                                        .ForEach(x => x.BorrarReferenciaConciliada());
            Session["POR_CONCILIAR_EXTERNO"] = listaReferenciaExternas;
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error: \n" + ex.Message);
        }
    }
    public void LimpiarExternosTodos()
    {
        try
        {
            listaReferenciaExternas = Session["POR_CONCILIAR_EXTERNO"] as List<ReferenciaNoConciliada>;
            listaReferenciaExternas.ForEach(x => x.BorrarReferenciaConciliada());
            Session["POR_CONCILIAR_EXTERNO"] = listaReferenciaExternas;
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Verificar:\n" + ex.Message);
        }
    }

    public void GenerarTablaExternos()//Genera la tabla Referencias Externas
    {
        try
        {
            tblReferenciaExternas = new DataTable("ReferenciasExternas");
            tblReferenciaExternas.Columns.Add("Selecciona", typeof(bool));
            tblReferenciaExternas.Columns.Add("Secuencia", typeof(int));
            tblReferenciaExternas.Columns.Add("Folio", typeof(int));
            tblReferenciaExternas.Columns.Add("Año", typeof(int));
            tblReferenciaExternas.Columns.Add("ConInterno", typeof(bool));
            tblReferenciaExternas.Columns.Add("FMovimiento", typeof(DateTime));
            tblReferenciaExternas.Columns.Add("FOperacion", typeof(DateTime));
            tblReferenciaExternas.Columns.Add("Referencia", typeof(string));
            tblReferenciaExternas.Columns.Add("RFCTercero", typeof(string));
            tblReferenciaExternas.Columns.Add("NombreTercero", typeof(string));
            tblReferenciaExternas.Columns.Add("Retiro", typeof(decimal));
            tblReferenciaExternas.Columns.Add("Deposito", typeof(decimal));
            tblReferenciaExternas.Columns.Add("Concepto", typeof(string));
            tblReferenciaExternas.Columns.Add("Cheque", typeof(string));
            tblReferenciaExternas.Columns.Add("Descripcion", typeof(string));
            tblReferenciaExternas.Columns.Add("StatusConciliacion", typeof(string));
            tblReferenciaExternas.Columns.Add("UbicacionIcono", typeof(string));

            foreach (ReferenciaNoConciliada rp in listaReferenciaExternas)
                tblReferenciaExternas.Rows.Add(
                          !rp.Selecciona,
                          rp.Secuencia,
                          rp.Folio,
                          rp.Año,
                          rp.ConInterno,
                          rp.FMovimiento,
                          rp.FOperacion,
                          rp.Referencia,
                          rp.RFCTercero,
                          rp.NombreTercero,
                          rp.Retiro,
                          rp.Deposito,
                          rp.Concepto,
                          rp.Cheque,
                          rp.Descripcion,
                          rp.StatusConciliacion,
                          rp.UbicacionIcono);

            HttpContext.Current.Session["TAB_EXTERNOS"] = tblReferenciaExternas;
            HttpContext.Current.Session["TAB_EXTERNOS_01"] = tblReferenciaExternas;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        
    }
    private void LlenaGridViewExternos()//Llena el gridview con las Referencias Externas
    {
        try
        {
            DataTable tablaReferenaciasE = (DataTable)HttpContext.Current.Session["TAB_EXTERNOS"];
            grvExternos.DataSource = tablaReferenaciasE;
            grvExternos.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        
    }
    private void LlenaGridViewExternosTemporal()//Llena el gridview con las Referencias Externas
    {
        DataTable tablaReferenciasE = (DataTable)HttpContext.Current.Session["TAB_EXTERNOS_01"];
        grvExternos.DataSource = tablaReferenciasE;
        grvExternos.DataBind();
    }
    public void Consulta_Externos(int corporativo, int sucursal, int año, short mes, int folio, decimal diferencia, int tipoConciliacion, int statusConcepto, bool esDeposito)
    {
        SeguridadCB.Seguridad seguridad = new SeguridadCB.Seguridad();
        System.Data.SqlClient.SqlConnection connection = seguridad.Conexion;
        if (connection.State == ConnectionState.Closed)
        {
            seguridad.Conexion.Open();
        }

        try
        {
            //listaReferenciaExternas = tipoConciliacion == 2
            //                              ? Conciliacion.RunTime.App.Consultas.ConsultaDetalleExternoPendienteDeposito
            //                                    (chkReferenciaEx.Checked
            //                                         ? Consultas.ConsultaExterno.DepositosConReferenciaPedido
            //                                         : Consultas.ConsultaExterno.DepositosPedido,
            //                                         corporativo, sucursal, año, mes, folio, diferencia, statusConcepto, esDeposito)
            //                              : Conciliacion.RunTime.App.Consultas.ConsultaDetalleExternoPendienteDeposito
            //                                    (chkReferenciaEx.Checked
            //                                         ? Consultas.ConsultaExterno.ConReferenciaInterno
            //                                         : Consultas.ConsultaExterno.TodoInterno,
            //                                         corporativo, sucursal, año, mes, folio, diferencia, statusConcepto, esDeposito);

            tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);
            formaConciliacion = Convert.ToInt16(Request.QueryString["FormaConciliacion"]);
            SolicitudConciliacion objSolicitdConciliacion = new SolicitudConciliacion();
            objSolicitdConciliacion.TipoConciliacion = Convert.ToSByte(tipoConciliacion);
            objSolicitdConciliacion.FormaConciliacion = formaConciliacion;

            if (objSolicitdConciliacion.ConsultaPedido())
                listaReferenciaExternas =
                    Conciliacion.RunTime.App.Consultas.ConsultaDetalleExternoPendienteDeposito(chkReferenciaEx.Checked
                                                         ? Consultas.ConsultaExterno.DepositosConReferenciaPedido
                                                         : Consultas.ConsultaExterno.DepositosPedido,
                                                         corporativo, sucursal, año, mes, folio, diferencia, statusConcepto, esDeposito);
            if (objSolicitdConciliacion.ConsultaArchivo())
                listaReferenciaExternas =
                    Conciliacion.RunTime.App.Consultas.ConsultaDetalleExternoPendienteDeposito
                                                    (chkReferenciaEx.Checked
                                                         ? Consultas.ConsultaExterno.ConReferenciaInterno
                                                         : Consultas.ConsultaExterno.TodoInterno,
                                                         corporativo, sucursal, año, mes, folio, diferencia, statusConcepto, esDeposito);

            Session["POR_CONCILIAR_EXTERNO"] = listaReferenciaExternas;
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

    protected void rdbSecuenciaIn_CheckedChanged(object sender, EventArgs e)
    {
        quitarSeleccionRadio("ARCHIVOINTERNO");
        RadioButton rdb = sender as RadioButton;
        rdb.Checked = true;
        GridViewRow grv = (GridViewRow)rdb.Parent.Parent;
        pintarFilaSeleccionadaArchivoInterno(grv.RowIndex);
        indiceInternoSeleccionado = grv.RowIndex;
        LimpiarExternosTodos();
        agregarArchivoInternoExterno();

    }
    public void agregarArchivoInternoExterno()
    {
        ReferenciaNoConciliada intSeleccionado = leerReferenciaInternaSeleccionada();

        if (!intSeleccionado.StatusConciliacion.Equals("CONCILIACION CANCELADA"))
        {
            List<ReferenciaNoConciliada> extSeleccionados = filasSeleccionadasExternos("EN PROCESO DE CONCILIACION");
            lblMontoInterno.Text = Decimal.Round(intSeleccionado.Monto, 2).ToString("C2");
            foreach (ReferenciaNoConciliada ex in extSeleccionados)
                ex.AgregarReferenciaConciliadaSinVerificacion(intSeleccionado);
        }
        else
        {
            lblMontoInterno.Text = "$0.00";
            LimpiarExternosTodos();
            App.ImplementadorMensajes.MostrarMensaje(
                "No se puede completar la accion. \nLa referencia interna esta cancelada.");

        }
    }
    public void agregarPedidoInternoExterno()
    {
        try
        {

            ReferenciaNoConciliadaPedido pedSeleccionado = leerReferenciaPedidoSeleccionada();

            if (pedSeleccionado != null)
            {
                List<ReferenciaNoConciliada> extSeleccionados = filasSeleccionadasExternos("EN PROCESO DE CONCILIACION");
                lblMontoInterno.Text = Decimal.Round(pedSeleccionado.Total, 2).ToString("C2");
                foreach (ReferenciaNoConciliada ex in extSeleccionados)
                    ex.AgregarReferenciaConciliada(pedSeleccionado);
            }
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }
    public void ConsultarPedidosInternos()
    {
        //Leer info actual de la conciliación.
        cargarInfoConciliacionActual();

        //dvExpera.Visible = grvExternos.Enabled = grvInternos.Enabled = btnVerInternos.Visible = false;
        dvExpera.Visible = grvExternos.Enabled = btnVerInternos.Visible = false;
        btnRegresarExternos.Visible = grvPedidos.Visible = true;
        tdVerINEX.Attributes.Add("class", "etiqueta lineaVertical centradoMedio bg-color-grisOscuro");

        ocultarOpciones("EXTERNO");
        Consulta_Pedidos(corporativo, sucursal, año, mes, folio, Convert.ToDecimal(txtDiferencia.Text), Convert.ToInt32(ddlCelula.SelectedItem.Value));
        GenerarTablaPedidos();
        LlenaGridViewPedidos();

    }

    public void ConsultarArchivosInternos()
    {
        //Leer info actual de la conciliación.
        cargarInfoConciliacionActual();

        //dvExpera.Visible = grvExternos.Enabled = grvInternos.Enabled = btnVerInternos.Visible = false;
        dvExpera.Visible = grvExternos.Enabled = btnVerInternos.Visible = false;
        btnRegresarExternos.Visible = grvInternos.Visible = true;
        tdVerINEX.Attributes.Add("class", "etiqueta lineaVertical centradoMedio bg-color-grisOscuro");

        ocultarOpciones("EXTERNO");
        encenderOpciones("INTERNO");

        Consulta_ArchivosInternos(corporativo, sucursal, año, mes, folio, Convert.ToInt16(ddlSucursal.SelectedItem.Value), Convert.ToSByte(txtDias.Text), Convert.ToDecimal(txtDiferencia.Text), Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value), EsDepositoRetiro(), Convert.ToDecimal(lblMontoAcumuladoExterno.Text), obtenerFechaMaximaExterna(), obtenerFechaMinimaExterna());
        GenerarTablaArchivosInternos();
        LlenaGridViewArchivosInternos();

    }
    public void quitarSeleccionRadio(string nombreGrid)
    {
        switch (nombreGrid)
        {
            case "EXTERNO":
                foreach (
                    RadioButton rb in
                        from GridViewRow gv in grvExternos.Rows
                        select (RadioButton)grvExternos.Rows[gv.RowIndex].FindControl("rdbSecuencia"))
                {
                    rb.Checked = false;
                    despintarFilaSeleccionadaExterno(((GridViewRow)rb.Parent.Parent).RowIndex);
                }
                break;
            case "ARCHIVOINTERNO":
                foreach (
                    RadioButton rb in
                        from GridViewRow gv in grvInternos.Rows
                        select (RadioButton)grvInternos.Rows[gv.RowIndex].FindControl("rdbSecuenciaIn"))
                {
                    rb.Checked = false;
                    despintarFilaSeleccionadaArchivoInterno(((GridViewRow)rb.Parent.Parent).RowIndex);
                }
                break;
            case "PEDIDO":
                foreach (
                    RadioButton rb in
                        from GridViewRow gv in grvPedidos.Rows
                        select (RadioButton)grvPedidos.Rows[gv.RowIndex].FindControl("rdbPedido"))
                {
                    rb.Checked = false;
                    despintarFilaSeleccionadaPedido(((GridViewRow)rb.Parent.Parent).RowIndex);
                }

                break;
        }

    }

    protected void chkReferenciaEx_CheckedChanged(object sender, EventArgs e)
    {
        //Leer info actual de la conciliación.
        cargarInfoConciliacionActual();

        Consulta_Externos(corporativo, sucursal, año, mes, folio, Convert.ToDecimal(txtDiferencia.Text), tipoConciliacion, Convert.ToInt32(ddlStatusConcepto.SelectedValue), EsDepositoRetiro());
        GenerarTablaExternos();
        LlenaGridViewExternos();

    }
    protected void grvConciliadas_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int corporativoConciliacion = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["CorporativoConciliacion"]);
        int sucursalConciliacion = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["SucursalConciliacion"]);
        int añoConciliacion = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["AñoConciliacion"]);
        int mesConciliacion = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["MesConciliacion"]);
        int folioConciliacion = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["FolioConciliacion"]);
        int folioExterno = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["FolioExterno"]);
        int secuenciaExterno = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["SecuenciaExterno"]);

        //Leer las TransaccionesConciliadas
        listaTransaccionesConciliadas = Session["CONCILIADAS"] as List<ReferenciaNoConciliada>;

        ReferenciaNoConciliada tConciliada = listaTransaccionesConciliadas.Single(
                    x => x.Corporativo == corporativoConciliacion &&
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
    protected void grvConciliadas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DESCONCILIAR"))
        {
            //Leer info actual de la conciliación.
            cargarInfoConciliacionActual();

            Button imgDesconciliar = e.CommandSource as Button;
            GridViewRow gRowConciliado = (GridViewRow)(imgDesconciliar).Parent.Parent;

            int corporativoConcilacion = Convert.ToInt32(grvConciliadas.DataKeys[gRowConciliado.RowIndex].Values["CorporativoConciliacion"]);
            int sucursalConciliacion = Convert.ToInt32(grvConciliadas.DataKeys[gRowConciliado.RowIndex].Values["SucursalConciliacion"]);
            int añoConciliacion = Convert.ToInt32(grvConciliadas.DataKeys[gRowConciliado.RowIndex].Values["AñoConciliacion"]);
            int mesConciliacion = Convert.ToInt32(grvConciliadas.DataKeys[gRowConciliado.RowIndex].Values["MesConciliacion"]);
            int folioConciliacion = Convert.ToInt32(grvConciliadas.DataKeys[gRowConciliado.RowIndex].Values["FolioConciliacion"]);
            int folioExterno = Convert.ToInt32(grvConciliadas.DataKeys[gRowConciliado.RowIndex].Values["FolioExterno"]);
            int secuenciaExterno = Convert.ToInt32(grvConciliadas.DataKeys[gRowConciliado.RowIndex].Values["SecuenciaExterno"]);

            //Leer las TransaccionesConciliadas
            listaTransaccionesConciliadas = Session["CONCILIADAS"] as List<ReferenciaNoConciliada>;

            tranDesconciliar = listaTransaccionesConciliadas.Single(
                    x => x.Corporativo == corporativoConcilacion &&
                    x.Sucursal == sucursalConciliacion &&
                    x.Año == añoConciliacion &&
                    x.MesConciliacion == mesConciliacion &&
                    x.FolioConciliacion == folioConciliacion &&
                    x.Folio == folioExterno &&
                    x.Secuencia == secuenciaExterno);

            tranDesconciliar.DesConciliar();
            //Consulta_TransaccionesConciliadas(corporativo, sucursal, año, mes, folio, Convert.ToInt32(ddlCriteriosConciliacion.SelectedValue));
            Consulta_TransaccionesConciliadas(corporativo, sucursal, año, mes, folio, formaConciliacion);
            GenerarTablaConciliados();
            LlenaGridViewConciliadas();
            LlenarBarraEstado();
            //Cargo y refresco nuevamente los archvos externos
            Consulta_Externos(corporativo, sucursal, año, mes, folio, Convert.ToDecimal(txtDiferencia.Text), tipoConciliacion, Convert.ToInt32(ddlStatusConcepto.SelectedValue), EsDepositoRetiro());
            GenerarTablaExternos();
            LlenaGridViewExternos();
        }
    }

    protected void imgFiltrar_Click(object sender, ImageClickEventArgs e)
    {
        //Leer el tipoConciliacion URL
        tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);

        cargar_ComboCampoFiltroDestino(tipoConciliacion, ddlFiltrarEn.SelectedItem.Value);
        enlazarComboCampoFiltrarDestino();
        InicializarControlesFiltro();
        mpeFiltrar.Show();
    }

    protected void chkReferenciaIn_CheckedChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(lblAgregadosExternos.Text) > 0)
        {
            //Leer el tipoConciliacion URL
            tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);

            //CONSULTAR INTERNO(ARCHIVOS O PEDIDOS) TANTO PENDIENTES COMO CANCELADOS
            if (tipoConciliacion == 2)
                ConsultarPedidosInternos();
            else
                ConsultarArchivosInternos();
        }
        else
        {
            App.ImplementadorMensajes.MostrarMensaje("No ha seleccionado ninguna referencia externa correcta.");
        }

    }
    protected void rdbTodosMenoresIn_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(lblAgregadosExternos.Text) > 0)
        {
            //Leer el tipoConciliacion URL
            tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);

            //CONSULTAR INTERNO(ARCHIVOS O PEDIDOS) TANTO PENDIENTES COMO CANCELADOS
            if (tipoConciliacion == 2)
                ConsultarPedidosInternos();
            else
                ConsultarArchivosInternos();
        }
        else
        {
            App.ImplementadorMensajes.MostrarMensaje("No ha seleccionado ninguna referencia externa correcta.");
        }

    }

    protected void grvPedidos_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtSortTable = HttpContext.Current.Session["TAB_INTER_RESP"] as DataTable;
        if (dtSortTable == null) return;
        string order = getSortDirectionString(e.SortExpression);
        dtSortTable.DefaultView.Sort = e.SortExpression + " " + order;
        HttpContext.Current.Session["TAB_INTER_RESP"] = dtSortTable;
        grvPedidos.DataSource = HttpContext.Current.Session["TAB_INTER_RESP"] as DataTable;
        grvPedidos.DataBind();
    }

    public string resaltarBusqueda(string entradaTexto)
    {
        if (!txtBuscar.Text.Equals(""))
        {
            string strBuscar = txtBuscar.Text;
            Regex regExp = new Regex(strBuscar.Replace(" ", "|").Trim(),
                           RegexOptions.IgnoreCase);
            return regExp.Replace(entradaTexto, pintarBusqueda);
        }
        return entradaTexto;
    }
    public string pintarBusqueda(Match m)
    {
        return "<span class=marcarBusqueda>" + m.Value + "</span>";
    }
    private string getSortDirectionString(string columna)
    {
        string sortDirection = "ASC";

        string sortExpression = ViewState["SortExpression"] as string;

        if (sortExpression != null && sortExpression == columna)
        {
            string lastDirection = ViewState["SortDirection"] as string;
            if ((lastDirection != null) && (lastDirection == "ASC"))
            {
                sortDirection = "DESC";
            }
        }

        ViewState["SortDirection"] = sortDirection;
        ViewState["SortExpression"] = columna;
        return sortDirection;
    }

    private void FiltrarCampo(string valorFiltro, string filtroEn)
    {
        try
        {
            DataTable dt = filtroEn.Equals("Externos")
                               ? (DataTable)HttpContext.Current.Session["TAB_EXTERNOS"]
                               : filtroEn.Equals("Internos")
                                     ? (DataTable)HttpContext.Current.Session["TAB_INTERNOS"]
                                     : (DataTable)HttpContext.Current.Session["TAB_CONCILIADAS"];

            DataView dv = new DataView(dt);
            string searchExpression = String.Empty;

            //Leer el tipoConciliacion URL
            tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);

            if (!String.IsNullOrEmpty(valorFiltro))
            {
                searchExpression = string.Format(
                    ddlOperacion.SelectedItem.Value == "LIKE"
                        ? "{0} {1} '%{2}%'"
                        : "{0} {1} '{2}'", ddlCampoFiltrar.SelectedItem.Text,
                    ddlOperacion.SelectedItem.Value, valorFiltro);
            }
            if (dv.Count <= 0) return;
            dv.RowFilter = searchExpression;

            if (filtroEn.Equals("Externos"))
            {
                HttpContext.Current.Session["TAB_EXTERNOS_01"] = dv.ToTable();
                grvExternos.DataSource = HttpContext.Current.Session["TAB_EXTERNOS_01"] as DataTable;
                grvExternos.DataBind();
            }
            else if (filtroEn.Equals("Internos"))
            {
                HttpContext.Current.Session["TAB_INTER_RESP"] = dv.ToTable();
                if (tipoConciliacion == 2)
                {
                    grvPedidos.DataSource = HttpContext.Current.Session["TAB_INTER_RESP"] as DataTable;
                    grvPedidos.DataBind();
                }
                else
                {
                    grvInternos.DataSource = HttpContext.Current.Session["TAB_INTER_RESP"] as DataTable;
                    grvInternos.DataBind();
                }
            }
            else
            {
                ViewState["TAB_CONCILIADAS"] = dv.ToTable();
                grvConciliadas.DataSource = ViewState["TAB_CONCILIADAS"] as DataTable;
                grvConciliadas.DataBind();
            }
            mpeFiltrar.Hide();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Verifique:\n- Valor no valido por tipo de Campo seleccionado.");
            mpeFiltrar.Hide();
        }
    }
    protected void btnIrFiltro_Click(object sender, EventArgs e)
    {
        //Leer el tipoConciliacion URL
        tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);

        cargar_ComboCampoFiltroDestino(tipoConciliacion, ddlFiltrarEn.SelectedItem.Value);

        FiltrarCampo(valorFiltro(tipoCampoSeleccionado()), ddlFiltrarEn.SelectedItem.Value);
        mpeFiltrar.Hide();
    }
    protected void btnIrBuscar_Click(object sender, EventArgs e)
    {
        if (ddlBuscarEn.SelectedItem.Value.Equals("Externos"))
        {
            grvExternos.DataSource = HttpContext.Current.Session["TAB_EXTERNOS_01"] as DataTable;
            grvExternos.DataBind();
        }
        else if (ddlBuscarEn.SelectedItem.Value.Equals("Internos"))
        {
            //Leer el tipoConciliacion URL
            tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);

            if (tipoConciliacion == 2)
            {
                grvPedidos.DataSource = HttpContext.Current.Session["TAB_INTER_RESP"] as DataTable;
                grvPedidos.DataBind();
            }
            else
            {
                grvInternos.DataSource = HttpContext.Current.Session["TAB_INTER_RESP"] as DataTable;
                grvInternos.DataBind();
            }
        }
        else
        {
            grvConciliadas.DataSource = ViewState["TAB_CONCILIADAS"] as DataTable;
            grvConciliadas.DataBind();
        }
        mpeBuscar.Hide();
    }
    //protected void imgBuscar_Click(object sender, ImageClickEventArgs e)
    //{
    //    txtBuscar.Text = String.Empty;
    //    mpeBuscar.Show();
    //}
    protected void grvAgregadosExternos_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;
        e.Row.Attributes.Add("onmouseover", "this.className='bg-color-rojo01'");
        e.Row.Attributes.Add("onmouseout", "this.className='bg-color-blanco'");
    }

    protected void OnCheckedChangedExternos(object sender, EventArgs e)
    {
        CheckBox chk = (sender as CheckBox);
        if (chk.ID == "chkTodosExternos")
            foreach (GridViewRow fila in grvExternos.Rows.Cast<GridViewRow>().Where(fila => fila.RowType == DataControlRowType.DataRow))
                fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked = chk.Checked;
    }
    protected void OnCheckedChangedInternos(object sender, EventArgs e)
    {
        CheckBox chk = (sender as CheckBox);
        if (chk.ID == "chkTodosInternos")
            foreach (GridViewRow fila in grvInternos.Rows.Cast<GridViewRow>().Where(fila => fila.RowType == DataControlRowType.DataRow))
                fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked = chk.Checked;

    }
    public void ocultarOpcionesSeleccionadoExterno()
    {
        if ((from GridViewRow row in grvExternos.Rows where row.RowType == DataControlRowType.DataRow select row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked).Any(estaMarcado => estaMarcado))
        {
            btnENPROCESOEXTERNO.Visible = true;
            btnCANCELAREXTERNO.Visible = true;
        }
        else
        {
            btnENPROCESOEXTERNO.Visible = false;
            btnCANCELAREXTERNO.Visible = false;
        }
    }
    public void ocultarOpcionesSeleccionadoInterno()
    {
        if ((from GridViewRow row in grvInternos.Rows
             where row.RowType == DataControlRowType.DataRow
             select row.Cells[1].Controls.OfType<CheckBox>().FirstOrDefault().Checked).Any(estaMarcado => estaMarcado))
            btnENPROCESOINTERNO.Visible = btnCANCELARINTERNO.Visible = true;
        else
            btnENPROCESOINTERNO.Visible = btnCANCELARINTERNO.Visible = false;
    }
    public List<ReferenciaNoConciliada> filasSeleccionadasExternos(string status)
    {
        listaReferenciaExternas = Session["POR_CONCILIAR_EXTERNO"] as List<ReferenciaNoConciliada>;
        return listaReferenciaExternas.Where(fila => fila.Selecciona == false && fila.StatusConciliacion.Equals(status)).ToList();
    }

    public List<GridViewRow> filasSeleccionadasInternos(string status)
    {
        return grvInternos.Rows.Cast<GridViewRow>().Where(fila => fila.RowType == DataControlRowType.DataRow && (fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked) && (fila.FindControl("imgStatusConciliacion") as System.Web.UI.WebControls.Image).AlternateText.Equals(status)).ToList();
    }
    protected void btnAceptarStatusExterno_Click(object sender, EventArgs e)
    {
        List<ReferenciaNoConciliada> rowsSeleccionados = filasSeleccionadasExternos("EN PROCESO DE CONCILIACION");
        //Leer info actual de la conciliación.
        cargarInfoConciliacionActual();

        foreach (ReferenciaNoConciliada fila in rowsSeleccionados)
        {
            fila.MotivoNoConciliado = Convert.ToInt32(ddlMotivosNoConciliado.SelectedItem.Value);
            fila.ComentarioNoConciliado = txtComentario.Text;

            if (tipoConciliacion == 2)
                fila.CancelarExternoPedido();
            else
                fila.CancelarExternoInterno();

            decimal montoAcumulado = Convert.ToDecimal(lblMontoAcumuladoExterno.Text);
            int extAgregados = Convert.ToInt32(lblAgregadosExternos.Text);
            lblAgregadosExternos.Text = Convert.ToString(extAgregados - 1);
            lblMontoAcumuladoExterno.Text = Decimal.Round((montoAcumulado - fila.Monto), 2).ToString();

        }


        Consulta_Externos(corporativo, sucursal, año, mes, folio, Convert.ToDecimal(txtDiferencia.Text), tipoConciliacion, Convert.ToInt32(ddlStatusConcepto.SelectedValue), EsDepositoRetiro());
        GenerarTablaExternos();
        LlenaGridViewExternosTemporal();
        mpeStatusTransaccion.Hide();
    }
    protected void btnAceptarStatusInterno_Click(object sender, EventArgs e)
    {
        int secuenciaInt;
        int folioInt;
        ReferenciaNoConciliada rfInterna;
        List<GridViewRow> rowsSeleccionados = filasSeleccionadasInternos("EN PROCESO DE CONCILIACION");
        //Leer Referencias Internas
        listaReferenciaArchivosInternos = Session["POR_CONCILIAR_INTERNO"] as List<ReferenciaNoConciliada>;
        foreach (GridViewRow fila in rowsSeleccionados)
        {
            secuenciaInt = Convert.ToInt32(grvInternos.DataKeys[fila.RowIndex].Values["Secuencia"]);
            folioInt = Convert.ToInt32(grvInternos.DataKeys[fila.RowIndex].Values["Folio"]);
            rfInterna = listaReferenciaArchivosInternos.Single(x => x.Secuencia == secuenciaInt && x.Folio == folioInt);
            rfInterna.MotivoNoConciliado = Convert.ToInt32(ddlMotivosNoConciliado.SelectedItem.Value);
            rfInterna.ComentarioNoConciliado = txtComentario.Text;
            rfInterna.CancelarInterno();
        }
        ConsultarArchivosInternos();
        mpeStatusTransaccion.Hide();
    }

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
                case "Numero":
                    decimal num = Convert.ToDecimal(txtValor.Text);
                    return num.ToString(CultureInfo.InvariantCulture);
                case "Fecha":
                    DateTime fecha = Convert.ToDateTime(txtValor.Text);
                    return fecha.ToString(CultureInfo.InvariantCulture);
            }

        }
        catch (Exception)
        {
            App.ImplementadorMensajes.MostrarMensaje("Verifique:\n- Valor no valido por tipo de Campo seleccionado.");
        }
        return string.Empty;
    }
    public void InicializarControlesFiltro()
    {
        ddlCampoFiltrar.SelectedIndex = ddlOperacion.SelectedIndex = 0;
        txtValor.Text = String.Empty;
    }
    public void activarOpcionesCancelarProcesoIn(bool activar)
    {
        btnENPROCESOINTERNO.Visible = btnCANCELARINTERNO.Visible = activar;
    }
    public void activarOpcionesCancelarProcesoEx(bool activar)
    {
        btnENPROCESOEXTERNO.Visible = btnCANCELAREXTERNO.Visible = activar;
    }
    public bool referenciaExInCancelada(string tpReferencia)
    {
        return tpReferencia.Equals("Externo") ? !(grvExternos.Rows[indiceExternoSeleccionado].FindControl("lblStatusConciliacion") as Label).Text.Equals("CONCILIACION CANCELADA")
            : !(grvInternos.Rows[indiceInternoSeleccionado].FindControl("lblStatusConciliacion") as Label).Text.Equals("CONCILIACION CANCELADA");
    }
    public ReferenciaNoConciliada leerReferenciaExternaSeleccionada()
    {
        listaReferenciaExternas = Session["POR_CONCILIAR_EXTERNO"] as List<ReferenciaNoConciliada>;

        int secuenciaExterno = Convert.ToInt32(grvExternos.DataKeys[indiceExternoSeleccionado].Values["Secuencia"]);
        int folioExterno = Convert.ToInt32(grvExternos.DataKeys[indiceExternoSeleccionado].Values["Folio"]);
        return listaReferenciaExternas.Single(x => x.Secuencia == secuenciaExterno && x.Folio == folioExterno);
    }
    public ReferenciaNoConciliada leerReferenciaInternaSeleccionada()
    {
        //Leer Referencias Internas
        listaReferenciaArchivosInternos = Session["POR_CONCILIAR_INTERNO"] as List<ReferenciaNoConciliada>;
        int secuenciaInterno =
                        Convert.ToInt32(grvInternos.DataKeys[indiceInternoSeleccionado].Values["Secuencia"]);
        int folioInterno = Convert.ToInt32(grvInternos.DataKeys[indiceInternoSeleccionado].Values["Folio"]);
        int sucursalInterno = Convert.ToInt16(grvInternos.DataKeys[indiceInternoSeleccionado].Values["Sucursal"]);

        return listaReferenciaArchivosInternos.Single(x => x.Secuencia == secuenciaInterno && x.Folio == folioInterno && x.Sucursal == sucursalInterno);
    }
    public ReferenciaNoConciliadaPedido leerReferenciaPedidoSeleccionada()
    {
        //listaReferenciaPedidos = Session["POR_CONCILIAR_INTERNO"] as List<ReferenciaNoConciliadaPedido>;
        listaReferenciaPedidos = Session["POR_CONCILIAR_PEDIDO"] as List<ReferenciaNoConciliadaPedido>;

        int celula = Convert.ToInt32(grvPedidos.DataKeys[indiceInternoSeleccionado].Values["Celula"]);
        int pedido = Convert.ToInt32(grvPedidos.DataKeys[indiceInternoSeleccionado].Values["Pedido"]);
        int añoPed = Convert.ToInt32(grvPedidos.DataKeys[indiceInternoSeleccionado].Values["AñoPed"]);
        int cliente = Convert.ToInt32(grvPedidos.DataKeys[indiceInternoSeleccionado].Values["Cliente"]);

        if (listaReferenciaPedidos != null)
        {
            if (listaReferenciaPedidos.Count > 0)
            {
                return listaReferenciaPedidos.Single(x => x.CelulaPedido == celula && x.Pedido == pedido && x.AñoPedido == añoPed && x.Cliente == cliente);
            }
        }
        return null;
    }

    protected void btnENPROCESOEXTERNO_Click(object sender, ImageClickEventArgs e)
    {
        List<ReferenciaNoConciliada> rowsSeleccionados = filasSeleccionadasExternos("CONCILIACION CANCELADA");
        //Leer info actual de la conciliación.
        cargarInfoConciliacionActual();

        if (rowsSeleccionados.Count > 0)
        {
            foreach (ReferenciaNoConciliada fila in rowsSeleccionados)
            {
                if (tipoConciliacion == 2)
                    fila.EliminarReferenciaConciliadaPedido();
                else
                    fila.EliminarReferenciaConciliada();
            }


            Consulta_Externos(corporativo, sucursal, año, mes, folio, Convert.ToDecimal(txtDiferencia.Text), tipoConciliacion, Convert.ToInt32(ddlStatusConcepto.SelectedValue), EsDepositoRetiro());
            GenerarTablaExternos();
            LlenaGridViewExternos();
            LimpiarTotalesAgregadosExternos();
        }
        else
            App.ImplementadorMensajes.MostrarMensaje("Verifique su selección , siguientes razones: \n" +
                                                     "1. No existe ninguna referencia externa seleccionada. \n" +
                                                     "2. Ninguna de las referencias externas seleccionadas estan CANCELADAS");
    }
    protected void btnCANCELAREXTERNO_Click(object sender, ImageClickEventArgs e)
    {
        if (filasSeleccionadasExternos("EN PROCESO DE CONCILIACION").Count > 0)
        {
            btnAceptarStatusExterno.Visible = true;
            dvMensajeExterno.Visible = true;
            btnAceptarStatusInterno.Visible = false;
            txtComentario.Text = String.Empty;
            mpeStatusTransaccion.Show();
        }
        else
            App.ImplementadorMensajes.MostrarMensaje("Verifique su selección , siguientes razones: \n" +
                                                   "1. No existe ninguna referencia externa seleccionada. \n" +
                                                   "2. Ninguna de las referencias externas seleccionadas estan EN PROCESO DE CONCILIACIÓN");
    }

    protected void grvInternos_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtSortTable = HttpContext.Current.Session["TAB_INTER_RESP"] as DataTable;
        if (dtSortTable == null) return;
        string order = getSortDirectionString(e.SortExpression);
        dtSortTable.DefaultView.Sort = e.SortExpression + " " + order;
        grvInternos.DataSource = dtSortTable;
        grvInternos.DataBind();
    }
    protected void grvExternos_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtSortTable = HttpContext.Current.Session["TAB_EXTERNOS_01"] as DataTable;
        if (dtSortTable == null) return;
        string order = getSortDirectionString(e.SortExpression);
        dtSortTable.DefaultView.Sort = e.SortExpression + " " + order;

        HttpContext.Current.Session["TAB_EXTERNOS_01"] = dtSortTable;
        LlenaGridViewExternosTemporal();

        ReferenciaNoConciliada rfExterno = leerReferenciaExternaSeleccionada();
        //Limpiar Referncias de Externos 
        LimpiarExternosReferencia(rfExterno);
    }
    protected void btnENPROCESOINTERNO_Click(object sender, ImageClickEventArgs e)
    {
        List<GridViewRow> rowsSeleccionados = filasSeleccionadasInternos("CONCILIACION CANCELADA");
        //Leer Referencias Internas
        listaReferenciaArchivosInternos = Session["POR_CONCILIAR_INTERNO"] as List<ReferenciaNoConciliada>;

        if (rowsSeleccionados.Count > 0)
        {
            int secuenciaInterno;
            int folioInterno;
            ReferenciaNoConciliada rfInterno;
            foreach (GridViewRow fila in rowsSeleccionados)
            {
                secuenciaInterno = Convert.ToInt32(grvInternos.DataKeys[fila.RowIndex].Values["Secuencia"]);
                folioInterno = Convert.ToInt32(grvInternos.DataKeys[fila.RowIndex].Values["Folio"]);
                rfInterno =
                    listaReferenciaArchivosInternos.Single(x => x.Secuencia == secuenciaInterno && x.Folio == folioInterno);
                rfInterno.EliminarReferenciaConciliadaInterno();
            }
            ConsultarArchivosInternos();
        }
        else
            App.ImplementadorMensajes.MostrarMensaje("Verifique su selección , siguientes razones: \n" +
                                                    "1. No existe ninguna referencia interna seleccionada. \n" +
                                                    "2. Ninguna de las referencias internas seleccionadas estan CANCELADAS");

    }
    protected void btnCANCELARINTERNO_Click(object sender, ImageClickEventArgs e)
    {
        if (filasSeleccionadasInternos("EN PROCESO DE CONCILIACION").Count > 0)
        {
            btnAceptarStatusExterno.Visible = false;
            dvMensajeExterno.Visible = false;
            btnAceptarStatusInterno.Visible = true;
            txtComentario.Text = String.Empty;
            mpeStatusTransaccion.Show();
        }
        else
            App.ImplementadorMensajes.MostrarMensaje("Verifique su selección , siguientes razones: \n" +
                                                   "1. No existe ninguna referencia interna seleccionada. \n" +
                                                   "2. Ninguna de las referencias internas seleccionadas estan EN PROCESO DE CONCILIACIÓN");

    }
    protected void grvConciliadas_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtSortTable = ViewState["TAB_CONCILIADAS"] as DataTable;

        if (dtSortTable == null) return;
        string order = getSortDirectionString(e.SortExpression);
        dtSortTable.DefaultView.Sort = e.SortExpression + " " + order;

        grvConciliadas.DataSource = dtSortTable;
        grvConciliadas.DataBind();
    }
    protected void btnActualizarConfig_Click(object sender, ImageClickEventArgs e)
    {
        //Leer info actual de la conciliación.
        cargarInfoConciliacionActual();

        Consulta_Externos(corporativo, sucursal, año, mes, folio, Convert.ToDecimal(txtDiferencia.Text), tipoConciliacion, Convert.ToInt32(ddlStatusConcepto.SelectedValue), EsDepositoRetiro());
        GenerarTablaExternos();
        LlenaGridViewExternos();

        dvExpera.Visible = grvExternos.Enabled = btnVerInternos.Visible = true;
        btnRegresarExternos.Visible = false;
        if (tipoConciliacion == 2)
        {
            grvPedidos.Visible = false;
            grvPedidos.DataSource = null;
            grvPedidos.DataBind();
        }
        else
        {
            grvInternos.Visible = false;
            grvInternos.DataSource = null;
            grvInternos.DataBind();
        }
        tdVerINEX.Attributes.Add("class", "etiqueta lineaVertical centradoMedio bg-color-naranja");
        ocultarOpciones("INTERNO");
        encenderOpciones("EXTERNO");
        lblMontoInterno.Text = "$0.00";
        //LimpiarExternosTodos();
        LimpiarTotalesAgregadosExternos();
    }
    protected void imgAutomatica_Click(object sender, ImageClickEventArgs e)
    {
        Enrutador objEnrutador = new Enrutador();
        string criterioConciliacion = "";

        criterioConciliacion = objEnrutador.ObtieneURLSolicitud(new SolicitudEnrutador(Convert.ToSByte(Request.QueryString["TipoConciliacion"]),
                                                                                       Convert.ToSByte(ddlCriteriosConciliacion.SelectedValue)));

        HttpContext.Current.Session["criterioConciliacion"] = criterioConciliacion;
        //Leer info actual de la conciliación.
        cargarInfoConciliacionActual();

        Response.Redirect("~/Conciliacion/FormasConciliar/" + criterioConciliacion +
                                      ".aspx?Folio=" + folio + "&Corporativo=" + corporativo +
                                      "&Sucursal=" + sucursal + "&Año=" + año + "&Mes=" +
                                      mes + "&TipoConciliacion=" + tipoConciliacion + "&FormaConciliacion=" + Convert.ToSByte(ddlCriteriosConciliacion.SelectedValue));
    }
    protected void Nueva_Ventana(string pagina, string titulo, int ancho, int alto, int x, int y)
    {

        ScriptManager.RegisterClientScriptBlock(upBarraHerramientas,
                                         upBarraHerramientas.GetType(),
                                            "ventana",
                                            "ShowWindow('" + pagina + "','" + titulo + "'," + ancho + "," + alto + "," + x + "," + y + ")",
                                            true);

    }

    protected void imgExportar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            AppSettingsReader settings = new AppSettingsReader();

            string strReporte = Server.MapPath("~/") + settings.GetValue("RutaReporteConciliacionTesoreria", typeof(string));
            if (!File.Exists(strReporte)) return;
            try
            {
                string strServer = settings.GetValue("Servidor", typeof(string)).ToString();
                string strDatabase = settings.GetValue("Base", typeof(string)).ToString();
                //Leer info actual de la conciliación.
                cargarInfoConciliacionActual();

                usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
                string strUsuario = usuario.IdUsuario.Trim();
                string strPW = usuario.ClaveDesencriptada;
                ArrayList Par = new ArrayList();

                Par.Add("@Corporativo=" + corporativo);
                Par.Add("@Sucursal=" + sucursal);
                Par.Add("@AñoConciliacion=" + año);
                Par.Add("@MesConciliacion=" + mes);
                Par.Add("@FolioConciliacion=" + folio);
                ClaseReporte reporte = new ClaseReporte(strReporte, Par, strServer, strDatabase, strUsuario, strPW);
                HttpContext.Current.Session["RepDoc"] = reporte.RepDoc;
                HttpContext.Current.Session["ParametrosReporte"] = Par;
                Nueva_Ventana("../../Reporte/Reporte.aspx", "Carta", 0, 0, 0, 0);
                reporte = null;
            }
            catch (Exception ex)
            {
                App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
            }
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    protected void imgCerrarConciliacion_Click(object sender, ImageClickEventArgs e)
    {
        usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
        //Leer info actual de la conciliación.
        cargarInfoConciliacionActual();

        cConciliacion c = App.Consultas.ConsultaConciliacionDetalle(corporativo, sucursal, año, mes, folio);
        if (c.CerrarConciliacion(usuario.IdUsuario))
        {
            App.ImplementadorMensajes.MostrarMensaje("CONCILIACIÓN CERRADA EXITOSAMENTE");
            System.Threading.Thread.Sleep(3000);
            Response.Redirect("~/Conciliacion/DetalleConciliacion.aspx?Folio=" + folio + "&Corporativo=" + corporativo +
                                    "&Sucursal=" + sucursal + "&Año=" + año + "&Mes=" +
                                    mes + "&TipoConciliacion=" + tipoConciliacion);
        }
        else
        {
            App.ImplementadorMensajes.MostrarMensaje("ERRORES AL CERRAR LA CONCILIACIÓN");
        }
    }

    protected void imgCancelarConciliacion_Click(object sender, ImageClickEventArgs e)
    {
        usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
        //Leer info actual de la conciliación.
        cargarInfoConciliacionActual();

        cConciliacion c = App.Consultas.ConsultaConciliacionDetalle(corporativo, sucursal, año, mes, folio);
        if (c.CancelarConciliacion(usuario.IdUsuario))
        {
            App.ImplementadorMensajes.MostrarMensaje("CONCILIACIÓN CANCELADA EXITOSAMENTE");
            System.Threading.Thread.Sleep(3000);
            Response.Redirect("~/Inicio.aspx");
        }
        else
        {
            App.ImplementadorMensajes.MostrarMensaje("ERRORES AL CANCELAR LA CONCILIACIÓN");
        }
    }

    public void ocultarFiltroFechas(short tpConciliacion)
    {
        short _formaConciliacion = Convert.ToInt16(Request.QueryString["FormaConciliacion"]);
        SolicitudConciliacion objSolicitdConciliacion = new SolicitudConciliacion();
        objSolicitdConciliacion.TipoConciliacion = tpConciliacion;
        objSolicitdConciliacion.FormaConciliacion = _formaConciliacion;
        
        lblFOperacion.Visible =
            txtFOInicio.Visible =
            txtFOTermino.Visible =
            btnRangoFechasFO.Visible =
            rvFOInicio.Visible =
            rvFMTermino.Visible = objSolicitdConciliacion.ConsultaArchivo();

        lblFMovimiento.Visible =
                    txtFMInicio.Visible =
                    txtFMTermino.Visible =
                    btnRangoFechasFM.Visible =
                    rvFMInicio.Visible =
                    rvFMTermino.Visible = objSolicitdConciliacion.ConsultaArchivo();

        lblFSuminstro.Visible =
                txtFSInicio.Visible =
                txtFSTermino.Visible =
                btnRangoFechasFS.Visible =
                rvFSInicio.Visible =
                rvFSTermino.Visible = objSolicitdConciliacion.ConsultaPedido();
    }

    protected void btnRangoFechasFO_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dt = (DataTable)HttpContext.Current.Session["TAB_INTERNOS"];
            DataView dv = new DataView(dt);

            string SearchExpression = String.Empty;
            if (!(String.IsNullOrEmpty(txtFOInicio.Text) || String.IsNullOrEmpty(txtFOTermino.Text)))
            {
                SearchExpression = string.Format("FOperacion >= '{0}' AND FOperacion <= '{1}'", txtFOInicio.Text, txtFOTermino.Text);
            }
            if (dv.Count <= 0) return;
            dv.RowFilter = SearchExpression;
            HttpContext.Current.Session["TAB_INTER_RESP"] = dv.ToTable();
            grvInternos.DataSource = HttpContext.Current.Session["TAB_INTER_RESP"] as DataTable;
            grvInternos.DataBind();

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    protected void btnRangoFechasFM_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dt = (DataTable)HttpContext.Current.Session["TAB_INTERNOS"];
            DataView dv = new DataView(dt);

            string SearchExpression = String.Empty;
            if (!(String.IsNullOrEmpty(txtFMInicio.Text) || String.IsNullOrEmpty(txtFMTermino.Text)))
            {
                SearchExpression = string.Format("FMovimiento >= '{0}' AND FMovimiento <= '{1}'", txtFMInicio.Text, txtFMTermino.Text);
            }
            if (dv.Count <= 0) return;
            dv.RowFilter = SearchExpression;
            HttpContext.Current.Session["TAB_INTER_RESP"] = dv.ToTable();
            grvInternos.DataSource = HttpContext.Current.Session["TAB_INTER_RESP"] as DataTable;
            grvInternos.DataBind();

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error\n" + ex.Message);
        }
    }

    protected void btnRangoFechasFS_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //DataTable dt = (DataTable)HttpContext.Current.Session["TAB_INTERNOS"];
            //DataView dv = new DataView(dt);

            //string SearchExpression = String.Empty;
            //if (!(String.IsNullOrEmpty(txtFSInicio.Text) || String.IsNullOrEmpty(txtFSTermino.Text)))
            //{
            //    SearchExpression = string.Format("FSuministro >= '{0}' AND FSuministro <= '{1}'", txtFSInicio.Text, txtFSTermino.Text);
            //}
            //if (dv.Count <= 0) return;
            //dv.RowFilter = SearchExpression;
            //HttpContext.Current.Session["TAB_INTER_RESP"] = dv.ToTable();
            //grvPedidos.DataSource = HttpContext.Current.Session["TAB_INTER_RESP"] as DataTable;
            //grvPedidos.DataBind();

            listaReferenciaPedidos = Session["POR_CONCILIAR_PEDIDO"] as List<ReferenciaNoConciliadaPedido>;
            List<ReferenciaNoConciliadaPedido> listaFiltrada;
            if (!(String.IsNullOrEmpty(txtFSInicio.Text) || String.IsNullOrEmpty(txtFSTermino.Text)))
            {
                listaFiltrada = listaReferenciaPedidos.
                                    Where(x => { bool v =
                                                    x.FSuministro >= DateTime.Parse(txtFSInicio.Text) & x.FSuministro <= DateTime.Parse(txtFSTermino.Text + " 23:59:59");
                                        return v;
                                    })
                                    .ToList();
                grvPedidos.DataSource = listaFiltrada;
                grvPedidos.DataBind();
            }
            else
            {
                btnFiltraCliente_Click(btnFiltraCliente, null);
            }

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    public ReferenciaNoConciliadaPedido leerReferenciaPedidoSeleccionada(int rowIndex)
    {
        //listaReferenciaPedidos = Session["POR_CONCILIAR_INTERNO"] as List<ReferenciaNoConciliadaPedido>;
        listaReferenciaPedidos = Session["POR_CONCILIAR_PEDIDO"] as List<ReferenciaNoConciliadaPedido>;

        int pedido = Convert.ToInt32(grvPedidos.DataKeys[rowIndex].Values["Pedido"]);
        int celulaPedido = Convert.ToInt32(grvPedidos.DataKeys[rowIndex].Values["Celula"]);
        int añoPedido = Convert.ToInt32(grvPedidos.DataKeys[rowIndex].Values["AñoPed"]);
        return listaReferenciaPedidos.Single(s => s.Pedido == pedido && s.CelulaPedido == celulaPedido && s.AñoPedido == añoPedido);
    }
    protected void grvExternos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvExternos.PageIndex = e.NewPageIndex;
            LlenaGridViewExternosTemporal();
        }
        catch (Exception)
        {

        }
    }
    protected void chkExterno_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = sender as CheckBox;
        GridViewRow grv = (GridViewRow)chk.Parent.Parent;

        indiceExternoSeleccionado = grv.RowIndex;
        ReferenciaNoConciliada rfEx = leerReferenciaExternaSeleccionada();

        if (chk.Checked)
        {
            //if (rfEx.StatusConciliacion == "CONCILIACION CANCELADA")
            //{
            //    chk.Checked = false;
            //    App.ImplementadorMensajes.MostrarMensaje(rfEx.StatusConciliacion + ". Seleccione otra para continuar.");
            //}
            rfEx.Selecciona = false;//Es solo para guardar la REFERENCIA SELECCIONADA..FALSE porq se hace un ! negacion..al cargar el Externos..para no modificar otra cosa.
            GenerarTablaExternos();
            if (rfEx.StatusConciliacion.Equals("EN PROCESO DE CONCILIACION"))
            {
                decimal montoAcumulado = Convert.ToDecimal(lblMontoAcumuladoExterno.Text);
                int extAgregados = Convert.ToInt32(lblAgregadosExternos.Text);
                lblAgregadosExternos.Text = Convert.ToString(extAgregados + 1);
                lblMontoAcumuladoExterno.Text = Decimal.Round((montoAcumulado + rfEx.Monto), 2).ToString();
            }
            pintarFilaSeleccionadaExterno(grv.RowIndex);
        }
        else
        {
            rfEx.Selecciona = true;
            GenerarTablaExternos();
            if (rfEx.StatusConciliacion.Equals("EN PROCESO DE CONCILIACION"))
            {
                decimal montoAcumulado = Convert.ToDecimal(lblMontoAcumuladoExterno.Text);
                int extAgregados = Convert.ToInt32(lblAgregadosExternos.Text);
                lblAgregadosExternos.Text = Convert.ToString(extAgregados - 1);
                lblMontoAcumuladoExterno.Text = Decimal.Round((montoAcumulado - rfEx.Monto), 2).ToString();
            }
            despintarFilaSeleccionadaExterno(grv.RowIndex);
        }
    }
    protected void btnVerInternos_Click(object sender, ImageClickEventArgs e)
    {
        //Leer el tipoConciliacion URL
        tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);
        formaConciliacion = Convert.ToInt16(Request.QueryString["FormaConciliacion"]); //const short _FormaConciliacion = 6;

        dvExpera.Visible = grvPedidos.Rows.Count == 0;

        SolicitudConciliacion objSolicitdConciliacion = new SolicitudConciliacion();
        objSolicitdConciliacion.TipoConciliacion = tipoConciliacion;
        objSolicitdConciliacion.FormaConciliacion = formaConciliacion;

        if (Convert.ToInt32(lblAgregadosExternos.Text) > 0)
        {
            if (objSolicitdConciliacion.ConsultaPedido())
            {
                ConsultarPedidosInternos();
            }
            if (objSolicitdConciliacion.ConsultaArchivo())
            {
                ConsultarArchivosInternos();
            }
        }
        else
        {
            App.ImplementadorMensajes.MostrarMensaje("No ha seleccionado ninguna referencia externa correcta.");
        }
    }

    protected void btnRegresarExternos_Click(object sender, ImageClickEventArgs e)
    {
        //dvExpera.Visible = grvExternos.Enabled = btnVerInternos.Visible = true;//RRV
        dvExpera.Visible = grvPedidos.Rows.Count == 0;//RRV
        grvExternos.Enabled = btnVerInternos.Visible = true;
        btnRegresarExternos.Visible = false;

        //Leer el tipoConciliacion URL
        tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);

        if (tipoConciliacion == 2)
        {
            grvPedidos.Visible = false;
            grvPedidos.DataSource = null;
            grvPedidos.DataBind();
        }
        else
        {
            grvInternos.Visible = false;
            grvInternos.DataSource = null;
            grvInternos.DataBind();
        }

        tdVerINEX.Attributes.Add("class", "etiqueta lineaVertical centradoMedio bg-color-naranja");
        ocultarOpciones("INTERNO");
        encenderOpciones("EXTERNO");
        lblMontoInterno.Text = "$0.00";
        LimpiarExternosTodos();
    }
    protected void grvInternos_DataBound(object sender, EventArgs e)
    {
        if (grvInternos.Rows.Count <= 0)
        {
            LimpiarExternosTodos();
            lblMontoInterno.Text = "$0.00";
        }
    }
    protected void grvInternos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvInternos.PageIndex = e.NewPageIndex;
        LlenaGridViewArchivosInternos();
    }
    protected void rdbPedido_CheckedChanged(object sender, EventArgs e)
    {
        //if (grvInternos.Enabled)
        //{
            quitarSeleccionRadio("PEDIDO");
            RadioButton rdb = sender as RadioButton;
            rdb.Checked = true;
            GridViewRow grv = (GridViewRow)rdb.Parent.Parent;
            pintarFilaSeleccionadaPedido(grv.RowIndex);
            indiceInternoSeleccionado = grv.RowIndex;
            LimpiarExternosTodos();
            agregarPedidoInternoExterno();
        //}
    }
    protected void grvPedidos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (grvPedidos.Rows.Count > 0)
                {
                    indiceInternoSeleccionado = 0;
                    ((RadioButton)grvPedidos.Rows[0].FindControl("rdbPedido")).Checked = true;
                    pintarFilaSeleccionadaPedido(indiceInternoSeleccionado);
                    LimpiarExternosTodos();
                    agregarPedidoInternoExterno();
                }
            }
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error referencias internas:\n" + ex.Message);
        }
    }
    protected void grvPedidos_DataBound(object sender, EventArgs e)
    {
        if (grvPedidos.Rows.Count <= 0)
        {
            LimpiarExternosTodos();
            lblMontoInterno.Text = "$0.00";
        }
    }

    //------------------------------INICIO MODULO "AGREGAR NUEVO INTERNO"---------------------------------
    protected void imgImportar_Click(object sender, ImageClickEventArgs e)
    {
        Carga_TipoFuenteInformacionInterno(Consultas.ConfiguracionTipoFuente.TipoFuenteInformacionInterno);
        limpiarVistaImportarInterno();
        enlazarComboFolioInterno();
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
        catch (SqlException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlTipoFuenteInfoInterno_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Leer Variables URL 
        cargarInfoConciliacionActual();

        Carga_FoliosInternos(
                       corporativo,
                       Convert.ToInt32(ddlSucursalInterno.SelectedItem.Value),
                       año,
                       mes,
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
                              corporativo,
                              Convert.ToInt32(ddlSucursalInterno.SelectedItem.Value),
                              año,
                              mes,
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
                       corporativo,
                       Convert.ToInt32(ddlSucursalInterno.SelectedItem.Value),
                       año,
                       mes,
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

            cConciliacion conciliacion =
                App.Consultas.ConsultaConciliacionDetalle(corporativo, sucursal, año, mes, folio);
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

            cConciliacion conciliacion =
                App.Consultas.ConsultaConciliacionDetalle(corporativo, sucursal, año, mes, folio);
            listArchivosInternos.ForEach(x => resultado = conciliacion.AgregarArchivo(x, cConciliacion.Operacion.Edicion));

            if (resultado)
            {
                //ACTUALIZAR GRID INTERNOS
                LlenarBarraEstado();
                if (filasSeleccionadasExternos("EN PROCESO DE CONCILIACION").Count > 0)
                    ConsultarArchivosInternos();
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
                       corporativo,
                       Convert.ToInt32(ddlSucursalInterno.SelectedItem.Value),
                       año,
                       mes,
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
                                            corporativo,
                                            Convert.ToInt16(ddlSucursalInterno.SelectedItem.Value),
                                            año, Convert.ToInt32(ddlFolioInterno.SelectedItem.Value));
        GenerarTablaDestinoDetalleInterno();
        LlenaGridViewDestinoDetalleInterno();
        lblFolioInterno.Text = ddlFolioInterno.SelectedItem.Value;
        grvVistaRapidaInterno_ModalPopupExtender.Show();

    }

    //---FIN MODULO "AGREGAR NUEVO INTERNO"


    protected void btnFiltraCliente_Click(object sender, ImageClickEventArgs e)
    {
        GridView grvPrima = null;
        DataTable dtPedidos = null;
        List<ReferenciaNoConciliadaPedido> ListPedidos;
        ReferenciaNoConciliadaPedido rp;
        try
        {
            short formaConciliacion = Convert.ToInt16(ddlCriteriosConciliacion.SelectedValue);

            if (tipoConciliacion == 2)
            {
                if (Convert.ToString(HttpContext.Current.Session["criterioConciliacion"]) == "VariosAUno")
                    grvPrima = (GridView)Session["TABLADEAGREGADOS"];
            }
            else
            {
                if (Convert.ToString(HttpContext.Current.Session["criterioConciliacion"]) == "VariosAUno")
                    grvPrima = (GridView)Session["TABLADEINTERNOS"];

                grvInternos.DataSource = wucBuscaClientesFacturas.FiltraCliente(grvPrima);
                if (grvInternos.DataSource == null || (grvInternos.DataSource as DataTable).Rows.Count == 0)
                {
                    dtPedidos = wucBuscaClientesFacturas.BuscaCliente();
                    dvExpera.Visible = grvPedidos.Rows.Count == 0;//RRV
                    if (dtPedidos.Rows.Count == 0) return;

                    grvPedidos.DataSource = dtPedidos;
                    grvPedidos.DataBind();
                    /*          Convertir DataTable a List<ReferenciaNoConciliadaPedido>            */
                    ListPedidos = new List<ReferenciaNoConciliadaPedido>();
                    foreach (DataRow tr in dtPedidos.Rows)
                    {
                        rp = App.ReferenciaNoConciliadaPedido.CrearObjeto();
                        rp.AñoPedido        = Convert.ToInt32(tr["AñoPed"]);
                        rp.CelulaPedido     = Convert.ToInt32(tr["Celula"]);
                        rp.Pedido           = Convert.ToInt32(tr["Pedido"]);
                        rp.Total            = Convert.ToDecimal(tr["Total"]);
                        rp.Foliofactura     = tr["FolioFactura"].ToString();
                        rp.Cliente          = Convert.ToInt32(tr["Cliente"]);
                        rp.Nombre           = tr["Nombre"].ToString();
                        rp.FormaConciliacion = formaConciliacion;
                        rp.FSuministro      = Convert.ToDateTime(tr["FSuministro"]);
                        ListPedidos.Add(rp);
                    }
                    Session["POR_CONCILIAR_PEDIDO"] = ListPedidos;
                    dvExpera.Visible = grvPedidos.Rows.Count == 0;
                    return;
                }
                grvInternos.DataBind();
                grvInternos.DataBind();
            }
            dvExpera.Visible = grvPedidos.Rows.Count == 0;
        }
        catch(Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }
    }

    private DataTable BuscarPedidosPorCliente(int cliente)
    {
        Cliente obCliente = App.Cliente.CrearObjeto();
        Conexion conexion = new Conexion();
        DataTable dtPedidos;

        try
        {
            conexion.AbrirConexion(false);
            dtPedidos = obCliente.ObtienePedidosCliente(cliente, conexion);

            HttpContext.Current.Session["PedidosBuscadosPorUsuario"] = dtPedidos;
            HttpContext.Current.Session["PedidosBuscadosPorUsuario_AX"] = dtPedidos;

            return dtPedidos;
        }
        finally
        {
            conexion.CerrarConexion();
        }
    }

    private void LlenaGridViewPedidosBuscadosPorUsuario()
    {
        try
        {
            DataTable tablaReferenciasP = (DataTable)HttpContext.Current.Session["PedidosBuscadosPorUsuario"];
            grvPedidos.PageIndex = 0;
            grvPedidos.DataSource = tablaReferenciasP;
            grvPedidos.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void imbBusquedaPedidos_Click(object sender, ImageClickEventArgs e)
    {
        BusquedaClienteDatosBancarios objBusqueda;
        List<Cliente> lstClientes;
        objBusqueda = App.BusquedaClienteDatosBancarios.CrearObjeto();
        lstClientes = objBusqueda.ConsultarCliente(ddlBusquedaPedidos.SelectedIndex + 1, txtBusquedaPedidos.Text);

        try
        {
            if (lstClientes == null || lstClientes.Count == 0)
            {
                throw new Exception("No se encontraron pedidos relacionados con su busqueda.");
            }
            else if (lstClientes.Count > 1)
            {
                wucClienteDatosBancarios.ClientesEncontrados = lstClientes;
                wucClienteDatosBancarios.CargarClientes();
                mpeLanzarSeleccionCliente.Show();
            }
            else if (lstClientes.Count == 1)
            {
                BuscarPedidosPorCliente(lstClientes[0].NumCliente);
                LlenaGridViewPedidosBuscadosPorUsuario();
            }
            dvExpera.Visible = grvPedidos.Rows.Count == 0;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "ErrorMsg", "alertify.alert('Conciliaci&oacute;n bancaria','Error: "
                + ex.Message + "', function(){ console.log('Warning'); });", true);
        }

    }

    protected void btnAceptarClienteDatosBancarios_Click(object sender, EventArgs e)
    {
        string cliente = wucClienteDatosBancarios.ClienteSeleccionado;
        mpeLanzarSeleccionCliente.Hide();

        if (cliente.Length > 0)
        {
            BuscarPedidosPorCliente(Convert.ToInt32(cliente));
            LlenaGridViewPedidosBuscadosPorUsuario();
        }
        dvExpera.Visible = grvPedidos.Rows.Count == 0;
    }

    protected void btnCancelarClienteDatosBancario_Click(object sender, EventArgs e)
    {
        mpeLanzarSeleccionCliente.Hide();
    }

    protected void chkSeleccionaExternosTodos_CheckedChanged(object sender, EventArgs e)
    {
        //foreach (
        //    RadioButton rb in
        //        from GridViewRow gv in grvExternos.Rows
        //        select (RadioButton)grvExternos.Rows[gv.RowIndex].FindControl("rdbSecuencia"))
        //{
        //    rb.Checked = false;
        //    despintarFilaSeleccionadaExterno(((GridViewRow)rb.Parent.Parent).RowIndex);
        //}
        CheckBox chk = (sender as CheckBox);
        foreach (GridViewRow fila in grvExternos.Rows.Cast<GridViewRow>().Where(fila => fila.RowType == DataControlRowType.DataRow))
        {
            fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked = chk.Checked;
            chkExterno_CheckedChanged(fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault(),null);
        }
    }

}