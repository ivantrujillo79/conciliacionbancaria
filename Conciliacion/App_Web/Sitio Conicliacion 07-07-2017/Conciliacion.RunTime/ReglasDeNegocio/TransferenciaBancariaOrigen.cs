using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.DatosSQL;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class TransferenciaBancariaOrigen:EmisorMensajes
    {
        private short corporativoTD;
        private short sucursalTD;
        private int añoTD;
        private int folioTD;
        private int secuenciaTD;
        private short corporativo;
        private short sucursal;
        private int año;
        private int folio;

        public TransferenciaBancariaOrigen(IMensajesImplementacion implementadorMensajes)
        {
            this.corporativoTD = 0;
            this.sucursalTD = 0;
            this.añoTD = 0;
            this.folioTD = 0;
            this.secuenciaTD=0;
            this.corporativo = 0;
            this.sucursal =0;
            this.año = 0;
            this.folio = 0;
        }

        public TransferenciaBancariaOrigen(short corporativoTD, short sucursalTD, int añoTD, int folioTD,
                                           int secuenciaTD,
                                           short corporativo, short sucursal, int año, int folio, IMensajesImplementacion implementadorMensajes)
        {
            this.corporativoTD = corporativoTD;
            this.sucursalTD = sucursalTD;
            this.añoTD = añoTD;
            this.folioTD = folioTD;
            this.secuenciaTD = secuenciaTD;
            this.corporativo = corporativo;
            this.sucursal = sucursal;
            this.año = año;
            this.folio = folio;
        }

        #region Propiedades
        public short CorporativoTD
        {
            get { return corporativoTD; }
            set { corporativoTD = value; }
        }

        public short SucursalTD
        {
            get { return sucursalTD; }
            set { sucursalTD = value; }
        }

        public int AñoTD
        {
            get { return añoTD; }
            set { añoTD = value; }
        }

        public int FolioTD
        {
            get { return folioTD; }
            set { folioTD = value; }
        }

        public int SecuenciaTD
        {
            get { return secuenciaTD; }
            set { secuenciaTD = value; }
        }

        public short Corporativo
        {
            get { return corporativo; }
            set { corporativo = value; }
        }

        public short Sucursal
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

        public virtual string CadenaConexion
        {
            get { return App.CadenaConexion; }
        }
#endregion

        public abstract TransferenciaBancariaOrigen CrearObjeto();
        public abstract bool Registrar(Conexion _conexion);
    }
}
