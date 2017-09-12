using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio
{
    public abstract class ConceptoBanco :ObjetoBase
    {

        int id;
        string descripcion;
        int idConceptoBancoGrupo;
        string status;
        string usuario;
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

        public int IdConceptoBancoGrupo
        {
            get { return idConceptoBancoGrupo; }
            set { idConceptoBancoGrupo = value; }
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


        public ConceptoBancoGrupo ConceptoBancoGrupo
        {
            get
            {
                return App.Consultas.ObtieneConceptoBancoGrupoPorId(this.IdConceptoBancoGrupo);
            }
        }

    }
}
