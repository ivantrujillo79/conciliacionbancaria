using System;
using System.Data;
using System.Collections;


namespace SeguridadWeb.Public
{
    public class Modulos
    {
        #region "Constructores"
        internal Modulos(DataTable listaModulos)
        {
            foreach (DataRow rw in listaModulos.Rows)
            {
                lista.Add(new Modulo(Convert.ToInt16(rw["Modulo"]), rw["Nombre"].ToString().Trim().ToUpper(), rw["Prefijo"].ToString().Trim().ToUpper(), rw["Version"].ToString().Trim().ToUpper(), Convert.ToBoolean(rw["Habilitado"])));
            }
        }
        #endregion
        #region "Variables globales"
        private ArrayList lista = new ArrayList();
        #endregion
        #region "Propiedades"
        public string ListaModulos
        {
            get {
                string listaModulos = string.Empty;
                foreach (Modulo mod in lista)
                    if (mod.Habilitado)
                        listaModulos += mod.ModuloID.ToString() + ", ";
                return listaModulos; 
                }
        }
        #endregion
        #region "Metodos"
        public bool EstaHabilitado(short modulo)
        {
            foreach (Modulo mod in lista)
                if (mod.ModuloID == modulo)
                    return mod.Habilitado;
            return false;
        }
        public bool EstaHabilitado(string nombre)
        {
            foreach (Modulo mod in lista)
                if (mod.Nombre == nombre.Trim().ToUpper())
                    return mod.Habilitado;
            return false;
        }
        #endregion
    }
    internal class Modulo
    {
        #region "Constructores"
        public Modulo(short modulo, string nombre, string prefijo, string version, bool habilitado)
        {
            this.modulo = modulo;
            this.nombre = nombre;
            this.prefijo = prefijo;
            this.version = version;
            this.habilitado = habilitado;
        }

        #endregion
        #region "Variables globales"
        private short modulo;
        string nombre, prefijo, version;
        bool habilitado;
        #endregion
        #region "Propiedades"
        public short ModuloID
        {
            get { return this.modulo; }
        }
        public string Nombre
        {
            get { return this.nombre; }
        }
        public string Prefijo
        {
            get { return this.prefijo; }
        }
        public string Version
        {
            get { return this.version; }
        }
        public bool Habilitado
        {
            get { return this.habilitado; }
        }
        #endregion
    }
}
