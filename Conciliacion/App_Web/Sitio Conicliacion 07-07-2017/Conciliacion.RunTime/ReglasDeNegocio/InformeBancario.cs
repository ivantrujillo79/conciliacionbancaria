using Conciliacion.RunTime.DatosSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class InformeBancario : EmisorMensajes
    {
        private string corporativo;
        private string sucursal;
        private int año;
        private int mes;
        private string cuentabancofinanciero;
        private int consecutivoflujo;
        private DateTime fecha;
        private string referencia;
        private string concepto;
        private decimal retiros;
        private decimal depositos;
        private decimal saldofinal;
        private string conceptoconciliado;
        private string documentoconciliado;

        public InformeBancario(IMensajesImplementacion implementadorMensajes)
        {
            this.implementadorMensajes = implementadorMensajes;
            this.corporativo = "";
            this.sucursal = "";
            this.año = 0;
            this.mes = 0;
            this.cuentabancofinanciero = "";
            this.consecutivoflujo = 0;
            this.fecha = DateTime.MinValue;
            this.referencia = "";
            this.concepto = "";
            this.retiros = 0;
            this.depositos = 0;
            this.saldofinal = 0;
            this.conceptoconciliado = "";
            this.documentoconciliado = "";
        }

        public InformeBancario(string corporativo, string sucursal, int año, int mes, string cuentabancofinanciero, int consecutivoflujo, DateTime fecha, string referencia, string concepto, decimal retiros, decimal depositos, decimal saldofinal, string conceptoconciliado, string documentoconciliado)
        {
            this.corporativo = corporativo;
            this.sucursal = sucursal;
            this.año = año;
            this.mes = mes;
            this.cuentabancofinanciero = cuentabancofinanciero;
            this.consecutivoflujo = consecutivoflujo;
            this.fecha = fecha;
            this.referencia = referencia;
            this.concepto = concepto;
            this.retiros = retiros;
            this.depositos = depositos;
            this.saldofinal = saldofinal;
            this.conceptoconciliado = conceptoconciliado;
            this.documentoconciliado = documentoconciliado;
        }

        public InformeBancario(string corporativo, string sucursal, int año, int mes, string cuentabancofinanciero, int consecutivoflujo, DateTime fecha, string referencia, string concepto, decimal retiros, decimal depositos, decimal saldofinal, string conceptoconciliado, string documentoconciliado, IMensajesImplementacion implementadorMensajes) : this(corporativo, sucursal, año, mes, cuentabancofinanciero, consecutivoflujo, fecha, referencia, concepto, retiros, depositos, saldofinal, conceptoconciliado, documentoconciliado)
        {
            this.implementadorMensajes = implementadorMensajes;
        }

        public string Corporativo
        {
            get { return corporativo; }
            set { corporativo = value; }
        }
        public string Sucursal
        {
            get { return sucursal; }
            set { sucursal = value; }
        }
        public int Año
        {
            get { return año; }
            set { año = value; }
        }
        public int Mes
        {
            get { return mes; }
            set { mes = value; }
        }
        public string CuentaBancoFinanciero
        {
            get { return cuentabancofinanciero; }
            set { cuentabancofinanciero = value; }
        }
        public int ConsecutivoFlujo
        {
            get { return consecutivoflujo; }
            set { consecutivoflujo = value; }
        }
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        public string Referencia
        {
            get { return referencia; }
            set { referencia = value; }
        }
        public string Concepto
        {
            get { return concepto; }
            set { concepto = value; }
        }
        public decimal Retiros
        {
            get { return retiros; }
            set { retiros = value; }
        }
        public decimal Depositos
        {
            get { return depositos; }
            set { depositos = value; }
        }
        public decimal SaldoFinal
        {
            get { return saldofinal; }
            set { saldofinal = value; }
        }
        public string ConceptoConciliado
        {
            get { return conceptoconciliado; }
            set { conceptoconciliado = value; }
        }
        public string DocumentoConciliado
        {
            get { return documentoconciliado; }
            set { documentoconciliado = value; }
        }

        public abstract InformeBancario CrearObjeto();

        public abstract List<InformeBancario> consultaPosicionDiariaBanco(Conexion _conexion, DateTime FechaIni, DateTime FechaFin, string Banco, string CuentaBanco, string Status, string StatusConcepto);

    }
}
