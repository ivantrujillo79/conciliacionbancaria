using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime;
using Conciliacion.RunTime.DatosSQL;


namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class TransferenciaBancarias:EmisorMensajes
    {
        //private short corporativoOrigDest;
        //private int sucursalOrigDest;
        //private DateTime año;
        //private short folio;                
        //private string nombreCorporativo;
        //private string nombreSucursal;
        //private short banco;
        //private string nombreBancoOrigDest;
        //private string cuentaBancoOrigDest;
        //private int monto;        
        //private short entrada; //se refuere como 0 (cero) salida y 1 (uno) entrada        
        //private string status; 


        private short corporativo;
        private int sucursal;
        private int año;
        private int folio;
        private string nombreCorporativo;
        private string nombreSucursal;
        private short tipoTransferencia;
        private string referencia;
        private DateTime fMovimiento;
        private DateTime fAplicacion;
        private string usuarioCaptura;
        private DateTime fCaptura;
        private string usuariooAprobo;
        private DateTime fAprobado;
        private string status;
        private string descripcion;
        private string bancoNombreOrigen;
        private string cuentaBancoOrigen;
        private string bancoNombreDestino;
        private string cuentaBancoDestino;
        private decimal monto;
        private short entrada; //se refuere como 0 (cero) salida y 1 (uno) entrada        
        //private decimal abono;
        //private decimal cargo;


        private List<TransferenciaBancariasDetalle> listTransferenciaBancariasDetalle= new List<TransferenciaBancariasDetalle>();
        private List<TransferenciaBancariaOrigen> listTransferenciaBancariaOrigen= new List<TransferenciaBancariaOrigen>();


        public TransferenciaBancarias(MensajesImplementacion implementadorMensajes)
        {
            //this.corporativoOrigDest = 0;
            //this.sucursalOrigDest = 0;
            //this.año = DateTime.Now;
            //this.folio = 0;
            //this.nombreCorporativo = "";
            //this.nombreSucursal = "";
            //this.banco = 0;
            //this.nombreBancoOrigDest = "";
            //this.cuentaBancoOrigDest = "";
            //this.monto = 0;
            //this.entrada = 0;
            //this.status = "";
            //this.implementadorMensajes = implementadorMensajes;

            this.corporativo = 0;
            this.sucursal = 0;
            this.año = 0;
            this.folio = 0;
            this.nombreCorporativo = "";
            this.nombreSucursal = "";
            this.tipoTransferencia = 0;
            this.referencia = "";
            this.fMovimiento = DateTime.Now;
            this.fAplicacion = DateTime.Now;
            this.usuarioCaptura = "";
            this.fCaptura = DateTime.Now;
            this.usuariooAprobo = "";
            this.fAprobado = DateTime.Now;
            this.status = "CAPTURADA";
            this.descripcion = "";
            this.bancoNombreOrigen = "";
            this.cuentaBancoOrigen = "";
            this.bancoNombreDestino = "";
            this.cuentaBancoDestino = "";
            this.monto = 0;
            this.entrada = 0;

            //this.abono = 0;
            //this.cargo = 0;
            this.implementadorMensajes = implementadorMensajes;
        }

        public TransferenciaBancarias(short corporativo, int sucursal, int año, int folio,string nombreCorporativo,string nombreSucursal, short tipoTransferencia,
                                      string referencia, DateTime fMovimiento, DateTime fAplicacion,
                                      string usuarioCaptura, DateTime fCaptura, string usuariooAprobo, DateTime fAprobado,
                                      string status, string descripcion, string bancoNombreOrigen, string cuentaBancoOrigen,
                                       string bancoNombreDestino, string cuentaBancoDestino, decimal monto, short entrada,//decimal abono,decimal cargo,
                                      MensajesImplementacion implementadorMensajes)
        {
            this.corporativo = corporativo;
            this.sucursal = sucursal;
            this.año = año;
            this.folio = folio;
            this.nombreCorporativo = nombreCorporativo;
            this.nombreSucursal = nombreSucursal;
            this.tipoTransferencia = tipoTransferencia;
            this.referencia = referencia;
            this.fMovimiento = fMovimiento;
            this.fAplicacion = fAplicacion;
            this.usuarioCaptura = usuarioCaptura;
            this.fCaptura = fCaptura;
            this.usuariooAprobo = usuariooAprobo;
            this.fAprobado = fAprobado;
            this.status = status;
            this.descripcion = descripcion;
            this.bancoNombreOrigen = bancoNombreOrigen;
            this.cuentaBancoOrigen = cuentaBancoOrigen;
            this.bancoNombreDestino = bancoNombreDestino;
            this.cuentaBancoDestino = cuentaBancoDestino;
            this.monto = monto;
            this.entrada = entrada;

            //this.abono = abono;
            //this.cargo = cargo;
            this.implementadorMensajes = implementadorMensajes;
        }

        //protected TransferenciaBancarias(MensajesImplementacion implementadorMensajes)
        //{
        //    this.implementadorMensajes = implementadorMensajes;
        //}

        //public TransferenciaBancarias(short folio, short corporativoOrigDest, string nombreCorporativo, int sucursalOrigDest,
        //    string nombreSucursal, short banco, string nombreBancoOrigDest, int monto, short entrada, DateTime año, string status,
        //    MensajesImplementacion implementadorMensajes)
        //{

        //    this.corporativoOrigDest = corporativoOrigDest;
        //    this.sucursalOrigDest = sucursalOrigDest;
        //    this.año = año;
        //    this.folio = folio;
        //    this.nombreCorporativo = nombreCorporativo;
        //    this.nombreSucursal = nombreSucursal;
        //    this.banco = banco;
        //    this.nombreBancoOrigDest = nombreBancoOrigDest;
        //    this.cuentaBancoOrigDest = cuentaBancoOrigDest;
        //    this.monto = monto;
        //    this.entrada = entrada;
        //    this.status = status;
        //    this.implementadorMensajes = implementadorMensajes;
        //}

        #region Propiedades

        public short Corporativo
        {
            get { return corporativo; }
            set { corporativo = value; }
        }
        public int Sucursal
        {
            get { return sucursal; }
            set { sucursal = value; }
        }
        public int Año
        {
            get { return año; }
            set { año = value; }
        }
        public int Folio
        {
            get { return folio; }
            set { folio = value; }
        }
        public string NombreCorporativo 
        {
            get { return nombreCorporativo; }
            set { nombreCorporativo = value; }
        }
        public string NombreSucursal 
        {
            get { return nombreSucursal; }
            set { nombreSucursal = value; }
        }
        public short TipoTransferencia
        {
            get { return tipoTransferencia; }
            set { tipoTransferencia = value; }
        }
        public string Referencia
        {
            get { return referencia; }
            set { referencia = value; }
        }
        public DateTime FMovimiento
        {
            get { return fMovimiento; }
            set { fMovimiento = value; }
        }
        public DateTime FAplicacion
        {
            get { return fAplicacion; }
            set { fAplicacion = value; }
        }
        public string UsuarioCaptura
        {
            get { return usuarioCaptura; }
            set { usuarioCaptura = value; }
        }
        public DateTime FCaptura
        {
            get { return fCaptura; }
            set { fCaptura = value; }
        }
        public string UsuariooAprobo
        {
            get { return usuariooAprobo; }
            set { usuariooAprobo = value; }
        }
        public DateTime FAprobado
        {
            get { return fAprobado; }
            set { fAprobado = value; }
        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        public string BancoNombreOrigen
        {
            get { return bancoNombreOrigen; }
            set { bancoNombreOrigen = value; }
        }
        public string CuentaBancoOrigen
        {
            get { return cuentaBancoOrigen; }
            set { cuentaBancoOrigen = value; }
        }
        
        public string BancoNombreDestino
        {
            get { return bancoNombreDestino; }
            set { bancoNombreDestino = value; }
        }
        public string CuentaBancoDestino
        {
            get { return cuentaBancoDestino; }
            set { cuentaBancoDestino = value; }
        }

        public decimal Monto 
        {
            get { return monto; }
            set { monto = value; }
        }
        public short Entrada 
        {
            get { return entrada; }
            set { entrada = value; }
        }

        //public decimal Abono
        //{
        //    get { return abono; }
        //    set { abono = value; }
        //}
        
        //public decimal Cargo
        //{
        //    get { return cargo; }
        //    set { cargo = value; }
        //}

        public List<TransferenciaBancariasDetalle> ListTransferenciaBancariasDetalle
        {
            get { return listTransferenciaBancariasDetalle; }
            set { listTransferenciaBancariasDetalle = value; }
        }

        public List<TransferenciaBancariaOrigen> ListTransferenciaBancariaOrigen
        {
            get { return listTransferenciaBancariaOrigen; }
            set { listTransferenciaBancariaOrigen = value; }
        }

        

        public virtual string CadenaConexion
        {
            get { Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App(); return objApp.CadenaConexion; }
        }
        #endregion


        //#region Propiedades

        //public short Folio
        //{
        //    get { return folio; }
        //    set { folio = value; }
        //}

        //public short CorporativoOrigDest
        //{
        //    get { return corporativoOrigDest; }
        //    set { corporativoOrigDest = value; }
        //}

        //public string NombreCorporativo
        //{
        //    get { return nombreCorporativo; }
        //    set { nombreCorporativo = value; }
        //}

        //public int sucursalOrigDest
        //{
        //    get { return sucursalOrigDest; }
        //    set { sucursalOrigDest = value; }
        //}

        //public string NombreSucursal
        //{
        //    get { return nombreSucursal; }
        //    set { nombreSucursal = value; }
        //}

        //public short Banco
        //{
        //    get { return banco; }
        //    set { banco = value; }
        //}

        //public string NombreBancoOrigDest
        //{
        //    get { return nombreBancoOrigDest; }
        //    set { nombreBancoOrigDest = value; }
        //}

        //public int Monto
        //{
        //    get { return monto; }
        //    set { monto = value; }
        //}

        //public short Entrada
        //{
        //    get { return entrada; }
        //    set { entrada = value; }
        //}

        //public DateTime Año
        //{
        //    get { return año; }
        //    set { año = value; }
        //}

        //public string Status
        //{
        //    get { return status; }
        //    set { status = value; }
        //}

        //#endregion

        #region Metodos
        public abstract TransferenciaBancarias CrearObjeto();
        public abstract bool CambiarStatus(short corporativo, int sucursal, int año, int folio, string usuarioaprobo,string status);
        public abstract bool Registrar(Conexion _conexion);
        public abstract bool Guardar();
        public abstract bool AplicarTransferencia(Conexion _conexion);
        #endregion
    }
}
