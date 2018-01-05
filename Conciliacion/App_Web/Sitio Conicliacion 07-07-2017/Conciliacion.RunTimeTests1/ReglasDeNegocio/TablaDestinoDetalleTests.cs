using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Conciliacion.Migracion.Runtime.ReglasNegocio;
using System.Configuration;
using System.Data.SqlClient;

namespace Conciliacion.Migracion.Runtime.Test
{
    [TestClass]
    public class TablaDestinoDetalleTests
    {
        string servidor = string.Empty, baseDatos = string.Empty, modulo = string.Empty;
        SeguridadCB.Seguridad.TipoSeguridad seguridad = SeguridadCB.Seguridad.TipoSeguridad.SQL;
        SqlConnection cn = new SqlConnection();

        private void ConfiguraConexion()
        {
            Conciliacion.Migracion.Runtime.App.UsuarioActual = "ROPIMA";
            AppSettingsReader settings = new AppSettingsReader();
            this.servidor = "192.168.1.38";
            this.baseDatos = "SigametReportes";
            this.seguridad = SeguridadCB.Seguridad.TipoSeguridad.SQL;
            cn.ConnectionString = "Application Name = " + "; Data Source = " + this.servidor + "; Initial Catalog = " +
            this.baseDatos + "; User ID = ROPIMA; Password = ROPIMA9999";
            Conciliacion.Migracion.Runtime.App.CadenaConexion = cn.ConnectionString;
        }

        [TestMethod]
        public void TestMethod_ActualizarClientePago_Exito()
        {
            ConfiguraConexion();

            TablaDestinoDetalle TablaDestinoDetalle = (TablaDestinoDetalle)App.TablaDestinoDetalle.CrearObjeto();
            TablaDestinoDetalle.IdCorporativo = 1;
            TablaDestinoDetalle.IdSucursal = 1;
            TablaDestinoDetalle.Anio = 2013;
            TablaDestinoDetalle.Folio = 72;
            TablaDestinoDetalle.Secuencia = 2;

            TablaDestinoDetalle.ClientePago = 1;

            TablaDestinoDetalle.ActualizarClientePago();

            int Existe = TablaDestinoDetalle.ExisteClientePago();
            if (Existe > 0)
            {
                if (Existe > 1)
                {
                    //Prueba fallida. Se encontro mas de un registro
                    Assert.Fail();
                }
            }
            else
            {
                //Prueba fallida. No se actualizo/encontro el registro en la base de datos
                Assert.Fail();
            }

        }

        [TestMethod]
        public void TestMethod_ActualizarClientePago_Fracaso()
        {
            ConfiguraConexion();

            TablaDestinoDetalle TablaDestinoDetalle = (TablaDestinoDetalle)App.TablaDestinoDetalle.CrearObjeto();
            TablaDestinoDetalle.IdCorporativo = 1;
            TablaDestinoDetalle.IdSucursal = 1;
            TablaDestinoDetalle.Anio = 2013;
            TablaDestinoDetalle.Folio = 999999999;
            TablaDestinoDetalle.Secuencia = 2;

            TablaDestinoDetalle.ClientePago = 1;

            TablaDestinoDetalle.ActualizarClientePago();

            int Existe = TablaDestinoDetalle.ExisteClientePago();
            if (Existe >= 0)
            {
                //throw new Exception("Prueba fallida. No debe actualizar ningun registro.");
                Assert.Fail();
            }

        }

    }
}
