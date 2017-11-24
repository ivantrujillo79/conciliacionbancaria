using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.ReglasDeNegocio;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Conciliacion.RunTime.DatosSQL
{
    public class FacturasComplementoDatos: FacturasComplemento
    {
        public FacturasComplementoDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

       

        public FacturasComplementoDatos(int corporativoConciliacion, int sucursalConciliacion, int anioConciliacion, int mesConciliacion,
           int folioConciliacion, IMensajesImplementacion implementadorMensajes)
        :base(corporativoConciliacion, sucursalConciliacion, anioConciliacion, mesConciliacion, folioConciliacion, implementadorMensajes)
        {
        }

        public override FacturasComplemento CrearObjeto()
        {
            return new FacturasComplementoDatos(this.ImplementadorMensajes);
        }

        public override bool Guardar(Conexion _conexion)
        {
            bool resultado = false;
            try
            {

                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBFacturaComplementoAltaModifica";
                _conexion.Comando.Parameters.Clear();
                _conexion.Comando.Parameters.Add("@CorporativoConciliacion", System.Data.SqlDbType.Int).Value = this.CorporativoConciliacion;
                _conexion.Comando.Parameters.Add("@SucursalConciliacion", System.Data.SqlDbType.Int).Value = this.SucursalConciliacion;
                _conexion.Comando.Parameters.Add("@AñoConciliacion", System.Data.SqlDbType.Int).Value = this.AnioConciliacion;
                _conexion.Comando.Parameters.Add("@MesConciliacion", System.Data.SqlDbType.Int).Value = this.MesConciliacion;
                _conexion.Comando.Parameters.Add("@FolioConciliacion", System.Data.SqlDbType.Int).Value = this.FolioConciliacion;

                _conexion.Comando.ExecuteNonQuery();
                this.ImplementadorMensajes.MostrarMensaje("Registro Guardado Con éxito");
                resultado = true;
            }

            catch (Exception ex)
            {
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("No se pudo guardar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
                resultado = false;
            }

            return resultado;
        }
    }
}
