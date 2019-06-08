using System;
using System.Collections.Generic;
using Conciliacion.RunTime.DatosSQL;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract  class Cobranza:EmisorMensajes
    {

        private List<ReferenciaConciliadaPedido> listaReferenciaConciliadaPedido = new List<ReferenciaConciliadaPedido>();

        private int cobranza;
        private int tipoCobranza;
        private DateTime fCobranza;
        private string usuarioCaptura;
        private int empleado;
        private decimal total;
        private DateTime fAlta;
        private DateTime fActualizacion;
        private string status;
        private string observaciones;
        private int cobranzaOrigen;
        private string usuarioEntrega;
        private DateTime fEntrega;
        private string statusEntrega;


        public Cobranza(MensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.cobranza = 0;
            this.tipoCobranza = 0;
            this.fCobranza = DateTime.MinValue;
            this.usuarioCaptura = "";
            this.empleado = 0;
            this.total = 0;
            this.fAlta = DateTime.MinValue;
            this.fActualizacion = DateTime.MinValue;
            this.status = "";
            this.observaciones = "";
            this.cobranzaOrigen = 0;
            this.usuarioEntrega = "";
            this.fEntrega = DateTime.MinValue;
            this.statusEntrega = "";
        }

        public Cobranza(int cobranza,
        int tipoCobranza,
        DateTime fCobranza,
        string usuarioCaptura,
        int empleado,
        decimal total,
        DateTime fAlta,
        DateTime fActualizacion,
        string status,
        string observaciones,
        int cobranzaOrigen,
        string usuarioEntrega,
        DateTime fEntrega,
        string statusEntrega, MensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.cobranza = cobranza;
            this.tipoCobranza = tipoCobranza;
            this.fCobranza = fCobranza;
            this.usuarioCaptura = usuarioCaptura;
            this.empleado = empleado;
            this.total = total;
            this.fAlta = fAlta;
            this.fActualizacion = fActualizacion;
            this.status = status;
            this.observaciones = observaciones;
            this.cobranzaOrigen = cobranzaOrigen;
            this.usuarioEntrega = usuarioEntrega;
            this.fEntrega = fEntrega;
            this.statusEntrega = statusEntrega;
        }

        //protected Cobranza(MensajesImplementacion implementadorMensajes)
        //{
        //    this.implementadorMensajes = implementadorMensajes;
        //}

        public int Id
        {
            get { return cobranza; }
            set { cobranza = value; }
        }


        public int TipoCobranza
        {
            get { return tipoCobranza; }
            set { tipoCobranza = value; }
        }

        public DateTime FCobranza
        {
            get { return fCobranza; }
            set { fCobranza = value; }
        }

        public string UsuarioCaptura
        {
            get { return usuarioCaptura; }
            set { usuarioCaptura = value; }
        }

        public int Empleado
        {
            get { return empleado; }
            set { empleado = value; }
        }

        public decimal Total
        {
            get { return total; }
            set { total = value; }
        }

        public DateTime FAlta
        {
            get { return fAlta; }
            set { fAlta = value; }
        }

        public DateTime FActualizacion
        {
            get { return fActualizacion; }
            set { fActualizacion = value; }
        }

        public string Status
        {
            get { return Status; }
            set { Status = value; }
        }

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }

        public int CobranzaOrigen
        {
            get { return cobranzaOrigen; }
            set { cobranzaOrigen = value; }
        }

        public string UsuarioEntrega
        {
            get { return UsuarioEntrega; }
            set { UsuarioEntrega = value; }
        }

        public DateTime FEntrega
        {
            get { return fEntrega; }
            set { fEntrega = value; }
        }

        public string StatusEntrega
        {
            get { return statusEntrega; }
            set { statusEntrega = value; }
        }

        public List<ReferenciaConciliadaPedido> ListaReferenciaConciliadaPedido
        {
            get { return listaReferenciaConciliadaPedido; }
            set { listaReferenciaConciliadaPedido = value; }
        }

        public abstract bool Guardar(Conexion _conexion);

        public abstract int GuardarProcesoCobranza(Conexion _conexion);

        public abstract Cobranza CrearObjeto();

        public virtual string CadenaConexion
        {
            get
            {
                Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
                return objApp.CadenaConexion;
            }
        }
    }
}
