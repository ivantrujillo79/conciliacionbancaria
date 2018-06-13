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
using DetalleReporteEstadoCuentaDia = Conciliacion.RunTime.DatosSQL.InformeBancarioDatos.DetalleReporteEstadoCuentaDia;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public class ExportadorInformeEstadoCuentaDia
    {

        #region Miembros privados

  

        private List<List<DetalleReporteEstadoCuentaDia>> _ReporteEstadoCuentaDia;
        private string _Ruta;
        private string _Archivo;
        private string _NombreHoja;

        #endregion

        #region Constructores

        public ExportadorInformeEstadoCuentaDia(List<List<DetalleReporteEstadoCuentaDia>> ReporteEstadoCuentaDia,
                                        string Ruta, string Archivo, string Nombre)
        {
            try
            {
                _ReporteEstadoCuentaDia = ReporteEstadoCuentaDia;
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

        public void generarInforme()
        {
            try
            {
                try
                {
                    ExcelPackage excelPackage = inicializar();
                   // crearEncabezado(excelPackage, "NombreHoja");
                    //exportarDatos(excelPackage, "NombreHoja", 8);
                    /*cerrar();*/
                    

                    for (int i = 0; i <= _ReporteEstadoCuentaDia.Count-1; i++)
                    {
           
                           // inicializar();
                            crearEncabezado(excelPackage, _ReporteEstadoCuentaDia[i][0].CuentaBancoFinanciero.ToString()) ;
                            exportarDatos(excelPackage, _ReporteEstadoCuentaDia[i][0].CuentaBancoFinanciero.ToString(), 5, _ReporteEstadoCuentaDia[i]);
                            




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
            string banco, cuenta, empresa, saldofinal, depositos, retiro;
            DateTime fecha;
            CultureInfo cultureInfo = CultureInfo.GetCultureInfo("es-MX");

            banco = "BANAMEX ";
            cuenta = "CTA ";// + _ReporteEstadoCuentaConciliado[0].CuentaBancoFinanciero + " ";
            empresa = "Corporativo";//_ReporteEstadoCuentaConciliado[0].Corporativo;
            fecha = DateTime.Now;//_ReporteEstadoCuentaConciliado[0].Fecha;

            //// banco = _DetalleReporteEstadoCuenta[0].
            //  banco = "BANAMEX ";
            // cuenta = "CTA " + ListaEncabezado[0].CuentaBancoFinanciero + " ";
            // empresa = ListaEncabezado[0].Corporativo;
            // fecha = DateTime.Parse(ListaEncabezado[0].Fecha);
            // retiro = ListaEncabezado[0].Retiro;
            // saldofinal = ListaEncabezado[0].SaldoFinal;
            // depositos = ListaEncabezado[0].Depositos;

            //if (excelPackage.Workbook.Worksheets.Count > 0)
            //{
            //    nombrehoja = "hoja2";
            //}
            
            ExcelWorksheet wsSheet1 = excelPackage.Workbook.Worksheets.Add(nombrehoja);
                  

            

            // Cuenta
            using (ExcelRange Rng = wsSheet1.Cells["B1:G1"])
            {
                Rng.Value = banco.ToUpper() + cuenta.ToUpper() + " MOVIMIENTOS DEL MES DE: " + fecha.ToString("MMMM", cultureInfo).ToUpper(); ;
                Rng.Merge = true;
            }

            // Empresa
            using (ExcelRange Rng = wsSheet1.Cells["B2:G2"])
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
            using (ExcelRange Rng = wsSheet1.Cells["H1:K1"])
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



            wsSheet1.Protection.IsProtected = false;
            wsSheet1.Protection.AllowSelectLockedCells = false;

            using (ExcelRange Rng = wsSheet1.Cells["B4:K4"])
            {
                estilarEncabezado("Fecha", Rng["B4"]);
                estilarEncabezado("Concepto", Rng["C4:G4"], true);//7, 4]);
                estilarEncabezado("Retiros", Rng["H4:I4"], true);
                estilarEncabezado("Depósitos", Rng["J4:K4"],true);

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


        public List<List<DetalleReporteEstadoCuentaDia>> Separat(List<DetalleReporteEstadoCuentaDia> source)
        {
            return source
                .GroupBy(s => s.CuentaBancoFinanciero)
                .OrderBy(g => g.Key)
                .Select(g => g.ToList())
                .ToList();
        }

        private void exportarDatos(ExcelPackage excelPackage, string nombreHoja, int aPartirDeLaFila, List<DetalleReporteEstadoCuentaDia> _DetalleAExportar)
        {
            int i = aPartirDeLaFila;

            ExcelWorksheet wsSheet1 = excelPackage.Workbook.Worksheets[nombreHoja];          

            foreach (DetalleReporteEstadoCuentaDia detalle in _DetalleAExportar)
            {
                wsSheet1.Cells[i, 3, i, 7].Merge = true;
                wsSheet1.Cells[i, 8, i, 9].Merge = true;
                wsSheet1.Cells[i, 10, i, 11].Merge = true;
                wsSheet1.Cells[i, 2].Value = detalle.Fecha.ToString();
                //wsSheet1.Cells[i, 4].Value = detalle.concepto.ToString();
                wsSheet1.Cells[i, 8].Value = detalle.Retiro.ToString();
                wsSheet1.Cells[i, 10].Value = detalle.Depositos.ToString();
                wsSheet1.Cells[i, 11].Value = detalle.SaldoFinal.ToString();
                i++;
            }

        }
        

        private bool ValidarMiembros()
        {
            StringBuilder mensajeExcepcion = new StringBuilder();
            bool valido = false;

            if (_ReporteEstadoCuentaDia == null)
            {
                mensajeExcepcion.Append("No se encontraron datos con los parámetros seleccionados. <br/>");
            }
            else if (_ReporteEstadoCuentaDia.Count == 0)
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
