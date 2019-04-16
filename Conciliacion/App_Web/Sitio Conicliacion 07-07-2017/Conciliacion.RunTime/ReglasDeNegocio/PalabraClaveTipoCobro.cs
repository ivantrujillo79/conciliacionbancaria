using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.ReglasDeNegocio
{

    public class PalabraClaveTipoCobro : EmisorMensajes
    {
        int banco;
        string cuentabanco;
        int tipocobro;
        string palabraclave;
        string columnadestino;

        public int Banco
        {
            set { banco = value; }
            get { return banco; }
        }

        public int TipoCobro
        {
            set { tipocobro = value; }
            get { return tipocobro; }
        }

        public string CuentaBanco
        {
            set { cuentabanco = value; }
            get { return cuentabanco; }
        }

        public string PalabraClave
        {
            set { palabraclave = value; }
            get { return palabraclave; }
        }

        public string ColumnaDestino
        {
            set { columnadestino = value; }
            get { return columnadestino; }
        }

    }
}
