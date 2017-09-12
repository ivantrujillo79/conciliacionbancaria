using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio
{
    public abstract class TablaDestino:ObjetoBase
    {
        int idCorporativo;
        int idSucursal;
        int anio;
        int folio;
        string cuentaBancoFinanciero;
        int idTipoFuenteInformacion;
        int idFrecuencia;
        DateTime fInicial;
        DateTime fFinal;
        string idStatusConciliacion;

       
        string statusConciliacion;
        string usuario;
        DateTime fAlta;
        List<TablaDestinoDetalle> detalles = new List<TablaDestinoDetalle>();



        //public abstract bool Guardar();
        //public abstract bool Actualizar();
        //public abstract bool Eliminar();
        //public abstract IObjetoBase CrearObjeto();




        public int IdCorporativo
        {
            get { return idCorporativo; }
            set { idCorporativo = value; }
        }
        

        public int IdSucursal
        {
            get { return idSucursal; }
            set { idSucursal = value; }
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



        public string CuentaBancoFinanciero
        {
            get { return cuentaBancoFinanciero; }
            set { cuentaBancoFinanciero = value; }
        }


        public int IdTipoFuenteInformacion
        {
            get { return idTipoFuenteInformacion; }
            set { idTipoFuenteInformacion = value; }
        }
    

        public int IdFrecuencia
        {
            get { return idFrecuencia; }
            set { idFrecuencia = value; }
        }
     

        public DateTime FInicial
        {
            get { return fInicial; }
            set { fInicial = value; }
        }
 

        public DateTime FFinal
        {
            get { return fFinal; }
            set { fFinal = value; }
        }


        public string IdStatusConciliacion
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
    

        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }
    

        public DateTime FAlta
        {
            get { return fAlta; }
            set { fAlta = value; }
        }


        //public virtual string CadenaConexion
        //{
        //    get
        //    {
        //        return App.CadenaConexion;
        //    }

        //}



        public Corporativo Corporativo
        {

            get
            {

                return App.Consultas.ObtieneCorporativoPorId(this.idCorporativo);
            }
        }

        public Sucursal Sucursal
        {

            get
            {

                return App.Consultas.ObtieneSucursalPorId(this.IdSucursal);
            }
        }

        public TipoFuenteInformacion TipoFuenteInformacion
        {
            get
            {

                return App.Consultas.ObtieneTipoFuenteDeInformacionePorId(this.IdTipoFuenteInformacion);
            }
        }

        public Frecuencia Frecuencia
        {
            get
            {
                return App.Consultas.ObtieneFrecuenciaPorId(this.IdFrecuencia);
            }
        }


        public List<TablaDestinoDetalle> Detalles
        {
            get { return detalles; }
            set { detalles = value; }
        }
    }
}
