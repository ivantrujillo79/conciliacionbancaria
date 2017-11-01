using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Conciliacion.RunTime
{
    [Serializable]
    public class EmisorMensajes
    {
        protected StackTrace stackTrace;
        protected IMensajesImplementacion implementadorMensajes;
        public IMensajesImplementacion ImplementadorMensajes
        {
            get { return implementadorMensajes; }
            set { implementadorMensajes = value; }
        }
    }
}
