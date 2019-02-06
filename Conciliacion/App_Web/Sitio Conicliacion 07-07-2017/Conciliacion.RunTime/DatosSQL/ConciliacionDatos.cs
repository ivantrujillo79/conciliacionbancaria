using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using Conciliacion.RunTime.ReglasDeNegocio;

namespace Conciliacion.RunTime.DatosSQL
{
    public class ConciliacionDatos : cConciliacion
    {
        public ConciliacionDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }

        public ConciliacionDatos(int corporativo, int sucursal, int año, short mes, int folio, DateTime finicial, DateTime ffinal, string statusconciliacion,
            int grupoconciliacion, short tipoconciliacion, int transaccionesinternas, int conciliadasinternas, int transaccionesexternas, int conciliadasexternas, decimal montoTotalExterno, decimal montoTotalInterno,
            string grupoconciliacionstr, bool accesototal, string cuentabancaria, string corporativodes, string sucursaldes, IMensajesImplementacion implementadorMensajes)
            : base(corporativo, sucursal, año, mes, folio, finicial, ffinal, statusconciliacion, grupoconciliacion, tipoconciliacion, transaccionesinternas, conciliadasinternas, transaccionesexternas, conciliadasexternas, montoTotalExterno, montoTotalInterno, grupoconciliacionstr, accesototal, cuentabancaria, corporativodes, sucursaldes, implementadorMensajes)
        {
        }

        public ConciliacionDatos(int corporativo, int sucursal, int año, short mes, int folio, DateTime finicial, DateTime ffinal, string statusconciliacion,
            int grupoconciliacion, short tipoconciliacion, int transaccionesinternas, int conciliadasinternas, int transaccionesexternas, int conciliadasexternas, decimal montoTotalExterno, decimal montoTotalInterno,
            string grupoconciliacionstr, bool accesototal, string cuentabancaria, string corporativodes, string sucursaldes, string bancostr, string ubicacionicono, IMensajesImplementacion implementadorMensajes)
            : base(corporativo, sucursal, año, mes, folio, finicial, ffinal, statusconciliacion, grupoconciliacion, tipoconciliacion, transaccionesinternas, conciliadasinternas, transaccionesexternas, conciliadasexternas, montoTotalExterno, montoTotalInterno, grupoconciliacionstr, accesototal, cuentabancaria,
            corporativodes, sucursaldes, bancostr, ubicacionicono, implementadorMensajes)
        {
        }

        public override IConciliacion CrearObjeto()
        {
            return new ConciliacionDatos(App.ImplementadorMensajes);
        }

        public override bool AgregarReferencia(cReferencia referenciaexterna, cReferencia referenciainterna)
        {
            throw new NotImplementedException();
        }

        public override bool Guardar(String usuario)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaConciliacion", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.SmallInt).Value = 0;
                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = this.Corporativo;
                    comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = this.Sucursal;
                    comando.Parameters.Add("@AñoConciliacion", System.Data.SqlDbType.Int).Value = this.Año;
                    comando.Parameters.Add("@MesConciliacion", System.Data.SqlDbType.SmallInt).Value = this.Mes;
                    comando.Parameters.Add("@FolioConciliacion ", System.Data.SqlDbType.Int).Value = this.Folio;
                    comando.Parameters.Add("@FolioExterno", System.Data.SqlDbType.Int).Value = this.ArchivoExterno.Folio;
                    comando.Parameters.Add("@UsuarioAlta", System.Data.SqlDbType.VarChar).Value = usuario;
                    comando.Parameters.Add("@GrupoConciliacion", System.Data.SqlDbType.SmallInt).Value = this.GrupoConciliacion;
                    comando.Parameters.Add("@CuentaBancoFinanciero", System.Data.SqlDbType.VarChar).Value = this.CuentaBancaria;
                    comando.Parameters.Add("@TipoConciliacion", System.Data.SqlDbType.SmallInt).Value = this.TipoConciliacion;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        this.Folio = Convert.ToInt32(reader["Folio"]);
                    }

                    if (this.TipoConciliacion != 2)
                    {
                        List<DatosArchivo> Archivos = this.ListaArchivos;
                        foreach (DatosArchivo Archivo in Archivos)
                        {
                            Archivo.SucursalConciliacion = this.Sucursal;
                            Archivo.FolioConciliacion = this.Folio;
                            resultado = Archivo.GuardarArchivoInterno();
                        }
                        if (resultado)
                            this.ImplementadorMensajes.MostrarMensaje("El Registro se guardo con éxito.");
                        else
                            this.BorrarConciliacion();
                    }
                    else
                        resultado = true;
                }
            }
            catch (SqlException ex)
            {
                this.BorrarConciliacion();

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("No se pudo guardar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return resultado;
        }

        public override bool GuardarSinInterno(String usuario)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaConciliacion", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.SmallInt).Value = 0;
                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = this.Corporativo;
                    comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = this.Sucursal;
                    comando.Parameters.Add("@AñoConciliacion", System.Data.SqlDbType.Int).Value = this.Año;
                    comando.Parameters.Add("@MesConciliacion", System.Data.SqlDbType.SmallInt).Value = this.Mes;
                    comando.Parameters.Add("@FolioConciliacion ", System.Data.SqlDbType.Int).Value = this.Folio;
                    comando.Parameters.Add("@FolioExterno", System.Data.SqlDbType.Int).Value = this.ArchivoExterno.Folio;
                    comando.Parameters.Add("@UsuarioAlta", System.Data.SqlDbType.VarChar).Value = usuario;
                    comando.Parameters.Add("@GrupoConciliacion", System.Data.SqlDbType.SmallInt).Value = this.GrupoConciliacion;
                    comando.Parameters.Add("@CuentaBancoFinanciero", System.Data.SqlDbType.VarChar).Value = this.CuentaBancaria;
                    comando.Parameters.Add("@TipoConciliacion", System.Data.SqlDbType.SmallInt).Value = this.TipoConciliacion;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        this.Folio = Convert.ToInt32(reader["Folio"]);
                    }

                    //if (this.TipoConciliacion != 2)
                    //{
                    //    List<DatosArchivo> Archivos = this.ListaArchivos;
                    //    foreach (DatosArchivo Archivo in Archivos)
                    //    {
                    //        Archivo.SucursalConciliacion = this.Sucursal;
                    //        Archivo.FolioConciliacion = this.Folio;
                    //        resultado = Archivo.GuardarArchivoInterno();
                    //    }
                    //    if (resultado)
                    //        this.ImplementadorMensajes.MostrarMensaje("El Registro se guardo con éxito.");
                    //    else
                    //        this.BorrarConciliacion();
                    //}
                    //else
                    this.ImplementadorMensajes.MostrarMensaje("El Registro se guardo con éxito.");
                    resultado = true;
                }
            }
            catch (SqlException ex)
            {
                this.BorrarConciliacion();

                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("No se pudo guardar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return resultado;
        }

        public override bool BorrarArchivosInternos()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaConciliacionArchivo", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.SmallInt).Value = 1;
                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = this.Corporativo;
                    comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = this.Sucursal;
                    comando.Parameters.Add("@AñoConciliacion", System.Data.SqlDbType.Int).Value = this.Año;
                    comando.Parameters.Add("@MesConciliacion", System.Data.SqlDbType.SmallInt).Value = this.Mes;
                    comando.Parameters.Add("@FolioConciliacion ", System.Data.SqlDbType.Int).Value = this.Folio;
                    comando.Parameters.Add("@SucursalInterno", System.Data.SqlDbType.TinyInt).Value = 0;
                    comando.Parameters.Add("@FolioInterno ", System.Data.SqlDbType.Int).Value = 0;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.ExecuteNonQuery();
                    resultado = true;
                }

            }
            catch (SqlException ex)
            {
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("No se pudo borrar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return resultado;
        }

        public override bool BorrarConciliacion()
        {
            bool resultado = false;
            try
            {
                this.BorrarArchivosInternos();
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaConciliacion", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.SmallInt).Value = 1;
                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = this.Corporativo;
                    comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = this.Sucursal;
                    comando.Parameters.Add("@AñoConciliacion", System.Data.SqlDbType.Int).Value = this.Año;
                    comando.Parameters.Add("@MesConciliacion", System.Data.SqlDbType.SmallInt).Value = this.Mes;
                    comando.Parameters.Add("@FolioConciliacion ", System.Data.SqlDbType.Int).Value = this.Folio;
                    comando.Parameters.Add("@FolioExterno", System.Data.SqlDbType.Int).Value = this.ArchivoExterno.Folio;
                    comando.Parameters.Add("@UsuarioAlta", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@GrupoConciliacion", System.Data.SqlDbType.SmallInt).Value = this.GrupoConciliacion;
                    comando.Parameters.Add("@CuentaBancoFinanciero", System.Data.SqlDbType.VarChar).Value = this.CuentaBancaria;
                    comando.Parameters.Add("@TipoConciliacion", System.Data.SqlDbType.SmallInt).Value = this.TipoConciliacion;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.ExecuteNonQuery();
                    resultado = true;
                }

            }
            catch (SqlException ex)
            {
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("No se pudo borrar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            return resultado;
        }

        public override bool CerrarConciliacion(string usuario)
        {
            bool resultado = false;
            if (this.PuedeCerrar() == true)
            {
                try
                {
                    using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                    {
                        cnn.Open();
                        SqlCommand comando = new SqlCommand("spCBCierraConciliacion", cnn);
                        comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.SmallInt).Value = 1;
                        comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = this.Corporativo;
                        comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = this.Sucursal;
                        comando.Parameters.Add("@AñoConciliacion", System.Data.SqlDbType.Int).Value = this.Año;
                        comando.Parameters.Add("@MesConciliacion", System.Data.SqlDbType.Int).Value = this.Mes;
                        comando.Parameters.Add("@FolioConciliacion", System.Data.SqlDbType.Int).Value = this.Folio;
                        comando.Parameters.Add("@UsuarioCierre", System.Data.SqlDbType.VarChar).Value = usuario;
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            this.StatusConciliacion = Convert.ToString(reader["StatusConciliacion"]);
                            resultado = true;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    stackTrace = new StackTrace();
                    this.ImplementadorMensajes.MostrarMensaje("No se pudo guardar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                    stackTrace = null;
                    resultado = false;
                }
            }
            else
            {
                this.ImplementadorMensajes.MostrarMensaje("No se puede cerrar la conciliacion, hay registros pendientes: Pendientes externos " + Convert.ToString(this.TransaccionesExternas - this.ConciliadasExternas) + " y pendientes internos/pedidos " + Convert.ToString(this.TransaccionesInternas - this.ConciliadasInternas));
                resultado = false;
            }

            return resultado;
        }

        public override bool PuedeCerrar()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBCierraConciliacion", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.SmallInt).Value = 0;
                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = this.Corporativo;
                    comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = this.Sucursal;
                    comando.Parameters.Add("@AñoConciliacion", System.Data.SqlDbType.Int).Value = this.Año;
                    comando.Parameters.Add("@MesConciliacion", System.Data.SqlDbType.Int).Value = this.Mes;
                    comando.Parameters.Add("@FolioConciliacion", System.Data.SqlDbType.SmallInt).Value = this.Folio;
                    comando.Parameters.Add("@UsuarioCierre", System.Data.SqlDbType.VarChar).Value = "";
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        this.TransaccionesInternas = Convert.ToInt32(reader["TransaccionesInternas"]);
                        this.TransaccionesExternas = Convert.ToInt32(reader["TransaccionesExternas"]);
                        this.ConciliadasInternas = Convert.ToInt32(reader["ConciliadasInternas"]);
                        this.ConciliadasExternas = Convert.ToInt32(reader["ConciliadasExternas"]);

                        if ((this.TransaccionesExternas - this.ConciliadasExternas == 0) && (this.TransaccionesInternas - this.ConciliadasInternas == 0))
                            resultado = true;
                        else
                            resultado = false;
                    }
                }
            }
            catch (SqlException ex)
            {
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("No se pudo consultar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
                resultado = false;
            }
            return resultado;
        }


        public override bool CancelarConciliacion(string usuario)
        {
            bool resultado = false;
            if (this.PuedeCancelar())
            {
                try
                {
                    using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                    {
                        cnn.Open();
                        SqlCommand comando = new SqlCommand("spCBCierraConciliacion", cnn);
                        comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.SmallInt).Value = 3;
                        comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = this.Corporativo;
                        comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = this.Sucursal;
                        comando.Parameters.Add("@AñoConciliacion", System.Data.SqlDbType.Int).Value = this.Año;
                        comando.Parameters.Add("@MesConciliacion", System.Data.SqlDbType.Int).Value = this.Mes;
                        comando.Parameters.Add("@FolioConciliacion", System.Data.SqlDbType.Int).Value = this.Folio;
                        comando.Parameters.Add("@UsuarioCierre", System.Data.SqlDbType.VarChar).Value = usuario;
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            this.StatusConciliacion = Convert.ToString(reader["StatusConciliacion"]);
                            resultado = true;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    stackTrace = new StackTrace();
                    this.ImplementadorMensajes.MostrarMensaje("No se pudo cancelar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                    stackTrace = null;
                    resultado = false;
                }
            }
            else
            {
                this.ImplementadorMensajes.MostrarMensaje("No se puede cancelar la conciliacion, hay registros conciliados: conciliados externos " + Convert.ToString(this.ConciliadasExternas) + " y conciliados internos/pedidos " + Convert.ToString(this.ConciliadasInternas));
                resultado = false;
            }

            return resultado;
        }

        public override bool PuedeCancelar()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBCierraConciliacion", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.SmallInt).Value = 2;
                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = this.Corporativo;
                    comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = this.Sucursal;
                    comando.Parameters.Add("@AñoConciliacion", System.Data.SqlDbType.Int).Value = this.Año;
                    comando.Parameters.Add("@MesConciliacion", System.Data.SqlDbType.Int).Value = this.Mes;
                    comando.Parameters.Add("@FolioConciliacion", System.Data.SqlDbType.SmallInt).Value = this.Folio;
                    comando.Parameters.Add("@UsuarioCierre", System.Data.SqlDbType.VarChar).Value = "";
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        this.ConciliadasInternas = Convert.ToInt32(reader["ConciliadasInternas"]);
                        this.ConciliadasExternas = Convert.ToInt32(reader["ConciliadasExternas"]);
                        resultado = (this.ConciliadasExternas == 0) && (this.ConciliadasInternas == 0);
                    }
                }
            }
            catch (SqlException ex)
            {
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("No se pudo consultar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
                resultado = false;
            }
            return resultado;
        }


    }
}
