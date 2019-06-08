using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Conciliacion.RunTime.ReglasDeNegocio;


namespace Conciliacion.RunTime.DatosSQL
{
    class TransferenciaBancariasDatos:TransferenciaBancarias
    {
        #region Constructores

        public TransferenciaBancariasDatos(MensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {

        }

        public TransferenciaBancariasDatos(short corporativo, int sucursal, int año, int folio, string nombreCorporativo, string nombreSucursal,
                                      short tipoTransferencia, string referencia, DateTime fMovimiento, DateTime fAplicacion,
                                      string usuarioCaptura,DateTime fCaptura, string usuariooAprobo, DateTime fAprobado,
                                      string status, string descripcion,string bancoNombreOrigen, string cuentaBancoOrigen,
                                      string bancoNombreDestino, string cuentaBancoDestino, decimal monto, short entrada,//decimal abono,decimal cargo, 
                                      MensajesImplementacion implementadorMensajes)
            : base( corporativo,  sucursal,  año,  folio, nombreCorporativo,nombreSucursal, tipoTransferencia,
                                       referencia,  fMovimiento,  fAplicacion,
                                       usuarioCaptura, fCaptura,  usuariooAprobo,  fAprobado,
                                       status, descripcion, bancoNombreOrigen,  cuentaBancoOrigen,
                                         bancoNombreDestino,  cuentaBancoDestino, monto, entrada, //abono,cargo,
                                       implementadorMensajes)
        {

        }

        #endregion
        
        #region Metodos

        public override TransferenciaBancarias CrearObjeto()
        {
            return new TransferenciaBancariasDatos(this.ImplementadorMensajes);
        }

        /*public override bool Registrar()
        {
            bool resultado = true;
            using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaTransferenciaBancaria", cnn);
                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = this.Corporativo;
                    comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = this.Sucursal;
                    comando.Parameters.Add("@Año", System.Data.SqlDbType.Int).Value = this.Año;
                    comando.Parameters.Add("@TipoTransferencia", System.Data.SqlDbType.SmallInt).Value =this.TipoTransferencia;
                    comando.Parameters.Add("@Referencia", System.Data.SqlDbType.VarChar).Value = this.Referencia;
                    comando.Parameters.Add("@FMovimiento", System.Data.SqlDbType.DateTime).Value = this.FMovimiento;
                    comando.Parameters.Add("@FAplicacion", System.Data.SqlDbType.DateTime).Value = this.FAplicacion;
                    comando.Parameters.Add("@UsuarioCaptura", System.Data.SqlDbType.Char).Value = this.UsuarioCaptura;
                    comando.Parameters.Add("@UsuarioAprobo", System.Data.SqlDbType.Char).Value = DBNull.Value;
                    comando.Parameters.Add("@FAprobado", System.Data.SqlDbType.DateTime).Value = DBNull.Value;
                    comando.Parameters.Add("@Status", System.Data.SqlDbType.Char).Value = this.Status;
                    comando.Parameters.Add("@Descripcion", System.Data.SqlDbType.Char).Value = this.Descripcion;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        this.Folio = Convert.ToInt32(reader["Folio"]);
                    }
                    this.ImplementadorMensajes.MostrarMensaje("Registro Guardado Con éxito");

                }
                catch (Exception ex)
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

        }*/


        public override bool Registrar(Conexion _conexion)
        {
            bool resultado = false;
            SqlDataReader drConsulta = null;


            try
            {
                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBActualizaTransferenciaBancaria";
                _conexion.Comando.Parameters.Clear();
                _conexion.Comando.Parameters.Add(new SqlParameter("@Corporativo", System.Data.SqlDbType.TinyInt)).Value = this.Corporativo;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Sucursal", System.Data.SqlDbType.Int)).Value = this.Sucursal;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Año", System.Data.SqlDbType.Int)).Value = this.Año;
                _conexion.Comando.Parameters.Add(new SqlParameter("@TipoTransferencia", System.Data.SqlDbType.SmallInt)).Value = this.TipoTransferencia;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Referencia", System.Data.SqlDbType.VarChar)).Value = this.Referencia;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FMovimiento", System.Data.SqlDbType.DateTime)).Value = this.FMovimiento;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FAplicacion", System.Data.SqlDbType.DateTime)).Value = this.FAplicacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@UsuarioCaptura", System.Data.SqlDbType.Char)).Value = this.UsuarioCaptura;
                _conexion.Comando.Parameters.Add(new SqlParameter("@UsuarioAprobo", System.Data.SqlDbType.Char)).Value = DBNull.Value;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FAprobado", System.Data.SqlDbType.DateTime)).Value = DBNull.Value;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Status", System.Data.SqlDbType.Char)).Value = this.Status;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.Char)).Value = this.Descripcion;

                drConsulta = _conexion.Comando.ExecuteReader();

                if (drConsulta.HasRows)
                {
                    while (drConsulta.Read())
                    {
                        this.Folio = Convert.ToInt32(drConsulta["Folio"]);
                    }
                }

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
            finally
            {
                if (drConsulta != null)
                {
                    if (!drConsulta.IsClosed)
                    {
                        drConsulta.Close();
                    }
                    drConsulta.Dispose();
                }
            }

            return resultado;
        }


        public override bool AplicarTransferencia(Conexion _conexion)
        {
            bool resultado = false;
            
            try
            {

                List<TransferenciaBancariasDetalle> listTransferenciaBancariasDetalle = this.ListTransferenciaBancariasDetalle;
                List<TransferenciaBancariaOrigen> listTransferenciaBancariaOrigen = this.ListTransferenciaBancariaOrigen;

               
                foreach (TransferenciaBancariasDetalle transferenciaBancariasDetalle in listTransferenciaBancariasDetalle)
                {
                    transferenciaBancariasDetalle.Folio = this.Folio;
                    transferenciaBancariasDetalle.Registrar(_conexion);
                }

                foreach (TransferenciaBancariaOrigen transferenciaBancariaOrigen in listTransferenciaBancariaOrigen)
                {
                    transferenciaBancariaOrigen.Folio = this.Folio;
                    transferenciaBancariaOrigen.Registrar(_conexion);
                }
                resultado = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }


        public override bool Guardar()
        {
            bool resultado = false;

            Conexion conexion = new Conexion();

            try
            {
                conexion.AbrirConexion(true);

                Registrar(conexion);
                AplicarTransferencia(conexion);
                
                conexion.Comando.Transaction.Commit();

                this.ImplementadorMensajes.MostrarMensaje("El Registro se guardo con éxito.");

                resultado = true;
            }
            catch (Exception ex)
            {
                if (conexion.Comando.Transaction != null)
                {
                    conexion.Comando.Transaction.Rollback();
                }
                throw ex;
            }
            finally
            {
                conexion.CerrarConexion();
            }

            return resultado;
        }

        

        public override bool CambiarStatus(short corporativo, int sucursal, int año, int folio, string usuarioaprobo,string status)
        {
            bool resultado = true;
            using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaStatusTransferenciaBancaria", cnn);
                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = corporativo;
                    comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = sucursal;
                    comando.Parameters.Add("@Año", System.Data.SqlDbType.Int).Value = año;
                    comando.Parameters.Add("@Folio", System.Data.SqlDbType.Int).Value = folio;
                    //comando.Parameters.Add("@TipoTransferencia", System.Data.SqlDbType.SmallInt).Value = 0;
                    //comando.Parameters.Add("@Referencia", System.Data.SqlDbType.VarChar).Value = "";
                    //comando.Parameters.Add("@FMovimiento", System.Data.SqlDbType.DateTime).Value = DateTime.Now;
                    //comando.Parameters.Add("@FAplicacion", System.Data.SqlDbType.DateTime).Value = DateTime.Now;
                    //comando.Parameters.Add("@UsuarioCaptura", System.Data.SqlDbType.Char).Value = "";
                    //comando.Parameters.Add("@FCaptura", System.Data.SqlDbType.DateTime).Value = DateTime.Now;
                    comando.Parameters.Add("@UsuarioAprobo", System.Data.SqlDbType.Char).Value = usuarioaprobo;
                    //comando.Parameters.Add("@FAprobado", System.Data.SqlDbType.DateTime).Value = DateTime.Now;
                    comando.Parameters.Add("@Status", System.Data.SqlDbType.Char).Value = status;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.ExecuteNonQuery();
                    this.ImplementadorMensajes.MostrarMensaje("Registro Modificado Con éxito");
                    
                }
                catch (Exception ex)
                {
                    stackTrace = new StackTrace();
                    this.ImplementadorMensajes.MostrarMensaje("No se puede cambiar el status del registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
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
