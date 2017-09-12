using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.Migracion.Runtime.ReglasNegocio
{
    public abstract class Etiqueta : ObjetoBase
    {

        int idBancoFinanciero;

        public int IdBancoFinanciero
        {
            get { return idBancoFinanciero; }
            set { idBancoFinanciero = value; }
        }
        int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        int idTipoFuenteInformacion;

        public int IdTipoFuenteInformacion
        {
            get { return idTipoFuenteInformacion; }
            set { idTipoFuenteInformacion = value; }
        }


        string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }



        string tipoDato;
        public string TipoDato
        {
            get { return tipoDato; }
            set { tipoDato = value; }
        }


        string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        DateTime? fAlta;

        public DateTime? FAlta
        {
            get { return fAlta; }
            set { fAlta = value; }
        }
        DateTime? fBaja;

        public DateTime? FBaja
        {
            get { return fBaja; }
            set { fBaja = value; }
        }

        string usuarioAlta;

        public string UsuarioAlta
        {
            get { return usuarioAlta; }
            set { usuarioAlta = value; }
        }
        string usuarioBaja;

        public string UsuarioBaja
        {
            get { return usuarioBaja; }
            set { usuarioBaja = value; }
        }

        //string finaliza;
        //public string Finaliza
        //{
        //    get { return finaliza; }
        //    set { finaliza = value; }
        //}
        //string campoRelacionado;

        //public string CampoRelacionado
        //{
        //    get { return campoRelacionado; }
        //    set { campoRelacionado = value; }
        //}
        //string ejemplo;

        //public string Ejemplo
        //{
        //    get { return ejemplo; }
        //    set { ejemplo = value; }
        //}

        //int longitud;

        //public int Longitud
        //{
        //    get { return longitud; }
        //    set { longitud = value; }
        //}


        string tablaDestino;

        public string TablaDestino
        {
            get { return tablaDestino; }
            set { tablaDestino = value; }
        }

        string columnaDestino;
        public string ColumnaDestino
        {
            get { return columnaDestino; }
            set { columnaDestino = value; }
        }

        bool concatenaetiqueta;
        public bool ConcatenaEtiqueta
        {
            get { return concatenaetiqueta; }
            set { concatenaetiqueta = value; }
        }
        string bancodes;
        public string BancoDes
        {
            get { return bancodes; }
            set { bancodes = value; }
        }
        public TipoFuenteInformacion TipoFuenteInformacion
        {

            get
            {
                return App.Consultas.ObtieneTipoFuenteDeInformacionePorId(this.IdTipoFuenteInformacion);
            }
        }

    }
}
