using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SeguridadCB.DataLayer
{
    internal class SeguridadDataLayer
    {
        #region "Variables globales"
        private SqlConnection conexion;
        private string cadenaconexion;
        #endregion
        #region "Propiedades"
        public SeguridadDataLayer()
        {
            conexion = new SqlConnection();
            conexion.ConnectionString = (System.Web.HttpContext.Current.Session["AppCadenaConexion"]).ToString();
        }

        public SqlConnection Conexion
        {
            get { return conexion; }
        }

        public string CadenaConexion
        {
            get
            {
                return cadenaconexion;
            }
        }
        #endregion
        #region "Funciones comunes"
        public void InicializaInterfase(SqlConnection conexion)
        {
            //DataLayer.this.conexion = conexion;
            //DataLayer.this.cadenaconexion = conexion.ConnectionString;
            this.conexion = conexion;
            this.cadenaconexion = conexion.ConnectionString;
        }
        //private string configuraConexion()
        //{
        //    //SeguridadCB.Public.Usuario usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
        //    AppSettingsReader settings = new AppSettingsReader();
        //    string servidor = settings.GetValue("Servidor", typeof(string)).ToString();
        //    string baseDatos = settings.GetValue("Base", typeof(string)).ToString();
        //    SeguridadCB.Seguridad.TipoSeguridad seguridad;
        //    string ConnectionString = "";
        //    if (settings.GetValue("Seguridad", typeof(string)).ToString() == "NT")
        //        seguridad = SeguridadCB.Seguridad.TipoSeguridad.NT;
        //    else
        //        seguridad = SeguridadCB.Seguridad.TipoSeguridad.SQL;
        //    if (seguridad == SeguridadCB.Seguridad.TipoSeguridad.NT)
        //        ConnectionString = "Application Name = Conciliacion Bancaria; Data Source=" + servidor + ";Initial Catalog=" + baseDatos + ";Integrated Security=SSPI;";
        //    else
        //        ConnectionString = "Application Name = Conciliacion Bancaria; Data Source = " + servidor + "; Initial Catalog = " +
        //                            baseDatos + "; User ID = " + usuario.IdUsuario.Trim() + "; Password = " + usuario.Clave;
        //    return ConnectionString;
        //}
        public void AbreConexion()
        {
            if (this.conexion.State != ConnectionState.Open)
                try
                {
                    //this.conexion.ConnectionString = configuraConexion();
                    this.conexion.Open();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
        }
        public void CierraConexion()
        {
            if (this.conexion.State != ConnectionState.Closed)
                try
                {
                    this.conexion.Close();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
        }
        public void IniciaConsulta()
        {
            SqlCommand cmd = new SqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED", this.conexion);
            try
            {
                if (conexion.State == ConnectionState.Open)
                    cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public void TerminaConsulta()
        {
            SqlCommand cmd = new SqlCommand("SET TRANSACTION ISOLATION LEVEL READ COMMITTED", this.conexion);
            try
            {
                if (conexion.State == ConnectionState.Open)
                    cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public  void IniciaConsulta(bool abrirConexion, bool cerrarConexion)
        {
            SqlCommand cmd = new SqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED", this.conexion);
            try
            {
                if (abrirConexion)
                    this.AbreConexion();
                if (conexion.State == ConnectionState.Open)
                    cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (cerrarConexion)
                    this.CierraConexion();
            }
        }
        public  void TerminaConsulta(bool abrirConexion, bool cerrarConexion)
        {
            SqlCommand cmd = new SqlCommand("SET TRANSACTION ISOLATION LEVEL READ COMMITTED", this.conexion);
            try
            {
                if (abrirConexion)
                    this.AbreConexion();
                if (conexion.State == ConnectionState.Open)
                    cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (cerrarConexion)
                    this.CierraConexion();
            }
        }

        #endregion
        #region "Funciones de seguridad"
        public  bool ExisteUsuarioActivo(string usuario)
        {
            SqlCommand cmd = new SqlCommand("spSEGUsuarioActivo", this.conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            bool res;
            cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 15).Value = usuario;
            try
            {
                IniciaConsulta(true, false);
                res = (bool)cmd.ExecuteScalar();
                return res;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                TerminaConsulta(false, true);
            }
        }

        public  SqlDataReader DatosUsuario(string usuario)
        {
            SqlCommand cmd = new SqlCommand("spSEGDatosUsuario", this.conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader res;
            cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 15).Value = usuario;
            try
            {
                IniciaConsulta(true, false);
                res = cmd.ExecuteReader();
                return res;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public  DataTable ModulosUsuario(string usuario)
        {
            SqlDataAdapter da = new SqlDataAdapter("spSEGUsuarioModulo", conexion);
            DataTable res = new DataTable("Modulos");
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Usuario", SqlDbType.VarChar, 15).Value = usuario;
            try
            {
                IniciaConsulta(true, false);
                da.Fill(res);
                return res;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                TerminaConsulta(false, true);
            }
        }

        //public  DataTable SucursalesCorporativos(ConfiguracionIden0 configuracion, int corporativo)
        //{
        //    SqlDataAdapter da = new SqlDataAdapter("spCBCargarComboSucursal", conexion);
        //    DataTable res = new DataTable("Sucursales");
        //    da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    da.SelectCommand.Parameters.Add("@Configuracion", System.Data.SqlDbType.SmallInt).Value = configuracion;
        //    da.SelectCommand.Parameters.Add("@Corporativo", SqlDbType.Int, 15).Value = corporativo;
        //    try
        //    {
        //        IniciaConsulta(true, false);
        //        da.Fill(res);
        //        return res;
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        TerminaConsulta(false, true);
        //    }
        //}

        public  DataTable CorporativosUsuario(string usuario)
        {
            SqlDataAdapter da = new SqlDataAdapter("spCBCorporativosUsuario", conexion);
            DataTable res = new DataTable("Corporativos");
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Usuario", SqlDbType.VarChar, 15).Value = usuario;
            try
            {
                IniciaConsulta(true, false);
                da.Fill(res);
                return res;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                TerminaConsulta(false, true);
            }
        }

        public  string InicialCorporativosUsuario(string usuario)
        {

            SqlCommand cmd = new SqlCommand("spCBCorporativosUsuarioInicial", this.conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            string res;
            cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 15).Value = usuario;
            try
            {
                IniciaConsulta(true, false);
                res = (string)cmd.ExecuteScalar();
                return res;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                TerminaConsulta(false, true);
            }            
        }


        //Se quito AreasUsuario
        //public  DataTable AreasUsuario(string usuario)
        //{
        //    SqlDataAdapter da = new SqlDataAdapter("spSEGAreasUsuario", conexion);
        //    DataTable res = new DataTable("Areas");
        //    da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    da.SelectCommand.Parameters.Add("@Usuario", SqlDbType.VarChar, 15).Value = usuario;
        //    try
        //    {
        //        IniciaConsulta(true, false);
        //        da.Fill(res);
        //        return res;
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        TerminaConsulta(false, true);
        //    }

        //}


        public  DataTable OperacionesUsuarioModulo(string modulo, string usuario)
        {
            SqlDataAdapter da = new SqlDataAdapter("spSEGPrivilegiosUsuario", conexion);
            DataTable res = new DataTable("Privilegios");
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Modulo", SqlDbType.VarChar, 2000).Value = modulo;
            da.SelectCommand.Parameters.Add("@Usuario", SqlDbType.VarChar, 15).Value = usuario;
            try
            {
                IniciaConsulta(true, false);
                da.Fill(res);
                return res;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                TerminaConsulta(false, true);
            }
        }
        //Se cambio a solo un modulo, no lista de modulos
        public  DataTable ParametrosModulo(string listamodulos, byte corporativo, int sucursal)
        {
            SqlDataAdapter da = new SqlDataAdapter("spSEGParametrosModulo", conexion);
            DataTable res = new DataTable("Parametros");
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Modulo", SqlDbType.VarChar).Value = listamodulos;
            da.SelectCommand.Parameters.Add("@Corporativo", SqlDbType.TinyInt).Value = corporativo;
            da.SelectCommand.Parameters.Add("@Sucursal", SqlDbType.Int).Value = sucursal;
            try
            {
                IniciaConsulta(true, false);
                da.Fill(res);

                return res;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                TerminaConsulta(false, true);
            }
        }

        #endregion

    }
}
