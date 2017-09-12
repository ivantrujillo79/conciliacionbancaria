using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime;

namespace CatalogoConciliacion.ReglasNegocio
{
    public abstract class TipoConciliacionUsuario : EmisorMensajes
    {
        int tipoConciliacion;
        string usuario;

        #region Constructores


        public TipoConciliacionUsuario(IMensajesImplementacion implementadorMensajes)
        {
            this.implementadorMensajes = implementadorMensajes;
            this.tipoConciliacion = 0;
            this.usuario = string.Empty;
        }

        public TipoConciliacionUsuario(int tipoConciliacion, string usuario, IMensajesImplementacion implementadorMensajes)
        {
            this.tipoConciliacion = tipoConciliacion;
            this.usuario = usuario ;
            this.implementadorMensajes = implementadorMensajes;
        }

        #endregion 


        #region Propiedades 

        public int TipoConciliacionId
        {
            get { return tipoConciliacion; }
            set { tipoConciliacion = value; }
        }

        public string Usuario
        {
            get { return usuario  ; }
            set { usuario   = value; }
        }

        public virtual string CadenaConexion
        {
            get
            {
                return App.CadenaConexion;
            }
        }

        #endregion


        public abstract TipoConciliacionUsuario  CrearObjeto();
        public abstract bool DesasignarTipo(int tipo, string usuario);
        public abstract bool AsignarTipo(int tipo, string usuario);

    }
}
