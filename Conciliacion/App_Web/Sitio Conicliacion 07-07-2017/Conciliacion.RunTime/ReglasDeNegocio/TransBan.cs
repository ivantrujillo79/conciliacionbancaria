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

        /*        public List<MovimientoCaja> ReorganizaTransban(MovimientoCajaDatos ObjMovimientoCajaDatos, int MaxDocumentos)
                {
                    try
                    {
                        Boolean ultimaCedula = false;
                        int cliente = 0;
                        MovimientoCaja objmovimientocajadatos;
                        List<MovimientoCaja> lstMovimientoCajaDatos = new List<MovimientoCaja>();
                        List<ReferenciaConciliadaPedido> lstPedidos = new List<ReferenciaConciliadaPedido>();
                        List<ReferenciaConciliadaPedido> lstPedidosOrdenada = new List<ReferenciaConciliadaPedido>();
                        List<Cobro> lstCobros = new List<Cobro>();

                        lstPedidosOrdenada = ObjMovimientoCajaDatos.ListaPedidos.OrderBy(s => s.Cliente).ToList();

                        lstCobros = ObjMovimientoCajaDatos.ListaCobros.OrderBy(s => s.Cliente).ToList();

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


                                    lstPedidos.Clear();
                                    lstPedidos.Add(Pedido);

                                    var ListaDistintosClientes = objmovimientocajadatos.ListaPedidos.Select(x => x.Cliente).Distinct().ToList();

                                    List<Conciliacion.RunTime.ReglasDeNegocio.Cobro> buffCobro = new List<Conciliacion.RunTime.ReglasDeNegocio.Cobro>();

                                    foreach (var clientedisti in ListaDistintosClientes)
                                    {
                                        buffCobro = lstCobros.Where(X => X.Cliente == clientedisti).ToList();    
                                    }

                                    objmovimientocajadatos.ListaCobros.Clear();
                                    buffCobro.ForEach(x=> objmovimientocajadatos.ListaCobros.Add(x));

                                    lstMovimientoCajaDatos.Add(objmovimientocajadatos);

                                    ultimaCedula = false;
                                    i = 1;
                                }
                                else
                                {
                                    int NumClientes = ObjMovimientoCajaDatos.ListaPedidos.Where(y => y.Cliente == Pedido.Cliente).Count();

                                    lstPedidos.Clear();
                                    lstPedidos.Add(Pedido);

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

                                    objmovimientocajadatos.ListaPedidos.Clear();
                                    objmovimientocajadatos.ListaCobros.Clear();

                                    objmovimientocajadatos.ListaPedidos.Add(Pedido);

                                    var ListaDistintosClientes = objmovimientocajadatos.ListaPedidos.Select(x => x.Cliente).Distinct().ToList();
                                    List<Conciliacion.RunTime.ReglasDeNegocio.Cobro> buffCobro = new List<Conciliacion.RunTime.ReglasDeNegocio.Cobro>();

                                    foreach (var clientedisti in ListaDistintosClientes)
                                    {
                                        buffCobro = lstCobros.Where(X => X.Cliente == clientedisti).ToList();
                                    }

                                    buffCobro.ForEach(x => objmovimientocajadatos.ListaCobros.Add(x));

                                    lstMovimientoCajaDatos.Add(objmovimientocajadatos);

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

                            var ListaDistintosClientes1 = objmovimientocajadatos.ListaPedidos.Select(x => x.Cliente).Distinct().ToList();

                            List<Conciliacion.RunTime.ReglasDeNegocio.Cobro> buffCobro1 = new List<Conciliacion.RunTime.ReglasDeNegocio.Cobro>();

                            foreach (var clientedisti in ListaDistintosClientes1)
                            {
                                buffCobro1 = lstCobros.Where(X => X.Cliente == clientedisti).ToList();
                            }

                            objmovimientocajadatos.ListaCobros.Clear();
                            //objmovimientocajadatos.ListaCobros = buffCobro1;
                            buffCobro1.ForEach(x => objmovimientocajadatos.ListaCobros.Add(x));


                            //lstMovimientoCajaDatos.Add(objmovimientocajadatos);
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
            }*/

        public List<MovimientoCaja> ReorganizaTransban(MovimientoCajaDatos ObjMovimientoCajaDatos, int MaxDocumentos)
        {
            List<MovimientoCaja> lstMovimientoCajaDatos = new List<MovimientoCaja>();
            List<Cobro> listaCobrosCliente;
            List<ReferenciaConciliadaPedido> listaPedidosCliente;
            MovimientoCajaDatos ObjMovimientoCajaActual;
            int restante;
            decimal _total = 0;
            decimal _saldoAFavor = 0;
          


            List<int> ListaDistintosClientes = new List<int>();

            if (ObjMovimientoCajaDatos == null)
            {
                throw new Exception("Error en método: TransBan.ReorganizaTransban, la instancia de MovimientoCajaDatos es nula, imposible continuar con el proceso de reorganización.");
            }
            ObjMovimientoCajaActual = creaMovimiento(ObjMovimientoCajaDatos);

            ListaDistintosClientes = ObjMovimientoCajaDatos.ListaPedidos.Select(x => x.Cliente).Distinct().ToList();
          
            if (ObjMovimientoCajaDatos.ListaPedidos.Count() > MaxDocumentos)
            {
                foreach (var clienteTemp in ListaDistintosClientes)
                {
                    listaPedidosCliente = ObjMovimientoCajaDatos.ListaPedidos.Where(x => x.Cliente == clienteTemp).ToList();
                    listaCobrosCliente = ObjMovimientoCajaDatos.ListaCobros.Where(x => x.Cliente == clienteTemp).ToList();
                    _total = listaPedidosCliente.Sum(x => x.Total);
                    _saldoAFavor = listaCobrosCliente.Sum(x => x.Saldo);



                    restante = MaxDocumentos - ObjMovimientoCajaActual.ListaPedidos.Count;

                    if(listaPedidosCliente.Count>restante)
                    {                     
                        if (ObjMovimientoCajaActual.ListaPedidos.Count!=0) 
                        {
                            lstMovimientoCajaDatos.Add(ObjMovimientoCajaActual);
                            ObjMovimientoCajaActual = creaMovimiento(ObjMovimientoCajaDatos);
                        }                       
                    }

                    ObjMovimientoCajaActual.ListaPedidos.AddRange(listaPedidosCliente);
                    ObjMovimientoCajaActual.ListaCobros.AddRange(listaCobrosCliente);
                    ObjMovimientoCajaActual.Total = ObjMovimientoCajaActual.Total + _total;
                    ObjMovimientoCajaActual.SaldoAFavor = ObjMovimientoCajaActual.SaldoAFavor + _saldoAFavor;
                }
                lstMovimientoCajaDatos.Add(ObjMovimientoCajaActual);
            }
            else
            {                //No se supera el máximo configurado
                lstMovimientoCajaDatos.Add(ObjMovimientoCajaDatos);
            }
            return lstMovimientoCajaDatos;
        }

        private MovimientoCajaDatos creaMovimiento(MovimientoCajaDatos ObjMovimientoCajaDatos)
        {
            MovimientoCajaDatos _objmovimientocajadatos = new MovimientoCajaDatos();
            _objmovimientocajadatos = new MovimientoCajaDatos();
            _objmovimientocajadatos.CadenaConexion = ObjMovimientoCajaDatos.CadenaConexion;
            _objmovimientocajadatos.Caja = ObjMovimientoCajaDatos.Caja;
            _objmovimientocajadatos.FMovimiento = ObjMovimientoCajaDatos.FMovimiento;
            _objmovimientocajadatos.FOperacion = ObjMovimientoCajaDatos.FOperacion;
            _objmovimientocajadatos.Empleado = ObjMovimientoCajaDatos.Empleado;
            _objmovimientocajadatos.Folio = ObjMovimientoCajaDatos.Folio;
            _objmovimientocajadatos.ImplementadorMensajes = ObjMovimientoCajaDatos.ImplementadorMensajes;
            _objmovimientocajadatos.Observaciones = ObjMovimientoCajaDatos.Observaciones;
            _objmovimientocajadatos.SaldoAFavor = 0;
            _objmovimientocajadatos.Total = 0;
            _objmovimientocajadatos.Usuario = ObjMovimientoCajaDatos.Usuario;
            _objmovimientocajadatos.ListaPedidos = new List<ReferenciaConciliadaPedido>();
            _objmovimientocajadatos.ListaCobros = new List<Cobro>();

            return _objmovimientocajadatos;
        }


        //public List<MovimientoCaja> ReorganizaTransban(MovimientoCajaDatos ObjMovimientoCajaDatos, int MaxDocumentos)
        //{
        //    List<MovimientoCaja> lstMovimientoCajaDatos = new List<MovimientoCaja>();
        //    List<Cobro> BufferCobro = new List<Cobro>();

        //    List<int> ListaDistintosClientes = new List<int>();

        //    if (ObjMovimientoCajaDatos == null)
        //    {
        //        throw new Exception("Error en método: TransBan.ReorganizaTransban, la instancia de MovimientoCajaDatos es nula, imposible continuar con el proceso de reorganización.");
        //    }

        //    ListaDistintosClientes = ObjMovimientoCajaDatos.ListaPedidos.Select(x => x.Cliente).Distinct().ToList();
        //    ObjMovimientoCajaDatos.ListaCobros.ForEach(X => BufferCobro.Add(X));

        //    //if (ObjMovimientoCajaDatos.ListaPedidos.Count() == ObjMovimientoCajaDatos.ListaCobros.Count())
        //    //{
        //        if (ObjMovimientoCajaDatos.ListaPedidos.Count() > MaxDocumentos)
        //        {
        //            MovimientoCajaDatos _objmovimientocajadatos = new MovimientoCajaDatos();
        //            //Se supera el máximo configurado
        //            foreach (var Cliente in ListaDistintosClientes)
        //            {
        //            //MovimientoCajaDatos _objmovimientocajadatos = new MovimientoCajaDatos();
        //                _objmovimientocajadatos = new MovimientoCajaDatos();
        //                _objmovimientocajadatos.CadenaConexion = ObjMovimientoCajaDatos.CadenaConexion;
        //                _objmovimientocajadatos.Caja = ObjMovimientoCajaDatos.Caja;
        //                _objmovimientocajadatos.FMovimiento = ObjMovimientoCajaDatos.FMovimiento;
        //                _objmovimientocajadatos.FOperacion = ObjMovimientoCajaDatos.FOperacion;
        //                _objmovimientocajadatos.Empleado = ObjMovimientoCajaDatos.Empleado;
        //                _objmovimientocajadatos.Folio = ObjMovimientoCajaDatos.Folio;
        //                _objmovimientocajadatos.ImplementadorMensajes = ObjMovimientoCajaDatos.ImplementadorMensajes;
        //                _objmovimientocajadatos.Observaciones = ObjMovimientoCajaDatos.Observaciones;
        //                _objmovimientocajadatos.SaldoAFavor = ObjMovimientoCajaDatos.SaldoAFavor;
        //                _objmovimientocajadatos.Total = ObjMovimientoCajaDatos.Total;
        //                _objmovimientocajadatos.Usuario = ObjMovimientoCajaDatos.Usuario;

        //                List<ReferenciaConciliadaPedido> ListaPedidosDelCliente = new List<ReferenciaConciliadaPedido>();
        //                ListaPedidosDelCliente = ObjMovimientoCajaDatos.ListaPedidos.Where(x => x.Cliente == Convert.ToInt32(Cliente)).ToList();

        //                List<Cobro> ListaCobrosDelCliente = new List<Cobro>();
        //                ListaCobrosDelCliente = BufferCobro.Where(x => x.Cliente == Convert.ToInt32(Cliente)).ToList();

        //                foreach (var Pedido in ListaPedidosDelCliente)
        //                {
        //                    _objmovimientocajadatos.ListaPedidos.Add(Pedido);
        //                }

        //                foreach (var Cobro in ListaCobrosDelCliente)
        //                {
        //                    _objmovimientocajadatos.ListaCobros.Add(Cobro);
        //                }

        //                /*lstMovimientoCajaDatos.Add(_objmovimientocajadatos);
        //                _objmovimientocajadatos = null;*/
        //            }
        //        _objmovimientocajadatos.ListaCobros.AddRange(BufferCobro);
        //        lstMovimientoCajaDatos.Add(_objmovimientocajadatos);
        //        _objmovimientocajadatos = null;
        //    }
        //    else
        //        {
        //            //No se supera el máximo configurado
        //            lstMovimientoCajaDatos.Add(ObjMovimientoCajaDatos);
        //        }
        //    //}
        //    return lstMovimientoCajaDatos;
        //}
    }

}
