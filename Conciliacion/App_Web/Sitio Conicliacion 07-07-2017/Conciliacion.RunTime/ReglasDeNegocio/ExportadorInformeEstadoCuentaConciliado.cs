using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using DetalleReporteEstadoCuentaConciliado = Conciliacion.RunTime.DatosSQL.InformeBancarioDatos.DetalleReporteEstadoCuentaConciliado;
using System.IO;
using System.Globalization;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using System.Diagnostics;
using System.Drawing;
using OfficeOpenXml.Style;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public class ExportadorInformeEstadoCuentaConciliado
    {

        #region Miembros privados

        private Microsoft.Office.Interop.Excel.Application xlAplicacion;
        private Microsoft.Office.Interop.Excel.Workbooks xlLibros;
        private Microsoft.Office.Interop.Excel.Workbook xlLibro;
        private Microsoft.Office.Interop.Excel.Sheets xlHojas;
        private Microsoft.Office.Interop.Excel.Worksheet xlHoja;
        private Microsoft.Office.Interop.Excel.Range xlRango;

        private List<DetalleReporteEstadoCuentaConciliado> _ReporteEstadoCuentaConciliado;
        private string _Ruta;
        private string _Archivo;
        private string _NombreHoja;

        #endregion

        #region Constructores

        public ExportadorInformeEstadoCuentaConciliado(List<DetalleReporteEstadoCuentaConciliado> ReporteEstadoCuentaConciliado,
                                        string Ruta, string Archivo, string Nombre)
        {
            try
            {
                _ReporteEstadoCuentaConciliado = ReporteEstadoCuentaConciliado;
                _Ruta = Ruta.Trim();
                _Archivo = Archivo.Trim();
                _NombreHoja = Nombre.Trim();

                //ValidarMiembros();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error en la creación del reporte: <br/>" + ex.Message);
            }
        }

        #endregion

        public void generarInforme()
        {
            try
            {
                ExcelPackage excelPackage = inicializar();
                crearEncabezado(excelPackage);
                /*exportarDatos();
                cerrar();*/
                excelPackage.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error en la creación del reporte: <br/>" + ex.Message);
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();

                if (xlRango != null) Marshal.ReleaseComObject(xlRango);
                if (xlHoja != null) Marshal.ReleaseComObject(xlHoja);
                if (xlHojas != null) Marshal.ReleaseComObject(xlHojas);
                if (xlLibro != null) Marshal.ReleaseComObject(xlLibro);
                if (xlLibros != null) Marshal.ReleaseComObject(xlLibros);
                if (xlAplicacion != null) Marshal.ReleaseComObject(xlAplicacion);
            }
        }

        private ExcelPackage inicializar()
        {
            string rutaCompleta = _Ruta + _Archivo;
            ExcelPackage excel = new ExcelPackage(new FileInfo(@rutaCompleta));
            
            return excel;

            /*
            xlAplicacion = new Microsoft.Office.Interop.Excel.Application();

            if (xlAplicacion == null)
            {
                throw new Exception("Microsoft Excel no está instalado correctamente en el equipo.");
            }

            xlAplicacion.Visible = false;

            xlAplicacion.DisplayAlerts = false;

            xlLibros = xlAplicacion.Workbooks;

            if (File.Exists(rutaCompleta))
            {
                xlLibro = xlAplicacion.Workbooks.Open(rutaCompleta,
                    0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "",
                    true, false, 0, true, false, false);

                //xlLibro = xlAplicacion.Workbooks.Open(rutaCompleta, Excel.XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing,
                //            false, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing,
                //            Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                xlHojas = xlLibro.Sheets;

                xlHoja = xlHojas.Add();

                xlHoja.Name = _NombreHoja;
            }
            else
            {
                xlLibro = xlLibros.Add(Excel.XlWBATemplate.xlWBATWorksheet);

                //xlHojas = xlLibro.Sheets;

                //xlHoja = xlHojas.Add();

                xlHoja = (Excel.Worksheet)xlLibro.Sheets[1];

                xlHoja.Name = _NombreHoja;
            }*/
        }

        private ExcelPackage crearEncabezado(ExcelPackage excelPackage)
        {
            string banco, cuenta, empresa;
            DateTime fecha;
            CultureInfo cultureInfo = CultureInfo.GetCultureInfo("es-MX");

            ExcelWorksheet wsSheet1 = excelPackage.Workbook.Worksheets.Add("NombreHoja");

            banco = "BANAMEX ";
            cuenta = "CTA ";// + _ReporteEstadoCuentaConciliado[0].CuentaBancoFinanciero + " ";
            empresa = "Corporativo";//_ReporteEstadoCuentaConciliado[0].Corporativo;
            fecha = DateTime.Now;//_ReporteEstadoCuentaConciliado[0].Fecha;

            


            /*wsSheet1.Cells["A2"].Value = empresa;
            wsSheet1.Cells["C3"].Value = fecha.ToString("MMMM", cultureInfo).ToUpper();

            Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#0000FF");
            wsSheet1.Cells["B1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            wsSheet1.Cells["B1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
            wsSheet1.Cells["B1"].Style.Font.Color.SetColor(Color.White);
            wsSheet1.Cells["B1"].Value = fecha.ToString("MMMM DE yyyy").ToUpper();

            */

            

            

            /*
            //banco = _DetalleReporteEstadoCuenta[0].Banco;
            banco = "BANAMEX ";
            cuenta = "CTA " + _ReporteEstadoCuentaConciliado[0].CuentaBancoFinanciero + " ";
            empresa = _ReporteEstadoCuentaConciliado[0].Corporativo;
            fecha = _ReporteEstadoCuentaConciliado[0].Fecha;*/

            // Cuenta
            using (ExcelRange Rng = wsSheet1.Cells["B1:H1"])
            {
                Rng.Value = banco + cuenta + "MOVIMIENTOS DEL MES DE:";
                Rng.Merge = true;
            }

            // Empresa
            using (ExcelRange Rng = wsSheet1.Cells["B2:H2"])
            {
                Rng.Merge= true;
                Rng.Value = empresa;
            }

            // Mes
            using (ExcelRange Rng = wsSheet1.Cells["D3:H3"])
            {
                Rng.Merge = true;
                Rng.Value = fecha.ToString("MMMM", cultureInfo).ToUpper();
                Rng.Style.Font.Bold = true;
                Rng.Style.Font.Size = 13;
                Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                Rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                Rng.Style.Fill.BackgroundColor.SetColor(Color.Blue);
                Rng.Style.Font.Color.SetColor(Color.White);
            }



            // Fecha

            using (ExcelRange Rng = wsSheet1.Cells["I1:K3"])
            {
                Rng.Merge = true;
                Rng.Style.Font.Bold = true;
                Rng.Value = fecha.ToString("MMMM DE yyyy").ToUpper();
                Rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                Rng.Style.Fill.BackgroundColor.SetColor(Color.Blue);
                Rng.Style.Font.Color.SetColor(Color.White);
            }
            
            // Clabe interbancaria
            wsSheet1.Cells["B2"].Value = "CLABE INTERBANCARIA";


            using (ExcelRange Rng = wsSheet1.Cells["B7:P7"])
            {
                Rng[7, 1].Value = "Fecha";
                Rng[7, 2].Value = "Referencia";
                Rng[7, 3].Value = "Concepto";
                Rng[7, 8].Value = "Retiros";
                Rng[7, 9].Value = "Depósitos";
                Rng[7, 10].Value = "Saldo";
                Rng[7, 11].Value = "Concepto conciliado";
                Rng[7, 15].Value = "Documento";
                Rng.Style.Font.Size = 10;
                Rng.Style.Font.Color.SetColor(Color.Blue);
                Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            }

            /*            
                                    // Columnas
                                    xlRango = xlHoja.Range["B7:P7"];
                                    xlRango[1, 1].Value2 = "Fecha";
                                    xlRango[1, 2].Value2 = "Referencia";
                                    xlRango[1, 3].Value2 = "Concepto";
                                    xlRango[1, 8].Value2 = "Retiros";
                                    xlRango[1, 9].Value2 = "Depósitos";
                                    xlRango[1, 10].Value2 = "Saldo";
                                    xlRango[1, 11].Value2 = "Concepto conciliado";
                                    xlRango[1, 15].Value2 = "Documento";
                                    xlRango.Font.ColorIndex = 23;
                                    xlRango.Font.Size = 10;
                                    xlRango.Interior.Color = Excel.XlRgbColor.rgbWhite;
                                    xlRango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                    xlRango.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                    xlRango.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium);

                                    xlRango = xlHoja.Columns["A"];
                                    xlRango.ColumnWidth = 4;
                                    xlRango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                    xlRango.Font.Bold = true;
                                    xlRango = xlHoja.Columns["P"];
                                    xlRango.ColumnWidth = 13;
                                    xlRango = xlHoja.Range["D7:H7"];
                                    xlRango.Merge();
                                    xlRango = xlHoja.Range["I7:K7"];
                                    xlRango.Font.Bold = true;
                                    xlRango.ColumnWidth = 13;
                                    xlRango = xlHoja.Range["L7:O7"];
                                    xlRango.Merge();
                                    xlRango = xlHoja.Range["L7:P7"];
                                    xlRango.Font.Bold = true;
                                    xlRango = xlHoja.Range["I:K"];
                                    xlRango.ColumnWidth = 13;
                                    xlRango = xlHoja.Range["B:H"];
                                    xlRango.ColumnWidth = 10;
                                    */

            using (ExcelRange Rng = wsSheet1.Cells["L1"])
            {
                Rng.Value = "Depósito";
                Rng.Style.Font.Size = 11;
                Rng.Style.Font.Bold = true;
                Rng.Style.Font.Italic = true;
            }
            using (ExcelRange Rng = wsSheet1.Cells["L2"])
            {
                Rng.Value = "Retiro";
                Rng.Style.Font.Size = 11;
                Rng.Style.Font.Bold = true;
                Rng.Style.Font.Italic = true;
            }
            using (ExcelRange Rng = wsSheet1.Cells["L3"])
            {
                Rng.Value = "Saldo final calculado";
                Rng.Style.Font.Size = 11;
                Rng.Style.Font.Bold = true;
                Rng.Style.Font.Italic = true;
            }
            using (ExcelRange Rng = wsSheet1.Cells["L4"])
            {
                Rng.Value = "Saldo final bancario";
                Rng.Style.Font.Size = 11;
                Rng.Style.Font.Bold = true;
                Rng.Style.Font.Italic = true;
            }
            using (ExcelRange Rng = wsSheet1.Cells["L5"])
            {
                Rng.Value = "Depósito conciliado";
                Rng.Style.Font.Size = 11;
                Rng.Style.Font.Bold = true;
                Rng.Style.Font.Italic = true;
            }
            using (ExcelRange Rng = wsSheet1.Cells["L6"])
            {
                Rng.Value = "Retiro conciliado";
                Rng.Style.Font.Size = 11;
                Rng.Style.Font.Bold = true;
                Rng.Style.Font.Italic = true;
            }
            wsSheet1.Protection.IsProtected = false;
            wsSheet1.Protection.AllowSelectLockedCells = false;
            return excelPackage;
        }

        private void exportarDatos()
        {
            int i = 8;
            Excel.Range celdaInicio;
            Excel.Range celdaFin;

            foreach (DetalleReporteEstadoCuentaConciliado detalle in _ReporteEstadoCuentaConciliado)
            {
                celdaInicio = xlHoja.Cells[i, 1];
                celdaFin = xlHoja.Cells[i, 16];

                xlRango = xlHoja.Range[celdaInicio, celdaFin];

                xlRango[1, 1].Value2 = detalle.ConsecutivoFlujo.ToString();
                xlRango[1, 2].Value2 = detalle.Fecha.ToString("dd/MM/yyyy");
                xlRango[1, 3].Value2 = detalle.Referencia;
                xlRango[1, 4].Value2 = detalle.Concepto;
                xlRango[1, 9].Value2 = detalle.Retiros.ToString("C");
                xlRango[1, 10].Value2 = detalle.Depositos.ToString("C");
                xlRango[1, 11].Value2 = detalle.SaldoFinal.ToString("C");
                xlRango[1, 12].Value2 = detalle.ConceptoConciliado;
                xlRango[1, 16].Value2 = detalle.DocumentoConciliado;

                i++;
            }
            celdaInicio = xlHoja.Cells[8, 1];
            celdaFin = xlHoja.Cells[i, 8];
            xlRango = xlHoja.Range[celdaInicio, celdaFin];
            xlRango.Font.Size = 10;

            celdaInicio = xlHoja.Cells[8, 4];   // D8
            celdaFin = xlHoja.Cells[i, 8];  // H#
            xlRango = xlHoja.Range[celdaInicio, celdaFin];
            xlRango.Merge(true);

            celdaInicio = xlHoja.Cells[8, 12];   // L8
            celdaFin = xlHoja.Cells[i, 15];  // O#
            xlRango = xlHoja.Range[celdaInicio, celdaFin];
            xlRango.Merge(true);
        }

        private void cerrar()
        {
            string rutaCompleta = _Ruta + _Archivo;

            //if (File.Exists(rutaCompleta)) File.Delete(rutaCompleta);

            xlLibro.SaveAs(rutaCompleta, Excel.XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing,
                            false, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            xlLibro.Close(0);
            xlLibros.Close();
            xlAplicacion.Quit();
        }

        private bool ValidarMiembros()
        {
            StringBuilder mensajeExcepcion = new StringBuilder();
            bool valido = false;

            if (_ReporteEstadoCuentaConciliado == null)
            {
                mensajeExcepcion.Append("No se encontraron datos con los parámetros seleccionados. <br/>");
            }
            else if (_ReporteEstadoCuentaConciliado.Count == 0)
            {
                mensajeExcepcion.Append("No se encontraron datos con los parámetros seleccionados. <br/>");
            }

            if (string.IsNullOrEmpty(_NombreHoja))
            {
                mensajeExcepcion.Append("El parámetro de configuración Nombre no puede estar vacío. <br/>");
            }

            if (string.IsNullOrEmpty(_Ruta))
            {
                mensajeExcepcion.Append("El parámetro de configuración Ruta no puede estar vacío. <br/>");
            }

            if (string.IsNullOrEmpty(_Archivo))
            {
                mensajeExcepcion.Append("El parámetro de configuración Archivo no puede estar vacío.");
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
        } // end ValidarMiembros()
    }
}
