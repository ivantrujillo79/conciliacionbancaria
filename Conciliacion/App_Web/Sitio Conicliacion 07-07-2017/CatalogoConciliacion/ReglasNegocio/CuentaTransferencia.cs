using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime;
using CatalogoConciliacion.ReglasNegocio;

namespace CatalogoConciliacion.ReglasNegocio
{
    public abstract class CuentaTransferencia : EmisorMensajes
    {
        private short cuentaTransferencia;
        private string corporativoOrigenDesc;
        private short corporativoOrigen;
        private string sucursalOrigenDesc;
        private int sucursalOrigen;
        private string cuentaBancoOrigen;
        private int bancoOrigen;
        private string bancoNombreOrigen;
        private string corporativoDestinoDesc;
        private short corporativoDestino;
        private string sucursalDestinoDesc;
        private int sucursalDestino;
        private string cuentaBancoDestino;
        private int bancoDestino;
        private string bancoNombreDestino;
        private string status;
        private string usuarioAlta;
        private DateTime FAlta;


        #region constructores

        public CuentaTransferencia(IMensajesImplementacion implemntadorMensajes)
        {
            this.cuentaTransferencia = 0;
            this.corporativoOrigenDesc = "";
            this.corporativoOrigen = 0;
            this.sucursalOrigenDesc = "";
            this.sucursalOrigen = 0;
            this.cuentaBancoOrigen = "";
            this.bancoOrigen = 0;
            this.bancoNombreOrigen = "";
            this.corporativoDestinoDesc = "";
            this.corporativoDestino = 0;
            this.sucursalDestinoDesc = "";
            this.sucursalDestino = 0;
            this.cuentaBancoDestino = "";
            this.bancoDestino = 0;
            this.bancoNombreDestino = "";
            this.status = "ACTIVO";
            this.usuarioAlta = "";
            this.FAlta = DateTime.Now;
            this.implementadorMensajes = implemntadorMensajes;
        }

        public CuentaTransferencia(short cuentaTransferencia, string corporativoOrigenDesc, short corporativoOrigen, string sucursalOrigenDesc, int sucursalOrigen,
                                   string cuentaBancoOrigen, int bancoOrigen, string bancoNombreOrigen, string corporativoDestinoDesc,
                                   short corporativoDestino, string sucursalDestinoDesc, int sucursalDestino, string cuentaBancoDestino,
                                   int bancoDestino, string bancoNombreDestino, string status, string usuarioAlta,
                                   DateTime FAlta, IMensajesImplementacion implemntadorMensajes)
        {
            
            this.cuentaTransferencia = cuentaTransferencia;
            this.corporativoOrigenDesc = corporativoOrigenDesc;
            this.corporativoOrigen = corporativoOrigen;
            this.sucursalOrigenDesc = sucursalOrigenDesc;
            this.sucursalOrigen = sucursalOrigen;
            this.cuentaBancoOrigen = cuentaBancoOrigen;
            this.bancoOrigen = bancoOrigen;
            this.bancoNombreOrigen = bancoNombreOrigen;
            this.corporativoDestinoDesc = corporativoDestinoDesc;
            this.corporativoDestino = corporativoDestino;
            this.sucursalDestinoDesc = sucursalDestinoDesc;
            this.sucursalDestino = sucursalDestino;
            this.cuentaBancoDestino = cuentaBancoDestino;
            this.bancoDestino = bancoDestino;
            this.bancoNombreDestino = bancoNombreDestino;
            this.status = status;
            this.usuarioAlta = usuarioAlta;
            this.FAlta = FAlta;
            this.implementadorMensajes = implemntadorMensajes;

        }

        #endregion

        #region Propiedades

        public short CuentaTransferencia_
        {
            get { return cuentaTransferencia; }
            set { cuentaTransferencia = value; }
        }

        public string CorporativoOrigenDesc
        {
            get { return corporativoOrigenDesc; }
            set { corporativoOrigenDesc = value; }
        }

        public short CorporativoOrigen
        {
            get { return corporativoOrigen; }
            set { corporativoOrigen = value; }
        }

        public string SucursalOrigenDesc
        {
            get { return sucursalOrigenDesc; }
            set { sucursalOrigenDesc = value; }
        }

        public int SucursalOrigen
        {
            get { return sucursalOrigen; }
            set { sucursalOrigen = value; }
        }

        public string CuentaBancoOrigen
        {
            get { return cuentaBancoOrigen; }
            set { cuentaBancoOrigen = value; }
        }

        public int BancoOrigen
        {
            get { return bancoOrigen; }
            set { bancoOrigen = value; }
        }

        public string BancoNombreOrigen
        {
            get { return bancoNombreOrigen; }
            set { bancoNombreOrigen = value; }
        }

        public string CorporativoDestinoDesc
        {
            get { return corporativoDestinoDesc; }
            set { corporativoDestinoDesc = value; }
        }

        public short CorporativoDestino
        {
            get { return corporativoDestino; }
            set { corporativoDestino = value; }
        }

        public string SucursalDestinoDesc
        {
            get { return sucursalDestinoDesc; }
            set { sucursalDestinoDesc = value; }
        }

        public int SucursalDestino
        {
            get { return sucursalDestino; }
            set { sucursalDestino = value; }
        }

        public string CuentaBancoDestino
        {
            get { return cuentaBancoDestino; }
            set { cuentaBancoDestino = value; }
        }

        public int BancoDestino
        {
            get { return bancoDestino; }
            set { bancoDestino = value; }
        }

        public string BancoNombreDestino
        {
            get { return bancoNombreDestino; }
            set { bancoNombreDestino = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string UsuarioAlta
        {
            get { return usuarioAlta; }
            set { usuarioAlta = value; }
        }

        public DateTime FAlta_
        {
            get { return FAlta; }
            set { FAlta = value; }
        }

        public virtual string CadenaConexion
        {
            get { return App.CadenaConexion; }
        }

        #endregion

        #region Metodos
        public abstract CuentaTransferencia CrearObjeto();
        public abstract bool CambiarStatus(int cta_transferencia);
        public abstract bool Registrar();

        #endregion
    }
}
