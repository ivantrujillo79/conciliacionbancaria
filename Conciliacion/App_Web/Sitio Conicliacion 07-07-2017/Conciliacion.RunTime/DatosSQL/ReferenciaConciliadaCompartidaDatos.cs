using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using Conciliacion.RunTime.ReglasDeNegocio;

namespace Conciliacion.RunTime.DatosSQL
{
    public class ReferenciaConciliadaCompartidaDatos : ReferenciaConciliadaCompartida
    {
        public ReferenciaConciliadaCompartidaDatos(int corporativoconciliacion, int sucursalconciliacion, int añoconciliacion, short mesconciliacion, int folioconciliacion,
                                                    int secuenciarelacion,
                                                    int corporativo, int sucursalext, string sucursalextdes, int añoexterno, int folioext, int secuenciaext, string conceptoext,
                                                    decimal montoconciliado, decimal diferencia, short formaconciliacion, short statusconcepto, string statusConciliacionMovimiento,
                                                    DateTime foperacionext, DateTime fmovimientoext, string chequeexterno, string referenciaexterno, string descripcionexterno,
                                                    string nombreterceroexterno, string rfcterceroexterno, decimal depositoexterno, decimal retiroexterno,

                                                    int? corporativoint, int? sucursalint, int? añoint, int? folioint, int? secuenciaint,
                                                    int? celula, int? añoped, int? pedido, decimal? total, int cliente,
                                                    string conceptoint, string descripcionint, string motivonoconciliado, string comentarionoconciliado, string ubicacionicono, decimal montoexterno,

                                                    IMensajesImplementacion implementadorMensajes)

            : base(corporativoconciliacion, sucursalconciliacion, añoconciliacion, mesconciliacion, folioconciliacion, secuenciarelacion,

                                      corporativo, sucursalext, sucursalextdes, añoexterno, folioext, secuenciaext, conceptoext,
                                      montoconciliado, diferencia, formaconciliacion, statusconcepto,
                                      statusConciliacionMovimiento, foperacionext, fmovimientoext,
                                      chequeexterno, referenciaexterno, descripcionexterno, nombreterceroexterno,
                                      rfcterceroexterno, depositoexterno, retiroexterno,

                                      corporativoint, sucursalint, añoint, folioint, secuenciaint,
                                      celula, añoped, pedido, total, cliente,
                                      conceptoint, descripcionint, motivonoconciliado, comentarionoconciliado, ubicacionicono, montoexterno,

                                      implementadorMensajes)
        {


        }


        public ReferenciaConciliadaCompartidaDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {


        }


        public override ReferenciaConciliada CrearObjeto()
        {
            return new ReferenciaConciliadaDatos(App.ImplementadorMensajes);
        }

        public override bool ActualizarStatusConceptoDescripcionConciliacionReferencia()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaConciliacionReferencia", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.SmallInt).Value = 5;
                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = this.Corporativo;
                    comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = this.Sucursal;
                    comando.Parameters.Add("@AñoConciliacion", System.Data.SqlDbType.Int).Value = this.AñoConciliacion;
                    comando.Parameters.Add("@MesConciliacion", System.Data.SqlDbType.SmallInt).Value = this.MesConciliacion;
                    comando.Parameters.Add("@FolioConciliacion ", System.Data.SqlDbType.Int).Value = this.FolioConciliacion;
                    comando.Parameters.Add("@SecuenciaRelacion", System.Data.SqlDbType.Int).Value = this.SecuenciaRelacion;

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
                    comando.Parameters.Add("@MontoExterno ", System.Data.SqlDbType.Money).Value = this.MontoExterno;
                    comando.Parameters.Add("@MontoInterno", System.Data.SqlDbType.Money).Value = 0;
                    comando.Parameters.Add("@FormaConciliacion", System.Data.SqlDbType.SmallInt).Value = 5;
                    comando.Parameters.Add("@StatusConcepto", System.Data.SqlDbType.SmallInt).Value = this.StatusConcepto;
                    comando.Parameters.Add("@StatusConciliacion", System.Data.SqlDbType.VarChar).Value = "CONCILIADA S/REFERENCIA";
                    comando.Parameters.Add("@MotivoNoConciliado", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@ComentarioNoConciliado", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar).Value = String.IsNullOrEmpty(this.DescripcionInterno) ? null: this.DescripcionInterno;
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.ExecuteNonQuery();

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
        public override bool ActualizarStatusConceptoDescripcionConciliacionPedido()
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(App.CadenaConexion))
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("spCBActualizaConciliacionPedido", cnn);
                    comando.Parameters.Add("@Configuracion", System.Data.SqlDbType.SmallInt).Value = 3;
                    comando.Parameters.Add("@Corporativo", System.Data.SqlDbType.TinyInt).Value = this.Corporativo;
                    comando.Parameters.Add("@Sucursal", System.Data.SqlDbType.Int).Value = this.Sucursal;
                    comando.Parameters.Add("@AñoConciliacion", System.Data.SqlDbType.Int).Value = this.AñoConciliacion;
                    comando.Parameters.Add("@FolioConciliacion ", System.Data.SqlDbType.Int).Value = this.FolioConciliacion;
                    comando.Parameters.Add("@MesConciliacion", System.Data.SqlDbType.SmallInt).Value = this.MesConciliacion;
                    comando.Parameters.Add("@SecuenciaRelacion", System.Data.SqlDbType.SmallInt).Value = this.SecuenciaRelacion;

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
                    comando.Parameters.Add("@MontoExterno", System.Data.SqlDbType.Money).Value = this.MontoExterno;
                    comando.Parameters.Add("@MontoInterno", System.Data.SqlDbType.Money).Value = 0;
                    comando.Parameters.Add("@FormaConciliacion", System.Data.SqlDbType.SmallInt).Value = 5;
                    comando.Parameters.Add("@StatusConcepto", System.Data.SqlDbType.SmallInt).Value = this.StatusConcepto;
                    comando.Parameters.Add("@StatusConciliacion", System.Data.SqlDbType.VarChar).Value = "CONCILIADA S/REFERENCIA";
                    comando.Parameters.Add("@StatusMovimiento", System.Data.SqlDbType.VarChar).Value = "PENDIENTE";
                    comando.Parameters.Add("@MotivoNoConciliado", System.Data.SqlDbType.Int).Value = 0;
                    comando.Parameters.Add("@ComentarioNoConciliado", System.Data.SqlDbType.VarChar).Value = "";
                    comando.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar).Value = String.IsNullOrEmpty(this.DescripcionInterno) ? null : this.DescripcionInterno; 

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
    }
}
