using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio.LectoresImplementaciones
{
    public  class LectorEstadoCuentaCSV:LectorEstadoCuenta
    {

        public override System.Data.DataColumn[] ObtenerColumnas(FuenteInformacion fuenteInformacion, string rutaArchivo)
        {
            try
            {
                using (StreamReader archivo = new StreamReader(rutaArchivo))
                {
                    string cabecera = archivo.ReadLine();
                    string[] strColumnas = cabecera.Split(',');
                    DataColumn[] columnas = new DataColumn[strColumnas.Length];
                    int index = 0;
                    foreach (string campo in strColumnas)
                    {
                        columnas[index] = new DataColumn(Regex.Replace(campo, @"[^a-zA-z0-9 ]+", "").Replace(' ', '_').Trim());
                        index++;
                    }
                    return columnas;
                }
            }
            catch (Exception ex)
            {
              
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }



            return null;
        }

        protected override System.Data.DataTable ObtenerContenido(System.Data.DataTable contenido, FuenteInformacion fuenteInformacion, string rutaArchivo)
        {
            string linea;
            int index = 0;
            try
            {
                using (StreamReader archivo = new StreamReader(rutaArchivo))
                {

                    while ((linea = archivo.ReadLine()) != null)
                    {
                        try
                        {
                            string[] fila = linea.Split(',');
                            if (index != 0)
                                contenido.Rows.Add(fila);
                            index++;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return contenido;
        }
    }
}
