using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conciliacion.RunTime.ReglasDeNegocio;
using System.Data;
using System.Data.SqlClient;

namespace Conciliacion.RunTime.DatosSQL
{
    public class ClienteDatos: Cliente
    {
        
        public virtual void Dispose(){

		}
        public ClienteDatos(string Referencia, Conexion _conexion) {

            this.Referencia = Referencia;

            bool ExisteCliente = ValidaClienteExiste(_conexion);

            if (ExisteCliente)
            {
                cConciliacion Conciliacion = new ConciliacionDatos(implementadorMensajes);
                ObtienePedidosNoConciliadosCliente(Conciliacion, _conexion);
            }

        }

        public override bool ValidaClienteExiste(Conexion _conexion)
        {
            try
            {
                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCCLConsultaVwDatosClientePorReferencia";


                _conexion.Comando.Parameters.Clear();
                _conexion.Comando.Parameters.Add(new SqlParameter("@NumeroReferencia", System.Data.SqlDbType.TinyInt)).Value = this.Referencia;
                SqlDataReader rdCliente = _conexion.Comando.ExecuteReader();

                if (rdCliente.HasRows)
                {
                    while (rdCliente.Read())
                    {
                        this.NumCliente = rdCliente.GetInt32(0);
                        this.Nombre = rdCliente.GetString(1);
                        this.RazonSocial = rdCliente.GetString(2);
                        this.Celula = rdCliente.GetByte(3);
                        this.Ruta = rdCliente.GetInt16(4);

                        if (rdCliente.GetBoolean(5))
                            this.Programacion = 1;
                        else
                            this.Programacion = 0;

                        this.TelefonoCasa = rdCliente.GetString(6);
                        this.TelefonoAlternoUno = rdCliente.GetString(7);
                        this.TelefonoAlternoDos = rdCliente.GetString(8);
                        this.Saldo = rdCliente.GetDecimal(9);
                        this.Email = rdCliente.GetString(10);
                        this.Direccion = rdCliente.GetString(11);

                    }
                    rdCliente.Close();
                    return true;
                }
                else
                {
                    return false;
                }             
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override List<ReferenciaNoConciliadaPedido> ObtienePedidosNoConciliadosCliente(cConciliacion Conciliacion, Conexion _conexion){

            _conexion.Comando.CommandType = CommandType.StoredProcedure;
            _conexion.Comando.CommandText = "spCCLConsultaVwDatosClientePorReferencia";


            _conexion.Comando.Parameters.Clear();
            //_conexion.Comando.Parameters.Add(new SqlParameter("@Configuracion", System.Data.SqlDbType.SmallInt)).Value = Conciliacion.con;
            _conexion.Comando.Parameters.Add(new SqlParameter("@CorporativoConciliacion", System.Data.SqlDbType.TinyInt)).Value = Conciliacion.Corporativo;
            _conexion.Comando.Parameters.Add(new SqlParameter("@SucursalConciliacion", System.Data.SqlDbType.TinyInt)).Value = Conciliacion.Sucursal;
            _conexion.Comando.Parameters.Add(new SqlParameter("@AñoConciliacion", System.Data.SqlDbType.Int)).Value = Conciliacion.Año;
            _conexion.Comando.Parameters.Add(new SqlParameter("@MesConciliacion", System.Data.SqlDbType.SmallInt)).Value = Conciliacion.Mes;
            _conexion.Comando.Parameters.Add(new SqlParameter("@FolioConciliacion", System.Data.SqlDbType.Int)).Value = Conciliacion.Folio;
            _conexion.Comando.Parameters.Add(new SqlParameter("@Folio", System.Data.SqlDbType.Int)).Value = Conciliacion.Folio;
            //_conexion.Comando.Parameters.Add(new SqlParameter("@Secuencia", System.Data.SqlDbType.Int)).Value = Conciliacion.;
            _conexion.Comando.Parameters.Add(new SqlParameter("@Celula", System.Data.SqlDbType.SmallInt)).Value = this.Celula;
            _conexion.Comando.Parameters.Add(new SqlParameter("@ClienteSeleccion", System.Data.SqlDbType.Int)).Value = this.NumCliente;
            //_conexion.Comando.Parameters.Add(new SqlParameter("@ClientePadre", System.Data.SqlDbType.Bit)).Value = Conciliacion.;
            SqlDataReader rdCliente = _conexion.Comando.ExecuteReader();

            List<ReferenciaNoConciliadaPedido> lstRefenciaNoConciliada = new List<ReferenciaNoConciliadaPedido>();

            return lstRefenciaNoConciliada;

        }

	}//end ClienteDatos

}//end namespace DatosSQL