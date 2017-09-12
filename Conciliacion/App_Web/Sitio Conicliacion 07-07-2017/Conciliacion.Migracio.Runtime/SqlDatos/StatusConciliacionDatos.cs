using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.Migracion.Runtime.ReglasNegocio;

namespace Conciliacion.Migracion.Runtime.SqlDatos
{
    class StatusConciliacionDatos:StatusConciliacion
    {
        public override bool Actualizar()
        {
            throw new NotImplementedException();
        }

        public override IObjetoBase CrearObjeto()
        {
            return new StatusConciliacionDatos();
        }

        public override bool Eliminar()
        {
            throw new NotImplementedException();
        }

        public override bool Guardar()
        {
            return true;
        }
    }
}
