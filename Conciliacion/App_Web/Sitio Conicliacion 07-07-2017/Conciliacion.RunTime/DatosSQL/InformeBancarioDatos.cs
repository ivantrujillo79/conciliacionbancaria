using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.ReglasDeNegocio;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Conciliacion.RunTime.DatosSQL;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using System.Globalization;

namespace Conciliacion.RunTime.DatosSQL
{
    public   class InformeBancarioDatos : InformeBancario
    {
        public InformeBancarioDatos(IMensajesImplementacion implementadorMensajes)
                    : base(implementadorMensajes)
        {
        }

        public InformeBancarioDatos(DetallePosicionDiariaBancos detalleposiciondiariabancos, IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public List<DetalleReporteEstadoCuentaDia> consultaReporteEstadoCuentaPorDia(Conexion _conexion, DateTime FechaIni, DateTime FechaFin, string Banco, string CuentaBanco)
        {
            List<DetalleReporteEstadoCuentaDia> ListaResultado = new List<DetalleReporteEstadoCuentaDia>();
            try
            {
                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBReporteEstadoDeCuentaPorDia";

                _conexion.Comando.Parameters.Clear();
                _conexion.Comando.Parameters.Add(new SqlParameter("@FechaIni", System.Data.SqlDbType.DateTime)).Value = FechaIni;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = FechaFin;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Banco", System.Data.SqlDbType.VarChar)).Value = Banco;
                _conexion.Comando.Parameters.Add(new SqlParameter("@CuentaBanco", System.Data.SqlDbType.VarChar)).Value = CuentaBanco;

                SqlDataReader reader = _conexion.Comando.ExecuteReader();
                List<DetalleReporteEstadoCuentaDia> lstInformeBancario = new List<DetalleReporteEstadoCuentaDia>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DetalleReporteEstadoCuentaDia dato = new DetalleReporteEstadoCuentaDia();
                        dato.Corporativo = Convert.ToString(reader["Corporativo"]);
                        dato.Sucursal = Convert.ToString(reader["Sucursal"]);
                        dato.CuentaBancoFinanciero = Convert.ToString(reader["CuentaBancoFinanciero"]);
                        dato.Fecha = Convert.ToString(reader["Fecha"]);
                        dato.Retiro = Convert.ToString(reader["Retiro"]);
                        dato.Depositos = Convert.ToString(reader["Depositos"]);
                        dato.SaldoFinal = Convert.ToString(reader["SaldoFinal"]);
                        lstInformeBancario.Add(dato);
                    }
                    reader.Close();
                }

                return lstInformeBancario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /* public override List<DetallePosicionDiariaBancos>  consultaPosicionDiariaBanco(Conexion _conexion, DateTime FechaIni, DateTime FechaFin, string Banco, string CuentaBanco, string Status, string StatusConcepto)
         {
             try
             {
                 _conexion.Comando.CommandType = CommandType.StoredProcedure;
                 _conexion.Comando.CommandText = "spCBReporteEstadoDeCuentaConciliado";

                 _conexion.Comando.Parameters.Clear();
                 _conexion.Comando.Parameters.Add(new SqlParameter("@FechaIni", System.Data.SqlDbType.SmallInt)).Value = FechaIni;
                 _conexion.Comando.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.SmallInt)).Value = FechaFin;
                 _conexion.Comando.Parameters.Add(new SqlParameter("@Banco", System.Data.SqlDbType.VarChar)).Value = Banco;
                 _conexion.Comando.Parameters.Add(new SqlParameter("@CuentaBanco", System.Data.SqlDbType.VarChar)).Value = CuentaBanco;
                 _conexion.Comando.Parameters.Add(new SqlParameter("@Status", System.Data.SqlDbType.VarChar)).Value = Status;
                 _conexion.Comando.Parameters.Add(new SqlParameter("@StatusConcepto", System.Data.SqlDbType.VarChar)).Value = StatusConcepto;

                 SqlDataReader reader = _conexion.Comando.ExecuteReader();
                 List<DetallePosicionDiariaBancos> lstInformeBancario = new List<DetallePosicionDiariaBancos>();

                 if (reader.HasRows)
                 {
                     while (reader.Read())
                     {
                         DetallePosicionDiariaBancos dato = new DetallePosicionDiariaBancos(
                                 Convert.ToString(reader["Corporativo"]),
                                 Convert.ToString(reader["Sucursal"]),
                                 Convert.ToInt32(reader["Año"]),
                                 Convert.ToInt32(reader["Mes"]),
                                 Convert.ToString(reader["CuentaBancoFinanciero"]),
                                 Convert.ToInt32(reader["ConsecutivoFlujo"]),
                                 Convert.ToDateTime(reader["Fecha"]),
                                 Convert.ToString(reader["Referencia"]),
                                 Convert.ToString(reader["Concepto"]),
                                (reader["Fecha"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["Fecha"])),
                                 Convert.ToByte(reader["Caja"]),
                                 Convert.ToDecimal(reader["Kilos"]),
                                 Convert.ToDecimal(reader["Importe"]),
                                 Convert.ToDecimal(reader["Retiros"]),
                                 Convert.ToDecimal(reader["Depositos"]),
                                 Convert.ToDecimal(reader["SaldoFinal"]),
                                 Convert.ToString(reader["ConceptoConciliado"]),
                                 Convert.ToString(reader["DocumentoConciliado"])
                                 );
                         lstInformeBancario.Add(dato);
                     }
                     reader.Close();
                 }

                 return lstInformeBancario;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
       */

        public override List<DetallePosicionDiariaBancos> consultaPosicionDiariaBanco(Conexion _conexion, DateTime FechaIni, DateTime FechaFin, byte Caja)
        {
            try
            {
                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBReportePosicionDiariaBancos";

                _conexion.Comando.Parameters.Clear();
                _conexion.Comando.Parameters.Add(new SqlParameter("@FechaIni", System.Data.SqlDbType.DateTime)).Value = FechaIni;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = FechaFin;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Caja", System.Data.SqlDbType.TinyInt)).Value = Caja;

                SqlDataReader reader = _conexion.Comando.ExecuteReader();
                List<DetallePosicionDiariaBancos> lstInformeBancario = new List<DetallePosicionDiariaBancos>();            
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DetallePosicionDiariaBancos dato = new DetallePosicionDiariaBancos(
                                Convert.ToString(reader["Concepto"]),
                                (reader["Fecha"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["Fecha"])),
                                Convert.ToByte(reader["Caja"]),
                                Convert.ToDecimal(reader["Kilos"]),
                                Convert.ToDecimal(reader["Importe"]),
                                Convert.ToInt32(reader["Detalle"])
                                );
                        lstInformeBancario.Add(dato);
                    }
                    reader.Close();
                }
                
                return lstInformeBancario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override InformeBancario CrearObjeto()
        {
            return new InformeBancarioDatos(this.ImplementadorMensajes);
        }

        public class DetalleReporteEstadoCuenta
        {
            private string banco;
            private string corporativo;
            private string sucursal;
            private short año;
            private short mes;
            private string cuentabancofinanciero;
            private int consecutivoflujo;
            private DateTime foperacion;
            private string referencia;
            private string concepto;
            private decimal retiros;
            private decimal depositos;
            private decimal saldofinal;

            public string Banco
            {
                get { return banco; }
                set { banco = value; }
            }
            public string Corporativo
            {
                get { return corporativo; }
                set { corporativo = value; }
            }
            public string Sucursal
            {
                get { return sucursal; }
                set { sucursal = value; }
            }
            public short Año
            {
                get { return año; }
                set { año = value; }
            }
            public short Mes
            {
                get { return mes; }
                set { mes = value; }
            }
            public string CuentaBancoFinanciero
            {
                get { return cuentabancofinanciero; }
                set { cuentabancofinanciero = value; }
            }
            public int ConsecutivoFlujo
            {
                get { return consecutivoflujo; }
                set { consecutivoflujo = value; }
            }
            public DateTime FOperacion
            {
                get { return foperacion; }
                set { foperacion = value; }
            }
            public string Referencia
            {
                get { return referencia; }
                set { referencia = value; }
            }
            public string Concepto
            {
                get { return concepto; }
                set { concepto = value; }
            }
            public decimal Retiros
            {
                get { return retiros; }
                set { retiros = value; }
            }
            public decimal Depositos
            {
                get { return depositos; }
                set { depositos = value; }
            }
            public decimal SaldoFinal
            {
                get { return saldofinal; }
                set { saldofinal = value; }
            }

            public DetalleReporteEstadoCuenta()
            {
            }

            public DetalleReporteEstadoCuenta(
                string banco,
                string corporativo,
                string sucursal,
                short año,
                short mes,
                string cuentabancofinanciero,
                int consecutivoflujo,
                DateTime foperacion,
                string referencia,
                string concepto,
                decimal retiros,
                decimal depositos,
                decimal saldofinal)
            {
                this.banco = banco;
                this.corporativo = corporativo;
                this.sucursal = sucursal;
                this.año = año;
                this.mes = mes;
                this.cuentabancofinanciero = cuentabancofinanciero;
                this.consecutivoflujo = consecutivoflujo;
                this.foperacion = foperacion;
                this.referencia = referencia;
                this.concepto = concepto;
                this.retiros = retiros;
                this.depositos = depositos;
                this.saldofinal = saldofinal;
            }

            public List<DetalleReporteEstadoCuenta> consultaReporteEstadoCuenta(Conexion _conexion, DateTime FechaIni, DateTime FechaFin, string Banco, string CuentaBanco)
            {
                List<DetalleReporteEstadoCuenta> ListaResultado = new List<DetalleReporteEstadoCuenta>();
                try
                {
                    _conexion.Comando.CommandType = CommandType.StoredProcedure;
                    _conexion.Comando.CommandText = "spCBReporteEstadoDeCuenta";
                    _conexion.Comando.Parameters.Clear();
                    _conexion.Comando.Parameters.Add(new SqlParameter("@FechaIni", System.Data.SqlDbType.DateTime)).Value = FechaIni;
                    _conexion.Comando.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = FechaFin;
                    _conexion.Comando.Parameters.Add(new SqlParameter("@Banco", System.Data.SqlDbType.VarChar)).Value = Banco;
                    //_conexion.Comando.Parameters.Add(new SqlParameter("@CuentaBanco", System.Data.SqlDbType.VarChar)).Value = CuentaBanco;
                    SqlDataReader reader = _conexion.Comando.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            DetalleReporteEstadoCuenta dato = new DetalleReporteEstadoCuenta();
                            //dato.Banco = (reader["banco"] == DBNull.Value ? "" : Convert.ToString(reader["banco"]));
                            dato.Corporativo = Convert.ToString(reader["corporativo"]);
                            dato.Sucursal = Convert.ToString(reader["sucursal"]);
                            dato.Año = Convert.ToInt16(reader["año"]);
                            dato.Mes = Convert.ToInt16(reader["mes"]);
                            dato.CuentaBancoFinanciero = Convert.ToString(reader["cuentabancofinanciero"]).Trim();
                            dato.ConsecutivoFlujo = Convert.ToInt32(reader["consecutivoflujo"]==DBNull.Value?"0": reader["consecutivoflujo"]); // mcc 20180503
                            dato.FOperacion = Convert.ToDateTime(reader["foperacion"]);
                            dato.Referencia = Convert.ToString(reader["referencia"]);
                            dato.Concepto = Convert.ToString(reader["concepto"]);
                            dato.Retiros = Convert.ToDecimal(reader["retiros"]);
                            dato.Depositos = Convert.ToDecimal(reader["depositos"]);
                            dato.SaldoFinal = Convert.ToDecimal(reader["saldofinal"]);
                            ListaResultado.Add(dato);
                        }
                        reader.Close();
                    }
                    return ListaResultado;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        public class DetallePosicionDiariaBancos
        {
            private string _Concepto;
            private DateTime _Fecha;
            private byte _Caja;
            private decimal _Kilos;
            private decimal _Importe;
            private int _Detalle;

            #region Propiedades

            public string Concepto
            {
                get { return _Concepto; }
                set { _Concepto = value; }
            }

            public DateTime Fecha
            {
                get { return _Fecha; }
                set { _Fecha = value; }
            }

            public byte Caja
            {
                get { return _Caja; }
                set { _Caja = value; }
            }

            public decimal Kilos
            {
                get { return _Kilos; }
                set { _Kilos = value; }
            }

            public decimal Importe
            {
                get { return _Importe; }
                set { _Importe = value; }
            }

            public int Detalle
            {
                get { return _Detalle; }
                set { _Detalle = value; }
            }

            #endregion

            #region Constructores

            public DetallePosicionDiariaBancos()
            {

            }

            public DetallePosicionDiariaBancos(
                                                string concepto,
                                                DateTime fecha,
                                                byte caja,
                                                decimal kilos,
                                                decimal importe, int detalle)
            {
                this._Concepto = concepto;
                this._Fecha = fecha;
                this._Caja = caja;
                this._Kilos = kilos;
                this._Importe = importe;
                this._Detalle = detalle;
            }

            #endregion

        }

        public class DetalleCuentaBanco
        {
            public DetalleCuentaBanco()
            {
            }

            public DetalleCuentaBanco(
                    int IDCuenta,
                    string Descripcion)
            {
            }

            public List<DetalleCuentaBanco> consultarCuentasBancarias(Conexion _conexion, Int16 Corporativo, Int16 Banco)
            {
                try
                {
                    List<DetalleCuentaBanco> ListaRetorno = new List<DetalleCuentaBanco>();

                    _conexion.Comando.CommandType = CommandType.StoredProcedure;
                    _conexion.Comando.CommandText = "spCBConsultaCuentaBanco";
                    _conexion.Comando.Parameters.Clear();
                    _conexion.Comando.Parameters.Add(new SqlParameter("@Corporativo", System.Data.SqlDbType.TinyInt)).Value = Corporativo;
                    _conexion.Comando.Parameters.Add(new SqlParameter("@Banco", System.Data.SqlDbType.SmallInt)).Value = Banco;
                    SqlDataReader reader = _conexion.Comando.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            DetalleCuentaBanco dato = new DetalleCuentaBanco(
                                    Convert.ToInt32(reader["Identificador"]),
                                    Convert.ToString(reader["Descripcion"])
                                    );
                            ListaRetorno.Add(dato);
                        }
                        reader.Close();
                    }
                    return ListaRetorno;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        public class DetalleReporteEstadoCuentaDia
        {
            private string corporativo;
            private string sucursal;
            private string cuentabancofinanciero;
            private string fecha;
            private string retiro;
            private string depositos;
            private string saldofinal;

            public string Corporativo
            {
                get { return corporativo; }
                set { corporativo = value; }
            }
            public string Sucursal
            {
                get { return sucursal; }
                set { sucursal = value; }
            }
            public string CuentaBancoFinanciero
            {
                get { return cuentabancofinanciero; }
                set { cuentabancofinanciero = value; }
            }
            public string Fecha
            {
                get { return fecha; }
                set { fecha = value; }
            }
            public string Retiro
            {
                get { return retiro; }
                set { retiro = value; }
            }
            public string Depositos
            {
                get { return depositos; }
                set { depositos = value; }
            }
            public string SaldoFinal
            {
                get { return saldofinal; }
                set { saldofinal = value; }
            }

          

          
        }

        public class DetalleBanco
        {
            private int idbanco;
            private string descripcion;


            public int IDBanco
            {
                get { return idbanco; }
                set { idbanco = value; }
            }

            public string Descripcion
            {
                get { return descripcion; }
                set { descripcion = value; }
            }

            public DetalleBanco()
            { }

            public DetalleBanco(int idbanco, string descripcion)
            { }

            public List<DetalleBanco> consultarBancos(Conexion _conexion, int Corporativo,string Usuario)
            {
                List<DetalleBanco> ListaRetorno = new List<DetalleBanco>();
                try
                {
                    _conexion.Comando.CommandType = CommandType.StoredProcedure;
                    _conexion.Comando.CommandText = "spCBConsultaBancoReportes"; // mcc 20180503
                    _conexion.Comando.Parameters.Clear();
                    _conexion.Comando.Parameters.Add(new SqlParameter("@Corporativo", System.Data.SqlDbType.SmallInt)).Value = Corporativo;
                    _conexion.Comando.Parameters.Add(new SqlParameter("@Usuario", System.Data.SqlDbType.Char)).Value = Usuario;
                    SqlDataReader reader = _conexion.Comando.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            DetalleBanco dato = new DetalleBanco();
                            dato.IDBanco = Convert.ToInt32(reader["IDENTIFICADOR"]);
                            dato.Descripcion = Convert.ToString(reader["Descripcion"]);
                            ListaRetorno.Add(dato);
                        }
                        reader.Close();
                    }

                    return ListaRetorno;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        public class DetalleReporteEstadoCuentaConciliado
        {
            private string corporativo;
            private string sucursal;
            private short año;
            private short mes;
            private string cuentaBancoFinanciero;
            private string consecutivoFlujo;
            private DateTime fecha;
            private string referencia;
            private string concepto;
            private decimal retiros;
            private decimal depositos;
            private decimal saldoFinal;
            private string conceptoConciliado;
            private string documentoConciliado;
            private string clabe;

            public string Corporativo
            {
                get { return corporativo; }
                set { corporativo = value; }
            }

            public string Sucursal
            {
                get { return sucursal; }
                set { sucursal = value; }
            }

            public short Año
            {
                get { return año; }
                set { año = value; }
            }

            public short Mes
            {
                get { return mes; }
                set { mes = value; }
            }

            public string CuentaBancoFinanciero
            {
                get { return cuentaBancoFinanciero; }
                set { cuentaBancoFinanciero = value; }
            }

            public string ConsecutivoFlujo
            {
                get { return consecutivoFlujo; }
                set { consecutivoFlujo = value; }
            }

            public DateTime Fecha
            {
                get { return fecha; }
                set { fecha = value; }
            }

            public string Referencia
            {
                get { return referencia; }
                set { referencia = value; }
            }

            public string Concepto
            {
                get { return concepto; }
                set { concepto = value; }
            }

            public decimal Retiros
            {
                get { return retiros; }
                set { retiros = value; }
            }

            public decimal Depositos
            {
                get { return depositos; }
                set { depositos = value; }
            }

            public decimal SaldoFinal
            {
                get { return saldoFinal; }
                set { saldoFinal = value; }
            }

            public string ConceptoConciliado
            {
                get { return conceptoConciliado; }
                set { conceptoConciliado = value; }
            }

            public string DocumentoConciliado
            {
                get { return documentoConciliado; }
                set { documentoConciliado = value; }
            }

            public string Clabe
            {
                get { return clabe; }
                set { clabe = value; }
            }

            public DetalleReporteEstadoCuentaConciliado()
            {
            }

            public DetalleReporteEstadoCuentaConciliado(
                string Corporativo,
                string Sucursal,
                short Año,
                short Mes,
                string CuentaBancoFinanciero,
                string ConsecutivoFlujo,
                DateTime Fecha,
                string Referencia,
                string Concepto,
                decimal Retiros,
                decimal Depositos,
                decimal SaldoFinal,
                string ConceptoConciliado,
                string DocumentoConciliado,
                string Clabe

            )
            {
                this.corporativo = Corporativo;
                this.sucursal = Sucursal;
                this.año = Año;
                this.mes = Mes;
                this.cuentaBancoFinanciero = CuentaBancoFinanciero;
                this.consecutivoFlujo = ConsecutivoFlujo;
                this.fecha = Fecha;
                this.referencia = Referencia;
                this.concepto = Concepto;
                this.retiros = Retiros;
                this.depositos = Depositos;
                this.saldoFinal = SaldoFinal;
                this.conceptoConciliado = ConceptoConciliado;
                this.documentoConciliado = DocumentoConciliado;
                this.clabe = Clabe;
            }

            #region Metodo

            public List<DetalleReporteEstadoCuentaConciliado> consultaReporteEstadoCuentaConciliado(Conexion _conexion, DateTime FechaIni, DateTime FechaFin, string Banco, string CuentaBanco, string Status, string StatusConcepto)
            {
                List<DetalleReporteEstadoCuentaConciliado> ListaResultado = new List<DetalleReporteEstadoCuentaConciliado>();
                try
                {
                    CultureInfo MyCultureInfo = new CultureInfo("es-MX");

                    _conexion.Comando.CommandType = CommandType.StoredProcedure;
                    _conexion.Comando.CommandText = "spCBReporteEstadoDeCuentaConciliado";
                    _conexion.Comando.Parameters.Clear();
                    _conexion.Comando.Parameters.Add(new SqlParameter("@FechaIni", System.Data.SqlDbType.DateTime)).Value = FechaIni;
                    _conexion.Comando.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = FechaFin;
                    _conexion.Comando.Parameters.Add(new SqlParameter("@Banco", System.Data.SqlDbType.VarChar)).Value = Banco;
                    _conexion.Comando.Parameters.Add(new SqlParameter("@CuentaBanco", System.Data.SqlDbType.VarChar)).Value = CuentaBanco;
                    _conexion.Comando.Parameters.Add(new SqlParameter("@Status", System.Data.SqlDbType.VarChar)).Value = Status;
                    _conexion.Comando.Parameters.Add(new SqlParameter("@StatusConcepto", System.Data.SqlDbType.VarChar)).Value = StatusConcepto;
                    SqlDataReader reader = _conexion.Comando.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            DetalleReporteEstadoCuentaConciliado dtReporteEstadosCuentaConciliado = new DetalleReporteEstadoCuentaConciliado();
                            dtReporteEstadosCuentaConciliado.Corporativo = Convert.ToString(reader["corporativo"]);
                            dtReporteEstadosCuentaConciliado.Sucursal = Convert.ToString(reader["sucursal"]);
                            dtReporteEstadosCuentaConciliado.Año = Convert.ToInt16(reader["año"]);
                            dtReporteEstadosCuentaConciliado.Mes = Convert.ToInt16(reader["mes"]);
                            dtReporteEstadosCuentaConciliado.CuentaBancoFinanciero = Convert.ToString(reader["cuentabancofinanciero"]);
                            dtReporteEstadosCuentaConciliado.ConsecutivoFlujo = Convert.ToString(reader["consecutivoflujo"]);
                            string[] formats = { "M/d/yyyy", "dd-mm-yyyy", "M-d-yyyy", "d-M-yyyy", "d-MMM-yy", "d-MMMM-yyyy", };
                            DateTime date;
                            string Fecha = reader["fecha"].ToString();
                            date = Convert.ToDateTime(Fecha, CultureInfo.InvariantCulture);
                           
                            dtReporteEstadosCuentaConciliado.Fecha = date;
                            
                                //DateTime.Parse(reader["fecha"].ToString(), MyCultureInfo);
                            dtReporteEstadosCuentaConciliado.Referencia = Convert.ToString(reader["referencia"]);
                            dtReporteEstadosCuentaConciliado.Concepto = Convert.ToString(reader["concepto"]);
                            dtReporteEstadosCuentaConciliado.Retiros = Convert.ToDecimal(reader["retiros"]);
                            dtReporteEstadosCuentaConciliado.Depositos = Convert.ToDecimal(reader["depositos"]);
                            dtReporteEstadosCuentaConciliado.SaldoFinal = Convert.ToDecimal(reader["saldofinal"]);
                            dtReporteEstadosCuentaConciliado.ConceptoConciliado = reader["ConceptoConciliado"] == DBNull.Value ? "" :
                                                                                    Convert.ToString(reader["ConceptoConciliado"]);
                            dtReporteEstadosCuentaConciliado.DocumentoConciliado = reader["DocumentoConciliado"] == DBNull.Value ? "" :
                                                                                    Convert.ToString(reader["DocumentoConciliado"]);
                            dtReporteEstadosCuentaConciliado.Clabe = reader["Clabe"] == DBNull.Value ? "" :
                                                                                Convert.ToString(reader["Clabe"]);

                            ListaResultado.Add(dtReporteEstadosCuentaConciliado);

                        }
                        reader.Close();
                    }
                    return ListaResultado;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            #endregion

        }

        public class DetalleCaja
        {
            public DetalleCaja()
            {
            }

            public DetalleCaja(
                    int IDCaja,
                    string Descripcion)
            {
            }

            public List<Caja> consultarCajas(Conexion _conexion, int Caja)
            {
                try
                {
                    List<Caja> ListaRetorno = new List<Caja>();
                    _conexion.Comando.CommandType = CommandType.StoredProcedure;
                    _conexion.Comando.CommandText = "spCBConsultaCajasCorte";
                    _conexion.Comando.Parameters.Clear();
                    if (Caja != 0)
                    {
                        _conexion.Comando.Parameters.Add(new SqlParameter("@Caja", System.Data.SqlDbType.TinyInt)).Value = Caja;
                    }
                    else
                    {
                        _conexion.Comando.Parameters.Add(new SqlParameter("@Caja", System.Data.SqlDbType.TinyInt)).Value = 0;
                    }
                    SqlDataReader reader = _conexion.Comando.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Caja dato = new Caja(
                                    Convert.ToInt32(reader["Caja"]),
                                    Convert.ToString(reader["Descripcion"])
                                    );
                            ListaRetorno.Add(dato);
                        }
                        reader.Close();
                    }
                    return ListaRetorno;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
    }
}

