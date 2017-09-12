using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio
{
    public abstract class StatusConcepto : EmisorMensajes
    {

        int id;
        string descripcion;
        string usuario;
        string status;
        DateTime fAlta;
        private bool permiteCaptura;
        private int tipoTransferencia;
        private int orden;

        #region constructores

        public StatusConcepto(IMensajesImplementacion implementadorMensajes)
        {
            this.implementadorMensajes = implementadorMensajes;
            this.id = 0;
            this.descripcion = string.Empty;
            this.usuario = string.Empty;
            this.status = string.Empty;
            this.fAlta = DateTime.Now;
            this.permiteCaptura = false;
            this.tipoTransferencia = 0;
            this.orden = 0;
        }

        public StatusConcepto(int id, string descripcion, string usuario, string status, DateTime fAlta, IMensajesImplementacion implementadorMensajes)
        {
            this.id = id;
            this.descripcion = descripcion;
            this.usuario = usuario;
            this.status = status;
            this.fAlta = fAlta;

            this.implementadorMensajes = implementadorMensajes;
        }

        #endregion


        #region IObjetoBase Members

        public abstract StatusConcepto CrearObjeto();

        public abstract bool Guardar();
        public abstract bool ActualizarDescripcion();
        public abstract bool CambiarStatus();
        public abstract bool Eliminar();

        public abstract bool AgregaEtiquetaStatus(int etiqueta);
        public abstract bool EliminaEtiquetaStatus(int etiqueta);
        #endregion


        public int Id
        {
            get { return id; }
            set { id = value; }
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

        public bool PermiteCaptura
        {
            get { return permiteCaptura; }
            set { permiteCaptura = value; }
        }

        public int TipoTransferencia
        {
            get { return tipoTransferencia; }
            set { tipoTransferencia = value; }
        }

        public int Orden
        {
            get { return orden; }
            set { orden = value; }
        }

        public virtual string CadenaConexion
        {
            get
            {
                return App.CadenaConexion;
            }
        }

    }
}
