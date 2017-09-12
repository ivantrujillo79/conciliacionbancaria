using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio
{
    public interface ILectorEstadoCuentaImplementacion
    {
        DataTable LeerArchivo  (FuenteInformacion fuenteInformacion, string rutaArchivo);
        DataColumn [] ObtenerColumnas(FuenteInformacion fuenteInformacion, string rutaArchivo);
    }
}
