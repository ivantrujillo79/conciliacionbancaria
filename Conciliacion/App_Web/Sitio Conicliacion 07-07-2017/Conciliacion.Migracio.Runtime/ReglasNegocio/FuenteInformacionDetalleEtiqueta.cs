using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio
{
    public abstract class FuenteInformacionDetalleEtiqueta:ObjetoBase
    {
        string cuentaBancoFinanciero;
        int idBancoFinanciero;
        int idFuenteInformacion;
        int secuencia;
        int idEtiqueta;

        public string CuentaBancoFinanciero
        {
            get { return cuentaBancoFinanciero; }
            set { cuentaBancoFinanciero = value; }
        }


        public int IdBancoFinanciero
        {
            get { return idBancoFinanciero; }
            set { idBancoFinanciero = value; }
        }


        public int IdFuenteInformacion
        {
            get { return idFuenteInformacion; }
            set { idFuenteInformacion = value; }
        }
  

        public int Secuencia
        {
            get { return secuencia; }
            set { secuencia = value; }
        }
     

        public int IdEtiqueta
        {
            get { return idEtiqueta; }
            set { idEtiqueta = value; }
        }

        int longitudFija;

        public int LongitudFija
        {
            get { return longitudFija; }
            set { longitudFija = value; }
        }
        string finaliza;

        public string Finaliza
        {
            get { return finaliza; }
            set { finaliza = value; }
        }


        public Etiqueta Etiqueta
        {

            get
            {
                return App.Consultas.ObtieneEtiquetaPorId(this.IdEtiqueta);
            }
        }

    }
}
