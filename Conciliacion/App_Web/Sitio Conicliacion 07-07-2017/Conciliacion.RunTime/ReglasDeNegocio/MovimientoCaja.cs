using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.DatosSQL;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class MovimientoCaja : EmisorMensajes
    {
        short caja;
        DateTime foperacion;
        short consecutivo;
        int folio;
        DateTime fmovimiento;
        decimal total;
        string usuario;
        int empleado;
        string observaciones;
        decimal saldoafavor;
        private Int16 tipoMovimientoCaja;
        
        private List<Cobro> listacobros = new List<Cobro>();
        private List<ReferenciaConciliadaPedido> listapedidos = new List<ReferenciaConciliadaPedido>();

        #region Constructores

        public MovimientoCaja()
        {
            
        }

        public MovimientoCaja(IMensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.caja = 0;
            this.consecutivo = 0;
            this.folio = 0;
            this.total = 0;
            this.usuario = "";
            this.empleado = 0;
            this.observaciones = "";
            this.saldoafavor = 0;
        }

        public MovimientoCaja(short caja, DateTime foperacion, short consecutivo, int folio, DateTime fmovimiento, decimal total, string usuario, int empleado, string observaciones,decimal saldoafavor, List<Cobro> listacobros, IMensajesImplementacion implementadorMensajes)
        {
            this.listacobros = listacobros;
            this.ImplementadorMensajes = implementadorMensajes;
            this.caja = caja;
            this.foperacion = foperacion;
            this.consecutivo = consecutivo;
            this.folio = folio;
            this.fmovimiento = fmovimiento;
            this.total = total;
            this.usuario = usuario;
            this.empleado = empleado;
            this.observaciones = observaciones;
            this.saldoafavor = saldoafavor;
        }

        #endregion

        #region Propiedades

        public List<Cobro> ListaCobros
        {
                get { return listacobros; }
                set { listacobros = value; }
        }

        public List<ReferenciaConciliadaPedido> ListaPedidos
        {
            get { return listapedidos; }
            set { listapedidos = value; }
        }


        public short Caja
        {
            get { return caja; }
            set { caja = value; }
        }

        public short TipoMovimientoCaja
        {
            get { return tipoMovimientoCaja; }
            set { tipoMovimientoCaja = value; }
        }

        public DateTime FOperacion
        {
            get { return foperacion; }
            set { foperacion = value; }
        }

        public short Consecutivo
        {
            get { return consecutivo; }
            set { consecutivo = value; }
        }

        public int Folio
        {
            get { return folio; }
            set { folio = value; }
        }

        public DateTime FMovimiento
        {
            get { return fmovimiento; }
            set { fmovimiento = value; }
        }

        public decimal Total
        {
            get { return total; }
            set { total = value; }
        }
        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }
        public int Empleado
        {
            get { return empleado; }
            set { empleado = value; }
        }
        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }
        public decimal SaldoAFavor
        {
            get { return saldoafavor; }
            set { saldoafavor = value; }
        }

        #endregion

        public abstract bool MovimientoCajaAlta(Conexion _conexion);
        public abstract bool ValidaMovimientoCaja(Conexion _conexion);
        public abstract bool AplicarCobros(Conexion _conexion);
        public abstract bool AplicarCobrosCRM(Conexion _conexion, string URLGateway);

        public abstract bool Guardar(Conexion conexion);
        public abstract bool Guardar(Conexion conexion, string URLGateway);

        public abstract MovimientoCaja CrearObjeto();

        public virtual string CadenaConexion
        {
            get
            {
                return App.CadenaConexion;
            }
            set { App.CadenaConexion = value; }
        }

    }
}
