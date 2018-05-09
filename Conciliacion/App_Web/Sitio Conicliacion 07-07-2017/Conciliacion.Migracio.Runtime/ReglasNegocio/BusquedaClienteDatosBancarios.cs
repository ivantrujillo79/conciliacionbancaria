using Conciliacion.Migracion.Runtime.SqlDatos;
using Conciliacion.RunTime.DatosSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio
{
    public abstract class BusquedaClienteDatosBancarios : EmisorMensajes
    {
        List<int> clientes;

        #region constructores
        public BusquedaClienteDatosBancarios(IMensajesImplementacion implementadorMensajes)
        {
            this.implementadorMensajes = implementadorMensajes;
            this.clientes = new List<int>();
        }

        public BusquedaClienteDatosBancarios(List<int> Clientes, IMensajesImplementacion implementadorMensajes)
        {
            this.clientes = Clientes;
            this.implementadorMensajes = implementadorMensajes;
        }
        #endregion

        public List<int> Clientes
        {
            get { return clientes; }
            set { clientes = value; }
        }

        public abstract BusquedaClienteDatosBancarios CrearObjeto();
        public abstract List<int> ConsultarCliente(Int16 BuscarPor, string Dato);

    }
}