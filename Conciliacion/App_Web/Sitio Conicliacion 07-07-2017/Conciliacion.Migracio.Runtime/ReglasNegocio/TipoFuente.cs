using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio
{
   public abstract  class TipoFuente:ObjetoBase
    {
        int id;
        string descripcion;



        //public abstract bool Guardar();
        //public abstract bool Actualizar();
        //public abstract bool Eliminar();
        //public abstract IObjetoBase CrearObjeto();

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


        //public virtual string CadenaConexion
        //{
        //    get
        //    {
        //        return App.CadenaConexion;
        //    }

        //}


   
    }
}
