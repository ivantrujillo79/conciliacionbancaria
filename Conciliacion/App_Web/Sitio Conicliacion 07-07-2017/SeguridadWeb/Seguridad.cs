using System;
using System.Data;
using System.Data.SqlClient;
using SeguridadWeb.DataLayer;
using SeguridadWeb.Public;

namespace SeguridadWeb
{
    public class Seguridad
    {
        private Seguridad()
        {

        }

        public SqlConnection Conexion
        {
            get { return SeguridadDataLayer.Conexion; }
            set { SeguridadDataLayer.InicializaInterfase(value); }
        }

        public bool ExisteUsuarioActivo(string usuario)
        {
            return SeguridadDataLayer.ExisteUsuarioActivo(usuario);
        }

        public Usuario DatosUsuario(string usuario)
        {
            DataTable dtCorporativos = new DataTable("Corporativos");
            DataTable dtPreferencias = new DataTable("Preferencias");
            //dtCorporativos = SeguridadDataLayer.CorporativosUsuario(usuario);
            //DataTable dtAreas = SeguridadDataLayer.AreasUsuario(usuario);
            dtPreferencias = SeguridadDataLayer.PreferenciasUsuario(usuario);
            SqlDataReader rdr = null;
            try
            {
                rdr = SeguridadDataLayer.DatosUsuario(usuario);
                rdr.Read();
                Encripter objEncrypter = new Encripter();
                Usuario objUsuario = new Usuario(rdr["Usuario"].ToString(), rdr["Nombre"].ToString(), Convert.ToInt32(rdr["Empleado"]), objEncrypter.ImplicitUnencript(rdr["Clave"].ToString()), objEncrypter.ImplicitUnencript(rdr["Clave"].ToString()), Convert.ToByte(rdr["Corporativo"]), rdr["NombreCorporativo"].ToString(), Convert.ToByte(rdr["Sucursal"]), rdr["SucursalDescripcion"].ToString(), Convert.ToInt16(rdr["Area"]), rdr["NombreArea"].ToString(), dtPreferencias);
                //objUsuario.Conceptos = SeguridadDataLayer.ConceptosPorUsuario(usuario);
                //return new Usuario(rdr["Usuario"].ToString(), rdr["Nombre"].ToString(), Convert.ToInt32(rdr["Empleado"]), Encripter.ImplicitUnencript(rdr["Clave"].ToString()), Encripter.ImplicitUnencript(rdr["Clave"].ToString()), Convert.ToByte(rdr["Corporativo"]), rdr["NombreCorporativo"].ToString(), Convert.ToByte(rdr["Sucursal"]), rdr["SucursalDescripcion"].ToString(), Convert.ToInt16(rdr["Area"]), rdr["NombreArea"].ToString(), dtCorporativos, dtAreas);
                return objUsuario;//new Usuario(rdr["Usuario"].ToString(), rdr["Nombre"].ToString(), Convert.ToInt32(rdr["Empleado"]), Encripter.ImplicitUnencript(rdr["Clave"].ToString()), Encripter.ImplicitUnencript(rdr["Clave"].ToString()), Convert.ToByte(rdr["Corporativo"]), rdr["NombreCorporativo"].ToString(), Convert.ToByte(rdr["Sucursal"]), rdr["SucursalDescripcion"].ToString(), Convert.ToInt16(rdr["Area"]), rdr["NombreArea"].ToString(),dtPreferencias);
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

        public bool ComparaClaves(string clave, Usuario datosUsuario)
        {
            return clave == datosUsuario.ClaveDesencriptada;
        }

        public Modulos Modulos(string usuario)
        {
            DataTable dt = SeguridadDataLayer.ModulosUsuario(usuario);
            return new Modulos(dt);
        }

        //METODO UTILIZADO PARA LA ESTRUCTURA DE FACTOR HUMANO
        public Operaciones Operaciones(string listamodulo, string usuario)
        {
            DataTable dt = SeguridadDataLayer.OperacionesUsuarioModulo(listamodulo, usuario);
            return new Operaciones(dt);
        }

        //METODO UTILIZADO PARA LA ESTRUCTURA SIGAMET
        public Operaciones Operaciones(short modulo, string usuario)
        {
            DataTable dt = SeguridadDataLayer.OperacionesUsuarioModulo(modulo, usuario);
            return new Operaciones(dt);
        }

        //METODO UTILIZADO PARA LA ESTRUCTURA DE FACTOR HUMANO
        public Parametros Parametros(string listamodulo, byte corporativo, byte sucursal)
        {
            DataTable dt = SeguridadDataLayer.ParametrosModulo(listamodulo, corporativo, sucursal);
            return new Parametros(dt);
        }

        //METODO UTILIZADO PARA LA ESTRUCTURA DE SIGAMET
        public Parametros Parametros(short modulo, byte corporativo)
        {
            DataTable dt = SeguridadDataLayer.ParametrosModulo(modulo, corporativo);
            return new Parametros(dt);
        }

        //METODO UTILIZADO PARA LA ESTRUCTURA DE SIGAMET
        public Parametros Parametros(short modulo, byte corporativo, byte sucursal)
        {
            DataTable dt = SeguridadDataLayer.ParametrosModulo(modulo, corporativo, sucursal);
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
