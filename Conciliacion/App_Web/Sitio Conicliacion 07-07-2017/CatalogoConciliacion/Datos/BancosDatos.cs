using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CatalogoConciliacion.ReglasNegocio;
using Conciliacion.RunTime;

namespace CatalogoConciliacion.Datos
{

    public class BancosDatos : Bancos   {


        public BancosDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public BancosDatos(int banco,string descripcion)
          : base( banco,  descripcion)
        {

        }



        public override Bancos CrearObjeto()
        {
            throw new NotImplementedException();
        }
    }
}

