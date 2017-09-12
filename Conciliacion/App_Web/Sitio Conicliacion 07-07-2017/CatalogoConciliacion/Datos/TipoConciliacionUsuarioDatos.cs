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
    public class TipoConciliacionUsuarioDatos : TipoConciliacionUsuario
    {

         public TipoConciliacionUsuarioDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }


         public TipoConciliacionUsuarioDatos(int tipoConciliacion, string usuario, IMensajesImplementacion implementadorMensajes)
            : base(tipoConciliacion,  usuario,  implementadorMensajes)
        {
        }



         public override TipoConciliacionUsuario CrearObjeto()
        {
            throw new NotImplementedException();
        }

        public override bool AsignarTipo(int tipo, string usuario)
        {
            bool resultado = true;
            using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaTipoConciliacionUsuario", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.Int).Value = 0;//this.Configuracion;
                    comando.Parameters.Add("@TipoConciliacion", System.Data.SqlDbType.Int).Value = tipo;
                    comando.Parameters.Add("@Usuario", System.Data.SqlDbType.VarChar).Value = usuario;
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


        public override bool DesasignarTipo(int tipo, string usuario)
        {
            bool resultado = true;
            using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaTipoConciliacionUsuario", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.Int).Value = 1;//this.Configuracion;
                    comando.Parameters.Add("@TipoConciliacion", System.Data.SqlDbType.Int).Value = tipo;
                    comando.Parameters.Add("@Usuario", System.Data.SqlDbType.VarChar).Value = usuario;
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
