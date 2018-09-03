using CatalogoConciliacion;
using Conciliacion.RunTime.DatosSQL;
using Conciliacion.RunTime.ReglasDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DetalleReporteEstadoCuentaDia = Conciliacion.RunTime.DatosSQL.InformeBancarioDatos.DetalleReporteEstadoCuentaDia;

public partial class ReportesConciliacion_ReporteDiario : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {          
            InicializarCajas();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "UpdateMsg",
                "alertify.alert('Conciliaci&oacute;n bancaria','Error: " + ex.Message +
                "', function(){ alertify.error('Error en la solicitud'); });", true);
        }
    }

    private void InicializarCajas()
    {
        
    }
    // se inicializa la funcion de exprtar datos

    protected void btnConsultar_Click(object sender, EventArgs e)
    {        
       try
        {
            List < List <InformeBancarioDatos.DetalleReporteEstadoCuentaDia>> lstDetalle = new List<List<InformeBancarioDatos.DetalleReporteEstadoCuentaDia>>();
            //lstDetalle = consultaReporteEstadoCuentaDia( );
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

            ExportadorInformeEstadoCuentaDia obExportador = new ExportadorInformeEstadoCuentaDia(lstDetalle,
                HttpRuntime.AppDomainAppPath+@"InformesExcel\", "EdoCuenta"+cero + fechaInicio.Month + fechaInicio.Year + ".xlsx", "Reporte", "", DateTime.Parse(txtFInicial.Text), DateTime.Parse(txtFInicial.Text), "");
            obExportador.generarInforme();           
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



    private List<List<DetalleReporteEstadoCuentaDia>> consultaReporteEstadoCuentaDia()
    {
        Conexion conexion = new Conexion();
        var lstDetalle = new List<InformeBancarioDatos.DetalleReporteEstadoCuentaDia>();
        var ListasDetalle = new List<List<DetalleReporteEstadoCuentaDia>>();

        try
        {
            var informeBancario = new InformeBancarioDatos(App.ImplementadorMensajes);
            DateTime fechaInicio = Convert.ToDateTime("01/01/2018");
            DateTime fechaFin = Convert.ToDateTime("17/01/2018");
            conexion.AbrirConexion(false);
            lstDetalle = informeBancario.consultaReporteEstadoCuentaPorDia(conexion, fechaInicio, fechaFin, "", "");

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

    public List<List<DetalleReporteEstadoCuentaDia>> Separat(List<DetalleReporteEstadoCuentaDia> source)
    {
        return source
            .GroupBy(s => s.CuentaBancoFinanciero)
            .OrderBy(g => g.Key)
            .Select(g => g.ToList())
            .ToList();
    }
}