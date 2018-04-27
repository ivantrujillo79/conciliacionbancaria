using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Conciliacion.RunTime.ReglasDeNegocio;

public partial class ReportesConciliacion_ReporteEstadoCuentaPorDia : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            InicializarCuentas();
            InicializarBancos();
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "UpdateMsg",
                "alertify.alert('Conciliaci&oacute;n bancaria','Error: " + ex.Message +
                "', function(){ alertify.error('Error en la solicitud'); });", true);
        }
    }

    private void InicializarCuentas()
    {
        //List<Cuenta> ListaCuentas = new List<Cuenta>();
        //try
        //{
        //    for (int i = 1; i <= 15; i++)
        //    {
        //        Cuenta cuenta = new Cuenta(i, "Cuenta " + (400 + (300 * i)));
        //        ListaCuentas.Add(cuenta);
        //    }

        //    WUCCuentasBancarias.Cuentas = ListaCuentas;
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}      
    }

    private void InicializarBancos()
    {
        //List<Cuenta> ListaBancos = new List<Cuenta>();
        //try
        //{
        //    for (int i = 1; i <= 6; i++)
        //    {
        //        Cuenta cuenta = new Cuenta(i, "Banco " + i);
        //        ListaBancos.Add(cuenta);
        //    }

        //    ddlBanco.DataValueField = "ID";
        //    ddlBanco.DataTextField = "Descripcion";
        //    ddlBanco.DataSource = ListaBancos;
        //    ddlBanco.DataBind();
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }

    private bool ValidarFechas()
    {
        return false;
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidarFechas())
            {

            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "UpdateMsg",
                "alertify.alert('Conciliaci&oacute;n bancaria','Error: " + ex.Message +
                "', function(){ alertify.error('Error en la solicitud'); });", true);
        }
    }
}