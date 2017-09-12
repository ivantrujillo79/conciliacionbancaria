using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime;

namespace CatalogoConciliacion.ReglasNegocio
{
    public abstract class GrupoConciliacion : EmisorMensajes
    {

        int grupoConciliacion;
        string descripcion;
        string usuario;
        string status;
        DateTime fAlta;
        int diasDefault, diasMaxima, diasMinima;
        decimal diferenciaDefault, diferenciaMaxima, diferenciaMinima;

        #region constructores

        public GrupoConciliacion(IMensajesImplementacion implementadorMensajes)
        {
            this.implementadorMensajes = implementadorMensajes;
            this.grupoConciliacion = this.diasDefault = this.diasMaxima = this.diasMinima=0 ;
            this.descripcion = string.Empty;
            this.usuario = string.Empty;
            this.status = string.Empty;
            this.fAlta = DateTime.Now;
            this.diferenciaDefault = this.diferenciaMaxima = this.diferenciaMinima = 0.0M;

        }

        public GrupoConciliacion(int grupoConciliacion, string descripcion, string usuario, string status, DateTime fAlta, int diasDefault, int diasMaxima, int diasMinima, decimal diferenciaDefault, decimal diferenciaMaxima, decimal diferenciaMinima, IMensajesImplementacion implementadorMensajes)
        {
            this.grupoConciliacion = grupoConciliacion;
            this.descripcion = descripcion;
            this.usuario = usuario;
            this.status = status;
            this.fAlta = fAlta;
            this.diasDefault = diasDefault;
            this.diasMaxima = diasMaxima;
            this.diasMinima = diasMinima;
            this.diferenciaDefault = diferenciaDefault;
            this.diferenciaMaxima = diferenciaMaxima;
            this.diferenciaMinima = diferenciaMinima;

            this.implementadorMensajes = implementadorMensajes;
        }

        #endregion


        #region Propiedades

        public int GrupoConciliacionId
        {
            get { return grupoConciliacion; }
            set { grupoConciliacion = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public DateTime FAlta
        {
            get { return fAlta; }
            set { fAlta = value; }
        }

        public int DiasDefault
        {
            get { return diasDefault; }
            set { diasDefault = value; }
        }
        public int DiasMaxima
        {
            get { return diasMaxima; }
            set { diasMaxima = value; }
        }
        public int DiasMinima
        {
            get { return diasMinima; }
            set { diasMinima = value; }
        }
        public Decimal DiferenciaDefault
        {
            get { return diferenciaDefault; }
            set { diferenciaDefault = value; }
        }
        public Decimal DiferenciaMaxima
        {
            get { return diferenciaMaxima; }
            set { diferenciaMaxima = value; }
        }
        public Decimal DiferenciaMinima
        {
            get { return diferenciaMinima; }
            set { diferenciaMinima = value; }
        }


        public virtual string CadenaConexion
        {
            get
            {
                return App.CadenaConexion;
            }
        }

        #endregion

        public abstract GrupoConciliacion CrearObjeto();
        public abstract bool CambiarStatus(int grupo);
        public abstract bool Guardar(string nombre, string usuario, int diasDefault, int diasMaxima, int diasMinima, decimal diferenciaDefault, decimal diferenciaMaxima, decimal diferenciaMinima);
        public abstract bool Modificar();
        public abstract bool AñadirStatusConcepto(int statusconcepto);
        public abstract bool QuitarStatusConcepto(int statusconcepto);


    }
}
