using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CatalogoConciliacion.ReglasNegocio;
using System.Data.SqlClient;
using Conciliacion.Migracion.Runtime.ReglasNegocio;
using Conciliacion.RunTime;
using Conciliacion.RunTime.ReglasDeNegocio;
using MotivoNoConciliado = CatalogoConciliacion.ReglasNegocio.MotivoNoConciliado;

namespace CatalogoConciliacion.Datos
{
    public class ConsultasDatos : CatalogoConciliacion.ReglasNegocio.Consultas
    {
        public override CatalogoConciliacion.ReglasNegocio.Consultas CrearObjeto()
        {
            return new ConsultasDatos();
        }

        //OBTIENE LOS DATOS DE UN MOTIVO ESPECIFICO
        public override MotivoNoConciliado ObtieneMotivoPorId(int configuracion, int idMotivoNoConciliado)
        {
            MotivoNoConciliado motivo = new MotivoNoConciliadoDatos(this.implementadorMensajes);
            using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
            {
                cnn.Open();
                SqlCommand comando = new SqlCommand("spCBConsultaMotivoNoConciliado", cnn);
                comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.Int).Value = configuracion;
                comando.Parameters.Add("@MotivoNoConciliado", System.Data.SqlDbType.Int).Value = idMotivoNoConciliado;

                comando.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    motivo.MotivoNoConciliadoId = Convert.ToInt16(reader["motivonononciliado"]);
                    motivo.Descripcion = reader["descripcion"].ToString();
                    motivo.Status = reader["status"].ToString();
                }
                return motivo;
            }
        }

        //OBTIENE LOS DATOS DE UN GRUPO ESPECIFICO
        public override GrupoConciliacion ObtieneGrupoPorId(int configuracion, int idGrupoConciliacion)
        {
            GrupoConciliacion grupo = new GrupoConciliacionDatos(this.implementadorMensajes);
            using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
            {
                cnn.Open();
                SqlCommand comando = new SqlCommand("spCBConsultaGrupoConciliacion", cnn);
                comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.Int).Value = configuracion;
                comando.Parameters.Add("@GrupoConciliacion", System.Data.SqlDbType.Int).Value = idGrupoConciliacion;

                comando.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    grupo.GrupoConciliacionId = Convert.ToInt16(reader["GrupoConciliacionId"]);
                    grupo.Descripcion = reader["Descripcion"].ToString();
                    grupo.Status = reader["Status"].ToString();
                    grupo.FAlta = Convert.ToDateTime(reader["Falta"]);

                }
                return grupo;
            }
        }

        //OBTIENE TODOS LOS USUARIOS PERTENECIENTES A UN GRUPO ESPECIFICO
        public override List<GrupoConciliacionUsuario> ObtieneUsuariosPorGrupo(int configuracion,
                                                                               int idGrupoConciliacion)
        {
            List<GrupoConciliacionUsuario> usuarios = new List<GrupoConciliacionUsuario>();
            using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
            {
                cnn.Open();
                SqlCommand comando = new SqlCommand("spCBConsultaGrupoConciliacionUsuario", cnn);
                comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.Int).Value = configuracion;
                comando.Parameters.Add("@GrupoConciliacion", System.Data.SqlDbType.Int).Value = idGrupoConciliacion;
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    GrupoConciliacionUsuario usuario =
                        new GrupoConciliacionUsuarioDatos(Convert.ToInt32(reader["grupoconciliacion"]),
                                                          Convert.ToString(reader["Usuario"]),
                                                          Convert.ToBoolean(reader["accesototal"]),
                                                          this.implementadorMensajes);

                    usuarios.Add(usuario);
                }
                return usuarios;
            }
        }


        //OBTIENE TODOS LOS MOTIVOS
        public override List<MotivoNoConciliado> ObtieneMotivos(int configuracion, int idMotivoNoConciliado)
        {
            List<MotivoNoConciliado> motivos = new List<MotivoNoConciliado>();
            using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
            {
                cnn.Open();
                SqlCommand comando = new SqlCommand("spCBConsultaMotivoNoConciliado", cnn);
                comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.Int).Value = configuracion;
                comando.Parameters.Add("@MotivoNoConciliado", System.Data.SqlDbType.Int).Value = idMotivoNoConciliado;

                comando.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    MotivoNoConciliado motivo =
                        new MotivoNoConciliadoDatos(Convert.ToInt32(reader["MotivoNoConciliadoId"]),
                                                    Convert.ToString(reader["Descripcion"]),
                                                    Convert.ToString(reader["Status"]),
                                                    this.implementadorMensajes);
                    motivos.Add(motivo);
                }
                return motivos;
            }
        }


        //OBTIENE TODOS LOS GRUPOS
        public override List<GrupoConciliacion> ObtieneGrupos(int configuracion, int idGrupoConciliacion)
        {
            List<GrupoConciliacion> grupos = new List<GrupoConciliacion>();
            using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
            {
                cnn.Open();
                SqlCommand comando = new SqlCommand("spCBConsultaGrupoConciliacion", cnn);
                comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.Int).Value = configuracion;
                comando.Parameters.Add("@GrupoConciliacion", System.Data.SqlDbType.Int).Value = idGrupoConciliacion;
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    GrupoConciliacion grupo = new GrupoConciliacionDatos(Convert.ToInt32(reader["GrupoConciliacionId"]),
                                                                         Convert.ToString(reader["Descripcion"]),
                                                                         Convert.ToString(reader["Usuario"]),
                                                                         Convert.ToString(reader["Status"]),
                                                                         Convert.ToDateTime(reader["FAlta"]),
                                                                         Convert.ToInt32(reader["DiferenciaDiasDefault"]),
                                                                         Convert.ToInt32(reader["DiferenciaDiasMaxima"]),
                                                                         Convert.ToInt32(reader["DiferenciaDiasMinima"]),
                                                                         Convert.ToDecimal(
                                                                             reader["DiferenciaCentavosDefault"]),
                                                                         Convert.ToDecimal(
                                                                             reader["DiferenciaCentavosMaxima"]),
                                                                         Convert.ToDecimal(
                                                                             reader["DiferenciaCentavosMinima"]),
                                                                         this.implementadorMensajes);

                    grupos.Add(grupo);
                }
                return grupos;
            }

        }

        // Llena el combo empleado 
        public override List<ListaCombo> ObtieneEmpleados(int configuracion, int grupo)
        {
            List<ListaCombo> datos = new List<ListaCombo>();
            using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
            {
                cnn.Open();
                SqlCommand comando = new SqlCommand("spCBCargarComboEmpleado", cnn);
                comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.TinyInt).Value = configuracion;
                comando.Parameters.Add("@GrupoConciliciacion", System.Data.SqlDbType.TinyInt).Value = grupo;

                comando.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {

                    ListaCombo dato = new ListaCombo(Convert.ToInt32(reader["Identificador"]),
                                                     Convert.ToString(reader["Descripcion"]),
                                                     Convert.ToString(reader["Campo1"]));
                    datos.Add(dato);
                }
                return datos;
            }
        }


        //OBTIENE TODAS LAS REFERNCIAS
        public override List<ReferenciaAComparar> ObtieneReferencias(int configuracion)
        {
            List<ReferenciaAComparar> referencias = new List<ReferenciaAComparar>();
            using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
            {
                cnn.Open();
                SqlCommand comando = new SqlCommand("spCBConsultaReferenciaAComparar", cnn);
                comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.Int).Value = configuracion;
                comando.Parameters.Add("@TipoConciliacion", System.Data.SqlDbType.Int).Value = 0;
                comando.Parameters.Add("@Secuencia", System.Data.SqlDbType.Int).Value = 0;
                comando.Parameters.Add("@ColumnaDestinoExt", System.Data.SqlDbType.VarChar).Value = "";
                comando.Parameters.Add("@ColumnaDestinoInt", System.Data.SqlDbType.VarChar).Value = "";
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    ReferenciaAComparar referencia =
                        new ReferenciaACompararDatos(Convert.ToInt32(reader["TipoConciliacion"]),
                                                     Convert.ToString(reader["TipoConciliacionDescripcion"]),
                                                     Convert.ToInt32(reader["Secuencia"]),
                                                     Convert.ToString(reader["ColumnaDestinoExt"]),
                                                     Convert.ToString(reader["ColumnaDestinoInt"]),
                                                     Convert.ToString(reader["Status"]),
                                                     this.implementadorMensajes);

                    referencias.Add(referencia);
                }
                return referencias;
            }

        }

        //OBTIENE LOS DATOS DE UNA REFERENCIA ESPECIFICA
        public override List<ReferenciaAComparar> ObtieneReferenciaPorIdLista(int configuracion, int tipoconciliacion,
                                                                              int secuencia, string columnaext,
                                                                              string columnain)
        {
            List<ReferenciaAComparar> referencias = new List<ReferenciaAComparar>();
            using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
            {
                cnn.Open();
                SqlCommand comando = new SqlCommand("spCBConsultaReferenciaAComparar", cnn);
                comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.Int).Value = configuracion;
                comando.Parameters.Add("@TipoConciliacion", System.Data.SqlDbType.Int).Value = tipoconciliacion;
                comando.Parameters.Add("@Secuencia", System.Data.SqlDbType.Int).Value = secuencia;
                comando.Parameters.Add("@ColumnaDestinoExt", System.Data.SqlDbType.VarChar).Value = columnaext;
                comando.Parameters.Add("@ColumnaDestinoInt", System.Data.SqlDbType.VarChar).Value = columnain;
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    ReferenciaAComparar referencia =
                        new ReferenciaACompararDatos(Convert.ToInt32(reader["TipoConciliacion"]),
                                                     Convert.ToString(reader["TipoConciliacionDescripcion"]),
                                                     Convert.ToInt32(reader["Secuencia"]),
                                                     Convert.ToString(reader["ColumnaDestinoExt"]),
                                                     Convert.ToString(reader["ColumnaDestinoExt"]),
                                                     Convert.ToString(reader["Status"]),
                                                     this.implementadorMensajes);

                    referencias.Add(referencia);
                }
                return referencias;
            }
        }

        //OBTIENE LOS DATOS DE UNA REFERENCIA ESPECIFICA
        public override ReferenciaAComparar ObtieneReferenciaPorId(int configuracion, int tipoconciliacion,
                                                                   int secuencia)
        {
            ReferenciaAComparar referencia = new ReferenciaACompararDatos(this.implementadorMensajes);
            using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
            {
                cnn.Open();
                SqlCommand comando = new SqlCommand("spCBConsultaReferenciaAComparar", cnn);
                comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.Int).Value = configuracion;
                comando.Parameters.Add("@TipoConciliacion", System.Data.SqlDbType.Int).Value = tipoconciliacion;
                comando.Parameters.Add("@Secuencia", System.Data.SqlDbType.Int).Value = secuencia;
                comando.Parameters.Add("@ColumnaDestinoExt", System.Data.SqlDbType.VarChar).Value = "";
                comando.Parameters.Add("@ColumnaDestinoInt", System.Data.SqlDbType.VarChar).Value = "";
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    referencia.Secuencia = Convert.ToInt16(reader["secuencia"]);
                    referencia.ColumnaDestinoExt = reader["columnaDestinoExt"].ToString();
                    referencia.ColumnaDestinoInt = reader["columnaDestinoInt"].ToString();
                    referencia.TipoConciliacion = Convert.ToInt16(reader["tipoConciliacion"]);
                    referencia.Status = reader["status"].ToString();

                }
                return referencia;
            }
        }

        // Llena el combo columnas
        public override List<ListaCombo> ObtieneColumnas(int configuracion, int tipoconciliacion, string campoexterno)
        {
            List<ListaCombo> datos = new List<ListaCombo>();
            using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
            {
                cnn.Open();
                SqlCommand comando = new SqlCommand("spCBCargarComboDestino", cnn);
                comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.TinyInt).Value = configuracion;
                comando.Parameters.Add("@TipoConciliacion", System.Data.SqlDbType.TinyInt).Value = tipoconciliacion;
                comando.Parameters.Add("@CampoExterno", System.Data.SqlDbType.VarChar).Value = campoexterno;

                comando.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {

                    ListaCombo dato = new ListaCombo(Convert.ToInt32(reader["Identificador"]),
                                                     Convert.ToString(reader["Descripcion"]),
                                                     Convert.ToString(reader["Campo1"]));
                    datos.Add(dato);
                }
                return datos;
            }
        }

        public override List<CuentaTransferencia> ObtenieneCuentasTransferenciaOrigenDestino(int configuracion, int corporativoOrigen,
                                                                                int sucursalOrigen,
                                                                                string cuentaBancoOrigen, int banco)
        {
            List<CuentaTransferencia> datos = new List<CuentaTransferencia>();
            using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
            {
                cnn.Open();
                SqlCommand comando = new SqlCommand("spCBConsultaCuentaTransferencia", cnn);
                comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.TinyInt).Value = configuracion;
                comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = corporativoOrigen;
                comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = sucursalOrigen;
                comando.Parameters.Add("@CuentaBanco", System.Data.SqlDbType.VarChar).Value = cuentaBancoOrigen;
                comando.Parameters.Add("@Banco", System.Data.SqlDbType.TinyInt).Value = banco;

                comando.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    CuentaTransferencia dato =
                        new CuentaTransferenciaDatos(Convert.ToInt16(reader["CuentaTransferencia"]),
                                                     Convert.ToString(reader["CorporativoOrigenDesc"]),
                                                     Convert.ToInt16(reader["CorporativoOrigen"]),
                                                     Convert.ToString(reader["SucursalOrigenDesc"]),
                                                     Convert.ToInt16(reader["SucursalOrigen"]),
                                                     Convert.ToString(reader["CuentaBancoOrigen"]),
                                                     Convert.ToInt32(reader["BancoOrigen"]),
                                                     Convert.ToString(reader["BancoNombreOrigen"]),
                                                     Convert.ToString(reader["CorporativoDestinoDesc"]),
                                                     Convert.ToInt16(reader["CorporativoDestino"]),
                                                     Convert.ToString(reader["SucursalDestinoDesc"]),
                                                     Convert.ToInt16(reader["SucursalDestino"]),
                                                     Convert.ToString(reader["CuentaBancoDestino"]),
                                                     Convert.ToInt32(reader["BancoDestino"]),
                                                     Convert.ToString(reader["BancoNombreDestino"]),
                                                     Convert.ToString(reader["Status"]),
                                                     Convert.ToString(reader["UsuarioAlta"]),
                                                     Convert.ToDateTime(reader["FAlta"]),
                                                     this.implementadorMensajes);
                    datos.Add(dato);
                }
                return datos;
            }
        }

        public override List<CuentaTransferencia> ObtenieneCuentasTransferenciaOrigenDestinoTodas(int configuracion, int corporativoOrigen,
                                                                                int sucursalOrigen,
                                                                                string cuentaBancoOrigen, int banco)
        {
            List<CuentaTransferencia> datos = new List<CuentaTransferencia>();
            using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
            {
                cnn.Open();
                SqlCommand comando = new SqlCommand("spCBConsultaCuentaTransferencia", cnn);
                comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.TinyInt).Value = configuracion;
                comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = corporativoOrigen;
                comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = sucursalOrigen;
                comando.Parameters.Add("@CuentaBanco", System.Data.SqlDbType.VarChar).Value = cuentaBancoOrigen;
                comando.Parameters.Add("@Banco", System.Data.SqlDbType.TinyInt).Value = banco;

                comando.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    CuentaTransferencia dato =
                        new CuentaTransferenciaDatos(Convert.ToInt16(reader["CuentaTransferencia"]),
                                                     Convert.ToString(reader["CorporativoOrigenDesc"]),
                                                     Convert.ToInt16(reader["CorporativoOrigen"]),
                                                     Convert.ToString(reader["SucursalOrigenDesc"]),
                                                     Convert.ToInt16(reader["SucursalOrigen"]),
                                                     Convert.ToString(reader["CuentaBancoOrigen"]),
                                                     Convert.ToInt32(reader["BancoOrigen"]),
                                                     Convert.ToString(reader["BancoNombreOrigen"]),
                                                     Convert.ToString(reader["CorporativoDestinoDesc"]),
                                                     Convert.ToInt16(reader["CorporativoDestino"]),
                                                     Convert.ToString(reader["SucursalDestinoDesc"]),
                                                     Convert.ToInt16(reader["SucursalDestino"]),
                                                     Convert.ToString(reader["CuentaBancoDestino"]),
                                                     Convert.ToInt32(reader["BancoDestino"]),
                                                     Convert.ToString(reader["BancoNombreDestino"]),
                                                     Convert.ToString(reader["Status"]),
                                                     Convert.ToString(reader["UsuarioAlta"]),
                                                     Convert.ToDateTime(reader["FAlta"]),
                                                     this.implementadorMensajes);
                    datos.Add(dato);
                }
                return datos;
            }
        }
        public override GrupoConciliacionUsuario ObtieneGrupoConciliacionUsuarioEspecifico(string usuario)
        {
            GrupoConciliacionUsuario dato = new GrupoConciliacionUsuarioDatos(this.implementadorMensajes);
            using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBConsultaGrupoConciliacionUsuario", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.Int).Value = 2;
                    comando.Parameters.Add("@GrupoConciliacion", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@Usuario", System.Data.SqlDbType.VarChar).Value = usuario;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        dato = new GrupoConciliacionUsuarioDatos(
                            Convert.ToInt32(reader["GrupoConciliacion"]),
                            Convert.ToString(reader["Usuario"]),
                            Convert.ToBoolean(reader["AccesoTotal"]),
                            this.ImplementadorMensajes);
                    }

                }
                catch (SqlException ex)
                {
                    stackTrace = new StackTrace();
                    this.ImplementadorMensajes.MostrarMensaje("Error al consultar la información.\n\rClase :" +
                                                              this.GetType().Name + "\n\r" + "Método :" +
                                                              stackTrace.GetFrame(0).GetMethod().Name + "\n\r" +
                                                              "Error :" + ex.Message);
                    stackTrace = null;
                }
                return dato;
            }
        }

        public override List<CuentaBancoSaldo> ConsultaCuentaBancariaSaldoFinalDia(int corporativo, int sucursal, int banco,//,int grupoconciliacion
            string cuentabancaria, DateTime fconsulta)
        {

            List<CuentaBancoSaldo> datos = new List<CuentaBancoSaldo>();
            using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
            {
                cnn.Open();
                SqlCommand comando = new SqlCommand("spCBConsultaSaldoFinalCuentaBanco", cnn);
                comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = corporativo;
                comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = sucursal;
                //comando.Parameters.Add("@GrupoConciliacion", System.Data.SqlDbType.Int).Value = grupoconciliacion;
                if (banco != -1)
                    comando.Parameters.Add("@Banco", System.Data.SqlDbType.Int).Value = banco;
                if(!cuentabancaria.Equals("TODAS"))
                comando.Parameters.Add("@CuentaBancoFinanciero", System.Data.SqlDbType.VarChar).Value = cuentabancaria;
                comando.Parameters.Add("@FConsulta", System.Data.SqlDbType.DateTime).Value = fconsulta;
                comando.CommandTimeout = 900;
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    CuentaBancoSaldo dato =
                        new CuentaBancoSaldoDatos(corporativo,
                                                     sucursal,
                                                     Convert.ToInt16(reader["Banco"]),
                                                     Convert.ToString(reader["BancoDescripcion"]),
                                                     Convert.ToString(reader["CuentaBancoFinanciero"]),
                                                     Convert.ToDecimal(reader["SaldoInicialMes"]),
                                                     Convert.ToDecimal(reader["SaldoFinal"]),
                                                     this.implementadorMensajes);
                    datos.Add(dato);
                }
                return datos;
            }
        }

    }
}

