using System;
using System.Data.SqlClient;
using System.Data;
using Conciliacion.RunTime;

namespace Conciliacion.RunTime.DatosSQL
{
    public class Conexion
    {
        private SqlConnection Conexionbd;
        public SqlCommand Comando { get; set; }
        private SqlTransaction Transaccion;

        public Conexion()
        {
            try
            {
                Conexionbd = new SqlConnection();
                Conexionbd.ConnectionString = App.CadenaConexion;
            }
            catch
            {
                throw;
            }
        }

        public Conexion(Boolean archivos)
        {
            try
            {
                Conexionbd = new SqlConnection();
                Conexionbd.ConnectionString = App.CadenaConexion;

            }
            catch
            {
                throw;
            }
        }

        public void AbrirConexion(Boolean transaccion)
        {
            if (Conexionbd.State == ConnectionState.Closed)
            {
                try
                {
                    Conexionbd.Open();
                    Comando = Conexionbd.CreateCommand();
                    Comando.CommandTimeout = 0;
                    if (transaccion)
                    {
                        Transaccion = Conexionbd.BeginTransaction(IsolationLevel.ReadCommitted);
                        Comando.Transaction = Transaccion;
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        public void AbrirConexion(Boolean transaccion, Boolean NoRepetible)
        {
            if (Conexionbd.State == ConnectionState.Closed)
            {
                try
                {
                    Conexionbd.Open();
                    Comando = Conexionbd.CreateCommand();
                    Comando.CommandTimeout = 0;
                    if (transaccion)
                    {
                        if (NoRepetible)
                        {
                            Transaccion = Conexionbd.BeginTransaction(IsolationLevel.ReadUncommitted);
                        }
                        else
                        {
                            Transaccion = Conexionbd.BeginTransaction(IsolationLevel.ReadUncommitted);
                        }
                        Comando.Transaction = Transaccion;
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        public void CerrarConexion()
        {
            try
            {
                if (Conexionbd.State == ConnectionState.Open)
                {
                    Conexionbd.Close();
                    Conexionbd.Dispose();
                    if (Transaccion != null)
                    {
                        Transaccion.Dispose();
                    }
                    Comando.Dispose();
                }
            }
            catch
            {
                throw;
            }
        }

        public void RollBackTransaction()
        {
            try
            {

                if (Transaccion != null)
                {
                    Transaccion.Rollback();

                    Transaccion = null;
                }


            }
            catch
            {
                throw;
            }
        }

        public void CommitTransaction()
        {
            try
            {
                if (Transaccion != null)
                {
                    Transaccion.Commit();

                    Transaccion = null;
                }
            }
            catch
            {
                throw;
            }
        }

    }
}
