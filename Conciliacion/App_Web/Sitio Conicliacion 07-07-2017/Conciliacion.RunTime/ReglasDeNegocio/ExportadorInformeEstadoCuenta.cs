using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using DetalleReporteEstadoCuenta = Conciliacion.RunTime.DatosSQL.InformeBancarioDatos.DetalleReporteEstadoCuenta;
//using DetallePosicionDiariaBancos = Conciliacion.RunTime.DatosSQL.InformeBancarioDatos.DetallePosicionDiariaBancos;
using Conciliacion.RunTime.DatosSQL;    // Temporal

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public class ExportadorInformeEstadoCuenta
    {

        #region Miembros privados

        private Microsoft.Office.Interop.Excel.Application xlAplicacion;
        private Microsoft.Office.Interop.Excel.Workbooks xlLibros;
        private Microsoft.Office.Interop.Excel.Workbook xlLibro;
        private Microsoft.Office.Interop.Excel.Sheets xlHojas;
        private Microsoft.Office.Interop.Excel.Worksheet xlHoja;
        private Microsoft.Office.Interop.Excel.Range xlRango;

        private List<DetalleReporteEstadoCuenta> _DetalleReporteEstadoCuenta;
        //private List<DetallePosicionDiariaBancos> _DetallePosicionDiariaBancos;
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

        public ExportadorInformeEstadoCuenta(List<DetalleReporteEstadoCuenta> DetalleReporteEstadoCuenta,
                                        string Ruta, string Archivo, string Nombre)
        {
            try
            {
                _DetalleReporteEstadoCuenta = DetalleReporteEstadoCuenta;
                _Ruta = Ruta.Trim();
                _Archivo = Archivo.Trim();
                _NombreLibro = Nombre.Trim();

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
                inicializar();
                crearEncabezado();
                //exportarPosicionDiariaBancos();
                //calcularTotalizadores();
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

                if (xlRango != null) Marshal.ReleaseComObject(xlRango);
                if (xlHoja != null) Marshal.ReleaseComObject(xlHoja);
                if (xlHojas != null) Marshal.ReleaseComObject(xlHojas);
                if (xlLibro != null) Marshal.ReleaseComObject(xlLibro);
                if (xlLibros != null) Marshal.ReleaseComObject(xlLibros);
                if (xlAplicacion != null) Marshal.ReleaseComObject(xlAplicacion);
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
            string banco, cuenta, empresa;
            DateTime fecha;
            CultureInfo cultureInfo = CultureInfo.GetCultureInfo("es-MX");

            //banco = _DetalleReporteEstadoCuenta[0].Banco;
            banco = "BANAMEX ";
            cuenta = "CTA " + _DetalleReporteEstadoCuenta[0].CuentaBancoFinanciero + " ";
            empresa = _DetalleReporteEstadoCuenta[0].Corporativo;
            fecha = _DetalleReporteEstadoCuenta[0].FOperacion;

            // Cuenta
            xlRango = xlHoja.Range["B1:H1"];
            xlRango.Merge(true);
            xlRango.Value2 = banco + cuenta + "MOVIMIENTOS DEL MES DE:";

            // Empresa
            xlRango = xlHoja.Range["B2:H2"];
            xlRango.Merge(true);
            xlRango.Value2 = empresa;

            // Mes
            xlRango = xlHoja.Range["D3:H3"];
            xlRango.Merge(true);
            xlRango.Value2 = fecha.ToString("MMMM", cultureInfo).ToUpper();
            xlRango.Font.Color = Excel.XlRgbColor.rgbWhite;
            xlRango.Font.Bold = true;
            xlRango.Font.Size = 13;
            xlRango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            xlRango.Interior.Color = Excel.XlRgbColor.rgbBlue;
            
            // Fecha
            xlRango = xlHoja.Range["I1:K3"];
            xlRango.Merge(true);
            xlRango.Font.Bold = true;
            xlRango[1, 1].Value2 = fecha.ToString("MMMM DE yyyy").ToUpper();
            xlRango[1, 1].Font.Color = Excel.XlRgbColor.rgbWhite;
            xlRango[1, 1].Interior.Color = Excel.XlRgbColor.rgbBlue;
            xlRango[1, 1].IndentLevel = 2;
            // Clabe interbancaria
            xlRango[2, 1].Value2 = "CLABE INTERBANCARIA";
            xlRango[2, 1].IndentLevel = 3;

            // Depósito - retiro - saldos
            xlRango = xlHoja.Range["L1:O4"];
            xlRango.Merge(true);
            xlRango[1, 1].Value2 = "Depósito";
            xlRango[2, 1].Value2 = "Retiro";
            xlRango[3, 1].Value2 = "Saldo final calculado";
            xlRango[4, 1].Value2 = "Saldo final bancario";
            xlRango.Font.Bold = true;

            // Columnas
            xlRango = xlHoja.Range["B4:K4"];
            xlRango[1, 1].Value2 = "Fecha";
            xlRango[1, 2].Value2 = "Referencia";
            xlRango[1, 3].Value2 = "Concepto";
            xlRango[1, 8].Value2 = "Retiros";
            xlRango[1, 9].Value2 = "Depósitos";
            xlRango[1, 10].Value2 = "Saldo";
            //xlRango.Font.Color = Excel.XlRgbColor.blue;
            xlRango.Font.ColorIndex = 23;
            xlRango.Font.Size = 10;
            xlRango.Interior.Color = Excel.XlRgbColor.rgbWhite;
            xlRango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            xlRango.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            xlRango.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium);

            xlRango = xlHoja.Columns["A"];
            xlRango.ColumnWidth = 4;
            xlRango = xlHoja.Range["D4:H4"];
            xlRango.Merge();
            xlRango = xlHoja.Range["I4:K4"];
            xlRango.Font.Bold = true;
            xlRango.ColumnWidth = 13;
            xlRango = xlHoja.Range["I:K"];
            xlRango.ColumnWidth = 13;
            xlRango = xlHoja.Range["B:H"];
            xlRango.ColumnWidth = 10;
        }

        //private void exportarPosicionDiariaBancos()
        //{
        //    string concepto;

        //    xlRango = xlHoja.Range["A4:E19"];
        //    xlRango.Merge(true);
        //    xlRango[1, 1].Value2 = "Portátil";
        //    xlRango[2, 1].Value2 = "Estacionario";
        //    xlRango[3, 1].Value2 = "Edificios";
        //    xlRango[4, 1].Value2 = "Servicios técnicos";
        //    xlRango[5, 1].Value2 = "Crédito portátil";
        //    xlRango[6, 1].Value2 = "Crédito estacionario";
        //    xlRango[7, 1].Value2 = "Crédito edificios";
        //    xlRango[8, 1].Value2 = "Crédito servicios técnicos";
        //    xlRango[9, 1].Value2 = "Cobranza";
        //    xlRango[10, 1].Value2 = "Cobranza filial";
        //    xlRango[11, 1].Value2 = "Otros ingresos";

        //    xlRango[13, 1].Value2 = "Total ingresos del día";
        //    xlRango[14, 1].Value2 = "Total crédito";
        //    xlRango[16, 1].Value2 = "Total ingresos del día neto";
        //    xlRango = xlHoja.Range["A16:A19"];
        //    xlRango.Font.Bold = true;

        //    // Seleccionar cuadro de celdas donde se imprimirán los datos
        //    xlRango = xlHoja.Range["F4:G14"];
        //    foreach (DetallePosicionDiariaBancos item in _DetallePosicionDiariaBancos)
        //    {
        //        concepto = RemoverAcentos(item.Concepto.ToUpper().Trim());

        //        switch (concepto)
        //        {
        //            case CONCEPTO1:
        //                xlRango[1, 1].Value2 = item.Kilos.ToString("0,0.##");
        //                xlRango[1, 2].Value2 = item.Importe.ToString("C");
        //                break;
        //            case CONCEPTO2:
        //                xlRango[2, 1].Value2 = item.Kilos.ToString("0,0.##");
        //                xlRango[2, 2].Value2 = item.Importe.ToString("C");
        //                break;
        //            case CONCEPTO3:
        //                xlRango[3, 1].Value2 = item.Kilos.ToString("0,0.##");
        //                xlRango[3, 2].Value2 = item.Importe.ToString("C");
        //                break;
        //            case CONCEPTO4:
        //                xlRango[4, 1].Value2 = item.Kilos.ToString("0,0.##");
        //                xlRango[4, 2].Value2 = item.Importe.ToString("C");
        //                break;
        //            case CONCEPTO5:
        //                xlRango[5, 1].Value2 = item.Kilos.ToString("0,0.##");
        //                xlRango[5, 2].Value2 = item.Importe.ToString("C");
        //                break;
        //            case CONCEPTO6:
        //                xlRango[6, 1].Value2 = item.Kilos.ToString("0,0.##");
        //                xlRango[6, 2].Value2 = item.Importe.ToString("C");
        //                break;
        //            case CONCEPTO7:
        //                xlRango[7, 1].Value2 = item.Kilos.ToString("0,0.##");
        //                xlRango[7, 2].Value2 = item.Importe.ToString("C");
        //                break;
        //            case CONCEPTO8:
        //                xlRango[8, 1].Value2 = item.Kilos.ToString("0,0.##");
        //                xlRango[8, 2].Value2 = item.Importe.ToString("C");
        //                break;
        //            case CONCEPTO9:
        //                xlRango[9, 1].Value2 = item.Kilos.ToString("0,0.##");
        //                xlRango[9, 2].Value2 = item.Importe.ToString("C");
        //                break;
        //            case CONCEPTO10:
        //                xlRango[10, 1].Value2 = item.Kilos.ToString("0,0.##");
        //                xlRango[10, 2].Value2 = item.Importe.ToString("C");
        //                break;
        //            case CONCEPTO11:
        //                xlRango[11, 1].Value2 = item.Kilos.ToString("0,0.##");
        //                xlRango[11, 2].Value2 = item.Importe.ToString("C");
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}

        //private void calcularTotalizadores()
        //{
        //    string concepto;
        //    decimal totalIngresoDia = 0m;
        //    decimal totalCredito = 0m;
        //    decimal totalNeto = 0m;
        //    decimal totalIngresoDiaKilos = 0m;
        //    decimal totalCreditoKilos = 0m;
        //    decimal totalNetoKilos = 0m;

        //    foreach (DetallePosicionDiariaBancos item in _DetallePosicionDiariaBancos)
        //    {
        //        concepto = RemoverAcentos(item.Concepto.Substring(0, 5).ToUpper().Trim());

        //        if ((!concepto.Equals("CREDI")) && (!concepto.Equals("TOTAL")))
        //        {
        //            //totalIngresoDia += item.Importe;
        //            //totalIngresoDiaKilos += item.Kilos;
        //            totalNeto += item.Importe;
        //            totalNetoKilos += item.Kilos;
        //        }
        //        else if (concepto.Equals("CREDI"))
        //        {
        //            totalCredito += item.Importe;
        //            totalCreditoKilos += item.Kilos;
        //        }
        //    }

        //    totalIngresoDia = totalNeto + totalCredito;
        //    totalIngresoDiaKilos = totalNetoKilos + totalCreditoKilos;

        //    xlRango = xlHoja.Range["F16:G19"];
        //    // Total kilos
        //    xlRango[1, 1].Value2 = totalIngresoDiaKilos.ToString("0,0.##");
        //    xlRango[2, 1].Value2 = totalCreditoKilos.ToString("0,0.##");
        //    xlRango[3, 1].Value2 = "-   ";
        //    xlRango[3, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
        //    xlRango[4, 1].Value2 = totalNetoKilos.ToString("0,0.##");
        //    // Total importe
        //    xlRango[1, 2].Value2 = totalIngresoDia.ToString("C");
        //    xlRango[2, 2].Value2 = totalCredito.ToString("C");
        //    xlRango[3, 2].Value2 = "-   ";
        //    xlRango[3, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
        //    xlRango[4, 2].Value2 = totalNeto.ToString("C");

        //    // Formato
        //    xlRango = xlHoja.Range["A16:G19"];
        //    xlRango.Interior.Color = Excel.XlRgbColor.rgbSkyBlue;
        //    xlRango = xlHoja.Range["A1:G19"];
        //    xlRango.BorderAround2(Excel.XlLineStyle.xlDouble, Excel.XlBorderWeight.xlThin,
        //        Excel.XlColorIndex.xlColorIndexAutomatic);
        //}

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

            if (_DetalleReporteEstadoCuenta == null)
            {
                mensajeExcepcion.Append("La lista del informe bancario está vacía. <br/>");
            }
            else if (_DetalleReporteEstadoCuenta.Count == 0)
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
