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
using System.IO;
using System.Globalization;

public partial class ReportesConciliacion_ReporteEstadoCuentaConciliados : System.Web.UI.Page
{
    Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                InicializarCampos();
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "ErrorMsg", @"alertify.alert('Conciliaci&oacute;n bancaria','Error: " + ex.Message + "', function(){ alertify.error('Error en la solicitud'); });", true);
        }
    }

    private void InicializarCampos()
    {
        if (Page.IsPostBack == false)
        {
            InicializarBancos();
            InicializaEstatusConcepto();
        }
  

        if (DrpBancos.SelectedItem != null)
        {
            InicializarCuentas(Convert.ToInt32(DrpBancos.SelectedValue),
                               DrpBancos.SelectedItem.Text);
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
            var informeBancario = new InformeBancarioDatos(objApp.ImplementadorMensajes);
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


            ListaCuentas = objApp.Consultas.ConsultaCuentasUsuario(usuario.IdUsuario.Trim());
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


    private List<List<InformeBancarioDatos.DetalleReporteEstadoCuentaConciliado>> consultaReporteEstadoCuentaConciliado()
    {
        Conexion conexion = new Conexion();
        var lstDetalle = new List<InformeBancarioDatos.DetalleReporteEstadoCuentaConciliado>();
        var ListasDetalle = new List<List<InformeBancarioDatos.DetalleReporteEstadoCuentaConciliado>>();

        try
        {
            var informeBancario = new InformeBancarioDatos(objApp.ImplementadorMensajes);
            DateTime fechaInicio = DateTime.ParseExact(txtFInicial.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime fechaFin = DateTime.ParseExact(txtFFinal.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            conexion.AbrirConexion(false);
            for( int i=0; i<= WUCListadoCuentasBancarias1.CuentasSeleccionadas.Count()-1; i++)
            {
                string banco = WUCListadoCuentasBancarias1.CuentasSeleccionadas[i].Descripcion.ToString().Substring(0, 20).TrimEnd();
                string numerocuenta = WUCListadoCuentasBancarias1.CuentasSeleccionadas[i].Descripcion.ToString().Substring(WUCListadoCuentasBancarias1.CuentasSeleccionadas[i].Descripcion.ToString().Length - 20).TrimStart();
                lstDetalle = informeBancario.consultaReporteEstadoCuentaConciliado(conexion, fechaInicio, fechaFin, banco, numerocuenta,  DrpEstatusConcepto.SelectedValue == "0" ? "" : DrpEstatusConcepto.SelectedValue, DrpEstatus.SelectedValue == "0" ? "" : DrpEstatus.SelectedValue);
            }

           
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
        return ListasDetalle;
    }

    public List<List<InformeBancarioDatos.DetalleReporteEstadoCuentaConciliado>> Separat(List<InformeBancarioDatos.DetalleReporteEstadoCuentaConciliado> source)
    {
        return source
            .GroupBy(s => s.CuentaBancoFinanciero)
            .OrderBy(g => g.Key)
            .Select(g => g.ToList())
            .ToList();
    }

    protected void DrpBancos_SelectedIndexChanged(object sender, EventArgs e)
    {        
        InicializarCuentas(int.Parse(DrpBancos.SelectedValue.ToString()), DrpBancos.SelectedItem.ToString());
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
       

        try
        {
             List<List<InformeBancarioDatos.DetalleReporteEstadoCuentaConciliado>> lstDetalleTotal = new List<List<InformeBancarioDatos.DetalleReporteEstadoCuentaConciliado>>();
         

            DateTime fechaInicio = DateTime.ParseExact(txtFInicial.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime fechaFin = DateTime.ParseExact(txtFFinal.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            Conexion conexion = new Conexion();
            var lstDetalle = new List<InformeBancarioDatos.DetalleReporteEstadoCuentaConciliado>();
            var ListasDetalle = new List<List<InformeBancarioDatos.DetalleReporteEstadoCuentaConciliado>>();

            string cero;
            int contador = 0;
            int registrofinal = 0;
            if (fechaInicio.Month < 10)
            {
                cero = "0";
            }
            else
            {
                cero = "";
            }

            if (File.Exists(HttpRuntime.AppDomainAppPath + @"InformesExcel\"+ "EdoCtaCon" + cero + fechaInicio.Month + fechaInicio.Year + ".xlsx"))
            {
                File.Delete(HttpRuntime.AppDomainAppPath + @"InformesExcel\"+ "EdoCtaCon" + cero + fechaInicio.Month + fechaInicio.Year + ".xlsx");
            }

            if (WUCListadoCuentasBancarias1.CuentasSeleccionadas != null && WUCListadoCuentasBancarias1.CuentasSeleccionadas.Count > 0)
            {
                Boolean esfinal = false;
                registrofinal = WUCListadoCuentasBancarias1.CuentasSeleccionadas.Count - 1;
                for (int i = 0; i <= WUCListadoCuentasBancarias1.CuentasSeleccionadas.Count() - 1; i++)
                {
                    var informeBancario = new InformeBancarioDatos(objApp.ImplementadorMensajes);
                    if (registrofinal == i)
                        esfinal = true;
                    conexion.AbrirConexion(false);
                    string banco = WUCListadoCuentasBancarias1.CuentasSeleccionadas[i].Descripcion.ToString().Substring(0, 20).TrimEnd();
                    string numerocuenta = WUCListadoCuentasBancarias1.CuentasSeleccionadas[i].Descripcion.ToString().Substring(WUCListadoCuentasBancarias1.CuentasSeleccionadas[i].Descripcion.ToString().Length - 20).TrimStart();
                    lstDetalle = informeBancario.consultaReporteEstadoCuentaConciliado(conexion, fechaInicio, fechaFin, banco, numerocuenta, DrpEstatusConcepto.SelectedValue == "0" ? "" : DrpEstatusConcepto.SelectedValue, DrpEstatus.SelectedValue == "0" ? "" : DrpEstatus.SelectedValue);
                    ExportadorInformeEstadoCuentaConciliado obExportador = new ExportadorInformeEstadoCuentaConciliado(lstDetalle,
                    HttpRuntime.AppDomainAppPath + @"InformesExcel\", "EdoCtaCon" + cero + fechaInicio.Month + fechaInicio.Year + ".xlsx", numerocuenta, banco, esfinal, "");
                    obExportador.FechaMesEncabezado = fechaInicio.ToString();
                    obExportador.generarInforme();

                    if (lstDetalle.Count > 0)
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg",
                            @"alertify.alert('Conciliaci&oacute;n bancaria','Informe generado con éxito!', function(){document.getElementById('LigaDescarga').click(); });", true);

                }
                contador = contador + 1;
            }
        }
        catch (Exception ex)
        {
            //    objApp.ImplementadorMensajes.MostrarMensaje(ex.Message);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg",
                @"alertify.alert('Conciliaci&oacute;n bancaria','Error: "
                + ex.Message + "', function(){ alertify.error('Error en la solicitud'); });", true);
        }
    }
}