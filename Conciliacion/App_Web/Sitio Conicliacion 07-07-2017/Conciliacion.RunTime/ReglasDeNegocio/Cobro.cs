using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.DatosSQL;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class Cobro : EmisorMensajes
    {
        short añocobro;
        int numcobro;
        string numerocheque;
        decimal total;
        decimal saldo;
        string numerocuenta;
        string numerocuentadestino;
        DateTime fcheque;
        int cliente;
        short banco;
        short bancoorigen;
        string observaciones;
        string status;
        short tipocobro;
        Boolean alta;
        string usuario;
        Boolean saldoafavor;
        //Se agregaron dos atributos
        int sucursalbancaria;
        string descripcion;
        int clientepago;
        decimal importecomision;
        decimal ivacomision;

        private List<ReferenciaConciliadaPedido> listapedidos = new List<ReferenciaConciliadaPedido>();
        

        #region Constructores

        public Cobro(MensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.añocobro = 0;
            this.numcobro = 0;
            this.numerocheque = "";
            this.total = 0;
            this.saldo = 0;
            this.numerocuenta = "";
            this.numerocuentadestino = "";
            this.cliente = 0;
            this.banco = 0;
            this.bancoorigen = 0;
            this.observaciones = "";
            this.status = "";
            this.tipocobro = 0;
            this.alta = true;
            this.usuario = "";
            this.saldoafavor = false;
            this.clientepago = 0;

            this.sucursalbancaria = 0;
            this.descripcion = "";

            this.ImporteComision = 0;
            this.IvaComision = 0;
        } 

        public Cobro(short añocobro, int numcobro, string numerocheque, decimal total, decimal saldo, string numerocuenta, string numerocuentadestino,
            DateTime fcheque, int cliente, short banco, short bancoorigen, string observaciones, string status, short tipocobro, Boolean alta,
            string usuario, Boolean saldoafavor, int sucursalbancaria, string descripcion, int clientepago, List<ReferenciaConciliadaPedido> listapedidos,
            decimal importecomision, decimal ivacomision, MensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.añocobro = añocobro;
            this.numcobro = numcobro;
            this.numerocheque = numerocheque;
            this.total = total;
            this.saldo = saldo;
            this.numerocuenta = numerocuenta;
            this.numerocuentadestino = numerocuentadestino;
            this.fcheque = fcheque;
            this.cliente = cliente;
            this.banco = banco;
            this.bancoorigen = bancoorigen;
            this.observaciones = observaciones;
            this.status = status;
            this.tipocobro = tipocobro;
            this.alta = alta;
            this.usuario = usuario;
            this.saldoafavor = saldoafavor;
            this.listapedidos = listapedidos;
            this.clientepago = clientepago;

            this.sucursalbancaria = sucursalbancaria;
            this.descripcion = descripcion;

            this.ImporteComision = importecomision;
            this.IvaComision = ivacomision;
        }



        #endregion

        #region Propiedades

        public List<ReferenciaConciliadaPedido> ListaPedidos
        {
            get { return listapedidos; }
            set { listapedidos = value; }
        }

        public short AñoCobro
        {
            get { return añocobro; }
            set { añocobro = value; }
        }

        public int NumCobro
        {
            get { return numcobro; }
            set { numcobro = value; }
        }

        public string NumeroCheque
        {
            get { return numerocheque; }
            set { numerocheque = value; }
        }

        public decimal Total
        {
            get { return total; }
            set { total = value; }
        }

        public decimal Saldo
        {
            get { return saldo; }
            set { saldo = value; }
        }

        public string NumeroCuenta
        {
            get { return numerocuenta; }
            set { numerocuenta = value; }
        }


        public string NumeroCuentaDestino
        {
            get { return numerocuentadestino; }
            set { numerocuentadestino = value; }
        }

        public DateTime FCheque
        {
            get { return fcheque; }
            set { fcheque = value; }
        }

        public int Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }

        public short Banco
        {
            get { return banco; }
            set { banco = value; }
        }

        public short BancoOrigen
        {
            get { return bancoorigen; }
            set { bancoorigen = value; }
        }

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public short TipoCobro
        {
            get { return tipocobro; }
            set { tipocobro = value; }
        }

        public Boolean Alta
        {
            get { return alta; }
            set { alta = value; }
        }

        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public Boolean SaldoAFavor
        {
            get { return saldoafavor; }
            set { saldoafavor = value; }
        }

        public int SucursalBancaria
        {
            get { return sucursalbancaria; }
            set { sucursalbancaria = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public int ClientePago
        {
            get { return clientepago; }
            set { clientepago = value; }
        }

        public decimal ImporteComision
        {
            get { return importecomision; }
            set { importecomision = value; }
        }

        public decimal IvaComision
        {
            get { return ivacomision; }
            set { ivacomision = value; }
        }
        #endregion

        public abstract bool ChequeTarjetaAltaModifica(Conexion _conexion);
        public abstract bool MovimientoCajaCobroAlta(short caja, DateTime foperacion, short consecutivo, int folio, Conexion _conexion);
        public abstract bool MovimientoCajaEntradaAlta(short caja, DateTime foperacion, short consecutivo, int folio, Conexion _conexion);

        public abstract bool ActualizaPagoReferenciado(short caja, DateTime foperacion, short consecutivo, int folio, Conexion _conexion);

        public abstract Cobro CrearObjeto();

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
