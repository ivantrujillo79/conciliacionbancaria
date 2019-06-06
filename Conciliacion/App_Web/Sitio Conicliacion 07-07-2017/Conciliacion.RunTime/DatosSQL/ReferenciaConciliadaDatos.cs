using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using Conciliacion.RunTime.ReglasDeNegocio;

namespace Conciliacion.RunTime.DatosSQL
{
    public class ReferenciaConciliadaDatos: ReferenciaConciliada
    {
        public ReferenciaConciliadaDatos(int corporativo, int añoconciliacion, short mesconciliacion, int folioconciliacion,
                                    int sucursalext, string sucursalextdes, int folioext, int secuenciaext, string conceptoext, decimal montoconciliado, decimal diferencia, short formaconciliacion, short statusconcepto, string statusconciliacion, DateTime foperacionext, DateTime fmovimientoext,
                                    string chequeexterno, string referenciaexterno, string descripcionexterno, string nombreterceroexterno, string rfcterceroexterno, decimal depositoexterno, decimal retiroexterno,
                                    int sucursalinterno, string sucursalintdes, int foliointerno, int secuenciainterno, string conceptointerno, decimal montointerno, DateTime foperacionint, DateTime fmovimientoint,
                                    string chequeinterno, string referenciainterno, string descripcioninterno, string nombretercerointerno, string rfctercerointerno, decimal depositointerno, decimal retirointerno,
                                    int añoexterno, int añointerno, 
                                    IMensajesImplementacion implementadorMensajes)
            : base(corporativo,añoconciliacion, mesconciliacion,folioconciliacion,sucursalext,sucursalextdes,folioext,secuenciaext,conceptoext,montoconciliado,diferencia,formaconciliacion,statusconcepto,statusconciliacion,foperacionext,fmovimientoext,
            chequeexterno,referenciaexterno, descripcionexterno,nombreterceroexterno,rfcterceroexterno,depositoexterno,retiroexterno,
            sucursalinterno,sucursalintdes,foliointerno,secuenciainterno,conceptointerno,montointerno,foperacionint,fmovimientoint,
            chequeinterno,referenciainterno,descripcioninterno,nombretercerointerno,rfctercerointerno,depositointerno,retirointerno,
            añoexterno,añointerno,implementadorMensajes)
        {

        }

        public ReferenciaConciliadaDatos(int corporativo, int añoconciliacion, short mesconciliacion, int folioconciliacion,
                                    int sucursalext, string sucursalextdes, int folioext, int secuenciaext, string conceptoext, decimal montoconciliado, decimal diferencia, short formaconciliacion, short statusconcepto, string statusconciliacion, DateTime foperacionext, DateTime fmovimientoext,
                                    string chequeexterno, string referenciaexterno, string descripcionexterno, string nombreterceroexterno, string rfcterceroexterno, decimal depositoexterno, decimal retiroexterno,
                                    int sucursalinterno, string sucursalintdes, int foliointerno, int secuenciainterno, string conceptointerno, decimal montointerno, DateTime foperacionint, DateTime fmovimientoint,
                                    string chequeinterno, string referenciainterno, string descripcioninterno, string nombretercerointerno, string rfctercerointerno, decimal depositointerno, decimal retirointerno,
                                    int añoexterno, int añointerno, string SerieFactura, string ClienteReferencia,
                                    IMensajesImplementacion implementadorMensajes)
            : base(corporativo, añoconciliacion, mesconciliacion, folioconciliacion, sucursalext, sucursalextdes, folioext, secuenciaext, conceptoext, montoconciliado, diferencia, formaconciliacion, statusconcepto, statusconciliacion, foperacionext, fmovimientoext,
            chequeexterno, referenciaexterno, descripcionexterno, nombreterceroexterno, rfcterceroexterno, depositoexterno, retiroexterno,
            sucursalinterno, sucursalintdes, foliointerno, secuenciainterno, conceptointerno, montointerno, foperacionint, fmovimientoint,
            chequeinterno, referenciainterno, descripcioninterno, nombretercerointerno, rfctercerointerno, depositointerno, retirointerno,
            añoexterno, añointerno,SerieFactura, ClienteReferencia, implementadorMensajes)
        {

        }

        public ReferenciaConciliadaDatos(int corporativo, int añoconciliacion, short mesconciliacion, int folioconciliacion,
                            int sucursalext, string sucursalextdes, int folioext, int secuenciaext, string conceptoext, decimal montoconciliado, decimal diferencia, short formaconciliacion, short statusconcepto, string statusconciliacion, DateTime foperacionext, DateTime fmovimientoext,
                            string chequeexterno, string referenciaexterno, string descripcionexterno, string nombreterceroexterno, string rfcterceroexterno, decimal depositoexterno, decimal retiroexterno,
                            int sucursalinterno, string sucursalintdes, int foliointerno, int secuenciainterno, string conceptointerno, decimal montointerno, DateTime foperacionint, DateTime fmovimientoint,
                            string chequeinterno, string referenciainterno, string descripcioninterno, string nombretercerointerno, string rfctercerointerno, decimal depositointerno, decimal retirointerno,
                            int añoexterno, int añointerno, int tipocobro, int tipocobroAnterior,
                            IMensajesImplementacion implementadorMensajes)
            : base(corporativo, añoconciliacion, mesconciliacion, folioconciliacion, sucursalext, sucursalextdes, folioext, secuenciaext, conceptoext, montoconciliado, diferencia, formaconciliacion, statusconcepto, statusconciliacion, foperacionext, fmovimientoext,
            chequeexterno, referenciaexterno, descripcionexterno, nombreterceroexterno, rfcterceroexterno, depositoexterno, retiroexterno,
            sucursalinterno, sucursalintdes, foliointerno, secuenciainterno, conceptointerno, montointerno, foperacionint, fmovimientoint,
            chequeinterno, referenciainterno, descripcioninterno, nombretercerointerno, rfctercerointerno, depositointerno, retirointerno,
            añoexterno, añointerno, tipocobro, tipocobroAnterior, implementadorMensajes)
        {

        }

        public ReferenciaConciliadaDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
            
        }

        public override ReferenciaConciliada CrearObjeto()
        {
            return new ReferenciaConciliadaDatos(App.ImplementadorMensajes);
        }

        public override bool Guardar()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaConciliacionReferencia", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.SmallInt).Value = 0;
                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = this.Corporativo;
                    comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = this.Sucursal;
                    comando.Parameters.Add("@AñoConciliacion", System.Data.SqlDbType.Int).Value = this.AñoConciliacion;
                    comando.Parameters.Add("@MesConciliacion", System.Data.SqlDbType.SmallInt).Value = this.MesConciliacion;
                    comando.Parameters.Add("@FolioConciliacion ", System.Data.SqlDbType.Int).Value = this.FolioConciliacion;
                    comando.Parameters.Add("@SecuenciaRelacion", System.Data.SqlDbType.Int).Value = 0;

                    comando.Parameters.Add("@Concepto", System.Data.SqlDbType.VarChar).Value = this.Concepto;
                    comando.Parameters.Add("@MontoConciliado", System.Data.SqlDbType.Money).Value = this.MontoConciliado;
                    comando.Parameters.Add("@Diferencia ", System.Data.SqlDbType.Money).Value = this.Diferencia;
                    comando.Parameters.Add("@FormaConciliacion", System.Data.SqlDbType.SmallInt).Value = this.FormaConciliacion;
                    comando.Parameters.Add("@StatusConcepto", System.Data.SqlDbType.SmallInt).Value = 0;//this.StatusConcepto; -->StatusConcepto: NINGUNO X DEFAULT
                    comando.Parameters.Add("@StatusConciliacion", System.Data.SqlDbType.VarChar).Value = "CONCILIADA";
                    comando.Parameters.Add("@MotivoNoConciliado", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@ComentarioNoConciliado", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar).Value = "";

                    comando.Parameters.Add("@SucursalInterno", System.Data.SqlDbType.Int).Value = this.SucursalInterno;
                    comando.Parameters.Add("@AñoInterno", System.Data.SqlDbType.Int).Value = this.AñoInterno;
                    comando.Parameters.Add("@FolioInterno", System.Data.SqlDbType.Int).Value = this.FolioInterno;
                    comando.Parameters.Add("@SecuenciaInterno", System.Data.SqlDbType.Int).Value = this.SecuenciaInterno;
                    comando.Parameters.Add("@MontoInterno", System.Data.SqlDbType.Money).Value = this.MontoInterno;

                    comando.Parameters.Add("@AñoExterno", System.Data.SqlDbType.Int).Value = this.Año;
                    comando.Parameters.Add("@FolioExterno", System.Data.SqlDbType.Int).Value = this.Folio;
                    comando.Parameters.Add("@SecuenciaExterno", System.Data.SqlDbType.Int).Value = this.Secuencia;
                    comando.Parameters.Add("@MontoExterno ", System.Data.SqlDbType.Money).Value = this.MontoConciliado;

                    //if (this.ClientePago == 0)
                    //    this.ClientePago = this.;
                    comando.Parameters.Add("@ClientePago", System.Data.SqlDbType.Int).Value = this.ClientePago;
                    comando.Parameters.Add("@UsuarioAlta", System.Data.SqlDbType.VarChar).Value = this.Usuario == "" ? null : this.Usuario;

                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();                   
                }
                resultado = true; 
            }
            catch (SqlException ex)
            {               
                stackTrace = new StackTrace();
                this.ImplementadorMensajes.MostrarMensaje("No se pudo guardar el registro.\n\rClase :" + this.GetType().Name + "\n\r" + "Metodo :" + stackTrace.GetFrame(0).GetMethod().Name + "\n\r" + "Error :" + ex.Message);
                stackTrace = null;
            }
            
            return resultado;
        }

        public override bool Modificar()
        {
            throw new NotImplementedException();
        }

        public override bool Eliminar()
        {
            throw new NotImplementedException();
        }
    }
}
