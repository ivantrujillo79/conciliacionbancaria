using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Conciliacion.RunTime;
using Conciliacion.RunTime.ReglasDeNegocio;
using Conciliacion.RunTime.DatosSQL;
using CatalogoConciliacion.ReglasNegocio;
using Conciliacion.Migracion.Runtime.ReglasNegocio;

public partial class ReportesConciliacion_ReporteEstadoCuentaConciliados : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InicializarBancos();
            InicializaEstatusConcepto();
        }
    }


    /// <summary>
    /// 
    /// </summary>
    private void InicializarBancos()
    {
        SeguridadCB.Public.Usuario usuario;
        usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];

        List<InformeBancarioDatos.DetalleBanco> lstDetalle = new List<InformeBancarioDatos.DetalleBanco>();
        lstDetalle = consultarBancos(usuario.IdUsuario.ToString().Trim());
        DrpBancos.DataValueField = "IDBanco";
        DrpBancos.DataTextField = "Descripcion";
        DrpBancos.DataSource = lstDetalle;
        DrpBancos.DataBind();
        DrpBancos.SelectedIndex = -1;       

    }

    private void InicializaEstatusConcepto()
    {

        List<StatusConcepto> lstEstatusConcepto = new List<StatusConcepto>();
        lstEstatusConcepto = Conciliacion.Migracion.Runtime.App.Consultas.ObtenieneStatusConceptos();
        DrpEstatusConcepto.DataValueField = "Id";
        DrpEstatusConcepto.DataTextField = "Descripcion";
        DrpEstatusConcepto.DataSource = lstEstatusConcepto;
        DrpEstatusConcepto.DataBind();
        DrpEstatusConcepto.SelectedIndex = -1;
    }




    private List<InformeBancarioDatos.DetalleBanco> consultarBancos(string Usuario)
    {
        Conexion conexion = new Conexion();
        var lstDetalle = new List<InformeBancarioDatos.DetalleBanco>();

        try
        {
            var informeBancario = new InformeBancarioDatos(App.ImplementadorMensajes);
            conexion.AbrirConexion(false);
            lstDetalle = informeBancario.consultarBancos(conexion, 1, Usuario);
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

    /// <summary>
    /// Devuelve Cuentas Bancarias
    /// </summary>
    private void InicializarCuentas(int Banco, string DscBanco)
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
                if (item.Banco == Banco || (Banco == 0 && item.Banco == 0))
                {
                    Cuenta cuenta = new Cuenta(item.ID, item.NombreBanco + " " + item.Descripcion, item.Banco, item.NombreBanco);
                    ListaCuentasControlUsr.Add(cuenta);
                }
                else if (Banco == 0)
                {
                    Cuenta cuenta = new Cuenta(item.ID, item.NombreBanco + " " + item.Descripcion, item.Banco, item.NombreBanco);
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


    private List<InformeBancarioDatos.DetalleReporteEstadoCuentaConciliado> consultaReporteEstadoCuentaConciliado()
    {
        Conexion conexion = new Conexion();
        var lstDetalle = new List<InformeBancarioDatos.DetalleReporteEstadoCuentaConciliado>();

        try
        {
            var informeBancario = new InformeBancarioDatos(App.ImplementadorMensajes);
            DateTime fechaInicio = Convert.ToDateTime(txtFInicial.Text);
            DateTime fechaFin = Convert.ToDateTime(txtFFinal.Text);
            conexion.AbrirConexion(false);
            lstDetalle = informeBancario.consultaReporteEstadoCuentaConciliado(conexion, fechaInicio, fechaFin, DrpBancos.SelectedValue == "0" ? "" : DrpBancos.SelectedItem.Text.Trim(), "", DrpEstatusConcepto.SelectedValue == "0" ? "": DrpEstatusConcepto.SelectedValue, DrpEstatus.SelectedValue=="0"?"": DrpEstatus.SelectedValue);
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

    protected void DrpBancos_SelectedIndexChanged(object sender, EventArgs e)
    {        
        InicializarCuentas(int.Parse(DrpBancos.SelectedValue.ToString()), DrpBancos.SelectedItem.ToString());
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        try
        {
            List<InformeBancarioDatos.DetalleReporteEstadoCuentaConciliado> lstDetalle = new List<InformeBancarioDatos.DetalleReporteEstadoCuentaConciliado>();
            lstDetalle = consultaReporteEstadoCuentaConciliado();
            DateTime fechaInicio = Convert.ToDateTime(txtFInicial.Text);
            string cero;
            if (fechaInicio.Month < 10)
            {
                cero = "0";
            }
            else
            {
                cero = "";
            }
            ExportadorInformeBancario obExportador = new ExportadorInformeBancario(lstDetalle,
              HttpRuntime.AppDomainAppPath + @"InformesExcel\", "EdoCtaCon" + cero + fechaInicio.Month + fechaInicio.Year + ".xlsx", "Reporte",DrpBancos.SelectedItem.Text);
            obExportador.gerenerarEdoCtaConciliados();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg",
          @"alertify.alert('Conciliaci&oacute;n bancaria','Informe generado con éxito!', function(){document.getElementById('LigaDescarga').click(); });", true);

        }
        catch (Exception ex)
        {
            //    App.ImplementadorMensajes.MostrarMensaje(ex.Message);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg",
                @"alertify.alert('Conciliaci&oacute;n bancaria','Error: "
                + ex.Message + "', function(){ alertify.error('Error en la solicitud'); });", true);
        }
    }
}