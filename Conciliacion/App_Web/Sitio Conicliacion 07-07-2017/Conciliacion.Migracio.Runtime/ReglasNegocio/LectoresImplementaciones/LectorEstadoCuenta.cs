using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio.LectoresImplementaciones
{
    public abstract class LectorEstadoCuenta : Conciliacion.Migracion.Runtime.EmisorMensajes, ILectorEstadoCuentaImplementacion
    {
        #region IExtractorImplementacion Members

       
        public System.Data.DataTable LeerArchivo(FuenteInformacion fuenteInformacion, string rutaArchivo)
        {
               DataTable tabla = new DataTable();
               try
               {

                   tabla.Columns.AddRange(ObtenerColumnas(fuenteInformacion, rutaArchivo));
                   tabla = ObtenerContenido(tabla, fuenteInformacion, rutaArchivo);
               }
               catch (Exception ex)
               {
                   stackTrace = new StackTrace();
                   this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                   stackTrace = null;
                   return null;

               }
            return tabla;
        }

        #endregion

        #region ILectorEstadoCuentaImplementacion Members



        protected abstract DataTable ObtenerContenido(DataTable contenido, FuenteInformacion fuenteInformacion, string rutaArchivo);
        

        public  abstract DataColumn[] ObtenerColumnas(FuenteInformacion fuenteInformacion, string rutaArchivo);
        

        #endregion

       
    }
}
