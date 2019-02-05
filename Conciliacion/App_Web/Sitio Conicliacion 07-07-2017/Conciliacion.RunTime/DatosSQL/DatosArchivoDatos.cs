using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.ReglasDeNegocio;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Conciliacion.RunTime.DatosSQL
{
    public class DatosArchivoDatos : DatosArchivo
    {
        public DatosArchivoDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {

        }

        public DatosArchivoDatos(int corporativo, int sucursal, int año, int folio, string cuentabanco, DateTime finicial, DateTime ffinal, short tipofuenteinformacion, string tipofuenteinformaciondes, string statusconciliacion, short tipofuente, string tipofuentedes, int sucursalconciliacion, int folioconciliacion, short mesconciliacion, IMensajesImplementacion implementadorMensajes)
            : base(corporativo, sucursal, año, folio, cuentabanco, finicial, ffinal, tipofuenteinformacion, tipofuenteinformaciondes, statusconciliacion, tipofuente, tipofuentedes, sucursalconciliacion, folioconciliacion, mesconciliacion, implementadorMensajes)
        {

        }

        public override bool GuardarArchivoInterno()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaConciliacionArchivo", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.SmallInt).Value = 0;
                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = this.Corporativo;
                    comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = this.SucursalConciliacion;
                    comando.Parameters.Add("@AñoConciliacion", System.Data.SqlDbType.Int).Value = this.Año;
                    comando.Parameters.Add("@MesConciliacion", System.Data.SqlDbType.SmallInt).Value = this.MesConciliacion;
                    comando.Parameters.Add("@FolioConciliacion ", System.Data.SqlDbType.Int).Value = this.FolioConciliacion;
                    comando.Parameters.Add("@SucursalInterno", System.Data.SqlDbType.Int).Value = this.Sucursal;
                    comando.Parameters.Add("@FolioInterno ", System.Data.SqlDbType.Int).Value = this.Folio;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.ExecuteNonQuery();
                    resultado = true;
                }

            }
            catch (SqlException ex)
            {
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("No se pudo guardar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return resultado;
        }

        public override bool ExisteArchivoInternoConciliacion()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaConciliacionArchivo", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.SmallInt).Value = 2;
                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = this.Corporativo;
                    comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = this.SucursalConciliacion;
                    comando.Parameters.Add("@AñoConciliacion", System.Data.SqlDbType.Int).Value = this.Año;
                    comando.Parameters.Add("@MesConciliacion", System.Data.SqlDbType.SmallInt).Value = this.MesConciliacion;
                    comando.Parameters.Add("@FolioConciliacion ", System.Data.SqlDbType.Int).Value = this.FolioConciliacion;
                    comando.Parameters.Add("@SucursalInterno", System.Data.SqlDbType.Int).Value = this.Sucursal;
                    comando.Parameters.Add("@FolioInterno ", System.Data.SqlDbType.Int).Value = this.Folio;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        resultado = reader["Apariciones"] == DBNull.Value ? false : Convert.ToInt32(reader["Apariciones"]) >= 1;
                    }
                   
                }

            }
            catch (SqlException ex)
            {
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("No se pudo guardar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return resultado;
        }

        public override bool BorrarArchivoInterno()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaConciliacionArchivo", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.SmallInt).Value = 1;
                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = this.Corporativo;
                    comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = this.Sucursal;
                    comando.Parameters.Add("@AñoConciliacion", System.Data.SqlDbType.Int).Value = this.Año;
                    comando.Parameters.Add("@MesConciliacion", System.Data.SqlDbType.SmallInt).Value = this.MesConciliacion;
                    comando.Parameters.Add("@FolioConciliacion ", System.Data.SqlDbType.Int).Value = this.Folio;
                    comando.Parameters.Add("@SucursalInterno", System.Data.SqlDbType.Int).Value = this.Sucursal;
                    comando.Parameters.Add("@FolioInterno ", System.Data.SqlDbType.Int).Value = 0; //this.Folio;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.ExecuteNonQuery();
                    resultado = true;
                }

            }
            catch (SqlException ex)
            {
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("No se pudo borrar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return resultado;
        }

        public override DatosArchivo CrearObjeto()
        {
            return new DatosArchivoDatos(this.ImplementadorMensajes);
        }

    }
}
