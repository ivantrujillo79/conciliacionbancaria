using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CatalogoConciliacion.ReglasNegocio;
using Conciliacion.Migracion.Runtime;

namespace CatalogoConciliacion.Datos
{

    public class CuentaContableBancoDatos : CuentaContableBanco
    {


        public CuentaContableBancoDatos(IMensajesImplementacion implementadorMensajes)
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
