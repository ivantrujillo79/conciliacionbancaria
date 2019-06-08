using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CatalogoConciliacion.ReglasNegocio;
using Conciliacion.RunTime;

namespace CatalogoConciliacion.Datos
{

    public class CuentaContableBancoDatos : CuentaContableBanco
    {


        public CuentaContableBancoDatos(MensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public CuentaContableBancoDatos(int banco, string numeroCuenta)
          : base(banco, numeroCuenta)
        {

        }



        public override Bancos CrearObjeto()
        {
            throw new NotImplementedException();
        }
    }
}
