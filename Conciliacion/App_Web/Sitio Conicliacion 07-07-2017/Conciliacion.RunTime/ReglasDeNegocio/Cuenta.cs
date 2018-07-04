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
        private int banco; //MCC 26-04-2018
        private string nombreBanco; //MCC 26-04-2018
        private string cuentaBanco;

        public Cuenta(int id, string descripcion, int banco, string nombreBanco, string cuentaBanco)
        {
            this.id = id;
            this.descripcion = descripcion;
            this.banco = banco;
            this.nombreBanco = nombreBanco;
            this.cuentaBanco = cuentaBanco;
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


        public int Banco
        {
            get { return banco; }
            set { banco = value; }
        }

        public string NombreBanco
        {
            get { return nombreBanco; }
            set { nombreBanco = value; }
        }

        public string CuentaBanco
        {
            get { return cuentaBanco; }
            set { cuentaBanco = value; }
        }

        #endregion

    }

}
