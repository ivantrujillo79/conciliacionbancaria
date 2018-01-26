using Conciliacion.RunTime.DatosSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class DetallePagare : EmisorMensajes
    {
        private Conexion conexion;
        private bool seleccionado;
        private int folio;
        private int año;
        private string cliente;
        private string nombrecliente;
        private string cuentabancaria;
        private string banco;
        private string sucursal;
        private string tipocargo;
        private bool global;
        private DateTime fsaldo;
        private decimal importe;
        private string conciliada;

        public DetallePagare(IMensajesImplementacion implementadorMensajes)
        {
            this.conexion = null;
            this.Seleccionado = false;
            this.Folio = 0;
            this.Cliente = "";
            this.NombreCliente = "";
            this.CuentaBancaria = "";
            this.Banco = "";
            this.Sucursal = "";
            this.TipoCargo = "";
            this.Global = false;
            this.Fsaldo = DateTime.MinValue;
            this.Importe = 0;
            this.Conciliada = "";
        }

        public DetallePagare(bool seleccionado, int folio, int año, string cliente, string nombrecliente, string cuentabancaria, string banco, string sucursal, string tipocargo, bool global, DateTime fsaldo, decimal importe, string conciliada, IMensajesImplementacion implementadorMensajes)
        {
            this.seleccionado = false;
            this.folio = 0;
            this.año = 0;
            this.cliente = "";
            this.nombrecliente = "";
            this.cuentabancaria = "";
            this.banco = "";
            this.sucursal = "";
            this.tipocargo = "";
            this.global = false;
            this.fsaldo = DateTime.MinValue;
            this.importe = 0;
            this.conciliada = "";
            this.implementadorMensajes = implementadorMensajes;
        }

        public abstract DetallePagare CrearObjeto();
        //public abstract List<DetallePagare> ConsultaSaldoAFavor(string FInicial, string FFinal, string Cliente, decimal monto, short TipoMovimientoAConciliar, Conexion conexion);
        public abstract List<DetallePagare> ConsultaSaldoAFavor(DateTime FInicial, DateTime FFinal, int Cliente, Decimal Monto, short TipoMovimientoAConciliar);

        public virtual string CadenaConexion
        {
            get { return App.CadenaConexion; }
        }
        public bool Seleccionado
        {
            get { return seleccionado; }
            set { seleccionado = value; }
        }
        public int Folio
        {
            get { return folio; }
            set { folio = value; }
        }
        public int Año
        {
            get { return año; }
            set { año = value; }
        }
        public string Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }
        public string NombreCliente
        {
            get { return nombrecliente; }
            set { nombrecliente = value; }
        }
        public string CuentaBancaria
        {
            get { return cuentabancaria; }
            set { cuentabancaria = value; }
        }
        public string Banco
        {
            get { return banco; }
            set { banco = value; }
        }
        public string Sucursal
        {
            get { return sucursal; }
            set { sucursal = value; }
        }
        public string TipoCargo
        {
            get { return tipocargo; }
            set { tipocargo = value; }
        }
        public bool Global
        {
            get { return global; }
            set { global = value; }
        }
        public DateTime Fsaldo
        {
            get { return fsaldo; }
            set { fsaldo = value; }
        }
        public decimal Importe
        {
            get { return importe; }
            set { importe = value; }
        }
        public string Conciliada
        {
            get { return conciliada; }
            set { conciliada = value; }
        }

    } //public abstract class DetallePagare
}
