using CatalogoConciliacion.ReglasNegocio;
using Conciliacion.RunTime;
using System;
using System.Activities.Expressions;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportesConciliacion_ReporteSaldoAnticiposxCliente : System.Web.UI.Page
{
    private SeguridadCB.Public.Operaciones operaciones;
    private SeguridadCB.Public.Usuario usuario;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public GrupoConciliacionUsuario LeerGrupoConciliacionUsuarioEspecifico(string usuario)
    {
        return CatalogoConciliacion.App.Consultas.ObtieneGrupoConciliacionUsuarioEspecifico(usuario);
    }

    private bool FiltroCorrecto()
    {
        bool resultado = true;
        StringBuilder mensaje = new StringBuilder();

        if (String.IsNullOrEmpty(txtFInicio.Text))
        {
            mensaje.Append("la Fecha Inicial");
            resultado = false;
        }
        else if (String.IsNullOrEmpty(txtFFInal.Text))
        {
            mensaje.Append("la Fecha Final");
            resultado = false;
        }

        if ( !resultado )
        { 
            ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg",
            "alertify.alert('Conciliaci&oacute;n bancaria','Verifique "+mensaje+"', "
            + "function(){ alertify.error('Error en la solicitud'); });", true);
        }
        return resultado;
    }

    protected void imgExportar_Click(object sender, ImageClickEventArgs e)
    {
        if (FiltroCorrecto())
        {
            try
            {
                if (String.IsNullOrEmpty(txtClienteID.Text))
                {
                    ddlTodos.SelectedIndex = 0; //Si es todos los clientes
                }
                if (ddlTodos.SelectedIndex == 0)
                {
                    txtClienteID.Text = "";
                }
                if (txtClienteID.Text.Trim() == string.Empty)
                {
                    txtClienteID.Text = "0";
                }

                AppSettingsReader settings = new AppSettingsReader();

                //Leer Variables URL 
                DateTime fInicial = Convert.ToDateTime(txtFInicio.Text); //Convert.ToDateTime("01/01/2000");
                DateTime fFinal = Convert.ToDateTime(txtFFInal.Text); //Convert.ToDateTime("01/01/2018");
                Boolean Todos = ddlTodos.SelectedIndex == 0; //todos los clientes
                int ClienteID = Convert.ToInt32(txtClienteID.Text);

                usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
                string strReporte;
                bool accesototal = LeerGrupoConciliacionUsuarioEspecifico(usuario.IdUsuario.Trim()).AccesoTotal;
                strReporte = Server.MapPath("~/") + settings.GetValue("ReporteSaldoAnticipoxCliente", typeof(string));

                if (!File.Exists(strReporte)) return;
                try
                {
                    string strServer = settings.GetValue("Servidor", typeof(string)).ToString();
                    string strDatabase = settings.GetValue("Base", typeof(string)).ToString();

                    string strUsuario = usuario.IdUsuario.Trim();
                    string strPW = usuario.ClaveDesencriptada;
                    ArrayList Par = new ArrayList();

                    Par.Add("@Cliente=" + ClienteID.ToString());
                    Par.Add("@FechaIni=" + fInicial.ToShortDateString());
                    Par.Add("@FechaFin=" + fInicial.ToShortDateString());
                    Par.Add("@Todos=" + Todos.ToString());

                    ClaseReporte reporte = new ClaseReporte(strReporte, Par, strServer, strDatabase, strUsuario, strPW);
                    HttpContext.Current.Session["RepDoc"] = reporte.RepDoc;
                    HttpContext.Current.Session["ParametrosReporte"] = Par;
                    //Nueva_Ventana("../Reporte/Reporte.aspx", "Carta", 0, 0, 0, 0);

                    StringBuilder sbScript = new StringBuilder();
                    sbScript.Append("<script language='javascript'>");
                    sbScript.Append("window.open('");
                    sbScript.Append("../Reporte/Reporte.aspx");
                    sbScript.Append("', 'CustomPopUp',");
                    sbScript.Append("'width=1200, height=400, menubar=yes, resizable=no');<");
                    sbScript.Append("/script>");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "@@@@MyPopUpScript", sbScript.ToString(), false);

                    //reporte = null;
                }
                catch (Exception ex)
                {
                    App.ImplementadorMensajes.MostrarMensaje("Error: Generar Reporte\n" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                App.ImplementadorMensajes.MostrarMensaje("Error: Leer Valores.\n" + ex.Message);
            }
        }
    }

    protected void btnActualizarConfig_Click(object sender, ImageClickEventArgs e)
    {
        //hdfFechaIni.Value = txtFInicial.Text;
        //hdfFechaFin.Value = txtFFinal.Text;
        //hdfCliente.Value = ddlCuentaBancaria.Text.Trim();
        //hdfTodos.Value = true;
    }

}