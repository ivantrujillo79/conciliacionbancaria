using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.ReglasDeNegocio;

namespace Conciliacion.RunTime.DatosSQL
{
    public class FuenteInformacionDatos : cFuenteInformacion
    {
        public FuenteInformacionDatos(MensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public FuenteInformacionDatos(int fuenteinformacion, string rutaarchivo, string cuentabancofinanciero, short tipofuenteinformacion, 
            string tipofuenteinformaciondes,short tipofuente, string tipofuentedes, short tipoarchivo, string tipoarchivodes, MensajesImplementacion implementadorMensajes): base (fuenteinformacion,rutaarchivo,cuentabancofinanciero,tipofuenteinformacion, tipofuenteinformaciondes,tipofuente,tipofuentedes,tipoarchivo,tipoarchivodes,implementadorMensajes)
        {
        }

        public override cFuenteInformacion CrearObjeto()
        {
            return new FuenteInformacionDatos(this.ImplementadorMensajes);
        }
    }
}
