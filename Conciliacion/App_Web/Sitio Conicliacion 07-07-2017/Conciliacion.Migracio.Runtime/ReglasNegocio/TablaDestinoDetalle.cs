using Conciliacion.Migracion.Runtime.SqlDatos;
using Conciliacion.RunTime.DatosSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio
{
    public abstract class TablaDestinoDetalle :ObjetoBase
    {
        int idCorporativo;
        int idSucursal;
        int anio;
        int folio;
        int secuencia;

        string cuentaBancaria;
        Nullable<DateTime> fOperacion;
        Nullable<DateTime> fMovimiento;
        string descripcion;
        string referencia;
        string transaccion;
        string sucursalBancaria;
        double deposito;
        double retiro;
        double saldoInicial;
        double saldoFinal;
        string movimiento;
        string cuentaTercero;
        string cheque;
        string _RFCTercero;
        string nombreTercero;
        string clabeTercero;
        string concepto;
        string poliza;
        int idCaja;
        int idStatusConcepto;
        string  idStatusConciliacion;
        int idConceptoBanco;
        int idMotivoNoConciliado;
        private int tipoFuenteInformacion;
        private int clientepago;
        private int afiliacion;

        //#region IObjetoBase Members
        //public virtual string CadenaConexion
        //{
        //    get
        //    {
        //        return App.CadenaConexion;
        //    }
        //}
        //public abstract bool Actualizar();
        //public abstract IObjetoBase CrearObjeto();
        //public abstract bool Eliminar();
        //public abstract bool Guardar();
        //#endregion

        int tipocobro;

        public int TipoCobro
        {
            get { return tipocobro; }
            set { tipocobro = value; }
        }

        public int IdCorporativo
        {
            get { return idCorporativo; }
            set { idCorporativo = value; }
        }


        public Corporativo Corporativo
        {
            get
            {
                return App.Consultas.ObtieneCorporativoPorId(this.IdCorporativo);
            }
        }

        public int IdSucursal
        {
            get { return idSucursal; }
            set { idSucursal = value; }
        }

        public Sucursal Sucursal
        {
            get
            {
                return App.Consultas.ObtieneSucursalPorId(this.IdSucursal);
            }
        }


        public int Anio
        {
            get { return anio; }
            set { anio = value; }
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

    

        public string CuentaBancaria
        {
            get { return cuentaBancaria; }
            set { cuentaBancaria = value; }
        }
    

        public Nullable<DateTime> FOperacion
        {
            get { return fOperacion; }
            set { fOperacion = value; }
        }
   

        public Nullable<DateTime> FMovimiento
        {
            get { return fMovimiento; }
            set { fMovimiento = value; }
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


        public string Transaccion
        {
            get { return transaccion; }
            set { transaccion = value; }
        }


        public string SucursalBancaria
        {
            get { return sucursalBancaria; }
            set { sucursalBancaria = value; }
        }


        public double Deposito
        {
            get { return deposito; }
            set { deposito = value; }
        }


        public double Retiro
        {
            get { return retiro; }
            set { retiro = value; }
        }


        public double SaldoInicial
        {
            get { return saldoInicial; }
            set { saldoInicial = value; }
        }
   

        public double SaldoFinal
        {
            get { return saldoFinal; }
            set { saldoFinal = value; }
        }
  

        public string Movimiento
        {
            get { return movimiento; }
            set { movimiento = value; }
        }
             

        public string CuentaTercero
        {
            get { return cuentaTercero; }
            set { cuentaTercero = value; }
        }
              

        public string Cheque
        {
            get { return cheque; }
            set { cheque = value; }
        }
 

        public string RFCTercero
        {
            get { return _RFCTercero; }
            set { _RFCTercero = value; }
        }

        public string NombreTercero
        {
            get { return nombreTercero; }
            set { nombreTercero = value; }
        }
        

        public string ClabeTercero
        {
            get { return clabeTercero; }
            set { clabeTercero = value; }
        }
       

        public string Concepto
        {
            get { return concepto; }
            set { concepto = value; }
        }
   
        public string Poliza
        {
            get { return poliza; }
            set { poliza = value; }
        }
             

        public int IdCaja
        {
            get { return idCaja; }
            set { idCaja = value; }
        }

        public Caja Caja
        {
            get
            {

                return App.Consultas.ObtieneCajaPorId(this.IdCaja);
            }
        }
   

        public int IdStatusConcepto
        {
            get { return idStatusConcepto; }
            set { idStatusConcepto = value; }
        }


        public StatusConcepto StatusConcepto
        {
            get
            {
                return App.Consultas.ObtieneStatusConceptoPorId(this.IdStatusConcepto);
            }
        }


        public string  IdStatusConciliacion
        {
            get { return idStatusConciliacion; }
            set { idStatusConciliacion = value; }
        }

        public StatusConciliacion StatusConciliacion
        {
            get
            {
                return App.Consultas.ObtieneStatusConciliacionPorId(this.IdStatusConciliacion);
            }
        }
  

        public int IdConceptoBanco
        {
            get { return idConceptoBanco; }
            set { idConceptoBanco = value; }
        }

        public ConceptoBanco ConceptoBanco
        {

            get
            {
                return App.Consultas.ObtieneConceptoBancoPorId(this.IdConceptoBanco);
            }
        }
  
        public int IdMotivoNoConciliado
        {
            get { return idMotivoNoConciliado; }
            set { idMotivoNoConciliado = value; }
        }

        public int TipoFuenteInformacion
        {
            get { return tipoFuenteInformacion; }
            set { tipoFuenteInformacion = value; }
        }

        public int ClientePago
        {
            get { return clientepago; }
            set { clientepago = value; }
        }

        public int Afiliacion
        {
            get {return afiliacion; }
            set { afiliacion = value; }
        }

        public MotivoNoConciliado MotivoNoConciliado
        {
            get
            {
                return App.Consultas.ObtieneMotivoNoConciliadoPorId(this.IdMotivoNoConciliado);
            }
        }

        public void ActualizarClientePago()
        {
            App.Consultas.ActualizarClientePago(this);
        }

        public int ExisteClientePago()
        {
            return App.Consultas.ExisteClientePago(this);
        }

    }
}
