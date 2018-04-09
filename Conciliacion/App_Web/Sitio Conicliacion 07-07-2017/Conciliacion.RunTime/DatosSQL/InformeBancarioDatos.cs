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

        public override InformeBancario CrearObjeto()
        {
            return new InformeBancarioDatos(this.ImplementadorMensajes);
        }

        public class DetallePosicionDiariaBancos
        {
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
            }
        }

    }

}
