using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime;

namespace CatalogoConciliacion.ReglasNegocio
{
    public abstract class TipoMovimientoCuenta : EmisorMensajes
    {
        private int tipoMovimiento;
        private string cuenta;

        #region Constructores


        public TipoMovimientoCuenta(MensajesImplementacion implementadorMensajes)
        {       
            this.tipoMovimiento = 0;
            this.cuenta = string.Empty;
            this.implementadorMensajes = implementadorMensajes;
        }

        public TipoMovimientoCuenta(int tipoMovimiento, string cuenta, MensajesImplementacion implementadorMensajes)
        {
            this.tipoMovimiento = tipoMovimiento;
            this.cuenta = cuenta ;
            this.implementadorMensajes = implementadorMensajes;
        }

        #endregion 


        #region Propiedades 

        public int TipoMovimiento
        {
            get { return tipoMovimiento; }
            set { tipoMovimiento = value; }
        }

        public string Cuenta
        {
            get { return cuenta  ; }
            set { cuenta   = value; }
        }

        public virtual string CadenaConexion
        {
            get
            {
                Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
                return objApp.CadenaConexion;
            }
        }

        #endregion


        public abstract TipoMovimientoCuenta CrearObjeto();
        public abstract bool Guardar();
        public abstract bool Eliminar(int tipoMovimiento, string cuenta);

    }
}
