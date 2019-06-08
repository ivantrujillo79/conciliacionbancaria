using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime;


namespace CatalogoConciliacion.ReglasNegocio
{
    public abstract class MotivoNoConciliado : EmisorMensajes 
    {

        int motivoNoConciliado;
        string descripcion;
        string status;


#region Constructores

        public MotivoNoConciliado(MensajesImplementacion implementadorMensajes)
        {
            this.motivoNoConciliado = 0;
            this.descripcion = "";
            this.status = "ACTIVO";
            this.implementadorMensajes = implementadorMensajes;
        }

        public MotivoNoConciliado(int motivoNoConciliado, string descripcion, string status, MensajesImplementacion implementadorMensajes)
        {
            this.motivoNoConciliado = motivoNoConciliado;
            this.descripcion = descripcion ;
            this.status = status;
            this.ImplementadorMensajes = implementadorMensajes;
        }

        #endregion

        #region Propiedades
        public int MotivoNoConciliadoId
        {
            get { return motivoNoConciliado; }
            set { motivoNoConciliado = value; }
        }

        public string Descripcion
        {
            get { return descripcion ; }
            set { descripcion  = value; }
        }

        public string Status
        {
            get { return status; }
            set { status  = value; }
        }


        public virtual string CadenaConexion
        {
            get
            {
                Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
                return objApp.CadenaConexion;
            }
        }

#endregion


        public abstract MotivoNoConciliado CrearObjeto();
        public abstract bool CambiarStatus(int motivo);
        public abstract bool Guardar();
        public abstract bool Modificar();

    }
}
