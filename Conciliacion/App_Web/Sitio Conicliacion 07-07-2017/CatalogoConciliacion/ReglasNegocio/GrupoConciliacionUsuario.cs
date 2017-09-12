using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime;


namespace CatalogoConciliacion.ReglasNegocio
{
    public abstract class GrupoConciliacionUsuario : EmisorMensajes 
    {

        int grupoConciliacion;
        string usuario;
        bool  accesoTotal;


        #region Constructores

        public GrupoConciliacionUsuario(IMensajesImplementacion implementadorMensajes)
        {
            this.implementadorMensajes = implementadorMensajes;
            this.grupoConciliacion = 0;
            this.usuario = string.Empty;
            this.accesoTotal = false ;
        }

        public GrupoConciliacionUsuario(int grupoConciliacion, string usuario, bool accesoTotal, IMensajesImplementacion implementadorMensajes)
        {
            this.grupoConciliacion = grupoConciliacion;
            this.usuario = usuario ;
            this.accesoTotal = accesoTotal ;
            this.implementadorMensajes = implementadorMensajes;
        }

        #endregion 


        #region Propiedades 

        public int GrupoConciliacionId
        {
            get { return grupoConciliacion; }
            set { grupoConciliacion = value; }
        }

        public string Usuario
        {
            get { return usuario  ; }
            set { usuario   = value; }
        }

        public bool  AccesoTotal
        {
            get { return accesoTotal ; }
            set { accesoTotal   = value; }
        }

        public virtual string CadenaConexion
        {
            get
            {
                return App.CadenaConexion;
            }
        }

        #endregion


        public abstract GrupoConciliacionUsuario  CrearObjeto();
        public abstract bool EliminaUsuario(int grupo, string usuario);
        public abstract bool AgregaUsuario(int grupo, string usuario,bool acceso );
        public abstract bool ModificaAcceso();

    }
}
