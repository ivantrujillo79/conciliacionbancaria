using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Globalization;
using DetallePosicionDiariaBancos = Conciliacion.RunTime.DatosSQL.InformeBancarioDatos.DetallePosicionDiariaBancos;
using DetalleReporteEstadoCuentaConciliado = Conciliacion.RunTime.DatosSQL.InformeBancarioDatos.DetalleReporteEstadoCuentaConciliado;

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

        private List<DetallePosicionDiariaBancos> _DetallePosicionDiariaBancos=null;
        private List<DetalleReporteEstadoCuentaConciliado> _DetalleReporteEstadoCuentaConciliado=null; // MCC 26-04-2018
        private string _Ruta;
        private string _Archivo;
        private string _NombreLibro;
        private string _Banco;

        private List<DateTime> _Fechas;
        private List<PosicionDiaria> _PosicionesDiarias;
        private List<DetalleReporteEstadoCuentaConciliado> _ReporteEstadoCuentaConciliado;


        private DateTime _FechaAOmitir = new DateTime(1900, 1, 1);

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
                _PosicionesDiarias = new List<PosicionDiaria>();

                ValidarMiembros();

            }
            catch(Exception ex)
            {
                throw new Exception("Ocurrió un error en la creación del reporte: <br/>" + ex.Message);
            }
        }

        public ExportadorInformeBancario(List<DetalleReporteEstadoCuentaConciliado> DetalleReporteEstadoCuentaConciliado,
                                        string Ruta, string Archivo, string Nombre,string Banco)
        {
            try
            {
                _DetalleReporteEstadoCuentaConciliado = DetalleReporteEstadoCuentaConciliado;
                _Ruta = Ruta.Trim();
                _Archivo = Archivo.Trim();
                _NombreLibro = Nombre.Trim();
                _ReporteEstadoCuentaConciliado = new List<DetalleReporteEstadoCuentaConciliado>();
                _Banco = Banco;
                ValidarMiembros();

            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error en la creación del reporte: <br/>" + ex.Message);
            }
        }

        #endregion

        #region Clases

        private class PosicionDiaria
        {
            private DateTime _Fecha;
            private int _Columna;
            //private decimal _TotalDiaKilos;
            //private decimal _TotalDiaImporte;
            private decimal _TotalCreditoKilos;
            private decimal _TotalCreditoImporte;
            private decimal _TotalNetoKilos;
            private decimal _TotalNetoImporte;

            public DateTime Fecha
            {
                get { return _Fecha; }
                set { _Fecha = value; }
            }
            public int Columna
            {
                get { return _Columna; }
                set { _Columna = value; }
            }
            public decimal TotalDiaKilos
            {
                //get { return _TotalDiaKilos; }
                //set { _TotalDiaKilos = value; }
                get { return _TotalNetoKilos + _TotalCreditoKilos; }
            }
            public decimal TotalDiaImporte
            {
                //get { return _TotalDiaImporte; }
                //set { _TotalDiaImporte = value; }
                get { return _TotalNetoImporte + _TotalCreditoImporte; }
            }
            public decimal TotalCreditoKilos
            {
                get { return _TotalCreditoKilos; }
                set { _TotalCreditoKilos = value; }
            }
            public decimal TotalCreditoImporte
            {
                get { return _TotalCreditoImporte; }
                set { _TotalCreditoImporte = value; }
            }
            public decimal TotalNetoKilos
            {
                get { return _TotalNetoKilos; }
                set { _TotalNetoKilos = value; }
            }
            public decimal TotalNetoImporte
            {
                get { return _TotalNetoImporte; }
                set { _TotalNetoImporte = value; }
            }

            public PosicionDiaria(DateTime fecha, int columna)
            {
                _Fecha = fecha;
                _Columna = columna;
                //_TotalDiaKilos = 0m;
                //_TotalDiaImporte = 0m;
                _TotalCreditoKilos = 0m;
                _TotalCreditoImporte = 0m;
                _TotalNetoKilos = 0m;
                _TotalNetoImporte = 0m;
            }
        }

        #endregion

        public void generarPosicionDiariaBancos()
        {
            try
            {
                inicializar();
                agruparPorFecha();
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

        public void gerenerarEdoCtaConciliados()
        {
            try
            {
                inicializar();
                crearEncabezadoEdoCuentaConcicliado();
                ExportarEdoCuentaConciliado();
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

        private void agruparPorFecha()
        {
            _Fechas = _DetallePosicionDiariaBancos.Select(detalle => detalle.Fecha)
                                                  .Where(fecha => fecha != DateTime.MinValue && fecha != _FechaAOmitir)
                                                  .Distinct()
                                                  .ToList();
        }

        private void crearEncabezado()
        {
            Excel.Range celdaDiaInicial = null;
            Excel.Range celdaDiaFinal   = null;
            Excel.Range celdaKilos      = null;
            Excel.Range celdaFecha      = null;
            int celda = 6;
            CultureInfo cultureInfo = CultureInfo.GetCultureInfo("es-MX");
            string dia;
            string caja = Convert.ToString(_DetallePosicionDiariaBancos
                                            .First(x => !x.Concepto.ToUpper().Contains("TOTAL"))
                                            .Caja);
            PosicionDiaria posicionDiaria;

            // Nombre del reporte
            xlRango = xlHoja.Range["A1"];
            xlRango.Value2 = "Reporte\nPOSICION DIARIA DE BANCOS";
            xlRango = xlHoja.Range["A1:E2"];
            xlRango.Merge();
            xlRango.Font.Bold = true;
            xlRango.RowHeight = 15;
            xlRango.Borders.LineStyle = Excel.XlLineStyle.xlDouble;
            xlRango.Interior.Color = Excel.XlRgbColor.rgbSkyBlue;

            try
            {
                foreach(DateTime fecha in _Fechas)
                {
                    posicionDiaria = new PosicionDiaria(fecha, celda);
                    _PosicionesDiarias.Add(posicionDiaria);

                    dia = fecha.ToString("dddd", cultureInfo).ToUpper();

                    // Día
                    celdaDiaInicial = xlHoja.Cells[1, celda];
                    celdaDiaFinal = xlHoja.Cells[1, celda + 1];
                    xlRango = xlHoja.Range[celdaDiaInicial, celdaDiaFinal];
                    xlRango.Merge();
                    xlRango.Value2 = dia;

                    // Kilos
                    celdaKilos = celdaDiaInicial.Offset[1, 0];
                    celdaKilos.Value2 = "KILOS";

                    // Fecha
                    celdaFecha = celdaKilos.Offset[0, 1];
                    celdaFecha.Value2 = fecha.ToString("d-MMM-yyyy", cultureInfo);

                    // Formato
                    xlRango = xlHoja.Range[celdaDiaInicial, celdaFecha];
                    xlRango.Borders.LineStyle = Excel.XlLineStyle.xlDouble;
                    xlRango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlRango.ColumnWidth = 16;

                    celda += 2;
                }
                
                // Caja
                xlRango = xlHoja.Range["A3:E3"];
                xlRango.Merge();
                xlRango.Value2 = "CAJA MATRIZ " + caja;
                xlRango.Borders.LineStyle = Excel.XlLineStyle.xlDouble;
                xlRango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            }
            finally
            {
                if (celdaDiaInicial != null)    Marshal.ReleaseComObject(celdaDiaInicial);
                if (celdaDiaFinal != null)      Marshal.ReleaseComObject(celdaDiaFinal);
                if (celdaKilos != null)         Marshal.ReleaseComObject(celdaKilos);
                if (celdaFecha != null)         Marshal.ReleaseComObject(celdaFecha);
            }
        }


        private void crearEncabezadoEdoCuentaConcicliado()
        {
            Excel.Range celdaDiaInicial = null;
            Excel.Range celdaDiaFinal = null;
            Excel.Range celdaKilos = null;
            Excel.Range celdaFecha = null;
            int celda = 6;
            //CultureInfo cultureInfo = CultureInfo.GetCultureInfo("es-MX");
            //string dia;
            //string caja = Convert.ToString(_DetallePosicionDiariaBancos
            //                                .First(x => !x.Concepto.ToUpper().Contains("TOTAL"))
            //                                .Caja);
            //PosicionDiaria posicionDiaria;

            // Nombre del reporte
            xlRango = xlHoja.Range["A1"];
            xlRango.Value2 = "Reporte\nEstado De Cuenta Conciliado";
            xlRango = xlHoja.Range["A1:E2"];
            xlRango.Merge();
            xlRango.Font.Bold = true;
            xlRango.RowHeight = 15;
            xlRango.Borders.LineStyle = Excel.XlLineStyle.xlDouble;
            xlRango.Interior.Color = Excel.XlRgbColor.rgbSkyBlue;

            try
            {
             

                // Caja
                xlRango = xlHoja.Range["A3:E3"];
                xlRango.Merge();
                xlRango.Value2 = "GAS METROPOLITANO S.A DE C.V";
                xlRango.Borders.LineStyle = Excel.XlLineStyle.xlDouble;
                xlRango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            }
            finally
            {
                if (celdaDiaInicial != null) Marshal.ReleaseComObject(celdaDiaInicial);
                if (celdaDiaFinal != null) Marshal.ReleaseComObject(celdaDiaFinal);
                if (celdaKilos != null) Marshal.ReleaseComObject(celdaKilos);
                if (celdaFecha != null) Marshal.ReleaseComObject(celdaFecha);
            }
        }

        private void exportarPosicionDiariaBancos()
        {
            string concepto;
            int columna;

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
            xlRango = xlHoja.Range["A4:B4"];
            foreach (DetallePosicionDiariaBancos item in _DetallePosicionDiariaBancos)
            {
                if (item.Fecha == DateTime.MinValue || item.Fecha == _FechaAOmitir)
                    break;

                concepto = RemoverAcentos(item.Concepto.ToUpper().Trim());
                columna = _PosicionesDiarias.Single(x => x.Fecha == item.Fecha)
                                      .Columna;

                switch (concepto)
                {
                    case CONCEPTO1:
                        xlRango[1, columna].Value2 = item.Kilos.ToString("0,0.##");
                        xlRango[1, columna + 1].Value2 = item.Importe.ToString("C");
                        break;
                    case CONCEPTO2:
                        xlRango[2, columna].Value2 = item.Kilos.ToString("0,0.##");
                        xlRango[2, columna + 1].Value2 = item.Importe.ToString("C");
                        break;
                    case CONCEPTO3:
                        xlRango[3, columna].Value2 = item.Kilos.ToString("0,0.##");
                        xlRango[3, columna + 1].Value2 = item.Importe.ToString("C");
                        break;
                    case CONCEPTO4:
                        xlRango[4, columna].Value2 = item.Kilos.ToString("0,0.##");
                        xlRango[4, columna + 1].Value2 = item.Importe.ToString("C");
                        break;
                    case CONCEPTO5:
                        xlRango[5, columna].Value2 = item.Kilos.ToString("0,0.##");
                        xlRango[5, columna + 1].Value2 = item.Importe.ToString("C");
                        break;
                    case CONCEPTO6:
                        xlRango[6, columna].Value2 = item.Kilos.ToString("0,0.##");
                        xlRango[6, columna + 1].Value2 = item.Importe.ToString("C");
                        break;
                    case CONCEPTO7:
                        xlRango[7, columna].Value2 = item.Kilos.ToString("0,0.##");
                        xlRango[7, columna + 1].Value2 = item.Importe.ToString("C");
                        break;
                    case CONCEPTO8:
                        xlRango[8, columna].Value2 = item.Kilos.ToString("0,0.##");
                        xlRango[8, columna + 1].Value2 = item.Importe.ToString("C");
                        break;
                    case CONCEPTO9:
                        xlRango[9, columna].Value2 = item.Kilos.ToString("0,0.##");
                        xlRango[9, columna + 1].Value2 = item.Importe.ToString("C");
                        break;
                    case CONCEPTO10:
                        xlRango[10, columna].Value2 = item.Kilos.ToString("0,0.##");
                        xlRango[10, columna + 1].Value2 = item.Importe.ToString("C");
                        break;
                    case CONCEPTO11:
                        xlRango[11, columna].Value2 = item.Kilos.ToString("0,0.##");
                        xlRango[11, columna + 1].Value2 = item.Importe.ToString("C");
                        break;
                    default:
                        break;
                }
            }
        }

        private  void ExportarEdoCuentaConciliado()
        {
            int row = 5;
            //int columna;
            //                                                                                                                                                                                                                                                                    Concepto Retiros               Depositos SaldoFinal            ConceptoConciliado DocumentoConciliado
            xlRango = xlHoja.Range["A4:E19"];
            xlRango.Merge(true);


            xlRango = xlHoja.Range["A16:A19"];
            xlRango.Font.Bold = true;

            // Seleccionar cuadro de celdas donde se imprimirán los datos
            xlRango = xlHoja.Range["A4:B4"];
            foreach (DetalleReporteEstadoCuentaConciliado item in _DetalleReporteEstadoCuentaConciliado)
            {              
                       xlRango[row, 1].Value2 = item._Corporativo;
                       xlRango[row, 2].Value2 = item._Sucursal;
                       xlRango[row, 3].Value2 = item._Año;
                       xlRango[row, 4].Value2 = item._CuentaBancoFinanciero;
                       xlRango[row, 5].Value2 = item._ConsecutivoFlujo;
                       xlRango[row, 6].Value2 = item._Fecha;
                       xlRango[row, 7].Value2 = item._Referencia;
                       xlRango[row, 8].Value2 = item._Retiros;
                       xlRango[row, 9].Value2 = item._Depositos;
                       xlRango[row, 10].Value2 = item._SaldoFinal;
                       xlRango[row, 11].Value2 = item._ConceptoConciliado;
                       xlRango[row, 12].Value2 = item._DocumentoConciliado;
                row = row + 1;
            }
            
        }




        private void calcularTotalizadores()
        {
            string concepto;
            int indice = 0;
            int columnaFinal = 0;
            int columna = 0;
            PosicionDiaria posicionDiaria;

            foreach (DetallePosicionDiariaBancos item in _DetallePosicionDiariaBancos)
            {
                if (item.Fecha == DateTime.MinValue || item.Fecha == _FechaAOmitir)
                    break;

                concepto = RemoverAcentos(item.Concepto.Substring(0, 5).ToUpper().Trim());
                posicionDiaria = _PosicionesDiarias.First(posicion => posicion.Fecha == item.Fecha);
                indice = _PosicionesDiarias.IndexOf(posicionDiaria);

                if ((!concepto.Equals("CREDI")) && (!concepto.Equals("TOTAL")))
                {
                    _PosicionesDiarias[indice].TotalNetoImporte += item.Importe;
                    _PosicionesDiarias[indice].TotalNetoKilos += item.Kilos;
                }
                else if (concepto.Equals("CREDI"))
                {
                    _PosicionesDiarias[indice].TotalCreditoImporte += item.Importe;
                    _PosicionesDiarias[indice].TotalCreditoKilos += item.Kilos;
                }
            }

            foreach(PosicionDiaria posicion in _PosicionesDiarias)
            {
                columna = posicion.Columna;

                xlRango = xlHoja.Range["A16:B16"];
                // Total kilos
                xlRango[1, columna].Value2 = posicion.TotalDiaKilos.ToString("0,0.##");
                xlRango[2, columna].Value2 = posicion.TotalCreditoKilos.ToString("0,0.##");
                xlRango[3, columna].Value2 = "-   ";
                xlRango[3, columna].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                xlRango[4, columna].Value2 = posicion.TotalNetoKilos.ToString("0,0.##");
                // Total importe
                xlRango[1, columna + 1].Value2 = posicion.TotalDiaImporte.ToString("C");
                xlRango[2, columna + 1].Value2 = posicion.TotalCreditoImporte.ToString("C");
                xlRango[3, columna + 1].Value2 = "-   ";
                xlRango[3, columna + 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                xlRango[4, columna + 1].Value2 = posicion.TotalNetoImporte.ToString("C");
            }

            // Color de fondo azul cielo
            columnaFinal = _PosicionesDiarias.Select(x => x.Columna)
                                             .Max()
                                             + 1;
            var celdaInicial = xlHoja.Cells[16, 1];
            var celdaFinal = xlHoja.Cells[19, columnaFinal];
            xlRango = xlHoja.Range[celdaInicial, celdaFinal];
            xlRango.Interior.Color = Excel.XlRgbColor.rgbSkyBlue;
            
            // Borde exterior
            celdaInicial = xlHoja.Cells[1, 1];
            xlRango = xlHoja.Range[celdaInicial, celdaFinal];
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
            mensajeExcepcion.Append(string.Empty);
            bool valido = false;

            if (_DetallePosicionDiariaBancos == null && _DetalleReporteEstadoCuentaConciliado == null )
            {
                mensajeExcepcion.Append("La lista del informe bancario está vacía. <br/>");
            }
            if(_DetallePosicionDiariaBancos!=null)
            {
                    if (_DetallePosicionDiariaBancos.Count == 0)
                {
                    mensajeExcepcion.Append("La lista del informe bancario está vacía. <br/>");
                }
            }
            if (_DetalleReporteEstadoCuentaConciliado != null)
            {
               if( _DetalleReporteEstadoCuentaConciliado.Count == 0)
                {
                    mensajeExcepcion.Append("La lista del informe bancario está vacía. <br/>");
                }
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
