using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.ReglasDeNegocio;

namespace Conciliacion.RunTime.DatosSQL
{
    public class FlujoProyectadoDatos : FlujoProyectado
    {

        public FlujoProyectadoDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public FlujoProyectadoDatos(int corporativo, int año, short mes, int statusconcepto, string statusconceptodes, short tipotransferencia, DateTime fflujo, decimal importeproyectado, decimal importereal, string tipoflujo, string statuscierre)
            : base(corporativo, año, mes, statusconcepto, statusconceptodes, tipotransferencia, fflujo, importeproyectado, importereal, tipoflujo, statuscierre)
        {
        }

        public override bool ActualizarFlujoEfectivo(string usuario)
        {
            bool resultado;
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {

                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaFlujoEfectivoRealProyectado", cnn);
                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.Int).Value = this.Corporativo;
                    comando.Parameters.Add("@Año", System.Data.SqlDbType.Int).Value = this.Año;
                    comando.Parameters.Add("@Mes", System.Data.SqlDbType.TinyInt).Value = this.Mes;
                    comando.Parameters.Add("@StatusConcepto", System.Data.SqlDbType.Int).Value = this.StatusConcepto;
                    comando.Parameters.Add("@TipoTransferencia", System.Data.SqlDbType.TinyInt).Value = this.TipoTransferencia;
                    comando.Parameters.Add("@FFlujo", System.Data.SqlDbType.DateTime).Value = this.FFlujo;
                    comando.Parameters.Add("@ImporteReal", System.Data.SqlDbType.Decimal).Value = this.ImporteReal;
                    comando.Parameters.Add("@ImporteProyectado", System.Data.SqlDbType.Decimal).Value = this.ImporteProyectado;
                    comando.Parameters.Add("@TipoFlujo", System.Data.SqlDbType.VarChar).Value = this.TipoFlujo;
                    comando.Parameters.Add("@UsuarioAlta", System.Data.SqlDbType.VarChar).Value = usuario;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.ExecuteNonQuery();
                    resultado = true;
                }

            }
            catch (Exception ex)
            {
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("No se pudo guardar el registro.\n\rClase :" +
                                                          this.GetType().Name + "\n\r" + "Metodo :" +
                                                          stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" +
                                                          ex.Message);
                stackTrace = null;
                resultado = false;
            }
            return resultado;
        }
    }
}
