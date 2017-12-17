using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public class Enrutador
    {
        public Enrutador()
        {

        }

        public List<ListaCombo> CargarFormaConciliacion(short tipoconciliacion)
        {
            List<ListaCombo> ListaRespuesta = new List<ListaCombo>();
            try
            {
                ListaRespuesta = Conciliacion.RunTime.App.Consultas.CargarFormaConciliacion(tipoconciliacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListaRespuesta;
        }

        public string ObtieneURLSolicitud(SolicitudEnrutador Solicitud)
        {
            string UrlDestino = "";
            try
            {
                UrlDestino = Conciliacion.RunTime.App.Consultas.ObtieneURLSolicitud(Solicitud.TipoConciliacion,
                                                                                Solicitud.FormaConciliacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return UrlDestino;
        }

        public string ObtieneURLSolicitudPorDefecto(SolicitudEnrutador Solicitud)
        {
            string UrlDestino = "";
            try
            {
                UrlDestino = Conciliacion.RunTime.App.Consultas.ObtieneURLSolicitud(Solicitud.TipoConciliacion,
                                                                                Solicitud.FormaConciliacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return UrlDestino;
        }


   } //Fin clase Enrutador

    public class SolicitudEnrutador
    {
        public short TipoConciliacion { get; set; }
        public short FormaConciliacion { get; set; }
        public SolicitudEnrutador(short TipoConciliacion, short FormaConciliacion)
        {
            this.TipoConciliacion = TipoConciliacion;
            this.FormaConciliacion = FormaConciliacion;
        }
    } //Fin clase SolicitudEnrutador

    public class SolicitudConciliacion
    {
        public short TipoConciliacion { get; set; }
        public short FormaConciliacion { get; set; }
        private List<ConfiguracionTipoFormaConciliacion> ListaConfiguraciones = new List<ConfiguracionTipoFormaConciliacion>();

        public SolicitudConciliacion()
        {
            /*Tipo de conciliación 1 => CONCILIACION DE INGRESOS POR VENTA Y COBRANZA*/
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 1, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false});
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 1, FuenteInsumo = "Archivo", CargaFuenteInsumo = true, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 2, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 2, FuenteInsumo = "Archivo", CargaFuenteInsumo = true, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 3, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 3, FuenteInsumo = "Archivo", CargaFuenteInsumo = true, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 4, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 4, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 5, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 5, FuenteInsumo = "Archivo", CargaFuenteInsumo = true, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 6, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 6, FuenteInsumo = "Archivo", CargaFuenteInsumo = true, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 7, FuenteInsumo = "Pedido", CargaFuenteInsumo = true, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 7, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 8, FuenteInsumo = "Pedido", CargaFuenteInsumo = true, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 8, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 9, FuenteInsumo = "Pedido", CargaFuenteInsumo = true, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 9, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            /*Tipo de conciliación 2 => CONCILIACION DE INGRESOS POR COBRANZA A ABONAR EDIFICIOS*/
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 2, FormaConciliacion = 1, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 2, FormaConciliacion = 1, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 2, FormaConciliacion = 2, FuenteInsumo = "Pedido", CargaFuenteInsumo = true, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 2, FormaConciliacion = 2, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 2, FormaConciliacion = 3, FuenteInsumo = "Pedido", CargaFuenteInsumo = true, BuscadoresHabilitados = false, CelulaPorDefecto = 8, TextoInternos = "Pedidos de edificios administrados", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 2, FormaConciliacion = 3, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 8, TextoInternos = "Pedidos de edificios administrados", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 2, FormaConciliacion = 4, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 2, FormaConciliacion = 4, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 2, FormaConciliacion = 5, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 2, FormaConciliacion = 5, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 2, FormaConciliacion = 5, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 2, FormaConciliacion = 5, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 2, FormaConciliacion = 5, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 2, FormaConciliacion = 5, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 2, FormaConciliacion = 5, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 2, FormaConciliacion = 5, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 2, FormaConciliacion = 5, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 2, FormaConciliacion = 6, FuenteInsumo = "Pedido", CargaFuenteInsumo = true, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 2, FormaConciliacion = 6, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 2, FormaConciliacion = 7, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 2, FormaConciliacion = 7, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 2, FormaConciliacion = 8, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 2, FormaConciliacion = 8, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 2, FormaConciliacion = 9, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 2, FormaConciliacion = 9, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });

            /*Tipo de conciliación 3 => CONCILIACION DE EGRESOS*/
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 3, FormaConciliacion = 1, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 3, FormaConciliacion = 1, FuenteInsumo = "Archivo", CargaFuenteInsumo = true, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 3, FormaConciliacion = 2, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 3, FormaConciliacion = 2, FuenteInsumo = "Archivo", CargaFuenteInsumo = true, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 3, FormaConciliacion = 3, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 3, FormaConciliacion = 3, FuenteInsumo = "Archivo", CargaFuenteInsumo = true, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 3, FormaConciliacion = 4, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 3, FormaConciliacion = 4, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 3, FormaConciliacion = 5, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 3, FormaConciliacion = 5, FuenteInsumo = "Archivo", CargaFuenteInsumo = true, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 3, FormaConciliacion = 6, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 3, FormaConciliacion = 6, FuenteInsumo = "Archivo", CargaFuenteInsumo = true, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 3, FormaConciliacion = 7, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 3, FormaConciliacion = 7, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 3, FormaConciliacion = 8, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 3, FormaConciliacion = 8, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 3, FormaConciliacion = 9, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 3, FormaConciliacion = 9, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });

            /*Tipo de conciliación 4 => CONCILIACION DE INGRESOS Y EGRESOS*/
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 4, FormaConciliacion = 1, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 4, FormaConciliacion = 1, FuenteInsumo = "Archivo", CargaFuenteInsumo = true, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 4, FormaConciliacion = 2, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 4, FormaConciliacion = 2, FuenteInsumo = "Archivo", CargaFuenteInsumo = true, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 4, FormaConciliacion = 3, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 4, FormaConciliacion = 3, FuenteInsumo = "Archivo", CargaFuenteInsumo = true, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 4, FormaConciliacion = 4, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 4, FormaConciliacion = 4, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 4, FormaConciliacion = 5, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 4, FormaConciliacion = 5, FuenteInsumo = "Archivo", CargaFuenteInsumo = true, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 4, FormaConciliacion = 6, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 4, FormaConciliacion = 6, FuenteInsumo = "Archivo", CargaFuenteInsumo = true, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 4, FormaConciliacion = 7, FuenteInsumo = "Pedido", CargaFuenteInsumo = true, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 4, FormaConciliacion = 7, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 4, FormaConciliacion = 8, FuenteInsumo = "Pedido", CargaFuenteInsumo = true, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 4, FormaConciliacion = 8, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 4, FormaConciliacion = 9, FuenteInsumo = "Pedido", CargaFuenteInsumo = true, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 4, FormaConciliacion = 9, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            
            
            /*Tipo de conciliación 6 => CONCILIACION DE INGRESOS POR COBRANZA A ABONAR PEDIDO*/
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 6, FormaConciliacion = 1, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 6, FormaConciliacion = 1, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 6, FormaConciliacion = 2, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 6, FormaConciliacion = 2, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 6, FormaConciliacion = 3, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 6, FormaConciliacion = 3, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 6, FormaConciliacion = 4, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 6, FormaConciliacion = 4, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 6, FormaConciliacion = 5, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 6, FormaConciliacion = 5, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 6, FormaConciliacion = 6, FuenteInsumo = "Pedido", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 6, FormaConciliacion = 6, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 6, FormaConciliacion = 7, FuenteInsumo = "Pedido", CargaFuenteInsumo = true, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 6, FormaConciliacion = 7, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 6, FormaConciliacion = 8, FuenteInsumo = "Pedido", CargaFuenteInsumo = true, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 6, FormaConciliacion = 8, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 6, FormaConciliacion = 9, FuenteInsumo = "Pedido", CargaFuenteInsumo = true, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 6, FormaConciliacion = 9, FuenteInsumo = "Archivo", CargaFuenteInsumo = false, BuscadoresHabilitados = false, CelulaPorDefecto = 0, TextoInternos = "", ActivaPagare = false });
        }

        public bool ConsultaActivaPagare()
        {
            bool Retorno = false;
            try
            {
                Retorno = ListaConfiguraciones.Where(x => x.TipoConciliacion == this.TipoConciliacion && x.FormaConciliacion == this.FormaConciliacion).Take(1).Single().ActivaPagare;
            }
            catch (InvalidOperationException IOE)
            {
                throw new Exception("No existe configuración para el tipo, forma e insumo Tipo: " + this.TipoConciliacion.ToString() + " Forma: " + this.FormaConciliacion.ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
            return Retorno;
        }

        public string ConsultaTextoInternos()
        {
            string Retorno = "";
            try
            {
                Retorno = ListaConfiguraciones.Where(x => x.TipoConciliacion == this.TipoConciliacion && x.FormaConciliacion == this.FormaConciliacion).Take(1).Single().TextoInternos;
            }
            catch (InvalidOperationException IOE)
            {
                throw new Exception("No existe configuración para el tipo, forma e insumo Tipo: " + this.TipoConciliacion.ToString() + " Forma: " + this.FormaConciliacion.ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
            return Retorno;
        }

        public int ConsultaCelulaPordefecto()
        {
            int Retorno = 0;
            try
            {
                Retorno = ListaConfiguraciones.Where(x => x.TipoConciliacion == this.TipoConciliacion
                                                                 && x.FormaConciliacion == this.FormaConciliacion
                                                                 && x.FuenteInsumo == "Pedido")
                .Single()
                .CelulaPorDefecto;
            }
            catch (InvalidOperationException IOE)
            {
                throw new Exception("No existe configuración para el tipo, forma e insumo Tipo: " + this.TipoConciliacion.ToString() + " Forma: " + this.FormaConciliacion.ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
            return Retorno;
        }


        public bool ConsultaPedido()
        {
            bool Retorno = false;
            try
            {
                Retorno = ListaConfiguraciones.Where(x => x.TipoConciliacion == this.TipoConciliacion
                                                                 && x.FormaConciliacion == this.FormaConciliacion
                                                                 && x.FuenteInsumo == "Pedido")
                .Single()
                .CargaFuenteInsumo;
            }
            catch (InvalidOperationException IOE)
            {
                throw new Exception("No existe configuración para el tipo, forma e insumo Tipo: " + this.TipoConciliacion.ToString() + " Forma: " + this.FormaConciliacion.ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
            return Retorno;
        }

        public bool ConsultaArchivo()
        {
            bool Retorno = false;
            Retorno = ListaConfiguraciones.Where(x => x.TipoConciliacion == this.TipoConciliacion 
                                                      && x.FormaConciliacion == this.FormaConciliacion 
                                                      && x.FuenteInsumo == "Archivo")
               .Single()
               .CargaFuenteInsumo;
            return Retorno;
        }

        public bool MuestraBuscadores()
        {
            bool Retorno = false;
            Retorno = ListaConfiguraciones.Where(x => x.TipoConciliacion == this.TipoConciliacion
                                                      && x.FormaConciliacion == this.FormaConciliacion
                                                      && x.FuenteInsumo == "Pedido")
               .Single()
               .BuscadoresHabilitados;
            return Retorno;
        }


    }

    public struct ConfiguracionTipoFormaConciliacion
    {
        public short TipoConciliacion;
        public short FormaConciliacion;
        public string FuenteInsumo;
        public bool CargaFuenteInsumo;
        public bool BuscadoresHabilitados;
        public int CelulaPorDefecto;
        public string TextoInternos;
        public bool ActivaPagare;
    }

}
