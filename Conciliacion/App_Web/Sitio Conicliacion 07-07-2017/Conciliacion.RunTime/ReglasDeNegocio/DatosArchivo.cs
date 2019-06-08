using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class DatosArchivo:EmisorMensajes
    {
       int corporativo;
       int sucursalconciliacion;
       int sucursal;
       int año;
       short mesconciliacion;
       int folio;
       int folioconciliacion;
       string cuentabanco;
       DateTime finicial;
       DateTime ffinal;
       short tipofuenteinformacion;
       string tipofuenteinformaciondes;
       short tipofuente;
       string tipofuentedes;
       string statusconciliacion;

       #region Constructores
       //public DatosArchivo(MensajesImplementacion implementadorMensajes)
       //{
       //    this.ImplementadorMensajes = implementadorMensajes;
       //    this.corporativo=0;
       //    this.sucursal=0;
       //    this.año=0;
       //    this.folio=0;
       //    this.cuentabanco="";
       //    this.tipofuenteinformacion=0;
       //    this.statusconciliacion="";
       //}

        protected DatosArchivo(MensajesImplementacion implementadorMensajes)
        {
            this.implementadorMensajes = implementadorMensajes;
            this.corporativo = 0;
            this.sucursal = 0;
            this.año = 0;
            this.folio = 0;
            this.cuentabanco = "";
            this.tipofuenteinformacion = 0;
            this.statusconciliacion = "";
        }

        //public DatosArchivo(int corporativo, int sucursal, int año, int folio, string cuentabanco, short tipofuenteinformacion, string statusconciliacion, MensajesImplementacion implementadorMensajes)
        //{
        //    this.corporativo=corporativo;
        //    this.sucursal=sucursal;
        //    this.año=año;
        //    this.folio=folio;
        //    this.cuentabanco=cuentabanco;
        //    this.tipofuenteinformacion=tipofuenteinformacion;
        //    this.statusconciliacion=statusconciliacion;
        //    this.ImplementadorMensajes = implementadorMensajes;
        //}

        public DatosArchivo(int corporativo, int sucursal, int año, int folio, string cuentabanco,DateTime finicial, DateTime ffinal, short tipofuenteinformacion, string tipofuenteinformaciondes, string statusconciliacion, short tipofuente, string tipofuentedes,int sucursalconciliacion, int folioconciliacion, short mesconciliacion, MensajesImplementacion implementadorMensajes)
       {
           this.corporativo = corporativo;
           this.sucursal = sucursal;
           this.año = año;
           this.folio = folio;
           this.cuentabanco = cuentabanco;
           this.finicial = finicial;
           this.ffinal = ffinal;
           this.tipofuenteinformacion = tipofuenteinformacion;
           this.tipofuenteinformaciondes = tipofuenteinformaciondes;
           this.statusconciliacion = statusconciliacion;
           this.tipofuente = tipofuente;
           this.tipofuentedes = tipofuentedes;
           this.sucursalconciliacion = sucursalconciliacion;
           this.folioconciliacion=folioconciliacion;
           this.mesconciliacion = mesconciliacion;
           this.ImplementadorMensajes = implementadorMensajes;
       }

        #endregion


        public abstract DatosArchivo CrearObjeto();
        public abstract bool GuardarArchivoInterno();
        public abstract bool ExisteArchivoInternoConciliacion();
        public abstract bool BorrarArchivoInterno();

       #region Propiedades

        public int Corporativo {
            get{return corporativo;}
            set {corporativo=value;}
        }

        public int Sucursal { 
            get {return sucursal;}
            set {sucursal = value;}
        }

        public int SucursalConciliacion
        {
            get { return sucursalconciliacion; }
            set { sucursalconciliacion = value; }
        }

        public int Año {
            get{return año;}
            set{año=value;} 
        }

        public short MesConciliacion
        {
            get { return mesconciliacion; }
            set { mesconciliacion = value; }
        }

        public int Folio {
            get{return folio;}
            set{folio =value;}
        }

        public int FolioConciliacion
        {
            get { return folioconciliacion; }
            set { folioconciliacion = value; }
        }

        public string CuentaBanco {
            get{return cuentabanco;} 
            set{cuentabanco=value;} 
        }

        public DateTime FInicial
        {
            get { return finicial; }
            set { finicial = value; }
        }

        public DateTime FFinal
        {
            get { return ffinal; }
            set { ffinal = value; }
        }

        public short TipoFuenteInformacion {
            get{return tipofuenteinformacion;} 
            set{tipofuenteinformacion=value;}
        }

        public string TipoFuenteInformacionDes
        {
            get { return tipofuenteinformaciondes; }
            set { tipofuenteinformaciondes = value; }
        }

        public string StatusConciliacion {
            get { return statusconciliacion; }
            set { statusconciliacion = value; }
        }

        public short TipoFuente
        {
            get { return tipofuente; }
            set { tipofuente = value; }
        }

        public string TipoFuenteDes
        {
            get { return tipofuentedes; }
            set { tipofuentedes = value; }
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
