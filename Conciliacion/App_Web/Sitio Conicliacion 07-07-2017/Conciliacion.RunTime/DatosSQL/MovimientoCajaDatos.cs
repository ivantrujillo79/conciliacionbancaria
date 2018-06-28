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
    public class MovimientoCajaDatos : MovimientoCaja
    {
        public MovimientoCajaDatos()
        {
            
        }

        public MovimientoCajaDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public MovimientoCajaDatos(short caja, DateTime foperacion, short consecutivo, int folio, DateTime fmovimiento, decimal total, string usuario, int empleado, string observaciones, decimal saldoafavor, List<Cobro> listacobros, IMensajesImplementacion implementadorMensajes)
            : base(caja, foperacion, consecutivo, folio, fmovimiento, total, usuario, empleado, observaciones, saldoafavor, listacobros, implementadorMensajes)
        {
        }

        public override MovimientoCaja CrearObjeto()
        {
            return new MovimientoCajaDatos(App.ImplementadorMensajes);
        }

        /*public override bool MovimientoCajaAlta()
        {
            //   MovimientoCaja movimiento = new MovimientoCajaDatos(this.implementadorMensajes);

            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBMovimientoCajaAlta", cnn);
                    comando.Parameters.Add("@Caja", System.Data.SqlDbType.SmallInt).Value = this.Caja;
                    comando.Parameters.Add("@FOperacion", System.Data.SqlDbType.DateTime).Value = this.FOperacion;
                    comando.Parameters.Add("@FMovimiento", System.Data.SqlDbType.DateTime).Value = this.FMovimiento;
                    comando.Parameters.Add("@Total", System.Data.SqlDbType.Decimal).Value = this.Total;
                    comando.Parameters.Add("@UsuarioCaptura ", System.Data.SqlDbType.Char).Value = this.Usuario;
                    comando.Parameters.Add("@Empleado", System.Data.SqlDbType.Int).Value = this.Empleado;
                    comando.Parameters.Add("@Observaciones", System.Data.SqlDbType.Char).Value = this.Observaciones;
                    comando.Parameters.Add("@SaldoAFavor", System.Data.SqlDbType.Decimal).Value = this.SaldoAFavor;

                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        this.Caja = Convert.ToInt16(reader["Caja"]);
                        this.FOperacion = Convert.ToDateTime(reader["FOperacion"]);
                        this.Consecutivo = Convert.ToInt16(reader["Consecutivo"]);
                        this.Folio = Convert.ToInt32(reader["Folio"]);
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

        public override bool MovimientoCajaAlta(Conexion _conexion)
        {
            //   MovimientoCaja movimiento = new MovimientoCajaDatos(this.implementadorMensajes);
            SqlDataReader drConsulta = null;
            

            bool resultado = false;
            try
            {
                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBMovimientoCajaAlta";
                _conexion.Comando.Parameters.Clear();
                _conexion.Comando.Parameters.Add(new SqlParameter("@Caja", System.Data.SqlDbType.SmallInt)).Value = this.Caja;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FOperacion", System.Data.SqlDbType.DateTime)).Value = this.FOperacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FMovimiento", System.Data.SqlDbType.DateTime)).Value = this.FMovimiento;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Total", System.Data.SqlDbType.Decimal)).Value = this.Total;
                _conexion.Comando.Parameters.Add(new SqlParameter("@UsuarioCaptura ", System.Data.SqlDbType.Char)).Value = this.Usuario;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Empleado", System.Data.SqlDbType.Int)).Value = this.Empleado;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Observaciones", System.Data.SqlDbType.Char)).Value = this.Observaciones;
                _conexion.Comando.Parameters.Add(new SqlParameter("@SaldoAFavor", System.Data.SqlDbType.Decimal)).Value = this.SaldoAFavor;

                drConsulta = _conexion.Comando.ExecuteReader();

                if (drConsulta.HasRows)
                {
                    while (drConsulta.Read())
                    {
                        this.Caja = Convert.ToInt16(drConsulta["Caja"]);
                        this.FOperacion = Convert.ToDateTime(drConsulta["FOperacion"]);
                        this.Consecutivo = Convert.ToInt16(drConsulta["Consecutivo"]);
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

        /*public override bool AplicarCobros()
        {
            bool resultado = false;
            List<Cobro> Cobros = this.ListaCobros;

            foreach (Cobro Cobro in Cobros)
            {
                resultado = Cobro.ChequeTarjetaAltaModifica();
                Cobro.MovimientoCajaCobroAlta(this.Caja, this.FOperacion, this.Consecutivo, this.Folio);
                Cobro.MovimientoCajaEntradaAlta(this.Caja, this.FOperacion, this.Consecutivo, this.Folio);
                Cobro.ActualizaPagoReferenciado(this.Caja, this.FOperacion, this.Consecutivo, this.Folio);

                //List<ReferenciaConciliadaPedido> Pedidos = Cobro.ListaPedidos;
                //if ((Cobro.ListaPedidos.GroupBy(s => s.Pedido).Select(s => s.First()).ToList().Count) == 1)
                //{

                //}
                List<ReferenciaConciliadaPedido> Pedidos = Cobro.ListaPedidos.GroupBy(s => s.Pedido).Select(s => s.First()).ToList();

                foreach (ReferenciaConciliadaPedido Pedido in Pedidos)
                {
                    Pedido.MontoConciliado = Cobro.ListaPedidos.Where(y => y.Pedido == Pedido.Pedido).Sum(x => x.MontoConciliado);
                    Pedido.CobroPedidoAlta(Cobro.AñoCobro, Cobro.NumCobro);
                    Pedido.PedidoActualizaSaldo();
                    Pedido.ActualizaPagosPorAplicar();
                    
                }

            }
            if (resultado)
            {
                this.ImplementadorMensajes.MostrarMensaje("El Registro se guardo con éxito.");
            }
            return resultado;
        }*/
        
        public override bool AplicarCobros(Conexion _conexion)
        {
            bool resultado = false;
            
            try
            {
                List<Cobro> Cobros = this.ListaCobros;

                foreach (Cobro Cobro in Cobros)
                {
                    resultado = Cobro.ChequeTarjetaAltaModifica(_conexion);
                    Cobro.MovimientoCajaCobroAlta(this.Caja, this.FOperacion, this.Consecutivo, this.Folio, _conexion);
                    Cobro.MovimientoCajaEntradaAlta(this.Caja, this.FOperacion, this.Consecutivo, this.Folio, _conexion);
                    Cobro.ActualizaPagoReferenciado(this.Caja, this.FOperacion, this.Consecutivo, this.Folio, _conexion);

                    List<ReferenciaConciliadaPedido> Pedidos =
                        Cobro.ListaPedidos.GroupBy(s => s.Pedido).Select(s => s.First()).ToList();

                    foreach (ReferenciaConciliadaPedido Pedido in Pedidos)
                    {
                        Pedido.MontoConciliado =
                            Cobro.ListaPedidos.Where(y => y.Pedido == Pedido.Pedido).Sum(x => x.MontoConciliado);
                        Pedido.CobroPedidoAlta(Cobro.AñoCobro, Cobro.NumCobro, _conexion);
                        Pedido.PedidoActualizaSaldo(_conexion);
                        Pedido.ActualizaPagosPorAplicar(_conexion);
                    }
                }
                //if (resultado)
                //{
                //    this.ImplementadorMensajes.MostrarMensaje("El Registro se guardo con éxito.");
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }

        /// <summary>
        /// Realiza el mismo proceso de AplicarCobros y adicionalmente ejecuta el método
        /// PedidoActualizaSaldoCRM() por cada uno de los pedidos
        /// </summary>
        /// <param name="_conexion"></param>
        /// <param name="URLGateway"></param>
        /// <returns></returns>
        public override bool AplicarCobrosCRM(Conexion _conexion, string URLGateway)
        {
            bool resultado = false;

            try
            {
                List<Cobro> Cobros = this.ListaCobros;

                foreach (Cobro Cobro in Cobros)
                {
                    resultado = Cobro.ChequeTarjetaAltaModifica(_conexion);
                    Cobro.MovimientoCajaCobroAlta(this.Caja, this.FOperacion, this.Consecutivo, this.Folio, _conexion);
                    Cobro.MovimientoCajaEntradaAlta(this.Caja, this.FOperacion, this.Consecutivo, this.Folio, _conexion);
                    Cobro.ActualizaPagoReferenciado(this.Caja, this.FOperacion, this.Consecutivo, this.Folio, _conexion);

                    List<ReferenciaConciliadaPedido> Pedidos =
                        Cobro.ListaPedidos.GroupBy(s => s.Pedido).Select(s => s.First()).ToList();

                    foreach (ReferenciaConciliadaPedido Pedido in Pedidos)
                    {
                        Pedido.MontoConciliado =
                            Cobro.ListaPedidos.Where(y => y.Pedido == Pedido.Pedido).Sum(x => x.MontoConciliado);
                        Pedido.CobroPedidoAlta(Cobro.AñoCobro, Cobro.NumCobro, _conexion);
                        Pedido.PedidoActualizaSaldo(_conexion);
                        Pedido.ActualizaPagosPorAplicar(_conexion);
                        Pedido.PedidoActualizaSaldoCRM(URLGateway);
                    }
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }

        /*public override bool ValidaMovimientoCaja()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBValidaMovimientoCaja", cnn);
                    comando.Parameters.Add("@Caja", System.Data.SqlDbType.SmallInt).Value = this.Caja;
                    comando.Parameters.Add("@FOperacion", System.Data.SqlDbType.DateTime).Value = this.FOperacion;
                    comando.Parameters.Add("@Consecutivo", System.Data.SqlDbType.SmallInt).Value = this.Consecutivo;
                    comando.Parameters.Add("@Folio", System.Data.SqlDbType.Int).Value = this.Folio;

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
        
        public override bool ValidaMovimientoCaja(Conexion _conexion)
        {
            bool resultado = false;
            try
            {

                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBValidaMovimientoCaja";
                _conexion.Comando.Parameters.Clear();

                _conexion.Comando.Parameters.Add(new SqlParameter("@Caja", System.Data.SqlDbType.SmallInt)).Value = this.Caja;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FOperacion", System.Data.SqlDbType.DateTime)).Value = this.FOperacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Consecutivo", System.Data.SqlDbType.SmallInt)).Value = this.Consecutivo;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Folio", System.Data.SqlDbType.Int)).Value = this.Folio;

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
        
        /*public override bool Guardar()
        {
            bool resultado = false;
            try
            {
                MovimientoCajaAlta();
                AplicarCobros();
                ValidaMovimientoCaja();
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

        public override bool Guardar(Conexion conexion)
        {
            bool resultado = false;
            try
            {
                conexion.AbrirConexion(true);
                MovimientoCajaAlta(conexion);
                AplicarCobros(conexion);
                ValidaMovimientoCaja(conexion);
               // conexion.Comando.Transaction.Commit();
                resultado = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //conexion.CerrarConexion();
            }
            return resultado;
        }

        /// <summary>
        /// Adicionalmente actualiza el saldo de los pedidos en CRM
        /// </summary>
        /// <param name="conexion"></param>
        /// <param name="URLGateway"></param>
        /// <returns></returns>
        public override bool Guardar(Conexion conexion, string URLGateway)
        {
            bool resultado = false;
            try
            {
                conexion.AbrirConexion(true);
                MovimientoCajaAlta(conexion);
                AplicarCobrosCRM(conexion, URLGateway);
                ValidaMovimientoCaja(conexion);
                // conexion.Comando.Transaction.Commit();
                resultado = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //conexion.CerrarConexion();
            }
            return resultado;
        }

    }
}