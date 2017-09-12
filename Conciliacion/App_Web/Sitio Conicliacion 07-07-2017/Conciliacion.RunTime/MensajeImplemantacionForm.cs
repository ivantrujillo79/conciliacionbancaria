using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Conciliacion.RunTime
{
    public class MensajeImplemantacionForm : IMensajesImplementacion
    {
        private object contenedor;
        public MensajeImplemantacionForm() { }


        public void MostrarMensaje(string texto)
        {
            Form ventana = (Form)this.ContenedorActual;
            if (this.MensajesActivos)
                MessageBox.Show(ventana, texto, ventana.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
