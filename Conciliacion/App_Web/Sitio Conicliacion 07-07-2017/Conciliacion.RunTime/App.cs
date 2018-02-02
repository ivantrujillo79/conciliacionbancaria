using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Conciliacion.RunTime.ReglasDeNegocio;
using Conciliacion.RunTime.DatosSQL;

namespace Conciliacion.RunTime
{
    public enum TipoMensaje
    {
        window,
        web,
        consola
    }

    public enum TipoSeguridad : byte { SQL = 0, NT = 1 }

    public class App
    {
        private static IMensajesImplementacion implementadorMensajes;
        public static IMensajesImplementacion ImplementadorMensajes
        {
            get
            {
                if (implementadorMensajes == null)
                    implementadorMensajes = App.ImplementadorMensajesFactory();
                return implementadorMensajes;
            }
        }


        private static IMensajesImplementacion ImplementadorMensajesFactory()
        {
            if (System.Web.HttpContext.Current == null)
                return new MensajeImplemantacionForm();
            else
                return new MensajeImplementacionWeb();
        }
        private static IMensajesImplementacion ImplementadorMensajesFactory(TipoMensaje entorno)
        {
            switch (entorno)
            {
                case TipoMensaje.consola:
                    return new MensajeImplementacionConsola();
                case TipoMensaje.window:
                    return new MensajeImplemantacionForm();
                case TipoMensaje.web:
                    return new MensajeImplementacionWeb();
            }
            return null;
        }

        private static Consultas consultas;
        public static Consultas Consultas
        {
            get
            {
                if (consultas == null)
                    consultas = new ConsultaDatos(App.ImplementadorMensajes);
                return consultas;

            }
        }

        private static cConciliacion conciliacion;
        public static cConciliacion Conciliacion
        {
            get
            {
                if (conciliacion == null)
                    conciliacion = new ConciliacionDatos(App.ImplementadorMensajes);
                return conciliacion;

            }
        }

        private static DatosArchivo datosarchivo;
        public static DatosArchivo DatosArchivo
        {
            get
            {
                if (datosarchivo == null)
                    datosarchivo = new DatosArchivoDatos(App.ImplementadorMensajes);
                return datosarchivo;

            }
        }

        private static DatosArchivoDetalle datosarchivodetalle;
        public static DatosArchivoDetalle DatosArchivoDetalle
        {
            get
            {
                if (datosarchivodetalle == null)
                    datosarchivodetalle = new DatosArchivoDetalleDatos(App.ImplementadorMensajes);
                return datosarchivodetalle;

            }
        }

        private static cFuenteInformacion fuenteinformacion;
        public static cFuenteInformacion FuenteInformacion
        {
            get
            {
                if (fuenteinformacion == null)
                    fuenteinformacion = new FuenteInformacionDatos(App.ImplementadorMensajes);
                return fuenteinformacion;

            }
        }

        private static FacturasComplemento facturasComplemento;
        public static FacturasComplemento FacturasComplemento
        {
            get
            {
                if (facturasComplemento == null)
                    facturasComplemento = new FacturasComplementoDatos(App.ImplementadorMensajes);
                return facturasComplemento;

            }
        }

        private static ReferenciaConciliada referenciaconciliada;
        public static ReferenciaConciliada ReferenciaConciliada
        {
            get
            {
                if (referenciaconciliada == null)
                    referenciaconciliada = new ReferenciaConciliadaDatos(App.ImplementadorMensajes);
                return referenciaconciliada;

            }
        }

        private static ReferenciaNoConciliada referencianoconciliada;
        public static ReferenciaNoConciliada ReferenciaNoConciliada
        {
            get
            {
                if (referencianoconciliada == null)
                    referencianoconciliada = new ReferenciaNoConciliadaDatos(App.ImplementadorMensajes);
                return referencianoconciliada;

            }
        }


        private static ReferenciaConciliadaPedido referenciaconciliadapedido;
        public static ReferenciaConciliadaPedido ReferenciaConciliadaPedido
        {
            get
            {
                if (referenciaconciliadapedido == null)
                    referenciaconciliadapedido = new ReferenciaConciliadaPedidoDatos(App.ImplementadorMensajes);
                return referenciaconciliadapedido;

            }
        }

        private static ReferenciaNoConciliadaPedido referencianoconciliadapedido;
        public static ReferenciaNoConciliadaPedido ReferenciaNoConciliadaPedido
        {
            get
            {
                if (referencianoconciliadapedido == null)
                    referencianoconciliadapedido = new ReferenciaNoConciliadaPedidoDatos(App.ImplementadorMensajes);
                return referencianoconciliadapedido;

            }
        }

        private static GrupoConciliacionDiasDiferencia grupoconciliaciondias;
        public static GrupoConciliacionDiasDiferencia GrupoConciliacionDias(short grupoconciliacion)
        {
            {
                if (grupoconciliaciondias == null)
                    grupoconciliaciondias = new GrupoConciliacionDiasDiferenciaDatos(grupoconciliacion,App.ImplementadorMensajes);
                return grupoconciliaciondias;
            }
        }

        //Agregado
        private static TransferenciaBancarias transferenciabancarias;
        public static TransferenciaBancarias TransferenciaBancarias
        {
            get
            {
                if(transferenciabancarias==null)
                    transferenciabancarias=new TransferenciaBancariasDatos(App.ImplementadorMensajes);
                return transferenciabancarias;
            }

        }

        private static TransferenciaBancariasDetalle transferenciabancariasdetalle;
        public static TransferenciaBancariasDetalle TransferenciaBancariasDetalle
        {
            get
            {
                if (transferenciabancariasdetalle == null)
                    transferenciabancariasdetalle = new TransferenciaBancariasDetalleDatos(App.ImplementadorMensajes);
                return transferenciabancariasdetalle;
            }

        }
        private static TransferenciaBancariaOrigen transferenciabancariaorigen;
        public static TransferenciaBancariaOrigen TransferenciaBancariaOrigen
        {
            get
            {
                if (transferenciabancariaorigen == null)
                    transferenciabancariaorigen = new TransferenciaBancariaOrigenDatos(App.ImplementadorMensajes);
                return transferenciabancariaorigen;
            }

        }

        private static RelacionCobranza relacionCobranza;
        public static RelacionCobranza RelCobranza
        {
            get
            {
                if (relacionCobranza == null)
                {
                    relacionCobranza = new RelacionCobranzaDatos(App.ImplementadorMensajes);
                    //relacionCobranza.CadenaConexion = App.CadenaConexion;                   

                }
                return relacionCobranza;
            }
        }

        private static Cobranza cobranza;
        public static Cobranza Cobranza
        {
            get
            {
                if (cobranza == null)
                    cobranza = new CobranzaDatos(App.ImplementadorMensajes);
                return cobranza;
            }

        }

        private static PagoAnticipado pagoAnticipado;
        public static PagoAnticipado PagoAnticipado
        {
            get
            {
                if (pagoAnticipado == null)
                    pagoAnticipado = new PagoAnticipadoDatos(App.ImplementadorMensajes);
                return pagoAnticipado;
            }
        }

        private static PedidoCobranza pedidoCobranza;
        public static PedidoCobranza PedidoCobranza
        {
            get
            {
                if (pedidoCobranza == null)
                    pedidoCobranza = new PedidoCobranzaDatos(App.ImplementadorMensajes);
                return pedidoCobranza;
            }

        }

        private static Cliente cliente;
        public static Cliente Cliente
        {
            get
            {
                if (cliente == null)
                    cliente = new ClienteDatos(App.ImplementadorMensajes);
                return cliente;
            }

        }

        private static Pagare pagare;
        public static Pagare Pagare
        {
            get
            {
                if (pagare == null)
                    pagare = new PagareDatos(App.ImplementadorMensajes);
                return pagare;
            }
        }

        private static DetallePagare detallepagare;
        public static DetallePagare DetallePagare
        {
            get
            {
                if (detallepagare == null)
                    detallepagare = new DetallePagareDatos(App.ImplementadorMensajes);
                return detallepagare;
            }
        }

        private static SaldoAFavor saldoafavor;
        public static SaldoAFavor SaldoAFavor
        {
            get
            {
                if (saldoafavor == null)
                    saldoafavor = new SaldoAFavorDatos(App.ImplementadorMensajes);
                return saldoafavor;
            }
        }

        private static ConciliacionReferencia conciliacionrefencia;
        public static ConciliacionReferencia ConciliacionReferencia
        {
            get
            {
                if (conciliacionrefencia  == null)
                    conciliacionrefencia = new ConciliacionReferenciaDatos(App.ImplementadorMensajes);
                return conciliacionrefencia;
            }
        }

        private static string cadenaconexion;
       
        
        public static string CadenaConexion
        {
            get
            {
                return cadenaconexion;
            }
            set
            {
                cadenaconexion = value;
            }
        }

    }
}
