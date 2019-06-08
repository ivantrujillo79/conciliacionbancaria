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
    Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
    CatalogoConciliacion.App objAppCat = new CatalogoConciliacion.App();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public GrupoConciliacionUsuario LeerGrupoConciliacionUsuarioEspecifico(string usuario)
    {
        return objAppCat.Consultas.ObtieneGrupoConciliacionUsuarioEspecifico(usuario);
    }

    private bool FiltroCorrecto()
    {
        bool resultado = true;
        StringBuilder mensaje = new StringBuilder();

        if (txtClienteID.Text.Trim() == string.Empty)
            txtClienteID.Text = "0";
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
                int ClientesConSaldo = 2;
                if (txtClienteID.Text.Trim() == string.Empty)
                {
                    txtClienteID.Text = "0";
                }
                if (ddlClientesConSaldo.Text == "TODOS")
                    ClientesConSaldo = 2;
                else
                if (ddlClientesConSaldo.Text == "SI")
                    ClientesConSaldo = 1;
                else
                if (ddlClientesConSaldo.Text == "NO")
                    ClientesConSaldo = 0;

                AppSettingsReader settings = new AppSettingsReader();

                //Leer Variables URL 
                DateTime fInicial = Convert.ToDateTime(txtFInicio.Text); 
                DateTime fFinal = Convert.ToDateTime(txtFFInal.Text); 
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
                    Par.Add("@FechaFin=" + fFinal.ToShortDateString());
                    Par.Add("@ClientesConSaldo=" + ClientesConSaldo.ToString());
                    //Par.Add("@ClientesConSaldo='" + ddlClientesConSaldo.Text + "'");

                    StringBuilder strInfoParam = new StringBuilder();
                    strInfoParam.Append("Periodo: ");
                    strInfoParam.Append(fInicial.ToShortDateString());
                    strInfoParam.Append(" a ");
                    strInfoParam.Append(fFinal.ToShortDateString());
                    if (ClienteID == 0)
                        strInfoParam.Append(" | Clientes: Todos");
                    else
                        strInfoParam.Append(" | Cliente: " + ClienteID.ToString());
                    strInfoParam.Append(" | Con Saldo: " + ddlClientesConSaldo.Text);

                    ClaseReporte reporte = new ClaseReporte(strReporte, Par, strServer, strDatabase, strUsuario, strPW, strInfoParam.ToString());
                    HttpContext.Current.Session["RepDoc"] = reporte.RepDoc;
                    HttpContext.Current.Session["ParametrosReporte"] = Par;

                    StringBuilder sbScript = new StringBuilder();
                    sbScript.Append("<script language='javascript'>");
                    sbScript.Append("window.open('");
                    sbScript.Append("../Reporte/Reporte.aspx");
                    sbScript.Append("', 'CustomPopUp',");
                    sbScript.Append("'width=1200, height=400, menubar=yes, resizable=no');<");
                    sbScript.Append("/script>");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "@@@@MyPopUpScript", sbScript.ToString(), false);

                }
                catch (Exception ex)
                {
                    objApp.ImplementadorMensajes.MostrarMensaje("Error: Generar Reporte\n" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                objApp.ImplementadorMensajes.MostrarMensaje("Error: Leer Valores.\n" + ex.Message);
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