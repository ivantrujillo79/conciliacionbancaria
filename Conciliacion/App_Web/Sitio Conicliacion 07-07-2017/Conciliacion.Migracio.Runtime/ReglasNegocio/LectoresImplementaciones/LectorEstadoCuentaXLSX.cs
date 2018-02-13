using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio.LectoresImplementaciones
{
    public class LectorEstadoCuentaXLSX:LectorEstadoCuenta
    {

        protected override System.Data.DataTable ObtenerContenido(System.Data.DataTable contenido, FuenteInformacion fuenteInformacion, string rutaArchivo)
        {
            try
            {
                DataSet dsMsExcel = new DataSet();
                using (System.Data.OleDb.OleDbConnection objOleConnection = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + rutaArchivo + ";Mode=ReadWrite;Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\""))
                {
                    System.Data.OleDb.OleDbDataAdapter adapter = new System.Data.OleDb.OleDbDataAdapter();
                    objOleConnection.Open();
                    DataTable worksheets = objOleConnection.GetSchema("Tables");
                    string hoja = worksheets.Rows[0][2].ToString();
                    System.Data.OleDb.OleDbCommand select = new System.Data.OleDb.OleDbCommand("SELECT  * FROM [" + hoja + "]", objOleConnection);
                    select.CommandType = CommandType.Text;
                    adapter.SelectCommand = select;
                    dsMsExcel.Tables.Clear();
                    adapter.Fill(dsMsExcel);
                    if (dsMsExcel.Tables.Count > 0)
                    {
                        foreach (DataRow filaEstadoCuenta in dsMsExcel.Tables[0].Rows)
                        {
                            contenido.Rows.Add(filaEstadoCuenta.ItemArray);
                        }
                        //contenido.Rows.Remove(contenido.Rows[0]);
                        return contenido;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;


        }

        public override System.Data.DataColumn[] ObtenerColumnas(FuenteInformacion fuenteInformacion, string rutaArchivo)
        {
            try
            {
                DataSet dsMsExcel = new DataSet();
                using (System.Data.OleDb.OleDbConnection objOleConnection = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + rutaArchivo + ";Mode=ReadWrite;Extended Properties=\"Excel 12.0 Xml;HDR=NO;IMEX=1\""))
                {
                    System.Data.OleDb.OleDbDataAdapter adapter = new System.Data.OleDb.OleDbDataAdapter();
                    objOleConnection.Open();
                    DataTable worksheets = objOleConnection.GetSchema("Tables");
                    string hoja = worksheets.Rows[0][2].ToString();
                    System.Data.OleDb.OleDbCommand select = new System.Data.OleDb.OleDbCommand("SELECT  * FROM [" + hoja + "]", objOleConnection);
                    select.CommandType = CommandType.Text;
                    adapter.SelectCommand = select;
                    dsMsExcel.Tables.Clear();
                    adapter.Fill(dsMsExcel);
                    if (dsMsExcel.Tables.Count > 0)
                    {
                        DataRow col = dsMsExcel.Tables[0].Rows[0];
                        DataColumn[] columnas = new DataColumn[col.ItemArray.Length];
                        int index = 0;
                        string nombre;
                        foreach (object campo in col.ItemArray)
                        {

                            if (!string.IsNullOrEmpty(campo.ToString()))
                            {
                                nombre = campo.ToString().Trim();
                            }
                            else
                                nombre = "-----------" + index.ToString();

                            columnas[index] = new DataColumn(nombre);
                            index++;

                        }
                        return columnas;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;

        }
    }
}
