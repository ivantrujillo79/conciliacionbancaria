using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Conciliacion.RunTime;
using Conciliacion.RunTime.ReglasDeNegocio;
using Conciliacion.RunTime.DatosSQL;
using System.Data;
using System.Data.SqlClient;

public partial class ReportesConciliacion_ReporteEstadoCuenta : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string Pagina = Request.QueryString["Reporte"];
            if (Pagina != null)
            {
                if (Pagina == "2" | Pagina == "3")
                {
                    InicializarCuentas();
                    InicializarBancos();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "UpdateMsg",
                                    "alertify.alert('Conciliaci&oacute;n bancaria','Error: Solo se puede acceder 2 o 3 " +
                                    "', function(){ alertify.error('Error en la solicitud'); });", true);
                }
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "UpdateMsg",
                "alertify.alert('Conciliaci&oacute;n bancaria','Error: " + ex.Message +
                "', function(){ alertify.error('Error en la solicitud'); });", true);
        }
    }


    private void InicializarCuentas()
    {
        List<Cuenta> ListaCuentas = new List<Cuenta>();
        try
        {
            for (int i = 1; i <= 15; i++)
            {
                Cuenta cuenta = new Cuenta(i, "Cuenta " + (400 + (300 * i)));
                ListaCuentas.Add(cuenta);
            }

            WUCListadoCuentasBancarias1.Cuentas = ListaCuentas;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void InicializarBancos()
    {
        List<InformeBancarioDatos.DetalleBanco> lstDetalle = new List<InformeBancarioDatos.DetalleBanco>();
        lstDetalle = consultarBancos();
        btnlista.DataValueField = "IDBanco";
        btnlista.DataTextField = "Descripcion";
        btnlista.DataSource = lstDetalle;
        btnlista.DataBind();

    }

    private List<InformeBancarioDatos.DetalleBanco> consultarBancos()
    {
        Conexion conexion = new Conexion();
        var lstDetalle = new List<InformeBancarioDatos.DetalleBanco>();

        try
        {
            var informeBancario = new InformeBancarioDatos(App.ImplementadorMensajes);
            conexion.AbrirConexion(false);
            lstDetalle = informeBancario.consultarBancos(conexion, 1);
        }
        catch (Exception ex)
        {
            //App.ImplementadorMensajes.MostrarMensaje(ex.Message);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", @"alertify.alert('Conciliaci&oacute;n bancaria','Error: " + ex.Message + "', function(){ alertify.error('Error en la solicitud'); });", true);
        }
        finally
        {
            conexion.CerrarConexion();
        }
        return lstDetalle;
    }

    private bool ValidarFechas()
    {
        return false;
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        string Pagina = Request.QueryString["Reporte"];
        if (Pagina != null)
        {

            if (Pagina == "2")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg",
         @"alertify.alert('Conciliaci&oacute;n bancaria','Informe Estado de cuenta generado con éxito!', function(){document.getElementById('LigaDescarga').click(); });", true);
            }
            if (Pagina == "3")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg",
         @"alertify.alert('Conciliaci&oacute;n bancaria','Informe Estado de cuenta por dia generado con éxito!', function(){document.getElementById('LigaDescarga').click(); });", true);
            }

        }

    }
}