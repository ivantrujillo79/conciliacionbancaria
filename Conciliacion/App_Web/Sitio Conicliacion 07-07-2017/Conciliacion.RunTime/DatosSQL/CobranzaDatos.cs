using System;
using System.Collections.Generic;
using System.Data;
using Conciliacion.RunTime.ReglasDeNegocio;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;


namespace Conciliacion.RunTime.DatosSQL
{
    public class CobranzaDatos:Cobranza
    {
       
         public CobranzaDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public CobranzaDatos(int cobranza,
            int tipoCobranza,
            DateTime fCobranza,
            string usuarioCaptura,
            int empleado,
            decimal total,
            DateTime fAlta,
            DateTime fActualizacion,
            string status,
            string observaciones,
            int cobranzaOrigen,
            string usuarioEntrega,
            DateTime fEntrega,
            string statusEntrega, IMensajesImplementacion implementadorMensajes)
            : base(cobranza,
                tipoCobranza,
                fCobranza,
                usuarioCaptura,
                empleado,
                total,
                fAlta,
                fActualizacion,
                status,
                observaciones,
                cobranzaOrigen,
                usuarioEntrega,
                fEntrega,
                statusEntrega, implementadorMensajes)
        {
        }

        public override Cobranza CrearObjeto()
        {
            return new CobranzaDatos(this.ImplementadorMensajes);
        }

        public override bool Guardar(Conexion _conexion)
        {
            bool resultado = false;
            SqlDataReader dr = null;
            try
            {
                _conexion.Comando.CommandType = System.Data.CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBGuardarCobranza";
                _conexion.Comando.Parameters.Clear();
                
                _conexion.Comando.Parameters.Add(new SqlParameter("@FCobranza", System.Data.SqlDbType.DateTime)).Value = this.FCobranza;
                _conexion.Comando.Parameters.Add(new SqlParameter("@UsuarioCaptura", System.Data.SqlDbType.VarChar)).Value = this.UsuarioCaptura;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Total", System.Data.SqlDbType.Decimal)).Value = this.Total;
                
                dr = _conexion.Comando.ExecuteReader();
                

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        this.Id = int.Parse(dr["SigCobranza"].ToString());
                        resultado = true;
                    }
                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    if (!dr.IsClosed)
                    {
                        dr.Close();
                    }
                    dr.Dispose();
                }
            }
            return resultado;
        }

        public override int GuardarProcesoCobranza(Conexion conexion)
        {
           //Conexion conexion = new Conexion();

            try
            {
               // conexion.AbrirConexion(true);

                List<ReferenciaConciliadaPedido> listaReferenciaConciliadaPedido = this.ListaReferenciaConciliadaPedido;

                decimal totalSaldo = 0;
                foreach (ReferenciaConciliadaPedido referenciaConciliadaPedido in listaReferenciaConciliadaPedido)
                {
                    totalSaldo = totalSaldo + referenciaConciliadaPedido.Total;
                }

                this.Total = totalSaldo;

                //this.Total = listaReferenciaConciliadaPedido.Aggregate(this.Total, (current, referenciaConciliadaPedido) => current = +referenciaConciliadaPedido.Saldo);

                Guardar(conexion);

                foreach (ReferenciaConciliadaPedido referenciaConciliadaPedido in listaReferenciaConciliadaPedido)
                {
                    PedidoCobranza pedidoCobranza = Conciliacion.RunTime.App.PedidoCobranza.CrearObjeto();
                    pedidoCobranza.Pedido = referenciaConciliadaPedido.Pedido;
                    pedidoCobranza.AnioPed = referenciaConciliadaPedido.AñoPedido;
                    pedidoCobranza.Celula = referenciaConciliadaPedido.CelulaPedido;
                    pedidoCobranza.Cobranza = this.Id;
                    pedidoCobranza.GestionInicial = 1;/*Gestion inicial siempre es 1*/
                    pedidoCobranza.Saldo = referenciaConciliadaPedido.Total;

                    pedidoCobranza.Guardar(conexion);
                }
                //this.ImplementadorMensajes.MostrarMensaje("El registro se guardó con éxito.");
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
            finally
            {
                //conexion.CerrarConexion();
            }

            return this.Id;
        }
    }
}
