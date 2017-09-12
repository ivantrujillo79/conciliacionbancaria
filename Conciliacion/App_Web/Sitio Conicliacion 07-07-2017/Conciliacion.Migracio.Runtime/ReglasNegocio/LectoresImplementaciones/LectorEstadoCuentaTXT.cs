using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio.LectoresImplementaciones
{
    public class LectorEstadoCuentaTXT : LectorEstadoCuenta
    {
        public bool esEspecial(string separador)
        {
            return separador[0] == Convert.ToChar(@"\");
        }

        public override System.Data.DataColumn[] ObtenerColumnas(FuenteInformacion fuenteInformacion, string rutaArchivo)
        {

            using (StreamReader archivo = new StreamReader(rutaArchivo))
            {
                string cabecera = archivo.ReadLine();
                //string[] strColumnas = cabecera.Split(Convert.ToChar(fuenteInformacion.TipoArchivo.Separador.Trim()));
                //string[] strColumnas = Regex.Split(cabecera, @fuenteInformacion.TipoArchivo.Separador.Trim());
                string[] strColumnas = esEspecial(fuenteInformacion.TipoArchivo.Separador.Trim()) ? Regex.Split(cabecera, @fuenteInformacion.TipoArchivo.Separador.Trim()) : cabecera.Split(Convert.ToChar(fuenteInformacion.TipoArchivo.Separador.Trim()));

                DataColumn[] columnas = new DataColumn[strColumnas.Length];
                int index = 0;
                foreach (string campo in strColumnas)
                {
                    columnas[index] = new DataColumn(Regex.Replace(campo, @"[^a-zA-z0-9 ]+", "").Replace(' ', '_').Trim());
                    index++;
                }
                return columnas;
            }
            return null;
        }


        protected override System.Data.DataTable ObtenerContenido(System.Data.DataTable contenido, FuenteInformacion fuenteInformacion, string rutaArchivo)
        {
            string linea;
            int index = 0;
            using (StreamReader archivo = new StreamReader(rutaArchivo))
            {
                while ((linea = archivo.ReadLine()) != null)
                {
                    //string[] fila = linea.Split(Convert.ToChar(fuenteInformacion.TipoArchivo.Separador.Trim()));
                    //string[] fila = Regex.Split(linea, @fuenteInformacion.TipoArchivo.Separador.Trim());
                    string[] fila = esEspecial(fuenteInformacion.TipoArchivo.Separador.Trim()) ? Regex.Split(linea, @fuenteInformacion.TipoArchivo.Separador.Trim()) : linea.Split(Convert.ToChar(fuenteInformacion.TipoArchivo.Separador.Trim()));
                    if (index != 0)
                    {
                        if (fila.Length == fuenteInformacion.NumColumnas)
                            contenido.Rows.Add(fila);
                    }
                    index++;
                }
            }
            return contenido;
        }
    }
}
