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
    public class ExportadorInformeInternosAFuturoDatos
    {
        public class DetalleInformeInternosAFuturo
        {
            private int secuencia;
            private string banco;
            private string corporativo;
            private string cuentabancofinanciero;
            private int consecutivoflujo;
            private DateTime foperacion;
            private DateTime fmovimiento;
            private string referencia;
            private string concepto;
            private decimal retiros;
            private decimal depositos;

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
            public int Secuencia
            {
                get { return secuencia; }
                set { secuencia = value; }
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
            public DateTime FMovimiento
            {
                get { return fmovimiento; }
                set { fmovimiento = value; }
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

            public DetalleInformeInternosAFuturo()
            {
            }

            public DetalleInformeInternosAFuturo(
                int secuencia,
                string banco,
                string corporativo,
                string cuentabancofinanciero,
                int consecutivoflujo,
                DateTime foperacion,
                DateTime fmovimiento,
                string referencia,
                string concepto,
                decimal retiros,
                decimal depositos)
            {
                this.banco = banco;
                this.corporativo = corporativo;
                this.secuencia = secuencia;
                this.cuentabancofinanciero = cuentabancofinanciero;
                this.consecutivoflujo = consecutivoflujo;
                this.foperacion = foperacion;
                this.fmovimiento = fmovimiento;
                this.referencia = referencia;
                this.concepto = concepto;
                this.retiros = retiros;
                this.depositos = depositos;
            }

            public List<DetalleInformeInternosAFuturo> consultaInformeInternosAFuturo(Conexion _conexion, DateTime FechaIni, DateTime FechaFin, string Banco, string CuentaBanco)
            {
                List<DetalleInformeInternosAFuturo> ListaResultado = new List<DetalleInformeInternosAFuturo>();
                try
                {
                    _conexion.Comando.CommandType = CommandType.StoredProcedure;
                    _conexion.Comando.CommandText = "spCBReporteMovimientosCanceladosAFuturoInternos";
                    _conexion.Comando.Parameters.Clear();
                    _conexion.Comando.Parameters.Add(new SqlParameter("@FechaIni", System.Data.SqlDbType.DateTime)).Value = FechaIni;
                    _conexion.Comando.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = FechaFin;
                    _conexion.Comando.Parameters.Add(new SqlParameter("@Banco", System.Data.SqlDbType.VarChar)).Value = Banco;
                    _conexion.Comando.Parameters.Add(new SqlParameter("@CuentaBancaria", System.Data.SqlDbType.VarChar)).Value = CuentaBanco;
                    SqlDataReader reader = _conexion.Comando.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            DetalleInformeInternosAFuturo dato = new DetalleInformeInternosAFuturo();
                            dato.Banco = (reader["banco"] == DBNull.Value ? "" : Convert.ToString(reader["banco"]));
                            dato.Corporativo = Convert.ToString(reader["corporativo"]);
                            dato.Secuencia = Convert.ToInt32(reader["secuencia"]);
                            dato.CuentaBancoFinanciero = Convert.ToString(reader["cuentabancofinanciero"]).Trim();
                            dato.ConsecutivoFlujo = Convert.ToInt32(reader["consecutivoflujo"] == DBNull.Value ? "0" : reader["consecutivoflujo"]); // mcc 20180503
                            dato.FOperacion = Convert.ToDateTime(reader["foperacion"]);
                            dato.FMovimiento = Convert.ToDateTime(reader["fmovimiento"]);
                            dato.Referencia = Convert.ToString(reader["referencia"]);
                            dato.Concepto = Convert.ToString(reader["concepto"]);
                            dato.Retiros = Convert.ToDecimal(reader["retiros"]);
                            dato.Depositos = Convert.ToDecimal(reader["depositos"]);
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





    }
}
