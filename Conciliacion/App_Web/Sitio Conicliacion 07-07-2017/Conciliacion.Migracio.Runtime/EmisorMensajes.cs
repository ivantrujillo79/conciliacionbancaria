using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Conciliacion.Migracion.Runtime
{
    public  class EmisorMensajes
    {
        public EmisorMensajes()
        {
            this.ImplementadorMensajes = App.ImplementadorMensajes;
        }


        protected  StackTrace stackTrace;
        protected  IMensajesImplementacion implementadorMensajes;
        protected IMensajesImplementacion ImplementadorMensajes
        {
            get { return implementadorMensajes; }
            set { implementadorMensajes = value; }
        }


    
            
    }
}
