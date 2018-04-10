using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.ReglasDeNegocio;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Conciliacion.RunTime.DatosSQL
{
    public class InformeBancarioDatos : InformeBancario
    {
        public InformeBancarioDatos(IMensajesImplementacion implementadorMensajes)
                    : base(implementadorMensajes)
        {
        }

        public InformeBancarioDatos(DetallePosicionDiariaBancos detalleposiciondiariabancos, IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public override List<DetallePosicionDiariaBancos> consultaPosicionDiariaBanco(Conexion _conexion, DateTime FechaIni, DateTime FechaFin, string Banco, string CuentaBanco, string Status, string StatusConcepto)
        {
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
                                reader["ConsecutivoFlujo"] == System.DBNull.Value ? 0 : Convert.ToInt32(reader["ConsecutivoFlujo"]),
                                //Convert.ToInt32(reader["ConsecutivoFlujo"]),
                                Convert.ToDateTime(reader["Fecha"]),
                                Convert.ToString(reader["Referencia"]),
                                Convert.ToString(reader["Concepto"]),
                                Convert.ToDecimal(reader["Retiros"]),
                                Convert.ToDecimal(reader["Depositos"]),
                                Convert.ToDecimal(reader["SaldoFinal"]),
                                reader["ConceptoConciliado"] == System.DBNull.Value ? "" : Convert.ToString(reader["ConceptoConciliado"]),
                                reader["DocumentoConciliado"] == System.DBNull.Value ? "" : Convert.ToString(reader["DocumentoConciliado"])
                                //Convert.ToString(reader["ConceptoConciliado"]),
                                //Convert.ToString(reader["DocumentoConciliado"])
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

        public class DetallePosicionDiariaBancos
        {
            private string corporativo;
            private string sucursal;
            private int año;
            private int mes;
            private string cuentabancofinanciero;
            private int consecutivoflujo;
            private DateTime fecha;
            private string referencia;
            private string concepto;
            private decimal retiros;
            private decimal depositos;
            private decimal saldofinal;
            private string conceptoconciliado;
            private string documentoconciliado;

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
            public int Año
            {
                get { return año; }
                set { año = value; }
            }
            public int Mes
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
                get { return saldofinal; }
                set { saldofinal = value; }
            }
            public string ConceptoConciliado
            {
                get { return conceptoconciliado; }
                set { conceptoconciliado = value; }
            }
            public string DocumentoConciliado
            {
                get { return documentoconciliado; }
                set { documentoconciliado = value; }
            }

            public DetallePosicionDiariaBancos(string corporativo,
                        string sucursal,
                        int año,
                        int mes,
                        string cuentabancofinanciero,
                        int consecutivoflujo,
                        DateTime fecha,
                        string referencia,
                        string concepto,
                        decimal retiros,
                        decimal depositos,
                        decimal saldofinal,
                        string conceptoconciliado,
                        string documentoconciliado)
            {
                this.corporativo = corporativo;
                this.sucursal = sucursal;
                this.año = año;
                this.mes = mes;
                this.cuentabancofinanciero = cuentabancofinanciero;
                this.consecutivoflujo = consecutivoflujo;
                this.fecha = fecha;
                this.referencia = referencia;
                this.concepto = concepto;
                this.retiros = retiros;
                this.depositos = depositos;
                this.saldofinal = saldofinal;
                this.conceptoconciliado = conceptoconciliado;
                this.documentoconciliado = documentoconciliado;
            }

            //public DetallePosicionDiariaBancos CrearObjeto()
            //{
            //return new DetallePosicionDiariaBancos(this.ImplementadorMensajes);
            //}
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

    }

}
