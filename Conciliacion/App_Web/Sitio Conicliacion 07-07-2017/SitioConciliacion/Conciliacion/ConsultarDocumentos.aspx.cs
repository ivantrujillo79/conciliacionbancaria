using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Conciliacion_ConsultarDocumentos : System.Web.UI.Page
{
    private DataTable tblConsultarDocumentos = new DataTable("ConsultarDocumentos");

    private void GenerarTabla()
    {
        tblConsultarDocumentos.Columns.Add("Campo1", typeof(string));
        tblConsultarDocumentos.Columns.Add("Campo2", typeof(int));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ClaseFiltros filtros = new ClaseFiltros();
            filtros = (ClaseFiltros)HttpContext.Current.Session["filtros"];
            Label1.Text = "Empresa: " + filtros.Empresa+". Sucursal: "+filtros.Sucursal+". Grupo: " + filtros.Grupo+".";
        }            
    }

}