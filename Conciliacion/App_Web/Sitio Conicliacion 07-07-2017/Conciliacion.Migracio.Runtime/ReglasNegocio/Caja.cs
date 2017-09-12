using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio
{
    public abstract class Caja : ObjetoBase
    {
        int id;
        string descripcion;
        string usuario;
        byte cobranza;
        byte tesoreria;


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
       
        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }
   

        public byte Cobranza
        {
            get { return cobranza; }
            set { cobranza = value; }
        }


        public byte Tesoreria
        {
            get { return tesoreria; }
            set { tesoreria = value; }
        }


        
    }
}
