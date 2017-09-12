using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio
{
    public abstract class TipoArchivo:ObjetoBase
    {
        int idTipoArchivo;
        string descripcion;
        string formatoFecha;
        string formatoMoneda;
        string separador;
        string usuario;
        string status;
        DateTime fAlta;

        public TipoArchivo()
        {
             this.idTipoArchivo = 0;
             this.descripcion = string.Empty;
             this.formatoFecha = string.Empty;
             this.formatoMoneda = string.Empty;
             this.separador = string.Empty;
             this.usuario = string.Empty;
             this.status = string.Empty;
          
        }



        //public abstract bool Guardar();
        //public abstract bool Actualizar();
        //public abstract bool Eliminar();
        //public abstract IObjetoBase CrearObjeto();



        public int IdTipoArchivo
        {
            get { return idTipoArchivo; }
            set { idTipoArchivo = value; }
        }
        
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        
        public string FormatoFecha
        {
            get { return formatoFecha; }
            set { formatoFecha = value; }
        }
        
        public string FormatoMoneda
        {
            get { return formatoMoneda; }
            set { formatoMoneda = value; }
        }
        
        public string Separador
        {
            get { return separador; }
            set { separador = value; }
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


        //public virtual string CadenaConexion
        //{
        //    get
        //    {
        //        return App.CadenaConexion;
        //    }

        //}

    }
}
