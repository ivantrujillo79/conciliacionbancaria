using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public class ExportadorInformeBancario
    {

        #region Miembros privados

        private Microsoft.Office.Interop.Excel.Application xlAplicacion;
        private Microsoft.Office.Interop.Excel.Workbook xlLibro;
        private Microsoft.Office.Interop.Excel.Worksheet xlHoja;
        //private Microsoft.Office.Interop.Excel.Range xlRango;

        private List<string> _DetallePosicionDiariaBancos;      // Corregir tipo de dato
        private string _Ruta;
        private string _Archivo;
        private string _NombreLibro;

        #endregion

        #region Constructores
        
        public ExportadorInformeBancario(List<string> DetallePosicionDiariaBancos, 
                                        string Ruta, string Archivo, string Nombre)
        {
            _DetallePosicionDiariaBancos = DetallePosicionDiariaBancos;
            _Ruta = Ruta.Trim();
            _Archivo = Archivo.Trim();
            _NombreLibro = Nombre.Trim();

            //ValidarParametrosConstructor();
        }

        #endregion

        public void generar()
        {
            try
            {
                inicializar();
                crearEncabezado();
            }
            finally
            {
                Marshal.ReleaseComObject(xlAplicacion);
                Marshal.ReleaseComObject(xlLibro);
                Marshal.ReleaseComObject(xlHoja);
                //Marshal.ReleaseComObject(xlRango);
            }
        }

        private void inicializar()
        {
            xlAplicacion = new Microsoft.Office.Interop.Excel.Application();

            if (xlAplicacion == null)
            {
                throw new Exception("Microsoft Excel no está instalado correctamente en el equipo.");
            }

            xlAplicacion.Visible = false;

            xlLibro = xlAplicacion.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);

            //string workbookPath = Ruta + Archivo;

            //exLibro = exAplicacion.Workbooks.Open(_NombreLibro, Type.Missing, false, 5, Type.Missing,
            //                                        Type.Missing, false, Excel.XlPlatform.xlWindows, 
            //                                        Type.Missing, true, false, Type.Missing, true, 
            //                                        false, false);

            //exLibro = exAplicacion.Workbooks.Open(_NombreLibro, 0, false, 5, "", "", false, 
            //                                        Excel.XlPlatform.xlWindows, "", true, false, 
            //                                        0, true, false, false);

            xlHoja = (Excel.Worksheet)xlLibro.Sheets[1];
        }

        private void crearEncabezado()
        {
            Excel.Range xlRango;
            string rutaCompleta = _Ruta + _Archivo;

            xlHoja.Cells[1, 1] = "Reporte\n" + "POSICION DIARIA DE BANCOS";
            //xlHoja.Cells[2, 1] = "POSICION DIARIA DE BANCOS";
            xlHoja.Cells[3, 1] = "Tercera columna";

            xlRango = xlHoja.Cells[1, 1];
            xlRango = xlHoja.Cells.Range["A1:F1", "A2:F2"];
            xlRango.Merge();


            //xlRango = xlHoja.Range["A1", "A2"];
            xlRango.Font.Bold = true;

            xlRango = xlHoja.Cells[3, 1];
            xlRango.Borders.LineStyle = Excel.XlLineStyle.xlDouble;

            if (File.Exists(rutaCompleta)) File.Delete(rutaCompleta);

            xlLibro.SaveAs(rutaCompleta, Excel.XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing,
                            false, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            xlLibro.Close(true, Type.Missing, Type.Missing);
            xlAplicacion.Quit();

        }
        
        private bool ValidarParametrosConstructor()
        {
            StringBuilder mensajeExcepcion = new StringBuilder();
            bool valido = true;

            if (_DetallePosicionDiariaBancos is null)
            {
                mensajeExcepcion.Append("La lista del informe bancario está vacía." + Environment.NewLine);
            }
            else if (_DetallePosicionDiariaBancos.Count == 0)
            {
                mensajeExcepcion.Append("La lista del informe bancario está vacía." + Environment.NewLine);
            }

            if (string.IsNullOrEmpty(_NombreLibro))
            {
                mensajeExcepcion.Append("El nombre del libro es incorrecto." + Environment.NewLine);
            }

            if (string.IsNullOrEmpty(_Ruta))
            {
                mensajeExcepcion.Append("La ruta para almacenar el archivo es incorrecta." + Environment.NewLine);
            }

            if (string.IsNullOrEmpty(_Archivo))
            {
                mensajeExcepcion.Append("El nombre de archivo es incorrecto.");
            }

            if (! string.IsNullOrEmpty(mensajeExcepcion.ToString()))
            {
                valido = false;
                throw new Exception(mensajeExcepcion.ToString());
            }

            return valido;
        } // end ValidarParametrosConstructor()
    }
}
