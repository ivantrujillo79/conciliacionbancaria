using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using DetallePosicionDiariaBancos = Conciliacion.RunTime.DatosSQL.InformeBancarioDatos.DetallePosicionDiariaBancos;
using OfficeOpenXml;


namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public class ExportadorInformeBancario
    {

        #region Miembros privados

        private List<DetallePosicionDiariaBancos> _DetallePosicionDiariaBancos=null;
        private string _Ruta;
        private string _Archivo;
        private string _NombreHoja;
        
        private List<DateTime> _Fechas;
        private List<PosicionDiaria> _PosicionesDiarias;
        
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
                _NombreHoja = Nombre.Trim();
                _PosicionesDiarias = new List<PosicionDiaria>();

                ValidarMiembros();

            }
            catch(Exception ex)
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
                ExcelPackage excelPackage = inicializar();
                //crearEncabezado(excelPackage, "NombreHoja");
                //exportarDatos(excelPackage, "NombreHoja", 8);
                /*cerrar();*/

                string caja = Convert.ToString(_DetallePosicionDiariaBancos
                                                .First(x => !x.Concepto.ToUpper().Contains("TOTAL"))
                                                .Caja);


                agruparPorFecha();
                CrearEncabezado(excelPackage, caja );
                exportarPosicionDiariaBancos(excelPackage);

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

            }
        }

        private void agruparPorFecha()
        {
            _Fechas = _DetallePosicionDiariaBancos.Select(detalle => detalle.Fecha)
                                                  .Where(fecha => fecha != DateTime.MinValue && fecha != _FechaAOmitir)
                                                  .Distinct()
                                                  .ToList();
        }

        private ExcelPackage inicializar()
        {
            string rutaCompleta = _Ruta + _Archivo;
            ExcelPackage excel = new ExcelPackage(new FileInfo(@rutaCompleta));

            return excel;
        }

      


        public ExcelPackage CrearEncabezado(ExcelPackage excelPackage, string nombrehoja)
        {

            ExcelWorksheet xlHoja = excelPackage.Workbook.Worksheets.Add(nombrehoja);
            //var cell as new  = wsSheet1.Cells["A1"];
            //ExcelRange as 
            ExcelRange celdaDiaInicial = xlHoja.Cells["B1:H1"];
            ExcelRange celdaDiaFinal = xlHoja.Cells["B1:H1"];
            ExcelRange celdaKilos = xlHoja.Cells["B1:H1"];
            ExcelRange celdaFecha = xlHoja.Cells["B1:H1"];

            //Excel.Range celdaDiaInicial = null;
            //Excel.Range celdaDiaFinal = null;
            //Excel.Range celdaKilos = null;
            //Excel.Range celdaFecha = null;

            int celda = 6;
            CultureInfo cultureInfo = CultureInfo.GetCultureInfo("es-MX");
            string dia;

               
            string caja = Convert.ToString(_DetallePosicionDiariaBancos
                                            .First(x => !x.Concepto.ToUpper().Contains("TOTAL"))
                                            .Caja);
            PosicionDiaria posicionDiaria;

            // Nombre del reporte

            ExcelRange xlRango = xlHoja.Cells["A1:H1"];
            
                xlHoja.Cells["A1"].Value = "REPORTE ";
                xlHoja.Cells["B1"].Value = "POSICION DIARIA DE BANCOS ";
                xlRango = xlHoja.Cells["A1"];
                xlRango.Value = "Reporte\nPOSICION DIARIA DE BANCOS";
                xlRango = xlHoja.Cells["A1:E2"];
                xlRango.Merge = true;
                xlRango.Style.Font.Bold = true;
                //xlRango.RowHeight = 15;
                //xlRango.Borders.LineStyle = Excel.XlLineStyle.xlDouble;
                //xlRango.Interior.Color = Excel.XlRgbColor.rgbSkyBlue;

                try
            {
                foreach (DateTime fecha in _Fechas)
                {
                    posicionDiaria = new PosicionDiaria(fecha, celda);
                    _PosicionesDiarias.Add(posicionDiaria);

                    dia = fecha.ToString("dddd", cultureInfo).ToUpper();

                    // Día
                    celdaDiaInicial = xlHoja.Cells[1, celda];
                    celdaDiaFinal = xlHoja.Cells[1, celda + 1];
                    xlRango = xlHoja.Cells[1,celda + 1];
                    xlRango.Merge = true;
                    xlRango.Value = dia;

                    // Kilos
                    celdaKilos = xlHoja.Cells[2, celda + 1];
                        celdaKilos.Value = "KILOS";

                    // Fecha
                    celdaFecha = xlHoja.Cells[2, celda + 2];
                    celdaFecha.Value = fecha.ToString("d-MMM-yyyy", cultureInfo);

                    //// Formato
                    //xlRango = xlHoja.Cells[1, celda+3];
                    //xlRango.Borders.LineStyle = Excel.XlLineStyle.xlDouble;
                    //xlRango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    //xlRango.ColumnWidth = 16;

                    celda += 2;
                }

                //// Caja
                //xlRango = xlHoja.Range["A3:E3"];
                //xlRango.Merge();
                //xlRango.Value = "CAJA MATRIZ " + caja;
                //xlRango.Borders.LineStyle = Excel.XlLineStyle.xlDouble;
                //xlRango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            }
            finally
            {
                //if (celdaDiaInicial != null) Marshal.ReleaseComObject(celdaDiaInicial);
                //if (celdaDiaFinal != null) Marshal.ReleaseComObject(celdaDiaFinal);
                //if (celdaKilos != null) Marshal.ReleaseComObject(celdaKilos);
                //if (celdaFecha != null) Marshal.ReleaseComObject(celdaFecha);
            }

            return excelPackage;
        }

        private void exportarPosicionDiariaBancos(ExcelPackage excelPackage)
        {
            string concepto;
            int columna;

            
            var xlHoja = excelPackage.Workbook.Worksheets[excelPackage.Workbook.Worksheets.Count()];

            ExcelRange celdaDiaIni = xlHoja.Cells["A4:E19"];

           // celdaDiaIni.Style. merge ...
            celdaDiaIni[4, 1].Value = "Portátil";
            celdaDiaIni[5, 1].Value = "Estacionario";
            celdaDiaIni[6, 1].Value = "Edificios";
            celdaDiaIni[7, 1].Value = "Servicios técnicos";
            celdaDiaIni[8, 1].Value = "Crédito portátil";
            celdaDiaIni[9, 1].Value = "Crédito estacionario";
            celdaDiaIni[10, 1].Value = "Crédito edificios";
            celdaDiaIni[11, 1].Value = "Crédito servicios técnicos";
            celdaDiaIni[12, 1].Value = "Cobranza";
            celdaDiaIni[13, 1].Value = "Cobranza filial";
            celdaDiaIni[14, 1].Value = "Otros ingresos";

            celdaDiaIni[16, 1].Value = "Total ingresos del día";
            celdaDiaIni[17, 1].Value = "Total crédito";
            celdaDiaIni[19, 1].Value = "Total ingresos del día neto";
            //celdaDiaIni = xlHoja.Range["A16:A19"];
            //celdaDiaIni.Font.Bold = true;

            // Seleccionar cuadro de celdas donde se imprimirán los datos
            ExcelRange celdaIniDatos = xlHoja.Cells["A4:B4"];

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
                        celdaIniDatos[4, columna].Value = item.Kilos.ToString("0,0.##");
                        celdaIniDatos[4, columna + 1].Value = item.Importe.ToString("C");
                        break;
                    case CONCEPTO2:
                        celdaIniDatos[5, columna].Value = item.Kilos.ToString("0,0.##");
                        celdaIniDatos[5, columna + 1].Value = item.Importe.ToString("C");
                        break;
                    case CONCEPTO3:
                        celdaIniDatos[6, columna].Value = item.Kilos.ToString("0,0.##");
                        celdaIniDatos[6, columna + 1].Value = item.Importe.ToString("C");
                        break;
                    case CONCEPTO4:
                        celdaIniDatos[7, columna].Value = item.Kilos.ToString("0,0.##");
                        celdaIniDatos[7, columna + 1].Value = item.Importe.ToString("C");
                        break;
                    case CONCEPTO5:
                        celdaIniDatos[8, columna].Value = item.Kilos.ToString("0,0.##");
                        celdaIniDatos[8, columna + 1].Value = item.Importe.ToString("C");
                        break;
                    case CONCEPTO6:
                        celdaIniDatos[9, columna].Value = item.Kilos.ToString("0,0.##");
                        celdaIniDatos[9, columna + 1].Value = item.Importe.ToString("C");
                        break;
                    case CONCEPTO7:
                        celdaIniDatos[10, columna].Value = item.Kilos.ToString("0,0.##");
                        celdaIniDatos[10, columna + 1].Value = item.Importe.ToString("C");
                        break;
                    case CONCEPTO8:
                        celdaIniDatos[11, columna].Value = item.Kilos.ToString("0,0.##");
                        celdaIniDatos[11, columna + 1].Value = item.Importe.ToString("C");
                        break;
                    case CONCEPTO9:
                        celdaIniDatos[12, columna].Value = item.Kilos.ToString("0,0.##");
                        celdaIniDatos[12, columna + 1].Value = item.Importe.ToString("C");
                        break;
                    case CONCEPTO10:
                        celdaIniDatos[13, columna].Value = item.Kilos.ToString("0,0.##");
                        celdaIniDatos[13, columna + 1].Value = item.Importe.ToString("C");
                        break;
                    case CONCEPTO11:
                        celdaIniDatos[14, columna].Value = item.Kilos.ToString("0,0.##");
                        celdaIniDatos[14, columna + 1].Value = item.Importe.ToString("C");
                        break;
                    default:
                        break;
                }
            }

            //Area de abajo...

            ExcelRange celdaDiaIniInf = xlHoja.Cells["A4:E19"];

            // celdaDiaIni.Style. merge ...
            celdaDiaIniInf[21, 1].Value = "Efectivo";
            celdaDiaIniInf[22, 1].Value = "Cheque nominativo";
            celdaDiaIniInf[23, 1].Value = "Transferencia electrónica de fondos";
            celdaDiaIniInf[24, 1].Value = "Tarjeta de crédito";
            celdaDiaIniInf[25, 1].Value = "Vales de despensa";
            celdaDiaIniInf[26, 1].Value = "Tarjeta de débito";
            celdaDiaIniInf[27, 1].Value = "Aplicación de Anticipos";
            celdaDiaIniInf[28, 1].Value = "Nómina";
            celdaDiaIniInf[29, 1].Value = "Otros Gastos";
            celdaDiaIniInf[30, 1].Value = "Total Depositado";
            celdaDiaIniInf[31, 1].Value = "Vales y aplicación de ant.";
            celdaDiaIniInf[32, 1].Value = "Total neto depositado";


            //var sheet = excelPackage.Workbook.Worksheets.Add("Formula");

            xlHoja.Cells["F21"].Formula = "SUM(F3:F17)";
            xlHoja.Cells["F22"].Formula = "SUM(G3:G17)";
            xlHoja.Calculate();


            //celdaDiaIni[34, 1].Value = "Total neto depositado";
            //celdaDiaIni[35, 1].Value = "Total crédito";
            //celdaDiaIni[36, 1].Value = "Total ingresos del día neto";
            //celdaDiaIni = xlHoja.Range["A16:A19"];
            //celdaDiaIni.Font.Bold = true;



        }

        //private void calcularTotalizadores()
        //{
        //    string concepto;
        //    int indice = 0;
        //    int columnaFinal = 0;
        //    int columna = 0;
        //    PosicionDiaria posicionDiaria;

        //    foreach (DetallePosicionDiariaBancos item in _DetallePosicionDiariaBancos)
        //    {
        //        if (item.Fecha == DateTime.MinValue || item.Fecha == _FechaAOmitir)
        //            break;

        //        concepto = RemoverAcentos(item.Concepto.Substring(0, 5).ToUpper().Trim());
        //        posicionDiaria = _PosicionesDiarias.First(posicion => posicion.Fecha == item.Fecha);
        //        indice = _PosicionesDiarias.IndexOf(posicionDiaria);

        //        if ((!concepto.Equals("CREDI")) && (!concepto.Equals("TOTAL")))
        //        {
        //            _PosicionesDiarias[indice].TotalNetoImporte += item.Importe;
        //            _PosicionesDiarias[indice].TotalNetoKilos += item.Kilos;
        //        }
        //        else if (concepto.Equals("CREDI"))
        //        {
        //            _PosicionesDiarias[indice].TotalCreditoImporte += item.Importe;
        //            _PosicionesDiarias[indice].TotalCreditoKilos += item.Kilos;
        //        }
        //    }

        //    foreach(PosicionDiaria posicion in _PosicionesDiarias)
        //    {
        //        columna = posicion.Columna;

        //        xlRango = xlHoja.Range["A16:B16"];
        //        // Total kilos
        //        xlRango[1, columna].Value = posicion.TotalDiaKilos.ToString("0,0.##");
        //        xlRango[2, columna].Value = posicion.TotalCreditoKilos.ToString("0,0.##");
        //        xlRango[3, columna].Value = "-   ";
        //        xlRango[3, columna].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
        //        xlRango[4, columna].Value = posicion.TotalNetoKilos.ToString("0,0.##");
        //        // Total importe
        //        xlRango[1, columna + 1].Value = posicion.TotalDiaImporte.ToString("C");
        //        xlRango[2, columna + 1].Value = posicion.TotalCreditoImporte.ToString("C");
        //        xlRango[3, columna + 1].Value = "-   ";
        //        xlRango[3, columna + 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
        //        xlRango[4, columna + 1].Value = posicion.TotalNetoImporte.ToString("C");
        //    }

        //    // Color de fondo azul cielo
        //    if (_PosicionesDiarias.Select(x => x.Columna).Count() != 0)
        //    {
        //        columnaFinal = _PosicionesDiarias.Select(x => x.Columna)
        //                            .Max()
        //                            + 1;

        //        var celdaInicial = xlHoja.Cells[16, 1];
        //        var celdaFinal = xlHoja.Cells[19, columnaFinal];
        //        xlRango = xlHoja.Range[celdaInicial, celdaFinal];
        //        xlRango.Interior.Color = Excel.XlRgbColor.rgbSkyBlue;

        //        // Borde exterior
        //        celdaInicial = xlHoja.Cells[1, 1];
        //        xlRango = xlHoja.Range[celdaInicial, celdaFinal];
        //        xlRango.BorderAround2(Excel.XlLineStyle.xlDouble, Excel.XlBorderWeight.xlThin,
        //            Excel.XlColorIndex.xlColorIndexAutomatic);
        //    }
        //}

        //private void cerrar()
        //{
        //    string rutaCompleta = _Ruta + _Archivo;

        //    //if (File.Exists(rutaCompleta)) File.Delete(rutaCompleta);

        //    xlLibro.SaveAs(rutaCompleta, Excel.XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing,
        //                    false, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing,
        //                    Type.Missing, Type.Missing, Type.Missing, Type.Missing);

        //    xlLibro.Close(0);
        //    xlLibros.Close();
        //    xlAplicacion.Quit();
        //}

        private bool ValidarMiembros()
        {
            StringBuilder mensajeExcepcion = new StringBuilder();
            mensajeExcepcion.Append(string.Empty);
            bool valido = false;

            if (_DetallePosicionDiariaBancos == null)
            {
                mensajeExcepcion.Append("No se encontraron datos con los parámetros seleccionados. <br/>");
            }
            else if(_DetallePosicionDiariaBancos.Count == 0)
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
