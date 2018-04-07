using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Globalization;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    class ExportadorInformeBancario
    {

        #region Miembros privados

        private Microsoft.Office.Interop.Excel.Application exAplicacion;
        private Microsoft.Office.Interop.Excel.Workbook exLibro;
        private Microsoft.Office.Interop.Excel.Worksheet exHoja;
        private Microsoft.Office.Interop.Excel.Range exRango;

        private string _Nombre;

        #endregion

        #region Constructores
        
        public ExportadorInformeBancario(string Nombre)
        {
            _Nombre = Nombre;
        }

        #endregion

        public void generarPosicionDiariaBancos()
        {
            try
            {
                inicializar();
            }
            catch(Exception ex)
            {
                throw new Exception("Ocurrió un error en la creación del reporte: " + ex.Message);
            }
            finally
            {
                Marshal.ReleaseComObject(exAplicacion);
                Marshal.ReleaseComObject(exLibro);
                Marshal.ReleaseComObject(exHoja);
                Marshal.ReleaseComObject(exRango);
            }
        }

        private void inicializar()
        {
            exAplicacion = new Microsoft.Office.Interop.Excel.Application();

            if (exAplicacion == null)
            {
                throw new Exception("Microsoft Excel no está instalado correctamente en el equipo.");
            }

            exAplicacion.Visible = false;

            exLibro = exAplicacion.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);

            //string workbookPath = Ruta + Archivo;

            exLibro = exAplicacion.Workbooks.Open(_Nombre, Type.Missing, false, 5, Type.Missing,
                                                Type.Missing, false, Excel.XlPlatform.xlWindows, 
                                                Type.Missing, true, false, Type.Missing, true, 
                                                false, false);

            exHoja = (Excel.Worksheet)exLibro.Sheets[1];
        }

        private void crearEncabezado()
        {
<<<<<<< HEAD
=======
            Excel.Range xlRango;
            string rutaCompleta = _Ruta + _Archivo;
            DateTime fecha1;
            DateTime fecha2;
            string dia1;
            string dia2;
            CultureInfo cultureInfo = CultureInfo.GetCultureInfo("es-MX");

            // Nombre del reporte
            xlHoja.Cells[1, 1] = "Reporte\nPOSICION DIARIA DE BANCOS";
            xlRango = xlHoja.Cells.Range["A1:E2"];
            xlRango.Merge();
            xlRango.Font.Bold = true;
            xlRango.RowHeight = 15;
            xlRango.Borders.LineStyle = Excel.XlLineStyle.xlDouble;
            xlRango.Interior.Color = Excel.XlRgbColor.rgbSkyBlue;

            // Día de la semana
            fecha1 = DateTime.Now;
            fecha2 = DateTime.Now.AddDays(1);
            dia1 = fecha1.ToString("dddd", cultureInfo).ToUpper();
            dia2 = fecha2.AddDays(1).ToString("dddd", cultureInfo).ToUpper();
            xlRango = xlHoja.Cells.Range["F1:G1"];
            xlRango.Merge();
            xlRango.Value2 = dia1;
            // Día 2
            xlRango = xlHoja.Cells.Range["H1:I1"];
            xlRango.Merge();
            xlRango.Value2 = dia2;

            // Kilos
            //xlHoja.Cells[2, 6].Value2 = "KILOS";
            xlHoja.Cells["F5"].Value2 = "KILOS";
            xlHoja.Cells[2, 8].Value2 = "KILOS";

            //Fecha
            xlHoja.Cells[2, 7].Value2 = fecha1.ToString("d-MMM-yyyy", cultureInfo);
            xlHoja.Cells[2, 9].Value2 = fecha2.ToString("d-MMM-yyyy", cultureInfo);

            // Formato
            xlRango = xlHoja.Cells.Range["F1:I2"];
            xlRango.Borders.LineStyle = Excel.XlLineStyle.xlDouble;
            xlRango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            xlRango.ColumnWidth = 15;
            
            if (File.Exists(rutaCompleta)) File.Delete(rutaCompleta);

            xlLibro.SaveAs(rutaCompleta, Excel.XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing,
                            false, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            xlLibro.Close(true, Type.Missing, Type.Missing);
            xlAplicacion.Quit();
>>>>>>> 3d1e9d2... Lista la funcionalidad del método crearEncabezado()

        }

    }
}
