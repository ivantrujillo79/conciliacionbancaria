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
}
