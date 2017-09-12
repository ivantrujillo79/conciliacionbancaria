using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Text;
using Conciliacion.RunTime;

public partial class Reporte_Reporte : System.Web.UI.Page
{
    private ReportDocument RepDoc = new ReportDocument();
    private int intPagMax = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
     
        try
        {
            RepDoc = (ReportDocument)HttpContext.Current.Session["RepDoc"];
            crviewRep.ReportSource = RepDoc;
            crviewRep.Visible = true;
            crviewRep.Width = Unit.Percentage(98);
            crviewRep.Height = Unit.Percentage(98);
            //crviewRep.RefreshReport();
            crviewRep.DisplayGroupTree = false;
            crviewRep.HasPrintButton = false;
            crviewRep.DisplayToolbar = true;
            crviewRep.EnableViewState = true;
            crviewRep.DataBind();
            if (crviewRep.ViewInfo.LastPageNumber > intPagMax) intPagMax = crviewRep.ViewInfo.LastPageNumber;
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }
    }
    #region "Funciones privadas"
    private void Imprimir()
    {
        StringBuilder strCadena = new StringBuilder();
        strCadena.Append("<script>");
        //strCadena.Append("if (confirm('Desea imprimir este reporte.'))");
        strCadena.Append("window.print();");
        strCadena.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "Impresion.", strCadena.ToString());
    }
    #endregion
}