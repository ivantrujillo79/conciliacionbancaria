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

public partial class Conciliacion_FormasConciliar_CopiaDeConciliacion : System.Web.UI.Page
{
    #region "Propiedades Globales"
    private SeguridadCB.Public.Operaciones operaciones;
    private SeguridadCB.Public.Usuario usuario;

    public List<ReferenciaConciliada> listaReferenciaConciliada = new List<ReferenciaConciliada>();
    public List<ReferenciaConciliadaPedido> listaReferenciaConciliadaPedidos = new List<ReferenciaConciliadaPedido>();
    public ReferenciaNoConciliada tranDesconciliar;
    public List<ReferenciaNoConciliada> listaTransaccionesConciliadas = new List<ReferenciaNoConciliada>();
    public DataTable tblDetalleTransaccionConciliada;
    private DataTable tblTransaccionesConciliadas;
    public List<ListaCombo> listCamposDestino = new List<ListaCombo>();
    public int corporativoConciliacion, añoConciliacion, folioConciliacion;
    public short mesConciliacion, sucursalConciliacion, tipoConciliacion, grupoConciliacion;

    private List<ListaCombo> listSucursales = new List<ListaCombo>();
    private List<ListaCombo> listStatusConcepto = new List<ListaCombo>();
    private List<ListaCombo> listFormasConciliacion = new List<ListaCombo>();

    private DatosArchivo datosArchivoInterno;
    private List<ListaCombo> listTipoFuenteInformacionExternoInterno = new List<ListaCombo>();
    public List<ListaCombo> listFoliosInterno = new List<ListaCombo>();
    public List<DatosArchivo> listArchivosInternos = new List<DatosArchivo>();

    private DataTable tblDestinoDetalleInterno;
    private List<DatosArchivoDetalle> listaDestinoDetalleInterno = new List<DatosArchivoDetalle>();
    #endregion


    private DataTable tblReferenciaConciliadaCopiar = new DataTable("ReferenciasConciliadasCopiar");

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
                corporativoConciliacion = Convert.ToInt32(Request.QueryString["Corporativo"]);
                sucursalConciliacion = Convert.ToSByte(Request.QueryString["Sucursal"]);
                añoConciliacion = Convert.ToInt32(Request.QueryString["Año"]);
                folioConciliacion = Convert.ToInt32(Request.QueryString["Folio"]);
                mesConciliacion = Convert.ToSByte(Request.QueryString["Mes"]);
                tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);
                grupoConciliacion = Convert.ToSByte(Request.QueryString["GrupoConciliacion"]);

                CargarRangoDiasDiferenciaGrupo(grupoConciliacion);

                Carga_SucursalCorporativo(corporativoConciliacion);
                Carga_StatusConcepto(Consultas.ConfiguracionStatusConcepto.ConEtiquetas);
                Carga_FormasConciliacion(tipoConciliacion);

                LlenarBarraEstado();
                //CARGAR LAS TRANSACCIONES CONCILIADAS POR EL CRITERIO DE CONCILIACION
                Consulta_TransaccionesConciliadas(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, Convert.ToInt32(ddlCriteriosConciliacion.SelectedValue));
                GenerarTablaConciliados();
                LlenaGridViewConciliadas();

                Consulta_ConciliarArchivosCopiar(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion);
                GenerarTablaReferenciasAConciliar();
                LlenaGridViewReferenciasConciliadas();

                txtDias.Enabled = false;
                txtDiferencia.Enabled = false;
                ddlStatusConcepto.Enabled = false;
                ddlSucursal.Enabled = false;
                btnActualizarConfig.Enabled = false;

                Carga_TipoFuenteInformacionInterno(Consultas.ConfiguracionTipoFuente.TipoFuenteInformacionInterno);
                activarImportacion(tipoConciliacion);
            }
        }

        catch (Exception ex) { App.ImplementadorMensajes.MostrarMensaje(ex.Message); }

    }
    //Cargar Rango DiasMaximo-Minimio-Default
    public void CargarRangoDiasDiferenciaGrupo(short grupoC)
    {
        GrupoConciliacionDiasDiferencia gcd = App.GrupoConciliacionDias(grupoC);
        if (!gcd.CargarDatos())
        {
            App.ImplementadorMensajes.MostrarMensaje("Conflicto al leer Grupo Conciliación");
            return;
        }
        txtDias.Text = Convert.ToString(gcd.DiferenciaDiasDefault);
        txtDiferencia.Text = Convert.ToString(gcd.DiferenciaCentavosDefault);

    }

    //Colocar el DropDown de Criterios de Evaluacion en la Actual
    public void ActualizarCriterioEvaluacion()
    {
        try
        {
            ddlCriteriosConciliacion.SelectedValue = ddlCriteriosConciliacion.Items.FindByText("COPIA DE CONCILIACION").Value;
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
    /// <summary>
    /// Llena el Combo de Sucurales según Corporativo
    /// </summary>
    public void Carga_SucursalCorporativo(int corporativo)
    {
        //try
        //{
        listSucursales = Conciliacion.RunTime.App.Consultas.ConsultaSucursales(Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionIden0.Sin0, corporativo);
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
        ActualizarCriterioEvaluacion();
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
    public void GenerarTablaReferenciasAConciliar()//Genera la tabla de Referencias A Conciliar
    {

        tblReferenciaConciliadaCopiar.Columns.Add("Selecciona", typeof(bool));
        tblReferenciaConciliadaCopiar.Columns.Add("FolioExt", typeof(int));
        tblReferenciaConciliadaCopiar.Columns.Add("SecuenciaExt", typeof(int));
        tblReferenciaConciliadaCopiar.Columns.Add("RFCTerceroExt", typeof(string));
        tblReferenciaConciliadaCopiar.Columns.Add("RetiroExt", typeof(decimal));
        tblReferenciaConciliadaCopiar.Columns.Add("ReferenciaExt", typeof(string));
        tblReferenciaConciliadaCopiar.Columns.Add("NombreTerceroExt", typeof(string));
        tblReferenciaConciliadaCopiar.Columns.Add("DescripcionExt", typeof(string));
        tblReferenciaConciliadaCopiar.Columns.Add("DepositoExt", typeof(decimal));
        tblReferenciaConciliadaCopiar.Columns.Add("ChequeExt", typeof(string));
        tblReferenciaConciliadaCopiar.Columns.Add("FMovimientoExt", typeof(DateTime));
        tblReferenciaConciliadaCopiar.Columns.Add("FOperacionExt", typeof(DateTime));
        tblReferenciaConciliadaCopiar.Columns.Add("MontoConciliadoExt", typeof(decimal));
        tblReferenciaConciliadaCopiar.Columns.Add("ConceptoExt", typeof(string));
        //Campos Internos
        tblReferenciaConciliadaCopiar.Columns.Add("SecuenciaInt", typeof(int));
        tblReferenciaConciliadaCopiar.Columns.Add("FolioInt", typeof(int));
        tblReferenciaConciliadaCopiar.Columns.Add("RFCTerceroInt", typeof(string));
        tblReferenciaConciliadaCopiar.Columns.Add("RetiroInt", typeof(decimal));
        tblReferenciaConciliadaCopiar.Columns.Add("ReferenciaInt", typeof(string));
        tblReferenciaConciliadaCopiar.Columns.Add("NombreTerceroInt", typeof(string));
        tblReferenciaConciliadaCopiar.Columns.Add("DescripcionInt", typeof(string));
        tblReferenciaConciliadaCopiar.Columns.Add("DepositoInt", typeof(decimal));
        tblReferenciaConciliadaCopiar.Columns.Add("ChequeInt", typeof(string));
        tblReferenciaConciliadaCopiar.Columns.Add("FMovimientoInt", typeof(DateTime));
        tblReferenciaConciliadaCopiar.Columns.Add("FOperacionInt", typeof(DateTime));
        tblReferenciaConciliadaCopiar.Columns.Add("MontoInt", typeof(decimal));
        tblReferenciaConciliadaCopiar.Columns.Add("ConceptoInt", typeof(string));
        foreach (ReferenciaConciliada rc in listaReferenciaConciliada)
        {
            tblReferenciaConciliadaCopiar.Rows.Add(
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
                rc.ConceptoInterno);
        }

        HttpContext.Current.Session["TBLCOPIAR"] = tblReferenciaConciliadaCopiar;
        ViewState["TBLCOPIAR"] = tblReferenciaConciliadaCopiar;
    }
    private void LlenaGridViewReferenciasConciliadas()//Llena el gridview con las conciliaciones antes leídas
    {
        DataTable tablaReferenacias = (DataTable)HttpContext.Current.Session["TBLCOPIAR"];
        this.grvCopiar.DataSource = tablaReferenacias;
        this.grvCopiar.DataBind();
    }
    public void Consulta_ConciliarArchivosCopiar(int corporativo, int sucursal, int año, short mes, int folio)
    {
        System.Data.SqlClient.SqlConnection Connection = SeguridadCB.Seguridad.Conexion;
        if (Connection.State == ConnectionState.Closed)
        {
            SeguridadCB.Seguridad.Conexion.Open();
            Connection = SeguridadCB.Seguridad.Conexion;
        }
        try
        {
            listaReferenciaConciliada = Conciliacion.RunTime.App.Consultas.ConsultaConciliarCopiar(corporativo, sucursal, año, mes, folio);
        }
        catch (Exception ex)
        {

            App.ImplementadorMensajes.MostrarMensaje("Erros al consultar la informacion.\n\rClase :" + this.GetType().Name + "\n\r" + "Error :" + ex.StackTrace);
        }

    }

    protected void grvCopiar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager && (grvCopiar.DataSource != null))
        {
            //TRAE EL TOTAL DE PAGINAS
            Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumPaginas");
            _TotalPags.Text = grvCopiar.PageCount.ToString();

            //LLENA LA LISTA CON EL NUMERO DE PAGINAS
            DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
            for (int i = 1; i <= Convert.ToInt32(grvCopiar.PageCount); i++)
            {
                list.Items.Add(i.ToString());
            }
            list.SelectedValue = (grvCopiar.PageIndex + 1).ToString();
        }
    }

    protected void grvCopiar_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvCopiar.PageIndex = e.NewPageIndex;

            LlenaGridViewReferenciasConciliadas();
        }
        catch (Exception)
        {

        }
    }
    protected void paginasDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag = 0;

        if (int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 && iNumPag <= grvCopiar.PageCount)
        {
            grvCopiar.PageIndex = iNumPag - 1;
        }
        else
        {
            grvCopiar.PageIndex = 0;
        }

        // BindGrid();
        LlenaGridViewReferenciasConciliadas();
    }

    protected void btnGuardarCopiar_Click(object sender, EventArgs e)
    {

    }

    protected void btnIrFormaConciliacion_Click(object sender, EventArgs e)
    {

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
        return listCamposDestino[ddlCampoFiltrar.SelectedIndex].Campo1;
    }

    protected void imgFiltrar_Click(object sender, ImageClickEventArgs e)
    {
        cargar_ComboCampoFiltroDestino(tipoConciliacion, ddlFiltrarEn.SelectedItem.Value);
        InicializarControlesFiltro();
        mpeFiltrar.Show();
    }
    protected void imgBuscar_Click(object sender, ImageClickEventArgs e)
    {
        txtBuscar.Text = String.Empty;
        mpeBuscar.Show();
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

        this.ddlCampoFiltrar.DataSource = listCamposDestino;
        this.ddlCampoFiltrar.DataValueField = "Identificador";
        this.ddlCampoFiltrar.DataTextField = "Descripcion";
        this.ddlCampoFiltrar.DataBind();
        this.ddlCampoFiltrar.Dispose();
    }
    //Crea la paginacion para Concilidos
    protected void grvConciliadas_RowDataBound(object sender, GridViewRowEventArgs e)
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
    //Comando para desconciliar
    protected void grvConciliadas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!e.CommandName.Equals("DESCONCILIAR")) return;
        try
        {
            Button imgDesconciliar = e.CommandSource as Button;
            GridViewRow gRowConciliado = (GridViewRow)(imgDesconciliar).Parent.Parent;

            int corporativoC = Convert.ToInt32(grvConciliadas.DataKeys[gRowConciliado.RowIndex].Values["CorporativoConciliacion"]);
            int sucursalC = Convert.ToInt32(grvConciliadas.DataKeys[gRowConciliado.RowIndex].Values["SucursalConciliacion"]);
            int añoC = Convert.ToInt32(grvConciliadas.DataKeys[gRowConciliado.RowIndex].Values["AñoConciliacion"]);
            int mesC = Convert.ToInt32(grvConciliadas.DataKeys[gRowConciliado.RowIndex].Values["MesConciliacion"]);
            int folioC = Convert.ToInt32(grvConciliadas.DataKeys[gRowConciliado.RowIndex].Values["FolioConciliacion"]);
            int folioEx = Convert.ToInt32(grvConciliadas.DataKeys[gRowConciliado.RowIndex].Values["FolioExt"]);
            int secuenciaEx = Convert.ToInt32(grvConciliadas.DataKeys[gRowConciliado.RowIndex].Values["Secuencia"]);

            tranDesconciliar = listaTransaccionesConciliadas.Single(
                    x => x.Corporativo == corporativoC &&
                    x.Sucursal == sucursalC &&
                    x.Año == añoC &&
                    x.MesConciliacion == mesC &&
                    x.FolioConciliacion == folioC &&
                    x.Folio == folioEx &&
                    x.Secuencia == secuenciaEx);

            tranDesconciliar.DesConciliar();
            Consulta_TransaccionesConciliadas(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, Convert.ToInt32(ddlCriteriosConciliacion.SelectedValue));
            GenerarTablaConciliados();
            LlenaGridViewConciliadas();
            LlenarBarraEstado();
            //Cargo de nuevo las ReferenciasCantidadConcuerda
            Consulta_ConciliarArchivosCopiar(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion);
            GenerarTablaReferenciasAConciliar();
            LlenaGridViewReferenciasConciliadas();
        }
        catch (Exception ex) { App.ImplementadorMensajes.MostrarMensaje(ex.Message); }

    }
    //Aceptar el Desconciliado de una Transaccion Conciliada
    protected void btnAceptarConfirmarDesconciliar_Click(object sender, EventArgs e)
    {

    }
    //Ver el detalle de la Transaccion Conciliada
    protected void grvConciliadas_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int corporativoConcilacion = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["CorporativoConciliacion"]);
        short sucursalConciliacion = Convert.ToSByte(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["SucursalConciliacion"]);
        int añoConciliacion = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["AñoConciliacion"]);
        short mesConciliacion = Convert.ToSByte(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["MesConciliacion"]);
        int folioConciliacion = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["FolioConciliacion"]);
        int folioExterno = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["FolioExt"]);
        int secuenciaExterno = Convert.ToInt32(grvConciliadas.DataKeys[e.NewSelectedIndex].Values["Secuencia"]);

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

        if (int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 && iNumPag <= grvConciliadas.PageCount)
        {
            grvConciliadas.PageIndex = iNumPag - 1;
        }
        else
        {
            grvConciliadas.PageIndex = 0;
        }
        LlenaGridViewConciliadas();
    }
    protected void ddlCampoFiltrar_SelectedIndexChanged(object sender, EventArgs e)
    {
        //activarControles(tipoCampoSeleccionado());
        //mpeFiltrar.Show();
    }
    protected void btnIrFiltro_Click(object sender, EventArgs e)
    {
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
                               : (DataTable)HttpContext.Current.Session["TBLCOPIAR"];

            DataView dv = new DataView(dt);
            string SearchExpression = String.Empty;
            if (!String.IsNullOrEmpty(valorFiltro))
            {
                SearchExpression = string.Format(
                    ddlOperacion.SelectedItem.Value == "LIKE"
                        ? !filtroEn.Equals("Conciliados")
                            ? filtroEn.Equals("Externos")
                                ? "{0}Ext {1} '%{2}%'"
                                : "{0}Int {1} '%{2}%'"
                            : "{0} {1} '%{2}%'"
                         : !filtroEn.Equals("Conciliados")
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
                //LlenaGridViewExternos();
            }
            else
            {
                ViewState["TBLCOPIAR"] = dv.ToTable();
                grvCopiar.DataSource = ViewState["TBLCOPIAR"] as DataTable;
                grvCopiar.DataBind();

            }
            mpeFiltrar.Hide();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.StackTrace);
            mpeFiltrar.Hide();
        }

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
            grvCopiar.DataSource = ViewState["TBLCOPIAR"] as DataTable;
            grvCopiar.DataBind();
        }

        mpeBuscar.Hide();
    }
    protected void grvConciliadas_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "this.className='bg-color-grisClaro02'");
        e.Row.Attributes.Add("onmouseout", "this.className='bg-color-blanco'");
    }
    //Consulta transacciones conciliadas
    public void Consulta_TransaccionesConciliadas(int corporativoconciliacion, short sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion, int formaconciliacion)
    {
        System.Data.SqlClient.SqlConnection Connection = SeguridadCB.Seguridad.Conexion;
        if (Connection.State == ConnectionState.Closed)
        {
            SeguridadCB.Seguridad.Conexion.Open();
            Connection = SeguridadCB.Seguridad.Conexion;
        }
        try
        {
            listaTransaccionesConciliadas = Conciliacion.RunTime.App.Consultas.ConsultaTransaccionesConciliadas(corporativoconciliacion, sucursalconciliacion, añoconciliacion, mesconciliacion, folioconciliacion, Convert.ToInt16(ddlCriteriosConciliacion.SelectedValue));
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
        tblTransaccionesConciliadas.Columns.Add("FolioExt", typeof(int));
        tblTransaccionesConciliadas.Columns.Add("FolioConciliacion", typeof(int));
        tblTransaccionesConciliadas.Columns.Add("CorporativoConciliacion", typeof(int));
        tblTransaccionesConciliadas.Columns.Add("SucursalConciliacion", typeof(short));
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
                rc.Concepto);
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
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }
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
    protected void grvCopiar_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='bg-color-grisClaro02'");
            e.Row.Attributes.Add("onmouseout", "this.className='bg-color-blanco'");
        }

    }
    protected void grvCopiar_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtSortTable = ViewState["TBLCOPIAR"] as DataTable;
        //DataTable dtSortTable = (DataTable)grvPedidos.DataSource;

        if (dtSortTable == null) return;
        string order = getSortDirectionString(e.SortExpression);
        dtSortTable.DefaultView.Sort = e.SortExpression + " " + order;
        //HttpContext.Current.Session["TAB_REF_CONCILIAR"] = dtSortTable;
        grvCopiar.DataSource = dtSortTable;
        grvCopiar.DataBind();
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
    protected void imgAutomatica_Click(object sender, ImageClickEventArgs e)
    {
        string criterioConciliacion = ddlCriteriosConciliacion.SelectedItem.Text.Equals("CANTIDAD CONCUERDA")
            //? "CantidadConcuerda" : ddlCriteriosConciliacion.SelectedItem.Text.Equals("CANTIDAD Y REFERENCIA CONCUERDAN") ? "CantidadYReferenciaConcuerdan" :
            ? "CantidadConcuerda"
            : ddlCriteriosConciliacion.SelectedItem.Text.Equals("CANTIDAD Y REFERENCIA CONCUERDAN")
                ? "CantidadYReferenciaConcuerdanEdificios"
                : ddlCriteriosConciliacion.SelectedItem.Text.Equals("CANTIDAD Y REFERENCIA CONCUERDAN PEDIDOS")
                    ? "CantidadYReferenciaConcuerdan"
                    : ddlCriteriosConciliacion.SelectedItem.Text.Equals("UNO A VARIOS")
                        ? "UnoAVarios"
                        : ddlCriteriosConciliacion.SelectedItem.Text.Equals("VARIOS A UNO")
                            ? "VariosAUno"
                            : ddlCriteriosConciliacion.SelectedItem.Text.Equals("COPIA DE CONCILIACION")
                                ? "CopiaDeConciliacion"
                                : "Manual";
        //Eliminar las variables de Session utilizadas en la Vista
        HttpContext.Current.Session["CONCILIADAS"] =
        HttpContext.Current.Session["POR_CONCILIAR"] = null;
        HttpContext.Current.Session.Remove("CONCILIADAS");
        HttpContext.Current.Session.Remove("POR_CONCILIAR");
        Response.Redirect("~/Conciliacion/FormasConciliar/" + criterioConciliacion +
                                              ".aspx?Folio=" + folioConciliacion + "&Corporativo=" + corporativoConciliacion +
                                              "&Sucursal=" + sucursalConciliacion + "&Año=" + añoConciliacion + "&Mes=" +
                                              mesConciliacion + "&TipoConciliacion=" + tipoConciliacion);
    }
    protected void btnActualizarConfig_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        bool resultado = false;
        if (grvCopiar.Rows.Count > 0)
        {
            foreach (GridViewRow un in grvCopiar.Rows)
            {
                resultado = listaReferenciaConciliada[un.RowIndex].Guardar();
            }
            App.ImplementadorMensajes.MostrarMensaje(resultado ? "Guardado Exitosamente" : "Error al guardar");
        }
        else
        {
            App.ImplementadorMensajes.MostrarMensaje("No existe ninguna referencia a conciliar. Verifique");
        }
        LlenarBarraEstado();
        Consulta_TransaccionesConciliadas(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion, Convert.ToInt32(ddlCriteriosConciliacion.SelectedValue));
        GenerarTablaConciliados();
        LlenaGridViewConciliadas();
        //Cargo de nuevo las ReferenciasCantidadConcuerda
        Consulta_ConciliarArchivosCopiar(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion);
        GenerarTablaReferenciasAConciliar();
        LlenaGridViewReferenciasConciliadas();
    }
    protected void Nueva_Ventana(string Pagina, string Titulo, int Ancho, int Alto, int X, int Y)
    {

        ScriptManager.RegisterClientScriptBlock(this.upCopiaConciliacion,
                                            upCopiaConciliacion.GetType(),
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

            string strReporte = Server.MapPath("~/") + settings.GetValue("RutaReporteConciliacionTesoreria", typeof(string));
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
        cConciliacion c = App.Consultas.ConsultaConciliacionDetalle(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion);
        if (c.CerrarConciliacion(usuario.IdUsuario))
        {
            // LlenarBarraEstado();
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
    protected void btnAceptarConfirmarCerrar_Click(object sender, EventArgs e)
    {

    }
    protected void imgCancelarConciliacion_Click(object sender, ImageClickEventArgs e)
    {
        usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
        cConciliacion c = App.Consultas.ConsultaConciliacionDetalle(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion);
        if (c.CancelarConciliacion(usuario.IdUsuario))
        {
            // LlenarBarraEstado();  
            App.ImplementadorMensajes.MostrarMensaje("CONCILIACIÓN CANCELADA EXITOSAMENTE");
            System.Threading.Thread.Sleep(3000);
            Response.Redirect("~/Inicio.aspx");
        }
        else
        {
            App.ImplementadorMensajes.MostrarMensaje("ERRORES AL CANCELAR LA CONCILIACIÓN");
        }
    }
    protected void btnAceptarConfirmarCancelar_Click(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Llena el Combo de Tipo Fuente Informacion Externo e Interno
    /// </summary>
    public void Carga_TipoFuenteInformacionInterno(Consultas.ConfiguracionTipoFuente tipo)
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
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.StackTrace);
        }
    }

    protected void ddlTipoFuenteInfoInterno_SelectedIndexChanged(object sender, EventArgs e)
    {
        Carga_FoliosInternos(
                       corporativoConciliacion,
                       Convert.ToInt32(ddlSucursalInterno.SelectedItem.Value),
                       añoConciliacion,
                       mesConciliacion,
                       lblCuenta.Text,
                       Convert.ToSByte(ddlTipoFuenteInfoInterno.SelectedItem.Value)
                       );


    }
    protected void ddlSucursalInterno_SelectedIndexChanged(object sender, EventArgs e)
    {
        Carga_FoliosInternos(
                              corporativoConciliacion,
                              Convert.ToInt32(ddlSucursalInterno.SelectedItem.Value),
                              añoConciliacion,
                              mesConciliacion,
                              lblCuenta.Text,
                              Convert.ToSByte(ddlTipoFuenteInfoInterno.SelectedItem.Value)
                              );
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
            this.ddlFolioInterno.DataSource = listFoliosInterno;
            this.ddlFolioInterno.DataValueField = "Identificador";
            this.ddlFolioInterno.DataTextField = "Descripcion";
            this.ddlFolioInterno.DataBind();
            this.ddlFolioInterno.Dispose();

        }
        catch
        {
        }
    }
    protected void ddlFolioInterno_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.lblStatusFolioInterno.Text = listFoliosInterno[ddlFolioInterno.SelectedIndex].Campo1;
        this.lblUsuarioAltaEx.Text = listFoliosInterno[ddlFolioInterno.SelectedIndex].Campo3;

    }
    protected void ddlFolioInterno_DataBound(object sender, EventArgs e)
    {
        if (ddlFolioInterno.Items.Count <= 0)
        {
            lblUsuarioAltaEx.Text = lblStatusFolioInterno.Text = String.Empty;
            return;
        }
        this.lblStatusFolioInterno.Text = listFoliosInterno[ddlFolioInterno.SelectedIndex].Campo1;
        this.lblUsuarioAltaEx.Text = listFoliosInterno[ddlFolioInterno.SelectedIndex].Campo3;
    }
    protected void btnAñadirFolio_Click(object sender, ImageClickEventArgs e)
    {
        if (listArchivosInternos.Exists(x => x.Folio == Convert.ToInt32(ddlFolioInterno.SelectedItem.Value)))
        {
            App.ImplementadorMensajes.MostrarMensaje("Este Folio Interno ya esta Agregado");
        }
        else
        {
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

            LlenaGridViewFoliosAgregados();
        }


    }
    /// <summary>
    /// Llena el GridView de Folios Internos Agregados
    /// </summary>
    private void LlenaGridViewFoliosAgregados()
    {
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
        if (listArchivosInternos.Count > 0)
        {
            cConciliacion conciliacion = App.Consultas.ConsultaConciliacionDetalle(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion);
            listArchivosInternos.ForEach(x => resultado = conciliacion.AgregarArchivo(x, cConciliacion.Operacion.Edicion));

            if (resultado)
            {
                //ACTUALIZAR GRID CANTIDAD CONCUERDA
                LlenarBarraEstado();
                Consulta_ConciliarArchivosCopiar(corporativoConciliacion, sucursalConciliacion, añoConciliacion, mesConciliacion, folioConciliacion);
                GenerarTablaReferenciasAConciliar();
                LlenaGridViewReferenciasConciliadas();
                App.ImplementadorMensajes.MostrarMensaje("Agregado de Folios Internos exitoso.");
            }
            else
                App.ImplementadorMensajes.MostrarMensaje("Ocurrieron problemas al agregar el nuevo Folio");
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
        int FolioInterno = Convert.ToInt32(grvAgregados.DataKeys[e.RowIndex].Value);
        listArchivosInternos.RemoveAll(x => x.Folio == FolioInterno);
        LlenaGridViewFoliosAgregados();
    }
    protected void grvAgregados_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try { grvAgregados.PageIndex = e.NewPageIndex; LlenaGridViewFoliosAgregados(); }
        catch (Exception ex) { }
    }
    protected void ddlTipoFuenteInfoInterno_DataBound(object sender, EventArgs e)
    {
        Carga_FoliosInternos(
                       corporativoConciliacion,
                       Convert.ToInt32(ddlSucursalInterno.SelectedItem.Value),
                       añoConciliacion,
                       mesConciliacion,
                       lblCuenta.Text,
                       Convert.ToSByte(ddlTipoFuenteInfoInterno.SelectedItem.Value)
                       );
    }

    protected void imgImportar_Click(object sender, ImageClickEventArgs e)
    {
        limpiarVistaImportarInterno();
        LlenaGridViewFoliosAgregados();
        popUpImportarArchivos.Show();
    }
    public void limpiarVistaImportarInterno()
    {
        listArchivosInternos.Clear();
        ddlTipoFuenteInfoInterno.SelectedIndex = ddlSucursalInterno.SelectedIndex = 0;
    }

    /// <summary>
    ///Consulta el detalle del Folio Interno
    /// </summary>
    public void Consulta_TablaDestinoDetalleInterno(Consultas.Configuracion configuracion, int empresa, short sucursal, int año, int folioInterno)
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
        Consulta_TablaDestinoDetalleInterno(Consultas.Configuracion.Previo,
                                            corporativoConciliacion,
                                            Convert.ToSByte(ddlSucursalInterno.SelectedItem.Value),
                                            añoConciliacion, Convert.ToInt32(ddlFolioInterno.SelectedItem.Value));
        GenerarTablaDestinoDetalleInterno();
        LlenaGridViewDestinoDetalleInterno();
        lblFolioInterno.Text = ddlFolioInterno.SelectedItem.Value;
        grvVistaRapidaInterno_ModalPopupExtender.Show();

    }
}