using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class ReferenciaNoConciliadaPedido : EmisorMensajes
    {

        int corporativo;
        int sucursal;
        short mesconciliacion;
        int folioconciliacion;
        string sucursaldes;
        int añoconciliacion;

        int celulapedido;
        int añopedido;
        int pedido;
        int cliente;
        string nombre;
	    int remisionpedido;
	    string seriepedido;
	    int foliosat;
        string seriesat;

        string concepto;
        decimal total;
        //decimal deposito;
        //decimal retiro;
        short formaconciliacion;
        short statusconcepto;
        string statusconciliacion;

        DateTime foperacion;
        DateTime fmovimiento;

        bool completo;
        decimal diferencia;

        int folio;
        int secuencia;

        string pedidoreferencia;

        bool selecciona;
        // AGREGADO PARA LA CONSULTA DE FACTURAS MANUALES
        string foliofacturaserie;
        DateTime ffactura;
        string serie;
        string foliofactura;

        //private List<ReferenciaConciliadaPedido> listareferenciaconciliadapedido = new List<ReferenciaConciliadaPedido>();
        
        #region Constructores

        public ReferenciaNoConciliadaPedido(int corporativo, int sucursal, string sucursaldes, int año,int folioconciliacion,
            short mesconciliacion, int celulapedido,int añopedido, int pedido, string pedidoreferencia, int cliente, string nombre,int remisionpedido, string seriepedido,
            int foliosat, string seriesat, string concepto,decimal total, short formaconciliacion, short statusconcepto, 
                string statusconciliacion, DateTime foperacion, DateTime fmovimiento, 
            decimal diferencia,IMensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;

            this.folioconciliacion = folioconciliacion;
            this.mesconciliacion = mesconciliacion;

            this.corporativo = corporativo;
            this.sucursal = sucursal;
            this.sucursaldes = sucursaldes;
            this.celulapedido= celulapedido;
            this.añopedido= añopedido;
            this.pedido= pedido;
            this.pedidoreferencia = pedidoreferencia;
            this.cliente= cliente;
            this.nombre = nombre;
            this.remisionpedido= remisionpedido;
            this.seriepedido= seriepedido;
            this.foliosat= foliosat;
            this.seriesat= seriesat;

            this.concepto= concepto;        
            this.total = total;
            //this.deposito = 0;
            //this.retiro = 0;
            this.formaconciliacion = formaconciliacion;
            this.statusconcepto = statusconcepto;
            this.statusconciliacion = statusconciliacion;

            this.foperacion = foperacion;
            this.fmovimiento = fmovimiento;

            this.completo = false;

            this.diferencia = diferencia;
            this.selecciona = true;
        }


        public ReferenciaNoConciliadaPedido(DateTime ffactura, int cliente, string nombrecliente, string foliofacturaserie, string serie, string foliofactura, IMensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.ffactura = ffactura;
            this.cliente = cliente;
            this.nombre = nombrecliente;
            this.foliofacturaserie = foliofacturaserie;
            this.serie = serie;
            this.foliofactura = foliofactura;
        }
        //public ReferenciaNoConciliadaPedido(int corporativo, int sucursal, string sucursaldes, int año, int folio, int secuencia, string concepto, decimal deposito, decimal retiro, 
        //    short formaconciliacion, short statusconcepto, string statusconciliacion, DateTime fmovimiento, DateTime foperacion, int folioconciliacion, short mes, 
        //    decimal diferencia, IMensajesImplementacion implementadorMensajes)
        //{
        //    this.ImplementadorMensajes = implementadorMensajes;

        //    this.folioconciliacion = folioconciliacion;
        //    this.mesconciliacion = mes;
        //    this.folio = folio;
        //    this.secuencia = secuencia;
        //    this.corporativo = corporativo;
        //    this.sucursal = sucursal;
        //    this.sucursaldes = sucursaldes;

        //    this.concepto = concepto;
        //    this.deposito = deposito;
        //    this.retiro = retiro;
        //    this.monto = retiro + deposito;
        //    this.formaconciliacion = formaconciliacion;
        //    this.statusconcepto = statusconcepto;
        //    this.statusconciliacion = statusconciliacion;

        //    this.foperacion = foperacion;
        //    this.fmovimiento = fmovimiento;

        //    this.completo = false;

        //    this.diferencia = diferencia;
        //}


      
        public ReferenciaNoConciliadaPedido(IMensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.corporativo = 0;
            this.sucursal = 0;
            this.celulapedido = 0;
            this.añopedido = 0;
            this.pedido = 0;
            this.cliente = 0;
            this.nombre = "";
            this.remisionpedido = 0;
            this.seriepedido = "";
            this.foliosat = 0;
            this.seriesat = "";

            this.concepto = "";  
            //this.monto = 0;
            this.formaconciliacion = 0;
            this.statusconcepto = 0;
            this.statusconciliacion = "";

            this.completo = false;

            this.diferencia = 0;
            this.selecciona = false;

        }

        #endregion

        public abstract bool Guardar();
        public abstract bool Modificar();
        public abstract bool Eliminar();
        public abstract ReferenciaNoConciliadaPedido CrearObjeto();

        
        #region Propiedades

        public int RemisionPedido
        {
            get { return remisionpedido; }
            set { remisionpedido = value; }
        }

        public string SeriePedido
        {
            get { return seriepedido; }
            set { seriepedido = value; }
        }

        public int FolioSat
        {
            get { return foliosat; }
            set { foliosat = value; }
        }

        public string SerieSat
        {
            get { return seriesat; }
            set { seriesat = value; }
        }


        public int Corporativo
        {
            get { return corporativo; }
            set { corporativo = value; }
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

        public int CelulaPedido
        {
            get { return celulapedido; }
            set { celulapedido = value; }
        }

        public int Año
        {
            get { return añoconciliacion; }
            set { añoconciliacion = value; }
        }

        public int AñoPedido
        {
            get { return añopedido; }
            set { añopedido = value; }
        }

        public int Pedido
        {
            get { return pedido; }
            set { pedido = value; }
        }

        public int Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Concepto
        {
            get { return concepto; }
            set { concepto = value; }
        }


        public string PedidoReferencia
        {
            get { return pedidoreferencia; }
            set { pedidoreferencia = value; }
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
            get { return statusconciliacion;}
            set {statusconciliacion =value;}
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

        public bool Completo
        {
            get { return completo; }
            set { completo = value; }
        }

        public decimal Diferencia
        {
            get { return diferencia; }
            set { diferencia = value; }
        }

        public decimal Total
        {
            get { return total; }
            set { total = value; }
        }

        public Boolean Selecciona
        {
            get { return selecciona; }
            set { selecciona = value; }
        }


        public String Foliofacturaserie
        {
            get { return foliofacturaserie; }
            set { foliofacturaserie = value; }
        }

        public DateTime Ffactura
        {
            get { return ffactura; }
            set { ffactura = value; }
        }

        public String Serie
        {
            get { return serie; }
            set { serie = value; }
        }

        public String Foliofactura
        {
            get { return foliofactura; }
            set { foliofactura = value; }
        }

        #endregion

         public virtual string CadenaConexion
        {
            get
            {
                return App.CadenaConexion;
            }
        }

    }
}
