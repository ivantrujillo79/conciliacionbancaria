using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CatalogoConciliacion.ReglasNegocio;
using System.Data;

public partial class Catalogos_MotivoNoConcilidado : System.Web.UI.Page
{
    CatalogoConciliacion.App objAppCat = new CatalogoConciliacion.App();
    #region "Propiedades Globales"
    private SeguridadCB.Public.Operaciones operaciones;
    private SeguridadCB.Public.Usuario usuario;
    #endregion

    private List<MotivoNoConciliado> listaMotivos = new List<MotivoNoConciliado>();
    private DataTable tblMotivos = new DataTable("Motivos");

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
            ConsultaMotivosNoConciliacion();
            LlenaGridViewMotivosNoConciliados();
        }

    }


    // Consulta los motivos de la base de datos
    public void ConsultaMotivosNoConciliacion()
    {
        listaMotivos =  objAppCat.Consultas.ObtieneMotivos(1, 0);
        GenerarTablaMotivosNoConciliados();
    }

    //Genera la estructura de la tabla temporal para guardar lo que devuelva la consulta
    public void GenerarTablaMotivosNoConciliados()
    {
        tblMotivos.Columns.Add("MotivoNoConciliadoId", typeof(int));
        tblMotivos.Columns.Add("Descripcion", typeof(string));
        tblMotivos.Columns.Add("Status", typeof(string));

        foreach (MotivoNoConciliado m in listaMotivos)
        {
            tblMotivos.Rows.Add(
                m.MotivoNoConciliadoId,
                m.Descripcion, m.Status
                );
        }
        HttpContext.Current.Session["TABLAMOTIVOS"] = tblMotivos;
    }

    // Asigna la tabla temporal al gridview
    private void LlenaGridViewMotivosNoConciliados()
    {
        DataTable tablaMotivos = (DataTable)HttpContext.Current.Session["TABLAMOTIVOS"];
        grdMotivos.DataSource = tablaMotivos;
        grdMotivos.DataBind();
    }


    //Navegacion entre las paginas del grid
    protected void grdMotivos_PageIndexChanging(object sender, GridViewPageEventArgs e) // Navegacion entre paginas del grid
    {
        try
        {
            grdMotivos.PageIndex = e.NewPageIndex;
            LlenaGridViewMotivosNoConciliados();
        }
        catch (Exception)
        {

        }

    }


    protected void Mensaje(string titulo, string texto)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "alert('" + texto + "');", true);
    }




    //Guarda el nuevo motivo en la base de datos
    protected void btnAgregar_Click1(object sender, EventArgs e)
    {
        //if (txtDescripcion.Text != "")
        //{
        MotivoNoConciliado mnc =  objAppCat.MotivoNoConciliado;
        mnc.MotivoNoConciliadoId = 0;
        mnc.Descripcion = txtDescripcion.Text;
        mnc.Status = "ACTIVO";
        mnc.Guardar();
        ConsultaMotivosNoConciliacion();
        LlenaGridViewMotivosNoConciliados();
        txtDescripcion.Text = String.Empty;

        //}
        //else {
        //    Mensaje("Agregar motivo", "Agregue una descripción valida para continuar");
        //}
    }



    protected void grdMotivos_RowDataBound(object sender, GridViewRowEventArgs e)
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
        if (e.Row.RowType == DataControlRowType.Pager && (grdMotivos.DataSource != null))
        {
            //TRAE EL TOTAL DE PAGINAS
            Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumPaginas");
            _TotalPags.Text = grdMotivos.PageCount.ToString();

            //LLENA LA LISTA CON EL NUMERO DE PAGINAS
            DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
            for (int i = 1; i <= Convert.ToInt32(grdMotivos.PageCount); i++)
            {
                list.Items.Add(i.ToString());
            }
            list.SelectedValue = (grdMotivos.PageIndex + 1).ToString();
        }
    }
    protected void paginasDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag = 0;

        if (int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 && iNumPag <= grdMotivos.PageCount)
        {
            grdMotivos.PageIndex = iNumPag - 1;
        }
        else
        {
            grdMotivos.PageIndex = 0;
        }

        LlenaGridViewMotivosNoConciliados();
    }
    // Cambia el status del motivo seleccionado
    protected void grdMotivos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("CAMBIARSTATUS"))
        {
            GridViewRow gRow = (GridViewRow)(e.CommandSource as Button).Parent.Parent;
             objAppCat.MotivoNoConciliado.CambiarStatus(Convert.ToInt16(grdMotivos.DataKeys[gRow.RowIndex].Values["MotivoNoConciliadoId"]));
            ConsultaMotivosNoConciliacion();
            LlenaGridViewMotivosNoConciliados();
        }
    }
}
