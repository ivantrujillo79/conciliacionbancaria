using CatalogoConciliacion.ReglasNegocio;
using Conciliacion.Migracion.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatalogoConciliacion.Datos
{
  
    public class ColumnaDestinoDatos : ColumnaDestino
    
    {


        public ColumnaDestinoDatos(IMensajesImplementacion implementadorMensajes)
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
