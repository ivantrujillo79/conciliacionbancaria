using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.DatosSQL;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class MovimientoCajaConciliacion: EmisorMensajes
    {

		private short caja;
		private DateTime foperacion;
		private short consecutivo;
		private int folio;
		private short corporativoconciliacion;
		private short sucursalconciliacion;
		private int a�oconciliacion;
		private short mesconciliacion;
		private int folioconciliacion;
		private short status;

        #region Constructores
        public MovimientoCajaConciliacion(IMensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.Caja = 0;
            this.Consecutivo = 0;
            this.Folio = 0;
            this.CorporativoConciliacion = 0;
            this.SucursalConciliacion = 0;
            this.A�oConciliacion = 0;
            this.MesConciliacion = 0;
            this.FolioConciliacion = 0;
            this.Status = 0; 
        }

        public MovimientoCajaConciliacion(short caja, DateTime foperacion, short consecutivo, int folio, short corporativoconciliacion, short sucursalconciliacion, int a�oconciliacion, short mesconciliacion, int folioconciliacion, short status, IMensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.Caja = caja;
            this.FOperacion = foperacion;
            this.Consecutivo = consecutivo;
            this.Folio = folio;
            this.CorporativoConciliacion = corporativoconciliacion;
            this.SucursalConciliacion = sucursalconciliacion;
            this.A�oConciliacion = a�oconciliacion;
            this.MesConciliacion = mesconciliacion;
            this.FolioConciliacion = folioconciliacion;
            this.Status = status;
        }

        public virtual void Dispose()
        {

        }
        #endregion

        #region Propiedades
        public short Caja{
			get { return caja; }
            set { caja = value; }
        }

		public DateTime FOperacion{
			get { return FOperacion; }
            set { FOperacion = value; }
        }

		public short Consecutivo{
            get { return consecutivo; }
            set { consecutivo = value; }
        }

        public int Folio{
            get { return folio; }
            set { folio = value; }
        }

        public short CorporativoConciliacion{
            get { return corporativoconciliacion; }
            set { corporativoconciliacion = value; }
        }

        public short SucursalConciliacion{
            get { return sucursalconciliacion; }
            set { sucursalconciliacion = value; }
        }

		public int A�oConciliacion{
            get { return a�oconciliacion; }
            set { a�oconciliacion = value; }
        }

		public short MesConciliacion{
            get { return mesconciliacion; }
            set { mesconciliacion = value; }
        }

		public int FolioConciliacion{
            get { return folioconciliacion; }
            set { folioconciliacion = value; }
        }

		public short Status{
            get { return status; }
            set { status = value; }
        }
        #endregion

        public abstract void Guardar(Conexion _conexion);

    }//end MovimientoCajaConciliacion

}//end namespace ReglasDeNegocio