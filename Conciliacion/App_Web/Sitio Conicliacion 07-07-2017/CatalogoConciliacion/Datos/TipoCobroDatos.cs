using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CatalogoConciliacion.ReglasNegocio;
using Conciliacion.Migracion.Runtime;
using Conciliacion.RunTime;

namespace CatalogoConciliacion.Datos
{
   
    public class TipoCobroDatos : TipoCobro
    {


        public TipoCobroDatos(MensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public TipoCobroDatos(int idtipocobro, string descripcion)
          : base(idtipocobro, descripcion)
        {

        }



        public override TipoCobro CrearObjeto()
        {
            throw new NotImplementedException();
        }
    }
}
