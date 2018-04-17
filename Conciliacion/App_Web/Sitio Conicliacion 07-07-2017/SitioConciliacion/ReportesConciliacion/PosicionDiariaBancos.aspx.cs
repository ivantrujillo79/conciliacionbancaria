using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Conciliacion.RunTime.ReglasDeNegocio;
using Conciliacion.RunTime.DatosSQL;
using DetalleCaja = Conciliacion.RunTime.DatosSQL.InformeBancarioDatos.DetalleCaja;

public partial class ReportesConciliacion_PosicionDiariaBancos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                CargarListadoCajas();
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMsg", "alertify.alert('Conciliaci&oacute;n bancaria','Error: " 
                + ex.Message + "', function(){ alertify.error('Error en la solicitud'); });", true);
        }
    }

    private void CargarListadoCajas()
    {
        DetalleCaja detalleCaja = new DetalleCaja();
        List<DetalleCaja> lsDetalleCaja;
        Caja caja;
        List<Caja> lsCajas = new List<Caja>();
        Conexion conexion = new Conexion();

        try
        {
            conexion.AbrirConexion(false);
            lsDetalleCaja = detalleCaja.consultarCajas(conexion, 0);

            if (lsDetalleCaja != null && lsDetalleCaja.Count > 0)
            {
                foreach (DetalleCaja item in lsDetalleCaja)
                {
                    caja = new Caja(
                        item.IDCaja,
                        item.Descripcion
                    );
                    lsCajas.Add(caja);
                }

                wucCajas.Cajas = lsCajas;
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
        finally
        {
            conexion.CerrarConexion();
        }
    }
}