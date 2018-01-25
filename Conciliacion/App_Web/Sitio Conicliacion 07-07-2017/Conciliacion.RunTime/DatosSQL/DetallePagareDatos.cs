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

        public override List<DetallePagare> ConsultaSaldoAFavor(int TipoMovimientoAConciliar, string FInicial, string FFinal, string Cliente, decimal monto, Conexion conexion)
        {
            List<DetallePagare> ListaRetorno = new List<DetallePagare>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBConsultaSaldosAFavor", cnn);
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        DetallePagare objDetalle = Conciliacion.RunTime.App.DetallePagare.CrearObjeto();
                        {
                            Folio = Convert.ToInt32(reader["Folio"]);
                            Cliente = reader["Cliente"].ToString();
                            NombreCliente = reader["NombreCliente"].ToString();
                            CuentaBancaria = reader["CuentaBancaria"].ToString();
                            Banco = reader["Banco"].ToString();
                            Sucursal = reader["Sucursal"].ToString();
                            TipoCargo = reader["TipoCargo"].ToString();
                            Global = bool.Parse(reader["Global"].ToString());
                            Fsaldo = DateTime.Parse(reader["Fsaldo"].ToString());
                            Importe = decimal.Parse(reader["Importe"].ToString());
                            Conciliada = reader["Conciliada"].ToString();
                        };
                        ListaRetorno.Add(objDetalle);
                    }
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
