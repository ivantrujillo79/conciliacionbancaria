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
        private  TipoMovimientoCuenta tipoMovimientoCuenta;
        private  MotivoNoConciliado motivoNoConciliado;
        private  GrupoConciliacion grupoConciliacion;
        private  GrupoConciliacionUsuario grupoConciliacionUsuario;
        private  TipoConciliacionUsuario tipoConciliacionUsuario;
        private  ReferenciaAComparar referenciaAComparar;
        private  CuentaTransferencia referenciaCuentaTransferencia;
        private  ParametroAplicacion parametro;


        private MensajesImplementacion implementadorMensajes;
        public MensajesImplementacion ImplementadorMensajes
        {
            get
            {
                //if (implementadorMensajes == null)
                //    implementadorMensajes = App.ImplementadorMensajesFactory();
                //MensajesImplementacion implementadorMensajes = new MensajesImplementacion();
                return implementadorMensajes;
            }
        }

        //private  MensajesImplementacion implementadorMensajes;
        //public  MensajesImplementacion ImplementadorMensajes
        //{
        //    get
        //    {
        //        if (implementadorMensajes == null)
        //            implementadorMensajes = App.ImplementadorMensajesFactory();
        //        return implementadorMensajes;
        //    }
        //}


        //private  MensajesImplementacion ImplementadorMensajesFactory()
        //{
        //    if (System.Web.HttpContext.Current == null)
        //        return new MensajeImplemantacionForm();
        //    else
        //    return new MensajeImplementacionWeb();
        //}

        //private  MensajesImplementacion ImplementadorMensajesFactory(TipoMensaje entorno)
        //{
        //    switch (entorno)
        //    {
        //        case TipoMensaje.consola:
        //            return new MensajeImplementacionConsola();
        //        case TipoMensaje.window:
        //            return new MensajeImplemantacionForm();
        //        case TipoMensaje.web:
        //            return new MensajeImplementacionWeb();
        //    }
        //    return null;
        //}


        private Consultas consultas;

        public MotivoNoConciliado MotivoNoConciliado
        {
            get
            {
                if (motivoNoConciliado == null)
                    motivoNoConciliado = new MotivoNoConciliadoDatos(ImplementadorMensajes);
                return motivoNoConciliado;
            }
        }


        public  GrupoConciliacion GrupoConciliacion
        {
            get
            {
                if (grupoConciliacion == null)
                    grupoConciliacion = new GrupoConciliacionDatos(ImplementadorMensajes);
                return grupoConciliacion;
            }
        }


        public  GrupoConciliacionUsuario GrupoConciliacionUsuario
        {
            get
            {
                if (grupoConciliacionUsuario == null)
                    grupoConciliacionUsuario = new GrupoConciliacionUsuarioDatos(ImplementadorMensajes);
                return grupoConciliacionUsuario;
            }
        }

        public  TipoConciliacionUsuario TipoConciliacionUsuario
        {
            get
            {
                if (tipoConciliacionUsuario == null)
                    tipoConciliacionUsuario = new TipoConciliacionUsuarioDatos(ImplementadorMensajes);
                return tipoConciliacionUsuario;
            }
        }

        public  ReferenciaAComparar ReferenciaAComparar
        {
            get
            {
                if (referenciaAComparar == null)
                    referenciaAComparar = new ReferenciaACompararDatos(ImplementadorMensajes);
                return referenciaAComparar;
            }
        }

        public  TipoMovimientoCuenta TipoMovimientoCuenta
        {
            get
            {
                if (tipoMovimientoCuenta == null)
                    tipoMovimientoCuenta = new TipoMovimientoCuentaDatos(ImplementadorMensajes);
                return tipoMovimientoCuenta;
            }
        }

        //Agregada
        public  CuentaTransferencia ReferenciaCuentaTransferencia
        {
            get
            {
                if (referenciaCuentaTransferencia == null)
                    referenciaCuentaTransferencia = new CuentaTransferenciaDatos(ImplementadorMensajes);
                return referenciaCuentaTransferencia;
            }
        }

        public  ParametroAplicacion Parametro
        {
            get
            {
                if (parametro == null)
                    parametro = new ParametroAplicacionDatos(ImplementadorMensajes);
                return parametro;
            }
        }

        public  Consultas Consultas
        {
            get
            {
                if (consultas == null)
                    consultas = new ConsultasDatos();
                return consultas;

            }
        }      



        private  string cadenaconexion;
      

        public  string CadenaConexion
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
