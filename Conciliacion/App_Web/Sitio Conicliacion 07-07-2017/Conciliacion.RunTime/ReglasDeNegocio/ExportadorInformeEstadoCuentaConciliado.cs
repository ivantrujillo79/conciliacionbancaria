using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

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

        //private Microsoft.Office.Interop.Excel.Application xlAplicacion;
        //private Microsoft.Office.Interop.Excel.Workbooks xlLibros;
        //private Microsoft.Office.Interop.Excel.Workbook xlLibro;
        //private Microsoft.Office.Interop.Excel.Sheets xlHojas;
        //private Microsoft.Office.Interop.Excel.Worksheet xlHoja;
        //private Microsoft.Office.Interop.Excel.Range xlRango;

        private List<DetalleReporteEstadoCuentaConciliado> _ReporteEstadoCuentaConciliado;
        private string _Ruta;
        private string _Archivo;
        private string _NombreHoja;
        private string _NombreBanco;
        private string _rutaCompleta;
        private bool _Esfinal;
        private string _ClabeInterbancaria;


        #endregion

        public string FechaMesEncabezado { get; set; }

        #region Constructores

        public ExportadorInformeEstadoCuentaConciliado(List<DetalleReporteEstadoCuentaConciliado> ReporteEstadoCuentaConciliado,
                                        string Ruta, string Archivo, string Nombre, string NombreBanco, bool esfinal, string ClabeInterbancaria )
        {
            try
            {
                _ReporteEstadoCuentaConciliado = ReporteEstadoCuentaConciliado;
                _Ruta = Ruta.Trim();
                _Archivo = Archivo.Trim();
                _NombreHoja = Nombre.Trim();
                _NombreBanco = NombreBanco.Trim();
                _Esfinal = esfinal;
                _ClabeInterbancaria = ClabeInterbancaria.Trim();


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
                try
                {
                    ExcelPackage excelPackage = inicializar();
                    // inicializar();
                    if (_ReporteEstadoCuentaConciliado.Count > 0)
                    {
                        crearEncabezado(excelPackage, _NombreHoja, _NombreBanco, _ReporteEstadoCuentaConciliado[0].Fecha.ToString(), _ReporteEstadoCuentaConciliado[0].Clabe.ToString());
                        exportarDatos(excelPackage, _NombreHoja, _NombreBanco, 8, _ReporteEstadoCuentaConciliado);
                        if (true || _Esfinal)
                        {
                            excelPackage.Save();
                        }
                    }
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

        //public void generarInforme(string nombreHoja, int contador, int registrofinal)
        //{
        //    try
        //    {
        //        string rutaCompleta = _Ruta + _Archivo;
               
        //            ExcelPackage excelPackage = inicializar();
        //            crearEncabezado(excelPackage, nombreHoja);
        //            exportarDatos(excelPackage, nombreHoja, 8, _ReporteEstadoCuentaConciliado[i]);
        //            excelPackage.Save(); 
             


        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Ocurrió un error en la creación del reporte: <br/>" + ex.Message);
        //    }
        //    finally
        //    {
        //        GC.Collect();
        //        GC.WaitForPendingFinalizers();
        //    }
        //}


       
            private ExcelPackage inicializar()
        {
            string rutaCompleta = _Ruta + _Archivo;
            ExcelPackage excel = new ExcelPackage(new FileInfo(@rutaCompleta));

            return excel;
        }

        private ExcelPackage crearEncabezado(ExcelPackage excelPackage, string nombreHoja, string NombreBanco,  string fechainicial, string ClabeInterbancaria)
        {
            string banco, cuenta, empresa;
            DateTime fecha;
            CultureInfo cultureInfo = CultureInfo.GetCultureInfo("es-MX");

            ExcelWorksheet wsSheet1 = excelPackage.Workbook.Worksheets.Add(nombreHoja);

            banco = NombreBanco;
            cuenta = "CTA " + nombreHoja;
            empresa = "Corporativo";//_ReporteEstadoCuentaConciliado[0].Corporativo;

            string format = "yyyy-MM-dd HH:mm:ss: tt";
            fecha = DateTime.Parse(FechaMesEncabezado);


            // Cuenta
            using (ExcelRange Rng = wsSheet1.Cells["B1:H1"])
            {
                Rng.Value = banco.ToUpper() +"  "+  cuenta.ToUpper() + " MOVIMIENTOS DEL MES: ";
                Rng.Merge = true;
            }

            // Empresa
            using (ExcelRange Rng = wsSheet1.Cells["B2:H2"])
            {
                Rng.Merge = true;
                Rng.Value = empresa.ToUpper();
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
            using (ExcelRange Rng = wsSheet1.Cells["I1:J1"])
            {
                Rng.Merge = true;
                Rng.Style.Font.Bold = true;
                Rng.Value = fecha.ToString("MMMM DE yyyy").ToUpper();
                Rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                Rng.Style.Fill.BackgroundColor.SetColor(Color.Blue);
                Rng.Style.Font.Color.SetColor(Color.White);
            }

            // Clabe interbancaria
            wsSheet1.Cells["I2"].Value = "CLABE INTERBANCARIA  " + ClabeInterbancaria;

            resalteOscuro("Depósito", wsSheet1.Cells["L1"]);            
            resalteOscuro("Retiro", wsSheet1.Cells["L2"]);            
            resalteOscuro("Saldo final calculado", wsSheet1.Cells["L3"]);            
            resalteOscuro("Saldo final bancario", wsSheet1.Cells["L4"]);            
            resalteOscuro("Depósito conciliado", wsSheet1.Cells["L5"]);            
            resalteOscuro("Retiro conciliado", wsSheet1.Cells["L6"]);
            
            wsSheet1.Protection.IsProtected = false;
            wsSheet1.Protection.AllowSelectLockedCells = false;

            using (ExcelRange Rng = wsSheet1.Cells["B7:P7"])
            {
                estilarEncabezado("Fecha",Rng[7, 2]);
                estilarEncabezado("Referencia", Rng[7, 3]);
                estilarEncabezado("Concepto", Rng["D7:H7"], true);//7, 4]);
                estilarEncabezado("Retiros", Rng["I7"]);
                estilarEncabezado("Depósitos",Rng["J7"]);
                estilarEncabezado("Saldo", Rng["K7"]);
                estilarEncabezado("Concepto conciliado",Rng["L7:O7"],true);
                estilarEncabezado("Documento", Rng["P7"]);

                enmarcarRegion(Rng[7, 2, 7, 16]);
            }

            return excelPackage;
        }

        private void estilarEncabezado(string Texto, ExcelRange Rng)
        {
            Rng.Value = Texto;
            Rng.Style.Font.Size = 10;
            Rng.Style.Font.Color.SetColor(Color.Blue);
            Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }

        private void estilarEncabezado(string Texto, ExcelRange Rng, Boolean Merge)
        {
            Rng.Merge = Merge;
            Rng.Value = Texto;
            Rng.Style.Font.Size = 10;
            Rng.Style.Font.Color.SetColor(Color.Blue);
            Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }

        private void resalteOscuro(string Texto, ExcelRange Rng)
        {
            Rng.Value = Texto;
            Rng.Style.Font.Size = 11;
            Rng.Style.Font.Bold = true;
            Rng.Style.Font.Italic = true;
        }

        private void enmarcarRegion(ExcelRange Rng)
        {
            Rng.Style.Border.Top.Style = ExcelBorderStyle.Medium;
            Rng.Style.Border.Top.Color.SetColor(Color.Black);
            Rng.Style.Border.Left.Style = ExcelBorderStyle.Medium;
            Rng.Style.Border.Left.Color.SetColor(Color.Black);
            Rng.Style.Border.Right.Style = ExcelBorderStyle.Medium;
            Rng.Style.Border.Right.Color.SetColor(Color.Black);
            Rng.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            Rng.Style.Border.Bottom.Color.SetColor(Color.Black);
        }

        private void exportarDatos(ExcelPackage excelPackage, string nombreHoja, string NombreBanco, int aPartirDeLaFila, List<DetalleReporteEstadoCuentaConciliado> _DetalleAExportar)
        {
            int i = aPartirDeLaFila;

            ExcelWorksheet wsSheet1 = excelPackage.Workbook.Worksheets[nombreHoja];

            decimal retiros = 0;
            decimal depositos = 0;
            //wsSheet1.Cells[7, 18].Value = "Depositos";
            //wsSheet1.Cells[7, 19].Value = "Retiros";
            foreach (DetalleReporteEstadoCuentaConciliado detalle in _DetalleAExportar)
            {
                wsSheet1.Cells[i, 4, i, 8].Merge = true;
                wsSheet1.Cells[i, 12, i, 15].Merge = true;
                wsSheet1.Cells[i,1].Value = detalle.ConsecutivoFlujo.ToString();
                wsSheet1.Cells[i, 2].Value = detalle.Fecha.ToString("dd/MM/yyyy");
                wsSheet1.Cells[i, 3].Value = detalle.Referencia;
                wsSheet1.Cells[i, 4].Value = detalle.Concepto;
                wsSheet1.Cells[i, 9].Value = detalle.Retiros;
                wsSheet1.Cells[i, 10].Value = detalle.Depositos;
                wsSheet1.Cells[i, 11].Value = detalle.SaldoFinal;
                wsSheet1.Cells[i, 12].Value = detalle.ConceptoConciliado;
                //wsSheet1.Cells[i, 16].Value = detalle.DocumentoConciliado;
                //wsSheet1.Cells[i, 17].Value = detalle.StatusConciliacion;

                if (detalle.StatusConciliacion == "CONCILIADA")
                {
                    retiros += detalle.Retiros;
                    depositos += detalle.Depositos;
                    //wsSheet1.Cells[i, 18].Value = detalle.Depositos;
                    //wsSheet1.Cells[i, 19].Value = detalle.Retiros;
                }
                i++;
            }
            
            //Get the final row for the column in the worksheet
            int finalrows = wsSheet1.Dimension.End.Row;

            //Para los depósitos
            string ColumnString = "J8:J" + finalrows.ToString();
            wsSheet1.Cells["M1"].Formula = "SUM(" + ColumnString + ")";
            wsSheet1.Cells["M1"].Calculate();

            //Para los Retiros
            string ColumnStringRet = "I8:I" + finalrows.ToString();
            wsSheet1.Cells["M2"].Formula = "SUM(" + ColumnStringRet + ")";
            wsSheet1.Cells["M2"].Calculate();

            //Se suman i y j del primer item, y se restan con K
            //Para el Saldo Final calculado 
            wsSheet1.Cells["M3"].Formula = ("(K8 + I8 - J8) + M1 - M2"); //"SUM(K8)-SUM(I8,J8)";
            wsSheet1.Cells["M3"].Calculate();

            //Para el Saldo final bancario
            string ColumnStringSaldofinalbancario= "K" + finalrows.ToString();
            wsSheet1.Cells["M4"].Value = wsSheet1.Cells[ColumnStringSaldofinalbancario].Value.ToString();
            wsSheet1.Cells["M4"].Formula = "SUM(" + ColumnStringSaldofinalbancario + ")";
            wsSheet1.Cells["M4"].Calculate();

            //---Depositos
            wsSheet1.Cells["M5"].Value = depositos;
            // ---Retiros
            wsSheet1.Cells["M6"].Value = retiros;

            string ColumnStringDep = "J8:J" + finalrows.ToString();
           // --- Saldos
            string ColumnStringSal = "K8:K" + finalrows.ToString();


            ExcelRange celdastotales = wsSheet1.Cells[ColumnStringRet];
            celdastotales.Style.Numberformat.Format = "$###,###,##0.00";

            ExcelRange celdastotalesdep = wsSheet1.Cells[ColumnStringDep];
            celdastotalesdep.Style.Numberformat.Format = "$###,###,##0.00";

            ExcelRange celdastotalessal = wsSheet1.Cells[ColumnStringSal];
            celdastotalessal.Style.Numberformat.Format = "$###,###,##0.00";

            ExcelRange celdastot= wsSheet1.Cells["M1:M6"];
            celdastot.Style.Numberformat.Format = "$###,###,##0.00";


            wsSheet1.Calculate();

        }

        //private void cerrar()
        //{
        //    string rutaCompleta = _Ruta + _Archivo;

        //    //if (File.Exists(rutaCompleta)) File.Delete(rutaCompleta);

        //   // xlLibro.SaveAs(rutaCompleta, Excel.XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing,//
        //                    //false, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing,//
        //                    //Type.Missing, Type.Missing, Type.Missing, Type.Missing);//

        //    xlLibro.Close(0);
        //    xlLibros.Close();
        //    xlAplicacion.Quit();
        //}

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
