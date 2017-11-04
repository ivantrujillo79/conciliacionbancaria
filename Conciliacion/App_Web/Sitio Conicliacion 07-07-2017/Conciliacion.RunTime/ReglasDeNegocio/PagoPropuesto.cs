using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conciliacion.RunTime.DatosSQL;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public class PagoPropuesto: EmisorMensajes
    {

        /// <summary>
        /// Bandera que indica si el sistema o el usuario propone la aplicación de un pago
        /// parcial, su uso tiene dos escenarios:
        /// 
        /// 1. Si se ejecuta el método DispersarPago ésta propiedad no tiene relevancia ya
        /// que será sobreescrita por el método mencionado.
        /// 2. Si se ejecuta el método ValidarDispersion el valor que tenga la propiedad
        /// deberá ser respetado ya que es resultado de un proceso Manual o de la clase
        /// misma.
        /// 
        /// En la interfaz gráfica se presentará en una columna de tipo checkbox indicando
        /// que el resultado de la dispersión ese DocumentoReferencia será pagado o no.
        /// </summary>
        private bool aplicarpago;
        /// <summary>
        /// Valor de Referencia del pedido o DocumentoRefeeencia.
        /// </summary>
        private string pedidoreferencia;
        /// <summary>
        /// Fecha (sin hora) en la que el pedido se suministró
        /// </summary>
        private DateTime fsuministro;
        /// <summary>
        /// Saldo pendiente de liquidación que tiene el pedido
        /// </summary>
        private decimal saldopedido;
        /// <summary>
        /// Monto del pago que el método DispersarPago o un usuario han determinado para el
        /// documento en particular.
        /// </summary>
        private decimal montopropuesto;
        /// <summary>
        /// Usuario que generó la propuesta (Sistema o Usuario firmado en la aplicación)
        /// </summary>
        private string usuario;
        /// <summary>
        /// Fecha en la que el usuario o el sistema propuso la aplicación del pago parcial
        /// </summary>
        private DateTime fechapropuesta;
        /// <summary>
        /// Número de referencia del cliente
        /// </summary>
        private string clientereferencia;

        public PagoPropuesto()
        {

        }
        public virtual void Dispose()
        {

        }

        /// <summary>
        /// Fecha (sin hora) en la que el pedido se suministró
        /// </summary>
        public DateTime FSuministro
        {
            get { return fsuministro; }
            set { fsuministro = value; }
        }

        /// <summary>
        /// Bandera que indica si el sistema o el usuario propone la aplicación de un pago
        /// parcial, su uso tiene dos escenarios:
        /// 
        /// 1. Si se ejecuta el método DispersarPago ésta propiedad no tiene relevancia ya
        /// que será sobreescrita por el método mencionado.
        /// 2. Si se ejecuta el método ValidarDispersion el valor que tenga la propiedad
        /// deberá ser respetado ya que es resultado de un proceso Manual o de la clase
        /// misma.
        /// 
        /// En la interfaz gráfica se presentará en una columna de tipo checkbox indicando
        /// que el resultado de la dispersión ese DocumentoReferencia será pagado o no.
        /// </summary>
        public bool AplicarPago
        {
            get { return aplicarpago; }
            set { aplicarpago = value; }
        }

        /// <summary>
        /// Monto del pago que el método DispersarPago o un usuario han determinado para el
        /// documento en particular.
        /// </summary>
        public decimal MontoPropuesto
        {
            get { return montopropuesto; }
            set { montopropuesto = value; }
        }

        /// <summary>
        /// Valor de Referencia del pedido o DocumentoRefeeencia.
        /// </summary>
        public string PedidoReferencia
        {
            get { return pedidoreferencia;}
            set { pedidoreferencia = value; }
        }

        /// <summary>
        /// Saldo pendiente de liquidación que tiene el pedido
        /// </summary>
        public decimal SaldoPedido
        {
            get { return saldopedido; }
            set { saldopedido = value; }
        }

        /// <summary>
        /// Usuario que generó la propuesta (Sistema o Usuario firmado en la aplicación)
        /// </summary>
        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        /// <summary>
        /// Fecha en la que el usuario o el sistema propuso la aplicación del pago parcial
        /// </summary>
        public DateTime FechaPropuesta
        {
            get { return fechapropuesta; }
            set { fechapropuesta = value; }
        }

        /// <summary>
        /// Número de referencia del cliente
        /// </summary>
        public string ClienteReferencia
        {
            get { return clientereferencia; }
            set { clientereferencia = value; }
            
        }

    }//end PagoPropuesto
}
