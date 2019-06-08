using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Conciliacion.RunTime;

public partial class Conciliacion_Pagos_ConsultarTransban : System.Web.UI.Page
{
    #region "Propiedades Globales"
    private SeguridadCB.Public.Operaciones operaciones;
    private SeguridadCB.Public.Usuario usuario;

    #endregion
    Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
    protected override void OnPreInit(EventArgs e)
    {
        if (HttpContext.Current.Session["Operaciones"] == null)
            Response.Redirect("~/Acceso/Acceso.aspx", true);
        else
            operaciones = (SeguridadCB.Public.Operaciones)HttpContext.Current.Session["Operaciones"];
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void imgExportar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lanzarReporteComprobanteDeCaja(Convert.ToInt16(txtConsecutivo.Text),
                                           Convert.ToInt16(txtFolio.Text),
                                           Convert.ToInt16(txtCaja.Text),
                                           Convert.ToDateTime(txtFOperacion.Text));
        }
        catch (Exception ex)
        {

            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    public void lanzarReporteComprobanteDeCaja(int consecutivo, int folio, int caja, DateTime foperacion)
    {
        //short consecutivo = mc.Consecutivo;
        //int folio = mc.Folio;
        //short caja = mc.Caja;
        //string FOperacion = Convert.ToString(mc.FOperacion);
        AppSettingsReader settings = new AppSettingsReader();

        string strReporte = Server.MapPath("~/") + settings.GetValue("RutaComprobanteDeCaja", typeof(string));
        if (!File.Exists(strReporte)) return;
        try
        {
            string strServer = settings.GetValue("Servidor", typeof(string)).ToString();
            string strDatabase = settings.GetValue("Base", typeof(string)).ToString();
            usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];

            string strUsuario = usuario.IdUsuario.Trim();
            string strPW = usuario.ClaveDesencriptada;
            ArrayList Par = new ArrayList();

            Par.Add("@Consecutivo=" + consecutivo);
            Par.Add("@Folio=" + folio);
            Par.Add("@Caja=" + caja);
            Par.Add("@FOperacion=" + foperacion);

            ClaseReporte Reporte = new ClaseReporte(strReporte, Par, strServer, strDatabase, strUsuario, strPW);
            HttpContext.Current.Session["RepDoc"] = Reporte.RepDoc;
            HttpContext.Current.Session["ParametrosReporte"] = Par;
            Nueva_Ventana("../../Reporte/Reporte.aspx", "Carta", 0, 0, 0, 0);
            Reporte = null;
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error: " + ex.Message);
        }
    }

    protected void Nueva_Ventana(string Pagina, string Titulo, int Ancho, int Alto, int X, int Y)
    {

        ScriptManager.RegisterClientScriptBlock(this.upLanzarReporte,
                                         upLanzarReporte.GetType(),
                                            "ventana",
                                            "ShowWindow('" + Pagina + "','" + Titulo + "'," + Ancho + "," + Alto + "," + X + "," + Y + ")",
                                            true);

    }
}