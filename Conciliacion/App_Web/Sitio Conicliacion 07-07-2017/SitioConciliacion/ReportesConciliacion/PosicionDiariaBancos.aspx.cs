using CatalogoConciliacion;
using Conciliacion.RunTime.DatosSQL;
using Conciliacion.RunTime.ReglasDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportesConciliacion_PosicionDiariaBancos : System.Web.UI.Page
{

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        try
        {
            List<InformeBancarioDatos.DetallePosicionDiariaBancos> lstDetalle = new List<InformeBancarioDatos.DetallePosicionDiariaBancos>();

            lstDetalle = ConsultarPosicionDiariaBancos();

            ExportadorInformeBancario obExportador = new ExportadorInformeBancario(lstDetalle,
                "C:\\Users\\DELL-Transforma\\Desktop\\", "MiExcel.xlsx", "Reporte");

            obExportador.generarPosicionDiariaBancos();

            ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg",
                @"alertify.alert('Conciliaci&oacute;n bancaria','Archivo creado!', function(){ console.log('success') });", true);

        }
        catch (Exception ex)
        {
            //App.ImplementadorMensajes.MostrarMensaje(ex.Message);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg",
                @"alertify.alert('Conciliaci&oacute;n bancaria','Error: "
                + ex.Message + "', function(){ alertify.error('Error en la solicitud'); });", true);
        }
    }


    protected void CrearExcel(object sender, EventArgs e)
    {
      
    }

    private List<InformeBancarioDatos.DetallePosicionDiariaBancos> ConsultarPosicionDiariaBancos()
    {
        Conexion conexion = new Conexion();
        var lstDetalle = new List<InformeBancarioDatos.DetallePosicionDiariaBancos>();

        try
        {
            var informeBancario = new InformeBancarioDatos(App.ImplementadorMensajes);
            var fechaInicio = new DateTime(2017, 12, 1);
            var fechaFin = new DateTime(2017, 12, 1);

            conexion.AbrirConexion(false);

            lstDetalle = informeBancario.consultaPosicionDiariaBanco(conexion, fechaInicio, fechaFin, 1);
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
}


