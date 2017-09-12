using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Conciliacion.RunTime.ReglasDeNegocio;
using Conciliacion.RunTime.DatosSQL;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using Conciliacion.Migracion.Runtime.ReglasNegocio;
using Conciliacion.Migracion.Runtime;
using SeguridadCB.Public;

public partial class Catalogos_StatusConcepto : System.Web.UI.Page
{
    #region "Propiedades Globales"
    private SeguridadCB.Public.Operaciones operaciones;
    private SeguridadCB.Public.Usuario usuario;
    #endregion

    public static List<StatusConcepto> listStatusConcepto = new List<StatusConcepto>();
    public static List<Etiqueta> listEtiquetasStatusConcepto = new List<Etiqueta>();
    public static List<Etiqueta> listEtiquetasBanco = new List<Etiqueta>();
    public static StatusConcepto statusConceptoActual;
    protected override void OnPreInit(EventArgs e)
    {
        if (HttpContext.Current.Session["Operaciones"] == null)
            Response.Redirect("~/Acceso/Acceso.aspx", true);
        else
            operaciones = (SeguridadCB.Public.Operaciones)HttpContext.Current.Session["Operaciones"];
    }
    protected void Page_Load(object sender, EventArgs e)
    {

       
        App.ImplementadorMensajes.ContenedorActual = this;
        if (!IsPostBack)
        {

            ConsultarStatusConcepto();
            LlenarGridViewStatusConcepto();

            ConsultaCorporativo();

            statusConceptoActual = App.StatusConcepto.CrearObjeto();
        }

    }
    private int indiceSeleccionadoGrupo
    {
        get
        {
            if (string.IsNullOrEmpty(Request.Form["GrupoStatusConcepto"]))
                return -1;
            else
                return Convert.ToInt32(Request.Form["GrupoStatusConcepto"]);
        }
    }
    public void ConsultarStatusConcepto()
    {
        System.Data.SqlClient.SqlConnection Connection = SeguridadCB.Seguridad.Conexion;
        if (Connection.State == ConnectionState.Closed)
        {
            SeguridadCB.Seguridad.Conexion.Open();
            Connection = SeguridadCB.Seguridad.Conexion;
        }
        try
        {
            listStatusConcepto = App.Consultas.ConsultaStatusConcepto(
                         Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionStatusConcepto.Todos);
        }
        catch (Exception)
        {

        }
    }
    public void LlenarGridViewStatusConcepto()
    {
        this.grvStatusConcepto.DataSource = listStatusConcepto;
        this.grvStatusConcepto.DataBind();
        this.grvStatusConcepto.Dispose();
    }

    public void ConsultaCorporativo()
    {
        try
        {
            this.ddlCorporativo.DataValueField = "Id";
            this.ddlCorporativo.DataTextField = "Nombre";
            this.ddlCorporativo.DataSource = App.Consultas.ObtieneListaCorporativo(((SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"]).IdUsuario);
            this.ddlCorporativo.DataBind();
        }
        catch
        {
        }
    }
    /// <summary>
    /// Llena el Combo de Bancos
    /// </summary>
    public void Carga_Banco(int corporativo)
    {
        try
        {
            this.ddlBancoFinanciero.DataSource = Conciliacion.RunTime.App.Consultas.ConsultaBancos(corporativo);
            this.ddlBancoFinanciero.DataValueField = "Identificador";
            this.ddlBancoFinanciero.DataTextField = "Descripcion";
            this.ddlBancoFinanciero.DataBind();
            this.ddlBancoFinanciero.Dispose();
        }
        catch
        {
        }
    }
    //Consultar Etiquetas por StatusConcepto
    public void ConsultarEtiquetasStatusConcepto(int statusConcepto)
    {
        System.Data.SqlClient.SqlConnection Connection = SeguridadCB.Seguridad.Conexion;
        if (Connection.State == ConnectionState.Closed)
        {
            SeguridadCB.Seguridad.Conexion.Open();
            Connection = SeguridadCB.Seguridad.Conexion;
        }
        try
        {
            listEtiquetasStatusConcepto = App.Consultas.ObtieneListaEtiquetaStatusConcepto(statusConcepto);
        }
        catch (Exception)
        {

        }
    }
    public void LlenarGridViewEtiquetasConcepto()
    {
        this.grvEtiquetaStatusConcepto.DataSource = listEtiquetasStatusConcepto;
        this.grvEtiquetaStatusConcepto.DataBind();
        this.grvEtiquetaStatusConcepto.Dispose();
    }
    //Consultar Etiquetas por Banco
    public void ConsultarEtiquetasBanco(int banco)
    {
        System.Data.SqlClient.SqlConnection Connection = SeguridadCB.Seguridad.Conexion;
        if (Connection.State == ConnectionState.Closed)
        {
            SeguridadCB.Seguridad.Conexion.Open();
            Connection = SeguridadCB.Seguridad.Conexion;
        }
        try
        {
            listEtiquetasBanco = App.Consultas.ObtieneListaEtiquetaBanco(banco);
            listEtiquetasBanco.RemoveAll(delegate(Etiqueta etiq)
            {
                return listEtiquetasStatusConcepto.Exists(x => x.Id == etiq.Id);
            });
        }
        catch (Exception)
        {
            throw;
        }
    }
    public void LlenarComboEtiquetasBanco()
    {
        this.ddlEtiquetasBanco.DataSource = listEtiquetasBanco;
        this.ddlEtiquetasBanco.DataValueField = "Id";
        this.ddlEtiquetasBanco.DataTextField = "Descripcion";
        this.ddlEtiquetasBanco.DataBind();
        this.ddlEtiquetasBanco.Dispose();
    }
    // Navegar entre las paginas del grid
    protected void grvStatusConcepto_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvStatusConcepto.PageIndex = e.NewPageIndex;
            LlenarGridViewStatusConcepto();
        }
        catch (Exception)
        {

        }
    }
    protected void grvStatusConcepto_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            Button btn = (Button)(e.Row.FindControl("btnStatus"));
            if (lblStatus.Text.Equals("ACTIVO"))
            {
                btn.CssClass = "boton activo";
                btn.ToolTip = "DESACTIVAR";
            }
            else
            {
                btn.CssClass = "boton inactivo";
                btn.ToolTip = "ACTIVAR";
            }

            Literal output = (Literal)e.Row.FindControl("rbElegirStatusConcepto");
            output.Text = string.Format(@"<input type=""radio"" name=""GrupoStatusConcepto"" 
                                        id=""rowGrupo{0}"" value=""{0}""",
                                        e.Row.RowIndex);

            if (indiceSeleccionadoGrupo == e.Row.RowIndex || (!Page.IsPostBack && e.Row.RowIndex == 0))
                output.Text += @" checked=""checked""";
            output.Text += " />";
        }
        if (e.Row.RowType == DataControlRowType.Pager && (grvStatusConcepto.DataSource != null))
        {
            //TRAE EL TOTAL DE PAGINAS
            Label _totalPags = (Label)e.Row.FindControl("lblTotalNumPaginas");
            _totalPags.Text = grvStatusConcepto.PageCount.ToString();

            //LLENA LA LISTA CON EL NUMERO DE PAGINAS
            DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
            for (int i = 1; i <= Convert.ToInt32(grvStatusConcepto.PageCount); i++)
            {
                list.Items.Add(i.ToString());
            }
            list.SelectedValue = (grvStatusConcepto.PageIndex + 1).ToString();
        }
    }
    protected void paginasDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag = 0;
        grvStatusConcepto.PageIndex = int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 && iNumPag <= grvStatusConcepto.PageCount ? iNumPag - 1 : 0;
        LlenarGridViewStatusConcepto();
    }

    protected void paginasDropDownListEtiquetas_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag = 0;
        grvEtiquetaStatusConcepto.PageIndex = int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 && iNumPag <= grvStatusConcepto.PageCount ? iNumPag - 1 : 0;
        LlenarGridViewEtiquetasConcepto();
    }
    protected void grvStatusConcepto_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("CAMBIARSTATUS"))
        {
            GridViewRow gRow = (GridViewRow)(e.CommandSource as Button).Parent.Parent;
            int statusConcepto = Convert.ToInt32(grvStatusConcepto.DataKeys[gRow.RowIndex].Values["Id"]);
            StatusConcepto sc = listStatusConcepto.Single(x => x.Id == statusConcepto);
            if (sc.CambiarStatus())
            {
                ConsultarStatusConcepto();
                LlenarGridViewStatusConcepto();
            }
        }
    }

    protected void btnGuardarStatusConcepto_Click(object sender, EventArgs e)
    {
        usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
        StatusConcepto nuevoStatusConcepto = App.StatusConcepto.CrearObjeto();
        nuevoStatusConcepto.Descripcion = txtDescripcion.Text;
        nuevoStatusConcepto.Usuario = usuario.IdUsuario.Trim();
        if (nuevoStatusConcepto.Guardar())
        {
            ConsultarStatusConcepto();
            LlenarGridViewStatusConcepto();
        }
    }

    protected void btnModificar_Click(object sender, EventArgs e)
    {
        if (indiceSeleccionadoGrupo < 0) { App.ImplementadorMensajes.MostrarMensaje("Elija un Status Concepto"); return; }
        bool statusActivo = Convert.ToString(grvStatusConcepto.DataKeys[indiceSeleccionadoGrupo].Values["Status"]).Equals("ACTIVO");
        if (statusActivo)
        {
            int idStatusConcepto = Convert.ToInt32(grvStatusConcepto.DataKeys[indiceSeleccionadoGrupo].Values["Id"]);
            //string descripcion = Convert.ToString(grvStatusConcepto.DataKeys[indiceSeleccionadoGrupo].Values["Descripcion"]);
            statusConceptoActual = listStatusConcepto.Single(x => x.Id == idStatusConcepto);
            lblStatusActual.Text = statusConceptoActual.Descripcion;

            ConsultarEtiquetasStatusConcepto(statusConceptoActual.Id);
            LlenarGridViewEtiquetasConcepto();
            popUpEtiquetas.Show();
        }
        else
            App.ImplementadorMensajes.MostrarMensaje("Status Concepto INACTIVO. Verifique su selección.");
    }
    protected void ddlCorpotativo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Carga_Banco(Convert.ToInt32(ddlCorporativo.SelectedItem.Value));
    }
    protected void ddlCorpotativo_DataBound(object sender, EventArgs e)
    {
        Carga_Banco(Convert.ToInt32(ddlCorporativo.SelectedItem.Value));
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgButton = (sender as ImageButton);
        GridViewRow row = imgButton.Parent.Parent as GridViewRow;
        int etiqueta = Convert.ToInt32(grvEtiquetaStatusConcepto.DataKeys[row.RowIndex].Values["Id"]);
        //StatusConcepto scActual = listStatusConcepto.Single(x => x.Id == Convert.ToInt32(grvStatusConcepto.DataKeys[indiceSeleccionadoGrupo].Values["Id"]));
        if (statusConceptoActual.EliminaEtiquetaStatus(etiqueta))
        {
            ConsultarEtiquetasStatusConcepto(statusConceptoActual.Id);
            LlenarGridViewEtiquetasConcepto();
            ConsultarEtiquetasBanco(Convert.ToInt32(ddlBancoFinanciero.SelectedItem.Value));
            LlenarComboEtiquetasBanco();

        }
    }
    protected void grvEtiquetaStatusConcepto_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvEtiquetaStatusConcepto.PageIndex = e.NewPageIndex;
            LlenarGridViewEtiquetasConcepto();
        }
        catch (Exception)
        {

        }
    }
    protected void grvEtiquetaStatusConcepto_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager && (grvEtiquetaStatusConcepto.DataSource != null))
        {

            Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumPaginas");
            _TotalPags.Text = grvEtiquetaStatusConcepto.PageCount.ToString();

            DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownListEtiquetas");
            for (int i = 1; i <= Convert.ToInt32(grvEtiquetaStatusConcepto.PageCount); i++)
            {
                list.Items.Add(i.ToString());
            }
            list.SelectedValue = (grvEtiquetaStatusConcepto.PageIndex + 1).ToString();
        }
    }
    protected void ddlBancoFinanciero_DataBound(object sender, EventArgs e)
    {
        try
        {
            if (ddlBancoFinanciero.Items.Count > 0)
            {
                ConsultarEtiquetasBanco(Convert.ToInt32(ddlBancoFinanciero.SelectedItem.Value));
                LlenarComboEtiquetasBanco();
            }
            else
            {
                listEtiquetasStatusConcepto.Clear();
                ddlEtiquetasBanco.DataSource = listEtiquetasStatusConcepto;
                ddlEtiquetasBanco.DataBind();
            }
        }
        catch (Exception ex) { throw; }
    }
    protected void ddlBancoFinanciero_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConsultarEtiquetasBanco(Convert.ToInt32(ddlBancoFinanciero.SelectedItem.Value));
        LlenarComboEtiquetasBanco();
    }
    protected void btnAgregarEtiqueta_Click(object sender, ImageClickEventArgs e)
    {
        //StatusConcepto scActual = listStatusConcepto.Single(x => x.Id == Convert.ToInt32(grvStatusConcepto.DataKeys[indiceSeleccionadoGrupo].Values["Id"]));
        if (statusConceptoActual.AgregaEtiquetaStatus(Convert.ToInt32(ddlEtiquetasBanco.SelectedItem.Value)))
        {
            ConsultarEtiquetasStatusConcepto(statusConceptoActual.Id);
            LlenarGridViewEtiquetasConcepto();
            ConsultarEtiquetasBanco(Convert.ToInt32(ddlBancoFinanciero.SelectedItem.Value));
            LlenarComboEtiquetasBanco();
        }
    }
}
