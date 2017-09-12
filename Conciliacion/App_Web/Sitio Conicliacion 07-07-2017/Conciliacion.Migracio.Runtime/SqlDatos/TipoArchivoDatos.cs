using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.Migracion.Runtime.ReglasNegocio;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Conciliacion.Migracion.Runtime.SqlDatos
{
    public class TipoArchivoDatos:TipoArchivo
    {
        public override bool Guardar()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBGuardaTipoArchivo", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@TipoArchivo", System.Data.SqlDbType.SmallInt).Value = this.IdTipoArchivo;
                    cmd.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar).Value = this.Descripcion;
                    cmd.Parameters.Add("@FormatoDeFecha", System.Data.SqlDbType.VarChar).Value = this.FormatoFecha;
                    cmd.Parameters.Add("@FormatoDeMoneda", System.Data.SqlDbType.VarChar).Value = this.FormatoMoneda;
                    cmd.Parameters.Add("@Separador", System.Data.SqlDbType.VarChar).Value = this.Separador;
                    cmd.Parameters.Add("@Usuario", System.Data.SqlDbType.Char).Value = this.Usuario;
                    cmd.Parameters.Add("@Status", System.Data.SqlDbType.VarChar).Value = this.Status;
                    cmd.Parameters.Add("@FAlta", System.Data.SqlDbType.DateTime).Value = this.FAlta;
                    cmd.ExecuteNonQuery();
                }
                resultado = true;
                this.ImplementadorMensajes.MostrarMensaje("El registro se Guardó con éxito.");
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
                    SqlCommand cmd = new SqlCommand("spCBActualizaTipoArchivo", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@TipoArchivo", System.Data.SqlDbType.SmallInt).Value = this.IdTipoArchivo;
                    cmd.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar).Value = this.Descripcion;
                    cmd.Parameters.Add("@FormatoDeFecha", System.Data.SqlDbType.VarChar).Value = this.FormatoFecha;
                    cmd.Parameters.Add("@FormatoDeMoneda", System.Data.SqlDbType.VarChar).Value = this.FormatoMoneda;
                    cmd.Parameters.Add("@Separador", System.Data.SqlDbType.VarChar).Value = this.Separador;
                    cmd.Parameters.Add("@Usuario", System.Data.SqlDbType.Char).Value = this.Usuario;
                    cmd.Parameters.Add("@Status", System.Data.SqlDbType.VarChar).Value = this.Status;
                    cmd.Parameters.Add("@FAlta", System.Data.SqlDbType.DateTime).Value = this.FAlta;
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

        public override IObjetoBase CrearObjeto()
        {
            return new TipoArchivoDatos();
        }
    }
}
