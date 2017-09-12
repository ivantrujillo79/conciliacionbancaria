using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class ReferenciaConciliadaCompartida : cReferencia
    {
        //Conciliacion

        int corporativoconciliacion;
        int sucursalconciliacion;
        int añoconciliacion;
        short mesconciliacion;
        int folioconciliacion;
        int secuenciarelacion;
        //Externo

        int corporativoexterno;
        int sucursalexterno;
        int añoexterno;
        int folioexterno;
        int secuenciaexterno;
        decimal montoexterno;

        string concepto;
        short statusconcepto;
        string statusConciliacionMovimiento;
        string ubicacionicono;

        bool completo;

        //Interno
        //a.Arhivo
        int? corporativointerno;
        int? añointerno;
        int? sucursalinterno;
        int? foliointerno;
        int? secuenciainterno;
        //b.Pedido
        int? celula;
        int? añoped;
        int? pedido;

        decimal? total;
        decimal montoconciliado;
        int cliente;
        // c. Ambos

        string conceptointerno;
        string descripcioninterno;
        int consecutivoflujo;

        string motivonoconciliado;
        string comentarionoconciliado;

        private List<cReferencia> listareferenciaconciliada = new List<cReferencia>();

        #region Constructores

        public ReferenciaConciliadaCompartida(
                        int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion,
                        int secuenciarelacion,
                        int corporativo, int sucursalext, string sucursalextdes, int añoexterno, int folioext, int secuenciaext, string conceptoext,
                        decimal montoconciliado, decimal diferencia, short formaconciliacion, short statusconcepto, string statusConciliacionMovimiento,
                        DateTime foperacionext, DateTime fmovimientoext, string chequeexterno, string referenciaexterno, string descripcionexterno,
                        string nombreterceroexterno, string rfcterceroexterno, decimal depositoexterno, decimal retiroexterno,

                        int? corporativoint, int? sucursalint, int? añoint, int? folioint, int? secuenciaint,
                        int? celula, int? añoped, int? pedido, decimal? total, int cliente,
                        string conceptoint, string descripcionint, string motivonoconciliado, string comentarionoconciliado, string ubicacionicono, decimal montoexterno,

                        IMensajesImplementacion implementadorMensajes)

            : base(corporativo, sucursalext, sucursalextdes, añoexterno, folioext, secuenciaext, conceptoext, montoconciliado, diferencia, formaconciliacion, statusconcepto,
              statusConciliacionMovimiento, foperacionext, fmovimientoext, chequeexterno, referenciaexterno, descripcionexterno, nombreterceroexterno, rfcterceroexterno, depositoexterno, retiroexterno, implementadorMensajes)
        {
            this.corporativoconciliacion = corporativoconciliacion;
            this.sucursalconciliacion = sucursalconciliacion;
            this.añoconciliacion = añoconciliacion;
            this.mesconciliacion = mesconciliacion;
            this.folioconciliacion = folioconciliacion;
            this.secuenciarelacion = secuenciarelacion;

            this.corporativoexterno = corporativo;
            this.sucursalexterno = sucursalext;
            this.añoexterno = añoexterno;
            this.folioexterno = folioext;
            this.secuenciaexterno = secuenciaext;

            this.montoexterno = montoexterno;
            this.statusconcepto = statusconcepto;
            this.statusConciliacionMovimiento = statusConciliacionMovimiento;
            this.ubicacionicono = ubicacionicono;

            this.corporativointerno = corporativoint;
            this.sucursalinterno = sucursalint;
            this.añointerno = añoint;
            this.foliointerno = folioint;
            this.secuenciainterno = secuenciaint;

            this.pedido = pedido;
            this.celula = celula;
            this.añoped = añoped;
            this.total = total;
            this.cliente = cliente;

            this.conceptointerno = conceptoint;
            this.descripcioninterno = descripcionint;

            this.motivonoconciliado = motivonoconciliado;
            this.comentarionoconciliado = comentarionoconciliado;
            //this.monto = depositoexterno + retiroexterno;
            this.completo = false;
            //this.tipotraspaso = tipotraspaso;
            //this.montotraspaso = montotraspaso;

        }


        public ReferenciaConciliadaCompartida(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {


        }

        #endregion

        public abstract ReferenciaConciliada CrearObjeto();

        #region Propiedades

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

        public int SecuenciaRelacion
        {
            get { return secuenciarelacion; }
            set { secuenciarelacion = value; }
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

        public decimal MontoExterno
        {
            get { return montoexterno; }
            set { montoexterno = value; }
        }
        public string StatusConciliacionMovimiento
        {
            get { return statusConciliacionMovimiento; }
            set { statusConciliacionMovimiento = value; }
        }
        public string UbicacionIcono
        {
            get { return ubicacionicono; }
            set { ubicacionicono = value; }
        }


        public short StatusConcepto
        {
            get { return statusconcepto; }
            set { statusconcepto = value; }

        }
        public int Cliente
        {
            get { return cliente; }
            set { cliente = value; }

        }
        public string DescripcionInterno
        {
            get { return descripcioninterno; }
            set { descripcioninterno = value; }

        }

        public string ConceptoInterno
        {
            get { return conceptointerno; }
            set { conceptointerno = value; }
        }

        public string MotivoNoConciliado
        {
            get { return motivonoconciliado; }
            set { motivonoconciliado = value; }

        }

        public string ComentarioNoConciliado
        {
            get { return comentarionoconciliado; }
            set { comentarionoconciliado = value; }
        }


        public decimal MontoPedido
        {
            get
            {
                decimal montopedido = 0;
                foreach (ReferenciaConciliadaPedido referen in this.listareferenciaconciliada)
                    montopedido = montopedido + referen.MontoConciliado;

                return montopedido;
            }
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
        public List<cReferencia> ListaReferenciaConciliada
        {
            get { return listareferenciaconciliada; }
        }
        public bool Completo
        {
            get { return completo; }
            set { completo = value; }
        }
        public decimal MontoConciliado
        {
            get { return this.montoconciliado; }
            set { this.montoconciliado = value; }
        }

        //public decimal MontoConciliado
        //{
        //    get
        //    {
        //        decimal montoconciliado = 0;
        //        //if (this.ConInterno)
        //        //    foreach (ReferenciaConciliada referen in this.listareferenciaconciliada)
        //        //        montoconciliado = montoconciliado + referen.MontoInterno;
        //        //else
        //        foreach (ReferenciaConciliadaPedido referen in this.listareferenciaconciliada)
        //            montoconciliado = montoconciliado + referen.MontoConciliado;

        //        return montoconciliado;
        //    }
        //}

        //public List<cReferencia> ListaReferenciaConciliada
        //{
        //    get { return listareferenciaconciliada; }
        //}

        //public bool ConInterno
        //{
        //    get
        //    {return coninterno;}
        //}

        #endregion

        #region Metodos
        public bool AgregarReferenciaConciliadaSinVerificacion(ReferenciaNoConciliadaPedido referencia)
        {
            bool resultado = true;
            try
            {
                //if ((this.MontoConciliado + referencia.Total > this.monto + this.Diferencia) & (this.MismoClienteM(referencia.Cliente) == false))
                //{
                //    this.ImplementadorMensajes.MostrarMensaje("El pedido " + referencia.Pedido + " supera el monto a conciliar: " + this.monto);
                //    return false;
                //}
                //else if (this.MontoConciliado == this.Monto)
                //{
                //    this.ImplementadorMensajes.MostrarMensaje("Ha acompletado el monto a conciliar, ya no puede agregar mas pedidos.");
                //    return false;
                //}
                ReferenciaConciliadaPedido RefConciliada;
                RefConciliada = Conciliacion.RunTime.App.ReferenciaConciliadaPedido.CrearObjeto();
                RefConciliada.Corporativo = this.CorporativoExterno; //CoporrativoConcialicion
                RefConciliada.Sucursal = this.SucursalConciliacion;//SucursalConciliacion
                RefConciliada.AñoConciliacion = this.AñoConciliacion; //AñoConciliacion
                RefConciliada.MesConciliacion = this.MesConciliacion; //MesConciliacion
                RefConciliada.FolioConciliacion = this.FolioConciliacion; //Folio de la conciliacion

                RefConciliada.SucursalPedido = referencia.Sucursal;//Sucursa del interno
                RefConciliada.CelulaPedido = referencia.CelulaPedido; //Folio del interno
                RefConciliada.AñoPedido = referencia.AñoPedido;//Secuencia del interno
                RefConciliada.Pedido = referencia.Pedido;
                RefConciliada.ConceptoPedido = referencia.Concepto;


                RefConciliada.Total = referencia.Total; //MontoInterno                    
                RefConciliada.FormaConciliacion = referencia.FormaConciliacion; //Forma de conciliacion dle interno
                RefConciliada.StatusConcepto = referencia.StatusConcepto;//Status del interno
                RefConciliada.StatusConciliacion = referencia.StatusConciliacion;//Status conciliacion del interno

                RefConciliada.Diferencia = this.Deposito - referencia.Total; //Monto del externo - Monto del interno

                RefConciliada.Folio = this.FolioExterno; //Folio del externo
                RefConciliada.Secuencia = this.SecuenciaExterno; //Secuencia del externo
                RefConciliada.Año = this.AñoExterno; //Año externo

                RefConciliada.MontoConciliado = this.MontoPedido + referencia.Total <= (this.MontoExterno + this.Diferencia)
                                                    ? referencia.Total //Monto del pedido
                    //: (this.MontoPedido + referencia.Total) - this.Monto; //La diferencia
                                                    : ((this.MontoExterno) - this.MontoPedido); //La diferencia

                RefConciliada.Deposito = this.MontoExterno;
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

        public bool GuardarReferenciaConciliada()
        {
            bool resultado = true;
            try
            {
                if (this.MontoConciliado < this.MontoExterno - this.Diferencia & (this.MismoCliente == false))
                {
                    this.ImplementadorMensajes.MostrarMensaje("No se puede guardar el registro. El monto conciliados es: " + this.MontoConciliado + ", debe estar entre: " + (this.MontoExterno - this.Diferencia) + " y " + (this.MontoExterno + this.Diferencia));
                    return false;
                }

                foreach (ReferenciaConciliadaPedido referen in this.ListaReferenciaConciliada)
                {
                    referen.Guardar();
                    this.Completo = true;
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

        public abstract bool ActualizarStatusConceptoDescripcionConciliacionReferencia();
        public abstract bool ActualizarStatusConceptoDescripcionConciliacionPedido();
        #endregion

    }
}
