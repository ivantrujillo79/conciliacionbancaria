using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using Conciliacion.RunTime.ReglasDeNegocio;

namespace Conciliacion.RunTime.DatosSQL
{
    class TransferenciaBancariaOrigenDatos : TransferenciaBancariaOrigen
    {
        public TransferenciaBancariaOrigenDatos(MensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {

        }

        public TransferenciaBancariaOrigenDatos(short corporativoTD, short sucursalTD, int añoTD, int folioTD,
                                           int secuenciaTD,
                                           short corporativo, short sucursal, int año, int folio, 
                                           MensajesImplementacion implementadorMensajes)
            : base(
                corporativoTD, sucursalTD, añoTD, folioTD, secuenciaTD, corporativo, sucursal, año, folio,
                implementadorMensajes)
        {

        }

        public override TransferenciaBancariaOrigen CrearObjeto()
        {
            return new TransferenciaBancariaOrigenDatos(this.ImplementadorMensajes);
        }

        /*public override bool Registrar()
        {
            bool resultado = true;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.CadenaConexion))
                {

                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaTransferenciaBancariaOrigen", cnn);
                    comando.Parameters.Add("@CorporativoTD", System.Data.SqlDbType.TinyInt).Value = this.CorporativoTD;
                    comando.Parameters.Add("@SucursalTD", System.Data.SqlDbType.TinyInt).Value = this.SucursalTD;
                    comando.Parameters.Add("@AñoTD", System.Data.SqlDbType.Int).Value = this.AñoTD;
                    comando.Parameters.Add("@FolioTD", System.Data.SqlDbType.Int).Value = this.FolioTD;
                    comando.Parameters.Add("@SecuenciaTD", System.Data.SqlDbType.Int).Value = this.SecuenciaTD;
                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = this.Corporativo;
                    comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.TinyInt).Value = this.Sucursal;
                    comando.Parameters.Add("@Año", System.Data.SqlDbType.Int).Value = this.Año;
                    comando.Parameters.Add("@Folio", System.Data.SqlDbType.Int).Value = this.Folio;
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
        }*/

        public override bool Registrar(Conexion _conexion)
        {
            bool resultado = false;
            try
            {
                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBActualizaTransferenciaBancariaOrigen";
                _conexion.Comando.Parameters.Clear();
                _conexion.Comando.Parameters.Add(new SqlParameter("@CorporativoTD", System.Data.SqlDbType.TinyInt)).Value = this.CorporativoTD;
                _conexion.Comando.Parameters.Add(new SqlParameter("@SucursalTD", System.Data.SqlDbType.TinyInt)).Value = this.SucursalTD;
                _conexion.Comando.Parameters.Add(new SqlParameter("@AñoTD", System.Data.SqlDbType.Int)).Value = this.AñoTD;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FolioTD", System.Data.SqlDbType.Int)).Value = this.FolioTD;
                _conexion.Comando.Parameters.Add(new SqlParameter("@SecuenciaTD", System.Data.SqlDbType.Int)).Value = this.SecuenciaTD;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Corporativo", System.Data.SqlDbType.TinyInt)).Value = this.Corporativo;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Sucursal", System.Data.SqlDbType.TinyInt)).Value = this.Sucursal;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Año", System.Data.SqlDbType.Int)).Value = this.Año;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Folio", System.Data.SqlDbType.Int)).Value = this.Folio;
                
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
