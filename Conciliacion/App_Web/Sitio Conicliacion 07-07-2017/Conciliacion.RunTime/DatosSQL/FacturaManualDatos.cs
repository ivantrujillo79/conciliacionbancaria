using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.ReglasDeNegocio;
using System.Data;
using System.Data.SqlClient;

namespace Conciliacion.RunTime.DatosSQL
{
    public class FacturaManualDatos : FacturaManual
    {
        public FacturaManualDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public FacturaManualDatos(
             byte corporativoconciliacion,
             byte sucursalconciliacion,
             Int32 añoconciliacion,
             Int32 mesconciliacion,
             Int32 folioconciliacion,
             Int32 secuenciarelacion,
             Int32 factura,
             byte corporativoexterno,
             byte sucursalexterno,
             Int32 añoexterno,
             Int32 folioexterno,
             Int32 secuenciaexterno,
             string concepto, //500
             decimal montoconciliado,
             decimal montoexterno,
             Int32 montointerno,
             Int32 formaconciliacion,
             Int32 statusconcepto,
             string statusconciliacion, //20
             string statusmovimiento, // 00
             string usuario, // 5
             DateTime falta,
             string descripcion, // 00
             string usuariostatusconcepto,
             DateTime fstatusconcepto,
            IMensajesImplementacion implementadorMensajes
            )
            : base(
                corporativoconciliacion,
                sucursalconciliacion,
                añoconciliacion,
                mesconciliacion,
                folioconciliacion,
                secuenciarelacion,
                factura,
                corporativoexterno,
                sucursalexterno,
                añoexterno,
                folioexterno,
                secuenciaexterno,
                concepto, //500
                montoconciliado,
                montoexterno,
                montointerno,
                formaconciliacion,
                statusconcepto,
                statusconciliacion, //20
                statusmovimiento, // 00
                usuario, // 5
                falta,
                descripcion, // 00
                usuariostatusconcepto,
                fstatusconcepto,
                implementadorMensajes
            )
        {
        }

        public override FacturaManual CrearObjeto()
        {
            return new FacturaManualDatos(this.implementadorMensajes);
        }

        public virtual void Dispose()
        {

        }

        public override bool Guardar(Conexion _conexion, byte corporativoconciliacion, byte sucursalconciliacion, int añoconciliacion, int mesconciliacion, 
                                    int folioconciliacion, int secuenciarelacion, int factura, byte corporativoexterno, byte sucursalexterno, int añoexterno, 
                                    int folioexterno, int secuenciaexterno, string concepto, decimal montoconciliado, decimal montoexterno, int montointerno, 
                                    int formaconciliacion, int statusconcepto, string statusconciliacion, string statusmovimiento, string usuario, DateTime falta, 
                                    string descripcion, string usuariostatusconcepto, DateTime fstatusconcepto)
        {
            bool resultado = false;
            try
            {
                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBConciliacionFacturaManualAlta";
                _conexion.Comando.Parameters.Clear();
                _conexion.Comando.Parameters.Add(new SqlParameter("@corporativoconciliacion", SqlDbType.SmallInt)).Value = corporativoconciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@sucursalconciliacion", SqlDbType.SmallInt)).Value = sucursalconciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@añoconciliacion", SqlDbType.SmallInt)).Value = añoconciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@mesconciliacion", SqlDbType.SmallInt)).Value = mesconciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@folioconciliacion", SqlDbType.SmallInt)).Value = folioconciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@secuenciarelacion", SqlDbType.SmallInt)).Value = secuenciarelacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@factura", SqlDbType.SmallInt)).Value = factura;
                _conexion.Comando.Parameters.Add(new SqlParameter("@corporativoexterno", SqlDbType.SmallInt)).Value = corporativoexterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@sucursalexterno", SqlDbType.SmallInt)).Value = sucursalexterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@añoexterno", SqlDbType.SmallInt)).Value = añoexterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@formaconciliacion", SqlDbType.SmallInt)).Value = formaconciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@statusconcepto", SqlDbType.SmallInt)).Value = statusconcepto;
                _conexion.Comando.Parameters.Add(new SqlParameter("@statusconciliacion", SqlDbType.SmallInt)).Value = statusconciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@statusmovimiento", SqlDbType.SmallInt)).Value = statusmovimiento;
                _conexion.Comando.Parameters.Add(new SqlParameter("@usuario", SqlDbType.SmallInt)).Value = usuario;
                _conexion.Comando.Parameters.Add(new SqlParameter("@falta", SqlDbType.SmallInt)).Value = falta;
                _conexion.Comando.Parameters.Add(new SqlParameter("@descripcion", SqlDbType.SmallInt)).Value = descripcion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@usuariostatusconcepto", SqlDbType.SmallInt)).Value = usuariostatusconcepto;
                _conexion.Comando.Parameters.Add(new SqlParameter("@fstatusconcepto", SqlDbType.SmallInt)).Value = fstatusconcepto;
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
