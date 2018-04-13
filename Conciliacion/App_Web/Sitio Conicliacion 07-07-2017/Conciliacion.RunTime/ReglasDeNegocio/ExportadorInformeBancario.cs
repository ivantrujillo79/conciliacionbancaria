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

        private List<DetallePosicionDiariaBancos> _DetallePosicionDiariaBancos;
        private string _Ruta;
        private string _Archivo;
        private string _NombreLibro;

        private const string CONCEPTO1 = "PORTATIL";
        private const string CONCEPTO2 = "ESTACIONARIO";
        private const string CONCEPTO3 = "EDIFICIOS";
        private const string CONCEPTO4 = "SERVICIOS TECNICOS";
        private const string CONCEPTO5 = "CREDITO PORTATIL";
        private const string CONCEPTO6 = "CREDITO ESTACIONARIO";
        private const string CONCEPTO7 = "CREDITO EDIFICIOS";
        private const string CONCEPTO8 = "CREDITO SERVICIOS TECNICOS";
        private const string CONCEPTO9 = "COBRANZA";
        private const string CONCEPTO10 = "COBRANZA FILIAL";
        private const string CONCEPTO11 = "OTROS INGRESOS";

        #endregion

        #region Constructores

        public ExportadorInformeBancario(List<DetallePosicionDiariaBancos> DetallePosicionDiariaBancos, 
                                        string Ruta, string Archivo, string Nombre)
        {
            try
            {
                _DetallePosicionDiariaBancos = DetallePosicionDiariaBancos;
                _Ruta = Ruta.Trim();
                _Archivo = Archivo.Trim();
                _NombreLibro = Nombre.Trim();

                ValidarMiembros();
            }
            catch(Exception ex)
            {
                throw new Exception("Ocurrió un error en la creación del reporte: <br/>" + ex.Message);
            }
        }
        
        #endregion

        public void generarPosicionDiariaBancos()
        {
            try
            {
                inicializar();
                crearEncabezado();
                exportarPosicionDiariaBancos();
                calcularTotalizadores();
                cerrar();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error en la creación del reporte: <br/>" + ex.Message);
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
            CultureInfo cultureInfo = CultureInfo.GetCultureInfo("es-MX");
            DateTime fecha = _DetallePosicionDiariaBancos
                                                    .First(x => !x.Concepto.ToUpper().Contains("TOTAL"))
                                                    .Fecha;
            string dia = fecha.ToString("dddd", cultureInfo).ToUpper();
            string caja = Convert.ToString(_DetallePosicionDiariaBancos
                                                    .First(x => !x.Concepto.ToUpper().Contains("TOTAL"))
                                                    .Caja);

            // Nombre del reporte
            xlRango = xlHoja.Range["A1"];
            xlRango.Value2 = "Reporte\nPOSICION DIARIA DE BANCOS";
            xlRango = xlHoja.Range["A1:E2"];
            xlRango.Merge();
            xlRango.Font.Bold = true;
            xlRango.RowHeight = 15;
            xlRango.Borders.LineStyle = Excel.XlLineStyle.xlDouble;
            xlRango.Interior.Color = Excel.XlRgbColor.rgbSkyBlue;
            // Día
            xlRango = xlHoja.Range["F1:G1"];
            xlRango.Merge();
            xlRango.Value2 = dia;
            // Kilos
            xlRango = xlHoja.Range["F2,G2"];
            xlRango[1, 1].Value2 = "KILOS";
            // Fecha
            xlRango[1, 2].Value2 = fecha.ToString("d-MMM-yyyy", cultureInfo);
            // Formato
            xlRango = xlHoja.Range["F1:G2"];
            xlRango.Borders.LineStyle = Excel.XlLineStyle.xlDouble;
            xlRango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            xlRango.ColumnWidth = 16;
            // Caja
            xlRango = xlHoja.Range["A3:E3"];
            xlRango.Merge();
            xlRango.Value2 = "CAJA MATRIZ " + caja;
            xlRango.Borders.LineStyle = Excel.XlLineStyle.xlDouble;
            xlRango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
        }

        private void exportarPosicionDiariaBancos()
        {
            string concepto;

            xlRango = xlHoja.Range["A4:E19"];
            xlRango.Merge(true);
            xlRango[1, 1].Value2 = "Portátil";
            xlRango[2, 1].Value2 = "Estacionario";
            xlRango[3, 1].Value2 = "Edificios";
            xlRango[4, 1].Value2 = "Servicios técnicos";
            xlRango[5, 1].Value2 = "Crédito portátil";
            xlRango[6, 1].Value2 = "Crédito estacionario";
            xlRango[7, 1].Value2 = "Crédito edificios";
            xlRango[8, 1].Value2 = "Crédito servicios técnicos";
            xlRango[9, 1].Value2 = "Cobranza";
            xlRango[10, 1].Value2 = "Cobranza filial";
            xlRango[11, 1].Value2 = "Otros ingresos";

            xlRango[13, 1].Value2 = "Total ingresos del día";
            xlRango[14, 1].Value2 = "Total crédito";
            xlRango[16, 1].Value2 = "Total ingresos del día neto";
            xlRango = xlHoja.Range["A16:A19"];
            xlRango.Font.Bold = true;

            // Seleccionar cuadro de celdas donde se imprimirán los datos
            xlRango = xlHoja.Range["F4:G14"];
            foreach (DetallePosicionDiariaBancos item in _DetallePosicionDiariaBancos)
            {
                concepto = RemoverAcentos(item.Concepto.ToUpper().Trim());

                switch (concepto)
                {
                    case CONCEPTO1:
                        xlRango[1, 1].Value2 = item.Kilos.ToString("0,0.##");
                        xlRango[1, 2].Value2 = item.Importe.ToString("C");
                        break;
                    case CONCEPTO2:
                        xlRango[2, 1].Value2 = item.Kilos.ToString("0,0.##");
                        xlRango[2, 2].Value2 = item.Importe.ToString("C");
                        break;
                    case CONCEPTO3:
                        xlRango[3, 1].Value2 = item.Kilos.ToString("0,0.##");
                        xlRango[3, 2].Value2 = item.Importe.ToString("C");
                        break;
                    case CONCEPTO4:
                        xlRango[4, 1].Value2 = item.Kilos.ToString("0,0.##");
                        xlRango[4, 2].Value2 = item.Importe.ToString("C");
                        break;
                    case CONCEPTO5:
                        xlRango[5, 1].Value2 = item.Kilos.ToString("0,0.##");
                        xlRango[5, 2].Value2 = item.Importe.ToString("C");
                        break;
                    case CONCEPTO6:
                        xlRango[6, 1].Value2 = item.Kilos.ToString("0,0.##");
                        xlRango[6, 2].Value2 = item.Importe.ToString("C");
                        break;
                    case CONCEPTO7:
                        xlRango[7, 1].Value2 = item.Kilos.ToString("0,0.##");
                        xlRango[7, 2].Value2 = item.Importe.ToString("C");
                        break;
                    case CONCEPTO8:
                        xlRango[8, 1].Value2 = item.Kilos.ToString("0,0.##");
                        xlRango[8, 2].Value2 = item.Importe.ToString("C");
                        break;
                    case CONCEPTO9:
                        xlRango[9, 1].Value2 = item.Kilos.ToString("0,0.##");
                        xlRango[9, 2].Value2 = item.Importe.ToString("C");
                        break;
                    case CONCEPTO10:
                        xlRango[10, 1].Value2 = item.Kilos.ToString("0,0.##");
                        xlRango[10, 2].Value2 = item.Importe.ToString("C");
                        break;
                    case CONCEPTO11:
                        xlRango[11, 1].Value2 = item.Kilos.ToString("0,0.##");
                        xlRango[11, 2].Value2 = item.Importe.ToString("C");
                        break;
                    default:
                        break;
                }
            }
        }

        private void calcularTotalizadores()
        {
            string concepto;
            decimal totalIngresoDia = 0m;
            decimal totalCredito = 0m;
            decimal totalNeto = 0m;
            decimal totalIngresoDiaKilos = 0m;
            decimal totalCreditoKilos = 0m;
            decimal totalNetoKilos = 0m;

            foreach (DetallePosicionDiariaBancos item in _DetallePosicionDiariaBancos)
            {
                concepto = RemoverAcentos(item.Concepto.Substring(0, 5).ToUpper().Trim());

                if ( (!concepto.Equals("CREDI")) && (!concepto.Equals("TOTAL")) )
                {
                    totalIngresoDia += item.Importe;
                    totalIngresoDiaKilos += item.Kilos;
                }
                else if (concepto.Equals("CREDI"))
                {
                    totalCredito += item.Importe;
                    totalCreditoKilos += item.Kilos;
                }
            }

            totalNeto = totalIngresoDia + totalCredito;
            totalNetoKilos = totalIngresoDiaKilos + totalCreditoKilos;

            xlRango = xlHoja.Range["F16:G19"];
            // Total kilos
            xlRango[1, 1].Value2 = totalIngresoDiaKilos.ToString("0,0.##");
            xlRango[2, 1].Value2 = totalCreditoKilos.ToString("0,0.##");
            xlRango[3, 1].Value2 = "-   ";
            xlRango[3, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            xlRango[4, 1].Value2 = totalNetoKilos.ToString("0,0.##");
            // Total importe
            xlRango[1, 2].Value2 = totalIngresoDia.ToString("C");
            xlRango[2, 2].Value2 = totalCredito.ToString("C");
            xlRango[3, 2].Value2 = "-   ";
            xlRango[3, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            xlRango[4, 2].Value2 = totalNeto.ToString("C");

            // Formato
            xlRango = xlHoja.Range["A16:G19"];
            xlRango.Interior.Color = Excel.XlRgbColor.rgbSkyBlue;
            xlRango = xlHoja.Range["A1:G19"];
            xlRango.BorderAround2(Excel.XlLineStyle.xlDouble, Excel.XlBorderWeight.xlThin, 
                Excel.XlColorIndex.xlColorIndexAutomatic);
        }

        private void cerrar()
        {
            string rutaCompleta = _Ruta + _Archivo;

            if (File.Exists(rutaCompleta)) File.Delete(rutaCompleta);

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

            if (_DetallePosicionDiariaBancos is null)
            {
                mensajeExcepcion.Append("La lista del informe bancario está vacía. <br/>");
            }
            else if (_DetallePosicionDiariaBancos.Count == 0)
            {
                mensajeExcepcion.Append("La lista del informe bancario está vacía. <br/>");
            }

            if (string.IsNullOrEmpty(_NombreLibro))
            {
                mensajeExcepcion.Append("El nombre del libro es incorrecto. <br/>");
            }

            if (string.IsNullOrEmpty(_Ruta))
            {
                mensajeExcepcion.Append("La ruta para almacenar el archivo es incorrecta. <br/>");
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
        } // end ValidarMiembros()

        private string RemoverAcentos(string text)
        {
            return string.Concat(
                text.Normalize(NormalizationForm.FormD)
                .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) !=
                                              UnicodeCategory.NonSpacingMark)
              ).Normalize(NormalizationForm.FormC);
        }
    }
}
