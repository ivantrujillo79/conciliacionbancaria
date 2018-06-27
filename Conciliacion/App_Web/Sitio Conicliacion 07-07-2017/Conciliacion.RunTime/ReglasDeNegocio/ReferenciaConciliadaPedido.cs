using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.DatosSQL;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class ReferenciaConciliadaPedido :  cReferencia
    {

        int añoconciliacion;
        short mesconciliacion;
        int folioconciliacion;
        
        int sucursalpedido;
        string sucursalpedidodes;
        int celulapedido;
        int añopedido;
        int pedido;
        int remisionpedido;
        string seriepedido;
        int foliosat;
        string seriesat;
        string conceptopedido;
        decimal total;
        string statusmovimiento;

        Boolean selecciona;

        int cliente;
        string nombre;
        string pedidoreferencia;
        byte tipoproducto;
        decimal saldo;

        bool portatil;

        #region Constructores

        public ReferenciaConciliadaPedido(int corporativo, int añoconciliacion, short mesconciliacion, int folioconciliacion, 
                                    int sucursalext, string sucursalextdes, int folioext, int secuenciaext, string conceptoext, decimal montoconciliado, decimal diferencia, short formaconciliacion, short statusconcepto, string statusconciliacion,DateTime foperacionext, DateTime fmovimientoext,
                                    string chequeexterno, string referenciaexterno, string descripcionexterno, string nombreterceroexterno, string rfcterceroexterno, decimal depositoexterno, decimal retiroexterno,
                                    int sucursalpedido,string sucursalpedidodes,int celulapedido,int añopedido, int pedido,int remisionpedido, string seriepedido,int foliosat,string seriesat, string conceptopedido, decimal total, string statusmovimiento,
                                    int cliente, string nombre,int añoexterno, IMensajesImplementacion implementadorMensajes)
            : base(corporativo, sucursalext, sucursalextdes,añoexterno,folioext,secuenciaext,conceptoext,montoconciliado,diferencia,formaconciliacion,statusconcepto,statusconciliacion,foperacionext,fmovimientoext,
            chequeexterno,referenciaexterno,descripcionexterno,nombreterceroexterno,rfcterceroexterno,depositoexterno,retiroexterno,
            implementadorMensajes)
        {
            this.añoconciliacion = añoconciliacion;
            this.mesconciliacion=mesconciliacion;
            this.folioconciliacion=folioconciliacion;

            this.sucursalpedido = sucursalpedido;
            this.sucursalpedidodes=sucursalpedidodes;
            this.celulapedido=celulapedido;
            this.añopedido=añopedido;
            this.pedido=pedido;
            this.remisionpedido=remisionpedido;
            this.seriepedido= seriepedido;
            this.foliosat=foliosat;
            this.seriesat=seriesat;
            this.total=total;
            this.conceptopedido = conceptopedido;
            this.statusmovimiento= statusmovimiento;
            this.selecciona = true;

            this.cliente = cliente;
            this.nombre = nombre;

        }

        public ReferenciaConciliadaPedido(int corporativo, int añoconciliacion, short mesconciliacion, int folioconciliacion,
                                    int sucursalext, string sucursalextdes, int folioext, int secuenciaext, string conceptoext, decimal montoconciliado, decimal diferencia, short formaconciliacion, short statusconcepto, string statusconciliacion,
                                    DateTime foperacionext, DateTime fmovimientoext,
                                    string chequeexterno, string referenciaexterno, string descripcionexterno, string nombreterceroexterno, string rfcterceroexterno, decimal depositoexterno, decimal retiroexterno,
                                    int sucursalpedido, string sucursalpedidodes, int celulapedido, int añopedido, int pedido, int remisionpedido, string seriepedido, int foliosat, string seriesat, string conceptopedido, decimal total, string statusmovimiento,
                                    int cliente, string nombre, string pedidoreferencia, int añoexterno, IMensajesImplementacion implementadorMensajes)
            : base(corporativo, sucursalext, sucursalextdes, añoexterno, folioext, secuenciaext, conceptoext, montoconciliado, diferencia, formaconciliacion, statusconcepto, statusconciliacion, foperacionext, fmovimientoext,
            chequeexterno, referenciaexterno, descripcionexterno, nombreterceroexterno, rfcterceroexterno, depositoexterno, retiroexterno,
            implementadorMensajes)
        {
            this.añoconciliacion = añoconciliacion;
            this.mesconciliacion = mesconciliacion;
            this.folioconciliacion = folioconciliacion;

            this.sucursalpedido = sucursalpedido;
            this.sucursalpedidodes = sucursalpedidodes;
            this.celulapedido = celulapedido;
            this.añopedido = añopedido;
            this.pedido = pedido;
            this.remisionpedido = remisionpedido;
            this.seriepedido = seriepedido;
            this.foliosat = foliosat;
            this.seriesat = seriesat;
            this.total = total;
            this.conceptopedido = conceptopedido;
            this.statusmovimiento = statusmovimiento;
            this.selecciona = true;

            this.cliente = cliente;
            this.nombre = nombre;
            this.pedidoreferencia = pedidoreferencia;
            
        }

        public ReferenciaConciliadaPedido(int corporativo, int añoconciliacion, short mesconciliacion, int folioconciliacion,
                                    int sucursalext, string sucursalextdes, int folioext, int secuenciaext, string conceptoext, decimal montoconciliado, decimal diferencia, short formaconciliacion, short statusconcepto, string statusconciliacion,
                                    DateTime foperacionext, DateTime fmovimientoext,
                                    string chequeexterno, string referenciaexterno, string descripcionexterno, string nombreterceroexterno, string rfcterceroexterno, decimal depositoexterno, decimal retiroexterno,
                                    int sucursalpedido, string sucursalpedidodes, int celulapedido, int añopedido, int pedido, int remisionpedido, string seriepedido, int foliosat, string seriesat, string conceptopedido, decimal total, string statusmovimiento,
                                    int cliente, string nombre, string pedidoreferencia, decimal saldo, int añoexterno, IMensajesImplementacion implementadorMensajes)
            : base(corporativo, sucursalext, sucursalextdes, añoexterno, folioext, secuenciaext, conceptoext, montoconciliado, diferencia, formaconciliacion, statusconcepto, statusconciliacion, foperacionext, fmovimientoext,
            chequeexterno, referenciaexterno, descripcionexterno, nombreterceroexterno, rfcterceroexterno, depositoexterno, retiroexterno,
            implementadorMensajes)
        {
            this.añoconciliacion = añoconciliacion;
            this.mesconciliacion = mesconciliacion;
            this.folioconciliacion = folioconciliacion;

            this.sucursalpedido = sucursalpedido;
            this.sucursalpedidodes = sucursalpedidodes;
            this.celulapedido = celulapedido;
            this.añopedido = añopedido;
            this.pedido = pedido;
            this.remisionpedido = remisionpedido;
            this.seriepedido = seriepedido;
            this.foliosat = foliosat;
            this.seriesat = seriesat;
            this.total = total;
            this.conceptopedido = conceptopedido;
            this.statusmovimiento = statusmovimiento;
            this.selecciona = true;

            this.cliente = cliente;
            this.nombre = nombre;
            this.pedidoreferencia = pedidoreferencia;
            this.saldo = saldo;

        }

      

        public ReferenciaConciliadaPedido(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
            this.añoconciliacion = 0;
            this.mesconciliacion = 0;
            this.folioconciliacion = 0;

            this.sucursalpedido = 0;
            this.sucursalpedidodes = "";
            this.celulapedido = 0;
            this.añopedido = 0;
            this.pedido = 0;
            this.remisionpedido = 0;
            this.seriepedido = "";
            this.foliosat = 0;
            this.seriesat = "";
            this.conceptopedido = "";
            this.total = 0;
            this.statusmovimiento = "PENDIENTE";
            this.cliente = 0;
            this.nombre = "";
            this.pedidoreferencia = "";

            this.tipoproducto = 0;
            this.portatil = false;
        }

        #endregion

        #region Propiedades

        public string PedidoReferencia 
        {
            get { return pedidoreferencia; }
            set { pedidoreferencia = value; }
        }
        
        public string StatusMovimiento
        {
            get { return statusmovimiento; }
            set { statusmovimiento = value; }
        }

        public int AñoConciliacion
        {
            get { return añoconciliacion; }
            set { añoconciliacion = value; }
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

        public int SucursalPedido
        {
            get { return sucursalpedido; }
            set { sucursalpedido = value; }
        }

        public string SucursalPedidoDes
        {
            get { return sucursalpedidodes; }
            set { sucursalpedidodes = value; }
        }
        
        public int CelulaPedido
        {
            get { return celulapedido; }
            set { celulapedido = value; }
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

        public int  FolioSat
        {
            get { return foliosat; }
            set { foliosat = value; }
        }

        public string SerieSat
        {
            get { return seriesat; }
            set { seriesat = value; }
        }

        public string ConceptoPedido
        {
            get { return conceptopedido; }
            set { conceptopedido = value; }
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

        public decimal Saldo
        {
            get { return saldo; }
            set { saldo = value; }
        }

        public byte TipoProducto
        {
            get { return tipoproducto; }
            set { tipoproducto = value; }
        }

        public bool Portatil
        {
            get { return portatil; }
            set { portatil = value; }
        }

        #endregion


        public abstract bool CobroPedidoAlta(short añocobro, int cobro, Conexion _conexion);

        public abstract bool PedidoActualizaSaldo(Conexion _conexion);

        public abstract bool ActualizaPagosPorAplicar(Conexion _conexion);

        public abstract bool PedidoActualizaSaldoCRM(string URLGateway);

        public abstract ReferenciaConciliadaPedido CrearObjeto();

        public virtual string CadenaConexion
        {
            get
            {
                return App.CadenaConexion;
            }
        }

    }
}
