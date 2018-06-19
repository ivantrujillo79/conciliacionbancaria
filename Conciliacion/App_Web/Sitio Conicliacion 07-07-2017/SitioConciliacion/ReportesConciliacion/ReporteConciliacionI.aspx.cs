using System;
using System.Activities;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using System.Windows.Forms.VisualStyles;
using CatalogoConciliacion.ReglasNegocio;
using Conciliacion.Migracion.Runtime.ReglasNegocio;
using Conciliacion.RunTime.DatosSQL;
using Conciliacion.RunTime.ReglasDeNegocio;
using System.Data.SqlTypes;

using SeguridadCB.Public;

using System.Collections.Generic;
using System.Diagnostics;
using Conciliacion.Migracion.Runtime;
using System.Web.UI;
using Consultas = Conciliacion.RunTime.ReglasDeNegocio.Consultas;
using Image = System.Web.UI.WebControls.Image;


public partial class ReportesConciliacion_ReporteConciliacionI : System.Web.UI.Page
{
    #region "Propiedades Globales"

    private SeguridadCB.Public.Operaciones operaciones;
    private SeguridadCB.Public.Usuario usuario;

    #endregion

    #region "Propiedades privadas"

    private StringBuilder mensaje;

    private List<ListaCombo> listSucursales = new List<ListaCombo>();
    private List<ReferenciaNoConciliada> listMovimientos = new List<ReferenciaNoConciliada>();
    private List<ReferenciaConciliadaCompartida> listMovimientosConciliadosEx = new List<ReferenciaConciliadaCompartida>();
    private List<ReferenciaNoConciliadaPedido> listaReferenciaPedidos = new List<ReferenciaNoConciliadaPedido>();
    private List<ReferenciaNoConciliadaPedido> listaReferenciaFacturaConsulta = new List<ReferenciaNoConciliadaPedido>();
    private DataTable dtbMovimientoConciliacionCompartida;
    private DataTable dtbMovimientoConciliadosMovEx;
    private DataTable tblPedidos;
    private ReferenciaConciliadaCompartida movSeleccionado;
    public List<ListaCombo> listCamposDestino = new List<ListaCombo>();
    private List<StatusConcepto> listStatusConcepto = new List<StatusConcepto>();//RRV quita private string StatusConciliacionSel = "";
    private List<Cliente> lstClientesCRM = new List<Cliente>();
    #endregion

    protected override void OnPreInit(EventArgs e)
    {
        if (HttpContext.Current.Session["Operaciones"] == null)
            Response.Redirect("~/Acceso/Acceso.aspx", true);
        else
            operaciones = (SeguridadCB.Public.Operaciones)HttpContext.Current.Session["Operaciones"];
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*      Registrar PostBackControl en la página para 
             *      arreglar bug de FileUpload Control dentro de Update Panel    */
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(wucCargaExcelCyC.FindControl("btnSubirArchivo"));
            
            Conciliacion.RunTime.App.ImplementadorMensajes.ContenedorActual = this;
            Conciliacion.Migracion.Runtime.App.ImplementadorMensajes.ContenedorActual = this;

            if (HttpContext.Current.Request.UrlReferrer != null)
            {
                if ((!HttpContext.Current.Request.UrlReferrer.AbsoluteUri.Contains("SitioConciliacion")) ||
                    (HttpContext.Current.Request.UrlReferrer.AbsoluteUri.Contains("Inicio.aspx")))
                {
                    HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
                    HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
                    Response.Cache.SetExpires(DateTime.Now);
                }
            }

            if (Convert.ToDecimal(hdfMontoResto.Value) > 0)
            {
                lblMontoResto.Text = Convert.ToDecimal(hdfMontoResto.Value).ToString("C2");
            }
            wucRangoFechas.GridViewFiltrar = grvPedidos;
            wucRangoFechas.CampoFiltrar = "FSuministro";
            wucRangoFechas.TextoDespliega = "Fecha Factura:";
            wucRangoFechas.VarSesionGridNombre = "TAB_PEDIDOS";

            CargarConfiguracion_wucCargaExcel();
            if (!Page.IsPostBack)
            {
                usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
                //Inicializacion de Propiedades
                //listaConciliaciones = new List<cConciliacion>();

                if (ddlEmpresa.Items.Count == 0) Carga_Corporativo();
                
                GrupoConciliacionUsuario gcu = LeerGrupoConciliacionUsuarioEspecifico(usuario.IdUsuario.Trim());
                Carga_StatusConcepto(gcu.GrupoConciliacionId);
                this.ddlEmpresa.Focus();
                HttpContext.Current.Session["MOVIMIENTOS_AUX"] = null;
            }
            else
            {
                MostrarPopUp_ConciliarPedido();
                RecuperarReferencias_wucCargaExcel();
            }
        }

        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error: Cargar la Pagina\n" + ex.Message);
        }
    }

    /// <summary>
    /// Recupera las referencias procesadas en el
    /// web user control "wucCargaExcelCyC"
    /// </summary>
    private bool RecuperarReferencias_wucCargaExcel()
    {
        //if (wucCargaExcelCyC.ReferenciasPorConciliarPedidoExcel.Count > 0)
        if (wucCargaExcelCyC.ReferenciasPorConciliarExcel.Count > 0)
        {
            listaReferenciaPedidos = ConvertirListaConciliadaAPedido(wucCargaExcelCyC.ReferenciasPorConciliarExcel);
            Session["PEDIDOS_CONCILIAR"] = listaReferenciaPedidos;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Convierte una lista de tipo ReferenciaNoConciliada a 
    /// ReferenciaNoConciliadaPedido
    /// </summary>
    private List<ReferenciaNoConciliadaPedido> ConvertirListaConciliadaAPedido(List<ReferenciaNoConciliada> Referencia)
    {
        List<ReferenciaNoConciliadaPedido> ListaReferenciaPedido = new List<ReferenciaNoConciliadaPedido>();
        ReferenciaNoConciliadaPedido ReferenciaPedido;
        try
        {
            foreach (ReferenciaNoConciliada Ref in Referencia)
            {
                ReferenciaPedido = Conciliacion.RunTime.App.ReferenciaNoConciliadaPedido.CrearObjeto();
                ReferenciaPedido.Corporativo = Ref.Corporativo;
                ReferenciaPedido.Sucursal = Ref.Sucursal;
                ReferenciaPedido.SucursalDes = Ref.SucursalDes;
                ReferenciaPedido.Año = Ref.Año;
                ReferenciaPedido.FolioConciliacion = Ref.FolioConciliacion;
                ReferenciaPedido.MesConciliacion = Ref.MesConciliacion;
                ReferenciaPedido.Concepto = Ref.Concepto;
                ReferenciaPedido.Total = Ref.Monto; /*   Total   */
                ReferenciaPedido.FolioConciliacion = Ref.FormaConciliacion;
                ReferenciaPedido.StatusConcepto = Ref.StatusConcepto;
                ReferenciaPedido.StatusConciliacion = Ref.StatusConciliacion;
                ReferenciaPedido.FOperacion = Ref.FOperacion;
                ReferenciaPedido.FMovimiento = Ref.FMovimiento;
                ReferenciaPedido.Diferencia = Ref.Diferencia;
                //ReferenciaPedido.ImplementadorMensajes = App.ImplementadorMensajes.ContenedorActual();

                ListaReferenciaPedido.Add(ReferenciaPedido);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", 
                "alertify.alert('Conciliaci&oacute;n bancaria','Error: " + ex.Message + 
                "', function(){ alertify.error('Error en la solicitud'); });", true);
        }
        return ListaReferenciaPedido;
    }

    /// <summary>
    /// Método para asignar propiedades del web user control
    /// "wucCargaExcelCyC"
    /// </summary>
    private void CargarConfiguracion_wucCargaExcel()
    {
        wucCargaExcelCyC.PopupContenedor = popUpConciliarMovPedido;
    }

    /// <summary>
    /// Metodo para mostrar de nuevo el ModalPopUp "popUpConciliarMovPedido"
    /// despues de que se cierra con el PostBack 
    /// </summary>
    private void MostrarPopUp_ConciliarPedido()
    { 
        if (hfVisibleConciliar.Value == "1")
        {
            popUpConciliarMovPedido.Show();
            popUpConciliarMovPedido.Focus();
        }
    }

    /// <summary>
    /// Este Metodo is usado para enlazar datos con el DropDowList del Encabezado
    /// del Grid de ConciliacionCompartida
    /// </summary>
    public void EnlazarDatosDDLStatus(DropDownList ddlStatus)
    {
        try
        {
            List<ListaCombo> status = Conciliacion.RunTime.App.Consultas.ConsultaStatusConciliacion();
            ddlStatus.DataValueField = "Descripcion";
            ddlStatus.DataTextField = "Descripcion";
            ddlStatus.DataSource = status;
            ddlStatus.DataBind();
            //EnlazarImagenes(ddlStatus, status);
            string valorStatusConciliacion = String.IsNullOrEmpty(hfStatusConciliacionFD.Value)
                       ? "TODOS"
                       : hfStatusConciliacionFD.Value;
            ddlStatus.SelectedValue = valorStatusConciliacion;
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    protected void ddlStatusMovimiento_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlStatus = sender as DropDownList;
            string status = ddlStatus.SelectedItem.Text;
            hdfStatusConciliacionSel.Value = status;//RRV
            FiltrarStatusConciliacionMovimiento(status);
        }
        catch (Exception ex)
        {
            App.implementadorMensajes.MostrarMensaje("Error al leer el valor de filtro. Error:\n" + ex.Message);
        }
    }

    protected void ddlStatusConcepto_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            short statusconceptofiltro = Convert.ToSByte((sender as DropDownList).SelectedItem.Value);
            hfStatusConceptoFiltro.Value = statusconceptofiltro.ToString();
            listMovimientos = Session["MOVIMIENTOS"] as List<ReferenciaNoConciliada>;
            if (statusconceptofiltro != -1)
                listMovimientos =
                    listMovimientos.Where(
                        x => x.ListaReferenciaConciliadaCompartida.Any(y => y.StatusConcepto == statusconceptofiltro)).ToList();
            GenerarTablaConciliacionCompartida(listMovimientos);
            grvConciliacionCompartida.DataSource = Session["MOVIMIENTOS_AUX"] as DataTable;
            grvConciliacionCompartida.DataBind();

        }
        catch (Exception ex)
        {
            App.implementadorMensajes.MostrarMensaje("Error al leer el valor de filtro. Error:\n" + ex.Message);
            hfStatusConceptoFiltro.Value = (100).ToString();
        }
    }

    /// <summary>
    /// Este Metodo is usado para enlazar los imagenes de cada elemento
    /// </summary>
    protected void EnlazarImagenes(DropDownList ddlStatus, List<ListaCombo> status)
    {
        try
        {
            if (ddlStatus == null) return;
            foreach (ListItem li in ddlStatus.Items)
            {
                li.Attributes["title"] = status.Find(x => x.Identificador == Convert.ToInt16(li.Value)).Campo1;
            }
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Ha ocurrido un error al enlazar las imagenes de los status. Error:\n" + ex.Message);
        }
    }
    private bool FiltroCorrecto()
    {
        bool resultado = true;
        mensaje = new StringBuilder();
        if (ddlEmpresa.Equals(null) || ddlEmpresa.Items.Count == 0)
        {
            mensaje.Append("Corporativo");
            resultado = false;
        }
        else if (ddlSucursal.Equals(null) || ddlSucursal.Items.Count == 0)
        {
            mensaje.Append("Sucursal");
            resultado = false;
        }
        else if (ddlBanco.Equals(null) || ddlBanco.Items.Count == 0)
        {
            mensaje.Append("Banco");
            resultado = false;
        }
        else if (ddlCuentaBancaria.Equals(null) || ddlCuentaBancaria.Items.Count == 0)
        {
            mensaje.Append("Cuenta Bancaria");
            resultado = false;
        }
        else if (String.IsNullOrEmpty(txtFInicial.Text))
        {
            mensaje.Append("Fecha Inicial");
            resultado = false;
        }
        else if (String.IsNullOrEmpty(txtFFinal.Text))
        {
            mensaje.Append("Fecha Final");
            resultado = false;
        }
        return resultado;
    }

    protected void ddlEmpresa_DataBound(object sender, EventArgs e)
    {
        if (ddlEmpresa.Items.Count > 0)
        {
            Carga_SucursalCorporativo(Convert.ToInt32(ddlEmpresa.SelectedItem.Value));
            Carga_Banco(Convert.ToInt32(ddlEmpresa.SelectedItem.Value));
        }
        else
        {
            ddlEmpresa.Items.Clear();
            ddlEmpresa.DataBind();
        }

    }

    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmpresa.Items.Count > 0)
        {
            Carga_SucursalCorporativo(Convert.ToInt32(ddlEmpresa.SelectedItem.Value));
            Carga_Banco(Convert.ToInt32(ddlEmpresa.SelectedItem.Value));
        }
        else
        {
            ddlEmpresa.Items.Clear();
            ddlEmpresa.DataBind();
        }
    }


    /// <summary>
    /// Llena el Combo de las Empresas por Usuario
    /// </summary>
    public void Carga_Corporativo()
    {
        try
        {
            DataTable dtEmpresas = new DataTable();
            Usuario usuario;
            usuario = (Usuario)HttpContext.Current.Session["Usuario"];
            dtEmpresas = usuario.CorporativoAcceso;
            this.ddlEmpresa.DataSource = dtEmpresas;
            this.ddlEmpresa.DataValueField = "Corporativo";
            this.ddlEmpresa.DataTextField = "NombreCorporativo";
            this.ddlEmpresa.DataBind();
            dtEmpresas.Dispose();

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    /// <summary>
    /// Llena el Combo de Sucurales según Corporativo
    /// </summary>
    public void Carga_SucursalCorporativo(int corporativo)
    {
        try
        {
            listSucursales =
                Conciliacion.RunTime.App.Consultas.ConsultaSucursales(
                    Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionIden0.Sin0, corporativo);
            this.ddlSucursal.DataSource = listSucursales;
            this.ddlSucursal.DataValueField = "Identificador";
            this.ddlSucursal.DataTextField = "Descripcion";
            this.ddlSucursal.DataBind();
            this.ddlSucursal.Dispose();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    /// <summary>
    /// Llena el Combo de Sucurales según Corporativo
    /// </summary>
    public void Carga_Banco(int corporativo)
    {
        try
        {
            ddlBanco.DataValueField = "Identificador";
            ddlBanco.DataTextField = "Descripcion";
            ddlBanco.DataSource = Conciliacion.RunTime.App.Consultas.ConsultaBancos(corporativo);
            ddlBanco.DataBind();
            //ddlBanco.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    /// <summary>
    /// Llena el Combo de Sucurales según Corporativo
    /// </summary>
    public void Carga_CuentaBancaria(int corporativo, int banco)
    {
        try
        {
            ddlCuentaBancaria.DataValueField = "Descripcion";
            ddlCuentaBancaria.DataTextField = "Descripcion";
            ddlCuentaBancaria.DataSource = App.Consultas.ObtieneListaCuentaFinancieroPorBanco(corporativo, banco);
            ddlCuentaBancaria.DataBind();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }
    /// <summary>
    /// Consulta campos de Filtro 
    /// </summary>
    public void cargar_ComboCampoFiltroDestino(short configuracion)
    {
        listCamposDestino = Conciliacion.RunTime.App.Consultas.ConsultaDestinoCompartidoExternoInterno(configuracion);

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
    public void InicializarControlesFiltro()
    {
        ddlCampoFiltrar.SelectedIndex = ddlOperacion.SelectedIndex = 0;
        txtValor.Text = String.Empty;
    }

    protected void ddlBanco_SelectedIndexChanged(object sender, EventArgs e)
    {
        Carga_CuentaBancaria(Convert.ToInt16(ddlEmpresa.SelectedItem.Value),
            ddlBanco.Items.Count > 0 ? Convert.ToSByte(ddlBanco.SelectedItem.Value) : -1);
    }

    protected void ddlBanco_DataBound(object sender, EventArgs e)
    {

        Carga_CuentaBancaria(Convert.ToInt16(ddlEmpresa.SelectedItem.Value),
            ddlBanco.Items.Count > 0 ? Convert.ToSByte(ddlBanco.SelectedItem.Value) : -1);
    }

    protected void grvPedidosFacturados_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
    {
        DataTable dtSortTable = HttpContext.Current.Session["FACTURAS_CONSULTAR"] as DataTable;

        if (dtSortTable == null) return;
        string order = getSortDirectionString(e.SortExpression);
        dtSortTable.DefaultView.Sort = e.SortExpression + " " + order;

        grvPedidosFacturados.DataSource = dtSortTable;
        grvPedidosFacturados.DataBind();
    }

    protected void grvConciliacionCompartida_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;  //IMPORTANTE DESCOMENTAR
        //e.Row.Attributes["onmouseover"] = string.Format("RowMouseOver({0});", e.Row.RowIndex); 
        //e.Row.Attributes["onmouseout"] = string.Format("RowMouseOut({0});", e.Row.RowIndex);
        //e.Row.Attributes["onclick"] = string.Format("RowSelect({0});", e.Row.RowIndex);
    }

    protected void grvConciliacionCompartida_RowDataBound(object sender,
        System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                try
                {
                    DropDownList ddlStatus = e.Row.FindControl("ddlStatusMovimiento") as DropDownList;
                    DropDownList ddlStatusConcepto = e.Row.FindControl("ddlStatusConcepto") as DropDownList;
                    short statusconceptofiltro = String.IsNullOrEmpty(hfStatusConceptoFiltro.Value)
                        ? Convert.ToSByte(-1)
                        : Convert.ToSByte(hfStatusConceptoFiltro.Value);
                    EnlazarDatosDDLStatus(ddlStatus);

                    Enlazar_StatusConcepto(ddlStatusConcepto, statusconceptofiltro);
                    //ddlStatusConcepto.SelectedValue = statusconceptofiltro.ToString();
                    e.Row.CssClass = "header";

                }
                catch (Exception ex)
                {
                    App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Leer la Referencia
                ReferenciaNoConciliada referencia = LeerReferenciaConciliadaCompartida(e.Row.RowIndex);
                //    //Traspaso
                string tipoTraspaso = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TipoTraspaso"));
                Image imgTraspaso = e.Row.FindControl("imgTipo") as Image;
                Label lblTraspaso = e.Row.FindControl("lblMontoTraspaso") as Label;
                Label lblFolioTraspaso = e.Row.FindControl("lblFolioTraspaso") as Label;
                lblFolioTraspaso.Visible = imgTraspaso.Visible = lblTraspaso.Visible = !String.IsNullOrEmpty(tipoTraspaso);
                //Pintar la Celda de Traspaso segun FOLIO
                e.Row.Cells[13].BackColor = referencia.ColorTraspaso;

                //Descripcion Interna
                //StatusConcepto
                //int secuenciarelacion = Convert.ToInt32(grvConciliacionCompartida.DataKeys[e.Row.RowIndex].Values["SecuenciaRelacion"]);
                //ddlStatusConcepto.Enabled = secuenciarelacion != 0;

                listMovimientosConciliadosEx =
                    referencia.ListaReferenciaConciliadaCompartida;
                GridView grvMovimientosConciliadosMovExterno = e.Row.FindControl("grvMovimientosConciliadosMovExterno") as GridView;
                GenerarTablaMovConciliadosMovEx(listMovimientosConciliadosEx);
                LlenaGridViewConciliadosMovExterno(grvMovimientosConciliadosMovExterno);

            }
            //if (e.Row.RowType == DataControlRowType.Pager && (grvConciliacionCompartida.DataSource != null))
            //{
            //    //TRAE EL TOTAL DE PAGINAS
            //    Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumPaginas");
            //    _TotalPags.Text = grvConciliacionCompartida.PageCount.ToString();

            //    //LLENA LA LISTA CON EL NUMERO DE PAGINAS
            //    DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
            //    for (int i = 1; i <= Convert.ToInt32(grvConciliacionCompartida.PageCount); i++)
            //        list.Items.Add(i.ToString());
            //    list.SelectedValue = (grvConciliacionCompartida.PageIndex + 1).ToString();

            //} 

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
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

    protected void grvConciliacionCompartida_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
    {
        DataTable dtSortTable = HttpContext.Current.Session["MOVIMIENTOS_AUX"] as DataTable;

        if (dtSortTable == null) return;
        string order = getSortDirectionString(e.SortExpression);
        dtSortTable.DefaultView.Sort = e.SortExpression + " " + order;

        grvConciliacionCompartida.DataSource = dtSortTable;
        grvConciliacionCompartida.DataBind();
    }

    protected void grvConciliacionCompartida_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvConciliacionCompartida.PageIndex = e.NewPageIndex;
            //DataTable dtSortTable = HttpContext.Current.Session["TAB_EXTERNOS_AX"] as DataTable;
            //grvExternos.DataSource = dtSortTable;
            //grvExternos.DataBind();
            ////Leer el tipoConciliacion URL
            //tipoConciliacion = Convert.ToSByte(Request.QueryString["TipoConciliacion"]);

            //ReferenciaNoConciliada rfEx = leerReferenciaExternaSeleccionada();
            ////Limpiar Listas de Referencia de demas Externos
            //LimpiarExternosReferencia(rfEx);
            //if (tipoConciliacion == 2)
            //    ConsultarPedidosInternos();
            //else
            //    ConsultarArchivosInternos();
            LlenaGridViewConciliacionCompartida();

        }
        catch (Exception ex)
        {
            //App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }

    }

    //protected void paginasDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DropDownList oIraPag = (DropDownList)sender;
    //    int iNumPag;
    //    grvConciliacionCompartida.PageIndex = int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 &&
    //                                          iNumPag <= grvConciliacionCompartida.PageCount
    //        ? iNumPag - 1
    //        : 0;
    //    LlenaGridViewConciliacionCompartida();
    //}
    protected void grvMovimientosConciliadosMovExterno_RowDataBound(object sender,
        System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                short statusConcepto = Convert.ToSByte(DataBinder.Eval(e.Row.DataItem, "StatusConcepto"));
                DropDownList ddlStatusConcepto = (e.Row.FindControl("ddlStatusConcepto") as DropDownList);
                Enlazar_StatusConcepto(ddlStatusConcepto, statusConcepto);

                //Cliente
                string cliente = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Cliente"));
                //StatusConciliacion
                string statusConciliacionMov =
                    Convert.ToString(DataBinder.Eval(e.Row.DataItem, "StatusConciliacion"));
                //if (String.IsNullOrEmpty(cliente)) //statusConciliacionMov.Equals("CONCILIACION ABIERTA")
                //{
                //    var textBox = e.Row.FindControl("txtCliente") as TextBox;
                //    var btn = e.Row.FindControl("btnBuscarPedido") as ImageButton;
                //    
                //    if (!statusConciliacionMov.Equals("CONCILIADA") || !statusConciliacionMov.Equals("CONCILIACION CANCELADA") && textBox != null)
                //    {
                //        textBox.Enabled = btn.Enabled = true;
                //        btn.CssClass = "icono bg-color-grisOscuro centradoMedio";
                //    }
                //}
                var textBox = e.Row.FindControl("txtCliente") as TextBox;
                var btn = e.Row.FindControl("btnBuscarPedido") as ImageButton;

                if (statusConciliacionMov.Equals("CONCILIADA") ||
                    statusConciliacionMov.Equals("CONCILIACION CANCELADA"))
                {

                    textBox.Enabled = btn.Enabled = false;
                    btn.CssClass = "icono bg-color-grisClaro01 centradoMedio";
                }
                //if (String.IsNullOrEmpty(cliente))
                if (cliente.Equals("-1")) textBox.Text = "-";

                TextBox txtTTDI = e.Row.FindControl("txtToolTipDescripcionInterno") as TextBox;
                TextBox txtTDI = e.Row.FindControl("txtDescripcionInterna") as TextBox;

                txtTTDI.Attributes.Add("onKeyUp", ColocarTexto(txtTDI, txtTTDI));

                //Descripcion Interna
                //StatusConcepto
                // GridView grv = sender as GridView;

                //int secuenciarelacion = Convert.ToInt32(grv.DataKeys[e.Row.RowIndex].Values["SecuenciaRelacion"]);
                //ddlStatusConcepto.Enabled = secuenciarelacion != 0;


            }
            //if (e.Row.RowType == DataControlRowType.Pager && (grvConciliacionCompartida.DataSource != null))
            //{
            //    //TRAE EL TOTAL DE PAGINAS
            //    Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumPaginas");
            //    _TotalPags.Text = grvConciliacionCompartida.PageCount.ToString();

            //    //LLENA LA LISTA CON EL NUMERO DE PAGINAS
            //    DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
            //    for (int i = 1; i <= Convert.ToInt32(grvConciliacionCompartida.PageCount); i++)
            //        list.Items.Add(i.ToString());
            //    list.SelectedValue = (grvConciliacionCompartida.PageIndex + 1).ToString();

            //} 
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }
    //**************** FILTRO *********************************************
    private bool FiltrarCampo(string valorFiltro, string filtrarEn)
    {
        bool resultado;
        try
        {
            if (filtrarEn.Equals("Externos"))
            {
                DataTable dt = (DataTable)HttpContext.Current.Session["MOVIMIENTOS_AUX"];

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
                if (dv.Count <= 0) return false;
                dv.RowFilter = SearchExpression;

                HttpContext.Current.Session["MOVIMIENTOS_AUX"] = dv.ToTable();

            }
            else
            {
                listMovimientos = Session["MOVIMIENTOS"] as List<ReferenciaNoConciliada>;
                if (!String.IsNullOrEmpty(valorFiltro))
                    if (listMovimientos != null)
                        listMovimientos = listMovimientos.Where(x => cumpleCondicion(x, valorFiltro)).ToList();

                GenerarTablaConciliacionCompartida(listMovimientos);

            }
            grvConciliacionCompartida.DataSource = HttpContext.Current.Session["MOVIMIENTOS_AUX"] as DataTable;
            grvConciliacionCompartida.DataBind();

            resultado = true;
            mpeFiltrar.Hide();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("La operacion no se puede realizar para el tipo de campo que selecciono. Favor de verificar su selección.\nError:\n" + ex.Message);
            resultado = false;
            mpeFiltrar.Hide();
        }
        return resultado;
    }

    public bool cumpleCondicion(ReferenciaNoConciliada x, string valorFiltro)
    {
        //bool resultado = false;

        ////Recorrer la lista de Movimientos, sacar la ListaReferenciaConciliadadCompartida.. y 
        ////realizar la consulta LINQ Dynamic

        //Leer la propiedad que nos envia el Usuario
        string propiedad = ddlCampoFiltrar.SelectedItem.Text;
        //Leer operacion seleccionada que nos envia el Usuario
        string operacion = ddlOperacion.SelectedItem.Value;

        ////Crear el IQueryable de objetos ReferenciaConciliadaCompartida
        IQueryable<ReferenciaConciliadaCompartida> queryableMovimientos = x.ListaReferenciaConciliadaCompartida.AsQueryable<ReferenciaConciliadaCompartida>();
        //// Crear los parametros de la expresion
        //ParameterExpression px = Expression.Parameter(typeof(ReferenciaConciliadaCompartida), "y");
        ////Expresion que representara : y.PropiedadAsignada por el Usuario == valorFiltro
        //Expression l = Expression.Property(px, propiedad);//typeof(string).GetProperty()
        ConstantExpression r = obtenerExpressionTipoPropiedad(valorFiltro, tipoCampoSeleccionado());//FaltaIdentificar que tipo es el CampoSeleccionado
        ////Realizar la Operacion (=,>=,<=,<>,LIKE)


        //Expression e1 = obtenerExpressionTipoOperacion(l, r, operacion);

        ////Implementing ExpressionBuilder
        //Expression<Func<ReferenciaConciliadaCompartida, bool>> deleg = (Expression<Func<ReferenciaConciliadaCompartida, bool>>) ExpressionBuilder.GetExpression<ReferenciaConciliadaCompartida>(px,propiedad,r,operacion);
        //x.ListaReferenciaConciliadaCompartida.Where(deleg);

        //////Crear el arbol que represente la expresion
        ////MethodCallExpression anyExpresion = Expression.Call(
        ////    typeof(Queryable),
        ////    "Where",
        ////    new Type[] { queryableMovimientos.ElementType },
        ////    queryableMovimientos.Expression,
        ////    Expression.Lambda<Func<ReferenciaConciliadaCompartida, bool>>(e1, new ParameterExpression[] { px }));
        //////Crear IQueryable que ayudara en la ejecucion del la expresion
        ////IQueryable<ReferenciaConciliadaCompartida> resultadoAny = queryableMovimientos.Provider.CreateQuery<ReferenciaConciliadaCompartida>(anyExpresion);

        ////resultado = resultadoAny.Any();


        //Implementing ExpressionBuilder
        Expression<Func<ReferenciaConciliadaCompartida, bool>> deleg = ExpressionBuilder.GetExpression<ReferenciaConciliadaCompartida>(propiedad, r, operacion);
        queryableMovimientos = queryableMovimientos.Where(deleg);

        return queryableMovimientos.Any();


    }



    public ConstantExpression obtenerExpressionTipoPropiedad(string valor, string tipoCampo)
    {
        try
        {
            switch (tipoCampo)
            {
                case "Cadena":
                    return Expression.Constant(valor, typeof(string));
                case "Numero":
                    return Expression.Constant(Convert.ToInt32(valor), typeof(int));
                case "Fecha":
                    return Expression.Constant(Convert.ToDateTime(valor), typeof(DateTime));
                default:
                    return Expression.Constant(valor);

            }

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
        return Expression.Constant(valor);
    }

    private void FiltrarStatusConciliacionMovimiento(string status)
    {

        try
        {
            DataTable dt = (DataTable)HttpContext.Current.Session["TAB_MOVIMIENTOS"];

            DataView dv = new DataView(dt);
            string SearchExpression = String.Empty;
            if (!String.IsNullOrEmpty(status) && !status.Equals("TODOS"))
            {
                SearchExpression = string.Format("{0} {1} '{2}'", "StatusConciliacion", "=", status);
            }
            if (dv.Count <= 0) return;
            dv.RowFilter = SearchExpression;
            //Guardar ValorFiltro
            hfStatusConciliacionFD.Value = status;
            HttpContext.Current.Session["MOVIMIENTOS_AUX"] = dv.ToTable();
            grvConciliacionCompartida.DataSource = HttpContext.Current.Session["MOVIMIENTOS_AUX"] as DataTable;
            grvConciliacionCompartida.DataBind();

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
            hfStatusConciliacionFD.Value = "TODOS";
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
                case "Numero":
                    decimal num = Convert.ToDecimal(txtValor.Text);
                    return num.ToString();
                case "Fecha":
                    DateTime fecha = Convert.ToDateTime(txtValor.Text);
                    return fecha.ToString();
            }

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Verifique:\n- Valor no valido por tipo de Campo seleccionado.");
        }
        return "";
    }

    public string tipoCampoSeleccionado()
    {
        return listCamposDestino[ddlCampoFiltrar.SelectedIndex].Campo1;
    }

    protected void btnIrFiltro_Click(object sender, EventArgs e)
    {
        //Leer el tipoConciliacion URL
        try
        {
            short configuracion = ddlFiltrarEn.SelectedItem.Value.Equals("Externos") ? Convert.ToSByte(4) : Convert.ToSByte(5);
            cargar_ComboCampoFiltroDestino(configuracion);
            FiltrarCampo(valorFiltro(tipoCampoSeleccionado()), Convert.ToString(ddlFiltrarEn.SelectedItem.Text));
            mpeFiltrar.Hide();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }
    /////////////////////// BUSQUEDA ////////////////////////
    public string resaltarBusqueda(string entradaTexto)
    {
        if (txtBuscar.Text.Equals("")) return entradaTexto;
        string strBuscar = txtBuscar.Text;
        Regex RegExp = new Regex(strBuscar.Replace(" ", "|").Trim(),
            RegexOptions.IgnoreCase);
        return RegExp.Replace(entradaTexto, pintarBusqueda);
    }

    public string pintarBusqueda(Match m)
    {
        return "<span class=marcarBusqueda>" + m.Value + "</span>";
    }

    protected void btnIrBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            grvConciliacionCompartida.DataSource = HttpContext.Current.Session["MOVIMIENTOS_AUX"] as DataTable;
            grvConciliacionCompartida.DataBind();

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }

    }
    protected void imgBuscar_Click(object sender, ImageClickEventArgs e)
    {
        txtBuscar.Text = String.Empty;
        mpeBuscar.Show();


    }

    protected void imgBotonBuscarFacturasManual_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string clienteBuscar = hdfClienteBuscar.Value;
            ReferenciaConciliadaCompartida rfc = Session["MOVIMIENTO_SELECCIONADO"] as ReferenciaConciliadaCompartida;
            lblMontoExterno.Text = lblMontoResto.Text = rfc.MontoExterno.ToString("C2");//Colocar el monto seleccionado en el label del MOdal
            //Consulta_Pedidos(rfc.CorporativoConciliacion, rfc.SucursalConciliacion, rfc.AñoConciliacion, rfc.MesConciliacion,
            //    rfc.FolioConciliacion,
            //    rfc.FolioExterno, rfc.SecuenciaExterno, clienteBuscar, rblClienteTipo.SelectedItem.Value.Equals("PADRE"));
            
            string sFInicio     = txtFacturaFechaInicial.Text.Trim();
            string sFFin        = txtFacturaFechaFinal.Text.Trim();
            DateTime FInicio    = (sFInicio.Length > 0 ? Convert.ToDateTime(sFInicio) : DateTime.MinValue);
            DateTime FFin       = (sFFin.Length > 0 ? Convert.ToDateTime(sFFin) : DateTime.MinValue);
            if (FFin > DateTime.MinValue)
            {
                FFin = FFin.AddDays(1D);
            }

            Consulta_FacturasManual(Convert.ToInt32(clienteBuscar),
                                    rblTipoClienteFactura.SelectedItem.Value.Equals("PADREL"),
                                    txtFacturaBusqueda.Text,
                                    FInicio,
                                    FFin);

            GenerarTablaFacturas();
            LlenaGridViewFacturasManuales();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Ha ocurrido un error al leer las facturas del Cliente.\nMensaje: " + ex.Message);
            mpeBusquedaFactura.Hide();
            mpeBusquedaFactura.Dispose();

        }
    }

    protected void imgCerrarBusquedaFactura_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarCamposFacturas();
    }

    private void LimpiarCamposFacturas()
    {
        txtFacturaFechaInicial.Text = "";
        txtFacturaFechaFinal.Text = "";
        txtFacturaBusqueda.Text = "";
        grvPedidosFacturados.DataSource = null;
        grvPedidosFacturados.DataBind();
    }

    private void LlenaGridViewFacturasManuales() //Llena el gridview dePedidos
    {
        DataTable tablaFacturasManuales = HttpContext.Current.Session["FACTURAS_CONSULTAR"] as DataTable;
        //grvPedidos.PageIndex = 0;
        grvPedidosFacturados.DataSource = tablaFacturasManuales;
        grvPedidosFacturados.DataBind();        
    }


    /////////////////////////////////////// BUSCAR FACTURA 
    protected void btnBuscarFactura_Click(object sender, EventArgs e) /*System.Web.UI.ImageClickEventArgs*/
    {
        rblTipoClienteFactura.SelectedValue = rblClienteTipo.SelectedValue;
        lblClienteFactura.Text = lblCliente.Text;

        //lblMontoExterno
        lblMontoExternoFactura.Text = lblMontoExterno.Text;
        lblMontoRestoFactrura.Text = lblMontoResto.Text;
        //lblMontoResto
        mpeBusquedaFactura.Show();
    }

    /////////////////////////////////////// EXPORTAR 
    protected void imgExportar_Click(object sender, ImageClickEventArgs e)
    {
        btnActualizarConfig_Click(btnActualizarConfig,null);//RRV
        try
        {
            AppSettingsReader settings = new AppSettingsReader();

            //Leer Variables URL 
            usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
            int corporativo = Convert.ToInt32(hdfCorporativo.Value);
            int sucursal = Convert.ToInt32(hdfSucursal.Value);
            string cuentabancaria = Convert.ToString(hdfCuentaBancaria.Value);
            DateTime fInicial = Convert.ToDateTime(hdfFInicial.Value);
            DateTime fFinal = Convert.ToDateTime(hdfFFinal.Value);
            string strReporte;
            bool accesototal = LeerGrupoConciliacionUsuarioEspecifico(usuario.IdUsuario.Trim()).AccesoTotal;
            strReporte = Server.MapPath("~/") + settings.GetValue("RutaReporteInformeMovimientosConciliadosExternos", typeof(string));

            if (!File.Exists(strReporte)) return;
            try
            {
                string strServer = settings.GetValue("Servidor", typeof(string)).ToString();
                string strDatabase = settings.GetValue("Base", typeof(string)).ToString();


                string strUsuario = usuario.IdUsuario.Trim();
                string strPW = usuario.ClaveDesencriptada;
                ArrayList Par = new ArrayList();


                Par.Add("@AccesoTotal=" + accesototal);
                Par.Add("@Corporativo=" + corporativo);
                Par.Add("@Sucursal=" + sucursal);
                Par.Add("@CuentaBancaria=" + cuentabancaria);
                Par.Add("@FInicial=" + fInicial);
                Par.Add("@FFinal=" + fFinal);

                ClaseReporte reporte = new ClaseReporte(strReporte, Par, strServer, strDatabase, strUsuario, strPW);
                HttpContext.Current.Session["RepDoc"] = reporte.RepDoc;
                HttpContext.Current.Session["ParametrosReporte"] = Par;
                Nueva_Ventana("../Reporte/Reporte.aspx", "Carta", 0, 0, 0, 0);
                reporte = null;
            }
            catch (Exception ex)
            {
                App.ImplementadorMensajes.MostrarMensaje("Error: Generar Reporte\n" + ex.Message);
            }
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error: Leer Valores.\n" + ex.Message);
        }
    }
    protected void Nueva_Ventana(string pagina, string titulo, int ancho, int alto, int x, int y)
    {

        ScriptManager.RegisterClientScriptBlock(this.upConciliacionCompartida,
                                                upConciliacionCompartida.GetType(),
                                                "ventana",
                                                "ShowWindow('" + pagina + "','" + titulo + "'," + ancho + "," + alto +
                                                "," + x + "," + y + ")",
                                                true);

    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// Metodos Propios //

    // Lee el Grupo de Conciliacion al que pertenece el USUARIO 

    public GrupoConciliacionUsuario LeerGrupoConciliacionUsuarioEspecifico(string usuario)
    {
        return CatalogoConciliacion.App.Consultas.ObtieneGrupoConciliacionUsuarioEspecifico(usuario);
    }
    public void ConsultaMovimientosConciliacionCompartida(bool accesoTotal, int corporativo, int sucursal,
        string cuentaBancaria, DateTime finicial, DateTime ffinal)
    {
        string statusconciliacion = "";
        System.Data.SqlClient.SqlConnection connection = SeguridadCB.Seguridad.Conexion;
        if (connection.State == ConnectionState.Closed)
        {
            SeguridadCB.Seguridad.Conexion.Open();
        }
        try
        {
            if (hdfStatusConciliacionSel.Value == "TODOS")//RRV
                statusconciliacion = "";
            else//RRV
                statusconciliacion = hdfStatusConciliacionSel.Value;//RRV
            listMovimientos =
                Conciliacion.RunTime.App.Consultas.ConsultaMovimientosConciliacionCompartida(accesoTotal, corporativo,
                    sucursal, cuentaBancaria, finicial, ffinal, statusconciliacion);

            //Session["MOVIMIENTOS"] = listMovimientos;
            listMovimientos = LeerListaReferenciasNoRepetidos(listMovimientos);
            Session["MOVIMIENTOS"] = listMovimientos;
            //Sacar los Meses y Años de la Consulta
            List<ReferenciaNoConciliada> distinctAñoMes = listMovimientos.GroupBy(s => new { s.AñoConciliacion, s.MesConciliacion }).Select(s => s.First()).ToList();
            GenerarDropDowAñoMes(distinctAñoMes);

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }

    }

    public void GenerarTablaConciliacionCompartida()
    {
        dtbMovimientoConciliacionCompartida = new DataTable("ConcilacionCompartida");
        dtbMovimientoConciliacionCompartida.Columns.Add("CorporativoConciliacion", typeof(int));
        dtbMovimientoConciliacionCompartida.Columns.Add("SucursalConciliacion", typeof(int));
        dtbMovimientoConciliacionCompartida.Columns.Add("AñoConciliacion", typeof(int));
        dtbMovimientoConciliacionCompartida.Columns.Add("MesConciliacion", typeof(short));
        dtbMovimientoConciliacionCompartida.Columns.Add("FolioConciliacion", typeof(int));


        dtbMovimientoConciliacionCompartida.Columns.Add("Corporativo", typeof(int));
        dtbMovimientoConciliacionCompartida.Columns.Add("Sucursal", typeof(int));
        dtbMovimientoConciliacionCompartida.Columns.Add("Año", typeof(int));
        dtbMovimientoConciliacionCompartida.Columns.Add("Folio", typeof(int));
        dtbMovimientoConciliacionCompartida.Columns.Add("Secuencia", typeof(int));
        dtbMovimientoConciliacionCompartida.Columns.Add("ConsecutivoFlujo", typeof(int));
        dtbMovimientoConciliacionCompartida.Columns.Add("StatusConciliacion", typeof(string));
        dtbMovimientoConciliacionCompartida.Columns.Add("UbicacionIcono", typeof(string));

        dtbMovimientoConciliacionCompartida.Columns.Add("FOperacion", typeof(DateTime));
        dtbMovimientoConciliacionCompartida.Columns.Add("Referencia", typeof(string));
        dtbMovimientoConciliacionCompartida.Columns.Add("Descripcion", typeof(string));
        dtbMovimientoConciliacionCompartida.Columns.Add("SucursalBancaria", typeof(string));
        dtbMovimientoConciliacionCompartida.Columns.Add("Retiro", typeof(decimal));
        dtbMovimientoConciliacionCompartida.Columns.Add("Deposito", typeof(decimal));
        dtbMovimientoConciliacionCompartida.Columns.Add("Saldo", typeof(decimal));
        dtbMovimientoConciliacionCompartida.Columns.Add("Caja", typeof(string));

        //dtbMovimientoConciliacionCompartida.Columns.Add("CorporativoInterno", typeof(int?));
        //dtbMovimientoConciliacionCompartida.Columns.Add("SucursalInterno", typeof(int?));
        //dtbMovimientoConciliacionCompartida.Columns.Add("AñoInterno", typeof(int?));
        //dtbMovimientoConciliacionCompartida.Columns.Add("FolioInterno", typeof(int?));
        //dtbMovimientoConciliacionCompartida.Columns.Add("SecuenciaInterno", typeof(int?));

        //dtbMovimientoConciliacionCompartida.Columns.Add("Pedido", typeof(int?));
        //dtbMovimientoConciliacionCompartida.Columns.Add("Celula", typeof(int?));
        //dtbMovimientoConciliacionCompartida.Columns.Add("AñoPed", typeof(int?));
        //dtbMovimientoConciliacionCompartida.Columns.Add("Cliente", typeof(int));

        //dtbMovimientoConciliacionCompartida.Columns.Add("DescripcionInterno", typeof(string));
        //dtbMovimientoConciliacionCompartida.Columns.Add("ConceptoInterno", typeof(string));

        dtbMovimientoConciliacionCompartida.Columns.Add("StatusConcepto", typeof(int));

        dtbMovimientoConciliacionCompartida.Columns.Add("TipoTraspaso", typeof(string));
        dtbMovimientoConciliacionCompartida.Columns.Add("MontoTraspaso", typeof(decimal));
        dtbMovimientoConciliacionCompartida.Columns.Add("AñoTraspaso", typeof(int));
        dtbMovimientoConciliacionCompartida.Columns.Add("FolioTraspaso", typeof(int));

        foreach (ReferenciaNoConciliada rcc in listMovimientos)
            dtbMovimientoConciliacionCompartida.Rows.Add(
                rcc.Corporativo,
                rcc.SucursalConciliacion,
                rcc.AñoConciliacion,
                rcc.MesConciliacion,
                rcc.FolioConciliacion,
                //rcc.SecuenciaRelacion,

                rcc.Corporativo,
                rcc.Sucursal,
                rcc.Año,
                rcc.Folio,
                rcc.Secuencia,
                rcc.ConsecutivoFlujo,
                rcc.StatusConciliacion,
                rcc.UbicacionIcono,

                rcc.FOperacion,
                rcc.Referencia,
                rcc.Descripcion,
                rcc.SucursalBancaria,
                rcc.Retiro,
                rcc.Deposito,
                rcc.Saldo,
                rcc.Caja,

                //rcc.Cliente,
                //rcc.DescripcionInterno,
                //rcc.ConceptoInterno,

                rcc.StatusConcepto,

                rcc.TipoTraspaso.ToUpper(),
                rcc.MontoTraspaso,
                rcc.AñoTraspaso,
                rcc.FolioTraspaso);

        HttpContext.Current.Session["TAB_MOVIMIENTOS"] = dtbMovimientoConciliacionCompartida;
        HttpContext.Current.Session["MOVIMIENTOS_AUX"] = dtbMovimientoConciliacionCompartida;
    }
    public void GenerarTablaConciliacionCompartida(List<ReferenciaNoConciliada> listMovimientosFiltro)
    {
        dtbMovimientoConciliacionCompartida = new DataTable("ConcilacionCompartida");
        dtbMovimientoConciliacionCompartida.Columns.Add("CorporativoConciliacion", typeof(int));
        dtbMovimientoConciliacionCompartida.Columns.Add("SucursalConciliacion", typeof(int));
        dtbMovimientoConciliacionCompartida.Columns.Add("AñoConciliacion", typeof(int));
        dtbMovimientoConciliacionCompartida.Columns.Add("MesConciliacion", typeof(short));
        dtbMovimientoConciliacionCompartida.Columns.Add("FolioConciliacion", typeof(int));


        dtbMovimientoConciliacionCompartida.Columns.Add("Corporativo", typeof(int));
        dtbMovimientoConciliacionCompartida.Columns.Add("Sucursal", typeof(int));
        dtbMovimientoConciliacionCompartida.Columns.Add("Año", typeof(int));
        dtbMovimientoConciliacionCompartida.Columns.Add("Folio", typeof(int));
        dtbMovimientoConciliacionCompartida.Columns.Add("Secuencia", typeof(int));
        dtbMovimientoConciliacionCompartida.Columns.Add("ConsecutivoFlujo", typeof(int));
        dtbMovimientoConciliacionCompartida.Columns.Add("StatusConciliacion", typeof(string));
        dtbMovimientoConciliacionCompartida.Columns.Add("UbicacionIcono", typeof(string));

        dtbMovimientoConciliacionCompartida.Columns.Add("FOperacion", typeof(DateTime));
        dtbMovimientoConciliacionCompartida.Columns.Add("Referencia", typeof(string));
        dtbMovimientoConciliacionCompartida.Columns.Add("Descripcion", typeof(string));
        dtbMovimientoConciliacionCompartida.Columns.Add("SucursalBancaria", typeof(string));
        dtbMovimientoConciliacionCompartida.Columns.Add("Retiro", typeof(decimal));
        dtbMovimientoConciliacionCompartida.Columns.Add("Deposito", typeof(decimal));
        dtbMovimientoConciliacionCompartida.Columns.Add("Saldo", typeof(decimal));
        dtbMovimientoConciliacionCompartida.Columns.Add("Caja", typeof(string));

        //dtbMovimientoConciliacionCompartida.Columns.Add("CorporativoInterno", typeof(int?));
        //dtbMovimientoConciliacionCompartida.Columns.Add("SucursalInterno", typeof(int?));
        //dtbMovimientoConciliacionCompartida.Columns.Add("AñoInterno", typeof(int?));
        //dtbMovimientoConciliacionCompartida.Columns.Add("FolioInterno", typeof(int?));
        //dtbMovimientoConciliacionCompartida.Columns.Add("SecuenciaInterno", typeof(int?));

        //dtbMovimientoConciliacionCompartida.Columns.Add("Pedido", typeof(int?));
        //dtbMovimientoConciliacionCompartida.Columns.Add("Celula", typeof(int?));
        //dtbMovimientoConciliacionCompartida.Columns.Add("AñoPed", typeof(int?));
        //dtbMovimientoConciliacionCompartida.Columns.Add("Cliente", typeof(int));

        //dtbMovimientoConciliacionCompartida.Columns.Add("DescripcionInterno", typeof(string));
        //dtbMovimientoConciliacionCompartida.Columns.Add("ConceptoInterno", typeof(string));

        dtbMovimientoConciliacionCompartida.Columns.Add("StatusConcepto", typeof(int));

        dtbMovimientoConciliacionCompartida.Columns.Add("TipoTraspaso", typeof(string));
        dtbMovimientoConciliacionCompartida.Columns.Add("MontoTraspaso", typeof(decimal));
        dtbMovimientoConciliacionCompartida.Columns.Add("AñoTraspaso", typeof(int));
        dtbMovimientoConciliacionCompartida.Columns.Add("FolioTraspaso", typeof(int));

        foreach (ReferenciaNoConciliada rcc in listMovimientosFiltro)
            dtbMovimientoConciliacionCompartida.Rows.Add(
                rcc.Corporativo,
                rcc.SucursalConciliacion,
                rcc.AñoConciliacion,
                rcc.MesConciliacion,
                rcc.FolioConciliacion,
                //rcc.SecuenciaRelacion,

                rcc.Corporativo,
                rcc.Sucursal,
                rcc.Año,
                rcc.Folio,
                rcc.Secuencia,
                rcc.ConsecutivoFlujo,
                rcc.StatusConciliacion,
                rcc.UbicacionIcono,

                rcc.FOperacion,
                rcc.Referencia,
                rcc.Descripcion,
                rcc.SucursalBancaria,
                rcc.Retiro,
                rcc.Deposito,
                rcc.Saldo,
                rcc.Caja,

                //rcc.Cliente,
                //rcc.DescripcionInterno,
                //rcc.ConceptoInterno,

                rcc.StatusConcepto,

                rcc.TipoTraspaso.ToUpper(),
                rcc.MontoTraspaso,
                rcc.AñoTraspaso,
                rcc.FolioTraspaso);

        //HttpContext.Current.Session["TAB_MOVIMIENTOS"] = dtbMovimientoConciliacionCompartida;
        HttpContext.Current.Session["MOVIMIENTOS_AUX"] = dtbMovimientoConciliacionCompartida;
    }

    private void LlenaGridViewConciliacionCompartida()
    {
        DataTable tablaConciliacionCompartida = (DataTable)HttpContext.Current.Session["TAB_MOVIMIENTOS"];
        grvConciliacionCompartida.DataSource = tablaConciliacionCompartida;
        grvConciliacionCompartida.DataBind();
    }
    /// <summary>
    /// Metodos para la generacion del Grid interno por Mov Externo
    /// </summary>
    public void GenerarTablaMovConciliadosMovEx(List<ReferenciaConciliadaCompartida> listMovConciladosEx)
    {
        dtbMovimientoConciliadosMovEx = new DataTable("MovimientosConciliadosMovEx");
        dtbMovimientoConciliadosMovEx.Columns.Add("CorporativoConciliacion", typeof(int));
        dtbMovimientoConciliadosMovEx.Columns.Add("SucursalConciliacion", typeof(int));
        dtbMovimientoConciliadosMovEx.Columns.Add("AñoConciliacion", typeof(int));
        dtbMovimientoConciliadosMovEx.Columns.Add("MesConciliacion", typeof(short));
        dtbMovimientoConciliadosMovEx.Columns.Add("FolioConciliacion", typeof(int));
        dtbMovimientoConciliadosMovEx.Columns.Add("SecuenciaRelacion", typeof(int));

        dtbMovimientoConciliadosMovEx.Columns.Add("Corporativo", typeof(int));
        dtbMovimientoConciliadosMovEx.Columns.Add("Sucursal", typeof(int));
        dtbMovimientoConciliadosMovEx.Columns.Add("Año", typeof(int));
        dtbMovimientoConciliadosMovEx.Columns.Add("Folio", typeof(int));
        dtbMovimientoConciliadosMovEx.Columns.Add("Secuencia", typeof(int));

        dtbMovimientoConciliadosMovEx.Columns.Add("StatusConciliacion", typeof(string));
        dtbMovimientoConciliadosMovEx.Columns.Add("UbicacionIcono", typeof(string));

        dtbMovimientoConciliadosMovEx.Columns.Add("MontoConciliado", typeof(decimal));

        //dtbMovimientoConciliadosMovEx.Columns.Add("CorporativoInterno", typeof(int?));
        //dtbMovimientoConciliadosMovEx.Columns.Add("SucursalInterno", typeof(int?));
        //dtbMovimientoConciliadosMovEx.Columns.Add("AñoInterno", typeof(int?));
        //dtbMovimientoConciliadosMovEx.Columns.Add("FolioInterno", typeof(int?));
        //dtbMovimientoConciliadosMovEx.Columns.Add("SecuenciaInterno", typeof(int?));

        //dtbMovimientoConciliadosMovEx.Columns.Add("Pedido", typeof(int?));
        //dtbMovimientoConciliadosMovEx.Columns.Add("Celula", typeof(int?));
        //dtbMovimientoConciliadosMovEx.Columns.Add("AñoPed", typeof(int?));
        dtbMovimientoConciliadosMovEx.Columns.Add("Cliente", typeof(int));

        dtbMovimientoConciliadosMovEx.Columns.Add("DescripcionInterno", typeof(string));
        dtbMovimientoConciliadosMovEx.Columns.Add("ConceptoInterno", typeof(string));

        dtbMovimientoConciliadosMovEx.Columns.Add("MotivoNoConciliado", typeof(string));
        dtbMovimientoConciliadosMovEx.Columns.Add("ComentarioNoConciliado", typeof(string));

        dtbMovimientoConciliadosMovEx.Columns.Add("StatusConcepto", typeof(int));


        foreach (ReferenciaConciliadaCompartida rcc in listMovConciladosEx)
            dtbMovimientoConciliadosMovEx.Rows.Add(
                rcc.Corporativo,
                rcc.SucursalConciliacion,
                rcc.AñoConciliacion,
                rcc.MesConciliacion,
                rcc.FolioConciliacion,
                rcc.SecuenciaRelacion,

                rcc.Corporativo,
                rcc.Sucursal,
                rcc.Año,
                rcc.Folio,
                rcc.Secuencia,

                rcc.StatusConciliacion,
                rcc.UbicacionIcono,

                rcc.MontoConciliado,
                rcc.Cliente,

                rcc.DescripcionInterno,
                rcc.ConceptoInterno,

                rcc.MotivoNoConciliado,
                rcc.ComentarioNoConciliado,

                rcc.StatusConcepto

             );

        HttpContext.Current.Session["MOVEXTERNOS_CONCILIADOS"] = dtbMovimientoConciliadosMovEx;
    }

    private void LlenaGridViewConciliadosMovExterno(GridView grvMovimientosConciliadosMovExterno)
    {
        DataTable tablaConciliadosMovExterno = (DataTable)HttpContext.Current.Session["MOVEXTERNOS_CONCILIADOS"];
        grvMovimientosConciliadosMovExterno.DataSource = tablaConciliadosMovExterno;
        grvMovimientosConciliadosMovExterno.DataBind();
    }

    /// <summary>
    /// Genera el DropDown del Mes/Año Consultado [NECESARIO PARA CERRAR MES]
    /// </summary>
    public void GenerarDropDowAñoMes(List<ReferenciaNoConciliada> añomes)
    {
        try
        {
            List<ListaCombo> l = (from r in añomes
                                  let añomess = Convert.ToInt32(string.Format("{0}{1}", r.MesConciliacion, r.AñoConciliacion))
                                  let mesaño = string.Format("{0}/{1}", obtenerNombreMesNumero(r.MesConciliacion), r.AñoConciliacion)
                                  select new ListaCombo(añomess, mesaño)).ToList();
            ddlMesAño.DataValueField = "Identificador";
            ddlMesAño.DataTextField = "Descripcion";
            ddlMesAño.DataSource = l;
            ddlMesAño.DataBind();
            ddlMesAño.Dispose();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error al generar el radioButton. Recargue la vista.\n" +
                                                     ex.Message);
        }
    }

    /// <summary>
    /// Consulta los StatusConepto que permiten ser Ajustables 
    /// </summary>
    public void Carga_StatusConcepto(int grupoconciliacion)
    {
        try
        {
            listStatusConcepto = Conciliacion.Migracion.Runtime.App.Consultas.ObtenieneStatusConceptosGrupoConciliacion(2, grupoconciliacion);
            Session["StatusConcepto"] = listStatusConcepto;
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error al cargar el Status Concepto. Recargue la vista.\n" +
                                                     ex.Message);
        }
    }

    /// <summary>
    /// Enlazar el dropdow list con los StatusConcepto Cargados
    /// </summary>
    public void Enlazar_StatusConcepto(DropDownList ddlStatusConcepto, short statusconcepto)
    {
        try
        {
            listStatusConcepto = Session["StatusConcepto"] as List<StatusConcepto>;
            if (listStatusConcepto == null) return;
            ddlStatusConcepto.DataValueField = "Id";
            ddlStatusConcepto.DataTextField = "Descripcion";
            ddlStatusConcepto.DataSource = listStatusConcepto;
            ddlStatusConcepto.DataBind();
            ddlStatusConcepto.Dispose();
            try
            {
                ddlStatusConcepto.Items.FindByValue(statusconcepto.ToString()).Selected = true;
                
            }
            catch (NullReferenceException)
            {
                ddlStatusConcepto.Items.FindByValue("0").Selected = true;
            }
            ddlStatusConcepto.BackColor = Convert.ToInt16(ddlStatusConcepto.SelectedItem.Value) == 0
                ? Color.LightGray
                : Color.LightGreen;

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error al cargar el Status Concepto. Recargue la vista.\n" +
                                                     ex.Message);
        }
    }

    protected void btnActualizarConfig_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        try
        {



            if (FiltroCorrecto())
            {
                usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
                hdfCorporativo.Value = ddlEmpresa.SelectedItem.Value;
                hdfSucursal.Value = ddlSucursal.SelectedItem.Value;
                hdfCuentaBancaria.Value = ddlCuentaBancaria.SelectedItem.Text.Trim();
                hdfFInicial.Value = txtFInicial.Text;
                hdfFFinal.Value = txtFFinal.Text;
                hdfCuentaBancaria.Value = ddlCuentaBancaria.Text.Trim();
                GrupoConciliacionUsuario gcu = LeerGrupoConciliacionUsuarioEspecifico(usuario.IdUsuario.Trim());
                ConsultaMovimientosConciliacionCompartida(gcu.AccesoTotal,
                    Convert.ToInt16(ddlEmpresa.SelectedItem.Value),
                    Convert.ToInt16(ddlSucursal.SelectedItem.Value),
                    ddlCuentaBancaria.SelectedItem.Text.Trim(),
                     Convert.ToDateTime(txtFInicial.Text),
                     Convert.ToDateTime(txtFFinal.Text)
                    );
                GenerarTablaConciliacionCompartida();
                LlenaGridViewConciliacionCompartida();
                ActualizarPopUp_CargaArchivo();
                mpeLoading.Hide();
            }

            else
                App.ImplementadorMensajes.MostrarMensaje("Dato Incorrecto: " + mensaje + ".\nVerifique su Selección");
        }
        catch (Exception ex)
        {

            App.ImplementadorMensajes.MostrarMensaje("Error al Consultar los Movimientos:\n" + ex.Message);
        }
    }

    private void ActualizarPopUp_CargaArchivo()
    {
        int CuentaBancaria = 0;
        string sCuentaBancaria = "";

        sCuentaBancaria = ddlCuentaBancaria.SelectedItem.Text.Replace(" ", "").Trim();

        if (sCuentaBancaria.Length > 5)
        {
            sCuentaBancaria = sCuentaBancaria.Substring(sCuentaBancaria.Length - 5, 5);
        }
        
        wucCargaExcelCyC.CuentaBancaria = CuentaBancaria;
    }

    protected void btnGuardarVista_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        bool resultado = false;
        if (grvConciliacionCompartida.Rows.Count > 0)
        {
            try
            {

                ReferenciaNoConciliada referencia;
                foreach (GridViewRow row in grvConciliacionCompartida.Rows)
                {
                    referencia = LeerReferenciaConciliadaCompartida(row.RowIndex);
                    GridView grvMCE = row.FindControl("grvMovimientosConciliadosMovExterno") as GridView;
                    foreach (GridViewRow rowMC in grvMCE.Rows)
                    {
                        ReferenciaConciliadaCompartida rcc = LeerMovimientoExternoConciliado(referencia, grvMCE,
                            rowMC.RowIndex);
                        short statusConcepto =
                            Convert.ToSByte((rowMC.FindControl("ddlStatusConcepto") as DropDownList).SelectedItem.Value);
                        TextBox txtTDI = rowMC.FindControl("txtDescripcionInterna") as TextBox;

                        if ((statusConcepto == rcc.StatusConcepto || statusConcepto == 0) && rcc.DescripcionInterno.Equals(txtTDI.Text))
                            continue;
                        rcc.StatusConcepto = statusConcepto;
                        rcc.DescripcionInterno = txtTDI.Text;

                        resultado = referencia.ConInterno
                            ? rcc.ActualizarStatusConceptoDescripcionConciliacionReferencia()
                            : rcc.ActualizarStatusConceptoDescripcionConciliacionPedido();
                    }
                }

                ConciliarMovExterno( (ReferenciaConciliadaCompartida)Session["MOVIMIENTO_SELECCIONADO"] );
            }
            catch (Exception ex)
            {
                App.ImplementadorMensajes.MostrarMensaje(
                    "Ocurrio un error al tratar de Cambiar Status Concepto y/o Descripcion del Movimientos. Recargue la vista actual. \nError:[" +
                    ex.Message + "]");

            }
            try
            {
                if (resultado)
                {
                    if (FiltroCorrecto())
                    {
                        usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
                        GrupoConciliacionUsuario gcu = LeerGrupoConciliacionUsuarioEspecifico(usuario.IdUsuario.Trim());
                        ConsultaMovimientosConciliacionCompartida(gcu.AccesoTotal,
                            Convert.ToInt16(ddlEmpresa.SelectedItem.Value),
                            Convert.ToInt16(ddlSucursal.SelectedItem.Value),
                            ddlCuentaBancaria.SelectedItem.Text.Trim(),
                            Convert.ToDateTime(txtFInicial.Text),
                            Convert.ToDateTime(txtFFinal.Text)
                            );
                        GenerarTablaConciliacionCompartida();
                        LlenaGridViewConciliacionCompartida();
                    }

                    else
                        App.ImplementadorMensajes.MostrarMensaje("Dato Incorrecto: " + mensaje +
                                                                 ".\nVerifique su Selección");

                    App.ImplementadorMensajes.MostrarMensaje("Datos actualizados exitosamente.");
                }

            }
            catch (Exception ex)
            {
                App.ImplementadorMensajes.MostrarMensaje(
                    "Ocurrio un error al tratar de Actualizar la Vista. Recargue manualmente la vista actual. \nError:[" +
                    ex.Message + "]");

            }
        }
        else
        {
            App.ImplementadorMensajes.MostrarMensaje("No existen datos en la Vista Actual.");
        }
    }
    private string obtenerNombreMesNumero(int numeroMes)
    {
        try
        {
            DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
            return formatoFecha.GetMonthName(numeroMes).ToUpper();
        }
        catch
        {
            return "Desconocido";
        }
    }
    protected void imgCerrarMesConciliacion_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {


        if (grvConciliacionCompartida.Rows.Count <= 0)
        {
            App.ImplementadorMensajes.MostrarMensaje("No existen datos necesarios para identificar el mes a cerrrar.");
            return;
        }
        mpeMesAño.Show();

    }

    protected void btnMesAñoCierre_Click(object sender, EventArgs e)
    {
        try
        {
            string mesaño = ddlMesAño.SelectedValue;

            int corporativo = Convert.ToInt32(hdfCorporativo.Value);
            int sucursal = Convert.ToInt32(hdfSucursal.Value);
            int año = Convert.ToInt16(mesaño.Substring((mesaño.Length - 4), 4));
            short mes = Convert.ToSByte(mesaño.Substring(0, mesaño.IndexOf(año.ToString(), System.StringComparison.Ordinal)));

            usuario = Session["Usuario"] as Usuario;
            if (MesValidoParaCerrar(0, corporativo, sucursal, año, mes, usuario.IdUsuario.Trim()))//Si el mes no esta CERRADO
            {

                if (MesValidoParaCerrar(1, corporativo, sucursal, año, mes, usuario.IdUsuario.Trim()))
                {
                    if (CerrarMesConciliacion(2, corporativo, sucursal, año, mes, usuario.IdUsuario.Trim()))
                    {
                        App.ImplementadorMensajes.MostrarMensaje(String.Format("El MES : {0}/{1} se ha CERRADO exitosamente.", obtenerNombreMesNumero(mes), año)); mpeMesAño.Hide(); mpeMesAño.Dispose();
                    }
                    else App.ImplementadorMensajes.MostrarMensaje("Ocurrio un error al intentar cerrar el mes. Favor de recargar la vista.");
                }
                else
                    App.ImplementadorMensajes.MostrarMensaje(String.Format("No es posible CERRAR el Mes : {0}/{1} pues existen Conciliaciones ABIERTAS.", obtenerNombreMesNumero(mes), año));
            }
            else
            {
                App.ImplementadorMensajes.MostrarMensaje(String.Format("El mes {0}/{1} ya está CERRADO.", obtenerNombreMesNumero(mes), año));
                mpeMesAño.Hide(); mpeMesAño.Dispose();
            }

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }

    }

    public bool MesValidoParaCerrar(short config, int corporativo, int sucursal, int año, short mes, string usuariocierre)
    {
        return Conciliacion.RunTime.App.Consultas.ValidarCierreMes(config, corporativo, sucursal, año, mes, usuariocierre);
    }
    public bool CerrarMesConciliacion(short config, int corporativo, int sucursal, int año, short mes, string usuariocierre)
    {
        return Conciliacion.RunTime.App.Consultas.CierreMesConciliacion(config, corporativo, sucursal, año, mes, usuariocierre);
    }

    public ReferenciaNoConciliada LeerReferenciaConciliadaCompartida(int index)
    {
        try
        {


            int folioConciliacion = Convert.ToInt32(grvConciliacionCompartida.DataKeys[index].Values["FolioConciliacion"]);
            int corporativoConciliacion = Convert.ToInt32(grvConciliacionCompartida.DataKeys[index].Values["CorporativoConciliacion"]);
            int sucursalConciliacion = Convert.ToInt32(grvConciliacionCompartida.DataKeys[index].Values["SucursalConciliacion"]);
            int añoConciliacion = Convert.ToInt32(grvConciliacionCompartida.DataKeys[index].Values["AñoConciliacion"]);
            short mesConciliacion = Convert.ToSByte(grvConciliacionCompartida.DataKeys[index].Values["MesConciliacion"]);
            int folio = Convert.ToInt32(grvConciliacionCompartida.DataKeys[index].Values["Folio"]); ;
            int sucursal = Convert.ToInt32(grvConciliacionCompartida.DataKeys[index].Values["Sucursal"]);
            int año = Convert.ToInt32(grvConciliacionCompartida.DataKeys[index].Values["Año"]);
            int secuencia = Convert.ToInt32(grvConciliacionCompartida.DataKeys[index].Values["Secuencia"]);
            //int secuenciarelacion = Convert.ToInt32(grvConciliacionCompartida.DataKeys[index].Values["SecuenciaRelacion"]);
            int conflujo = Convert.ToInt32(grvConciliacionCompartida.DataKeys[index].Values["ConsecutivoFlujo"]);
            //Leer MovimientoSeleccionado.
            listMovimientos = Session["MOVIMIENTOS"] as List<ReferenciaNoConciliada>;
            return listMovimientos.Find(x => x.Corporativo == corporativoConciliacion &&
                                                        x.SucursalConciliacion == sucursalConciliacion &&
                                                        x.AñoConciliacion == añoConciliacion &&
                                                        x.MesConciliacion == mesConciliacion &&
                                                        x.FolioConciliacion == folioConciliacion &&
                                                        x.Corporativo == corporativoConciliacion &&
                                                        x.Sucursal == sucursal &&
                                                        x.Año == año &&
                                                        x.Folio == folio &&
                                                        x.Secuencia == secuencia &&
                //x.SecuenciaRelacion == secuenciarelacion &&
                                                        x.ConsecutivoFlujo == conflujo);
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error al tratar de leer los movimientos seleccionados. Recargue la Vista Actual.\nError: [" + ex.Message + "]");
            throw;
        }
    }


    public ReferenciaConciliadaCompartida LeerMovimientoExternoConciliado(ReferenciaNoConciliada rcc, GridView grMCE, int index)
    {
        try
        {

            int secuenciarelacion = Convert.ToInt32(grMCE.DataKeys[index].Values["SecuenciaRelacion"]);

            //Leer MovimientoSeleccionado.

            return rcc.ListaReferenciaConciliadaCompartida.Find(x => x.SecuenciaRelacion == secuenciarelacion);
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error al tratar de leer los movimientos seleccionados. Recargue la Vista Actual.\nError: [" + ex.Message + "]");
            throw;
        }
    }
    //-------------------------------------------------------------------------------------------------
    public ClienteAuxiliar Consulta_ClienteValido(string cliente)
    {
        ClienteAuxiliar ClienteRetorno = new ClienteAuxiliar();
        ClienteRetorno.Referencia = cliente;
        
        bool resultado = true;
        try
        {
            Int64 cli = Convert.ToInt64(cliente);
            ListaCombo clt = Conciliacion.RunTime.App.Consultas.ConsultaDatosCliente(cli);
            ClienteRetorno.Cliente = clt.Identificador;
            lblCliente.Text = clt != null ? clt.Descripcion : "Cliente no Identificado";
        }
        catch (FormatException e)
        {
            resultado = false;
            App.ImplementadorMensajes.MostrarMensaje("Cliente no es Valido, verifique su consulta.");

        }
        catch (OverflowException e)
        {
            resultado = false;
            App.ImplementadorMensajes.MostrarMensaje("Cliente no es Valido, verifique su consulta.");

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Mensaje de Error:\n" + ex.Message);
        }

        return ClienteRetorno;
    }

    public void Consulta_Pedidos(int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion,
                                     short mesconciliacion, int folioconciliacion, int folio, int secuencia, string cliente, bool clientepadre)
    {
        System.Data.SqlClient.SqlConnection connection = SeguridadCB.Seguridad.Conexion;
        if (connection.State == ConnectionState.Closed)
        {
            SeguridadCB.Seguridad.Conexion.Open();
        }
        try
        {
            listaReferenciaPedidos =
                Conciliacion.RunTime.App.Consultas.ConciliacionBusquedaPedido(Consultas.BusquedaPedido.Todos,
                                                                              corporativoconciliacion,
                                                                              sucursalconciliacion, añoconciliacion,
                                                                              mesconciliacion, folioconciliacion,
                                                                              folio, secuencia,
                                                                              0, -1, cliente, clientepadre);
            Session["PEDIDOS_CONCILIAR"] = listaReferenciaPedidos;
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    public void Consulta_PedidosFactura(int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion,
                             short mesconciliacion, int folioconciliacion, int folio, int secuencia, string cliente, bool clientepadre, SqlString factura, DateTime fechafactura)
    {
        System.Data.SqlClient.SqlConnection connection = SeguridadCB.Seguridad.Conexion;
        if (connection.State == ConnectionState.Closed)
        {
            SeguridadCB.Seguridad.Conexion.Open();
        }
        try
        {
            listaReferenciaPedidos =
                Conciliacion.RunTime.App.Consultas.ConciliacionBusquedaPedido(Consultas.BusquedaPedido.Todos,
                                                                              corporativoconciliacion,
                                                                              sucursalconciliacion, añoconciliacion,
                                                                              mesconciliacion, folioconciliacion,
                                                                              folio, secuencia,
                                                                              0, -1, cliente, clientepadre, factura, fechafactura);
            Session["PEDIDOS_CONCILIAR"] = listaReferenciaPedidos;
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    public void Consulta_FacturasManual(int cliente, bool clientepadre, SqlString factura, DateTime fechaIni, DateTime fechaFin)
    {
        System.Data.SqlClient.SqlConnection connection = SeguridadCB.Seguridad.Conexion;
        if (connection.State == ConnectionState.Closed)
        {
            SeguridadCB.Seguridad.Conexion.Open();
        }
        try
        {

            listaReferenciaFacturaConsulta =
            Conciliacion.RunTime.App.Consultas.ConciliacionBusquedaFacturaManual(cliente, clientepadre, factura, fechaIni, fechaFin);
            Session["FACTURAS_CONSULTAR_LISTA"] = listaReferenciaFacturaConsulta;
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }



    public void btnSalir_Click(object sender, EventArgs e)
    {
        try
        {
            Session["FACTURAS_CONSULTAR"] = "";
            Session["FACTURAS_CONSULTAR_LISTA"] = "";

            LimpiarCamposFacturas();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    public void GenerarTablaPedidos() //Genera la tabla Referencias a Conciliar de Pedidos.
    {
        SeguridadCB.Public.Parametros parametros;
        parametros = (SeguridadCB.Public.Parametros)HttpContext.Current.Session["Parametros"];
        AppSettingsReader settings = new AppSettingsReader();
        string _URLGateway = parametros.ValorParametro(Convert.ToSByte(settings.GetValue("Modulo", typeof(sbyte))), "URLGateway");
        lstClientesCRM.Clear();
        //tblPedidos = new DataTable("ReferenciasInternas");
        //tblPedidos.Columns.Add("Pedido", typeof(int));
        //tblPedidos.Columns.Add("PedidoReferencia", typeof(int));
        //tblPedidos.Columns.Add("AñoPed", typeof(int));
        //tblPedidos.Columns.Add("Celula", typeof(int));
        //tblPedidos.Columns.Add("Cliente", typeof(string));
        //tblPedidos.Columns.Add("Nombre", typeof(string));
        //tblPedidos.Columns.Add("FSuministro", typeof(DateTime));
        //tblPedidos.Columns.Add("Total", typeof(decimal));
        //tblPedidos.Columns.Add("Concepto", typeof(string));
        tblPedidos = new DataTable("ReferenciasInternas");
        tblPedidos.Columns.Add("Pedido", typeof(int));
        tblPedidos.Columns.Add("PedidoReferencia", typeof(string));
        tblPedidos.Columns.Add("RemisionPedido", typeof(int));
        tblPedidos.Columns.Add("FolioSat", typeof(int));
        tblPedidos.Columns.Add("SerieSat", typeof(string));
        tblPedidos.Columns.Add("Total", typeof(decimal));
        tblPedidos.Columns.Add("FSuministro", typeof(DateTime));
        tblPedidos.Columns.Add("Nombre", typeof(string));
        tblPedidos.Columns.Add("Concepto", typeof(string));
        tblPedidos.Columns.Add("Cliente", typeof(string));
        tblPedidos.Columns.Add("Celula", typeof(int));
        tblPedidos.Columns.Add("AñoPed", typeof(int));

        foreach (
            ReferenciaNoConciliadaPedido rc in
                listaReferenciaPedidos)
        {
            tblPedidos.Rows.Add(
                rc.Pedido,
                rc.PedidoReferencia,
                rc.RemisionPedido,
                rc.FolioSat,
                rc.SerieSat,
                rc.Total,
                rc.FMovimiento,
                _URLGateway != string.Empty ? ConsultaClienteCRM(rc.Cliente, _URLGateway) : rc.Nombre,
                rc.Concepto,
                rc.Cliente,
                rc.CelulaPedido,
                rc.AñoPedido
                );
        }
        HttpContext.Current.Session["TAB_PEDIDOS"] = tblPedidos;
    }

    public string ConsultaClienteCRM(int NumCliente, string urlGateway)
    {
        string Nombre = "";
        Cliente cliente;
        cliente = lstClientesCRM.Find(x => x.NumCliente == NumCliente);
        if (cliente == null)
        {
            cliente = Conciliacion.RunTime.App.Cliente.CrearObjeto();
            Nombre = cliente.consultaClienteCRM(NumCliente, urlGateway);
            cliente.NumCliente = NumCliente;
            cliente.Nombre = Nombre;
            lstClientesCRM.Add(cliente);
        }
        else
            Nombre = cliente.Nombre;

        return Nombre;  

    }

    public void GenerarTablaFacturas() //Genera la tabla Referencias a Conciliar de Pedidos.
    {
        SeguridadCB.Public.Parametros parametros;
        parametros = (SeguridadCB.Public.Parametros)HttpContext.Current.Session["Parametros"];
        AppSettingsReader settings = new AppSettingsReader();
        string _URLGateway = parametros.ValorParametro(Convert.ToSByte(settings.GetValue("Modulo", typeof(sbyte))), "URLGateway");
        Cliente cliente = Conciliacion.RunTime.App.Cliente.CrearObjeto();

        tblPedidos = new DataTable("FacturasManual");
        tblPedidos.Columns.Add("Cliente", typeof(string));
        tblPedidos.Columns.Add("Nombre", typeof(string));
        tblPedidos.Columns.Add("Factura", typeof(int));
        tblPedidos.Columns.Add("FechaFactura", typeof(DateTime));
        tblPedidos.Columns.Add("FolioFactura", typeof(string));
        tblPedidos.Columns.Add("Concepto", typeof(string));
        tblPedidos.Columns.Add("Total", typeof(decimal));

        foreach (
            ReferenciaNoConciliadaPedido rc in
                listaReferenciaFacturaConsulta)
        {
            tblPedidos.Rows.Add(
                rc.Cliente,
                //_URLGateway != string.Empty ? cliente.consultaClienteCRM(rc.Cliente, _URLGateway) : rc.Nombre,
                _URLGateway != string.Empty ? ConsultaClienteCRM(rc.Cliente, _URLGateway) : rc.Nombre,
                rc.Factura,
                rc.Ffactura,
                rc.Foliofactura,
                rc.Concepto,
                rc.Total
                /*rc.PedidoReferencia,
                //rc.Foliofacturaserie,
                rc.AñoPedido,
                rc.CelulaPedido,
                rc.Cliente,
                rc.Nombre,
                rc.FMovimiento,
                rc.Total,
                rc.Concepto*/
                );
        }
        HttpContext.Current.Session["FACTURAS_CONSULTAR"] = tblPedidos;
    }

    private void LlenaGridViewPedidos() //Llena el gridview dePedidos
    {
        DataTable tablaReferenciasP = (DataTable)HttpContext.Current.Session["TAB_PEDIDOS"];
        //grvPedidos.PageIndex = 0;
        grvPedidos.DataSource = tablaReferenciasP;
        grvPedidos.DataBind();
    }
    protected void grvPedidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvPedidos.PageIndex = e.NewPageIndex;
            LlenaGridViewPedidos();
        }
        catch (Exception)
        {
        }
    }

    protected void grvFacturasManuales_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvPedidosFacturados.PageIndex = e.NewPageIndex;
            LlenaGridViewFacturasManuales();
        }
        catch (Exception)
        {
        }
    }
    protected void btnBuscarPedido_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnBuscarCliente = sender as ImageButton;
            GridView grvMCE = (GridView)btnBuscarCliente.Parent.Parent.Parent.Parent;
            GridViewRow grvRMCE = (GridViewRow)btnBuscarCliente.Parent.Parent;
            GridViewRow grvRCC = (GridViewRow)btnBuscarCliente.Parent.Parent.Parent.Parent.Parent.Parent;
            //int folioConciliacion = Convert.ToInt32(grvConciliacionCompartida.DataKeys[grv.RowIndex].Values["FolioConciliacion"]);
            //int corporativoConciliacion = Convert.ToInt32(grvConciliacionCompartida.DataKeys[grv.RowIndex].Values["CorporativoConciliacion"]);
            //int sucursalConciliacion = Convert.ToInt32(grvConciliacionCompartida.DataKeys[grv.RowIndex].Values["SucursalConciliacion"]);
            //int añoConciliacion = Convert.ToInt32(grvConciliacionCompartida.DataKeys[grv.RowIndex].Values["AñoConciliacion"]);
            //short mesConciliacion = Convert.ToSByte(grvConciliacionCompartida.DataKeys[grv.RowIndex].Values["MesConciliacion"]);
            //int folio = Convert.ToInt32(grvConciliacionCompartida.DataKeys[grv.RowIndex].Values["Folio"]); ;
            //int sucursal = Convert.ToInt32(grvConciliacionCompartida.DataKeys[grv.RowIndex].Values["Sucursal"]);
            //int año = Convert.ToInt32(grvConciliacionCompartida.DataKeys[grv.RowIndex].Values["Año"]);
            //int secuencia = Convert.ToInt32(grvConciliacionCompartida.DataKeys[grv.RowIndex].Values["Secuencia"]);
            int secuenciarelacion = Convert.ToInt32(grvMCE.DataKeys[grvRMCE.RowIndex].Values["SecuenciaRelacion"]);
            
            decimal deposito = decimal.Parse((grvRCC.FindControl("lblDeposito") as Label).Text, NumberStyles.Currency);
            //int corporativo = Convert.ToInt32(grvConciliacionCompartida.DataKeys[grvRCC.RowIndex].Values["CorporativoConciliacion"].ToString());
            //int sucursal = Convert.ToInt32(grvConciliacionCompartida.DataKeys[grvRCC.RowIndex].Values["SucursalConciliacion"].ToString());
            int corporativo = Convert.ToInt32(hdfCorporativo.Value);
            short sucursal = Convert.ToInt16(hdfSucursal.Value);

            DropDownList DDL = (grvRMCE.FindControl("ddlStatusConcepto") as DropDownList);
            int iStatus = Int32.Parse(DDL.SelectedItem.Value);
            this.hdfStatusConcepto.Value = iStatus.ToString();

            ReferenciaNoConciliada referencia = LeerReferenciaConciliadaCompartida(grvRCC.RowIndex);
            listMovimientosConciliadosEx =
                       referencia.ListaReferenciaConciliadaCompartida;
            Int64 clienteBuscar = 0;
            //Leer y Guardar en Memoria el MovimientoSeleccionado.
            listMovimientos = Session["MOVIMIENTOS"] as List<ReferenciaNoConciliada>;
            movSeleccionado = listMovimientosConciliadosEx.Single(x => x.Corporativo == referencia.Corporativo &&
                                                        x.SucursalConciliacion == referencia.SucursalConciliacion &&
                                                        x.AñoConciliacion == referencia.AñoConciliacion &&
                                                        x.MesConciliacion == referencia.MesConciliacion &&
                                                        x.FolioConciliacion == referencia.FolioConciliacion &&
                                                        x.CorporativoExterno == referencia.Corporativo &&
                                                        x.SucursalExterno == referencia.Sucursal &&
                                                        x.AñoExterno == referencia.Año &&
                                                        x.FolioExterno == referencia.Folio &&
                                                        x.SecuenciaExterno == referencia.Secuencia &&
                                                        x.SecuenciaRelacion == secuenciarelacion);
            Session["MOVIMIENTO_SELECCIONADO"] = movSeleccionado;
            try
            {
                clienteBuscar = Convert.ToInt64((grvRMCE.FindControl("txtCliente") as TextBox).Text.Trim());
                hdfClienteBuscar.Value = clienteBuscar.ToString();
            }
            catch (FormatException)
            {
                App.ImplementadorMensajes.MostrarMensaje("Cliente no es valido, verifique su consulta.");
                return;
            }
            catch (OverflowException)
            {
                App.ImplementadorMensajes.MostrarMensaje("Cliente no es valido, verifique su consulta.");
                return;
            }

            Conciliacion.RunTime.DatosSQL.Conexion conexion = new Conciliacion.RunTime.DatosSQL.Conexion();
            try
            {
                conexion.AbrirConexion(true);

                Cliente objCliente = Conciliacion.RunTime.App.Cliente.CrearObjeto();
                objCliente.Referencia = clienteBuscar.ToString();
                if (objCliente.ValidaClienteExiste(conexion))
                {
                    if (iStatus == 28) // 28 = Deposito por pago anticipado
                    {
                        bool clienteValido = ValidarClientePagoAnticipado(conexion, clienteBuscar);

                        if (!clienteValido)
                            throw new Exception("El cliente indicado no existe en la relación de pagos " +
                                                "anticipados o se encuentra inactivo.");
                    }

                    BuscarPedidosClientes();
                }
                else
                    throw new Exception("El número de cliente introducido no existe, por favor verifique");
            }
            finally
            {
                conexion.CerrarConexion();
            }

            //mpeTipoCliente.Show(); MOD: SALTAR PROCESO DE SELECCION

            /*      Asignar propiedades de Carga archivo Excel      */
            ActualizarControl_CargaArchivo(Convert.ToInt32(hdfCuentaBancaria.Value.ToString().Replace(" ", "")), deposito, clienteBuscar, corporativo, sucursal);

        }
        catch (Exception ex)
        {
            this.hfVisibleConciliar.Value = "";     // No mostrar Popup popUpConciliarMovPedido
            this.popUpConciliarMovPedido.Hide();
            App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }
    }

    /// <summary>
    /// Verifica que el cliente se encuentra en la tabla ClientePagoAnticipado
    /// </summary>
    private bool ValidarClientePagoAnticipado(Conexion conexion, Int64 Cliente)
    {
        try
        {
            PagoAnticipado Pago = Conciliacion.RunTime.App.PagoAnticipado.CrearObjeto();
            return Pago.ValidarClientePagoAnticipado(conexion, Cliente);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private bool RegistraConciliacionReferencia(Conexion conexion, ReferenciaConciliadaCompartida Referencia)
    {
        try
        {
            usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];

            PagoAnticipado Pago = Conciliacion.RunTime.App.PagoAnticipado.CrearObjeto();
            Pago.CorporativoConciliacion        = Referencia.Corporativo;
            Pago.SucursalConciliacion           = Referencia.SucursalConciliacion;
            Pago.AñoConciliacion                = Referencia.AñoConciliacion;
            Pago.MesConciliacion                = Referencia.MesConciliacion;
            Pago.FolioConciliacion              = Referencia.FolioConciliacion;
            Pago.SecuenciaRelacion              = Referencia.SecuenciaRelacion;
            Pago.CorporativoExteno              = Referencia.Corporativo;
            Pago.SucursalExterno                = Referencia.Sucursal;
            Pago.AñoExterno                     = Referencia.Año;
            Pago.FolioExterno                   = Referencia.Folio;
            Pago.SecuenciaExterno               = Referencia.Secuencia;
            //Pago.MontoExterno                   = Referencia.Deposito;
            Pago.MontoExterno                   = Referencia.MontoExterno;
            Pago.FormaConciliacion              = Referencia.FormaConciliacion;
            Pago.ComentarioNoConciliado         = "";
            Pago.Usuario                        = usuario.IdUsuario.Trim();
            Pago.FAlta                          = DateTime.Now;
            Pago.Descripcion                    = Referencia.Descripcion;
            Pago.MotivoNoConciliado             = 0;
            //Pago.UsuarioStatusConcepto;
            //Pago.FStatusConcepto;
            //Pago.StatusConciliacion;
            return Pago.RegistraConciliacionReferencia(conexion);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private List<GridViewRow> ObtenerFacturasSeleccionadas()
    {
        List<GridViewRow> facturasSeleccionadas = new List<GridViewRow>();
        listaReferenciaFacturaConsulta = Session["FACTURAS_CONSULTAR_LISTA"] as List<ReferenciaNoConciliadaPedido>;

        if (listaReferenciaFacturaConsulta != null && listaReferenciaFacturaConsulta.Count > 0)
        {
            facturasSeleccionadas =
                grvPedidosFacturados.Rows.Cast<GridViewRow>()
                           .Where(
                               fila =>
                               fila.RowType == DataControlRowType.DataRow &&
                               (fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked))
                           .ToList();
        }
        return facturasSeleccionadas;
    }
    
    private bool RegistraPagoAnticipado(Conexion conexion, ReferenciaConciliadaCompartida rfExterno, decimal monto)
    {
        const int TIPOMOVIMIENTO = 3;       // Pago anticipado
        int iCliente = 0;
        
        SaldoAFavor obSaldoAFavor = Conciliacion.RunTime.App.SaldoAFavor.CrearObjeto();
        
        int.TryParse(hdfClienteBuscar.Value, out iCliente);
        if (iCliente <= 0) return false;
        if (monto <= 0) return false;
            
        obSaldoAFavor.FolioMovimiento           = -1;      // Se genera un consecutivo automáticamente
        obSaldoAFavor.AñoMovimiento             = DateTime.Now.Year;
        obSaldoAFavor.TipoMovimientoAConciliar  = TIPOMOVIMIENTO;
        obSaldoAFavor.Factura                   = 0;
        obSaldoAFavor.Monto                     = monto;
        obSaldoAFavor.StatusMovimiento          = "REGISTRADO";
        obSaldoAFavor.FMovimiento               = rfExterno.FOperacion;

        obSaldoAFavor.StatusConciliacion        = "CONCILIADA";
        obSaldoAFavor.FConciliacion             = DateTime.Now;
        obSaldoAFavor.CorporativoConciliacion   = rfExterno.CorporativoConciliacion;
        obSaldoAFavor.SucursalConciliacion      = rfExterno.SucursalConciliacion;
        obSaldoAFavor.AñoConciliacion           = rfExterno.AñoConciliacion;
        obSaldoAFavor.MesConciliacion           = rfExterno.MesConciliacion;
        obSaldoAFavor.FolioConciliacion         = rfExterno.FolioConciliacion;

        obSaldoAFavor.CorporativoExterno        = rfExterno.CorporativoExterno;
        obSaldoAFavor.SucursalExterno           = rfExterno.SucursalExterno;
        obSaldoAFavor.AñoExterno                = rfExterno.AñoExterno;
        obSaldoAFavor.FolioExterno              = rfExterno.FolioExterno;
        obSaldoAFavor.SecuenciaExterno          = rfExterno.SecuenciaExterno;
        obSaldoAFavor.Cliente                   = iCliente;

        return obSaldoAFavor.Guardar(conexion);
    }

    private void ActualizarControl_CargaArchivo(int cuenta, decimal montoPago, Int64 cliente, int corporativo, short sucursal)
    {
        wucCargaExcelCyC.CuentaBancaria = cuenta;
        wucCargaExcelCyC.MontoPago = montoPago;
        wucCargaExcelCyC.ClienteReferencia = cliente;
        wucCargaExcelCyC.Corporativo = corporativo;
        wucCargaExcelCyC.DispersionAutomatica = true;
        wucCargaExcelCyC.Sucursal = sucursal;
    }

    public bool ConciliarFactura(Conexion conexion, ReferenciaConciliadaCompartida rfExterna)
    {
        bool resultado = false;
        try
        {
            usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];

            listaReferenciaFacturaConsulta = Session["FACTURAS_CONSULTAR_LISTA"] as List<ReferenciaNoConciliadaPedido>;
            if (listaReferenciaFacturaConsulta != null && listaReferenciaFacturaConsulta.Count > 0)
            {
                List<GridViewRow> pedidosSeleccionados =
                    grvPedidosFacturados.Rows.Cast<GridViewRow>()
                               .Where(
                                   fila =>
                                   fila.RowType == DataControlRowType.DataRow &&
                                   (fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked))
                               .ToList();
                if (pedidosSeleccionados.Count == 0)
                {
                    throw new Exception("No ha seleccionado ninguna factura. Verifique su selección.");
                }
                // Asignar FormaConciliacion = Cociliación manual
                listaReferenciaFacturaConsulta.Select(s => { s.FormaConciliacion = 5; return s; }).ToList();

                foreach (GridViewRow row in pedidosSeleccionados)
                {
                    int factura = Convert.ToInt32(grvPedidosFacturados.DataKeys[row.RowIndex].Values["Factura"].ToString());

                    ReferenciaNoConciliadaPedido rfFactura =
                        listaReferenciaFacturaConsulta.Single(s => s.Factura == factura);
                    decimal montoInterno            = rfFactura.Total;
                    string concepto                 = rfFactura.Concepto;

                    FacturaManual facturaManual = new FacturaManualDatos(Conciliacion.RunTime.App.ImplementadorMensajes);
                    byte corporativoConciliacion    = Convert.ToByte(rfExterna.CorporativoConciliacion);
                    byte sucursalConciliacion       = Convert.ToByte(rfExterna.SucursalConciliacion);
                    int añoConciliacion             = rfExterna.AñoConciliacion;
                    short mesConciliacion           = rfExterna.MesConciliacion;
                    int folioConciliacion           = rfExterna.FolioConciliacion;
                    int secuenciaRelacion           = rfExterna.SecuenciaRelacion;
                    byte corporativoExterno         = Convert.ToByte(rfExterna.CorporativoExterno);
                    byte sucursalExterno            = Convert.ToByte(rfExterna.SucursalExterno);
                    int añoExterno                  = rfExterna.AñoExterno;
                    int folioExterno                = rfExterna.FolioExterno;
                    int secuenciaExterno            = rfExterna.SecuenciaExterno;
                    decimal montoConciliado         = rfExterna.MontoConciliado;
                    decimal montoExterno            = rfExterna.MontoExterno;
                    short formaConciliacion         = rfExterna.FormaConciliacion = 5;
                    short statusConcepto            = rfExterna.StatusConcepto;
                    //string statusConciliacion       = rfExterna.StatusConciliacion;
                    string statusConciliacion       = "CONCILIADA";
                    DateTime fAlta                  = DateTime.Now;
                    DateTime fStatusConcepto        = DateTime.Now;
                    //string statusMovimiento         = rfExterna.StatusConciliacionMovimiento;
                    string statusMovimiento         = "CONCILIADA";
                                                                                     
                    resultado = facturaManual.Guardar(conexion, corporativoConciliacion, sucursalConciliacion, añoConciliacion,
                                        mesConciliacion, folioConciliacion, secuenciaRelacion, factura, corporativoExterno,
                                        sucursalExterno, añoExterno, folioExterno, secuenciaExterno, concepto, montoConciliado,
                                        montoExterno, montoInterno, formaConciliacion, statusConcepto, statusConciliacion, statusMovimiento,
                                        usuario.IdUsuario.Trim(), fAlta, "", usuario.IdUsuario.Trim(), fStatusConcepto);

                    if (!resultado) break;
                }
            }        
            else
            {
                throw new Exception("No se ha cargado ninguna factura.");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return resultado;
    }

    protected void btnConciliarFACT_Click(object sender, EventArgs e)
    {
        decimal montoFacturas = 0M;
        bool conciliacionFactura = false;
        bool registraConciliacion = false;
        bool registraPagoAnticipado = false;
        const string ERROR_CONCILIACION = "Ocurrió un error conciliando la(s) factura(s). Comuníquese con el área de sistemas.";
        Conexion conexion = new Conexion();
        try
        {
            if (grvPedidosFacturados.Rows.Count > 0)
            {
                if (!VerificarTotalFacturas() && hdfStatusConcepto.Value == "28")
                {
                    throw new Exception("El total de la(s) factura(s) debe ser igual al monto del desposito");
                }

                //if (!RecuperarReferencias_wucCargaExcel()) return;
                ReferenciaConciliadaCompartida rnc = Session["MOVIMIENTO_SELECCIONADO"] as ReferenciaConciliadaCompartida;
                rnc.BorrarReferenciaConciliada();

                montoFacturas = Convert.ToDecimal(hdfTotalFacturas.Value);

                conexion.AbrirConexion(true);
                conciliacionFactura = ConciliarFactura(conexion, rnc);
                if (hdfStatusConcepto.Value == "28")
                {
                    registraConciliacion = RegistraConciliacionReferencia(conexion, rnc);
                    registraPagoAnticipado = RegistraPagoAnticipado(conexion, rnc, montoFacturas);

                    if (!conciliacionFactura || !registraConciliacion || !registraPagoAnticipado)
                    {
                        throw new Exception(ERROR_CONCILIACION);
                    }
                }

                if (!conciliacionFactura)
                {
                    throw new Exception(ERROR_CONCILIACION);
                }

                conexion.CommitTransaction();

                mpeBusquedaFactura.Hide(); mpeBusquedaFactura.Dispose();
                popUpConciliarMovPedido.Hide(); popUpConciliarMovPedido.Dispose();

                //      Recargar la Vista con los datos
                if (FiltroCorrecto())
                {
                    usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
                    GrupoConciliacionUsuario gcu = LeerGrupoConciliacionUsuarioEspecifico(usuario.IdUsuario.Trim());
                    ConsultaMovimientosConciliacionCompartida(gcu.AccesoTotal,
                        Convert.ToInt16(ddlEmpresa.SelectedItem.Value),
                        Convert.ToInt16(ddlSucursal.SelectedItem.Value),
                        ddlCuentaBancaria.SelectedItem.Text.Trim(),
                         Convert.ToDateTime(txtFInicial.Text),
                         Convert.ToDateTime(txtFFinal.Text)
                        );
                    GenerarTablaConciliacionCompartida();
                    LlenaGridViewConciliacionCompartida();
                }

                App.ImplementadorMensajes.MostrarMensaje("La factura se concilió exitosamente.");
            }
            else
            {
                App.ImplementadorMensajes.MostrarMensaje("Para conciliar una factura manual primero debe buscarla por criterio de fecha o por su identificador.");
            }
        }
        catch (Exception ex)
        {
            conexion.RollBackTransaction();
            App.ImplementadorMensajes.MostrarMensaje("Error: " + ex.Message);
        }
        finally
        {
            conexion.CerrarConexion();
            LimpiarCamposFacturas();
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            ReferenciaConciliadaCompartida rnc = Session["MOVIMIENTO_SELECCIONADO"] as ReferenciaConciliadaCompartida;
            rnc.BorrarReferenciaConciliada();
            if (!ConciliarMovExterno(rnc)) return;
            popUpConciliarMovPedido.Hide(); popUpConciliarMovPedido.Dispose();
            App.ImplementadorMensajes.MostrarMensaje("Movimiento Conciliado con Éxito.");
            //Recargar la Vista con los datos
            if (FiltroCorrecto())
            {
                usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
                GrupoConciliacionUsuario gcu = LeerGrupoConciliacionUsuarioEspecifico(usuario.IdUsuario.Trim());
                ConsultaMovimientosConciliacionCompartida(gcu.AccesoTotal,
                    Convert.ToInt16(ddlEmpresa.SelectedItem.Value),
                    Convert.ToInt16(ddlSucursal.SelectedItem.Value),
                    ddlCuentaBancaria.SelectedItem.Text.Trim(),
                     Convert.ToDateTime(txtFInicial.Text),
                     Convert.ToDateTime(txtFFinal.Text)
                    );
                GenerarTablaConciliacionCompartida();
                LlenaGridViewConciliacionCompartida();
            }

            else
                App.ImplementadorMensajes.MostrarMensaje("Dato Incorrecto: [" + mensaje + "].\nVerifique su Selección");

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error al obtener el movimiento seleccionado.\n[" + ex.Message + "]\nRefresque nuevamente la vista");
        }
    }


    //protected void btnBuscarPedidosCliente_Click(object sender, EventArgs e)
    protected void BuscarPedidosClientes()
    {
        try
        {
            string clienteBuscar = hdfClienteBuscar.Value;
            ClienteAuxiliar objCliente = new ClienteAuxiliar();
            objCliente = Consulta_ClienteValido(clienteBuscar);

            if (objCliente.Cliente == 0) return;

            //ScriptManager.RegisterClientScriptBlock(this.upConciliacionCompartida,
            //                                upConciliacionCompartida.GetType(),
            //                                "Scroll",
            //                                "grvPedidosScroll()",
            //                                true);
            ReferenciaConciliadaCompartida rfc = Session["MOVIMIENTO_SELECCIONADO"] as ReferenciaConciliadaCompartida;
            lblMontoExterno.Text = lblMontoResto.Text = rfc.MontoExterno.ToString("C2");//Colocar el monto seleccionado en el label del MOdal

            Consulta_Pedidos(rfc.CorporativoConciliacion, rfc.SucursalConciliacion, rfc.AñoConciliacion, rfc.MesConciliacion,
                rfc.FolioConciliacion,
                rfc.FolioExterno, rfc.SecuenciaExterno, objCliente.Cliente.ToString(), rdbTipoCliente.SelectedItem.Value.Equals("PADRE"));

            GenerarTablaPedidos();
            LlenaGridViewPedidos();

            popUpConciliarMovPedido.Show();

            //mpeTipoCliente.Hide();
            //mpeTipoCliente.Dispose();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Ha ocurrido un error al leer los pedidos del Cliente.\nMensaje: " + ex.Message);
            popUpConciliarMovPedido.Hide();
            popUpConciliarMovPedido.Dispose();

        }
    }


    protected void btnBuscarPedidosCliente_Click(object sender, EventArgs e)
    {
        try
        {
            string clienteBuscar = hdfClienteBuscar.Value;
            ClienteAuxiliar objCliente = new ClienteAuxiliar();
            objCliente = Consulta_ClienteValido(clienteBuscar);
            if (objCliente.Cliente == 0) return;

            //ScriptManager.RegisterClientScriptBlock(this.upConciliacionCompartida,
            //                                upConciliacionCompartida.GetType(),
            //                                "Scroll",
            //                                "grvPedidosScroll()",
            //                                true);
            ReferenciaConciliadaCompartida rfc = Session["MOVIMIENTO_SELECCIONADO"] as ReferenciaConciliadaCompartida;
            lblMontoExterno.Text = lblMontoResto.Text = rfc.MontoExterno.ToString("C2");//Colocar el monto seleccionado en el label del MOdal
            
            //Consulta_Pedidos(rfc.CorporativoConciliacion, rfc.SucursalConciliacion, rfc.AñoConciliacion, rfc.MesConciliacion,
            //    rfc.FolioConciliacion,
            //    rfc.FolioExterno, rfc.SecuenciaExterno, clienteBuscar, rdbTipoCliente.SelectedItem.Value.Equals("PADRE"));
            
            GenerarTablaPedidos();
            LlenaGridViewPedidos();
            
            popUpConciliarMovPedido.Show();

            mpeTipoCliente.Hide();
            mpeTipoCliente.Dispose();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Ha ocurrido un error al leer los pedidos del Cliente.\nMensaje: " + ex.Message);
            popUpConciliarMovPedido.Hide();
            popUpConciliarMovPedido.Dispose();

        }
    }
    protected void imgBuscarPedido_Click(object sender, EventArgs e)
    {
        try
        {
            string clienteBuscar = hdfClienteBuscar.Value;

            //ScriptManager.RegisterClientScriptBlock(this.upConciliacionCompartida,
            //                                upConciliacionCompartida.GetType(),
            //                                "Scroll",
            //                                "grvPedidosScroll()",
            //                                true);

            ReferenciaConciliadaCompartida rfc = Session["MOVIMIENTO_SELECCIONADO"] as ReferenciaConciliadaCompartida;
            lblMontoExterno.Text = lblMontoResto.Text = rfc.MontoExterno.ToString("C2");//Colocar el monto seleccionado en el label del MOdal
            //Consulta_Pedidos(rfc.CorporativoConciliacion, rfc.SucursalConciliacion, rfc.AñoConciliacion, rfc.MesConciliacion,
            //    rfc.FolioConciliacion,
            //    rfc.FolioExterno, rfc.SecuenciaExterno, clienteBuscar, rblClienteTipo.SelectedItem.Value.Equals("PADRE"));

            Consulta_PedidosFactura(rfc.CorporativoConciliacion, rfc.SucursalConciliacion, rfc.AñoConciliacion, rfc.MesConciliacion,
                rfc.FolioConciliacion,
                rfc.FolioExterno, 
                rfc.SecuenciaExterno, 
                clienteBuscar, 
                rblClienteTipo.SelectedItem.Value.Equals("PADREL"), 
                txtFactura.Text,
                //(txtFechaFactura.Text == "" ? DateTime.MinValue : Convert.ToDateTime(txtFechaFactura.Text))                
                (wucRangoFechas.FFinal.ToString() == "" ? DateTime.MinValue : wucRangoFechas.FFinal)
                );

            GenerarTablaPedidos();
            LlenaGridViewPedidos();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Ha ocurrido un error al leer los pedidos del Cliente.\nMensaje: " + ex.Message);
            popUpConciliarMovPedido.Hide();
            popUpConciliarMovPedido.Dispose();

        }
    }


    /*
    protected void imgBuscarPedido_Click(object sender, EventArgs e)
    {
        try
        {
            string clienteBuscar = hdfClienteBuscar.Value;
            
            //ScriptManager.RegisterClientScriptBlock(this.upConciliacionCompartida,
            //                                upConciliacionCompartida.GetType(),
            //                                "Scroll",
            //                                "grvPedidosScroll()",
            //                                true);

            ReferenciaConciliadaCompartida rfc = Session["MOVIMIENTO_SELECCIONADO"] as ReferenciaConciliadaCompartida;
            lblMontoExterno.Text =lblMontoResto.Text= rfc.MontoExterno.ToString("C2");//Colocar el monto seleccionado en el label del MOdal
            Consulta_Pedidos(rfc.CorporativoConciliacion, rfc.SucursalConciliacion, rfc.AñoConciliacion, rfc.MesConciliacion,
                rfc.FolioConciliacion,
                rfc.FolioExterno, rfc.SecuenciaExterno, clienteBuscar, rdbTipoCliente.SelectedItem.Value.Equals("PADRE"));
            GenerarTablaPedidos();
            LlenaGridViewPedidos();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Ha ocurrido un error al leer los pedidos del Cliente.\nMensaje: " + ex.Message);
            popUpConciliarMovPedido.Hide();
            popUpConciliarMovPedido.Dispose();

        }
    }*/

    

    //Genera codigo JavaScript que copiara el contenido del campo Control1
    //al campo Control2 en el momento que el usuario salga (onblur) del Control1
    public string ColocarTexto(Control txtDI_ID, Control txtDTI_ID)
    {

        StringBuilder script = new StringBuilder();
        string txtDI = txtDI_ID.ClientID;
        string txtDIT = txtDTI_ID.ClientID;
        script.Append("document.getElementById('");
        script.Append(txtDI);
        script.Append("').value = ");
        script.Append("document.getElementById('");
        script.Append(txtDIT);
        script.Append("').value;");
        return script.ToString();
    }

    public bool ConciliarMovExterno(ReferenciaConciliadaCompartida rfExterna)
    {
        bool resultado = false;
        try
        {

            listaReferenciaPedidos = Session["PEDIDOS_CONCILIAR"] as List<ReferenciaNoConciliadaPedido>;
            List<GridViewRow> pedidosSeleccionados =
                grvPedidos.Rows.Cast<GridViewRow>()
                           .Where(
                               fila =>
                               fila.RowType == DataControlRowType.DataRow &&
                               (fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked))

                           .ToList();
            if (pedidosSeleccionados.Count <= 0)
            {
                App.ImplementadorMensajes.MostrarMensaje("No ha seleccionado ningún Pedido. Verifique su selección.");
                return false;
            }
            foreach (GridViewRow grv in pedidosSeleccionados)
            {
                int pedido = Convert.ToInt32(grvPedidos.DataKeys[grv.RowIndex].Values["Pedido"]);
                int celulaPedido = Convert.ToInt32(grvPedidos.DataKeys[grv.RowIndex].Values["Celula"]);
                int añoPedido = Convert.ToInt32(grvPedidos.DataKeys[grv.RowIndex].Values["AñoPed"]);

                ReferenciaNoConciliadaPedido rfPedido =
                listaReferenciaPedidos.Single(
                   s => s.Pedido == pedido && s.CelulaPedido == celulaPedido && s.AñoPedido == añoPedido);
                if (!rfExterna.AgregarReferenciaConciliadaSinVerificacion(rfPedido)) return false;
            }

            resultado = rfExterna.GuardarReferenciaConciliada();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Ha ocurrido un conflicto al Conciliar el Movimiento.\n" + ex.Message + "\nRecargue su selección.");
            resultado = false;
        }
        return resultado;
    }

    /// ******************************
    /// Elimina de la lista, elementos duplicados por CorporativoExterno,SucursalExterno,AñoExterno
    /// FolioExterno,SecuenciaExterno.
    public List<ReferenciaNoConciliada> LeerListaReferenciasNoRepetidos(List<ReferenciaNoConciliada> listaMovimientos)
    {
        //var nuevaLista= listaMovimientos.GroupBy(m=>new { m.Corporativo, m.Sucursal, m.Año,m.Folio,m.Secuencia });
        List<ReferenciaNoConciliada> nuevaLista = new List<ReferenciaNoConciliada>();
        foreach (ReferenciaNoConciliada v in listaMovimientos)
        {
            if (v.StatusConciliacion.Equals("CONCILIADA") && !nuevaLista.Exists(x => x.Corporativo == v.Corporativo && x.Sucursal == v.Sucursal
                                        && x.Año == v.Año && x.Folio == v.Folio && x.Secuencia == v.Secuencia))
                nuevaLista.Add(v);
            else if
                (!nuevaLista.Exists(x => x.Corporativo == v.Corporativo && x.Sucursal == v.Sucursal
                                        && x.Año == v.Año && x.Folio == v.Folio && x.Secuencia == v.Secuencia))

                nuevaLista.Add(v);
        }

        return nuevaLista;
    }
    protected void btnFiltrar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            short configuracion = ddlFiltrarEn.SelectedItem.Value.Equals("Externos") ? Convert.ToSByte(4) : Convert.ToSByte(5);
            cargar_ComboCampoFiltroDestino(configuracion);
            enlazarComboCampoFiltrarDestino();
            InicializarControlesFiltro();
            mpeFiltrar.Show();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Ocurrió un problema al cargar los datos para el Filtro.\nError: " + ex.Message);
        }

    }

    /// <summary>
    /// Verifíca que el monto de las facturas seleccionadas sea igual 
    /// al monto del externo. En caso contrario regresa falso
    /// </summary>
    /// <returns></returns>
    private bool VerificarTotalFacturas()
    {
        decimal totalFacturas = 0M;
        bool resultado = false;

        List<GridViewRow> facturas = ObtenerFacturasSeleccionadas();
        ReferenciaConciliadaCompartida rncExterno = Session["MOVIMIENTO_SELECCIONADO"] as ReferenciaConciliadaCompartida;

        if ((facturas != null && facturas.Count > 0)
            && (listaReferenciaFacturaConsulta != null && listaReferenciaFacturaConsulta.Count > 0))
        {
            foreach (GridViewRow row in facturas)
            {
                int iFactura = Convert.ToInt32(grvPedidosFacturados.DataKeys[row.RowIndex].Values["Factura"].ToString());

                ReferenciaNoConciliadaPedido rnpFactura =
                    listaReferenciaFacturaConsulta.Single(s => s.Factura == iFactura);

                totalFacturas += rnpFactura.Total;
            }

            if (rncExterno != null)
            {
                resultado = totalFacturas == rncExterno.MontoExterno;
                if (resultado)
                {
                    hdfTotalFacturas.Value = totalFacturas.ToString();
                }
                else
                {
                    hdfTotalFacturas.Value = "0";
                }
            }
        }
        return resultado;
    }

    //protected void chkSeleccionado_CheckedChanged(object sender, EventArgs e)
    //{
    //    decimal mMonto, mMontoExterno, mResto;
    //    mMonto = mMontoExterno = mResto = 0M;
    //    listaReferenciaPedidos = Session["PEDIDOS_CONCILIAR"] as List<ReferenciaNoConciliadaPedido>;

    //    /*          LEER MONTO EXTERNO          */
    //    ReferenciaConciliadaCompartida rfc = Session["MOVIMIENTO_SELECCIONADO"] as ReferenciaConciliadaCompartida;
    //    mMontoExterno = rfc.MontoExterno;

    //    List<GridViewRow> pedidosSeleccionados =
    //        grvPedidos.Rows.Cast<GridViewRow>()
    //                   .Where(
    //                       fila =>
    //                       fila.RowType == DataControlRowType.DataRow &&
    //                       (fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked))
    //                   .ToList();
    //    if (pedidosSeleccionados.Count <= 0)
    //    {
    //        lblMontoResto.Text = lblMontoExterno.Text;
    //        return;
    //    }
    //    foreach (GridViewRow grv in pedidosSeleccionados)
    //    {
    //        mMonto += decimal.Parse(grvPedidos.DataKeys[grv.RowIndex].Values["Total"].ToString(), NumberStyles.Currency);
    //    }

    //    mResto = mMontoExterno - mMonto;
    //    lblMontoResto.Text = mResto.ToString("C");
    //}
}

