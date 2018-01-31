﻿using Conciliacion.RunTime.ReglasDeNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.DatosSQL
{
    public class PagoAnticipadoDatos : PagoAnticipado
    {

        public PagoAnticipadoDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }
        public PagoAnticipadoDatos(int cliente,
            int corporativoconciliacion,
            int sucursalconciliacion,
            int añoconciliacion,
            int mesconciliacion,
            int folioconciliacion,
            int Secuenciarelacion,
            int corporativoexteno,
            int sucursalexterno,
            int añoexterno,
            int folioexterno,
            int secuenciaexterno,
            decimal montoexterno,
            int formaconciliacion,
            int statusconcepto,
            int statusconciliacion,
            int motivonoconciliado,
            string comentarionoconciliado,
            string usuario,
            DateTime falta,
            string descripcion,
            string usuariostatusconcepto,
            DateTime fstatusconcepto, IMensajesImplementacion implementadorMensajes)
            : base(corporativoconciliacion,
                    sucursalconciliacion,
                    añoconciliacion,
                    mesconciliacion,
                    folioconciliacion,
                    Secuenciarelacion,
                    corporativoexteno,
                    sucursalexterno,
                    añoexterno,
                    folioexterno,
                    secuenciaexterno,
                    montoexterno,
                    formaconciliacion,
                    statusconcepto,
                    statusconciliacion,
                    motivonoconciliado,
                    comentarionoconciliado,
                    usuario,
                    falta,
                    descripcion,
                    usuariostatusconcepto,
                    fstatusconcepto, implementadorMensajes)
        {
        }

        public override PagoAnticipado CrearObjeto()
        {
            return new PagoAnticipadoDatos(this.ImplementadorMensajes);
        }

        public override bool ValidarClientePagoAnticipado(Conexion _conexion, Int64 NumeroCliente)
        {
            bool resultado = false;
            try
            {
                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBValidaClientePagoAnticipado";
                _conexion.Comando.Parameters.Clear();
                _conexion.Comando.Parameters.Add(new SqlParameter("@NumeroCliente", System.Data.SqlDbType.BigInt)).Value = NumeroCliente;

                SqlDataReader reader = _conexion.Comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        resultado = Convert.ToInt16(reader["VALIDO"]) == 1;
                        break;
                    }
                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }

        public override bool RegistraConciliacionReferencia(Conexion _conexion)
            //,int CorporativoConciliacion, int SucursalConciliacion, int AñoConciliacion, int MesConciliacion, int FolioConciliacion, int SecuenciaRelacion, int CorporativoExterno, int SucursalExterno, int AñoExterno, int FolioExterno, int SecuenciaExterno, decimal MontoExterno, int FormaConciliacion, int StatusConcepto, string StatusConciliacion, int MotivoNoConciliado, string ComentarioNoConciliado, string Usuario, DateTime FAlta, string Descripcion, string UsuarioStatusConcepto, DateTime FStatusConcepto)
        {
            bool resultado = false;
            this.StatusConcepto = 28;
            this.StatusConciliacion = 1; // "CONCILIACION CANCELADA";
            this.MotivoNoConciliado = 1;
            try
            {
                _conexion.Comando.CommandType = CommandType.StoredProcedure;
                _conexion.Comando.CommandText = "spCBAltaConciliacionReferencia";
                _conexion.Comando.Parameters.Clear();
                _conexion.Comando.Parameters.Add(new SqlParameter("@CorporativoConciliacion", System.Data.SqlDbType.SmallInt)).Value = CorporativoConciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@SucursalConciliacion", System.Data.SqlDbType.SmallInt)).Value = SucursalConciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@AñoConciliacion", System.Data.SqlDbType.Int)).Value = AñoConciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@MesConciliacion", System.Data.SqlDbType.SmallInt)).Value = MesConciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FolioConciliacion", System.Data.SqlDbType.Int)).Value = FolioConciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@SecuenciaRelacion", System.Data.SqlDbType.Int)).Value = SecuenciaRelacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@CorporativoExterno", System.Data.SqlDbType.SmallInt)).Value = CorporativoExteno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@SucursalExterno", System.Data.SqlDbType.SmallInt)).Value = SucursalExterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@AñoExterno", System.Data.SqlDbType.Int)).Value = AñoExterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FolioExterno", System.Data.SqlDbType.Int)).Value = FolioExterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@SecuenciaExterno", System.Data.SqlDbType.Int)).Value = SecuenciaExterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@MontoExterno", System.Data.SqlDbType.Decimal)).Value = MontoExterno;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FormaConciliacion", System.Data.SqlDbType.SmallInt)).Value = FormaConciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@StatusConcepto", System.Data.SqlDbType.SmallInt)).Value = 28;
                //_conexion.Comando.Parameters.Add(new SqlParameter("@StatusConciliacion", System.Data.SqlDbType.VarChar)).Value = StatusConciliacion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@StatusConciliacion", System.Data.SqlDbType.VarChar)).Value = "CONCILIADA";
                _conexion.Comando.Parameters.Add(new SqlParameter("@MotivoNoConciliado", System.Data.SqlDbType.SmallInt)).Value = MotivoNoConciliado;
                _conexion.Comando.Parameters.Add(new SqlParameter("@ComentarioNoConciliado", System.Data.SqlDbType.VarChar)).Value = ComentarioNoConciliado;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Usuario", System.Data.SqlDbType.Char)).Value = Usuario;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FAlta", System.Data.SqlDbType.DateTime)).Value = FAlta;
                //_conexion.Comando.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar)).Value = Descripcion;
                _conexion.Comando.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar)).Value = System.DBNull.Value;
                //_conexion.Comando.Parameters.Add(new SqlParameter("@UsuarioStatusConcepto", System.Data.SqlDbType.Char)).Value = UsuarioStatusConcepto;
                _conexion.Comando.Parameters.Add(new SqlParameter("@UsuarioStatusConcepto", System.Data.SqlDbType.Char)).Value = System.DBNull.Value;
                //_conexion.Comando.Parameters.Add(new SqlParameter("@FStatusConcepto", System.Data.SqlDbType.DateTime)).Value = FStatusConcepto;
                _conexion.Comando.Parameters.Add(new SqlParameter("@FStatusConcepto", System.Data.SqlDbType.DateTime)).Value = System.DBNull.Value;

                int RegistrosAfectados = _conexion.Comando.ExecuteNonQuery();
                resultado = RegistrosAfectados == 1;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultado;
        }


    }
}
