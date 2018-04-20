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
        List<Caja> ListaCajas = new List<Caja>();
        try
        {
            for (int i = 1; i <= 6; i++)
            {
                Caja Caja = new Caja(i, "Caja " +i);
                ListaCajas.Add(Caja);
            }

            wucListadoCajas1.Cajas = ListaCajas;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    // se inicializa la funcion de exprtar datos

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        try
        {
          List<InformeBancarioDatos.DetallePosicionDiariaBancos> lstDetalle = new List<InformeBancarioDatos.DetallePosicionDiariaBancos>();
           lstDetalle = ConsultarPosicionDiariaBancos();
           ExportadorInformeBancario obExportador = new ExportadorInformeBancario(lstDetalle,
           @"C:\Users\Transforma1\Source\repos\conciliacionbancaria\Conciliacion\App_Web\Sitio Conicliacion 07-07-2017\SitioConciliacion\InformesExcel\", "InformePosicionDiariaBancos.xlsx", "Reporte");

           obExportador.generarPosicionDiariaBancos();
           
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



    private List<InformeBancarioDatos.DetallePosicionDiariaBancos> ConsultarPosicionDiariaBancos()
    {
        Conexion conexion = new Conexion();
        var lstDetalle = new List<InformeBancarioDatos.DetallePosicionDiariaBancos>();

        try
        {
            var informeBancario = new InformeBancarioDatos(App.ImplementadorMensajes);
            DateTime fechaInicio = Convert.ToDateTime(txtFInicial.Text);
            DateTime fechaFin = Convert.ToDateTime(txtFFinal.Text);
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