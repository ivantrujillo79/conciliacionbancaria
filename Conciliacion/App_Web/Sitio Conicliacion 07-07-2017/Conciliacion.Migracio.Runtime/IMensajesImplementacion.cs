using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Conciliacion.Migracion.Runtime
{
    public interface IMensajesImplementacion
    {
        object ContenedorActual {get;set;}
        void MostrarMensaje(string texto);
        bool MensajesActivos { get;set;}
    }

}
