using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Conciliacion.RunTime.ReglasDeNegocio;
using System.Data.SqlTypes;

namespace Conciliacion.RunTime.DatosSQL
{
    public class MovimientoCajaConciliacionDatos: MovimientoCajaConciliacion {

        public MovimientoCajaConciliacionDatos(MensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public MovimientoCajaConciliacionDatos(short caja, DateTime foperacion, short consecutivo,int folio, int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion, String status, int cobranza, MensajesImplementacion implementadorMensajes)
            : base(caja, foperacion, consecutivo, folio, corporativoconciliacion, sucursalconciliacion, añoconciliacion, mesconciliacion, folioconciliacion, status, cobranza, implementadorMensajes)
        {
        }

        public override void Dispose(){

		}

		public override void Guardar(Conexion _conexion){
            try
            {
                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBMovimientoCajaConciliacionAlta";


                _conexion.Comando.Parameters.Clear();
                _conexion.Comando.Parameters.Add(new SqlParameter("@Caja", System.Data.SqlDbType.TinyInt)).Value = this.Caja;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FOperacion", System.Data.SqlDbType.DateTime)).Value = this.FOperacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Consecutivo", System.Data.SqlDbType.TinyInt)).Value = this.Consecutivo;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Folio", System.Data.SqlDbType.Int)).Value = this.Folio;
                _conexion.Comando.Parameters.Add(new SqlParameter("@CorporativoConciliacion", System.Data.SqlDbType.TinyInt)).Value = this.CorporativoConciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@SucursalConciliacion", System.Data.SqlDbType.TinyInt)).Value = this.SucursalConciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@AñoConciliacion", System.Data.SqlDbType.Int)).Value = this.AñoConciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@MesConciliacion", System.Data.SqlDbType.SmallInt)).Value = this.MesConciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FolioConciliacion", System.Data.SqlDbType.Int)).Value = this.FolioConciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Status", System.Data.SqlDbType.TinyInt)).Value = 1;
                _conexion.Comando.Parameters.Add(new SqlParameter("@cobranza", System.Data.SqlDbType.Int)).Value = this.Cobranza == 0 ? SqlInt32.Null : Cobranza;
                _conexion.Comando.ExecuteNonQuery();
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

		
	}//end MovimientoCajaConciliacionDatos

}//end namespace DatosSQL