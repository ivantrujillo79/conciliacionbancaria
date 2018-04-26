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
            if (!IsPostBack)
            {
                InicializarBancos();
            }


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "UpdateMsg",
                "alertify.alert('Conciliaci&oacute;n bancaria','Error: " + ex.Message +
                "', function(){ alertify.error('Error en la solicitud'); });", true);
        }
    }




/// <summary>
/// Devuelve Cuentas Bancarias
/// </summary>
    private void InicializarCuentas(int Banco,string DscBanco)
    {
        List<Cuenta> ListaCuentas = new List<Cuenta>();
        List<Cuenta> ListaCuentasControlUsr = new List<Cuenta>();
        List<Cuenta> ListaCuentasPorBanco = new List<Cuenta>();

        SeguridadCB.Public.Usuario usuario;

        try
        {
            usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];


            ListaCuentas = Conciliacion.RunTime.App.Consultas.ConsultaCuentasUsuario(usuario.IdUsuario.Trim());
            foreach (Cuenta item in ListaCuentas)
            {
                if (item.Banco==Banco || (Banco == 0 && item.Banco == 0))
                {
                    Cuenta cuenta = new Cuenta(item.ID, item.NombreBanco + " " + item.Descripcion, item.Banco,item.NombreBanco);
                    ListaCuentasControlUsr.Add(cuenta);
                }
                else if(Banco == 0)
                {
                    Cuenta cuenta = new Cuenta(item.ID, item.NombreBanco + " "+ item.Descripcion, item.Banco,item.NombreBanco);
                    ListaCuentasControlUsr.Add(cuenta);
                }
            }

            WUCListadoCuentasBancarias1.Cuentas = ListaCuentasControlUsr;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void InicializarBancos()
    {
        SeguridadCB.Public.Usuario usuario;
        usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];

        List<InformeBancarioDatos.DetalleBanco> lstDetalle = new List<InformeBancarioDatos.DetalleBanco>();
        lstDetalle = consultarBancos(usuario.IdUsuario.ToString().Trim());
        btnlista.DataValueField = "IDBanco";
        btnlista.DataTextField = "Descripcion";
        btnlista.DataSource = lstDetalle;
        btnlista.DataBind();
        btnlista.SelectedIndex =-1;


    }

    private List<InformeBancarioDatos.DetalleBanco> consultarBancos(string Usuario)
    {
        Conexion conexion = new Conexion();
        var lstDetalle = new List<InformeBancarioDatos.DetalleBanco>();

        try
        {
            var informeBancario = new InformeBancarioDatos(App.ImplementadorMensajes);          
            conexion.AbrirConexion(false);
            lstDetalle = informeBancario.consultarBancos(conexion,1,Usuario);
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
        try
        {
            if (ValidarFechas())
            {

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "UpdateMsg",
                "alertify.alert('Conciliaci&oacute;n bancaria','Error: " + ex.Message +
                "', function(){ alertify.error('Error en la solicitud'); });", true);
        }
    }




    protected void btnlista_SelectedIndexChanged(object sender, EventArgs e)
    {
        InicializarCuentas(int.Parse(btnlista.SelectedValue.ToString()), btnlista.SelectedItem.ToString());
    }
}