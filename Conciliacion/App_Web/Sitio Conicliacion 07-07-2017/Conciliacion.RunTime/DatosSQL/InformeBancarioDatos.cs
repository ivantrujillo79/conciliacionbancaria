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
                                Convert.ToDecimal(reader["Importe"])
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

        //public class DetallePosicionDiariaBancos
        //{
        //    private string corporativo;
        //    private string sucursal;
        //    private int año;
        //    private int mes;
        //    private string cuentabancofinanciero;
        //    private int consecutivoflujo;
        //    private DateTime fecha;
        //    private string referencia;
        //    private string concepto;
        //    private decimal retiros;
        //    private decimal depositos;
        //    private decimal saldofinal;
        //    private string conceptoconciliado;
        //    private string documentoconciliado;

        //    public string Corporativo
        //    {
        //        get { return corporativo; }
        //        set { corporativo = value; }
        //    }
        //    public string Sucursal
        //    {
        //        get { return sucursal; }
        //        set { sucursal = value; }
        //    }
        //    public int Año
        //    {
        //        get { return año; }
        //        set { año = value; }
        //    }
        //    public int Mes
        //    {
        //        get { return mes; }
        //        set { mes = value; }
        //    }
        //    public string CuentaBancoFinanciero
        //    {
        //        get { return cuentabancofinanciero; }
        //        set { cuentabancofinanciero = value; }
        //    }
        //    public int ConsecutivoFlujo
        //    {
        //        get { return consecutivoflujo; }
        //        set { consecutivoflujo = value; }
        //    }
        //    public DateTime Fecha
        //    {
        //        get { return fecha; }
        //        set { fecha = value; }
        //    }
        //    public string Referencia
        //    {
        //        get { return referencia; }
        //        set { referencia = value; }
        //    }
        //    public string Concepto
        //    {
        //        get { return concepto; }
        //        set { concepto = value; }
        //    }
        //    public decimal Retiros
        //    {
        //        get { return retiros; }
        //        set { retiros = value; }
        //    }
        //    public decimal Depositos
        //    {
        //        get { return depositos; }
        //        set { depositos = value; }
        //    }
        //    public decimal SaldoFinal
        //    {
        //        get { return saldofinal; }
        //        set { saldofinal = value; }
        //    }
        //    public string ConceptoConciliado
        //    {
        //        get { return conceptoconciliado; }
        //        set { conceptoconciliado = value; }
        //    }
        //    public string DocumentoConciliado
        //    {
        //        get { return documentoconciliado; }
        //        set { documentoconciliado = value; }
        //    }

        //    public DetallePosicionDiariaBancos()
        //    {

        //    }

        //    public DetallePosicionDiariaBancos(string corporativo,
        //                string sucursal,
        //                int año,
        //                int mes,
        //                string cuentabancofinanciero,
        //                int consecutivoflujo,
        //                DateTime fecha,
        //                string referencia,
        //                string concepto,
        //                decimal retiros,
        //                decimal depositos,
        //                decimal saldofinal,
        //                string conceptoconciliado,
        //                string documentoconciliado)
        //    {
        //        this.corporativo = corporativo;
        //        this.sucursal = sucursal;
        //        this.año = año;
        //        this.mes = mes;
        //        this.cuentabancofinanciero = cuentabancofinanciero;
        //        this.consecutivoflujo = consecutivoflujo;
        //        this.fecha = fecha;
        //        this.referencia = referencia;
        //        this.concepto = concepto;
        //        this.retiros = retiros;
        //        this.depositos = depositos;
        //        this.saldofinal = saldofinal;
        //        this.conceptoconciliado = conceptoconciliado;
        //        this.documentoconciliado = documentoconciliado;
        //    }

        //    //public DetallePosicionDiariaBancos CrearObjeto()
        //    //{
        //    //return new DetallePosicionDiariaBancos(this.ImplementadorMensajes);
        //    //}
        //}

        public class DetalleReporteEstadoCuenta
        {
            private string corporativo;
            private string sucursal;
            private string año;
            private string mes;
            private string cuentabancofinanciero;
            private string consecutivoflujo;
            private string foperacion;
            private string referencia;
            private string concepto;
            private string retiros;
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
            public string Año
            {
                get { return año; }
                set { año = value; }
            }
            public string Mes
            {
                get { return mes; }
                set { mes = value; }
            }
            public string CuentaBancoFinanciero
            {
                get { return cuentabancofinanciero; }
                set { cuentabancofinanciero = value; }
            }
            public string ConsecutivoFlujo
            {
                get { return consecutivoflujo; }
                set { consecutivoflujo = value; }
            }
            public string FOpercion
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
            public string Retiros
            {
                get { return retiros; }
                set { retiros = value; }
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

            public DetalleReporteEstadoCuenta()
            {
            }

            public DetalleReporteEstadoCuenta(
                string corporativo,
                string sucursal,
                string año,
                string mes,
                string cuentabancofinanciero,
                string consecutivoflujo,
                string foperacion,
                string referencia,
                string concepto,
                string retiros,
                string depositos,
                string saldofinal)
            {
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
                    _conexion.Comando.Parameters.Add(new SqlParameter("@CuentaBanco", System.Data.SqlDbType.VarChar)).Value = CuentaBanco;
                    SqlDataReader reader = _conexion.Comando.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            DetalleReporteEstadoCuenta dato = new DetalleReporteEstadoCuenta();
                            dato.Corporativo = Convert.ToString(reader["corporativo"]);
                            dato.Sucursal = Convert.ToString(reader["sucursal"]);
                            dato.Año = Convert.ToString(reader["año"]);
                            dato.Mes = Convert.ToString(reader["mes"]);
                            dato.CuentaBancoFinanciero = Convert.ToString(reader["cuentabancofinanciero"]);
                            dato.ConsecutivoFlujo = Convert.ToString(reader["consecutivoflujo"]);
                            dato.FOpercion = Convert.ToString(reader["foperacion"]);
                            dato.Referencia = Convert.ToString(reader["referencia"]);
                            dato.Concepto = Convert.ToString(reader["concepto"]);
                            dato.Retiros = Convert.ToString(reader["retiros"]);
                            dato.Depositos = Convert.ToString(reader["depositos"]);
                            dato.SaldoFinal = Convert.ToString(reader["saldofinal"]);
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
                                                decimal importe)
            {
                this._Concepto = concepto;
                this._Fecha = fecha;
                this._Caja = caja;
                this._Kilos = kilos;
                this._Importe = importe;
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

            public DetalleReporteEstadoCuentaDia()
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
                    _conexion.Comando.Parameters.Add(new SqlParameter("@FechaIni", System.Data.SqlDbType.SmallInt)).Value = FechaIni;
                    _conexion.Comando.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.SmallInt)).Value = FechaFin;
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

            public List<DetalleBanco> consultarBancos(Conexion _conexion, int Corporativo)
            {
                List<DetalleBanco> ListaRetorno = new List<DetalleBanco>();
                try
                {
                    _conexion.Comando.CommandType = CommandType.StoredProcedure;
                    _conexion.Comando.CommandText = "spCBConsultaBanco";
                    _conexion.Comando.Parameters.Clear();
                    _conexion.Comando.Parameters.Add(new SqlParameter("@Corporativo", System.Data.SqlDbType.SmallInt)).Value = Corporativo;
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
            private string Corporativo;
            private string Sucursal;
            private string Año;
            private string Mes;
            private string CuentaBancoFinanciero;
            private string ConsecutivoFlujo;
            private string Fecha;
            private string Referencia;
            private string Concepto;
            private string Retiros;
            private string Depositos;
            private string SaldoFinal;
            private string ConceptoConciliado;
            private string DocumentoConciliado;

            public string _Corporativo
            {
                get { return Corporativo; }
                set { Corporativo = value; }
            }

            public string _Sucursal
            {
                get { return Sucursal; }
                set { Sucursal = value; }
            }

            public string _Año
            {
                get { return Año; }
                set { Año = value; }
            }

            public string _Mes
            {
                get { return Mes; }
                set { Mes = value; }
            }

            public string _CuentaBancoFinanciero
            {
                get { return CuentaBancoFinanciero; }
                set { CuentaBancoFinanciero = value; }
            }

            public string _ConsecutivoFlujo
            {
                get { return ConsecutivoFlujo; }
                set { ConsecutivoFlujo = value; }
            }

            public string _Fecha
            {
                get { return Fecha; }
                set { Fecha = value; }
            }

            public string _Referencia
            {
                get { return Referencia; }
                set { Referencia = value; }
            }

            public string _Concepto
            {
                get { return Concepto; }
                set { Concepto = value; }
            }

            public string _Retiros
            {
                get { return Retiros; }
                set { Retiros = value; }
            }

            public string _Depositos
            {
                get { return Depositos; }
                set { Depositos = value; }
            }

            public string _SaldoFinal
            {
                get { return SaldoFinal; }
                set { SaldoFinal = value; }
            }

            public string _ConceptoConciliado
            {
                get { return ConceptoConciliado; }
                set { ConceptoConciliado = value; }
            }

            public string _DocumentoConciliado
            {
                get { return DocumentoConciliado; }
                set { DocumentoConciliado = value; }
            }

            #region Metodo
            public DetalleReporteEstadoCuentaConciliado()
            {
            }

            public DetalleReporteEstadoCuentaConciliado(
            string Corporativo,
            string Sucursal,
            string Año,
            string Mes,
            string CuentaBancoFinanciero,
            string ConsecutivoFlujo,
            string Fecha,
            string Referencia,
            string Concepto,
            string Retiros,
            string Depositos,
            string SaldoFinal,
            string ConceptoConciliado,
            string DocumentoConciliado
                )
            { }


            public List<DetalleReporteEstadoCuentaConciliado> consultaReporteEstadoCuentaConciliado(Conexion _conexion, DateTime FechaIni, DateTime FechaFin, string Banco, string CuentaBanco, string Status, string StatusConcepto)
            {
                List<DetalleReporteEstadoCuentaConciliado> ListaResultado = new List<DetalleReporteEstadoCuentaConciliado>();
                try
                {
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
                            dtReporteEstadosCuentaConciliado.Año = Convert.ToString(reader["año"]);
                            dtReporteEstadosCuentaConciliado.Mes = Convert.ToString(reader["mes"]);
                            dtReporteEstadosCuentaConciliado.CuentaBancoFinanciero = Convert.ToString(reader["cuentabancofinanciero"]);
                            dtReporteEstadosCuentaConciliado.ConsecutivoFlujo = Convert.ToString(reader["consecutivoflujo"]);
                            dtReporteEstadosCuentaConciliado.Fecha = Convert.ToString(reader["foperacion"]);
                            dtReporteEstadosCuentaConciliado.Referencia = Convert.ToString(reader["referencia"]);
                            dtReporteEstadosCuentaConciliado.Concepto = Convert.ToString(reader["concepto"]);
                            dtReporteEstadosCuentaConciliado.Retiros = Convert.ToString(reader["retiros"]);
                            dtReporteEstadosCuentaConciliado.Depositos = Convert.ToString(reader["depositos"]);
                            dtReporteEstadosCuentaConciliado.SaldoFinal = Convert.ToString(reader["saldofinal"]);
                            dtReporteEstadosCuentaConciliado.ConceptoConciliado = Convert.ToString(reader["ConceptoConciliado"]);
                            dtReporteEstadosCuentaConciliado.DocumentoConciliado = Convert.ToString(reader["DocumentoConciliado"]);

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
                #endregion
            }
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

            public List<DetalleCaja> consultarCajas(Conexion _conexion, int Caja)
            {
                try
                {
                    List<DetalleCaja> ListaRetorno = new List<DetalleCaja>();
                    _conexion.Comando.CommandType = CommandType.StoredProcedure;
                    _conexion.Comando.CommandText = "spCBConsultaCajasCorte";
                    _conexion.Comando.Parameters.Clear();
                    _conexion.Comando.Parameters.Add(new SqlParameter("@Caja", System.Data.SqlDbType.TinyInt)).Value = Caja;
                    SqlDataReader reader = _conexion.Comando.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            DetalleCaja dato = new DetalleCaja(
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

