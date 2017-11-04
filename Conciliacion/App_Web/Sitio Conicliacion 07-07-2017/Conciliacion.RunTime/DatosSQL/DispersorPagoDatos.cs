using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conciliacion.RunTime.ReglasDeNegocio;
using System.Data;
using System.Data.SqlClient;

namespace Conciliacion.RunTime.DatosSQL
{
    public class DispersorPagoDatos : DispersorPago
    {

        #region Constructores
        public void DispersarPagoDatos()
        {

        }
        public void DispersarPagoDatos(string clientereferencia, decimal saldoafavor, decimal montototalpago, List<PagoPropuesto> pagosporanalizar, List<PagoPropuesto> pagospropuestos)
        {
            this.ClienteReferencia = clientereferencia;
            this.SaldoAFavor = saldoafavor;
            this.MontoTotalPago = montototalpago;
            this.PagosPorAnalizar = pagosporanalizar;
            this.PagosPropuestos = pagospropuestos;
        }
        #endregion

        public override void InicializarPagos()
        {

        }

        public override bool ValidaClientes(List<PagoPropuesto> pagosavalidar, string clientereferencia, Conexion _conexion)
        {
            try
            {
                this.ClienteReferencia = clientereferencia;
                this.PagosPorAnalizar = pagosavalidar;
                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCCLConsultaVwDatosClientePorReferencia";


                _conexion.Comando.Parameters.Clear();
                _conexion.Comando.Parameters.Add(new SqlParameter("@NumeroReferencia", System.Data.SqlDbType.TinyInt)).Value = this.ClienteReferencia;
                SqlDataReader rdCliente = _conexion.Comando.ExecuteReader();

                if (rdCliente.HasRows)
                {   
                    rdCliente.Close();
                    List<PagoPropuesto> lstPagoPropuesto = DispersarATotales(PagosPorAnalizar);
                    lstPagoPropuesto = DispersarAParciales(lstPagoPropuesto);

                    this.PagosPropuestos = lstPagoPropuesto;

                    return ValidarDispersion();
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override bool ValidarDispersion()
        {
            decimal MontoAValidar = this.PagosPropuestos.Sum(x => x.MontoPropuesto);

            MontoAValidar = this.MontoTotalPago - MontoAValidar;

            if (MontoAValidar >= 0)
            {
                return true;
            }else
            {
                return false;
            }
        }

        public override List<PagoPropuesto> DispersarATotales(List<PagoPropuesto> DocumentosADispersar)
        {
            List<PagoPropuesto> lstPagoPropuesto = DocumentosADispersar
                                                //.Where(r => r.AplicarPago == false)
                                                .OrderBy(s => s.FSuministro)
                                                .OrderByDescending(c => c.SaldoPedido)
                                                .ToList();
            List<PagoPropuesto> lstPagoProcesado = new List<PagoPropuesto>();

            foreach (PagoPropuesto Pagos in lstPagoPropuesto)
            {
                if (SaldoAFavor >= Pagos.SaldoPedido)
                {
                    //SaldoAFavor = MontoTotalPago;
                    Pagos.AplicarPago = true;
                    Pagos.MontoPropuesto = Pagos.SaldoPedido;
                    SaldoAFavor = SaldoAFavor - Pagos.MontoPropuesto;

                }
                lstPagoProcesado.Add(Pagos);
            }

            return lstPagoProcesado;
        }

        public override List<PagoPropuesto> DispersarAParciales(List<PagoPropuesto> DocumentosADispersar)
        {
            if (SaldoAFavor > 0)
            {
                List<PagoPropuesto> lstPagoPropuesto = DocumentosADispersar
                    //.Where(r => r.AplicarPago == false)
                    .OrderBy(s => s.FSuministro)
                    .OrderByDescending(c => c.SaldoPedido).ToList();

                List<PagoPropuesto> lstPagoProcesado = new List<PagoPropuesto>();

                foreach (PagoPropuesto Pagos in lstPagoPropuesto)
                {
                    if (SaldoAFavor < Pagos.SaldoPedido && Pagos.AplicarPago == false)
                    {
                        //SaldoAFavor = MontoTotalPago;
                        Pagos.AplicarPago = true;
                        Pagos.MontoPropuesto = SaldoAFavor;
                        SaldoAFavor =0;
                    }
                    lstPagoProcesado.Add(Pagos);
                }
                return lstPagoProcesado;
            }
            else
            {
                return DocumentosADispersar;
            }           
        }
    }	

}//end DispersorPagoDatos