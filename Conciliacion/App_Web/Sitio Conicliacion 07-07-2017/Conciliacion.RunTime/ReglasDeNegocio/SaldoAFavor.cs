using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.DatosSQL;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class SaldoAFavor : EmisorMensajes
    {
        private int foliomovimiento;
        private int añomovimiento;
        private Int16 tipomovimientoaconciliar;
        private int empresacontable;
        private short caja;
        private DateTime foperacion;
        private int tipoficha;
        private int consecutivo;
        private byte tipoaplicacioningreso;
        private int consecutivotipoaplicacion;
        private int factura;
        private Int16 añocobro;
        private int cobro;
        private decimal monto;
        private string statusmovimiento;
        private DateTime fmovimiento;
        private string statusconciliacion;
        private DateTime fconciliacion;
        private int corporativoconciliacion;
        private int sucursalconciliacion;
        private int añoconciliacion;
        private Int16 mesconciliacion;
        private int folioconciliacion;
        private int corporativoexterno;
        private int sucursalexterno;
        private int añoexterno;
        private int folioexterno;
        private int secuenciaexterno;

        public SaldoAFavor(IMensajesImplementacion implementadorMensajes)
        {
            this.implementadorMensajes = implementadorMensajes;
            this.folioconciliacion = 0;
            this.añomovimiento = 0;
            this.tipomovimientoaconciliar = 0;
            this.empresacontable = 0;
            this.caja = 0;
            this.foperacion = DateTime.MinValue;
            this.tipoficha = 0;
            this.consecutivo = 0;
            this.tipoaplicacioningreso = 0;
            this.consecutivotipoaplicacion = 0;
            this.factura = 0;
            this.añocobro = 0;
            this.cobro = 0;
            this.monto = 0;
            this.statusmovimiento = "";
            this.fmovimiento = DateTime.MinValue;
            this.statusconciliacion = "";
            this.fconciliacion = DateTime.MinValue;
            this.corporativoconciliacion = 0;
            this.sucursalconciliacion = 0;
            this.añoconciliacion = 0;
            this.mesconciliacion = 0;
            this.folioconciliacion = 0;
            this.corporativoexterno = 0;
            this.sucursalexterno = 0;
            this.añoexterno = 0;
            this.folioexterno = 0;
            this.secuenciaexterno = 0;
        }

        public SaldoAFavor(
            int foliomovimiento,
            int añomovimiento,
            Int16 tipomovimientoaconciliar,
            int empresacontable,
            short caja,
            DateTime foperacion,
            int tipoficha,
            int consecutivo,
            byte tipoaplicacioningreso,
            int consecutivotipoaplicacion,
            int factura,
            Int16 añocobro,
            int cobro,
            decimal monto,
            string statusmovimiento,
            DateTime fmovimiento,
            string statusconciliacion,
            DateTime fconciliacion,
            int corporativoconciliacion,
            int sucursalconciliacion,
            int añoconciliacion,
            Int16 mesconciliacion,
            int folioconciliacion,
            int corporativoexterno,
            int sucursalexterno,
            int añoexterno,
            int folioexterno,
            int secuenciaexterno,            
            IMensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.folioconciliacion = folioconciliacion;
            this.añomovimiento = añomovimiento;
            this.tipomovimientoaconciliar = tipomovimientoaconciliar;
            this.empresacontable = empresacontable;
            this.caja = caja;
            this.foperacion = foperacion;
            this.tipoficha = tipoficha;
            this.consecutivo = consecutivo;
            this.tipoaplicacioningreso = tipoaplicacioningreso;
            this.consecutivotipoaplicacion = consecutivotipoaplicacion;
            this.factura = factura;
            this.añocobro = añocobro;
            this.cobro = cobro;
            this.monto = monto;
            this.statusmovimiento = statusmovimiento;
            this.fmovimiento = fmovimiento;
            this.statusconciliacion = statusconciliacion;
            this.fconciliacion = fconciliacion;
            this.corporativoconciliacion = corporativoconciliacion;
            this.sucursalconciliacion = sucursalconciliacion;
            this.añoconciliacion = añoconciliacion;
            this.mesconciliacion = mesconciliacion;
            this.folioconciliacion = folioconciliacion;
            this.corporativoexterno = corporativoexterno;
            this.sucursalexterno = sucursalexterno;
            this.añoexterno = añoexterno;
            this.folioexterno = folioexterno;
            this.secuenciaexterno = secuenciaexterno;
        }

        public int FolioMovimiento
        {
            get { return foliomovimiento; }
            set { foliomovimiento = value; }
        }
        public int AñoMovimiento
        {
            get { return añomovimiento; }
            set { añomovimiento = value; }
        }
        public Int16 TipoMovimientoAConciliar
        {
            get { return tipomovimientoaconciliar; }
            set { tipomovimientoaconciliar = value; }
        }
        public int EmpresaContable
        {
            get { return empresacontable; }
            set { empresacontable = value; }
        }
        public short Caja
        {
            get { return caja; }
            set { caja = value; }
        }
        public DateTime FOperacion
        {
            get { return foperacion; }
            set { foperacion = value; }
        }
        public int TipoFicha
        {
            get { return tipoficha; }
            set { tipoficha = value; }
        }
        public int Consecutivo
        {
            get { return consecutivo; }
            set { consecutivo = value; }
        }
        public byte TipoAplicacionIngreso
        {
            get { return tipoaplicacioningreso; }
            set { tipoaplicacioningreso = value; }
        }
        public int ConsecutivoTipoAplicacion
        {
            get { return consecutivotipoaplicacion; }
            set { consecutivotipoaplicacion = value; }
        }
        public int Factura
        {
            get { return factura; }
            set { factura = value; }
        }
        public Int16 AñoCobro
        {
            get { return añocobro; }
            set { añocobro = value; }
        }
        public int Cobro
        {
            get { return cobro; }
            set { cobro = value; }
        }
        public decimal Monto
        {
            get { return monto; }
            set { monto = value; }
        }
        public string StatusMovimiento
        {
            get { return statusmovimiento; }
            set { statusmovimiento = value; }
        }
        public DateTime FMovimiento
        {
            get { return fmovimiento; }
            set { fmovimiento = value; }
        }
        public string StatusConciliacion
        {
            get { return statusconciliacion; }
            set { statusconciliacion = value; }
        }
        public DateTime FConciliacion
        {
            get { return fconciliacion; }
            set { fconciliacion = value; }
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
        public Int16 MesConciliacion
        {
            get { return mesconciliacion; }
            set { mesconciliacion = value; }
        }
        public int FolioConciliacion
        {
            get { return folioconciliacion; }
            set { folioconciliacion = value; }
        }
        public int CorporativoExterno
        {
            get { return corporativoexterno; }
            set { corporativoexterno = value; }
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

        public abstract SaldoAFavor CrearObjeto();

        public abstract List<DetalleSaldoAFavor> ConsultaSaldoAFavor(string FInicial, string FFinal, string Cliente, decimal monto, Conexion conexion);

        public abstract bool Guardar(Conexion _conexion);

        public abstract bool RegistrarCobro(Conexion _conexion);
        
        public abstract bool ExisteExterno(Conexion _conexion);

        public virtual string CadenaConexion
        {
            get { return App.CadenaConexion; }
        }

    }

    public class OpcionSaldoAFavor
    {
        public byte IDOpcion { get; set; }
        public string OpcionConciliacion { get; set; }
    }

    public class DetalleSaldoAFavor
    {
        public bool Seleccionado { get; set; }
        public int Folio  { get; set; }
        public string Cliente   { get; set; }
        public string  NombreCliente { get; set; }
        public string CuentaBancaria { get; set; }
        public string Banco { get; set; }
        public string Sucursal { get; set; }
        public string TipoCargo { get; set; }
        public bool Global { get; set; }
        public DateTime Fsaldo { get; set; }
        public decimal Importe { get; set; }
        public string Conciliada { get; set; }


        //public List<DetalleSaldoAFavor> ConsultaSaldoAFavor(string FInicial, string FFinal, string Cliente, decimal monto)
        //{
        //    List<DetalleSaldoAFavor> Lista = new List<DetalleSaldoAFavor>();

        //    return Lista;
        //}


    }
}
