using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conciliacion.RunTime.DatosSQL;
using System.Data;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
	public abstract class Cliente: EmisorMensajes
    {

		private int cliente;
		private byte celula;
		private Int16 digitoverificador;
		private string nombre;
		private string referencia;
		private string razonsocial;
		private int ruta;
		private int programacion;
		private string telefonocasa;
		private string telefonoalternouno;
		private string telefonoalternodos;
		private decimal saldo;
		private string email;
		private string direccion;
        private string tipo;

        public Cliente(IMensajesImplementacion implementadorMensajes)
        {
            this.implementadorMensajes = implementadorMensajes;
            this.celula = 0;
            this.digitoverificador = 0;
            this.nombre = "";
            this.referencia = "";
            this.razonsocial = "";
            this.ruta = 0;
            this.programacion = 0;
            this.telefonocasa = "";
            this.telefonoalternouno = "";
            this.telefonoalternodos = "";
            this.saldo = 0;
            this.email = "";
            this.direccion = "";
            this.tipo = "";
        }

        public Cliente(byte celula, 
                       short digitoverificador, 
                       string nombre, 
                       string referencia, 
                       string razonsocial, 
                       int ruta, 
                       int programacion, 
                       string telefonocasa, 
                       string telefonoalternouno, 
                       string telefonoalternodos, 
                       decimal saldo, 
                       string email, 
                       string direccion, 
                       string tipo, IMensajesImplementacion implementadorMensajes)
        {
            this.celula = celula;
            this.digitoverificador = digitoverificador;
            this.nombre = nombre;
            this.referencia = referencia;
            this.razonsocial = razonsocial;
            this.ruta = ruta;
            this.programacion = programacion;
            this.telefonocasa = telefonocasa;
            this.telefonoalternouno = telefonoalternouno;
            this.telefonoalternodos = telefonoalternodos;
            this.saldo = saldo;
            this.email = email;
            this.direccion = direccion;
            this.tipo = tipo;
            this.implementadorMensajes = implementadorMensajes;            
        }

        #region Propiedades
        public byte Celula
        {
            get { return celula; }
            set { celula = value; }
        }

        public Int16 DigitoVerificador
        {
            get { return digitoverificador; }
            set { digitoverificador = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Referencia
        {
            get { return referencia; }
            set { referencia = value; }
        }

        public string RazonSocial
        {
            get { return razonsocial; }
            set { razonsocial = value; }
        }

        public int Ruta
        {
            get { return ruta; }
            set { ruta = value; }
        }

        public int Programacion
        {
            get { return programacion; }
            set { programacion = value; }
        }

        public string TelefonoCasa
        {
            get { return telefonocasa; }
            set { telefonocasa = value; }
        }

        public string TelefonoAlternoUno
        {
            get { return telefonoalternouno; }
            set { telefonoalternouno = value; }
        }

        public string TelefonoAlternoDos
        {
            get { return telefonoalternodos; }
            set { telefonoalternodos = value; }
        }

        public decimal Saldo
        {
            get { return saldo; }
            set { saldo = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        public int NumCliente
        {
            get { return cliente; }
            set { cliente = value; }
        }

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        #endregion

        public abstract Cliente CrearObjeto();

        public abstract bool ValidaClienteExiste(Conexion _conexion);

        public abstract List<ReferenciaNoConciliadaPedido> ObtienePedidosNoConciliadosCliente(cConciliacion Conciliacion, Conexion _conexion);

        public abstract DataTable ObtienePedidosCliente(int Cliente, Conexion _conexion);

        public abstract DetalleClientePedidoExcel ObtieneDetalleClientePedidoExcel(string PedidoReferencia, Conexion _conexion);
        
        public abstract string consultaClienteCRM(int cliente);

        public abstract string consultaClienteCRM(int cliente, string URLGateway);

    }//end Cliente


    public class ClienteAuxiliar
    {
        public int Cliente { get; set; }
        public string Referencia { get; set; }
        
        public ClienteAuxiliar()
        {
            
        }

    }
}//end namespace ReglasDeNegocio