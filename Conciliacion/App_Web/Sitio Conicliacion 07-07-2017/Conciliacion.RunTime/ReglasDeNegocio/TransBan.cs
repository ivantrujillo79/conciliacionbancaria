using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conciliacion.RunTime.DatosSQL;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public class TransBan : EmisorMensajes
    {
        public TransBan()
        {
        }

        public List<MovimientoCaja> ReorganizaTransban(MovimientoCajaDatos ObjMovimientoCajaDatos, int MaxDocumentos)
        {
            try
            {
                Boolean ultimaCedula = false;
                int cliente = 0;
                MovimientoCaja objmovimientocajadatos;
                List<MovimientoCaja> lstMovimientoCajaDatos = new List<MovimientoCaja>();
                List<ReferenciaConciliadaPedido> lstPedidos = new List<ReferenciaConciliadaPedido>();
                List<ReferenciaConciliadaPedido> lstPedidosOrdenada = new List<ReferenciaConciliadaPedido>();

                lstPedidosOrdenada = ObjMovimientoCajaDatos.ListaPedidos.OrderBy(s => s.Cliente).ToList();

                if (ObjMovimientoCajaDatos.ListaPedidos.Count() > MaxDocumentos)
                {
                    int i = 0;
                    foreach (ReferenciaConciliadaPedido Pedido in lstPedidosOrdenada)
                    {
                        i = i + 1;

                        if (Pedido.Cliente != cliente && ultimaCedula)
                        {
                            objmovimientocajadatos = new MovimientoCajaDatos(ObjMovimientoCajaDatos.Caja,
                                                                ObjMovimientoCajaDatos.FOperacion,
                                                                ObjMovimientoCajaDatos.Consecutivo,
                                                                ObjMovimientoCajaDatos.Folio,
                                                                ObjMovimientoCajaDatos.FMovimiento,
                                                                ObjMovimientoCajaDatos.Total,
                                                                ObjMovimientoCajaDatos.Usuario,
                                                                ObjMovimientoCajaDatos.Empleado,
                                                                ObjMovimientoCajaDatos.Observaciones,
                                                                ObjMovimientoCajaDatos.SaldoAFavor,
                                                                ObjMovimientoCajaDatos.ListaCobros,
                                                                implementadorMensajes);
                            lstPedidos.ForEach(c => objmovimientocajadatos.ListaPedidos.Add(c));

                            lstMovimientoCajaDatos.Add(objmovimientocajadatos);

                            lstPedidos.Clear();
                            lstPedidos.Add(Pedido);
                            ultimaCedula = false;
                            i = 1;

                        }
                        else
                        {
                            int NumClientes = ObjMovimientoCajaDatos.ListaPedidos.Where(y => y.Cliente == Pedido.Cliente).Count();

                            lstPedidos.Add(Pedido);
                            
                            if (NumClientes + i > MaxDocumentos && ultimaCedula == false)
                                ultimaCedula = true;

                            cliente = Pedido.Cliente;
                        }

                    }
                    objmovimientocajadatos =
                                    new MovimientoCajaDatos(ObjMovimientoCajaDatos.Caja,
                                    ObjMovimientoCajaDatos.FOperacion,
                                    ObjMovimientoCajaDatos.Consecutivo,
                                    ObjMovimientoCajaDatos.Folio,
                                    ObjMovimientoCajaDatos.FMovimiento,
                                    ObjMovimientoCajaDatos.Total,
                                    ObjMovimientoCajaDatos.Usuario,
                                    ObjMovimientoCajaDatos.Empleado,
                                    ObjMovimientoCajaDatos.Observaciones,
                                    ObjMovimientoCajaDatos.SaldoAFavor,
                                    ObjMovimientoCajaDatos.ListaCobros,
                                    implementadorMensajes);
                    lstPedidos.ForEach(c => objmovimientocajadatos.ListaPedidos.Add(c));
                    lstMovimientoCajaDatos.Add(objmovimientocajadatos);
                }
                else
                {
                    lstMovimientoCajaDatos.Add(ObjMovimientoCajaDatos);
                }

                TransBanException objTransBanException = new TransBanException();
                objTransBanException.DetalleExcepcion.CodigoError = 0;
                objTransBanException.DetalleExcepcion.Mensaje = "Proceso Exitoso";
                objTransBanException.DetalleExcepcion.VerificacionValida = true;

                return lstMovimientoCajaDatos;
            }
            catch (Exception ex)
            {
                TransBanException objTransBanException = new TransBanException();
                objTransBanException.DetalleExcepcion.CodigoError = 202;
                objTransBanException.DetalleExcepcion.Mensaje = ex.Message;
                objTransBanException.DetalleExcepcion.VerificacionValida = false;
                throw ex;
            }

        }
    }
}
