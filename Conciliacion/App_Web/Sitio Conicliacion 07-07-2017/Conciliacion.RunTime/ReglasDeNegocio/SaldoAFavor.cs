using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.DatosSQL;


namespace Conciliacion.RunTime.ReglasDeNegocio
{
   public  class SaldoAFavor 
    {
        public int FolioMovimiento { get; set; }
        public int AñoMovimiento { get; set; }
        public Int16 TipoMovimientoAConciliar { get; set; }
        public int EmpresaContable { get; set; }
        public byte Caja { get; set; }
        public DateTime FOperacion { get; set; }
        public int TipoFicha { get; set; }
        public int Consecutivo { get; set; }
        public byte TipoAplicacionIngreso { get; set; }
        public int ConsecutivoTipoAplicacion { get; set; }
        public int Factura { get; set; }
        public Int16 AñoCobro { get; set; }
        public int Cobro { get; set; }
        public int Monto { get; set; }
        public string StatusMovimiento { get; set; }
        public DateTime FMovimiento { get; set; }
        public string StatusConciliacion { get; set; }
        public DateTime FConciliacion { get; set; }
        public byte CorporativoConciliacion { get; set; }
        public byte SucursalConciliacion { get; set; }
        public int AñoConciliacion { get; set; }
        public Int16 MesConciliacion { get; set; }
        public int FolioConciliacion { get; set; }
        public byte CorporativoExterno { get; set; }
        public byte SucursalExterno { get; set; }
        public int AñoExterno { get; set; }
        public int FolioExterno { get; set; }
        public int SecuenciaExterno { get; set; }
	



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
