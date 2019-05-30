using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Conciliacion.RunTime.DatosSQL;
using CrystalDecisions.Shared;
using SeguridadCB.Public;
using Conciliacion.RunTime;
using Conciliacion.RunTime.ReglasDeNegocio;
using System.Collections.Generic;
using System.IO;

public partial class Inicio : System.Web.UI.Page
{
    #region "Propiedades Globales"
    private SeguridadCB.Public.Operaciones operaciones;
    private SeguridadCB.Public.Usuario usuario;
    #endregion


    #region "Propiedades privadas"
    private List<ListaCombo> listAñoConciliacion = new List<ListaCombo>();
    private List<ListaCombo> listTipoConciliacion = new List<ListaCombo>();
    private List<ListaCombo> listStatusConciliacion = new List<ListaCombo>();
    private List<ListaCombo> listGrupoConciliacion = new List<ListaCombo>();
    private List<ListaCombo> listSucursales = new List<ListaCombo>();
    public List<cConciliacion> listaConciliaciones = new List<cConciliacion>();
    private DataTable tblConciliaciones = new DataTable("Conciliaciones");
    public int tipoConciliacion;
    public string statusConciliacion;

    #endregion


    #region "Eventos de la forma"
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
            Conciliacion.RunTime.App.ImplementadorMensajes.ContenedorActual = this;

            if (HttpContext.Current.Request.UrlReferrer != null)
            {
                if ((!HttpContext.Current.Request.UrlReferrer.AbsoluteUri.Contains("SitioConciliacion")) || (HttpContext.Current.Request.UrlReferrer.AbsoluteUri.Contains("Inicio.aspx")))
                {
                    HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
                    HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
                    Response.Cache.SetExpires(DateTime.Now);
                }
            }
            //wucBuscadorPagoEstadoCuenta.Contenedor = mpeBuscadorPagoEdoCta;
            if (!Page.IsPostBack)
            {
                usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
                //Inicializacion de Propiedades
                //listaConciliaciones = new List<cConciliacion>();

                

                Carga_TipoConciliacion(usuario.IdUsuario.Trim());
                if (ddlEmpresa.Items.Count == 0) Carga_Corporativo();
                Carga_GrupoConciliacion(usuario.IdUsuario.Trim());

                Carga_AñoConciliacion();
                Carga_StatusConciliacion();

                ddlMesConciliacion.SelectedValue = leerMes();
                ddlAñoConciliacion.SelectedValue = leerAño();                
               
                if (HttpContext.Current.Session["filtros"] != null)
                {

                    ClaseFiltros filtros = new ClaseFiltros();
                    filtros = (ClaseFiltros)HttpContext.Current.Session["filtros"];
                    ddlEmpresa.SelectedIndex = filtros.Empresa-1;
                    ddlSucursal.SelectedIndex = filtros.Sucursal;
                    ddlGrupo.SelectedIndex = filtros.Grupo-1;
                    ddlTipoConciliacion.SelectedIndex = filtros.TipoConciliacion;
                    ddlStatusConciliacion.SelectedIndex = filtros.Status;
                    ddlAñoConciliacion.SelectedIndex = filtros.Anio;
                    ddlMesConciliacion.SelectedIndex = filtros.Mes;                    
                    HttpContext.Current.Session.Remove("filtros");
                }
                Consulta_Conciliacion();
                GenerarTablaConciliaciones();
                LlenaGridViewConciliaciones();
                //wucBuscadorPagoEstadoCuenta.ActivaEstaConciliacion = false;
                txtFinicio.Text = (DateTime.Today.AddMonths(-1)).ToShortDateString();
                txtFfinal.Text = DateTime.Today.ToShortDateString();
            }
            Consulta_Conciliacion();
            this.ddlEmpresa.Focus();            
        }

        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }
    #endregion

    #region "Funciones privadas"
    /// <summary>
    /// Habilitar-Deshabilitar Opciones del MenuContextual
    /// </summary>
    public void habilitarOpcionesMenuContextual()
    {
        try
        {
            if (statusConciliacion.Equals("CONCILIACION CANCELADA"))
            {
                miMenu.Visible = false;
            }
            else
            {
                miMenu.Visible = true;

                lnkConsultarDoc.Attributes.Add("onclick", "return fnConsultar()");
                lnkConsultarDoc.Attributes.CssStyle.Add("opacity", "1");
                if (statusConciliacion.Equals("CONCILIACION ABIERTA") || statusConciliacion.Equals("CONCILIACION CERRADA"))
                {
                    lnkVerM.Attributes.Add("onclick", "return fnVerConciliacion()");
                    lnkVerM.Attributes.CssStyle.Add("opacity", "1");
                    /*lnkVerM.Attributes.Add("onclick", "return false");
                    lnkVerM.Attributes.CssStyle.Add("opacity", "0.7");*/

                    if (operaciones.EstaHabilitada(30, "Aplicar pagos"))//&& tipoConciliacion == 2)
                    {
                        lnkPagosM.Attributes.Add("onclick", "return fnPagos()");
                        lnkPagosM.Attributes.CssStyle.Add("opacity", "1");
                    }

                    //Validacion Informe
                    if (operaciones.EstaHabilitada(30, "Informe contabilidad") && (tipoConciliacion == 1 || tipoConciliacion == 2 || tipoConciliacion == 4 || tipoConciliacion == 6))
                    {
                        lnkInformeM.Attributes.Add("onclick", "return fnInforme()");
                        lnkInformeM.Attributes.CssStyle.Add("opacity", "1");
                    }
                }
                else
                {
                    lnkVerM.Attributes.Add("onclick", "return fnVerConciliacion()");
                    lnkVerM.Attributes.CssStyle.Add("opacity", "1");

                    lnkPagosM.Attributes.Add("onclick", "return false");
                    lnkPagosM.Attributes.CssStyle.Add("opacity", "0.7");

                    lnkInformeM.Attributes.Add("onclick", "return false");
                    lnkInformeM.Attributes.CssStyle.Add("opacity", "0.7");
                }
            }

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
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
    public void Carga_SucursalCorporativo(int corporativo, int tipoConciliacion)
    {
        try
        {
            listSucursales = Conciliacion.RunTime.App.Consultas.ConsultaSucursales(tipoConciliacion != 2 ? Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionIden0.Con0 : Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionIden0.Sin0, corporativo);
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
    /// Llena el Combo de Grupo Conciliacion
    /// </summary>
    public void Carga_GrupoConciliacion(string usuario)
    {
        try
        {
            listGrupoConciliacion = Conciliacion.RunTime.App.Consultas.ConsultaGruposConciliacion(Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionGrupo.Asignados, usuario);
            this.ddlGrupo.DataSource = listGrupoConciliacion;
            this.ddlGrupo.DataValueField = "Identificador";
            this.ddlGrupo.DataTextField = "Descripcion";
            this.ddlGrupo.DataBind();
            this.ddlGrupo.Dispose();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }
    }
    /// <summary>
    /// Llena el Combo de Tipo Conciliacion
    /// </summary>
    public void Carga_TipoConciliacion(string usuario)
    {
        try
        {
            listTipoConciliacion = Conciliacion.RunTime.App.Consultas.ConsultaTipoConciliacion(Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionGrupo.Asignados, usuario);
            this.ddlTipoConciliacion.DataSource = listTipoConciliacion;
            this.ddlTipoConciliacion.DataValueField = "Identificador";
            this.ddlTipoConciliacion.DataTextField = "Descripcion";
            this.ddlTipoConciliacion.DataBind();
            this.ddlTipoConciliacion.Dispose();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }
    }
    /// <summary>
    /// Llena el Combo de Año Conciliacion de los existentes en la tabla y el actual
    /// </summary>
    public void Carga_AñoConciliacion()
    {
        try
        {
            listAñoConciliacion = Conciliacion.RunTime.App.Consultas.ConsultaAños();
            this.ddlAñoConciliacion.DataSource = listAñoConciliacion;
            this.ddlAñoConciliacion.DataValueField = "Identificador";
            this.ddlAñoConciliacion.DataTextField = "Descripcion";
            this.ddlAñoConciliacion.DataBind();
            this.ddlAñoConciliacion.Dispose();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }
    }
    /// <summary>
    /// Lee mes actual
    /// </summary>
    public string leerMes()
    {
        DateTime fechaActual = Convert.ToDateTime(fechaMaximaConciliacion());
        return fechaActual.Month.ToString();
    }
    /// <summary>
    /// Lee mes año actual
    /// </summary>
    public string leerAño()
    {
        DateTime fechaActual = Convert.ToDateTime(fechaMaximaConciliacion());
        return fechaActual.Year.ToString();
    }
    /// <summary>
    /// Llena el Combo de los Status de Conciliación
    /// </summary>
    public void Carga_StatusConciliacion()
    {
        try
        {
            listStatusConciliacion = Conciliacion.RunTime.App.Consultas.ConsultaStatusConciliacion();
            this.ddlStatusConciliacion.DataSource = listStatusConciliacion;
            this.ddlStatusConciliacion.DataValueField = "Identificador";
            this.ddlStatusConciliacion.DataTextField = "Descripcion";
            this.ddlStatusConciliacion.DataBind();
            this.ddlStatusConciliacion.Dispose();
            ListaCombo c = listStatusConciliacion.Find(x => x.Descripcion == "CONCILIACION ABIERTA");
            if (c == null) return;
            this.ddlStatusConciliacion.SelectedValue = c.Identificador.ToString();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }
    }
    /// <summary>
    /// Genera la tabla de Conciliaciones
    /// </summary>
    public void GenerarTablaConciliaciones()
    {
        tblConciliaciones.Columns.Add("FolioConciliacion", typeof(int));
        tblConciliaciones.Columns.Add("CuentaBancaria", typeof(string));
        tblConciliaciones.Columns.Add("Banco", typeof(string));
        tblConciliaciones.Columns.Add("FInicial", typeof(DateTime));
        tblConciliaciones.Columns.Add("FFinal", typeof(DateTime));
        tblConciliaciones.Columns.Add("TransaccionesInternas", typeof(int));
        tblConciliaciones.Columns.Add("TransaccionesExternas", typeof(int));
        tblConciliaciones.Columns.Add("ConciliacionesExternas", typeof(int));
        tblConciliaciones.Columns.Add("ConciliacionesInternas", typeof(int));
        tblConciliaciones.Columns.Add("GrupoConciliacionstr", typeof(string));
        tblConciliaciones.Columns.Add("SucursalDes", typeof(string));
        tblConciliaciones.Columns.Add("StatusConciliacion", typeof(string));
        tblConciliaciones.Columns.Add("UbicacionIcono", typeof(string));
        tblConciliaciones.Columns.Add("Corporativo", typeof(int));
        tblConciliaciones.Columns.Add("Sucursal", typeof(int));
        tblConciliaciones.Columns.Add("Año", typeof(int));
        tblConciliaciones.Columns.Add("Mes", typeof(sbyte));
        tblConciliaciones.Columns.Add("TipoConciliacion", typeof(sbyte));

        foreach (cConciliacion c in listaConciliaciones)
        {
            tblConciliaciones.Rows.Add(c.Folio,
                c.CuentaBancaria,
                c.BancoStr,
                c.FInicial,
                c.FFinal,
                c.TransaccionesInternas,
                c.TransaccionesExternas,
                c.ConciliadasExternas,
                c.ConciliadasInternas,
                c.GrupoConciliacionStr,
                c.SucursalDes,
                c.StatusConciliacion,
                c.UbicacionIcono,
                c.Corporativo, c.Sucursal, c.Año, c.Mes, c.TipoConciliacion);
        }
        HttpContext.Current.Session["TBL_CONCILIACION"] = tblConciliaciones;
    }
    /// <summary>
    /// Llena el gridview con las conciliaciones antes leídas
    /// </summary>
    private void LlenaGridViewConciliaciones()
    {
        DataTable tablaConciliaciones = (DataTable)HttpContext.Current.Session["TBL_CONCILIACION"];
        grvConciliacion.DataSource = tablaConciliaciones;
        grvConciliacion.DataBind();
    }
    /// <summary>
    /// Lee el metodo que llena la lista con las conciliaciones
    /// </summary>
    public void Consulta_Conciliacion()
    {
        SeguridadCB.Seguridad seguridad = new SeguridadCB.Seguridad();
        System.Data.SqlClient.SqlConnection Connection = seguridad.Conexion;
        usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
        if (Connection.State == ConnectionState.Closed)
        {
            seguridad.Conexion.Open();
            Connection = seguridad.Conexion;
        }
        try
        {
            listaConciliaciones = Conciliacion.RunTime.App.Consultas.ConsultaConciliacion(
                        Convert.ToInt32(ddlEmpresa.SelectedItem.Value),
                        Convert.ToInt32(ddlSucursal.SelectedItem.Value),
                        Convert.ToInt32(this.ddlGrupo.SelectedItem.Value),
                        Convert.ToInt32(this.ddlAñoConciliacion.SelectedItem.Value),
                        Convert.ToSByte(this.ddlMesConciliacion.SelectedItem.Value),
                        Convert.ToSByte(this.ddlTipoConciliacion.SelectedItem.Value),
                        Convert.ToString(this.ddlStatusConciliacion.SelectedItem.Text),
                        usuario.IdUsuario.Trim());
            tipoConciliacion = Convert.ToSByte(this.ddlTipoConciliacion.SelectedItem.Value);
            statusConciliacion = Convert.ToString(this.ddlStatusConciliacion.SelectedItem.Text);
            habilitarOpcionesMenuContextual();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }

    }
    /// <summary>
    /// Obtiene Fecha Maxima Conciliacion
    /// </summary>
    public string fechaMaximaConciliacion()
    {
        return Conciliacion.RunTime.App.Consultas.ConsultaFechaActualInicial();
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

    #endregion

    #region "Funciones de formas"

    protected void ddlEmpresa_DataBound(object sender, EventArgs e)
    {
        Carga_SucursalCorporativo(Convert.ToInt32(ddlEmpresa.SelectedItem.Value), Convert.ToInt32(ddlTipoConciliacion.SelectedItem.Value));
    }
    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        Carga_SucursalCorporativo(Convert.ToInt32(ddlEmpresa.SelectedItem.Value), Convert.ToInt32(ddlTipoConciliacion.SelectedItem.Value));
    }
    protected void btnNuevaConciliacion_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Conciliacion/NuevaConciliacion.aspx");
    }
    protected void lnkVer_Click(object sender, EventArgs e)
    {
        int folioConciliacion = Convert.ToInt32(grvConciliacion.DataKeys[Convert.ToInt32(fldIndiceConcilacion.Value.Trim())].Value);
        cConciliacion conciliacion = listaConciliaciones.Find(x => x.Folio == folioConciliacion);

        Enrutador objEnrutador = new Enrutador();
        List<ListaCombo> listFormasConciliacion = objEnrutador.CargarFormaConciliacion(conciliacion.TipoConciliacion);
        string URLDestino = objEnrutador.ObtieneURLSolicitudPorDefecto(new SolicitudEnrutador(conciliacion.TipoConciliacion, 0));

        Response.Redirect("~/Conciliacion/FormasConciliar/" + URLDestino +
                                     ".aspx?Folio=" + folioConciliacion + "&Corporativo=" + conciliacion.Corporativo +
                                     "&Sucursal=" + conciliacion.Sucursal + "&Año=" + conciliacion.Año + "&Mes=" +
                                     conciliacion.Mes + "&TipoConciliacion=" + conciliacion.TipoConciliacion + "&GrupoConciliacion=" + conciliacion.GrupoConciliacion);
    }
    protected void lnkDetalle_Click(object sender, EventArgs e)
    {
       
        int folioConciliacion = Convert.ToInt32(grvConciliacion.DataKeys[Convert.ToInt32(fldIndiceConcilacion.Value.Trim())].Value);
        cConciliacion conciliacion = listaConciliaciones.Find(x => x.Folio == folioConciliacion);

        
        Response.Redirect("~/Conciliacion/DetalleConciliacion.aspx?Folio=" + folioConciliacion + "&Corporativo=" + conciliacion.Corporativo +
                                     "&Sucursal=" + conciliacion.Sucursal + "&Año=" + conciliacion.Año + "&Mes=" +
                                     conciliacion.Mes );

    }
    protected void lnkPagos_Click(object sender, EventArgs e)
    {
        short esEdificios = 0;

        int folioConciliacion = Convert.ToInt32(grvConciliacion.DataKeys[Convert.ToInt32(fldIndiceConcilacion.Value.Trim())].Value);
        cConciliacion conciliacion = listaConciliaciones.Find(x => x.Folio == folioConciliacion);

        if (conciliacion.TipoConciliacion == 2)
        {
            esEdificios = 1;
        }
        Response.Redirect("~/Conciliacion/Pagos/AplicarPago.aspx?Folio=" + folioConciliacion + "&Corporativo=" + conciliacion.Corporativo +
                                     "&Sucursal=" + conciliacion.Sucursal + "&Año=" + conciliacion.Año + "&Mes=" +
                                     conciliacion.Mes + "&EsEdificios=" + esEdificios + "&TipoConciliacion=" + conciliacion.TipoConciliacion);
    }

    //Nueva funcionalidad
    protected void lnkInforme_Click(object sender, EventArgs e)
    {
        AppSettingsReader settings = new AppSettingsReader();
        int folioConciliacion = Convert.ToInt32(grvConciliacion.DataKeys[Convert.ToInt32(fldIndiceConcilacion.Value.Trim())].Value);
        cConciliacion conciliacion = listaConciliaciones.Find(x => x.Folio == folioConciliacion);
        string strReporte = Server.MapPath("~/") + settings.GetValue("RutaReporteInformeContabilidad", typeof(string));

        if (!File.Exists(strReporte)) return;
        try
        {
            string strServer = settings.GetValue("Servidor", typeof(string)).ToString();
            string strDatabase = settings.GetValue("Base", typeof(string)).ToString();

            usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
            string strUsuario = usuario.IdUsuario.Trim();
            string strPW = usuario.ClaveDesencriptada;
            ArrayList Par = new ArrayList();

            Par.Add("@CorporativoConciliacion=" + conciliacion.Corporativo);
            Par.Add("@SucursalConciliacion=" + conciliacion.Sucursal);
            Par.Add("@FolioConciliacion=" + conciliacion.Folio);
            Par.Add("@MesConciliacion=" + conciliacion.Mes);
            Par.Add("@AñoConciliacion=" + conciliacion.Año);

            ClaseReporte Reporte = new ClaseReporte(strReporte, Par, strServer, strDatabase, strUsuario, strPW);
            //Reporte.Imprimir_Reporte();
            HttpContext.Current.Session["RepDoc"] = Reporte.RepDoc;
            HttpContext.Current.Session["ParametrosReporte"] = Par;
            Nueva_Ventana("Reporte/Reporte.aspx", "Carta", 0, 0, 0, 0);
            //if (Reporte.Hay_Error) Mensaje("Error.", Reporte.Error);
            Reporte = null;

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error: " + ex.Message);
        }
    }

    protected void Nueva_Ventana(string Pagina, string Titulo, int Ancho, int Alto, int X, int Y)
    {
        ScriptManager.RegisterClientScriptBlock(this.upInicio,
                                            upInicio.GetType(),
                                            "ventana",
                                            "ShowWindow('" + Pagina + "','" + Titulo + "'," + Ancho + "," + Alto + "," + X + "," + Y + ")",
                                            true);
    }


    protected void grvConciliacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvConciliacion.PageIndex = e.NewPageIndex;
            LlenaGridViewConciliaciones();
        }
        catch (Exception)
        {

        }
    }
    protected void grvConciliacion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label l = e.Row.FindControl("lblIndice") as Label;
            l.Text = e.Row.RowIndex.ToString();
        }
        if (e.Row.RowType == DataControlRowType.Pager && (grvConciliacion.DataSource != null))
        {

            Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumPaginas");
            _TotalPags.Text = grvConciliacion.PageCount.ToString();

            DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
            for (int i = 1; i <= Convert.ToInt32(grvConciliacion.PageCount); i++)
            {
                list.Items.Add(i.ToString());
            }
            list.SelectedValue = (grvConciliacion.PageIndex + 1).ToString();
        }
    }
    protected void paginasDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag;
        grvConciliacion.PageIndex = int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 &&
                                    iNumPag <= grvConciliacion.PageCount
                                        ? iNumPag - 1
                                        : 0;
        LlenaGridViewConciliaciones();
    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Consulta_Conciliacion();
        GenerarTablaConciliaciones();
        LlenaGridViewConciliaciones();
    }
    protected void grvConciliacion_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;
        e.Row.Attributes["onmouseover"] = string.Format("RowMouseOver({0});", e.Row.RowIndex);
        e.Row.Attributes["onmouseout"] = string.Format("RowMouseOut({0});", e.Row.RowIndex);
        e.Row.Attributes["onclick"] = string.Format("RowSelect({0});", e.Row.RowIndex);
    }
    protected void grvConciliacion_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtTblOrdenada = (DataTable)HttpContext.Current.Session["TBL_CONCILIACION"];
        if (dtTblOrdenada == null) return;
        string orden = direccionOrdenarCadena(e.SortExpression);
        dtTblOrdenada.DefaultView.Sort = e.SortExpression + " " + orden;
        HttpContext.Current.Session["TBL_CONCILIACION"] = dtTblOrdenada;
        grvConciliacion.DataSource = dtTblOrdenada;
        grvConciliacion.DataBind();
    }

    #endregion



    protected void lnkConsultar_Click(object sender, EventArgs e)
    {
        //consultar documentos trans ban                
        ClaseFiltros filtros = new ClaseFiltros();
        filtros.Empresa = ddlEmpresa.SelectedIndex;
        filtros.Sucursal = ddlSucursal.SelectedIndex;
        filtros.Grupo = ddlGrupo.SelectedIndex;
        filtros.TipoConciliacion = ddlTipoConciliacion.SelectedIndex;
        filtros.Status = ddlStatusConciliacion.SelectedIndex;
        filtros.Anio = ddlAñoConciliacion.SelectedIndex;
        filtros.Mes = ddlMesConciliacion.SelectedIndex;
        filtros.Folio= Convert.ToInt32(grvConciliacion.DataKeys[Convert.ToInt32(fldIndiceConcilacion.Value.Trim())].Value);
        cConciliacion conciliacion = listaConciliaciones.Find(x => x.Folio == filtros.Folio);
        filtros.Conciliacion = conciliacion;
        HttpContext.Current.Session["filtros"] = filtros;
        Response.Redirect("~/Conciliacion/ConsultarDocumentos.aspx");
    }


    protected void btnBuscaPagoEdoCtaAceptar_Click(object sender, EventArgs e)
    {
        mpeBuscadorPagoEdoCta.Hide();
    }

    protected void btnBuscaEdoCtaBuscar_Click(object sender, EventArgs e)
    {
        if (txtMonto.Text.Trim() == "")
            txtMonto.Text = "0";
        if (txtFinicio.Text == "")
            txtFinicio.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
        if (txtFfinal.Text == "")
            txtFfinal.Text = DateTime.Now.ToShortDateString();
        List<EstadoDeCuenta> listaEstadoCuenta = Conciliacion.RunTime.App.Consultas.BuscarPagoEstadoCuenta(DateTime.Parse(txtFinicio.Text), DateTime.Parse(txtFfinal.Text), decimal.Parse(txtMonto.Text), chkBuscaEnRetiros.Checked, chkBuscarEnDepositos.Checked);
        grvPagoEstadoCuenta.DataSource = listaEstadoCuenta;
        if (listaEstadoCuenta.Count > 0)
            grvPagoEstadoCuenta.DataBind();
        else
        {
            mpeBuscadorPagoEdoCta.Hide();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Mensaje",
                    @"alertify.alert('Conciliaci&oacute;n bancaria','No se encontr&oacute; registro que coincida con los par&aacute;metros de b&uacute;squeda proporcionados.', function(){ });", true);

        }
    }
}
