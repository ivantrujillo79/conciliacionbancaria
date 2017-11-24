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

        public FacturasComplemento(IMensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.corporativoConciliacion=0;
            this.sucursalConciliacion=0;
            this.anioConciliacion = 0;
            this.mesConciliacion = 0;
            this.folioConciliacion = 0;            
        }

        public FacturasComplemento(int corporativoConciliacion, int sucursalConciliacion, int anioConciliacion, int mesConciliacion,
            int folioConciliacion, IMensajesImplementacion implementadorMensajes)
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
            set { value = corporativoConciliacion; }
        }

        public int SucursalConciliacion
        {
            get {return sucursalConciliacion; }
            set { value = sucursalConciliacion; }
        }

        public int AnioConciliacion
        {
            get {return anioConciliacion; }
            set {value= anioConciliacion; }
        }

        public int MesConciliacion
        {
            get {return mesConciliacion; }
            set {value= mesConciliacion; }
        }

        public int FolioConciliacion
        {
            get {return folioConciliacion; }
            set {value= folioConciliacion; }
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
