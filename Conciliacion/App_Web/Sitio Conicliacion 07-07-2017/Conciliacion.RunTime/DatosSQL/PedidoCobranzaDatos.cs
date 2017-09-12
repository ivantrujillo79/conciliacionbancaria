using System;
using System.Collections.Generic;
using System.Data;
using Conciliacion.RunTime.ReglasDeNegocio;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Conciliacion.RunTime.DatosSQL
{
    public class PedidoCobranzaDatos : PedidoCobranza
    {
      
         public PedidoCobranzaDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public PedidoCobranzaDatos(Int16 anioPed,
            Int16 celula,
            int pedido,
            int cobranza,
            decimal saldo,
            Int16 gestionInicial, IMensajesImplementacion implementadorMensajes)
            : base(anioPed,
                celula,
                pedido,
                cobranza,
                saldo,
                gestionInicial,
                implementadorMensajes)
        {
        }

        public override PedidoCobranza CrearObjeto()
        {
            return new PedidoCobranzaDatos(this.ImplementadorMensajes);
        }


        public override bool Guardar(Conexion _conexion)
        {
            bool resultado = false;
            try
            {
                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBGuardarPedidoCobranza";
                _conexion.Comando.Parameters.Clear();

                _conexion.Comando.Parameters.Add(new SqlParameter("@AñoPed", System.Data.SqlDbType.SmallInt)).Value = this.AnioPed;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Celula", System.Data.SqlDbType.TinyInt)).Value = this.Celula;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Pedido", System.Data.SqlDbType.Int)).Value = this.Pedido;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Saldo", System.Data.SqlDbType.Money)).Value = this.Saldo;
                _conexion.Comando.Parameters.Add(new SqlParameter("@GestionInicial", System.Data.SqlDbType.TinyInt)).Value = this.GestionInicial;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Cobranza", System.Data.SqlDbType.Int)).Value = this.Cobranza;

              _conexion.Comando.ExecuteNonQuery();

              resultado = true;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return resultado;
        }
    }
}
