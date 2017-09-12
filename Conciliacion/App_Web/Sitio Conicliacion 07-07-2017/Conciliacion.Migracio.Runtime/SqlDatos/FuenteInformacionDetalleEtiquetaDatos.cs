using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Conciliacion.Migracion.Runtime.SqlDatos
{
    public class FuenteInformacionDetalleEtiquetaDatos:Conciliacion.Migracion.Runtime.ReglasNegocio.FuenteInformacionDetalleEtiqueta
    {


        public override bool Guardar()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBGuardaFuenteInformacionDetalleEtiqueta", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CuentaBancoFinanciero", System.Data.SqlDbType.VarChar).Value = this.CuentaBancoFinanciero;
                    cmd.Parameters.Add("@BancoFinanciero", System.Data.SqlDbType.SmallInt).Value = this.IdBancoFinanciero;
                    cmd.Parameters.Add("@FuenteInformacion", System.Data.SqlDbType.Int).Value = this.IdFuenteInformacion;
                    cmd.Parameters.Add("@Secuencia", System.Data.SqlDbType.Int).Value = this.Secuencia;
                    cmd.Parameters.Add("@Etiqueta", System.Data.SqlDbType.SmallInt).Value = this.IdEtiqueta;
                    cmd.Parameters.Add("@LongitudFija", System.Data.SqlDbType.SmallInt).Value = this.LongitudFija;
                    cmd.Parameters.Add("@Finaliza", System.Data.SqlDbType.VarChar).Value = this.Finaliza;
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
                    SqlCommand cmd = new SqlCommand("spCBActualizaFuenteInformacionDetalleEtiqueta", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CuentaBancoFinanciero", System.Data.SqlDbType.VarChar).Value = this.CuentaBancoFinanciero;
                    cmd.Parameters.Add("@BancoFinanciero", System.Data.SqlDbType.SmallInt).Value = this.IdBancoFinanciero;
                    cmd.Parameters.Add("@FuenteInformacion", System.Data.SqlDbType.Int).Value = this.IdFuenteInformacion;
                    cmd.Parameters.Add("@Secuencia", System.Data.SqlDbType.Int).Value = this.Secuencia;
                    cmd.Parameters.Add("@Etiqueta", System.Data.SqlDbType.SmallInt).Value = this.IdEtiqueta;
                    cmd.Parameters.Add("@LongitudFija", System.Data.SqlDbType.SmallInt).Value = this.LongitudFija;
                    cmd.Parameters.Add("@Finaliza", System.Data.SqlDbType.VarChar).Value = this.Finaliza;
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
                    SqlCommand cmd = new SqlCommand("spCBEliminaFuenteInformacionDetalleEtiqueta", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CuentaBancoFinanciero", System.Data.SqlDbType.VarChar).Value = this.CuentaBancoFinanciero;
                    cmd.Parameters.Add("@BancoFinanciero", System.Data.SqlDbType.SmallInt).Value = this.IdBancoFinanciero;
                    cmd.Parameters.Add("@FuenteInformacion", System.Data.SqlDbType.Int).Value = this.IdFuenteInformacion;
                    cmd.Parameters.Add("@Secuencia", System.Data.SqlDbType.Int).Value = this.Secuencia;
                    cmd.Parameters.Add("@Etiqueta", System.Data.SqlDbType.SmallInt).Value = this.IdEtiqueta;
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
            return new FuenteInformacionDetalleEtiquetaDatos();
        }
    }
}
