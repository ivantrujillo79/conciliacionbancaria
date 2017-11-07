using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conciliacion.RunTime.DatosSQL;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class RelacionCobranza : EmisorMensajes
    {
        MovimientoCaja objmovientocajadatos;
        Boolean conboveda;
        int cobranza;
        List<ReferenciaConciliadaPedido> listaPedidos = new List<ReferenciaConciliadaPedido>();
        RelacionCobranzaException objrelacioncobranzaexcepcion;

        #region Constructores

        public RelacionCobranza(IMensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
        }

        public RelacionCobranza(MovimientoCaja objMovientoCajaDatos, Boolean ConBoveda)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.ObjMovimientoCajaDatos = objMovientoCajaDatos;
            this.ConBoveda = ConBoveda;
        }
        #endregion

        #region Propiedades
        public Boolean ConBoveda
        {
            get { return conboveda; }
            set { conboveda = value; }
        }

        public int Cobranza
        {
            get { return cobranza; }
            set { cobranza = value; }
        }

        public List<ReferenciaConciliadaPedido> ListaPedidos
        {
            get { return listaPedidos; }
            set { listaPedidos = value; }
        }

        public MovimientoCaja ObjMovimientoCajaDatos
        {
            get { return objmovientocajadatos; }
            set { objmovientocajadatos = value; }
        }

        public RelacionCobranzaException ObjRelacionCobranzaExcepcion
        {
            get { return objrelacioncobranzaexcepcion; }
            set { objrelacioncobranzaexcepcion = value; }
        }
        #endregion

        public abstract RelacionCobranzaException CreaRelacionCobranza(Conexion _conexion);
        public abstract void CreaEncabezadoRelacionCobranza(Conexion _conexion);
        public abstract void CreaDetalleRelacionCobranza(Conexion _conexion);
        public abstract List<ReferenciaConciliadaPedido> AjustarPedidos(Conexion _conexion);

        public abstract RelacionCobranza CrearObjeto(MovimientoCaja objMovientoCajaDatos, Boolean ConBoveda);

        public virtual string CadenaConexion
        {
            get { return App.CadenaConexion; }
            set { App.CadenaConexion = value; }
        }
    }
}
