using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio
{
    public abstract class FuenteInformacion : ObjetoBase
    {
        int idFuenteInformacion;
        string rutaArchivo;
        string cuentaBancoFinanciero;
        int bancoFinanciero;
        int idTipoFuenteInformacion;
        int idSucursal;
        int numColumnas;
        int idTipoArchivo;

        string desTipoFuenteInformacion;

        public string DesTipoFuenteInformacion
        {
            get { return desTipoFuenteInformacion; }
            set { desTipoFuenteInformacion = value; }
        }

        //public abstract bool Guardar();
        //public abstract bool Actualizar();
        //public abstract bool Eliminar();}

        public abstract bool CopiarFuenteInformacionDetalle(string idCuentaBancoFinancieroFuente);
        //public abstract IObjetoBase CrearObjeto();


        public int IdFuenteInformacion
        {
            get { return idFuenteInformacion; }
            set { idFuenteInformacion = value; }
        }


        public string RutaArchivo
        {
            get { return rutaArchivo; }
            set { rutaArchivo = value; }
        }


        public string CuentaBancoFinanciero
        {
            get { return cuentaBancoFinanciero; }
            set { cuentaBancoFinanciero = value; }
        }


        public int BancoFinanciero
        {
            get { return bancoFinanciero; }
            set { bancoFinanciero = value; }
        }


        public int IdTipoFuenteInformacion
        {
            get { return idTipoFuenteInformacion; }
            set { idTipoFuenteInformacion = value; }
        }


        public int IdSucursal
        {
            get { return idSucursal; }
            set { idSucursal = value; }
        }


        public int NumColumnas
        {
            get { return numColumnas; }
            set { numColumnas = value; }
        }


        public int IdTipoArchivo
        {
            get { return idTipoArchivo; }
            set { idTipoArchivo = value; }
        }


        public TipoFuenteInformacion TipoFuenteInformacion
        {
            get
            {

                return App.Consultas.ObtieneTipoFuenteDeInformacionePorId(this.IdTipoFuenteInformacion);
            }
        }


        public Sucursal Sucursal
        {
            get
            {

                return App.Consultas.ObtieneSucursalPorId(this.IdSucursal);
            }
        }


        public TipoArchivo TipoArchivo
        {
            get
            {

                return App.Consultas.ObtieneTipoArchivoPorId(this.IdTipoArchivo);
            }
        }

        public List<FuenteInformacionDetalle> FuenteInformacionDetalle
        {
            get
            {

                return App.Consultas.ObtieneListaFuenteInoformacionDetallePorId(this.BancoFinanciero, this.CuentaBancoFinanciero, this.IdFuenteInformacion);
            }
        }



        //public virtual string CadenaConexion
        //{
        //    get
        //    {
        //        return App.CadenaConexion;
        //    }

        //}

    }
}
