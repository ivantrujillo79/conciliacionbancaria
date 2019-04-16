using Conciliacion.Migracion.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatalogoConciliacion.ReglasNegocio
{
 

    public abstract class CuentaContableBanco : EmisorMensajes
    {
        int banco;
        string numeroCuenta;


        public CuentaContableBanco(IMensajesImplementacion implementadorMensajes)
        {
            this.implementadorMensajes = implementadorMensajes;
        }

        public CuentaContableBanco(int banco, string numeroCuenta)
        {
            this.banco = banco;
            this.numeroCuenta = numeroCuenta;
        }


        public int Banco
        {
            get { return banco; }
            set { banco = value; }
        }

                    

        public string NumeroCuenta
        {
            get { return numeroCuenta; }
            set { numeroCuenta = value; }
        }


        public abstract Bancos CrearObjeto();

    }
}
