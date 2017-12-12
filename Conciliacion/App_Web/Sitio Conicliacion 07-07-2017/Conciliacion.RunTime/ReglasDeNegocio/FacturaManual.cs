using Conciliacion.RunTime.DatosSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class FacturaManual : EmisorMensajes 
    {
        private byte corporativoconciliacion;
        private byte sucursalconciliacion;
        private Int32 añoconciliacion;
        private Int32 mesconciliacion;
        private Int32 folioconciliacion;
        private Int32 secuenciarelacion;
        private Int32 factura;
        private byte corporativoexterno;
        private byte sucursalexterno;
        private Int32 añoexterno;
        private Int32 folioexterno;
        private Int32 secuenciaexterno;
        private string concepto; //500
        private decimal montoconciliado;
        private decimal montoexterno;
        private Int32 montointerno;
        private Int32 formaconciliacion;
        private Int32 statusconcepto;
        private string statusconciliacion; //20
        private string statusmovimiento; // 00
        private string usuario; // 5
        private DateTime falta;
        private string descripcion; // 00
        private string usuariostatusconcepto;
        private DateTime fstatusconcepto;

        public FacturaManual(IMensajesImplementacion implementadorMensajes)
        {
            this.corporativoconciliacion = 0;
            this.sucursalconciliacion = 0;
            this.añoconciliacion = 0;
            this.mesconciliacion = 0;
            this.folioconciliacion = 0;
            this.secuenciarelacion = 0;
            this.factura = 0;
            this.corporativoexterno = 0;
            this.sucursalexterno = 0;
            this.añoexterno = 0;
            this.folioexterno = 0;
            this.secuenciaexterno = 0;
            this.concepto = "";
            this.montoconciliado = 0;
            this.montoexterno = 0;
            this.montointerno = 0;
            this.formaconciliacion = 0;
            this.statusconcepto = 0;
            this.statusconciliacion = "";
            this.statusmovimiento = "";
            this.usuario = "";
            this.falta = DateTime.MinValue;
            this.descripcion = "";
            this.usuariostatusconcepto = "";
            this.fstatusconcepto = DateTime.MinValue;
            this.implementadorMensajes = implementadorMensajes;
        }

        public FacturaManual(byte corporativoconciliacion, byte sucursalconciliacion, int añoconciliacion, int mesconciliacion, int folioconciliacion, int secuenciarelacion, int factura, byte corporativoexterno, byte sucursalexterno, int añoexterno, int folioexterno, int secuenciaexterno, string concepto, decimal montoconciliado, decimal montoexterno, int montointerno, int formaconciliacion, int statusconcepto, string statusconciliacion, string statusmovimiento, string usuario, DateTime falta, string descripcion, string usuariostatusconcepto, DateTime fstatusconcepto, IMensajesImplementacion implementadorMensajes)
        {
            this.corporativoconciliacion = corporativoconciliacion;
            this.sucursalconciliacion = sucursalconciliacion;
            this.añoconciliacion = añoconciliacion;
            this.mesconciliacion = mesconciliacion;
            this.folioconciliacion = folioconciliacion;
            this.secuenciarelacion = secuenciarelacion;
            this.factura = factura;
            this.corporativoexterno = corporativoexterno;
            this.sucursalexterno = sucursalexterno;
            this.añoexterno = añoexterno;
            this.folioexterno = folioexterno;
            this.secuenciaexterno = secuenciaexterno;
            this.concepto = concepto;
            this.montoconciliado = montoconciliado;
            this.montoexterno = montoexterno;
            this.montointerno = montointerno;
            this.formaconciliacion = formaconciliacion;
            this.statusconcepto = statusconcepto;
            this.statusconciliacion = statusconciliacion;
            this.statusmovimiento = statusmovimiento;
            this.usuario = usuario;
            this.falta = falta;
            this.descripcion = descripcion;
            this.usuariostatusconcepto = usuariostatusconcepto;
            this.fstatusconcepto = fstatusconcepto;
            this.implementadorMensajes = implementadorMensajes;
        }

        public abstract FacturaManual CrearObjeto();

        public virtual string CadenaConexion
        {
            get { return App.CadenaConexion; }
        }

        public abstract bool Guardar(Conexion _conexion,
            byte corporativoconciliacion, byte sucursalconciliacion, int añoconciliacion, int mesconciliacion, int folioconciliacion, 
            int secuenciarelacion, int factura, byte corporativoexterno, byte sucursalexterno, int añoexterno, int folioexterno, 
            int secuenciaexterno, string concepto, decimal montoconciliado, decimal montoexterno, decimal montointerno, int formaconciliacion, 
            int statusconcepto, string statusconciliacion, string statusmovimiento, string usuario, DateTime falta, string descripcion, 
            string usuariostatusconcepto, DateTime fstatusconcepto);

        public byte CorporativoConciliacion
        {
            get
            {
                return corporativoconciliacion;
            }

            set
            {
                corporativoconciliacion = value;
            }
        }

        public byte SucursalConciliacion 
        {
            get
            {
                return sucursalconciliacion;
            }

            set
            {
                sucursalconciliacion = value;
            }
        }

        public int AñoConciliacion 
        {
            get
            {
                return añoconciliacion;
            }

            set
            {
                añoconciliacion = value;
            }
        }

        public int MesConciliacion 
        {
            get
            {
                return mesconciliacion;
            }

            set
            {
                mesconciliacion = value;
            }
        }

        public int FolioConciliacion 
        {
            get
            {
                return folioconciliacion;
            }

            set
            {
                folioconciliacion = value;
            }
        }

        public int SecuenciaRelacion 
        {
            get
            {
                return secuenciarelacion;
            }

            set
            {
                secuenciarelacion = value;
            }
        }

        public int Factura 
        {
            get
            {
                return factura;
            }

            set
            {
                factura = value;
            }
        }

        public byte CorporativoExterno 
        {
            get
            {
                return corporativoexterno;
            }

            set
            {
                corporativoexterno = value;
            }
        }

        public byte SucursalExterno 
        {
            get
            {
                return sucursalexterno;
            }

            set
            {
                sucursalexterno = value;
            }
        }

        public int AñoExterno 
        {
            get
            {
                return añoexterno;
            }

            set
            {
                añoexterno = value;
            }
        }

        public int FolioExterno 
        {
            get
            {
                return folioexterno;
            }

            set
            {
                folioexterno = value;
            }
        }

        public int SecuenciaExterno 
        {
            get
            {
                return secuenciaexterno;
            }

            set
            {
                secuenciaexterno = value;
            }
        }

        public string Concepto 
        {
            get
            {
                return concepto;
            }

            set
            {
                concepto = value;
            }
        }

        public decimal MontoConciliado 
        {
            get
            {
                return montoconciliado;
            }

            set
            {
                montoconciliado = value;
            }
        }

        public decimal MontoExterno 
        {
            get
            {
                return montoexterno;
            }

            set
            {
                montoexterno = value;
            }
        }

        public int MontoInt32erno 
        {
            get
            {
                return montointerno;
            }

            set
            {
                montointerno = value;
            }
        }

        public int FormaConciliacion 
        {
            get
            {
                return formaconciliacion;
            }

            set
            {
                formaconciliacion = value;
            }
        }

        public int StatusConcepto
        {
            get
            {
                return statusconcepto;
            }

            set
            {
                statusconcepto = value;
            }
        }

        public string StatusConciliacion 
        {
            get
            {
                return statusconciliacion;
            }

            set
            {
                statusconciliacion = value;
            }
        }

        public string StatusMovimiento 
        {
            get
            {
                return statusmovimiento;
            }

            set
            {
                statusmovimiento = value;
            }
        }

        public string Usuario 
        {
            get
            {
                return usuario;
            }

            set
            {
                usuario = value;
            }
        }

        public DateTime FAlta 
        {
            get
            {
                return falta;
            }

            set
            {
                falta = value;
            }
        }

        public string Descripcion 
        {
            get
            {
                return descripcion;
            }

            set
            {
                descripcion = value;
            }
        }

        public string UsuarioStatusConcepto 
        {
            get
            {
                return usuariostatusconcepto;
            }

            set
            {
                usuariostatusconcepto = value;
            }
        }

        public DateTime FStatusConcepto 
        {
            get
            {
                return fstatusconcepto;
            }

            set
            {
                fstatusconcepto = value;
            }
        }
    }
}
