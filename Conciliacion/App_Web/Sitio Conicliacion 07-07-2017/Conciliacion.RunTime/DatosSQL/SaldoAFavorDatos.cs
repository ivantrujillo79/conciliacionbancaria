using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.ReglasDeNegocio;
using System.Data.SqlClient;
using System.Data;

namespace Conciliacion.RunTime.DatosSQL
{
    class SaldoAFavorDatos
    {
        public List<DetalleSaldoAFavor> ConsultaSaldoAFavor(string FInicial, string FFinal, string Cliente, decimal monto, Conexion conexion)
        {
            List<DetalleSaldoAFavor> ListaRetorno = new List<DetalleSaldoAFavor>();

            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBMovimientoAconciliar", cnn);
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        DetalleSaldoAFavor objDetalle = new DetalleSaldoAFavor
                        {
                            Folio = Convert.ToInt32(reader["Folio"]),
                            Cliente = reader["Cliente"].ToString(),
                            NombreCliente = reader["statusmovimiento"].ToString()//,
                            //CuentaBancaria = reader["statusmovimiento"]
        /*Banco 
        Sucursal 
        TipoCargo 
        Global 
        Fsaldo 
        Importe 
        Conciliada*/

                        };


                        ListaRetorno.Add(objDetalle);
                    }
                }
            }
            catch (Exception ex)
            {
                
            }


            return ListaRetorno;
        }
    }
}
