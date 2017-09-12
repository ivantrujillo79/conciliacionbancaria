using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class cFuenteInformacion : EmisorMensajes
    {
        int fuenteinformacion;
        string rutaarchivo;
        string cuentabancofinanciero;
        short tipofuenteinformacion;
        string tipofuenteinformaciondes;
        short tipofuente;
        string tipofuentedes;
        short tipoarchivo;
        string tipoarchivodes;

        #region Constructores

        public cFuenteInformacion(IMensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.fuenteinformacion=0;
            this.rutaarchivo="";
            this.cuentabancofinanciero = "";
            this.tipofuenteinformacion = 0;
            this.tipofuenteinformaciondes = "";
            this.tipofuente = 0;
            this.tipofuentedes = "";
            this.tipoarchivo = 0;
            this.TipoArchivoDes = "";
        }

        public cFuenteInformacion(int fuenteinformacion, string rutaarchivo, string cuentabancofinanciero,short tipofuenteinformacion, 
            string tipofuenteinformaciondes,short tipofuente, string tipofuentedes, short tipoarchivo, string tipoarchivodes, IMensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.fuenteinformacion = fuenteinformacion;
            this.rutaarchivo = rutaarchivo;
            this.cuentabancofinanciero = cuentabancofinanciero;
            this.tipofuenteinformacion = tipofuenteinformacion;
            this.tipofuenteinformaciondes = tipofuenteinformaciondes;
            this.tipofuente = tipofuente;
            this.tipofuentedes = tipofuentedes;
            this.tipoarchivo = tipoarchivo;
            this.tipoarchivodes = tipoarchivodes;
        }


        #endregion

        public abstract cFuenteInformacion CrearObjeto();

        #region Propiedades

        public int FuenteInformacion
        {
            get { return fuenteinformacion;}
            set { value =fuenteinformacion;}
        }

        public string RutaArchivo
        {
            get {return rutaarchivo;}
            set { value = rutaarchivo;}
        }

        public string CuentaBancoFinanciero
        {
            get {return cuentabancofinanciero;}
            set {value= cuentabancofinanciero;}
        }

        public short TipoFuenteInformacion
        {
            get {return tipofuenteinformacion;}
            set {value= tipofuenteinformacion;}
        }

        public string TipoFuenteInformacionDes
        {
            get {return tipofuenteinformaciondes;}
            set {value= tipofuenteinformaciondes;}
        }

        public short TipoFuente
        {
            get {return tipofuente;}
            set {value= tipofuente;}
        }
        public string TipoFuenteDes
        {
            get {return tipofuentedes;}
            set {value= tipofuentedes;}
        }
        public short TipoArchivo
        {
            get {return tipoarchivo;}
            set { value = tipoarchivo; }
        }
        public string TipoArchivoDes
        {
            get { return tipoarchivodes; }
            set { value = tipoarchivodes; }
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
