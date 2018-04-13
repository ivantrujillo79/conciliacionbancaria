using Conciliacion.RunTime.DatosSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Conciliacion.RunTime.DatosSQL.InformeBancarioDatos;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class InformeBancario : EmisorMensajes
    {
        public List<DetalleReporteEstadoCuentaDia> consultaReporteEstadoCuentaPorDia(Conexion _conexion, DateTime FechaIni, DateTime FechaFin, string Banco, string CuentaBanco)
        {
            List<DetalleReporteEstadoCuentaDia> ListaResultado;
            DetalleReporteEstadoCuentaDia objDatos = new DetalleReporteEstadoCuentaDia();
            ListaResultado = objDatos.consultaReporteEstadoCuentaPorDia(_conexion, FechaIni, FechaFin, Banco, CuentaBanco);
            return ListaResultado;
        }

        public List<DetalleCuentaBanco> consultarCuentasBancarias(Conexion _conexion, short Corporativo, short Banco)
        {
            List<DetalleCuentaBanco> ListaResultado;
            DetalleCuentaBanco objDatos = new DetalleCuentaBanco();
            ListaResultado = objDatos.consultarCuentasBancarias(_conexion, Corporativo, Banco);
            return ListaResultado;
        }

        public List<DetalleBanco> consultarBancos(Conexion _conexion, int Corporativo )
        {
            List<DetalleBanco> ListaResultado;
            DetalleBanco objDatos = new DetalleBanco();            
            ListaResultado = objDatos.consultarBancos(_conexion,Corporativo);
            return ListaResultado;
        }

        public InformeBancario(IMensajesImplementacion implementadorMensajes)
        {
            this.implementadorMensajes = implementadorMensajes;
        }

        public abstract InformeBancario CrearObjeto();

        public abstract List<DetallePosicionDiariaBancos> consultaPosicionDiariaBanco(Conexion _conexion, DateTime FechaIni, DateTime FechaFin, string Banco, string CuentaBanco, string Status, string StatusConcepto);

    }
}
