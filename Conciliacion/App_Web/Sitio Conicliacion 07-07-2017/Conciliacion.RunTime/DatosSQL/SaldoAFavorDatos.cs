using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.ReglasDeNegocio;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;

namespace Conciliacion.RunTime.DatosSQL
{
    public class SaldoAFavorDatos : SaldoAFavor
    {
        Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
        public SaldoAFavorDatos(MensajesImplementacion implementadorMensajes)
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
            decimal Monto, 
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
            int SecuenciaExterno,MensajesImplementacion implementadorMensajes)
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
                using (SqlConnection cnn = new SqlConnection(objApp.CadenaConexion))
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
                    = (this.FolioMovimiento > 0 ? this.FolioMovimiento : (object)System.DBNull.Value);
                _conexion.Comando.Parameters.Add(new SqlParameter("@AñoMovimiento", SqlDbType.Int)).Value = this.AñoMovimiento;
                _conexion.Comando.Parameters.Add(new SqlParameter("@TipoMovimientoAConciliar", SqlDbType.SmallInt)).Value = this.TipoMovimientoAConciliar;

                _conexion.Comando.Parameters.Add(new SqlParameter("@EmpresaContable", SqlDbType.Int)).Value 
                    = this.EmpresaContable > 0 ? this.EmpresaContable : (object)System.DBNull.Value;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Caja", SqlDbType.TinyInt)).Value 
                    = this.Caja > 0 ? this.Caja : (object)System.DBNull.Value;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FOperacion", SqlDbType.DateTime)).Value 
                    = this.FOperacion > DateTime.MinValue ? this.FOperacion : (object)System.DBNull.Value;
                _conexion.Comando.Parameters.Add(new SqlParameter("@TipoFicha", SqlDbType.Int)).Value 
                    = this.TipoFicha > 0 ? this.TipoFicha : (object)System.DBNull.Value;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Consecutivo", SqlDbType.Int)).Value 
                    = this.Consecutivo > 0 ? this.Consecutivo : (object)System.DBNull.Value;
                _conexion.Comando.Parameters.Add(new SqlParameter("@TipoAplicacionIngreso", SqlDbType.TinyInt)).Value 
                    = this.TipoAplicacionIngreso > 0 ? this.TipoAplicacionIngreso : (object)System.DBNull.Value;
                _conexion.Comando.Parameters.Add(new SqlParameter("@ConsecutivoTipoAplicacion", SqlDbType.Int)).Value 
                    = this.ConsecutivoTipoAplicacion > 0 ? this.ConsecutivoTipoAplicacion : (object)System.DBNull.Value;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Factura", SqlDbType.Int)).Value 
                    = this.Factura > 0 ? this.Factura : (object)System.DBNull.Value;
                _conexion.Comando.Parameters.Add(new SqlParameter("@AñoCobro", SqlDbType.SmallInt)).Value 
                    = this.AñoCobro > 0 ? this.AñoCobro : (object)System.DBNull.Value;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Cobro", SqlDbType.Int)).Value 
                    = this.Cobro > 0 ? this.Cobro : (object)System.DBNull.Value;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Cliente", SqlDbType.Int)).Value
                    = this.Cliente > 0 ? this.Cliente : (object)System.DBNull.Value;

                _conexion.Comando.Parameters.Add(new SqlParameter("@Monto", SqlDbType.Money)).Value = this.Monto;
                _conexion.Comando.Parameters.Add(new SqlParameter("@StatusMovimiento", SqlDbType.VarChar)).Value = this.StatusMovimiento;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FMovimiento", SqlDbType.DateTime)).Value = this.FMovimiento;
                _conexion.Comando.Parameters.Add(new SqlParameter("@StatusConciliacion", SqlDbType.VarChar)).Value = this.StatusConciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FConciliacion", SqlDbType.DateTime)).Value = this.FConciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@CorporativoConciliacion", SqlDbType.Int)).Value = this.CorporativoConciliacion != 0 ? this.CorporativoConciliacion : SqlInt32.Null;
                _conexion.Comando.Parameters.Add(new SqlParameter("@SucursalConciliacion", SqlDbType.Int)).Value = this.SucursalConciliacion != 0 ? this.SucursalConciliacion : SqlInt32.Null;
                _conexion.Comando.Parameters.Add(new SqlParameter("@AñoConciliacion", SqlDbType.Int)).Value = this.AñoConciliacion != 0 ? this.AñoConciliacion : SqlInt32.Null;
                _conexion.Comando.Parameters.Add(new SqlParameter("@MesConciliacion", SqlDbType.SmallInt)).Value = this.MesConciliacion != 0 ? this.MesConciliacion : SqlInt16.Null;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FolioConciliacion", SqlDbType.Int)).Value = this.FolioConciliacion != 0 ? this.FolioConciliacion : SqlInt32.Null;
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

        public override bool RegistrarCobro(Conexion _conexion)
        {
            bool resultado = false;
            try
            {
                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBSaldoAFavorRegistraCobro";
                _conexion.Comando.Parameters.Clear();
                
                _conexion.Comando.Parameters.Add(new SqlParameter("@CorporativoExterno", SqlDbType.TinyInt)).Value  = this.CorporativoExterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@SucursalExterno", SqlDbType.TinyInt)).Value     = this.SucursalExterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@AñoExterno", SqlDbType.Int)).Value              = this.AñoExterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FolioExterno", SqlDbType.Int)).Value            = this.FolioExterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@SecuenciaExterno", SqlDbType.Int)).Value        = this.SecuenciaExterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@AñoCobro", SqlDbType.SmallInt)).Value           = this.AñoCobro;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Cobro", SqlDbType.Int)).Value                   = this.Cobro;

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

        public override bool ExisteExterno(Conexion _conexion)
        {
            bool resultado = false;
            try
            {
                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBSaldoAFavorExiste";
                _conexion.Comando.Parameters.Clear();

                _conexion.Comando.Parameters.Add(new SqlParameter("@CorporativoExterno", SqlDbType.TinyInt)).Value  = this.CorporativoExterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@SucursalExterno", SqlDbType.TinyInt)).Value     = this.SucursalExterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@AñoExterno", SqlDbType.Int)).Value              = this.AñoExterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FolioExterno", SqlDbType.Int)).Value            = this.FolioExterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@SecuenciaExterno", SqlDbType.Int)).Value        = this.SecuenciaExterno;

                SqlDataReader reader = _conexion.Comando.ExecuteReader();
                while (reader.Read())
                {
                    resultado = Convert.ToInt32(reader["TotalRegistros"]) > 0;
                    break;
                }
                reader.Close();
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

        public override bool AgregarCobro(Conexion _conexion)
        {
            bool resultado = false;
            try
            {
                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBSaldoAFavorRegistraCobroNuevo";
                _conexion.Comando.Parameters.Clear();

                _conexion.Comando.Parameters.Add(new SqlParameter("@CorporativoExterno", SqlDbType.TinyInt)).Value = this.CorporativoExterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@SucursalExterno", SqlDbType.TinyInt)).Value = this.SucursalExterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@AñoExterno", SqlDbType.Int)).Value = this.AñoExterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FolioExterno", SqlDbType.Int)).Value = this.FolioExterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@SecuenciaExterno", SqlDbType.Int)).Value = this.SecuenciaExterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@AñoCobro", SqlDbType.SmallInt)).Value = this.AñoCobro;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Cobro", SqlDbType.Int)).Value = this.Cobro;

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
