using Conciliacion.Migracion.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatalogoConciliacion.ReglasNegocio
{
    public abstract class ColumnaDestino : EmisorMensajes
    {

        string dscColumnaDestino;

  
        public ColumnaDestino(IMensajesImplementacion implementadorMensajes)
    {
        this.implementadorMensajes = implementadorMensajes;
    }

    public ColumnaDestino(string dscColumnaDestino)
    {
        this.dscColumnaDestino = dscColumnaDestino;
    }


    public string DscColumnaDestino
    {
        get { return dscColumnaDestino; }
        set { dscColumnaDestino = value; }
    }

    public abstract ColumnaDestino CrearObjeto();

}
}
