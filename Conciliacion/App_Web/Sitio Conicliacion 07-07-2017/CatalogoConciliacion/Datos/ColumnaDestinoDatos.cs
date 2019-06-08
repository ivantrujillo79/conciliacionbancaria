using CatalogoConciliacion.ReglasNegocio;
using Conciliacion.RunTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatalogoConciliacion.Datos
{
  
    public class ColumnaDestinoDatos : ColumnaDestino
    
    {


        public ColumnaDestinoDatos(MensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public ColumnaDestinoDatos( string dscColumnaDestino)
          : base(dscColumnaDestino)
        {

        }



        public override ColumnaDestino CrearObjeto()
        {
            throw new NotImplementedException();
        }
    }
}
