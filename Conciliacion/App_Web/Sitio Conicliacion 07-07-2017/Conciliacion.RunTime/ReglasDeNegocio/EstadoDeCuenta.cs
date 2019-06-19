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
        private string banco;
        private string cuentabancaria;
        private int folioexterno;
        private int secuenciaexterno;
        private string documento;
        private string transban;
        private DateTime fmovtransban;

        private DateTime foperacionExt;
        private decimal retiroExt;
        private decimal depositoExt;
        private string conceptoExt;
        private string descripcionExt;

        private DateTime foperacionInt;
        private decimal retiroInt;
        private decimal depositoInt;
        private string conceptoInt;
        private string descripcionInt;
        private int motivoNoconciliado;

        public EstadoDeCuenta(int añoconciliacion, int mesconciliacion, int folioconciliacion, 
            string banco, string cuentabancaria,
            int folioexterno, int secuenciaexterno, string documento, 
            string transban, DateTime fmovtransban, 
            DateTime foperacionExt, decimal retiroExt, decimal depositoExt, string conceptoExt, string descripcionExt,
            DateTime foperacionInt, decimal retiroInt, decimal depositoInt, string conceptoInt, string descripcionInt,
            int motivoNoconciliado)
        {
            this.añoconciliacion = añoconciliacion;
            this.mesconciliacion = mesconciliacion;
            this.folioconciliacion = folioconciliacion;
            this.banco = banco;
            this.cuentabancaria = cuentabancaria;
            this.folioexterno = folioexterno;
            this.secuenciaexterno = secuenciaexterno;
            this.documento = documento;
            this.transban = transban;
            this.fmovtransban = fmovtransban;
            this.foperacionExt = foperacionExt;
            this.retiroExt = retiroExt;
            this.depositoExt = depositoExt;
            this.conceptoExt = conceptoExt;
            this.descripcionExt = descripcionExt;
            this.foperacionInt = foperacionInt;
            this.retiroInt = retiroInt;
            this.depositoInt = depositoInt;
            this.conceptoInt = conceptoInt;
            this.descripcionInt = descripcionInt;
            this.motivoNoconciliado = motivoNoconciliado;
        }

        public EstadoDeCuenta(MensajesImplementacion implementadorMensajes)
        {
            this.implementadorMensajes = implementadorMensajes;
            this.añoconciliacion = 0;
            this.mesconciliacion = 0;
            this.folioconciliacion = 0;
            this.banco = "";
            this.cuentabancaria = "";
            this.folioexterno = 0;
            this.secuenciaexterno = 0;
            this.documento = "";
            this.transban = "";
            this.fmovtransban = DateTime.MinValue; //DateTime.Now;
            this.foperacionExt = DateTime.MinValue;
            this.retiroExt = 0;
            this.depositoExt = 0;
            this.conceptoExt = "";
            this.descripcionExt = "";
            this.foperacionInt = DateTime.MinValue;
            this.retiroInt = 0;
            this.depositoInt = 0;
            this.conceptoInt = "";
            this.descripcionInt = "";
            this.motivoNoconciliado = 0;
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
        public string Banco
        {
            get { return banco; }
            set { banco = value; }
        }
        public string CuentaBancaria
        {
            get { return cuentabancaria; }
            set { cuentabancaria = value; }
        }
        public int SecuenciaExterno
        {
            get { return secuenciaexterno; }
            set { secuenciaexterno = value; }
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
        public DateTime FOperacionExt
        {
            get { return foperacionExt; }
            set { foperacionExt = value; }
        }
        public decimal RetiroExt
        {
            get { return retiroExt; }
            set { retiroExt = value; }
        }
        public decimal DepositoExt
        {
            get { return depositoExt; }
            set { depositoExt = value; }
        }
        public string ConceptoExt
        {
            get { return conceptoExt; }
            set { conceptoExt = value; }
        }
        public string DescripcionExt
        {
            get { return descripcionExt; }
            set { descripcionExt = value; }
        }

        public DateTime FOperacionInt
        {
            get { return foperacionInt; }
            set { foperacionInt = value; }
        }
        public decimal RetiroInt
        {
            get { return retiroInt; }
            set { retiroInt = value; }
        }
        public decimal DepositoInt
        {
            get { return depositoInt; }
            set { depositoInt = value; }
        }
        public string ConceptoInt
        {
            get { return conceptoInt; }
            set { conceptoInt = value; }
        }
        public string DescripcionInt
        {
            get { return descripcionInt; }
            set { descripcionInt = value; }
        }
        public int MotivoNoConciliado
        {
            get { return motivoNoconciliado; }
            set { motivoNoconciliado = value; }
        }

        private MensajesImplementacion implementadorMensajes;

    }
}
