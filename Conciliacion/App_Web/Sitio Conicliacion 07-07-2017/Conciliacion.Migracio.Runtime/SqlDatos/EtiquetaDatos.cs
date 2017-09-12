using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace Conciliacion.Migracion.Runtime.SqlDatos
{
    public class EtiquetaDatos:Conciliacion.Migracion.Runtime.ReglasNegocio.Etiqueta
    {
        public override bool Guardar()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBGuardaEtiqueta", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Etiqueta", SqlDbType.SmallInt).Value = this.Id;
                    cmd.Parameters.Add("@BancoFinanciero", SqlDbType.SmallInt).Value = this.IdBancoFinanciero;
                    cmd.Parameters.Add("@TipoFuenteInformacion", SqlDbType.SmallInt).Value = this.IdTipoFuenteInformacion;
                    cmd.Parameters.Add("@Decripcion", SqlDbType.VarChar).Value = this.Descripcion;
                    cmd.Parameters.Add("@TipoDato", SqlDbType.VarChar).Value = this.TipoDato;
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = this.Status;
                    cmd.Parameters.Add("@FAlta", SqlDbType.DateTime).Value = this.FAlta;
                    cmd.Parameters.Add("@UsuarioAlta", SqlDbType.Char).Value =  string.IsNullOrEmpty(this.UsuarioAlta ) ? null : this.UsuarioAlta;
                    cmd.Parameters.Add("@FBaja", SqlDbType.DateTime).Value = this.FBaja;
                    cmd.Parameters.Add("@UsuarioBaja", SqlDbType.Char).Value = string.IsNullOrEmpty( this.UsuarioBaja)?null:  this.UsuarioBaja;
                    cmd.Parameters.Add("@TablaDestino", System.Data.SqlDbType.VarChar).Value = this.TablaDestino;
                    cmd.Parameters.Add("@ColumnaDestino", System.Data.SqlDbType.VarChar).Value = this.ColumnaDestino;
                    cmd.Parameters.Add("@ConcatenaEtiqueta", System.Data.SqlDbType.Bit).Value = this.ConcatenaEtiqueta;
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
            }
            return resultado;
        }

        public override bool Actualizar()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBActualizaEtiqueta", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Etiqueta", SqlDbType.SmallInt).Value = this.Id;
                    cmd.Parameters.Add("@BancoFinanciero", SqlDbType.SmallInt).Value = this.IdBancoFinanciero;
                    cmd.Parameters.Add("@TipoFuenteInformacion", SqlDbType.SmallInt).Value = this.IdTipoFuenteInformacion;
                    cmd.Parameters.Add("@Decripcion", SqlDbType.VarChar).Value = this.Descripcion;
                    cmd.Parameters.Add("@TipoDato", SqlDbType.VarChar).Value = this.TipoDato;
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = this.Status;
                    cmd.Parameters.Add("@FAlta", SqlDbType.DateTime).Value = this.FAlta;
                    cmd.Parameters.Add("@UsuarioAlta", SqlDbType.Char).Value = string.IsNullOrEmpty(this.UsuarioAlta) ? null : this.UsuarioAlta;
                    cmd.Parameters.Add("@FBaja", SqlDbType.DateTime).Value = this.FBaja;
                    cmd.Parameters.Add("@UsuarioBaja", SqlDbType.Char).Value = string.IsNullOrEmpty(this.UsuarioBaja) ? null : this.UsuarioBaja;
                    cmd.Parameters.Add("@TablaDestino", System.Data.SqlDbType.VarChar).Value = this.TablaDestino;
                    cmd.Parameters.Add("@ColumnaDestino", System.Data.SqlDbType.VarChar).Value = this.ColumnaDestino;
                    cmd.ExecuteNonQuery();
                }

                resultado = true;
                this.ImplementadorMensajes.MostrarMensaje("El registro se actualizó con éxito.");
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
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBEliminaEtiqueta", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Etiqueta", SqlDbType.SmallInt).Value = this.Id;
                    cmd.ExecuteNonQuery();
                }
                resultado = true;
                this.ImplementadorMensajes.MostrarMensaje("El registro se eliminó con éxito.");
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

        public override ReglasNegocio.IObjetoBase CrearObjeto()
        {
            return new EtiquetaDatos();
        }
    }
}
