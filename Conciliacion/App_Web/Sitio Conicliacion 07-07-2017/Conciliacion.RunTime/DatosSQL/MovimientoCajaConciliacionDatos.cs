using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Conciliacion.RunTime.ReglasDeNegocio;
namespace Conciliacion.RunTime.DatosSQL
{
    public class MovimientoCajaConciliacionDatos: MovimientoCajaConciliacion {

        public MovimientoCajaConciliacionDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public MovimientoCajaConciliacionDatos(short caja, DateTime foperacion, short consecutivo,int folio, int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion, String status, IMensajesImplementacion implementadorMensajes)
            : base(caja, foperacion, consecutivo, folio, corporativoconciliacion, sucursalconciliacion, añoconciliacion, mesconciliacion, folioconciliacion, status, implementadorMensajes)
        {
        }

        public override void Dispose(){

		}

		public override void Guardar(Conexion _conexion)
		{
		    SqlConnection cnn = new SqlConnection(App.CadenaConexion);
            SqlCommand cmd = new SqlCommand("spCBMovimientoCajaConciliacionAlta",cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //_conexion.Comando.CommandType = CommandType.StoredProcedure;
            //_conexion.Comando.CommandText = "spCBMovimientoCajaConciliacionAlta";

            cmd.Parameters.Clear();

            cmd.Parameters.Add(new SqlParameter("@Caja", System.Data.SqlDbType.TinyInt)).Value = this.Caja;
            cmd.Parameters.Add(new SqlParameter("@FOperacion", System.Data.SqlDbType.DateTime)).Value = this.FOperacion;
            cmd.Parameters.Add(new SqlParameter("@Consecutivo", System.Data.SqlDbType.TinyInt)).Value = this.Consecutivo;
            cmd.Parameters.Add(new SqlParameter("@Folio", System.Data.SqlDbType.Int)).Value = this.Folio;
            cmd.Parameters.Add(new SqlParameter("@CorporativoConciliacion", System.Data.SqlDbType.TinyInt)).Value = this.CorporativoConciliacion;
            cmd.Parameters.Add(new SqlParameter("@SucursalConciliacion", System.Data.SqlDbType.TinyInt)).Value = this.SucursalConciliacion;
            cmd.Parameters.Add(new SqlParameter("@AñoConciliacion", System.Data.SqlDbType.Int)).Value = this.AñoConciliacion;
            cmd.Parameters.Add(new SqlParameter("@MesConciliacion", System.Data.SqlDbType.SmallInt)).Value = this.MesConciliacion;
            cmd.Parameters.Add(new SqlParameter("@FolioConciliacion", System.Data.SqlDbType.Int)).Value = this.FolioConciliacion;
            cmd.Parameters.Add(new SqlParameter("@Status", System.Data.SqlDbType.TinyInt)).Value = this.Folio;
		    
            cnn.Open();
            cmd.ExecuteNonQuery();
        }

		
	}//end MovimientoCajaConciliacionDatos

}//end namespace DatosSQL