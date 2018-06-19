using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using DetalleReporteEstadoCuentaConciliado = Conciliacion.RunTime.DatosSQL.InformeBancarioDatos.DetalleReporteEstadoCuentaConciliado;
using DetalleInformeInternosAFuturo = Conciliacion.RunTime.DatosSQL.ExportadorInformeInternosAFuturoDatos.DetalleInformeInternosAFuturo;
using System.IO;
using System.Globalization;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using System.Diagnostics;
using System.Drawing;
using OfficeOpenXml.Style;

namespace Conciliacion.RunTime
{
    public class ExportadorInformeInternosAFuturo
    {
        #region Miembros privados


        private List<List<DetalleInformeInternosAFuturo>> _InformeInternosAFuturo;
        private string _Ruta;
        private string _Archivo;
        private string _NombreHoja;
        private string _Banco;
        private string _FechaIni;
        private string _FechaFin;
        private string _Empresa;

        #endregion

        #region Constructores

        public ExportadorInformeInternosAFuturo(List<List<DetalleInformeInternosAFuturo>> InformeInternosAFuturo,
                                        string Ruta, string Archivo, string Nombre, string Banco, string FechaIni, string FechaFin, string Empresa)
        {
            try
            {
                _InformeInternosAFuturo = InformeInternosAFuturo;
                _Ruta = Ruta.Trim();
                _Archivo = Archivo.Trim();
                _NombreHoja = Nombre.Trim();
                _Banco = Banco.Trim();
                _FechaIni = FechaIni.Trim();
                _FechaFin = FechaFin.Trim();
                _Empresa = Empresa.Trim();

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
                crearEncabezado(excelPackage, _NombreHoja, _Banco);
                exportarDatos(excelPackage, _NombreHoja, 8);
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

        private ExcelPackage crearEncabezado(ExcelPackage excelPackage, string nombreHoja, string Banco)
        {
            //string banco, cuenta, empresa;
            //DateTime fecha;
            CultureInfo cultureInfo = CultureInfo.GetCultureInfo("es-MX");

            ExcelWorksheet wsSheet1 = excelPackage.Workbook.Worksheets.Add(nombreHoja);

            //banco = "BANAMEX ";
            //cuenta = "CTA ";// + _ReporteEstadoCuentaConciliado[0].CuentaBancoFinanciero + " ";
            //empresa = "Corporativo";//_ReporteEstadoCuentaConciliado[0].Corporativo;
            //fecha = DateTime.Now;//_ReporteEstadoCuentaConciliado[0].Fecha;

            wsSheet1.Column(2).Width = 10;
            wsSheet1.Column(3).Width = 10;
            wsSheet1.Column(4).Width = 10;
            wsSheet1.Column(5).Width = 10;
            wsSheet1.Column(6).Width = 30;
            wsSheet1.Column(7).Width = 10;
            wsSheet1.Column(8).Width = 10;

            // Cuenta
            using (ExcelRange Rng = wsSheet1.Cells["B2:E2"])
            {
                Rng.Value = _Banco + " " + _NombreHoja.ToUpper();
                Rng.Merge = true;
            }

            // Empresa
            using (ExcelRange Rng = wsSheet1.Cells["B3:E3"])
            {
                Rng.Merge = true;
                Rng.Value = _Empresa.ToUpper();
            }

            // Fechas
            using (ExcelRange Rng = wsSheet1.Cells["B4:E4"])
            {
                Rng.Merge = true;
                Rng.Value = "DEL " + _FechaIni.ToUpper() + " AL " + _FechaFin.ToUpper();
                Rng.Style.Font.Bold = true;
                Rng.Style.Font.Size = 12;
                Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                Rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                Rng.Style.Fill.BackgroundColor.SetColor(Color.Blue);
                Rng.Style.Font.Color.SetColor(Color.White);
            }

            // Fecha Generacion
            using (ExcelRange Rng = wsSheet1.Cells["G2:H2"])
            {
                Rng.Merge = true;
                Rng.Value = "Fecha:  " + string.Format("{0:dd/MM/yyyy}", DateTime.Now);
                Rng.Style.Font.Bold = true;
                Rng.Style.Font.Size = 10;
                Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            }



            // Clabe interbancaria
            using (ExcelRange Rng = wsSheet1.Cells["G3:H3"])
            {
                Rng.Merge = true;
                Rng.Value = "CLABE INTERBANCARIA";
                Rng.Style.Font.Bold = true;
                Rng.Style.Font.Size = 10;
                Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            // Numero Clabe
            using (ExcelRange Rng = wsSheet1.Cells["G4:H4"])
            {
                Rng.Merge = true;
                Rng.Value = "";
                Rng.Style.Font.Bold = true;
                Rng.Style.Font.Size = 10;
                Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                Rng.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                Rng.Style.Border.Bottom.Color.SetColor(Color.Black);
            }
            

            // Mes
            //using (ExcelRange Rng = wsSheet1.Cells["C4:E4"])
            //{
            //    Rng.Merge = true;
            //    Rng.Value = fecha.ToString("MMMM", cultureInfo).ToUpper();
            //    Rng.Style.Font.Bold = true;
            //    Rng.Style.Font.Size = 13;
            //    Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //    Rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
            //    Rng.Style.Fill.BackgroundColor.SetColor(Color.Blue);
            //    Rng.Style.Font.Color.SetColor(Color.White);
            //}

            // Fecha
            //using (ExcelRange Rng = wsSheet1.Cells["I1:J1"])
            //{
            //    Rng.Merge = true;
            //    Rng.Style.Font.Bold = true;
            //    Rng.Value = fecha.ToString("MMMM DE yyyy").ToUpper();
            //    Rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
            //    Rng.Style.Fill.BackgroundColor.SetColor(Color.Blue);
            //    Rng.Style.Font.Color.SetColor(Color.White);
            //}

            // Clabe interbancaria
            //wsSheet1.Cells["I2"].Value = "CLABE INTERBANCARIA";

            //resalteOscuro("Depósito", wsSheet1.Cells["L1"]);
            //resalteOscuro("Retiro", wsSheet1.Cells["L2"]);
            //resalteOscuro("Saldo final calculado", wsSheet1.Cells["L3"]);
            //resalteOscuro("Saldo final bancario", wsSheet1.Cells["L4"]);
            //resalteOscuro("Depósito conciliado", wsSheet1.Cells["L5"]);
            //resalteOscuro("Retiro conciliado", wsSheet1.Cells["L6"]);

            wsSheet1.Protection.IsProtected = false;
            wsSheet1.Protection.AllowSelectLockedCells = false;

            using (ExcelRange Rng = wsSheet1.Cells["B7:H7"])
            {
                estilarEncabezado("Consecutivo", Rng[7, 2]);
                estilarEncabezado("FOperacion", Rng[7, 3]);
                estilarEncabezado("FMovimiento", Rng[7, 4]);
                estilarEncabezado("Referencia", Rng[7, 5]);
                estilarEncabezado("Concepto", Rng[7, 6]);
                estilarEncabezado("Retiros", Rng[7, 7]);
                estilarEncabezado("Depósitos", Rng[7, 8]);

                enmarcarRegion(Rng[7, 2, 7, 8]);
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

            foreach (List<DetalleInformeInternosAFuturo> detalle in _InformeInternosAFuturo)
            {
                foreach (var item in detalle)
                {
                    wsSheet1.Cells[i, 2].Value = item.ConsecutivoFlujo.ToString();
                    wsSheet1.Cells[i, 3].Value = item.FOperacion.ToString("dd/MM/yyyy");
                    wsSheet1.Cells[i, 4].Value = item.FMovimiento.ToString("dd/MM/yyyy");
                    wsSheet1.Cells[i, 5].Value = item.Referencia;
                    wsSheet1.Cells[i, 6].Value = item.Concepto;
                    wsSheet1.Cells[i, 7].Value = item.Retiros;
                    wsSheet1.Cells[i, 8].Value = item.Depositos;
                    i++;
                }
                
            }

            // Total
            using (ExcelRange Rng = wsSheet1.Cells[i + 1, 6])
            {
                Rng.Merge = true;
                Rng.Value = "Total";
            }

            wsSheet1.Cells[i + 1, 7].Formula = "SUM(" + wsSheet1.Cells[8, 7].Address + ":" + wsSheet1.Cells[i, 7].Address + ")";
            wsSheet1.Cells[i + 1, 8].Formula = "SUM(" + wsSheet1.Cells[8, 8].Address + ":" + wsSheet1.Cells[i, 8].Address + ")";

            using (ExcelRange Rng = wsSheet1.Cells[i + 1, 6, i + 1, 8])
            {
                Rng.Style.Font.Bold = true;
                Rng.Style.Font.Size = 10;
                Rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                Rng.Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                Rng.Style.Border.Bottom.Color.SetColor(Color.Black);
            }

        }



       

        private bool ValidarMiembros()
        {
            StringBuilder mensajeExcepcion = new StringBuilder();
            bool valido = false;

            if (_InformeInternosAFuturo == null)
            {
                mensajeExcepcion.Append("No se encontraron datos con los parámetros seleccionados. <br/>");
            }
            else if (_InformeInternosAFuturo.Count == 0)
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
