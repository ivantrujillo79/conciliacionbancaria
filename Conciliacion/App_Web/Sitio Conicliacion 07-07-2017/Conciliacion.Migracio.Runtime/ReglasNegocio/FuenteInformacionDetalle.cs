using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio
{
    public abstract class FuenteInformacionDetalle : ObjetoBase
    {

        int idFuenteInformacion;
        int secuencia;
        string columnaOrigen;
        bool esTipoFecha;

    
        
        string tablaDestino;
        string columnaDestino;
        int idConceptoBanco;

        string cuentaBancoFinanciero;
        int bancoFinanciero;


        public bool EsTipoFecha
        {
            get { return esTipoFecha; }
            set { esTipoFecha = value; }
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
      
        public string ColumnaOrigen
        {
            get { return columnaOrigen; }
            set { columnaOrigen = value; }
        }
      

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
      

        public int IdConceptoBanco
        {
            get { return idConceptoBanco; }
            set { idConceptoBanco = value; }
        }

        public string CuentaBancoFinanciero
        {
            get { return cuentaBancoFinanciero; }
            set { cuentaBancoFinanciero = value; }
        }


        public int BancoFinanciero
        {
            get { return bancoFinanciero; }
            set { bancoFinanciero = value; }
        }

        public FuenteInformacion FuenteInformacion
        {
            get
            {
                return App.Consultas.ObtieneFuenteInformacionPorId(this.BancoFinanciero,this.CuentaBancoFinanciero,this.IdFuenteInformacion);

            }
        }


        public Destino Destino
        {
            get
            {
                return App.Consultas.ObtieneDestinoPorId(this.TablaDestino, this.ColumnaDestino);
            }
        }


        public List<FuenteInformacionDetalleEtiqueta> Etiquetas
        {

            get 
            {
                return App.Consultas.ObtieneListaFuenteInformacionDetalleEtiqueta(this.cuentaBancoFinanciero, this.BancoFinanciero, this.IdFuenteInformacion, this.Secuencia);
            }
        }

    }
}
