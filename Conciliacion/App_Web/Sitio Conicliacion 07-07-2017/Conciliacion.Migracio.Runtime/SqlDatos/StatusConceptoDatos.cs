using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.Migracion.Runtime.ReglasNegocio;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace Conciliacion.Migracion.Runtime.SqlDatos
{
    class StatusConceptoDatos : StatusConcepto
    {
        public override bool Guardar()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBActualizaStatusConceptoEtiqueta", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Configuracion", SqlDbType.Int).Value = 0;
                    cmd.Parameters.Add("@StatusConcepto", SqlDbType.Int).Value = 0;
                    cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = this.Descripcion;
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@UsuarioAlta", SqlDbType.VarChar).Value = this.Usuario;
                    cmd.Parameters.Add("@Etiqueta ", System.Data.SqlDbType.Int).Value = 0;
                    cmd.ExecuteNonQuery();
                }
                resultado = true;
                this.ImplementadorMensajes.MostrarMensaje("El registro se guardó con éxito.");
            }
            catch (SqlException ex)
            {
                resultado = false;
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
                throw (ex);
            }
            return resultado;

        }

        public override bool ActualizarDescripcion()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBActualizaStatusConceptoEtiqueta", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Configuracion", SqlDbType.Int).Value = 1;
                    cmd.Parameters.Add("@StatusConcepto", SqlDbType.Int).Value = this.Id;
                    cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = this.Descripcion;
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@UsuarioAlta", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@Etiqueta ", System.Data.SqlDbType.Int).Value = 0;
                    cmd.ExecuteNonQuery();
                }
                resultado = true;
                this.ImplementadorMensajes.MostrarMensaje("El registro se Actualizó con éxito.");
            }
            catch (SqlException ex)
            {
                resultado = false;
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return resultado;
        }

        public override bool CambiarStatus()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBActualizaStatusConceptoEtiqueta", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Configuracion", SqlDbType.VarChar).Value = 2;
                    cmd.Parameters.Add("@StatusConcepto", SqlDbType.Int).Value = this.Id;
                    cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@UsuarioAlta", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@Etiqueta ", System.Data.SqlDbType.Int).Value = 0;
                    cmd.ExecuteNonQuery();
                }
                resultado = true;
                this.ImplementadorMensajes.MostrarMensaje("El registro se Actualizó con éxito.");
            }
            catch (SqlException ex)
            {
                resultado = false;
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return resultado;
        }

        public override bool Eliminar()
        {
            return true;
        }

        public override bool AgregaEtiquetaStatus(int etiqueta)
        {
            bool resultado = true;
            using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaStatusConceptoEtiqueta", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.Int).Value = 3;//this.Configuracion;
                    comando.Parameters.Add("@StatusConcepto", System.Data.SqlDbType.Int).Value = this.Id;
                    comando.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@UsuarioAlta", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@Status", SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@Etiqueta ", System.Data.SqlDbType.Int).Value = etiqueta;
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



        public override bool EliminaEtiquetaStatus(int etiqueta)
        {
            bool resultado = true;
            using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaStatusConceptoEtiqueta", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.Int).Value = 4;//this.Configuracion;
                    comando.Parameters.Add("@StatusConcepto", System.Data.SqlDbType.Int).Value = this.Id;
                    comando.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@UsuarioAlta", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@Status", SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@Etiqueta ", System.Data.SqlDbType.Int).Value = etiqueta;
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

        public StatusConceptoDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }


        public StatusConceptoDatos(int statusConceptoId, string descripcion, string usuario, string status, DateTime fAlta, IMensajesImplementacion implementadorMensajes)
            : base(statusConceptoId, descripcion, usuario, status, fAlta, implementadorMensajes)
        {
        }



        public override StatusConcepto CrearObjeto()
        {
            return new StatusConceptoDatos(this.implementadorMensajes);
        }
    }
}
