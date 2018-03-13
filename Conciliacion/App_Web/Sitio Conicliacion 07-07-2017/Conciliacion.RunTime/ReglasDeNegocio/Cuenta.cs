using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.ReglasDeNegocio
{

    [Serializable]

    public struct Cuenta
    {

        private int id;
        private string descripcion;

        public Cuenta(int id, string descripcion)
        {
            this.id = id;
            this.descripcion = descripcion;
        }

        #region Propiedades

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        #endregion

    }

}
