using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using Conciliacion.Migracion.Runtime.ReglasNegocio;

namespace Conciliacion.Migracion.Runtime.SqlDatos
{
    class ImportacionAplicacionDatos : ImportacionAplicacion
    {
        public ImportacionAplicacionDatos(int corporativo, int sucursal, int año, string cuentabanco, int tipofuenteinformacion, DateTime finicial, DateTime ffinal, DateTime falta, string procedimiento, 
            string usuario, string statusConciliacion, int folio, string servidor, string basededatos, string usuarioconsulta, string pass)
            : base(corporativo, sucursal, año, cuentabanco, tipofuenteinformacion, finicial, ffinal, falta, procedimiento, usuario, statusConciliacion,folio, servidor, basededatos, usuarioconsulta, pass)
        {
        }

        public ImportacionAplicacionDatos(int corporativo, int sucursal, int año, string cuentabanco, DateTime finicial, DateTime ffinal, DateTime falta,
            string usuario, string statusConciliacion, int folio, string pass, List<RunTime.ReglasDeNegocio.ImportacionAplicacion> listadoExtractores)
            : base(corporativo, sucursal, año, cuentabanco, finicial, ffinal, falta, usuario, statusConciliacion, folio, pass, listadoExtractores)
        {
        }

        public override bool PeriodoFechasOcupado(DateTime Finicio, DateTime FFinal)
        {
            bool resultado = false;
            using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spExisteFechaTablaDestino", cnn);
                    comando.Parameters.Add("@FInicial", System.Data.SqlDbType.DateTime).Value = Finicio;
                    comando.Parameters.Add("@FFinal", System.Data.SqlDbType.DateTime).Value = FFinal;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.CommandTimeout = 500;
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        resultado = Convert.ToBoolean(reader["Existe"]);
                    }
                }
                catch (Exception ex)
                {
                    stackTrace = new StackTrace();
                    this.ImplementadorMensajes.MostrarMensaje("Erros al consultar la informacion.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.StackTrace);
                    stackTrace = null;
                }
                return resultado;
            }
        }

        public override List<TablaDestinoDetalle> LlenarObjetosDestinoDestalle()
        {
            List<TablaDestinoDetalle> datos = new List<TablaDestinoDetalle>();
            using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
            {                

                try
                {
                    cnn.Open();

                    SqlCommand comando = new SqlCommand(this.NombreSp, cnn);
                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = this.TablaDestino.IdCorporativo;
                    comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = this.TablaDestino.IdSucursal;
                    comando.Parameters.Add("@FInicial", System.Data.SqlDbType.DateTime).Value = this.TablaDestino.FInicial;
                    comando.Parameters.Add("@FFinal", System.Data.SqlDbType.DateTime).Value = this.TablaDestino.FFinal;
                    comando.Parameters.Add("@CuentaBanco", System.Data.SqlDbType.VarChar).Value = this.TablaDestino.CuentaBancoFinanciero;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;


                       comando.CommandTimeout = 500;
                    SqlDataReader reader = comando.ExecuteReader();
                    
                    while (reader.Read())
                    {

                        TablaDestinoDetalle dato = new TablaDestinoDetalleDatos();
                        dato.CuentaBancaria = Convert.ToString(reader["CuentaBancaria"]);
                        dato.FOperacion = Convert.ToDateTime(reader["FOperacion"]);
                        dato.FMovimiento = Convert.ToDateTime(reader["FMovimiento"]);
                        dato.Descripcion = Convert.ToString(reader["Descripcion"]);
                        dato.Referencia = Convert.ToString(reader["Referencia"]);
                        dato.Transaccion = Convert.ToString(reader["Transaccion"]);
                        dato.Deposito = Convert.ToDouble(reader["Deposito"]);
                        dato.Retiro = Convert.ToDouble(reader["Retiro"]);
                        dato.SaldoInicial = Convert.ToDouble(reader["SaldoInicial"]);
                        dato.SaldoFinal = Convert.ToDouble(reader["SaldoFinal"]);
                        dato.Movimiento = Convert.ToString(reader["Movimiento"]);
                        dato.CuentaTercero = Convert.ToString(reader["CuentaTercero"]);
                        dato.Cheque = Convert.ToString(reader["Cheque"]);
                        dato.RFCTercero = Convert.ToString(reader["RFCTercero"]);
                        dato.NombreTercero = Convert.ToString(reader["NombreTercero"]);
                        dato.ClabeTercero = Convert.ToString(reader["ClabeTercero"]);
                        dato.Concepto = Convert.ToString(reader["Concepto"]);
                        dato.Poliza = Convert.ToString(reader["Poliza"]);
                        dato.IdCaja = Convert.ToInt32(reader["IdCaja"]);
                        dato.IdStatusConcepto = Convert.ToInt32(reader["IdStatusConcepto"]);
                        dato.IdStatusConciliacion = Convert.ToString(reader["IdStatusConciliacion"]);
                        dato.IdConceptoBanco = Convert.ToInt32(reader["IdConceptoBanco"]);
                        dato.IdMotivoNoConciliado = Convert.ToInt32(reader["IdMotivoNoConciliado"]);
                        dato.TipoFuenteInformacion = this.TablaDestino.TipoFuenteInformacion.Id;
                        datos.Add(dato);
                    }
                }
                catch (SqlException ex)
                {
                    stackTrace = new StackTrace();
                    this.ImplementadorMensajes.MostrarMensaje("Erros al consultar la informacion.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.StackTrace);
                    stackTrace = null;
                }
                catch (Exception ex)
                {
                    stackTrace = new StackTrace();
                    this.ImplementadorMensajes.MostrarMensaje("Erros al consultar la informacion.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.StackTrace);
                    stackTrace = null;
                }
                return datos;
            }
        }
    }
}
