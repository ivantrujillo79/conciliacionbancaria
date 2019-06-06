using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Conciliacion.RunTime.ReglasDeNegocio;
using Conciliacion.RunTime;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using Locker;
using SeguridadCB.Public;

public partial class Conciliacion_FormasConciliar_CantidadYReferenciaConcuerdan : System.Web.UI.Page
{
    #region "Propiedades Globales"
    private SeguridadCB.Public.Operaciones operaciones;
    private SeguridadCB.Public.Usuario usuario;
    private List<Cliente> lstClientes = new List<Cliente>();
    private List<RTGMCore.DireccionEntrega> listaDireccinEntrega = new List<RTGMCore.DireccionEntrega>();
    private bool validarPeticion = false;
    private List<int> listaClientesEnviados;
    private List<int> listaClientes = new List<int>();

    #endregion
    #region "Propiedades Privadas"
    public int corporativo, año, folio, sucursal;
    public short mes;
    public short tipoConciliacion, grupoConciliacion;
    public int Nhilos = 0;

    public List<ReferenciaConciliada> listaReferenciaConciliada = new List<ReferenciaConciliada>();
    public List<ReferenciaConciliadaPedido> listaReferenciaConciliadaPedido = new List<ReferenciaConciliadaPedido>();
    public List<ReferenciaNoConciliada> listaTransaccionesConciliadas = new List<ReferenciaNoConciliada>();
    public List<ReferenciaNoConciliada> listaReferenciaExternas = new List<ReferenciaNoConciliada>();
    public int indiceExternoSeleccionado = 0;

    private List<ListaCombo> listSucursales = new List<ListaCombo>();
    private List<ListaCombo> listStatusConcepto = new List<ListaCombo>();
    private List<ListaCombo> listFormasConciliacion = new List<ListaCombo>();

    private DataTable tblReferenciasAConciliar;
    private DataTable tblTransaccionesConciliadas;
    public DataTable tblDetalleTransaccionConciliada;
    private List<ListaCombo> listCamposExternos = new List<ListaCombo>();
    private List<ListaCombo> listCamposInternos = new List<ListaCombo>();
    public List<ListaCombo> listCamposDestino = new List<ListaCombo>();
    public ReferenciaNoConciliada tranDesconciliar;
    private string DiferenciaDiasMaxima, DiferenciaDiasMinima, DiferenciaCentavosMaxima, DiferenciaCentavosMinima;

    private DatosArchivo datosArchivoInterno;
    private List<ListaCombo> listTipoFuenteInformacionExternoInterno = new List<ListaCombo>();
    public List<ListaCombo> listFoliosInterno = new List<ListaCombo>();
    public List<DatosArchivo> listArchivosInternos = new List<DatosArchivo>();

    private DataTable tblDestinoDetalleInterno;
    private List<DatosArchivoDetalle> listaDestinoDetalleInterno = new List<DatosArchivoDetalle>();
    private string _URLGateway;

    private bool activepaging = true;
    public bool ActivePaging
    {
        get { return activaPaginacion(); }
    }

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
    protected void Page_Load(object sender, EventArgs e)
    {
        short _FormaConciliacion = Asigna_FormaConciliacionActual();
        //short _FormaConciliacion = Convert.ToSByte(Request.QueryString["FormaConciliacion"]);
        //if (_FormaConciliacion == 0)
        //{
        //    if (Convert.ToSByte(Request.QueryString["TipoConciliacion"]) == 6)
        //    {
        //        _FormaConciliacion = 7;
        //    }
        //    else
        //    {
        //        _FormaConciliacion = 2;
        //    }
        //}

        if (IsPostBack)
        {
            if (Page.Request.Params["__EVENTTARGET"] == "miPostBack")
            {
                validarPeticion = false;
                listaDireccinEntrega = ViewState["LISTAENTREGA"] as List<RTGMCore.DireccionEntrega>;
                listaClientes = ViewState["LISTACLIENTES"] as List<int>;
                //ViewState["TBL_REFCON_CANTREF"] = tblReferenciasAConciliar;
                string dat = Page.Request.Params["__EVENTARGUMENT"].ToString();
                if (dat == "1")
                {
                    ObtieneNombreCliente(listaClientes);
                }
                else if (dat == "2")
                {
                    llenarListaEntrega();
                }
            }
        }
        Conciliacion.RunTime.App.ImplementadorMensajes.ContenedorActual = this;
        DataTable _tblReferenciasAConciliarPedido = new DataTable();
        DataTable _tblReferenciasAConciliarArchivo = new DataTable();
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

            LlenaGridViewDestinoDetalleInterno();

            corporativo = Convert.ToInt32(Request.QueryString["Corporativo"]);
            sucursal = Convert.ToInt16(Request.QueryString["Sucursal"]);
            año = Convert.ToInt32(Request.QueryString["Año"]);
            folio = Convert.ToInt32(Request.QueryString["Folio"]);
            mes = Convert.ToSByte(Request.QueryString["Mes"]);

            wucBuscadorPagoEstadoCuenta.Contenedor = mpeBuscadorPagoEdoCta;
            wucBuscadorPagoEstadoCuenta.ActivaEstaConciliacion = true;
            wucBuscadorPagoEstadoCuenta.Corporativo = corporativo;
            wucBuscadorPagoEstadoCuenta.Sucursal = sucursal;
            wucBuscadorPagoEstadoCuenta.Año = año;
            wucBuscadorPagoEstadoCuenta.Folio = folio;
            if (!Page.IsPostBack)
            {

                /*Se requiere realizar la modificacion de tipoConimgAutomaticaciliacion desde esta vista, 
                 * ya que no se requiere modifcar alguna otra vista por lo cual se envia de manera 
                 * estatica el valor del tipo conciliacion*/

                tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);
                grupoConciliacion = Convert.ToSByte(Request.QueryString["GrupoConciliacion"]);

                SolicitudConciliacion objSolicitdConciliacion = new SolicitudConciliacion();
                objSolicitdConciliacion.TipoConciliacion = tipoConciliacion;
                objSolicitdConciliacion.FormaConciliacion = _FormaConciliacion;

                CargarRangoDiasDiferenciaGrupo(grupoConciliacion);


                Carga_SucursalCorporativo(corporativo);
                Carga_StatusConcepto(Consultas.ConfiguracionStatusConcepto.ConEtiquetas);
                Carga_FormasConciliacion(tipoConciliacion);
                if (objSolicitdConciliacion.ConsultaPedido())
                    Carga_ComboTiposDeCobro();
                else
                {
                    //lblTiposdeCobro.Visible = false;
                    ddlTiposDeCobro.Visible = false;
                }
                try
                {
                    Carga_CamposExternos(tipoConciliacion);
                }
                catch(Exception ex)
                {
                    App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
                }

                LlenarBarraEstado();
                ////CARGAR LAS TRANSACCIONES CONCILIADAS POR EL CRITERIO DE AUTOCONCILIACIÓN
                //Consulta_TransaccionesConciliadas(corporativo, sucursal, año, mes, folio, Convert.ToInt32(ddlCriteriosConciliacion.SelectedValue));
                //GenerarTablaConciliados();
                //LlenaGridViewConciliadas();

                if (objSolicitdConciliacion.ConsultaPedido())
                {
                    lblPedidos.Visible = true;
                    //txtDias.Enabled = 
                    tdEtiquetaMontoIn.Visible = tdMontoIn.Visible = false;//imgExportar.Enabled
                    btnActualizarConfig.ValidationGroup = "CantidadReferenciaPedidos";
                    rvDiferencia.ValidationGroup = "CantidadReferenciaPedidos";
                    rfvDiferenciaVacio.ValidationGroup = "CantidadReferenciaPedidos";
                    Consulta_Externos(corporativo, sucursal, año, mes, folio, Convert.ToDecimal(txtDiferencia.Text), tipoConciliacion, Convert.ToInt32(ddlStatusConcepto.SelectedValue), true);
                    Consulta_ConciliarPedidosCantidadReferencia(Convert.ToDecimal(txtDiferencia.Text), Convert.ToSByte(ddlStatusConcepto.SelectedItem.Value), ddlCampoExterno.SelectedItem.Text, ddlCampoInterno.SelectedItem.Text);
                    GenerarTablaReferenciasAConciliarPedidos();
                    chkSeleccionarTodos.Visible = true;
                    //_tblReferenciasAConciliarPedido = (DataTable)HttpContext.Current.Session["TBL_REFCON_CANTREF"];
                }

                if (objSolicitdConciliacion.ConsultaArchivo())
                {
                    btnActualizarConfig.ValidationGroup = "CantidadReferencia";
                    txtDias.Enabled = true;
                    lblArchivosInternos.Visible = true;
                    try
                    {
                        Consulta_ConciliarArchivosCantidadReferencia(corporativo, sucursal, año, mes, folio, Convert.ToSByte(txtDias.Text), Convert.ToDecimal(txtDiferencia.Text), ddlCampoExterno.SelectedItem.Text, ddlCampoInterno.SelectedItem.Text, Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value));
                    }
                    catch (Exception ex)
                    {
                        App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
                    }
                    GenerarTablaReferenciasAConciliarArchivos();
                    _tblReferenciasAConciliarArchivo = (DataTable)HttpContext.Current.Session["TBL_REFCON_CANTREF"];
                    HttpContext.Current.Session["SolicitdConciliacionConsultaArchivo"] = 1;
                    chkSeleccionarTodos.Visible = false;
                }
                else
                    HttpContext.Current.Session["SolicitdConciliacionConsultaArchivo"] = 0;

                txtDias.Enabled = true;

                LlenaGridViewReferenciasConciliadas(tipoConciliacion);

                Carga_TipoFuenteInformacionInterno(Consultas.ConfiguracionTipoFuente.TipoFuenteInformacionInterno);
                activarImportacion(tipoConciliacion);

                ListItem selectedListItem = ddlCriteriosConciliacion.Items.FindByValue(_FormaConciliacion.ToString());
                ddlCriteriosConciliacion.ClearSelection();
                if (selectedListItem != null)
                {
                    selectedListItem.Selected = true;
                }

                //CARGAR LAS TRANSACCIONES CONCILIADAS POR EL CRITERIO DE AUTOCONCILIACIÓN
                if (ddlCriteriosConciliacion.SelectedValue == "2" || ddlCriteriosConciliacion.SelectedValue == "7")
                    if (corporativo != 0 && sucursal != 0 && año != 0 && mes != 0 && folio != 0)
                    {
                        Consulta_TransaccionesConciliadas(corporativo, sucursal, año, mes, folio, Convert.ToInt32(ddlCriteriosConciliacion.SelectedValue));
                        GenerarTablaConciliados();
                        LlenaGridViewConciliadas();
                    }
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
        corporativo = Convert.ToInt32(Request.QueryString["Corporativo"]);
        sucursal = Convert.ToInt16(Request.QueryString["Sucursal"]);
        año = Convert.ToInt32(Request.QueryString["Año"]);
        folio = Convert.ToInt32(Request.QueryString["Folio"]);
        mes = Convert.ToSByte(Request.QueryString["Mes"]);

        /*Se requiere realizar la modificacion de tipoConciliacion desde esta vista, 
             * ya que no se requiere modifcar alguna otra vista por lo cual se envia de manera 
             * estatica el valor del tipo conciliacion*/

        tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]); //antes estaba asi: tipoConciliacion = 2; 
    }
    //Limpian variables de Session
    public void limpiarVariablesSession()
    {
        //Eliminar las variables de Session utilizadas en la Vista
        HttpContext.Current.Session["CONCILIADAS"] = null;
        HttpContext.Current.Session["TBL_REFCON_CANTREF"] = null;
        HttpContext.Current.Session["POR_CONCILIAR"] = null;
        HttpContext.Current.Session["RepDoc"] = null;
        HttpContext.Current.Session["ParametrosReporte"] = null;
        HttpContext.Current.Session["NUEVOS_INTERNOS"] = null;
        HttpContext.Current.Session["DETALLEINTERNO"] = null;

        HttpContext.Current.Session.Remove("CONCILIADAS");
        HttpContext.Current.Session.Remove("TBL_REFCON_CANTREF");
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
    //Colocar el DropDown de Criterios de Evaluacion en la Actual
    public void ActualizarCriterioEvaluacion()
    {
        try
        {
            ddlCriteriosConciliacion.SelectedValue =
                ddlCriteriosConciliacion.Items.FindByText("CANTIDAD Y REFERENCIA CONCUERDAN").Value;
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
    /// <summary>
    /// Cargar campos de Filtro y Busqueda externo
    /// </summary>
    public void cargar_ComboCampoFiltroDestino(int tConciliacion, string filtrarEn)
    {
        if (filtrarEn.Equals("Externos") || filtrarEn.Equals("Conciliados"))
        {
            listCamposDestino = Conciliacion.RunTime.App.Consultas.ConsultaDestino();
        }
        else
            listCamposDestino = tConciliacion != 2 ? Conciliacion.RunTime.App.Consultas.ConsultaDestino() : Conciliacion.RunTime.App.Consultas.ConsultaDestinoPedido();

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
    public void GenerarTablaReferenciasAConciliarArchivos()//Genera la tabla Referencias a Conciliar Archivos
    {
        try
        {
            tblReferenciasAConciliar = new DataTable("ReferenciasConciliadas");
            tblReferenciasAConciliar.Columns.Add("Selecciona", typeof(bool));
            tblReferenciasAConciliar.Columns.Add("FolioExt", typeof(int));
            tblReferenciasAConciliar.Columns.Add("SecuenciaExt", typeof(int));
            //tblReferenciasAConciliar.Columns.Add("Secuencia", typeof(int));
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
            tblReferenciasAConciliar.Columns.Add("TipoCobro", typeof(int));

            //tblReferenciasAConciliar.Columns.Add("Pedido", typeof(int));
            //tblReferenciasAConciliar.Columns.Add("Celula", typeof(int));
            //tblReferenciasAConciliar.Columns.Add("AñoPed", typeof(int));
            //tblReferenciasAConciliar.Columns.Add("FMovimiento", typeof(DateTime));
            //tblReferenciasAConciliar.Columns.Add("FOperacion", typeof(DateTime));
            //tblReferenciasAConciliar.Columns.Add("MontoConciliado", typeof(decimal));
            //tblReferenciasAConciliar.Columns.Add("Concepto", typeof(string));
            //tblReferenciasAConciliar.Columns.Add("Descripcion", typeof(string));
            //tblReferenciasAConciliar.Columns.Add("PedidoReferencia", typeof(string));
            //tblReferenciasAConciliar.Columns.Add("Total", typeof(decimal));
            //tblReferenciasAConciliar.Columns.Add("ConceptoPedido", typeof(string));
            //tblReferenciasAConciliar.Columns.Add("Nombre", typeof(string));
            //tblReferenciasAConciliar.Columns.Add("Factura", typeof(string));
            //tblReferenciasAConciliar.Columns.Add("Cliente", typeof(string));

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
                    rc.TipoCobro);
            }
            HttpContext.Current.Session["TBL_REFCON_CANTREF"] = tblReferenciasAConciliar;
            ViewState["TBL_REFCON_CANTREF"] = tblReferenciasAConciliar;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void consultaClienteCRM(int cliente, SeguridadCB.Public.Usuario usuariot, byte  modulot, string cadena)
    {
        RTGMGateway.RTGMGateway Gateway;
        RTGMGateway.SolicitudGateway Solicitud;
        RTGMCore.DireccionEntrega DireccionEntrega = new RTGMCore.DireccionEntrega();
        Cliente _cliente = null;
        try
        {
            if (_URLGateway != string.Empty)
            {
                AppSettingsReader settings = new AppSettingsReader();
                SeguridadCB.Public.Usuario usuario = usuariot;//(SeguridadCB.Public.Usuario)Session["Usuario"]; //SeguridadCB.Public.Usuario usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
                byte modulo = modulot;//byte.Parse(settings.GetValue("Modulo", typeof(string)).ToString());
                Gateway = new RTGMGateway.RTGMGateway(modulo, cadena);//App.CadenaConexion);
                Gateway.URLServicio = _URLGateway;
                Solicitud = new RTGMGateway.SolicitudGateway();
                Solicitud.IDCliente = cliente;
                try
                {
                    DireccionEntrega = Gateway.buscarDireccionEntrega(Solicitud);
               
                    if (DireccionEntrega != null)
                    {
                        if (DireccionEntrega.Message != null)
                        {
                            _cliente = App.Cliente.CrearObjeto();
                            _cliente.NumCliente = cliente;
                            _cliente.Nombre = DireccionEntrega.Message;
                            lstClientes.Add(_cliente);
                        }
                        else
                        {
                            _cliente = App.Cliente.CrearObjeto();
                            _cliente.NumCliente = cliente;
                            _cliente.Nombre = DireccionEntrega.Nombre;
                            lstClientes.Add(_cliente);
                        }
                    }
                    else
                    {
                        _cliente = App.Cliente.CrearObjeto();
                        _cliente.NumCliente = cliente;
                        _cliente.Nombre = "No se encontró cliente";
                        lstClientes.Add(_cliente);
                    }
                }
                catch (Exception ex)
                {
                    _cliente = App.Cliente.CrearObjeto();
                    _cliente.NumCliente = cliente;
                    _cliente.Nombre = ex.Message;
                    lstClientes.Add(_cliente);
                }
            }
        }
        catch (Exception ex)
        {
            //throw ex;
            //App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
        //if (DireccionEntrega != null && DireccionEntrega.Nombre != null)
        //    return DireccionEntrega.Nombre.Trim();
        //else
        //    return "No encontrado";
    }

    private void llenarListaEntrega()
    {
        try
        {
            tblReferenciasAConciliar = new DataTable("ReferenciasConciliadas");
            //EXTERNO
            tblReferenciasAConciliar.Columns.Add("Selecciona", typeof(bool));
            tblReferenciasAConciliar.Columns.Add("FolioExt", typeof(int));
            tblReferenciasAConciliar.Columns.Add("Secuencia", typeof(int));
            tblReferenciasAConciliar.Columns.Add("RFCTercero", typeof(string));
            tblReferenciasAConciliar.Columns.Add("Retiro", typeof(decimal));
            tblReferenciasAConciliar.Columns.Add("Referencia", typeof(string));
            tblReferenciasAConciliar.Columns.Add("NombreTercero", typeof(string));
            tblReferenciasAConciliar.Columns.Add("Descripcion", typeof(string));
            tblReferenciasAConciliar.Columns.Add("Deposito", typeof(decimal));
            tblReferenciasAConciliar.Columns.Add("Cheque", typeof(string));
            tblReferenciasAConciliar.Columns.Add("FMovimiento", typeof(DateTime));
            tblReferenciasAConciliar.Columns.Add("FOperacion", typeof(DateTime));
            tblReferenciasAConciliar.Columns.Add("MontoConciliado", typeof(decimal));
            tblReferenciasAConciliar.Columns.Add("Concepto", typeof(string));
            //PEDIDO
            tblReferenciasAConciliar.Columns.Add("Pedido", typeof(int));
            tblReferenciasAConciliar.Columns.Add("PedidoReferencia", typeof(string));
            tblReferenciasAConciliar.Columns.Add("AñoPed", typeof(int));
            tblReferenciasAConciliar.Columns.Add("Celula", typeof(int));
            tblReferenciasAConciliar.Columns.Add("Cliente", typeof(string));
            tblReferenciasAConciliar.Columns.Add("Nombre", typeof(string));
            tblReferenciasAConciliar.Columns.Add("Total", typeof(decimal));
            tblReferenciasAConciliar.Columns.Add("ConceptoPedido", typeof(string));
            tblReferenciasAConciliar.Columns.Add("Factura", typeof(string));
            tblReferenciasAConciliar.Columns.Add("TipoCobro", typeof(int));
            tblReferenciasAConciliar.Columns.Add("TipoCobroAnterior", typeof(int));

            listaReferenciaConciliadaPedido = HttpContext.Current.Session["POR_CONCILIAR"] as List<ReferenciaConciliadaPedido>;
            if (_URLGateway != string.Empty)
            { 
                foreach (var item in listaReferenciaConciliadaPedido)
                {
                    try
                    {
                        RTGMCore.DireccionEntrega cliente = listaDireccinEntrega.FirstOrDefault(x => x.IDDireccionEntrega == item.Cliente);
                        if (cliente != null)
                        {
                            item.Nombre = cliente.Nombre;
                        }
                        else
                        {
                            item.Nombre = "No encontrado";
                        }
                    }
                    catch(Exception Ex)
                    {
                        item.Nombre = Ex.Message;
                    }
                }
            }

            foreach (ReferenciaConciliadaPedido rc in listaReferenciaConciliadaPedido)
            {
                //NombreCliente = ObtieneNombreCliente(lstClientes, rc.Cliente, rc.Nombre);
                tblReferenciasAConciliar.Rows.Add(
                    //EXTERNO
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
                    //PEDIDO
                    rc.Pedido,
                    rc.PedidoReferencia,
                    rc.AñoPedido,
                    rc.CelulaPedido,
                    rc.Cliente,
                    rc.Nombre,//NombreCliente, //rc.Nombre,
                    rc.Total,
                    rc.ConceptoPedido,
                    rc.FolioSat + rc.SerieSat,
                    rc.TipoCobro,
                    rc.TipoCobroAnterior
                    );
            }
            HttpContext.Current.Session["TBL_REFCON_CANTREF"] = tblReferenciasAConciliar;
            Session["TBL_REFCON_CANTREF"] = tblReferenciasAConciliar;
            ViewState["TBL_REFCON_CANTREF"] = tblReferenciasAConciliar;
            ViewState["LISTAENTREGA"] = listaDireccinEntrega;
            ViewState["LISTACLIENTES"] = listaClientes;
            LlenaGridViewReferenciasConciliadas(tipoConciliacion);
        }
        catch(Exception ex)
        {
             App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    public void completarListaEntregas(List<RTGMCore.DireccionEntrega> direccionEntregas)
    {
        RTGMCore.DireccionEntrega direccionEntrega;
        RTGMCore.DireccionEntrega direccionEntregaTemp;
        bool errorConsulta = false ;
        try
        {
            foreach (var item in direccionEntregas)
            {
                try
                {
                    if (item != null)
                    {
                        if (item.Message != null)
                        {
                            direccionEntrega = new RTGMCore.DireccionEntrega();
                            direccionEntrega.IDDireccionEntrega = item.IDDireccionEntrega;
                            direccionEntrega.Nombre = item.Message;
                            listaDireccinEntrega.Add(direccionEntrega);
                        }
                        else if (item.IDDireccionEntrega == -1)
                        {
                            errorConsulta = true;
                        }
                        else if(item.IDDireccionEntrega >= 0)
                        {
                            listaDireccinEntrega.Add(item);
                        }
                    }
                    else
                    {
                        direccionEntrega = new RTGMCore.DireccionEntrega();
                        direccionEntrega.IDDireccionEntrega = item.IDDireccionEntrega;
                        direccionEntrega.Nombre = "No se encontró cliente";
                        listaDireccinEntrega.Add(direccionEntrega);
                    }
                }
                catch(Exception ex)
                {
                    direccionEntrega = new RTGMCore.DireccionEntrega();
                    direccionEntrega.IDDireccionEntrega = item.IDDireccionEntrega;
                    direccionEntrega.Nombre = ex.Message;
                    listaDireccinEntrega.Add(direccionEntrega);
                }
            }
            if (validarPeticion && errorConsulta)
            {
                validarPeticion = false;
                listaClientes = new List<int>();
                foreach (var item in listaClientesEnviados)
                {
                    direccionEntregaTemp = listaDireccinEntrega.FirstOrDefault(x => x.IDDireccionEntrega == item);
                    if(direccionEntregaTemp == null)
                    {
                        listaClientes.Add(item);
                    }
                }
                ViewState["LISTAENTREGA"] = listaDireccinEntrega;
                ViewState["LISTACLIENTES"] = listaClientes;
                HttpContext.Current.Session["POR_CONCILIAR"] = listaReferenciaConciliadaPedido;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Mensaje", " mensajeAsincrono("+listaClientes.Count+");", true);
            }
            else
            {
                llenarListaEntrega();
            }
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    private void ObtieneNombreCliente(List<int> listadistintos)
    {
        RTGMGateway.RTGMGateway oGateway;
        RTGMGateway.SolicitudGateway oSolicitud;
        AppSettingsReader settings = new AppSettingsReader();
        string cadena = App.CadenaConexion;
        try
        {
            SeguridadCB.Public.Parametros parametros;
            parametros = (SeguridadCB.Public.Parametros)HttpContext.Current.Session["Parametros"];
            _URLGateway = parametros.ValorParametro(Convert.ToSByte(settings.GetValue("Modulo", typeof(sbyte))), "URLGateway").Trim();
            if (_URLGateway != string.Empty)
            {
                SeguridadCB.Public.Usuario user = (SeguridadCB.Public.Usuario)Session["Usuario"];
                oGateway = new RTGMGateway.RTGMGateway(byte.Parse(settings.GetValue("Modulo", typeof(string)).ToString()), App.CadenaConexion);//,_URLGateway);
                oGateway.ListaCliente = listadistintos;
                oGateway.URLServicio = _URLGateway;//"http://192.168.1.21:88/GasMetropolitanoRuntimeService.svc";//URLGateway;
                oGateway.eListaEntregas += completarListaEntregas;
                oSolicitud = new RTGMGateway.SolicitudGateway();
                listaClientesEnviados = listadistintos;
                foreach (var item in listadistintos)
                {
                    oSolicitud.IDCliente = item;
                    oGateway.busquedaDireccionEntregaAsync(oSolicitud);
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private string ObtieneNombreCliente(List<Cliente> lstClientes, int numCliente, string NombreClienteBD)
    {
        string NombreCliente = "";
        if (_URLGateway != "")
        {
            AppSettingsReader settings = new AppSettingsReader();
            Cliente cliente = lstClientes.Find(x => x.NumCliente == numCliente);
            if (cliente == null)
            {
                consultaClienteCRM(numCliente, (SeguridadCB.Public.Usuario)Session["Usuario"], byte.Parse(settings.GetValue("Modulo", typeof(string)).ToString()), App.CadenaConexion);
                if (lstClientes.Count > 0)
                { 
                    NombreCliente = lstClientes.FirstOrDefault(x => x.NumCliente == numCliente).Nombre;
                    cliente = App.Cliente.CrearObjeto();
                    cliente.NumCliente = numCliente;
                    cliente.Nombre = NombreCliente;
                    lstClientes.Add(cliente);
                }
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

    public void GenerarTablaReferenciasAConciliarPedidos()//Genera la tabla Referencias a Conciliar
    {
        SeguridadCB.Public.Parametros parametros;
        parametros = (SeguridadCB.Public.Parametros)HttpContext.Current.Session["Parametros"];
        AppSettingsReader settings = new AppSettingsReader();
        _URLGateway = parametros.ValorParametro(Convert.ToSByte(settings.GetValue("Modulo", typeof(sbyte))), "URLGateway").Trim();
        try
        {
            tblReferenciasAConciliar = new DataTable("ReferenciasConciliadas");
            //EXTERNO
            tblReferenciasAConciliar.Columns.Add("Selecciona", typeof(bool));
            tblReferenciasAConciliar.Columns.Add("FolioExt", typeof(int));
            tblReferenciasAConciliar.Columns.Add("Secuencia", typeof(int));
            tblReferenciasAConciliar.Columns.Add("RFCTercero", typeof(string));
            tblReferenciasAConciliar.Columns.Add("Retiro", typeof(decimal));
            tblReferenciasAConciliar.Columns.Add("Referencia", typeof(string));
            tblReferenciasAConciliar.Columns.Add("NombreTercero", typeof(string));
            tblReferenciasAConciliar.Columns.Add("Descripcion", typeof(string));
            tblReferenciasAConciliar.Columns.Add("Deposito", typeof(decimal));
            tblReferenciasAConciliar.Columns.Add("Cheque", typeof(string));
            tblReferenciasAConciliar.Columns.Add("FMovimiento", typeof(DateTime));
            tblReferenciasAConciliar.Columns.Add("FOperacion", typeof(DateTime));
            tblReferenciasAConciliar.Columns.Add("MontoConciliado", typeof(decimal));
            tblReferenciasAConciliar.Columns.Add("Concepto", typeof(string));
            //PEDIDO
            tblReferenciasAConciliar.Columns.Add("Pedido", typeof(int));
            tblReferenciasAConciliar.Columns.Add("PedidoReferencia", typeof(string));
            tblReferenciasAConciliar.Columns.Add("AñoPed", typeof(int));
            tblReferenciasAConciliar.Columns.Add("Celula", typeof(int));
            tblReferenciasAConciliar.Columns.Add("Cliente", typeof(string));
            tblReferenciasAConciliar.Columns.Add("Nombre", typeof(string));
            tblReferenciasAConciliar.Columns.Add("Total", typeof(decimal));
            tblReferenciasAConciliar.Columns.Add("ConceptoPedido", typeof(string));
            tblReferenciasAConciliar.Columns.Add("Factura", typeof(string));

            //string NombreCliente = "";
            //List<Cliente> lstClientes = new List<Cliente>();
            List<int> listadistintos = new List<int>();//listaReferenciaConciliadaPedido.GroupBy(item => item.Cliente).Select(x=> x.f).ToList();//.GroupBy(x => x.Cliente).Select(c => c.First()).ToList();
            listaClientesEnviados = new List<int>();
            try
            {
                listaDireccinEntrega = ViewState["LISTAENTREGA"] as List<RTGMCore.DireccionEntrega>;
                if(listaDireccinEntrega == null)
                {
                    listaDireccinEntrega = new List<RTGMCore.DireccionEntrega>();
                }
            }
            catch(Exception)
            {

            }
            foreach (var item in listaReferenciaConciliadaPedido)
            {
                if (!listaDireccinEntrega.Exists(x => x.IDDireccionEntrega == item.Cliente))
                {
                    if (!listadistintos.Exists(x => x == item.Cliente))
                    {
                        listadistintos.Add(item.Cliente);
                    }
                }
            }
            try
            {
                if (listadistintos.Count > 0)
                {
                    validarPeticion = true;
                    ObtieneNombreCliente(listadistintos);
                }
                //else
                //{
                    llenarListaEntrega();
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //foreach (ReferenciaConciliadaPedido rc in listaReferenciaConciliadaPedido)
            //{
            //    //NombreCliente = ObtieneNombreCliente(lstClientes, rc.Cliente, rc.Nombre);
            //    tblReferenciasAConciliar.Rows.Add(
            //        //EXTERNO
            //        rc.Selecciona,
            //        rc.Folio,
            //        rc.Secuencia,
            //        rc.RFCTercero,
            //        rc.Retiro,
            //        rc.Referencia,
            //        rc.NombreTercero,
            //        rc.Descripcion,
            //        rc.Deposito,
            //        rc.Cheque,
            //        rc.FMovimiento,
            //        rc.FOperacion,
            //        rc.MontoConciliado,
            //        rc.Concepto,
            //        //PEDIDO
            //        rc.Pedido,
            //        rc.PedidoReferencia,
            //        rc.AñoPedido,
            //        rc.CelulaPedido,
            //        rc.Cliente,
            //        rc.Nombre,//NombreCliente, //rc.Nombre,
            //        rc.Total,
            //        rc.ConceptoPedido,
            //        rc.FolioSat + rc.SerieSat
            //        );
            //}
            ////HttpContext.Current.Session["TBL_REFCON_CANTREF"] = tblReferenciasAConciliar;
            //Session["TBL_REFCON_CANTREF"] = tblReferenciasAConciliar;
            //ViewState["TBL_REFCON_CANTREF"] = tblReferenciasAConciliar;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void UnCheckBloquedos(GridView grv)
    {
        bool Existen = false;
        if (LockerExterno.ExternoBloqueado != null)
        {
            SolicitudConciliacion objSolicitdConciliacion = new SolicitudConciliacion();
            tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);
            short _FormaConciliacion = Asigna_FormaConciliacionActual();
            objSolicitdConciliacion.TipoConciliacion = tipoConciliacion;
            objSolicitdConciliacion.FormaConciliacion = _FormaConciliacion;
            int filaindex = 0;
            foreach (GridViewRow fila in grv.Rows)
            {
                if (fila.RowType == DataControlRowType.DataRow)
                {
                    if (objSolicitdConciliacion.ConsultaPedido())
                    {
                        listaReferenciaConciliadaPedido[filaindex].Selecciona = fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                        if (fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked)
                        {
                            Existen = LockerExterno.ExternoBloqueado.Exists(x => x.SessionID != Session.SessionID &&
                                                                            x.Corporativo == listaReferenciaConciliadaPedido[filaindex].Corporativo &&
                                                                            x.Sucursal == listaReferenciaConciliadaPedido[filaindex].Sucursal &&
                                                                            x.Año == listaReferenciaConciliadaPedido[filaindex].Año &&
                                                                            x.Folio == listaReferenciaConciliadaPedido[filaindex].Folio &&
                                                                            x.Secuencia == listaReferenciaConciliadaPedido[filaindex].Secuencia);
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

    //private void AsignaValoresDefaultTipoCobro(GridView grid)
    //{
        
    //}

    private void LlenaGridViewReferenciasConciliadas(int tipoConcilacion)//Llena el gridview con las conciliaciones antes leídas
    {
        try
        {
            DataTable tablaReferenacias = (DataTable)HttpContext.Current.Session["TBL_REFCON_CANTREF"];
            tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);
            short _FormaConciliacion = Convert.ToSByte(Request.QueryString["FormaConciliacion"]);
            if (_FormaConciliacion == 0)
            {
                if (Convert.ToSByte(Request.QueryString["TipoConciliacion"]) == 6)
                {
                    _FormaConciliacion = 7;
                }
                else
                {
                    _FormaConciliacion = 2;
                }
            }

            SolicitudConciliacion objSolicitdConciliacion = new SolicitudConciliacion();
            objSolicitdConciliacion.TipoConciliacion = tipoConciliacion;
            objSolicitdConciliacion.FormaConciliacion = _FormaConciliacion;

            if (objSolicitdConciliacion.ConsultaPedido())
            {
                grvCantidadReferenciaConcuerdanPedido.DataSource = tablaReferenacias;
                grvCantidadReferenciaConcuerdanPedido.DataBind(); //incidente 199
                UnCheckBloquedos(grvCantidadReferenciaConcuerdanPedido);
                bloqueaTodoLoSeleccionado(grvCantidadReferenciaConcuerdanPedido);

                //AsignaValoresDefaultTipoCobro(grvCantidadReferenciaConcuerdanPedido);

            }
            if (objSolicitdConciliacion.ConsultaArchivo())
            {
                grvCantidadReferenciaConcuerdanArchivos.DataSource = tablaReferenacias;
                grvCantidadReferenciaConcuerdanArchivos.DataBind();
                UnCheckBloquedos(grvCantidadReferenciaConcuerdanArchivos);
                bloqueaTodoLoSeleccionado(grvCantidadReferenciaConcuerdanArchivos);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //Consultar archivo Externo
    public void Consulta_Externos(int corporativo, int sucursal, int año, short mes, int folio, decimal diferencia,
                                  int tipoConciliacion, int statusConcepto, bool esDeposito)
    {
        SeguridadCB.Seguridad seguridad = new SeguridadCB.Seguridad();
        System.Data.SqlClient.SqlConnection connection = seguridad.Conexion;
        if (connection.State == ConnectionState.Closed)
        {
            seguridad.Conexion.Open();
            /*
                        connection = SeguridadCB.Seguridad.Conexion;
            */
        }

        try
        {
            cConciliacion c = App.Consultas.ConsultaConciliacionDetalle(corporativo, sucursal, año, mes, folio);
            listaReferenciaExternas =
                //tipoConciliacion == 2 ? 
                Conciliacion.RunTime.App.Consultas.ConsultaDetalleExternoPendienteDeposito(
                //chkReferenciaEx.Checked? 
                                                       Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConsultaExterno.DepositosConReferenciaPedido,
                //: Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConsultaExterno.DepositosPedido,
                                                 corporativo, sucursal, año, mes, folio, diferencia, statusConcepto,
                                                 esDeposito, c.Banco, lblCuenta.Text)
                //: Conciliacion.RunTime.App.Consultas.ConsultaDetalleExternoPendienteDeposito
                //      (chkReferenciaEx.Checked
                //           ? Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConsultaExterno.ConReferenciaInterno
                //           : Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConsultaExterno.TodoInterno,
                //       corporativo, sucursal, año, mes, folio, diferencia, statusConcepto,
                //       esDeposito)
                                          ;

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

    public void Consulta_ConciliarArchivosCantidadReferencia(int corporativo, int sucursal, int año, short mes, int folio, short dias, decimal centavos, string campoexterno, string campointerno, int statusConcepto)
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
            listaReferenciaConciliada = Conciliacion.RunTime.App.Consultas.ConsultaConciliarPorReferencia(corporativo, sucursal, año, mes, folio, dias, centavos, campoexterno, campointerno, statusConcepto);
            HttpContext.Current.Session["POR_CONCILIAR"] = listaReferenciaConciliada;
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
    public void Consulta_ConciliarPedidosCantidadReferencia(decimal centavos, short statusConcepto, string campoExterno, string campoInterno)
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

            listaReferenciaConciliadaPedido = new List<ReferenciaConciliadaPedido>();
            List<ReferenciaConciliadaPedido> listResultado;
            listaReferenciaExternas =
                listaReferenciaExternas.FindAll(
                    x =>
                        x.StatusConciliacion.Equals("CONCILIACION ABIERTA") ||
                        x.StatusConciliacion.Equals("EN PROCESO DE CONCILIACION"));
            foreach (ReferenciaNoConciliada rnc in listaReferenciaExternas)
            {
                bool resultado = false;
                listResultado = new List<ReferenciaConciliadaPedido>();
                listResultado = rnc.ConciliarPedidoCantidadYReferenciaMovExterno(centavos,
                    statusConcepto, campoExterno, campoInterno);
                listaTransaccionesConciliadas = Session["CONCILIADAS"] as List<ReferenciaNoConciliada>;
                foreach (ReferenciaConciliadaPedido rc in listResultado)
                {
                    rc.TipoCobro = rnc.TipoCobro;
                    rc.TipoCobroAnterior = rnc.TipoCobro;
                    //if (rnc.TipoCobro == 0)
                    //{
                    //    rc.TipoCobro = 10;
                    //    rc.TipoCobroAnterior = 10;
                    //}
                    if (listaTransaccionesConciliadas != null)
                        resultado =
                            listaTransaccionesConciliadas.SelectMany(
                                rcc => rcc.ListaReferenciaConciliada.Cast<ReferenciaConciliadaPedido>())
                                .ToList()
                                .Exists(
                                    p =>
                                        p.AñoPedido == rc.AñoPedido && p.Pedido == rc.Pedido &&
                                        p.CelulaPedido == rc.CelulaPedido);

                    if (resultado) break;

                    resultado = listaReferenciaConciliadaPedido.Exists(
                          c => c.Pedido == rc.Pedido && c.AñoPedido == rc.AñoPedido && c.CelulaPedido == rc.CelulaPedido);
                    if (resultado) break;
                }

                if (resultado) continue;

                //if (listResultado.Count <= 2 && listResultado.Count > 0) // Se comenta para corregir incidencia 109
                listaReferenciaConciliadaPedido.AddRange(listResultado);
            }
            HttpContext.Current.Session["POR_CONCILIAR"] = listaReferenciaConciliadaPedido;
            Nhilos = 0;
        }
        catch (SqlException ex)
        {
            Nhilos = 0;
            throw ex;
        }
        catch (Exception ex)
        {
            Nhilos = 0;
            throw ex;
        }
    }


    public void ValidacionConsulta(List<ReferenciaConciliadaPedido> listResultado)
    {
        try
        {
            bool resultado = false;
            listaTransaccionesConciliadas = Session["CONCILIADAS"] as List<ReferenciaNoConciliada>;
            foreach (ReferenciaConciliadaPedido rc in listResultado)
            {
                if (listaTransaccionesConciliadas != null)
                    resultado =
                        listaTransaccionesConciliadas.SelectMany(
                            rcc => rcc.ListaReferenciaConciliada.Cast<ReferenciaConciliadaPedido>())
                            .ToList()
                            .Exists(
                                p =>
                                    p.AñoPedido == rc.AñoPedido && p.Pedido == rc.Pedido &&
                                    p.CelulaPedido == rc.CelulaPedido);

                if (resultado) break;

                resultado = listaReferenciaConciliadaPedido.Exists(
                      c => c.Pedido == rc.Pedido && c.AñoPedido == rc.AñoPedido && c.CelulaPedido == rc.CelulaPedido);
                if (resultado) break;
            }

            if (!resultado)
            {
                //if (listResultado.Count <= 2 && listResultado.Count > 0) // Se comenta para corregir incidencia 109
                listaReferenciaConciliadaPedido.AddRange(listResultado);
            }
            Nhilos--;
        }
        catch(Exception)
        {
            Nhilos--;
        }
    }

    protected void grvCantidadReferenciaConcuerdanArchivos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.Pager && (grvCantidadReferenciaConcuerdanArchivos.DataSource != null))
        //{
        //    //TRAE EL TOTAL DE PAGINAS
        //    Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumPaginas");
        //    _TotalPags.Text = grvCantidadReferenciaConcuerdanArchivos.PageCount.ToString();

        //    //LLENA LA LISTA CON EL NUMERO DE PAGINAS
        //    DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
        //    for (int i = 1; i <= Convert.ToInt32(grvCantidadReferenciaConcuerdanArchivos.PageCount); i++)
        //    {
        //        list.Items.Add(i.ToString());
        //    }
        //    list.SelectedValue = (grvCantidadReferenciaConcuerdanArchivos.PageIndex + 1).ToString();
        //}
    }
    protected void grvCantidadReferenciaConcuerdanArchivos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //Leer el tipoConciliacion URL

                /*Se requiere realizar la modificacion de tipoConciliacion desde esta vista, 
                 * ya que no se requiere modifcar alguna otra vista por lo cual se envia de manera 
                 * estatica el valor del tipo conciliacion*/
            
            tipoConciliacion = 2;//Convert.ToSByte(Request.QueryString["TipoConciliacion"]);

            grvCantidadReferenciaConcuerdanArchivos.PageIndex = e.NewPageIndex;
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
        if (int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 && iNumPag <= grvCantidadReferenciaConcuerdanArchivos.PageCount)
        {
            if (int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 && iNumPag <= grvCantidadReferenciaConcuerdanArchivos.PageCount)
            {
                grvCantidadReferenciaConcuerdanArchivos.PageIndex = iNumPag - 1;
            }
            else
            {
                grvCantidadReferenciaConcuerdanArchivos.PageIndex = 0;
            }
        }
        // BindGrid();
        //Leer el tipoConciliacion URL

        /*Se requiere realizar la modificacion de tipoConciliacion desde esta vista, 
             * ya que no se requiere modifcar alguna otra vista por lo cual se envia de manera 
             * estatica el valor del tipo conciliacion*/

        tipoConciliacion = 2; //Convert.ToSByte(Request.QueryString["TipoConciliacion"]);
        LlenaGridViewReferenciasConciliadas(tipoConciliacion);
    }

    protected void grvCantidadReferenciaConcuerdanPedido_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Control ctrlddl = e.Row.FindControl("ddlPorConciliarTipoCobro");
            Control ctrllbl = e.Row.FindControl("lblTipoCobro");
            if (ctrlddl != null && ctrllbl != null)
            {
                DropDownList dd = ctrlddl as DropDownList;
                Label lb = ctrllbl as Label;
                dd.SelectedValue = lb.Text;
                //e.Row.Attributes.Add("onmouseover", "this.className='bg-color-grisClaro02'");
                //dd.Attributes.Add("onmouseover", "this.className='bg-color-grisClaro02'");
                if (lb.Text == "0")
                    dd.CssClass = "select-css-rojo";
                else
                    dd.CssClass = "select-css";
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            if (HttpContext.Current.Session["SolicitdConciliacionConsultaArchivo"] != null && int.Parse(HttpContext.Current.Session["SolicitdConciliacionConsultaArchivo"].ToString()) == 1)
            {
                if (e.Row.Cells.Count >= 15)
                    e.Row.Cells[14].Visible = false;
                if (e.Row.Cells.Count >= 16)
                    e.Row.Cells[15].Visible = false;
            }
        }
    }

    protected void grvCantidadReferenciaConcuerdanPedido_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //Leer el tipoConciliacion URL
            
            /*Se requiere realizar la modificacion de tipoConciliacion desde esta vista, 
             * ya que no se requiere modifcar alguna otra vista por lo cual se envia de manera 
             * estatica el valor del tipo conciliacion*/

            tipoConciliacion = 2;//Convert.ToSByte(Request.QueryString["TipoConciliacion"]);

            grvCantidadReferenciaConcuerdanPedido.PageIndex = e.NewPageIndex;
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

        if (int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 && iNumPag <= grvCantidadReferenciaConcuerdanPedido.PageCount)
        {
            grvCantidadReferenciaConcuerdanPedido.PageIndex = iNumPag - 1;
        }
        else
        {
            grvCantidadReferenciaConcuerdanPedido.PageIndex = 0;
        }

        //Leer el tipoConciliacion URL

        /*Se requiere realizar la modificacion de tipoConciliacion desde esta vista, 
             * ya que no se requiere modifcar alguna otra vista por lo cual se envia de manera 
             * estatica el valor del tipo conciliacion*/

        tipoConciliacion = 2;//Convert.ToSByte(Request.QueryString["TipoConciliacion"]);

        LlenaGridViewReferenciasConciliadas(tipoConciliacion);
    }

    //Crea la paginacion para Concilidos
    protected void grvConciliadas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }

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
        catch (Exception)
        {

            throw;
        }
    }
    //Asignar Valoresa Css de cada Row
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

        if (int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 && iNumPag <= grvConciliadas.PageCount)
        {
            grvConciliadas.PageIndex = iNumPag - 1;
        }
        else
        {
            grvConciliadas.PageIndex = 0;
        }
        LlenaGridViewConciliadasAjustado();
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
            listaTransaccionesConciliadas = Conciliacion.RunTime.App.Consultas.ConsultaTransaccionesConciliadas(corporativoconciliacion, sucursalconciliacion, añoconciliacion, mesconciliacion, folioconciliacion, Convert.ToInt16(ddlCriteriosConciliacion.SelectedValue));
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
        tblTransaccionesConciliadas.Columns.Add("TipoCobro", typeof(int));

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
                rc.ClienteReferencia,
                rc.TipoCobro);
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
            App.ImplementadorMensajes.MostrarMensaje(ex.StackTrace);
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
        catch (Exception e)
        {

        }
    }

    //protected void btnConsultar_Click(object sender, EventArgs e)
    //{
    //    //Leer Variables URL 
    //    cargarInfoConciliacionActual();

    //    Consulta_ConciliarArchivosCantidadReferencia(corporativo, sucursal, año, mes, folio, Convert.ToSByte(txtDias.Text), Convert.ToDecimal(txtDiferencia.Text), ddlCampoExterno.SelectedItem.Text, ddlCampoInterno.SelectedItem.Text, Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value));
    //    GenerarTablaReferenciasAConciliarArchivos();
    //    LlenaGridViewReferenciasConciliadas(tipoConciliacion);
    //}
    /// <summary>
    /// Llena el Combo de Campos Externos
    /// </summary>
    public void Carga_CamposExternos(short tipoConciliacion)
    {
        try
        {
            listCamposExternos = Conciliacion.RunTime.App.Consultas.ConsultaDestinoExterno(tipoConciliacion);

            if (listCamposExternos.Count > 0)
            {

                this.ddlCampoExterno.DataSource = listCamposExternos;
                this.ddlCampoExterno.DataValueField = "Identificador";
                this.ddlCampoExterno.DataTextField = "Descripcion";
                this.ddlCampoExterno.DataBind();
                this.ddlCampoExterno.Dispose();
            }
            else
            {
                throw new Exception("No existe configuración para los campos de referencia");
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
    /// Llena el Combo de Campos Internos
    /// </summary>
    public void Carga_CamposInternos(short tipoConciliacion, string campoExterno)
    {
        try
        {
            listCamposInternos = Conciliacion.RunTime.App.Consultas.ConsultaDestinoInterno(tipoConciliacion, campoExterno);
            this.ddlCampoInterno.DataSource = listCamposInternos;
            this.ddlCampoInterno.DataValueField = "Identificador";
            this.ddlCampoInterno.DataTextField = "Descripcion";
            this.ddlCampoInterno.DataBind();
            //this.ddlCampoInterno.Dispose();
        }
        catch
        {
        }
    }
    //protected void btnConsultarCantidadReferenciaPedidos_Click(object sender, EventArgs e)
    //{
    //    //Leer Variables URL 
    //    cargarInfoConciliacionActual();

    //    Consulta_ConciliarPedidosCantidadReferencia(corporativo, sucursal, año, mes, folio, Convert.ToDecimal(txtDiferencia.Text), Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value), ddlCampoExterno.SelectedItem.Text, ddlCampoInterno.SelectedItem.Text);
    //    GenerarTablaReferenciasAConciliarPedidos();
    //    LlenaGridViewReferenciasConciliadas(tipoConciliacion);
    //}
    protected void ddlCampoExterno_DataBound(object sender, EventArgs e)
    {
        //Leer el tipoConciliacion URL

        /*Se requiere realizar la modificacion de tipoConciliacion desde esta vista, 
             * ya que no se requiere modifcar alguna otra vista por lo cual se envia de manera 
             * estatica el valor del tipo conciliacion*/

        tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);

        Carga_CamposInternos(tipoConciliacion, ddlCampoExterno.SelectedItem.Text);
    }
    protected void ddlCampoExterno_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Leer Variables URL 
        cargarInfoConciliacionActual();

        Carga_CamposInternos(tipoConciliacion, ddlCampoExterno.SelectedItem.Text);
        if (tipoConciliacion != 2)
        {
            Consulta_ConciliarArchivosCantidadReferencia(corporativo, sucursal, año, mes, folio,
                                                                      Convert.ToSByte(txtDias.Text),
                                                                      Convert.ToDecimal(txtDiferencia.Text),
                                                                      ddlCampoExterno.SelectedItem.Text,
                                                                      ddlCampoInterno.SelectedItem.Text,
                                                                      Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value));
            GenerarTablaReferenciasAConciliarArchivos();
        }
        else
        {
            //cargarInfoConciliacionActual();
            Consulta_Externos(corporativo, sucursal, año, mes, folio, Convert.ToDecimal(txtDiferencia.Text), tipoConciliacion, Convert.ToInt32(ddlStatusConcepto.SelectedValue), true);

            Consulta_ConciliarPedidosCantidadReferencia(
                                                        Convert.ToDecimal(txtDiferencia.Text),
                                                        Convert.ToSByte(ddlStatusConcepto.SelectedItem.Value),
                                                        ddlCampoExterno.SelectedItem.Text, ddlCampoInterno.SelectedItem.Text);
            GenerarTablaReferenciasAConciliarPedidos();
        }
        LlenaGridViewReferenciasConciliadas(tipoConciliacion);
    }

    protected void btnGuardarCantidadReferencia_Click(object sender, EventArgs e)
    {


    }

    //Metodos
    //Ver el detalle de la Transaccion Conciliada
    protected void grvConciliadas_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int corporativoConcilacion = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["CorporativoConciliacion"]);
        int sucursalConciliacion = Convert.ToInt16(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["SucursalConciliacion"]);
        int añoConciliacion = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["AñoConciliacion"]);
        short mesConciliacion = Convert.ToSByte(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["MesConciliacion"]);
        int folioConciliacion = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["FolioConciliacion"]);
        int folioExterno = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["FolioExt"]);
        int secuenciaExterno = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["Secuencia"]);

        //Leer las TransaccionesConciliadas
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
    //Comando para desconciliar
    protected void grvConciliadas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!e.CommandName.Equals("DESCONCILIAR")) return;
        try
        {
            Button imgDesconciliar = e.CommandSource as Button;
            GridViewRow gRowConciliado = (GridViewRow)(imgDesconciliar).Parent.Parent;

            int corporativoConcilacion = Convert.ToInt32(grvConciliadas.DataKeys[gRowConciliado.RowIndex].Values["CorporativoConciliacion"]);
            int sucursalConciliacion = Convert.ToInt32(grvConciliadas.DataKeys[gRowConciliado.RowIndex].Values["SucursalConciliacion"]);
            int añoConciliacion = Convert.ToInt32(grvConciliadas.DataKeys[gRowConciliado.RowIndex].Values["AñoConciliacion"]);
            int mesConciliacion = Convert.ToInt32(grvConciliadas.DataKeys[gRowConciliado.RowIndex].Values["MesConciliacion"]);
            int folioConciliacion = Convert.ToInt32(grvConciliadas.DataKeys[gRowConciliado.RowIndex].Values["FolioConciliacion"]);
            int folioExterno = Convert.ToInt32(grvConciliadas.DataKeys[gRowConciliado.RowIndex].Values["FolioExt"]);
            int secuenciaExterno = Convert.ToInt32(grvConciliadas.DataKeys[gRowConciliado.RowIndex].Values["Secuencia"]);

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
            //Leer Variables URL 
            cargarInfoConciliacionActual();

            Consulta_TransaccionesConciliadas(corporativo, sucursal, año, mes, folio, Convert.ToInt32(ddlCriteriosConciliacion.SelectedValue));
            GenerarTablaConciliados();
            LlenaGridViewConciliadas();
            LlenarBarraEstado();
            //Cargo y refresco nuevamente los archivos externos

            short _FormaConciliacion = Asigna_FormaConciliacionActual();
            SolicitudConciliacion objSolicitdConciliacion = new SolicitudConciliacion();
            objSolicitdConciliacion.TipoConciliacion = tipoConciliacion;
            objSolicitdConciliacion.FormaConciliacion = _FormaConciliacion;
            //if (tipoConciliacion != 2)
            if (objSolicitdConciliacion.ConsultaArchivo())
            {
                Consulta_ConciliarArchivosCantidadReferencia(corporativo, sucursal, año, mes, folio,
                                                             Convert.ToSByte(txtDias.Text),
                                                             Convert.ToDecimal(txtDiferencia.Text),
                                                             ddlCampoExterno.SelectedItem.Text,
                                                             ddlCampoInterno.SelectedItem.Text,
                                                             Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value));
                GenerarTablaReferenciasAConciliarArchivos();
            }
            if (objSolicitdConciliacion.ConsultaPedido())
            {
                //cargarInfoConciliacionActual();
                Consulta_Externos(corporativo, sucursal, año, mes, folio, Convert.ToDecimal(txtDiferencia.Text), tipoConciliacion, Convert.ToInt32(ddlStatusConcepto.SelectedValue), true);

                Consulta_ConciliarPedidosCantidadReferencia(
                                                            Convert.ToDecimal(txtDiferencia.Text),
                                                            Convert.ToSByte(ddlStatusConcepto.SelectedItem.Value),
                                                            ddlCampoExterno.SelectedItem.Text, ddlCampoInterno.SelectedItem.Text);
                GenerarTablaReferenciasAConciliarPedidos();
            }
            LlenaGridViewReferenciasConciliadas(tipoConciliacion);
        }
        catch (Exception ex) { App.ImplementadorMensajes.MostrarMensaje(ex.Message); }

    }
    protected void btnAceptarConfirmarDesconciliar_Click(object sender, EventArgs e)
    {

    }

    protected void ddlCampoFiltrar_SelectedIndexChanged(object sender, EventArgs e)
    {
        //activarControles(tipoCampoSeleccionado());
        //mpeFiltrar.Show();
    }
    protected void ddlStatusConcepto_SelectedIndexChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    if (tipoConciliacion != 2)
        //    {
        //        Consulta_ConciliarArchivosCantidadReferencia(corporativo, sucursal, año, mes, folio,
        //                                                     Convert.ToSByte(txtDias.Text),
        //                                                     Convert.ToDecimal(txtDiferencia.Text),
        //                                                     ddlCampoExterno.SelectedItem.Text,
        //                                                     ddlCampoInterno.SelectedItem.Text,
        //                                                     Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value));
        //        GenerarTablaReferenciasAConciliarArchivos();
        //    }

        //    else
        //    {
        //        Consulta_ConciliarPedidosCantidadReferencia(corporativo, sucursal, año, mes, folio,
        //                                                    Convert.ToDecimal(txtDiferencia.Text),
        //                                                    Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value));
        //        GenerarTablaReferenciasAConciliarPedidos();

        //    }

        //    LlenaGridViewReferenciasConciliadas(tipoConciliacion);
        //}
        //catch (Exception ex)
        //{
        //    App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        //}



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
    ////Activar los textbox segun sea el tipo de campa seleccionado en el filtro
    //public void activarControles(string tipoCampo)
    //{
    //    switch (tipoCampo)
    //    {
    //        case "Numero":
    //            // this.rfvValorExterno.ControlToValidate = "txtValorNumericoExterno";
    //            this.txtValorNumericoFiltro.Visible = true;
    //            this.txtValorCadenaFiltro.Visible = false;
    //            this.txtValorFechaFiltro.Visible = false;
    //            this.txtValorNumericoFiltro.Text = "0";
    //            this.txtValorCadenaFiltro.Text = String.Empty;
    //            this.txtValorFechaFiltro.Text = String.Empty;
    //            break;
    //        case "Fecha":
    //            //this.rfvValorExterno.ControlToValidate = "txtValorFechaExterno";
    //            this.txtValorFechaFiltro.Visible = true;
    //            this.txtValorNumericoFiltro.Visible = false;
    //            this.txtValorCadenaFiltro.Visible = false;
    //            this.txtValorFechaFiltro.Text = String.Empty;
    //            this.txtValorNumericoFiltro.Text = String.Empty;
    //            this.txtValorCadenaFiltro.Text = String.Empty;
    //            break;
    //        case "Cadena":
    //            //  this.rfvValorExterno.ControlToValidate = "txtValorCadenaExterno";
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
        //Leer el tipoConciliacion URL

        /*Se requiere realizar la modificacion de tipoConciliacion desde esta vista, 
             * ya que no se requiere modifcar alguna otra vista por lo cual se envia de manera 
             * estatica el valor del tipo conciliacion*/

        tipoConciliacion = 2;//Convert.ToSByte(Request.QueryString["TipoConciliacion"]);
        cargar_ComboCampoFiltroDestino(tipoConciliacion, ddlFiltrarEn.SelectedItem.Value);
        return listCamposDestino[ddlCampoFiltrar.SelectedIndex].Campo1;
    }

    protected void btnIrFiltro_Click(object sender, EventArgs e)
    {
        FiltrarCampo(valorFiltro(tipoCampoSeleccionado()), ddlFiltrarEn.SelectedItem.Value);
        mpeFiltrar.Hide();
    }
    private void FiltrarCampo(string valorFiltro, string filtroEn)
    {
        try
        {
            DataTable dt = filtroEn.Equals("Conciliados")
                               ? (DataTable)HttpContext.Current.Session["TAB_CONCILIADAS"]
                               : (DataTable)HttpContext.Current.Session["TBL_REFCON_CANTREF"];

            //Leer el tipoConciliacion URL

            /*Se requiere realizar la modificacion de tipoConciliacion desde esta vista, 
             * ya que no se requiere modifcar alguna otra vista por lo cual se envia de manera 
             * estatica el valor del tipo conciliacion*/
            
            tipoConciliacion = 2; //Convert.ToSByte(Request.QueryString["TipoConciliacion"]);

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
            // grvPedidos.DataSource = dv;
            //grvPedidos.DataBind();

            if (filtroEn.Equals("Conciliados"))
            {
                ViewState["TAB_CONCILIADAS"] = dv.ToTable();
                grvConciliadas.DataSource = ViewState["TAB_CONCILIADAS"] as DataTable;
                grvConciliadas.DataBind();

            }
            else
            {
                ViewState["TBL_REFCON_CANTREF"] = dv.ToTable();
                if (tipoConciliacion == 2)
                {
                    grvCantidadReferenciaConcuerdanPedido.DataSource = ViewState["TBL_REFCON_CANTREF"] as DataTable;
                    grvCantidadReferenciaConcuerdanPedido.DataBind();
                }
                else
                {
                    grvCantidadReferenciaConcuerdanArchivos.DataSource = ViewState["TBL_REFCON_CANTREF"] as DataTable;
                    grvCantidadReferenciaConcuerdanArchivos.DataBind();
                }
            }

            mpeFiltrar.Hide();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Verifique:\n- Valor no valido por tipo de Campo seleccionado.");
            mpeFiltrar.Hide();
        }

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
    protected void btnIrBuscar_Click(object sender, EventArgs e)
    {
        if (ddlBuscarEn.SelectedItem.Value.Equals("Conciliados"))
        {
            grvConciliadas.DataSource = ViewState["TAB_CONCILIADAS"] as DataTable;
            grvConciliadas.DataBind();
        }
        else
        {
            //Leer el tipoConciliacion URL

            /*Se requiere realizar la modificacion de tipoConciliacion desde esta vista, 
             * ya que no se requiere modifcar alguna otra vista por lo cual se envia de manera 
             * estatica el valor del tipo conciliacion*/

            tipoConciliacion = 2; //Convert.ToSByte(Request.QueryString["TipoConciliacion"]);

            if (tipoConciliacion == 2)
            {
                grvCantidadReferenciaConcuerdanPedido.DataSource = ViewState["TBL_REFCON_CANTREF"] as DataTable;
                grvCantidadReferenciaConcuerdanPedido.DataBind();
            }
            else
            {
                grvCantidadReferenciaConcuerdanArchivos.DataSource = ViewState["TBL_REFCON_CANTREF"] as DataTable;
                grvCantidadReferenciaConcuerdanArchivos.DataBind();
            }
        }


        mpeBuscar.Hide();
    }

    protected void ddlCriteriosConciliacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string criterioConciliacion = ddlCriteriosConciliacion.SelectedItem.Text.Equals("CANTIDAD CONCUERDA")
        //                                   ? "CantidadConcuerda" : ddlCriteriosConciliacion.SelectedItem.Text.Equals("CANTIDAD Y REFERENCIA CONCUERDA") ? "CantidadYRefernciaConcuerdan" :
        //                                                               ddlCriteriosConciliacion.SelectedItem.Text.Equals("UNO A VARIOS") ? "UnoAVarios" : "CopiaDeConciliacion";

        //Response.Redirect("~/Conciliacion/FormasConciliar/" + criterioConciliacion +
        //                              ".aspx?Folio=" + folio + "&Corporativo=" + corporativo +
        //                              "&Sucursal=" + sucursal + "&Año=" + año + "&Mes=" +
        //                              mes + "&TipoConciliacion=" + tipoConciliacion);

    }
    protected void grvCantidadReferenciaConcuerdanArchivos_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='bg-color-grisClaro02'");
            e.Row.Attributes.Add("onmouseout", "this.className='bg-color-blanco'");
        }
    }
    protected void grvCantidadReferenciaConcuerdanPedido_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='bg-color-grisClaro02'");
            e.Row.Attributes.Add("onmouseout", "this.className='bg-color-blanco'");
        }
    }
    protected void grvCantidadReferenciaConcuerdanArchivos_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtSortTable = ViewState["TBL_REFCON_CANTREF"] as DataTable;
        //DataTable dtSortTable = (DataTable)grvPedidos.DataSource;

        if (dtSortTable == null) return;
        string order = getSortDirectionString(e.SortExpression);
        dtSortTable.DefaultView.Sort = e.SortExpression + " " + order;
        //HttpContext.Current.Session["TBL_REFCON_CANTREF"] = dtSortTable;
        grvCantidadReferenciaConcuerdanArchivos.DataSource = dtSortTable;
        grvCantidadReferenciaConcuerdanArchivos.DataBind();
    }
    protected void grvConciliadas_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtSortTable = ViewState["TAB_CONCILIADAS"] as DataTable;
        //DataTable dtSortTable = (DataTable)grvPedidos.DataSource;

        if (dtSortTable == null) return;
        string order = getSortDirectionString(e.SortExpression);
        dtSortTable.DefaultView.Sort = e.SortExpression + " " + order;
        //HttpContext.Current.Session["TAB_CONCILIADAS"] = dtSortTable;
        grvConciliadas.DataSource = dtSortTable;
        grvConciliadas.DataBind();
    }
    protected void grvCantidadReferenciaConcuerdanPedido_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtSortTable = HttpContext.Current.Session["TBL_REFCON_CANTREF"] as DataTable;
        //DataTable dtSortTable = (DataTable)grvPedidos.DataSource;

        if (dtSortTable == null) return;
        string order = getSortDirectionString(e.SortExpression);
        dtSortTable.DefaultView.Sort = e.SortExpression + " " + order;
        //HttpContext.Current.Session["TBL_REFCON_CANTREF"] = dtSortTable;
        grvCantidadReferenciaConcuerdanPedido.DataSource = dtSortTable;
        grvCantidadReferenciaConcuerdanPedido.DataBind();
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

        // Save new values in ViewState.
        ViewState["SortDirection"] = sortDirection;
        ViewState["SortExpression"] = columna;

        return sortDirection;
    }
    protected void ddlCampoInterno_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Leer Variables URL 
        cargarInfoConciliacionActual();

        if (tipoConciliacion != 2)
        {
            Consulta_ConciliarArchivosCantidadReferencia(corporativo, sucursal, año, mes, folio,
                                                                      Convert.ToSByte(txtDias.Text),
                                                                      Convert.ToDecimal(txtDiferencia.Text),
                                                                      ddlCampoExterno.SelectedItem.Text,
                                                                      ddlCampoInterno.SelectedItem.Text,
                                                                      Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value));
            GenerarTablaReferenciasAConciliarArchivos();
        }
        else
        {
            //cargarInfoConciliacionActual();

            Consulta_Externos(corporativo, sucursal, año, mes, folio, Convert.ToDecimal(txtDiferencia.Text), tipoConciliacion, Convert.ToInt32(ddlStatusConcepto.SelectedValue), true);

            Consulta_ConciliarPedidosCantidadReferencia(
                                                        Convert.ToDecimal(txtDiferencia.Text),
                                                        Convert.ToSByte(ddlStatusConcepto.SelectedItem.Value),
                                                        ddlCampoExterno.SelectedItem.Text, ddlCampoInterno.SelectedItem.Text);
            GenerarTablaReferenciasAConciliarPedidos();
        }
        LlenaGridViewReferenciasConciliadas(tipoConciliacion);
    }

    //protected void imgBuscar_Click(object sender, ImageClickEventArgs e)
    //{
    //    txtBuscar.Text = String.Empty;
    //    mpeBuscar.Show();
    //}

    protected void btnIrFormaConciliacion_Click(object sender, EventArgs e)
    {

    }

    protected void btnActualizarConfig_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //Leer Variables URL 
            cargarInfoConciliacionActual();

            if (tipoConciliacion == 2)
            {
                Consulta_Externos(corporativo, sucursal, año, mes, folio, Convert.ToDecimal(txtDiferencia.Text), tipoConciliacion, Convert.ToInt32(ddlStatusConcepto.SelectedValue), true);

                Consulta_ConciliarPedidosCantidadReferencia(
                                                            Convert.ToDecimal(txtDiferencia.Text),
                                                            Convert.ToSByte(ddlStatusConcepto.SelectedItem.Value),
                                                            ddlCampoExterno.SelectedItem.Text, ddlCampoInterno.SelectedItem.Text);
                GenerarTablaReferenciasAConciliarPedidos();
            }
            else
            {
                if (tipoConciliacion != 6)
                {
                    Consulta_ConciliarArchivosCantidadReferencia(corporativo, sucursal, año, mes, folio,
                        Convert.ToSByte(txtDias.Text),
                        Convert.ToDecimal(txtDiferencia.Text),
                        ddlCampoExterno.SelectedItem.Text,
                        ddlCampoInterno.SelectedItem.Text,
                        Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value));
                    GenerarTablaReferenciasAConciliarArchivos();
                }
                else
                {
                    Consulta_ConciliarArchivosCantidadReferencia(corporativo, sucursal, año, mes, folio,
                        Convert.ToSByte(txtDias.Text),
                        Convert.ToDecimal(txtDiferencia.Text),
                        ddlCampoExterno.SelectedItem.Text,
                        ddlCampoInterno.SelectedItem.Text,
                        Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value));
                    GenerarTablaReferenciasAConciliarArchivos();
                }

            }
            LlenaGridViewReferenciasConciliadas(tipoConciliacion);
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }
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
        //Limpian variables de Session
        limpiarVariablesSession();

        Response.Redirect("~/Conciliacion/FormasConciliar/" + criterioConciliacion +
                                      ".aspx?Folio=" + folio + "&Corporativo=" + corporativo +
                                      "&Sucursal=" + sucursal + "&Año=" + año + "&Mes=" +
                                      mes + "&TipoConciliacion=" + Convert.ToSByte(Request.QueryString["TipoConciliacion"]) + "&FormaConciliacion=" + Convert.ToSByte(ddlCriteriosConciliacion.SelectedValue));
    }

    private short Asigna_FormaConciliacionActual()
    {
        short _FormaConciliacion = Convert.ToSByte(Request.QueryString["FormaConciliacion"]);
        if (_FormaConciliacion == 0)
        {
            if (Convert.ToSByte(Request.QueryString["TipoConciliacion"]) == 6)
            {
                _FormaConciliacion = 7;
            }
            else
            {
                _FormaConciliacion = 2;
            }
        }
        return _FormaConciliacion;
    }

    private void BloqueaUnSeleccionado(ReferenciaConciliadaPedido rfEx)
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
            short _FormaConciliacion = Asigna_FormaConciliacionActual();
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
                FormaConciliacion = "CANTIDADYREFERENCIACONCUERDA",
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

    private void BloqueaUnSeleccionadoArchivo(ReferenciaConciliada rfEx)
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
            short _FormaConciliacion = Asigna_FormaConciliacionActual();
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
                FormaConciliacion = "CANTIDADYREFERENCIACONCUERDA",
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

    private bool hayBloqueados(GridView grv)
    {
        bool Existen = false;
        if (LockerExterno.ExternoBloqueado != null)
        {
            SolicitudConciliacion objSolicitdConciliacion = new SolicitudConciliacion();
            tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);
            short _FormaConciliacion = Asigna_FormaConciliacionActual();
            objSolicitdConciliacion.TipoConciliacion = tipoConciliacion;
            objSolicitdConciliacion.FormaConciliacion = _FormaConciliacion;
            int filaindex = 0;
            foreach (GridViewRow fila in grv.Rows) //grvCantidadConcuerdanPedidos
            {
                if (fila.RowType == DataControlRowType.DataRow)
                {
                    if (objSolicitdConciliacion.ConsultaPedido())  //if (tipoConciliacion == 2)
                    {
                        listaReferenciaConciliadaPedido[filaindex].Selecciona = fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                        if (fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked)
                        {
                            Existen = LockerExterno.ExternoBloqueado.Exists(x => x.Corporativo == listaReferenciaConciliadaPedido[filaindex].Corporativo &&
                                                                                x.Sucursal == listaReferenciaConciliadaPedido[filaindex].Sucursal &&
                                                                                x.Año == listaReferenciaConciliadaPedido[filaindex].Año &&
                                                                                x.Folio == listaReferenciaConciliadaPedido[filaindex].Folio &&
                                                                                x.Secuencia == listaReferenciaConciliadaPedido[filaindex].Secuencia);
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
            short _FormaConciliacion = Asigna_FormaConciliacionActual();
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
                        listaReferenciaConciliadaPedido[filaindex].Selecciona = fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                        Corporativo = listaReferenciaConciliadaPedido[filaindex].Corporativo;
                        Sucursal = listaReferenciaConciliadaPedido[filaindex].Sucursal;
                        Año = listaReferenciaConciliadaPedido[filaindex].Año;
                        Folio = listaReferenciaConciliadaPedido[filaindex].Folio;
                        Secuencia = listaReferenciaConciliadaPedido[filaindex].Secuencia;
                        Descripcion = listaReferenciaConciliadaPedido[filaindex].Descripcion;
                        Monto = listaReferenciaConciliadaPedido[filaindex].Total; //monto
                    }
                    else //if (objSolicitdConciliacion.ConsultaArchivo())
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
                            FormaConciliacion = "CANTIDADYREFERENCIACONCUERDA",
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
        short _FormaConciliacion = Asigna_FormaConciliacionActual();
        //Leer Variables URL 
        hfTipoCobroSeleccionado.Value = ddlTiposDeCobro.SelectedValue;
        cargarInfoConciliacionActual();

        SolicitudConciliacion objSolicitdConciliacion = new SolicitudConciliacion();
        objSolicitdConciliacion.TipoConciliacion = tipoConciliacion;
        objSolicitdConciliacion.FormaConciliacion = _FormaConciliacion;

        Usuario usuario = (Usuario)HttpContext.Current.Session["Usuario"];
        if (objSolicitdConciliacion.ConsultaArchivo())
        {
            if (grvCantidadReferenciaConcuerdanArchivos.Rows.Count > 0)
            {
                listaReferenciaConciliada = HttpContext.Current.Session["POR_CONCILIAR"] as List<ReferenciaConciliada>;
                try
                {
                    if (listaReferenciaConciliada != null)
                    {
                        listaReferenciaConciliada.ForEach(x => x.Usuario = usuario.IdUsuario);
                        listaReferenciaConciliada.ForEach(x => resultado = x.Guardar());
                    }
                    else
                        App.ImplementadorMensajes.MostrarMensaje("No existe ninguna referencia a conciliar. Verifique");
                }
                finally
                {
                    desBloqueaTodo();
                }
            }
            else
                App.ImplementadorMensajes.MostrarMensaje("No existe ninguna referencia a conciliar. Verifique");
        }
        if (objSolicitdConciliacion.ConsultaPedido())
        {
            bool guardado = false;
            if (grvCantidadReferenciaConcuerdanPedido.Rows.Count > 0)
            {
                listaReferenciaConciliadaPedido = HttpContext.Current.Session["POR_CONCILIAR"] as List<ReferenciaConciliadaPedido>;
                IList listRefCon = listaReferenciaConciliadaPedido;
                if (listRefCon != null)
                {
                    try
                    {
                        for (int i = 0; i < listRefCon.Count; i++)
                        {
                            ReferenciaConciliadaPedido refcon = (ReferenciaConciliadaPedido)listRefCon[i];
                            if (refcon.Selecciona)
                            {
                                refcon.Usuario = usuario.IdUsuario;
                                refcon.Guardar();
                                guardado = true;
                            }
                        }
                    }
                    finally
                    {
                        desBloqueaTodo();
                    }
                    if (!guardado)
                        App.ImplementadorMensajes.MostrarMensaje("No se selecciono ninguna referencia a conciliar. Verifique");
                }
                else
                    App.ImplementadorMensajes.MostrarMensaje("No existe ninguna referencia a conciliar. Verifique");
            }
            else
                App.ImplementadorMensajes.MostrarMensaje("No existe ninguna referencia a conciliar. Verifique");
            resultado = guardado;
        }

        //ACTUALIZAR BARRAS Y DE MAS 
        LlenarBarraEstado();
        Consulta_TransaccionesConciliadas(corporativo, sucursal, año, mes, folio, Convert.ToInt32(ddlCriteriosConciliacion.SelectedItem.Value));
        GenerarTablaConciliados();
        LlenaGridViewConciliadas();

        if (tipoConciliacion == 2)
        {
            Consulta_Externos(corporativo, sucursal, año, mes, folio, Convert.ToDecimal(txtDiferencia.Text), tipoConciliacion, Convert.ToInt32(ddlStatusConcepto.SelectedValue), true);

            Consulta_ConciliarPedidosCantidadReferencia(Convert.ToDecimal(txtDiferencia.Text), Convert.ToSByte(ddlStatusConcepto.SelectedItem.Value), ddlCampoExterno.SelectedItem.Text, ddlCampoInterno.SelectedItem.Text);
            GenerarTablaReferenciasAConciliarPedidos();
        }
        else
        {
            Consulta_ConciliarArchivosCantidadReferencia(corporativo, sucursal, año, mes, folio,
                                                          Convert.ToSByte(txtDias.Text),
                                                          Convert.ToDecimal(txtDiferencia.Text),
                                                          ddlCampoExterno.SelectedItem.Text,
                                                          ddlCampoInterno.SelectedItem.Text,
                                                          Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value));
            GenerarTablaReferenciasAConciliarArchivos();
        }

        LlenaGridViewReferenciasConciliadas(tipoConciliacion);

        App.ImplementadorMensajes.MostrarMensaje(resultado ?
            "Guardado Exitosamente" : "Error al Guardar");
    }
    protected void Nueva_Ventana(string Pagina, string Titulo, int Ancho, int Alto, int X, int Y)
    {

        ScriptManager.RegisterClientScriptBlock(this.upBarraHerramientas,
                                            upBarraHerramientas.GetType(),
                                            "ventana",
                                            "ShowWindow('" + Pagina + "','" + Titulo + "'," + Ancho + "," + Alto + "," + X + "," + Y + ")",
                                            true);

        //Page.ClientScript.RegisterStartupScript(this.GetType(), "ventana", "ShowWindow('" + Pagina + "','" + Titulo + "'," + Ancho + "," + Alto + "," + X + "," + Y + ")");
    }
    protected void imgExportar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            AppSettingsReader settings = new AppSettingsReader();
            //Leer Variables URL 
            cargarInfoConciliacionActual();

            string strReporte = Server.MapPath("~/") + settings.GetValue(tipoConciliacion == 2 ? "RutaReporteRemanentesConciliacion" : "RutaReporteConciliacionTesoreria", typeof(string));

            if (!File.Exists(strReporte)) return;
            try
            {
                string strServer = settings.GetValue("Servidor", typeof(string)).ToString();
                string strDatabase = settings.GetValue("Base", typeof(string)).ToString();



                usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
                string strUsuario = usuario.IdUsuario.Trim();
                string strPW = usuario.ClaveDesencriptada;
                ArrayList Par = new ArrayList();

                Par.Add("@Corporativo=" + corporativo);
                Par.Add("@Sucursal=" + sucursal);
                Par.Add("@AñoConciliacion=" + año);
                Par.Add("@MesConciliacion=" + mes);
                Par.Add("@FolioConciliacion=" + folio);
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
        cConciliacion c = App.Consultas.ConsultaConciliacionDetalle(corporativo, sucursal, año, mes, folio);
        if (c.CerrarConciliacion(usuario.IdUsuario))
        {
            // LlenarBarraEstado();
            App.ImplementadorMensajes.MostrarMensaje("CONCILIACIÓN CERRADA EXITOSAMENTE");
            mpeConfirmarCerrar.Hide();
            System.Threading.Thread.Sleep(3000);
            Response.Redirect("~/Conciliacion/DetalleConciliacion.aspx?Folio=" + folio + "&Corporativo=" + corporativo +
                                    "&Sucursal=" + sucursal + "&Año=" + año + "&Mes=" +
                                    mes + "&TipoConciliacion=" + tipoConciliacion);
        }
        else
        {
            App.ImplementadorMensajes.MostrarMensaje("ERRORES AL CERRAR LA CONCILIACIÓN");
            mpeConfirmarCerrar.Hide();
        }
    }
    protected void btnAceptarConfirmarCerrar_Click(object sender, EventArgs e)
    {

    }
    protected void imgCancelarConciliacion_Click(object sender, ImageClickEventArgs e)
    {
        usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
        //Leer Variables URL 
        cargarInfoConciliacionActual();
        cConciliacion c = App.Consultas.ConsultaConciliacionDetalle(corporativo, sucursal, año, mes, folio);
        if (c.CancelarConciliacion(usuario.IdUsuario))
        {
            // LlenarBarraEstado();  
            App.ImplementadorMensajes.MostrarMensaje("CONCILIACIÓN CANCELADA EXITOSAMENTE");
            mpeConfirmarCancelar.Hide();
            System.Threading.Thread.Sleep(3000);
            Response.Redirect("~/Inicio.aspx");
        }
        else
        {
            App.ImplementadorMensajes.MostrarMensaje("ERRORES AL CANCELAR LA CONCILIACIÓN");
            mpeConfirmarCancelar.Hide();
        }
    }
    protected void btnAceptarConfirmarCancelar_Click(object sender, EventArgs e)
    {

    }

    protected void imgFiltrar_Click(object sender, ImageClickEventArgs e)
    {
        //Leer el tipoConciliacion URL

        /*Se requiere realizar la modificacion de tipoConciliacion desde esta vista, 
             * ya que no se requiere modifcar alguna otra vista por lo cual se envia de manera 
             * estatica el valor del tipo conciliacion*/

        tipoConciliacion = 2; //Convert.ToSByte(Request.QueryString["TipoConciliacion"]);

        cargar_ComboCampoFiltroDestino(tipoConciliacion, ddlFiltrarEn.SelectedItem.Value);
        enlazarCampoFiltrar();
        InicializarControlesFiltro();
        mpeFiltrar.Show();
    }

    //------------------------------IMPORTAR---------------------------------------------------
    protected void imgCerrarImportar_Click(object sender, ImageClickEventArgs e)
    {
        //Limpiar Remover Variable (Session) de Internos 
        limpiarVistaImportarInterno();

        popUpImportarArchivos.Hide();
        popUpImportarArchivos.Dispose();

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
    /// Llena el Combo de Folios Internos según parametros de filtro.
    /// </summary>
    public void Carga_FoliosInternos(int corporativo, int sucursal, int añoF, short mesF, string cuentabancaria, short tipofuenteinformacion)
    {
        try
        {
            listFoliosInterno = Conciliacion.RunTime.App.Consultas.ConsultaFoliosTablaDestino(corporativo, sucursal, añoF, mesF, cuentabancaria, tipofuenteinformacion);
            //HttpContext.Current.Session["listFoliosInterno"] = listFoliosInterno;

        }
        catch
        {
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

        if (listArchivosInternos.Exists(x => x.Folio == Convert.ToInt32(ddlFolioInterno.SelectedItem.Value)))
        {
            App.ImplementadorMensajes.MostrarMensaje("Este Folio Interno ya esta Agregado");
        }
        else
        {
            //Leer Variables URL 
            cargarInfoConciliacionActual();

            cConciliacion conciliacion = App.Consultas.ConsultaConciliacionDetalle(corporativo, sucursal, año, mes, folio);
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
        this.grvAgregados.Dispose();
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

            cConciliacion conciliacion = App.Consultas.ConsultaConciliacionDetalle(corporativo, sucursal, año, mes, folio);
            listArchivosInternos.ForEach(x => resultado = conciliacion.AgregarArchivo(x, cConciliacion.Operacion.Edicion));

            if (resultado)
            {
                //Leer Variables URL 
                cargarInfoConciliacionActual();

                //ACTUALIZAR GRID CANTIDAD Y REFERENCIA CONCUERDA
                LlenarBarraEstado();
                if (tipoConciliacion == 2)
                {
                    Consulta_Externos(corporativo, sucursal, año, mes, folio, Convert.ToDecimal(txtDiferencia.Text), tipoConciliacion, Convert.ToInt32(ddlStatusConcepto.SelectedValue), true);
                    Consulta_ConciliarPedidosCantidadReferencia(
                                                                Convert.ToDecimal(txtDiferencia.Text),
                                                                Convert.ToSByte(ddlStatusConcepto.SelectedItem.Value),
                                                                ddlCampoExterno.SelectedItem.Text, ddlCampoInterno.SelectedItem.Text);
                    GenerarTablaReferenciasAConciliarPedidos();
                }

                else
                {
                    Consulta_ConciliarArchivosCantidadReferencia(corporativo, sucursal, año, mes, folio,
                                                                  Convert.ToSByte(txtDias.Text),
                                                                  Convert.ToDecimal(txtDiferencia.Text),
                                                                  ddlCampoExterno.SelectedItem.Text,
                                                                  ddlCampoInterno.SelectedItem.Text,
                                                                  Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value));
                    GenerarTablaReferenciasAConciliarArchivos();

                }
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


    protected void imgImportar_Click(object sender, ImageClickEventArgs e)
    {
        Carga_TipoFuenteInformacionInterno(Consultas.ConfiguracionTipoFuente.TipoFuenteInformacionInterno);
        LlenaGridViewFoliosAgregados();
        popUpImportarArchivos.Show();
    }
    public void limpiarVistaImportarInterno()
    {
        Session["NUEVOS_INTERNOS"] = null;
        Session.Remove("NUEVOS_INTERNOS");

        ddlTipoFuenteInfoInterno.SelectedIndex = ddlSucursalInterno.SelectedIndex = 0;
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
        this.grvVistaRapidaInterno.DataSource = tablaDestinoDetalleInterno;
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

    public List<ReferenciaConciliadaPedido> filasSeleccionadasPedidos()
    {
        listaReferenciaConciliadaPedido = HttpContext.Current.Session["POR_CONCILIAR"] as List<ReferenciaConciliadaPedido>;
        return listaReferenciaConciliadaPedido.Where(fila => fila.Selecciona == false).ToList();
    }

    public ReferenciaConciliadaPedido leerReferenciaExternaSeleccionada()
    {
        listaReferenciaConciliadaPedido = HttpContext.Current.Session["POR_CONCILIAR"] as List<ReferenciaConciliadaPedido>;
        int secuenciaExterno = Convert.ToInt32(grvCantidadReferenciaConcuerdanPedido.DataKeys[indiceExternoSeleccionado].Values["Secuencia"]);
        int folioExterno = Convert.ToInt32(grvCantidadReferenciaConcuerdanPedido.DataKeys[indiceExternoSeleccionado].Values["FolioExt"]);
        int Pedido = Convert.ToInt32(grvCantidadReferenciaConcuerdanPedido.DataKeys[indiceExternoSeleccionado].Values["Pedido"]);
        int Celula = Convert.ToInt32(grvCantidadReferenciaConcuerdanPedido.DataKeys[indiceExternoSeleccionado].Values["Celula"]);
        int AñoPed = Convert.ToInt32(grvCantidadReferenciaConcuerdanPedido.DataKeys[indiceExternoSeleccionado].Values["AñoPed"]);
        return listaReferenciaConciliadaPedido.Single(x => x.Secuencia == secuenciaExterno && x.Folio == folioExterno && x.Pedido == Pedido && x.CelulaPedido == Celula && x.AñoPedido == AñoPed);
    }

    public ReferenciaConciliada leerReferenciaExternaSeleccionadaArchivo()
    {        
        listaReferenciaConciliada = HttpContext.Current.Session["POR_CONCILIAR"] as List<ReferenciaConciliada>;
        int secuenciaExterno = Convert.ToInt32(grvCantidadReferenciaConcuerdanArchivos.DataKeys[indiceExternoSeleccionado].Values["SecuenciaExt"]);
        int folioExterno = Convert.ToInt32(grvCantidadReferenciaConcuerdanArchivos.DataKeys[indiceExternoSeleccionado].Values["FolioExt"]);
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

            short _FormaConciliacion = Asigna_FormaConciliacionActual();
            tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);
            SolicitudConciliacion objSolicitdConciliacion = new SolicitudConciliacion();
            objSolicitdConciliacion.TipoConciliacion = tipoConciliacion;
            objSolicitdConciliacion.FormaConciliacion = _FormaConciliacion;

            if (objSolicitdConciliacion.ConsultaPedido())
            { 
                ReferenciaConciliadaPedido rfEx = leerReferenciaExternaSeleccionada();
                return LockerExterno.ExternoBloqueado.Exists(x => x.Corporativo == rfEx.Corporativo &&
                                                                x.Sucursal == rfEx.Sucursal &&
                                                                x.Año == rfEx.Año &&
                                                                x.Folio == rfEx.Folio &&
                                                                x.Secuencia == rfEx.Secuencia);
            }
            else //if (objSolicitdConciliacion.ConsultaArchivo())
            {
                ReferenciaConciliada rfEx = leerReferenciaExternaSeleccionadaArchivo();
                return LockerExterno.ExternoBloqueado.Exists(x => x.Corporativo == rfEx.Corporativo &&
                                                                x.Sucursal == rfEx.Sucursal &&
                                                                x.Año == rfEx.Año &&
                                                                x.Folio == rfEx.Folio &&
                                                                x.Secuencia == rfEx.Secuencia);
            }
        }
        else
            return false;
    }

    private void DesBloqueaPedido(ReferenciaConciliadaPedido rfEx)
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

    private void DesBloqueaArchivo(ReferenciaConciliada rfEx)
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

    protected void chkSeleccionado_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = sender as CheckBox;
        GridViewRow grv = (GridViewRow)chk.Parent.Parent;

        int respaldoIndiceSeleccionado = indiceExternoSeleccionado;
        indiceExternoSeleccionado = grv.RowIndex;
        ReferenciaConciliadaPedido rfEx = leerReferenciaExternaSeleccionada();

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
            rfEx.Selecciona = true;
        }
        else
        { 
            rfEx.Selecciona = false;
            DesBloqueaPedido(rfEx);
        }
        GenerarTablaReferenciasAConciliarPedidos();
    }

    protected void chkSeleccionarTodos_CheckedChanged1(object sender, EventArgs e)
    {
        int index = 0;
        int almenosunoBloqueado = 0;
        short _FormaConciliacion = Asigna_FormaConciliacionActual();
        tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);
        SolicitudConciliacion objSolicitdConciliacion = new SolicitudConciliacion();
        objSolicitdConciliacion.TipoConciliacion = tipoConciliacion;
        objSolicitdConciliacion.FormaConciliacion = _FormaConciliacion;

        LockerExterno.EliminarBloqueos(Session.SessionID);
        if (objSolicitdConciliacion.ConsultaPedido())
        {
            foreach (GridViewRow fila in grvCantidadReferenciaConcuerdanPedido.Rows)
            {
                CheckBox chk = fila.Cells[0].Controls[1] as CheckBox;
                indiceExternoSeleccionado = index;
                ReferenciaConciliadaPedido rfEx = leerReferenciaExternaSeleccionada();
                if (!ExisteExternoBloqueado())
                {
                    chk.Checked = chkSeleccionarTodos.Checked;
                    rfEx.Selecciona = chkSeleccionarTodos.Checked;
                    BloqueaUnSeleccionado(rfEx);
                }
                else
                {
                    almenosunoBloqueado++;
                    chk.Checked = false;
                    rfEx.Selecciona = false;
                }
                index++;
            }
            GenerarTablaReferenciasAConciliarPedidos();
        }
        if (objSolicitdConciliacion.ConsultaArchivo())
        {
            foreach (GridViewRow fila in grvCantidadReferenciaConcuerdanArchivos.Rows)
            {
                CheckBox chk = fila.Cells[0].Controls[1] as CheckBox;
                indiceExternoSeleccionado = index;
                ReferenciaConciliada rfEx = leerReferenciaExternaSeleccionadaArchivo();
                if (!ExisteExternoBloqueado())
                {
                    chk.Checked = chkSeleccionarTodos.Checked;
                    rfEx.Selecciona = chkSeleccionarTodos.Checked;
                    BloqueaUnSeleccionadoArchivo(rfEx);
                }
                else
                {
                    almenosunoBloqueado++;
                    chk.Checked = false;
                    rfEx.Selecciona = false;
                }
                index++;
            }
            GenerarTablaReferenciasAConciliarArchivos();
        }
        if (almenosunoBloqueado > 0)
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Mensaje",
                @"alertify.alert('Conciliaci&oacute;n bancaria','El registro no se puede Guardar, el externo seleccionado ya ha sido conciliado por otro usuario.', function(){ });", true);
    }

    protected void grvVistaRapidaInterno_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grvVistaRapidaInterno.DataSource = (DataTable)HttpContext.Current.Session["DETALLEINTERNO"];
        this.grvVistaRapidaInterno.PageIndex = e.NewPageIndex;
        this.grvVistaRapidaInterno.DataBind();
    }

    protected void chkSeleccionadoArchivos_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = sender as CheckBox;
        GridViewRow grv = (GridViewRow)chk.Parent.Parent;

        int respaldoIndiceSeleccionado = indiceExternoSeleccionado;
        indiceExternoSeleccionado = grv.RowIndex;
        ReferenciaConciliada rfEx = leerReferenciaExternaSeleccionadaArchivo();

        if (chk.Checked)
        {
            rfEx.Selecciona = false;//Es solo para guardar la REFERENCIA SELECCIONADA..FALSE porq se hace un ! negacion..al cargar el Externos..para no modificar otra cosa.
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
                BloqueaUnSeleccionadoArchivo(rfEx);
            rfEx.Selecciona = true;
        }
        else
        {
            rfEx.Selecciona = false;
            DesBloqueaArchivo(rfEx);
        }
        GenerarTablaReferenciasAConciliarArchivos();
    }


    protected void ddlPorConciliarTipoCobro_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;
        GridViewRow grv = (GridViewRow)ddl.Parent.Parent;
        indiceExternoSeleccionado = grv.RowIndex;
        ReferenciaConciliadaPedido rfEx = leerReferenciaExternaSeleccionada();
        rfEx.TipoCobroAnterior = rfEx.TipoCobro;
        rfEx.TipoCobro = int.Parse(ddl.SelectedValue.ToString());
    }

    protected void imgDesconciliar_Click(object sender, EventArgs e)
    {

    }
}