using Conciliacion.RunTime.DatosSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class PagoAnticipado : EmisorMensajes
    {
        private int corporativoconciliacion;
        private int sucursalconciliacion;
        private int añoconciliacion;
        private int mesconciliacion;
        private int folioconciliacion;
        private int secuenciarelacion;
        private int corporativoexteno;
        private int sucursalexterno;
        private int añoexterno;
        private int folioexterno;
        private int secuenciaexterno;
        private decimal montoexterno;
        private int formaconciliacion;
        private int statusconciliacion;
        private int motivonoconciliado;
        private string comentarionoconciliado;
        private string usuario;
        private DateTime falta;
        private string descripcion;
        private string usuariostatusconcepto;
        private DateTime fstatusconcepto;

        public PagoAnticipado(IMensajesImplementacion implementadorMensajes)
        {
            this.implementadorMensajes = implementadorMensajes;
            this.corporativoconciliacion = 0;
            this.sucursalconciliacion = 0;
            this.añoconciliacion = 0;
            this.mesconciliacion = 0;
            this.folioconciliacion = 0;
            this.secuenciarelacion = 0;
            this.corporativoexteno = 0;
            this.sucursalexterno = 0;
            this.añoexterno = 0;
            this.folioexterno = 0;
            this.secuenciaexterno = 0;
            this.montoexterno = 0;
            this.formaconciliacion = 0;
            this.statusconciliacion = 0;
            this.motivonoconciliado = 0;
            this.comentarionoconciliado = "";
            this.usuario = "";
            this.falta = DateTime.MinValue;
            this.descripcion = "";
            this.usuariostatusconcepto = "";
            this.fstatusconcepto = DateTime.MinValue;
        }

        public PagoAnticipado(
            int corporativoconciliacion,
            int sucursalconciliacion,
            int añoconciliacion,
            int mesconciliacion,
            int folioconciliacion,
            int Secuenciarelacion,
            int corporativoexteno,
            int sucursalexterno,
            int añoexterno,
            int folioexterno,
            int secuenciaexterno,
            decimal montoexterno,
            int formaconciliacion,
            int statusconciliacion,
            int motivonoconciliado,
            string comentarionoconciliado,
            string usuario,
            DateTime falta,
            string descripcion,
            string usuariostatusconcepto,
            DateTime fstatusconcepto,
            IMensajesImplementacion implementadorMensajes)
        {
            this.corporativoconciliacion = 0;
            this.sucursalconciliacion = 0;
            this.añoconciliacion = 0;
            this.mesconciliacion = 0;
            this.folioconciliacion = 0;
            this.secuenciarelacion = 0;
            this.corporativoexteno = 0;
            this.sucursalexterno = 0;
            this.añoexterno = 0;
            this.folioexterno = 0;
            this.secuenciaexterno = 0;
            this.montoexterno = 0;
            this.formaconciliacion = 0;
            this.statusconciliacion = 0;
            this.motivonoconciliado = 0;
            this.comentarionoconciliado = "";
            this.usuario = "";
            this.falta = DateTime.MinValue;
            this.descripcion = "";
            this.usuariostatusconcepto = "";
            this.fstatusconcepto = DateTime.MinValue;
            this.implementadorMensajes = implementadorMensajes;
        }

        public int CorporativoConciliacion
        {
            get { return corporativoconciliacion; }
            set { corporativoconciliacion = value; }
        }
        public int SucursalConciliacion
        {
            get { return sucursalconciliacion; }
            set { sucursalconciliacion = value; }
        }
        public int AñoConciliacion
        {
            get { return añoconciliacion; }
            set { añoconciliacion = value; }
        }
        public int MesConciliacion
        {
            get { return mesconciliacion; }
            set { mesconciliacion = value; }
        }
        public int FolioConciliacion
        {
            get { return folioconciliacion; }
            set { folioconciliacion = value; }
        }
        public int SecuenciaRelacion
        {
            get { return secuenciarelacion; }
            set { secuenciarelacion = value; }
        }
        public int CorporativoExteno
        {
            get { return corporativoexteno; }
            set { corporativoexteno = value; }
        }
        public int SucursalExterno
        {
            get { return sucursalexterno; }
            set { sucursalexterno = value; }
        }
        public int AñoExterno
        {
            get { return añoexterno; }
            set { añoexterno = value; }
        }
        public int FolioExterno
        {
            get { return folioexterno; }
            set { folioexterno = value; }
        }
        public int SecuenciaExterno
        {
            get { return secuenciaexterno; }
            set { secuenciaexterno = value; }
        }
        public decimal MontoExterno
        {
            get { return montoexterno; }
            set { montoexterno = value; }
        }
        public int FormaConciliacion
        {
            get { return formaconciliacion; }
            set { formaconciliacion = value; }
        }
        public int StatusConciliacion
        {
            get { return statusconciliacion; }
            set { statusconciliacion = value; }
        }
        public int MotivoNoConciliado
        {
            get { return motivonoconciliado; }
            set { motivonoconciliado = value; }
        }
        public string ComentarioNoConciliado
        {
            get { return comentarionoconciliado; }
            set { comentarionoconciliado = value; }
        }
        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }
        public DateTime FAlta
        {
            get { return falta; }
            set { falta = value; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        public string UsuarioStatusConcepto
        {
            get { return usuariostatusconcepto; }
            set { usuariostatusconcepto = value; }
        }
        public DateTime FStatusConcepto
        {
            get { return fstatusconcepto; }
            set { fstatusconcepto = value; }
        }

        public abstract PagoAnticipado CrearObjeto();
        public abstract bool ValidarClientePagoAnticipado(Conexion _conexion, int NumeroCliente);
        public abstract bool RegistraConciliacionReferencia(Conexion _conexion, int CorporativoConciliacion, int SucursalConciliacion, int AñoConciliacion, int MesConciliacion, int FolioConciliacion, int SecuenciaRelacion, int CorporativoExterno, int SucursalExterno, int AñoExterno, int FolioExterno, int SecuenciaExterno, decimal MontoExterno, int FormaConciliacion, int StatusConcepto, string StatusConciliacion, int MotivoNoConciliado, string ComentarioNoConciliado, string Usuario, DateTime FAlta, string Descripcion, string UsuarioStatusConcepto, DateTime FStatusConcepto);

    }
}
