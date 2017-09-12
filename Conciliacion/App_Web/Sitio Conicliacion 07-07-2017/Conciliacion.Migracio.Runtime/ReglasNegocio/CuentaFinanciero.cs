using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio
{
    public abstract class CuentaFinanciero : ObjetoBase
    {

        
            int id;
            string descripcion;



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


        
    }
}
