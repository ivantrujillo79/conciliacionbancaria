using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
//using Excel = Microsoft.Office.Interop.Excel;
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
                crearEncabezado(excelPackage, "NombreHoja");
                exportarDatos(excelPackage, "NombreHoja", 8);
                /*cerrar();*/
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

        private ExcelPackage inicializar()
        {
            string rutaCompleta = _Ruta + _Archivo;
            ExcelPackage excel = new ExcelPackage(new FileInfo(@rutaCompleta));

            return excel;
        }

        private ExcelPackage crearEncabezado(ExcelPackage excelPackage, string nombreHoja)
        {
            string banco, cuenta, empresa;
            DateTime fecha;
            CultureInfo cultureInfo = CultureInfo.GetCultureInfo("es-MX");

            ExcelWorksheet wsSheet1 = excelPackage.Workbook.Worksheets.Add(nombreHoja);

            banco = "BANAMEX ";
            cuenta = "CTA ";// + _ReporteEstadoCuentaConciliado[0].CuentaBancoFinanciero + " ";
            empresa = "Corporativo";//_ReporteEstadoCuentaConciliado[0].Corporativo;
            fecha = DateTime.Now;//_ReporteEstadoCuentaConciliado[0].Fecha;

            // Cuenta
            using (ExcelRange Rng = wsSheet1.Cells["B1:H1"])
            {
                Rng.Value = banco.ToUpper() + cuenta.ToUpper() + " MOVIMIENTOS DEL MES DE: ";
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
            wsSheet1.Cells["I2"].Value = "CLABE INTERBANCARIA";

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

        private void exportarDatos(ExcelPackage excelPackage, string nombreHoja, int aPartirDeLaFila)
        {
            int i = aPartirDeLaFila;

            ExcelWorksheet wsSheet1 = excelPackage.Workbook.Worksheets[nombreHoja];

            foreach (DetalleReporteEstadoCuentaConciliado detalle in _ReporteEstadoCuentaConciliado)
            {
                wsSheet1.Cells[i, 4, i, 8].Merge = true;
                wsSheet1.Cells[i, 12, i, 15].Merge = true;
                wsSheet1.Cells[i,1].Value = detalle.ConsecutivoFlujo.ToString();
                wsSheet1.Cells[i, 2].Value = detalle.Fecha.ToString("dd/MM/yyyy");
                wsSheet1.Cells[i, 3].Value = detalle.Referencia;
                wsSheet1.Cells[i, 4].Value = detalle.Concepto;
                wsSheet1.Cells[i, 9].Value = detalle.Retiros.ToString("C");
                wsSheet1.Cells[i, 10].Value = detalle.Depositos.ToString("C");
                wsSheet1.Cells[i, 11].Value = detalle.SaldoFinal.ToString("C");
                wsSheet1.Cells[i, 12].Value = detalle.ConceptoConciliado;
                wsSheet1.Cells[i, 16].Value = detalle.DocumentoConciliado;
                i++;
            }
            
        }

        private void cerrar()
        {
            string rutaCompleta = _Ruta + _Archivo;

            //if (File.Exists(rutaCompleta)) File.Delete(rutaCompleta);

            /*xlLibro.SaveAs(rutaCompleta, Excel.XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing,
                            false, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing);*/

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
