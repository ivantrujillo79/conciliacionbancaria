using Conciliacion.RunTime.ReglasDeNegocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace Conciliacion.RunTime.DatosSQL
{
    class BusquedaClienteDatosBancariosDatos : BusquedaClienteDatosBancarios
    {
        public override List<int> ConsultarCliente(int BuscarPor, string Dato)
        {
            //(1, "Cuenta bancaria");
            //(2, "Clabe bancaria");
            //(3, "RFC");
            //(4, "Referencia de pago");

            List<int> Clientes = new List<int>();
            using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBConsultarCliente", cnn);
                    comando.Parameters.Add("@BuscarPor", System.Data.SqlDbType.SmallInt).Value = BuscarPor;
                    comando.Parameters.Add("@Dato", System.Data.SqlDbType.VarChar).Value = Dato;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        Clientes.Add(Convert.ToInt32(reader["Cliente"]));
                    }
                }
                catch (SqlException ex)
                {
                    stackTrace = new StackTrace();
                    this.ImplementadorMensajes.MostrarMensaje("Error al consultar la información.\n\rClase :" +
                                                              this.GetType().Name + "\n\r" + "Metodo :" +
                                                              //StackTrace.GetFrame(0).GetMethod().Name + "\n\r" +
                                                              "Error :" + ex.Message);
                    stackTrace = null;
                }
                return Clientes;
            }

        }

        public BusquedaClienteDatosBancariosDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }


        public BusquedaClienteDatosBancariosDatos(List<int> clientes, IMensajesImplementacion implementadorMensajes)
            : base(clientes, implementadorMensajes)
        {
        }

        public override BusquedaClienteDatosBancarios CrearObjeto()
        {
            return new BusquedaClienteDatosBancariosDatos(this.implementadorMensajes);
        }

    }
}
