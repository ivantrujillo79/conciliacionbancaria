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
    public class MotivoNoConciliadoDatos : MotivoNoConciliado
    {

        public MotivoNoConciliadoDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public MotivoNoConciliadoDatos(int motivonoconciliado, string descripcion, string status, IMensajesImplementacion implementadorMensajes)
            : base(motivonoconciliado, descripcion, status, implementadorMensajes)
        {
        }

        public override MotivoNoConciliado CrearObjeto()
        {
            throw new NotImplementedException();
        }


        public override bool Guardar()
        {
            bool resultado = true;
            using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaMotivoNoConciliado", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.Int).Value = 0;//this.Configuracion;
                    comando.Parameters.Add("@MotivoNoConciliado ", System.Data.SqlDbType.Int).Value = this.MotivoNoConciliadoId;
                    comando.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar).Value = this.Descripcion;
                    comando.Parameters.Add("@Status", System.Data.SqlDbType.VarChar).Value = this.Status;
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

        public override bool Modificar()
        {
            throw new Exception("The method or operation is not implemented.");
        }


        public override bool CambiarStatus(int motivo)
        {

            bool resultado = true;
            using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaMotivoNoConciliado", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.Int).Value = 2;//this.Configuracion;
                    comando.Parameters.Add("@MotivoNoConciliado ", System.Data.SqlDbType.Int).Value = motivo;
                    comando.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@Status", System.Data.SqlDbType.VarChar).Value = "";
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.ExecuteNonQuery();
                    this.ImplementadorMensajes.MostrarMensaje("Registro Modificado Con éxito");
                }
                catch (SqlException ex)
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