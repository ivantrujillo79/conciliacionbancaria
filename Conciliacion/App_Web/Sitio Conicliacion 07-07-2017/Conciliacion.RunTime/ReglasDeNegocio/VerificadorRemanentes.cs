using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public class VerificadorRemanentes
    {

        public List<DetalleVerificadorRemanente> VerificarRemanentePedidos(DataTable dtPedidos, decimal MontoRemanente)
        {
            List<DetalleVerificadorRemanente> ListaRetorno = new List<DetalleVerificadorRemanente>();

            foreach (DataRow drPedido in dtPedidos.Rows)
            {
                string PedidoReferencia = drPedido[1].ToString();
                decimal TotalPedido = Convert.ToDecimal(drPedido[7]);
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

            return ListaRetorno;
        }



    }

    public class DetalleVerificadorRemanente
    {
        public string PedidoReferencia { get; set; }
        public decimal TotalPedido { get; set; }
    }
}
