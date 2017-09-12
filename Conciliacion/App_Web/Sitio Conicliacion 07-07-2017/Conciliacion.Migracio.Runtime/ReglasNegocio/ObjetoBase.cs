using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio
{
    public abstract class ObjetoBase :EmisorMensajes, Conciliacion.Migracion.Runtime.ReglasNegocio.IObjetoBase
    {
        

        public abstract bool Guardar();
        public abstract bool Actualizar();
        public abstract bool Eliminar();
        public abstract IObjetoBase CrearObjeto();







        public virtual string CadenaConexion
        {
            get
            {
                return App.CadenaConexion;
            }
        }

    

       
    }
}
