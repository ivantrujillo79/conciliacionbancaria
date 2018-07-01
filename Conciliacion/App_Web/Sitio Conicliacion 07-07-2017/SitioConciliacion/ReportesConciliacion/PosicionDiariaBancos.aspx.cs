using CatalogoConciliacion;
using Conciliacion.RunTime.DatosSQL;
using Conciliacion.RunTime.ReglasDeNegocio;
using SeguridadCB.Public;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
public partial class ReportesConciliacion_PosicionDiariaBancos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                InicializarCajas();
                Usuario usuario;
                usuario = (Usuario)HttpContext.Current.Session["Usuario"];
                hdfIniEmpresa.Value = usuario.InicialCorporativo;

            }
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
        InformeBancarioDatos.DetalleCaja obDetalleCaja = new InformeBancarioDatos.DetalleCaja();
        Conexion conexion = new Conexion();
        try
        {
            conexion.AbrirConexion(false);

            wucListadoCajas1.Cajas = obDetalleCaja.consultarCajas(conexion, 0);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conexion.CerrarConexion();
        }
    }
    // se inicializa la funcion de exprtar datos

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
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
        Usuario usuario;
        usuario = (Usuario)HttpContext.Current.Session["Usuario"];
        var rutaCompleta = HttpRuntime.AppDomainAppPath + @"InformesExcel\";
        var Archivo = "PosicionDiaria" + usuario.InicialCorporativo + cero + fechaInicio.Month + fechaInicio.Year + ".xlsx";
        int contador = 0;
        try
        {
            if (wucListadoCajas1.CajasSeleccionadas.Count > 0)
            {
               
                if (File.Exists(rutaCompleta+ Archivo)) File.Delete(rutaCompleta+ Archivo);
                foreach (Caja caja in wucListadoCajas1.CajasSeleccionadas)
                {
                    contador = contador + 1;
                 List<InformeBancarioDatos.DetallePosicionDiariaBancos> lstDetalle = new List<InformeBancarioDatos.DetallePosicionDiariaBancos>();
                 lstDetalle = ConsultarPosicionDiariaBancos(caja.ID);                   
                 ExportadorInformeBancario obExportador = new ExportadorInformeBancario(lstDetalle,
                 rutaCompleta,Archivo, caja.Descripcion);
                 obExportador.generarPosicionDiariaBancos(0);

                    if (wucListadoCajas1.CajasSeleccionadas.Count() == contador)
                           {
                        obExportador.generarPosicionDiariaBancos(1);
                          }
                    
                                  

                }

                

                //List<InformeBancarioDatos.DetallePosicionDiariaBancos> lstDetallet = new List<InformeBancarioDatos.DetallePosicionDiariaBancos>();
                //lstDetallet = ConsultarPosicionDiariaBancos(caja.ID);
                //ExportadorInformeBancario obExportador = new ExportadorInformeBancario(lstDetalle,
                //rutaCompleta, Archivo, "TOTAL");
                //obExportador.generarPosicionDiariaBancos();

                ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg",
                               @"alertify.alert('Conciliaci&oacute;n bancaria','¡Informe generado con éxito!', function(){document.getElementById('LigaDescarga').click(); });", true);
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg",
                               @"alertify.alert('Conciliaci&oacute;n bancaria','Error: lista de cajas vacia "
                               , true);

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




    private List<InformeBancarioDatos.DetallePosicionDiariaBancos> ConsultarPosicionDiariaBancos(int caja)
    {
        Conexion conexion = new Conexion();
        var lstDetalle = new List<InformeBancarioDatos.DetallePosicionDiariaBancos>();       
            try
        {                     
               
                var informeBancario = new InformeBancarioDatos(App.ImplementadorMensajes);

            DateTime fechaInicio = DateTime.ParseExact(txtFInicial.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime fechaFin = DateTime.ParseExact(txtFFinal.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            conexion.AbrirConexion(false);
                lstDetalle = informeBancario.consultaPosicionDiariaBanco( conexion, fechaInicio, fechaFin,(byte)caja);
            
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