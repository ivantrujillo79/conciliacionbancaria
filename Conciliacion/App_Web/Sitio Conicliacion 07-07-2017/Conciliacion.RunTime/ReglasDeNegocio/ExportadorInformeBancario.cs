using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Globalization;
using DetallePosicionDiariaBancos = Conciliacion.RunTime.DatosSQL.InformeBancarioDatos.DetallePosicionDiariaBancos;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public class ExportadorInformeBancario
    {

        #region Miembros privados

        private Microsoft.Office.Interop.Excel.Application xlAplicacion;
        private Microsoft.Office.Interop.Excel.Workbooks xlLibros;
        private Microsoft.Office.Interop.Excel.Workbook xlLibro;
        private Microsoft.Office.Interop.Excel.Worksheet xlHoja;
        private Microsoft.Office.Interop.Excel.Range xlRango;

        private string _Nombre;
        private List<DetallePosicionDiariaBancos> _DetallePosicionDiariaBancos;
        private string _Ruta;
        private string _Archivo;
        private string _NombreLibro;

        #endregion

        #region Constructores

        public ExportadorInformeBancario(List<DetallePosicionDiariaBancos> DetallePosicionDiariaBancos, 
                                        string Ruta, string Archivo, string Nombre)
        {
            _Nombre = Nombre;
            _DetallePosicionDiariaBancos = DetallePosicionDiariaBancos;
            _Ruta = Ruta.Trim();
            _Archivo = Archivo.Trim();
            _NombreLibro = Nombre.Trim();

            //ValidarParametrosConstructor();
        }


        #endregion

        public void generarPosicionDiariaBancos()
        {
            try
            {
                inicializar();
                crearEncabezado();
                cerrar();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error en la creación del reporte: " + ex.Message);
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();

                if (xlRango != null)
                {
                    Marshal.ReleaseComObject(xlRango);
                }
                Marshal.ReleaseComObject(xlHoja);
                Marshal.ReleaseComObject(xlLibro);
                Marshal.ReleaseComObject(xlLibros);
                Marshal.ReleaseComObject(xlAplicacion);
                //xlAplicacion = null;
                //xlLibro = null;
                //xlHoja = null;
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

            xlLibros = xlAplicacion.Workbooks;

            xlLibro = xlLibros.Add(Excel.XlWBATemplate.xlWBATWorksheet);

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
            //Excel.Range xlRango;
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
            xlHoja.Cells[2, 6].Value2 = "KILOS";
            xlHoja.Cells[2, 8].Value2 = "KILOS";
            //Fecha
            xlHoja.Cells[2, 7].Value2 = fecha1.ToString("d-MMM-yyyy", cultureInfo);
            xlHoja.Cells[2, 9].Value2 = fecha2.ToString("d-MMM-yyyy", cultureInfo);
            // Formato
            xlRango = xlHoja.Cells.Range["F1:I2"];
            xlRango.Borders.LineStyle = Excel.XlLineStyle.xlDouble;
            xlRango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            xlRango.ColumnWidth = 15;

            xlRango = xlHoja.Cells.Range["A3:E200"];
            xlRango.Merge(true);
        }

        private void exportarPosicionDiariaBancos()
        {
            string celda;
            int contador = 4;

            foreach(DetallePosicionDiariaBancos item in _DetallePosicionDiariaBancos)
            {
                celda = "A" + contador;
                xlRango = xlHoja.Cells[celda, celda];
                xlRango.Value2 = item.ToString();
                ++contador;
                //item.
            }
        }

        private void cerrar()
        {
            string rutaCompleta = _Ruta + _Archivo;

            if (File.Exists(rutaCompleta)) File.Delete(rutaCompleta);

            xlLibro.SaveAs(rutaCompleta, Excel.XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing,
                            false, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            //xlLibro.Close(true, _Archivo, Type.Missing);
            xlLibro.Close(0);
            xlLibros.Close();
            xlAplicacion.Quit();
        }

        private bool ValidarParametrosConstructor()
        {
            StringBuilder mensajeExcepcion = new StringBuilder();
            bool valido = false;

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

            if (!string.IsNullOrEmpty(mensajeExcepcion.ToString()))
            {
                throw new Exception(mensajeExcepcion.ToString());
            }
            else
            {
                valido = true;
            }

            return valido;
        } // end ValidarParametrosConstructor()
    }
}
