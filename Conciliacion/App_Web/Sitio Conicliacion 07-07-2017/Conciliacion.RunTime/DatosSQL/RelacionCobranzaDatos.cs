using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.ReglasDeNegocio;
using System.Data.SqlClient;
using System.Data;

namespace Conciliacion.RunTime.DatosSQL
{
    public class RelacionCobranzaDatos : RelacionCobranza
    {

        public override RelacionCobranza CrearObjeto(MovimientoCaja objMovientoCajaDatos, Boolean ConBoveda)
        {
            return new RelacionCobranzaDatos(objMovientoCajaDatos,ConBoveda);
        }

        public RelacionCobranzaDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public RelacionCobranzaDatos(MovimientoCaja objMovientoCajaDatos, Boolean ConBoveda)
            : base(objMovientoCajaDatos, ConBoveda)
        {
        }

        public override RelacionCobranzaException CreaRelacionCobranza(Conexion conexion)
        {
            try
            {
                ObjRelacionCobranzaExcepcion = new RelacionCobranzaException();

                if (ConBoveda)
                {
                    CreaEncabezadoRelacionCobranza(conexion);
                    AjustarPedidos(conexion);
                    CreaDetalleRelacionCobranza(conexion);
                    conexion.Comando.Transaction.Commit();
                }

                ObjRelacionCobranzaExcepcion.DetalleExcepcion.CodigoError = 0;
                ObjRelacionCobranzaExcepcion.DetalleExcepcion.Mensaje = "Proceso Realizado Con Exito";
                ObjRelacionCobranzaExcepcion.DetalleExcepcion.VerificacionValida = true;

                return ObjRelacionCobranzaExcepcion;
            }
            catch (Exception ex)
            {
                if (conexion.Comando.Transaction != null)
                {
                    conexion.Comando.Transaction.Rollback();
                }
                ObjRelacionCobranzaExcepcion.DetalleExcepcion.CodigoError = 201;
                ObjRelacionCobranzaExcepcion.DetalleExcepcion.Mensaje = ex.Message;
                ObjRelacionCobranzaExcepcion.DetalleExcepcion.VerificacionValida = false;
                return ObjRelacionCobranzaExcepcion;
            }
            finally
            {
                //conexion.CerrarConexion();
            }

        }

        public override void CreaEncabezadoRelacionCobranza(Conexion _conexion)
        {

            try
            {
                _conexion.Comando.CommandType = CommandType.StoredProcedure;
            _conexion.Comando.CommandText = "spCyCCobranzaAltaModifica";


            _conexion.Comando.Parameters.Clear();
            _conexion.Comando.Parameters.Add(new SqlParameter("@TipoCobranza", System.Data.SqlDbType.SmallInt)).Value = 10;
            _conexion.Comando.Parameters.Add(new SqlParameter("@FCobranza", System.Data.SqlDbType.DateTime)).Value = this.ObjMovimientoCajaDatos.FOperacion;
            _conexion.Comando.Parameters.Add(new SqlParameter("@UsuarioCaptura", System.Data.SqlDbType.Char)).Value = this.ObjMovimientoCajaDatos.Usuario;
            _conexion.Comando.Parameters.Add(new SqlParameter("@Empleado", System.Data.SqlDbType.Int)).Value = this.ObjMovimientoCajaDatos.Empleado;
            _conexion.Comando.Parameters.Add(new SqlParameter("@Total", System.Data.SqlDbType.Money)).Value = this.ObjMovimientoCajaDatos.Total;
            _conexion.Comando.Parameters.Add(new SqlParameter("@Observaciones", System.Data.SqlDbType.VarChar)).Value = this.ObjMovimientoCajaDatos.Observaciones;
            _conexion.Comando.Parameters.Add(new SqlParameter("@Status", System.Data.SqlDbType.Char)).Value = "ABIERTO";
            _conexion.Comando.Parameters.Add(new SqlParameter("@SigCobranza", System.Data.SqlDbType.Int));
            _conexion.Comando.Parameters["@SigCobranza"].Direction = ParameterDirection.Output;
            _conexion.Comando.Parameters["@SigCobranza"].Value = 0;
            _conexion.Comando.ExecuteNonQuery();
            this.Cobranza = int.Parse(_conexion.Comando.Parameters["@SigCobranza"].Value.ToString());
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public override void CreaDetalleRelacionCobranza(Conexion _conexion)
        {
            _conexion.Comando.CommandType = CommandType.StoredProcedure;
            _conexion.Comando.CommandText = "spCYCPedidoCobranzaAlta";

            foreach (ReferenciaConciliadaPedido referenciaPedidos in ListaPedidos)
            {
                _conexion.Comando.Parameters.Clear();
                _conexion.Comando.Parameters.Add(new SqlParameter("@AñoPed", System.Data.SqlDbType.SmallInt)).Value = referenciaPedidos.AñoPedido;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Celula", System.Data.SqlDbType.TinyInt)).Value = referenciaPedidos.CelulaPedido;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Pedido", System.Data.SqlDbType.Int)).Value = referenciaPedidos.Pedido;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Cobranza", System.Data.SqlDbType.Int)).Value = this.Cobranza;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Saldo", System.Data.SqlDbType.Money)).Value = referenciaPedidos.Saldo;
                _conexion.Comando.ExecuteNonQuery();
            }

        }

        public override List<ReferenciaConciliadaPedido> AjustarPedidos(Conexion _conexion)
        {
            try
            {

                {

                    List<ReferenciaConciliadaPedido> Pedidos =
                        ObjMovimientoCajaDatos.ListaPedidos.GroupBy(s => new { s.CelulaPedido, s.AñoPedido, s.Pedido })
                        .Select(s => s.First()).ToList();
                    foreach (ReferenciaConciliadaPedido Pedido in Pedidos)
                    {
                        Pedido.Total =
                            ObjMovimientoCajaDatos.ListaPedidos
                            .Where(y => y.CelulaPedido == Pedido.CelulaPedido)
                            .Where(y => y.AñoPedido == Pedido.AñoPedido)
                            .Where(y => y.Pedido == Pedido.Pedido)
                            .Sum(x => x.Saldo);
                        ListaPedidos.Add(Pedido);
                    }

                }
                return ListaPedidos;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }//end RelacionCobranzaDatos

}//end namespace SitioConciliacion