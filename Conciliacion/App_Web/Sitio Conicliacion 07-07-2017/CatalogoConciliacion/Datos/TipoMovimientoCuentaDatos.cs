using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CatalogoConciliacion.ReglasNegocio;
using System.Data.SqlClient;
using System.Diagnostics;
using Conciliacion.RunTime;

namespace CatalogoConciliacion.Datos
{
    public class TipoMovimientoCuentaDatos : TipoMovimientoCuenta
    {

         public TipoMovimientoCuentaDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public TipoMovimientoCuentaDatos(int tipoMovimiento, string cuenta, IMensajesImplementacion implementadorMensajes)
            : base(tipoMovimiento, cuenta, implementadorMensajes)
        {
        }

         public override TipoMovimientoCuenta CrearObjeto()
        {
            return new TipoMovimientoCuentaDatos(this.ImplementadorMensajes);
        }


        public override bool Guardar()
        {
            bool resultado = true;
            using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBInsertaImportacionAplicacionCuenta", cnn);
                    comando.Parameters.Add("@ImportacionAplicacion", System.Data.SqlDbType.Int).Value = this.TipoMovimiento;//this.Configuracion;
                    comando.Parameters.Add("@CuentaBanco", System.Data.SqlDbType.VarChar).Value = this.Cuenta;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.ExecuteNonQuery();
                    this.ImplementadorMensajes.MostrarMensaje("Registro Guardado Con éxito");
                }

                catch (SqlException ex)
                {
                    stackTrace = new StackTrace();
                    this.ImplementadorMensajes.MostrarMensaje("No se pudo guardar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                    stackTrace = null;
                    resultado = false;
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
            }
            return resultado;
        }

        public override bool Eliminar(int tipoMovimiento, string cuenta)
        {
            bool resultado = true;
            using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBEliminaImportacionAplicacionCuenta", cnn);
                    comando.Parameters.Add("@ImportacionAplicacion", System.Data.SqlDbType.Int).Value = tipoMovimiento;//this.Configuracion;
                    comando.Parameters.Add("@CuentaBanco", System.Data.SqlDbType.VarChar).Value = cuenta;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.ExecuteNonQuery();
                    this.ImplementadorMensajes.MostrarMensaje("Registro Eliminado Con éxito");
                }

                catch (SqlException ex)
                {
                    stackTrace = new StackTrace();
                    this.ImplementadorMensajes.MostrarMensaje("No se pudo eliminar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                    stackTrace = null;
                    resultado = false;
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
            }
            return resultado;
        }

    }
}
