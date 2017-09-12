using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public abstract class cConciliacion : EmisorMensajes, IConciliacion
    {
        int corporativo;
        string corporativodes;
        int sucursal;
        string sucursaldes;
        int año;
        short mes;
        int folio;
        DateTime finicial;
        DateTime ffinal;
        String statusconciliacion;
        int grupoconciliacion;
        short tipoconciliacion;
        DatosArchivo archivoexterno;
        int transaccionesinternas;
        int conciliadasinternas;
        int transaccionesexternas;
        int conciliadasexternas;
        decimal montoTotalExterno;
        decimal montoTotalInterno;
        string grupoconciliacionstr;
        bool accesototal;
        string cuentabancaria;

        string tipoconciliacionstr;
        string bancostr;
        string ubicacionicono;

        int banco;
        int tipofuenteinformacioninterno;
        int tipofuenteinformacionexterno;

        public enum Operacion
        {
            Nueva,
            Edicion
        }

        private List<DatosArchivo> listaarchivos = new List<DatosArchivo>();
        private List<ReferenciaConciliada> listareferenciaconciliada = new List<ReferenciaConciliada>();
        private List<cReferencia> listaReferenciaexterna = new List<cReferencia>();

        #region Constructores

        public cConciliacion(IMensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.corporativo = 0;
            this.sucursal = 0;
            this.año = 0;
            this.mes = 0;
            this.folio = 0;

        }

        //public cConciliacion(int corporativo, int sucursal, int año, short mes, int folio, IMensajesImplementacion implementadorMensajes)
        //{
        //    this.ImplementadorMensajes = implementadorMensajes;
        //    this.corporativo = corporativo;
        //    this.sucursal = sucursal;
        //    this.año = año;
        //    this.mes = mes;
        //    this.folio = folio;
        //    //this.listaReferenciaexterna = App.Consultas.ObtieneReferenciasPorConciliacion(this.corporativo, this.sucursal, this.año, this.mes, this.folio);
        //}

        public cConciliacion(int corporativo, int sucursal, int año, short mes, int folio, DateTime finicial, DateTime ffinal, string statusconciliacion,
            int grupoconciliacion, short tipoconciliacion, int transaccionesinternas, int conciliadasinternas, int transaccionesexternas, int conciliadasexternas, decimal montoTotalExterno, decimal montoTotalInterno,
            string grupoconciliacionstr, bool accesototal, string cuentabancaria, string corporativodes, string sucursaldes, IMensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.corporativo = corporativo;
            this.sucursal = sucursal;
            this.año = año;
            this.mes = mes;
            this.folio = folio;
            this.finicial = finicial;
            this.ffinal = ffinal;
            this.statusconciliacion = statusconciliacion;
            this.grupoconciliacion = grupoconciliacion;
            this.tipoconciliacion = tipoconciliacion;
            this.transaccionesinternas = transaccionesinternas;
            this.conciliadasinternas = conciliadasinternas;
            this.transaccionesexternas = transaccionesexternas;
            this.conciliadasexternas = conciliadasexternas;
            this.montoTotalExterno = montoTotalExterno;
            this.montoTotalInterno = montoTotalInterno;
            this.grupoconciliacionstr = grupoconciliacionstr;
            this.accesototal = accesototal;
            this.cuentabancaria = cuentabancaria;
            this.corporativodes = corporativodes;
            this.sucursaldes = sucursaldes;
        }

        public cConciliacion(int corporativo, int sucursal, int año, short mes, int folio, DateTime finicial, DateTime ffinal, string statusconciliacion,
           int grupoconciliacion, short tipoconciliacion, int transaccionesinternas, int conciliadasinternas, int transaccionesexternas, int conciliadasexternas, decimal montoTotalExterno, decimal montoTotalInterno,
           string grupoconciliacionstr, bool accesototal, string cuentabancaria, string corporativodes,string sucursaldes, string bancostr, string ubicacionicono, IMensajesImplementacion implementadorMensajes)
        {
            this.ImplementadorMensajes = implementadorMensajes;
            this.corporativo = corporativo;
            this.sucursal = sucursal;
            this.año = año;
            this.mes = mes;
            this.folio = folio;
            this.finicial = finicial;
            this.ffinal = ffinal;
            this.statusconciliacion = statusconciliacion;
            this.grupoconciliacion = grupoconciliacion;
            this.tipoconciliacion = tipoconciliacion;
            this.transaccionesinternas = transaccionesinternas;
            this.conciliadasinternas = conciliadasinternas;
            this.transaccionesexternas = transaccionesexternas;
            this.conciliadasexternas = conciliadasexternas;
            this.montoTotalExterno = montoTotalExterno;
            this.montoTotalInterno = montoTotalInterno;
            this.grupoconciliacionstr = grupoconciliacionstr;
            this.accesototal = accesototal;
            this.cuentabancaria = cuentabancaria;
            this.corporativodes = corporativodes;
            this.sucursaldes = sucursaldes;
            this.bancostr = bancostr;
            this.ubicacionicono = ubicacionicono;
        }

        //Convert.ToInt32(reader["CorporativoConciliacion"]), Convert.ToInt32(reader["SucursalConciliacion"]),
        //                                                               Convert.ToString(reader["SucursalDes"]), Convert.ToInt32(reader["AñoConciliacion"]), 
        //                                                               Convert.ToInt16(reader["MesConciliacion"]), Convert.ToInt32(reader["FolioConciliacion"]),
        //                                                               Convert.ToString(reader["GrupoConciliacionstr"]), Convert.ToString(reader["TipoConciliacionstr"]),
        //                                                               Convert.ToString(reader["StatusConciliacion"]),
        //                                                               Convert.ToInt32(reader["TransaccionesInternas"]),Convert.ToInt32(reader["ConciliadasInternas"]),
        //                                                               Convert.ToInt32(reader["TransaccionesExternas"]),Convert.ToInt32(reader["ConciliadasExternas"]),
        //                                                               Convert.ToString(reader["CuentaBancoFinanciero"]),Convert.ToString(reader["Bancostr"]),
        //                                                               Convert.ToString(reader["UbicacionIcono"]), this.implementadorMensajes);


        #endregion


        #region Propiedades

        public int Corporativo
        {
            get { return corporativo; }
            set { corporativo = value; }
        }

        public string CorporativoDes
        {
            get { return corporativodes; }
            set { corporativodes = value; }
        }

        public int Sucursal
        {
            get { return sucursal; }
            set { sucursal = value; }
        }

        public string SucursalDes
        {
            get { return sucursaldes; }
            set { sucursaldes = value; }
        }

        public int Año
        {
            get { return año; }
            set { año = value; }
        }

        public short Mes
        {
            get { return mes; }
            set { mes = value; }
        }

        public int Folio
        {
            get { return folio; }
            set { folio = value; }
        }

        public DateTime FInicial
        {
            get { return finicial; }
            set { finicial = value; }
        }

        public DateTime FFinal
        {
            get { return ffinal; }
            set { ffinal = value; }
        }

        public String StatusConciliacion
        {
            get { return statusconciliacion; }
            set { statusconciliacion = value; }
        }

        public int GrupoConciliacion
        {
            get { return grupoconciliacion; }
            set { grupoconciliacion = value; }
        }

        public short TipoConciliacion
        {
            get { return tipoconciliacion; }
            set { tipoconciliacion = value; }
        }

        public int TransaccionesInternas
        {
            get { return transaccionesinternas; }
            set { transaccionesinternas = value; }
        }

        public int ConciliadasInternas
        {
            get { return conciliadasinternas; }
            set { conciliadasinternas = value; }
        }

        public int TransaccionesExternas
        {
            get { return transaccionesexternas; }
            set { transaccionesexternas = value; }
        }

        public int ConciliadasExternas
        {
            get { return conciliadasexternas; }
            set { conciliadasexternas = value; }
        }
        public decimal MontoTotalExterno
        {
            get { return montoTotalExterno; }
            set { montoTotalExterno = value; }
        }
        public decimal MontoTotalInterno
        {
            get { return montoTotalInterno; }
            set { montoTotalInterno = value; }
        }
        public string GrupoConciliacionStr
        {
            get { return grupoconciliacionstr; }
            set { grupoconciliacionstr = value; }
        }

        public bool AccesoTotal
        {
            get { return accesototal; }
            set { accesototal = value; }
        }

        public string CuentaBancaria
        {
            get { return cuentabancaria; }
            set { cuentabancaria = value; }
        }

        public string TipoConciliacionStr
        {
            get { return tipoconciliacionstr; }
            set { tipoconciliacionstr = value; }
        }
        public string BancoStr
        {
            get { return bancostr; }
            set { bancostr = value; }
        }
        public string UbicacionIcono
        {
            get { return ubicacionicono; }
            set { ubicacionicono = value; }
        }


        public int Banco
        {
            get { return banco; }
            set { banco = value; }
        }
        public int TipoFuenteInformacionExterno
        {
            get { return tipofuenteinformacionexterno; }
            set { tipofuenteinformacionexterno = value; }
        }
        public int TipoFuenteInformacionInterno
        {
            get { return tipofuenteinformacioninterno; }
            set { tipofuenteinformacioninterno = value; }
        }



        public DatosArchivo ArchivoExterno
        {
            get { return archivoexterno; }
            set { archivoexterno = value; }
        }

        public List<DatosArchivo> ListaArchivos
        {
            get { return listaarchivos; }
        }

        public List<ReferenciaConciliada> ListaReferenciaConciliada
        {
            get { return listareferenciaconciliada; }
        }

        public List<cReferencia> ListaReferenciaExterna
        {
            get { return listaReferenciaexterna; }
        }

        #endregion

        public abstract bool Guardar(string usuario);
        public abstract bool GuardarSinInterno(string usuario);
        public abstract bool BorrarArchivosInternos();
        public abstract bool BorrarConciliacion();
        public abstract bool CerrarConciliacion(string usuario);
        public abstract bool PuedeCerrar();
        public abstract bool PuedeCancelar();
        public abstract bool CancelarConciliacion(string usuario);

        public abstract IConciliacion CrearObjeto();
        public abstract bool AgregarReferencia(cReferencia referenciaexterna, cReferencia referenciainterna);


        #region Metodos

        public bool AgregarArchivo(DatosArchivo datosarchivo, Operacion operacion)
        {
            bool resultado = true;
            try
            {
                foreach (DatosArchivo arc in this.listaarchivos)
                {
                    if (arc.Folio == datosarchivo.Folio)
                    {
                        this.ImplementadorMensajes.MostrarMensaje("El folio " + datosarchivo.Folio + " ya fue seleccionado, verifique porfavor.");
                        return false;
                    }
                }
                if (operacion == Operacion.Edicion)
                {
                    if (!datosarchivo.ExisteArchivoInternoConciliacion())
                        datosarchivo.GuardarArchivoInterno();
                    else
                    {
                        this.ImplementadorMensajes.MostrarMensaje("El folio " + datosarchivo.Folio + " ya fue agregado a la conciliación, verifique porfavor.");
                        return false;
                    }
                }
                listaarchivos.Add(datosarchivo);
            }
            catch (Exception ex)
            {
                resultado = false;
                throw (ex);
            }

            return resultado;
        }

        public bool QuitarArchivo(DatosArchivo datosarchivo, Operacion operacion)
        {
            bool resultado = true;
            try
            {
                if (operacion == Operacion.Edicion)
                    datosarchivo.BorrarArchivoInterno();
                listaarchivos.Remove(datosarchivo);
            }
            catch (Exception ex)
            {
                resultado = false;
                throw (ex);
            }

            return resultado;
        }

        public bool AgregarArhivoExterno(DatosArchivo datosarhivo)
        {
            bool resultado = true;
            try
            {
                this.archivoexterno = datosarhivo;
            }
            catch (Exception ex)
            {
                resultado = false;
                throw (ex);
            }

            return resultado;
        }

        #endregion

    }
}
