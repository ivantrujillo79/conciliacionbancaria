using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime.ReglasDeNegocio;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data.SqlTypes;
using System.Data;
using System.Text.RegularExpressions;

namespace Conciliacion.RunTime.DatosSQL
{
    public class ClienteDatos : Cliente
    {
        private string _URLGateway = "";
        private short GLOBAL_Modulo = 4;
        private short GLOBAL_Empresa;
        private short GLOBAL_Sucursal;

        public ClienteDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public ClienteDatos(int cliente,
            byte celula,
            Int16 digitoverificador,
            string nombre,
            string referencia,
            string razonsocial,
            int ruta,
            int programacion,
            string telefonocasa,
            string telefonoalternouno,
            string telefonoalternodos,
            decimal saldo,
            string email,
            string direccion,
            string tipo, IMensajesImplementacion implementadorMensajes)
            : base(celula,
            digitoverificador,
            nombre,
            referencia,
            razonsocial,
            ruta,
            programacion,
            telefonocasa,
            telefonoalternouno,
            telefonoalternodos,
            saldo,
            email,
            direccion,
            tipo, implementadorMensajes)
        {
        }

        public override Cliente CrearObjeto()
        {
            return new ClienteDatos(this.ImplementadorMensajes);
        }

        public override bool ValidaClienteExiste(Conexion _conexion)
        {
            try
            {
                ClienteException ObjClienteException = new ClienteException();

                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCCLConsultaVwDatosClienteReferencia";


                _conexion.Comando.Parameters.Clear();
                _conexion.Comando.Parameters.Add(new SqlParameter("@Cliente", System.Data.SqlDbType.VarChar)).Value = this.Referencia;
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
                        if (rdCliente.GetInt32(12) == this.NumCliente)
                            this.Tipo = "PADRE";
                        else
                            this.Tipo = "SUCURSAL";

                    }
                    rdCliente.Close();

                    ObjClienteException.ResultadoValidacion.CodigoError = 0;
                    ObjClienteException.ResultadoValidacion.Mensaje = "Proceso realizado existosamente";
                    ObjClienteException.ResultadoValidacion.VerificacionValida = true;

                    return true;
                }
                else
                {
                    ObjClienteException.ResultadoValidacion.CodigoError = 203;
                    ObjClienteException.ResultadoValidacion.Mensaje = "Cliente no existe";
                    ObjClienteException.ResultadoValidacion.VerificacionValida = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override List<ReferenciaNoConciliadaPedido> ObtienePedidosNoConciliadosCliente(cConciliacion Conciliacion, Conexion _conexion)
        {
            try
            {
                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBConciliacionBusquedaPedido";

                _conexion.Comando.Parameters.Clear();
                _conexion.Comando.Parameters.Add(new SqlParameter("@Configuracion", System.Data.SqlDbType.SmallInt)).Value = 0;
                _conexion.Comando.Parameters.Add(new SqlParameter("@CorporativoConciliacion", System.Data.SqlDbType.TinyInt)).Value = Conciliacion.Corporativo;
                _conexion.Comando.Parameters.Add(new SqlParameter("@SucursalConciliacion", System.Data.SqlDbType.TinyInt)).Value = Conciliacion.Sucursal;
                _conexion.Comando.Parameters.Add(new SqlParameter("@AñoConciliacion", System.Data.SqlDbType.Int)).Value = Conciliacion.Año;
                _conexion.Comando.Parameters.Add(new SqlParameter("@MesConciliacion", System.Data.SqlDbType.SmallInt)).Value = Conciliacion.Mes;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FolioConciliacion", System.Data.SqlDbType.Int)).Value = Conciliacion.Folio;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Folio", System.Data.SqlDbType.Int)).Value = Conciliacion.Folio;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Secuencia", System.Data.SqlDbType.Int)).Value = 1;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Celula", System.Data.SqlDbType.SmallInt)).Value = this.Celula;
                _conexion.Comando.Parameters.Add(new SqlParameter("@ClienteSeleccion", System.Data.SqlDbType.Int)).Value = this.NumCliente;
                _conexion.Comando.Parameters.Add(new SqlParameter("@ClientePadre", System.Data.SqlDbType.Bit)).Value = 0;
                SqlDataReader reader = _conexion.Comando.ExecuteReader();
                List<ReferenciaNoConciliadaPedido> lstRefenciaNoConciliada = new List<ReferenciaNoConciliadaPedido>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ReferenciaNoConciliadaPedido dato = new ReferenciaNoConciliadaPedidoDatos(
                                Convert.ToInt16(reader["Corporativo"]), Convert.ToInt16(reader["Sucursal"]),
                                Convert.ToString(reader["SucursalDes"]), Conciliacion.Año, Conciliacion.Folio, Conciliacion.Mes,
                                Convert.ToInt32(reader["Celula"]), Convert.ToInt32(reader["AñoPed"]),
                                Convert.ToInt32(reader["Pedido"]), Convert.ToString(reader["PedidoReferencia"]),
                                Convert.ToInt32(reader["Cliente"]), Convert.ToString(reader["Nombre"]),
                                Convert.ToInt32(reader["RemisionPedido"]), Convert.ToString(reader["SeriePedido"]),
                                Convert.ToInt32(reader["FolioSat"]), Convert.ToString(reader["SerieSat"]),
                                Convert.ToString(reader["Concepto"]), Convert.ToDecimal(reader["Monto"]),
                                Convert.ToInt16(reader["FormaConciliacion"]), Convert.ToInt16(reader["StatusConcepto"]),
                                Convert.ToString(reader["StatusConciliacion"]), Convert.ToDateTime(reader["FOperacion"]),
                                Convert.ToDateTime(reader["FMovimiento"]), 0, this.implementadorMensajes);
                        lstRefenciaNoConciliada.Add(dato);
                    }
                    reader.Close();
                }
                ClienteException ObjClienteException = new ClienteException();
                ObjClienteException.ResultadoValidacion.CodigoError = 0;
                ObjClienteException.ResultadoValidacion.Mensaje = "Proceso exitoso";
                ObjClienteException.ResultadoValidacion.VerificacionValida = true;

                return lstRefenciaNoConciliada;
            }
            catch (Exception ex)
            {
                ClienteException ObjClienteException = new ClienteException();
                ObjClienteException.ResultadoValidacion.CodigoError = 203;
                ObjClienteException.ResultadoValidacion.Mensaje = ex.Message;
                ObjClienteException.ResultadoValidacion.VerificacionValida = false;
                throw ex;
            }
        }

        public override string CargaURLGateway()
        {
            string url = "";
            SigaMetClasses.cConfig oConfig = new SigaMetClasses.cConfig(GLOBAL_Modulo, GLOBAL_Empresa, GLOBAL_Sucursal);
            try
            {
                url = oConfig.Parametros["URLGateway"].ToString();
                Regex re = new Regex(
                                "^(https?|ftp|file)://[-A-Z0-9+&@#/%?=~_|!:,.;]*[-A-Z0-9+&@#/%=~_|]",
                                RegexOptions.IgnoreCase);
                Match m = re.Match(_URLGateway);
                if (m.Captures.Count == 0)
                {
                    throw new Exception("El valor configurado al parámetro URLGateway no es correcto.");
                }
            }
            catch (Exception ex)
            {
                _URLGateway = "";
            }
            return url;
        }

        public override string consultaClienteCRM(int cliente)
        {
            RTGMGateway.RTGMGateway Gateway;
            RTGMGateway.SolicitudGateway Solicitud;
            RTGMCore.DireccionEntrega DireccionEntrega = new RTGMCore.DireccionEntrega();

            try
            {
                if (_URLGateway != string.Empty) 
                {
                    Gateway = new RTGMGateway.RTGMGateway();
                    Gateway.URLServicio = _URLGateway;
                    Solicitud = new RTGMGateway.SolicitudGateway();
                    Solicitud.Fuente = RTGMCore.Fuente.Sigamet;
                    Solicitud.IDCliente = cliente;
                    Solicitud.IDEmpresa = 0;
                    DireccionEntrega = Gateway.buscarDireccionEntrega(Solicitud);
                }
            }
            catch (Exception ex)
            { 
                throw ex;
            }
            return DireccionEntrega.Nombre.Trim();
        }

        public override DataTable ObtienePedidosCliente(int Cliente, Conexion _conexion)
        {
            DataTable dtRetorno = new DataTable();
            try
            {
                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBPedidosClienteOPadre";

                _conexion.Comando.Parameters.Clear();
                _conexion.Comando.Parameters.Add(new SqlParameter("@Cliente", System.Data.SqlDbType.Int)).Value = Cliente;

                SqlDataAdapter Dap = new SqlDataAdapter(_conexion.Comando);
                Dap.Fill(dtRetorno);

                dtRetorno.Columns.Add("ClienteNombre",typeof(String));
                if (_URLGateway != string.Empty)
                    foreach (DataRow fila in dtRetorno.Rows)
                    {
                        fila["ClienteNombre"] = consultaClienteCRM( int.Parse( fila[0].ToString() ));
                    }

            }
            catch (Exception Ex)
            {

            }
            return dtRetorno;

        }


        public override DetalleClientePedidoExcel ObtieneDetalleClientePedidoExcel(string PedidoReferencia, Conexion _conexion)
        {
            try
            {
                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBExcelDetalleClientePedido";

                _conexion.Comando.Parameters.Clear();
                _conexion.Comando.Parameters.Add(new SqlParameter("@PedidoReferencia", System.Data.SqlDbType.VarChar,20)).Value = PedidoReferencia;
                SqlDataReader reader = _conexion.Comando.ExecuteReader();

                DetalleClientePedidoExcel objRespuesta = new DetalleClientePedidoExcel();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objRespuesta.Cliente = Convert.ToInt32(reader["Cliente"]);
                        objRespuesta.ClientePadre = Convert.ToInt32(reader["ClientePadre"]);
                        objRespuesta.NombreCliente = Convert.ToString(reader["Nombre"]);
                    }
                    reader.Close();
                }
                     ClienteException ObjClienteException = new ClienteException();
                    ObjClienteException.ResultadoValidacion.CodigoError = 0;
                                    ObjClienteException.ResultadoValidacion.Mensaje = "Proceso exitoso";
                                    ObjClienteException.ResultadoValidacion.VerificacionValida = true;

                return objRespuesta;
            }
            catch (Exception ex)
            {
                ClienteException ObjClienteException = new ClienteException();
                ObjClienteException.ResultadoValidacion.CodigoError = 204;
                ObjClienteException.ResultadoValidacion.Mensaje = ex.Message;
                ObjClienteException.ResultadoValidacion.VerificacionValida = false;
                throw ex;
            }
            }



	}//end ClienteDatos

    public struct DetalleClientePedidoExcel
    {
        public int Cliente { get; set; }
        public int ClientePadre { get; set; }
        public string NombreCliente { get; set; }
    }



}//end namespace DatosSQL