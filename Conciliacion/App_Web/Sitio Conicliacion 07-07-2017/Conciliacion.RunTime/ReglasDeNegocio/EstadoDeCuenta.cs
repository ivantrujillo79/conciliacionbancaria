using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public class EstadoDeCuenta
    {
        private int añoconciliacion;
        private int mesconciliacion;
        private int folioconciliacion;
        private int folioexterno;
        private string documento;
        private string transban;
        private DateTime fmovtransban;
        private DateTime foperacion;
        private decimal retiro;
        private decimal deposito;
        private string concepto;
        private string descripcion;

        public EstadoDeCuenta(int añoconciliacion, int mesconciliacion, int folioconciliacion, int folioexterno, string documento, string transban, DateTime fmovtransban, DateTime foperacion, decimal retiro, decimal deposito, string concepto, string descripcion)
        {
            this.añoconciliacion = añoconciliacion;
            this.mesconciliacion = mesconciliacion;
            this.folioconciliacion = folioconciliacion;
            this.folioexterno = folioexterno;
            this.documento = documento;
            this.transban = transban;
            this.fmovtransban = fmovtransban;
            this.foperacion = foperacion;
            this.retiro = retiro;
            this.deposito = deposito;
            this.concepto = concepto;
            this.descripcion = descripcion;
        }

        public EstadoDeCuenta(MensajesImplementacion implementadorMensajes)
        {
            this.implementadorMensajes = implementadorMensajes;
            this.añoconciliacion = 0;
            this.mesconciliacion = 0;
            this.folioconciliacion = 0;
            this.folioexterno = 0;
            this.documento = "";
            this.transban = "";
            this.fmovtransban = DateTime.Now;
            this.foperacion = DateTime.Now;
            this.retiro = 0;
            this.deposito = 0;
            this.concepto = "";
            this.descripcion = "";
        }

        public int AñoConciliacion
        {
            get { return añoconciliacion; }
            set { añoconciliacion = value; }
        }
        public int MesConciliacion
        {
            get { return mesconciliacion; }
            set { mesconciliacion = value; }
        }
        public int FolioConciliacion
        {
            get { return folioconciliacion; }
            set { folioconciliacion = value; }
        }
        public int FolioExterno
        {
            get { return folioexterno; }
            set { folioexterno = value; }
        }
        public string Documento
        {
            get { return documento; }
            set { documento = value; }
        }
        public string Transban
        {
            get { return transban; }
            set { transban = value; }
        }
        public DateTime FMovTransban
        {
            get { return fmovtransban; }
            set { fmovtransban = value; }
        }
        public DateTime FOperacion
        {
            get { return foperacion; }
            set { foperacion = value; }
        }
        public decimal Retiro
        {
            get { return retiro; }
            set { retiro = value; }
        }
        public decimal Deposito
        {
            get { return deposito; }
            set { deposito = value; }
        }
        public string Concepto
        {
            get { return concepto; }
            set { concepto = value; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        private MensajesImplementacion implementadorMensajes;

    }
}
