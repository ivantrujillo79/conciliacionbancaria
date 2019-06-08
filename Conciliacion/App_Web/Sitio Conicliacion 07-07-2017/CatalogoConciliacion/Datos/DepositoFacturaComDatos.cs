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
        public DepositoFacturaComDatos(MensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public DepositoFacturaComDatos(string cuentabancofinanciero, string cuentabanco, string fdeposito, string deposito,
            string foliocomple, string seriecomple, string ftimbradocomple,
            string totalcomple, string uuidcomple, string folio, string serie, string ftimbrado,
            string total, string uuid, string rfcliente, MensajesImplementacion implemntadorMensajes)
            : base(cuentabancofinanciero, cuentabanco, fdeposito, deposito,
            foliocomple, seriecomple, ftimbradocomple,
            totalcomple, uuidcomple, folio, serie, ftimbrado,
            total, uuid, rfcliente, implemntadorMensajes)
        {
        }

    }
}
