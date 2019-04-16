using Conciliacion.Migracion.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatalogoConciliacion.ReglasNegocio
{
    public abstract class PalabrasClave : EmisorMensajes
    {

     int banco;
    string cuentaBanco;
    int tipocobro;
    string palabraClave;
    string columnadestino;


    public PalabrasClave(IMensajesImplementacion implementadorMensajes)
    {
        this.implementadorMensajes = implementadorMensajes;
    }

    public PalabrasClave(int banco, string cuentaBanco,int tipocobro,string palabraClave, string columnadestino)
    {
        this.banco = banco;
        this.cuentaBanco = cuentaBanco;
        this.tipocobro = tipocobro;
        this.palabraClave = palabraClave;
        this.columnadestino = columnadestino;



    }


    public int Banco
    {
        get { return banco; }
        set { banco = value; }
    }

        public string CuentaBanco
        {
            get { return cuentaBanco; }
            set { cuentaBanco = value; }
        }


        public int TipoCobro
        {
            get { return tipocobro; }
            set { tipocobro = value; }
        }


        public string PalabraClave
        {
            get { return palabraClave; }
            set { palabraClave = value; }
        }


        public string ColumnaDestino
        {
            get { return columnadestino; }
            set { columnadestino = value; }
        }


        public abstract PalabrasClave CrearObjeto();
        public abstract bool Guardar();
        
    }
}
