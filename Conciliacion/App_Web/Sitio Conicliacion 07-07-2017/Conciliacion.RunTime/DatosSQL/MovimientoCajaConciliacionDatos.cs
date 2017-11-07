using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.DatosSQL;
using System.Data;
using System.Data.SqlClient;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public class MovimientoCajaConciliacionDatos: MovimientoCajaConciliacion {

        public MovimientoCajaConciliacionDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public MovimientoCajaConciliacionDatos(short caja, DateTime foperacion, short consecutivo,int folio, short corporativoconciliacion, short sucursalconciliacion, int a�oconciliacion, short mesconciliacion, int folioconciliacion, short status, IMensajesImplementacion implementadorMensajes)
            : base(caja, foperacion, consecutivo, folio, corporativoconciliacion, sucursalconciliacion, a�oconciliacion, mesconciliacion, folioconciliacion, status, implementadorMensajes)
        {
        }

        public override void Dispose(){

		}

		public override void Guardar(Conexion _conexion){
            _conexion.Comando.CommandType = CommandType.StoredProcedure;
            _conexion.Comando.CommandText = "spCBMovimientoCajaConciliacionAlta";


            _conexion.Comando.Parameters.Clear();
            _conexion.Comando.Parameters.Add(new SqlParameter("@Caja", System.Data.SqlDbType.TinyInt)).Value = this.Caja;
            _conexion.Comando.Parameters.Add(new SqlParameter("@FOperacion", System.Data.SqlDbType.DateTime)).Value = this.FOperacion;
            _conexion.Comando.Parameters.Add(new SqlParameter("@Consecutivo", System.Data.SqlDbType.TinyInt)).Value = this.Consecutivo;
            _conexion.Comando.Parameters.Add(new SqlParameter("@Folio", System.Data.SqlDbType.Int)).Value = this.Folio;
            _conexion.Comando.Parameters.Add(new SqlParameter("@CorporativoConciliacion", System.Data.SqlDbType.TinyInt)).Value = this.CorporativoConciliacion;
            _conexion.Comando.Parameters.Add(new SqlParameter("@SucursalConciliacion", System.Data.SqlDbType.TinyInt)).Value = this.SucursalConciliacion;
            _conexion.Comando.Parameters.Add(new SqlParameter("@A�oConciliacion", System.Data.SqlDbType.Int)).Value = this.A�oConciliacion;
            _conexion.Comando.Parameters.Add(new SqlParameter("@MesConciliacion", System.Data.SqlDbType.SmallInt)).Value = this.MesConciliacion;
            _conexion.Comando.Parameters.Add(new SqlParameter("@FolioConciliacion", System.Data.SqlDbType.Int)).Value = this.FolioConciliacion;
            _conexion.Comando.Parameters.Add(new SqlParameter("@Status", System.Data.SqlDbType.TinyInt)).Value = this.Folio;
            _conexion.Comando.ExecuteNonQuery();
        }

		
	}//end MovimientoCajaConciliacionDatos

}//end namespace DatosSQL