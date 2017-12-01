using Conciliacion.Migracion.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime;

namespace CatalogoConciliacion.ReglasNegocio
{
    public abstract class DepositoFacturaCom : Conciliacion.RunTime.EmisorMensajes
    {        
        public string cuentabancofinanciero;
        public string cuentabanco;
        public string fdeposito;
        public string deposito;
        public string foliocomple;
        public string seriecomple;
        public string ftimbradocomple;
        public string totalcomple;
        public string uuidcomple;
        public string folio;
        public string serie;
        public string ftimbrado;
        public string total;
        public string uuid;
        public string rfcliente;
        public Conciliacion.RunTime.IMensajesImplementacion implementadorMensajes;

        #region constructores
        public DepositoFacturaCom(Conciliacion.RunTime.IMensajesImplementacion implemntadorMensajes)
        {
            this.cuentabancofinanciero = "";
            this.cuentabanco = "";
            this.fdeposito = "";
            this.deposito = "";
            this.foliocomple = "";
            this.seriecomple = "";
            this.ftimbradocomple = "";
            this.totalcomple = "";
            this.uuidcomple = "";
            this.folio = "";
            this.serie = "";
            this.ftimbrado = "";
            this.total = "";
            this.uuid = "";
            this.rfcliente = "";
            this.implementadorMensajes = implemntadorMensajes;
        }
        public DepositoFacturaCom(string cuentabancofinanciero, string cuentabanco, string fdeposito, string deposito, 
            string foliocomple, string seriecomple, string ftimbradocomple, 
            string totalcomple, string uuidcomple, string folio, string serie, string ftimbrado, 
            string total, string uuid, string rfcliente, Conciliacion.RunTime.IMensajesImplementacion implemntadorMensajes)
        {
            this.cuentabancofinanciero = cuentabancofinanciero;
            this.cuentabanco = cuentabanco;
            this.fdeposito = fdeposito;
            this.deposito = deposito;
            this.foliocomple = foliocomple;
            this.foliocomple = foliocomple;
            this.ftimbradocomple = ftimbradocomple;
            this.totalcomple = totalcomple;
            this.uuidcomple = uuidcomple;
            this.folio = folio;
            this.serie = serie;
            this.ftimbrado = ftimbrado;
            this.total = total;
            this.uuid = uuid;
            this.rfcliente = rfcliente;
            this.implementadorMensajes = implemntadorMensajes;
        }

        //public DepositoFacturaCom(Conciliacion.RunTime.IMensajesImplementacion implementadorMensajes)
        //{
        //    this.implementadorMensajes = implementadorMensajes;
        //}
        #endregion

        private string CuentaBancoFinanciero { get { return cuentabancofinanciero; } set { cuentabancofinanciero = value; } }
        private string CuentaBanco { get { return cuentabanco; } set { cuentabanco = value; } }
        private string FDeposito { get { return fdeposito; } set { fdeposito = value; } }
        private string Deposito { get { return deposito; } set { deposito = value; } }
        private string FolioComple { get { return foliocomple; } set { foliocomple = value; } }
        private string SerieComple { get { return seriecomple; } set { seriecomple = value; } }
        private string FTimbradoComple { get { return ftimbradocomple; } set { ftimbradocomple = value; } }
        private string TotalComple { get { return totalcomple; } set { totalcomple = value; } }
        private string UUIDComple { get { return uuidcomple; } set { uuidcomple = value; } }
        private string Folio { get { return folio; } set { folio = value; } }
        private string Serie { get { return serie; } set { serie = value; } }
        private string FTimbrado { get { return ftimbrado; } set { ftimbrado = value; } }
        private string Total { get { return total; } set { total = value; } }
        private string UUID { get { return uuid; } set { uuid = value; } }
        private string RFCCliente { get { return rfcliente; } set { rfcliente = value; } }

    }
}
