using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class BusquedaClienteDatosBancarios : EmisorMensajes
    {
        List<int> clientes;

        #region constructores
        public BusquedaClienteDatosBancarios(MensajesImplementacion implementadorMensajes)
        {
            this.implementadorMensajes = implementadorMensajes;
            this.clientes = new List<int>();
        }

        public BusquedaClienteDatosBancarios(List<int> Clientes, MensajesImplementacion implementadorMensajes)
        {
            this.clientes = Clientes;
            this.implementadorMensajes = implementadorMensajes;
        }

        //protected BusquedaClienteDatosBancarios(MensajesImplementacion implementadorMensajes)
        //{
        //    this.implementadorMensajes = implementadorMensajes;
        //}
        #endregion

        public List<int> Clientes
        {
            get { return clientes; }
            set { clientes = value; }
        }

        public abstract BusquedaClienteDatosBancarios CrearObjeto();
        public abstract List<Cliente> ConsultarCliente(int BuscarPor, string Dato);

    }
}
