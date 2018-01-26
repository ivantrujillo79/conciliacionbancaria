using Conciliacion.RunTime.ReglasDeNegocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.DatosSQL
{
    public class DetallePagareDatos : DetallePagare
    {
        public DetallePagareDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public DetallePagareDatos(
            bool seleccionado, 
            int folio, 
            int año,
            string cliente, 
            string nombrecliente, 
            string cuentabancaria, 
            string banco, 
            string sucursal, 
            string tipocargo, 
            bool global, 
            DateTime fsaldo, 
            decimal importe, 
            string conciliada, 
            IMensajesImplementacion implementadorMensajes)
            : base(seleccionado,
                   folio,
                   año,
                   cliente,
                   nombrecliente,
                   cuentabancaria,
                   banco,
                   sucursal,
                   tipocargo,
                   global,
                   fsaldo,
                   importe,
                   conciliada,
                   implementadorMensajes)
        {
        }

        public override DetallePagare CrearObjeto()
        {
            return new DetallePagareDatos(this.ImplementadorMensajes);
        }

        public override List<DetallePagare> ConsultaSaldoAFavor(DateTime FInicial, DateTime FFinal, int Cliente, Decimal Monto, short TipoMovimientoAConciliar)
        {
            List<DetallePagare> ListaRetorno = new List<DetallePagare>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    if (TipoMovimientoAConciliar <= 0)
                    {
                        throw new Exception("El parámetro TipoMovimiento no es válido.");
                    }
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBConsultaDetalleSaldoAFavor", cnn);
                    comando.Parameters.Add("@FInicio", System.Data.SqlDbType.DateTime).Value    = (FInicial == DateTime.MinValue ? (object)System.DBNull.Value : FInicial);
                    comando.Parameters.Add("@FFin", System.Data.SqlDbType.DateTime).Value       = (FFinal == DateTime.MinValue ? (object)System.DBNull.Value : FFinal);
                    comando.Parameters.Add("@Cliente", System.Data.SqlDbType.Int).Value         = (Cliente == -1 ? (object)System.DBNull.Value : Cliente);
                    comando.Parameters.Add("@Monto", System.Data.SqlDbType.Money).Value         = (Monto == -1 ? (object)System.DBNull.Value : Monto);
                    comando.Parameters.Add("@TipoMovimientoAConciliar", System.Data.SqlDbType.SmallInt).Value = TipoMovimientoAConciliar;

                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        DetallePagare detalle = new DetallePagareDatos(this.ImplementadorMensajes);
                        detalle.Seleccionado        = false;
                        detalle.Folio               = Convert.ToInt32(reader["Folio"]);
                        detalle.Año                 = Convert.ToInt32(reader["Año"]);
                        detalle.Cliente             = Convert.ToString(reader["Cliente"]);
                        detalle.NombreCliente       = Convert.ToString(reader["NombreCliente"]);
                        detalle.CuentaBancaria      = Convert.ToString(reader["CuentaBancaria"]);
                        detalle.Banco               = "";
                        detalle.Sucursal            = Convert.ToString(reader["Sucursal"]);
                        detalle.TipoCargo           = "";
                        detalle.Global              = false;
                        detalle.Fsaldo              = Convert.ToDateTime(reader["FSaldo"]);
                        detalle.Importe             = Convert.ToDecimal(reader["Importe"]);
                        detalle.Conciliada          = Convert.ToString(reader["Conciliada"]);
                        ListaRetorno.Add(detalle);
                    }
                    reader.Close();
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
            return ListaRetorno;
        }

    }

}
