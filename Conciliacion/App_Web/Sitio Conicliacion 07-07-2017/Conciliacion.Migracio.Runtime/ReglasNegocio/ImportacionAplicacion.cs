using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Data;
using Conciliacion.RunTime.ReglasDeNegocio;
using SeguridadCB;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio
{
    public abstract class ImportacionAplicacion : EmisorMensajes
    {
        TablaDestino tabladestino;

        String nombresp;

        String servidor;
        String basededatos;
        String usuarioconsulta;
        String pass;
        private int corporativo;
        private int sucursal;
        private int año;
        private string cuentabanco;
        private DateTime finicial;
        private DateTime ffinal;
        private DateTime falta;
        private string procedimiento;
        private string usuario;
        private string statusConciliacion;
        private int folio;
        private List<RunTime.ReglasDeNegocio.ImportacionAplicacion> listadoExtractores;

        public ImportacionAplicacion(int corporativo, int sucursal, int año, string cuentabanco, 
            int tipofuenteinformacion, DateTime finicial, DateTime ffinal, DateTime falta, 
            string procedimiento, string usuario, string statusConciliacion, int folio, string servidor,
            string basededatos, string usuarioconsulta, string pass)
        {
            tabladestino = new Conciliacion.Migracion.Runtime.SqlDatos.TablaDestinoDatos();
            TablaDestino.IdCorporativo = corporativo;
            TablaDestino.IdSucursal = sucursal;
            TablaDestino.Anio = año;
            TablaDestino.CuentaBancoFinanciero = cuentabanco;
            TablaDestino.IdTipoFuenteInformacion = tipofuenteinformacion;
            TablaDestino.IdFrecuencia = 1;
            TablaDestino.FInicial = finicial;
            TablaDestino.FFinal = ffinal;
            TablaDestino.FAlta = falta;
            TablaDestino.Usuario = usuario;
            TablaDestino.Folio = folio;
            TablaDestino.IdStatusConciliacion = statusConciliacion;
            this.NombreSp = procedimiento;

            this.Servidor = servidor;
            this.BaseDeDatos = basededatos;
            this.UsuarioConsulta = usuarioconsulta;
            this.Pass = pass;

            TablaDestino.Detalles = LlenarObjetosDestinoDestalle();
        }


        public ImportacionAplicacion(int corporativo, int sucursal, int año, string cuentabanco,
            DateTime finicial, DateTime ffinal, DateTime falta,
            string usuario, string statusConciliacion, int folio, string pass,
             List<RunTime.ReglasDeNegocio.ImportacionAplicacion> listadoExtractores)
           
       
        {
           

            tabladestino = new Conciliacion.Migracion.Runtime.SqlDatos.TablaDestinoDatos();
            TablaDestino.IdCorporativo = corporativo;
            TablaDestino.IdSucursal = sucursal;
            TablaDestino.Anio = año;
            TablaDestino.CuentaBancoFinanciero = cuentabanco;            
            TablaDestino.IdFrecuencia = 1;
            TablaDestino.FInicial = finicial;
            TablaDestino.FFinal = ffinal;
            TablaDestino.FAlta = falta;
            TablaDestino.Usuario = usuario;
            TablaDestino.Folio = folio;
            TablaDestino.IdStatusConciliacion = statusConciliacion;
            /**Aqui**/

            SeguridadCB.Seguridad seguridad = new SeguridadCB.Seguridad();
            foreach (RunTime.ReglasDeNegocio.ImportacionAplicacion extractor in listadoExtractores)
            {
                NombreSp = extractor.Procedimiento;
                Servidor = extractor.Servidor;
                BaseDeDatos = extractor.BaseDeDatos;
                UsuarioConsulta = extractor.UsuarioConsulta;
                Pass = seguridad.DesencriptaClave(extractor.Pass);
                TablaDestino.IdTipoFuenteInformacion = extractor.TipoFuenteInformacion; //extractor.Identificador;

                TablaDestino.Detalles.AddRange(LlenarObjetosDestinoDestalle());

            }            
        }


        #region propiedades

        public TablaDestino TablaDestino
        {
            get { return tabladestino; }
            set { tabladestino = value; }
        }

        public string NombreSp
        {
            get { return nombresp; }
            set { nombresp = value; }
        }

        public string Servidor
        {
            get { return servidor; }
            set { servidor = value; }
        }

        public string BaseDeDatos
        {
            get { return basededatos; }
            set { basededatos = value; }
        }

        public string UsuarioConsulta
        {
            get { return usuarioconsulta; }
            set { usuarioconsulta = value; }
        }

        public string Pass
        {
            get { return pass; }
            set { pass = value; }
        }

        #endregion

        public bool GuardaEnTablaDestinoDetalle()
        {
            if (TablaDestino != null && TablaDestino.Detalles.Count > 0)
                return App.Consultas.GuardaListaTablaDestinoDetalle(TablaDestino);
            else
                return false;
        }

        public abstract List<TablaDestinoDetalle> LlenarObjetosDestinoDestalle();
        
        public virtual string CadenaConexion
        {
            get
            {
                return "Data Source=" + this.Servidor + ";Initial Catalog=" + this.BaseDeDatos + ";user id = " + this.UsuarioConsulta+ "; password = " + this.Pass;
            }
        }

        public abstract bool PeriodoFechasOcupado(DateTime Finicio, DateTime FFinal, int Corporativo, int Sucursal, int Año, string CuentaBancoFinanciero,
            List<Conciliacion.RunTime.ReglasDeNegocio.ImportacionAplicacion> listadoExtractores);

    }
}
