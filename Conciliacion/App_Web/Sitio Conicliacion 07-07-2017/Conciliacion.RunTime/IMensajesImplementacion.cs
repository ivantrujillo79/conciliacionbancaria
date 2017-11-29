using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime
{
    public interface IMensajesImplementacion
    {
        object ContenedorActual { get; set; }
        void MostrarMensaje(string texto);
        void MostrarMensajeError(string Mensaje);
        void MostrarMensajeExito(string Mensaje);
        bool MensajesActivos { get; set; }
    }
}
