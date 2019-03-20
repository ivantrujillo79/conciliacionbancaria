using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using System.Diagnostics;
using System.Drawing;
using OfficeOpenXml.Style;
using DetalleReporteEstadoCuenta = Conciliacion.RunTime.DatosSQL.InformeBancarioDatos.DetalleReporteEstadoCuenta;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public class ExportadorInformeEstadoCuenta
    {

        #region Miembros privados



        private List<DetalleReporteEstadoCuenta> _ReporteEstadoCuenta;
        private string _Ruta;
        private string _Archivo;
        private string _NombreHoja;

        #endregion

        #region Constructores

        public ExportadorInformeEstadoCuenta(List<DetalleReporteEstadoCuenta> ReporteEstadoCuenta,
                                       string Ruta, string Archivo, string Nombre)
        {
            try
            {
                _ReporteEstadoCuenta = ReporteEstadoCuenta;
                _Ruta = Ruta.Trim();
                _Archivo = Archivo.Trim();
                _NombreHoja = Nombre.Trim();

                //  ValidarMiembros();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error en la creación del reporte: <br/>" + ex.Message);
            }
        }

        #endregion

        public void generarInforme(string Cuenta)
        {
            try
            {
                try
                {
                    ExcelPackage excelPackage = inicializar();
                   
                        inicializar();
                        crearEncabezado(excelPackage, Cuenta);
                        exportarDatos(excelPackage, Cuenta, 5, _ReporteEstadoCuenta);                                 

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


        private ExcelPackage crearEncabezado(ExcelPackage excelPackage, string nombrehoja)
        {
            DateTime fecha;
            CultureInfo cultureInfo = CultureInfo.GetCultureInfo("es-MX");
            fecha = DateTime.Now;//_ReporteEstadoCuentaConciliado[0].Fecha;
            //// banco = _DetalleReporteEstadoCuenta[0].
            //  banco = "BANAMEX ";
            // cuenta = "CTA " + ListaEncabezado[0].CuentaBancoFinanciero + " ";
            // empresa = ListaEncabezado[0].Corporativo;
            // fecha = DateTime.Parse(ListaEncabezado[0].Fecha);
            // retiro = ListaEncabezado[0].Retiro;
            // saldofinal = ListaEncabezado[0].SaldoFinal;
            // depositos = ListaEncabezado[0].Depositos;

            ExcelWorksheet wsSheet1 = excelPackage.Workbook.Worksheets.Add(nombrehoja.Trim());

            // Cuenta
            using (ExcelRange Rng = wsSheet1.Cells["B1:H1"])
            {
                Rng.Value = _NombreHoja + " MOVIMIENTOS DEL MES DE: " + fecha.ToString("MMMM", cultureInfo).ToUpper(); ;
                Rng.Merge = true;
            }

            // Empresa
            using (ExcelRange Rng = wsSheet1.Cells["B2:H2"])
            {
                Rng.Merge = true;
                Rng.Value = "";// empresa.ToUpper();
            }

            // Mes
            using (ExcelRange Rng = wsSheet1.Cells["D3:K3"])
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
            using (ExcelRange Rng = wsSheet1.Cells["I1:K1"])
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


            wsSheet1.Protection.IsProtected = false;
            wsSheet1.Protection.AllowSelectLockedCells = false;

            using (ExcelRange Rng = wsSheet1.Cells["B4:K4"])
            {
                estilarEncabezado("Fecha", Rng["B4"]);
                estilarEncabezado("Referencia", Rng["C4"]);
                estilarEncabezado("Concepto", Rng["D4:H4"], true);//7, 4]);
                estilarEncabezado("Retiros", Rng["I4"]);
                estilarEncabezado("Depósitos", Rng["J4"]);
                estilarEncabezado("Saldo", Rng["K4"]);

                enmarcarRegion(Rng["B4:K4"]);
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


        public List<List<DetalleReporteEstadoCuenta>> Separat(List<DetalleReporteEstadoCuenta> source)
        {
            return source
                .GroupBy(s => s.CuentaBancoFinanciero)
                .OrderBy(g => g.Key)
                .Select(g => g.ToList())
                .ToList();
        }

        private void exportarDatos(ExcelPackage excelPackage, string nombreHoja, int aPartirDeLaFila, List<DetalleReporteEstadoCuenta> _ReporteEstadoCuenta)
        {
            int i = aPartirDeLaFila;

            ExcelWorksheet wsSheet1 = excelPackage.Workbook.Worksheets[nombreHoja.Trim()];

            foreach (DetalleReporteEstadoCuenta detalle in _ReporteEstadoCuenta)
            {
                wsSheet1.Cells[i, 4, i, 8].Merge = true;
                wsSheet1.Cells[i, 2].Value = detalle.FOperacion.ToShortDateString();
                wsSheet1.Cells[i, 3].Value = detalle.Referencia;
                wsSheet1.Cells[i, 4].Value = detalle.Concepto;
                wsSheet1.Cells[i, 9].Value = detalle.Retiros.ToString();
                wsSheet1.Cells[i, 10].Value = detalle.Depositos.ToString();
                wsSheet1.Cells[i, 11].Value = detalle.SaldoFinal.ToString();

                i++;
            }

        }


        private bool ValidarMiembros()
        {
            StringBuilder mensajeExcepcion = new StringBuilder();
            bool valido = false;

            if (_ReporteEstadoCuenta == null)
            {
                mensajeExcepcion.Append("No se encontraron datos con los parámetros seleccionados. <br/>");
            }
            else if (_ReporteEstadoCuenta.Count == 0)
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
