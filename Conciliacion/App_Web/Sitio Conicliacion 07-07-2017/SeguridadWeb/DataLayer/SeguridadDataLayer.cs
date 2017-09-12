using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace SeguridadWeb.DataLayer
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

        //public static  List<ControlDeFolios.ConceptoFactura> ConceptosPorUsuario(string usuario)
        //{
        //    List<ControlDeFolios.ConceptoFactura> conceptos = new List<ControlDeFolios.ConceptoFactura>();
        //    try
        //    {
        //        using (SqlConnection cnn = new SqlConnection(SeguridadDataLayer.CadenaConexion))
        //        {
        //            cnn.Open();
        //            SqlCommand cmd = new SqlCommand("spSEGConceptosUsuario", cnn);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 15).Value = usuario;
        //            SqlDataReader  reader = cmd.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                ControlDeFolios.ConceptoFactura conceptoFactura = new ControlDeFolios.ConceptoFactura();
        //                conceptoFactura.IdConceptoFactura  = Convert.ToInt32(reader["ConceptoFactura"]);
        //                conceptoFactura.DescripcionCbo = reader["Serie"].ToString() + "|" + reader["DescripcionLarga"].ToString();
        //                conceptoFactura.Descripcion = Convert.ToString(reader["Descripcion"]);
        //                conceptoFactura.FacturacionDetallada = Convert.ToByte(reader["FacturacionDetallada"] != DBNull.Value ? reader["FacturacionDetallada"] : 0);
        //                conceptoFactura.Serie = Convert.ToString(reader["Serie"]);
        //                conceptoFactura.IdTipoDocumento = Convert.ToInt32(reader["TipoDocumento"]);
        //                conceptoFactura.TipoDocumento = Convert.ToString(reader["TipoDocumentoDescripcion"]);
        //                conceptoFactura.TipoCaptura = Convert.ToString(reader["TipoCaptura"]);
        //                conceptoFactura.CapturarPedido = Convert.ToBoolean(reader["CapturarPedido"]);
        //                conceptoFactura.CapturarRemision = Convert.ToBoolean(reader["CapturarRemision"]);
        //                conceptoFactura.CapturaSinReferencia = Convert.ToBoolean(reader["CapturarSinReferencia"]);
        //                conceptoFactura.DescripcionLarga = reader["DescripcionLarga"].ToString();
        //                conceptoFactura.NCCancela = reader["NCCancela"] == DBNull.Value ? false : Convert.ToBoolean(reader["NCCancela"]);
        //                conceptos.Add(conceptoFactura);
        //            }
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw ex;
        //    }
        //    return conceptos;
        //}

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

        public static DataTable CorporativosUsuario(string usuario)
        {
            SqlDataAdapter da = new SqlDataAdapter("spSEGCorporativosUsuario", conexion);
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

        public static DataTable AreasUsuario(string usuario)
        {
            SqlDataAdapter da = new SqlDataAdapter("spSEGAreasUsuario", conexion);
            DataTable res = new DataTable("Areas");
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
                TerminaConsulta(false,true);
            }

        }

        public static DataTable PreferenciasUsuario(string usuario)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.spSEGUsuarioPreferencia", conexion);
            DataTable res = new DataTable("Preferencias");
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

        //METODO OCUPADO PARA LA ESTRUCTURA SIGAMET
        public static DataTable OperacionesUsuarioModulo(short modulo, string usuario)
        {
            SqlDataAdapter da = new SqlDataAdapter("spSEGPrivilegiosUsuario", conexion);
            DataTable res = new DataTable("Privilegios");
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Modulo", SqlDbType.SmallInt).Value = modulo;
            da.SelectCommand.Parameters.Add("@Usuario", SqlDbType.Char, 15).Value = usuario;
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

        //METODO OCUPADO PARA LA ESTRUCTURA DEL MODULO DE FACTOR HUMANO
        public static DataTable OperacionesUsuarioModulo(string listamodulos, string usuario)
        {
            SqlDataAdapter da = new SqlDataAdapter("spSEGPrivilegiosUsuario", conexion);
            DataTable res = new DataTable("Privilegios");
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Modulos", SqlDbType.VarChar, 2000).Value = listamodulos;
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

        //METODO OCUPADO PARA LA ESTRUCTURA SIGAMET
        public static DataTable ParametrosModulo(short modulo, byte corporativo)
        {
            SqlDataAdapter da = new SqlDataAdapter("spSEGParametrosModulo", conexion);
            DataTable res = new DataTable("Parametros");
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Modulo", SqlDbType.SmallInt).Value = modulo;
            da.SelectCommand.Parameters.Add("@Corporativo", SqlDbType.TinyInt).Value = corporativo;
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

        //METODO OCUPADO PARA LA ESTRUCTURA SIGAMET
        public static DataTable ParametrosModulo(short modulo, byte corporativo, byte sucursal)
        {
            SqlDataAdapter da = new SqlDataAdapter("spSEGParametrosModulo", conexion);
            DataTable res = new DataTable("Parametros");
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Modulo", SqlDbType.SmallInt).Value = modulo;
            da.SelectCommand.Parameters.Add("@Corporativo", SqlDbType.TinyInt).Value = corporativo;
            da.SelectCommand.Parameters.Add("@Sucursal", SqlDbType.TinyInt).Value = sucursal;
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


        //METODO OCUPADO PARA LA ESTRUCTURA DEL MODULO DE FACTOR HUMANO
        public static DataTable ParametrosModulo(string listamodulos, byte corporativo, byte sucursal)
        {
            SqlDataAdapter da = new SqlDataAdapter("spSEGParametrosModulo", conexion);
            DataTable res = new DataTable("Parametros");
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Modulos", SqlDbType.VarChar, 2000).Value = listamodulos;
            da.SelectCommand.Parameters.Add("@Corporativo", SqlDbType.TinyInt).Value = corporativo;
            da.SelectCommand.Parameters.Add("@Sucursal", SqlDbType.TinyInt).Value = sucursal;
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
