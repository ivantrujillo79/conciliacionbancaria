using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class GrupoConciliacionDiasDiferencia : EmisorMensajes
    {
        short grupoconciliacion;
        short diferenciadiasminima;
        short diferenciadiasmaxima;
        short diferenciadiasdefault;
        decimal diferenciacentavosminima;
        decimal diferenciacentavosmaxima;
        decimal diferenciacentavosdefault;

        public GrupoConciliacionDiasDiferencia(short grupoconciliacion, IMensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.grupoconciliacion = grupoconciliacion;
            this.diferenciadiasminima = 0;
            this.diferenciadiasmaxima = 0;
            this.diferenciadiasdefault = 0;
            this.diferenciacentavosminima = this.diferenciacentavosmaxima = this.diferenciacentavosdefault = 0.0M;
        }

        public GrupoConciliacionDiasDiferencia(short grupoconciliacion, short diferenciadiasminima, short diferenciadiasmaxima, short diferenciadiasdefault, decimal diferenciacentavosminima, decimal diferenciacentavosmaxima, decimal diferenciacentavosdefault, IMensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.grupoconciliacion=grupoconciliacion;
            this.diferenciadiasminima=diferenciadiasminima;
            this.diferenciadiasmaxima=diferenciadiasmaxima;
            this.diferenciadiasdefault = diferenciadiasdefault;
            this.diferenciacentavosminima=diferenciacentavosminima;
            this.diferenciacentavosmaxima=diferenciacentavosmaxima;
            this.diferenciacentavosdefault = diferenciacentavosdefault;
        }

        public short GrupoConciliacion
        {
            get { return grupoconciliacion; }
            set { grupoconciliacion = value; }
        }

        public short DiferenciaDiasMinima
        {
            get { return diferenciadiasminima; }
            set { diferenciadiasminima = value; }
        }

        public short DiferenciaDiasMaxima
        {
            get { return diferenciadiasmaxima; }
            set { diferenciadiasmaxima = value; }
        }

        public short DiferenciaDiasDefault
        {
            get { return diferenciadiasdefault; }
            set { diferenciadiasdefault = value; }
        }

        public decimal DiferenciaCentavosMinima
        {
            get { return diferenciacentavosminima; }
            set { diferenciacentavosminima = value; }
        }

        public decimal DiferenciaCentavosMaxima
        {
            get { return diferenciacentavosmaxima; }
            set { diferenciacentavosmaxima = value; }
        }

        public decimal DiferenciaCentavosDefault
        {
            get { return diferenciacentavosdefault; }
            set { diferenciacentavosdefault = value; }
        }
        public abstract GrupoConciliacionDiasDiferencia CrearObjeto(short grupoconciliacion);

        public abstract bool CargarDatos();

        public virtual string CadenaConexion
        {
            get
            {
                return App.CadenaConexion;
            }
        }
        
    }
}
