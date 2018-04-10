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
        private Microsoft.Office.Interop.Excel.Sheets xlHojas;
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

                if (xlRango != null)        Marshal.ReleaseComObject(xlRango);
                if (xlHoja != null)         Marshal.ReleaseComObject(xlHoja);
                if (xlHojas != null)        Marshal.ReleaseComObject(xlHojas);
                if (xlLibro != null)        Marshal.ReleaseComObject(xlLibro);
                if (xlLibros != null)       Marshal.ReleaseComObject(xlLibros);
                if (xlAplicacion != null)   Marshal.ReleaseComObject(xlAplicacion);
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

            xlHojas = xlLibro.Sheets;

            //xlHoja = (Excel.Worksheet)xlLibro.Sheets[1];

            xlHoja = xlHojas.Add();
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
            xlRango = xlHoja.Range["A1"];
            xlRango.Value2 = "Reporte\nPOSICION DIARIA DE BANCOS";
            xlRango = xlHoja.Range["A1:E2"];
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
            xlRango = xlHoja.Range["F1:G1"];
            xlRango.Merge();
            xlRango.Value2 = dia1;
            // Día 2
            xlRango = xlHoja.Range["H1:I1"];
            xlRango.Merge();
            xlRango.Value2 = dia2;
            // Kilos
            xlRango = xlHoja.Range["F2,H2"];
            xlRango.Value2 = "KILOS";
            //Fecha
            xlRango = xlHoja.Range["G2,I2"];
            xlRango[1].Value2 = fecha1.ToString("d-MMM-yyyy", cultureInfo);
            xlRango[1,3].Value2 = fecha2.ToString("d-MMM-yyyy", cultureInfo);
            // Formato
            xlRango = xlHoja.Range["F1:I2"];
            xlRango.Borders.LineStyle = Excel.XlLineStyle.xlDouble;
            xlRango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            xlRango.ColumnWidth = 15;
            
            xlRango = xlHoja.Range["A3:E200"];
            xlRango.Merge(true);
        }

        private void exportarPosicionDiariaBancos()
        {
            string celda;
            int contador = 4;

            foreach(DetallePosicionDiariaBancos item in _DetallePosicionDiariaBancos)
            {
                celda = "A" + contador;
                xlRango = xlHoja.Range[celda];
                xlRango.Value2 = item.ToString();
                ++contador;
                item.Año.ToString();
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
                mensajeExcepcion.Append("La lista del informe bancario está vacía. " + Environment.NewLine);
            }
            else if (_DetallePosicionDiariaBancos.Count == 0)
            {
                mensajeExcepcion.Append("La lista del informe bancario está vacía. " + Environment.NewLine);
            }

            if (string.IsNullOrEmpty(_NombreLibro))
            {
                mensajeExcepcion.Append("El nombre del libro es incorrecto. " + Environment.NewLine);
            }

            if (string.IsNullOrEmpty(_Ruta))
            {
                mensajeExcepcion.Append("La ruta para almacenar el archivo es incorrecta. " + Environment.NewLine);
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
