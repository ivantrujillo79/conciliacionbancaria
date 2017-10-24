using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Conciliacion.RunTime;

namespace ValidacionArchivosConciliacion
{
    #region CuentaBancariaExisteException
    public class CuentaBancariaExisteException : Exception
    {

        public DetalleValidacion ResultadoValidacion;

        public CuentaBancariaExisteException()
        {
            
        }

        ~CuentaBancariaExisteException()
        {

        }

        //public override void Dispose()
        //{

        //}

    }
    #endregion CuentaBancariaExisteException

    #region DocumentoReferenciaExisteException
    public class DocumentoReferenciaExisteException : Exception
    {

        public DetalleValidacion ResultadoValidacion;

        public DocumentoReferenciaExisteException()
        {

        }

        ~DocumentoReferenciaExisteException()
        {

        }

        //public override void Dispose()
        //{

        //}

    }
    #endregion DocumentoReferenciaExisteException

    #region EncabezadoExisteException
    public class EncabezadoExisteException : Exception
    {

        public DetalleValidacion ResultadoValidacion;

        public EncabezadoExisteException()
        {

        }

        ~EncabezadoExisteException()
        {

        }

        //public override void Dispose()
        //{

        //}

    }
    #endregion EncabezadoExisteException

    #region MontoValidoException
    public class MontoValidoException : Exception
    {

        public DetalleValidacion ResultadoValidacion;

        public MontoValidoException()
        {

        }

        ~MontoValidoException()
        {

        }

        //public override void Dispose()
        //{

        //}

    }
    #endregion MontoValidoException

    #region LineaVaciaException
    public class LineaVaciaException : Exception
    {

        public DetalleValidacion ResultadoValidacion;

        public LineaVaciaException()
        {

        }

        ~LineaVaciaException()
        {

        }

        //public override void Dispose()
        //{

        //}

    }
    #endregion LineaVaciaException

    #region LayoutException
    public class LayoutException : Exception
    {

        public DetalleValidacion ResultadoValidacion;

        public LayoutException()
        {

        }

        ~LayoutException()
        {

        }

        //public override void Dispose()
        //{

        //}

    }
    #endregion

    #region FormatoExcelException 
    public class FormatoExcelException : Exception
    {

        public DetalleValidacion ResultadoValidacion;

        public FormatoExcelException()
        {

        }

        ~FormatoExcelException()
        {

        }

        //public override void Dispose()
        //{

        //}

    }
    #endregion FormatoExcelException 

    #region IValidadorExcel
    public interface IValidadorExcel
    {
        string RutaArchivo { get; set; }
        string NombreArchivo { get; set; }
        string TipoMIME { get; set; }

        int CuentaBancaria { get; set; }
        int DocumentoReferencia { get; set; }

        bool ArchivoValido(string RutaArchivo, string NombreArchivo);
        DataTable CargaArchivo(string RutaArchivo, string NombreArchivo);
        List<DetalleValidacion> ValidacionCompleta();
    }
    #endregion

    #region DetalleValidacion
    public class DetalleValidacion
    {
        public bool VerificacionValida;
        public int CodigoError;
        public string Mensaje;

        public DetalleValidacion()
        {

        }

        ~DetalleValidacion()
        {

        }

        public virtual void Dispose()
        {

        }

    }//end DetalleValidacion
    #endregion

    #region ValidadorCyC
    public class ValidadorCyC : IValidadorExcel
    {
        public string RutaArchivo { get; set; }
        public string NombreArchivo { get; set; }
        public string TipoMIME { get; set; }

        public int CuentaBancaria { get; set; }
        public int DocumentoReferencia { get; set; }

        private const int colDoc = 0;
        private const int colCta = 1;
        private const int colMon = 2;

        private const string strEncabezado = "DocumentoCuentaMonto";
        private const int layoutColumnas = 3;

        private const int erCtaBan_EncontroCuentaDistinta = 1;
        private const int erMonto_Invalido = 2;
        private const int erArchivo_NoEsExcel = 3;
        private const int erCelda_Vacia = 4;
        private const int erLayOut_NoEsElEsperado = 5;
        private const int erEncabezado_Invalido = 6;
        private const int erDocRef_EncontroDocRefDistinto = 7;

        private DataTable dtArchivo = null;

        private string GeneraMD5(string str)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public ValidadorCyC()
        {
            
        }

        ~ValidadorCyC()
        {

        }

        public virtual void Dispose()
        {

        }

        public ValidadorCyC(string rutaArchivo, string nombreArchivo, string tipoMIME)
        {
            RutaArchivo = rutaArchivo;
            NombreArchivo = nombreArchivo;
            TipoMIME = tipoMIME;
        }

        public List<DetalleValidacion> ValidacionCompleta()
        {
            List<DetalleValidacion> listDetalleValidacion = new List<DetalleValidacion>();
            if (dtArchivo != null)
            {
                listDetalleValidacion.Add(ValidaFormatoExcel());
                listDetalleValidacion.Add(ValidaLayout());
                listDetalleValidacion.Add(ValidaLineaVacia());
                listDetalleValidacion.Add(ValidaEncabezado());
                listDetalleValidacion.Add(ValidaCuentaBancaria());
                listDetalleValidacion.Add(ValidaDocumentoReferencia());
                listDetalleValidacion.Add(ValidaMonto());
            }

            return listDetalleValidacion;
        }

        private DetalleValidacion ValidaCuentaBancaria()
        {
            DetalleValidacion detallevalidacion = new DetalleValidacion();
            bool Exito = true;
            string ValoresInvalidos = "";
            int RowNo = 1;

            foreach (DataRow row in dtArchivo.Rows)
            {
                RowNo = RowNo + 1;
                if (Convert.ToString(row[colCta]).Trim() != string.Empty & Convert.ToString(row[colCta]).Trim() != CuentaBancaria.ToString())
                {
                    Exito = false;
                    ValoresInvalidos = ValoresInvalidos + RowNo.ToString() + ", ";
                }
            }

            if (Exito)
            {
                detallevalidacion.CodigoError = 0;
                detallevalidacion.Mensaje = "EXITO";
                detallevalidacion.VerificacionValida = true;
            }
            else
            {
                detallevalidacion.CodigoError = erCtaBan_EncontroCuentaDistinta;
                detallevalidacion.Mensaje = "ERROR: Se espera cuenta bancaria: " + CuentaBancaria.ToString() + ". Corrija la(s) fila(s): " + ValoresInvalidos;
                detallevalidacion.VerificacionValida = false;
            }

            return detallevalidacion;
        }

        private DetalleValidacion ValidaMonto()
        {
            DetalleValidacion detallevalidacion = new DetalleValidacion();
            bool Exito = true;
            string ValoresInvalidos = "";
            double Monto;
            double Dec;
            string strDec;
            int rowNo = 1;

            foreach (DataRow row in dtArchivo.Rows)
            {
                rowNo = rowNo + 1;
                //if (double.TryParse(row[colMon].ToString().Trim(), out Monto))
                //{
                //    Dec = Monto - Math.Truncate(Monto); 
                //    strDec = Dec.ToString();
                //    if (Monto <= 0 || strDec.Length > 4)
                //    {
                //        Exito = false;
                //        ValoresInvalidos = ValoresInvalidos + rowNo + ", ";
                //    }
                //}

                if (double.TryParse(row[colMon].ToString().Trim(), out Monto))
                {
                    Monto = Convert.ToDouble(string.Format("{0:C}", Monto).Replace("$", ""));
                    if (Monto <= 0)
                    {
                        Exito = false;
                        ValoresInvalidos = ValoresInvalidos + rowNo + ", ";
                    }
                }
                else
                if (Convert.ToString(row[colMon]) != string.Empty)
                {
                    Exito = false;
                    ValoresInvalidos = ValoresInvalidos + rowNo + ", ";
                }
            }

            if (Exito)
            {
                detallevalidacion.CodigoError = 0;
                detallevalidacion.Mensaje = "EXITO";
                detallevalidacion.VerificacionValida = true;
            }
            else
            {
                detallevalidacion.CodigoError = erMonto_Invalido;
                detallevalidacion.Mensaje = "ERROR: Monto invalido. No es un valor numerico, es menor a $1 o excede dos decimales. Corrija los valores en la(s) fila(s): " + ValoresInvalidos;
                detallevalidacion.VerificacionValida = false;
            }
            return detallevalidacion;
        }

        private DetalleValidacion ValidaDocumentoReferencia()
        {
            DetalleValidacion detallevalidacion = new DetalleValidacion();

            bool Exito = true;
            string ValoresInvalidos = "";
            int rowNo = 1;

            foreach (DataRow row in dtArchivo.Rows)
            {
                rowNo = rowNo + 1;

                if ( ! Conciliacion.RunTime.App.Consultas.VerificaPedidoReferenciaExiste(row[colDoc].ToString()) )
                {
                    Exito = false;
                    ValoresInvalidos = ValoresInvalidos + rowNo.ToString() + ", ";
                }
            }

            if (Exito)
            {
                detallevalidacion.CodigoError = 0;
                detallevalidacion.Mensaje = "EXITO";
                detallevalidacion.VerificacionValida = true;
            }
            else
            {
                detallevalidacion.CodigoError = erDocRef_EncontroDocRefDistinto;
                detallevalidacion.Mensaje = "ERROR: No existe la Referencia Documento. Corrija la(s) fila(s): " + ValoresInvalidos;
                detallevalidacion.VerificacionValida = false;
            }

            return detallevalidacion;
        }

        private DetalleValidacion ValidaEncabezado()
        {
            DetalleValidacion detallevalidacion = new DetalleValidacion();

            if (dtArchivo.Columns.Count >= 3  
                &&
                string.Compare(GeneraMD5(strEncabezado), GeneraMD5(dtArchivo.Columns[0].ColumnName + dtArchivo.Columns[1].ColumnName + dtArchivo.Columns[2].ColumnName)) == 0)
            {
                detallevalidacion.CodigoError = 0;
                detallevalidacion.Mensaje = "EXITO";
                detallevalidacion.VerificacionValida = true;
            }
            else
            {
                detallevalidacion.CodigoError = erEncabezado_Invalido;
                detallevalidacion.Mensaje = "ERROR: Encabezado invalido. Se espera Documento Cuenta Monto en la primer fila";
                detallevalidacion.VerificacionValida = false;
            }

            return detallevalidacion;
        }

        private DetalleValidacion ValidaFormatoExcel()
        {
            DetalleValidacion detallevalidacion = new DetalleValidacion();

            string contentType = "application/octetstream";
            string ext = System.IO.Path.GetExtension(RutaArchivo + NombreArchivo).ToLower();
            Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (registryKey != null && registryKey.GetValue("Content Type") != null)
                contentType = registryKey.GetValue("Content Type").ToString();

            //"application/vnd.ms-excel" 
            //"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            if (contentType == TipoMIME)
            {
                detallevalidacion.CodigoError = 0;
                detallevalidacion.Mensaje = "EXITO";
                detallevalidacion.VerificacionValida = true;
            }
            else
            {
                detallevalidacion.CodigoError = erArchivo_NoEsExcel;
                detallevalidacion.Mensaje = "ERROR: Archivo invalido. No es un archivo de Excel valido.";
                detallevalidacion.VerificacionValida = false;
            }

            return detallevalidacion;
        }

        private DetalleValidacion ValidaLayout()
        {
            DetalleValidacion detallevalidacion = new DetalleValidacion();
            bool Exito = true;
            string ValoresInvalidos = "";

            if (dtArchivo.Columns.Count != layoutColumnas)
            {
                Exito = false;
                ValoresInvalidos = "Se esperan 3 columnas.";
            }
            if (dtArchivo.Rows.Count == 0)
            {
                Exito = false;
                ValoresInvalidos = ValoresInvalidos + "Se esperan al menos 2 filas.";
            }
            //else
            //{
            //    foreach (DataRow row in dtArchivo.Rows)
            //    {
            //        rowNo = rowNo + 1;
            //        if (string.Compare(row[layoutColumnas + 1].ToString().Trim(), string.Empty) != 0)
            //        {
            //            Exito = false;
            //            ValoresInvalidos = ValoresInvalidos + " Fila: " + rowNo + ", ";
            //        }
            //    }
            //}

            if (Exito)
            {
                detallevalidacion.CodigoError = 0;
                detallevalidacion.Mensaje = "EXITO";
                detallevalidacion.VerificacionValida = true;
            }
            else
            {
                detallevalidacion.CodigoError = erLayOut_NoEsElEsperado;
                detallevalidacion.Mensaje = "ERROR: El layout no corresponde con el esperado. " + ValoresInvalidos;
                detallevalidacion.VerificacionValida = false;
            }

            return detallevalidacion;
        }

        public DetalleValidacion ValidaLineaVacia()
        {
            DetalleValidacion detallevalidacion = new DetalleValidacion();
            bool Exito = true;
            string ValoresInvalidos = "";
            int rowNo = 1;

            foreach (DataRow row in dtArchivo.Rows)
            {
                rowNo = rowNo + 1;
                for (int colNo = 0; colNo <= layoutColumnas - 1; colNo++)
                {
                    if (Convert.ToString(row[colNo]) == string.Empty)
                    {
                        Exito = false;
                        ValoresInvalidos = ValoresInvalidos + rowNo.ToString() + ", ";
                        break;
                    }
                }
                //rowNo = 1;
            }

            if (Exito)
            {
                detallevalidacion.CodigoError = 0;
                detallevalidacion.Mensaje = "EXITO";
                detallevalidacion.VerificacionValida = true;
            }
            else
            {
                detallevalidacion.CodigoError = erCelda_Vacia;
                detallevalidacion.Mensaje = "ERROR: Celda vacia. Una o mas celdas estan vacias. Corrija la(s) fila(s): " + ValoresInvalidos;
                detallevalidacion.VerificacionValida = false;
            }

            return detallevalidacion;
        }

        public bool ArchivoValido(string RutaArchivo, string NombreArchivo)
        {
            bool existe;
            DetalleValidacion ValidoExcel = new DetalleValidacion();

            existe = (NombreArchivo.Trim() != string.Empty & TipoMIME.Trim() != string.Empty & RutaArchivo.Trim() != string.Empty) & (File.Exists(RutaArchivo + NombreArchivo));

            if (existe)
            {
                ValidoExcel = ValidaFormatoExcel();
                return ValidoExcel.CodigoError == 0;
            }
            else
                return false;
        }

        public DataTable CargaArchivo(string RutaArchivo, string NombreArchivo)
        {
            OleDbConnection oledbConn = new OleDbConnection();
            OleDbCommand cmd;
            OleDbDataAdapter oleda;
            DataSet ds;            
            ds = new DataSet();
            string sArchivo = "";

            try
            {
                //sArchivo = System.IO.Path.GetFullPath(Server.MapPath("~/")) + RutaArchivo + NombreArchivo;
                sArchivo = RutaArchivo + NombreArchivo;

                if (Path.GetExtension(sArchivo) == ".xls")
                {
                    //oledbConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;" +
                    //    "Data Source = " + sArchivo +
                    //    ";Extended Properties =\"Excel 8.0;HDR=Yes;IMEX=2\"");

                    oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;" +
                    "Data Source=" + sArchivo +
                    ";Extended Properties = 'Excel 12.0;HDR=YES;IMEX=1;'; ");
                }
                else
                if (Path.GetExtension(sArchivo) == ".xlsx")
                {
                    oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;" +
                    "Data Source=" + sArchivo +
                    ";Extended Properties = 'Excel 12.0;HDR=YES;IMEX=1;'; ");
                }

                if (oledbConn != null)
                {
                    oledbConn.Open();
                    try
                    {
                        cmd = new OleDbCommand("SELECT * FROM [Hoja1$]", oledbConn);
                        oleda = new OleDbDataAdapter();
                        oleda.SelectCommand = cmd;
                        oleda.Fill(ds, "Registros");
                        dtArchivo = ds.Tables[0];
                    }
                    finally
                    {
                        oledbConn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                App.ImplementadorMensajes.MostrarMensaje(ex.Message);
            }
            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
            //if (dtArchivo != null)
            //    return true;
            //else
            //    return false;
        }
    }//end ValidadorCyC
    #endregion

}
