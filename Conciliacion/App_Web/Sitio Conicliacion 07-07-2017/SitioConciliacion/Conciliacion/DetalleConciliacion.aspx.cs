using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Conciliacion.RunTime;
using Conciliacion.RunTime.ReglasDeNegocio;
using System.Text.RegularExpressions;
using System.Configuration;

using System.Collections;
using System.IO;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SeguridadCB.Public;

public partial class Conciliacion_DetalleConciliacion : System.Web.UI.Page
{
    #region "Propiedades Globales / afortunadamente no son globales, hay que remover éstos miembros *Iván Trujillo"
    private SeguridadCB.Public.Operaciones operaciones;
    private SeguridadCB.Public.Usuario usuario;
    private SeguridadCB.Public.Parametros parametros;
    private List<ReferenciaConciliada> listaReferenciaConciliada = new List<ReferenciaConciliada>();
    private List<ReferenciaNoConciliada> listaReferenciaArchivosInternos = new List<ReferenciaNoConciliada>();

    private List<ReferenciaNoConciliada> listaReferenciaExternas = new List<ReferenciaNoConciliada>();
    private List<ReferenciaNoConciliadaPedido> listaReferenciaPedidos = new List<ReferenciaNoConciliadaPedido>();
    private List<ListaCombo> listMotivosNoConciliados = new List<ListaCombo>();

    private List<ListaCombo> listFormasConciliacion = new List<ListaCombo>();
    private List<ListaCombo> listCelulas = new List<ListaCombo>();
    private List<ListaCombo> listSucursales = new List<ListaCombo>();
    private List<ListaCombo> listStatusConcepto = new List<ListaCombo>();

    private DataTable tblReferenciaExternas;
    private DataTable tblReferenciaInternas;
    private DataTable tblDetalleTransaccionConciliada;

    private List<ReferenciaNoConciliada> listaTransaccionesConciliadas = new List<ReferenciaNoConciliada>();

    private DataTable tblTransaccionesConciliadas;
    private List<ListaCombo> listCamposDestino = new List<ListaCombo>();

    private DatosArchivo datosArchivoInterno;
    private List<ListaCombo> listTipoFuenteInformacionExternoInterno = new List<ListaCombo>();
    private List<ListaCombo> listFoliosInterno = new List<ListaCombo>();
    private List<DatosArchivo> listArchivosInternos = new List<DatosArchivo>();

    private DataTable tblDestinoDetalleInterno;
    private List<DatosArchivoDetalle> listaDestinoDetalleInterno = new List<DatosArchivoDetalle>();
    #endregion

    private int corporativoConciliacion, añoConciliacion, folioConciliacion, folioExterno, sucursalConciliacion;
    private short mesConciliacion;
    private string DiferenciaDiasMaxima, DiferenciaDiasMinima, DiferenciaCentavosMaxima, DiferenciaCentavosMinima;
    public short tipoConciliacion;

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
        //try
        //{

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
            AppSettingsReader settings = new AppSettingsReader();

            DiferenciaCentavosMaxima = parametros.ValorParametro(Convert.ToSByte(settings.GetValue("Modulo", typeof(sbyte))), "DiferenciaCentavosMaxima");
            DiferenciaCentavosMinima = parametros.ValorParametro(Convert.ToSByte(settings.GetValue("Modulo", typeof(sbyte))), "DiferenciaCentavosMinima");

            txtDiferencia.Text = parametros.ValorParametro(Convert.ToSByte(settings.GetValue("Modulo", typeof(sbyte))), "DiferenciaCentavosMaxima");//DiferenciaCentavosMaxima

            rvDiferencia.MaximumValue = DiferenciaCentavosMaxima;
            rvDiferencia.MinimumValue = DiferenciaCentavosMinima;
            rvDiferencia.ErrorMessage = "Valores permitidos entre " + DiferenciaCentavosMinima + " - " + DiferenciaCentavosMaxima + " centavos";

            corporativoConciliacion = Convert.ToInt32(Request.QueryString["Corporativo"]);
            sucursalConciliacion = Convert.ToInt16(Request.QueryString["Sucursal"]);
            añoConciliacion = Convert.ToInt32(Request.QueryString["Año"]);
            folioConciliacion = Convert.ToInt32(Request.QueryString["Folio"]);
            mesConciliacion = Convert.ToSByte(Request.QueryString["Mes"]);
            tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);

            cargar_ComboMotivosNoConciliado();


            Carga_FormasConciliacion(tipoConciliacion);
            Carga_StatusConcepto(Consultas.ConfiguracionStatusConcepto.ConEtiquetas);
            LlenarBarraEstado();

            //CARGAR LAS TRANSACCIONES CONCILIADAS POR EL CRITERIO DE AUTOCONCILIACIÓN
            Consulta_TransaccionesConciliadas(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, Convert.ToSByte(ddlCriteriosConciliacion.SelectedItem.Value), tipoConciliacion);
            GenerarTablaConciliados();
            LlenaGridViewConciliadas();

            Consulta_Externos(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value));
            GenerarTablaExternos();
            LlenaGridViewExternos();
            if (tipoConciliacion == 2)
            {
                lblSucursalCelula.Text = "Celula Interna";
                ddlCelula.Visible = true;
                lblPedidos.Visible = true;
                //tdExportar.Attributes.Add("class", "iconoOpcion bg-color-grisClaro02");
                btnCANCELARINTERNO.Visible = tdEtiquetaMontoIn.Visible = tdMontoIn.Visible = false;//imgExportar.Enabled = 
                Carga_CelulaCorporativo(corporativoConciliacion);
                Consulta_Pedidos(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, Convert.ToInt32(ddlCelula.SelectedItem.Value));
                GenerarTablaPedidos();
                LlenaGridViewPedidos();
            }
            else
            {
                lblSucursalCelula.Text = "Sucursal Interna";
                ddlSucursal.Visible = true;

                lblArchivosInternos.Visible = true;
                btnCANCELARINTERNO.Visible = true;
                Carga_SucursalCorporativo(corporativoConciliacion);
                Consulta_ArchivosInternos(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, Convert.ToDecimal(txtDiferencia.Text), Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value), Convert.ToInt16(ddlSucursal.SelectedItem.Value));
                GenerarTablaArchivosInternos();
                LlenaGridViewArchivosInternos();
            }
            Carga_TipoFuenteInformacionInterno(Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionTipoFuente.TipoFuenteInformacionInterno);
            activarImportacion(tipoConciliacion);
            ocultarFiltroFechas(tipoConciliacion);
        }
        //}
        //catch (Exception ex) { App.ImplementadorMensajes.MostrarMensaje(ex.Message); }
    }


    //Cargar InfoConciliacion Actual
    public void cargarInfoConciliacionActual()
    {
        //Leer variables de URL
        corporativoConciliacion = Convert.ToInt32(Request.QueryString["Corporativo"]);
        sucursalConciliacion = Convert.ToInt16(Request.QueryString["Sucursal"]);
        añoConciliacion = Convert.ToInt32(Request.QueryString["Año"]);
        folioConciliacion = Convert.ToInt32(Request.QueryString["Folio"]);
        mesConciliacion = Convert.ToSByte(Request.QueryString["Mes"]);
        tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);

    }
    //Limpian variables de Session
    public void limpiarVariablesSession()
    {
        //Eliminar las variables de Session utilizadas en la Vista
        HttpContext.Current.Session["TAB_EXTERNOS"] = null;
        HttpContext.Current.Session["POR_CONCILIAR_EXTERNO"] = null;
        HttpContext.Current.Session["CONCILIADAS"] = null;
        HttpContext.Current.Session["TAB_CONCILIADAS"] = null;
        HttpContext.Current.Session["TAB_EXTERNOS"] = null;
        HttpContext.Current.Session["TAB_INTERNOS"] = null;
        HttpContext.Current.Session["POR_CONCILIAR_INTERNO"] = null;
        HttpContext.Current.Session["RepDoc"] = null;
        HttpContext.Current.Session["ParametrosReporte"] = null;
        HttpContext.Current.Session["NUEVOS_INTERNOS"] = null;
        HttpContext.Current.Session["DETALLEINTERNO"] = null;

        HttpContext.Current.Session.Remove("TAB_EXTERNOS");
        HttpContext.Current.Session.Remove("POR_CONCILIAR_EXTERNO");
        HttpContext.Current.Session.Remove("CONCILIADAS");
        HttpContext.Current.Session.Remove("TAB_CONCILIADAS");
        HttpContext.Current.Session.Remove("TAB_EXTERNOS");
        HttpContext.Current.Session.Remove("TAB_INTERNOS");
        HttpContext.Current.Session.Remove("POR_CONCILIAR_INTERNO");
        HttpContext.Current.Session.Remove("RepDoc");
        HttpContext.Current.Session.Remove("ParametrosReporte");
        HttpContext.Current.Session.Remove("NUEVOS_INTERNOS");
        HttpContext.Current.Session.Remove("DETALLEINTERNO");

    }
    /// <summary>
    /// Cargar Combo de Motivos por lo q no se Cancela la Tranasaccion
    /// </summary>
    public void cargar_ComboMotivosNoConciliado()
    {
        listMotivosNoConciliados = Conciliacion.RunTime.App.Consultas.ConsultaMotivoNoConciliado();
        this.ddlMotivosNoConciliado.DataSource = listMotivosNoConciliados;
        this.ddlMotivosNoConciliado.DataValueField = "Identificador";
        this.ddlMotivosNoConciliado.DataTextField = "Descripcion";
        this.ddlMotivosNoConciliado.DataBind();
        this.ddlMotivosNoConciliado.Dispose();
    }
    public void GenerarTablaExternos()//Genera la tabla Referencias Externas
    {
        tblReferenciaExternas = new DataTable("ReferenciasExternas");
        tblReferenciaExternas.Columns.Add("Secuencia", typeof(int));
        tblReferenciaExternas.Columns.Add("Folio", typeof(int));
        tblReferenciaExternas.Columns.Add("Año", typeof(int));
        tblReferenciaExternas.Columns.Add("ConInterno", typeof(bool));
        tblReferenciaExternas.Columns.Add("FMovimiento", typeof(DateTime));
        tblReferenciaExternas.Columns.Add("FOperacion", typeof(DateTime));
        tblReferenciaExternas.Columns.Add("Referencia", typeof(string));
        tblReferenciaExternas.Columns.Add("Retiro", typeof(decimal));
        tblReferenciaExternas.Columns.Add("Deposito", typeof(decimal));
        tblReferenciaExternas.Columns.Add("Concepto", typeof(string));
        tblReferenciaExternas.Columns.Add("Descripcion", typeof(string));
        tblReferenciaExternas.Columns.Add("StatusConciliacion", typeof(string));
        tblReferenciaExternas.Columns.Add("UbicacionIcono", typeof(string));
        foreach (ReferenciaNoConciliada rp in listaReferenciaExternas)
            tblReferenciaExternas.Rows.Add(
                      rp.Secuencia,
                      rp.Folio,
                      rp.Año,
                      rp.ConInterno,
                      rp.FMovimiento,
                      rp.FOperacion,
                      rp.Referencia,
                      rp.Retiro,
                      rp.Deposito,
                      rp.Concepto,
                      rp.Descripcion,
                      rp.StatusConciliacion,
                      rp.UbicacionIcono);

        HttpContext.Current.Session["TAB_EXTERNOS"] = tblReferenciaExternas;
        ViewState["TAB_EXTERNOS"] = tblReferenciaExternas;
    }
    private void LlenaGridViewExternos()//Llena el gridview con las Referencias Externas
    {
        DataTable tablaReferenaciasE = (DataTable)HttpContext.Current.Session["TAB_EXTERNOS"];
        grvExternos.DataSource = tablaReferenaciasE;
        grvExternos.DataBind();
    }
    public void Consulta_Externos(int corporativo, int sucursal, int año, short mes, int folio, int statusConcepto)
    {
        System.Data.SqlClient.SqlConnection Connection = SeguridadCB.Seguridad.Conexion;
        if (Connection.State == ConnectionState.Closed)
        {
            SeguridadCB.Seguridad.Conexion.Open();
            Connection = SeguridadCB.Seguridad.Conexion;
        }
        //try
        //{
        listaReferenciaExternas = Conciliacion.RunTime.App.Consultas.ConsultaTrasaccionesExternasPendientes(corporativo, sucursal, año, mes, folio, statusConcepto);
        Session["POR_CONCILIAR_EXTERNO"] = listaReferenciaExternas;
        //}
        //catch (Exception ex)
        //{
        //    App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        //}
    }

    //Llenar Barra de Estado
    public void LlenarBarraEstado()
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
    /// <summary>
    /// Llena el Combo de Formas de Conciliacion
    /// </summary>
    public void Carga_FormasConciliacion(short tipoConciliacion)
    {
        //try
        //{
        listFormasConciliacion = Conciliacion.RunTime.App.Consultas.ConsultaFormaConciliacion(tipoConciliacion);
        this.ddlCriteriosConciliacion.DataSource = listFormasConciliacion;
        this.ddlCriteriosConciliacion.DataValueField = "Identificador";
        this.ddlCriteriosConciliacion.DataTextField = "Descripcion";
        this.ddlCriteriosConciliacion.DataBind();
        this.ddlCriteriosConciliacion.Dispose();
        //}
        //catch (Exception ex)
        //{
        //    App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        //}
    }
    /// <summary>
    /// Llena el Combo de Formas de Conciliacion
    /// </summary>
    public void Carga_CelulaCorporativo(int corporativo)
    {
        //try
        //{
        listCelulas = Conciliacion.RunTime.App.Consultas.ConsultaCelula(corporativo);
        this.ddlCelula.DataSource = listCelulas;
        this.ddlCelula.DataValueField = "Identificador";
        this.ddlCelula.DataTextField = "Descripcion";
        this.ddlCelula.DataBind();
        this.ddlCelula.Dispose();
        //}
        //catch (Exception ex)
        //{
        //    App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        //}
    }
    /// <summary>
    /// Llena el Combo de Sucurales según Corporativo
    /// </summary>
    public void Carga_SucursalCorporativo(int corporativo)
    {
        //try
        //{
        listSucursales =
                Conciliacion.RunTime.App.Consultas.ConsultaSucursales(
                    Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionIden0.Sin0, corporativo);
        this.ddlSucursal.DataSource = this.ddlSucursalInterno.DataSource = listSucursales;
        this.ddlSucursal.DataValueField = this.ddlSucursalInterno.DataValueField = "Identificador";
        this.ddlSucursal.DataTextField = this.ddlSucursalInterno.DataTextField = "Descripcion";

        this.ddlSucursal.DataBind();
        this.ddlSucursal.Dispose();
        this.ddlSucursalInterno.DataBind();
        this.ddlSucursalInterno.Dispose();
        //}
        //catch (Exception ex)
        //{
        //    App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        //}
    }
    /// <summary>
    /// Llena el Combo de Sucurales según Corporativo
    /// </summary>
    public void Carga_StatusConcepto(Consultas.ConfiguracionStatusConcepto cConcepto)
    {
        //try
        //{
        listStatusConcepto = Conciliacion.RunTime.App.Consultas.ConsultaStatusConcepto(cConcepto);
        this.ddlStatusConcepto.DataSource = listStatusConcepto;
        this.ddlStatusConcepto.DataValueField = "Identificador";
        this.ddlStatusConcepto.DataTextField = "Descripcion";
        this.ddlStatusConcepto.DataBind();
        this.ddlStatusConcepto.Dispose();
        //}
        //catch (Exception ex)
        //{
        //    App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        //}
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
    public void Consulta_TransaccionesConciliadas(int corporativoC, int sucursalC, int añoC, short mesC, int folioC, short formaC, int tipoC)
    {
        System.Data.SqlClient.SqlConnection Connection = SeguridadCB.Seguridad.Conexion;
        if (Connection.State == ConnectionState.Closed)
        {
            SeguridadCB.Seguridad.Conexion.Open();
            Connection = SeguridadCB.Seguridad.Conexion;
        }
        try
        {
            listaTransaccionesConciliadas = Conciliacion.RunTime.App.Consultas.ConsultaTransaccionesRegistradas(tipoC == 2 ? Consultas.ConfiguracionConsultaConciliaciones.Pedido : Consultas.ConfiguracionConsultaConciliaciones.Interno, corporativoC, sucursalC, añoC, mesC, folioC, 0, formaC);
            Session["CONCILIADAS"] = listaTransaccionesConciliadas;

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }
    }
    //Genera la tabla de transacciones Conciliadas
    public void GenerarTablaConciliados()
    {
        tblTransaccionesConciliadas = new DataTable("TransaccionesConciladas");
        tblTransaccionesConciliadas.Columns.Add("Selecciona", typeof(bool));
        tblTransaccionesConciliadas.Columns.Add("Folio", typeof(int));
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
        tblTransaccionesConciliadas.Columns.Add("Concepto", typeof(string));
        tblTransaccionesConciliadas.Columns.Add("Tipo", typeof(string));
        tblTransaccionesConciliadas.Columns.Add("StatusConciliacion", typeof(string));
        tblTransaccionesConciliadas.Columns.Add("UbicacionIcono", typeof(string));

        foreach (ReferenciaNoConciliada rc in listaTransaccionesConciliadas)
        {
            tblTransaccionesConciliadas.Rows.Add(
                rc.Selecciona,
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
                rc.Concepto,
                rc.Tipo,
                rc.StatusConciliacion,
                rc.UbicacionIcono);
        }

        HttpContext.Current.Session["TAB_CONCILIADAS"] = tblTransaccionesConciliadas;
        ViewState["TAB_CONCILIADAS"] = tblTransaccionesConciliadas;
    }
    //Llena el Gridview Transacciones Concilidadas
    private void LlenaGridViewConciliadas()
    {
        DataTable tablaConciliadas = (DataTable)HttpContext.Current.Session["TAB_CONCILIADAS"];
        grvConciliadas.DataSource = tablaConciliadas;
        grvConciliadas.DataBind();

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
            App.ImplementadorMensajes.MostrarMensaje("Mensaje de error " + ex.Message);
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

    //Crea la paginacion para Concilidos
    protected void grvConciliadas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button b;
            switch (DataBinder.Eval(e.Row.DataItem, "Tipo").ToString())
            {
                case "CONCILIADO":
                    e.Row.CssClass = "bg-color-azulClaro01";
                    break;
                case "CANCELADO INTERNO":
                    e.Row.CssClass = "bg-color-amarilloClaro";
                    b = e.Row.FindControl("imgDetalleConciliado") as Button;
                    b.Visible = false;
                    break;
                case "CANCELADO EXTERNO":
                    e.Row.CssClass = "bg-color-verdeClaro01";
                    b = e.Row.FindControl("imgDetalleConciliado") as Button;
                    b.Visible = false;
                    break;
                case "CONCILIADO S/REFERENCIA":
                    e.Row.CssClass = "bg-color-moradoClaro";
                    b = e.Row.FindControl("imgDetalleConciliado") as Button;
                    b.Visible = false;
                    break;
            }
        }
    }

    //Ver el detalle de la Transaccion Conciliada
    protected void grvConciliadas_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

        int corporativoCon = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["CorporativoConciliacion"]);
        int sucursalCon = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["SucursalConciliacion"]);
        int añoCon = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["AñoConciliacion"]);
        int mesCon = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["MesConciliacion"]);
        int folioCon = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["FolioConciliacion"]);
        int folio = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["Folio"]);
        int secuencia = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["Secuencia"]);

        //Leer las TransaccionesConciliadas
        listaTransaccionesConciliadas = Session["CONCILIADAS"] as List<ReferenciaNoConciliada>;

        ReferenciaNoConciliada tConciliada = listaTransaccionesConciliadas.Single(
                    x => x.Corporativo == corporativoCon &&
                    x.Sucursal == sucursalCon &&
                    x.Año == añoCon &&
                    x.MesConciliacion == mesCon &&
                    x.FolioConciliacion == folioCon &&
                    x.Folio == folio &&
                    x.Secuencia == secuencia);

        GeneraTablaDetalleArchivosInternos(tConciliada);
        ConsultaDetalleTransaccionConciliada(tConciliada);
        LlenarGridDetalleInterno(tConciliada);
        mpeLanzarDetalle.Show();


    }
    protected void grvConciliadas_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType != DataControlRowType.DataRow) return;
        //e.Row.Attributes.Add("onmouseover", "eventosMouse(this, event)");
        //e.Row.Attributes.Add("onmouseout", "eventosMouse(this, event)");
    }
    protected void grvConciliadas_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtSortTable = ViewState["TAB_CONCILIADAS"] as DataTable;

        //(DataTable)HttpContext.Current.Session["TAB_CONCILIADAS"];
        //DataTable dtSortTable = (DataTable)grvPedidos.DataSource;

        if (dtSortTable == null) return;
        string order = getSortDirectionString(e.SortExpression);
        dtSortTable.DefaultView.Sort = e.SortExpression + " " + order;
        //HttpContext.Current.Session["TAB_CONCILIADAS"] = dtSortTable;
        grvConciliadas.DataSource = dtSortTable;
        grvConciliadas.DataBind();
        //LlenaGridViewConciliadas();
    }
    protected void grvExternos_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void grvExternos_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
        {
            e.Row.Attributes.Add("onmouseover", "this.className='bg-color-grisClaro02'");
            e.Row.Attributes.Add("onmouseout", "this.className='bg-color-blanco'");
        }
    }
    protected void grvExternos_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtSortTable = ViewState["TAB_EXTERNOS"] as DataTable;
        //(DataTable)HttpContext.Current.Session["TAB_EXTERNOS"];
        if (dtSortTable == null) return;
        string order = getSortDirectionString(e.SortExpression);
        dtSortTable.DefaultView.Sort = e.SortExpression + " " + order;
        //HttpContext.Current.Session["TAB_EXTERNOS"] = dtSortTable;
        grvExternos.DataSource = dtSortTable;
        grvExternos.DataBind();
    }
    protected void grvExternos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvExternos.PageIndex = e.NewPageIndex;
            LlenaGridViewExternos();
        }
        catch (Exception ex)
        {
            //App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }
    }

    public void GenerarTablaArchivosInternos()//Genera la tabla Referencias a Conciliar de Acchivos Internos
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

        tblReferenciaInternas.Columns.Add("StatusConciliacion", typeof(string));
        tblReferenciaInternas.Columns.Add("UbicacionIcono", typeof(string));


        foreach (ReferenciaNoConciliada rc in listaReferenciaArchivosInternos)
        {
            tblReferenciaInternas.Rows.Add(
                rc.Secuencia,
                rc.Folio,
                rc.Año,
                rc.Sucursal,
                rc.FMovimiento,
                rc.FOperacion,
                rc.Retiro,
                rc.Deposito,
                rc.Referencia,
                rc.Descripcion,
                rc.Monto,
                rc.Concepto,
                rc.RFCTercero,
                rc.StatusConciliacion,
                rc.UbicacionIcono
                );
        }

        HttpContext.Current.Session["TAB_INTERNOS"] = tblReferenciaInternas;
        ViewState["TAB_INTERNOS"] = tblReferenciaInternas;
    }
    private void LlenaGridViewArchivosInternos()//Llena el gridview Referencias Internas
    {
        DataTable tablaReferenciasAI = (DataTable)HttpContext.Current.Session["TAB_INTERNOS"];
        grvInternos.DataSource = tablaReferenciasAI;
        grvInternos.DataBind();

    }
    public void Consulta_ArchivosInternos(int corporativo, int sucursal, int año, short mes, int folio, decimal diferencia, int statusConcepto, int sucursalInterno)
    {
        System.Data.SqlClient.SqlConnection Connection = SeguridadCB.Seguridad.Conexion;
        if (Connection.State == ConnectionState.Closed)
        {
            SeguridadCB.Seguridad.Conexion.Open();
            Connection = SeguridadCB.Seguridad.Conexion;
        }
        try
        {
            listaReferenciaArchivosInternos =
            Conciliacion.RunTime.App.Consultas.ConsultaTrasaccionesInternasPendientes(Consultas.Configuracion.Previo, corporativo, sucursal, año, mes, folio, statusConcepto, sucursalInterno);
            Session["POR_CONCILIAR_INTERNO"] = listaReferenciaArchivosInternos;
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.StackTrace);
        }
    }

    public void Consulta_Pedidos(int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion, int celula)
    {
        System.Data.SqlClient.SqlConnection Connection = SeguridadCB.Seguridad.Conexion;
        if (Connection.State == ConnectionState.Closed)
        {
            SeguridadCB.Seguridad.Conexion.Open();
            Connection = SeguridadCB.Seguridad.Conexion;
        }
        try
        {
            listaReferenciaPedidos = Conciliacion.RunTime.App.Consultas.ConciliacionBusquedaPedido(Consultas.BusquedaPedido.Todos, corporativoconciliacion, sucursalconciliacion, añoconciliacion, mesconciliacion, folioconciliacion, 0, 0, 0, celula, "-1",false);//@ClientePadre=FALSE : Solo mandar los PEDIDOS del Cliente.
            Session["POR_CONCILIAR_INTERNO"] = listaReferenciaPedidos;
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
        tblReferenciaInternas.Columns.Add("Cliente", typeof(string));
        tblReferenciaInternas.Columns.Add("Nombre", typeof(string));
        tblReferenciaInternas.Columns.Add("FSuministro", typeof(DateTime));
        tblReferenciaInternas.Columns.Add("Total", typeof(decimal));
        tblReferenciaInternas.Columns.Add("Concepto", typeof(string));

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
        ViewState["TAB_INTERNOS"] = tblReferenciaInternas;
    }
    private void LlenaGridViewPedidos()//Llena el gridview de Pedidos
    {
        DataTable tablaReferenciasP = (DataTable)HttpContext.Current.Session["TAB_INTERNOS"];
        grvPedidos.DataSource = tablaReferenciasP;
        grvPedidos.DataBind();

    }

    protected void grvInternos_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void grvInternos_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='bg-color-grisClaro02'");
            e.Row.Attributes.Add("onmouseout", "this.className='bg-color-blanco'");
        }
    }
    protected void grvInternos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvInternos.PageIndex = e.NewPageIndex;
            LlenaGridViewArchivosInternos();
        }
        catch (Exception ex)
        {
            // App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }
    }
    protected void grvInternos_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtSortTable = ViewState["TAB_INTERNOS"] as DataTable;
        //(DataTable)HttpContext.Current.Session["TAB_INTERNOS"];
        //DataTable dtSortTable = (DataTable)grvPedidos.DataSource;

        if (dtSortTable == null) return;
        string order = getSortDirectionString(e.SortExpression);
        dtSortTable.DefaultView.Sort = e.SortExpression + " " + order;
        //HttpContext.Current.Session["TAB_INTERNOS"] = dtSortTable;
        //LlenaGridViewArchivosInternos();
        grvInternos.DataSource = dtSortTable;
        grvInternos.DataBind();
    }
    protected void paginasDropDownListInterno_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag = 0;

        if (int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 && iNumPag <= grvInternos.PageCount)
        {
            grvInternos.PageIndex = iNumPag - 1;
        }
        else
        {
            grvInternos.PageIndex = 0;
        }

        LlenaGridViewArchivosInternos();

    }


    protected void grvPedidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvPedidos.PageIndex = e.NewPageIndex;
            LlenaGridViewPedidos();
        }
        catch (Exception ex)
        {
            // App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }
    }
    protected void paginasDropDownListPedidos_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag = 0;

        grvPedidos.PageIndex = int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 && iNumPag <= grvPedidos.PageCount ? iNumPag - 1 : 0;

        LlenaGridViewPedidos();

    }
    protected void grvPedidos_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtSortTable = ViewState["TAB_INTERNOS"] as DataTable;
        if (dtSortTable == null) return;
        string order = getSortDirectionString(e.SortExpression);
        dtSortTable.DefaultView.Sort = e.SortExpression + " " + order;
        grvPedidos.DataSource = dtSortTable;
        grvPedidos.DataBind();
    }
    protected void grvPedidos_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='bg-color-grisClaro02'");
            e.Row.Attributes.Add("onmouseout", "this.className='bg-color-blanco'");
        }
    }

    protected void paginasDropDownListExterno_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList oIraPag = sender as DropDownList;
        int iNumPag = 0;

        grvExternos.PageIndex = int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 &&
                                iNumPag <= grvExternos.PageCount
                                    ? iNumPag - 1
                                    : 0;

        LlenaGridViewExternos();

    }

    protected void ddlStatusConcepto_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Leer info Actual de la Conciliacion
        cargarInfoConciliacionActual();
        switch (ddlStatusEn.SelectedItem.Value)
        {
            case "Externos":
                Consulta_Externos(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value));
                GenerarTablaExternos();
                LlenaGridViewExternos();
                break;
            case "Internos":
                if (tipoConciliacion == 2)
                {
                    Consulta_Pedidos(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, Convert.ToInt32(ddlCelula.SelectedItem.Value));
                    GenerarTablaPedidos();
                    LlenaGridViewPedidos();
                }
                else
                {
                    Consulta_ArchivosInternos(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, Convert.ToDecimal(txtDiferencia.Text), Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value), Convert.ToInt16(ddlSucursal.SelectedItem.Value));
                    GenerarTablaArchivosInternos();
                    LlenaGridViewArchivosInternos();
                }
                break;
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
    protected void btnFiltrar_Click(object sender, ImageClickEventArgs e)
    {
        //Leer el tipoConciliacion URL
        tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);

        cargar_ComboCampoFiltroDestino(tipoConciliacion, ddlFiltrarEn.SelectedItem.Value);
        enlazarComboCampoFiltrarDestino();
        InicializarControlesFiltro();
        mpeFiltrar.Show();
    }
    protected void imgBuscar_Click(object sender, ImageClickEventArgs e)
    {
        txtBuscar.Text = String.Empty;
        mpeBuscar.Show();
    }
    public void InicializarControlesFiltro()
    {
        ddlCampoFiltrar.SelectedIndex = ddlOperacion.SelectedIndex = 0;
        txtValor.Text = String.Empty;
    }
    protected void btnIrBuscar_Click(object sender, EventArgs e)
    {
        //Leer el tipoConciliacion URL
        tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);

        if (ddlBuscarEn.SelectedItem.Value.Equals("Externos"))
        {
            grvExternos.DataSource = ViewState["TAB_EXTERNOS"];
            grvExternos.DataBind();
        }
        else if (ddlBuscarEn.SelectedItem.Value.Equals("Internos"))
        {
            if (tipoConciliacion == 2)
            {
                grvPedidos.DataSource = ViewState["TAB_INTERNOS"];
                grvPedidos.DataBind();
            }
            else
            {
                grvInternos.DataSource = ViewState["TAB_INTERNOS"];
                grvInternos.DataBind();
            }

        }
        else
        {
            grvConciliadas.DataSource = ViewState["TAB_CONCILIADAS"];
            grvConciliadas.DataBind();
        }

        mpeBuscar.Hide();
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
    protected void ddlCampoFiltrar_SelectedIndexChanged(object sender, EventArgs e)
    {
        //activarControles(tipoCampoSeleccionado());
        mpeFiltrar.Show();
    }
    protected void btnIrFiltro_Click(object sender, EventArgs e)
    {
        //Leer el tipoConciliacion URL
        tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);
        cargar_ComboCampoFiltroDestino(tipoConciliacion, ddlFiltrarEn.SelectedItem.Value);

        FiltrarCampo(valorFiltro(tipoCampoSeleccionado()), ddlFiltrarEn.SelectedItem.Value);
        mpeFiltrar.Hide();
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
                ViewState["TAB_EXTERNOS"] = dv.ToTable();
                grvExternos.DataSource = ViewState["TAB_EXTERNOS"] as DataTable;
                grvExternos.DataBind();

            }
            else
            {
                //Leer el tipoConciliacion URL
                tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);

                if (filtroEn.Equals("Internos"))
                {
                    ViewState["TAB_INTERNOS"] = dv.ToTable();
                    if (tipoConciliacion != 2)
                    {
                        grvInternos.DataSource = ViewState["TAB_INTERNOS"] as DataTable;
                        grvInternos.DataBind();
                    }
                    else
                    {
                        grvPedidos.DataSource = ViewState["TAB_INTERNOS"] as DataTable;
                        grvPedidos.DataBind();
                    }

                }
                else
                {
                    ViewState["TAB_CONCILIADAS"] = dv.ToTable();
                    grvConciliadas.DataSource = ViewState["TAB_CONCILIADAS"] as DataTable;
                    grvConciliadas.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.Message);
            mpeFiltrar.Hide();
        }
    }

    protected void OnCheckedChangedConciliados(object sender, EventArgs e)
    {
        CheckBox chk = (sender as CheckBox);
        if (chk.ID == "chkAllConciliados")
            foreach (GridViewRow fila in grvConciliadas.Rows.Cast<GridViewRow>().Where(fila => fila.RowType == DataControlRowType.DataRow))
                fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked = chk.Checked;

        //CheckBox chkAll = (grvConciliadas.HeaderRow.FindControl("chkAllConciliados") as CheckBox);
        //chkAll.Checked = true;

        //foreach (bool estaMarcado in grvConciliadas.Rows.Cast<GridViewRow>().Where(row => row.RowType == DataControlRowType.DataRow).Select(row => row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked).Where(estaMarcado => !estaMarcado))
        //{
        //    chkAll.Checked = false;
        //    break;
        //}
    }
    protected void OnCheckedChangedExternos(object sender, EventArgs e)
    {
        CheckBox chk = (sender as CheckBox);
        if (grvExternos.Rows.Count > 0)
        {

            if (chk.ID == "chkTodosExternos")
                foreach (GridViewRow fila in grvExternos.Rows.Cast<GridViewRow>().Where(fila => fila.RowType == DataControlRowType.DataRow))
                    fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked = chk.Checked;
        }
        else
            chk.Checked = false;
        //CheckBox chkAll = (grvConciliadas.HeaderRow.FindControl("chkAllConciliados") as CheckBox);
        //chkAll.Checked = true;

        //foreach (bool estaMarcado in grvConciliadas.Rows.Cast<GridViewRow>().Where(row => row.RowType == DataControlRowType.DataRow).Select(row => row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked).Where(estaMarcado => !estaMarcado))
        //{
        //    chkAll.Checked = false;
        //    break;
        //}
    }
    protected void OnCheckedChangedInternos(object sender, EventArgs e)
    {
        CheckBox chk = (sender as CheckBox);
        if (grvInternos.Rows.Count > 0)
        {
            if (chk.ID == "chkTodosInternos")
                foreach (GridViewRow fila in grvInternos.Rows.Cast<GridViewRow>().Where(fila => fila.RowType == DataControlRowType.DataRow))
                    fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked = chk.Checked;
        }
        else
            chk.Checked = false;
    }
    protected void OnCheckedChangedPedidos(object sender, EventArgs e)
    {
        CheckBox chk = (sender as CheckBox);
        if (chk.ID == "chkTodosPedidos")
            foreach (GridViewRow fila in grvPedidos.Rows.Cast<GridViewRow>().Where(fila => fila.RowType == DataControlRowType.DataRow))
                fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked = chk.Checked;
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
    protected void ddlStatusEn_SelectedIndexChanged(object sender, EventArgs e)
    {
        consultaExternosInternos();
    }
    protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    public void consultaExternosInternos()
    {

    }
    protected void btnDesconciliar_Click(object sender, EventArgs e)
    {
        if (!lblStatusConciliacion.Text.Equals("CONCILIACION CERRADA"))
        {
            if (existenCheckSeleccionados("Conciliadas"))
            {
                desconciliarTransExternas();
            }
            else
                App.ImplementadorMensajes.MostrarMensaje("NO EXISTEN TRANSACCIONES CONCILIADAS SELECCIONADAS");
        }
        else
            App.ImplementadorMensajes.MostrarMensaje("LA CONCILIACIÓN ESTA CERRADA, NO LA PUEDE MODIFICAR");
    }
    public void desconciliarTransExternas()
    {
        try
        {
            bool resultado = false;
            int corporativoCon, sucursalCon, añoCon, mesCon, folioCon, folio, secuencia;
            string tipo;

            //Leer info Actual de la Conciliacion
            cargarInfoConciliacionActual();

            foreach (GridViewRow gr in grvConciliadas.Rows.Cast<GridViewRow>().Where(fila => fila.RowType == DataControlRowType.DataRow).Where(gr => (gr.FindControl("chkFolio") as CheckBox).Checked))
            {
                //Leer las TransaccionesConciliadas
                listaTransaccionesConciliadas = Session["CONCILIADAS"] as List<ReferenciaNoConciliada>;

                corporativoCon = Convert.ToInt32(grvConciliadas.DataKeys[gr.RowIndex].Values["CorporativoConciliacion"]);
                sucursalCon = Convert.ToInt32(grvConciliadas.DataKeys[gr.RowIndex].Values["SucursalConciliacion"]);
                añoCon = Convert.ToInt32(grvConciliadas.DataKeys[gr.RowIndex].Values["AñoConciliacion"]);
                mesCon = Convert.ToInt32(grvConciliadas.DataKeys[gr.RowIndex].Values["MesConciliacion"]);
                folioCon = Convert.ToInt32(grvConciliadas.DataKeys[gr.RowIndex].Values["FolioConciliacion"]);
                folio = Convert.ToInt32(grvConciliadas.DataKeys[gr.RowIndex].Values["Folio"]);
                secuencia = Convert.ToInt32(grvConciliadas.DataKeys[gr.RowIndex].Values["Secuencia"]);
                tipo = Convert.ToString(grvConciliadas.DataKeys[gr.RowIndex].Values["Tipo"]);

                if (tipo.Equals("CONCILIADO") || tipo.Equals("CANCELADO EXTERNO") || tipo.Equals("CONCILIADO S/REFERENCIA"))
                {

                    resultado = listaTransaccionesConciliadas.Single(
                       x => x.Corporativo == corporativoCon &&
                       x.Sucursal == sucursalCon &&
                       x.Año == añoCon &&
                       x.MesConciliacion == mesCon &&
                       x.FolioConciliacion == folioCon &&
                       x.Folio == folio &&
                       x.Secuencia == secuencia).DesConciliar();
                }
                else
                {
                    resultado = listaTransaccionesConciliadas.Single(
                        x => x.Corporativo == corporativoCon &&
                        x.Sucursal == sucursalCon &&
                        x.Año == añoCon &&
                        x.MesConciliacion == mesCon &&
                        x.FolioConciliacion == folioCon &&
                        x.Folio == folio &&
                        x.Secuencia == secuencia).EliminarReferenciaConciliadaInterno();
                }

            }

            Consulta_TransaccionesConciliadas(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, Convert.ToSByte(ddlCriteriosConciliacion.SelectedItem.Value), tipoConciliacion);
            GenerarTablaConciliados();
            LlenaGridViewConciliadas();
            LlenarBarraEstado();
            //Cargo y refresco nuevamente los archvos externos
            Consulta_Externos(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, Convert.ToInt32(ddlStatusConcepto.SelectedValue));
            GenerarTablaExternos();
            LlenaGridViewExternos();
            if (tipoConciliacion == 2)
            {
                Consulta_Pedidos(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, Convert.ToInt32(ddlCelula.SelectedItem.Value));
                GenerarTablaPedidos();
                LlenaGridViewPedidos();
            }
            else
            {
                Consulta_ArchivosInternos(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, Convert.ToDecimal(txtDiferencia.Text), Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value), Convert.ToInt16(ddlSucursal.SelectedItem.Value));
                GenerarTablaArchivosInternos();
                LlenaGridViewArchivosInternos();
            }

            App.ImplementadorMensajes.MostrarMensaje(resultado
                                                         ? "DESCONCILIADO CORRECTAMENTE"
                                                         : "DESCONCILIADO INCORRECTAMENTE");
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.StackTrace);
        }
    }
    protected void btnAceptarConfirmar_Click(object sender, EventArgs e)
    {

    }
    protected void imgBitacora_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void imgAutomatica_Click(object sender, ImageClickEventArgs e)
    {
        //Leer info Actual de la Conciliacion
        cargarInfoConciliacionActual();
        

        Consulta_TransaccionesConciliadas(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, Convert.ToSByte(ddlCriteriosConciliacion.SelectedItem.Value), tipoConciliacion);
        GenerarTablaConciliados();
        LlenaGridViewConciliadas();
    }
    protected void btnActualizarConfig_Click(object sender, ImageClickEventArgs e)
    {
        //Leer info Actual de la Conciliacion
        cargarInfoConciliacionActual();

        switch (ddlStatusEn.SelectedItem.Value)
        {
            case "Externos":
                Consulta_Externos(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value));
                GenerarTablaExternos();
                LlenaGridViewExternos();
                break;
            case "Internos":
                if (tipoConciliacion == 2)
                {
                    Consulta_Pedidos(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, Convert.ToInt32(ddlCelula.SelectedItem.Value));
                    GenerarTablaPedidos();
                    LlenaGridViewPedidos();
                }
                else
                {
                    Consulta_ArchivosInternos(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, Convert.ToDecimal(txtDiferencia.Text), Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value), Convert.ToInt16(ddlSucursal.SelectedItem.Value));
                    GenerarTablaArchivosInternos();
                    LlenaGridViewArchivosInternos();
                }
                break;
        }
    }
    protected void Nueva_Ventana(string Pagina, string Titulo, int Ancho, int Alto, int X, int Y)
    {
        ScriptManager.RegisterClientScriptBlock(this.upDetalleConciliacion,
                                            upDetalleConciliacion.GetType(),
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
            if (tipoConciliacion == 2)
                strReporte = Server.MapPath("~/") + settings.GetValue("RutaReporteRemanentesConciliacion", typeof(string));
            else
                strReporte = Server.MapPath("~/") + settings.GetValue("RutaReporteConciliacionTesoreria", typeof(string));

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
                Nueva_Ventana("../Reporte/Reporte.aspx", "Carta", 0, 0, 0, 0);
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

    /// <summary>
    /// Verifica si Existe algún CheckBox Selecionado dentro del GridView[Externos, ArchivosInterno o PedidosInternos,]
    /// </summary>
    public bool existenCheckSeleccionados(string grv)
    {
        return grv.Equals("Externos")
                   ? (from GridViewRow un in grvExternos.Rows
                      select un.FindControl("chkFolioExt") as CheckBox).Count(chek => chek.Checked) > 0
                   : grv.Equals("Internos")
                         ? (from GridViewRow un in grvInternos.Rows
                            select un.FindControl("chkSecuenciaInt") as CheckBox).Count(chek => chek.Checked) > 0
                         : (from GridViewRow un in grvConciliadas.Rows
                            select un.FindControl("chkFolio") as CheckBox).Count(chek => chek.Checked) > 0;
    }

    protected void btnAceptarStatusExterno_Click(object sender, EventArgs e)
    {
        try
        {


            if (existenCheckSeleccionados("Externos"))
            {
                int folioEx, secuenciaExt;
                ReferenciaNoConciliada rfExt;
                //Leer info Actual de la Conciliacion
                cargarInfoConciliacionActual();
                listaReferenciaExternas = Session["POR_CONCILIAR_EXTERNO"] as List<ReferenciaNoConciliada>;

                foreach (
                  GridViewRow gr in
                      grvExternos.Rows.Cast<GridViewRow>()
                                    .Where(fila => fila.RowType == DataControlRowType.DataRow)
                                    .Where(gr => (gr.FindControl("chkFolioExt") as CheckBox).Checked))
                {
                    folioEx = Convert.ToInt32(grvExternos.DataKeys[gr.RowIndex].Values["Folio"]);
                    secuenciaExt = Convert.ToInt32(grvExternos.DataKeys[gr.RowIndex].Values["Secuencia"]);

                    rfExt = listaReferenciaExternas.Single(x => x.Folio == folioEx && x.Secuencia == secuenciaExt);
                    rfExt.MotivoNoConciliado = Convert.ToInt32(ddlMotivosNoConciliado.SelectedItem.Value);
                    rfExt.ComentarioNoConciliado = txtComentario.Text;

                    if (tipoConciliacion != 2)
                        rfExt.CancelarExternoInterno();
                    else
                        rfExt.CancelarExternoPedido();
                }
                Consulta_Externos(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value));
                GenerarTablaExternos();
                LlenaGridViewExternos();
                //CARGAR LAS TRANSACCIONES CONCILIADAS POR EL CRITERIO DE AUTOCONCILIACIÓN
                Consulta_TransaccionesConciliadas(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, Convert.ToSByte(ddlCriteriosConciliacion.SelectedItem.Value), tipoConciliacion);
                GenerarTablaConciliados();
                LlenaGridViewConciliadas();

                mpeStatusTransaccion.Hide();
            }
            else
            {
                App.ImplementadorMensajes.MostrarMensaje("NO EXISTEN REFERENCIAS EXTERNAS SELECCIONADAS");
            }
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.StackTrace);
            mpeStatusTransaccion.Hide();
        }
    }

    protected void btnCANCELAREXTERNO_Click(object sender, ImageClickEventArgs e)
    {
        if (!lblStatusConciliacion.Text.Equals("CONCILIACION CERRADA"))
        {
            if (existenCheckSeleccionados("Externos"))
            {
                btnAceptarStatusExterno.Visible = true;
                btnAceptarStatusInterno.Visible = false;
                txtComentario.Text = String.Empty;
                mpeStatusTransaccion.Show();
            }
            else
                App.ImplementadorMensajes.MostrarMensaje("NO HAY TRANSACCIONES SELECCIONADAS");
        }
        else
            App.ImplementadorMensajes.MostrarMensaje("CONCILIACION CERRADA, NO PUEDE CANCELAR TRANSACCIONES EXTERNAS");
    }
    public void ocultarFiltroFechas(int tpConciliacion)
    {
        bool blVisble = tpConciliacion != 2;
        lblFOperacion.Visible =
            txtFOInicio.Visible =
            txtFOTermino.Visible =
            btnRangoFechasFO.Visible =
            rvFOInicio.Visible =
            rvFMTermino.Visible = blVisble;

        lblFMovimiento.Visible =
                    txtFMInicio.Visible =
                    txtFMTermino.Visible =
                    btnRangoFechasFM.Visible =
                    rvFMInicio.Visible =
                    rvFMTermino.Visible = blVisble;

        lblFSuminstro.Visible =
            txtFSInicio.Visible =
            txtFSTermino.Visible =
            btnRangoFechasFS.Visible =
            rvFSInicio.Visible =
            rvFSTermino.Visible = !blVisble;

    }

    protected void btnCANCELARINTERNO_Click(object sender, ImageClickEventArgs e)
    {
        if (!lblStatusConciliacion.Text.Equals("CONCILIACION CERRADA"))
        {
            if (existenCheckSeleccionados("Internos"))
            {
                btnAceptarStatusExterno.Visible = false;
                btnAceptarStatusInterno.Visible = true;
                txtComentario.Text = String.Empty;
                mpeStatusTransaccion.Show();
            }
            else
                App.ImplementadorMensajes.MostrarMensaje("NO HAY TRANSACCIONES SELECCIONADAS");
        }
        else
            App.ImplementadorMensajes.MostrarMensaje("CONCILIACION CERRADA, NO PUEDE CANCELAR TRANSACCIONES INTERNAS");
    }
    protected void btnAceptarStatusInterno_Click(object sender, EventArgs e)
    {
        try
        {
            if (existenCheckSeleccionados("Internos"))
            {
                int folioInt, secuenciaInt;
                ReferenciaNoConciliada rfInt;

                //Leer info Actual de la Conciliacion
                cargarInfoConciliacionActual();

                //Leer Referencias Internas
                listaReferenciaArchivosInternos = Session["POR_CONCILIAR_INTERNO"] as List<ReferenciaNoConciliada>;

                foreach (
                  GridViewRow gr in
                      grvInternos.Rows.Cast<GridViewRow>()
                                    .Where(fila => fila.RowType == DataControlRowType.DataRow)
                                    .Where(gr => (gr.FindControl("chkSecuenciaInt") as CheckBox).Checked))
                {
                    folioInt = Convert.ToInt32(grvInternos.DataKeys[gr.RowIndex].Values["Folio"]);
                    secuenciaInt = Convert.ToInt32(grvInternos.DataKeys[gr.RowIndex].Values["Secuencia"]);

                    rfInt = listaReferenciaArchivosInternos.Single(x => x.Folio == folioInt && x.Secuencia == secuenciaInt);
                    rfInt.MotivoNoConciliado = Convert.ToInt32(ddlMotivosNoConciliado.SelectedItem.Value);
                    rfInt.ComentarioNoConciliado = txtComentario.Text;
                    rfInt.CancelarInterno();
                }
                Consulta_ArchivosInternos(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, Convert.ToDecimal(txtDiferencia.Text), Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value), Convert.ToInt16(ddlSucursal.SelectedItem.Value));
                GenerarTablaArchivosInternos();
                LlenaGridViewArchivosInternos();
                mpeStatusTransaccion.Hide();

                //CARGAR LAS TRANSACCIONES CONCILIADAS POR EL CRITERIO DE AUTOCONCILIACIÓN
                Consulta_TransaccionesConciliadas(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, Convert.ToSByte(ddlCriteriosConciliacion.SelectedItem.Value), tipoConciliacion);
                GenerarTablaConciliados();
                LlenaGridViewConciliadas();
            }
            else
            {
                App.ImplementadorMensajes.MostrarMensaje("NO EXISTEN REFERENCIAS INTERNAS SELECCIONADAS");
                mpeStatusTransaccion.Hide();
            }
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }
    }
    protected void imgCerrarConciliacion_Click(object sender, ImageClickEventArgs e)
    {
        if (lblStatusConciliacion.Text.Equals("CONCILIACION CERRADA"))
        {
            App.ImplementadorMensajes.MostrarMensaje("La conciliación actual esta CERRADA");
            return;
        }
        usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
        //Leer info Actual de la Conciliacion
        cargarInfoConciliacionActual();

        cConciliacion c = App.Consultas.ConsultaConciliacionDetalle(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion);
        if (c.CerrarConciliacion(usuario.IdUsuario))
        {

            App.ImplementadorMensajes.MostrarMensaje("Conciliación CERRADA exitosamente.");
            mpeConfirmarCerrar.Hide();
            limpiarVariablesSession();
            System.Threading.Thread.Sleep(3000);
            Response.Redirect("~/Conciliacion/DetalleConciliacion.aspx?Folio=" + folioConciliacion + "&Corporativo=" + corporativoConciliacion +
                                    "&Sucursal=" + sucursalConciliacion + "&Año=" + añoConciliacion + "&Mes=" +
                                    mesConciliacion + "&TipoConciliacion=" + tipoConciliacion);
        }
        else
        {
            App.ImplementadorMensajes.MostrarMensaje("Error al CERRAR la conciliación.");
            mpeConfirmarCerrar.Hide();
        }
    }
    protected void btnAceptarConfirmarCerrar_Click(object sender, EventArgs e)
    {

    }
    protected void imgCancelarConciliacion_Click(object sender, ImageClickEventArgs e)
    {
        if (lblStatusConciliacion.Text.Equals("CONCILIACION CERRADA"))
        {
            App.ImplementadorMensajes.MostrarMensaje("La conciliación actual esta CERRADA");
            return;
        }
        usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
        //Leer info Actual de la Conciliacion
        cargarInfoConciliacionActual();

        cConciliacion c = App.Consultas.ConsultaConciliacionDetalle(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion);
        if (c.CancelarConciliacion(usuario.IdUsuario))
        {
            App.ImplementadorMensajes.MostrarMensaje("Conciliación CANCELADA exitosamente.");
            mpeConfirmarCancelar.Hide();
            limpiarVariablesSession();
            System.Threading.Thread.Sleep(3000);
            Response.Redirect("~/Inicio.aspx");
        }
        else
        {
            App.ImplementadorMensajes.MostrarMensaje("Errores al CANCELAR la conciliación-");
            mpeConfirmarCancelar.Hide();
        }
    }
    protected void btnAceptarConfirmarCancelar_Click(object sender, EventArgs e)
    {

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
            ViewState["TAB_INTERNOS"] = dv.ToTable();
            grvInternos.DataSource = ViewState["TAB_INTERNOS"] as DataTable;
            grvInternos.DataBind();

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.Message);
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
            ViewState["TAB_INTERNOS"] = dv.ToTable();
            grvInternos.DataSource = ViewState["TAB_INTERNOS"] as DataTable;
            grvInternos.DataBind();

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }
    }
    protected void btnRangoFechasFS_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dt = (DataTable)HttpContext.Current.Session["TAB_INTERNOS"];
            DataView dv = new DataView(dt);

            string SearchExpression = String.Empty;
            if (!(String.IsNullOrEmpty(txtFSInicio.Text) || String.IsNullOrEmpty(txtFSTermino.Text)))
            {
                SearchExpression = string.Format("FSuministro >= '{0}' AND FSuministro <= '{1}'", txtFSInicio.Text, txtFSTermino.Text);
            }
            if (dv.Count <= 0) return;
            dv.RowFilter = SearchExpression;
            ViewState["TAB_INTERNOS"] = dv.ToTable();
            grvPedidos.DataSource = ViewState["TAB_INTERNOS"] as DataTable;
            grvPedidos.DataBind();

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.Message);
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

            cConciliacion conciliacion =
                App.Consultas.ConsultaConciliacionDetalle(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion);
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
                App.Consultas.ConsultaConciliacionDetalle(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion);
            listArchivosInternos.ForEach(x => resultado = conciliacion.AgregarArchivo(x, cConciliacion.Operacion.Edicion));

            if (resultado)
            {
                //ACTUALIZAR GRID INTERNOS
                LlenarBarraEstado();
                Consulta_ArchivosInternos(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, Convert.ToDecimal(txtDiferencia.Text), Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value), Convert.ToInt16(ddlSucursal.SelectedItem.Value));
                GenerarTablaArchivosInternos();
                LlenaGridViewArchivosInternos();

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
        System.Data.SqlClient.SqlConnection Connection = SeguridadCB.Seguridad.Conexion;
        if (Connection.State == ConnectionState.Closed)
        {
            SeguridadCB.Seguridad.Conexion.Open();
            Connection = SeguridadCB.Seguridad.Conexion;
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

    }

    //---FIN MODULO "AGREGAR NUEVO INTERNO"

}