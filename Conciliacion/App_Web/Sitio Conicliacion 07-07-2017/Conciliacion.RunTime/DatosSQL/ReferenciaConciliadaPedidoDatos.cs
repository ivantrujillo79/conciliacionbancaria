using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using Conciliacion.RunTime.ReglasDeNegocio;
using RTGMGateway;
using System.Configuration;
using System.Web;

namespace Conciliacion.RunTime.DatosSQL
{
    public class ReferenciaConciliadaPedidoDatos : ReferenciaConciliadaPedido
    {
          public ReferenciaConciliadaPedidoDatos(int corporativo, int añoconciliacion, short mesconciliacion, int folioconciliacion,
                                    int sucursalext, string sucursalextdes, int folioext, int secuenciaext, string conceptoext, decimal montoconciliado, decimal diferencia, short formaconciliacion, short statusconcepto, string statusconciliacion, DateTime foperacionext, DateTime fmovimientoext,
                                    string chequeexterno, string referenciaexterno, string descripcionexterno, string nombreterceroexterno, string rfcterceroexterno, decimal depositoexterno, decimal retiroexterno,
                                    int sucursalpedido, string sucursalpedidodes, int celulapedido, int añopedido, int pedido, int remisionpedido, string seriepedido, int foliosat, string seriesat, string conceptopedido, decimal total, string statusmovimiento,
                                    int cliente, string nombre,int añoexterno, IMensajesImplementacion implementadorMensajes)
            : base(corporativo, añoconciliacion, mesconciliacion, folioconciliacion, sucursalext, sucursalextdes, folioext, secuenciaext, conceptoext, montoconciliado, diferencia, formaconciliacion, statusconcepto, statusconciliacion, foperacionext, fmovimientoext,
             chequeexterno, referenciaexterno, descripcionexterno, nombreterceroexterno, rfcterceroexterno, depositoexterno, retiroexterno,
             sucursalpedido,  sucursalpedidodes,  celulapedido,  añopedido,  pedido,  remisionpedido,  seriepedido,  foliosat,  seriesat, conceptopedido, total, statusmovimiento, cliente,nombre,añoexterno, implementadorMensajes)
        {

        }

          public ReferenciaConciliadaPedidoDatos(int corporativo, int añoconciliacion, short mesconciliacion, int folioconciliacion,
                                     int sucursalext, string sucursalextdes, int folioext, int secuenciaext, string conceptoext, decimal montoconciliado, decimal diferencia, short formaconciliacion, short statusconcepto, string statusconciliacion, DateTime foperacionext, DateTime fmovimientoext,
                                     string chequeexterno, string referenciaexterno, string descripcionexterno, string nombreterceroexterno, string rfcterceroexterno, decimal depositoexterno, decimal retiroexterno,
                                     int sucursalpedido, string sucursalpedidodes, int celulapedido, int añopedido, int pedido, int remisionpedido, string seriepedido, int foliosat, string seriesat, string conceptopedido, decimal total, string statusmovimiento,
                                     int cliente, string nombre, string pedidoreferencia, int añoexterno, IMensajesImplementacion implementadorMensajes)
              : base(corporativo, añoconciliacion, mesconciliacion, folioconciliacion, sucursalext, sucursalextdes, folioext, secuenciaext, conceptoext, montoconciliado, diferencia, formaconciliacion, statusconcepto, statusconciliacion, foperacionext, fmovimientoext,
              chequeexterno, referenciaexterno, descripcionexterno, nombreterceroexterno, rfcterceroexterno, depositoexterno, retiroexterno,
               sucursalpedido, sucursalpedidodes, celulapedido, añopedido, pedido, remisionpedido, seriepedido, foliosat, seriesat, conceptopedido, total, statusmovimiento, cliente, nombre, pedidoreferencia,  añoexterno, implementadorMensajes)
          {

          }

          public ReferenciaConciliadaPedidoDatos(int corporativo, int añoconciliacion, short mesconciliacion, int folioconciliacion,
                                     int sucursalext, string sucursalextdes, int folioext, int secuenciaext, string conceptoext, decimal montoconciliado, decimal diferencia, short formaconciliacion, short statusconcepto, string statusconciliacion, DateTime foperacionext, DateTime fmovimientoext,
                                     string chequeexterno, string referenciaexterno, string descripcionexterno, string nombreterceroexterno, string rfcterceroexterno, decimal depositoexterno, decimal retiroexterno,
                                     int sucursalpedido, string sucursalpedidodes, int celulapedido, int añopedido, int pedido, int remisionpedido, string seriepedido, int foliosat, string seriesat, string conceptopedido, decimal total, string statusmovimiento,
                                     int cliente, string nombre, string pedidoreferencia, decimal saldo, int añoexterno, IMensajesImplementacion implementadorMensajes)
              : base(corporativo, añoconciliacion, mesconciliacion, folioconciliacion, sucursalext, sucursalextdes, folioext, secuenciaext, conceptoext, montoconciliado, diferencia, formaconciliacion, statusconcepto, statusconciliacion, foperacionext, fmovimientoext,
              chequeexterno, referenciaexterno, descripcionexterno, nombreterceroexterno, rfcterceroexterno, depositoexterno, retiroexterno,
               sucursalpedido, sucursalpedidodes, celulapedido, añopedido, pedido, remisionpedido, seriepedido, foliosat, seriesat, conceptopedido, total, statusmovimiento, cliente, nombre, pedidoreferencia, saldo, añoexterno, implementadorMensajes)
          {

          }

        public ReferenciaConciliadaPedidoDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
            
        }

        public override ReferenciaConciliadaPedido CrearObjeto()
        {
            return new ReferenciaConciliadaPedidoDatos(App.ImplementadorMensajes);
        }

        public override bool Guardar()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaConciliacionPedido", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.SmallInt).Value = 0;
                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = this.Corporativo;
                    comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = this.Sucursal;
                    comando.Parameters.Add("@AñoConciliacion", System.Data.SqlDbType.Int).Value = this.AñoConciliacion;
                    comando.Parameters.Add("@FolioConciliacion ", System.Data.SqlDbType.Int).Value = this.FolioConciliacion;
                    comando.Parameters.Add("@MesConciliacion", System.Data.SqlDbType.SmallInt).Value = this.MesConciliacion;
                    
                    comando.Parameters.Add("@Celula", System.Data.SqlDbType.Int).Value = this.CelulaPedido;
                    comando.Parameters.Add("@AñoPed", System.Data.SqlDbType.Int).Value = this.AñoPedido;
                    comando.Parameters.Add("@Pedido", System.Data.SqlDbType.Int).Value = this.Pedido;

                    comando.Parameters.Add("@AñoExterno", System.Data.SqlDbType.Int).Value = this.Año;
                    comando.Parameters.Add("@FolioExterno", System.Data.SqlDbType.Int).Value = this.Folio;
                    comando.Parameters.Add("@SecuenciaExterno", System.Data.SqlDbType.Int).Value = this.Secuencia;
                    comando.Parameters.Add("@Concepto", System.Data.SqlDbType.VarChar).Value = this.Concepto;
                    comando.Parameters.Add("@RemisionPedido", System.Data.SqlDbType.Int).Value = this.RemisionPedido;
                    comando.Parameters.Add("@SeriePedido ", System.Data.SqlDbType.VarChar).Value = this.SeriePedido;
                    comando.Parameters.Add("@FolioSat", System.Data.SqlDbType.Int).Value = this.FolioSat; 
                    comando.Parameters.Add("@SerieSat", System.Data.SqlDbType.VarChar).Value = this.SerieSat;
                    comando.Parameters.Add("@MontoConciliado", System.Data.SqlDbType.Money).Value = this.MontoConciliado;
                    comando.Parameters.Add("@MontoExterno", System.Data.SqlDbType.Money).Value = this.Deposito;//<----- MontoExterno
                    comando.Parameters.Add("@MontoInterno", System.Data.SqlDbType.Money).Value = this.Total;
                    comando.Parameters.Add("@FormaConciliacion", System.Data.SqlDbType.SmallInt).Value = this.FormaConciliacion;
                    comando.Parameters.Add("@StatusConcepto", System.Data.SqlDbType.SmallInt).Value = 0;//this.StatusConcepto; -->StatusConcepto: NINGUNO X DEFAULT
                    comando.Parameters.Add("@StatusConciliacion", System.Data.SqlDbType.VarChar).Value = "CONCILIADA";
                    comando.Parameters.Add("@StatusMovimiento", System.Data.SqlDbType.VarChar).Value = this.StatusMovimiento;
                    comando.Parameters.Add("@MotivoNoConciliado", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@ComentarioNoConciliado", System.Data.SqlDbType.VarChar).Value = "";
                    if (this.ClientePago == 0)
                        this.ClientePago = this.Cliente;
                    comando.Parameters.Add("@ClientePago", System.Data.SqlDbType.VarChar).Value = this.ClientePago;

                    if (this.ImporteComision > 0m)
                    {
                        comando.Parameters.Add("@ImporteComision", System.Data.SqlDbType.Money).Value = this.ImporteComision;
                    }
                    if (this.IVAComision > 0m)
                    {
                        comando.Parameters.Add("@IvaComision", System.Data.SqlDbType.Money).Value = this.IVAComision;
                    }

                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();                   
                }
                resultado = true;
            }
            catch (SqlException ex)
            {               
                stackTrace = new StackTrace();
                //this.ImplementadorMensajes.MostrarMensaje("No se pudo guardar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                throw new Exception("No se pudo guardar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            
            return resultado;            
        }
        
        /*public override bool CobroPedidoAlta(short añocobro, int cobro)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBCobroPedidoAlta", cnn);
                    comando.Parameters.Add("@Celula", System.Data.SqlDbType.SmallInt).Value = this.CelulaPedido;
                    comando.Parameters.Add("@AnoCobro", System.Data.SqlDbType.SmallInt).Value = añocobro;
                    comando.Parameters.Add("@Cobro", System.Data.SqlDbType.Int).Value = cobro;
                    comando.Parameters.Add("@AnoPed", System.Data.SqlDbType.SmallInt).Value = this.AñoPedido;
                    comando.Parameters.Add("@Pedido", System.Data.SqlDbType.Int).Value = this.Pedido;
                    comando.Parameters.Add("@Total", System.Data.SqlDbType.Decimal).Value = this.MontoConciliado;

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


        public override bool CobroPedidoAlta(short añocobro, int cobro, Conexion _conexion)
        {
            bool resultado = false;
            try
            {


                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBCobroPedidoAlta";
                _conexion.Comando.Parameters.Clear();
                _conexion.Comando.Parameters.Add(new SqlParameter("@Celula", System.Data.SqlDbType.SmallInt)).Value = this.CelulaPedido;
                _conexion.Comando.Parameters.Add(new SqlParameter("@AnoCobro", System.Data.SqlDbType.SmallInt)).Value = añocobro;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Cobro", System.Data.SqlDbType.Int)).Value = cobro;
                _conexion.Comando.Parameters.Add(new SqlParameter("@AnoPed", System.Data.SqlDbType.SmallInt)).Value = this.AñoPedido;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Pedido", System.Data.SqlDbType.Int)).Value = this.Pedido;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Total", System.Data.SqlDbType.Decimal)).Value = this.MontoConciliado;

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

        /*public override bool PedidoActualizaSaldo()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBPedidoActualizaSaldo", cnn);
                    comando.Parameters.Add("@Celula", System.Data.SqlDbType.SmallInt).Value = this.CelulaPedido;
                    comando.Parameters.Add("@AnoPed", System.Data.SqlDbType.SmallInt).Value = this.AñoPedido;
                    comando.Parameters.Add("@Pedido", System.Data.SqlDbType.Int).Value = this.Pedido;
                    comando.Parameters.Add("@Abono", System.Data.SqlDbType.Decimal).Value = this.MontoConciliado;
                    comando.CommandTimeout = 900;
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
        
        public override bool PedidoActualizaSaldo(Conexion _conexion)
        {
            bool resultado = false;
            try
            {

                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBPedidoActualizaSaldo";
                _conexion.Comando.Parameters.Clear();
                _conexion.Comando.Parameters.Add(new SqlParameter("@Celula", System.Data.SqlDbType.SmallInt)).Value = this.CelulaPedido;
                _conexion.Comando.Parameters.Add(new SqlParameter("@AnoPed", System.Data.SqlDbType.SmallInt)).Value = this.AñoPedido;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Pedido", System.Data.SqlDbType.Int)).Value = this.Pedido;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Abono", System.Data.SqlDbType.Decimal)).Value = this.MontoConciliado;
                _conexion.Comando.CommandTimeout = 900;

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

        public override bool PedidoActualizaSaldoCRM(string URLGateway)
        {
            List<RTGMCore.Pedido> Pedidos = new List<RTGMCore.Pedido>();

            AppSettingsReader settings = new AppSettingsReader();
            SeguridadCB.Public.Usuario usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
            byte modulo = byte.Parse(settings.GetValue("Modulo", typeof(string)).ToString());

            RTGMActualizarPedido obActualizar = new RTGMActualizarPedido(modulo, App.CadenaConexion);
            bool resultado = false;

            try
            {
                obActualizar.URLServicio = URLGateway;

                Pedidos.Add(new RTGMCore.PedidoCRMSaldo
                {
                    IDPedido            = Convert.ToInt32(this.PedidoReferencia),
                    IDZona              = this.CelulaPedido,
                    AnioPed             = this.AñoPedido,
                    Abono               = this.Total,
                    PedidoReferencia    = this.PedidoReferencia
                });

                SolicitudActualizarPedido obSolicitud = new SolicitudActualizarPedido
                {
                    //Fuente              = RTGMCore.Fuente.CRM, //RRV: revisar eso 
                    //IDEmpresa           = this.Corporativo,
                    Pedidos             = Pedidos,
                    Portatil            = false,
                    TipoActualizacion   = RTGMCore.TipoActualizacion.Saldo,
                    Usuario             = this.Usuario
                };

                List<RTGMCore.Pedido> PedidosRespuesta = obActualizar.ActualizarPedido(obSolicitud);
                resultado = PedidosRespuesta.Count > 0;
            }
            catch (SqlException ex)
            {
                stackTrace = new StackTrace();
                string strError = "Error al actualizar el pedido en CRM.\n\rClase: " + this.GetType().Name + "\n\r" 
                                    + "Metodo: " + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" 
                                    + "Error: " + ex.Message;
                stackTrace = null;
                throw new Exception(strError);
            }
            return resultado;
        }

        /*public override bool ActualizaPagosPorAplicar()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaPagosPorAplicar", cnn);
                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.Int).Value = this.Corporativo;
                    comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = this.Sucursal;
                    comando.Parameters.Add("@AñoConciliacion", System.Data.SqlDbType.Int).Value = this.AñoConciliacion;
                    comando.Parameters.Add("@MesConciliacion", System.Data.SqlDbType.SmallInt).Value = this.MesConciliacion;
                    comando.Parameters.Add("@FolioConciliacion ", System.Data.SqlDbType.Int).Value = this.FolioConciliacion;
                    comando.Parameters.Add("@Celula", System.Data.SqlDbType.Int).Value = this.CelulaPedido;
                    comando.Parameters.Add("@AñoPed", System.Data.SqlDbType.Int).Value = this.AñoPedido;
                    comando.Parameters.Add("@Pedido", System.Data.SqlDbType.Int).Value = this.Pedido;
                    
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
        
        public override bool ActualizaPagosPorAplicar(Conexion _conexion)
        {
            bool resultado = false;
            try
            {

                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBActualizaPagosPorAplicar";
                _conexion.Comando.Parameters.Clear();
                _conexion.Comando.Parameters.Add(new SqlParameter("@Corporativo", System.Data.SqlDbType.Int)).Value = this.Corporativo;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Sucursal", System.Data.SqlDbType.Int)).Value = this.Sucursal;
                _conexion.Comando.Parameters.Add(new SqlParameter("@AñoConciliacion", System.Data.SqlDbType.Int)).Value = this.AñoConciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@MesConciliacion", System.Data.SqlDbType.SmallInt)).Value = this.MesConciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FolioConciliacion ", System.Data.SqlDbType.Int)).Value = this.FolioConciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Celula", System.Data.SqlDbType.Int)).Value = this.CelulaPedido;
                _conexion.Comando.Parameters.Add(new SqlParameter("@AñoPed", System.Data.SqlDbType.Int)).Value = this.AñoPedido;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Pedido", System.Data.SqlDbType.Int)).Value = this.Pedido;

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
        
        public override bool Modificar()
        {
            throw new NotImplementedException();
        }

        public override bool Eliminar()
        {
            throw new NotImplementedException();
        }
    }
}
