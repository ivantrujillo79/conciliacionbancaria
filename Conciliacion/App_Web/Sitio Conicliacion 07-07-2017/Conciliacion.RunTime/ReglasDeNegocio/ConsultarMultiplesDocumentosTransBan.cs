using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class ConsultarMultiplesDocumentosTransBan: EmisorMensajes
    {
        string campo1;
        int campo2;

        public ConsultarMultiplesDocumentosTransBan(IMensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.campo1 = "";
            this.campo2 = 0;
        }

        public ConsultarMultiplesDocumentosTransBan(string campo1, int campo2)
        {
            this.campo1 = campo1;
            this.campo2 = campo2;
        }

        public string Campo1
        {
            get { return campo1; }
            set { campo1 = value; }
        }

        public int Campo2
        {
            get { return Campo2; }
            set { campo2 = value; }
        }

        public abstract ConsultarMultiplesDocumentosTransBan CrearObjeto();
    }

    

}
