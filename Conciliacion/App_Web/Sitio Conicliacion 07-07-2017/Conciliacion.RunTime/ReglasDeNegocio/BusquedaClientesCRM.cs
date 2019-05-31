using Conciliacion.RunTime.DatosSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    

    public class BusquedaClientesCRM
    {
        private class claseCliente
        {
            private int _cliente;
            private string _nombre;


            public int Cliente
            {
                get
                {
                    return _cliente;
                }

                set
                {
                    _cliente = value;
                }
            }

            public string Nombre
            {
                get
                {
                    return _nombre;
                }

                set
                {
                    _nombre = value;
                }
            }
        }

        private List<claseCliente> listaClientes;
        private string _urlGateway;
        IMensajesImplementacion _implementadorMensajes;

        public BusquedaClientesCRM(string _urlGateway, IMensajesImplementacion implementadorMensajes)
        {
            this._urlGateway = _urlGateway;
            this._implementadorMensajes = implementadorMensajes;
            listaClientes = new List<claseCliente>();
        }

        public void AgregaCliente(int numeroCliente)
        {
            if (!listaClientes.Exists(cliente => cliente.Cliente == numeroCliente))
            {
                claseCliente objCliente = new claseCliente();
                objCliente.Cliente = numeroCliente;
                listaClientes.Add(objCliente);
            }
        }

        public string ObtenNombre(int numeroCliente)
        {
           claseCliente objCliente = listaClientes.FirstOrDefault(cliente => cliente.Cliente == numeroCliente);

            if (objCliente != null)
            {
                return objCliente.Nombre;
            }
            else
            {
                return String.Empty;
            }
        }

        public void consultar()
        {
            Cliente clienteCRM = new ClienteDatos(_implementadorMensajes);
            foreach (claseCliente cliente in listaClientes)
            {
                try
                {
                    cliente.Nombre = clienteCRM.consultaClienteCRM(cliente.Cliente, _urlGateway);
                }
                catch(Exception e)
                {
                    cliente.Nombre = e.Message;
                }                
            }
        }




    }
}
