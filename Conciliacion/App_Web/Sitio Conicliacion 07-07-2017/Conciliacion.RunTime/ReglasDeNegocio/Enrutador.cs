﻿using System;
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

    public class SolicitdConciliacion
    {
        public short TipoConciliacion { get; set; }
        public short FormaConciliacion { get; set; }
        private List<ConfiguracionTipoFormaConciliacion> ListaConfiguraciones = new List<ConfiguracionTipoFormaConciliacion>();

        public SolicitdConciliacion()
        {
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 1, FuenteInsumo = "Pedido", CargaFuenteInsumo = true });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 1, FuenteInsumo = "Archivo", CargaFuenteInsumo = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 2, FuenteInsumo = "Pedido", CargaFuenteInsumo = true });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 2, FuenteInsumo = "Archivo", CargaFuenteInsumo = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 3, FuenteInsumo = "Pedido", CargaFuenteInsumo = true });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 3, FuenteInsumo = "Archivo", CargaFuenteInsumo = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 4, FuenteInsumo = "Pedido", CargaFuenteInsumo = true });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 4, FuenteInsumo = "Archivo", CargaFuenteInsumo = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 5, FuenteInsumo = "Pedido", CargaFuenteInsumo = true });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 5, FuenteInsumo = "Archivo", CargaFuenteInsumo = false });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 6, FuenteInsumo = "Pedido", CargaFuenteInsumo = true });
            ListaConfiguraciones.Add(new ConfiguracionTipoFormaConciliacion { TipoConciliacion = 1, FormaConciliacion = 6, FuenteInsumo = "Archivo", CargaFuenteInsumo = false });
        }

        public bool ConsultaPedido()
        {
            bool Retorno = false;

            Retorno = ListaConfiguraciones.Where(x => x.TipoConciliacion == this.TipoConciliacion
                                                                 && x.FormaConciliacion == this.FormaConciliacion
                                                                 && x.FuenteInsumo == "Pedido")
                .Single()
                .CargaFuenteInsumo;

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

    }

    public struct ConfiguracionTipoFormaConciliacion
    {
        public short TipoConciliacion;
        public short FormaConciliacion;
        public string FuenteInsumo;
        public bool CargaFuenteInsumo;
    }

}
