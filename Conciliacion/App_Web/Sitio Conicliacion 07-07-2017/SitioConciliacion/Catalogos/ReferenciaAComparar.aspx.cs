using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CatalogoConciliacion.ReglasNegocio;
using Conciliacion.RunTime;
using Conciliacion.RunTime.ReglasDeNegocio;
using System.Data;

public partial class Catalogos_ReferenciaAComparar : System.Web.UI.Page
{
    Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
    CatalogoConciliacion.App objAppCat = new CatalogoConciliacion.App();
    #region "Propiedades Globales"
    private SeguridadCB.Public.Operaciones operaciones;
    #endregion

    GridViewRow row;
    private List<ReferenciaAComparar> listaReferencias = new List<ReferenciaAComparar>();
    private List<ReferenciaAComparar> listaReferenciaExistente = new List<ReferenciaAComparar>();

    private DataTable tblReferencias = new DataTable("Referencias");
    private List<ListaCombo> listTipoConciliacion = new List<ListaCombo>();
    private List<ListaCombo> listColumnaDestinoExt = new List<ListaCombo>();
    private List<ListaCombo> listColumnaDestinoInt = new List<ListaCombo>();

    protected override void OnPreInit(EventArgs e)
    {
        if (HttpContext.Current.Session["Operaciones"] == null)
            Response.Redirect("~/Acceso/Acceso.aspx", true);
        else
            operaciones = (SeguridadCB.Public.Operaciones)HttpContext.Current.Session["Operaciones"];
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        objAppCat.ImplementadorMensajes.ContenedorActual = this;
        if (!IsPostBack)
        {
            ConsultaReferenciasAComparar();
            LlenaGridReferenciasAComparar();
            CargarCombos();
        }
    }

    public void CargarCombos()
    { 
        //Combo Tipo Conciliacion 
        listTipoConciliacion = objApp.Consultas.ConsultaTipoConciliacion(Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionGrupo.Todos, "");
        ddlTipoConciliacion.DataSource = listTipoConciliacion;
        ddlTipoConciliacion.DataValueField = "Identificador";
        ddlTipoConciliacion.DataTextField = "Descripcion";
        ddlTipoConciliacion.DataBind();
        ddlTipoConciliacion.Dispose();

        //Combo columna externa
        listColumnaDestinoExt =  objAppCat.Consultas.ObtieneColumnas(2, 0, "");
        ddlColumnaDestExt.DataSource = listColumnaDestinoExt;
        ddlColumnaDestExt.DataValueField = "Descripcion";//"Identificador";
        ddlColumnaDestExt.DataTextField = "Descripcion";
        ddlColumnaDestExt.DataBind();
        ddlColumnaDestExt.Dispose();

        //Combo columan interna
        listColumnaDestinoInt =  objAppCat.Consultas.ObtieneColumnas(2, 0, "");
        ddlColumnaDestInt.DataSource = listColumnaDestinoInt;
        ddlColumnaDestInt.DataValueField = "Descripcion";//"Identificador";
        ddlColumnaDestInt.DataTextField = "Descripcion";
        ddlColumnaDestInt.DataBind();
        ddlColumnaDestInt.Dispose();
    }

    public void ConsultaReferenciasAComparar()
    {
        listaReferencias =  objAppCat.Consultas.ObtieneReferencias(1);
        GenerarTablaReferenciasAComparar();
    }


    public void GenerarTablaReferenciasAComparar()
    {
        tblReferencias.Columns.Add("TipoConciliacion", typeof(int));
        tblReferencias.Columns.Add("TipoConciliacionDescripcion", typeof(string));
        tblReferencias.Columns.Add("Secuencia", typeof(int));
        tblReferencias.Columns.Add("ColumnaDestinoExt", typeof(string));
        tblReferencias.Columns.Add("ColumnaDestinoInt", typeof(string));
        tblReferencias.Columns.Add("Status", typeof(string));

        foreach (ReferenciaAComparar m in listaReferencias)
        {
            tblReferencias.Rows.Add(m.TipoConciliacion,
                m.TipoConciliacionDescripcion,
                m.Secuencia,
                m.ColumnaDestinoExt,
                m.ColumnaDestinoInt,
                m.Status
                );
        }
        HttpContext.Current.Session["TABLAREFERENCIAS"] = tblReferencias;
    }

    private void LlenaGridReferenciasAComparar()
    {
        DataTable tablaReferencias = (DataTable)HttpContext.Current.Session["TABLAREFERENCIAS"];
        grdReferencias.DataSource = tablaReferencias;
        grdReferencias.DataBind();
    }

    //Navegacion entre las paginas del grid
    protected void grdReferencias_PageIndexChanging(object sender, GridViewPageEventArgs e) // Navegacion entre paginas del grid
    {
        try
        {
            grdReferencias.PageIndex = e.NewPageIndex;
            LlenaGridReferenciasAComparar();
        }
        catch (Exception ex) { }
    }

    protected void Mensaje(string titulo, string texto)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "alert('" + texto + "');", true);
    }


    protected void btnAgregar_Click1(object sender, EventArgs e)
    {
        try
        {
        listColumnaDestinoExt =  objAppCat.Consultas.ObtieneColumnas(2, 0, "");
        listColumnaDestinoInt =  objAppCat.Consultas.ObtieneColumnas(2, 0, "");
        if (listColumnaDestinoExt[ddlColumnaDestExt.SelectedIndex].Campo1 != listColumnaDestinoInt[ddlColumnaDestInt.SelectedIndex].Campo1)
        {
            Mensaje("Agregar referencia", "Los tipos de datos no coinciden, no se puede agregar");
        }
        else
        {
            listaReferenciaExistente =  objAppCat.Consultas.ObtieneReferenciaPorIdLista(3, Convert.ToInt32(ddlTipoConciliacion.SelectedItem.Value), 0, ddlColumnaDestExt.SelectedItem.Value, ddlColumnaDestInt.SelectedItem.Value);
            if (listaReferenciaExistente.Count > 0)
            {
                Mensaje("Agregar referencia", "La combinación ya existe, no se puede agregar");
            }
            else
            {
                ReferenciaAComparar rc =  objAppCat.ReferenciaAComparar;
                rc.TipoConciliacion = Convert.ToInt32(ddlTipoConciliacion.SelectedItem.Value);
                rc.ColumnaDestinoExt = ddlColumnaDestExt.SelectedItem.Value;
                rc.ColumnaDestinoInt = ddlColumnaDestInt.SelectedItem.Value;
                rc.Guardar();
                CargarCombos();
            }
            ConsultaReferenciasAComparar();
            LlenaGridReferenciasAComparar();
        }
        }
        catch (Exception ex) { objApp.ImplementadorMensajes.MostrarMensaje(ex.Message); }
    }

    protected void grdReferencias_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("CAMBIARSTATUS"))
        {
            GridViewRow gRow = (GridViewRow)(e.CommandSource as Button).Parent.Parent;
             objAppCat.ReferenciaAComparar.CambiarStatus(Convert.ToInt16(grdReferencias.DataKeys[gRow.RowIndex].Values["TipoConciliacion"]), Convert.ToInt16(grdReferencias.DataKeys[gRow.RowIndex].Values["Secuencia"]));
            ConsultaReferenciasAComparar();
            LlenaGridReferenciasAComparar();
        }
    }

    protected void grdReferencias_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int status;
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
        }
        if (e.Row.RowType == DataControlRowType.Pager && (grdReferencias.DataSource != null))
        {
            //TRAE EL TOTAL DE PAGINAS
            Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumPaginas");
            _TotalPags.Text = grdReferencias.PageCount.ToString();

            //LLENA LA LISTA CON EL NUMERO DE PAGINAS
            DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
            for (int i = 1; i <= Convert.ToInt32(grdReferencias.PageCount); i++)
            {
                list.Items.Add(i.ToString());
            }
            list.SelectedValue = (grdReferencias.PageIndex + 1).ToString();
        }
    }
    protected void paginasDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag = 0;

        if (int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 && iNumPag <= grdReferencias.PageCount)
        {
            grdReferencias.PageIndex = iNumPag - 1;
        }
        else
        {
            grdReferencias.PageIndex = 0;
        }

        LlenaGridReferenciasAComparar();
    }
}