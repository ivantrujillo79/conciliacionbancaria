using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.Migracion.Runtime.ReglasNegocio;
using System.Data.SqlClient;
using System.Diagnostics;
using Conciliacion.RunTime.ReglasDeNegocio;
using Consultas = Conciliacion.Migracion.Runtime.ReglasNegocio.Consultas;

namespace Conciliacion.Migracion.Runtime.SqlDatos
{
    public class ConsultasDatos : Consultas
    {


        public override TipoFuente ConsultaTipoFuentePorId(int id)
        {
            throw new NotImplementedException();
        }

        public override TipoFuenteInformacion ObtieneTipoFuenteDeInformacionePorId(int id)
        {

            TipoFuenteInformacion tipoFuenteInformacion = new TipoFuenteInformacionDatos();
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBObtieneTipoFuenteInformacion", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@TipoFuenteInformacion", System.Data.SqlDbType.SmallInt).Value = id;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        tipoFuenteInformacion.Id = Convert.ToInt32(reader["TipoFuenteInformacion"]);
                        tipoFuenteInformacion.Descripcion = reader["Descripcion"].ToString();
                        tipoFuenteInformacion.IdTipoFuente = Convert.ToInt32(reader["TipoFuente"]);
                    }
                    cnn.Close();
                }
            }
            catch (SqlException ex)
            {

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return tipoFuenteInformacion;
        }


        public override Sucursal ObtieneSucursalPorId(int id)
        {
            throw new NotImplementedException();
        }

        public override TipoArchivo ObtieneTipoArchivoPorId(int id)
        {
            TipoArchivo tipoArchivo = new TipoArchivoDatos();
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBObtieneTipoArchivo", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@TipoArchivo", System.Data.SqlDbType.SmallInt).Value = id;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        tipoArchivo.IdTipoArchivo = Convert.ToInt32(reader["TipoArchivo"]);
                        tipoArchivo.Descripcion = reader["Descripcion"].ToString();
                        tipoArchivo.FormatoFecha = reader["FormatoDeFecha"].ToString();
                        tipoArchivo.FormatoMoneda = reader["FormatoDeMoneda"].ToString();
                        tipoArchivo.Separador = reader["Separador"].ToString();
                        tipoArchivo.Usuario = reader["Usuario"].ToString();
                        tipoArchivo.Status = reader["Status"].ToString();
                        tipoArchivo.FAlta = Convert.ToDateTime(reader["FAlta"]);

                    }
                }
            }
            catch (SqlException ex)
            {

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return tipoArchivo;
        }


        public override FuenteInformacion ObtieneFuenteInformacionPorId(int idBanco, string idCuenta, int id)
        {
            FuenteInformacion fuenteInformacion = new FuenteInformacionDatos();
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBObtieneFuenteInformacion", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CuentaBancoFinanciero", System.Data.SqlDbType.VarChar).Value = idCuenta;
                    cmd.Parameters.Add("@BancoFinanciero", System.Data.SqlDbType.SmallInt).Value = idBanco;
                    cmd.Parameters.Add("@FuenteInformacion", System.Data.SqlDbType.Int).Value = id;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        fuenteInformacion.CuentaBancoFinanciero = reader["CuentaBancoFinanciero"].ToString();
                        fuenteInformacion.BancoFinanciero = Convert.ToInt32(reader["BancoFinanciero"]);
                        fuenteInformacion.IdFuenteInformacion = Convert.ToInt32(reader["FuenteInformacion"]);
                        fuenteInformacion.RutaArchivo = reader["RutaArchivo"].ToString();
                        fuenteInformacion.IdTipoFuenteInformacion = Convert.ToInt32(reader["TipoFuenteInformacion"]);
                        fuenteInformacion.IdSucursal = Convert.ToInt32(reader["Sucursal"]);
                        fuenteInformacion.NumColumnas = Convert.ToInt32(reader["NumColumnas"]);
                        fuenteInformacion.IdTipoArchivo = Convert.ToInt32(reader["TipoArchivo"]);


                    }
                }
            }
            catch (SqlException ex)
            {

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return fuenteInformacion;
        }

        public override Consultas CrearObjeto()
        {
            throw new NotImplementedException();
        }

        public override Corporativo ObtieneCorporativoPorId(int id)
        {
            Corporativo corporativo = new CorporativoDatos();
            return corporativo;
        }

        public override Frecuencia ObtieneFrecuenciaPorId(int id)
        {
            Frecuencia frecuencia = new FrecuenciaDatos();
            return frecuencia;
        }

        public override ConceptoBancoGrupo ObtieneConceptoBancoGrupoPorId(int id)
        {
            throw new NotImplementedException();
        }

        public override Conciliacion.Migracion.Runtime.ReglasNegocio.Caja ObtieneCajaPorId(int id)
        {
            throw new NotImplementedException();
        }

        public override StatusConcepto ObtieneStatusConceptoPorId(int id)
        {
            throw new NotImplementedException();
        }

        public override List<ListaCombo> ObtieneListaTipoTransferencia()
        {
            List<ListaCombo> Datos = new List<ListaCombo>();
            using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBCargaComboTipoTransferencia", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.SmallInt).Value = 0;

                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        ListaCombo dato = new ListaCombo(Convert.ToInt32(reader["TipoTransferencia"]),
                            Convert.ToString(reader["Descripcion"]));
                        Datos.Add(dato);
                    }

                }
                catch (SqlException ex)
                {
                    stackTrace = new StackTrace();
                    this.ImplementadorMensajes.MostrarMensaje("Error al consultar la información.\n\rClase :" +
                                                              this.GetType().Name + "\n\r" + "Metodo :" +
                                                              stackTrace.GetFrame(0).GetMethod().Name + "\n\r" +
                                                              "Error :" + ex.Message);
                    stackTrace = null;
                }
                return Datos;
            }
        }

        public override StatusConciliacion ObtieneStatusConciliacionPorId(int id)
        {
            throw new NotImplementedException();
        }

        public override ConceptoBanco ObtieneConceptoBancoPorId(int id)
        {
            throw new NotImplementedException();
        }

        public override MotivoNoConciliado ObtieneMotivoNoConciliadoPorId(int id)
        {
            throw new NotImplementedException();
        }

        public override Destino ObtieneDestinoPorId(string tablaDestino, string columnaDestino)
        {
            Destino destino = new DestinoDatos();
            return destino;
        }

        public override List<FuenteInformacionDetalle> ObtieneListaFuenteInoformacionDetallePorId(int idBanco, string idCuenta, int id)
        {
            List<FuenteInformacionDetalle> detalles = new List<FuenteInformacionDetalle>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBObtieneListaFuenteInformacionDetalle", cnn);
                    cmd.Parameters.Add("@CuentaBancoFinanciero", System.Data.SqlDbType.VarChar).Value = idCuenta;
                    cmd.Parameters.Add("@BancoFinanciero", System.Data.SqlDbType.SmallInt).Value = idBanco;
                    cmd.Parameters.Add("@FuenteInformacion", System.Data.SqlDbType.Int).Value = id;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        FuenteInformacionDetalle detalle = new FuenteInformacionDetalleDatos();
                        detalle.CuentaBancoFinanciero = reader["CuentaBancoFinanciero"].ToString();
                        detalle.BancoFinanciero = Convert.ToInt32(reader["BancoFinanciero"]);
                        detalle.IdFuenteInformacion = Convert.ToInt32(reader["FuenteInformacion"]);
                        detalle.Secuencia = Convert.ToInt32(reader["Secuencia"]);
                        detalle.ColumnaOrigen = reader["ColumnaOrigen"].ToString();
                        detalle.TablaDestino = reader["TablaDestino"].ToString();
                        detalle.ColumnaDestino = reader["ColumnaDestino"].ToString();
                        detalle.IdConceptoBanco = Convert.ToInt32(reader["ConceptoBanco"]);
                        detalle.EsTipoFecha = reader["EsTipoFecha"] == DBNull.Value ? false : Convert.ToBoolean(reader["EsTipoFecha"]);
                        detalles.Add(detalle);
                    }
                }
            }
            catch (SqlException ex)
            {

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return detalles;
        }

        public override void ActualizarClientePago(TablaDestinoDetalle TablaDestinoDetalle)
        {
            using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
            {
                cnn.Open();
                try
                {
                    TablaDestinoDetalleDatos tabladestinodetalle = new TablaDestinoDetalleDatos();
                    tabladestinodetalle.Anio = TablaDestinoDetalle.Anio;
                    tabladestinodetalle.IdCorporativo = TablaDestinoDetalle.IdCorporativo;
                    tabladestinodetalle.IdSucursal = TablaDestinoDetalle.IdSucursal;
                    tabladestinodetalle.Folio = TablaDestinoDetalle.Folio;
                    tabladestinodetalle.Secuencia = TablaDestinoDetalle.Secuencia;
                    tabladestinodetalle.ClientePago = TablaDestinoDetalle.ClientePago;

                    tabladestinodetalle.ActualizarClientePago(cnn);

                }
                catch (Exception ex)
                {
                    stackTrace = new StackTrace();
                    //#if (TestProyect)
                    this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                    //#else
                    stackTrace = null;
                    //#endif
                }
            }
        }

        public override int ExisteClientePago(TablaDestinoDetalle TablaDestinoDetalle)
        {
            int resultado = 0;
            using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
            {
                cnn.Open();
                try
                {
                    TablaDestinoDetalleDatos tabladestinodetalle = new TablaDestinoDetalleDatos();
                    tabladestinodetalle.Anio = TablaDestinoDetalle.Anio;
                    tabladestinodetalle.IdCorporativo = TablaDestinoDetalle.IdCorporativo;
                    tabladestinodetalle.IdSucursal = TablaDestinoDetalle.IdSucursal;
                    tabladestinodetalle.Folio = TablaDestinoDetalle.Folio;
                    tabladestinodetalle.Secuencia = TablaDestinoDetalle.Secuencia;
                    tabladestinodetalle.ClientePago = TablaDestinoDetalle.ClientePago;

                    resultado = tabladestinodetalle.ExisteClientePago(cnn);

                }
                catch (Exception ex)
                {
                    stackTrace = new StackTrace();
                    this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                    stackTrace = null;
                }
            }
            return resultado;
        }

        public override bool GuardaListaTablaDestinoDetalle(TablaDestino tabla)
        {
            bool resultado = false;
            int secuencia = 1;

            using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
            {
                cnn.Open();
                SqlTransaction transaction = cnn.BeginTransaction();
                try
                {

                    SqlCommand cmd = new SqlCommand("spCBGuardaTablaDestino", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Transaction = transaction;
                    cmd.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = tabla.IdCorporativo;
                    cmd.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = tabla.IdSucursal;
                    cmd.Parameters.Add("@Año", System.Data.SqlDbType.Int).Value = tabla.Anio;
                    cmd.Parameters.Add("@Folio", System.Data.SqlDbType.Int).Value = tabla.Folio;
                    cmd.Parameters.Add("@CuentaBancoFinanciero", System.Data.SqlDbType.VarChar).Value = tabla.CuentaBancoFinanciero;
                    cmd.Parameters.Add("@TipoFuenteInformacion", System.Data.SqlDbType.SmallInt).Value = tabla.IdTipoFuenteInformacion;
                    cmd.Parameters.Add("@Frecuencia", System.Data.SqlDbType.SmallInt).Value = tabla.IdFrecuencia;
                    cmd.Parameters.Add("@FInicial", System.Data.SqlDbType.DateTime).Value = tabla.FInicial;
                    cmd.Parameters.Add("@FFinal", System.Data.SqlDbType.DateTime).Value = tabla.FFinal;
                    cmd.Parameters.Add("@StatusConciliacion", System.Data.SqlDbType.VarChar).Value = tabla.IdStatusConciliacion;
                    cmd.Parameters.Add("@Usuario", System.Data.SqlDbType.Char).Value = tabla.Usuario;
                    cmd.Parameters.Add("@FAlta", System.Data.SqlDbType.DateTime).Value = tabla.FAlta;
                    cmd.ExecuteNonQuery();
                    foreach (TablaDestinoDetalle tablaDestinoDetalle in tabla.Detalles)
                    {
                        tablaDestinoDetalle.IdCorporativo = tabla.IdCorporativo;
                        tablaDestinoDetalle.IdSucursal = tabla.IdSucursal;
                        tablaDestinoDetalle.Anio = tabla.Anio;
                        tablaDestinoDetalle.Folio = tabla.Folio;
                        tablaDestinoDetalle.IdStatusConciliacion = tabla.IdStatusConciliacion;
                        SqlCommand cmd2 = new SqlCommand("spCBGuardaTablaDestinoDetalle", cnn);
                        cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd2.Transaction = transaction;
                        cmd2.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = tablaDestinoDetalle.IdCorporativo;
                        cmd2.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = tablaDestinoDetalle.IdSucursal;
                        cmd2.Parameters.Add("@Año", System.Data.SqlDbType.Int).Value = tablaDestinoDetalle.Anio;
                        cmd2.Parameters.Add("@Folio", System.Data.SqlDbType.Int).Value = tablaDestinoDetalle.Folio;
                        cmd2.Parameters.Add("@Secuencia", System.Data.SqlDbType.Int).Value = secuencia;
                        cmd2.Parameters.Add("@CuentaBancaria", System.Data.SqlDbType.VarChar).Value = tablaDestinoDetalle.CuentaBancaria;
                        cmd2.Parameters.Add("@FOperacion", System.Data.SqlDbType.DateTime).Value = tablaDestinoDetalle.FOperacion;
                        cmd2.Parameters.Add("@FMovimiento", System.Data.SqlDbType.DateTime).Value = tablaDestinoDetalle.FMovimiento;
                        cmd2.Parameters.Add("@Referencia", System.Data.SqlDbType.VarChar).Value = tablaDestinoDetalle.Referencia;
                        cmd2.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar).Value = tablaDestinoDetalle.Descripcion;
                        cmd2.Parameters.Add("@Transaccion", System.Data.SqlDbType.VarChar).Value = tablaDestinoDetalle.Transaccion;
                        cmd2.Parameters.Add("@SucursalBancaria", System.Data.SqlDbType.VarChar).Value = tablaDestinoDetalle.SucursalBancaria;
                        cmd2.Parameters.Add("@Deposito", System.Data.SqlDbType.Money).Value = tablaDestinoDetalle.Deposito;
                        cmd2.Parameters.Add("@Retiro", System.Data.SqlDbType.Money).Value = tablaDestinoDetalle.Retiro;
                        cmd2.Parameters.Add("@SaldoInicial", System.Data.SqlDbType.Money).Value = tablaDestinoDetalle.SaldoInicial;
                        cmd2.Parameters.Add("@SaldoFinal", System.Data.SqlDbType.Money).Value = tablaDestinoDetalle.SaldoFinal;
                        cmd2.Parameters.Add("@Movimiento", System.Data.SqlDbType.VarChar).Value = tablaDestinoDetalle.FMovimiento;
                        cmd2.Parameters.Add("@CuentaTercero", System.Data.SqlDbType.VarChar).Value = tablaDestinoDetalle.CuentaTercero;
                        cmd2.Parameters.Add("@Cheque", System.Data.SqlDbType.VarChar).Value = tablaDestinoDetalle.Cheque;
                        cmd2.Parameters.Add("@RFCTercero", System.Data.SqlDbType.VarChar).Value = tablaDestinoDetalle.RFCTercero;
                        cmd2.Parameters.Add("@NombreTercero", System.Data.SqlDbType.VarChar).Value = tablaDestinoDetalle.NombreTercero;
                        cmd2.Parameters.Add("@ClabeTercero", System.Data.SqlDbType.VarChar).Value = tablaDestinoDetalle.ClabeTercero;
                        cmd2.Parameters.Add("@Concepto", System.Data.SqlDbType.VarChar).Value = tablaDestinoDetalle.Concepto;
                        cmd2.Parameters.Add("@Poliza", System.Data.SqlDbType.VarChar).Value = tablaDestinoDetalle.Poliza;

                        if (tablaDestinoDetalle.IdCaja == 0)
                            cmd2.Parameters.Add("@Caja", System.Data.SqlDbType.TinyInt).Value = DBNull.Value;
                        else
                            cmd2.Parameters.Add("@Caja", System.Data.SqlDbType.TinyInt).Value = tablaDestinoDetalle.IdCaja;

                        if (tablaDestinoDetalle.IdStatusConcepto == 0)
                            cmd2.Parameters.Add("@StatusConcepto", System.Data.SqlDbType.SmallInt).Value = DBNull.Value;
                        else
                            cmd2.Parameters.Add("@StatusConcepto", System.Data.SqlDbType.SmallInt).Value = tablaDestinoDetalle.IdStatusConcepto;


                        cmd2.Parameters.Add("@StatusConciliacion", System.Data.SqlDbType.VarChar).Value = tablaDestinoDetalle.IdStatusConciliacion;

                        if (tablaDestinoDetalle.IdConceptoBanco == 0)
                            cmd2.Parameters.Add("@ConceptoBanco", System.Data.SqlDbType.SmallInt).Value = DBNull.Value;
                        else
                            cmd2.Parameters.Add("@ConceptoBanco", System.Data.SqlDbType.SmallInt).Value = tablaDestinoDetalle.IdConceptoBanco;

                        if (tablaDestinoDetalle.IdMotivoNoConciliado == 0)
                            cmd2.Parameters.Add("@MotivoNoConciliado", System.Data.SqlDbType.SmallInt).Value = DBNull.Value;
                        else
                            cmd2.Parameters.Add("@MotivoNoConciliado", System.Data.SqlDbType.SmallInt).Value = tablaDestinoDetalle.IdMotivoNoConciliado;

                        cmd2.Parameters.Add("@TipoFuenteInformacion", System.Data.SqlDbType.SmallInt).Value = tablaDestinoDetalle.TipoFuenteInformacion;
                        cmd2.ExecuteNonQuery();
                        secuencia++;
                    }
                    resultado = true;
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    stackTrace = new StackTrace();
                    this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                    stackTrace = null;
                    resultado = false;
                }
            }
            return resultado;
        }


        public override int ObtieneTipoArchivoNumeroMaximo()
        {
            int numero = 0;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBObtieneNumeroMaximoTipoArchivo", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    numero = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return numero;
        }

        public override List<Separador> ObtieneListaSeparador()
        {
            List<Separador> separadores = new List<Separador>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBObtieneListaSeparadores", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Separador separador = new SeparadorDatos();
                        separador.Descripcion = reader["Separador"].ToString();
                        separador.Status = reader["Status"].ToString();
                        separadores.Add(separador);
                    }
                }
            }
            catch (SqlException ex)
            {

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return separadores;

        }

        public override List<TipoFuenteInformacion> ObtieneListaTipoFuenteDeInformacion()
        {

            List<TipoFuenteInformacion> lista = new List<TipoFuenteInformacion>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBObtieneListaTipoFuenteInformacion", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        TipoFuenteInformacion tipoFuenteInformacion = new TipoFuenteInformacionDatos();
                        tipoFuenteInformacion.Id = Convert.ToInt32(reader["TipoFuenteInformacion"]);
                        tipoFuenteInformacion.Descripcion = reader["Descripcion"].ToString();
                        tipoFuenteInformacion.IdTipoFuente = Convert.ToInt32(reader["TipoFuente"]);
                        lista.Add(tipoFuenteInformacion);
                    }
                }
            }
            catch (SqlException ex)
            {
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return lista;
        }

        public override List<TipoArchivo> ObtieneListaTipoArchivo()
        {


            List<TipoArchivo> lista = new List<TipoArchivo>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBObtieneListaTipoArchivo", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        TipoArchivo tipoArchivo = new TipoArchivoDatos();
                        tipoArchivo.IdTipoArchivo = Convert.ToInt32(reader["TipoArchivo"]);
                        tipoArchivo.Descripcion = reader["Descripcion"].ToString();
                        tipoArchivo.FormatoFecha = reader["FormatoDeFecha"].ToString();
                        tipoArchivo.FormatoMoneda = reader["FormatoDeMoneda"].ToString();
                        tipoArchivo.Separador = reader["Separador"].ToString();
                        tipoArchivo.Usuario = reader["Usuario"].ToString();
                        tipoArchivo.Status = reader["Status"].ToString();
                        tipoArchivo.FAlta = Convert.ToDateTime(reader["FAlta"]);
                        lista.Add(tipoArchivo);
                    }
                }
            }
            catch (SqlException ex)
            {

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return lista;
        }

        public override int ObtieneFuenteInformacionNumeroMaximo(int idBanco, string idCuenta)
        {
            int numero = 0;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBObtieneNumeroMaximoFuenteInformacion", cnn);
                    cmd.Parameters.Add("@CuentaBancoFinanciero", System.Data.SqlDbType.VarChar).Value = idCuenta;
                    cmd.Parameters.Add("@BancoFinanciero", System.Data.SqlDbType.SmallInt).Value = idBanco;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    numero = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return numero;
        }

        public override List<BancoFinanciero> ObtieneListaBancoFinanciero(int corporativo)
        {
            List<BancoFinanciero> lista = new List<BancoFinanciero>();

            List<Conciliacion.RunTime.ReglasDeNegocio.ListaCombo> listaCombo = Conciliacion.RunTime.App.Consultas.ConsultaBancos(corporativo);

            foreach (Conciliacion.RunTime.ReglasDeNegocio.ListaCombo item in listaCombo)
            {
                BancoFinanciero banco = new BancoFinancieroDatos();
                banco.Id = item.Identificador;
                banco.Descripcion = item.Descripcion;
                lista.Add(banco);
            }
            return lista;
        }

        public override List<StatusConcepto> ConsultaStatusConcepto(Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionStatusConcepto configuracion)
        {
            List<StatusConcepto> datos = new List<StatusConcepto>();

            using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBCargaComboStatusConcepto", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.SmallInt).Value = configuracion;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {

                        StatusConcepto dato = new StatusConceptoDatos(Convert.ToInt32(reader["Identificador"]), Convert.ToString(reader["Descripcion"]), Convert.ToString(reader["Usuario"]), Convert.ToString(reader["Status"]), Convert.ToDateTime(reader["FAlta"]), this.implementadorMensajes);
                        datos.Add(dato);
                    }
                }
                catch (SqlException ex)
                {
                    stackTrace = new StackTrace();
                    this.ImplementadorMensajes.MostrarMensaje("Error al consultar la información.\n\rClase :" + this.GetType().Name + "\n\r" + "Método :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                    stackTrace = null;
                }
                return datos;
            }
        }
        public override List<CuentaFinanciero> ObtieneListaCuentaFinancieroPorBanco(int corporativo, int idBanco)
        {

            List<CuentaFinanciero> lista = new List<CuentaFinanciero>();

            List<Conciliacion.RunTime.ReglasDeNegocio.ListaCombo> listaCombo = Conciliacion.RunTime.App.Consultas.ConsultaCuentasBancaria(corporativo, Convert.ToInt16(idBanco));

            foreach (Conciliacion.RunTime.ReglasDeNegocio.ListaCombo item in listaCombo)
            {
                CuentaFinanciero cuenta = new CuentaFinancieroDatos();
                cuenta.Id = item.Identificador;
                cuenta.Descripcion = item.Descripcion;
                lista.Add(cuenta);
            }
            return lista;

        }

        public override List<CuentaFinanciero> ConsultaCuentaExistenteFuenteInformacion(short idBanco, string idCuentaBancaria, short idFuenteInformacion)
        {

            List<CuentaFinanciero> datos = new List<CuentaFinanciero>();

            using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBConsultaCuentaExistenteFuenteInformacion", cnn);
                    comando.Parameters.Add("@Banco", System.Data.SqlDbType.SmallInt).Value = idBanco;
                    comando.Parameters.Add("@CuentaBancaria", System.Data.SqlDbType.VarChar).Value = idCuentaBancaria;
                    comando.Parameters.Add("@TipoFuente", System.Data.SqlDbType.SmallInt).Value = idFuenteInformacion;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {

                        CuentaFinanciero cuenta = new CuentaFinancieroDatos();
                        cuenta.Id = Convert.ToInt32(reader["Identificador"]);
                        cuenta.Descripcion = Convert.ToString(reader["Descripcion"]);
                        datos.Add(cuenta);

                    }
                }
                catch (SqlException ex)
                {
                    stackTrace = new StackTrace();
                    this.ImplementadorMensajes.MostrarMensaje("Error al consultar la información.\n\rClase :" + this.GetType().Name + "\n\r" + "Método :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                    stackTrace = null;
                }
                return datos;
            }

        }

        public override List<string> ObtieneListaDiferentesTablasDestino()
        {
            List<string> lista = new List<string>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBObtieneDistintasTablasDestino", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string tabla = reader["Tabla"].ToString();
                        lista.Add(tabla);
                    }
                }
            }
            catch (SqlException ex)
            {

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return lista;
        }

        public override List<Destino> ObtieneDestinoPorTabla(string tablaDestino)
        {
            List<Destino> lista = new List<Destino>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBObtieneDestinosPorTabla", cnn);
                    cmd.Parameters.Add("@Tabla", System.Data.SqlDbType.VarChar).Value = tablaDestino;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Destino desino = new DestinoDatos();
                        desino.TablaDestino = reader["TablaDestino"].ToString();
                        desino.ColumnaDestino = reader["ColumnaDestino"].ToString();
                        desino.IdTipoDato = reader["TipoDato"].ToString();
                        desino.TablaCabecera = reader["TablaCabecera"].ToString();
                        lista.Add(desino);
                    }
                }
            }
            catch (SqlException ex)
            {

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return lista;
        }

        public override List<FuenteInformacion> ObtieneListaFuenteInformacionPorBancoCuenta(int banco, string cuenta)
        {
            List<FuenteInformacion> lista = new List<FuenteInformacion>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBObtieneListaFuenteInformacion", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CuentaBancoFinanciero", System.Data.SqlDbType.VarChar).Value = cuenta;
                    cmd.Parameters.Add("@BancoFinanciero", System.Data.SqlDbType.SmallInt).Value = banco;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        FuenteInformacion fuenteInformacion = new FuenteInformacionDatos();
                        fuenteInformacion.CuentaBancoFinanciero = reader["CuentaBancoFinanciero"].ToString();
                        fuenteInformacion.BancoFinanciero = Convert.ToInt32(reader["BancoFinanciero"]);
                        fuenteInformacion.IdFuenteInformacion = Convert.ToInt32(reader["FuenteInformacion"]);
                        fuenteInformacion.RutaArchivo = reader["RutaArchivo"].ToString();
                        fuenteInformacion.IdTipoFuenteInformacion = Convert.ToInt32(reader["TipoFuenteInformacion"]);
                        fuenteInformacion.IdSucursal = Convert.ToInt32(reader["Sucursal"]);
                        fuenteInformacion.NumColumnas = Convert.ToInt32(reader["NumColumnas"]);
                        fuenteInformacion.IdTipoArchivo = Convert.ToInt32(reader["TipoArchivo"]);
                        fuenteInformacion.DesTipoFuenteInformacion = reader["Descripcion"].ToString();
                        lista.Add(fuenteInformacion);
                    }
                }
            }
            catch (SqlException ex)
            {

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return lista;
        }

        public override int ObtieneFuenteInformacionDetalleNumeroMaximo(int idBanco, string idCuenta, int idFuenteInformacion)
        {
            int numero = 0;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBObtieneNumeroMaximoFuenteInformacionDetalle", cnn);
                    cmd.Parameters.Add("@CuentaBancoFinanciero", System.Data.SqlDbType.VarChar).Value = idCuenta;
                    cmd.Parameters.Add("@BancoFinanciero", System.Data.SqlDbType.SmallInt).Value = idBanco;
                    cmd.Parameters.Add("@FuenteInformacion", System.Data.SqlDbType.Int).Value = idFuenteInformacion;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    numero = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return numero;
        }

        public override StatusConciliacion ObtieneStatusConciliacionPorId(string id)
        {
            StatusConciliacion status = new StatusConciliacionDatos();
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBObtieneStatusConciliacion", cnn);
                    cmd.Parameters.Add("@StatusConciliacion", System.Data.SqlDbType.VarChar).Value = id;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        status.Id = reader["StatusConciliacion"].ToString();
                        status.Usuario = reader["Usuario"].ToString();
                        status.Status = reader["Status"].ToString();
                        status.FAlta = Convert.ToDateTime(reader["FAlta"]);
                    }
                }
            }
            catch (SqlException ex)
            {

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return status;
        }

        public override List<StatusConciliacion> ObtieneListaStatusConciliacion()
        {
            List<StatusConciliacion> lista = new List<StatusConciliacion>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBObtieneListaStatusConciliacion", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        StatusConciliacion statusConciliacion = new StatusConciliacionDatos();
                        statusConciliacion.Id = reader["StatusConciliacion"].ToString();
                        statusConciliacion.Usuario = reader["Usuario"].ToString();
                        statusConciliacion.Status = reader["Status"].ToString();
                        statusConciliacion.FAlta = Convert.ToDateTime(reader["FAlta"]);
                        lista.Add(statusConciliacion);
                    }
                }
            }
            catch (SqlException ex)
            {

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return lista;
        }

        public override List<Frecuencia> ObtieneListaFrecuencia()
        {

            List<Frecuencia> lista = new List<Frecuencia>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBObtieneListaFrecuencia", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Frecuencia frecuencia = new FrecuenciaDatos();
                        frecuencia.Id = Convert.ToInt32(reader["Frecuencia"]);
                        frecuencia.Descripcion = reader["Descripcion"].ToString();
                        lista.Add(frecuencia);
                    }
                }
            }
            catch (SqlException ex)
            {

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return lista;
        }

        public override int ObtieneTablaDestinoNumeroMaximo(int corporativo, int sucursal, int anio)
        {
            int numero = 0;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBObtieneNumeroMaximoTablaDestino", cnn);
                    cmd.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = corporativo;
                    cmd.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = sucursal;
                    cmd.Parameters.Add("@Año", System.Data.SqlDbType.Int).Value = anio;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    numero = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return numero;

        }

        public override List<ConceptoBanco> ObtieneListaConceptoBanco()
        {
            List<ConceptoBanco> lista = new List<ConceptoBanco>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBObtieneListaConceptoBanco", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ConceptoBanco concepto = new ConceptoBancoDatos();
                        concepto.Id = Convert.ToInt32(reader["ConceptoBanco"]);
                        concepto.Descripcion = reader["Descripcion"].ToString();
                        concepto.IdConceptoBancoGrupo = Convert.ToInt32(reader["ConceptoBancoGrupo"]);
                        concepto.Status = reader["Status"].ToString();
                        concepto.Usuario = reader["Usuario"].ToString();
                        concepto.FAlta = Convert.ToDateTime(reader["FAlta"]);
                        lista.Add(concepto);
                    }
                }
            }
            catch (SqlException ex)
            {

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return lista;
        }

        public override List<Sucursal> ObtieneListaSucursalPorCorporativo(int corporativo)
        {
            List<Sucursal> lista = new List<Sucursal>();

            List<Conciliacion.RunTime.ReglasDeNegocio.ListaCombo> listaCombo = Conciliacion.RunTime.App.Consultas.ConsultaSucursales(Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionIden0.Con0, corporativo);

            foreach (Conciliacion.RunTime.ReglasDeNegocio.ListaCombo item in listaCombo)
            {
                Sucursal cuenta = new SucursalDatos();
                cuenta.Id = item.Identificador;
                cuenta.Descripcion = item.Descripcion;
                lista.Add(cuenta);
            }
            return lista;
        }

        //public override List<Sucursal> ObtieneListaSucursal()
        //{
        //    List<Sucursal> lista = new List<Sucursal>();
        //    Sucursal sucursal = new SucursalDatos();
        //    sucursal.Id = 1;
        //    sucursal.Descripcion = "Sucursal 1";
        //    sucursal.Siglas = "s1";
        //    lista.Add(sucursal);

        //    Sucursal sucursal2 = new SucursalDatos();
        //    sucursal2.Id = 2;
        //    sucursal2.Descripcion = "Sucursal 2";
        //    sucursal2.Siglas = "s2";
        //    lista.Add(sucursal2);

        //    Sucursal sucursal3 = new SucursalDatos();
        //    sucursal3.Id = 3;
        //    sucursal3.Descripcion = "Sucursal 3";
        //    sucursal3.Siglas = "s3";
        //    lista.Add(sucursal3);

        //    return lista;
        //}

        public override List<Corporativo> ObtieneListaCorporativo(string usuario)
        {
            List<Corporativo> lista = new List<Corporativo>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBCorporativosUsuario", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Usuario", System.Data.SqlDbType.VarChar, 15).Value = usuario;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Corporativo concepto = new CorporativoDatos();
                        concepto.Id = Convert.ToInt32(reader["Corporativo"]);
                        concepto.Nombre = reader["NombreCorporativo"].ToString();
                        //concepto.su = reader["Inicial"].ToString();
                        lista.Add(concepto);
                    }
                }
            }
            catch (SqlException ex)
            {
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return lista;
        }

        public override List<int> ObtieneListaAnios()
        {
            List<int> lista = new List<int>();
            List<Conciliacion.RunTime.ReglasDeNegocio.ListaCombo> listaCombo = Conciliacion.RunTime.App.Consultas.ConsultaAños();
            foreach (Conciliacion.RunTime.ReglasDeNegocio.ListaCombo item in listaCombo)
            {
                lista.Add(Convert.ToInt32(item.Descripcion));
            }
            return lista;
        }

#region Etiqueta

        public override Etiqueta ObtieneEtiquetaPorId(int id)
        {
            Etiqueta etiqueta = new EtiquetaDatos();
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBObtieneEtiqueta", cnn);
                    cmd.CommandTimeout = 1800;
                    cmd.Parameters.Add("@Etiqueta", System.Data.SqlDbType.Int).Value = id;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        etiqueta.Id = Convert.ToInt32(reader["Etiqueta"]);
                        etiqueta.IdBancoFinanciero = Convert.ToInt32(reader["BancoFinanciero"]);
                        etiqueta.IdTipoFuenteInformacion = Convert.ToInt32(reader["TipoFuenteInformacion"]);
                        etiqueta.Descripcion = reader["Decripcion"].ToString();
                        etiqueta.TipoDato = reader["TipoDato"].ToString();
                        etiqueta.UsuarioAlta = reader["UsuarioAlta"].ToString();
                        etiqueta.UsuarioBaja = reader["UsuarioBaja"].ToString();
                        etiqueta.Status = reader["Status"].ToString();
                        etiqueta.FAlta = reader["FAlta"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(reader["FAlta"]);
                        etiqueta.FBaja = reader["FBaja"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(reader["FBaja"]);
                        etiqueta.TablaDestino = reader["TablaDestino"].ToString();
                        etiqueta.ColumnaDestino = reader["ColumnaDestino"].ToString();
                        etiqueta.ConcatenaEtiqueta = Convert.ToBoolean(reader["ConcatenaEtiqueta"]);

                    }
                }
            }
            catch (SqlException ex)
            {

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return etiqueta;

        }
        public override List<Etiqueta> ObtieneListaEtiqueta()
        {

            List<Etiqueta> lista = new List<Etiqueta>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBObtieneListaEtiqueta", cnn);
                    cmd.Parameters.Add("@Configuracion", System.Data.SqlDbType.Int).Value = 0;
                    cmd.Parameters.Add("@StatusConcepto", System.Data.SqlDbType.Int).Value = 0;
                    cmd.Parameters.Add("@Banco", System.Data.SqlDbType.Int).Value = 0;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Etiqueta etiqueta = new EtiquetaDatos();
                        etiqueta.Id = Convert.ToInt32(reader["Etiqueta"]);
                        etiqueta.IdBancoFinanciero = Convert.ToInt32(reader["BancoFinanciero"]);
                        etiqueta.IdTipoFuenteInformacion = Convert.ToInt32(reader["TipoFuenteInformacion"]);
                        etiqueta.Descripcion = reader["Decripcion"].ToString();
                        etiqueta.TipoDato = reader["TipoDato"].ToString();
                        etiqueta.UsuarioAlta = reader["UsuarioAlta"].ToString();
                        etiqueta.UsuarioBaja = reader["UsuarioBaja"].ToString();
                        etiqueta.Status = reader["Status"].ToString();
                        etiqueta.FAlta = reader["FAlta"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(reader["FAlta"]);
                        etiqueta.FBaja = reader["FBaja"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(reader["FBaja"]);
                        etiqueta.TablaDestino = reader["TablaDestino"].ToString();
                        etiqueta.ColumnaDestino = reader["ColumnaDestino"].ToString();
                        etiqueta.ConcatenaEtiqueta = Convert.ToBoolean(reader["ConcatenaEtiqueta"]);
                        etiqueta.BancoDes = Convert.ToString(reader["BancoDes"]);
                        lista.Add(etiqueta);
                    }
                }
            }
            catch (SqlException ex)
            {

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return lista;

        }

        public override List<Etiqueta> ObtieneListaEtiquetaStatusConcepto(int statusConcepto)
        {

            List<Etiqueta> lista = new List<Etiqueta>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBObtieneListaEtiqueta", cnn);
                    cmd.Parameters.Add("@Configuracion", System.Data.SqlDbType.Int).Value = 1;
                    cmd.Parameters.Add("@StatusConcepto", System.Data.SqlDbType.Int).Value = statusConcepto;
                    cmd.Parameters.Add("@Banco", System.Data.SqlDbType.Int).Value = 0;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Etiqueta etiqueta = new EtiquetaDatos();
                        etiqueta.Id = Convert.ToInt32(reader["Etiqueta"]);
                        etiqueta.IdBancoFinanciero = Convert.ToInt32(reader["BancoFinanciero"]);
                        etiqueta.IdTipoFuenteInformacion = Convert.ToInt32(reader["TipoFuenteInformacion"]);
                        etiqueta.Descripcion = reader["Decripcion"].ToString();
                        etiqueta.TipoDato = reader["TipoDato"].ToString();
                        etiqueta.UsuarioAlta = reader["UsuarioAlta"].ToString();
                        etiqueta.UsuarioBaja = reader["UsuarioBaja"].ToString();
                        etiqueta.Status = reader["Status"].ToString();
                        etiqueta.FAlta = reader["FAlta"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(reader["FAlta"]);
                        etiqueta.FBaja = reader["FBaja"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(reader["FBaja"]);
                        etiqueta.TablaDestino = reader["TablaDestino"].ToString();
                        etiqueta.ColumnaDestino = reader["ColumnaDestino"].ToString();
                        lista.Add(etiqueta);
                    }
                }
            }
            catch (SqlException ex)
            {

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return lista;

        }

        public override List<Etiqueta> ObtieneListaEtiquetaBanco(int bancoFinanciero)
        {

            List<Etiqueta> lista = new List<Etiqueta>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBObtieneListaEtiqueta", cnn);
                    cmd.Parameters.Add("@Configuracion", System.Data.SqlDbType.Int).Value = 2;
                    cmd.Parameters.Add("@StatusConcepto", System.Data.SqlDbType.Int).Value = 0;
                    cmd.Parameters.Add("@Banco", System.Data.SqlDbType.Int).Value = bancoFinanciero;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Etiqueta etiqueta = new EtiquetaDatos();
                        etiqueta.Id = Convert.ToInt32(reader["Etiqueta"]);
                        etiqueta.IdBancoFinanciero = Convert.ToInt32(reader["BancoFinanciero"]);
                        etiqueta.IdTipoFuenteInformacion = Convert.ToInt32(reader["TipoFuenteInformacion"]);
                        etiqueta.Descripcion = reader["Decripcion"].ToString();
                        etiqueta.TipoDato = reader["TipoDato"].ToString();
                        etiqueta.UsuarioAlta = reader["UsuarioAlta"].ToString();
                        etiqueta.UsuarioBaja = reader["UsuarioBaja"].ToString();
                        etiqueta.Status = reader["Status"].ToString();
                        etiqueta.FAlta = reader["FAlta"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(reader["FAlta"]);
                        etiqueta.FBaja = reader["FBaja"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(reader["FBaja"]);
                        etiqueta.TablaDestino = reader["TablaDestino"].ToString();
                        etiqueta.ColumnaDestino = reader["ColumnaDestino"].ToString();
                        lista.Add(etiqueta);
                    }
                }
            }
            catch (SqlException ex)
            {

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return lista;

        }


        public override int ObtieneNumeroMaximoEtiqueta()
        {
            int numero = 0;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBObtieneNumeroMaximoEtiqueta", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    numero = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return numero;

        }

#endregion


#region FuenteInformacionDetalleEtiqueta

        public override FuenteInformacionDetalleEtiqueta ObtieneFuenteInformacionDetalleEtiquetaPorId(string cuentaBancoFinanciero,
        int idBancoFinanciero,
        int idFuenteInformacion,
        int secuencia,
        int idEtiqueta)
        {
            FuenteInformacionDetalleEtiqueta FIDetiqueta = new FuenteInformacionDetalleEtiquetaDatos();
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBObtieneFuenteInformacionDetalleEtiqueta", cnn);
                    cmd.Parameters.Add("@CuentaBancoFinanciero", System.Data.SqlDbType.VarChar).Value = cuentaBancoFinanciero;
                    cmd.Parameters.Add("@BancoFinanciero", System.Data.SqlDbType.SmallInt).Value = idBancoFinanciero;
                    cmd.Parameters.Add("@FuenteInformacion", System.Data.SqlDbType.Int).Value = idFuenteInformacion;
                    cmd.Parameters.Add("@Secuencia", System.Data.SqlDbType.Int).Value = secuencia;
                    cmd.Parameters.Add("@Etiqueta", System.Data.SqlDbType.SmallInt).Value = idEtiqueta;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        FIDetiqueta.CuentaBancoFinanciero = reader["CuentaBancoFinanciero"].ToString();
                        FIDetiqueta.IdBancoFinanciero = Convert.ToInt32(reader["BancoFinanciero"]);
                        FIDetiqueta.IdFuenteInformacion = Convert.ToInt32(reader["FuenteInformacion"]);
                        FIDetiqueta.Secuencia = Convert.ToInt32(reader["Secuencia"]);
                        FIDetiqueta.IdEtiqueta = Convert.ToInt32(reader["Etiqueta"]);
                        FIDetiqueta.LongitudFija = Convert.ToInt32(reader["LongitudFija"]);
                        FIDetiqueta.Finaliza = reader["Finaliza"].ToString();
                    }
                }
            }
            catch (SqlException ex)
            {

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return FIDetiqueta;
        }

        public override List<FuenteInformacionDetalleEtiqueta> ObtieneListaFuenteInformacionDetalleEtiqueta(string cuentaBancoFinanciero,
        int idBancoFinanciero,
        int idFuenteInformacion,
        int secuencia)
        {

            List<FuenteInformacionDetalleEtiqueta> lista = new List<FuenteInformacionDetalleEtiqueta>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBObtieneListaFuenteInformacionDetalleEtiqueta", cnn);
                    cmd.Parameters.Add("@CuentaBancoFinanciero", System.Data.SqlDbType.VarChar).Value = cuentaBancoFinanciero;
                    cmd.Parameters.Add("@BancoFinanciero", System.Data.SqlDbType.SmallInt).Value = idBancoFinanciero;
                    cmd.Parameters.Add("@FuenteInformacion", System.Data.SqlDbType.Int).Value = idFuenteInformacion;
                    cmd.Parameters.Add("@Secuencia", System.Data.SqlDbType.Int).Value = secuencia;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        FuenteInformacionDetalleEtiqueta FIDetiqueta = new FuenteInformacionDetalleEtiquetaDatos();
                        FIDetiqueta.CuentaBancoFinanciero = reader["CuentaBancoFinanciero"].ToString();
                        FIDetiqueta.IdBancoFinanciero = Convert.ToInt32(reader["BancoFinanciero"]);
                        FIDetiqueta.IdFuenteInformacion = Convert.ToInt32(reader["FuenteInformacion"]);
                        FIDetiqueta.Secuencia = Convert.ToInt32(reader["Secuencia"]);
                        FIDetiqueta.IdEtiqueta = Convert.ToInt32(reader["Etiqueta"]);
                        FIDetiqueta.LongitudFija = Convert.ToInt32(reader["LongitudFija"]);
                        FIDetiqueta.Finaliza = reader["Finaliza"].ToString();
                        lista.Add(FIDetiqueta);

                    }
                }
            }
            catch (SqlException ex)
            {

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return lista;
        }


        public override int ObtieneMaximoFuenteInformacionDetalleEtiqueta(string cuentaBancoFinanciero,
    int idBancoFinanciero,
    int idFuenteInformacion)
        {
            int numero = 0;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBObtieneNumeroMaximoDetalleEtiqueta", cnn);
                    cmd.Parameters.Add("@CuentaBancoFinanciero", System.Data.SqlDbType.VarChar).Value = cuentaBancoFinanciero;
                    cmd.Parameters.Add("@BancoFinanciero", System.Data.SqlDbType.SmallInt).Value = idBancoFinanciero;
                    cmd.Parameters.Add("@FuenteInformacion", System.Data.SqlDbType.Int).Value = idFuenteInformacion;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    numero = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return numero;

        }


#endregion


#region TipoDato


        public override List<TipoDato> ObtieneListaTipoDato()
        {
            List<TipoDato> lista = new List<TipoDato>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBObtieneListaTipoDato", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        TipoDato tipoDato = new TipoDatoDatos();
                        tipoDato.Descripcion = reader["TipoDato"].ToString();
                        lista.Add(tipoDato);
                    }
                }
            }
            catch (SqlException ex)
            {

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return lista;

        }

#endregion


        public override int VerificarArchivo(int corporativo, int sucursal, int anio, string cuentaBancoFinanciero, int tipoFuenteInformacion, int frecuencia, DateTime fechaInicial, DateTime fFinal)
        {

            int resultado = 0;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("spCBVerificaArchivo", cnn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = corporativo;
                    cmd.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = sucursal;
                    cmd.Parameters.Add("@Año", System.Data.SqlDbType.Int).Value = anio;
                    cmd.Parameters.Add("@CuentaBancoFinanciero", System.Data.SqlDbType.VarChar).Value = cuentaBancoFinanciero;
                    cmd.Parameters.Add("@TipoFuenteInformacion", System.Data.SqlDbType.SmallInt).Value = tipoFuenteInformacion;
                    cmd.Parameters.Add("@Frecuencia", System.Data.SqlDbType.SmallInt).Value = frecuencia;
                    cmd.Parameters.Add("@FInicial", System.Data.SqlDbType.DateTime).Value = fechaInicial;
                    cmd.Parameters.Add("@FFinal", System.Data.SqlDbType.DateTime).Value = fFinal;
                    resultado = (int)cmd.ExecuteScalar();

                }
            }
            catch (SqlException ex)
            {

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Clase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }

            return resultado;
        }

        public override List<StatusConcepto> ObtenieneStatusConceptosGrupoConciliacion(int configuracion,
          int grupoconciliacion)
        {
            List<StatusConcepto> datos = new List<StatusConcepto>();

            using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBCargaComboStatusConceptoGrupoConciliacion", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.SmallInt).Value = configuracion;
                    comando.Parameters.Add("@GrupoConciliacion", System.Data.SqlDbType.Int).Value = grupoconciliacion;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {

                        StatusConcepto dato = new StatusConceptoDatos(Convert.ToInt32(reader["Identificador"]), Convert.ToString(reader["Descripcion"]), Convert.ToString(reader["Usuario"]), Convert.ToString(reader["Status"]), Convert.ToDateTime(reader["FAlta"]), this.implementadorMensajes);
                        datos.Add(dato);
                    }
                }
                catch (SqlException ex)
                {
                    stackTrace = new StackTrace();
                    this.ImplementadorMensajes.MostrarMensaje("Error al consultar la informacion.\n\rClase :" + this.GetType().Name + "\n\r" + "Método :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                    stackTrace = null;
                }
                return datos;
            }
        }

        /// <summary>
        /// Devuelve Catalogo Estatus Concepto
        /// </summary>
        /// <returns></returns>
        public override List<StatusConcepto> ObtenieneStatusConceptos()
        {
            List<StatusConcepto> datos = new List<StatusConcepto>();

            using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBCargaComboStatusConcepto", cnn);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {

                        StatusConcepto dato = new StatusConceptoDatos(Convert.ToInt32(reader["Identificador"]), Convert.ToString(reader["Descripcion"]), Convert.ToString(reader["Usuario"]), Convert.ToString(reader["Status"]), Convert.ToDateTime(reader["FAlta"]), this.implementadorMensajes);
                        datos.Add(dato);
                    }
                }
                catch (SqlException ex)
                {
                    stackTrace = new StackTrace();
                    this.ImplementadorMensajes.MostrarMensaje("Error al consultar la informacion.\n\rClase :" + this.GetType().Name + "\n\r" + "Método :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                    stackTrace = null;
                }
                return datos;
            }
        }

    }

    
}
