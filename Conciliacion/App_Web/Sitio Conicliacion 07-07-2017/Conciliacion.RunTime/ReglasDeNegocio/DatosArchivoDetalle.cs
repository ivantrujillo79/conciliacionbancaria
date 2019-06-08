using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class DatosArchivoDetalle : EmisorMensajes
    {
        int corporativo;
        int sucursal;
        int año;
        int folio;
        int secuencia;
        DateTime foperacion;
        DateTime fmovimiento;
        string referencia;
        string descripcion;
        decimal deposito;
        decimal retiro;
        string concepto;

        #region Constructores

        public DatosArchivoDetalle(MensajesImplementacion implementadorMensajes)
        {
            this.implementadorMensajes = implementadorMensajes;
        }

        public DatosArchivoDetalle(int corporativo, int sucursal, int año, int folio, int secuencia, DateTime foperacion, DateTime fmovimiento, string referencia, string descripcion, decimal deposito, decimal retiro, string concepto, MensajesImplementacion implementadorMensajes)
        {
            this.corporativo = corporativo;
            this.sucursal = sucursal;
            this.folio = folio;
            this.secuencia = secuencia;
            this.foperacion = foperacion;
            this.fmovimiento = fmovimiento;
            this.referencia = referencia;
            this.descripcion = descripcion;
            this.deposito = deposito;
            this.retiro = retiro;
            this.concepto = concepto;
            this.implementadorMensajes = implementadorMensajes;
        }

        #endregion

        public abstract DatosArchivoDetalle CrearObjeto();

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

        public string Concepto
        {
            get { return concepto; }
            set { concepto = value; }
        }

        #endregion

        public virtual string CadenaConexion
        {
            get
            {
                Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
                return objApp.CadenaConexion;
            }
        }

    }
}
