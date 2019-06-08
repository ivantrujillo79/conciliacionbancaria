using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime
{
    public class MensajeImplementacionConsola : MensajesImplementacion
    {
        private object contenedor;
        public void MostrarMensaje(string texto)
        {
            string cont = (string)contenedor;
            if (this.MensajesActivos)
                System.Console.Write(texto);
        }

        public void MostrarMensajeError(string Mensaje)
        {
            
        }

        public void MostrarMensajeExito(string Mensaje)
        {

        }

        public object ContenedorActual
        {
            get { return contenedor; }
            set { contenedor = value; }
        }
        private bool mensajesActivos = true;
        public bool MensajesActivos
        {
            get
            {
                return mensajesActivos;
            }
            set
            {
                mensajesActivos = value;
            }
        }



    }
}
