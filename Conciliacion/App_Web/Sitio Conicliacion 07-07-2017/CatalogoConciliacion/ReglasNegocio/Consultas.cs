using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.Migracion.Runtime.ReglasNegocio;
using Conciliacion.RunTime;
using Conciliacion.RunTime.ReglasDeNegocio; 


namespace CatalogoConciliacion.ReglasNegocio
{
    public abstract class Consultas : EmisorMensajes
    {
        //Listado de Motivos y Detalle de Motivo
        public abstract List<MotivoNoConciliado> ObtieneMotivos(int configuracion, int idMotivoNoConciliado);
        public abstract MotivoNoConciliado ObtieneMotivoPorId(int configuracion, int idMotivoNoConciliado);

        //Listado de Grupos y Detalle de Grupo
        public abstract List<GrupoConciliacion> ObtieneGrupos(int configuracion, int idGrupoConciliacion);
        public abstract GrupoConciliacion ObtieneGrupoPorId(int configuracion, int idGrupoConciliacion);
        
        //Listado de Usuarios por grupo
        public abstract List<GrupoConciliacionUsuario> ObtieneUsuariosPorGrupo(int configuracion, int idGrupoConciliacion);
        
        // Combo
        public abstract List<ListaCombo> ObtieneEmpleados(int configuracion, int grupoconciliacion);
        public abstract List<ListaCombo> ObtieneColumnas(int configuracion, int tipoconciliacion, string campoexterno);

                //Listado de Referencias y Detalle de Referencia
        public abstract List<ReferenciaAComparar> ObtieneReferencias(int configuracion );
        public abstract ReferenciaAComparar ObtieneReferenciaPorId(int configuracion,int tipoconciliacion, int secuencia);
        public abstract List<ReferenciaAComparar> ObtieneReferenciaPorIdLista(int configuracion, int tipoconciliacion, int secuencia, string columnaex , string columnain);

        //Listas nuevas
        public abstract List<CuentaTransferencia> ObtenieneCuentasTransferenciaOrigenDestino(int configuracion, int corporativoOrigen, int sucursalOrigen, string cuentaBancoOrigen, int banco);

        public abstract List<CuentaTransferencia> ObtenieneCuentasTransferenciaOrigenDestinoTodas(int configuracion, int corporativoOrigen, int sucursalOrigen, string cuentaBancoOrigen, int banco);

        public abstract GrupoConciliacionUsuario ObtieneGrupoConciliacionUsuarioEspecifico(string usuario);

        public abstract List<CuentaBancoSaldo> ConsultaCuentaBancariaSaldoFinalDia(int corporativo, int sucursal,int banco,  string cuentabancaria, DateTime fconsulta);//, int grupoconciliacion

        public abstract List<DepositoFacturaCom> ConsultaDepositoFacturaComp(int TipoFecha, DateTime FechaIni, DateTime FechaFin);

        public abstract Consultas CrearObjeto();

        public abstract List<Bancos> ObtieneBancos();
        public abstract List<CuentaContableBanco> ObtieneCuentaContableBanco(int Banco);
        public abstract List<TipoCobro> ObtieneTipoCobro();
        public abstract List<ColumnaDestino> ObtieneColumnaDestino();
        public abstract List<PalabrasClave> ConsultarPalabrasClave(int Banco,string CuentaBanco,int TipoCobro, string columna);

        public virtual string CadenaConexion
        {
            get
            {
                Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
                return objApp.CadenaConexion;
            }
        }

    }
}
