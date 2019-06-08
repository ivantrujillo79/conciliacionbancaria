using Conciliacion.RunTime.DatosSQL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class FacturasComplemento : EmisorMensajes
    {
        private int corporativoConciliacion;
        private int sucursalConciliacion;
        private int anioConciliacion;
        private int mesConciliacion;
        private int folioConciliacion;


        #region Constructores

        public FacturasComplemento(MensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.corporativoConciliacion=0;
            this.sucursalConciliacion=0;
            this.anioConciliacion = 0;
            this.mesConciliacion = 0;
            this.folioConciliacion = 0;            
        }

        public FacturasComplemento(int corporativoConciliacion, int sucursalConciliacion, int anioConciliacion, int mesConciliacion,
            int folioConciliacion, MensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.corporativoConciliacion = corporativoConciliacion;
            this.sucursalConciliacion = sucursalConciliacion;            
            this.anioConciliacion = anioConciliacion;
            this.mesConciliacion = mesConciliacion;
            this.folioConciliacion = folioConciliacion;
        }


        #endregion

        public abstract FacturasComplemento CrearObjeto();

        public abstract Boolean Guardar(Conexion _conexion);

        #region Propiedades

        public int CorporativoConciliacion
        {
            get { return corporativoConciliacion; }
            set { corporativoConciliacion = value; }
        }

        public int SucursalConciliacion
        {
            get {return sucursalConciliacion; }
            set { sucursalConciliacion = value; }
        }

        public int AnioConciliacion
        {
            get {return anioConciliacion; }
            set {anioConciliacion = value; }
        }

        public int MesConciliacion
        {
            get {return mesConciliacion; }
            set {mesConciliacion = value; }
        }

        public int FolioConciliacion
        {
            get {return folioConciliacion; }
            set {folioConciliacion = value; }
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
