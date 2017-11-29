using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Conciliacion.RunTime
{
    public class MensajeImplementacionWeb : IMensajesImplementacion
    {
        private object contenedor;
        public void MostrarMensaje(string texto)
        {
            Page pagina = (Page)this.contenedor;
            if (mensajesActivos)
                ScriptManager.RegisterStartupScript(pagina, pagina.GetType(), Guid.NewGuid().ToString(), "alert('" + LimpiarTexto(texto) + "');", true);
        }

        public void MostrarMensajeError(string texto)
        {
            Page pagina = (Page)this.contenedor;
            if (mensajesActivos)
                ScriptManager.RegisterStartupScript(pagina, pagina.GetType(), Guid.NewGuid().ToString(), "alert('" + LimpiarTexto(texto) + "');", true);
        }

        public void MostrarMensajeExito(string texto)
        {
            Page pagina = (Page)this.contenedor;
            if (mensajesActivos)
                ScriptManager.RegisterStartupScript(pagina, pagina.GetType(), Guid.NewGuid().ToString(), "alert('" + LimpiarTexto(texto) + "');", true);
        }


        public object ContenedorActual
        {
            get { return contenedor; }
            set { contenedor = value; }
        }

        private string LimpiarTexto(string texto)
        {
            texto = texto.Replace("\b", "\\b");    //Retroceso [Backspace] 
            texto = texto.Replace("\f", "\\f");    //[Form feed] 
            texto = texto.Replace("\n", "\\n");    //Nueva línea 
            texto = texto.Replace("\r", "\\r");    //Retorno de carro [Carriage return] 
            texto = texto.Replace("\t", "\\t");    //Tabulador [Tab] 
            texto = texto.Replace("\v", "\\v");    //Tabulador vertical 
            texto = texto.Replace("\'", "\\'");    //Apóstrofe o comilla simple 
            //texto=texto.Replace("\"","\\\"");    //Doble comilla 
            //texto = texto.Replace("\\", "\\\\");    //Caracter Backslash (\). 
            //texto=texto.Replace(@"\XXX","\\XXX");  //El caracter de codificación Latin-1 especificado por tres dígitos octales XXX entre 0 y 377. Por ejemplo, \251 es una secuencia octal para el símbolo de derechos de copia [copyright] . 
            //texto=texto.Replace(@"\xXX",@"\\xXX");  //El caracter de codificación Latin-1 especificado por dos dígitos hexadecimales XX entre 00 y FF. Por ejemplo, \xA9 es una secuencia hexadecimal para el símbolo de copyright. 
            //texto=texto.Replace(@"\uXXXX",@"\\uXXXX"); //El caracter Unicode especificado por cuatro dígitos hexadecimales XXXX. por ejemplo, \u00A9 es una secuencia Unicode para el símbolo de copyright. Véase Secuencia de escape Unicode. 
            return texto;
        }



        #region IMensajesImplementacion Members

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

        #endregion
    }
}
