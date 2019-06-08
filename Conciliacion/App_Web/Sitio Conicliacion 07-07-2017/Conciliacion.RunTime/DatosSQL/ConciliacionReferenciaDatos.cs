using Conciliacion.RunTime.ReglasDeNegocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.DatosSQL
{
    public class ConciliacionReferenciaDatos: ConciliacionReferencia
    {
        Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();

        public ConciliacionReferenciaDatos(MensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        //public ConciliacionReferenciaDatos(
        //    byte corporativoconciliacion,
        //    byte sucursalconciliacion,
        //    int añoconciliacion,
        //    byte mesconciliacion,
        //    int folioconciliacion,
        //    int secuenciarelacion,
        //    byte corporativointerno,
        //    byte sucursalinterno,
        //    int añointerno,
        //    int foliointerno,
        //    int secuenciainterno,
        //    byte corporativoexterno,
        //    byte sucursalexterno,
        //    int añoexterno,
        //    int folioexterno,
        //    int secuenciaexterno,
        //    string concepto,
        //    decimal montoconciliado,
        //    decimal diferencia,
        //    decimal montoexterno,
        //    decimal montointerno,
        //    int formaconciliacion,
        //    int statusconcepto,
        //    string statusconciliacion,
        //    int motivonoconciliado,
        //    string comentarionoconciliado,
        //    string usuario,
        //    DateTime falta,
        //    string descripcion,
        //    string usuariostatusconcepto,
        //    DateTime fstatusconcepto,
        //    MensajesImplementacion implementadorMensajes)
        //    : base( corporativoconciliacion,
        //            sucursalconciliacion,
        //            añoconciliacion,
        //            mesconciliacion,
        //            folioconciliacion,
        //            secuenciarelacion,
        //            corporativointerno,
        //            sucursalinterno,
        //            añointerno,
        //            foliointerno,
        //            secuenciainterno,
        //            corporativoexterno,
        //            sucursalexterno,
        //            añoexterno,
        //            folioexterno,
        //            secuenciaexterno,
        //            concepto,
        //            montoconciliado,
        //            diferencia,
        //            montoexterno,
        //            montointerno,
        //            formaconciliacion,
        //            statusconcepto,
        //            statusconciliacion,
        //            motivonoconciliado,
        //            comentarionoconciliado,
        //            usuario,
        //            falta,
        //            descripcion,
        //            usuariostatusconcepto,
        //            fstatusconcepto,
        //            implementadorMensajes);

        public override void ActualizaPagoAnticipado(byte CorporativoConciliacion, byte SucursalConciliacion, int AñoConciliacion, int MesConciliacion, int FolioConciliacion, int SecuenciaRelacion, int StatusConcepto, string StatusConciliacion, byte MotivoNoConciliado, string ComentarioNoConciliado, decimal MontoExterno)
        {
            using (SqlConnection cnn = new SqlConnection(objApp.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaConciliacionReferenciaPagoAnticipado", cnn);
                    comando.Parameters.Add("@CORPORATIVOCONCILIACION", System.Data.SqlDbType.Int).Value = CorporativoConciliacion;
                    comando.Parameters.Add("@SUCURSALCONCILIACION", System.Data.SqlDbType.Int).Value = SucursalConciliacion;
                    comando.Parameters.Add("@AÑOCONCILIACION", System.Data.SqlDbType.Int).Value = AñoConciliacion;
                    comando.Parameters.Add("@MESCONCILIACION", System.Data.SqlDbType.Int).Value = MesConciliacion;
                    comando.Parameters.Add("@FOLIOCONCILIACION", System.Data.SqlDbType.Int).Value = FolioConciliacion;
                    comando.Parameters.Add("@SECUENCIARELACION", System.Data.SqlDbType.Int).Value = SecuenciaRelacion;
                    comando.Parameters.Add("@MONTOEXTERNO", System.Data.SqlDbType.Decimal).Value = MontoExterno;
                    comando.Parameters.Add("@STATUSCONCEPTO", System.Data.SqlDbType.Int).Value = StatusConcepto;
                    comando.Parameters.Add("@STATUSCONCILIACION", System.Data.SqlDbType.VarChar).Value = StatusConciliacion;
                    comando.Parameters.Add("@MOTIVONOCONCILIADO", System.Data.SqlDbType.Int).Value = MotivoNoConciliado;
                    comando.Parameters.Add("@COMENTARIONOCONCILIADO", System.Data.SqlDbType.VarChar).Value = ComentarioNoConciliado;
                    
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    int RegistrosAfectados = comando.ExecuteNonQuery();

                    //if (RegistrosAfectados == 0)
                    //    throw new Exception("No se encontró registro para actualizar estatus.");
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public override ConciliacionReferencia CrearObjeto()
        {
            return new ConciliacionReferenciaDatos(this.ImplementadorMensajes);
        }

    }
}
