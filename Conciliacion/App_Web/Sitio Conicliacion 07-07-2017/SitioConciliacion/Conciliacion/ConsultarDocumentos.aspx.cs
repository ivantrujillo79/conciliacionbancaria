using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Conciliacion_ConsultarDocumentos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ClaseFiltros filtros = new ClaseFiltros();
            filtros = (ClaseFiltros)HttpContext.Current.Session["filtros"];
            Label1.Text = "Empresa: " + filtros.Empresa+". Sucursal: "+filtros.Sucursal+". Grupo: " + filtros.Grupo+".";
        }            
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}