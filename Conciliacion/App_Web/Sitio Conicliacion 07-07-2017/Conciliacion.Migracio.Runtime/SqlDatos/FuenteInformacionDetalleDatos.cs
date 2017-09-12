using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.Migracion.Runtime.ReglasNegocio;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Conciliacion.Migracion.Runtime.SqlDatos
{
    public class FuenteInformacionDetalleDatos : FuenteInformacionDetalle
    {
        public override bool Guardar()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBGuardaFuenteInformacionDetalle", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CuentaBancoFinanciero", System.Data.SqlDbType.VarChar).Value = this.CuentaBancoFinanciero;
                    cmd.Parameters.Add("@BancoFinanciero", System.Data.SqlDbType.SmallInt).Value = this.BancoFinanciero;
                    cmd.Parameters.Add("@FuenteInformacion", System.Data.SqlDbType.Int).Value = this.IdFuenteInformacion;
                    cmd.Parameters.Add("@Secuencia", System.Data.SqlDbType.Int).Value = this.Secuencia;
                    cmd.Parameters.Add("@ColumnaOrigen", System.Data.SqlDbType.VarChar).Value = this.ColumnaOrigen;
                    cmd.Parameters.Add("@TablaDestino", System.Data.SqlDbType.VarChar).Value = this.TablaDestino;
                    cmd.Parameters.Add("@ColumnaDestino", System.Data.SqlDbType.VarChar).Value = this.ColumnaDestino;
                    cmd.Parameters.Add("@ConceptoBanco", System.Data.SqlDbType.SmallInt).Value = this.IdConceptoBanco;
                    cmd.Parameters.Add("@EsTipoFecha", System.Data.SqlDbType.Bit).Value = this.EsTipoFecha;
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
            throw new NotImplementedException();
        }

        public override bool Eliminar()
        {
             bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBEliminaFuenteInformacionDetalle", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CuentaBancoFinanciero", System.Data.SqlDbType.VarChar).Value = this.CuentaBancoFinanciero;
                    cmd.Parameters.Add("@BancoFinanciero", System.Data.SqlDbType.SmallInt).Value = this.BancoFinanciero;
                    cmd.Parameters.Add("@FuenteInformacion", System.Data.SqlDbType.Int).Value = this.IdFuenteInformacion;
                    cmd.Parameters.Add("@Secuencia", System.Data.SqlDbType.Int).Value = this.Secuencia;
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

        public override IObjetoBase CrearObjeto()
        {
            return  new FuenteInformacionDetalleDatos();
        }
    }
}
