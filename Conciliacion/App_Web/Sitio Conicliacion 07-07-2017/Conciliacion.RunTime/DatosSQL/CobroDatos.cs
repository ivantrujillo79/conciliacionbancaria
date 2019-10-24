using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.ReglasDeNegocio;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Conciliacion.RunTime.DatosSQL
{
    public class CobroDatos : Cobro
    {

        public CobroDatos(MensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public CobroDatos(short añocobro, int numcobro, string numerocheque, decimal total, decimal saldo, string numerocuenta, string numerocuentadestino,
            DateTime fcheque, int cliente, short banco, short bancoorigen, string observaciones, string status, short tipocobro, Boolean alta,
            string usuario, Boolean saldoafavor, int sucursalbancaria, string descripcion, int clientepago, List<ReferenciaConciliadaPedido> listapedidos, decimal ImporteComision, decimal IvaComision, MensajesImplementacion implementadorMensajes)
            : base(añocobro, numcobro, numerocheque, total, saldo, numerocuenta, numerocuentadestino,
             fcheque, cliente, banco, bancoorigen, observaciones, status, tipocobro, alta,
             usuario, saldoafavor, sucursalbancaria, descripcion, clientepago, listapedidos, ImporteComision, IvaComision, implementadorMensajes)
        {
        }


        public override Cobro CrearObjeto()
        {
            return new CobroDatos(ImplementadorMensajes);
        }


        /*public override bool ChequeTarjetaAltaModifica()
        {
            bool resultado = false;
            // Cobro cobro = new CobroDatos(this.implementadorMensajes);
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBChequeTarjetaAltaModifica", cnn);
                    comando.Parameters.Add("@NumeroCheque", System.Data.SqlDbType.Char).Value = this.NumeroCheque;
                    comando.Parameters.Add("@Total", System.Data.SqlDbType.Decimal).Value = this.Total;
                    comando.Parameters.Add("@Saldo", System.Data.SqlDbType.Decimal).Value = this.Saldo;
                    comando.Parameters.Add("@NumeroCuenta", System.Data.SqlDbType.Char).Value = this.NumeroCuenta;
                    comando.Parameters.Add("@NumeroCuentaDestino", System.Data.SqlDbType.Char).Value = this.NumeroCuentaDestino;
                    comando.Parameters.Add("@FCheque", System.Data.SqlDbType.DateTime).Value = this.FCheque;
                    comando.Parameters.Add("@Cliente", System.Data.SqlDbType.Int).Value = this.Cliente;
                    comando.Parameters.Add("@Banco", System.Data.SqlDbType.SmallInt).Value = this.Banco;
                    comando.Parameters.Add("@BancoOrigen", System.Data.SqlDbType.SmallInt).Value = this.BancoOrigen;
                    comando.Parameters.Add("@Observaciones", System.Data.SqlDbType.Char).Value = this.Observaciones;
                    comando.Parameters.Add("@Estatus", System.Data.SqlDbType.Char).Value = this.Status;
                    comando.Parameters.Add("@TipoCobro", System.Data.SqlDbType.SmallInt).Value = this.TipoCobro;
                    comando.Parameters.Add("@Alta", System.Data.SqlDbType.Bit).Value = this.Alta;
                    comando.Parameters.Add("@Usuario", System.Data.SqlDbType.Char).Value = this.Usuario;
                    comando.Parameters.Add("@SaldoAFavor", System.Data.SqlDbType.Bit).Value = this.SaldoAFavor;
                    //comando.Parameters.Add("@SucursalBancaria", System.Data.SqlDbType.SmallInt).Value = this.SucursalBancaria;
                    //comando.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar).Value = this.Descripcion;

                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        this.AñoCobro = Convert.ToInt16(reader["AñoCobro"]);
                        this.NumCobro = Convert.ToInt32(reader["Cobro"]);
                    }

                }
                resultado = true;
            }
            catch (SqlException ex)
            {
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("No se pudo guardar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }

            return resultado;
        }*/

        public override bool ChequeTarjetaAltaModifica(Conexion _conexion)
        {
            bool resultado = false;
            SqlDataReader drConsulta = null;
            // Cobro cobro = new CobroDatos(this.implementadorMensajes);

            try
            {

                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBChequeTarjetaAltaModifica";
                _conexion.Comando.Parameters.Clear();
                _conexion.Comando.Parameters.Add(new SqlParameter("@NumeroCheque", System.Data.SqlDbType.Char)).Value =
                    this.NumeroCheque;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Total", System.Data.SqlDbType.Decimal)).Value =
                    this.Total;

                _conexion.Comando.Parameters.Add(new SqlParameter("@Saldo", System.Data.SqlDbType.Decimal)).Value = this.Saldo;

                _conexion.Comando.Parameters.Add(new SqlParameter("@NumeroCuenta", System.Data.SqlDbType.Char)).Value =
                    this.NumeroCuenta;
                _conexion.Comando.Parameters.Add(new SqlParameter("@NumeroCuentaDestino", System.Data.SqlDbType.Char))
                    .Value = this.NumeroCuentaDestino;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FCheque", System.Data.SqlDbType.DateTime)).Value =
                    this.FCheque;
                if (this.ClientePago==0)
                {
                    _conexion.Comando.Parameters.Add(new SqlParameter("@Cliente", System.Data.SqlDbType.Int)).Value =
                   this.Cliente;
                }
                else
                {
                    _conexion.Comando.Parameters.Add(new SqlParameter("@Cliente", System.Data.SqlDbType.Int)).Value =
                  this.ClientePago;
                }
               


                _conexion.Comando.Parameters.Add(new SqlParameter("@Banco", System.Data.SqlDbType.SmallInt)).Value =
                    this.Banco;
                _conexion.Comando.Parameters.Add(new SqlParameter("@BancoOrigen", System.Data.SqlDbType.SmallInt)).Value
                    = this.BancoOrigen;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Observaciones", System.Data.SqlDbType.Char)).Value =
                    this.Observaciones;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Estatus", System.Data.SqlDbType.Char)).Value =
                    this.Status;
                _conexion.Comando.Parameters.Add(new SqlParameter("@TipoCobro", System.Data.SqlDbType.SmallInt)).Value =
                    this.TipoCobro;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Alta", System.Data.SqlDbType.Bit)).Value = this.Alta;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Usuario", System.Data.SqlDbType.Char)).Value =
                    this.Usuario;
                _conexion.Comando.Parameters.Add(new SqlParameter("@SaldoAFavor", System.Data.SqlDbType.Bit)).Value =
                    this.SaldoAFavor;

                drConsulta = _conexion.Comando.ExecuteReader();

                if (drConsulta.HasRows)
                {
                    while (drConsulta.Read())
                    {
                        this.AñoCobro = Convert.ToInt16(drConsulta["AñoCobro"]);
                        this.NumCobro = Convert.ToInt32(drConsulta["Cobro"]);
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

        /*public override bool MovimientoCajaCobroAlta(short caja, DateTime foperacion, short consecutivo, int folio)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBMovimientoCajaCobroAlta", cnn);
                    comando.Parameters.Add("@Caja", System.Data.SqlDbType.SmallInt).Value = caja;
                    comando.Parameters.Add("@FOperacion", System.Data.SqlDbType.DateTime).Value = foperacion;
                    comando.Parameters.Add("@Consecutivo", System.Data.SqlDbType.SmallInt).Value = consecutivo;
                    comando.Parameters.Add("@Folio", System.Data.SqlDbType.Int).Value = folio;
                    comando.Parameters.Add("@AnoCobro", System.Data.SqlDbType.SmallInt).Value = this.AñoCobro;
                    comando.Parameters.Add("@Cobro", System.Data.SqlDbType.Int).Value = this.NumCobro;

                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                }
                resultado = true;
            }
            catch (SqlException ex)
            {
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("No se pudo guardar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }

            return resultado;
        }*/

        public override bool MovimientoCajaCobroAlta(short caja, DateTime foperacion, short consecutivo, int folio, Conexion _conexion)
        {
            bool resultado = false;
            try
            {

                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBMovimientoCajaCobroAlta";
                _conexion.Comando.Parameters.Clear();
                _conexion.Comando.Parameters.Add(new SqlParameter("@Caja", System.Data.SqlDbType.SmallInt)).Value = caja;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FOperacion", System.Data.SqlDbType.DateTime)).Value = foperacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Consecutivo", System.Data.SqlDbType.SmallInt)).Value = consecutivo;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Folio", System.Data.SqlDbType.Int)).Value = folio;
                _conexion.Comando.Parameters.Add(new SqlParameter("@AnoCobro", System.Data.SqlDbType.SmallInt)).Value = this.AñoCobro;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Cobro", System.Data.SqlDbType.Int)).Value = this.NumCobro;

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

        /*public override bool MovimientoCajaEntradaAlta(short caja, DateTime foperacion, short consecutivo, int folio)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBMovimientoCajaEntradaAlta", cnn);
                    comando.Parameters.Add("@Caja", System.Data.SqlDbType.SmallInt).Value = caja;
                    comando.Parameters.Add("@FOperacion", System.Data.SqlDbType.DateTime).Value = foperacion;
                    comando.Parameters.Add("@Consecutivo", System.Data.SqlDbType.SmallInt).Value = consecutivo;
                    comando.Parameters.Add("@Folio", System.Data.SqlDbType.Int).Value = folio;
                    comando.Parameters.Add("@AnoCobro", System.Data.SqlDbType.SmallInt).Value = this.AñoCobro;
                    comando.Parameters.Add("@Cobro", System.Data.SqlDbType.Int).Value = this.NumCobro;
                    comando.Parameters.Add("@Pesos", System.Data.SqlDbType.Decimal).Value = this.Total;

                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                }
                resultado = true;
            }
            catch (SqlException ex)
            {
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("No se pudo guardar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }

            return resultado;
        }*/


        public override bool MovimientoCajaEntradaAlta(short caja, DateTime foperacion, short consecutivo, int folio, Conexion _conexion)
        {
            bool resultado = false;
            try
            {

                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBMovimientoCajaEntradaAlta";
                _conexion.Comando.Parameters.Clear();
                _conexion.Comando.Parameters.Add(new SqlParameter("@Caja", System.Data.SqlDbType.SmallInt)).Value = caja;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FOperacion", System.Data.SqlDbType.DateTime)).Value = foperacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Consecutivo", System.Data.SqlDbType.SmallInt)).Value = consecutivo;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Folio", System.Data.SqlDbType.Int)).Value = folio;
                _conexion.Comando.Parameters.Add(new SqlParameter("@AnoCobro", System.Data.SqlDbType.SmallInt)).Value = this.AñoCobro;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Cobro", System.Data.SqlDbType.Int)).Value = this.NumCobro;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Pesos", System.Data.SqlDbType.Decimal)).Value = this.Total;
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

        /*public override bool ActualizaPagoReferenciado(short caja, DateTime foperacion, short consecutivo, int folio)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaPagoReferenciado", cnn);
                    comando.Parameters.Add("@Caja", System.Data.SqlDbType.SmallInt).Value = caja;
                    comando.Parameters.Add("@FOperacion", System.Data.SqlDbType.DateTime).Value = foperacion;
                    comando.Parameters.Add("@Consecutivo", System.Data.SqlDbType.SmallInt).Value = consecutivo;
                    comando.Parameters.Add("@Folio", System.Data.SqlDbType.Int).Value = folio;

                    comando.Parameters.Add("@NumeroCheque", System.Data.SqlDbType.Char).Value = this.NumeroCheque;
                    comando.Parameters.Add("@Total", System.Data.SqlDbType.Decimal).Value = this.Total;
                    comando.Parameters.Add("@NumeroCuenta", System.Data.SqlDbType.VarChar).Value =
                        this.NumeroCuentaDestino;//this.NumeroCuenta;
                    //comando.Parameters.Add("@NumeroCuentaDestino", System.Data.SqlDbType.VarChar).Value = this.NumeroCuentaDestino;
                    comando.Parameters.Add("@Cliente", System.Data.SqlDbType.Int).Value = this.Cliente;

                    comando.Parameters.Add("@FCheque", System.Data.SqlDbType.DateTime).Value = this.FCheque;
                    comando.Parameters.Add("@Banco", System.Data.SqlDbType.SmallInt).Value = this.Banco;
                    comando.Parameters.Add("@SucursalBancaria", System.Data.SqlDbType.Int).Value = this.SucursalBancaria;
                    //comando.Parameters.Add("@Estatus", System.Data.SqlDbType.Char).Value = this.Status;
                    comando.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar).Value = this.Descripcion;
                    comando.Parameters.Add("@AñoCobro", System.Data.SqlDbType.SmallInt).Value = this.AñoCobro;
                    comando.Parameters.Add("@Cobro", System.Data.SqlDbType.Int).Value = this.NumCobro;

                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.ExecuteNonQuery();

                }
                resultado = true;
            }
            catch (SqlException ex)
            {
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("No se pudo guardar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }

            return resultado;
        }*/

        public override bool ActualizaPagoReferenciado(short caja, DateTime foperacion, short consecutivo, int folio, Conexion _conexion)
        {
            bool resultado = false;
            try
            {
                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBActualizaPagoReferenciado";
                _conexion.Comando.Parameters.Clear();
                _conexion.Comando.Parameters.Add(new SqlParameter("@Caja", System.Data.SqlDbType.SmallInt)).Value = caja;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FOperacion", System.Data.SqlDbType.DateTime)).Value = foperacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Consecutivo", System.Data.SqlDbType.SmallInt)).Value = consecutivo;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Folio", System.Data.SqlDbType.Int)).Value = folio;

                _conexion.Comando.Parameters.Add(new SqlParameter("@NumeroCheque", System.Data.SqlDbType.Char)).Value = this.NumeroCheque;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Total", System.Data.SqlDbType.Decimal)).Value = this.Total;
                _conexion.Comando.Parameters.Add(new SqlParameter("@NumeroCuenta", System.Data.SqlDbType.VarChar)).Value =
                    this.NumeroCuentaDestino;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Cliente", System.Data.SqlDbType.Int)).Value = this.Cliente;

                _conexion.Comando.Parameters.Add(new SqlParameter("@FCheque", System.Data.SqlDbType.DateTime)).Value = this.FCheque;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Banco", System.Data.SqlDbType.SmallInt)).Value = this.Banco;
                _conexion.Comando.Parameters.Add(new SqlParameter("@SucursalBancaria", System.Data.SqlDbType.Int)).Value = this.SucursalBancaria;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar)).Value = this.Descripcion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@AñoCobro", System.Data.SqlDbType.SmallInt)).Value = this.AñoCobro;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Cobro", System.Data.SqlDbType.Int)).Value = this.NumCobro;

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
