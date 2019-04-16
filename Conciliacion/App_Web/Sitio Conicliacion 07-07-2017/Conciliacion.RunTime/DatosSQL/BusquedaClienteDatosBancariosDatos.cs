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
        public override List<Cliente> ConsultarCliente(int BuscarPor, string Dato)
        {
            //(1, "Cuenta bancaria");
            //(2, "Clabe bancaria");
            //(3, "RFC");
            //(4, "Referencia de pago");

            List<Cliente> Clientes = new List<Cliente>();
            Cliente objCliente = Conciliacion.RunTime.App.Cliente.CrearObjeto();
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
                        objCliente = Conciliacion.RunTime.App.Cliente.CrearObjeto();
                        objCliente.NumCliente = Convert.ToInt32(reader["Cliente"]);
                        objCliente.Nombre = Convert.ToString(reader["Nombre"]);
                        objCliente.RazonSocial = Convert.ToString(reader["razonsocial"]);
                        Clientes.Add(objCliente);
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
