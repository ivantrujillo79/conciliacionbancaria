using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.ReglasDeNegocio;
using System.Data.SqlClient;
using System.Diagnostics;


namespace Conciliacion.RunTime.DatosSQL
{
    class ReferenciaNoConciliadaPedidoDatos :ReferenciaNoConciliadaPedido 
    {
        public ReferenciaNoConciliadaPedidoDatos
            (int corporativo, int sucursal, string sucursaldes, int año, int folio, short mes, int celula, int añoped, int pedido, string pedidoreferencia, int cliente, string nombre, int remisionpedido, string seriepedido,
            int foliosat, string seriesat, string concepto, decimal total, short formaconciliacion, short statusconcepto, string statusconciliacion,DateTime foperacion, DateTime fmovimiento,
            decimal diferencia,  IMensajesImplementacion implementadorMensajes)

            : base(corporativo,sucursal, sucursaldes,año,folio,mes, celula, añoped, pedido, pedidoreferencia, cliente,nombre, remisionpedido,seriepedido, foliosat, seriesat,concepto,total,formaconciliacion,
                    statusconcepto,statusconciliacion, foperacion,fmovimiento, diferencia, implementadorMensajes)
        {

        }


        public ReferenciaNoConciliadaPedidoDatos(DateTime ffactura, int cliente, string nombrecliente, string foliofactura, string serie, string folio, IMensajesImplementacion implementadorMensajes)
            : base(ffactura, cliente, nombrecliente, foliofactura, serie, foliofactura, implementadorMensajes)
        {

        }


        //public ReferenciaNoConciliadaPedidoDatos
        //    (int corporativo, int sucursal, string sucursaldes, int año, int folio, int secuencia, string concepto, decimal deposito, decimal retiro, 
        //    short formaconciliacion, short statusconcepto, string statusconciliacion, DateTime fmovimiento, DateTime foperacion, int folioconciliacion, short mes, 
        //    decimal diferencia, IMensajesImplementacion implementadorMensajes)

        //    : base(corporativo, sucursal, sucursaldes, año, folio,  secuencia,  concepto,  deposito,  retiro, 
        //     formaconciliacion,  statusconcepto,  statusconciliacion,  fmovimiento, foperacion,  folioconciliacion,  mes, 
        //     diferencia, implementadorMensajes)
        //{

        //}


        public ReferenciaNoConciliadaPedidoDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
            
        }

        public ReferenciaNoConciliadaPedidoDatos
            (int corporativo, int sucursal, string sucursaldes, int año, int folio, short mes, int celula, int añoped, int pedido, string pedidoreferencia, int cliente, string nombre, int remisionpedido, string seriepedido,
            int foliosat, string seriesat, string concepto, decimal total, short formaconciliacion, short statusconcepto, string statusconciliacion, DateTime foperacion, DateTime fmovimiento,
            decimal diferencia, DetalleSaldoAFavor DetalleSaldo, IMensajesImplementacion implementadorMensajes)
            : base(corporativo, sucursal, sucursaldes, año, folio, mes, celula, añoped, pedido, pedidoreferencia, cliente, nombre, remisionpedido, seriepedido, foliosat, seriesat, concepto, total, formaconciliacion,
                    statusconcepto, statusconciliacion, foperacion, fmovimiento, diferencia, DetalleSaldo, implementadorMensajes)
        {

        }

        public override ReferenciaNoConciliadaPedido CrearObjeto()
        {
            return new ReferenciaNoConciliadaPedidoDatos(App.ImplementadorMensajes);
        }

  

        public override bool Guardar()
        {
            throw new NotImplementedException();
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
