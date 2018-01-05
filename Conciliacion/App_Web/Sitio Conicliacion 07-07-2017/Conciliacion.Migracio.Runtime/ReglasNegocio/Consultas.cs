using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.ReglasDeNegocio;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio
{
    public abstract class Consultas : EmisorMensajes
    {

        #region tipo Fuente

        public abstract TipoFuente ConsultaTipoFuentePorId(int id);

        #endregion

        #region Tipo Fuente de Informacion

        public abstract List<TipoFuenteInformacion> ObtieneListaTipoFuenteDeInformacion();
        public abstract TipoFuenteInformacion ObtieneTipoFuenteDeInformacionePorId(int id);

        #endregion

        #region Sucursal

        public abstract Sucursal ObtieneSucursalPorId(int id);
        public abstract List<Sucursal> ObtieneListaSucursalPorCorporativo(int corporativo);
        //public abstract List<Sucursal> ObtieneListaSucursal();

        #endregion

        #region TipoArchivo

        public abstract List<TipoArchivo> ObtieneListaTipoArchivo();
        public abstract int ObtieneTipoArchivoNumeroMaximo();
        public abstract TipoArchivo ObtieneTipoArchivoPorId(int id);

        #endregion


        #region FuenteInformacion

        public abstract int ObtieneFuenteInformacionNumeroMaximo(int idBanco, string idCuenta);
        public abstract FuenteInformacion ObtieneFuenteInformacionPorId(int idBanco, string idCuenta, int id);
        public abstract List<FuenteInformacion> ObtieneListaFuenteInformacionPorBancoCuenta(int banco, string cuenta);

        #endregion

        #region Corporativo

        public abstract Corporativo ObtieneCorporativoPorId(int id);
        public abstract List<Corporativo> ObtieneListaCorporativo(string usuario);

        #endregion

        #region Frecuencia

        public abstract Frecuencia ObtieneFrecuenciaPorId(int id);
        public abstract List<Frecuencia> ObtieneListaFrecuencia();
        #endregion


        #region ConceptoBancoGrupo

        public abstract ConceptoBancoGrupo ObtieneConceptoBancoGrupoPorId(int id);

        #endregion

        #region Caja

        public abstract Caja ObtieneCajaPorId(int id);

        #endregion

        #region StatusConcepto

        public abstract StatusConcepto ObtieneStatusConceptoPorId(int id);

        public abstract List<StatusConcepto> ConsultaStatusConcepto(Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionStatusConcepto id);

        public abstract List<ListaCombo> ObtieneListaTipoTransferencia ();

        //StatusConcepto por Grupo de Conciliación.
        public abstract List<StatusConcepto> ObtenieneStatusConceptosGrupoConciliacion(int configuracion, int grupoconciliacion);

        #endregion

        #region SatatusConciliacion

        public abstract StatusConciliacion ObtieneStatusConciliacionPorId(int id);

        #endregion

        #region ConceptoBanco

        public abstract ConceptoBanco ObtieneConceptoBancoPorId(int id);
        public abstract List<ConceptoBanco> ObtieneListaConceptoBanco();

        #endregion

        #region MotivoNoConciliado

        public abstract MotivoNoConciliado ObtieneMotivoNoConciliadoPorId(int id);

        #endregion

        #region Destino

        public abstract Destino ObtieneDestinoPorId(string tablaDestino, string columnaDestino);
        public abstract List<Destino> ObtieneDestinoPorTabla(string tablaDestino);
        public abstract List<string> ObtieneListaDiferentesTablasDestino();
        #endregion

        #region FuenteInformacionDetalle
        public abstract List<FuenteInformacionDetalle> ObtieneListaFuenteInoformacionDetallePorId(int idBanco, string idCuenta, int idFuenteInformacion);
        public abstract int ObtieneFuenteInformacionDetalleNumeroMaximo(int idBanco, string idCuenta, int idFuenteInformacion);
        #endregion

        #region TablaDestinoDetalle

        public abstract bool GuardaListaTablaDestinoDetalle(TablaDestino table);

        public abstract void ActualizarClientePago(TablaDestinoDetalle TablaDestinoDetalle);

        public abstract int ExisteClientePago(TablaDestinoDetalle tablaDestinoDetalle);

        #endregion

        #region TablaDestino

        public abstract int ObtieneTablaDestinoNumeroMaximo(int corporativo, int sucursal, int anio);
        public abstract int VerificarArchivo(int corporativo, int sucursal, int anio, string cuentaBancoFinanciero, int tipoFuenteInformacion, int frecuencia, DateTime fechaInicial, DateTime fFinal);
        #endregion

        #region Separador

        public abstract List<Separador> ObtieneListaSeparador();

        #endregion

        #region BancoFinanciero

        public abstract List<BancoFinanciero> ObtieneListaBancoFinanciero(int corporativo);

        #endregion

        #region CuentaFinanciero

        public abstract List<CuentaFinanciero> ObtieneListaCuentaFinancieroPorBanco(int corporativo, int idBanco);

        public abstract List<CuentaFinanciero> ConsultaCuentaExistenteFuenteInformacion(short idBanco, string idCuentaBancaria, short idFuenteInformacion);
        #endregion

        #region statusConciliacion

        public abstract StatusConciliacion ObtieneStatusConciliacionPorId(string id);
        public abstract List<StatusConciliacion> ObtieneListaStatusConciliacion();

        #endregion

        #region Etiqueta

        public abstract Etiqueta ObtieneEtiquetaPorId(int id);
        public abstract List<Etiqueta> ObtieneListaEtiqueta();
        public abstract List<Etiqueta> ObtieneListaEtiquetaStatusConcepto(int statusConcepto);
        public abstract List<Etiqueta> ObtieneListaEtiquetaBanco(int bancoFinanciero);
        public abstract int ObtieneNumeroMaximoEtiqueta();

        #endregion

        #region FuenteInformacionDetalleEtiqueta

        public abstract FuenteInformacionDetalleEtiqueta ObtieneFuenteInformacionDetalleEtiquetaPorId(string cuentaBancoFinanciero,
        int idBancoFinanciero,
        int idFuenteInformacion,
        int secuencia,
        int idEtiqueta);
        public abstract List<FuenteInformacionDetalleEtiqueta> ObtieneListaFuenteInformacionDetalleEtiqueta(string cuentaBancoFinanciero,
        int idBancoFinanciero,
        int idFuenteInformacion,
        int secuencia);

        public abstract int ObtieneMaximoFuenteInformacionDetalleEtiqueta(string cuentaBancoFinanciero,
       int idBancoFinanciero,
       int idFuenteInformacion);


        #endregion

        #region TipoDato


        public abstract List<TipoDato> ObtieneListaTipoDato();


        #endregion

        public abstract List<int> ObtieneListaAnios();


        public abstract Consultas CrearObjeto();

        public virtual string CadenaConexion
        {
            get
            {
                return App.CadenaConexion;
            }

        }

    }
}
