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
    public class GrupoConciliacionUsuarioDatos:GrupoConciliacionUsuario
    {

        
         public GrupoConciliacionUsuarioDatos(MensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }


         public GrupoConciliacionUsuarioDatos(int grupoConciliacion, string usuario, bool  accesoTotal, MensajesImplementacion implementadorMensajes)
            : base(grupoConciliacion,  usuario,  accesoTotal , implementadorMensajes)
        {
        }



        public override GrupoConciliacionUsuario CrearObjeto()
        {
            throw new NotImplementedException();
        }

        public override bool AgregaUsuario(int grupo, string usuario, bool acceso)
        {
            bool resultado = true;
            using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
            {
                try
                {
                cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaGrupoConciliacionUsuario", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.Int).Value = 0;//this.Configuracion;
                    comando.Parameters.Add("@GrupoConciliacion", System.Data.SqlDbType.Int).Value = grupo;
                    comando.Parameters.Add("@Usuario", System.Data.SqlDbType.VarChar).Value = usuario;
                    comando.Parameters.Add("@AccesoTotal ", System.Data.SqlDbType.Bit).Value = acceso;
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



        public override bool ModificaAcceso()
        {
            throw new Exception("The method or operation is not implemented.");  
        }



        public override bool EliminaUsuario(int grupo, string usuario)
        {
            bool resultado = true;
            using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaGrupoConciliacionUsuario", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.Int).Value = 2;//this.Configuracion;
                    comando.Parameters.Add("@GrupoConciliacion", System.Data.SqlDbType.Int).Value = grupo;
                    comando.Parameters.Add("@Usuario", System.Data.SqlDbType.VarChar).Value = usuario;
                    comando.Parameters.Add("@AccesoTotal ", System.Data.SqlDbType.Bit).Value = false;
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
