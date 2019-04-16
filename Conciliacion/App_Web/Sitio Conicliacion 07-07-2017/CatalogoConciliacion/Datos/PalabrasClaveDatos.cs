using Conciliacion.Migracion.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CatalogoConciliacion.ReglasNegocio;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web;

namespace CatalogoConciliacion.Datos
{
    public class PalabrasClaveDatos : PalabrasClave
    {

        public PalabrasClaveDatos(IMensajesImplementacion implementadorMensajes)
        : base(implementadorMensajes)
    {
    }

    public PalabrasClaveDatos(int Banco, string CuentaBanco, int TipoCobro, string PalabraClave,string columnadestino)
        : base(Banco, CuentaBanco, TipoCobro, PalabraClave, columnadestino)
    {
    }

    public override PalabrasClave CrearObjeto()
    {
        throw new NotImplementedException();
    }



        public override bool Guardar()
        {
            bool resultado = true;
            using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBGuardarPalabrasClave", cnn);
                    comando.Parameters.Add("@Banco", System.Data.SqlDbType.SmallInt).Value = this.Banco;//this.Configuracion;
                    comando.Parameters.Add("@CuentaBanco", System.Data.SqlDbType.VarChar).Value = this.CuentaBanco;
                    comando.Parameters.Add("@TipoCobro", System.Data.SqlDbType.TinyInt).Value = this.TipoCobro;
                    comando.Parameters.Add("@PalabraClave", System.Data.SqlDbType.VarChar).Value = this.PalabraClave;
                    comando.Parameters.Add("@ColumnaDestino", System.Data.SqlDbType.VarChar).Value = this.ColumnaDestino;
                    comando.Parameters.Add("@Usuario", System.Data.SqlDbType.VarChar).Value = ((SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"]).IdUsuario;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.ExecuteNonQuery();
                    //this.ImplementadorMensajes.MostrarMensaje("Registro Guardado Con éxito");
                }

                catch (SqlException ex)
                {
                    stackTrace = new StackTrace();
                    this.ImplementadorMensajes.MostrarMensaje("No se pudo guardar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                    stackTrace = null;
                    resultado = false;
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
            }
            return resultado;
        }
    }

}
