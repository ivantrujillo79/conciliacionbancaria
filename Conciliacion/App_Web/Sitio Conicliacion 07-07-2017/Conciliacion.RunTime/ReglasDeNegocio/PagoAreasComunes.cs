using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.DatosSQL;
using System.Data;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class PagoAreasComunes : EmisorMensajes
    {

        private int _clientePadre;
        private DateTime _fSuministroInicio;
        private DateTime _fSuministroFin;
        private string _pedidoReferencia;
        private string _nombreClientePadre;
        private Boolean _tienePagos;
        DataTable _pagos;
        private decimal _monto;

        #region Constructores

        public PagoAreasComunes(IMensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this._clientePadre= -1;
            this._fSuministroFin = DateTime.MinValue;
            this._fSuministroInicio = DateTime.MinValue;
            this._pedidoReferencia = "";
            this._nombreClientePadre = "";
            this._tienePagos = false;
            this._pagos = null;
            this._monto = 0;

        } 

        public PagoAreasComunes(int clientePadre, IMensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.ClientePadre = clientePadre;
            this._fSuministroFin = DateTime.MinValue;
            this._fSuministroInicio = DateTime.MinValue;
            this._pedidoReferencia = "";
            this._nombreClientePadre = "";
            this._tienePagos = false;
            this._pagos = null;
            this._monto = 0;
        }

        #endregion

        #region Propiedades
        public int ClientePadre
        {
            get
            {
                return _clientePadre;
            }

            set
            {
                _clientePadre = value;
            }
        }
        public DateTime FSuministroInicio
        {
            get
            {
                return _fSuministroInicio;
            }

            set
            {
                _fSuministroInicio = value;
            }
        }

        public DateTime FSuministroFin
        {
            get
            {
                return _fSuministroFin;
            }

            set
            {
                _fSuministroFin = value;
            }
        }

        public string PedidoReferencia
        {
            get
            {
                return _pedidoReferencia;
            }

            set
            {
                _pedidoReferencia = value;
            }
        }

        public virtual string CadenaConexion
        {
            get
            {
                return App.CadenaConexion;
            }
        }

        public string NombreClientePadre
        {
            get
            {
                return _nombreClientePadre;
            }

            set
            {
                _nombreClientePadre = value;
            }
        }

        public bool TienePagos
        {
            get
            {
                return _tienePagos;
            }

            set
            {
                _tienePagos = value;
            }
        }

        public DataTable Pagos
        {
            get
            {
                return _pagos;
            }

            set
            {
                _pagos = value;
            }
        }

        public decimal Monto
        {
            get
            {
                return _monto;
            }

            set
            {
                _monto = value;
            }
        }
        #endregion

        public abstract void consulta(Conexion _conexion);       

        public abstract PagoAreasComunes CrearObjeto();

        
    }
}
