using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.ReglasDeNegocio;
using System.Data.SqlClient;
using System.Data;

namespace Conciliacion.RunTime.DatosSQL
{
    public class SaldoAFavorDatos : SaldoAFavor
    {

        public SaldoAFavorDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public SaldoAFavorDatos(
            int FolioMovimiento, 
            int AñoMovimiento, 
            Int16 TipoMovimientoAConciliar, 
            int EmpresaContable, 
            short Caja, 
            DateTime FOperacion, 
            int TipoFicha,
            int Consecutivo, 
            byte TipoAplicacionIngreso, 
            int ConsecutivoTipoAplicacion, 
            int Factura, 
            Int16 AñoCobro, 
            int Cobro, 
            int Monto, 
            string StatusMovimiento,
            DateTime FMovimiento, 
            string StatusConciliacion, 
            DateTime FConciliacion, 
            int CorporativoConciliacion, 
            int SucursalConciliacion, 
            int AñoConciliacion,
            Int16 MesConciliacion, 
            int FolioConciliacion, 
            int CorporativoExterno, 
            int SucursalExterno, 
            int AñoExterno, 
            int FolioExterno, 
            int SecuenciaExterno,IMensajesImplementacion implementadorMensajes)
            : base(FolioMovimiento,
                    AñoMovimiento,
                    TipoMovimientoAConciliar,
                    EmpresaContable,
                    Caja,
                    FOperacion,
                    TipoFicha,
                    Consecutivo,
                    TipoAplicacionIngreso,
                    ConsecutivoTipoAplicacion,
                    Factura,
                    AñoCobro,
                    Cobro,
                    Monto,
                    StatusMovimiento,
                    FMovimiento,
                    StatusConciliacion,
                    FConciliacion,
                    CorporativoConciliacion,
                    SucursalConciliacion,
                    AñoConciliacion,
                    MesConciliacion,
                    FolioConciliacion,
                    CorporativoExterno,
                    SucursalExterno,
                    AñoExterno,
                    FolioExterno,
                    SecuenciaExterno, 
                    implementadorMensajes)
        {
        }

        public override SaldoAFavor CrearObjeto()
        {
            return new SaldoAFavorDatos(this.ImplementadorMensajes);
        }

        public override List<DetalleSaldoAFavor> ConsultaSaldoAFavor(string FInicial, string FFinal, string Cliente, decimal monto, Conexion conexion)
        {
            List<DetalleSaldoAFavor> ListaRetorno = new List<DetalleSaldoAFavor>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBConsultaSaldosAFavor", cnn);
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        DetalleSaldoAFavor objDetalle = new DetalleSaldoAFavor
                        {
                            Folio = Convert.ToInt32(reader["Folio"]),
                            Cliente = reader["Cliente"].ToString(),
                            NombreCliente = reader["NombreCliente"].ToString(),
                            CuentaBancaria = reader["CuentaBancaria"].ToString(),
                            Banco = reader["Banco"].ToString(),
                            Sucursal = reader["Sucursal"].ToString(),
                            TipoCargo = reader["TipoCargo"].ToString(),
                            Global = bool.Parse(reader["Global"].ToString()),
                            Fsaldo = DateTime.Parse(reader["Fsaldo"].ToString()),
                            Importe = decimal.Parse(reader["Importe"].ToString()),
                            Conciliada = reader["Conciliada"].ToString(),
                        };
                        ListaRetorno.Add(objDetalle);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListaRetorno;
        }

        public override bool Guardar(Conexion _conexion)
        {
            bool resultado = false;
            try
            {
                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBInsertaMovimientoAConciliar";
                _conexion.Comando.Parameters.Clear();

                _conexion.Comando.Parameters.Add(new SqlParameter("@FolioMovimiento", SqlDbType.Int)).Value 
                    = (this.FolioMovimiento == -1 ? (object)System.DBNull.Value : this.FolioMovimiento);
                _conexion.Comando.Parameters.Add(new SqlParameter("@AñoMovimiento", SqlDbType.Int)).Value = this.AñoMovimiento;
                _conexion.Comando.Parameters.Add(new SqlParameter("@TipoMovimientoAConciliar", SqlDbType.Int)).Value = this.TipoMovimientoAConciliar;
                _conexion.Comando.Parameters.Add(new SqlParameter("@EmpresaContable", SqlDbType.Int)).Value = this.EmpresaContable;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Caja", SqlDbType.SmallInt)).Value = this.Caja;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FOperacion", SqlDbType.DateTime)).Value = this.FOperacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@TipoFicha", SqlDbType.Int)).Value = this.TipoFicha;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Consecutivo", SqlDbType.Int)).Value = this.Consecutivo;
                _conexion.Comando.Parameters.Add(new SqlParameter("@TipoAplicacionIngreso", SqlDbType.SmallInt)).Value = this.TipoAplicacionIngreso;
                _conexion.Comando.Parameters.Add(new SqlParameter("@ConsecutivoTipoAplicacion", SqlDbType.Int)).Value = this.ConsecutivoTipoAplicacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Factura", SqlDbType.Int)).Value = this.Factura;
                _conexion.Comando.Parameters.Add(new SqlParameter("@AñoCobro", SqlDbType.SmallInt)).Value = this.AñoCobro;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Cobro", SqlDbType.Int)).Value = this.Cobro;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Monto", SqlDbType.Int)).Value = this.Monto;
                _conexion.Comando.Parameters.Add(new SqlParameter("@StatusMovimiento", SqlDbType.VarChar)).Value = this.StatusMovimiento;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FMovimiento", SqlDbType.DateTime)).Value = this.FMovimiento;
                _conexion.Comando.Parameters.Add(new SqlParameter("@StatusConciliacion", SqlDbType.VarChar)).Value = this.StatusConciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FConciliacion", SqlDbType.DateTime)).Value = this.FConciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@CorporativoConciliacion", SqlDbType.Int)).Value = this.CorporativoConciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@SucursalConciliacion", SqlDbType.Int)).Value = this.SucursalConciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@AñoConciliacion", SqlDbType.Int)).Value = this.AñoConciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@MesConciliacion", SqlDbType.SmallInt)).Value = this.MesConciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FolioConciliacion", SqlDbType.Int)).Value = this.FolioConciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@CorporativoExterno", SqlDbType.Int)).Value = this.CorporativoExterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@SucursalExterno", SqlDbType.Int)).Value = this.SucursalExterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@AñoExterno", SqlDbType.Int)).Value = this.AñoExterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FolioExterno", SqlDbType.Int)).Value = this.FolioExterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@SecuenciaExterno", SqlDbType.Int)).Value = this.SecuenciaExterno;

                _conexion.Comando.ExecuteNonQuery();

                resultado = true;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultado;
        }

    }
}
