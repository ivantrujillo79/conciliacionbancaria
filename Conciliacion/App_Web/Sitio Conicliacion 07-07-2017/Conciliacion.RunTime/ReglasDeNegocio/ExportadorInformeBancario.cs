using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using DetallePosicionDiariaBancos = Conciliacion.RunTime.DatosSQL.InformeBancarioDatos.DetallePosicionDiariaBancos;
using OfficeOpenXml;
using System.Drawing;


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

        private const string CONCEPTO24 = "CHEQUES BANAMEX 0671084374";
        private const string CONCEPTO25 = "CHEQUES BANCOMER 0671084374";
        private const string CONCEPTO26 = "CHEQUES BANORTE 0671084374";
        private const string CONCEPTO27 = "CHEQUES HSBC 0671084374";
        private const string CONCEPTO28 = "CHEQUES OTROS 0671084374";
        private const string CONCEPTO29 = "EFECTIVO 0671084374";
        private const string CONCEPTO30 = "EFECTIVO COBRANZA 0671084374";
        private const string CONCEPTO31 = "EFECTIVO LIQUIDACION 0671084374";
        private const string CONCEPTO32 = "TARJETA BANORTE 0671084374";

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

        private decimal SumaConceptoImporte1 = 0;
        private decimal SumaConceptoImporte2 = 0;
        private decimal SumaConceptoImporte3 = 0;
        private decimal SumaConceptoImporte4 = 0;
        private decimal SumaConceptoImporte5 = 0;
        private decimal SumaConceptoImporte6 = 0;
        private decimal SumaConceptoImporte7 = 0;
        private decimal SumaConceptoImporte8 = 0;
        private decimal SumaConceptoImporte9 = 0;
        private decimal SumaConceptoImporte10 = 0;
        private decimal SumaConceptoImporte11 = 0;
        private decimal SumaConceptoImporte12 = 0;
        private decimal SumaConceptoImporte13 = 0;
        private decimal SumaConceptoImporte14 = 0;
        private decimal SumaConceptoImporte15 = 0;
        private decimal SumaConceptoImporte16 = 0;
        private decimal SumaConceptoImporte17 = 0;
        private decimal SumaConceptoImporte18 = 0;
        private decimal SumaConceptoImporte19 = 0;
        private decimal SumaConceptoImporte20 = 0;
        private decimal SumaConceptoImporte21 = 0;
        private decimal SumaConceptoImporte22 = 0;
        private decimal SumaConceptoImporte23 = 0;
        private decimal SumaConceptoImporte24 = 0;
        private decimal SumaConceptoImporte25 = 0;
        private decimal SumaConceptoImporte26 = 0;
        private decimal SumaConceptoImporte27 = 0;

        private decimal SumaConceptoImporte28 = 0;
        private decimal SumaConceptoImporte29 = 0;
        private decimal SumaConceptoImporte30 = 0;
        private decimal SumaConceptoImporte31 = 0;
        private decimal SumaConceptoImporte32 = 0;

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

        public void generarTotales()
        {
          

        }

        public void generarPosicionDiariaBancos(int Esfinal )
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
                if (Esfinal ==0 )
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

               
            string caja = Convert.ToString(_DetallePosicionDiariaBancos
                                            .First(x => !x.Concepto.ToUpper().Contains("TOTAL"))
                                            .Caja);
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
            xlRango.Value = "CAJA " + nombrehoja;
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

                    dia = fecha.ToString("dddd", cultureInfo).ToUpper();

                    // Día
                    celdaDiaInicial = xlHoja.Cells[1, celda];
                    celdaDiaFinal = xlHoja.Cells[1, celda + 1];

                    var range = xlHoja.Cells[1, celda+1, 1, celda+2];//Address "b1:c1                    
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
                    celdaFecha.Value = fecha.ToString("d-MMM-yyyy", cultureInfo);
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

      

        private void exportarPosicionDiariaBancos(ExcelPackage excelPackage, String descripcioncaja )
        {
            string concepto;
            int columna = 1;

            
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

            foreach (DetallePosicionDiariaBancos item in _DetallePosicionDiariaBancos)
            {
                if (item.Fecha != _FechaAOmitir)
                {
                concepto = RemoverAcentos(item.Concepto.ToUpper().Trim());
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



                    switch (concepto)
                    {
                        case CONCEPTO1:
                            celdaIniDatos[4, columna].Value = item.Kilos;
                            celdaIniDatos.Style.Numberformat.Format = "###,###,##0.00";
                            celdaIniDatos[4, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            SumaConcepto1 = SumaConcepto1 + item.Kilos;
                            SumaConceptoImporte1 = SumaConceptoImporte1 + item.Importe;

                            if (descripcioncaja =="TOTAL")
                            {
                                celdaIniDatos[4, columna].Value = SumaConcepto1;
                                celdaIniDatos[4, columna + 1].Value = SumaConceptoImporte1;
                            }
                            

                            break;
                        case CONCEPTO2:
                            celdaIniDatos[5, columna].Value = item.Kilos;
                            celdaIniDatos.Style.Numberformat.Format = "###,###,##0.00";
                            celdaIniDatos[5, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            SumaConcepto2 = SumaConcepto2 + item.Kilos;
                            SumaConceptoImporte2 = SumaConceptoImporte2 + item.Importe;

                            if (descripcioncaja == "TOTAL")
                            {
                                celdaIniDatos[5, columna].Value = SumaConcepto2;
                                celdaIniDatos[5, columna + 1].Value = SumaConceptoImporte2;
                            }


                            break;
                        case CONCEPTO3:
                            celdaIniDatos[6, columna].Value = item.Kilos;
                            celdaIniDatos.Style.Numberformat.Format = "###,###,##0.00";
                            celdaIniDatos[6, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            SumaConcepto3 = SumaConcepto3 + item.Kilos;
                            SumaConceptoImporte3 = SumaConceptoImporte3 + item.Importe;

                            if (descripcioncaja == "TOTAL")
                            {
                                celdaIniDatos[6, columna].Value = SumaConcepto3;
                                celdaIniDatos[6, columna + 1].Value = SumaConceptoImporte3;
                            }
                            break;
                        case CONCEPTO4:
                            celdaIniDatos[7, columna].Value = item.Kilos;
                            celdaIniDatos.Style.Numberformat.Format = "###,###,##0.00";
                            celdaIniDatos[7, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            SumaConcepto4 = SumaConcepto4 + item.Kilos;
                            SumaConceptoImporte4 = SumaConceptoImporte4 + item.Importe;

                            if (descripcioncaja == "TOTAL")
                            {
                                celdaIniDatos[7, columna].Value = SumaConcepto4;
                                celdaIniDatos[7, columna + 1].Value = SumaConceptoImporte4;
                            }

                            break;
                        case CONCEPTO5:
                            celdaIniDatos[8, columna].Value = item.Kilos;
                            celdaIniDatos.Style.Numberformat.Format = "###,###,##0.00";
                            celdaIniDatos[8, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            SumaConcepto5 = SumaConcepto5 + item.Kilos;
                            SumaConceptoImporte5= SumaConceptoImporte5 + item.Importe;
                            if (descripcioncaja == "TOTAL")
                            {
                                celdaIniDatos[8, columna].Value = SumaConcepto5;
                                celdaIniDatos[8, columna + 1].Value = SumaConceptoImporte5;
                            }

                            break;
                        case CONCEPTO6:
                            celdaIniDatos[9, columna].Value = item.Kilos;
                            celdaIniDatos.Style.Numberformat.Format = "###,###,##0.00";
                            celdaIniDatos[9, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            SumaConcepto6 = SumaConcepto6 + item.Kilos;
                            SumaConceptoImporte6 = SumaConceptoImporte6 + item.Importe;
                            if (descripcioncaja == "TOTAL")
                            {
                                celdaIniDatos[9, columna].Value = SumaConcepto6;
                                celdaIniDatos[9, columna + 1].Value = SumaConceptoImporte6;
                            }
                            break;
                        case CONCEPTO7:
                            celdaIniDatos[10, columna].Value = item.Kilos;
                            celdaIniDatos.Style.Numberformat.Format = "###,###,##0.00";
                            celdaIniDatos[10, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            SumaConcepto7 = SumaConcepto7 + item.Kilos;
                            SumaConceptoImporte7 = SumaConceptoImporte7 + item.Importe;
                            if (descripcioncaja == "TOTAL")
                            {
                                celdaIniDatos[10, columna].Value = SumaConcepto7;
                                celdaIniDatos[10, columna + 1].Value = SumaConceptoImporte7;
                            }
                            break;
                        case CONCEPTO8:
                            celdaIniDatos[11, columna].Value = item.Kilos;
                            celdaIniDatos.Style.Numberformat.Format = "###,###,##0.00";
                            celdaIniDatos[11, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            SumaConcepto8 = SumaConcepto8 + item.Kilos;
                            SumaConceptoImporte8 = SumaConceptoImporte8 + item.Importe;
                            if (descripcioncaja == "TOTAL")
                            {
                                celdaIniDatos[11, columna].Value = SumaConcepto8;
                                celdaIniDatos[11, columna + 1].Value = SumaConceptoImporte8;
                            }
                            break;
                        case CONCEPTO9:
                            celdaIniDatos[12, columna].Value = item.Kilos;
                            celdaIniDatos.Style.Numberformat.Format = "###,###,##0.00";
                            celdaIniDatos[12, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            SumaConcepto9 = SumaConcepto9 + item.Kilos;
                            SumaConceptoImporte9 = SumaConceptoImporte9 + item.Importe;

                            if (descripcioncaja == "TOTAL")
                            {
                                celdaIniDatos[12, columna].Value = SumaConcepto9;
                                celdaIniDatos[12, columna + 1].Value = SumaConceptoImporte9;
                            }

                            break;
                        case CONCEPTO10:
                            celdaIniDatos[13, columna].Value = item.Kilos;
                            celdaIniDatos.Style.Numberformat.Format = "###,###,##0.00";
                            celdaIniDatos[13, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            SumaConcepto10 = SumaConcepto10 + item.Kilos;
                            SumaConceptoImporte10 = SumaConceptoImporte10 + item.Importe;
                            break;
                        case CONCEPTO11:
                            celdaIniDatos[14, columna].Value = item.Kilos;
                            celdaIniDatos.Style.Numberformat.Format = "###,###,##0.00";
                            celdaIniDatos[14, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            SumaConcepto11 = SumaConcepto11 + item.Kilos;
                            SumaConceptoImporte11 = SumaConceptoImporte11 + item.Importe;
                            break;                 

                        


                        case CONCEPTO12:
                            celdaIniDatos[21, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            SumaConcepto12 = SumaConcepto11 + item.Kilos;
                            SumaConceptoImporte12 = SumaConceptoImporte12 + item.Importe;
                            break;
                        case CONCEPTO13:
                            celdaIniDatos[22, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            SumaConcepto13 = SumaConcepto13 + item.Kilos;
                            SumaConceptoImporte13 = SumaConceptoImporte13 + item.Importe;
                            break;
                        case CONCEPTO14:
                            celdaIniDatos[23, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            SumaConcepto14= SumaConcepto14 + item.Kilos;
                            SumaConceptoImporte14 = SumaConceptoImporte14 + item.Importe;
                            break;

                        case CONCEPTO15:
                            celdaIniDatos[24, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            SumaConcepto15 = SumaConcepto15 + item.Kilos;
                            SumaConceptoImporte15 = SumaConceptoImporte15 + item.Importe;
                            break;

                        case CONCEPTO16:
                            celdaIniDatos[25, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            SumaConcepto16 = SumaConcepto16 + item.Kilos;
                            SumaConceptoImporte16 = SumaConceptoImporte16 + item.Importe;
                            break;

                        case CONCEPTO17:
                            celdaIniDatos[26, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            SumaConcepto17 = SumaConcepto17 + item.Kilos;
                            SumaConceptoImporte17= SumaConceptoImporte17 + item.Importe;
                            break;

                        case CONCEPTO18:
                            celdaIniDatos[27, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            SumaConcepto18 = SumaConcepto18 + item.Kilos;
                            SumaConceptoImporte18 = SumaConceptoImporte18 + item.Importe;
                            break;

                        case CONCEPTO19:
                            celdaIniDatos[28, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            SumaConcepto19 = SumaConcepto19 + item.Kilos;
                            SumaConceptoImporte19 = SumaConceptoImporte19 + item.Importe;
                            break;

                        case CONCEPTO20:
                            celdaIniDatos[29, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            SumaConcepto20 = SumaConcepto20 + item.Kilos;
                            SumaConceptoImporte20 = SumaConceptoImporte20 + item.Importe;
                            break;

                        case CONCEPTO21:
                            celdaIniDatos[30, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            SumaConcepto21 = SumaConcepto21 + item.Kilos;
                            SumaConceptoImporte21 = SumaConceptoImporte21 + item.Importe;
                            break;

                        case CONCEPTO22:
                            celdaIniDatos[31, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            SumaConcepto22 = SumaConcepto22 + item.Kilos;
                            SumaConceptoImporte22 = SumaConceptoImporte22 + item.Importe;
                            break;

                        case CONCEPTO23:
                            celdaIniDatos[32, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            SumaConcepto23 = SumaConcepto23 + item.Kilos;
                            SumaConceptoImporte23 = SumaConceptoImporte23 + item.Importe;
                            break;

                        case CONCEPTO24:
                            celdaIniDatos[35, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            celdaIniDatos.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            celdaIniDatos.Style.Fill.BackgroundColor.SetColor(Color.Turquoise);
                            SumaConcepto24 = SumaConcepto24 + item.Kilos;
                            SumaConceptoImporte24 = SumaConceptoImporte24 + item.Importe;
                            break;

                        case CONCEPTO25:
                            celdaIniDatos[36, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            celdaIniDatos.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            celdaIniDatos.Style.Fill.BackgroundColor.SetColor(Color.Lime);
                            SumaConcepto25 = SumaConcepto25 + item.Kilos;
                            SumaConceptoImporte25 = SumaConceptoImporte25 + item.Importe;
                            break;

                        case CONCEPTO26:
                            celdaIniDatos[37, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            celdaIniDatos.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            celdaIniDatos.Style.Fill.BackgroundColor.SetColor(Color.Thistle);
                            SumaConcepto26 = SumaConcepto26 + item.Kilos;
                            SumaConceptoImporte26 = SumaConceptoImporte26 + item.Importe;
                            break;


                        case CONCEPTO27:
                            celdaIniDatos[38, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            celdaIniDatos.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            celdaIniDatos.Style.Fill.BackgroundColor.SetColor(Color.BlanchedAlmond);
                            SumaConcepto27 = SumaConcepto27 + item.Kilos;
                            SumaConceptoImporte27 = SumaConceptoImporte27 + item.Importe;
                            break;


                        case CONCEPTO28:
                            celdaIniDatos[39, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            celdaIniDatos.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            celdaIniDatos.Style.Fill.BackgroundColor.SetColor(Color.DodgerBlue);
                            SumaConcepto28 = SumaConcepto28 + item.Kilos;
                            SumaConceptoImporte28 = SumaConceptoImporte28 + item.Importe;
                            break;


                        case CONCEPTO29:
                            celdaIniDatos[40, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            celdaIniDatos.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            celdaIniDatos.Style.Fill.BackgroundColor.SetColor(Color.DarkOrchid);
                            SumaConcepto29 = SumaConcepto29 + item.Kilos;
                            SumaConceptoImporte29 = SumaConceptoImporte29 + item.Importe;
                            break;


                        case CONCEPTO30:
                            celdaIniDatos[41, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            celdaIniDatos.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            celdaIniDatos.Style.Fill.BackgroundColor.SetColor(Color.Salmon);
                            SumaConcepto30 = SumaConcepto30 + item.Kilos;
                            SumaConceptoImporte30 = SumaConceptoImporte30 + item.Importe;
                            break;

                        case CONCEPTO31:
                            celdaIniDatos[42, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            celdaIniDatos.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            celdaIniDatos.Style.Fill.BackgroundColor.SetColor(Color.PaleVioletRed);
                            SumaConcepto31 = SumaConcepto31 + item.Kilos;
                            SumaConceptoImporte31 = SumaConceptoImporte31 + item.Importe;
                            break;

                        case CONCEPTO32:
                            celdaIniDatos[43, columna + 1].Value = item.Importe;
                            celdaIniDatos.Style.Numberformat.Format = "$###,###,##0.00";
                            celdaIniDatos.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            celdaIniDatos.Style.Fill.BackgroundColor.SetColor(Color.PaleGreen);
                            SumaConcepto32 = SumaConcepto32 + item.Kilos;
                            SumaConceptoImporte32 = SumaConceptoImporte32 + item.Importe;
                            break;

                            default:
                           
                            break;

                    }

                    CalculaSumatoria(columna + 1, excelPackage);

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

            celdaDiaIniInf[35, 1].Value = "CHEQUES BANAMEX 0671084374";
            celdaDiaIniInf[36, 1].Value = "CHEQUES BANCOMER 0671084374";
            celdaDiaIniInf[37, 1].Value = "CHEQUES BANORTE 0671084374";
            celdaDiaIniInf[38, 1].Value = "CHEQUES HSBC 0671084374";
            celdaDiaIniInf[39, 1].Value = "CHEQUES OTROS 0671084374";
            celdaDiaIniInf[40, 1].Value = "EFECTIVO 0671084374";
            celdaDiaIniInf[41, 1].Value = "EFECTIVO COBRANZA 0671084374";
            celdaDiaIniInf[42, 1].Value = "EFECTIVO LIQUIDACIÓN 0671084374";
            celdaDiaIniInf[43, 1].Value = "TARJETA BANORTE 0671084374";

           //var sheet = excelPackage.Workbook.Worksheets.Add("Formula");

            
            xlHoja.Calculate();

            xlHoja.Workbook.FullCalcOnLoad  = true;
            //xlHoja.Workbook.CalcMode = 
            //xlHoja.WorkbookPart.Workbook.CalculationProperties.FullCalculationOnLoad = true;


            for (int i = 2; i <= columna + 4; i++)
            {
                xlHoja.Column(i).Width = 18;
            }
           
           

        }

        private void CalculaSumatoria(int columna, ExcelPackage excelPackage)
        {
            var xlHoja = excelPackage.Workbook.Worksheets[excelPackage.Workbook.Worksheets.Count()];
            var range = xlHoja.Cells[columna, 4 , columna, 14];//Address "b1:c1       
            xlHoja.Cells[16,columna].Formula = "=SUM(" + xlHoja.Cells[4, columna].Address + ":" + xlHoja.Cells[14, columna].Address + ")";
            xlHoja.Cells[16,columna].Style.Numberformat.Format = "$###,###,##0.00";
            xlHoja.Column(1).Width = 35;

            //xlHoja.Cells["F22"].Formula = "SUM(G3:G17)";


            xlHoja.Cells[17, columna].Formula = "=SUM(" + xlHoja.Cells[8,columna].Address + ":" + xlHoja.Cells[11, columna].Address + ")";
            xlHoja.Cells[17, columna].Style.Numberformat.Format = "$###,###,##0.00";
            xlHoja.Cells[17, columna].Calculate();


            xlHoja.Cells[19,columna].Formula = "SUM(columna,16: columna ,16)-SUM(columna,17:columna,17)";
            xlHoja.Cells[19, columna].Formula = "=SUM(" + xlHoja.Cells[16, columna].Address + ":" + xlHoja.Cells[16,columna].Address + ")-SUM(" + xlHoja.Cells[17,columna].Address + ":" + xlHoja.Cells[17,columna].Address + ")";
            xlHoja.Cells[19, columna].Style.Numberformat.Format = "$###,###,##0.00";
            xlHoja.Cells[19, columna].Calculate();

            xlHoja.Cells[30, columna].Formula = "=SUM(" + xlHoja.Cells[21, columna].Address + ":" + xlHoja.Cells[29, columna].Address + ")";
            xlHoja.Cells[30, columna].Style.Numberformat.Format = "$###,###,##0.00";
            xlHoja.Cells[30, columna].Calculate();


            xlHoja.Cells[31, columna].Formula = "=SUM(" + xlHoja.Cells[25, columna].Address + ":" + xlHoja.Cells[27, columna].Address + ")";
            xlHoja.Cells[31, columna].Style.Numberformat.Format = "$###,###,##0.00";
            xlHoja.Cells[31, columna].Calculate();


            xlHoja.Cells[32, columna].Formula = "=SUM(" + xlHoja.Cells[30, columna].Address + ":" + xlHoja.Cells[30, columna].Address + ")-SUM(" + xlHoja.Cells[31, columna].Address + ":" + xlHoja.Cells[31, columna].Address + ")";
            xlHoja.Cells[32, columna].Style.Numberformat.Format = "$###,###,##0.00";
            xlHoja.Cells[32, columna].Calculate();

            xlHoja.Cells[34, columna].Formula = "=SUM(" + xlHoja.Cells[19, columna].Address + ":" + xlHoja.Cells[19, columna].Address + ")-SUM(" + xlHoja.Cells[32, columna].Address + ":" + xlHoja.Cells[32, columna].Address + ")";
            xlHoja.Cells[34, columna].Style.Numberformat.Format = "$###,###,##0.00";
            xlHoja.Cells[34, columna].Calculate();

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
