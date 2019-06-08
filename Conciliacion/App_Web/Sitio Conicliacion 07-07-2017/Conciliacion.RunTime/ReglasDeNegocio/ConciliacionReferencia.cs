using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class ConciliacionReferencia : EmisorMensajes
    {
        private byte corporativoconciliacion;
        private byte sucursalconciliacion;
        private int añoconciliacion;
        private byte mesconciliacion;
        private int folioconciliacion;
        private int secuenciarelacion;
        private byte corporativointerno;
        private byte sucursalinterno;
        private int añointerno;
        private int foliointerno;
        private int secuenciainterno;
        private byte corporativoexterno;
        private byte sucursalexterno;
        private int añoexterno;
        private int folioexterno;
        private int secuenciaexterno;
        private string concepto;
        private decimal montoconciliado;
        private decimal diferencia;
        private decimal montoexterno;
        private decimal montointerno;
        private int formaconciliacion;
        private int statusconcepto;
        private string statusconciliacion;
        private int motivonoconciliado;
        private string comentarionoconciliado;
        private string usuario;
        private DateTime falta;
        private string descripcion;
        private string usuariostatusconcepto;
        private DateTime fstatusconcepto;

        public ConciliacionReferencia(MensajesImplementacion implementadorMensajes)
        {
            this.corporativoconciliacion = 0;
            this.sucursalconciliacion = 0;
            this.añoconciliacion = 0;
            this.mesconciliacion = 0;
            this.folioconciliacion = 0;
            this.secuenciarelacion = 0;
            this.corporativointerno = 0;
            this.sucursalinterno = 0;
            this.añointerno = 0;
            this.foliointerno = 0;
            this.secuenciainterno = 0;
            this.corporativoexterno = 0;
            this.sucursalexterno = 0;
            this.añoexterno = 0;
            this.folioexterno = 0;
            this.secuenciaexterno = 0;
            this.concepto = "";
            this.montoconciliado = 0;
            this.diferencia = 0;
            this.montoexterno = 0;
            this.montointerno = 0;
            this.formaconciliacion = 0;
            this.statusconcepto = 0;
            this.statusconciliacion = "";
            this.motivonoconciliado = 0;
            this.comentarionoconciliado = "";
            this.usuario = "";
            this.falta = DateTime.MinValue;
            this.descripcion = "";
            this.usuariostatusconcepto = "";
            this.fstatusconcepto = DateTime.MinValue;
        }

        public ConciliacionReferencia(
            byte corporativoconciliacion,
          byte sucursalconciliacion,
          int añoconciliacion,
          byte mesconciliacion,
          int folioconciliacion,
          int secuenciarelacion,
          byte corporativointerno,
          byte sucursalinterno,
          int añointerno,
          int foliointerno,
          int secuenciainterno,
          byte corporativoexterno,
          byte sucursalexterno,
          int añoexterno,
          int folioexterno,
          int secuenciaexterno,
          string concepto,
          decimal montoconciliado,
          decimal diferencia,
          decimal montoexterno,
          decimal montointerno,
          int formaconciliacion,
          int statusconcepto,
          string statusconciliacion,
          int motivonoconciliado,
          string comentarionoconciliado,
          string usuario,
          DateTime falta,
          string descripcion,
          string usuariostatusconcepto,
          DateTime fstatusconcepto, MensajesImplementacion implementadorMensajes)
        {
            this.corporativoconciliacion = corporativoconciliacion;
            this.sucursalconciliacion = sucursalconciliacion;
            this.añoconciliacion = añoconciliacion;
            this.mesconciliacion = mesconciliacion;
            this.folioconciliacion = folioconciliacion;
            this.secuenciarelacion = secuenciarelacion;
            this.corporativointerno = corporativointerno;
            this.sucursalinterno = sucursalinterno;
            this.añointerno = añointerno;
            this.foliointerno = foliointerno;
            this.secuenciainterno = secuenciainterno;
            this.corporativoexterno = corporativoexterno;
            this.sucursalexterno = sucursalexterno;
            this.añoexterno = añoexterno;
            this.folioexterno = folioexterno;
            this.secuenciaexterno = secuenciaexterno;
            this.concepto = concepto;
            this.montoconciliado = montoconciliado;
            this.diferencia = diferencia;
            this.montoexterno = montoexterno;
            this.montointerno = montointerno;
            this.formaconciliacion = formaconciliacion;
            this.statusconcepto = statusconcepto;
            this.statusconciliacion = statusconciliacion;
            this.motivonoconciliado = motivonoconciliado;
            this.comentarionoconciliado = comentarionoconciliado;
            this.usuario = usuario;
            this.falta = falta;
            this.descripcion = descripcion;
            this.usuariostatusconcepto = usuariostatusconcepto;
            this.fstatusconcepto = fstatusconcepto;
            this.implementadorMensajes = implementadorMensajes;
        }

        //protected ConciliacionReferencia(MensajesImplementacion implementadorMensajes)
        //{
        //    this.implementadorMensajes = implementadorMensajes;
        //}

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

        public byte MesConciliacion
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

        public byte CorporativoInterno
        {
            get
            {
                return corporativointerno;
            }
            set
            {
                corporativointerno = value;
            }
        }

        public byte SucursalInterno
        {
            get
            {
                return sucursalinterno;
            }
            set
            {
                sucursalinterno = value;
            }
        }

        public int AñoInterno
        {
            get
            {
                return añointerno;
            }
            set
            {
                añointerno = value;
            }
        }

        public int FolioInterno
        {
            get
            {
                return foliointerno;
            }
            set
            {
                foliointerno = value;
            }
        }

        public int SecuenciaInterno
        {
            get
            {
                return secuenciainterno;
            }
            set
            {
                secuenciainterno = value;
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

        public decimal Diferencia
        {
            get
            {
                return diferencia;
            }
            set
            {
                diferencia = value;
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

        public decimal MontoInterno
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

        public int MotivoNoConciliado
        {
            get
            {
                return motivonoconciliado;
            }
            set
            {
                motivonoconciliado = value;
            }
        }

        public string ComentarioNoConciliado
        {
            get
            {
                return comentarionoconciliado;
            }
            set
            {
                comentarionoconciliado = value;
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

        public abstract ConciliacionReferencia CrearObjeto();
        public abstract void ActualizaPagoAnticipado(byte CorporativoConciliacion, byte SucursalConciliacion, int AñoConciliacion, int MesConciliacion, int FolioConciliacion, int SecuenciaRelacion, int StatusConcepto, string StatusConciliacion, byte MotivoNoConciliado, string ComentarioNoConciliado, decimal MontoExterno);



    }
}