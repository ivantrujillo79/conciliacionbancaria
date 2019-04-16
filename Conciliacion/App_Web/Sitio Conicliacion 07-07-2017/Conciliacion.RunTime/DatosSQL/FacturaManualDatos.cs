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
                                    int folioexterno, int secuenciaexterno, string concepto, decimal montoconciliado, decimal montoexterno, decimal montointerno, 
                                    int formaconciliacion, int statusconcepto, string statusconciliacion, string statusmovimiento, string usuario, DateTime falta, 
                                    string descripcion, string usuariostatusconcepto, DateTime fstatusconcepto)
        {
            bool resultado = false;
            try
            {
                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBConciliacionFacturaManualAlta";
                _conexion.Comando.Parameters.Clear();
                _conexion.Comando.Parameters.Add(new SqlParameter("@CorporativoConciliacion", SqlDbType.TinyInt)).Value = corporativoconciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@SucursalConciliacion", SqlDbType.TinyInt)).Value = sucursalconciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@AñoConciliacion", SqlDbType.Int)).Value = añoconciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@MesConciliacion", SqlDbType.SmallInt)).Value = mesconciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FolioConciliacion", SqlDbType.Int)).Value = folioconciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@SecuenciaRelacion", SqlDbType.Int)).Value = secuenciarelacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Factura", SqlDbType.Int)).Value = factura;
                _conexion.Comando.Parameters.Add(new SqlParameter("@CorporativoExterno", SqlDbType.TinyInt)).Value = corporativoexterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@SucursalExterno", SqlDbType.TinyInt)).Value = sucursalexterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@AñoExterno", SqlDbType.Int)).Value = añoexterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FolioExterno", SqlDbType.Int)).Value = folioexterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@SecuenciaExterno", SqlDbType.Int)).Value = secuenciaexterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Concepto", SqlDbType.VarChar, 500)).Value = concepto;
                _conexion.Comando.Parameters.Add(new SqlParameter("@MontoConciliado", SqlDbType.Money)).Value = montoconciliado;
                _conexion.Comando.Parameters.Add(new SqlParameter("@MontoExterno", SqlDbType.Money)).Value = montoexterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@MontoInterno", SqlDbType.Money)).Value = montointerno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FormaConciliacion", SqlDbType.SmallInt)).Value = formaconciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@StatusConcepto", SqlDbType.SmallInt)).Value = statusconcepto;
                _conexion.Comando.Parameters.Add(new SqlParameter("@StatusConciliacion", SqlDbType.VarChar, 20)).Value = statusconciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@StatusMovimiento", SqlDbType.VarChar, 100)).Value = statusmovimiento;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Usuario", SqlDbType.Char, 15)).Value = usuario;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FAlta", SqlDbType.DateTime)).Value = falta;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar, 100)).Value = descripcion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@UsuarioStatusConcepto", SqlDbType.Char, 15)).Value = usuariostatusconcepto;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FStatusConcepto", SqlDbType.DateTime)).Value = fstatusconcepto;
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
