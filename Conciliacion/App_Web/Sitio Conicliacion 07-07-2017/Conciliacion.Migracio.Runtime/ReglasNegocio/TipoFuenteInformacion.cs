using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio
{
    public abstract  class TipoFuenteInformacion:ObjetoBase
    {

        int id;
        string descripcion;
        int idTipoFuente;




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
       

        public int IdTipoFuente
        {
            get { return idTipoFuente; }
            set { idTipoFuente = value; }
        }


        public TipoFuente TipoFuente
        {
            get
            {
                return App.Consultas.ConsultaTipoFuentePorId(this.IdTipoFuente);
            }

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
