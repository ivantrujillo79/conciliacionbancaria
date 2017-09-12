using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conciliacion.RunTime;

namespace CatalogoConciliacion.ReglasNegocio
{
    public abstract class CuentaBancoSaldo : EmisorMensajes
    {
        private int corporativo;
        private int sucursal;
        private int banco;
        private string bancodes;
        private string cuentabancaria;
        private decimal saldoinicialmes;
        private decimal saldofinal;

        #region constructores

        public CuentaBancoSaldo(IMensajesImplementacion implemntadorMensajes)
        {
            this.corporativo = 0;
            this.sucursal = 0;
            this.banco = 0;
            this.bancodes = "";
            this.cuentabancaria = "";
            this.saldoinicialmes = 0;
            this.saldofinal = 0;
            this.implementadorMensajes = implemntadorMensajes;
        }
        public CuentaBancoSaldo(int corporativo, int sucursal, int banco, string bancodes, string cuentabancaria,
                                decimal saldoinicialmes, decimal saldofinal, IMensajesImplementacion implemntadorMensajes)
        {
            this.corporativo = corporativo;
            this.sucursal = sucursal;
            this.banco = banco;
            this.bancodes = bancodes;
            this.cuentabancaria = cuentabancaria;
            this.saldoinicialmes = saldoinicialmes;
            this.saldofinal = saldofinal;
            this.implementadorMensajes = implemntadorMensajes;
        }
        #endregion

        #region Propiedades
        public int Corporativo
        {
            get { return corporativo; }
            set { corporativo = value; }
        }
        public int Sucursal
        {
            get { return sucursal; }
            set { sucursal = value; }
        }
        public int Banco
        {
            get { return banco; }
            set { banco = value; }
        }
        public string BancoDes
        {
            get { return bancodes; }
            set { bancodes = value; }
        }
        public string CuentaBancaria
        {
            get { return cuentabancaria; }
            set { cuentabancaria = value; }
        }
        public decimal SaldoInicialMes
        {
            get { return saldoinicialmes; }
            set { saldoinicialmes = value; }
        }
        public decimal SaldoFinal
        {
            get { return saldofinal; }
            set { saldofinal = value; }
        }
        public virtual string CadenaConexion
        {
            get
            {
                return App.CadenaConexion;
            }
        }
        #endregion
    }
}
