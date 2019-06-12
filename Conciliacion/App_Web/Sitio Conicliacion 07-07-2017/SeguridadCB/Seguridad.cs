using System;
using System.Data;
using System.Data.SqlClient;
using SeguridadCB.DataLayer;
using SeguridadCB.Public;

namespace SeguridadCB
{
    public class Seguridad
    {
        private SqlConnection conexion;
        private string _InicialCorporativo = "";
        SeguridadDataLayer seguridaddatalayer = new SeguridadDataLayer();

        public Seguridad()
        {
            conexion = new SqlConnection();
            conexion.ConnectionString = (System.Web.HttpContext.Current.Session["AppCadenaConexion"]).ToString();
        }

        public SqlConnection Conexion
        {
            get
            {
                return conexion;
            }
            //set { SeguridadDataLayer.InicializaInterfase(value); }
            set
            {
                seguridaddatalayer.InicializaInterfase(value);
            }
        }

        public bool ExisteUsuarioActivo(string usuario)
        {
            return seguridaddatalayer.ExisteUsuarioActivo(usuario);
        }

        public Usuario DatosUsuario(string usuario)
        {
            _InicialCorporativo = seguridaddatalayer.InicialCorporativosUsuario(usuario);
            DataTable dtCorporativos = new DataTable("Corporativos");
            dtCorporativos = seguridaddatalayer.CorporativosUsuario(usuario);                          
            SqlDataReader rdr = null;
            try
            {
                rdr = seguridaddatalayer.DatosUsuario(usuario);
                rdr.Read();
                Encripter objEncrypter = new Encripter();
                return  new Usuario(rdr["Usuario"].ToString(),
                                    rdr["Nombre"].ToString(), 
                                    Convert.ToInt32(rdr["Empleado"]), 
                                    objEncrypter.ImplicitUnencript(rdr["Clave"].ToString()), 
                                    objEncrypter.ImplicitUnencript(rdr["Clave"].ToString()), 
                                    Convert.ToByte(rdr["Corporativo"]), 
                                    rdr["NombreCorporativo"].ToString(), 
                                    Convert.ToInt16(rdr["Sucursal"]), 
                                    rdr["SucursalDescripcion"].ToString(), 
                                    dtCorporativos,
                                    _InicialCorporativo
                                    ,Convert.ToInt16(rdr["Area"].ToString())
                                    );//Convert.ToInt16(rdr["Area"]), rdr["NombreArea"].ToString(), dtAreas
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
                seguridaddatalayer.TerminaConsulta(false, true);
            }
        }

        public bool ComparaClaves(string clave, Usuario datosUsuario)
        {
            return clave == datosUsuario.ClaveDesencriptada;
        }

        public Modulos Modulos(string usuario)
        {
            DataTable dt = seguridaddatalayer.ModulosUsuario(usuario);
            return new Modulos(dt);
        }

        public Operaciones Operaciones(string modulo, string usuario)
        {
            DataTable dt = seguridaddatalayer.OperacionesUsuarioModulo(modulo, usuario);
            return new Operaciones(dt);
        }

        //Se cambio a solo un modulo, no lista
        public Parametros Parametros(string modulo, byte corporativo, int sucursal)
        {
            DataTable dt = seguridaddatalayer.ParametrosModulo(modulo, corporativo, sucursal);
            return new Parametros(dt);
        }
       
        public string EncriptaClave(string clave)
        {
            Encripter objEncrypter = new Encripter();
            return objEncrypter.ImplicitEncript(clave);
        }

        public string DesencriptaClave(string clave)
        {
            Encripter objEncrypter = new Encripter();
            return objEncrypter.ImplicitUnencript(clave);
        }

        public enum TipoSeguridad : byte { SQL = 0, NT = 1 }
    }
}
