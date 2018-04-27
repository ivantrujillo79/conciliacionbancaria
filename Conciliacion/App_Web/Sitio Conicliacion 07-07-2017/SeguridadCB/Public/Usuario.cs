using System;
using System.Data;
using System.Collections;

namespace SeguridadCB.Public
{
    public class Usuario
    {
        #region "Constructores"
        internal Usuario(string usuario, string nombre, int empleado, string clave, string clavedesencriptada,
                         byte corporativo, string nombreCorporativo, int sucursal, string nombreSucursal,
                         DataTable tablaCorporativoAcceso)//short area,string nombreArea, DataTable tablaAreaAcceso
        {
            this.usuario = usuario;
            this.nombre = nombre;
            this.empleado = empleado;
            this.clave = clave;
            this.clavedesencriptada = clavedesencriptada;

            this.corporativo = corporativo;
            this.nombreCorporativo = nombreCorporativo;

            this.sucursal = sucursal;
            this.nombreSucursal = nombreSucursal;

            //this.area = area;
            //this.nombreArea = nombreArea;

            this.tablaCorporativoAcceso = tablaCorporativoAcceso;
          //  this.tablaAreaAcceso = tablaAreaAcceso;
        }

        internal Usuario(string usuario, string nombre, int empleado, string clave, string clavedesencriptada,
                         byte corporativo, string nombreCorporativo, int sucursal, string nombreSucursal,
                         DataTable tablaCorporativoAcceso,string inicial)//short area,string nombreArea, DataTable tablaAreaAcceso
        {
            this.usuario = usuario;
            this.nombre = nombre;
            this.empleado = empleado;
            this.clave = clave;
            this.clavedesencriptada = clavedesencriptada;

            this.corporativo = corporativo;
            this.nombreCorporativo = nombreCorporativo;

            this.sucursal = sucursal;
            this.nombreSucursal = nombreSucursal;

            //this.area = area;
            //this.nombreArea = nombreArea;

            this.tablaCorporativoAcceso = tablaCorporativoAcceso;
            //  this.tablaAreaAcceso = tablaAreaAcceso;
            this._inicialCorporativo = inicial;
        }

        #endregion
        #region "Variables globales"
        string usuario, nombre, clave, clavedesencriptada, nombreCorporativo, nombreSucursal, nombreArea;
        byte corporativo;
        int sucursal;
        //short area;
        int empleado;
        DataTable tablaCorporativoAcceso;//, tablaAreaAcceso;
        private string _inicialCorporativo;
        #endregion
        #region "Propiedades"
        public string IdUsuario
        {
            get { return usuario; }
        }
        public string Nombre
        {
            get { return this.nombre; }
        }
        public int Empleado
        {
            get { return this.empleado; }
        }
        public string Clave
        {
            get { return this.clave; }
        }
        public string ClaveDesencriptada
        {
            get { return this.clavedesencriptada; }
        }
        public byte Corporativo
        {
            get { return this.corporativo; }
        }
        public string NombreCorporativo
        {
            get { return this.nombreCorporativo; }
        }
        public int Sucursal
        {
            get { return this.sucursal; }
        }
        public string NombreSucursal
        {
            get { return this.nombreSucursal; }
        }
        //public short Area
        //{
        //    get { return this.area; }
        //}
        //public string NombreArea
        //{
        //    get { return this.nombreArea; }
        //}
        public DataTable CorporativoAcceso
        {
            get { return TablaCorporativo(); }
        }
        public DataTable SucursalAcceso
        {
            get { return this.tablaCorporativoAcceso; }
        }

        public string InicialCorporativo
        {
            get { return _inicialCorporativo; }
        }

        //public DataTable AreaAcceso
        //{
        //    get { return this.tablaAreaAcceso; }
        //}
        #endregion
        #region "Metodos"

        private DataTable TablaCorporativo()
        {
            DataTable corporativo = new DataTable("CorporativoAcceso");
            DataColumn column;
            DataColumn[] keys = new DataColumn[1];

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int16");
            column.ColumnName = "Corporativo";
            corporativo.Columns.Add(column);
            keys[0] = column;

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "NombreCorporativo";
            corporativo.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.Boolean");
            column.ColumnName = "CorporativoDefault";
            corporativo.Columns.Add(column);

            //column = new DataColumn();
            //column.DataType = Type.GetType("System.String");
            //column.ColumnName = "RutaLogotipo";
            //corporativo.Columns.Add(column);

            corporativo.PrimaryKey = keys;

            if (this.tablaCorporativoAcceso != null)
            {
                Int16 intcorporativo = 0;
                foreach (DataRow rw in this.tablaCorporativoAcceso.Rows)
                {
                    if (intcorporativo != Convert.ToInt16(rw["Corporativo"]))
                    {
                        corporativo.Rows.Add(Convert.ToInt16(rw["Corporativo"]), rw["NombreCorporativo"].ToString().Trim().ToUpper(), Convert.ToBoolean(rw["SucursalDefault"]));//, rw["RutaLogotipo"].ToString().Trim()
                    }
                    intcorporativo = Convert.ToInt16(rw["Corporativo"]);
                }
            }
            return corporativo;
        }

        #endregion

    }
}
