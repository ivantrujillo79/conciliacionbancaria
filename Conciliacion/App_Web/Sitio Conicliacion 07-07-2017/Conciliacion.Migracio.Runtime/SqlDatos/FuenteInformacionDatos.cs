using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.Migracion.Runtime.ReglasNegocio;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Conciliacion.Migracion.Runtime.SqlDatos
{
    class FuenteInformacionDatos : FuenteInformacion
    {
        public override bool Guardar()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBGuardaFuenteInformacion", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CuentaBancoFinanciero", System.Data.SqlDbType.VarChar).Value = this.CuentaBancoFinanciero;
                    cmd.Parameters.Add("@BancoFinanciero", System.Data.SqlDbType.SmallInt).Value = this.BancoFinanciero;
                    cmd.Parameters.Add("@FuenteInformacion", System.Data.SqlDbType.Int).Value = this.IdFuenteInformacion;
                    cmd.Parameters.Add("@RutaArchivo", System.Data.SqlDbType.VarChar).Value = this.RutaArchivo;
                    cmd.Parameters.Add("@TipoFuenteInformacion", System.Data.SqlDbType.SmallInt).Value = this.IdTipoFuenteInformacion;
                    cmd.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = this.IdSucursal;
                    cmd.Parameters.Add("@NumColumnas", System.Data.SqlDbType.Int).Value = this.NumColumnas;
                    cmd.Parameters.Add("@TipoArchivo", System.Data.SqlDbType.SmallInt).Value = this.IdTipoArchivo;
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

        //public override bool Actualizar()
        //{
        //    return true;
        //}
        public override bool Actualizar()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBActualizaFuenteInformacion", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CuentaBancoFinanciero", System.Data.SqlDbType.VarChar).Value = this.CuentaBancoFinanciero;
                    cmd.Parameters.Add("@BancoFinanciero", System.Data.SqlDbType.SmallInt).Value = this.BancoFinanciero;
                    cmd.Parameters.Add("@FuenteInformacion", System.Data.SqlDbType.Int).Value = this.IdFuenteInformacion;
                    cmd.Parameters.Add("@RutaArchivo", System.Data.SqlDbType.VarChar).Value = this.RutaArchivo;
                    cmd.Parameters.Add("@TipoArchivo", System.Data.SqlDbType.Int).Value = this.IdTipoArchivo;
                    cmd.Parameters.Add("@Columnas", System.Data.SqlDbType.Int).Value = this.NumColumnas;
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
                    SqlCommand cmd = new SqlCommand("spCBEliminaFuenteInformacion", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CuentaBancoFinanciero", System.Data.SqlDbType.VarChar).Value = this.CuentaBancoFinanciero;
                    cmd.Parameters.Add("@BancoFinanciero", System.Data.SqlDbType.SmallInt).Value = this.BancoFinanciero;
                    cmd.Parameters.Add("@FuenteInformacion", System.Data.SqlDbType.Int).Value = this.IdFuenteInformacion;
                    cmd.ExecuteNonQuery();
                }
                resultado = true;
                this.ImplementadorMensajes.MostrarMensaje("El registro se eliminó con éxito.");
            }
            catch (SqlException ex)
            {
                resultado = false;
                stackTrace = new StackTrace();
                if (ex.Number == 547)
                    this.ImplementadorMensajes.MostrarMensaje("La plantilla no puede ser eliminada, ya ha sido utilizada. Verifique el mapeo");
                else
                    this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return resultado;
        }

        public override bool CopiarFuenteInformacionDetalle(string idCuentaBancoFinancieroFuente)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBCopiarFuenteInformacionDetalle", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@BancoFinanciero", System.Data.SqlDbType.SmallInt).Value = this.BancoFinanciero;
                    cmd.Parameters.Add("@CuentaBancariaOrigen", System.Data.SqlDbType.VarChar).Value = idCuentaBancoFinancieroFuente;
                    cmd.Parameters.Add("@CuentaBancariaDestino", System.Data.SqlDbType.VarChar).Value = this.CuentaBancoFinanciero;
                    cmd.Parameters.Add("@FuenteInformacion", System.Data.SqlDbType.Int).Value = this.IdFuenteInformacion;
                    cmd.ExecuteNonQuery();
                }
                resultado = true;
                this.ImplementadorMensajes.MostrarMensaje("Copia realizada con éxito.");
            }
            catch (SqlException ex)
            {
                resultado = false;
                stackTrace = new StackTrace();
               if (ex.Number == 2627)
                    this.ImplementadorMensajes.MostrarMensaje("Los campos a copiar ya se encuentran mapeados en la Fuente Información. Verifique Mapeo");
                else
                    this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return resultado;
        }

        public override IObjetoBase CrearObjeto()
        {
            return new FuenteInformacionDatos();
        }
    }
}
