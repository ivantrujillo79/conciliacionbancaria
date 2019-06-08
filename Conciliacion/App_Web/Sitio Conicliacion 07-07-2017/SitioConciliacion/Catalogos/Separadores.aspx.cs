using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;
using AjaxControlToolkit;
using Conciliacion.Migracion.Runtime;
using Conciliacion.Migracion.Runtime.ReglasNegocio;
using System.Data.SqlClient;

public partial class ImportacionArchivos_Separadores : System.Web.UI.Page
{

    #region "Propiedades Globales"
    private SeguridadCB.Public.Operaciones operaciones;
    #endregion
    Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
    private void Limpiar()
    {
        this.txtSeparador.Text = string.Empty;
        this.grvSeparadores.DataSource = Conciliacion.Migracion.Runtime.App.Consultas.ObtieneListaSeparador();
        this.grvSeparadores.DataBind();
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
      
        objApp.ImplementadorMensajes.ContenedorActual = this;
        if (!IsPostBack)
        {

            this.grvSeparadores.DataSource = Conciliacion.Migracion.Runtime.App.Consultas.ObtieneListaSeparador();
            this.grvSeparadores.DataBind();
        }
    }
    protected void btnGuardarDatos_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.txtSeparador.Text))
        {
            try
            {
                Separador separador = Conciliacion.Migracion.Runtime.App.Separador;
                separador.Descripcion = this.txtSeparador.Text;
                separador.Status = "ACTIVO";
                Conciliacion.Migracion.Runtime.App.implementadorMensajes.MensajesActivos = false;
                if (separador.Guardar())
                {
                    this.grvSeparadores.DataSource = Conciliacion.Migracion.Runtime.App.Consultas.ObtieneListaSeparador();
                    this.grvSeparadores.DataBind();
                    Limpiar();
                }
            }
            catch (SqlException ex)
            {
                if (ex.ErrorCode == -2146232060)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('El separador ya existe en el sistema .')", true);
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('" + ex.Message + "')", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Escriba la descripción del separador.')", true);
        }
        Conciliacion.Migracion.Runtime.App.implementadorMensajes.MensajesActivos = true;
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton imgButton = (sender as ImageButton);
        GridViewRow row = imgButton.Parent.Parent as GridViewRow;
        Label lblReferencia = (Label)row.FindControl("lblGVSeparador");

        Conciliacion.Migracion.Runtime.App.Separador.Descripcion = lblReferencia.Text.Trim();
        Conciliacion.Migracion.Runtime.App.Separador.Status = "INACTIVO";
        if (App.Separador.Actualizar())
        {
            this.grvSeparadores.DataSource = Conciliacion.Migracion.Runtime.App.Consultas.ObtieneListaSeparador();
            this.grvSeparadores.DataBind();
        }
    }
    protected void btnCancelarDatos_Click(object sender, EventArgs e)
    {
        Limpiar();
    }
    protected void grvSeparadores_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.grvSeparadores.PageIndex = e.NewPageIndex;
            this.grvSeparadores.DataSource = Conciliacion.Migracion.Runtime.App.Consultas.ObtieneListaSeparador();
            this.grvSeparadores.DataBind();
        }
        catch (Exception)
        {

        }
    }
    protected void grvSeparadores_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager && (grvSeparadores.DataSource != null))
        {
            Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumPaginas");
            _TotalPags.Text = grvSeparadores.PageCount.ToString();

            DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
            for (int i = 1; i <= Convert.ToInt32(grvSeparadores.PageCount); i++)
            {
                list.Items.Add(i.ToString());
            }
            list.SelectedValue = (grvSeparadores.PageIndex + 1).ToString();
        }
    }
    protected void paginasDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag;
        grvSeparadores.PageIndex = int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 &&
                                    iNumPag <= grvSeparadores.PageCount
                                        ? iNumPag - 1
                                        : 0;
        this.grvSeparadores.DataSource = Conciliacion.Migracion.Runtime.App.Consultas.ObtieneListaSeparador();
        this.grvSeparadores.DataBind();
    }

}
