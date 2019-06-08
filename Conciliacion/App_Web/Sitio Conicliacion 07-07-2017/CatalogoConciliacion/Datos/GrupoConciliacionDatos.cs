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
    public class GrupoConciliacionDatos : GrupoConciliacion
    {


        public GrupoConciliacionDatos(MensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }


        public GrupoConciliacionDatos(int grupoConciliacion, string descripcion, string usuario, string status, DateTime fAlta, int diasDefault, int diasMaxima, int diasMinima, decimal diferenciaDefault, decimal diferenciaMaxima, decimal diferenciaMinima, MensajesImplementacion implementadorMensajes)
            : base(grupoConciliacion, descripcion, usuario, status, fAlta, diasDefault, diasMaxima, diasMinima, diferenciaDefault, diferenciaMaxima, diferenciaMinima, implementadorMensajes)
        {
        }

        public override GrupoConciliacion CrearObjeto()
        {
            throw new NotImplementedException();
        }

        public override bool Guardar(string nombre, string usuario, int diasDefault, int diasMaxima, int diasMinima, decimal diferenciaDefault, decimal diferenciaMaxima, decimal diferenciaMinima)
        {
            bool resultado = true;
            using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaGrupoConciliacion", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.Int).Value = 0;//this.Configuracion;
                    comando.Parameters.Add("@GrupoConciliacion", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar).Value = nombre;
                    comando.Parameters.Add("@Status", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@UsuarioAlta", System.Data.SqlDbType.VarChar).Value = usuario;
                    comando.Parameters.Add("@DiasDefault", System.Data.SqlDbType.Int).Value = diasDefault;
                    comando.Parameters.Add("@DiasMaxima", System.Data.SqlDbType.Int).Value = diasMaxima;
                    comando.Parameters.Add("@DiasMinima", System.Data.SqlDbType.Int).Value = diasMinima;
                    comando.Parameters.Add("@DiferenciaDefault", System.Data.SqlDbType.Decimal).Value = diferenciaDefault;
                    comando.Parameters.Add("@DiferenciaMaxima", System.Data.SqlDbType.Decimal).Value = diferenciaMaxima;
                    comando.Parameters.Add("@DiferenciaMinima", System.Data.SqlDbType.Decimal).Value = diferenciaMinima;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.ExecuteNonQuery();
                    resultado = true;
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


        public override bool CambiarStatus(int grupo)
        {
            bool resultado = true;
            using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaGrupoConciliacion", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.Int).Value = 2;//this.Configuracion;
                    comando.Parameters.Add("@GrupoConciliacion", System.Data.SqlDbType.Int).Value = grupo;
                    comando.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@Status", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@UsuarioAlta", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@DiasDefault", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@DiasMaxima", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@DiasMinima", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@DiferenciaDefault", System.Data.SqlDbType.Decimal).Value = 0.0;
                    comando.Parameters.Add("@DiferenciaMaxima", System.Data.SqlDbType.Decimal).Value = 0.0;
                    comando.Parameters.Add("@DiferenciaMinima", System.Data.SqlDbType.Decimal).Value = 0.0;
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
        public override bool AñadirStatusConcepto(int statusconcepto)
        {
            bool resultado = true;
            using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaGrupoConciliacionStatusConcepto", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.Int).Value = 0;//this.Configuracion;
                    comando.Parameters.Add("@GrupoConciliacion", System.Data.SqlDbType.Int).Value = this.GrupoConciliacionId;
                    comando.Parameters.Add("@StatusConcepto", System.Data.SqlDbType.Int).Value = statusconcepto;

                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.ExecuteNonQuery();
    
                }
                catch (SqlException ex)
                {
                    stackTrace = new StackTrace();
                    this.ImplementadorMensajes.MostrarMensaje("No se pudo modificar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                    stackTrace = null;
                    resultado = false;
                }
                catch (Exception ex)
                {
                    stackTrace = new StackTrace();
                    this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                    stackTrace = null;
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
            }
            return resultado;
        }
        public override bool QuitarStatusConcepto(int statusconcepto)
        {
            bool resultado = true;
            using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaGrupoConciliacionStatusConcepto", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.Int).Value = 1;//this.Configuracion;
                    comando.Parameters.Add("@GrupoConciliacion", System.Data.SqlDbType.Int).Value = this.GrupoConciliacionId;
                    comando.Parameters.Add("@StatusConcepto", System.Data.SqlDbType.Int).Value = statusconcepto;

                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.ExecuteNonQuery();
                   
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
