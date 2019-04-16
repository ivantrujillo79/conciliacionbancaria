using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class cReferencia : EmisorMensajes
    {
        int corporativo;
        int sucursal;
        string sucursaldes;
        int año;
        int folio;
        int secuencia;
        string concepto;
        decimal montoconciliado;
        decimal diferencia;
        short formaconciliacion;
        short statusconcepto;
        string statusconciliacion;
        DateTime foperacion;
        DateTime fmovimiento;

        string cheque;
        string referencia;
        string descripcion;
        string nombretercero;
        string rfctercero;
        decimal deposito;
        decimal retiro;
        private IMensajesImplementacion iMensajesImplementacion;

        decimal importeComision;
        decimal ivaComision;

        string usuario;

        #region Constructores

        public cReferencia(int corporativo, int sucursal, string sucursaldes, int año, int folio,int secuencia, string concepto, decimal montoconciliado, decimal diferencia, short formaconciliacion, short statusconcepto, string statusconciliacion, DateTime foperacion, DateTime fmovimiento, 
            string cheque, string referencia, string descripcion, string nombretercero, string rfctercero, decimal deposito, decimal retiro,
            IMensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.corporativo = corporativo;
            this.sucursal = sucursal;
            this.sucursaldes = sucursaldes;
            this.año = año;
            this.folio = folio;
            this.secuencia = secuencia;
            this.concepto = concepto;
            this.montoconciliado = montoconciliado;
            this.diferencia = diferencia;
            this.formaconciliacion = formaconciliacion;
            this.statusconcepto = statusconcepto;
            this.statusconciliacion = statusconciliacion;
            this.foperacion = foperacion;
            this.fmovimiento = fmovimiento;

            this.cheque = cheque;
            this.referencia=referencia;
            this.descripcion=descripcion;
            this.nombretercero=nombretercero;
            this.rfctercero=rfctercero;
            this.deposito=deposito;
            this.retiro=retiro;
        }

        public cReferencia(IMensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.corporativo = 0;
            this.sucursal = 0;
            this.año = 0;
            this.folio = 0;
            this.secuencia = 0;
            this.concepto = "";
            this.montoconciliado = 0;
            this.diferencia = 0;
            this.formaconciliacion = 0;
            this.statusconcepto = 0;
            this.statusconciliacion = "";

            this.cheque = "";
            this.referencia = "";
            this.descripcion = "";
            this.nombretercero = "";
            this.rfctercero = "";
            this.deposito = 0;
            this.retiro = 0;

            this.usuario = "";
        }

     

        #endregion

        public abstract bool Guardar();
        public abstract bool Modificar();
        public abstract bool Eliminar();
        //public abstract Referencia CrearObjeto();

        #region Propiedades

        public int Corporativo
        {
            get { return corporativo; }
            set { corporativo = value; }
        }

        public int Sucursal
        {
            get { return sucursal; }
            set { sucursal = value; }
        }

        public string SucursalDes
        {
            get { return sucursaldes; }
            set { sucursaldes = value; }
        }

        public int Año
        {
            get { return año; }
            set { año = value; }
        }

        public int Folio
        {
            get { return folio; }
            set { folio = value; }
        }

        public int Secuencia
        {
            get { return secuencia; }
            set { secuencia = value; }
        }

        public string Concepto
        {
            get { return concepto; }
            set { concepto = value; }
        }

        public decimal MontoConciliado
        {
            get { return montoconciliado; }
            set { montoconciliado = value; }
        }

        public decimal Diferencia
        {
            get { return diferencia; }
            set { diferencia = value; }
        }

        public short FormaConciliacion
        {
            get { return formaconciliacion; }
            set { formaconciliacion = value; }
        }

        public short StatusConcepto
        {
            get { return statusconcepto; }
            set { statusconcepto = value; }
        }

        public string StatusConciliacion
        {
            get { return statusconciliacion;}
            set {statusconciliacion =value;}
        }

        public DateTime FOperacion
        {
            get { return foperacion; }
            set { foperacion = value; }
        }

        public DateTime FMovimiento
        {
            get { return fmovimiento; }
            set { fmovimiento = value; }
        }

        public string Cheque
        {
            get { return cheque; }
            set { cheque = value; }
        }

        public string Referencia
        {
            get { return referencia; }
            set { referencia = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public string NombreTercero
        {
            get { return nombretercero; }
            set { nombretercero = value; }
        }

        public string RFCTercero
        {
            get { return rfctercero; }
            set { rfctercero = value; }
        }

        public decimal Deposito
        {
            get { return deposito; }
            set { deposito = value; }
        }

        public decimal Retiro
        {
            get { return retiro; }
            set { retiro = value; }
        }

        public decimal ImporteComision
        {
            get { return importeComision; }
            set { importeComision = value; }
        }

        public decimal IVAComision
        {
            get { return ivaComision; }
            set { ivaComision = value; }
        }

        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        #endregion

        public virtual string CadenaConexion
        {
            get
            {
                return App.CadenaConexion;
            }
        }

    }
}
