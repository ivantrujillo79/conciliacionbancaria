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
    public class ParametroAplicacionDatos : ParametroAplicacion
    {

         public ParametroAplicacionDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public ParametroAplicacionDatos(string parametro, string valor, string observaciones, IMensajesImplementacion implementadorMensajes)
            : base(parametro, valor, observaciones, implementadorMensajes)
        {
        }

         public override ParametroAplicacion CrearObjeto()
        {
            return new ParametroAplicacionDatos(this.ImplementadorMensajes);
        }

        public override bool Consultar()
        {
            bool resultado = true;
            using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBConsultaParametroNoDocumento", cnn);
                    comando.Parameters.Add("@Modulo", System.Data.SqlDbType.Int).Value = 30;
                    comando.Parameters.Add("@Parametro", System.Data.SqlDbType.VarChar).Value = this.Parametro;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        this.Valor = Convert.ToString(reader["Valor"]);
                        this.Observaciones = Convert.ToString(reader["Observaciones"]);
                    }
                    else
                    {
                        this.ImplementadorMensajes.MostrarMensaje("No existe parametro");
                        stackTrace = null;
                        resultado = false;
                    }
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

        public override bool Modificar()
        {
            bool resultado = true;
            using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaParametroNoDocumento", cnn);
                    comando.Parameters.Add("@Modulo", System.Data.SqlDbType.Int).Value = 30;
                    comando.Parameters.Add("@Valor", System.Data.SqlDbType.VarChar).Value = this.Valor;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.ExecuteNonQuery();
                   // this.ImplementadorMensajes.MostrarMensaje("Registro Modificado Con éxito");
                }

                catch (Exception ex)
                {
                    stackTrace = new StackTrace();
                    this.ImplementadorMensajes.MostrarMensaje("No se pudo modificar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
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
