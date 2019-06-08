using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class FlujoProyectado : EmisorMensajes
    {
        int corporativo;
        int año;
        short mes;
        int statusconcepto;
        string statusconceptodes;
        short tipotransferencia;
        DateTime fflujo;
        decimal importeproyectado;
        decimal importereal;
        string tipoflujo;
        string statusmes;
        #region Propiedades
        public int Corporativo
        {
            get { return corporativo; }
            set { corporativo = value; }
        }

        public int Año
        {
            get { return año; }
            set { año = value; }
        }

        public short Mes
        {
            get { return mes; }
            set { mes = value; }
        }
        public int StatusConcepto
        {
            get { return statusconcepto; }
            set { statusconcepto = value; }
        }
        public string StatusConceptoDes
        {
            get { return statusconceptodes; }
            set { statusconceptodes = value; }
        }

        public short TipoTransferencia
        {
            get { return tipotransferencia; }
            set { tipotransferencia = value; }
        }
        public string TipoFlujo
        {
            get { return tipoflujo; }
            set { tipoflujo = value; }
        }
        public DateTime FFlujo
        {
            get { return fflujo; }
            set { fflujo = value; }
        }

        public decimal ImporteProyectado
        {
            get { return importeproyectado; }
            set { importeproyectado = value; }
        }

        public decimal ImporteReal
        {
            get { return importereal; }
            set { importereal = value; }
        }
        public string StatusMes
        {
            get { return statusmes; }
            set { statusmes = value; }
        }
        #endregion
        #region Constructores
        public FlujoProyectado(MensajesImplementacion implementadorMensajes)
        {
            this.corporativo = 0;
            this.año = 0;
            this.mes = 0;
            this.statusconcepto = 0;
            this.statusconceptodes = "";
            this.tipotransferencia = 0;
            this.fflujo = DateTime.Now;
            this.importeproyectado = 0;
            this.importereal = 0;
            this.tipoflujo = "";
            this.statusmes = "";
        }
        public FlujoProyectado(
        int corporativo,
        int año,
        short mes,
        int statusconcepto,
        string statusconceptodes,
        short tipotransferencia,
        DateTime fflujo,
        decimal importeproyectado,
        decimal importereal,
        string tipoflujo,
        string statusmes)
        {
            this.corporativo = corporativo;
            this.año = año;
            this.mes = mes;
            this.statusconcepto = statusconcepto;
            this.statusconceptodes = statusconceptodes;
            this.tipotransferencia = tipotransferencia;
            this.fflujo = fflujo;
            this.importeproyectado = importeproyectado;
            this.importereal = importereal;
            this.tipoflujo = tipoflujo;
            this.statusmes = statusmes;
        }
        #endregion
        #region Metodos

        public abstract bool ActualizarFlujoEfectivo(string usuario);

        #endregion
    }
}
