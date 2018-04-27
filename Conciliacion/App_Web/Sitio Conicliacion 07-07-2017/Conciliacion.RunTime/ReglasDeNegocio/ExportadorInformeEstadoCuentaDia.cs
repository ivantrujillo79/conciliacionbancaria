using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using DetalleReporteEstadoCuenta = Conciliacion.RunTime.DatosSQL.InformeBancarioDatos.DetalleReporteEstadoCuenta;
using DetalleReporteEstadoCuentaDia= Conciliacion.RunTime.DatosSQL.InformeBancarioDatos.DetalleReporteEstadoCuentaDia;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public class ExportadorInformeEstadoCuentaDia
    {

        #region Miembros privados

        private Microsoft.Office.Interop.Excel.Application xlAplicacion;
        private Microsoft.Office.Interop.Excel.Workbooks xlLibros;
        private Microsoft.Office.Interop.Excel.Workbook xlLibro;
        private Microsoft.Office.Interop.Excel.Sheets xlHojas;
        private Microsoft.Office.Interop.Excel.Worksheet xlHoja;
        private Microsoft.Office.Interop.Excel.Range xlRango;

        private List<List<DetalleReporteEstadoCuentaDia>> _DetalleReporteEstadoCuentaDia;
        private string _Ruta;
        private string _Archivo;
        private string _NombreHoja;

        #endregion

        #region Constructores

        public ExportadorInformeEstadoCuentaDia(List<List<DetalleReporteEstadoCuentaDia>> DetalleReporteEstadoCuentaDia,
                                        string Ruta, string Archivo, string Nombre)
        {
            try
            {
                _DetalleReporteEstadoCuentaDia = DetalleReporteEstadoCuentaDia;
                _Ruta = Ruta.Trim();
                _Archivo = Archivo.Trim();
                _NombreHoja = Nombre.Trim();

                ValidarMiembros();
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

                for (int i = 0; i < _DetalleReporteEstadoCuentaDia.Count; i++)
                {
                    if (i == 0)
                    {
                        inicializar(_DetalleReporteEstadoCuentaDia[i]);
                        ExportarDatosCuenta(_DetalleReporteEstadoCuentaDia[i]);
                    }
                    else
                    {
                        ExportarDatosCuenta(_DetalleReporteEstadoCuentaDia[i]);
                    }
                    //crearEncabezado(_DetalleReporteEstadoCuentaDia[i]);
                    
                }

             
                

               

                             
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

        private void inicializar(List<DetalleReporteEstadoCuentaDia> ListaInicial)
        {
            string rutaCompleta = _Ruta + _Archivo;

            xlAplicacion = new Microsoft.Office.Interop.Excel.Application();

            if (xlAplicacion == null)
            {
                throw new Exception("Microsoft Excel no está instalado correctamente en el equipo.");
            }

            xlAplicacion.Visible = false;

            xlAplicacion.DisplayAlerts = false;

            xlLibros = xlAplicacion.Workbooks;

            if (File.Exists(rutaCompleta))
            {
                xlLibro = xlAplicacion.Workbooks.Open(rutaCompleta,
                    0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "",
                    true, false, 0, true, false, false);

                //xlLibro = xlAplicacion.Workbooks.Open(rutaCompleta, Excel.XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing,
                //            false, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing,
                //            Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                xlHojas = xlLibro.Sheets;

                xlHoja = xlHojas.Add();

                xlHoja.Name = ListaInicial[0].CuentaBancoFinanciero;
            }
            else
            {
                xlLibro = xlLibros.Add(Excel.XlWBATemplate.xlWBATWorksheet);

                //xlHojas = xlLibro.Sheets;

                //xlHoja = xlHojas.Add();

                xlHoja = (Excel.Worksheet)xlLibro.Sheets[1];

                xlHoja.Name = ListaInicial[0].CuentaBancoFinanciero;
            }
        }

        private void crearEncabezado(List<DetalleReporteEstadoCuentaDia> ListaEncabezado)
        {
            string banco, cuenta, empresa, saldofinal, depositos, retiro;
            DateTime fecha;
            CultureInfo cultureInfo = CultureInfo.GetCultureInfo("es-MX");

            //banco = _DetalleReporteEstadoCuenta[0].Banco;
            banco = "BANAMEX ";
            cuenta = "CTA " + ListaEncabezado[0].CuentaBancoFinanciero + " ";
            empresa = ListaEncabezado[0].Corporativo;
            fecha = DateTime.Parse(ListaEncabezado[0].Fecha);
            retiro = ListaEncabezado[0].Retiro;
            saldofinal = ListaEncabezado[0].SaldoFinal;
            depositos = ListaEncabezado[0].Depositos;

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
            xlRango.Font.ColorIndex = 23;
            xlRango.Font.Size = 10;
            xlRango.Interior.Color = Excel.XlRgbColor.rgbWhite;
            xlRango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            xlRango.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            xlRango.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium);

            xlRango = xlHoja.Columns["A"];
            xlRango.ColumnWidth = 4;
            xlRango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            xlRango.Font.Bold = true;
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

        public List<List<DetalleReporteEstadoCuentaDia>> Separat(List<DetalleReporteEstadoCuentaDia> source)
        {
            return source
                .GroupBy(s => s.CuentaBancoFinanciero)
                .OrderBy(g => g.Key)
                .Select(g => g.ToList())
                .ToList();
        }


        public void ExportarDatosCuenta(List<DetalleReporteEstadoCuentaDia> _DetalleAExportar)
        {
            int i = 5;
            Excel.Range celdaInicio;
            Excel.Range celdaFin;

            xlHojas = xlLibro.Sheets;
          
                xlHoja = (Microsoft.Office.Interop.Excel.Worksheet)xlLibro.Worksheets.Add
              (System.Reflection.Missing.Value,
               xlLibro.Worksheets[xlLibro.Worksheets.Count],
               System.Reflection.Missing.Value,
               System.Reflection.Missing.Value);
              xlHoja.Name = _DetalleAExportar[0].CuentaBancoFinanciero;
                 
            xlRango = xlHoja.Range["A5:K5"];            
            
            foreach (DetalleReporteEstadoCuentaDia detalle in _DetalleAExportar)
            {
                celdaInicio = xlHoja.Cells[i, 1];
                celdaFin = xlHoja.Cells[i, 11];
                xlRango = xlHoja.Range[celdaInicio, celdaFin];               
                xlRango[1, 1].Value2 = detalle.CuentaBancoFinanciero.ToString();
                xlRango[1, 2].Value2 = detalle.Fecha.ToString();
                xlRango[1, 3].Value2 = detalle.Depositos.ToString();
                xlRango[1, 4].Value2 = detalle.Retiro.ToString();
                xlRango[1, 5].Value2 = detalle.SaldoFinal.ToString();

                i++;
            }
            celdaInicio = xlHoja.Cells[5, 1];
            celdaFin = xlHoja.Cells[i, 8];
            xlRango = xlHoja.Range[celdaInicio, celdaFin];
            xlRango.Font.Size = 10;

            celdaInicio = xlHoja.Cells[5, 4];   // D5
            celdaFin = xlHoja.Cells[i, 8];  // H#

            xlRango = xlHoja.Range[celdaInicio, celdaFin];
            xlRango.Merge(true);
        }
        
        private void cerrar()
        {
            string rutaCompleta = _Ruta + _Archivo;

            //if (File.Exists(rutaCompleta)) File.Delete(rutaCompleta);

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

            if (_DetalleReporteEstadoCuentaDia == null)
            {
                mensajeExcepcion.Append("La lista del informe bancario está vacía. <br/>");
            }
            else if (_DetalleReporteEstadoCuentaDia.Count == 0)
            {
                mensajeExcepcion.Append("La lista del informe bancario está vacía. <br/>");
            }

            if (string.IsNullOrEmpty(_NombreHoja))
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
        
    }
}
