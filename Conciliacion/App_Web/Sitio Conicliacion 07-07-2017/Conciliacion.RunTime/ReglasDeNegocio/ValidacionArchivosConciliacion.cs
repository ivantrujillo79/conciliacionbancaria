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
        
    }
    #endregion FormatoExcelException 

    #region IValidadorExcel
    public interface IValidadorExcel
    {
        string RutaArchivo { get; set; }
        string NombreArchivo { get; set; }
        string TipoMIME { get; set; }

        byte Modulo { get; set; }
        string CadenaConexion { get; set; }
        string URLGateway { get; set; }

        int CuentaBancaria { get; set; }
        int DocumentoReferencia { get; set; }

        bool ArchivoValido(string RutaArchivo, string NombreArchivo);
        //DataTable CargaArchivo(string RutaArchivo, string NombreArchivo);
        DataTable CargaArchivo(string RutaArchivo, bool TieneEncabezado = true);
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

    }
    #endregion

    #region ValidadorCyC
    public class ValidadorCyC : IValidadorExcel
    {
        public string RutaArchivo { get; set; }
        public string NombreArchivo { get; set; }
        public string TipoMIME { get; set; }

        public byte Modulo { get; set; }
        public string CadenaConexion { get; set; }
        public string URLGateway { get; set; }

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
                //listDetalleValidacion.Add(ValidaFormatoExcel());
                listDetalleValidacion.Add(ValidaLayout());
                listDetalleValidacion.Add(ValidaLineaVacia());
                listDetalleValidacion.Add(ValidaEncabezado());
                listDetalleValidacion.Add(ValidaCuentaBancaria());
                listDetalleValidacion.Add(ValidaDocumentoReferencia());
                listDetalleValidacion.Add(ValidaMonto());
                if (!String.IsNullOrEmpty(URLGateway))
                {
                    listDetalleValidacion.Add(ValidaParentezcoCRM());
                }
                else
                {
                    listDetalleValidacion.Add(ValidaParentezco());
                }
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
                detallevalidacion.Mensaje = "Se espera cuenta bancaria: " + CuentaBancaria.ToString() + ". Corrija la(s) fila(s): " + ValoresInvalidos;
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
            int rowNo = 1;

            foreach (DataRow row in dtArchivo.Rows)
            {
                rowNo = rowNo + 1;

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
                detallevalidacion.Mensaje = "Monto invalido. No es un valor numérico, es menor a $1 o excede dos decimales. Corrija los valores en la(s) fila(s): " + ValoresInvalidos;
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
                detallevalidacion.Mensaje = "No existe la Referencia Documento. Corrija la(s) fila(s): " + ValoresInvalidos;
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
                detallevalidacion.Mensaje = "Encabezado invalido. Se espera: Documento Cuenta Monto en la primer fila";
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
                detallevalidacion.Mensaje = "Archivo invalido. No es un archivo de Excel valido.";
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

            if (Exito)
            {
                detallevalidacion.CodigoError = 0;
                detallevalidacion.Mensaje = "EXITO";
                detallevalidacion.VerificacionValida = true;
            }
            else
            {
                detallevalidacion.CodigoError = erLayOut_NoEsElEsperado;
                detallevalidacion.Mensaje = "El layout no corresponde con el esperado. " + ValoresInvalidos;
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
                detallevalidacion.Mensaje = "Celda vacía. Una o mas celdas estan vacías. Corrija la(s) fila(s): " + ValoresInvalidos;
                detallevalidacion.VerificacionValida = false;
            }
            
            return detallevalidacion;
        }

        public DetalleValidacion ValidaParentezco()
        {
            DetalleValidacion detallevalidacion = new DetalleValidacion();
            string Pedidoreferencia = "";
            var ListaPedidoCliente = CrearListaGenerica(new {PedidoReferencia = "", Cliente = ""});
            ListaPedidoCliente.Clear();
            
            bool ResultadoValidacion = true;
            string DetalleError = "";

            if (dtArchivo.Rows.Count > 0)
            {
                foreach (DataRow row in dtArchivo.Rows)
                {
                    Pedidoreferencia = row[colDoc].ToString();
                    DataTable dtDetallePedido = Conciliacion.RunTime.App.Consultas.PedidoReferenciaDetalle(Pedidoreferencia);
                    if (dtDetallePedido.Rows.Count > 0)
                    {
                        ListaPedidoCliente.Add(new { PedidoReferencia = dtDetallePedido.Rows[0]["PedidoReferencia"].ToString().Trim(), Cliente = dtDetallePedido.Rows[0]["Cliente"].ToString().Trim() });
                    }
                }
            }
            DataTable dtClienteFamilia;
            if (ListaPedidoCliente.Count > 0)
            { 
                dtClienteFamilia = Conciliacion.RunTime.App.Consultas.FamiliaresCliente(Convert.ToInt32(ListaPedidoCliente[0].Cliente));
                List<string> ListaFamilia = (from DataRow row in dtClienteFamilia.Rows select row["Cliente"].ToString()).Distinct().ToList();
                foreach (var Cliente in ListaPedidoCliente)
                {
                    if (!ListaFamilia.Exists(e => e.Contains(Cliente.Cliente)))
                    {
                        var ListaPedidos = ListaPedidoCliente.Where(x => x.Cliente == Cliente.Cliente).ToList();
                        ListaPedidos.ForEach(x => DetalleError += " \n " + x.PedidoReferencia.ToString().Trim()+ " del cliente: " + x.Cliente.ToString().Trim() + ",");
                        ResultadoValidacion = false;
                    }
                }
            }

            if (ResultadoValidacion)
            {
                detallevalidacion.CodigoError = 0;
                detallevalidacion.Mensaje = "Todos los pedidos cargados corresponden a clientes emparentados.";
                detallevalidacion.VerificacionValida = true;
            }
            else
            {
                detallevalidacion.CodigoError = 500;
                detallevalidacion.Mensaje = "Los pedidos " + DetalleError  + "\n no están emparentados y no serán cargados.";
                detallevalidacion.VerificacionValida = false;
            }

            return detallevalidacion;
        }

        public DetalleValidacion ValidaParentezcoCRM()
        {
            string Pedidoreferencia = "";
            string DetalleError = "";
            bool ResultadoValidacion = true;
            DetalleValidacion detalleValidacion = new DetalleValidacion();
            var ListaPedidoCliente = CrearListaGenerica(new { PedidoReferencia = "", Cliente = "" });
            ListaPedidoCliente.Clear();

            if (dtArchivo.Rows.Count == 0) { return detalleValidacion; }

            foreach (DataRow row in dtArchivo.Rows)
            {
                Pedidoreferencia = row[colDoc].ToString();
                DataTable dtDetallePedido = Conciliacion.RunTime.App.Consultas.PedidoReferenciaDetalle(Pedidoreferencia);
                if (dtDetallePedido.Rows.Count > 0)
                {
                    ListaPedidoCliente.Add(new
                    {
                        PedidoReferencia = dtDetallePedido.Rows[0]["PedidoReferencia"].ToString().Trim(),
                        Cliente = dtDetallePedido.Rows[0]["Cliente"].ToString().Trim()
                    });
                }
            }

            RTGMGateway.RTGMGateway obGateway = new RTGMGateway.RTGMGateway(this.Modulo, this.CadenaConexion);
            obGateway.URLServicio = this.URLGateway;
            RTGMGateway.SolicitudGateway obSolicitud = new RTGMGateway.SolicitudGateway
            {
                IDCliente = Convert.ToInt32(ListaPedidoCliente[0].Cliente)
            };

            RTGMCore.DireccionEntrega obDireccionEntrega = obGateway.buscarDireccionEntrega(obSolicitud);

            if (!obDireccionEntrega.Success && !String.IsNullOrEmpty(obDireccionEntrega.Message))
            {
                throw new Exception(obDireccionEntrega.Message);
            }

            if (obDireccionEntrega.IDClientesRelacionados.Count > 0)
            {
                foreach (var obPedido in ListaPedidoCliente)
                {
                    int cliente = Convert.ToInt32(obPedido.Cliente);
                    string pedido = obPedido.PedidoReferencia;

                    if (!obDireccionEntrega.IDClientesRelacionados.Contains(cliente))
                    {
                        DetalleError += " \n " + pedido + " del cliente: " + cliente.ToString() + ",";
                        ResultadoValidacion = false;
                    }
                }
            }
            
            if (ResultadoValidacion)
            {
                detalleValidacion.CodigoError = 0;
                detalleValidacion.Mensaje = "Todos los pedidos cargados corresponden a clientes emparentados.";
                detalleValidacion.VerificacionValida = true;
            }
            else
            {
                detalleValidacion.CodigoError = 500;
                detalleValidacion.Mensaje = "Los pedidos " + DetalleError + "\n no están emparentados y no serán cargados.";
                detalleValidacion.VerificacionValida = false;
            }

            return detalleValidacion;
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

        //public DataTable CargaArchivo(string RutaArchivo, string NombreArchivo)
        public DataTable CargaArchivo(string RutaArchivo, bool TieneEncabezado = true)
        {
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = File.OpenRead(RutaArchivo))
                {
                    pck.Load(stream);
                }
                var ws = pck.Workbook.Worksheets.First();
                dtArchivo = new DataTable("Hoja1");

                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    dtArchivo.Columns.Add(TieneEncabezado ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                }

                var startRow = TieneEncabezado ? 2 : 1;
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    DataRow row = dtArchivo.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                }
            }
            return dtArchivo;

            #region Carga de archivo utilizando la paquetería de Microsoft Office
            //OleDbConnection oledbConn = new OleDbConnection();
            //OleDbCommand cmd;
            //OleDbDataAdapter oleda;
            //DataSet ds;
            //ds = new DataSet();
            //string sArchivo = "";

            //try
            //{
            //    sArchivo = RutaArchivo + NombreArchivo;


            //    oledbConn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + sArchivo + ";Extended Properties = 'Excel 12.0;HDR=YES;IMEX=1;'; ");

            //    if (oledbConn != null)
            //    {
            //        oledbConn.Open();
            //        try
            //        {
            //            cmd = new OleDbCommand("SELECT * FROM [Hoja1$]", oledbConn);
            //            oleda = new OleDbDataAdapter();
            //            oleda.SelectCommand = cmd;
            //            oleda.Fill(ds, "Registros");
            //            dtArchivo = ds.Tables[0];
            //        }
            //        finally
            //        {
            //            oledbConn.Close();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    App.ImplementadorMensajes.MostrarMensaje(ex.Message);
            //}
            //if (ds.Tables.Count > 0)
            //    return ds.Tables[0];
            //else
            //    return null;
            #endregion
        }

        static List<T> CrearListaGenerica<T>(T value)
        {
            var list = new List<T>();
            list.Add(value);
            return list;
        }


    }
    #endregion

}
