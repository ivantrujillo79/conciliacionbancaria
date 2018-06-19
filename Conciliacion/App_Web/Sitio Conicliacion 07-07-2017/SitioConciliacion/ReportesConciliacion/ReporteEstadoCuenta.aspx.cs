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
using SeguridadCB.Public;
using DetalleReporteEstadoCuentaDia = Conciliacion.RunTime.DatosSQL.InformeBancarioDatos.DetalleReporteEstadoCuentaDia;
using DetalleInformeInternosAFuturo = Conciliacion.RunTime.DatosSQL.ExportadorInformeInternosAFuturoDatos.DetalleInformeInternosAFuturo;
using System.IO;
using System.Globalization;

public partial class ReportesConciliacion_ReporteEstadoCuenta : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            if (!IsPostBack)
            {
                Usuario usuario;
                usuario = (Usuario)HttpContext.Current.Session["Usuario"];
             
                hdfIniEmpresa.Value = usuario.InicialCorporativo ;
                InicializarBancos();
                if (btnlista.Items.Count > 0)
                {
                    btnlista_SelectedIndexChanged(sender, e);
                }
            }
            else
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
                    Cuenta cuenta = new Cuenta(item.ID, item.NombreBanco.Trim() + " " + item.Descripcion, item.Banco, item.NombreBanco);
                    ListaCuentasControlUsr.Add(cuenta);
                }
                else if (Banco == 0)
                {
                    Cuenta cuenta = new Cuenta(item.ID, item.NombreBanco.Trim() + " " + item.Descripcion, item.Banco, item.NombreBanco);
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
        btnlista.SelectedIndex = -1;


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

    private bool ValidarFechas()
    {
        return false;
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        DataTable dtEmpresas = new DataTable();
        Usuario usuario;
        usuario = (Usuario)HttpContext.Current.Session["Usuario"];        
        DateTime fechaInicio = Convert.ToDateTime(txtFInicial.Text);
        var rutaCompleta = HttpRuntime.AppDomainAppPath + @"InformesExcel\";
        string cero ;
        if (fechaInicio.Month < 10)
        {
            cero = "0";
        }
        else
        {
            cero = "";
        }
      
        string Pagina = Request.QueryString["Reporte"];
        if (Pagina != null)
        {
            if (Pagina == "2")
            {
            var   Archivo = "EdoCta" + usuario.InicialCorporativo + cero + fechaInicio.Month + fechaInicio.Year + ".xlsx";
                try
                 {
                    if (WUCListadoCuentasBancarias1.CuentasSeleccionadas!=null)// mcc 2018 0503
                    {
                        if (WUCListadoCuentasBancarias1.CuentasSeleccionadas.Count > 0)
                    {
                        if (File.Exists(rutaCompleta + Archivo)) File.Delete(rutaCompleta + Archivo);
                        foreach (Cuenta cuenta in WUCListadoCuentasBancarias1.CuentasSeleccionadas) {                            
                            List<InformeBancarioDatos.DetalleReporteEstadoCuenta> lstDetalleCuenta = new List<InformeBancarioDatos.DetalleReporteEstadoCuenta>();
                            lstDetalleCuenta = consultaReporteEstadoCuenta(cuenta.ID.ToString());                        
                            ExportadorInformeEstadoCuenta obExportador = new ExportadorInformeEstadoCuenta(lstDetalleCuenta,
                            rutaCompleta, Archivo, cuenta.Descripcion);
                            obExportador.generarInforme(cuenta.Descripcion);
                        }
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg",
                          @"alertify.alert('Conciliaci&oacute;n bancaria','¡Informe estado cuenta generado con éxito!', function(){document.getElementById('LigaDescarga').click(); });", true);

                    }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", @"alertify.alert('Conciliaci&oacute;n bancaria','Error: " + "Seleccione una cuenta bancaria" + "', function(){ alertify.error('Error en la solicitud'); });", true);
                    }
                }
                catch (Exception ex)
                    {
                        //    App.ImplementadorMensajes.MostrarMensaje(ex.Message);
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg",
                            @"alertify.alert('Conciliaci&oacute;n bancaria','Error: "
                            + ex.Message + "', function(){ alertify.error('Error en la solicitud'); });", true);
                    }
            }
                if (Pagina == "3")
            {             
               dtEmpresas = usuario.CorporativoAcceso;
                try
                {
                var  Archivo = "EdoCtaDia" + usuario.InicialCorporativo + cero + fechaInicio.Month + fechaInicio.Year + ".xlsx";
                    if (WUCListadoCuentasBancarias1.CuentasSeleccionadas != null && WUCListadoCuentasBancarias1.CuentasSeleccionadas.Count > 0)
                    {
                        if (File.Exists(rutaCompleta + Archivo)) File.Delete(rutaCompleta + Archivo);

                         List<List<InformeBancarioDatos.DetalleReporteEstadoCuentaDia>> lstDetalle = new List<List<InformeBancarioDatos.DetalleReporteEstadoCuentaDia>>();
                            lstDetalle = consultaReporteEstadoCuentaDia("");
                            ExportadorInformeEstadoCuentaDia obExportador = new ExportadorInformeEstadoCuentaDia(lstDetalle,
                            rutaCompleta, Archivo, "");
                            obExportador.generarInforme();       
                       
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg",
                            @"alertify.alert('Conciliaci&oacute;n bancaria','¡Informe generado con éxito!', function(){document.getElementById('LigaDescarga').click(); });", true);
                    }
                }
                catch (Exception ex)
                {
                    //    App.ImplementadorMensajes.MostrarMensaje(ex.Message);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg",
                        @"alertify.alert('Conciliaci&oacute;n bancaria','Error: "
                        + ex.Message + "', function(){ alertify.error('Error en la solicitud'); });", true);
                }

            }

            if (Pagina == "4")
            {
                dtEmpresas = usuario.CorporativoAcceso;
                string fechaIni = txtFInicial.Text;
                string fechaFin = txtFFinal.Text;
                string empresa = usuario.NombreCorporativo;

                try
                {
                    var Archivo = "InternosConciliarFuturo" + string.Format("{0:ddMMyyyy}", DateTime.Now) + ".xlsx";

                    if (WUCListadoCuentasBancarias1.CuentasSeleccionadas.Count > 0)
                    {
                        if (File.Exists(rutaCompleta + Archivo)) File.Delete(rutaCompleta + Archivo);

                        foreach (Cuenta cuenta in WUCListadoCuentasBancarias1.CuentasSeleccionadas)
                        {
                            List<List<DetalleInformeInternosAFuturo>> lstDetalle = new List<List<DetalleInformeInternosAFuturo>>();
                            lstDetalle = consultaInformeInternosAFuturo(cuenta.Descripcion.ToString());
                            ExportadorInformeInternosAFuturo obExportador = new ExportadorInformeInternosAFuturo(lstDetalle, rutaCompleta, Archivo, cuenta.Descripcion, cuenta.NombreBanco, fechaIni, fechaFin, empresa);
                            obExportador.generarInforme();
                        }

                        ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg",
                            @"alertify.alert('Conciliaci&oacute;n bancaria','¡Informe generado con éxito!', function(){document.getElementById('LigaDescarga').click(); });", true);
                    }
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
    }


    private List<InformeBancarioDatos.DetalleReporteEstadoCuenta> consultaReporteEstadoCuenta(string cuenta)
    {
        Conexion conexion = new Conexion();
        var lstDetalleCuenta = new List<InformeBancarioDatos.DetalleReporteEstadoCuenta>();
        string Banco = btnlista.SelectedItem.Text;        
        try
        {
            var informeBancario = new InformeBancarioDatos(App.ImplementadorMensajes);
            DateTime fechaInicio = DateTime.ParseExact(txtFInicial.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime fechaFin = DateTime.ParseExact(txtFFinal.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            conexion.AbrirConexion(false);
            lstDetalleCuenta = informeBancario.consultaReporteEstadoCuenta(conexion, fechaInicio, fechaFin, Banco, cuenta);
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
        return lstDetalleCuenta;
    }

    private List<List<DetalleReporteEstadoCuentaDia>> consultaReporteEstadoCuentaDia(string cuenta)
    {
        Conexion conexion = new Conexion();
        var lstDetalle = new List<InformeBancarioDatos.DetalleReporteEstadoCuentaDia>();
        var ListasDetalle = new List<List<DetalleReporteEstadoCuentaDia>>();
        string Banco = btnlista.SelectedItem.Text;

        try
        {
            var informeBancario = new InformeBancarioDatos(App.ImplementadorMensajes);
            DateTime fechaInicio = DateTime.ParseExact(txtFInicial.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime fechaFin = DateTime.ParseExact(txtFFinal.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            conexion.AbrirConexion(false);
            lstDetalle = informeBancario.consultaReporteEstadoCuentaPorDia(conexion, fechaInicio, fechaFin,Banco, cuenta);

            ListasDetalle = Separat(lstDetalle);

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
        //Se regresa una lista de listas... con las cuentas separadas...
        return ListasDetalle;
    }

    private List<List<DetalleInformeInternosAFuturo>> consultaInformeInternosAFuturo(string cuenta)
   {
       Conexion conexion = new Conexion();
       var lstDetalle = new List<ExportadorInformeInternosAFuturoDatos.DetalleInformeInternosAFuturo>();
       var ListasDetalle = new List<List<DetalleInformeInternosAFuturo>>();
       string Banco = btnlista.SelectedItem.Text;

       try
       {
           var informeBancario = new InformeBancarioDatos(App.ImplementadorMensajes);
            DateTime fechaInicio = DateTime.ParseExact(txtFInicial.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime fechaFin = DateTime.ParseExact(txtFFinal.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            conexion.AbrirConexion(false);
           lstDetalle = informeBancario.consultaconsultaInformeInternosAFuturo(conexion, fechaInicio, fechaFin, Banco, cuenta);

           //ListasDetalle = Separat(lstDetalle);

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
       //Se regresa una lista de listas... con las cuentas separadas...
       return ListasDetalle;
   }


    protected void btnlista_SelectedIndexChanged(object sender, EventArgs e)
    {
        InicializarCuentas(int.Parse(btnlista.SelectedValue.ToString()), btnlista.SelectedItem.ToString());
    }

    public List<List<DetalleReporteEstadoCuentaDia>> Separat(List<DetalleReporteEstadoCuentaDia> source)
    {
        return source
            .GroupBy(s => s.CuentaBancoFinanciero)
            .OrderBy(g => g.Key)
            .Select(g => g.ToList())
            .ToList();
    }
}