using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using Conciliacion.RunTime.ReglasDeNegocio;


namespace Conciliacion.RunTime.DatosSQL
{
    class ReferenciaNoConciliadaDatos :ReferenciaNoConciliada 
    {

        public ReferenciaNoConciliadaDatos
            (int corporativo, int sucursal,string sucursaldes, int añoconciliacion, int folio, int secuencia, string descripcion,string concepto,decimal deposito, decimal retiro, short formaconciliacion, short statusconcepto, string statusconciliacion,
             DateTime foperacion, DateTime fmovimiento, int folioconciliacion, short mesconciliacion, decimal diferencia, bool coninterno,
             string cheque, string referencia, string nombretercero, string rfctercero, int sucursalconciliacion, int año, int cliente,
             IMensajesImplementacion implementadorMensajes)
            : base(corporativo,sucursal, sucursaldes,añoconciliacion,folio,secuencia,descripcion,concepto,deposito, retiro,formaconciliacion,statusconcepto,statusconciliacion, foperacion,fmovimiento, folioconciliacion,mesconciliacion,diferencia,coninterno,
            cheque,referencia,nombretercero,rfctercero,sucursalconciliacion,año, 
            implementadorMensajes)
        {

        }

     //   public ReferenciaNoConciliadaDatos(int corporativo, int sucursal, string sucursaldes, int año, int folio, int secuencia, string descripcion, string concepto, decimal deposito, decimal retiro, short formaconciliacion, short statusconcepto, string statusconciliacion,
     //DateTime foperacion, DateTime fmovimiento, int folioconciliacion, short mesconciliacion, decimal diferencia, bool coninterno,
     //       string cheque, string referencia, string nombretercero, string rfctercero,
     //       IMensajesImplementacion implementadorMensajes)
     //       : base(corporativo, sucursal, sucursaldes, año, folio, secuencia, descripcion, concepto,deposito,retiro, formaconciliacion, statusconcepto, statusconciliacion, foperacion, fmovimiento,folioconciliacion,mesconciliacion,diferencia,coninterno, 
     //       cheque,referencia,nombretercero,rfctercero,
     //       implementadorMensajes)
     //   {

     //   }

        public ReferenciaNoConciliadaDatos
(int corporativo, int sucursal, string sucursaldes, int añoconciliacion, int folio, int secuencia, string descripcion, string concepto, decimal deposito, decimal retiro, short formaconciliacion, short statusconcepto, string statusconciliacion,
DateTime foperacion, DateTime fmovimiento, string ubicacionicono, int folioconciliacion, short mesconciliacion, decimal diferencia, bool coninterno,
            string cheque, string referencia, string nombretercero, string rfctercero, int sucursalconciliacion, int año, int cliente,
            IMensajesImplementacion implementadorMensajes)
            : base(corporativo, sucursal, sucursaldes, añoconciliacion, folio, secuencia, descripcion, concepto, deposito, retiro, formaconciliacion, statusconcepto, statusconciliacion, foperacion, fmovimiento, ubicacionicono, folioconciliacion, mesconciliacion, diferencia, coninterno,
            cheque,referencia,nombretercero,rfctercero,sucursalconciliacion, año, 
            implementadorMensajes)
        {

        }
        

        public ReferenciaNoConciliadaDatos
  (int corporativo, int sucursal, string sucursaldes, int añoconciliacion, int folio, int secuencia, string descripcion, string concepto, decimal deposito, decimal retiro, short formaconciliacion, short statusconcepto, string statusconciliacion,
   DateTime foperacion, DateTime fmovimiento, int folioconciliacion, short mesconciliacion, bool coninterno, List<cReferencia> listareferenciaconciliada,
            string cheque, string referencia, string nombretercero, string rfctercero, int sucursalconciliacion, int año, int cliente,
            IMensajesImplementacion implementadorMensajes)
            : base(corporativo, sucursal, sucursaldes, añoconciliacion, folio, secuencia, descripcion, concepto, deposito, retiro, formaconciliacion, statusconcepto, statusconciliacion, foperacion, fmovimiento, folioconciliacion,
            mesconciliacion, coninterno, listareferenciaconciliada,
            cheque, referencia, nombretercero, rfctercero, sucursalconciliacion,año, 
            implementadorMensajes)
        {

        }

        public ReferenciaNoConciliadaDatos
(int corporativo, int sucursal, string sucursaldes, int añoconciliacion, int folio, int secuencia, string descripcion, string concepto, decimal deposito, decimal retiro, short formaconciliacion, short statusconcepto, string statusconciliacion,
DateTime foperacion, DateTime fmovimiento, int folioconciliacion, short mesconciliacion, bool coninterno, List<cReferencia> listareferenciaconciliada,
    string cheque, string referencia, string nombretercero, string rfctercero, int sucursalconciliacion, string tipo, string ubicacionicono, int año, int cliente,
    DetalleSaldoAFavor DetalleSaldo,
    IMensajesImplementacion implementadorMensajes)
            : base(corporativo, sucursal, sucursaldes, añoconciliacion, folio, secuencia, descripcion, concepto, deposito, retiro, formaconciliacion, statusconcepto, statusconciliacion, foperacion, fmovimiento, folioconciliacion,
            mesconciliacion, coninterno, listareferenciaconciliada,
            cheque, referencia, nombretercero, rfctercero, sucursalconciliacion, ubicacionicono,año, 
            DetalleSaldo,
            implementadorMensajes)
        {
            this.Tipo = tipo;
        }
        //******************************** NUEVO CONSTRUCTOR CONCILIACION COMPARTIDA ******************************//
        public ReferenciaNoConciliadaDatos(int corporativo, int sucursalconciliacion, int añoconciliacion,
            short mesconciliacion, int folioconciliacion,
            int corporativoex, int sucursalex, int añoex, int folioex, int secuenciaex, int consecutivoflujo,
            bool coninterno, short statusconcepto, string statusconciliacion, string ubicacionicono,
            DateTime foperacion, DateTime fmovimiento, string referencia, string descripcion,
            decimal deposito, decimal retiro, decimal saldo, int caja, string sucursalbancaria,
            string tipotraspaso, decimal? montotraspaso,int? corporativotraspaso,int? sucursaltraspaso,int? añotraspaso,int? foliotraspaso,
            List<ReferenciaConciliadaCompartida> listareferenciaconciliadacompartida,
            IMensajesImplementacion implementadorMensajes)
            : base(  corporativo,    sucursalconciliacion,   añoconciliacion,  mesconciliacion,    folioconciliacion,
                     corporativoex,   sucursalex,  añoex,  folioex,   secuenciaex,  consecutivoflujo,
                     coninterno,  statusconcepto,  statusconciliacion,  ubicacionicono,
                     foperacion,   fmovimiento,  referencia,  descripcion, 
                     deposito,   retiro,  saldo,  caja,   sucursalbancaria,
                     tipotraspaso,  montotraspaso,corporativotraspaso,sucursaltraspaso,añotraspaso,foliotraspaso,
                     listareferenciaconciliadacompartida, 
                     implementadorMensajes)
        {
        }

        public ReferenciaNoConciliadaDatos(int corporativo, int sucursalconciliacion, int añoconciliacion,
            short mesconciliacion, int folioconciliacion,
            int corporativoex, int sucursalex, int añoex, int folioex, int secuenciaex, int consecutivoflujo,
            bool coninterno, short statusconcepto, string statusconciliacion, string ubicacionicono,
            DateTime foperacion, DateTime fmovimiento, string referencia, string descripcion,
            decimal deposito, decimal retiro, decimal saldo, int caja, string sucursalbancaria,
            string tipotraspaso, decimal? montotraspaso, int? corporativotraspaso, int? sucursaltraspaso, int? añotraspaso, int? foliotraspaso,
            List<ReferenciaConciliadaCompartida> listareferenciaconciliadacompartida,
            DetalleSaldoAFavor DetalleSaldo,
            IMensajesImplementacion implementadorMensajes)
            : base(corporativo, sucursalconciliacion, añoconciliacion, mesconciliacion, folioconciliacion,
                     corporativoex, sucursalex, añoex, folioex, secuenciaex, consecutivoflujo,
                     coninterno, statusconcepto, statusconciliacion, ubicacionicono,
                     foperacion, fmovimiento, referencia, descripcion,
                     deposito, retiro, saldo, caja, sucursalbancaria,
                     tipotraspaso, montotraspaso, corporativotraspaso, sucursaltraspaso, añotraspaso, foliotraspaso,
                     listareferenciaconciliadacompartida, DetalleSaldo,
                     implementadorMensajes)
        {
        }
        public ReferenciaNoConciliadaDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
            
        }

        public override ReferenciaNoConciliada CrearObjeto()
        {
            return new ReferenciaNoConciliadaDatos(App.ImplementadorMensajes);
        }

        public override bool Guardar()
        {
            throw new NotImplementedException();
        }
        public override bool Modificar()
        {
            throw new NotImplementedException();
        }

        public override bool Eliminar()
        {
            throw new NotImplementedException();
        }

        public override bool EliminarReferenciaConciliada()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaConciliacionReferencia", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.SmallInt).Value = 1;
                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = this.Corporativo;
                    comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = this.Sucursal;
                    comando.Parameters.Add("@AñoConciliacion", System.Data.SqlDbType.Int).Value = this.AñoConciliacion;
                    comando.Parameters.Add("@MesConciliacion", System.Data.SqlDbType.SmallInt).Value = this.MesConciliacion;
                    comando.Parameters.Add("@FolioConciliacion ", System.Data.SqlDbType.Int).Value = this.FolioConciliacion;
                    comando.Parameters.Add("@SecuenciaRelacion", System.Data.SqlDbType.Int).Value = 0;

                    comando.Parameters.Add("@SucursalInterno", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@AñoInterno", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@FolioInterno", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@SecuenciaInterno", System.Data.SqlDbType.Int).Value = 0;

                    comando.Parameters.Add("@AñoExterno", System.Data.SqlDbType.Int).Value = this.Año;
                    comando.Parameters.Add("@FolioExterno", System.Data.SqlDbType.Int).Value = this.Folio;
                    comando.Parameters.Add("@SecuenciaExterno", System.Data.SqlDbType.Int).Value = this.Secuencia;

                    comando.Parameters.Add("@Concepto", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@MontoConciliado", System.Data.SqlDbType.Money).Value = 0;
                    comando.Parameters.Add("@Diferencia ", System.Data.SqlDbType.Money).Value = 0;
                    comando.Parameters.Add("@MontoExterno ", System.Data.SqlDbType.Money).Value = 0;
                    comando.Parameters.Add("@MontoInterno", System.Data.SqlDbType.Money).Value = 0;
                    comando.Parameters.Add("@FormaConciliacion", System.Data.SqlDbType.SmallInt).Value = 0;
                    comando.Parameters.Add("@StatusConcepto", System.Data.SqlDbType.SmallInt).Value = 0;
                    comando.Parameters.Add("@StatusConciliacion", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@MotivoNoConciliado", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@ComentarioNoConciliado", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar).Value = "";

                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                    resultado = true;
                }
            }
            catch (SqlException ex) 
            {
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("No se pudo borrar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
                resultado = false;
            }
            return resultado;
        }

        public override bool EliminarReferenciaConciliadaPedido()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaConciliacionPedido", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.SmallInt).Value = 1;
                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = this.Corporativo;
                    comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = this.Sucursal;
                    comando.Parameters.Add("@AñoConciliacion", System.Data.SqlDbType.Int).Value = this.AñoConciliacion;
                    comando.Parameters.Add("@FolioConciliacion ", System.Data.SqlDbType.Int).Value = this.FolioConciliacion;
                    comando.Parameters.Add("@MesConciliacion", System.Data.SqlDbType.SmallInt).Value = this.MesConciliacion;
                    
                    comando.Parameters.Add("@Celula", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@AñoPed", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@Pedido", System.Data.SqlDbType.Int).Value = 0;

                    comando.Parameters.Add("@AñoExterno", System.Data.SqlDbType.Int).Value = this.Año;
                    comando.Parameters.Add("@FolioExterno", System.Data.SqlDbType.Int).Value = this.Folio;
                    comando.Parameters.Add("@SecuenciaExterno", System.Data.SqlDbType.Int).Value = this.Secuencia;
                    comando.Parameters.Add("@Concepto", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@RemisionPedido", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@SeriePedido ", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@FolioSat", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@SerieSat", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@MontoConciliado", System.Data.SqlDbType.Money).Value = 0;
                    comando.Parameters.Add("@MontoExterno", System.Data.SqlDbType.Money).Value = 0;
                    comando.Parameters.Add("@MontoInterno", System.Data.SqlDbType.Money).Value = 0;
                    comando.Parameters.Add("@FormaConciliacion", System.Data.SqlDbType.SmallInt).Value = 0;
                    comando.Parameters.Add("@StatusConcepto", System.Data.SqlDbType.SmallInt).Value = 0;
                    comando.Parameters.Add("@StatusConciliacion", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@StatusMovimiento", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@MotivoNoConciliado", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@ComentarioNoConciliado", System.Data.SqlDbType.VarChar).Value = "";

                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                    resultado = true;
                }
            }
            catch (SqlException ex)
            {
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("No se pudo borrar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
                resultado = false;
            }
            return resultado;
        }

        public override bool EliminarReferenciaConciliadaInterno()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaConciliacionReferencia", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.SmallInt).Value = 4;
                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = this.Corporativo;
                    comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = this.SucursalConciliacion;
                    comando.Parameters.Add("@AñoConciliacion", System.Data.SqlDbType.Int).Value = this.AñoConciliacion;
                    comando.Parameters.Add("@MesConciliacion", System.Data.SqlDbType.SmallInt).Value = this.MesConciliacion;
                    comando.Parameters.Add("@FolioConciliacion ", System.Data.SqlDbType.Int).Value = this.FolioConciliacion;
                    comando.Parameters.Add("@SecuenciaRelacion", System.Data.SqlDbType.Int).Value = 0; 

                    comando.Parameters.Add("@SucursalInterno", System.Data.SqlDbType.Int).Value = this.Sucursal;
                    comando.Parameters.Add("@AñoInterno", System.Data.SqlDbType.Int).Value = this.Año;
                    comando.Parameters.Add("@FolioInterno", System.Data.SqlDbType.Int).Value = this.Folio;
                    comando.Parameters.Add("@SecuenciaInterno", System.Data.SqlDbType.Int).Value = this.Secuencia;

                    comando.Parameters.Add("@AñoExterno", System.Data.SqlDbType.Int).Value =0;
                    comando.Parameters.Add("@FolioExterno", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@SecuenciaExterno", System.Data.SqlDbType.Int).Value = 0;

                    comando.Parameters.Add("@Concepto", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@MontoConciliado", System.Data.SqlDbType.Money).Value = 0;
                    comando.Parameters.Add("@Diferencia ", System.Data.SqlDbType.Money).Value = 0;
                    comando.Parameters.Add("@MontoExterno ", System.Data.SqlDbType.Money).Value = 0;
                    comando.Parameters.Add("@MontoInterno", System.Data.SqlDbType.Money).Value = 0;
                    comando.Parameters.Add("@FormaConciliacion", System.Data.SqlDbType.SmallInt).Value = 0;
                    comando.Parameters.Add("@StatusConcepto", System.Data.SqlDbType.SmallInt).Value = 0;
                    comando.Parameters.Add("@StatusConciliacion", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@MotivoNoConciliado", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@ComentarioNoConciliado", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar).Value = "";

                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                    resultado = true;
                }
            }
            catch (SqlException ex)
            {
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("No se pudo borrar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
                resultado = false;
            }
            return resultado;
        }


        public override bool CancelarExternoPedido()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaConciliacionPedido", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.SmallInt).Value = 2;
                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = this.Corporativo;
                    comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = this.Sucursal;
                    comando.Parameters.Add("@AñoConciliacion", System.Data.SqlDbType.Int).Value = this.AñoConciliacion;
                    comando.Parameters.Add("@FolioConciliacion ", System.Data.SqlDbType.Int).Value = this.FolioConciliacion;
                    comando.Parameters.Add("@MesConciliacion", System.Data.SqlDbType.SmallInt).Value = this.MesConciliacion;
                    comando.Parameters.Add("@Celula", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@AñoPed", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@Pedido", System.Data.SqlDbType.Int).Value = 0;

                    comando.Parameters.Add("@AñoExterno", System.Data.SqlDbType.Int).Value = this.Año;
                    comando.Parameters.Add("@FolioExterno", System.Data.SqlDbType.Int).Value = this.Folio;
                    comando.Parameters.Add("@SecuenciaExterno", System.Data.SqlDbType.Int).Value = this.Secuencia;
                    comando.Parameters.Add("@Concepto", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@RemisionPedido", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@SeriePedido ", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@FolioSat", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@SerieSat", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@MontoConciliado", System.Data.SqlDbType.Money).Value = 0;
                    comando.Parameters.Add("@MontoExterno", System.Data.SqlDbType.Money).Value = this.Monto;
                    comando.Parameters.Add("@MontoInterno", System.Data.SqlDbType.Money).Value = 0;
                    comando.Parameters.Add("@FormaConciliacion", System.Data.SqlDbType.SmallInt).Value = 5;
                    comando.Parameters.Add("@StatusConcepto", System.Data.SqlDbType.SmallInt).Value = this.StatusConcepto;
                    comando.Parameters.Add("@StatusConciliacion", System.Data.SqlDbType.VarChar).Value = "CONCILIACION CANCELADA";
                    comando.Parameters.Add("@StatusMovimiento", System.Data.SqlDbType.VarChar).Value = "PENDIENTE";
                    comando.Parameters.Add("@MotivoNoConciliado", System.Data.SqlDbType.Int).Value = this.MotivoNoConciliado;
                    comando.Parameters.Add("@ComentarioNoConciliado", System.Data.SqlDbType.VarChar).Value = this.ComentarioNoConciliado;

                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                    resultado = true;
                }
            }
            catch (SqlException ex)
            {
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("No se pudo guardar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
                resultado = false;
            }
            return resultado;
        }

        public override bool CancelarExternoInterno()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaConciliacionReferencia", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.SmallInt).Value = 2;
                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = this.Corporativo;
                    comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = this.Sucursal;
                    comando.Parameters.Add("@AñoConciliacion", System.Data.SqlDbType.Int).Value = this.AñoConciliacion;
                    comando.Parameters.Add("@MesConciliacion", System.Data.SqlDbType.SmallInt).Value = this.MesConciliacion;
                    comando.Parameters.Add("@FolioConciliacion ", System.Data.SqlDbType.Int).Value = this.FolioConciliacion;
                    comando.Parameters.Add("@SecuenciaRelacion", System.Data.SqlDbType.Int).Value = 0;

                    comando.Parameters.Add("@SucursalInterno", System.Data.SqlDbType.TinyInt).Value = 0;
                    comando.Parameters.Add("@AñoInterno", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@FolioInterno", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@SecuenciaInterno", System.Data.SqlDbType.Int).Value = 0;

                    comando.Parameters.Add("@AñoExterno", System.Data.SqlDbType.Int).Value = this.Año;
                    comando.Parameters.Add("@FolioExterno", System.Data.SqlDbType.Int).Value = this.Folio;
                    comando.Parameters.Add("@SecuenciaExterno", System.Data.SqlDbType.Int).Value = this.Secuencia;

                    comando.Parameters.Add("@Concepto", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@MontoConciliado", System.Data.SqlDbType.Money).Value = 0;
                    comando.Parameters.Add("@Diferencia ", System.Data.SqlDbType.Money).Value = 0;
                    comando.Parameters.Add("@MontoExterno ", System.Data.SqlDbType.Money).Value = this.Monto;
                    comando.Parameters.Add("@MontoInterno", System.Data.SqlDbType.Money).Value = 0;
                    comando.Parameters.Add("@FormaConciliacion", System.Data.SqlDbType.SmallInt).Value = 5;
                    comando.Parameters.Add("@StatusConcepto", System.Data.SqlDbType.SmallInt).Value = this.StatusConcepto;
                    comando.Parameters.Add("@StatusConciliacion", System.Data.SqlDbType.VarChar).Value = "CONCILIACION CANCELADA";
                    comando.Parameters.Add("@MotivoNoConciliado", System.Data.SqlDbType.Int).Value = this.MotivoNoConciliado;
                    comando.Parameters.Add("@ComentarioNoConciliado", System.Data.SqlDbType.VarChar).Value =  this.ComentarioNoConciliado;
                    comando.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar).Value = "";

                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                    resultado = true;
                }
            }
            catch (SqlException ex)
            {
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("No se pudo guardar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
                resultado = false;
            }
            return resultado;
        }

        public override bool CancelarInterno()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaConciliacionReferencia", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.SmallInt).Value = 3;
                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = this.Corporativo;
                    comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = this.SucursalConciliacion; ;
                    comando.Parameters.Add("@AñoConciliacion", System.Data.SqlDbType.Int).Value = this.AñoConciliacion;
                    comando.Parameters.Add("@MesConciliacion", System.Data.SqlDbType.SmallInt).Value = this.MesConciliacion;
                    comando.Parameters.Add("@FolioConciliacion ", System.Data.SqlDbType.Int).Value = this.FolioConciliacion;
                    comando.Parameters.Add("@SecuenciaRelacion", System.Data.SqlDbType.Int).Value = 0;

                    comando.Parameters.Add("@SucursalInterno", System.Data.SqlDbType.Int).Value = this.Sucursal;
                    comando.Parameters.Add("@AñoInterno", System.Data.SqlDbType.Int).Value = this.Año;
                    comando.Parameters.Add("@FolioInterno", System.Data.SqlDbType.Int).Value = this.Folio;
                    comando.Parameters.Add("@SecuenciaInterno", System.Data.SqlDbType.Int).Value = this.Secuencia;

                    comando.Parameters.Add("@AñoExterno", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@FolioExterno", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@SecuenciaExterno", System.Data.SqlDbType.Int).Value = 0;

                    comando.Parameters.Add("@Concepto", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@MontoConciliado", System.Data.SqlDbType.Money).Value = 0;
                    comando.Parameters.Add("@Diferencia ", System.Data.SqlDbType.Money).Value = 0;
                    comando.Parameters.Add("@MontoExterno ", System.Data.SqlDbType.Money).Value = 0;
                    comando.Parameters.Add("@MontoInterno", System.Data.SqlDbType.Money).Value = this.Monto;
                    comando.Parameters.Add("@FormaConciliacion", System.Data.SqlDbType.SmallInt).Value = 5;
                    comando.Parameters.Add("@StatusConcepto", System.Data.SqlDbType.SmallInt).Value = this.StatusConcepto;
                    comando.Parameters.Add("@StatusConciliacion", System.Data.SqlDbType.VarChar).Value = "CONCILIACION CANCELADA";
                    comando.Parameters.Add("@MotivoNoConciliado", System.Data.SqlDbType.Int).Value = this.MotivoNoConciliado;
                    comando.Parameters.Add("@ComentarioNoConciliado", System.Data.SqlDbType.VarChar).Value = this.ComentarioNoConciliado;
                    comando.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar).Value = "";

                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                    resultado = true;
                }
            }
            catch (SqlException ex)
            {
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("No se pudo guardar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
                resultado = false;
            }
            return resultado;
        }

        ////ConsultarPedidosCorrespondientes por Movimiento Externo

        /*public override List<ReferenciaConciliadaPedido> ConciliarPedidoCantidadYReferenciaMovExterno(
          decimal centavos, short statusconcepto, string campoexterno, string campopedido)
        {
            List<ReferenciaConciliadaPedido> datos = new List<ReferenciaConciliadaPedido>();
            using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBConciliarPedidosPorReferenciaPorMovExterno", cnn);

                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = this.Corporativo;
                    comando.Parameters.Add("@SucursalConciliacion", System.Data.SqlDbType.Int).Value = this.SucursalConciliacion;
                    comando.Parameters.Add("@AñoConciliacion", System.Data.SqlDbType.Int).Value = this.AñoConciliacion;
                    comando.Parameters.Add("@MesConciliacion", System.Data.SqlDbType.SmallInt).Value = this.MesConciliacion;
                    comando.Parameters.Add("@FolioConciliacion", System.Data.SqlDbType.Int).Value = this.FolioConciliacion;

                    comando.Parameters.Add("@SucursalExterno", System.Data.SqlDbType.Int).Value = this.Sucursal;
                    comando.Parameters.Add("@AñoExterno", System.Data.SqlDbType.Int).Value = this.Año;
                    comando.Parameters.Add("@FolioExterno", System.Data.SqlDbType.VarChar).Value = this.Folio;
                    comando.Parameters.Add("@SecuenciaExterno", System.Data.SqlDbType.VarChar).Value = this.Secuencia;

                    comando.Parameters.Add("@Centavos", System.Data.SqlDbType.VarChar).Value = centavos;
                    comando.Parameters.Add("@StatusConcepto", System.Data.SqlDbType.VarChar).Value = statusconcepto;
                    comando.Parameters.Add("@Cadena", System.Data.SqlDbType.VarChar).Value = App.Consultas.ObtenerCadenaDeEtiquetas(0, this.Corporativo, this.SucursalConciliacion, this.AñoConciliacion, this.MesConciliacion, this.FolioConciliacion, statusconcepto);
                    comando.Parameters.Add("@CampoExterno", System.Data.SqlDbType.VarChar).Value = campoexterno;
                    comando.Parameters.Add("@CampoPedido", System.Data.SqlDbType.VarChar).Value = campopedido;

                    comando.CommandTimeout = 900;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {

                        ReferenciaConciliadaPedido dato =
                           new ReferenciaConciliadaPedidoDatos(this.Corporativo,
                                                               this.AñoConciliacion,
                                                               this.MesConciliacion,
                                                               this.FolioConciliacion,
                                                               this.Sucursal,
                                                               this.SucursalDes,
                                                               this.Folio,
                                                               this.Secuencia,
                                                               this.Concepto,
                                                               Convert.ToDecimal(reader["MontoConciliado"]),
                                                               0,
                                                               2,
                                                               this.StatusConcepto,
                                                               "EN PROCESO DE CONCILIACION",
                                                               this.FOperacion,
                                                               this.FMovimiento,

                                                               this.Cheque,
                                                               this.Referencia,
                                                               this.Descripcion,
                                                               this.NombreTercero,
                                                               this.RFCTercero,
                                                               this.Deposito,
                                                               this.Retiro,

                                                               Convert.ToInt16(reader["SucursalPedido"]),
                                                               Convert.ToString(reader["SucursalPedidoDes"]),
                                                               Convert.ToInt32(reader["CelulaPedido"]),
                                                               Convert.ToInt32(reader["AñoPedido"]),
                                                               Convert.ToInt32(reader["Pedido"]),
                                                               Convert.ToInt32(reader["RemisionPedido"]),
                                                               Convert.ToString(reader["SeriePedido"]),
                                                               Convert.ToInt32(reader["FolioSat"]),
                                                               Convert.ToString(reader["SerieSat"]),
                                                               Convert.ToString(reader["ConceptoPedido"]),
                                                               Convert.ToDecimal(reader["Total"]),
                                                               "PENDIENTE",
                                                               Convert.ToInt32(reader["Cliente"]),
                                                               Convert.ToString(reader["Nombre"]),
                                                               Convert.ToString(reader["PedidoReferencia"]),
                                                               this.AñoConciliacion,
                                                               this.implementadorMensajes);
                        datos.Add(dato);
                    }
                }
                catch (SqlException ex)
                {
                    stackTrace = new StackTrace();
                    this.ImplementadorMensajes.MostrarMensaje("Erros al consultar la informacion.\n\rClase :" +
                                                              this.GetType().Name + "\n\r" + "Metodo :" +
                                                              stackTrace.GetFrame(0).GetMethod().Name + "\n\r" +
                                                              "Error :" + ex.Message);
                    stackTrace = null;
                }
                return datos;
            }
        }*/

        public override List<ReferenciaConciliadaPedido> ConciliarPedidoCantidadYReferenciaMovExternoEdificios(
          decimal centavos, short statusconcepto, string campoexterno, string campopedido)
        {
            List<ReferenciaConciliadaPedido> datos = new List<ReferenciaConciliadaPedido>();
            using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBConciliarPedidosPorReferenciaPorMovExternoEdificios", cnn);

                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = this.Corporativo;
                    comando.Parameters.Add("@SucursalConciliacion", System.Data.SqlDbType.Int).Value = this.SucursalConciliacion;
                    comando.Parameters.Add("@AñoConciliacion", System.Data.SqlDbType.Int).Value = this.AñoConciliacion;
                    comando.Parameters.Add("@MesConciliacion", System.Data.SqlDbType.SmallInt).Value = this.MesConciliacion;
                    comando.Parameters.Add("@FolioConciliacion", System.Data.SqlDbType.Int).Value = this.FolioConciliacion;

                    comando.Parameters.Add("@SucursalExterno", System.Data.SqlDbType.Int).Value = this.Sucursal;
                    comando.Parameters.Add("@AñoExterno", System.Data.SqlDbType.Int).Value = this.Año;
                    comando.Parameters.Add("@FolioExterno", System.Data.SqlDbType.VarChar).Value = this.Folio;
                    comando.Parameters.Add("@SecuenciaExterno", System.Data.SqlDbType.VarChar).Value = this.Secuencia;

                    comando.Parameters.Add("@Centavos", System.Data.SqlDbType.VarChar).Value = centavos;
                    comando.Parameters.Add("@StatusConcepto", System.Data.SqlDbType.VarChar).Value = statusconcepto;
                    comando.Parameters.Add("@Cadena", System.Data.SqlDbType.VarChar).Value = App.Consultas.ObtenerCadenaDeEtiquetas(0, this.Corporativo, this.SucursalConciliacion, this.AñoConciliacion, this.MesConciliacion, this.FolioConciliacion, statusconcepto);
                    comando.Parameters.Add("@CampoExterno", System.Data.SqlDbType.VarChar).Value = campoexterno;
                    comando.Parameters.Add("@CampoPedido", System.Data.SqlDbType.VarChar).Value = campopedido;

                    comando.CommandTimeout = 900;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {

                        ReferenciaConciliadaPedido dato =
                           new ReferenciaConciliadaPedidoDatos(this.Corporativo,
                                                               this.AñoConciliacion,
                                                               this.MesConciliacion,
                                                               this.FolioConciliacion,
                                                               this.Sucursal,
                                                               this.SucursalDes,
                                                               this.Folio,
                                                               this.Secuencia,
                                                               this.Concepto,
                                                               Convert.ToDecimal(reader["MontoConciliado"]),
                                                               0,
                                                               2,
                                                               this.StatusConcepto,
                                                               "EN PROCESO DE CONCILIACION",
                                                               this.FOperacion,
                                                               this.FMovimiento,

                                                               this.Cheque,
                                                               this.Referencia,
                                                               this.Descripcion,
                                                               this.NombreTercero,
                                                               this.RFCTercero,
                                                               this.Deposito,
                                                               this.Retiro,

                                                               Convert.ToInt16(reader["SucursalPedido"]),
                                                               Convert.ToString(reader["SucursalPedidoDes"]),
                                                               Convert.ToInt32(reader["CelulaPedido"]),
                                                               Convert.ToInt32(reader["AñoPedido"]),
                                                               Convert.ToInt32(reader["Pedido"]),
                                                               Convert.ToInt32(reader["RemisionPedido"]),
                                                               Convert.ToString(reader["SeriePedido"]),
                                                               Convert.ToInt32(reader["FolioSat"]),
                                                               Convert.ToString(reader["SerieSat"]),
                                                               Convert.ToString(reader["ConceptoPedido"]),
                                                               Convert.ToDecimal(reader["Total"]),
                                                               "PENDIENTE",
                                                               Convert.ToInt32(reader["Cliente"]),
                                                               Convert.ToString(reader["Nombre"]),
                                                               Convert.ToString(reader["PedidoReferencia"]),
                                                               this.AñoConciliacion,
                                                               this.implementadorMensajes);
                        datos.Add(dato);
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return datos;
            }
        }

        public override List<ReferenciaConciliadaPedido> ConciliarPedidoCantidadYReferenciaMovExterno(
         decimal centavos, short statusconcepto, string campoexterno, string campopedido)
        {
            List<ReferenciaConciliadaPedido> datos = new List<ReferenciaConciliadaPedido>();
            using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBConciliarPedidosPorReferenciaPorMovExterno", cnn);

                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = this.Corporativo;
                    comando.Parameters.Add("@SucursalConciliacion", System.Data.SqlDbType.Int).Value = this.SucursalConciliacion;
                    comando.Parameters.Add("@AñoConciliacion", System.Data.SqlDbType.Int).Value = this.AñoConciliacion;
                    comando.Parameters.Add("@MesConciliacion", System.Data.SqlDbType.SmallInt).Value = this.MesConciliacion;
                    comando.Parameters.Add("@FolioConciliacion", System.Data.SqlDbType.Int).Value = this.FolioConciliacion;

                    comando.Parameters.Add("@SucursalExterno", System.Data.SqlDbType.Int).Value = this.Sucursal;
                    comando.Parameters.Add("@AñoExterno", System.Data.SqlDbType.Int).Value = this.Año;
                    comando.Parameters.Add("@FolioExterno", System.Data.SqlDbType.VarChar).Value = this.Folio;
                    comando.Parameters.Add("@SecuenciaExterno", System.Data.SqlDbType.VarChar).Value = this.Secuencia;

                    comando.Parameters.Add("@Centavos", System.Data.SqlDbType.VarChar).Value = centavos;
                    comando.Parameters.Add("@StatusConcepto", System.Data.SqlDbType.VarChar).Value = statusconcepto;
                    comando.Parameters.Add("@Cadena", System.Data.SqlDbType.VarChar).Value = App.Consultas.ObtenerCadenaDeEtiquetas(0, this.Corporativo, this.SucursalConciliacion, this.AñoConciliacion, this.MesConciliacion, this.FolioConciliacion, statusconcepto);
                    comando.Parameters.Add("@CampoExterno", System.Data.SqlDbType.VarChar).Value = campoexterno;
                    comando.Parameters.Add("@CampoPedido", System.Data.SqlDbType.VarChar).Value = campopedido;

                    comando.CommandTimeout = 900;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {

                        ReferenciaConciliadaPedido dato =
                           new ReferenciaConciliadaPedidoDatos(this.Corporativo,
                                                               this.AñoConciliacion,
                                                               this.MesConciliacion,
                                                               this.FolioConciliacion,
                                                               this.Sucursal,
                                                               this.SucursalDes,
                                                               this.Folio,
                                                               this.Secuencia,
                                                               this.Concepto,
                                                               Convert.ToDecimal(reader["MontoConciliado"]),
                                                               0,
                                                               7,
                                                               this.StatusConcepto,
                                                               "EN PROCESO DE CONCILIACION",
                                                               this.FOperacion,
                                                               this.FMovimiento,

                                                               this.Cheque,
                                                               this.Referencia,
                                                               this.Descripcion,
                                                               this.NombreTercero,
                                                               this.RFCTercero,
                                                               this.Deposito,
                                                               this.Retiro,

                                                               Convert.ToInt16(reader["SucursalPedido"]),
                                                               Convert.ToString(reader["SucursalPedidoDes"]),
                                                               Convert.ToInt32(reader["CelulaPedido"]),
                                                               Convert.ToInt32(reader["AñoPedido"]),
                                                               Convert.ToInt32(reader["Pedido"]),
                                                               Convert.ToInt32(reader["RemisionPedido"]),
                                                               Convert.ToString(reader["SeriePedido"]),
                                                               Convert.ToInt32(reader["FolioSat"]),
                                                               Convert.ToString(reader["SerieSat"]),
                                                               Convert.ToString(reader["ConceptoPedido"]),
                                                               Convert.ToDecimal(reader["Total"]),
                                                               "PENDIENTE",
                                                               Convert.ToInt32(reader["Cliente"]),
                                                               Convert.ToString(reader["Nombre"]),
                                                               Convert.ToString(reader["PedidoReferencia"]),
                                                               this.AñoConciliacion,
                                                               this.implementadorMensajes);
                        datos.Add(dato);
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return datos;
            }
        }
    }
}
