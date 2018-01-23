using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public class VerificadorRemanentes
    {

        public List<DetalleVerificadorRemanente> VerificarRemanentePedidos(DataTable dtPedidos, decimal MontoRemanente, byte Opcion)
        {
            List<DetalleVerificadorRemanente> ListaRetorno = new List<DetalleVerificadorRemanente>();
            if (dtPedidos != null)
            {
                foreach (DataRow drPedido in dtPedidos.Rows)
                {
                    string PedidoReferencia = "";
                    decimal TotalPedido = 0;
                    if (Opcion == 1)
                    {//Match
                        PedidoReferencia = drPedido[1].ToString();
                        TotalPedido = Convert.ToDecimal(drPedido[7]);
                    }
                    else
                    {//Busqueda
                        PedidoReferencia = drPedido[0].ToString();
                        TotalPedido = Convert.ToDecimal(drPedido[5]);
                    }
                    if (MontoRemanente >= TotalPedido)
                    {
                        DetalleVerificadorRemanente objValidacion = new DetalleVerificadorRemanente
                        {
                            PedidoReferencia = PedidoReferencia,
                            TotalPedido = TotalPedido
                        };
                        ListaRetorno.Add(objValidacion);
                    }
                }
            }
            return ListaRetorno;
        }



    }

    public class DetalleVerificadorRemanente
    {
        public string PedidoReferencia { get; set; }
        public decimal TotalPedido { get; set; }
    }
}
