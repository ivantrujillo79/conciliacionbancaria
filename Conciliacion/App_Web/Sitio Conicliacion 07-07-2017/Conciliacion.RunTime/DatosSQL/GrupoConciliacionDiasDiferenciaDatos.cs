using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using Conciliacion.RunTime.ReglasDeNegocio;
namespace Conciliacion.RunTime.DatosSQL
{
    class GrupoConciliacionDiasDiferenciaDatos : GrupoConciliacionDiasDiferencia
    {

        public GrupoConciliacionDiasDiferenciaDatos(short grupoconciliacion,IMensajesImplementacion implementadorMensajes):
            base(grupoconciliacion,implementadorMensajes)
        {

        }

        public GrupoConciliacionDiasDiferenciaDatos(short grupoconciliacion, short diferenciadiasminima, short diferenciadiasmaxima, short diferenciadiasdefault, decimal diferenciacentavosminima, decimal diferenciacentavosmaxima, decimal diferenciacentavosdefault, IMensajesImplementacion implementadorMensajes) : 
         base(grupoconciliacion,diferenciadiasminima,diferenciadiasmaxima,diferenciadiasdefault,diferenciacentavosminima,diferenciacentavosmaxima,diferenciacentavosdefault,implementadorMensajes)
        {

        }

        public override GrupoConciliacionDiasDiferencia CrearObjeto(short grupoconciliacion)
        {
            return new GrupoConciliacionDiasDiferenciaDatos(grupoconciliacion,App.ImplementadorMensajes);
        }

        public override bool CargarDatos()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBConsultaGrupoConciliacion", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.SmallInt).Value = 3;
                    comando.Parameters.Add("@GrupoConciliacion", System.Data.SqlDbType.SmallInt).Value = this.GrupoConciliacion;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                       this.GrupoConciliacion=Convert.ToInt16(reader["GrupoConciliacion"]);
                       this.DiferenciaDiasMinima=Convert.ToInt16(reader["DiferenciaDiasMinima"]);
                       this.DiferenciaDiasMaxima=Convert.ToInt16(reader["DiferenciaDiasMaxima"]);
                       this.DiferenciaDiasDefault=Convert.ToInt16(reader["DiferenciaDiasDefault"]);
                       this.DiferenciaCentavosMinima = Convert.ToDecimal(reader["DiferenciaCentavosMinima"]);
                       this.DiferenciaCentavosMaxima = Convert.ToDecimal(reader["DiferenciaCentavosMaxima"]);
                       this.DiferenciaCentavosDefault = Convert.ToDecimal(reader["DiferenciaCentavosDefault"]);
                       resultado = true;
                    }
                }
            }
            catch (SqlException ex)
            {
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("No se pudo consultar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
                resultado = false;
            }
            return resultado;
        }

    }
}
