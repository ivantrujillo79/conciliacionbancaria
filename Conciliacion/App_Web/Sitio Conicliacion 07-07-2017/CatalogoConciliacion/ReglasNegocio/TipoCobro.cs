using Conciliacion.RunTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatalogoConciliacion.ReglasNegocio
{
 

    public abstract class TipoCobro : EmisorMensajes
    {
        int idTipoCobro;
        string descripcion;


        public TipoCobro(MensajesImplementacion implementadorMensajes)
        {
            this.implementadorMensajes = implementadorMensajes;
        }

        public TipoCobro(int idTipoCobro, string descripcion)
        {
            this.IdTipoCobro = idTipoCobro;
            this.descripcion = descripcion;
        }


        public int IdTipoCobro
        {
            get { return idTipoCobro; }
            set { idTipoCobro = value; }
        }



        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public abstract TipoCobro CrearObjeto();

    }
}
