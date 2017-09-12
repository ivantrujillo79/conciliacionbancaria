using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio
{
    public class ImportacionController : EmisorMensajes
    {
        FuenteInformacion fuenteInformacion;
        string rutaArchivo;
        ILectorEstadoCuentaImplementacion lectorEstadoCuentaImplementacion;
        List<DateTime> fechas = new List<DateTime>();

        public DataColumn[] ObtenerColumnasEstadoCuenta()
        {
            return lectorEstadoCuentaImplementacion.ObtenerColumnas(this.fuenteInformacion, rutaArchivo);

        }

        public ImportacionController(FuenteInformacion fuenteInformacion, string rutaArchivo)
        {
            this.fuenteInformacion = fuenteInformacion;
            this.rutaArchivo = rutaArchivo;
            lectorEstadoCuentaImplementacion = this.FabricaLectores(new FileInfo(rutaArchivo).Extension.ToUpper());
        }

        public bool ImportarArchivo(TablaDestino tablaDestino)
        {
            bool resultado = false;
            if (this.lectorEstadoCuentaImplementacion != null)
            {
                try
                {
                    if (lectorEstadoCuentaImplementacion != null)
                    {

                        DataTable dtEstadoCuenta = this.lectorEstadoCuentaImplementacion.LeerArchivo(fuenteInformacion, rutaArchivo);
                        int rr = dtEstadoCuenta.Rows.Count;
                        int rc = dtEstadoCuenta.Columns.Count;
                        if (dtEstadoCuenta != null)
                        {
                            DataTable dtDestinoDetalle = MapearTablaDestinoDetalleConEstadoCuenta(dtEstadoCuenta);
                            if (dtDestinoDetalle != null)
                            {
                                if (fechas.Count != 0)
                                {
                                    tablaDestino.FInicial = fechas.Min<DateTime>();
                                    tablaDestino.FFinal = fechas.Max<DateTime>();
                                    int x = App.Consultas.VerificarArchivo(tablaDestino.IdCorporativo, tablaDestino.IdSucursal, tablaDestino.Anio, tablaDestino.CuentaBancoFinanciero, tablaDestino.IdTipoFuenteInformacion, tablaDestino.IdFrecuencia, tablaDestino.FInicial, tablaDestino.FFinal);
                                    if (x == 0)
                                    {
                                        tablaDestino.Detalles = LlenarObjetosDestinoDestalle(dtDestinoDetalle, fuenteInformacion.TipoArchivo);
                                        if (tablaDestino.Detalles != null)
                                        {
                                            resultado = GuardaEnTablaDestinoDetalle(tablaDestino);
                                            this.ImplementadorMensajes.MostrarMensaje("La migración se realizó  con éxito.");
                                        }
                                    }
                                    else
                                    {
                                        this.ImplementadorMensajes.MostrarMensaje("No se puede Migrar el archivo, ya existe en el sistema.");
                                    }
                                }
                                else
                                {
                                    this.ImplementadorMensajes.MostrarMensaje("La cuenta no tiene configurado un campo de tipo fecha.");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    resultado = false;
                    stackTrace = new StackTrace();
                    this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                    stackTrace = null;
                }
            }
            return resultado;
        }

        public List<Destino> ObtenerColumnasDestino(string tabla)
        {
            return FiltraCulumnasDestino(App.Consultas.ObtieneDestinoPorTabla(tabla), App.Consultas.ObtieneListaFuenteInoformacionDetallePorId(fuenteInformacion.BancoFinanciero, fuenteInformacion.CuentaBancoFinanciero, fuenteInformacion.IdFuenteInformacion));

        }

        public List<FuenteInformacionDetalle> ObtieneCamposMapeados()
        {
            return App.Consultas.ObtieneListaFuenteInoformacionDetallePorId(fuenteInformacion.BancoFinanciero, fuenteInformacion.CuentaBancoFinanciero, fuenteInformacion.IdFuenteInformacion);
        }

        public List<string> ObtenerCamposEstadoCuenta()
        {
            //return FiltraCulumnasEstadoCuenta(ObtenerListaColumnas(this.ObtenerColumnasEstadoCuenta()), App.Consultas.ObtieneListaFuenteInoformacionDetallePorId(fuenteInformacion.BancoFinanciero, fuenteInformacion.CuentaBancoFinanciero, fuenteInformacion.IdFuenteInformacion));
            return ObtenerListaColumnas(this.ObtenerColumnasEstadoCuenta());
        }

        private List<string> ObtenerListaColumnas(DataColumn[] columnas)
        {
            List<string> lista = new List<string>();
            foreach (DataColumn col in columnas)
            {
                lista.Add(col.ColumnName);
            }
            return lista;
        }

        private ILectorEstadoCuentaImplementacion FabricaLectores(string extension)
        {
            switch (extension)
            {
                case ".TXT":
                    return new LectoresImplementaciones.LectorEstadoCuentaTXT();
                    break;
                case ".XLS":
                    return new LectoresImplementaciones.LectorEstadoCuentaXLS();
                    break;
                case ".XLSX":
                    return new LectoresImplementaciones.LectorEstadoCuentaXLSX();
                    break;
                case ".CSV":
                    return new LectoresImplementaciones.LectorEstadoCuentaCSV();
                    break;
                default: return null;
                    break;
            }
        }

        private bool GuardaEnTablaDestinoDetalle(TablaDestino tablaDestino)
        {
            if (tablaDestino != null)
                return App.Consultas.GuardaListaTablaDestinoDetalle(tablaDestino);
            else
                return false;
        }

        private DataTable MapearTablaDestinoDetalleConEstadoCuenta(DataTable dtEstadoCuenta)
        {

            string nr;

            fechas.Clear();
            List<FuenteInformacionDetalle> columnasMapeo = fuenteInformacion.FuenteInformacionDetalle;
            DataTable dtDestinoDetalle = CrearDataTableDestinoDetalle(App.TablaDestinoDetalle);
            try
            {

                foreach (DataRow filaEstadoCuenta in dtEstadoCuenta.Rows)
                {
                    DataRow campoTabla = dtDestinoDetalle.NewRow();

                    foreach (FuenteInformacionDetalle columnaMapeo in columnasMapeo)
                    {

                        foreach (FuenteInformacionDetalleEtiqueta patron in columnaMapeo.Etiquetas)
                        {
                            string valor = BuscarValor(patron, filaEstadoCuenta[columnaMapeo.ColumnaOrigen.Trim()].ToString());
                            if (!string.IsNullOrEmpty(valor))
                            {
                                campoTabla[patron.Etiqueta.ColumnaDestino] = valor;
                            }
                        }
                        //if (columnaMapeo.ColumnaDestino != "DescripcionInterno")
                        //{
                        //    campoTabla[columnaMapeo.ColumnaDestino] = filaEstadoCuenta[columnaMapeo.ColumnaOrigen.Trim()].ToString();
                        //}

                        /*Es para que nos deje agregar el archivo*/
                        campoTabla[columnaMapeo.ColumnaDestino] = filaEstadoCuenta[columnaMapeo.ColumnaOrigen.Trim()].ToString();

                        if (columnaMapeo.EsTipoFecha)
                        {
                            //nr = filaEstadoCuenta[columnaMapeo.ColumnaOrigen.Trim()].ToString();
                            fechas.Add(Convert.ToDateTime(filaEstadoCuenta[columnaMapeo.ColumnaOrigen.Trim()].ToString()));
                        }

                    }
                    dtDestinoDetalle.Rows.Add(campoTabla);
                }
            }
            catch (Exception ex)
            {
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message + "\n\r" + "Stack :" + ex.StackTrace);
                stackTrace = null;
                return null;

            }
            return dtDestinoDetalle;
        }

        //private string BuscarValor(FuenteInformacionDetalleEtiqueta patron, string texto)
        //{
        //    string valor = string.Empty; ;
        //    string strPatron = patron.Etiqueta.Descripcion.Replace('_', ' ');

        //    if (texto.Contains(strPatron))
        //    {
        //        int inicio = texto.IndexOf(strPatron);

        //        if (patron.LongitudFija > 0)
        //        {
        //            valor = texto.Substring((inicio + strPatron.Length), patron.LongitudFija);
        //        }
        //        else
        //        {
        //            valor = texto.Substring((inicio + strPatron.Length), BuscarFinalPatron(patron, texto));
        //        }
        //    }
        //    return valor;
        //}

        //private string BuscarValor(FuenteInformacionDetalleEtiqueta patron, string texto)
        //{

        //    string valor = string.Empty; ;
        //    string strPatron = patron.Etiqueta.Descripcion.Replace('_', ' ');

        //    if (texto.Contains(strPatron))
        //    {

        //        int inicio = texto.IndexOf(strPatron);
        //        if (patron.LongitudFija > 0)
        //        {
        //            valor = texto.Substring((inicio + strPatron.Length), patron.LongitudFija);
        //        }
        //        else
        //        {
        //            int final = BuscarFinalPatron(patron, texto);
        //            if (final > 0)
        //            {

        //                valor = texto.Substring(inicio + strPatron.Length, final);

        //            }

        //        }

        //    }

        //    return valor;

        //}
        private string BuscarValor(FuenteInformacionDetalleEtiqueta patron, string texto)
        {
            string valor = string.Empty;

            string strPatron = patron.Etiqueta.Descripcion.Replace('_', ' ');
            int i = texto.IndexOf(strPatron);

            if (i != 0)
            {
                strPatron = " " + strPatron;
            }
            //if (texto.Contains(" " + strPatron))
            if (texto.Contains(strPatron))
            {
                int inicio = texto.IndexOf(strPatron);

                if (patron.LongitudFija > 0)
                {
                    int LongitudValor = (texto.Length - (inicio + (patron.Etiqueta.ConcatenaEtiqueta ? 0 : strPatron.Length)));
                    if (LongitudValor >= patron.LongitudFija)
                        valor = patron.Etiqueta.ConcatenaEtiqueta
                            ? texto.Substring((inicio), patron.LongitudFija)
                            : texto.Substring((inicio + strPatron.Length), patron.LongitudFija);
                }
                else
                {
                    int final = BuscarFinalPatron(patron, texto);
                    if (final > 0)
                    {
                        valor = patron.Etiqueta.ConcatenaEtiqueta
                            ? texto.Substring(inicio, final)
                            : texto.Substring(inicio + strPatron.Length, final);
                    }
                }
            }

            return valor.Trim();
        }
        //private int BuscarFinalPatron(FuenteInformacionDetalleEtiqueta patron, string texto)
        //{
        //    string strPatron = patron.Etiqueta.Descripcion.Replace('_', ' ');
        //    int inicio = texto.IndexOf(strPatron);
        //    int fin = texto.IndexOf(patron.Finaliza.Replace('_', ' '), (inicio + strPatron.Length)) - (inicio + strPatron.Length);
        //    return fin;
        //}

        private int BuscarFinalPatron(FuenteInformacionDetalleEtiqueta patron, string texto)
        {
            string strPatron = patron.Etiqueta.Descripcion.Replace('_', ' ');
            int inicio = texto.IndexOf(strPatron);

            int c = texto.IndexOf(patron.Finaliza.Replace('_', ' '), (inicio + (patron.Etiqueta.ConcatenaEtiqueta ? 0 : strPatron.Length)), System.StringComparison.Ordinal);
            int z = (inicio + (patron.Etiqueta.ConcatenaEtiqueta ? 0 : strPatron.Length));
            int fin = c - z;
            return fin;
        }

        private List<string> GetPropiedades(object o)
        {
            List<string> result = new List<string>();
            foreach (MemberInfo mi in o.GetType().GetMembers())
            {
                if (mi.MemberType == MemberTypes.Property)
                {
                    PropertyInfo pi = mi as PropertyInfo;
                    if (pi != null)
                    {
                        result.Add(pi.Name);
                    }
                }
            }
            return result;
        }

        private DataTable CrearDataTableDestinoDetalle(TablaDestinoDetalle destinoDetalle)
        {
            DataTable tabla = new DataTable();
            List<string> propiedades = GetPropiedades(destinoDetalle);
            if (propiedades.Count > 0)
            {
                foreach (string columna in propiedades)
                {
                    tabla.Columns.Add(columna);
                }
            }
            return tabla;
        }

        //private List<TablaDestinoDetalle> LlenarObjetosDestinoDestalle(DataTable dtTablaDestinoDetalle, TipoArchivo tipoArchivo)
        //{
        //    List<TablaDestinoDetalle> listaTablaDestinoDetalle = new List<TablaDestinoDetalle>();
        //    int i = 2;
        //    string columna = string.Empty;
        //    string valor = string.Empty;
        //    try
        //    {

        //        foreach (DataRow registro in dtTablaDestinoDetalle.Rows)
        //        {

        //            TablaDestinoDetalle tablaDestinoDetalle = (TablaDestinoDetalle)App.TablaDestinoDetalle.CrearObjeto();

        //            columna = "CuentaBancaria";
        //            valor = registro["CuentaBancaria"].ToString().Trim();
        //            tablaDestinoDetalle.CuentaBancaria = registro["CuentaBancaria"].ToString().Trim();
        //            columna = "FOperacion";
        //            valor = registro["FOperacion"].ToString().Trim();
        //            tablaDestinoDetalle.FOperacion = (string.IsNullOrEmpty(registro["FOperacion"].ToString().Trim()) || registro["FOperacion"].ToString().ToUpper().Trim() == "NULL") ? null : (Nullable<DateTime>)DateTime.Parse(DateTime.Parse(registro["FOperacion"].ToString().Trim()).ToString(tipoArchivo.FormatoFecha));
        //            columna = "FMovimiento";
        //            valor = registro["FMovimiento"].ToString().Trim();
        //            tablaDestinoDetalle.FMovimiento = (string.IsNullOrEmpty(registro["FMovimiento"].ToString().Trim()) || registro["FMovimiento"].ToString().ToUpper().Trim() == "NULL") ? null : (Nullable<DateTime>)DateTime.Parse(DateTime.Parse(registro["FMovimiento"].ToString().Trim()).ToString(tipoArchivo.FormatoFecha));
        //            columna = "Descripcion";
        //            valor = registro["Descripcion"].ToString().Trim();
        //            tablaDestinoDetalle.Descripcion = registro["Descripcion"].ToString().Trim();
        //            columna = "Referencia";
        //            valor = registro["Referencia"].ToString().Trim();
        //            tablaDestinoDetalle.Referencia = registro["Referencia"].ToString().Trim();
        //            columna = "Transaccion";
        //            valor = registro["Transaccion"].ToString().Trim();
        //            tablaDestinoDetalle.Transaccion = registro["Transaccion"].ToString().Trim();
        //            columna = "SucursalBancaria";
        //            valor = registro["SucursalBancaria"].ToString().Trim();
        //            tablaDestinoDetalle.SucursalBancaria = registro["SucursalBancaria"].ToString().Trim();
        //            columna = "Deposito";
        //            valor = registro["Deposito"].ToString().Trim();
        //            tablaDestinoDetalle.Deposito = string.IsNullOrEmpty(registro["Deposito"].ToString().Trim()) ? 0 : double.Parse(double.Parse(registro["Deposito"].ToString(), System.Globalization.NumberStyles.Number | System.Globalization.NumberStyles.AllowCurrencySymbol).ToString(tipoArchivo.FormatoMoneda), System.Globalization.NumberStyles.Number | System.Globalization.NumberStyles.AllowCurrencySymbol);
        //            columna = "Retiro";
        //            valor = registro["Retiro"].ToString().Trim();
        //            tablaDestinoDetalle.Retiro = string.IsNullOrEmpty(registro["Retiro"].ToString().Trim()) ? 0 : double.Parse(double.Parse(registro["Retiro"].ToString(), System.Globalization.NumberStyles.Number | System.Globalization.NumberStyles.AllowCurrencySymbol).ToString(tipoArchivo.FormatoMoneda), System.Globalization.NumberStyles.Number | System.Globalization.NumberStyles.AllowCurrencySymbol);
        //            columna = "SaldoInicial";
        //            valor = registro["SaldoInicial"].ToString().Trim();
        //            tablaDestinoDetalle.SaldoInicial = string.IsNullOrEmpty(registro["SaldoInicial"].ToString().Trim()) ? 0 : double.Parse(double.Parse(registro["SaldoInicial"].ToString(), System.Globalization.NumberStyles.Number | System.Globalization.NumberStyles.AllowCurrencySymbol).ToString(tipoArchivo.FormatoMoneda), System.Globalization.NumberStyles.Number | System.Globalization.NumberStyles.AllowCurrencySymbol);
        //            columna = "SaldoFinal";
        //            valor = registro["SaldoFinal"].ToString().Trim();
        //            tablaDestinoDetalle.SaldoFinal = string.IsNullOrEmpty(registro["SaldoFinal"].ToString().Trim()) ? 0 : double.Parse(double.Parse(registro["SaldoFinal"].ToString(), System.Globalization.NumberStyles.Number | System.Globalization.NumberStyles.AllowCurrencySymbol).ToString(tipoArchivo.FormatoMoneda), System.Globalization.NumberStyles.Number | System.Globalization.NumberStyles.AllowCurrencySymbol);
        //            columna = "Movimiento";
        //            valor = registro["Movimiento"].ToString().Trim();
        //            tablaDestinoDetalle.Movimiento = registro["Movimiento"].ToString().Trim();
        //            columna = "CuentaTercero";
        //            valor = registro["CuentaTercero"].ToString().Trim();
        //            tablaDestinoDetalle.CuentaTercero = registro["CuentaTercero"].ToString().Trim();
        //            columna = "Cheque";
        //            valor = registro["Cheque"].ToString().Trim();
        //            tablaDestinoDetalle.Cheque = registro["Cheque"].ToString().Trim();
        //            columna = "RFCTercero";
        //            valor = registro["RFCTercero"].ToString().Trim();
        //            tablaDestinoDetalle.RFCTercero = registro["RFCTercero"].ToString().Trim();
        //            columna = "NombreTercero";
        //            valor = registro["NombreTercero"].ToString().Trim();
        //            tablaDestinoDetalle.NombreTercero = registro["NombreTercero"].ToString().Trim();
        //            columna = "ClabeTercero";
        //            valor = registro["ClabeTercero"].ToString().Trim();
        //            tablaDestinoDetalle.ClabeTercero = registro["ClabeTercero"].ToString().Trim();
        //            columna = "Concepto";
        //            valor = registro["Concepto"].ToString().Trim();
        //            tablaDestinoDetalle.Concepto = registro["Concepto"].ToString().Trim();
        //            columna = "Poliza";
        //            valor = registro["Poliza"].ToString().Trim();
        //            tablaDestinoDetalle.Poliza = registro["Poliza"].ToString().Trim();
        //            columna = "IdCaja";
        //            valor = registro["IdCaja"].ToString().Trim();
        //            tablaDestinoDetalle.IdCaja = string.IsNullOrEmpty(registro["IdCaja"].ToString().Trim()) ? 0 : Convert.ToInt32(registro["IdCaja"].ToString());
        //            columna = "IdStatusConcepto";
        //            valor = registro["IdStatusConcepto"].ToString().Trim();
        //            tablaDestinoDetalle.IdStatusConcepto = string.IsNullOrEmpty(registro["IdStatusConcepto"].ToString().Trim()) ? 0 : Convert.ToInt32(registro["IdStatusConcepto"].ToString());
        //            columna = "IdStatusConciliacion";
        //            valor = registro["IdStatusConciliacion"].ToString().Trim();
        //            tablaDestinoDetalle.IdStatusConciliacion = string.IsNullOrEmpty(registro["IdStatusConciliacion"].ToString().Trim()) ? string.Empty : registro["IdStatusConciliacion"].ToString();
        //            columna = "IdConceptoBanco";
        //            valor = registro["IdConceptoBanco"].ToString().Trim();
        //            tablaDestinoDetalle.IdConceptoBanco = string.IsNullOrEmpty(registro["IdConceptoBanco"].ToString().Trim()) ? 0 : Convert.ToInt32(registro["IdConceptoBanco"].ToString());
        //            columna = "IdMotivoNoConciliado";
        //            valor = registro["IdMotivoNoConciliado"].ToString().Trim();
        //            tablaDestinoDetalle.IdMotivoNoConciliado = string.IsNullOrEmpty(registro["IdMotivoNoConciliado"].ToString().Trim()) ? 0 : Convert.ToInt32(registro["IdMotivoNoConciliado"].ToString());

        //            i++;
        //            if ((tablaDestinoDetalle.FOperacion != null) && (tablaDestinoDetalle.FMovimiento != null))
        //                listaTablaDestinoDetalle.Add(tablaDestinoDetalle);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        stackTrace = new StackTrace();
        //        //this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message + "\n\r" + "Linea :" + i.ToString());
        //        this.ImplementadorMensajes.MostrarMensaje("Linea :" + (i).ToString() + "\n\r" + "Columna :" + columna + "  Valor :" + valor + "\n\r" + "Error :" + ex.Message);
        //        stackTrace = null;
        //        return null;
        //    }

        //    return listaTablaDestinoDetalle;

        //}
        private List<TablaDestinoDetalle> LlenarObjetosDestinoDestalle(DataTable dtTablaDestinoDetalle, TipoArchivo tipoArchivo)
        {
            List<TablaDestinoDetalle> listaTablaDestinoDetalle = new List<TablaDestinoDetalle>();
            int i = 2;
            string columna = string.Empty;
            string valor = string.Empty;
            try
            {

                foreach (DataRow registro in dtTablaDestinoDetalle.Rows)
                {
                    TablaDestinoDetalle tablaDestinoDetalle = (TablaDestinoDetalle)App.TablaDestinoDetalle.CrearObjeto();
                    columna = "CuentaBancaria";
                    valor = registro["CuentaBancaria"].ToString().Trim();
                    tablaDestinoDetalle.CuentaBancaria = registro["CuentaBancaria"].ToString().Trim();
                    columna = "FOperacion";
                    valor = registro["FOperacion"].ToString().Trim();
                    tablaDestinoDetalle.FOperacion = (string.IsNullOrEmpty(registro["FOperacion"].ToString().Trim()) || registro["FOperacion"].ToString().ToUpper().Trim() == "NULL") ? null : (Nullable<DateTime>)DateTime.Parse(DateTime.Parse(registro["FOperacion"].ToString().Trim()).ToString(tipoArchivo.FormatoFecha));
                    columna = "FMovimiento";
                    valor = registro["FMovimiento"].ToString().Trim();
                    tablaDestinoDetalle.FMovimiento = (string.IsNullOrEmpty(registro["FMovimiento"].ToString().Trim()) || registro["FMovimiento"].ToString().ToUpper().Trim() == "NULL") ? null : (Nullable<DateTime>)DateTime.Parse(DateTime.Parse(registro["FMovimiento"].ToString().Trim()).ToString(tipoArchivo.FormatoFecha));
                    columna = "Descripcion";
                    valor = registro["Descripcion"].ToString().Trim();
                    tablaDestinoDetalle.Descripcion = registro["Descripcion"].ToString().Trim();
                    columna = "Referencia";
                    valor = registro["Referencia"].ToString().Trim();
                    tablaDestinoDetalle.Referencia = registro["Referencia"].ToString().Trim();
                    columna = "Transaccion";
                    valor = registro["Transaccion"].ToString().Trim();
                    tablaDestinoDetalle.Transaccion = registro["Transaccion"].ToString().Trim();
                    columna = "SucursalBancaria";
                    valor = registro["SucursalBancaria"].ToString().Trim();
                    tablaDestinoDetalle.SucursalBancaria = registro["SucursalBancaria"].ToString().Trim();

                    double prueba;
                    columna = "Deposito";
                    valor = registro["Deposito"].ToString().Trim();

                    if (double.TryParse(Regex.Replace(registro["Deposito"].ToString(), "([^0-9|.])", ""), out prueba))
                        tablaDestinoDetalle.Deposito = string.IsNullOrEmpty(registro["Deposito"].ToString().Trim()) ? 0 : double.Parse(double.Parse(Regex.Replace(registro["Deposito"].ToString(), "([^0-9|.])", ""), System.Globalization.NumberStyles.Number | System.Globalization.NumberStyles.AllowCurrencySymbol).ToString(tipoArchivo.FormatoMoneda), System.Globalization.NumberStyles.Number | System.Globalization.NumberStyles.AllowCurrencySymbol);
                    columna = "Retiro";
                    valor = registro["Retiro"].ToString().Trim();
                    if (double.TryParse(Regex.Replace(registro["Retiro"].ToString(), "([^0-9|.])", ""), out prueba))
                        tablaDestinoDetalle.Retiro = string.IsNullOrEmpty(registro["Retiro"].ToString().Trim()) ? 0 : double.Parse(double.Parse(Regex.Replace(registro["Retiro"].ToString(), "([^0-9|.])", ""), System.Globalization.NumberStyles.Number | System.Globalization.NumberStyles.AllowCurrencySymbol).ToString(tipoArchivo.FormatoMoneda), System.Globalization.NumberStyles.Number | System.Globalization.NumberStyles.AllowCurrencySymbol);
                    columna = "SaldoInicial";
                    valor = registro["SaldoInicial"].ToString().Trim();
                    if (double.TryParse(Regex.Replace(registro["SaldoInicial"].ToString(), "([^0-9|.])", ""), out prueba))
                        tablaDestinoDetalle.SaldoInicial = string.IsNullOrEmpty(registro["SaldoInicial"].ToString().Trim()) ? 0 : double.Parse(double.Parse(Regex.Replace(registro["SaldoInicial"].ToString(), "([^0-9|.])", ""), System.Globalization.NumberStyles.Number | System.Globalization.NumberStyles.AllowCurrencySymbol).ToString(tipoArchivo.FormatoMoneda), System.Globalization.NumberStyles.Number | System.Globalization.NumberStyles.AllowCurrencySymbol);
                    columna = "SaldoFinal";
                    valor = registro["SaldoFinal"].ToString().Trim();
                    if (double.TryParse(Regex.Replace(registro["SaldoFinal"].ToString(), "([^0-9|.])", ""), out prueba))
                        tablaDestinoDetalle.SaldoFinal = string.IsNullOrEmpty(registro["SaldoFinal"].ToString().Trim()) ? 0 : double.Parse(double.Parse(Regex.Replace(registro["SaldoFinal"].ToString(), "([^0-9|.])", ""), System.Globalization.NumberStyles.Number | System.Globalization.NumberStyles.AllowCurrencySymbol).ToString(tipoArchivo.FormatoMoneda), System.Globalization.NumberStyles.Number | System.Globalization.NumberStyles.AllowCurrencySymbol);


                    columna = "Movimiento";
                    valor = registro["Movimiento"].ToString().Trim();
                    tablaDestinoDetalle.Movimiento = registro["Movimiento"].ToString().Trim();
                    columna = "CuentaTercero";
                    valor = registro["CuentaTercero"].ToString().Trim();
                    tablaDestinoDetalle.CuentaTercero = registro["CuentaTercero"].ToString().Trim();
                    columna = "Cheque";
                    valor = registro["Cheque"].ToString().Trim();
                    tablaDestinoDetalle.Cheque = registro["Cheque"].ToString().Trim();
                    columna = "RFCTercero";
                    valor = registro["RFCTercero"].ToString().Trim();
                    tablaDestinoDetalle.RFCTercero = registro["RFCTercero"].ToString().Trim();
                    columna = "NombreTercero";
                    valor = registro["NombreTercero"].ToString().Trim();
                    tablaDestinoDetalle.NombreTercero = registro["NombreTercero"].ToString().Trim();
                    columna = "ClabeTercero";
                    valor = registro["ClabeTercero"].ToString().Trim();
                    tablaDestinoDetalle.ClabeTercero = registro["ClabeTercero"].ToString().Trim();
                    columna = "Concepto";
                    valor = registro["Concepto"].ToString().Trim();
                    tablaDestinoDetalle.Concepto = registro["Concepto"].ToString().Trim();
                    columna = "Poliza";
                    valor = registro["Poliza"].ToString().Trim();
                    tablaDestinoDetalle.Poliza = registro["Poliza"].ToString().Trim();
                    columna = "IdCaja";
                    valor = registro["IdCaja"].ToString().Trim();
                    tablaDestinoDetalle.IdCaja = string.IsNullOrEmpty(registro["IdCaja"].ToString().Trim()) ? 0 : Convert.ToInt32(registro["IdCaja"].ToString());
                    columna = "IdStatusConcepto";
                    valor = registro["IdStatusConcepto"].ToString().Trim();
                    tablaDestinoDetalle.IdStatusConcepto = string.IsNullOrEmpty(registro["IdStatusConcepto"].ToString().Trim()) ? 0 : Convert.ToInt32(registro["IdStatusConcepto"].ToString());
                    columna = "IdStatusConciliacion";
                    valor = registro["IdStatusConciliacion"].ToString().Trim();
                    tablaDestinoDetalle.IdStatusConciliacion = string.IsNullOrEmpty(registro["IdStatusConciliacion"].ToString().Trim()) ? string.Empty : registro["IdStatusConciliacion"].ToString();
                    columna = "IdConceptoBanco";
                    valor = registro["IdConceptoBanco"].ToString().Trim();
                    tablaDestinoDetalle.IdConceptoBanco = string.IsNullOrEmpty(registro["IdConceptoBanco"].ToString().Trim()) ? 0 : Convert.ToInt32(registro["IdConceptoBanco"].ToString());
                    columna = "IdMotivoNoConciliado";
                    valor = registro["IdMotivoNoConciliado"].ToString().Trim();
                    tablaDestinoDetalle.IdMotivoNoConciliado = string.IsNullOrEmpty(registro["IdMotivoNoConciliado"].ToString().Trim()) ? 0 : Convert.ToInt32(registro["IdMotivoNoConciliado"].ToString());

                    i++;
                    if ((tablaDestinoDetalle.FOperacion != null) && (tablaDestinoDetalle.FMovimiento != null))
                        listaTablaDestinoDetalle.Add(tablaDestinoDetalle);
                }
            }
            catch (Exception ex)
            {
                stackTrace = new StackTrace();
                //this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message + "\n\r" + "Linea :" + i.ToString());
                this.ImplementadorMensajes.MostrarMensaje("Linea :" + (i).ToString() + "\n\r" + "Columna :" + columna + "  Valor :" + valor + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
                return null;
            }

            return listaTablaDestinoDetalle;

        }
        private List<Destino> FiltraCulumnasDestino(List<Destino> totaldestinos, List<FuenteInformacionDetalle> listaFuenteInformacionDetalle)
        {
            foreach (FuenteInformacionDetalle fuenteInformacionDetalle in listaFuenteInformacionDetalle)
                totaldestinos.Remove(BuscarItem(totaldestinos, fuenteInformacionDetalle.TablaDestino, fuenteInformacionDetalle.ColumnaDestino));
            return totaldestinos;
        }

        private List<string> FiltraCulumnasEstadoCuenta(List<string> totalColumnasEstadoCuenta, List<FuenteInformacionDetalle> listaFuenteInformacionDetalle)
        {
            foreach (FuenteInformacionDetalle fuenteInformacionDetalle in listaFuenteInformacionDetalle)
            {
                string cam = BuscarItem(totalColumnasEstadoCuenta, fuenteInformacionDetalle.ColumnaOrigen);
                totalColumnasEstadoCuenta.Remove(cam);
            }
            return totalColumnasEstadoCuenta;
        }

        private Destino BuscarItem(List<Destino> totaldestinos, string tabla, string columna)
        {
            return totaldestinos.Find(delegate(Destino destino)
            {
                return ((destino.TablaDestino.Trim().ToUpper() == tabla.Trim().ToUpper()) && (destino.ColumnaDestino.Trim().ToUpper() == columna.Trim().ToUpper()));
            });
        }

        private string BuscarItem(List<string> totalColumnasEstadoCuenta, string columna)
        {
            return totalColumnasEstadoCuenta.Find(delegate(string campoEstadoCuenta)
            {
                return (campoEstadoCuenta.Trim().ToUpper() == columna.Trim().ToUpper());
            });
        }

    }
}
