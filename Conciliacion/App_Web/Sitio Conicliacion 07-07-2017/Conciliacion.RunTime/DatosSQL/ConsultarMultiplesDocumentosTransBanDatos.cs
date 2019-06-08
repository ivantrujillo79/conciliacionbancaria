using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.ReglasDeNegocio;

namespace Conciliacion.RunTime.DatosSQL
{
    public class ConsultarMultiplesDocumentosTransBanDatos : ConsultarMultiplesDocumentosTransBan
    {
        Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();

        public ConsultarMultiplesDocumentosTransBanDatos(MensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {

        }
        public ConsultarMultiplesDocumentosTransBanDatos(string clave, DateTime fMovimiento, string cajaDescripcion, int caja, DateTime fOperacion, string tipoMovimientoCajaDescripcion, decimal total, int consecutivo, int folio)
            :base(clave,fMovimiento,cajaDescripcion,caja,fOperacion,tipoMovimientoCajaDescripcion,total,consecutivo,folio)
        {

        }
        public override ConsultarMultiplesDocumentosTransBan CrearObjeto()
        {
            return new ConsultarMultiplesDocumentosTransBanDatos(objApp.ImplementadorMensajes);
        }
    }
}
