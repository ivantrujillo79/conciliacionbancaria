using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.Migracion.Runtime.ReglasNegocio;

namespace Conciliacion.Migracion.Runtime.SqlDatos
{
    public class ConceptoBancoDatos:ConceptoBanco
    {
        public override bool Actualizar()
        {
            throw new NotImplementedException();
        }

        public override IObjetoBase CrearObjeto()
        {
            return new ConceptoBancoDatos();
        }

        public override bool Eliminar()
        {
            throw new NotImplementedException();
        }

        public override bool Guardar()
        {
            throw new NotImplementedException();
        }
    }
}
