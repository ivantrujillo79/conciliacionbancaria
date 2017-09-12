using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime;
using Conciliacion.RunTime.DatosSQL;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public  abstract  class TransferenciaBancariasDetalle:EmisorMensajes
    {
        private short corporativo;
        private int sucursal;
        private int año;
        private int folio;
        private int secuencia;
        private short corporativoDeatalle;
        private int sucursalDetalle;
        private string cuentaBanco;
        private short entrada;
        private decimal cargo;
        private decimal abono;


        public TransferenciaBancariasDetalle(IMensajesImplementacion implementadorMensajes)
        {
            this.corporativo = 0;
            this.sucursal = 0;
            this.año = 0;
            this.folio = 0;
            this.secuencia = 0;
            this.corporativoDeatalle = 0;
            this.sucursalDetalle = 0;
            this.cuentaBanco = "";
            this.entrada = 0;
            this.cargo = 0;
            this.abono = 0;
        }

        public TransferenciaBancariasDetalle(short corporativo, int sucursal, int año, int folio, int secuencia, short corporativoDeatalle, int sucursalDetalle,
                                             string cuentaBanco, short entrada, decimal cargo, decimal abono,
                                             IMensajesImplementacion implementadorMensajes)
        {
            this.corporativo = corporativo;
            this.sucursal = sucursal;
            this.año = año;
            this.folio = folio;
            this.secuencia = secuencia;
            this.corporativoDeatalle = corporativoDeatalle;
            this.sucursalDetalle = sucursalDetalle;
            this.cuentaBanco = cuentaBanco;
            this.entrada = entrada;
            this.cargo = cargo;
            this.abono = abono;
        }




        #region Propiedades
        public short Corporativo
        {
            get { return corporativo; }
            set { corporativo = value; }
        }

        public int Sucursal
        {
            get { return sucursal; }
            set { sucursal = value; }
        }

        public int Año
        {
            get { return año; }
            set { año = value; }
        }

        public int Folio
        {
            get { return folio; }
            set { folio = value; }
        }
        public int Secuencia
        {
            get { return secuencia; }
            set { secuencia = value; }
        }

        public short CorporativoDeatalle
        {
            get { return corporativoDeatalle; }
            set { corporativoDeatalle = value; }
        }

        public int SucursalDetalle
        {
            get { return sucursalDetalle; }
            set { sucursalDetalle = value; }
        }

        public string CuentaBanco
        {
            get { return cuentaBanco; }
            set { cuentaBanco = value; }
        }

        public short Entrada
        {
            get { return entrada; }
            set { entrada = value; }
        }

        public decimal Cargo
        {
            get { return cargo; }
            set { cargo = value; }
        }

        public decimal Abono
        {
            get { return abono; }
            set { abono = value; }
        }

        public virtual string CadenaConexion
        {
            get { return App.CadenaConexion; }
        }
        #endregion

        public abstract TransferenciaBancariasDetalle CrearObjeto();
        public abstract bool Registrar(Conexion _conexion);
    }
}
