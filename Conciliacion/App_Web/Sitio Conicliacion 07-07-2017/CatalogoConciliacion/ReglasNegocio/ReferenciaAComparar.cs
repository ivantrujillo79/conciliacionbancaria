using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime;


namespace CatalogoConciliacion.ReglasNegocio
{
    public abstract class ReferenciaAComparar : EmisorMensajes
    {
        
        int tipoConciliacion;
        string tipoConciliacionDescripcion;
        int secuencia;
        string columnaDestinoExt;
        string columnaDestinoInt;
        string status;


#region Constructores

        public ReferenciaAComparar(IMensajesImplementacion implementadorMensajes)
        {
            this.secuencia = 0;
            this.columnaDestinoExt="";
            this.columnaDestinoInt="";
            this.tipoConciliacion=0;
            this.status = "ACTIVO";
            this.implementadorMensajes = implementadorMensajes;
        }

        public ReferenciaAComparar(int tipoConciliacion, string tipoConciliaciondescripcion, int secuencia,string columnaDestinoExt,string columnaDestinoInt,string status, IMensajesImplementacion implementadorMensajes)
        {
            this.secuencia = secuencia;
            this.columnaDestinoExt=columnaDestinoExt;
            this.columnaDestinoInt=columnaDestinoInt;
            this.tipoConciliacionDescripcion = tipoConciliaciondescripcion;
            this.tipoConciliacion=tipoConciliacion;
            this.status = status;
            this.implementadorMensajes = implementadorMensajes;
        }

#endregion

#region Propiedades
        public int Secuencia
        {
            get { return secuencia; }
            set { secuencia = value; }
        }


                public string ColumnaDestinoExt
        {
            get { return columnaDestinoExt; }
            set { columnaDestinoExt  = value; }
        }


                public string ColumnaDestinoInt
        {
            get { return columnaDestinoInt; }
            set { columnaDestinoInt  = value; }
        }



                public int TipoConciliacion
        {
            get { return tipoConciliacion; }
            set { tipoConciliacion = value; }
        }

                public string TipoConciliacionDescripcion
                {
                    get { return tipoConciliacionDescripcion; }
                    set { tipoConciliacionDescripcion = value; }
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
                return App.CadenaConexion;
            }
        }

#endregion


        public abstract ReferenciaAComparar CrearObjeto();
        public abstract bool CambiarStatus( int tipoconciliacion, int secuencia);
        public abstract bool Guardar();
        public abstract bool Modificar();

    }


}
