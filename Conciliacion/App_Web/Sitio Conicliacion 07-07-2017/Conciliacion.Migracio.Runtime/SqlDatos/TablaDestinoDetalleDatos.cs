using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.Migracion.Runtime.ReglasNegocio;
using System.Data.SqlClient;
using Conciliacion.RunTime.DatosSQL;

namespace Conciliacion.Migracion.Runtime.SqlDatos
{
    class TablaDestinoDetalleDatos : TablaDestinoDetalle
    {
        public override bool Actualizar()
        {
            throw new NotImplementedException();
        }

        public override IObjetoBase CrearObjeto()
        {
            return new TablaDestinoDetalleDatos();
        }

        public override bool Eliminar()
        {
            throw new NotImplementedException();
        }

        public override bool Guardar()
        {
            return true;
        }

        public void ActualizarClientePago(SqlConnection cnn)
        {
            //bool resultado = false;

            if (this.IdCorporativo <= 0 | this.IdSucursal <= 0 | this.Anio <= 0 | this.Folio <= 0 | this.Secuencia <= 0)
                throw new Exception("Faltan parametros para ejecutar actualizacion");

            if (this.ClientePago <= 0)
                throw new Exception("No se especificó un cliente para actualizar la partida de pago.");

            try
            {
                SqlCommand cmd = new SqlCommand("spCBActualizarClientePago", cnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@Corporativo", System.Data.SqlDbType.TinyInt)).Value = this.IdCorporativo;
                cmd.Parameters.Add(new SqlParameter("@Sucursal", System.Data.SqlDbType.TinyInt)).Value = this.IdSucursal;
                cmd.Parameters.Add(new SqlParameter("@Año", System.Data.SqlDbType.Int)).Value = this.Anio;
                cmd.Parameters.Add(new SqlParameter("@Folio", System.Data.SqlDbType.Int)).Value = this.Folio;
                cmd.Parameters.Add(new SqlParameter("@Secuencia", System.Data.SqlDbType.Int)).Value = this.Secuencia;
                cmd.Parameters.Add(new SqlParameter("@ClientePago", System.Data.SqlDbType.Int)).Value = this.ClientePago;
                int RegistrosAfectados = cmd.ExecuteNonQuery();

                if(RegistrosAfectados == 0)
                    throw new Exception("No se encontró un pago para actualizar el cliente " + this.ClientePago.ToString());

                //resultado = true;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
            //return resultado;
        }

        public int ExisteClientePago(SqlConnection cnn)
        {
            int resultado = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("spCBExisteClientePago", cnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@Corporativo", System.Data.SqlDbType.TinyInt)).Value = this.IdCorporativo;
                cmd.Parameters.Add(new SqlParameter("@Sucursal", System.Data.SqlDbType.TinyInt)).Value = this.IdSucursal;
                cmd.Parameters.Add(new SqlParameter("@Año", System.Data.SqlDbType.Int)).Value = this.Anio;
                cmd.Parameters.Add(new SqlParameter("@Folio", System.Data.SqlDbType.Int)).Value = this.Folio;
                cmd.Parameters.Add(new SqlParameter("@Secuencia", System.Data.SqlDbType.Int)).Value = this.Secuencia;
                cmd.Parameters.Add(new SqlParameter("@ClientePago", System.Data.SqlDbType.Int)).Value = this.ClientePago;

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resultado = int.Parse(reader["EXISTE"].ToString());
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
            return resultado;            
        }
    }
}
