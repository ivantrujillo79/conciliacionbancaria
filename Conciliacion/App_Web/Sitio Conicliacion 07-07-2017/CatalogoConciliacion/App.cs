using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using CatalogoConciliacion.ReglasNegocio;
using CatalogoConciliacion.Datos;
using Conciliacion.RunTime;
using System.Reflection;
using System.IO;
using System.Web;

namespace CatalogoConciliacion
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
        private static TipoMovimientoCuenta tipoMovimientoCuenta;
        private static MotivoNoConciliado motivoNoConciliado;
        private static GrupoConciliacion grupoConciliacion;
        private static GrupoConciliacionUsuario grupoConciliacionUsuario;
        private static TipoConciliacionUsuario tipoConciliacionUsuario;
        private static ReferenciaAComparar referenciaAComparar;
        private static CuentaTransferencia referenciaCuentaTransferencia;
        private static ParametroAplicacion parametro;



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

        public static MotivoNoConciliado MotivoNoConciliado
        {
            get
            {
                if (motivoNoConciliado == null)
                    motivoNoConciliado = new MotivoNoConciliadoDatos(App.ImplementadorMensajes);
                return motivoNoConciliado;
            }
        }


        public static GrupoConciliacion GrupoConciliacion
        {
            get
            {
                if (grupoConciliacion == null)
                    grupoConciliacion = new GrupoConciliacionDatos(App.ImplementadorMensajes);
                return grupoConciliacion;
            }
        }


        public static GrupoConciliacionUsuario GrupoConciliacionUsuario
        {
            get
            {
                if (grupoConciliacionUsuario == null)
                    grupoConciliacionUsuario = new GrupoConciliacionUsuarioDatos(App.ImplementadorMensajes);
                return grupoConciliacionUsuario;
            }
        }

        public static TipoConciliacionUsuario TipoConciliacionUsuario
        {
            get
            {
                if (tipoConciliacionUsuario == null)
                    tipoConciliacionUsuario = new TipoConciliacionUsuarioDatos(App.ImplementadorMensajes);
                return tipoConciliacionUsuario;
            }
        }

        public static ReferenciaAComparar ReferenciaAComparar
        {
            get
            {
                if (referenciaAComparar == null)
                    referenciaAComparar = new ReferenciaACompararDatos(App.ImplementadorMensajes);
                return referenciaAComparar;
            }
        }

        public static TipoMovimientoCuenta TipoMovimientoCuenta
        {
            get
            {
                if (tipoMovimientoCuenta == null)
                    tipoMovimientoCuenta = new TipoMovimientoCuentaDatos(App.ImplementadorMensajes);
                return tipoMovimientoCuenta;
            }
        }

        //Agregada
        public static CuentaTransferencia ReferenciaCuentaTransferencia
        {
            get
            {
                if (referenciaCuentaTransferencia == null)
                    referenciaCuentaTransferencia = new CuentaTransferenciaDatos(App.ImplementadorMensajes);
                return referenciaCuentaTransferencia;
            }
        }

        public static ParametroAplicacion Parametro
        {
            get
            {
                if (parametro == null)
                    parametro = new ParametroAplicacionDatos(App.ImplementadorMensajes);
                return parametro;
            }
        }

        public static Consultas Consultas
        {
            get
            {
                if (consultas == null)
                    consultas = new ConsultasDatos();
                return consultas;

            }
        }      



        private static string cadenaconexion;
      

        public static string CadenaConexion
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
                    ConnectionString = "Application Name = Conciliación Bancaría" + " v.2.5.0.0" + "; Data Source = " + servidor + "; Initial Catalog = " +
                                        baseDatos + "; User ID = " + usuario.IdUsuario.Trim() + "; Integrated Security = Yes";
                else
                    ConnectionString = "Application Name = " + "; Data Source = " + servidor + "; Initial Catalog = " +
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
