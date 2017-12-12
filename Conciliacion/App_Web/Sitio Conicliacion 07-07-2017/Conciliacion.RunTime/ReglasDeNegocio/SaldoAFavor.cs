using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.DatosSQL;


namespace Conciliacion.RunTime.ReglasDeNegocio
{
   public  class SaldoAFavor 
    {
    }

    public class OpcionSaldoAFavor
    {
        public byte IDOpcion { get; set; }
        public string OpcionConciliacion { get; set; }
    }

    public class DetalleSaldoAFavor
    {
        public bool Seleccionado { get; set; }
        public int Folio  { get; set; }
        public string Cliente   { get; set; }
        public string  NombreCliente { get; set; }
        public string CuentaBancaria { get; set; }
        public string Banco { get; set; }
        public string Sucursal { get; set; }
        public string TipoCargo { get; set; }
        public bool Global { get; set; }
        public DateTime Fsaldo { get; set; }
        public decimal Importe { get; set; }
        public string Conciliada { get; set; }

        public List<DetalleSaldoAFavor> ConsultaSaldoAFavor(string FInicial, string FFinal, string Cliente, decimal monto)
        {
            List<DetalleSaldoAFavor> Lista = new List<DetalleSaldoAFavor>();

            return Lista;
        }


    }
}
