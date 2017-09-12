using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.Migracion.Runtime.ReglasNegocio;
using Conciliacion.Migracion.Runtime.SqlDatos;
using System.Configuration;

namespace Conciliacion.Migracion.Runtime
{
    public class App
    {

        public static IMensajesImplementacion implementadorMensajes;
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

        static FuenteInformacionDetalleEtiqueta fuenteInformacionDetalleEtiqueta;
        public static FuenteInformacionDetalleEtiqueta FuenteInformacionDetalleEtiqueta
        {

            get
            {
                if (fuenteInformacionDetalleEtiqueta == null)
                    fuenteInformacionDetalleEtiqueta = new FuenteInformacionDetalleEtiquetaDatos();
                return fuenteInformacionDetalleEtiqueta;
            }
        }



        static Etiqueta etiqueta;
        public static Etiqueta Etiqueta
        {

            get
            {
                if (etiqueta == null)
                    etiqueta = new EtiquetaDatos();
                return etiqueta;
            }
        }


        static Consultas consultas;
        public static Consultas Consultas
        {

            get
            {
                if (consultas == null)
                    consultas = new ConsultasDatos();
                return consultas;
            }
        }


        static FuenteInformacionDetalle fuenteInformacionDetalle;
        public static FuenteInformacionDetalle FuenteInformacionDetalle
        {

            get
            {
                if (fuenteInformacionDetalle == null)
                    fuenteInformacionDetalle = new FuenteInformacionDetalleDatos();
                return fuenteInformacionDetalle;
            }
        }


        static FuenteInformacion fuenteInformacion;
        public static FuenteInformacion FuenteInformacion
        {

            get
            {
                if (fuenteInformacion == null)
                    fuenteInformacion = new FuenteInformacionDatos();
                return fuenteInformacion;
            }
        }

        static Destino destino;
        public static Destino Destino
        {

            get
            {
                if (destino == null)
                    destino = new DestinoDatos();
                return destino;
            }
        }


        static TablaDestinoDetalle tablaDestinoDetalle;
        public static TablaDestinoDetalle TablaDestinoDetalle
        {

            get
            {
                if (tablaDestinoDetalle == null)
                    tablaDestinoDetalle = new TablaDestinoDetalleDatos();
                return tablaDestinoDetalle;
            }
        }


        static Separador separador;
        public static Separador Separador
        {
            get
            {
                if (separador == null)
                    separador = new SeparadorDatos();
                return separador;
            }
        }


        static TipoArchivo tipoArchivo;
        public static TipoArchivo TipoArchivo
        {
            get
            {
                if (tipoArchivo == null)
                    tipoArchivo = new TipoArchivoDatos();
                return tipoArchivo;
            }
        }


        static string currentUser;
        public static string CurrentUser
        {
            get
            {
                return currentUser;
            }
            set
            {
                currentUser = value;
            }
        }


        private static TablaDestino tablaDestino;
        public static TablaDestino TablaDestino
        {
            get
            {
                if (tablaDestino == null)
                    tablaDestino = new TablaDestinoDatos();
                return tablaDestino;
            }
        }


        private static ConceptoBanco conceptoBanco;
        public static ConceptoBanco ConceptoBanco
        {
            get
            {
                if (conceptoBanco == null)
                    conceptoBanco = new ConceptoBancoDatos();
                return conceptoBanco;
            }
        }


        static string currentSucursal;
        public static string CurrentSucursal
        {
            get
            {
                return currentSucursal;
            }
            set
            {
                currentSucursal = value;
            }
        }

        private static StatusConcepto statusConcepto;
        public static StatusConcepto StatusConcepto
        {
            get
            {
                if (statusConcepto == null)
                    statusConcepto = new StatusConceptoDatos(implementadorMensajes);
                return statusConcepto;
            }
        }

        private static string usuarioActual;
        public static string UsuarioActual
        {
            get { return usuarioActual; }
            set { usuarioActual = value; }
        }

        private static ImportacionAplicacion importacionaplicacion;
        public static ImportacionAplicacion ImportacionAplicacion(int corporativo, int sucursal, int año, string cuentabanco, int tipofuenteinformacion, DateTime finicial, DateTime ffinal, DateTime factual,
            string procedimiento, string usuario, string statusConciliacion, int folio, string servidor, string basededatos, string usuarioconsulta, string pass)
        {
            {
                //if (importacionaplicacion == null)
                importacionaplicacion = new ImportacionAplicacionDatos(corporativo, sucursal, año, cuentabanco, tipofuenteinformacion, finicial, ffinal, factual, procedimiento, usuario, statusConciliacion, folio, servidor, basededatos, usuarioconsulta, pass);
                return importacionaplicacion;
            }
        }


        private static string connectionString;
        public static string CadenaConexion
        {

            //get
            //{

            //    AppSettingsReader settings = new AppSettingsReader();
            //    if (connectionString == null)
            //        connectionString = "data source=" + settings.GetValue("Servidor", typeof(string)).ToString() + "; initial catalog=" + settings.GetValue("Base", typeof(string)).ToString() + "; User ID = " + App.usuarioActual + " ;Integrated Security = Yes";
            //        //connectionString = @"Data Source=25.101.58.101\SQLSERVER2008;Initial Catalog=SigametFlamaAzul;user id = sa; password = 123";
            //    return connectionString;
            //    //return @"Data Source=HMONTER_\SQLEXPRESS;Initial Catalog=Prueba;Integrated Security = yes;";
            //    //return @"Data Source=25.101.58.101\SQLSERVER2008;Initial Catalog=SigametFlamaAzul;user id = sa; password = 123";
            //}

            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
            }
        }



    }
}
