using SeguridadCB.Public;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Conciliacion.RunTime.ReglasDeNegocio
{

    public struct DetalleSaldoConciliacion
    {
        int cliente;
        decimal montoSaldoAFavor;

        public int Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }

        public decimal MontoSaldoAFavor
        {
            get { return montoSaldoAFavor; }
            set { montoSaldoAFavor = value; }
        }
    }

    public abstract class ReferenciaNoConciliada : EmisorMensajes
    {
        int corporativo;
        int sucursalconciliacion;
        int sucursal;
        short mesconciliacion;
        int folioconciliacion;

        string sucursaldes;
        int año;
        int añoconciliacion;
        int folio;
        int secuencia;
        string descripcion;
        string concepto;

        decimal monto;
        decimal deposito;
        decimal retiro;
        decimal comision;
        //decimal montoconciliado;
        private decimal resto;
        short formaconciliacion;
        short statusconcepto;
        string statusconciliacion;

        DateTime foperacion;
        DateTime fmovimiento;

        bool completo;
        bool coninterno;

        decimal diferencia;
        string ubicacionicono;

        int motivonoconciliado;
        string comentarionoconciliado;

        string cheque;
        string referencia;
        string nombretercero;
        string rfctercero;

        string tipo;

        bool selecciona;
        //Nuevos atributos
        int consecutivoflujo;
        decimal saldo;
        int caja;
        string sucursalbancaria;

        //Traspaso
        string tipotraspaso;
        decimal? montotraspaso;

        int? corporativotraspaso;
        int? sucursaltraspaso;
        int? añotraspaso;
        int? foliotraspaso;

        public int cliente = 1;
        public int clientepago;

        public DetalleSaldoAFavor DetalleSaldo;

        private List<cReferencia> listareferenciaconciliada = new List<cReferencia>();
        private List<ReferenciaConciliadaCompartida> listareferenciaconciliadacompartida = new List<ReferenciaConciliadaCompartida>();
        private string statusMovimiento;
        private int pedido;
        private int corporativoex;
        private int sucursalex;
        private int añoex;
        private int folioex;
        private int secuenciaex;
        private int tipoCobro;
        private int tipoCobroAnterior;

        private string usuario;

        #region Constructores

        public ReferenciaNoConciliada(int corporativo, int sucursal, string sucursaldes, int añoconciliacion, int folio, int secuencia,
           string descripcion, string concepto, decimal deposito, decimal retiro, short formaconciliacion,
           short statusconcepto, string statusconciliacion, DateTime foperacion, DateTime fmovimiento, int folioconciliacion,
           short mesconciliacion, decimal diferencia, bool coninterno,
           string cheque, string referencia, string nombretercero, string rfctercero, int sucursalconciliacion,
           int año, MensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;

            this.folioconciliacion = folioconciliacion;
            this.mesconciliacion = mesconciliacion;

            this.corporativo = corporativo;
            this.sucursal = sucursal;
            this.sucursaldes = sucursaldes;
            this.añoconciliacion = añoconciliacion;
            this.año = año;
            this.folio = folio;
            this.secuencia = secuencia;
            this.descripcion = descripcion;
            this.concepto = concepto;

            this.monto = deposito + retiro;
            this.deposito = deposito;
            this.retiro = retiro;
            this.formaconciliacion = formaconciliacion;
            this.statusconcepto = statusconcepto;
            this.statusconciliacion = statusconciliacion;

            this.foperacion = foperacion;
            this.fmovimiento = fmovimiento;

            this.completo = false;
            this.diferencia = diferencia;
            this.coninterno = coninterno;
            this.motivonoconciliado = 0;
            this.comentarionoconciliado = "";

            this.cheque = cheque;
            this.referencia = referencia;
            this.nombretercero = nombretercero;
            this.rfctercero = rfctercero;

            this.sucursalconciliacion = sucursalconciliacion;

            this.selecciona = true;

        }

        public ReferenciaNoConciliada(int corporativo, int sucursal, string sucursaldes, int añoconciliacion, int folio, int secuencia,
           string descripcion, string concepto, decimal deposito, decimal retiro, short formaconciliacion,
           short statusconcepto, string statusconciliacion, DateTime foperacion, DateTime fmovimiento, int folioconciliacion,
           short mesconciliacion, decimal diferencia, bool coninterno,
           string cheque, string referencia, string nombretercero, string rfctercero, int sucursalconciliacion,
           int año, string statusMovimiento, MensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;

            this.folioconciliacion = folioconciliacion;
            this.mesconciliacion = mesconciliacion;

            this.corporativo = corporativo;
            this.sucursal = sucursal;
            this.sucursaldes = sucursaldes;
            this.añoconciliacion = añoconciliacion;
            this.año = año;
            this.folio = folio;
            this.secuencia = secuencia;
            this.descripcion = descripcion;
            this.concepto = concepto;

            this.monto = deposito + retiro;
            this.deposito = deposito;
            this.retiro = retiro;
            this.formaconciliacion = formaconciliacion;
            this.statusconcepto = statusconcepto;
            this.statusconciliacion = statusconciliacion;

            this.foperacion = foperacion;
            this.fmovimiento = fmovimiento;

            this.completo = false;
            this.diferencia = diferencia;
            this.coninterno = coninterno;
            this.motivonoconciliado = 0;
            this.comentarionoconciliado = "";

            this.cheque = cheque;
            this.referencia = referencia;
            this.nombretercero = nombretercero;
            this.rfctercero = rfctercero;

            this.sucursalconciliacion = sucursalconciliacion;
            this.statusMovimiento = statusMovimiento;

            this.selecciona = true;

        }


        public ReferenciaNoConciliada(int corporativo, int sucursal, string sucursaldes, int añoconciliacion, int folio, int secuencia,
          string descripcion, string concepto, decimal deposito, decimal retiro, short formaconciliacion,
          short statusconcepto, string statusconciliacion, DateTime foperacion, DateTime fmovimiento, string ubicacionicono, int folioconciliacion,
          short mesconciliacion, decimal diferencia, bool coninterno,
          string cheque, string referencia, string nombretercero, string rfctercero, int sucursalconciliacion,
          int año, MensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;

            this.folioconciliacion = folioconciliacion;
            this.mesconciliacion = mesconciliacion;

            this.corporativo = corporativo;
            this.sucursal = sucursal;
            this.sucursaldes = sucursaldes;
            this.añoconciliacion = añoconciliacion;
            this.año = año;
            this.folio = folio;
            this.secuencia = secuencia;
            this.descripcion = descripcion;
            this.concepto = concepto;

            this.monto = deposito + retiro;
            this.deposito = deposito;
            this.retiro = retiro;
            this.formaconciliacion = formaconciliacion;
            this.statusconcepto = statusconcepto;
            this.statusconciliacion = statusconciliacion;

            this.foperacion = foperacion;
            this.fmovimiento = fmovimiento;

            this.completo = false;
            this.diferencia = diferencia;
            this.coninterno = coninterno;
            this.ubicacionicono = ubicacionicono;
            this.motivonoconciliado = 0;
            this.comentarionoconciliado = "";

            this.cheque = cheque;
            this.referencia = referencia;
            this.nombretercero = nombretercero;
            this.rfctercero = rfctercero;

            this.sucursalconciliacion = sucursalconciliacion;

            this.selecciona = true;

        }

        public ReferenciaNoConciliada(int corporativo, int sucursal, string sucursaldes, int añoconciliacion, int folio, int secuencia,
          string descripcion, string concepto, decimal deposito, decimal retiro, short formaconciliacion,
          short statusconcepto, string statusconciliacion, DateTime foperacion, DateTime fmovimiento, string ubicacionicono, int folioconciliacion,
          short mesconciliacion, decimal diferencia, bool coninterno,
          string cheque, string referencia, string nombretercero, string rfctercero, int sucursalconciliacion,
          int año, int tipocobro, int tipocobroanterior, int motivonoconciliado,
          MensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;

            this.folioconciliacion = folioconciliacion;
            this.mesconciliacion = mesconciliacion;

            this.corporativo = corporativo;
            this.sucursal = sucursal;
            this.sucursaldes = sucursaldes;
            this.añoconciliacion = añoconciliacion;
            this.año = año;
            this.folio = folio;
            this.secuencia = secuencia;
            this.descripcion = descripcion;
            this.concepto = concepto;

            this.monto = deposito + retiro;
            this.deposito = deposito;
            this.retiro = retiro;
            this.formaconciliacion = formaconciliacion;
            this.statusconcepto = statusconcepto;
            this.statusconciliacion = statusconciliacion;

            this.foperacion = foperacion;
            this.fmovimiento = fmovimiento;

            this.completo = false;
            this.diferencia = diferencia;
            this.coninterno = coninterno;
            this.ubicacionicono = ubicacionicono;
            this.motivonoconciliado = motivonoconciliado;
            this.comentarionoconciliado = "";

            this.cheque = cheque;
            this.referencia = referencia;
            this.nombretercero = nombretercero;
            this.rfctercero = rfctercero;

            this.sucursalconciliacion = sucursalconciliacion;

            this.tipoCobro = tipocobro;
            this.tipoCobroAnterior = tipocobroanterior;

            this.selecciona = true;

        }

        public ReferenciaNoConciliada(int corporativo, int sucursal, string sucursaldes, int añoconciliacion, int folio, int secuencia,
          string descripcion, string concepto, decimal deposito, decimal retiro, short formaconciliacion,
          short statusconcepto, string statusconciliacion, DateTime foperacion, DateTime fmovimiento, int folioconciliacion,
           short mesconciliacion, bool coninterno, List<cReferencia> listareferenciaconciliada,
           string cheque, string referencia, string nombretercero, string rfctercero, int sucursalconciliacion,
           int año, MensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;

            this.corporativo = corporativo;
            this.sucursal = sucursal;
            this.sucursaldes = sucursaldes;
            this.añoconciliacion = añoconciliacion;
            this.año = año;
            this.folio = folio;
            this.secuencia = secuencia;
            this.descripcion = descripcion;
            this.concepto = concepto;

            this.folioconciliacion = folioconciliacion;
            this.mesconciliacion = mesconciliacion;

            this.monto = deposito + retiro;
            this.deposito = deposito;
            this.retiro = retiro;
            this.formaconciliacion = formaconciliacion;
            this.statusconcepto = statusconcepto;
            this.statusconciliacion = statusconciliacion;

            this.foperacion = foperacion;
            this.fmovimiento = fmovimiento;

            this.completo = false;
            this.listareferenciaconciliada = listareferenciaconciliada;
            this.coninterno = coninterno;
            this.motivonoconciliado = 0;
            this.comentarionoconciliado = "";

            this.cheque = cheque;
            this.referencia = referencia;
            this.nombretercero = nombretercero;
            this.rfctercero = rfctercero;

            this.sucursalconciliacion = sucursalconciliacion;

            this.selecciona = true;

        }


        public ReferenciaNoConciliada(int corporativo, int sucursal, string sucursaldes, int añoconciliacion, int folio, int secuencia,
           string descripcion, string concepto, decimal deposito, decimal retiro, short formaconciliacion,
           short statusconcepto, string statusconciliacion, DateTime foperacion, DateTime fmovimiento, int folioconciliacion,
            short mesconciliacion, bool coninterno, List<cReferencia> listareferenciaconciliada,
            string cheque, string referencia, string nombretercero, string rfctercero, int sucursalconciliacion, string ubicacionicono,
            int año, DetalleSaldoAFavor DetalleSaldo, string statusmovimiento, MensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;

            this.corporativo = corporativo;
            this.sucursal = sucursal;
            this.sucursaldes = sucursaldes;
            this.añoconciliacion = añoconciliacion;
            this.año = año;
            this.folio = folio;
            this.secuencia = secuencia;
            this.descripcion = descripcion;
            this.concepto = concepto;

            this.folioconciliacion = folioconciliacion;
            this.mesconciliacion = mesconciliacion;

            this.monto = deposito + retiro;
            this.deposito = deposito;
            this.retiro = retiro;
            this.formaconciliacion = formaconciliacion;
            this.statusconcepto = statusconcepto;
            this.statusconciliacion = statusconciliacion;

            this.foperacion = foperacion;
            this.fmovimiento = fmovimiento;
            this.ubicacionicono = ubicacionicono;
            this.completo = false;
            this.listareferenciaconciliada = listareferenciaconciliada;
            this.coninterno = coninterno;
            this.motivonoconciliado = 0;
            this.comentarionoconciliado = "";

            this.cheque = cheque;
            this.referencia = referencia;
            this.nombretercero = nombretercero;
            this.rfctercero = rfctercero;

            this.sucursalconciliacion = sucursalconciliacion;

            this.selecciona = true;

            this.DetalleSaldo = DetalleSaldo;
            this.StatusMovimiento = statusmovimiento;
        }

        //************************ NUEVO CONSTRUCTOR CONCILIACION COMPARTIDA***********************************************
        public ReferenciaNoConciliada(int corporativo, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion,
                                     int corporativoex, int sucursalex, int añoex, int folioex, int secuenciaex, int consecutivoflujo,
                                     bool coninterno, short statusconcepto, string statusconciliacion, string ubicacionicono,
                                     DateTime foperacion, DateTime fmovimiento, string referencia, string descripcion,
                                     decimal deposito, decimal retiro, decimal saldo, int caja, string sucursalbancaria,
                                     string tipotraspaso, decimal? montotraspaso, int? corporativotraspaso, int? sucursaltraspaso, int? añotraspaso, int? foliotraspaso,//int motivonoconciliado,string comentarionoconciliado,
                                     List<ReferenciaConciliadaCompartida> listareferenciaconciliadacompartida,
                                     MensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;

            this.corporativo = corporativo;
            this.sucursalconciliacion = sucursalconciliacion;
            this.añoconciliacion = añoconciliacion;
            this.mesconciliacion = mesconciliacion;
            this.folioconciliacion = folioconciliacion;


            this.sucursal = sucursalex;
            this.año = añoex;
            this.folio = folioex;
            this.secuencia = secuenciaex;
            this.consecutivoflujo = consecutivoflujo;

            this.descripcion = descripcion;

            this.monto = deposito + retiro;
            this.deposito = deposito;
            this.retiro = retiro;
            this.caja = caja;
            this.saldo = saldo;
            this.statusconcepto = statusconcepto;
            this.statusconciliacion = statusconciliacion;
            this.ubicacionicono = ubicacionicono;
            this.sucursalbancaria = sucursalbancaria;
            this.foperacion = foperacion;
            this.fmovimiento = fmovimiento;

            this.completo = false;
            this.listareferenciaconciliadacompartida = listareferenciaconciliadacompartida;
            this.coninterno = coninterno;



            this.referencia = referencia;
            this.montotraspaso = montotraspaso;
            this.tipotraspaso = tipotraspaso;
            this.corporativotraspaso = corporativotraspaso;
            this.sucursaltraspaso = sucursaltraspaso;
            this.añotraspaso = añotraspaso;
            this.foliotraspaso = foliotraspaso;

        }

        public ReferenciaNoConciliada(MensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.corporativo = 0;
            this.sucursal = 0;
            this.añoconciliacion = 0;
            this.año = 0;
            this.folio = 0;
            this.secuencia = 0;
            this.concepto = "";
            this.monto = 0;
            this.formaconciliacion = 0;
            this.statusconcepto = 0;
            this.statusconciliacion = "";

            this.completo = false;

            this.diferencia = 0;
            this.coninterno = false;
            this.motivonoconciliado = 0;
            this.comentarionoconciliado = "";

            this.cheque = "";
            this.referencia = "";
            this.nombretercero = "";
            this.rfctercero = "";

            this.sucursalconciliacion = 0;

            this.selecciona = false;

            this.DetalleSaldo = null;
        }

        public ReferenciaNoConciliada(int corporativo, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion, int corporativoex, int sucursalex, int añoex, int folioex, int secuenciaex, int consecutivoflujo, bool coninterno, short statusconcepto, string statusconciliacion, string ubicacionicono, DateTime foperacion, DateTime fmovimiento, string referencia, string descripcion, decimal deposito, decimal retiro, decimal saldo, int caja, string sucursalbancaria, string tipotraspaso, decimal? montotraspaso, int? corporativotraspaso, int? sucursaltraspaso, int? añotraspaso, int? foliotraspaso, List<ReferenciaConciliadaCompartida> listareferenciaconciliadacompartida, DetalleSaldoAFavor detalleSaldo, MensajesImplementacion implementadorMensajes)
        {
            this.corporativo = corporativo;
            this.sucursalconciliacion = sucursalconciliacion;
            this.añoconciliacion = añoconciliacion;
            this.mesconciliacion = mesconciliacion;
            this.folioconciliacion = folioconciliacion;
            this.corporativoex = corporativoex;
            this.sucursalex = sucursalex;
            this.añoex = añoex;
            this.folioex = folioex;
            this.secuenciaex = secuenciaex;
            this.consecutivoflujo = consecutivoflujo;
            this.coninterno = coninterno;
            this.statusconcepto = statusconcepto;
            this.statusconciliacion = statusconciliacion;
            this.ubicacionicono = ubicacionicono;
            this.foperacion = foperacion;
            this.fmovimiento = fmovimiento;
            this.referencia = referencia;
            this.descripcion = descripcion;
            this.deposito = deposito;
            this.retiro = retiro;
            this.saldo = saldo;
            this.caja = caja;
            this.sucursalbancaria = sucursalbancaria;
            this.tipotraspaso = tipotraspaso;
            this.montotraspaso = montotraspaso;
            this.corporativotraspaso = corporativotraspaso;
            this.sucursaltraspaso = sucursaltraspaso;
            this.añotraspaso = añotraspaso;
            this.foliotraspaso = foliotraspaso;
            this.listareferenciaconciliadacompartida = listareferenciaconciliadacompartida;
            DetalleSaldo = detalleSaldo;
            this.implementadorMensajes = implementadorMensajes;
        }

        protected ReferenciaNoConciliada(int corporativo, int sucursal, string sucursaldes, int añoconciliacion, int folio, int secuencia, string descripcion, string concepto, decimal deposito, decimal retiro, short formaconciliacion, short statusconcepto, string statusconciliacion, DateTime foperacion, DateTime fmovimiento, int folioconciliacion, short mesconciliacion, bool coninterno, List<cReferencia> listareferenciaconciliada, string cheque, string referencia, string nombretercero, string rfctercero, int sucursalconciliacion, string ubicacionicono, int año, int tipocobro, int tipocobroanterior, MensajesImplementacion implementadorMensajes)
        {
            this.corporativo = corporativo;
            this.sucursal = sucursal;
            this.sucursaldes = sucursaldes;
            this.añoconciliacion = añoconciliacion;
            this.folio = folio;
            this.secuencia = secuencia;
            this.descripcion = descripcion;
            this.concepto = concepto;
            this.deposito = deposito;
            this.retiro = retiro;
            this.formaconciliacion = formaconciliacion;
            this.statusconcepto = statusconcepto;
            this.statusconciliacion = statusconciliacion;
            this.foperacion = foperacion;
            this.fmovimiento = fmovimiento;
            this.folioconciliacion = folioconciliacion;
            this.mesconciliacion = mesconciliacion;
            this.coninterno = coninterno;
            this.listareferenciaconciliada = listareferenciaconciliada;
            this.cheque = cheque;
            this.referencia = referencia;
            this.nombretercero = nombretercero;
            this.rfctercero = rfctercero;
            this.sucursalconciliacion = sucursalconciliacion;
            this.ubicacionicono = ubicacionicono;
            this.año = año;
            this.tipoCobro = tipocobro;
            this.tipoCobroAnterior = tipocobroanterior;
            this.implementadorMensajes = implementadorMensajes;
        }

        protected ReferenciaNoConciliada(int corporativo, int sucursal, string sucursaldes, int añoconciliacion, int folio, int secuencia, string descripcion, string concepto, decimal deposito, decimal retiro, short formaconciliacion, short statusconcepto, string statusconciliacion, DateTime foperacion, DateTime fmovimiento, string ubicacionicono, int folioconciliacion, short mesconciliacion, decimal diferencia, bool coninterno, string cheque, string referencia, string nombretercero, string rfctercero, int sucursalconciliacion, int año, int cliente, int tipocobro, int tipocobroanterior, int motivonoconciliado, MensajesImplementacion implementadorMensajes)
        {
            this.corporativo = corporativo;
            this.sucursal = sucursal;
            this.sucursaldes = sucursaldes;
            this.añoconciliacion = añoconciliacion;
            this.folio = folio;
            this.secuencia = secuencia;
            this.descripcion = descripcion;
            this.concepto = concepto;
            this.deposito = deposito;
            this.retiro = retiro;
            this.formaconciliacion = formaconciliacion;
            this.statusconcepto = statusconcepto;
            this.statusconciliacion = statusconciliacion;
            this.foperacion = foperacion;
            this.fmovimiento = fmovimiento;
            this.ubicacionicono = ubicacionicono;
            this.folioconciliacion = folioconciliacion;
            this.mesconciliacion = mesconciliacion;
            this.diferencia = diferencia;
            this.coninterno = coninterno;
            this.cheque = cheque;
            this.referencia = referencia;
            this.nombretercero = nombretercero;
            this.rfctercero = rfctercero;
            this.sucursalconciliacion = sucursalconciliacion;
            this.año = año;
            this.cliente = cliente;
            this.tipoCobro = tipocobro;
            this.tipoCobroAnterior = tipocobroanterior;
            this.motivonoconciliado = motivonoconciliado;
            this.implementadorMensajes = implementadorMensajes;
        }

        //protected ReferenciaNoConciliada(MensajesImplementacion implementadorMensajes)
        //{
        //    this.implementadorMensajes = implementadorMensajes;
        //}

        #endregion

        public abstract bool Guardar();
        public abstract bool Modificar();
        public abstract bool Eliminar();
        public abstract bool EliminarReferenciaConciliada();
        public abstract bool EliminarReferenciaConciliadaPedido();
        public abstract bool EliminarReferenciaConciliadaInterno();

        public abstract bool CancelarExternoPedido();
        public abstract bool CancelarExternoInterno();
        public abstract bool CancelarInterno();

        public abstract string ValidaPedido(string PedidoReferencia);

        public abstract ReferenciaNoConciliada CrearObjeto();

        Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();

        #region Propiedades

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


        public string UbicacionIcono
        {
            get { return ubicacionicono; }
            set { ubicacionicono = value; }
        }


        public int Corporativo
        {
            get { return corporativo; }
            set { corporativo = value; }
        }

        public int SucursalConciliacion
        {
            get { return sucursalconciliacion; }
            set { sucursalconciliacion = value; }
        }

        public int Sucursal
        {
            get { return sucursal; }
            set { sucursal = value; }
        }

        public short MesConciliacion
        {
            get { return mesconciliacion; }
            set { mesconciliacion = value; }
        }

        public int FolioConciliacion
        {
            get { return folioconciliacion; }
            set { folioconciliacion = value; }
        }

        public string SucursalDes
        {
            get { return sucursaldes; }
            set { sucursaldes = value; }
        }

        public int Año
        {
            get { return año; }
            set { año = value; }
        }

        public int AñoConciliacion
        {
            get { return añoconciliacion; }
            set { añoconciliacion = value; }
        }

        public int Folio
        {
            get { return folio; }
            set { folio = value; }
        }

        public int Secuencia
        {
            get { return secuencia; }
            set { secuencia = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public string Concepto
        {
            get { return concepto; }
            set { concepto = value; }
        }


        public decimal Monto
        {
            get { return monto; }
            set { monto = value; }
        }

        public decimal Comision
        {
            get { return comision; }
            set { comision = value; }
        }

        public decimal Resto
        {
            //get { return (this.monto + this.Diferencia) - this.MontoConciliado; }
            get {
                //resto = (this.Deposito + this.Comision) - this.MontoConciliado;
                resto = (this.monto + this.Comision) - this.MontoConciliado;
                if (resto >= 0)
                    return resto;
                else
                    return 0;
            }
        }

        public decimal Deposito
        {
            get { return deposito; }
            set { deposito = value; }
        }

        public decimal Retiro
        {
            get { return retiro; }
            set { retiro = value; }
        }

        public String StatusMovimiento
        {
            get { return statusMovimiento; }
            set { statusMovimiento = value; }
        }

        public int Pedido
        {
            get { return pedido; }
            set { pedido = value; }
        }

        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public decimal MontoConciliado
        {
            get
            {
                decimal montoconciliado = 0;
                if (this.ConInterno)
                    foreach (ReferenciaConciliada referen in this.listareferenciaconciliada)
                        montoconciliado = montoconciliado + referen.MontoInterno;
                else
                    foreach (ReferenciaConciliadaPedido referen in this.listareferenciaconciliada)
                        montoconciliado = montoconciliado + referen.MontoConciliado;

                return montoconciliado;
            }
        }
        //public decimal MontoConciliado
        //{
        //    get
        //    {
        //        const decimal montoconciliado = 0;
        //        return this.ConInterno
        //            ? this.listareferenciaconciliada.Cast<ReferenciaConciliada>().Aggregate(montoconciliado, (current, referen) => current + referen.MontoInterno)
        //            : this.listareferenciaconciliada.Cast<ReferenciaConciliadaPedido>().Aggregate(montoconciliado, (current, referen) => current + referen.Total);
        //    }
        //}
        public decimal MontoPedido
        {
            get
            {
                return this.listareferenciaconciliada.Cast<ReferenciaConciliadaPedido>().Aggregate<ReferenciaConciliadaPedido, decimal>(0, (current, referen) => current + referen.MontoConciliado);
            }
        }


        public short FormaConciliacion
        {
            get { return formaconciliacion; }
            set { formaconciliacion = value; }
        }

        public short StatusConcepto
        {
            get { return statusconcepto; }
            set { statusconcepto = value; }
        }

        public string StatusConciliacion
        {
            get { return statusconciliacion; }
            set { statusconciliacion = value; }
        }

        public DateTime FOperacion
        {
            get { return foperacion; }
            set { foperacion = value; }
        }

        public DateTime FMovimiento
        {
            get { return fmovimiento; }
            set { fmovimiento = value; }
        }

        public List<cReferencia> ListaReferenciaConciliada
        {
            get { return listareferenciaconciliada; }
            set { listareferenciaconciliada = value; }
        }

        public bool Completo
        {
            get { return completo; }
            set { completo = value; }
        }

        public bool ConInterno
        {
            get { return coninterno; }
            set { coninterno = value; }
        }

        public decimal Diferencia
        {
            get { return diferencia; }
            set { diferencia = value; }
        }

        public bool MismoCliente
        {
            get
            {
                int cliente = 0;
                foreach (ReferenciaConciliadaPedido referen in this.listareferenciaconciliada)
                {
                    if ((cliente != referen.Cliente) & (cliente > 0))
                        return false;
                    cliente = referen.Cliente;
                }
                return true;
            }
        }

        public int ClientePago
        {
            set
            {
                clientepago = value;
                foreach (ReferenciaConciliadaPedido referen in this.listareferenciaconciliada)
                {
                    referen.ClientePago = clientepago;
                }
            }
            get
            {
                foreach (ReferenciaConciliadaPedido referen in this.listareferenciaconciliada)
                {
                    referen.ClientePago = clientepago;
                    break;
                }
                return clientepago;
            }
        }

        public string Cheque
        {
            get { return cheque; }
            set { cheque = value; }
        }

        public string Referencia
        {
            get { return referencia; }
            set { referencia = value; }
        }

        public string NombreTercero
        {
            get { return nombretercero; }
            set { nombretercero = value; }
        }

        public string RFCTercero
        {
            get { return rfctercero; }
            set { rfctercero = value; }
        }

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public Boolean Selecciona
        {
            get { return selecciona; }
            set { selecciona = value; }
        }

        // ********* NUEVAS PROPIEDADES ************

        public int ConsecutivoFlujo
        {
            get { return consecutivoflujo; }
            set { consecutivoflujo = value; }
        }
        public decimal Saldo
        {
            get { return saldo; }
            set { saldo = value; }
        }
        public int Caja
        {
            get { return caja; }
            set { caja = value; }
        }
        public string SucursalBancaria
        {
            get { return sucursalbancaria; }
            set { sucursalbancaria = value; }
        }
        public string TipoTraspaso
        {
            get { return tipotraspaso; }
            set { tipotraspaso = value; }
        }
        public decimal? MontoTraspaso
        {
            get { return montotraspaso; }
            set { montotraspaso = value; }
        }
        public int? AñoTraspaso
        {
            get { return añotraspaso; }
            set { añotraspaso = value; }
        }
        public int? FolioTraspaso
        {
            get { return foliotraspaso; }
            set { foliotraspaso = value; }
        }

        public int TipoCobro
        {
            get { return tipoCobro; }
            set { tipoCobro = value; }
        }

        public int TipoCobroAnterior
        {
            get { return tipoCobroAnterior; }
            set { tipoCobroAnterior = value; }
        }

        public Color ColorTraspaso
        {
            get
            {
                int r = 211;
                int g = 211;
                int b = 211;
                if (corporativotraspaso == null) return Color.FromArgb(r, g, b);
                r = Convert.ToInt32(string.Format("{0}{1}", añotraspaso, foliotraspaso));
                g = Convert.ToInt32(string.Format("{0}{1}{2}", foliotraspaso, corporativo, sucursal));
                b = Convert.ToInt32(string.Format("{0}{1}{1}", añotraspaso, foliotraspaso));
                return Color.FromArgb(r%255, g%165, b%100);
            }
        }

        public string SerieFactura { get; set; }
        public string ClienteReferencia { get; set; }


        public List<ReferenciaConciliadaCompartida> ListaReferenciaConciliadaCompartida
        {
            get { return listareferenciaconciliadacompartida; }
        }
        #endregion


        public bool AgregarReferenciaConciliada(ReferenciaNoConciliada referencia)
        {
            objApp.ImplementadorMensajes.ContenedorActual = this.implementadorMensajes.ContenedorActual;
            bool resultado = true;
            try
            {

                if (this.MontoConciliado + referencia.Monto > this.monto + this.Diferencia)
                {
                    
                    //ScriptManager.RegisterStartupScript(this.ImplementadorMensajes.ContenedorActual as System.Web.UI.Page, typeof(Page), "UpdateMsg",
                    //    @"alertify.alert('Conciliaci&oacute;n bancaria',
                    //    'Error: PRUEBA" + "', function(){ alertify.error('Error en la solicitud'); });", true);

                    objApp.ImplementadorMensajes.MostrarMensaje("El folio " + referencia.Folio + " supera el monto a conciliar: " + this.monto);
                    return false;
                }

                ReferenciaConciliada RefConciliada;
                RefConciliada = objApp.ReferenciaConciliada.CrearObjeto();

                RefConciliada.Corporativo = this.corporativo; //CoporrativoConcialicion
                RefConciliada.Sucursal = this.sucursalconciliacion;//SucursalConciliacion

                RefConciliada.AñoConciliacion = this.AñoConciliacion; //AñoConciliacion
                RefConciliada.MesConciliacion = this.mesconciliacion; //MesConciliacion
                RefConciliada.FolioConciliacion = this.folioconciliacion; //Folio de la conciliacion

                RefConciliada.AñoInterno = referencia.Año;  //Año de ReferenciaNoConciliadad
                RefConciliada.SucursalInterno = referencia.Sucursal;//Sucursa del interno
                RefConciliada.FolioInterno = referencia.Folio; //Folio del interno
                RefConciliada.SecuenciaInterno = referencia.Secuencia;//Secuencia del interno
                RefConciliada.ConceptoInterno = referencia.Concepto;
                RefConciliada.MontoInterno = referencia.Monto; //MontoInterno                    
                RefConciliada.FormaConciliacion = referencia.FormaConciliacion; //Forma de conciliacion dle interno
                RefConciliada.StatusConcepto = referencia.StatusConcepto;//Status del interno
                RefConciliada.StatusConciliacion = referencia.StatusConciliacion;//Status conciliacion del interno

                RefConciliada.Diferencia = this.monto - referencia.Monto; //Monto del externo - Monto del interno
                RefConciliada.Folio = this.folio; //Folio del externo
                RefConciliada.Secuencia = this.secuencia; //Secuencia del externo
                RefConciliada.Año = this.año; //Año externo

                RefConciliada.MontoConciliado = this.monto;//Monto del externo
                RefConciliada.FMovimientoInt = referencia.FMovimiento;//Fecha Movimiento Interno
                RefConciliada.FOperacionInt = referencia.FOperacion;//Fecha Operacion Interno

                RefConciliada.ChequeInterno = referencia.Cheque;
                RefConciliada.ReferenciaInterno = referencia.Referencia;
                RefConciliada.DescripcionInterno = referencia.Descripcion;
                RefConciliada.NombreTerceroInterno = referencia.NombreTercero;
                RefConciliada.RFCTerceroInterno = referencia.RFCTercero;
                RefConciliada.DepositoInterno = referencia.Deposito;
                RefConciliada.RetiroInterno = referencia.Retiro;

                this.listareferenciaconciliada.Add(RefConciliada);
            }
            catch (Exception ex)
            {
                resultado = false;
                throw (ex);
            }

            return resultado;
        }

        public bool AgregarReferenciaConciliadaSinVerificacion(ReferenciaNoConciliada referencia)
        {
            bool resultado = true;
            try
            {
                //if (this.MontoConciliado + referencia.Monto > this.monto + this.Diferencia)
                //{
                //    this.ImplementadorMensajes.MostrarMensaje("El folio " + referencia.Folio + " supera el monto a conciliar: " + this.monto);
                //    return false;
                //}
                ReferenciaConciliada RefConciliada;
                RefConciliada = objApp.ReferenciaConciliada.CrearObjeto();

                RefConciliada.Corporativo = this.corporativo; //CoporrativoConcialicion
                RefConciliada.Sucursal = this.sucursalconciliacion;//SucursalConciliacion

                RefConciliada.AñoConciliacion = this.AñoConciliacion; //AñoConciliacion
                RefConciliada.MesConciliacion = this.mesconciliacion; //MesConciliacion
                RefConciliada.FolioConciliacion = this.folioconciliacion; //Folio de la conciliacion

                RefConciliada.AñoInterno = referencia.Año;  //Año de ReferenciaNoConciliadad
                RefConciliada.SucursalInterno = referencia.Sucursal;//Sucursa del interno
                RefConciliada.FolioInterno = referencia.Folio; //Folio del interno
                RefConciliada.SecuenciaInterno = referencia.Secuencia;//Secuencia del interno
                RefConciliada.ConceptoInterno = referencia.Concepto;
                RefConciliada.MontoInterno = referencia.Monto; //MontoInterno                    
                RefConciliada.FormaConciliacion = referencia.FormaConciliacion; //Forma de conciliacion dle interno
                RefConciliada.StatusConcepto = referencia.StatusConcepto;//Status del interno
                RefConciliada.StatusConciliacion = referencia.StatusConciliacion;//Status conciliacion del interno

                RefConciliada.Diferencia = this.monto - referencia.Monto; //Monto del externo - Monto del interno
                RefConciliada.Folio = this.folio; //Folio del externo
                RefConciliada.Secuencia = this.secuencia; //Secuencia del externo
                RefConciliada.Año = this.año; //Año externo

                RefConciliada.MontoConciliado = this.monto;//Monto del externo
                RefConciliada.FMovimientoInt = referencia.FMovimiento;//Fecha Movimiento Interno
                RefConciliada.FOperacionInt = referencia.FOperacion;//Fecha Operacion Interno

                RefConciliada.ChequeInterno = referencia.Cheque;
                RefConciliada.ReferenciaInterno = referencia.Referencia;
                RefConciliada.DescripcionInterno = referencia.Descripcion;
                RefConciliada.NombreTerceroInterno = referencia.NombreTercero;
                RefConciliada.RFCTerceroInterno = referencia.RFCTercero;
                RefConciliada.DepositoInterno = referencia.Deposito;
                RefConciliada.RetiroInterno = referencia.Retiro;

                this.listareferenciaconciliada.Add(RefConciliada);
            }
            catch (Exception ex)
            {
                resultado = false;
                throw (ex);
            }

            return resultado;
        }

        public bool AgregarReferenciaConciliada(ReferenciaNoConciliadaPedido referencia)
        {
            bool resultado = true;
            try
            {
                if ((this.MontoConciliado + referencia.Total > this.monto + this.Diferencia) & (this.MismoClienteM(referencia.Cliente) == false))
                {
                    this.ImplementadorMensajes.MostrarMensaje("El pedido " + referencia.Pedido + " supera el monto a conciliar: " + this.monto);
                    return false;
                }
                else if (this.MontoConciliado == this.Monto)
                {
                    this.ImplementadorMensajes.MostrarMensaje("Ha acompletado el monto a conciliar, ya no puede agregar mas pedidos.");
                    return false;
                }
                ReferenciaConciliadaPedido RefConciliada;
                RefConciliada = objApp.ReferenciaConciliadaPedido.CrearObjeto();
                RefConciliada.Corporativo = this.corporativo; //CoporrativoConcialicion
                RefConciliada.Sucursal = this.sucursalconciliacion;//SucursalConciliacion
                RefConciliada.AñoConciliacion = this.AñoConciliacion; //AñoConciliacion
                RefConciliada.MesConciliacion = this.mesconciliacion; //MesConciliacion
                RefConciliada.FolioConciliacion = this.folioconciliacion; //Folio de la conciliacion

                RefConciliada.SucursalPedido = referencia.Sucursal;//Sucursa del interno
                RefConciliada.CelulaPedido = referencia.CelulaPedido; //Folio del interno
                RefConciliada.AñoPedido = referencia.AñoPedido;//Secuencia del interno
                RefConciliada.Pedido = referencia.Pedido;
                RefConciliada.ConceptoPedido = referencia.Concepto;


                RefConciliada.Total = referencia.Total; //MontoInterno                    
                RefConciliada.FormaConciliacion = referencia.FormaConciliacion; //Forma de conciliacion dle interno
                RefConciliada.StatusConcepto = referencia.StatusConcepto;//Status del interno
                RefConciliada.StatusConciliacion = referencia.StatusConciliacion;//Status conciliacion del interno

                RefConciliada.Diferencia = this.Monto - referencia.Total; //Monto del externo - Monto del interno

                RefConciliada.Folio = this.folio; //Folio del externo
                RefConciliada.Secuencia = this.secuencia; //Secuencia del externo
                RefConciliada.Año = this.año; //Año externo

                RefConciliada.MontoConciliado = this.MontoPedido + referencia.Total <= (this.Monto + this.Diferencia) ? referencia.Total : ((this.Monto) - this.MontoPedido);

                RefConciliada.Deposito = this.Monto;
                RefConciliada.FMovimiento = referencia.FMovimiento;//Fecha Movimiento 
                RefConciliada.FOperacion = referencia.FOperacion;//Fecha Operacion

                RefConciliada.Cliente = referencia.Cliente;
                RefConciliada.Nombre = referencia.Nombre;

                RefConciliada.PedidoReferencia = referencia.PedidoReferencia;

                this.listareferenciaconciliada.Add(RefConciliada);
            }
            catch (Exception ex)
            {
                resultado = false;
                throw (ex);
            }

            return resultado;
        }

        public bool AgregarReferenciaConciliadaSinVerificacion(ReferenciaNoConciliadaPedido referencia)
        {
            bool resultado = true;
            try
            {
                ReferenciaConciliadaPedido RefConciliada;
                RefConciliada = objApp.ReferenciaConciliadaPedido.CrearObjeto();
                RefConciliada.Corporativo = this.corporativo; //CoporrativoConcialicion
                RefConciliada.Sucursal = this.sucursalconciliacion;//SucursalConciliacion
                RefConciliada.AñoConciliacion = this.AñoConciliacion; //AñoConciliacion
                RefConciliada.MesConciliacion = this.mesconciliacion; //MesConciliacion
                RefConciliada.FolioConciliacion = this.folioconciliacion; //Folio de la conciliacion

                RefConciliada.SucursalPedido = referencia.Sucursal;//Sucursa del interno
                RefConciliada.CelulaPedido = referencia.CelulaPedido; //Folio del interno
                RefConciliada.AñoPedido = referencia.AñoPedido;//Secuencia del interno
                RefConciliada.Pedido = referencia.Pedido;
                RefConciliada.ConceptoPedido = referencia.Concepto;


                RefConciliada.Total = referencia.Total; //MontoInterno                    
                RefConciliada.FormaConciliacion = referencia.FormaConciliacion; //Forma de conciliacion dle interno
                RefConciliada.StatusConcepto = referencia.StatusConcepto;//Status del interno
                RefConciliada.StatusConciliacion = referencia.StatusConciliacion;//Status conciliacion del interno

                RefConciliada.Diferencia = this.Monto - referencia.Total; //Monto del externo - Monto del interno

                RefConciliada.Folio = this.folio; //Folio del externo
                RefConciliada.Secuencia = this.secuencia; //Secuencia del externo
                RefConciliada.Año = this.año; //Año externo
                
                if ((this.MontoPedido + referencia.Total <= (this.Monto+this.Comision + this.Diferencia) ? referencia.Total : ((this.Monto+this.Comision) - this.MontoPedido)) > 0)
                {
                    RefConciliada.MontoConciliado = this.MontoPedido + referencia.Total <= (this.Monto+this.Comision + this.Diferencia)
                                                    ? referencia.Total //Monto del pedido
                    //: (this.MontoPedido + referencia.Total) - this.Monto; //La diferencia
                                                    : ((this.Monto+this.Comision) - this.MontoPedido); //La diferencia
                }
                else
                    throw new Exception("El total acumulado es mayor a monto del pedido.");

                RefConciliada.Deposito = this.Monto;
                RefConciliada.FMovimiento = referencia.FMovimiento;//Fecha Movimiento 
                RefConciliada.FOperacion = referencia.FOperacion;//Fecha Operacion

                RefConciliada.Cliente = referencia.Cliente;
                RefConciliada.Nombre = referencia.Nombre;

                RefConciliada.PedidoReferencia = referencia.PedidoReferencia;

                this.listareferenciaconciliada.Add(RefConciliada);
            }
            catch (Exception ex)
            {
                resultado = false;
                throw (ex);
            }

            return resultado;
        }


        public bool QuitarReferenciaConciliada(int sucursalinterno, int añointerno, int foliointerno, int secuenciainterno)
        {
            bool resultado = true;
            try
            {
                List<ReferenciaConciliada> listareferencia = new List<ReferenciaConciliada>();//crar una nueva pero de ReferenciaConciliada
                this.listareferenciaconciliada.ForEach(c => listareferencia.Add(c as ReferenciaConciliada));
                listareferencia.RemoveAll(x => x.SucursalInterno == sucursalinterno && x.AñoInterno == añointerno &&
                                        x.FolioInterno == foliointerno && x.SecuenciaInterno == secuenciainterno);
                this.listareferenciaconciliada.Clear();
                listareferencia.ForEach(x => this.listareferenciaconciliada.Add(x as cReferencia));

            }
            catch (Exception ex)
            {
                resultado = false;
                throw (ex);
            }

            return resultado;
        }

        public bool QuitarReferenciaConciliada(int pedido, int celulapedido, int añopedido)
        {
            bool resultado = true;
            try
            {
                List<ReferenciaConciliadaPedido> listareferencia = new List<ReferenciaConciliadaPedido>();//crar una nueva pero de ReferenciaConciliadaPedido
                this.listareferenciaconciliada.ForEach(c => listareferencia.Add(c as ReferenciaConciliadaPedido));
                listareferencia.RemoveAll(x => //x.Sucursal == referencia.Sucursal && 
                                        x.AñoPedido == añopedido && x.CelulaPedido == celulapedido &&
                                        x.Pedido == pedido);
                this.listareferenciaconciliada.Clear();
                listareferencia.ForEach(x => this.listareferenciaconciliada.Add(x as cReferencia));

            }
            catch (Exception ex)
            {
                resultado = false;
                throw (ex);
            }

            return resultado;
        }

        public bool GuardarReferenciaConciliada()
        {
            bool resultado = true;
            try
            {
                if (this.coninterno)
                {
                    //Conc Manual: this = externos, this.ListaReferenciaConciliada = Internos
                    //Internos <= Externos
                    //if (this.MontoConciliado < this.Monto - this.Diferencia)
                    //{
                    //    this.ImplementadorMensajes.MostrarMensaje(
                    //        "No se puede guardar el registro. " + this.MontoConciliado + ", debe ser mayor a: " + (this.monto) + " con diferencia de +- " + (this.Diferencia));
                    //    return false;
                    //}
                    Usuario usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
                    foreach (ReferenciaConciliada referen in this.ListaReferenciaConciliada)
                    {
                        if (referen.SucursalInterno == null || referen.SucursalInterno == 0)
                        {
                            referen.SucursalInterno = this.sucursal;
                        }
                        referen.TipoCobro = this.TipoCobro;
                        referen.TipoCobroAnterior = this.TipoCobroAnterior;
                        referen.Usuario = usuario.IdUsuario;
                        referen.Guardar();
                        this.Completo = true;
                    }
                }
                else
                {
                    decimal saldoDeposito = this.Deposito + this.Comision;
                    foreach (ReferenciaConciliadaPedido referen in this.ListaReferenciaConciliada)
                    {

                        if (saldoDeposito < 0)
                            referen.MontoConciliado = 0;
                        else
                        if (saldoDeposito > referen.Total)
                            referen.MontoConciliado = referen.Total;
                        else
                        if (saldoDeposito < referen.Total)
                            referen.MontoConciliado = saldoDeposito;

                        referen.TipoCobro = this.TipoCobro;
                        referen.TipoCobroAnterior = this.TipoCobroAnterior;
                        referen.Guardar();

                        saldoDeposito = saldoDeposito - referen.Total;

                        this.Completo = true;
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                this.Completo = false;
                throw (ex);
            }


            return resultado;
        }

        public bool BorrarReferenciaConciliada()
        {
            bool resultado = true;
            try
            {
                this.ListaReferenciaConciliada.Clear();
            }
            catch (Exception ex)
            {
                resultado = false;
                this.Completo = false;
                throw (ex);
            }

            return resultado;
        }

        //public List<ReferenciaNoConciliada> ObtenerReferenciaNoConciliada()
        //{
        //    List<ReferenciaNoConciliada> datos = new List<ReferenciaNoConciliada>();
        //    try
        //    {
        //        foreach (ReferenciaConciliada referen in this.ListaReferenciaConciliada)
        //        {
        //            ReferenciaNoConciliada dato = objApp.ReferenciaNoConciliada;
        //            dato.Corporativo = referen.Corporativo;
        //            dato.Sucursal = referen.SucursalInterno;
        //            dato.SucursalDes = referen.SucursalIntDes;
        //            dato.Año = referen.Año;
        //            dato.Folio = referen.FolioInterno;
        //            dato.Secuencia = referen.SecuenciaInterno;
        //            dato.Concepto = referen.ConceptoInterno;
        //            dato.Monto = referen.MontoInterno;
        //            dato.FormaConciliacion = referen.FormaConciliacion;
        //            dato.StatusConcepto = referen.StatusConcepto;
        //            dato.StatusConciliacion = referen.StatusConciliacion;
        //            dato.FMovimiento = referen.FMovimientoInt;
        //            dato.FOperacion = referen.FOperacionInt;




        //            datos.Add(dato);
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        throw (ex);
        //    }
        //    return datos;
        //}

        //public List<ReferenciaNoConciliadaPedido> ObtenerReferenciaNoConciliadaPedido()
        //{
        //    List<ReferenciaNoConciliadaPedido> datos = new List<ReferenciaNoConciliadaPedido>();
        //    try
        //    {
        //        foreach (ReferenciaConciliadaPedido referen in this.ListaReferenciaConciliada)
        //        {
        //            ReferenciaNoConciliadaPedido dato = objApp.ReferenciaNoConciliadaPedido;
        //            dato.Corporativo = referen.Corporativo;
        //            dato.Sucursal = referen.Sucursal;
        //            dato.SucursalDes = referen.SucursalDes;
        //            dato.Año = referen.Año;
        //            dato.Folio = referen.Folio;
        //            dato.Secuencia = referen.Secuencia;
        //            dato.CelulaPedido = referen.CelulaPedido;
        //            dato.AñoPedido = referen.AñoPedido;
        //            dato.Pedido = referen.Pedido;
        //            dato.Concepto = referen.Concepto;
        //            dato.Monto = referen.Total;
        //            dato.FormaConciliacion = referen.FormaConciliacion;
        //            dato.StatusConcepto = referen.StatusConcepto;
        //            dato.StatusConciliacion = referen.StatusConciliacion;
        //            dato.FMovimiento = referen.FMovimiento;
        //            dato.FOperacion = referen.FOperacion;

        //            dato.Cliente = referen.Cliente;
        //            dato.Nombre = referen.Nombre;

        //            datos.Add(dato);
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        throw (ex);
        //    }

        //    return datos;
        //}

        public bool EliminarReferenciaConciliada(ReferenciaNoConciliada referencia)
        {
            bool resultado = true;
            try
            {
                //this.listareferenciaconciliada.RemoveAll(x => x.SucursalInterno == referencia.Sucursal &&
                //                        x.FolioInterno == referencia.Folio && x.SecuenciaInterno == referencia.Secuencia); 

                List<ReferenciaConciliada> listareferencia = new List<ReferenciaConciliada>();//crar una nueva pero de ReferenciaConciliada
                this.listareferenciaconciliada.ForEach(c => listareferencia.Add(c as ReferenciaConciliada));
                listareferencia.RemoveAll(x => x.SecuenciaInterno == referencia.Sucursal &&
                                        x.FolioInterno == referencia.Folio && x.SecuenciaInterno == referencia.Secuencia);
                this.listareferenciaconciliada.Clear();
                listareferencia.ForEach(x => this.listareferenciaconciliada.Add(x as cReferencia));

            }
            catch (Exception ex)
            {
                resultado = false;
                throw (ex);
            }

            return resultado;
        }


        public bool DesConciliar()
        {
            bool resultado = true;
            try
            {
                if (this.coninterno == true)
                {
                    this.EliminarReferenciaConciliada();
                }
                else
                {
                    this.EliminarReferenciaConciliadaPedido();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                this.Completo = false;
                objApp.ImplementadorMensajes.MostrarMensaje(ex.StackTrace);
            }


            return resultado;
        }

        public bool ExisteReferenciaConciliadaInterno(int sucursalinterno, int añointerno, int foliointerno, int secuenciainterno)
        {
            try
            {
                List<ReferenciaConciliada> listareferencia = new List<ReferenciaConciliada>();//crar una nueva pero de ReferenciaConciliadaPedido
                this.listareferenciaconciliada.ForEach(c => listareferencia.Add(c as ReferenciaConciliada));

                return listareferencia.Exists(x => x.SucursalInterno == sucursalinterno && x.AñoInterno == añointerno &&
                                        x.FolioInterno == foliointerno && x.SecuenciaInterno == secuenciainterno);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public bool ExisteReferenciaConciliadaPedido(int pedido, int celulapedido, int añopedido)
        {
            try
            {
                List<ReferenciaConciliadaPedido> listareferencia = new List<ReferenciaConciliadaPedido>();//crar una nueva pero de ReferenciaConciliadaPedido
                this.listareferenciaconciliada.ForEach(c => listareferencia.Add(c as ReferenciaConciliadaPedido));

                return listareferencia.Exists(x =>
                                        x.AñoPedido == añopedido && x.CelulaPedido == celulapedido &&
                                        x.Pedido == pedido);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public bool MismoClienteM(int cliente)
        {
            //bool mismocliente = true;
            //foreach (ReferenciaConciliadaPedido referen in this.listareferenciaconciliada)
            //{
            //    if (cliente != referen.Cliente)
            //        return false;
            //}
            //return mismocliente;

            return this.listareferenciaconciliada.Cast<ReferenciaConciliadaPedido>().All(x => cliente == x.Cliente);
        }




        //ConsultarPedidosCorrespondientes por Movimiento Externo
        public abstract List<ReferenciaConciliadaPedido> ConciliarPedidoCantidadYReferenciaMovExternoEdificios(
          decimal centavos, short statusconcepto, string campoexterno, string campopedido);

        public abstract List<ReferenciaConciliadaPedido> ConciliarPedidoCantidadYReferenciaMovExterno(
          decimal centavos, short statusconcepto, string campoexterno, string campopedido);

        public virtual string CadenaConexion
        {
            get
            {
                return objApp.CadenaConexion;
            }
        }

    }
}
