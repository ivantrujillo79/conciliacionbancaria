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

        public void ActualizarClientePago(Conexion _conexion)
        {
            //bool resultado = false;

            if (this.IdCorporativo <= 0 | this.IdSucursal <= 0 | this.Anio <= 0 | this.Folio <= 0 | this.Secuencia <= 0)
                throw new Exception("No se encontró un pago para actualizar por el cliente " + this.ClientePago.ToString() );

            if (this.ClientePago <= 0)
                throw new Exception("No se especificó un cliente para actualizar la partida de pago.");

            try
            {
                _conexion.Comando.CommandType = System.Data.CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBActualizarClientePago";
                _conexion.Comando.Parameters.Clear();

                _conexion.Comando.Parameters.Add(new SqlParameter("@Corporativo", System.Data.SqlDbType.TinyInt)).Value = this.IdCorporativo;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Sucursal", System.Data.SqlDbType.TinyInt)).Value = this.IdSucursal;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Año", System.Data.SqlDbType.Int)).Value = this.Anio;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Folio", System.Data.SqlDbType.Int)).Value = this.Folio;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Secuencia", System.Data.SqlDbType.Int)).Value = this.Secuencia;
                _conexion.Comando.Parameters.Add(new SqlParameter("@ClientePago", System.Data.SqlDbType.Int)).Value = this.ClientePago;

                _conexion.Comando.ExecuteNonQuery();

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
    }
}
