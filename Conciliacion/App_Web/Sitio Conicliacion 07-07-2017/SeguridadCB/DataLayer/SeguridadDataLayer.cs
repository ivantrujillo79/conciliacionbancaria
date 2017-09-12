using System;
using System.Data;
using System.Data.SqlClient;

namespace SeguridadCB.DataLayer
{
    internal class SeguridadDataLayer
    {
        #region "Variables globales"
        private static SqlConnection conexion;
        private static string cadenaconexion;
        #endregion
        #region "Propiedades"
        public static SqlConnection Conexion
        {
            get { return conexion; }
        }

        public static string CadenaConexion
        {
            get
            {
                return cadenaconexion;
            }
        }
        #endregion
        #region "Funciones comunes"
        public static void InicializaInterfase(SqlConnection conexion)
        {
            DataLayer.SeguridadDataLayer.conexion = conexion;
            DataLayer.SeguridadDataLayer.cadenaconexion = conexion.ConnectionString;
        }
        public static void AbreConexion()
        {
            if (SeguridadDataLayer.conexion.State != ConnectionState.Open)
                try
                {
                    SeguridadDataLayer.conexion.Open();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
        }
        public static void CierraConexion()
        {
            if (SeguridadDataLayer.conexion.State != ConnectionState.Closed)
                try
                {
                    SeguridadDataLayer.conexion.Close();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
        }
        public static void IniciaConsulta()
        {
            SqlCommand cmd = new SqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED", SeguridadDataLayer.conexion);
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
        public static void TerminaConsulta()
        {
            SqlCommand cmd = new SqlCommand("SET TRANSACTION ISOLATION LEVEL READ COMMITTED", SeguridadDataLayer.conexion);
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

        public static void IniciaConsulta(bool abrirConexion, bool cerrarConexion)
        {
            SqlCommand cmd = new SqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED", SeguridadDataLayer.conexion);
            try
            {
                if (abrirConexion)
                    SeguridadDataLayer.AbreConexion();
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
                    SeguridadDataLayer.CierraConexion();
            }
        }
        public static void TerminaConsulta(bool abrirConexion, bool cerrarConexion)
        {
            SqlCommand cmd = new SqlCommand("SET TRANSACTION ISOLATION LEVEL READ COMMITTED", SeguridadDataLayer.conexion);
            try
            {
                if (abrirConexion)
                    SeguridadDataLayer.AbreConexion();
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
                    SeguridadDataLayer.CierraConexion();
            }
        }

        #endregion
        #region "Funciones de seguridad"
        public static bool ExisteUsuarioActivo(string usuario)
        {
            SqlCommand cmd = new SqlCommand("spSEGUsuarioActivo", SeguridadDataLayer.conexion);
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

        public static SqlDataReader DatosUsuario(string usuario)
        {
            SqlCommand cmd = new SqlCommand("spSEGDatosUsuario", SeguridadDataLayer.conexion);
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

        public static DataTable ModulosUsuario(string usuario)
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

        //public static DataTable SucursalesCorporativos(ConfiguracionIden0 configuracion, int corporativo)
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

        public static DataTable CorporativosUsuario(string usuario)
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
        //Se quito AreasUsuario
        //public static DataTable AreasUsuario(string usuario)
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


        public static DataTable OperacionesUsuarioModulo(string modulo, string usuario)
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
        public static DataTable ParametrosModulo(string listamodulos, byte corporativo, int sucursal)
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
