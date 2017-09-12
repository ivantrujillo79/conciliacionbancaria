using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio
{
    public abstract class ConceptoBancoGrupo : ObjetoBase
    {

        int id;
        string descripcion;
        string status;
        string usuario;
        DateTime fAlta;


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
 

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
  

        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }


        public DateTime FAlta
        {
            get { return fAlta; }
            set { fAlta = value; }
        }

       // #region IObjetoBase Members
       // public virtual string CadenaConexion
       //{
       //    get
       //    {
       //        return App.CadenaConexion;
       //    }
       //}
       // public abstract  bool Actualizar();
       // public abstract IObjetoBase CrearObjeto();
       // public abstract  bool Eliminar();
       // public abstract bool Guardar();
       // #endregion
    }
}
