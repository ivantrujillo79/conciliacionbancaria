using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    class ExportadorInformeBancario
    {

        #region Miembros privados

        private Microsoft.Office.Interop.Excel.Application exAplicacion;
        private Microsoft.Office.Interop.Excel.Workbook exLibro;
        private Microsoft.Office.Interop.Excel.Worksheet exHoja;
        private Microsoft.Office.Interop.Excel.Range exRango;

        private string _Nombre;

        #endregion

        #region Constructores
        
        public ExportadorInformeBancario(string Nombre)
        {
            _Nombre = Nombre;
        }

        #endregion

        public void generar()
        {
            try
            {
                inicializar();
            }
            finally
            {
                Marshal.ReleaseComObject(exAplicacion);
                Marshal.ReleaseComObject(exLibro);
                Marshal.ReleaseComObject(exHoja);
                Marshal.ReleaseComObject(exRango);
            }
        }

        private void inicializar()
        {
            exAplicacion = new Microsoft.Office.Interop.Excel.Application();

            if (exAplicacion == null)
            {
                throw new Exception("Microsoft Excel no está instalado correctamente en el equipo.");
            }

            exAplicacion.Visible = false;

            exLibro = exAplicacion.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);

            //string workbookPath = Ruta + Archivo;

            exLibro = exAplicacion.Workbooks.Open(_Nombre, Type.Missing, false, 5, Type.Missing,
                                                Type.Missing, false, Excel.XlPlatform.xlWindows, 
                                                Type.Missing, true, false, Type.Missing, true, 
                                                false, false);

            exHoja = (Excel.Worksheet)exLibro.Sheets[1];
        }

        private void crearEncabezado()
        {

        }

    }
}
