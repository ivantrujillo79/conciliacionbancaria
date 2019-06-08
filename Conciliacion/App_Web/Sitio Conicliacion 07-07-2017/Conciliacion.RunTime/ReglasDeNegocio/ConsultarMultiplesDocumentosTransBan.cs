using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class ConsultarMultiplesDocumentosTransBan: EmisorMensajes
    {
        string clave;
        DateTime fMovimiento;
        string cajaDescripcion;
        int caja;
        DateTime fOperacion;
        string tipoMovimientoCajaDescripcion;
        decimal total;
        int consecutivo;
        int folio;

        public ConsultarMultiplesDocumentosTransBan(MensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.clave="";
            this.fMovimiento = DateTime.Now;
            this.cajaDescripcion="";
            this.caja=0;
            this.fOperacion=DateTime.Now;
            this.tipoMovimientoCajaDescripcion="";
            this.total=0;
            this.consecutivo = 0;
            this.folio = 0;
        }

        public ConsultarMultiplesDocumentosTransBan(string clave, DateTime fMovimiento, string cajaDescripcion, int caja, DateTime fOperacion, string tipoMovimientoCajaDescripcion, decimal total, int consecutivo, int folio)
        {
            Clave = clave;
            FMovimiento = fMovimiento;
            CajaDescripcion = cajaDescripcion;
            Caja = caja;
            FOperacion = fOperacion;
            TipoMovimientoCajaDescripcion = tipoMovimientoCajaDescripcion;
            Total = total;
            Consecutivo = consecutivo;
            Folio = folio;
        }

        public string Clave { get; set; }
        public DateTime FMovimiento { get; set; }
        public string CajaDescripcion { get; set ; }
        public int Caja { get; set; }
        public DateTime FOperacion { get; set; }
        public string TipoMovimientoCajaDescripcion { get; set; }
        public decimal Total { get; set; }
        public int Consecutivo { get; set; }
        public int Folio { get; set; }

        public abstract ConsultarMultiplesDocumentosTransBan CrearObjeto();

    }

    

}
