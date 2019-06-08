using System;
using Conciliacion.RunTime.DatosSQL;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class PedidoCobranza : EmisorMensajes
    {

        private int anioPed;
        private int celula;
        private int pedido;
        private int cobranza;
        private decimal saldo;
        private Int16 gestionInicial;


          public PedidoCobranza(MensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.anioPed = 0;
            this.celula = 0;
            this.pedido = 0;
            this.cobranza = 0;
            this.saldo = 0;
            this.gestionInicial = 0;
        }

        public PedidoCobranza(Int16 anioPed,
            Int16 celula,
            int pedido,
            int cobranza,
            decimal saldo,
            Int16 gestionInicial, MensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.anioPed = anioPed;
            this.celula = celula;
            this.pedido = pedido;
            this.cobranza = cobranza;
            this.saldo = saldo;
            this.gestionInicial = gestionInicial;
        }

        //protected PedidoCobranza(MensajesImplementacion implementadorMensajes)
        //{
        //    this.implementadorMensajes = implementadorMensajes;
        //}

        public int AnioPed
        {
            get { return anioPed; }
            set { anioPed = value; }
        }


        public int Celula
        {
            get { return celula; }
            set { celula = value; }
        }

        public int Pedido
        {
            get { return pedido; }
            set { pedido = value; }
        }

        public int Cobranza
        {
            get { return cobranza; }
            set { cobranza = value; }
        }

        public decimal Saldo
        {
            get { return saldo; }
            set { saldo = value; }
        }

        public Int16 GestionInicial
        {
            get { return gestionInicial; }
            set { gestionInicial = value; }
        }

        public abstract bool Guardar(Conexion _conexion);

        public abstract PedidoCobranza CrearObjeto();

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
