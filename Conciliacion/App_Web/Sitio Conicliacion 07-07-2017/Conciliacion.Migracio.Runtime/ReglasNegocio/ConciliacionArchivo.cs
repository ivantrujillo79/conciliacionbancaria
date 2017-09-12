using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio
{
    public abstract class ConciliacionArchivo :ObjetoBase
    {

        int idCorporativoConciliacion;

        public int IdCorporativoConciliacion
        {
            get { return idCorporativoConciliacion; }
            set { idCorporativoConciliacion = value; }
        }
        int idSucursalConciliacion;

        public int IdSucursalConciliacion
        {
            get { return idSucursalConciliacion; }
            set { idSucursalConciliacion = value; }
        }
        int anioConciliacion;

        public int AnioConciliacion
        {
            get { return anioConciliacion; }
            set { anioConciliacion = value; }
        }
        int mesConciliacion;

        public int MesConciliacion
        {
            get { return mesConciliacion; }
            set { mesConciliacion = value; }
        }
        int folioConciliacion;

        public int FolioConciliacion
        {
            get { return folioConciliacion; }
            set { folioConciliacion = value; }
        }

        int idCorporativoInterno;

        public int IdCorporativoInterno
        {
            get { return idCorporativoInterno; }
            set { idCorporativoInterno = value; }
        }
        int idSucursalInterno;

        public int IdSucursalInterno
        {
            get { return idSucursalInterno; }
            set { idSucursalInterno = value; }
        }
        int anioInterno;

        public int AnioInterno
        {
            get { return anioInterno; }
            set { anioInterno = value; }
        }
        int mesInterno;

        public int MesInterno
        {
            get { return mesInterno; }
            set { mesInterno = value; }
        }
        int folioInterno;

        public int FolioInterno
        {
            get { return folioInterno; }
            set { folioInterno = value; }
        }


        public Corporativo CorporativoConciliacion
        {

            get
            {

                return App.Consultas.ObtieneCorporativoPorId(this.IdCorporativoConciliacion);
            }
        }

        public Sucursal SucursalConciliacion
        {

            get
            {

                return App.Consultas.ObtieneSucursalPorId(this.IdSucursalConciliacion);
            }
        }


        public Corporativo CorporativoInterno
        {

            get
            {

                return App.Consultas.ObtieneCorporativoPorId(this.IdCorporativoInterno);
            }
        }

        public Sucursal SucursalInterno
        {

            get
            {

                return App.Consultas.ObtieneSucursalPorId(this.IdSucursalInterno);
            }
        }


        //#region IObjetoBase Members
        //public virtual string CadenaConexion
        //{
        //    get
        //    {
        //        return App.CadenaConexion;
        //    }
        //}
        //public abstract bool Actualizar();
        //public abstract IObjetoBase CrearObjeto();
        //public abstract bool Eliminar();
        //public abstract bool Guardar();

        //#endregion
    }
}
