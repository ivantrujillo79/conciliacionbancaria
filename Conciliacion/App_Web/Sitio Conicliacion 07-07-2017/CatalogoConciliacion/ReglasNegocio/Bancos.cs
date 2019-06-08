using Conciliacion.RunTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatalogoConciliacion.ReglasNegocio
{
    public abstract class Bancos : EmisorMensajes
    {
        int banco;
        string descripcion;


        public Bancos(MensajesImplementacion implementadorMensajes)
        {
            this.implementadorMensajes = implementadorMensajes;
        }

        public Bancos(int banco,string descripcion)
        {
            this.banco = banco;
            this.descripcion = descripcion;
        }


        public int Banco
        {
            get { return banco; }
            set { banco = value; }
        }

   

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public abstract Bancos CrearObjeto();

    }
}
