using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.ReglasDeNegocio;
using System.Data.SqlClient;
using System.Diagnostics;
using RTGMGateway;
using System.Configuration;
using System.Web;

namespace Conciliacion.RunTime.DatosSQL
{    
    public class MovimientoCajaDatos : MovimientoCaja
    {
        private string status;

        public string Status
        {
            get {return status;}
            set{status = value;}
        }

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
                _conexion.Comando.Parameters.Add(new SqlParameter("@TipoMovimientoCaja", System.Data.SqlDbType.SmallInt)).Value = this.TipoMovimientoCaja;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Status", System.Data.SqlDbType.Char,10)).Value = this.Status;

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
                        if (StatusAltaMC == StatusMovimientoCaja.Validado)
                        {
                            Pedido.PedidoActualizaSaldo(_conexion);
                        }
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

        private bool ExitoAlAplicarPedidos(List<RTGMCore.Pedido> rspPedidoActualiza)
        {
            bool resultado = false;
            foreach (RTGMCore.Pedido rsp in rspPedidoActualiza)
            {
                resultado = rsp.Message.Contains("EXITO");
                if (!resultado)
                    break;
            }
            return resultado;
        }

        //private bool ExitoAlAplicarPedido(List<RTGMCore.Pedido> listaRsp, string IDPedido)
        //{
        //    bool resultado = false;
        //    foreach (RTGMCore.Pedido rsp in rspPedidoActualiza)
        //    {
        //        if (rsp.IDPedido.ToString() == IDPedido)
        //        {
        //            resultado = rsp.Message.Contains("NO HAY ERROR");
        //            break;
        //        }
        //    }
        //    return resultado;
        //}

        private void SiHayErroresMostrar(List<RTGMCore.Pedido> rspPedidoActualiza)
        {
            StringBuilder mensajeErrores = new StringBuilder();
            foreach (RTGMCore.Pedido rsp in rspPedidoActualiza)
            {
                if (! rsp.Success )
                    mensajeErrores.Append(rsp.Message);
            }
            if (mensajeErrores.Length > 0)
                this.ImplementadorMensajes.MostrarMensaje(mensajeErrores.ToString());
        }

        private bool Exitoso(List<RTGMCore.Pedido> listaRsp)
        {
            bool resultado = false;
            foreach (RTGMCore.Pedido rsp in listaRsp)
            {
                resultado = rsp.Success;
                if (!resultado)
                    break;
            }
            return resultado;
        }

        private DataTable DatosDePedido(Conexion _conexion, int AñoPed, int Celula, int Pedido)
        {
            DataTable dtRetorno = new DataTable();
            try
            {
                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBDatosDePedido"; 

                _conexion.Comando.Parameters.Clear();
                _conexion.Comando.Parameters.Add(new SqlParameter("@AñoPed", System.Data.SqlDbType.Int)).Value = AñoPed;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Celula", System.Data.SqlDbType.Int)).Value = Celula;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Pedido", System.Data.SqlDbType.Int)).Value = Pedido;

                SqlDataAdapter Dap = new SqlDataAdapter(_conexion.Comando);
                Dap.Fill(dtRetorno);
                
            }
            catch (Exception ex)
            {
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("Error al consultar la informacion.\n\rClase :" +
                                                          this.GetType().Name + "\n\r" + "Metodo :" +
                                                          stackTrace.GetFrame(0).GetMethod().Name + "\n\r" +
                                                          "Error :" + ex.Message);
                stackTrace = null;
            }
            return dtRetorno;
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
            DataTable dtPedido;
            int ruta;
            int producto;
            decimal importe;
            decimal descuento;
            decimal impuesto;
            decimal precio;
            decimal litros;
            decimal total;
            int añoatt;
            DateTime fsuministro;
            int remision;
            int folio;
            int autotanque;
            int tipocobro;
            int tipocargo;
            int tipopedido;
            string serieremision;

            AppSettingsReader settings = new AppSettingsReader();
            SeguridadCB.Public.Usuario usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
            byte modulo = byte.Parse(settings.GetValue("Modulo", typeof(string)).ToString());
            byte corporativo = ((SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"]).Corporativo;
            
            try
            {
                List<Cobro> Cobros = this.ListaCobros;
                foreach (Cobro Cobro in Cobros)
                {
                    List<ReferenciaConciliadaPedido> Pedidos = Cobro.ListaPedidos.GroupBy(s => s.Pedido).Select(s => s.First()).ToList();
                    if (Pedidos.Count > 0)
                    {
                        List<RTGMCore.DetallePedido> listaDetallePedidos = new List<RTGMCore.DetallePedido>();

                        RTGMActualizarPedido objGateway = new RTGMActualizarPedido(modulo, App.CadenaConexion);
                        objGateway.URLServicio = URLGateway;
                        List<RTGMCore.Pedido> lstPedido = new List<RTGMCore.Pedido>();
                        foreach (ReferenciaConciliadaPedido Pedido in Pedidos)
                        {
                            dtPedido = DatosDePedido(_conexion, Pedido.AñoPedido, Pedido.CelulaPedido, Pedido.Pedido);
                            if (dtPedido.Rows.Count > 0)
                            {
                                producto = int.Parse(dtPedido.Rows[0]["producto"].ToString());
                                ruta = int.Parse(dtPedido.Rows[0]["ruta"].ToString());
                                descuento = decimal.Parse(dtPedido.Rows[0]["descuento"].ToString());
                                importe = decimal.Parse(dtPedido.Rows[0]["importe"].ToString());
                                impuesto = decimal.Parse(dtPedido.Rows[0]["impuesto"].ToString());
                                precio = decimal.Parse(dtPedido.Rows[0]["precio"].ToString());
                                litros = decimal.Parse(dtPedido.Rows[0]["litros"].ToString());
                                total = decimal.Parse(dtPedido.Rows[0]["total"].ToString());

                                RTGMCore.Producto obProducto = new RTGMCore.Producto { IDProducto = producto }; //TipoCobro de pedido
                                RTGMCore.RutaCRMDatos obRuta = new RTGMCore.RutaCRMDatos { IDRuta = ruta }; //campo ruta de pedido
                                listaDetallePedidos.Add(new RTGMCore.DetallePedido
                                {
                                    Producto = obProducto,
                                    DescuentoAplicado = descuento, //campo descuento tabla pedido
                                    Importe = importe, //tabla pedido
                                    Impuesto = impuesto,//tabla pedido
                                    Precio = precio,//tabla pedido
                                    CantidadSurtida = litros,//litros tabla pedido
                                    Total = total,//tabla pedido
                                    CantidadLectura = 0,
                                    CantidadLecturaAnterior = 0,
                                    CantidadSolicitada = 0,
                                    DescuentoAplicable = 0,
                                    DiferenciaDeLecturas = 0,
                                    IDDetallePedido = 0,
                                    IDPedido = 0,
                                    ImpuestoAplicable = 0,
                                    PorcentajeTanque = 0,
                                    PrecioAplicable = 0,
                                    RedondeoAnterior = 0,
                                    TotalAplicable = 0
                                });
                                añoatt = int.Parse(dtPedido.Rows[0]["añoatt"].ToString());
                                fsuministro = DateTime.Parse(dtPedido.Rows[0]["fsuministro"].ToString());
                                remision = int.Parse(dtPedido.Rows[0]["remision"].ToString());
                                folio = int.Parse(dtPedido.Rows[0]["folio"].ToString());
                                autotanque = int.Parse(dtPedido.Rows[0]["autotanque"].ToString());
                                tipocobro = int.Parse(dtPedido.Rows[0]["tipocobro"].ToString());
                                tipocargo = int.Parse(dtPedido.Rows[0]["tipocargo"].ToString());
                                tipopedido = int.Parse(dtPedido.Rows[0]["tipopedido"].ToString());
                                serieremision = dtPedido.Rows[0]["serieremision"].ToString();
                                lstPedido.Add(new RTGMCore.PedidoCRMDatos
                                {
                                    IDPedido = Pedido.Pedido,
                                    IDZona = Pedido.CelulaPedido, //checar si corresponde con campo Celula
                                    RutaSuministro = obRuta,
                                    DetallePedido = listaDetallePedidos,
                                    IDDireccionEntrega = Pedido.Cliente,
                                    AnioAtt = añoatt, //tabla pedido campo añoAtt
                                    FSuministro = fsuministro, //tabla pedido campo FSuministro
                                    FolioRemision = remision, //Pedido.RemisionPedido, //checar si corresponde con campo Remision //17327695,
                                    IDAutotanque = autotanque, //campo Autotanque
                                    IDEmpresa = corporativo,
                                    IDFolioAtt = folio, //47697, //tabla pedido campo Folio
                                    IDFormaPago = tipocobro, //campo TipoCobro
                                    IDTipoCargo = tipocargo, //campo tipocargo
                                    IDTipoPedido = tipopedido, //campo TIpopedido
                                    IDTipoServicio = 1, //queda hardcodeado
                                    Importe = importe, //campo importe
                                    Impuesto = impuesto,//campo impuesto
                                    SerieRemision = serieremision,//campo SerieRemision
                                    Total = total//campo total    
                                });
                            }
                        }
                        //Aplica Liquidacion
                        SolicitudActualizarPedido Solicitud = new SolicitudActualizarPedido
                        {
                            Pedidos = lstPedido,
                            Portatil = false,
                            TipoActualizacion = RTGMCore.TipoActualizacion.Liquidacion,
                            Usuario = "ROPIMA"
                        };
                        List<RTGMCore.Pedido> ListaRespuesta = objGateway.ActualizarPedido(Solicitud);
                        resultado = Exitoso(ListaRespuesta);

                        SiHayErroresMostrar(ListaRespuesta);
                        if (!resultado)
                            break;
                    }
                    if (!resultado)
                        break;
                }

                //Actualiza Saldo 
                if (resultado)
                {
                    foreach (Cobro Cobro in Cobros)
                    {
                        List<ReferenciaConciliadaPedido> Pedidos = Cobro.ListaPedidos.GroupBy(s => s.Pedido).Select(s => s.First()).ToList();
                        if (Pedidos.Count > 0)
                        {
                            RTGMActualizarPedido objGateway = new RTGMActualizarPedido(modulo, App.CadenaConexion);
                            objGateway.URLServicio = URLGateway;
                            List<RTGMCore.Pedido> lstPedido = new List<RTGMCore.Pedido>();
                            foreach (ReferenciaConciliadaPedido Pedido in Pedidos)
                            {
                                dtPedido = DatosDePedido(_conexion, Pedido.AñoPedido, Pedido.CelulaPedido, Pedido.Pedido);
                                if (dtPedido.Rows.Count > 0)
                                {
                                    lstPedido.Add(new RTGMCore.PedidoCRMSaldo
                                    {
                                        IDEmpresa = corporativo,
                                        IDPedido = Pedido.Pedido,
                                        PedidoReferencia = Pedido.Pedido.ToString(),
                                        IDZona = Pedido.CelulaPedido, 
                                        Abono = Pedido.MontoConciliado //checar si el importe del abono
                                    });
                                }
                            } //construye lista de pedidos

                            SolicitudActualizarPedido Solicitud = new SolicitudActualizarPedido
                            {
                                Pedidos = lstPedido,
                                Portatil = false,
                                TipoActualizacion = RTGMCore.TipoActualizacion.Saldo,
                            };
                            List<RTGMCore.Pedido> ListaRespuesta = objGateway.ActualizarPedido(Solicitud);
                            resultado = Exitoso(ListaRespuesta);
                            SiHayErroresMostrar(ListaRespuesta);

                            if (!resultado)
                                break;

                        }
                    }
                    
                }

            }
            catch (Exception ex)
            {
                resultado = false;
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
                if (AplicarCobrosCRM(conexion, URLGateway)) 
                { 
                    MovimientoCajaAlta(conexion);
                    AplicarCobros(conexion);
                    ValidaMovimientoCaja(conexion);
                    // conexion.Comando.Transaction.Commit();
                    resultado = true;
                }
                else
                    resultado = false;
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