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
    public class ReferenciaACompararDatos : ReferenciaAComparar
    {

         public ReferenciaACompararDatos(MensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

         public ReferenciaACompararDatos( int tipoConciliacion, string tipoConciliaciondescripcion, int secuencia, string columnaDestinoExt, string columnaDestinoInt, string status, MensajesImplementacion implementadorMensajes)
            : base( tipoConciliacion, tipoConciliaciondescripcion, secuencia, columnaDestinoExt,columnaDestinoInt,status, implementadorMensajes)
        {
        }

         public override ReferenciaAComparar CrearObjeto()
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
                    SqlCommand comando = new SqlCommand("spCBActualizaReferenciaAComparar", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.Int).Value = 0;//this.Configuracion;
                    comando.Parameters.Add("@Secuencia", System.Data.SqlDbType.Int).Value = this.Secuencia;
                    comando.Parameters.Add("@ColumnaDestinoExt", System.Data.SqlDbType.VarChar).Value = this.ColumnaDestinoExt;
                    comando.Parameters.Add("@ColumnaDestinoInt", System.Data.SqlDbType.VarChar).Value = this.ColumnaDestinoInt;
                    comando.Parameters.Add("@TipoConciliacion", System.Data.SqlDbType.Int).Value = this.TipoConciliacion;
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


        public override bool CambiarStatus( int tipo, int sec)
        {

            bool resultado = true;
            using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaReferenciaAComparar", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.Int).Value = 1;//this.Configuracion;
                    comando.Parameters.Add("@Secuencia", System.Data.SqlDbType.Int).Value = sec;
                    comando.Parameters.Add("@ColumnaDestinoExt", System.Data.SqlDbType.VarChar).Value = this.ColumnaDestinoExt;
                    comando.Parameters.Add("@ColumnaDestinoInt", System.Data.SqlDbType.VarChar).Value = this.ColumnaDestinoInt;
                    comando.Parameters.Add("@TipoConciliacion", System.Data.SqlDbType.Int).Value = tipo;
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
