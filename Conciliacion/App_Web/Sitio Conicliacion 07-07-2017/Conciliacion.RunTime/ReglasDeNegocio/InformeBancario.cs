﻿using Conciliacion.RunTime.DatosSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Conciliacion.RunTime.DatosSQL.ExportadorInformeInternosAFuturoDatos;
using static Conciliacion.RunTime.DatosSQL.InformeBancarioDatos;
using static Conciliacion.RunTime.DatosSQL.InformeBancarioDatos.DetalleCuentaBanco;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class InformeBancario : EmisorMensajes
    {
        public List<Caja> consultarCajas(Conexion _conexion, short Caja)
        {
            List<Caja> ListaResultado;
            DetalleCaja objDatos = new DetalleCaja();
            ListaResultado = objDatos.consultarCajas(_conexion, Caja);
            return ListaResultado;
        }

        public List<DetalleReporteEstadoCuenta> consultaReporteEstadoCuenta(Conexion _conexion, DateTime FechaIni, DateTime FechaFin, string Banco, string CuentaBanco)
        {
            List<DetalleReporteEstadoCuenta> ListaResultado;
            DetalleReporteEstadoCuenta objDatos = new DetalleReporteEstadoCuenta();
            ListaResultado = objDatos.consultaReporteEstadoCuenta(_conexion, FechaIni, FechaFin, Banco, CuentaBanco);
            return ListaResultado;
        }

        public List<DetalleReporteEstadoCuentaDia> consultaReporteEstadoCuentadia(Conexion _conexion, DateTime FechaIni, DateTime FechaFin, string Banco, string CuentaBanco)
        {
            List<DetalleReporteEstadoCuentaDia> ListaResultado = new List<DetalleReporteEstadoCuentaDia>();
            DetalleReporteEstadoCuentaDia objDatos = new DetalleReporteEstadoCuentaDia();
            //ListaResultado = objDatos.consultaReporteEstadoCuentaPorDia(_conexion, FechaIni, FechaFin, Banco, CuentaBanco);
            return ListaResultado;
        }

        public List<DetalleReporteEstadoCuentaConciliado> consultaReporteEstadoCuentaConciliado(Conexion _conexion, DateTime FechaIni , DateTime FechaFin, string Banco , string CuentaBanco, string Status, string StatusConcepto)
        {
            List<DetalleReporteEstadoCuentaConciliado> ListaResultado;
            DetalleReporteEstadoCuentaConciliado objDatos = new DetalleReporteEstadoCuentaConciliado();
            ListaResultado = objDatos.consultaReporteEstadoCuentaConciliado(_conexion,FechaIni, FechaFin, Banco, CuentaBanco, Status, StatusConcepto);
            return ListaResultado;
        }

        public List<DetalleCuentaBanco> consultarCuentasBancarias(Conexion _conexion, short Corporativo, short Banco)
        {
            List<DetalleCuentaBanco> ListaResultado;
            DetalleCuentaBanco objDatos = new DetalleCuentaBanco();
            ListaResultado = objDatos.consultarCuentasBancarias(_conexion, Corporativo, Banco);
            return ListaResultado;
        }

        public List<DetalleBanco> consultarBancos(Conexion _conexion, int Corporativo,string Usuario )
        {
            List<DetalleBanco> ListaResultado;
            DetalleBanco objDatos = new DetalleBanco();            
            ListaResultado = objDatos.consultarBancos(_conexion,Corporativo,Usuario);
            return ListaResultado;

        }

        public InformeBancario(MensajesImplementacion implementadorMensajes)
        {
            this.implementadorMensajes = implementadorMensajes;
        }

        public abstract InformeBancario CrearObjeto();
        
        //public abstract List<DetallePosicionDiariaBancos> consultaPosicionDiariaBanco(Conexion _conexion, DateTime FechaIni, DateTime FechaFin, string Banco, string CuentaBanco, string Status, string StatusConcepto);
        public abstract List<DetallePosicionDiariaBancos> consultaPosicionDiariaBanco(Conexion _conexion, DateTime FechaIni, DateTime FechaFin, byte Caja);

        public List<DetalleInformeInternosAFuturo> consultaconsultaInformeInternosAFuturo(Conexion _conexion, DateTime FechaIni, DateTime FechaFin, string Banco, string CuentaBanco)
        {
            List<DetalleInformeInternosAFuturo> ListaResultado;
            DetalleInformeInternosAFuturo objDatos = new DetalleInformeInternosAFuturo();
            ListaResultado = objDatos.consultaInformeInternosAFuturo(_conexion, FechaIni, FechaFin, Banco, CuentaBanco);
            return ListaResultado;
        }

    }
}
