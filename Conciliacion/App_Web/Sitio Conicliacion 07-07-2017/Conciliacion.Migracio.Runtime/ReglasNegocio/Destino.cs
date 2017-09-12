using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio
{
    public abstract class Destino : ObjetoBase
    {
        string tablaDestino;
        string columnaDestino;
        TipoDato tipoDato;
        string tablaCabecera;
        string idTipoDato;

        public string IdTipoDato
        {
            get { return idTipoDato; }
            set { idTipoDato = value; }
        }

        //public abstract bool Guardar();
        //public abstract bool Actualizar();
        //public abstract bool Eliminar();
        //public abstract IObjetoBase CrearObjeto();



        public string TablaDestino
        {
            get { return tablaDestino; }
            set { tablaDestino = value; }
        }
    

        public string ColumnaDestino
        {
            get { return columnaDestino; }
            set { columnaDestino = value; }
        }
     

        public TipoDato TipoDato
        {
            get { return tipoDato; }
            set { tipoDato = value; }
        }
       

        public string TablaCabecera
        {
            get { return tablaCabecera; }
            set { tablaCabecera = value; }
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
