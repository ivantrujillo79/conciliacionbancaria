using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio
{
    public abstract class StatusConciliacion:ObjetoBase
    {
        string  id;
        string usuario;
        string status;
        DateTime fAlta;

        //#region IObjetoBase Members
        //public virtual string CadenaConexion
        //{
        //    get
        //    {
        //        return App.CadenaConexion;
        //    }
        //}
        //public abstract bool Actualizar();
        //public abstract IObjetoBase CrearObjeto();
        //public abstract bool Eliminar();
        //public abstract bool Guardar();
        //#endregion

        public string  Id
        {
            get { return id; }
            set { id = value; }
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


    }
}
