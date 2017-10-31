using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.PropertyGridInternal;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class ReferenciaConciliada :  cReferencia
    {
        int añoconciliacion;
        short mesconciliacion;
        int folioconciliacion;

        int añointerno;
        int sucursalinterno;
        string sucursalintdes;
        int foliointerno;
        int secuenciainterno;
        string conceptointerno;
        decimal montointerno;
        DateTime foperacionint;
        DateTime fmovimientoint;

        Boolean selecciona;

        string chequeinterno;
        string referenciainterno;
        string descripcioninterno;
        string nombretercerointerno;
        string rfctercerointerno;
        decimal depositointerno;
        decimal retirointerno;


        #region Constructores

        public ReferenciaConciliada(int corporativo, int añoconciliacion, short mesconciliacion, int folioconciliacion, 
                                    int sucursalext, string sucursalextdes, int folioext, int secuenciaext, string conceptoext, decimal montoconciliado, decimal diferencia, short formaconciliacion, short statusconcepto, string statusconciliacion,DateTime foperacionext, DateTime fmovimientoext,
                                    string chequeexterno, string referenciaexterno, string descripcionexterno, string nombreterceroexterno, string rfcterceroexterno, decimal depositoexterno, decimal retiroexterno,
                                    int sucursalinterno, string sucursalintdes, int foliointerno, int secuenciainterno, string conceptointerno, decimal montointerno,DateTime foperacionint, DateTime fmovimientoint,
                                    string chequeinterno, string referenciainterno, string descripcioninterno, string nombretercerointerno, string rfctercerointerno, decimal depositointerno, decimal retirointerno,
                                    int añoexterno, int añointerno,
                                    IMensajesImplementacion implementadorMensajes)
            : base(corporativo, sucursalext, sucursalextdes, añoexterno, folioext, secuenciaext, conceptoext, montoconciliado, diferencia, formaconciliacion, statusconcepto, statusconciliacion, foperacionext, fmovimientoext, chequeexterno, referenciaexterno, descripcionexterno, nombreterceroexterno, rfcterceroexterno, depositoexterno, retiroexterno, implementadorMensajes)
        {
            this.añoconciliacion = añoconciliacion;
            this.mesconciliacion=mesconciliacion;
            this.folioconciliacion=folioconciliacion;

            this.añointerno = añointerno;
            this.sucursalinterno = sucursalinterno;
            this.sucursalintdes=sucursalintdes;
            this.foliointerno = foliointerno;
            this.secuenciainterno = secuenciainterno;
            this.conceptointerno = conceptointerno;
            this.montointerno = montointerno;
            this.foperacionint = foperacionint;
            this.fmovimientoint = fmovimientoint;
            this.selecciona = true;

            this.chequeinterno = chequeinterno;
            this.referenciainterno = referenciainterno;
            this.descripcioninterno = descripcioninterno;
            this.nombretercerointerno = nombretercerointerno;
            this.rfctercerointerno = rfctercerointerno;
            this.depositointerno = depositointerno;
            this.retirointerno = retirointerno;
        }

        public ReferenciaConciliada(int corporativo, int añoconciliacion, short mesconciliacion, int folioconciliacion,
                                    int sucursalext, string sucursalextdes, int folioext, int secuenciaext, string conceptoext, decimal montoconciliado, decimal diferencia, short formaconciliacion, short statusconcepto, string statusconciliacion, DateTime foperacionext, DateTime fmovimientoext,
                                    string chequeexterno, string referenciaexterno, string descripcionexterno, string nombreterceroexterno, string rfcterceroexterno, decimal depositoexterno, decimal retiroexterno,
                                    int sucursalinterno, string sucursalintdes, int foliointerno, int secuenciainterno, string conceptointerno, decimal montointerno, DateTime foperacionint, DateTime fmovimientoint,
                                    string chequeinterno, string referenciainterno, string descripcioninterno, string nombretercerointerno, string rfctercerointerno, decimal depositointerno, decimal retirointerno,
                                    int añoexterno, int añointerno,string SerieFactura, string ClienteReferencia,
                                    IMensajesImplementacion implementadorMensajes)
            : base(corporativo, sucursalext, sucursalextdes, añoexterno, folioext, secuenciaext, conceptoext, montoconciliado, diferencia, formaconciliacion, statusconcepto, statusconciliacion, foperacionext, fmovimientoext, chequeexterno, referenciaexterno, descripcionexterno, nombreterceroexterno, rfcterceroexterno, depositoexterno, retiroexterno, implementadorMensajes)
        {
            this.añoconciliacion = añoconciliacion;
            this.mesconciliacion = mesconciliacion;
            this.folioconciliacion = folioconciliacion;

            this.añointerno = añointerno;
            this.sucursalinterno = sucursalinterno;
            this.sucursalintdes = sucursalintdes;
            this.foliointerno = foliointerno;
            this.secuenciainterno = secuenciainterno;
            this.conceptointerno = conceptointerno;
            this.montointerno = montointerno;
            this.foperacionint = foperacionint;
            this.fmovimientoint = fmovimientoint;
            this.selecciona = true;

            this.chequeinterno = chequeinterno;
            this.referenciainterno = referenciainterno;
            this.descripcioninterno = descripcioninterno;
            this.nombretercerointerno = nombretercerointerno;
            this.rfctercerointerno = rfctercerointerno;
            this.depositointerno = depositointerno;
            this.retirointerno = retirointerno;
            this.SerieFactura = SerieFactura;
            this.ClienteReferencia = ClienteReferencia;

        }

        public ReferenciaConciliada(IMensajesImplementacion implementadorMensajes): base(implementadorMensajes)
        {
            this.añoconciliacion = 0;
            this.mesconciliacion = 0;
            this.folioconciliacion = 0;

            this.añointerno = 0;
            this.sucursalinterno = 0;
            this.sucursalintdes = "";
            this.foliointerno = 0;
            this.secuenciainterno = 0;
            this.conceptointerno = "";
            this.montointerno = 0;

            this.chequeinterno = "";
            this.referenciainterno = "";
            this.descripcioninterno = "";
            this.nombretercerointerno = "";
            this.rfctercerointerno = "";
            this.depositointerno = 0;
            this.retirointerno = 0;
        }

        #endregion

        #region Propiedades

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

        public int AñoInterno
        {
            get { return añointerno; }
            set { añointerno = value; }
        }

        public int SucursalInterno
        {
            get { return sucursalinterno; }
            set { sucursalinterno = value; }
        }

        public string SucursalIntDes
        {
            get { return sucursalintdes; }
            set { sucursalintdes = value; }
        }

        public int FolioInterno
        {
            get { return foliointerno; }
            set { foliointerno = value; }
        }

        public int SecuenciaInterno
        {
            get { return secuenciainterno; }
            set { secuenciainterno = value; }
        }

        public string ConceptoInterno
        {
            get { return conceptointerno; }
            set { conceptointerno = value; }
        }

        public decimal MontoInterno
        {
            get { return montointerno; }
            set { montointerno = value; }
        }

        public DateTime FOperacionInt
        {
            get { return foperacionint; }
            set { foperacionint = value; }
        }

        public DateTime FMovimientoInt
        {
            get { return fmovimientoint; }
            set { fmovimientoint = value; }
        }

        public Boolean Selecciona
        {
            get { return selecciona; }
            set { selecciona = value; }
        }

        public string ChequeInterno
        {
            get { return chequeinterno; }
            set { chequeinterno = value; }
        }

        public string ReferenciaInterno
        {
            get { return referenciainterno; }
            set { referenciainterno = value; }
        }

        public string DescripcionInterno
        {
            get { return descripcioninterno; }
            set { descripcioninterno = value; }
        }

        public string NombreTerceroInterno
        {
            get { return nombretercerointerno; }
            set { nombretercerointerno = value; }
        }

        public string RFCTerceroInterno
        {
            get { return rfctercerointerno; }
            set { rfctercerointerno = value; }
        }

        public decimal DepositoInterno
        {
            get { return depositointerno; }
            set { depositointerno = value; }
        }

        public decimal RetiroInterno
        {
            get { return retirointerno; }
            set { retirointerno = value; }
        }

        public string SerieFactura { get; set; }

        public string ClienteReferencia { get; set; }

        #endregion

        public abstract ReferenciaConciliada CrearObjeto();

        public virtual string CadenaConexion
        {
            get
            {
                return App.CadenaConexion;
            }
        }

    }
}
