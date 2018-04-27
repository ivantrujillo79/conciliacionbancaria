using System;
using System.Data;
using System.Data.SqlClient;
using SeguridadCB.DataLayer;
using SeguridadCB.Public;

namespace SeguridadCB
{
    public class Seguridad
    {
        private static string _InicialCorporativo = "";
        private Seguridad()
        {

        }

        public static SqlConnection Conexion
        {
            get { return SeguridadDataLayer.Conexion; }
            set { SeguridadDataLayer.InicializaInterfase(value); }
        }

        public static bool ExisteUsuarioActivo(string usuario)
        {
            return SeguridadDataLayer.ExisteUsuarioActivo(usuario);
        }

        public static Usuario DatosUsuario(string usuario)
        {
            _InicialCorporativo = SeguridadDataLayer.InicialCorporativosUsuario(usuario);
            DataTable dtCorporativos = new DataTable("Corporativos");
            dtCorporativos = SeguridadDataLayer.CorporativosUsuario(usuario);                          
            //Se quitó AreasUsuario como parametro para la lectura de un usuario.
            //DataTable dtAreas = SeguridadDataLayer.AreasUsuario(usuario);
            SqlDataReader rdr = null;
            try
            {
                rdr = SeguridadDataLayer.DatosUsuario(usuario);
                rdr.Read();
                return  new Usuario(rdr["Usuario"].ToString(), rdr["Nombre"].ToString(), Convert.ToInt32(rdr["Empleado"]), Encripter.ImplicitUnencript(rdr["Clave"].ToString()), Encripter.ImplicitUnencript(rdr["Clave"].ToString()), Convert.ToByte(rdr["Corporativo"]), rdr["NombreCorporativo"].ToString(), Convert.ToInt16(rdr["Sucursal"]), rdr["SucursalDescripcion"].ToString(), dtCorporativos, _InicialCorporativo);//Convert.ToInt16(rdr["Area"]), rdr["NombreArea"].ToString(), dtAreas
                
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (rdr != null)
                    rdr.Close();
                SeguridadDataLayer.TerminaConsulta(false, true);
            }
        }

        public static bool ComparaClaves(string clave, Usuario datosUsuario)
        {
            return clave == datosUsuario.ClaveDesencriptada;
        }

        public static Modulos Modulos(string usuario)
        {
            DataTable dt = SeguridadDataLayer.ModulosUsuario(usuario);
            return new Modulos(dt);
        }

        public static Operaciones Operaciones(string modulo, string usuario)
        {
            DataTable dt = SeguridadDataLayer.OperacionesUsuarioModulo(modulo, usuario);
            return new Operaciones(dt);
        }

        //Se cambio a solo un modulo, no lista
        public static Parametros Parametros(string modulo, byte corporativo, int sucursal)
        {
            DataTable dt = SeguridadDataLayer.ParametrosModulo(modulo, corporativo, sucursal);
            return new Parametros(dt);
        }
       
        public static string EncriptaClave(string clave)
        {
            return Encripter.ImplicitEncript(clave);
        }

        public static string DesencriptaClave(string clave)
        {
            return Encripter.ImplicitUnencript(clave);
        }

        public enum TipoSeguridad : byte { SQL = 0, NT = 1 }
    }
}
