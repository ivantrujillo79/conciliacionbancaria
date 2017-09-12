using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio
{
    public abstract class Separador:ObjetoBase
    {
        string descripcion;
        string status;

        
        
        //public abstract bool Guardar();
        //public abstract bool Actualizar();
        //public abstract bool Eliminar();
        //public abstract IObjetoBase CrearObjeto();


        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        

        public string Status
        {
            get { return status; }
            set { status = value; }
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
