using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.DatosSQL;
using System.Data.SqlTypes;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class Consultas : EmisorMensajes
    {
        public Consultas(IMensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
        }

        public enum ConfiguracionGrupo
        {
            Asignados = 0,
            Todos = 1,
            NoAsignados = 2,
            ConAccesoTotal = 2
        }

        public enum ConfiguracionTipoFuente
        {
            TipoFuenteInformacion = 0,
            TipoFuente = 1,
            TipoFuenteInformacionExterno = 2,
            TipoFuenteInformacionInterno = 3
        }

        public enum Configuracion
        {
            Previo = 0,
            Todos = 1
        }

        public enum ConfiguracionIden0
        {
            Con0 = 0,
            Sin0 = 1
        }

        public enum FormaConciliacion
        {
            Cantidad = 1,
            CantidadyReferencia = 2,
            UnoaVarios = 3,
            Copia = 4,
            Manual = 5
        }

        public enum BusquedaConciliacion
        {
            Externo = 0,
            Interno = 1,
        }


        public enum ConsultaExterno
        {
            TodoInterno = 0,
            ConReferenciaInterno = 1,
            DepositosPedido = 2,
            DepositosConReferenciaPedido = 3
        }

        public enum BusquedaConciliacionPedido
        {
            Pedido = 0,
            Externo = 1,
        }

        public enum BusquedaPedido
        {
            Todos = 0,
            ConReferenciaMenores = 1,
            SinReferenciaMenores = 2,
            ConReferenciaTodos = 3
        }

        public enum ConciliacionInterna
        {
            SinReferencia = 0,
            ConReferencia = 1
        }

        public enum ConfiguracionStatusConcepto
        {
            Todos = 0,
            ConEtiquetas = 1
            //AfectanFlujoEfectivo = 2
        }


        public enum ConfiguracionConsultaConciliaciones
        {
            Interno = 0,
            Pedido = 1
        }

        public enum TipoTransferencia
        {
            EntradaSalida = 1,
            Salida = 2,
            Entrada=3
            
        }
        //ObtieneReferenciasPorConciliacion de seria la informacion de TablaDestinoDetalle que hace referencia en Conciliacion a FolioExterno que no esten en la tabla ConciliacionReferencia
        //public abstract List<ReferenciaNoConciliada> ObtieneReferenciasPorConciliacion(int corporativo, int sucursal, int años, short mes, int folio);

        public abstract DatosArchivo ObtieneArchivoExterno(int corporativo, int sucursal, int años, short mes, int folio);

        public abstract List<cConciliacion> ConsultaConciliacion(int corporativo, int sucursal, int grupoconciliacion, int año, short mes, short tipoconciliacion, string statusconciliacion, string usuario);

        public abstract List<ListaCombo> ConsultaAños();

        public abstract List<ListaCombo> ConsultaSucursales(ConfiguracionIden0 configuracion, int corporativo);

        public abstract List<ListaCombo> ConsultaGruposConciliacion(ConfiguracionGrupo configuracion, string usuario);

        public abstract List<ListaCombo> ConsultaTipoConciliacion(ConfiguracionGrupo configuracion, string usuario);

        public abstract List<ListaCombo> ConsultaStatusConciliacion();

        public abstract List<ListaCombo> ConsultaTipoInformacionDatos(ConfiguracionTipoFuente configuracion);

        public abstract List<ListaCombo> ConsultaBancos(int corporativo);

        public abstract List<ListaCombo> ConsultaCuentasBancaria(int corporativo, short banco);

        public abstract List<ListaCombo> ConsultaFoliosTablaDestino(int corporativo, int sucursal, int año, short mes, string cuentabancaria, short tipofuenteinformacion);

        public abstract List<ListaCombo> ConsultaDestinoExterno(short tipoconciliacion);

        public abstract List<ListaCombo> ConsultaDestinoInterno(short tipoconciliacion, string campoexterno);

        public abstract List<ListaCombo> ConsultaDestino();

        public abstract List<ListaCombo> ConsultaDestinoCompartidoExternoInterno(short configuracion);

        public abstract List<ListaCombo> ConsultaDestinoPedido();

        public abstract List<ListaCombo> ConsultaFormaConciliacion(short tipoconciliacion);

        public abstract List<ListaCombo> ConsultaMotivoNoConciliado();

        public abstract List<ListaCombo> ConsultaStatusConcepto(ConfiguracionStatusConcepto configuracion);

        public abstract List<ListaCombo> ConsultaCelula(int corporativo);

        public abstract List<ImportacionAplicacion> ConsultaImportacionAplicacion(int sucursal);


        public abstract List<DatosArchivo> ConsultaArchivosNoReferenciados(int corporativo, int sucursal, int año, short mes, short tipofuenteinformacion);

        public abstract List<cFuenteInformacion> ConsultaFuenteInformacion(int corporativo, int sucursal, string cuentabancaria, short tipofuenteinformacion);

        public abstract List<DatosArchivoDetalle> ConsultaTablaDestinoDetalle(Configuracion configuracion, int corporativo, int sucursal, int año, int folio);

        public abstract List<ReferenciaConciliada> ConsultaConciliarArchivosCantidad(int corporativo, int sucursal, int año, short mes, int folio, short dias, decimal centavos, int statusconcepto);

        public abstract List<ReferenciaConciliada> ConsultaConciliarCopiar(int corporativo, int sucursal, int año, short mes, int folio);

        public abstract List<ReferenciaConciliada> ConsultaConciliarPorReferencia(int corporativo, int sucursal, int año, short mes, int folio, short dias, decimal centavos, string campoexterno, string campointerno, int statusconcepto);

        public abstract List<ReferenciaNoConciliada> ConsultaDetalleExternoPendiente(ConsultaExterno configuracion, int corporativo, int sucursal, int año, short mes, int folio, decimal diferencia, int statusconcepto);


        public abstract List<ReferenciaNoConciliada> ConsultaDetalleInternoPendiente(ConciliacionInterna configuracion, int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion, int folioexterno, int secuenciaexterno, int sucursalinterno, short dias, decimal diferencia, int statusconcepto);


        public abstract List<ReferenciaNoConciliada> ConciliacionBusqueda(BusquedaConciliacion configuracion, int corporativo, int sucursal, int año, short mes, int folio, String campo, string operacion, string valor, string tipocampo, decimal diferencia);

        public abstract String ObtenerCadenaDeEtiquetas(short configuracion, int corporativo, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion, int statusconcepto);



        public abstract List<ReferenciaConciliadaPedido> ConsultaConciliarPedidoCantidadReferencia(int corporativo, int sucursal, int año, short mes, int folio, decimal centavos, int statusconcepto, string campoexterno, string campopedido);

        public abstract List<ReferenciaConciliadaPedido> ConsultaConciliarPedidoCantidad(int corporativo, int sucursal, int año, short mes, int folio, decimal centavos, int statusconcepto);

        //public abstract List<ReferenciaNoConciliadaPedido> ConsultaDetallePedidoPendiente(ConfiguracionPedido configuracion,  int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion, int folioexterno, int secuenciaexterno, int sucursalinterno);

        //public abstract List<ReferenciaNoConciliadaPedido> ConsultaDetalleExternoPendientePedido(ConsultaExterno configuracion, int corporativo, int sucursal, int año, short mes, int folio, decimal diferencia);

        public abstract List<ReferenciaNoConciliadaPedido> ConciliacionBusquedaPedido(BusquedaPedido configuracion, int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion, int folioexterno, int secuenciaexterno, decimal diferencia, int celula, string cliente, bool clientepadre);
        public abstract List<ReferenciaNoConciliadaPedido> ConciliacionBusquedaPedido(BusquedaPedido configuracion, int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion, int folioexterno, int secuenciaexterno, decimal diferencia, int celula, string cliente, bool clientepadre, SqlString factura, DateTime ffactura);
        public abstract List<ReferenciaNoConciliadaPedido> ConciliacionBusquedaFacturaManual(string cliente, bool clientepadre, SqlString factura, DateTime ffactura);

        public abstract bool ValidaPedidoEspecifico(int corporativo, int sucursal,
            string pedidoReferencia);

        public abstract ReferenciaNoConciliadaPedido ConsultaPedidoReferenciaEspecifico(
            int corporativoconciliacion,
            int sucursalconciliacion,
            int añoconciliacion,
            short mesconciliacion,
            int folioconciliacion,
            decimal diferencia,
            string pedidoReferencia);

        public abstract List<ReferenciaNoConciliadaPedido> ConciliacionBusquedaPedidoVariosUno(BusquedaPedido configuracion, int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion, int folioexterno, int secuenciaexterno, decimal diferencia, int celula);

        public abstract List<ReferenciaNoConciliadaPedido> ConciliacionBusquedaPedidoManual(BusquedaPedido configuracion, int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion, int folioexterno, int secuenciaexterno, decimal diferencia, int celula);
        //public abstract List<ReferenciaNoConciliadaPedido> ConciliacionBusquedaExternoPedido(int corporativo, int sucursal, int año, short mes, int folio, String campo, string operacion, string valor, string tipocampo, decimal diferencia);       



        public abstract List<ReferenciaNoConciliada> ConsultaTransaccionesConciliadas(int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion, int formaconciliacion);
        public abstract List<cReferencia> ConsultaTransaccionesConciliadasDetalle(int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion, int folioexterno, int secuenciaexterno, short cointerno, int añoexterno);


        public abstract cConciliacion ConsultaConciliacionDetalle(int corporativo, int sucursal, int año, short mes, int folioconciliacion);



        public abstract List<ReferenciaNoConciliada> ConsultaDetalleInternoCanceladoPendiente(ConciliacionInterna configuracion, int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion, int folioexterno, int secuenciaexterno, decimal diferencia);

        public abstract List<ReferenciaNoConciliada> ConsultaTransaccionesRegistradas(ConfiguracionConsultaConciliaciones configuracion, int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion, int statusconcepto, short formaconciliacion);

        public abstract List<ReferenciaNoConciliada> ConsultaDetalleExternoCanceladoPendiente(ConsultaExterno configuracion, int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion, int sucursalinterno, int foliointerno, int secuenciainterno, int statusconcepto, decimal diferencia);

        public abstract List<ReferenciaNoConciliada> ConsultaTrasaccionesExternasPendientes(int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion, int statusconcepto);

        public abstract List<ReferenciaNoConciliada> ConsultaTrasaccionesInternasPendientes(Configuracion configuracion, int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion, int statusconcepto, int sucursalinterno);


        public abstract List<ReferenciaConciliadaPedido> ConsultaPagosPorAplicar(int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion);

        public abstract List<ReferenciaConciliadaPedido> ConsultaPagosPorAplicarCliente(int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion, int cliente,
                                                                                        int corporativoexterno, int sucursalexterno, int añoexterno, int folioexterno, int secuenciaexterno);


        public abstract MovimientoCaja ConsultaMovimientoCajaAlta(int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion);

        public abstract List<Cobro> ConsultaChequeTarjetaAltaModifica(int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion);


        public abstract string ConsultaFechaActualInicial();

        public abstract string ConsultaFechaPermitidoConciliacion(string numMesesAnterior, string fechaMaxActual);

        public abstract bool BorrarTransaccionesNoCorrespondidas(int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion);

        public abstract List<ReferenciaNoConciliada> ConsultaDetalleExternoPendienteDeposito(ConsultaExterno configuracion, int corporativo, int sucursal, int año, short mes, int folio, decimal diferencia, int statusconcepto, bool deposito);

        public abstract List<ReferenciaNoConciliada> ConsultaDetalleInternoPendienteDeposito(ConciliacionInterna configuracion, int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion, int sucursalinterno, short dias, decimal diferencia, int statusconcepto, decimal monto, bool deposito, DateTime fminima, DateTime fmaxima);

        //Lista Nueva
        public abstract List<TransferenciaBancarias> ObtenieneTransferenciasBancarias(short corporativoOrigen, int sucursalOrigen,
                                                                                      string cuentabancoOrigen, int año, short mes, string status);

        //Codigo agregado
        public abstract List<ListaCombo> ConsultaCorporativoTransferencia(int configuracion, int corporativo, int sucursal, int banco, string cuentabanco);
        public abstract List<ListaCombo> ConsultaSucursalTransferencia(int configuracion, int corporativo, int sucursal, int banco, string cuentabanco, int cbocorporativo);
        public abstract List<ListaCombo> ConsultaNombreBancoTransferencia(int configuracion, int corporativo, int sucursal, int banco, string cuentabanco, int cbocorporativo, int cbosucursal);
        public abstract List<ListaCombo> ConsultaCuentaBancoTransferencia(int configuracion, int corporativo, int sucursal, int banco, string cuentabanco, int cbocorporativo, int cbosucursal, int cbobanco);


        ////ConsultarPedidosCorrespondientes por Movimiento Externo
        //public abstract List<ReferenciaConciliadaPedido> ConciliarPedidoCantidadYReferenciaMovExterno(
        //   int corporativo, int sucursal, int año, short mes, int folio,
        //   int corporativoEx,int sucursalEx,int añoEx,int folioEx,int secuenciaEx,
        //   decimal centavos, short statusconcepto, string campoexterno, string campopedido);


        //public abstract List<ReferenciaConciliadaCompartida> ConsultaMovimientosConciliacionCompartida(bool accesoTotal, int corporativo, int sucursal,
        //string cuentaBancaria, DateTime finicial, DateTime ffinal);
        public abstract Boolean ObtieneExternosTransferencia(short corporativoTD, short sucursalTD,
                                                                                     int añoTD, int folioTD, int secuenciaTD);
        public abstract List<ReferenciaNoConciliada> ConsultaMovimientosConciliacionCompartida(bool accesoTotal, int corporativo, int sucursal,
        string cuentaBancaria, DateTime finicial, DateTime ffinal);

        public abstract List<ReferenciaConciliadaCompartida> ConsultaMovimientosConciliadosMovExterno(int corporativoconciliacion, int sucursalconciliacion,
            int añoconciliacion, short mesconciliacion, int folioconciliacion, int corporativo, int sucursal, int año, int folio, int secuencia);
        public abstract List<FlujoProyectado> ConsultaFlujoEfectivo(int corporativo, int sucursal, TipoTransferencia tipotransferencia, DateTime fInicial,
            DateTime fFinal);

        public abstract decimal CalculaImporteRealFlujoEfectivo(int corporativo, short mes, DateTime fconsulta,
            int statusconcepto);

        //public abstract bool AccesoTotalFlujoEfectivo(string usuario);
        public abstract bool ValidarCierreMes(short config, int corporativo, int sucursal, int año, short mes, string usuariocierre);
        public abstract bool CierreMesConciliacion(short config, int corporativo, int sucursal, int año, short mes,string usuariocierre);
        

        public abstract ListaCombo ConsultaDatosCliente(int cliente);
        public abstract bool ClienteValido(string cliente);

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
