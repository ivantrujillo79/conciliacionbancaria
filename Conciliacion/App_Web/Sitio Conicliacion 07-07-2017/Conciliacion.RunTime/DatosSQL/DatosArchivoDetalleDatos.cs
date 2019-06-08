using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Conciliacion.RunTime.ReglasDeNegocio;

namespace Conciliacion.RunTime.DatosSQL
{
    public class DatosArchivoDetalleDatos: DatosArchivoDetalle
    {
        public DatosArchivoDetalleDatos(MensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public DatosArchivoDetalleDatos(int corporativo, int sucursal, int año, int folio, int secuencia, DateTime foperacion, DateTime fmovimiento, string referencia, string descripcion, decimal deposito, decimal retiro, string concepto, MensajesImplementacion implementadorMensajes)
            : base(corporativo, sucursal, año, folio, secuencia, foperacion, fmovimiento, referencia, descripcion, deposito, retiro, concepto, implementadorMensajes)
        {
        }

        public override DatosArchivoDetalle CrearObjeto()
        {
            return new DatosArchivoDetalleDatos(this.implementadorMensajes);
        }
    }
}
