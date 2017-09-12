using System;

using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using Conciliacion.RunTime.ReglasDeNegocio;

using SeguridadCB.Public;

using System.Collections.Generic;


using Conciliacion.Migracion.Runtime;

public partial class ReportesConciliacion_ReporteConciliacionII : System.Web.UI.Page
{
    #region "Propiedades Globales"
    private SeguridadCB.Public.Operaciones operaciones;
    private SeguridadCB.Public.Usuario usuario;
    #endregion

    #region "Propiedades privadas"
    private List<ListaCombo> listSucursales = new List<ListaCombo>();


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
            Conciliacion.RunTime.App.ImplementadorMensajes.ContenedorActual = this;
            Conciliacion.Migracion.Runtime.App.ImplementadorMensajes.ContenedorActual = this;

            if (HttpContext.Current.Request.UrlReferrer != null)
            {
                if ((!HttpContext.Current.Request.UrlReferrer.AbsoluteUri.Contains("SitioConciliacion")) || (HttpContext.Current.Request.UrlReferrer.AbsoluteUri.Contains("Inicio.aspx")))
                {
                    HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
                    HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
                    Response.Cache.SetExpires(DateTime.Now);
                }
            }
            if (!Page.IsPostBack)
            {
                usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
                //Inicializacion de Propiedades
                //listaConciliaciones = new List<cConciliacion>();

                if (ddlEmpresa.Items.Count == 0) Carga_Corporativo();

                Carga_Año();

                ddlMes.SelectedValue = leerMes();
                ddlAño.SelectedValue = leerAño();
                //Consulta_ReporteContabilidadI();
                //GenerarTablaReporteContabilidadI();

                //LlenaGridViewReporteContabilidadI();

            }

            this.ddlEmpresa.Focus();
        }

        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
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
    /// Llena el Combo de Año Conciliacion de los existentes en la tabla y el actual
    /// </summary>
    public void Carga_Año()
    {
        try
        {
            this.ddlAño.DataSource = Conciliacion.RunTime.App.Consultas.ConsultaAños();
            this.ddlAño.DataValueField = "Identificador";
            this.ddlAño.DataTextField = "Descripcion";
            this.ddlAño.DataBind();
            this.ddlAño.Dispose();
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
    /// Obtiene Fecha Maxima Conciliacion
    /// </summary>
    public string fechaMaximaConciliacion()
    {
        return Conciliacion.RunTime.App.Consultas.ConsultaFechaActualInicial();
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
            listSucursales = Conciliacion.RunTime.App.Consultas.ConsultaSucursales(Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionIden0.Sin0, corporativo);
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


    protected void ddlBanco_SelectedIndexChanged(object sender, EventArgs e)
    {
        Carga_CuentaBancaria(Convert.ToInt16(ddlEmpresa.SelectedItem.Value), ddlBanco.Items.Count > 0 ? Convert.ToSByte(ddlBanco.SelectedItem.Value) : -1);
    }

    protected void ddlBanco_DataBound(object sender, EventArgs e)
    {

        Carga_CuentaBancaria(Convert.ToInt16(ddlEmpresa.SelectedItem.Value), ddlBanco.Items.Count > 0 ? Convert.ToSByte(ddlBanco.SelectedItem.Value) : -1);
    }
    protected void grvReporten_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {

    }
    protected void grvReporte_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {

    }
    protected void grvReporte_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
    {

    }
    protected void paginasDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag;
        grvReporte.PageIndex = int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 &&
                                    iNumPag <= grvReporte.PageCount
                                        ? iNumPag - 1
                                        : 0;
        //LlenaGridViewReporteContabilidadI();
    }
}