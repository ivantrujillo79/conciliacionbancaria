using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.ReglasDeNegocio;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Conciliacion.RunTime.DatosSQL
{
    public class PagoAreasComunesDatos : PagoAreasComunes
    {
        Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();

        public PagoAreasComunesDatos(MensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public PagoAreasComunesDatos(int clientePadre, MensajesImplementacion implementadorMensajes)
            : base(clientePadre, implementadorMensajes)
        {
        }


        public override PagoAreasComunes CrearObjeto()
        {
            return new PagoAreasComunesDatos(objApp.ImplementadorMensajes);
        }

        public override void consulta(Conexion _conexion)
        {

           Pagos = new DataTable("Pagos");

            try
            {


                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "uspCBConsultaAreasComunes";
                _conexion.Comando.Parameters.Clear();
                _conexion.Comando.Parameters.Add(new SqlParameter("@ClientePadre", System.Data.SqlDbType.Int)).Value = ClientePadre;
                _conexion.Comando.Parameters.Add(new SqlParameter("@NombreClientePadre", System.Data.SqlDbType.VarChar, 80)).Value = "";
                _conexion.Comando.Parameters["@NombreClientePadre"].Direction = ParameterDirection.Output;

                if (FSuministroInicio != DateTime.MinValue)
                {
                    _conexion.Comando.Parameters.Add(new SqlParameter("@FSuministroInicio", System.Data.SqlDbType.DateTime)).Value = FSuministroInicio;
                }

                if (FSuministroFin != DateTime.MinValue)
                {
                    _conexion.Comando.Parameters.Add(new SqlParameter("@FSuministroFin", System.Data.SqlDbType.DateTime)).Value = FSuministroFin;
                }

                if (PedidoReferencia != "")
                {
                    _conexion.Comando.Parameters.Add(new SqlParameter("@PedidoReferencia", System.Data.SqlDbType.VarChar, 20)).Value = PedidoReferencia;
                }

                //_conexion.Comando.ExecuteNonQuery();

                System.Data.SqlClient.SqlDataAdapter adaptador = new System.Data.SqlClient.SqlDataAdapter();
                adaptador.SelectCommand = _conexion.Comando;

                adaptador.Fill(Pagos);


                NombreClientePadre = _conexion.Comando.Parameters["@NombreClientePadre"].Value.ToString();
                TienePagos = Pagos.Rows.Count > 0;


                
 
              
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                 
            }
        }

      
      


    }
}
