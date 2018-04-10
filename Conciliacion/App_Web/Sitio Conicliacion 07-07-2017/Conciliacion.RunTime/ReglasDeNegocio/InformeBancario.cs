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
        public List<DetalleCuentaBanco> consultarCuentasBancarias(Conexion _conexion, short Corporativo, short Banco)
        {
            List<DetalleCuentaBanco> ListaResultado;
            DetalleCuentaBanco objDatos = new DetalleCuentaBanco();
            ListaResultado = objDatos.consultarCuentasBancarias(_conexion, Corporativo, Banco);
            return ListaResultado;
        }

        public InformeBancario(IMensajesImplementacion implementadorMensajes)
        {
            this.implementadorMensajes = implementadorMensajes;
            //this.corporativo = "";
            //this.sucursal = "";
            //this.año = 0;
            //this.mes = 0;
            //this.cuentabancofinanciero = "";
            //this.consecutivoflujo = 0;
            //this.fecha = DateTime.MinValue;
            //this.referencia = "";
            //this.concepto = "";
            //this.retiros = 0;
            //this.depositos = 0;
            //this.saldofinal = 0;
            //this.conceptoconciliado = "";
            //this.documentoconciliado = "";
        }

        //public InformeBancario(string corporativo, string sucursal, int año, int mes, string cuentabancofinanciero, int consecutivoflujo, DateTime fecha, string referencia, string concepto, decimal retiros, decimal depositos, decimal saldofinal, string conceptoconciliado, string documentoconciliado)
        //{
        //    this.corporativo = corporativo;
        //    this.sucursal = sucursal;
        //    this.año = año;
        //    this.mes = mes;
        //    this.cuentabancofinanciero = cuentabancofinanciero;
        //    this.consecutivoflujo = consecutivoflujo;
        //    this.fecha = fecha;
        //    this.referencia = referencia;
        //    this.concepto = concepto;
        //    this.retiros = retiros;
        //    this.depositos = depositos;
        //    this.saldofinal = saldofinal;
        //    this.conceptoconciliado = conceptoconciliado;
        //    this.documentoconciliado = documentoconciliado;
        //}

        public abstract InformeBancario CrearObjeto();

        //public abstract List<InformeBancario> consultaPosicionDiariaBanco(Conexion _conexion, DateTime FechaIni, DateTime FechaFin, string Banco, string CuentaBanco, string Status, string StatusConcepto);

        public abstract List<DetallePosicionDiariaBancos> consultaPosicionDiariaBanco(Conexion _conexion, DateTime FechaIni, DateTime FechaFin, string Banco, string CuentaBanco, string Status, string StatusConcepto);

    }
}
