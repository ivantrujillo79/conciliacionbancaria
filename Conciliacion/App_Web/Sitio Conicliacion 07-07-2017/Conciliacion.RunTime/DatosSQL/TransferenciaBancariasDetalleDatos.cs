using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using Conciliacion.RunTime.ReglasDeNegocio;

namespace Conciliacion.RunTime.DatosSQL
{
    class TransferenciaBancariasDetalleDatos:TransferenciaBancariasDetalle
    {
        public TransferenciaBancariasDetalleDatos(IMensajesImplementacion implementadorMensajes)
            :base(implementadorMensajes)
        {

        }

        public TransferenciaBancariasDetalleDatos(short corporativo, int sucursal, int año, int folio, int secuencia,
                                             short corporativoDeatalle, int sucursalDetalle,
                                             string cuentaBanco, short entrada, decimal cargo, decimal abono,
                                             IMensajesImplementacion implementadorMensajes)
            : base(corporativo, sucursal, año, folio, secuencia, corporativoDeatalle, sucursalDetalle, cuentaBanco, entrada, cargo,
                abono, implementadorMensajes)
        {

        }

        public override TransferenciaBancariasDetalle CrearObjeto()
        {
            return new TransferenciaBancariasDetalleDatos(this.ImplementadorMensajes);
        }

        /*public override bool Registrar()
        {
            bool resultado = true;
            using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaTransferenciaBancariaDetalle", cnn);
                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = this.Corporativo;
                    comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = this.Sucursal;
                    comando.Parameters.Add("@Año", System.Data.SqlDbType.Int).Value = this.Año;
                    comando.Parameters.Add("@Folio", System.Data.SqlDbType.Int).Value = this.Folio;
                    comando.Parameters.Add("@CorporativoDetalle", System.Data.SqlDbType.TinyInt).Value = this.CorporativoDeatalle;
                    comando.Parameters.Add("@SucursalDetalle", System.Data.SqlDbType.Int).Value = this.SucursalDetalle;
                    comando.Parameters.Add("@CuentaBanco", System.Data.SqlDbType.VarChar).Value = this.CuentaBanco;
                    comando.Parameters.Add("@Entrada", System.Data.SqlDbType.Bit).Value = this.Entrada;
                    comando.Parameters.Add("@Cargo", System.Data.SqlDbType.Money).Value = this.Cargo;
                    comando.Parameters.Add("@Abono", System.Data.SqlDbType.Money).Value = this.Abono;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.ExecuteNonQuery();
                    //this.ImplementadorMensajes.MostrarMensaje("Registro Guardado Con éxito");

                }
                catch (Exception ex)
                {
                    stackTrace = new StackTrace();
                    this.ImplementadorMensajes.MostrarMensaje("No se pudo guardar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                    stackTrace = null;
                    resultado = false;
                    throw;
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
            }
            return resultado;
        }*/

        public override bool Registrar(Conexion _conexion)
        {
            bool resultado = false;


            try
            {
                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBActualizaTransferenciaBancariaDetalle";
                _conexion.Comando.Parameters.Clear();
                _conexion.Comando.Parameters.Add(new SqlParameter("@Corporativo", System.Data.SqlDbType.TinyInt)).Value = this.Corporativo;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Sucursal", System.Data.SqlDbType.Int)).Value = this.Sucursal;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Año", System.Data.SqlDbType.Int)).Value = this.Año;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Folio", System.Data.SqlDbType.Int)).Value = this.Folio;
                _conexion.Comando.Parameters.Add(new SqlParameter("@CorporativoDetalle", System.Data.SqlDbType.TinyInt)).Value = this.CorporativoDeatalle;
                _conexion.Comando.Parameters.Add(new SqlParameter("@SucursalDetalle", System.Data.SqlDbType.Int)).Value = this.SucursalDetalle;
                _conexion.Comando.Parameters.Add(new SqlParameter("@CuentaBanco", System.Data.SqlDbType.VarChar)).Value = this.CuentaBanco;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Entrada", System.Data.SqlDbType.Bit)).Value = this.Entrada;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Cargo", System.Data.SqlDbType.Money)).Value = this.Cargo;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Abono", System.Data.SqlDbType.Money)).Value = this.Abono;
                //_conexion.Comando.Parameters.Add(new SqlParameter("@Secuencia", System.Data.SqlDbType.SmallInt)).Value = this.Secuencia;
                
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
