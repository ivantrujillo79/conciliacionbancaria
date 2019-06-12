using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Conciliacion.RunTime.ReglasDeNegocio;
using Conciliacion.RunTime.DatosSQL;
using System.Web;

namespace Conciliacion.RunTime
{
    public enum TipoMensaje
    {
        window,
        web,
        consola
    }

    public enum TipoSeguridad : byte { SQL = 0, NT = 1 }

    //public class MensajesImplementacion
    //{
    //    public object ContenedorActual { get; set; }
    //    bool MensajesActivos { get; set; }

    //    public MensajesImplementacion() { }

    //    public void MostrarMensaje(string texto)
    //    {
    //    }
    //    public void MostrarMensajeError(string Mensaje)
    //    {
    //    }
    //    public void MostrarMensajeExito(string Mensaje)
    //    {
    //    }
    //}

    public class App
    {
        private MensajesImplementacion implementadorMensajes;
        public MensajesImplementacion ImplementadorMensajes
        {
            get
            {
                if (implementadorMensajes == null)
                    implementadorMensajes = new Conciliacion.RunTime.MensajesImplementacion();
                return implementadorMensajes;
            }
        }

        private Consultas consultas;
        public Consultas Consultas
        {
            get
            {
                if (consultas == null)
                    consultas = new ConsultaDatos(ImplementadorMensajes);
                return consultas;

            }
        }

        private   cConciliacion conciliacion;
        public   cConciliacion Conciliacion
        {
            get
            {
                if (conciliacion == null)
                    conciliacion = new ConciliacionDatos(ImplementadorMensajes);
                return conciliacion;

            }
        }

        private   DatosArchivo datosarchivo;
        public   DatosArchivo DatosArchivo
        {
            get
            {
                if (datosarchivo == null)
                    datosarchivo = new DatosArchivoDatos(ImplementadorMensajes);
                return datosarchivo;

            }
        }

        private   DatosArchivoDetalle datosarchivodetalle;
        public   DatosArchivoDetalle DatosArchivoDetalle
        {
            get
            {
                if (datosarchivodetalle == null)
                    datosarchivodetalle = new DatosArchivoDetalleDatos(ImplementadorMensajes);
                return datosarchivodetalle;

            }
        }

        private   cFuenteInformacion fuenteinformacion;
        public   cFuenteInformacion FuenteInformacion
        {
            get
            {
                if (fuenteinformacion == null)
                    fuenteinformacion = new FuenteInformacionDatos(ImplementadorMensajes);
                return fuenteinformacion;

            }
        }

        private   FacturasComplemento facturasComplemento;
        public   FacturasComplemento FacturasComplemento
        {
            get
            {
                if (facturasComplemento == null)
                    facturasComplemento = new FacturasComplementoDatos(ImplementadorMensajes);
                return facturasComplemento;

            }
        }

        private   ReferenciaConciliada referenciaconciliada;
        public   ReferenciaConciliada ReferenciaConciliada
        {
            get
            {
                if (referenciaconciliada == null)
                    referenciaconciliada = new ReferenciaConciliadaDatos(ImplementadorMensajes);
                return referenciaconciliada;

            }
        }

        private   ReferenciaNoConciliada referencianoconciliada;
        public   ReferenciaNoConciliada ReferenciaNoConciliada
        {
            get
            {
                if (referencianoconciliada == null)
                    referencianoconciliada = new ReferenciaNoConciliadaDatos(ImplementadorMensajes);
                return referencianoconciliada;

            }
        }


        private   ReferenciaConciliadaPedido referenciaconciliadapedido;
        public   ReferenciaConciliadaPedido ReferenciaConciliadaPedido
        {
            get
            {
                if (referenciaconciliadapedido == null)
                    referenciaconciliadapedido = new ReferenciaConciliadaPedidoDatos(ImplementadorMensajes);
                return referenciaconciliadapedido;

            }
        }

        private   ReferenciaNoConciliadaPedido referencianoconciliadapedido;
        public   ReferenciaNoConciliadaPedido ReferenciaNoConciliadaPedido
        {
            get
            {
                if (referencianoconciliadapedido == null)
                    referencianoconciliadapedido = new ReferenciaNoConciliadaPedidoDatos(ImplementadorMensajes);
                return referencianoconciliadapedido;

            }
        }

        private   GrupoConciliacionDiasDiferencia grupoconciliaciondias;
        public   GrupoConciliacionDiasDiferencia GrupoConciliacionDias(short grupoconciliacion)
        {
            {
                if (grupoconciliaciondias == null)
                    grupoconciliaciondias = new GrupoConciliacionDiasDiferenciaDatos(grupoconciliacion,ImplementadorMensajes);
                return grupoconciliaciondias;
            }
        }

        //Agregado
        private   TransferenciaBancarias transferenciabancarias;
        public   TransferenciaBancarias TransferenciaBancarias
        {
            get
            {
                if(transferenciabancarias==null)
                    transferenciabancarias=new TransferenciaBancariasDatos(ImplementadorMensajes);
                return transferenciabancarias;
            }

        }

        private   TransferenciaBancariasDetalle transferenciabancariasdetalle;
        public   TransferenciaBancariasDetalle TransferenciaBancariasDetalle
        {
            get
            {
                if (transferenciabancariasdetalle == null)
                    transferenciabancariasdetalle = new TransferenciaBancariasDetalleDatos(ImplementadorMensajes);
                return transferenciabancariasdetalle;
            }

        }
        private   TransferenciaBancariaOrigen transferenciabancariaorigen;
        public   TransferenciaBancariaOrigen TransferenciaBancariaOrigen
        {
            get
            {
                if (transferenciabancariaorigen == null)
                    transferenciabancariaorigen = new TransferenciaBancariaOrigenDatos(ImplementadorMensajes);
                return transferenciabancariaorigen;
            }

        }

        private   RelacionCobranza relacionCobranza;
        public   RelacionCobranza RelCobranza
        {
            get
            {
                if (relacionCobranza == null)
                {
                    relacionCobranza = new RelacionCobranzaDatos(ImplementadorMensajes);
                    //relacionCobranza.CadenaConexion = App.CadenaConexion;                   

                }
                return relacionCobranza;
            }
        }

        private   Cobranza cobranza;
        public   Cobranza Cobranza
        {
            get
            {
                if (cobranza == null)
                    cobranza = new CobranzaDatos(ImplementadorMensajes);
                return cobranza;
            }

        }

        private   PagoAnticipado pagoAnticipado;
        public   PagoAnticipado PagoAnticipado
        {
            get
            {
                if (pagoAnticipado == null)
                    pagoAnticipado = new PagoAnticipadoDatos(ImplementadorMensajes);
                return pagoAnticipado;
            }
        }

        private   PedidoCobranza pedidoCobranza;
        public   PedidoCobranza PedidoCobranza
        {
            get
            {
                if (pedidoCobranza == null)
                    pedidoCobranza = new PedidoCobranzaDatos(ImplementadorMensajes);
                return pedidoCobranza;
            }

        }

        private   Cliente cliente;
        public   Cliente Cliente
        {
            get
            {
                if (cliente == null)
                    cliente = new ClienteDatos(ImplementadorMensajes);
                return cliente;
            }

        }

        private   PagoAreasComunes pagoAreasComunes;
        public   PagoAreasComunes PagoAreasComunes
        {
            get
            {
                if (pagoAreasComunes == null)
                    pagoAreasComunes = new PagoAreasComunesDatos(ImplementadorMensajes);
                return pagoAreasComunes;
            }

        }

        private   Pagare pagare;
        public   Pagare Pagare
        {
            get
            {
                if (pagare == null)
                    pagare = new PagareDatos(ImplementadorMensajes);
                return pagare;
            }
        }

        private   DetallePagare detallepagare;
        public   DetallePagare DetallePagare
        {
            get
            {
                if (detallepagare == null)
                    detallepagare = new DetallePagareDatos(ImplementadorMensajes);
                return detallepagare;
            }
        }

        private   SaldoAFavor saldoafavor;
        public   SaldoAFavor SaldoAFavor
        {
            get
            {
                if (saldoafavor == null)
                    saldoafavor = new SaldoAFavorDatos(ImplementadorMensajes);
                return saldoafavor;
            }
        }

        private   ConciliacionReferencia conciliacionrefencia;
        public   ConciliacionReferencia ConciliacionReferencia
        {
            get
            {
                if (conciliacionrefencia  == null)
                    conciliacionrefencia = new ConciliacionReferenciaDatos(ImplementadorMensajes);
                return conciliacionrefencia;
            }
        }

        private   BusquedaClienteDatosBancarios busquedaclientedatosbancarios;
        public   BusquedaClienteDatosBancarios BusquedaClienteDatosBancarios
        {
            get
            {
                if (busquedaclientedatosbancarios == null)
                    busquedaclientedatosbancarios = new BusquedaClienteDatosBancariosDatos(ImplementadorMensajes);
                return busquedaclientedatosbancarios;
            }
        }

        private   string cadenaconexion;
       
        
        public   string CadenaConexion
        {
            get
            {
                SeguridadCB.Public.Usuario usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
                AppSettingsReader settings = new AppSettingsReader();
                string servidor = settings.GetValue("Servidor", typeof(string)).ToString();
                string baseDatos = settings.GetValue("Base", typeof(string)).ToString();
                SeguridadCB.Seguridad.TipoSeguridad seguridad;
                string ConnectionString = "";
                if (settings.GetValue("Seguridad", typeof(string)).ToString() == "NT")
                    seguridad = SeguridadCB.Seguridad.TipoSeguridad.NT;
                else
                    seguridad = SeguridadCB.Seguridad.TipoSeguridad.SQL;
                if (seguridad == SeguridadCB.Seguridad.TipoSeguridad.NT)
                    ConnectionString = "Application Name = Conciliacion Bancaria; Data Source=" + servidor + ";Initial Catalog=" + baseDatos + ";Integrated Security=SSPI;";
                else
                    ConnectionString = "Application Name = Conciliacion Bancaria; Data Source = " + servidor + "; Initial Catalog = " +
                                        baseDatos + "; User ID = " + usuario.IdUsuario.Trim() + "; Password = " + usuario.Clave;
                return ConnectionString;
            }
            set
            {
                cadenaconexion = value;
            }
        }

    }

}
