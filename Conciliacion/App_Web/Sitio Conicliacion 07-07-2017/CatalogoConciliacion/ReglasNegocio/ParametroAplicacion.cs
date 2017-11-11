using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime;

namespace CatalogoConciliacion.ReglasNegocio
{
    public abstract class ParametroAplicacion : EmisorMensajes
    {
        private string parametro;
        private string valor;
        private string observaciones;


        #region Constructores


        public ParametroAplicacion(IMensajesImplementacion implementadorMensajes)
        {       
            this.parametro = string.Empty;
            this.valor = string.Empty;
            this.observaciones = string.Empty;
            this.implementadorMensajes = implementadorMensajes;
        }

        public ParametroAplicacion(string parametro, string valor, string observaciones, IMensajesImplementacion implementadorMensajes)
        {
            this.parametro = parametro;
            this.valor = valor;
            this.observaciones = observaciones;
            this.implementadorMensajes = implementadorMensajes;
        }

        #endregion 


        #region Propiedades 

        public String Parametro
        {
            get { return parametro; }
            set { parametro = value; }
        }

        public string Valor
        {
            get { return valor  ; }
            set { valor = value; }
        }

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }

        public virtual string CadenaConexion
        {
            get
            {
                return App.CadenaConexion;
            }
        }

        #endregion


        public abstract ParametroAplicacion CrearObjeto();
        public abstract bool Consultar();
        public abstract bool Modificar();

    }
}
