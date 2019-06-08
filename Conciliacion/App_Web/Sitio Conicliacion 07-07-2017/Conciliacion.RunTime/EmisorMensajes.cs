using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Conciliacion.RunTime
{
    public class EmisorMensajes
    {
        protected StackTrace stackTrace;
        //protected MensajesImplementacion implementadorMensajes;
        //public MensajesImplementacion ImplementadorMensajes
        //{
        //    get { return implementadorMensajes; }
        //    set { implementadorMensajes = value; }
        //}
        protected MensajesImplementacion implementadorMensajes;
        public MensajesImplementacion ImplementadorMensajes
        {
            get { return implementadorMensajes; }
            set { implementadorMensajes = value; }
        }

    }
}
