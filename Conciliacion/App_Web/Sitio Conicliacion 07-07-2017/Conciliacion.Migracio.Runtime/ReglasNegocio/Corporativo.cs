using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio
{
    public abstract class Corporativo : ObjetoBase
    {
        int id;
        string nombre;
        string inicial;


        //public abstract bool Guardar();
        //public abstract bool Actualizar();
        //public abstract bool Eliminar();
        //public abstract IObjetoBase CrearObjeto();

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
      

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
       

        public string Inicial
        {
            get { return inicial; }
            set { inicial = value; }
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
