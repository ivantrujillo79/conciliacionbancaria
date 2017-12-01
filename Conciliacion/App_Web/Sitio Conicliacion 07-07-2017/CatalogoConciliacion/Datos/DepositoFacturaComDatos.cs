using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CatalogoConciliacion.ReglasNegocio;
using Conciliacion.RunTime;

namespace CatalogoConciliacion.Datos
{
    public class DepositoFacturaComDatos:DepositoFacturaCom
    {
        public DepositoFacturaComDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public DepositoFacturaComDatos(string cuentabancariasaldofinal, string cuentabancofinanciero, string cuentabanco, string fdeposito, string deposito,
            string foliocumple, string seriecumplestring, string ftimbradocumple,
            string totalcumple, string uuidcomple, string folio, string serie, string ftimbrado,
            string total, string uuid, string rfcliente, IMensajesImplementacion implemntadorMensajes)
            : base(cuentabancariasaldofinal, cuentabancofinanciero, cuentabanco, fdeposito, deposito,
            foliocumple, seriecumplestring, ftimbradocumple,
            totalcumple, uuidcomple, folio, serie, ftimbrado,
            total, uuid, rfcliente, implemntadorMensajes)
        {
        }

    }
}
