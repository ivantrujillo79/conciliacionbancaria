using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using DetallePosicionDiariaBancos = Conciliacion.RunTime.DatosSQL.InformeBancarioDatos.DetallePosicionDiariaBancos;
using OfficeOpenXml;
using System.Drawing;
using System.Data;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public class ExportadorInformeBancario
    {

        #region Miembros privados

        private List<DetallePosicionDiariaBancos> _DetallePosicionDiariaBancos = null;
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

        private const string CONCEPTO12 = "EFECTIVO";
        private const string CONCEPTO13 = "CHEQUES";
        private const string CONCEPTO14 = "TRANSFERENCIA";
        private const string CONCEPTO15 = "TARJETA DE CREDITO";
        private const string CONCEPTO16 = "VALES DE DESPENSA";
        private const string CONCEPTO17 = "TARJETA DE DEBITO";
        private const string CONCEPTO18 = "APLICACION DE ANTICIPOS";
        private const string CONCEPTO19 = "NOMINA";
        private const string CONCEPTO20 = "OTROS GASTOS";
        private const string CONCEPTO21 = "TOTAL DEPOSITADO";
        private const string CONCEPTO22 = "VALES Y APLICACION DE ANTICIPO";
        private const string CONCEPTO23 = "TOTAL NETO DEPOSITADO";

        private const string CONCEPTO24 = "0671084374";

        private decimal SumaConcepto1 = 0;
        private decimal SumaConcepto2 = 0;
        private decimal SumaConcepto3 = 0;
        private decimal SumaConcepto4 = 0;
        private decimal SumaConcepto5 = 0;
        private decimal SumaConcepto6 = 0;
        private decimal SumaConcepto7 = 0;
        private decimal SumaConcepto8 = 0;
        private decimal SumaConcepto9 = 0;
        private decimal SumaConcepto10 = 0;
        private decimal SumaConcepto11 = 0;
        private decimal SumaConcepto12 = 0;
        private decimal SumaConcepto13 = 0;
        private decimal SumaConcepto14 = 0;
        private decimal SumaConcepto15 = 0;
        private decimal SumaConcepto16 = 0;
        private decimal SumaConcepto17 = 0;
        private decimal SumaConcepto18 = 0;
        private decimal SumaConcepto19 = 0;
        private decimal SumaConcepto20 = 0;
        private decimal SumaConcepto21 = 0;
        private decimal SumaConcepto22 = 0;
        private decimal SumaConcepto23 = 0;
        private decimal SumaConcepto24 = 0;
        private decimal SumaConcepto25 = 0;
        private decimal SumaConcepto26 = 0;
        private decimal SumaConcepto27 = 0;
        private decimal SumaConcepto28 = 0;
        private decimal SumaConcepto29 = 0;
        private decimal SumaConcepto30 = 0;
        private decimal SumaConcepto31 = 0;
        private decimal SumaConcepto32 = 0;

        private decimal SumaConcepto35 = 0;
        private decimal SumaConcepto36 = 0;
        private decimal SumaConcepto37 = 0;
        private decimal SumaConcepto38 = 0;
        private decimal SumaConcepto39 = 0;
        private decimal SumaConcepto40 = 0;
        private decimal SumaConcepto41 = 0;
        private decimal SumaConcepto42 = 0;
        private decimal SumaConcepto43 = 0;

        private decimal SumaKilos4 = 0;
        private decimal SumaKilos5 = 0;
        private decimal SumaKilos6 = 0;
        private decimal SumaKilos7 = 0;
        private decimal SumaKilos8 = 0;
        private decimal SumaKilos9 = 0;
        private decimal SumaKilos10 = 0;
        private decimal SumaKilos11 = 0;
        private decimal SumaKilos12 = 0;
        private decimal SumaKilos13 = 0;
        private decimal SumaKilos14 = 0;

        private decimal SumaKilos16 = 0;
        private decimal SumaKilos17 = 0;
        private decimal SumaKilos19 = 0;
        private decimal SumaKilos21 = 0;
        private decimal SumaKilos22 = 0;
        private decimal SumaKilos23 = 0;
        private decimal SumaKilos24 = 0;
        private decimal SumaKilos25 = 0;
        private decimal SumaKilos26 = 0;
        private decimal SumaKilos27 = 0;
        private decimal SumaKilos28 = 0;
        private decimal SumaKilos29 = 0;
        private decimal SumaKilos30 = 0;
        private decimal SumaKilos31 = 0;
        private decimal SumaKilos32 = 0;

        private decimal SumaKilos35 = 0;
        private decimal SumaKilos36 = 0;
        private decimal SumaKilos37 = 0;
        private decimal SumaKilos38 = 0;
        private decimal SumaKilos39 = 0;
        private decimal SumaKilos40 = 0;
        private decimal SumaKilos41 = 0;
        private decimal SumaKilos42 = 0;
        private decimal SumaKilos43 = 0;



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
            private int _Detalle;

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

            public int Detalle
            {
                get { return _Detalle; }
                set { _Detalle = value; }
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
                _Detalle = 0;
              
            }
        }

        #endregion

        public void generarTotales()
        {


        }

        public void generarPosicionDiariaBancos(int Esfinal)
        {
            try
            {
                ExcelPackage excelPackage = inicializar();
                //crearEncabezado(excelPackage, "NombreHoja");
                //exportarDatos(excelPackage, "NombreHoja", 8);
                /*cerrar();*/

                StringBuilder mensajeExcepcion = new StringBuilder();
                mensajeExcepcion.Append(string.Empty);


                if (_DetallePosicionDiariaBancos == null)
                {
                    mensajeExcepcion.Append("No se encontraron datos con los parámetros seleccionados. <br/>");
                }
                else if (_DetallePosicionDiariaBancos.Count == 0)
                {
                    mensajeExcepcion.Append("No se encontraron datos con los parámetros seleccionados. <br/>");
                }
                
                if (_DetallePosicionDiariaBancos.Count > 0)
                {
                    string caja = Convert.ToString(_DetallePosicionDiariaBancos
                                .First(x => !x.Concepto.ToUpper().Contains("TOTAL"))
                                .Caja);
                }

                agruparPorFecha();
                if (Esfinal == 0)
                {
                    CrearEncabezado(excelPackage, _NombreHoja);
                    exportarPosicionDiariaBancos(excelPackage, _NombreHoja);
                    //Hasta aquí, se genera lo qe viene en el query, falta la hoja de totales
                }
                else
                {
                    CrearEncabezado(excelPackage, "TOTAL");
                    exportarPosicionDiariaBancos(excelPackage, "TOTAL");
                }

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


        private static void CreateSection(ExcelRange basePosition)
        {
            var rangeToMerge = basePosition.Offset(0, 0, 2, 1);
            rangeToMerge.Merge = true;
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

            int celda = 1;
            CultureInfo cultureInfo = CultureInfo.GetCultureInfo("es-MX");
            string dia;

            if (_DetallePosicionDiariaBancos.Count > 0)
            {
                string caja = Convert.ToString(_DetallePosicionDiariaBancos
                                                .First(x => !x.Concepto.ToUpper().Contains("TOTAL"))
                                                .Caja);
            }

 


            //DetallePosicionDiariaBancos FilteredUsers = _DetallePosicionDiariaBancos.Where(i => i.Concepto.ToLower().Contains("0671084374"));

            //List<DetallePosicionDiariaBancos> Detallecontinuo = _DetallePosicionDiariaBancos.Contains("0671084374");
            //List<string> resultList = files.Where(x => x.EndsWith("_Test.txt")).ToList();
            //var filteredFileList = _DetallePosicionDiariaBancos.Where(concepto => _DetallePosicionDiariaBancos.Contains("0671084374"));

            PosicionDiaria posicionDiaria;

            // Nombre del reporte

            ExcelRange xlRango = xlHoja.Cells["A1:H1"];

            //xlHoja.Cells["A1"].Value = "REPORTE ";
            //xlHoja.Cells["B1"].Value = "POSICION DIARIA DE BANCOS ";
            xlRango = xlHoja.Cells["A1"];
            xlRango.Value = "Reporte\nPOSICION DIARIA DE BANCOS";
            xlRango = xlHoja.Cells["A1"];
            xlRango.Merge = true;
            xlRango.Style.Font.Bold = true;

            xlRango.Style.Font.Bold = true;
            xlRango.Style.Font.Size = 12;
            xlRango.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            xlRango.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            xlRango.Style.Fill.BackgroundColor.SetColor(Color.DeepSkyBlue);
            xlRango.Style.Font.Color.SetColor(Color.Black);

            xlRango = xlHoja.Cells["A2"];
            xlRango.Value = nombrehoja;
            xlRango.Merge = true;
            xlRango.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
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

                    if (nombrehoja != "TOTAL")
                    {
                        dia = fecha.ToString("dddd", cultureInfo).ToUpper();
                    }
                    else
                    {
                        dia = fecha.ToString("dddd", cultureInfo).ToUpper();
                    }


                    // Día
                    celdaDiaInicial = xlHoja.Cells[1, celda];
                    celdaDiaFinal = xlHoja.Cells[1, celda + 1];

                    var range = xlHoja.Cells[1, celda + 1, 1, celda + 2];//Address "b1:c1                    
                    range.Merge = true;
                    range.Value = dia;
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.White);
                    enmarcarRegion(range);


                    // Kilos
                    celdaKilos = xlHoja.Cells[2, celda + 1];
                    celdaKilos.Value = "KILOS";
                    celdaKilos.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    celdaKilos.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    celdaKilos.Style.Border.Top.Color.SetColor(Color.Black);
                    celdaKilos.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    celdaKilos.Style.Border.Left.Color.SetColor(Color.Black);
                    celdaKilos.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    celdaKilos.Style.Border.Right.Color.SetColor(Color.Black);
                    celdaKilos.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    celdaKilos.Style.Border.Bottom.Color.SetColor(Color.Black);

                    // celdaKilos.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;

                    // Fecha
                    celdaFecha = xlHoja.Cells[2, celda + 2];
                    if (nombrehoja != "TOTAL")
                    {
                        celdaFecha.Value = fecha.ToString("d-MMM-yyyy", cultureInfo);
                    }
                    else
                    {
                        //celdaFecha.Value = fecha.ToString("MMMM", cultureInfo);
                        celdaFecha.Value = fecha.ToString("d-MMM-yyyy", cultureInfo);
                    }


                    celdaFecha.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    celdaFecha.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    celdaFecha.Style.Border.Top.Color.SetColor(Color.Black);
                    celdaFecha.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    celdaFecha.Style.Border.Left.Color.SetColor(Color.Black);
                    celdaFecha.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    celdaFecha.Style.Border.Right.Color.SetColor(Color.Black);
                    celdaFecha.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    celdaFecha.Style.Border.Bottom.Color.SetColor(Color.Black);
                    //celdaFecha.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;

                    //// Formato
                    //xlRango = xlHoja.Cells[1, celda+3];
                    //xlRango.Borders.LineStyle = Excel.XlLineStyle.xlDouble;
                    //xlRango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    //xlRango.ColumnWidth = 16;

                    celda += 2;

                    //if (nombrehoja == "TOTAL")
                    //    break;
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

            xlHoja.Cells[xlHoja.Dimension.Address].AutoFitColumns();

            return excelPackage;
        }

        private void enmarcarRegion(ExcelRange Rng)
        {
            Rng.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            // Rng.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            Rng.Style.Border.Top.Color.SetColor(Color.Black);
            Rng.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            Rng.Style.Border.Left.Color.SetColor(Color.Black);
            Rng.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            Rng.Style.Border.Right.Color.SetColor(Color.Black);
            Rng.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            Rng.Style.Border.Bottom.Color.SetColor(Color.Black);
        }



        private void exportarPosicionDiariaBancos(ExcelPackage excelPackage, String descripcioncaja)
        {
            string concepto;
            int esDetalle; 
            int columna = 1;

            if (descripcioncaja == "TOTAL")
            {
                _DetallePosicionDiariaBancos.Clear(); 
            }

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
            ExcelRange celdaIniDatos = xlHoja.Cells["A4:A4"];

            int renglontodos = 0;
            Dictionary<string, DataTable> dic = new Dictionary<string, DataTable>();
            DataTable dtEstructura = new DataTable();
            dtEstructura.Columns.Add("Concepto", typeof(string));
            dtEstructura.Columns.Add("Importe", typeof(float));
            dtEstructura.Columns.Add("Fecha", typeof(DateTime));
            dtEstructura.Columns.Add("Columna", typeof(int));

            DataTable dtDetTemp = new DataTable();
            dtDetTemp.Columns.Add("Concepto", typeof(string));
            dtDetTemp.Columns.Add("Importe", typeof(float));
            dtDetTemp.Columns.Add("Fecha", typeof(DateTime));
            dtDetTemp.Columns.Add("Columna", typeof(int));

            foreach (DetallePosicionDiariaBancos item in _DetallePosicionDiariaBancos)
            {
                if (item.Fecha != _FechaAOmitir)
                {

                    concepto = RemoverAcentos(item.Concepto.ToUpper().Trim());
                    esDetalle = item.Detalle;

                    if (descripcioncaja != "TOTAL")
                    {
                        columna = _PosicionesDiarias.Single(x => x.Fecha == item.Fecha)
                                     .Columna;
                    }
                    else
                    {
                        columna = 1;
                        item.Kilos = 0;
                        item.Importe = 0;
                    }

                    columna = columna + 1;
                    string conceptoOriginal = "";

                    if (esDetalle == 1)
                    {
                        conceptoOriginal = concepto;
                        if (renglontodos == 0)
                        {
                            renglontodos = 36;
                        }
                        concepto = CONCEPTO24;
                    }

                    switch (concepto)
                    {
                        case CONCEPTO1:
                            celdaIniDatos[4, columna].Value = item.Kilos;
                            celdaIniDatos.Style.Numberformat.Format = "###,###,##0.00";
                            celdaIniDatos[4, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";



                            break;
                        case CONCEPTO2:
                            celdaIniDatos[5, columna].Value = item.Kilos;
                            celdaIniDatos.Style.Numberformat.Format = "###,###,##0.00";
                            celdaIniDatos[5, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";



                            break;
                        case CONCEPTO3:
                            celdaIniDatos[6, columna].Value = item.Kilos;
                            celdaIniDatos.Style.Numberformat.Format = "###,###,##0.00";
                            celdaIniDatos[6, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";

                            break;
                        case CONCEPTO4:
                            celdaIniDatos[7, columna].Value = item.Kilos;
                            celdaIniDatos.Style.Numberformat.Format = "###,###,##0.00";
                            celdaIniDatos[7, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";


                            break;
                        case CONCEPTO5:
                            celdaIniDatos[8, columna].Value = item.Kilos;
                            celdaIniDatos.Style.Numberformat.Format = "###,###,##0.00";
                            celdaIniDatos[8, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";


                            break;
                        case CONCEPTO6:
                            celdaIniDatos[9, columna].Value = item.Kilos;
                            celdaIniDatos.Style.Numberformat.Format = "###,###,##0.00";
                            celdaIniDatos[9, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";

                            break;
                        case CONCEPTO7:
                            celdaIniDatos[10, columna].Value = item.Kilos;
                            celdaIniDatos.Style.Numberformat.Format = "###,###,##0.00";
                            celdaIniDatos[10, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";

                            break;
                        case CONCEPTO8:
                            celdaIniDatos[11, columna].Value = item.Kilos;
                            celdaIniDatos.Style.Numberformat.Format = "###,###,##0.00";
                            celdaIniDatos[11, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";

                            break;
                        case CONCEPTO9:
                            celdaIniDatos[12, columna].Value = item.Kilos;
                            celdaIniDatos.Style.Numberformat.Format = "###,###,##0.00";
                            celdaIniDatos[12, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";


                            break;
                        case CONCEPTO10:
                            celdaIniDatos[13, columna].Value = item.Kilos;
                            celdaIniDatos.Style.Numberformat.Format = "###,###,##0.00";
                            celdaIniDatos[13, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            SumaConcepto10 = SumaConcepto10 + item.Kilos;


                            break;
                        case CONCEPTO11:
                            celdaIniDatos[14, columna].Value = item.Kilos;
                            celdaIniDatos.Style.Numberformat.Format = "###,###,##0.00";
                            celdaIniDatos[14, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";

                            break;

                        case CONCEPTO12:
                            celdaIniDatos[21, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";

                            break;
                        case CONCEPTO13:
                            celdaIniDatos[22, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";

                            break;
                        case CONCEPTO14:
                            celdaIniDatos[23, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";

                            break;

                        case CONCEPTO15:
                            celdaIniDatos[24, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";

                            break;

                        case CONCEPTO16:
                            celdaIniDatos[25, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";

                            break;

                        case CONCEPTO17:
                            celdaIniDatos[26, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";

                            break;

                        case CONCEPTO18:
                            celdaIniDatos[27, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";

                            break;

                        case CONCEPTO19:
                            celdaIniDatos[28, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";

                            break;

                        case CONCEPTO20:
                            celdaIniDatos[29, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";

                            break;

                        case CONCEPTO21:
                            celdaIniDatos[30, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";

                            break;

                        case CONCEPTO22:
                            celdaIniDatos[31, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";

                            break;

                        case CONCEPTO23:
                            celdaIniDatos[32, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";

                            break;

                        case CONCEPTO24:
                            if (descripcioncaja != "TOTAL")
                            {
                                int finalrows = xlHoja.Dimension.End.Row;
                                //DataTable dt;
                                //string dtNombre = item.Fecha.ToShortDateString();
                                //if (!dic.TryGetValue(dtNombre, out dt))
                                //{
                                //    dic.Add(dtNombre, new DataTable());
                                //    dic.TryGetValue(dtNombre, out dt);
                                //    dt = dtEstructura.Clone();
                                //}
                                //DataRow NewRow = dt.NewRow();
                                //NewRow["Concepto"] = conceptoOriginal;
                                //NewRow["Importe"] = item.Importe;
                                //dt.Rows.Add(NewRow);
                                //dic[dtNombre] = dt;

                                DataRow NewRow = dtDetTemp.NewRow();
                                NewRow["Concepto"] = conceptoOriginal;
                                NewRow["Importe"] = item.Importe;
                                NewRow["Fecha"] = item.Fecha;
                                NewRow["Columna"] = columna;
                                dtDetTemp.Rows.Add(NewRow);

                            }
                            break;

                        default:
                            break;

                    }
                    CalculaSumatoria(columna + 1, excelPackage);
                }
            }

            DataView view = dtDetTemp.DefaultView;
            view.Sort = "Concepto ASC";
            DataTable dtDetTempSorted = view.ToTable();

            foreach (DataRow item in dtDetTempSorted.Rows)
            {
                DataTable dt;
                string dicKey = item["Concepto"].ToString();
                if (!dic.TryGetValue(dicKey, out dt))
                {
                    dic.Add(dicKey, new DataTable());
                    dic.TryGetValue(dicKey, out dt);
                    dt = dtEstructura.Clone();
                }
                DataRow NewRow = dt.NewRow();
                NewRow["Concepto"] = item["Concepto"].ToString();
                NewRow["Importe"] = item["Importe"].ToString();
                NewRow["Columna"] = item["Columna"].ToString();
                NewRow["Fecha"] = item["Fecha"].ToString();
                dt.Rows.Add(NewRow);
                dic[dicKey] = dt;
            }

            int colAntes = -1;
            foreach (KeyValuePair<string, DataTable> pair in dic)
            {
                renglontodos = 36;
                DataTable dt = pair.Value;
                foreach (DataRow itemDia in dt.Rows)
                {
                    columna = int.Parse(itemDia["columna"].ToString()) + 1;
                    if (columna != colAntes)
                    { 
                        colAntes = columna;
                        renglontodos = 36;
                    }
                    if (celdaIniDatos[renglontodos, 1].Value != null && ! celdaIniDatos[renglontodos, 1].Value.Equals(""))
                    {
                        while (celdaIniDatos[renglontodos, 1].Value != null && !celdaIniDatos[renglontodos, 1].Value.Equals(itemDia["Concepto"].ToString()))
                            if (celdaIniDatos[renglontodos, 1].Value.Equals(itemDia["Concepto"].ToString()))
                                break;
                            else
                                renglontodos++;
                    }
                    celdaIniDatos[renglontodos, 1].Value = itemDia["Concepto"].ToString();
                    celdaIniDatos[renglontodos, columna].Value = itemDia["Importe"]; 
                    celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                    celdaIniDatos.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    celdaIniDatos.Style.Fill.BackgroundColor.SetColor(Color.Turquoise);
                    renglontodos = renglontodos + 1;                    
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

            celdaDiaIniInf[34, 1].Value = "Diferencia";

            if (descripcioncaja == "TOTAL")
            {
                columna = 3;

                var xlHojaaSumar = excelPackage.Workbook.Worksheets[1];
                //Get the final column for the column in the worksheet
                int finalrows = xlHojaaSumar.Dimension.End.Row;
                int totalhojas = excelPackage.Workbook.Worksheets.Count();
                int totalcolumnas = xlHojaaSumar.Dimension.End.Column;
                var xlHojaTotales = excelPackage.Workbook.Worksheets[totalhojas];
                string nombrecelda = "";
                for (int j = 1; j < totalhojas; j++)
                {
                    xlHojaaSumar = excelPackage.Workbook.Worksheets[j];

                    for (int m = 3; m <= totalcolumnas; m += 2)
                    {
                        xlHojaTotales.Cells[4, m].Value = ((xlHojaTotales.Cells[4, m].Value != null) ? System.Convert.ToDecimal(xlHojaTotales.Cells[4, m].Value.ToString()) : 0) + ((xlHojaaSumar.Cells[4, m].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[4, m].Value.ToString()) : 0);
                        xlHojaTotales.Cells[5, m].Value = ((xlHojaTotales.Cells[5, m].Value != null) ? System.Convert.ToDecimal(xlHojaTotales.Cells[5, m].Value.ToString()) : 0) + ((xlHojaaSumar.Cells[5, m].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[5, m].Value.ToString()) : 0);
                        xlHojaTotales.Cells[6, m].Value = ((xlHojaTotales.Cells[6, m].Value != null) ? System.Convert.ToDecimal(xlHojaTotales.Cells[6, m].Value.ToString()) : 0) + ((xlHojaaSumar.Cells[6, m].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[6, m].Value.ToString()) : 0);
                        xlHojaTotales.Cells[7, m].Value = ((xlHojaTotales.Cells[7, m].Value != null) ? System.Convert.ToDecimal(xlHojaTotales.Cells[7, m].Value.ToString()) : 0) + ((xlHojaaSumar.Cells[7, m].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[7, m].Value.ToString()) : 0);
                        xlHojaTotales.Cells[8, m].Value = ((xlHojaTotales.Cells[8, m].Value != null) ? System.Convert.ToDecimal(xlHojaTotales.Cells[8, m].Value.ToString()) : 0) + ((xlHojaaSumar.Cells[8, m].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[8, m].Value.ToString()) : 0);
                        xlHojaTotales.Cells[9, m].Value = ((xlHojaTotales.Cells[9, m].Value != null) ? System.Convert.ToDecimal(xlHojaTotales.Cells[9, m].Value.ToString()) : 0) + ((xlHojaaSumar.Cells[9, m].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[9, m].Value.ToString()) : 0);
                        xlHojaTotales.Cells[10, m].Value = ((xlHojaTotales.Cells[10, m].Value != null) ? System.Convert.ToDecimal(xlHojaTotales.Cells[10, m].Value.ToString()) : 0) + ((xlHojaaSumar.Cells[10, m].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[10, m].Value.ToString()) : 0);
                        xlHojaTotales.Cells[11, m].Value = ((xlHojaTotales.Cells[11, m].Value != null) ? System.Convert.ToDecimal(xlHojaTotales.Cells[11, m].Value.ToString()) : 0) + ((xlHojaaSumar.Cells[11, m].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[11, m].Value.ToString()) : 0);
                        xlHojaTotales.Cells[12, m].Value = ((xlHojaTotales.Cells[12, m].Value != null) ? System.Convert.ToDecimal(xlHojaTotales.Cells[12, m].Value.ToString()) : 0) + ((xlHojaaSumar.Cells[12, m].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[12, m].Value.ToString()) : 0);
                        xlHojaTotales.Cells[13, m].Value = ((xlHojaTotales.Cells[13, m].Value != null) ? System.Convert.ToDecimal(xlHojaTotales.Cells[13, m].Value.ToString()) : 0) + ((xlHojaaSumar.Cells[13, m].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[13, m].Value.ToString()) : 0);
                        xlHojaTotales.Cells[14, m].Value = ((xlHojaTotales.Cells[14, m].Value != null) ? System.Convert.ToDecimal(xlHojaTotales.Cells[14, m].Value.ToString()) : 0) + ((xlHojaaSumar.Cells[14, m].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[14, m].Value.ToString()) : 0);

                        xlHojaTotales.Cells[16, m].Value = ((xlHojaTotales.Cells[16, m].Value != null) ? System.Convert.ToDecimal(xlHojaTotales.Cells[16, m].Value.ToString()) : 0) + ((xlHojaaSumar.Cells[16, m].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[16, m].Value.ToString()) : 0);

                        xlHojaTotales.Cells[17, m].Value = ((xlHojaTotales.Cells[17, m].Value != null) ? System.Convert.ToDecimal(xlHojaTotales.Cells[17, m].Value.ToString()) : 0) + ((xlHojaaSumar.Cells[17, m].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[17, m].Value.ToString()) : 0);
                        xlHojaTotales.Cells[19, m].Value = ((xlHojaTotales.Cells[19, m].Value != null) ? System.Convert.ToDecimal(xlHojaTotales.Cells[19, m].Value.ToString()) : 0) + ((xlHojaaSumar.Cells[19, m].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[19, m].Value.ToString()) : 0);

                        xlHojaTotales.Cells[21, m].Value = ((xlHojaTotales.Cells[21, m].Value != null) ? System.Convert.ToDecimal(xlHojaTotales.Cells[21, m].Value.ToString()) : 0) + ((xlHojaaSumar.Cells[21, m].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[21, m].Value.ToString()) : 0);
                        xlHojaTotales.Cells[22, m].Value = ((xlHojaTotales.Cells[22, m].Value != null) ? System.Convert.ToDecimal(xlHojaTotales.Cells[22, m].Value.ToString()) : 0) + ((xlHojaaSumar.Cells[22, m].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[22, m].Value.ToString()) : 0);
                        xlHojaTotales.Cells[23, m].Value = ((xlHojaTotales.Cells[23, m].Value != null) ? System.Convert.ToDecimal(xlHojaTotales.Cells[23, m].Value.ToString()) : 0) + ((xlHojaaSumar.Cells[23, m].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[23, m].Value.ToString()) : 0);
                        xlHojaTotales.Cells[24, m].Value = ((xlHojaTotales.Cells[24, m].Value != null) ? System.Convert.ToDecimal(xlHojaTotales.Cells[24, m].Value.ToString()) : 0) + ((xlHojaaSumar.Cells[24, m].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[24, m].Value.ToString()) : 0);

                        xlHojaTotales.Cells[25, m].Value = ((xlHojaTotales.Cells[25, m].Value != null) ? System.Convert.ToDecimal(xlHojaTotales.Cells[25, m].Value.ToString()) : 0) + ((xlHojaaSumar.Cells[25, m].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[25, m].Value.ToString()) : 0);
                        xlHojaTotales.Cells[26, m].Value = ((xlHojaTotales.Cells[26, m].Value != null) ? System.Convert.ToDecimal(xlHojaTotales.Cells[26, m].Value.ToString()) : 0) + ((xlHojaaSumar.Cells[26, m].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[26, m].Value.ToString()) : 0);
                        xlHojaTotales.Cells[27, m].Value = ((xlHojaTotales.Cells[27, m].Value != null) ? System.Convert.ToDecimal(xlHojaTotales.Cells[27, m].Value.ToString()) : 0) + ((xlHojaaSumar.Cells[27, m].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[27, m].Value.ToString()) : 0);
                        xlHojaTotales.Cells[28, m].Value = ((xlHojaTotales.Cells[28, m].Value != null) ? System.Convert.ToDecimal(xlHojaTotales.Cells[28, m].Value.ToString()) : 0) + ((xlHojaaSumar.Cells[28, m].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[28, m].Value.ToString()) : 0);

                        xlHojaTotales.Cells[29, m].Value = ((xlHojaTotales.Cells[29, m].Value != null) ? System.Convert.ToDecimal(xlHojaTotales.Cells[29, m].Value.ToString()) : 0) + ((xlHojaaSumar.Cells[29, m].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[29, m].Value.ToString()) : 0);
                        xlHojaTotales.Cells[30, m].Value = ((xlHojaTotales.Cells[30, m].Value != null) ? System.Convert.ToDecimal(xlHojaTotales.Cells[30, m].Value.ToString()) : 0) + ((xlHojaaSumar.Cells[30, m].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[30, m].Value.ToString()) : 0);
                        xlHojaTotales.Cells[31, m].Value = ((xlHojaTotales.Cells[31, m].Value != null) ? System.Convert.ToDecimal(xlHojaTotales.Cells[31, m].Value.ToString()) : 0) + ((xlHojaaSumar.Cells[31, m].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[31, m].Value.ToString()) : 0);
                        xlHojaTotales.Cells[32, m].Value = ((xlHojaTotales.Cells[32, m].Value != null) ? System.Convert.ToDecimal(xlHojaTotales.Cells[32, m].Value.ToString()) : 0) + ((xlHojaaSumar.Cells[32, m].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[32, m].Value.ToString()) : 0);

                        // string ColumnStringSal = "K8:K" + finalrows.ToString();
                        xlHojaTotales.Column(m).Style.Numberformat.Format = "$###,###,##0.00";
                      
                    }





                }

                // Detalle del final
                 totalhojas = excelPackage.Workbook.Worksheets.Count();
                 totalcolumnas = xlHojaaSumar.Dimension.End.Column;
                 xlHojaTotales = excelPackage.Workbook.Worksheets[totalhojas];

                for (int j = 1; j < totalhojas; j++)
                {
                    xlHojaaSumar = excelPackage.Workbook.Worksheets[j];
                    int maxRenglones = xlHojaaSumar.Dimension.End.Row;
                    int maxColumnas = xlHojaaSumar.Dimension.End.Column;
                    int maxRenglonestotal = xlHojaTotales.Dimension.End.Row;
                    int maxColumnasTotal = xlHojaTotales.Dimension.End.Column;
                    int rowinicial = 35;

                    if (j==1)
                    {
                        try
                        {
                            xlHojaaSumar.Cells[rowinicial, 1, maxRenglones, maxColumnas].Copy(xlHojaTotales.Cells[rowinicial, 1, maxRenglones, maxColumnas]);
                        }
                        catch(System.ArgumentOutOfRangeException ex)
                        {
                            ex = null;
                        }
                    }

                    else
                    {
                        if (xlHojaaSumar.Cells[36,1].Value != null && maxRenglones >34 )

                        xlHojaaSumar.Cells[rowinicial, 1,  maxRenglones, maxColumnas].Copy(xlHojaTotales.Cells[maxRenglonestotal+1, 1, maxRenglonestotal+maxRenglones, maxColumnas]);

                    }
                    
                                      

                }

                  //  workSheet.Cells[1, 5, 100, 5].Copy(workSheet.Cells[1, 2, 100, 2]);

                //nombrecelda = nombrecelda.Remove(nombrecelda.Length - 1);
                //nombrecelda =  nombrecelda.Replace("|", ";");
                //xlHojaTotales.Cells[4, 2].Formula = "SUM(" + nombrecelda + ")";

                xlHoja.Calculate();
                xlHoja.Workbook.FullCalcOnLoad = true;

                excelPackage.Save();
            }
        }

        private void CalculaSumatoria(int columna, ExcelPackage excelPackage)
        {
            var xlHoja = excelPackage.Workbook.Worksheets[excelPackage.Workbook.Worksheets.Count()];
            var range = xlHoja.Cells[columna, 4, columna, 14];//Address "b1:c1   
            xlHoja.Cells[16, columna].Formula = "SUM(" + xlHoja.Cells[4, columna] + ":" + xlHoja.Cells[14, columna] + ")";
            xlHoja.Cells[16, columna].Calculate();
            xlHoja.Column(1).Width = 35;
            xlHoja.Cells[16, columna].Style.Numberformat.Format = "$###,###,##0.00";


            xlHoja.Cells[17, columna].Formula = "SUM(" + xlHoja.Cells[8, columna] + ":" + xlHoja.Cells[11, columna] + ")";
            xlHoja.Cells[17, columna].Style.Numberformat.Format = "$###,###,##0.00";
            xlHoja.Cells[17, columna].Calculate();


            xlHoja.Cells[19, columna].Formula = "SUM(columna,16: columna ,16)-SUM(columna,17:columna,17)";
            xlHoja.Cells[19, columna].Formula = "SUM(" + xlHoja.Cells[16, columna] + ":" + xlHoja.Cells[16, columna] + ")-SUM(" + xlHoja.Cells[17, columna] + ":" + xlHoja.Cells[17, columna] + ")";
            xlHoja.Cells[19, columna].Style.Numberformat.Format = "$###,###,##0.00";
            xlHoja.Cells[19, columna].Calculate();

            xlHoja.Cells[30, columna].Formula = "SUM(" + xlHoja.Cells[21, columna] + ":" + xlHoja.Cells[29, columna] + ")";
            xlHoja.Cells[30, columna].Style.Numberformat.Format = "$###,###,##0.00";
            xlHoja.Cells[30, columna].Calculate();


            xlHoja.Cells[31, columna].Formula = "SUM(" + xlHoja.Cells[25, columna] + ":" + xlHoja.Cells[27, columna] + ")";
            xlHoja.Cells[31, columna].Style.Numberformat.Format = "$###,###,##0.00";
            xlHoja.Cells[31, columna].Calculate();


            xlHoja.Cells[32, columna].Formula = "SUM(" + xlHoja.Cells[30, columna] + ":" + xlHoja.Cells[30, columna] + ")-SUM(" + xlHoja.Cells[31, columna] + ":" + xlHoja.Cells[31, columna] + ")";
            xlHoja.Cells[32, columna].Style.Numberformat.Format = "$###,###,##0.00";
            xlHoja.Cells[32, columna].Calculate();

            xlHoja.Cells[34, columna].Formula = "SUM(" + xlHoja.Cells[19, columna] + ":" + xlHoja.Cells[19, columna] + ")-SUM(" + xlHoja.Cells[32, columna] + ":" + xlHoja.Cells[32, columna] + ")";
            xlHoja.Cells[34, columna].Style.Numberformat.Format = "$###,###,##0.00";
            xlHoja.Cells[34, columna].Calculate();

            xlHoja.Calculate();


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





//    var xlHojaaSumar = excelPackage.Workbook.Worksheets[1];
//for (int j = 1; j < excelPackage.Workbook.Worksheets.Count(); j++)
//{
//    xlHojaaSumar = excelPackage.Workbook.Worksheets[j];
//    for (int k = 3; k <= 70; k += 2)
//    {
//        SumaConcepto4 = SumaConcepto4 + ((xlHojaaSumar.Cells[4, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[4, k].Value.ToString()) : 0);
//        //SumaConcepto5 = SumaConcepto5 + ((xlHojaaSumar.Cells[5, 5].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[5, k].Value.ToString()) : 0);
//        //SumaConcepto6 = SumaConcepto6 + ((xlHojaaSumar.Cells[6, 7].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[6, k].Value.ToString()) : 0);
//        //SumaConcepto7 = SumaConcepto7 + ((xlHojaaSumar.Cells[7, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[7, k].Value.ToString()) : 0);
//        //SumaConcepto8 = SumaConcepto8 + ((xlHojaaSumar.Cells[8, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[8, k].Value.ToString()) : 0);
//        //SumaConcepto9 = SumaConcepto9 + ((xlHojaaSumar.Cells[9, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[9, k].Value.ToString()) : 0);
//        //SumaConcepto10 = SumaConcepto10 + ((xlHojaaSumar.Cells[10, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[10, k].Value.ToString()) : 0);
//        //SumaConcepto11 = SumaConcepto11 + ((xlHojaaSumar.Cells[11, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[11, k].Value.ToString()) : 0);
//        //SumaConcepto12 = SumaConcepto12 + ((xlHojaaSumar.Cells[12, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[12, k].Value.ToString()) : 0);
//        //SumaConcepto13 = SumaConcepto13 + ((xlHojaaSumar.Cells[13, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[13, k].Value.ToString()) : 0);
//        //SumaConcepto14 = SumaConcepto14 + ((xlHojaaSumar.Cells[14, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[14, k].Value.ToString()) : 0);

//        //SumaConcepto16 = SumaConcepto16 + ((xlHojaaSumar.Cells[16, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[16, k].Value.ToString()) : 0);
//        //SumaConcepto17 = SumaConcepto17 + ((xlHojaaSumar.Cells[17, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[17, k].Value.ToString()) : 0);
//        //SumaConcepto19 = SumaConcepto19 + ((xlHojaaSumar.Cells[19, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[19, k].Value.ToString()) : 0);
//        //SumaConcepto21 = SumaConcepto21 + ((xlHojaaSumar.Cells[21, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[21, k].Value.ToString()) : 0);
//        //SumaConcepto22 = SumaConcepto22 + ((xlHojaaSumar.Cells[22, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[22, k].Value.ToString()) : 0);
//        //SumaConcepto23 = SumaConcepto23 + ((xlHojaaSumar.Cells[23, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[23, k].Value.ToString()) : 0);
//        //SumaConcepto24 = SumaConcepto24 + ((xlHojaaSumar.Cells[24, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[24, k].Value.ToString()) : 0);
//        //SumaConcepto25 = SumaConcepto25 + ((xlHojaaSumar.Cells[25, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[25, k].Value.ToString()) : 0);
//        //SumaConcepto26 = SumaConcepto26 + ((xlHojaaSumar.Cells[26, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[26, k].Value.ToString()) : 0);
//        //SumaConcepto27 = SumaConcepto27 + ((xlHojaaSumar.Cells[27, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[27, k].Value.ToString()) : 0);
//        //SumaConcepto28 = SumaConcepto28 + ((xlHojaaSumar.Cells[28, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[28, k].Value.ToString()) : 0);
//        //SumaConcepto29 = SumaConcepto29 + ((xlHojaaSumar.Cells[29, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[29, k].Value.ToString()) : 0);
//        //SumaConcepto30 = SumaConcepto30 + ((xlHojaaSumar.Cells[30, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[30, k].Value.ToString()) : 0);
//        //SumaConcepto31 = SumaConcepto31 + ((xlHojaaSumar.Cells[31, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[31, k].Value.ToString()) : 0);
//        //SumaConcepto32 = SumaConcepto32 + ((xlHojaaSumar.Cells[32, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[32, k].Value.ToString()) : 0);

//        //SumaConcepto35 = SumaConcepto35 + ((xlHojaaSumar.Cells[35, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[35, k].Value.ToString()) : 0);
//        //SumaConcepto36 = SumaConcepto36 + ((xlHojaaSumar.Cells[36, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[36, k].Value.ToString()) : 0);
//        //SumaConcepto37 = SumaConcepto37 + ((xlHojaaSumar.Cells[37, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[37, k].Value.ToString()) : 0);
//        //SumaConcepto38 = SumaConcepto38 + ((xlHojaaSumar.Cells[38, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[38, k].Value.ToString()) : 0);
//        //SumaConcepto39 = SumaConcepto39 + ((xlHojaaSumar.Cells[39, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[39, k].Value.ToString()) : 0);
//        //SumaConcepto40 = SumaConcepto40 + ((xlHojaaSumar.Cells[40, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[40, k].Value.ToString()) : 0);
//        //SumaConcepto41 = SumaConcepto41 + ((xlHojaaSumar.Cells[41, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[41, k].Value.ToString()) : 0);
//        //SumaConcepto42 = SumaConcepto42 + ((xlHojaaSumar.Cells[42, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[42, k].Value.ToString()) : 0);
//        //SumaConcepto43 = SumaConcepto43 + ((xlHojaaSumar.Cells[43, k].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[43, k].Value.ToString()) : 0);

//        //SumaKilos4 = SumaKilos4 + ((xlHojaaSumar.Cells[4, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[4, k - 1].Value.ToString()) : 0);
//        //SumaKilos5 = SumaKilos5 + ((xlHojaaSumar.Cells[5, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[5, k - 1].Value.ToString()) : 0);
//        //SumaKilos6 = SumaKilos6 + ((xlHojaaSumar.Cells[6, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[6, k - 1].Value.ToString()) : 0);
//        //SumaKilos7 = SumaKilos7 + ((xlHojaaSumar.Cells[7, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[7, k - 1].Value.ToString()) : 0);
//        //SumaKilos8 = SumaKilos8 + ((xlHojaaSumar.Cells[8, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[8, k - 1].Value.ToString()) : 0);
//        //SumaKilos9 = SumaKilos9 + ((xlHojaaSumar.Cells[9, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[9, k - 1].Value.ToString()) : 0);
//        //SumaKilos10 = SumaKilos10 + ((xlHojaaSumar.Cells[10, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[10, k - 1].Value.ToString()) : 0);
//        //SumaKilos11 = SumaKilos11 + ((xlHojaaSumar.Cells[11, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[11, k - 1].Value.ToString()) : 0);
//        //SumaKilos12 = SumaKilos12 + ((xlHojaaSumar.Cells[12, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[12, k - 1].Value.ToString()) : 0);
//        //SumaKilos13 = SumaKilos13 + ((xlHojaaSumar.Cells[13, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[13, k - 1].Value.ToString()) : 0);
//        //SumaKilos14 = SumaKilos14 + ((xlHojaaSumar.Cells[14, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[14, k - 1].Value.ToString()) : 0);

//        //SumaKilos16 = SumaKilos16 + ((xlHojaaSumar.Cells[16, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[16, k - 1].Value.ToString()) : 0);
//        //SumaKilos17 = SumaKilos17 + ((xlHojaaSumar.Cells[17, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[17, k - 1].Value.ToString()) : 0);
//        //SumaKilos19 = SumaKilos19 + ((xlHojaaSumar.Cells[19, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[19, k - 1].Value.ToString()) : 0);
//        //SumaKilos21 = SumaKilos21 + ((xlHojaaSumar.Cells[21, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[21, k - 1].Value.ToString()) : 0);
//        //SumaKilos22 = SumaKilos22 + ((xlHojaaSumar.Cells[22, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[22, k - 1].Value.ToString()) : 0);
//        //SumaKilos23 = SumaKilos23 + ((xlHojaaSumar.Cells[23, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[23, k - 1].Value.ToString()) : 0);
//        //SumaKilos24 = SumaKilos24 + ((xlHojaaSumar.Cells[24, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[24, k - 1].Value.ToString()) : 0);
//        //SumaKilos25 = SumaKilos25 + ((xlHojaaSumar.Cells[25, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[25, k - 1].Value.ToString()) : 0);
//        //SumaKilos26 = SumaKilos26 + ((xlHojaaSumar.Cells[26, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[26, k - 1].Value.ToString()) : 0);
//        //SumaKilos27 = SumaKilos27 + ((xlHojaaSumar.Cells[27, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[27, k - 1].Value.ToString()) : 0);
//        //SumaKilos28 = SumaKilos28 + ((xlHojaaSumar.Cells[28, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[28, k - 1].Value.ToString()) : 0);
//        //SumaKilos29 = SumaKilos29 + ((xlHojaaSumar.Cells[29, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[29, k - 1].Value.ToString()) : 0);
//        //SumaKilos30 = SumaKilos30 + ((xlHojaaSumar.Cells[30, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[30, k - 1].Value.ToString()) : 0);
//        //SumaKilos31 = SumaKilos31 + ((xlHojaaSumar.Cells[31, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[31, k - 1].Value.ToString()) : 0);
//        //SumaKilos32 = SumaKilos32 + ((xlHojaaSumar.Cells[32, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[32, k - 1].Value.ToString()) : 0);

//        //SumaKilos35 = SumaKilos35 + ((xlHojaaSumar.Cells[35, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[35, k - 1].Value.ToString()) : 0);
//        //SumaKilos36 = SumaKilos36 + ((xlHojaaSumar.Cells[36, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[36, k - 1].Value.ToString()) : 0);
//        //SumaKilos37 = SumaKilos37 + ((xlHojaaSumar.Cells[37, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[37, k - 1].Value.ToString()) : 0);
//        //SumaKilos38 = SumaKilos38 + ((xlHojaaSumar.Cells[38, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[38, k - 1].Value.ToString()) : 0);
//        //SumaKilos39 = SumaKilos39 + ((xlHojaaSumar.Cells[39, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[39, k - 1].Value.ToString()) : 0);
//        //SumaKilos40 = SumaKilos40 + ((xlHojaaSumar.Cells[40, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[40, k - 1].Value.ToString()) : 0);
//        //SumaKilos41 = SumaKilos41 + ((xlHojaaSumar.Cells[41, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[41, k - 1].Value.ToString()) : 0);
//        //SumaKilos42 = SumaKilos42 + ((xlHojaaSumar.Cells[42, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[42, k - 1].Value.ToString()) : 0);
//        //SumaKilos43 = SumaKilos43 + ((xlHojaaSumar.Cells[43, k - 1].Value != null) ? System.Convert.ToDecimal(xlHojaaSumar.Cells[43, k - 1].Value.ToString()) : 0);



//    }
//}   
//celdaIniDatos[4, columna].Value = SumaConcepto4;
//celdaIniDatos[5, columna].Value = SumaConcepto5;
//celdaIniDatos[6, columna].Value = SumaConcepto6;
//celdaIniDatos[7, columna].Value = SumaConcepto7;
//celdaIniDatos[8, columna].Value = SumaConcepto8;
//celdaIniDatos[9, columna].Value = SumaConcepto9;
//celdaIniDatos[10, columna].Value = SumaConcepto10;
//celdaIniDatos[11, columna].Value = SumaConcepto11;
//celdaIniDatos[12, columna].Value = SumaConcepto12;
//celdaIniDatos[13, columna].Value = SumaConcepto13;
//celdaIniDatos[14, columna].Value = SumaConcepto14;

//celdaIniDatos[16, columna].Value = SumaConcepto16;
//celdaIniDatos[17, columna].Value = SumaConcepto17;
//celdaIniDatos[19, columna].Value = SumaConcepto19;

//celdaIniDatos[21, columna].Value = SumaConcepto21;
//celdaIniDatos[22, columna].Value = SumaConcepto22;
//celdaIniDatos[23, columna].Value = SumaConcepto23;
//celdaIniDatos[24, columna].Value = SumaConcepto24;
//celdaIniDatos[25, columna].Value = SumaConcepto25;
//celdaIniDatos[26, columna].Value = SumaConcepto26;
//celdaIniDatos[27, columna].Value = SumaConcepto27;
//celdaIniDatos[28, columna].Value = SumaConcepto28;
//celdaIniDatos[29, columna].Value = SumaConcepto29;
//celdaIniDatos[30, columna].Value = SumaConcepto30;
//celdaIniDatos[31, columna].Value = SumaConcepto31;
//celdaIniDatos[32, columna].Value = SumaConcepto32;

//celdaIniDatos[35, columna].Value = SumaConcepto35;
//celdaIniDatos[36, columna].Value = SumaConcepto36;
//celdaIniDatos[37, columna].Value = SumaConcepto37;
//celdaIniDatos[38, columna].Value = SumaConcepto38;
//celdaIniDatos[39, columna].Value = SumaConcepto39;
//celdaIniDatos[40, columna].Value = SumaConcepto40;
//celdaIniDatos[41, columna].Value = SumaConcepto41;
//celdaIniDatos[42, columna].Value = SumaConcepto42;
//celdaIniDatos[43, columna].Value = SumaConcepto43;

//celdaIniDatos[4, columna - 1].Value = SumaKilos4;
//celdaIniDatos[5, columna - 1].Value = SumaKilos5;
//celdaIniDatos[6, columna - 1].Value = SumaKilos6;
//celdaIniDatos[7, columna - 1].Value = SumaKilos7;
//celdaIniDatos[8, columna - 1].Value = SumaKilos8;
//celdaIniDatos[9, columna - 1].Value = SumaKilos9;
//celdaIniDatos[10, columna - 1].Value = SumaKilos10;
//celdaIniDatos[11, columna - 1].Value = SumaKilos11;
//celdaIniDatos[12, columna - 1].Value = SumaKilos12;
//celdaIniDatos[13, columna - 1].Value = SumaKilos13;
//celdaIniDatos[14, columna - 1].Value = SumaKilos14;

//celdaIniDatos[16, columna - 1].Value = SumaKilos16;
//celdaIniDatos[17, columna - 1].Value = SumaKilos17;
//celdaIniDatos[19, columna - 1].Value = SumaKilos19;

//celdaIniDatos[21, columna - 1].Value = SumaKilos21;
//celdaIniDatos[22, columna - 1].Value = SumaKilos22;
//celdaIniDatos[23, columna - 1].Value = SumaKilos23;
//celdaIniDatos[24, columna - 1].Value = SumaKilos24;
//celdaIniDatos[25, columna - 1].Value = SumaKilos25;
//celdaIniDatos[26, columna - 1].Value = SumaKilos26;
//celdaIniDatos[27, columna - 1].Value = SumaKilos27;
//celdaIniDatos[28, columna - 1].Value = SumaKilos28;
//celdaIniDatos[29, columna - 1].Value = SumaKilos29;
//celdaIniDatos[30, columna - 1].Value = SumaKilos30;
//celdaIniDatos[31, columna - 1].Value = SumaKilos31;
//celdaIniDatos[32, columna - 1].Value = SumaKilos32;

//celdaIniDatos[35, columna - 1].Value = SumaKilos35;
//celdaIniDatos[36, columna - 1].Value = SumaKilos36;
//celdaIniDatos[37, columna - 1].Value = SumaKilos37;
//celdaIniDatos[38, columna - 1].Value = SumaKilos38;
//celdaIniDatos[39, columna - 1].Value = SumaKilos39;
//celdaIniDatos[40, columna - 1].Value = SumaKilos40;
//celdaIniDatos[41, columna - 1].Value = SumaKilos41;
//celdaIniDatos[42, columna - 1].Value = SumaKilos42;
//celdaIniDatos[43, columna - 1].Value = SumaKilos43;

//ExcelRange celdastotales = xlHoja.Cells["C4:C43"];
//celdastotales.Style.Numberformat.Format = "$###,###,##0.00";
// }    



//excelPackage.Workbook.FullCalcOnLoad 
//excelPackage.Workbook.CalculationProperties.ForceFullCalculation = true;
//spreadSheet.WorkbookPart.Workbook.CalculationProperties.FullCalculationOnLoad = true;

//excelPackage.Worksheet[1].




