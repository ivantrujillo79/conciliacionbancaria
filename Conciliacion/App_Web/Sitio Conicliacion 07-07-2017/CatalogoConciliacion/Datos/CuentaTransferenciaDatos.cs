using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CatalogoConciliacion.ReglasNegocio;
using Conciliacion.RunTime;
using System.Data.SqlClient;
using System.Diagnostics;

namespace CatalogoConciliacion.Datos
{
    class CuentaTransferenciaDatos : CuentaTransferencia
    {
        #region Constructores

        public CuentaTransferenciaDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public CuentaTransferenciaDatos(short cuentaTransferencia, string corporativoOrigenDesc, short corporativoOrigen, string sucursalOrigenDesc, int sucursalOrigen,
                                  string cuentaBancoOrigen, int bancoOrigen, string bancoNombreOrigen, string corporativoDestinoDesc,
                                  short corporativoDestino, string sucursalDestinoDesc, int sucursalDestino, string cuentaBancoDestino,
                                  int bancoDestino, string bancoNombreDestino, string status, string usuarioAlta,
                                  DateTime FAlta, IMensajesImplementacion implemntadorMensajes)
            : base(cuentaTransferencia, corporativoOrigenDesc, corporativoOrigen, sucursalOrigenDesc, sucursalOrigen, cuentaBancoOrigen, bancoOrigen, bancoNombreOrigen,
                    corporativoDestinoDesc, corporativoDestino, sucursalDestinoDesc, sucursalDestino, cuentaBancoDestino, bancoDestino, bancoNombreDestino, status, usuarioAlta, FAlta, implemntadorMensajes)
        {
        }
        #endregion
        #region Metodos

        public override CuentaTransferencia CrearObjeto()
        {
            return new CuentaTransferenciaDatos(this.ImplementadorMensajes);
        }

        public override bool Registrar()
        {
            bool resultado = true;

            using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaCuentaTransferencia", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.Int).Value = 0;//this.Configuracion;
                    comando.Parameters.Add("@CuentaTransferencia", System.Data.SqlDbType.SmallInt).Value = this.CuentaTransferencia_;
                    comando.Parameters.Add("@CorporativoOrigen", System.Data.SqlDbType.TinyInt).Value = this.CorporativoOrigen;
                    comando.Parameters.Add("@SucursalOrigen", System.Data.SqlDbType.Int).Value = this.SucursalOrigen;
                    comando.Parameters.Add("@CuentaBancoOrigen", System.Data.SqlDbType.VarChar).Value = this.CuentaBancoOrigen;
                    comando.Parameters.Add("@BancoOrigen", System.Data.SqlDbType.Int).Value = this.BancoOrigen;
                    comando.Parameters.Add("@BancoNombreOrigen", System.Data.SqlDbType.VarChar).Value = this.BancoNombreOrigen;
                    comando.Parameters.Add("@CorporativoDestino", System.Data.SqlDbType.TinyInt).Value = this.CorporativoDestino;
                    comando.Parameters.Add("@SucursalDestino", System.Data.SqlDbType.Int).Value = this.SucursalDestino;
                    comando.Parameters.Add("@CuentaBancoDestino", System.Data.SqlDbType.VarChar).Value = this.CuentaBancoDestino;
                    comando.Parameters.Add("@BancoDestino", System.Data.SqlDbType.Int).Value = this.BancoDestino;
                    comando.Parameters.Add("@BancoNombreDestino", System.Data.SqlDbType.VarChar).Value = this.BancoNombreDestino;
                    comando.Parameters.Add("@Status", System.Data.SqlDbType.VarChar).Value = this.Status;
                    comando.Parameters.Add("@UsuarioAlta", System.Data.SqlDbType.Char).Value = this.UsuarioAlta;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.ExecuteNonQuery();
                    this.ImplementadorMensajes.MostrarMensaje("Registro Guardado Con éxito");
                }
                catch (SqlException ex)
                {
                    stackTrace = new StackTrace();
                    this.ImplementadorMensajes.MostrarMensaje("No se pudo guardar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                    stackTrace = null;
                    resultado = false;
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }

            }
            return resultado;
        }

        public override bool CambiarStatus(int cta_transferencia)
        {
            bool resultado = true;

            using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaCuentaTransferencia", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.Int).Value = 1;
                    comando.Parameters.Add("@CuentaTransferencia", System.Data.SqlDbType.SmallInt).Value =
                       cta_transferencia;
                    comando.Parameters.Add("@CorporativoOrigen", System.Data.SqlDbType.TinyInt).Value =
                        0;
                    comando.Parameters.Add("@SucursalOrigen", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@CuentaBancoOrigen", System.Data.SqlDbType.VarChar).Value =
                        "";
                    comando.Parameters.Add("@BancoOrigen", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@BancoNombreOrigen", System.Data.SqlDbType.VarChar).Value =
                        "";
                    comando.Parameters.Add("@CorporativoDestino", System.Data.SqlDbType.TinyInt).Value =
                        0;
                    comando.Parameters.Add("@SucursalDestino", System.Data.SqlDbType.Int).Value =
                        0;
                    comando.Parameters.Add("@CuentaBancoDestino", System.Data.SqlDbType.VarChar).Value =
                        "";
                    comando.Parameters.Add("@BancoDestino", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@BancoNombreDestino", System.Data.SqlDbType.VarChar).Value =
                        "";
                    comando.Parameters.Add("@Status", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@UsuarioAlta", System.Data.SqlDbType.Char).Value = "";
                    // comando.Parameters.Add("@FAlta", System.Data.SqlDbType.DateTime).Value = this.FAlta_;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.ExecuteNonQuery();
                    this.ImplementadorMensajes.MostrarMensaje("Registro Modificado Exitosamente");
                }
                catch (SqlException ex)
                {
                    stackTrace = new StackTrace();
                    this.ImplementadorMensajes.MostrarMensaje("No se puede Modificar el registro.\n\rClase :" +
                                                              this.GetType().Name + "\n\r" + "Metodo :" +
                                                              stackTrace.GetFrame(0).GetMethod().Name + "\n\r" +
                                                              "Error :" + ex.Message);
                    stackTrace = null;
                    resultado = false;
                    
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }

            }
            return resultado;
        }

        #endregion
    }
}
