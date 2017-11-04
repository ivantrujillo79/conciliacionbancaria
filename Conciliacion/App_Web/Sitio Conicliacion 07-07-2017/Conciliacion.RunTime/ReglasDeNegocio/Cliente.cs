using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conciliacion.RunTime.DatosSQL;

namespace Conciliacion.RunTime.ReglasDeNegocio{
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

        #region Propiedades
        public byte Celula
        {
            get { return celula; }
            set { celula = value; }
        }

        public Int16 DigitoVerificador
        {
            get { return digitoverificador;}
            set { digitoverificador = value; }
        }

        public string Nombre
        {
            get { return nombre;}
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
            set {saldo = value; }
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
        #endregion

        #region Constructores
        public Cliente()
        {
        }
        public Cliente(string Referencia)
        {
        }
        public virtual void Dispose()
        {
		}
        #endregion

        public abstract bool ValidaClienteExiste(Conexion _conexion);
        public abstract List<ReferenciaNoConciliadaPedido> ObtienePedidosNoConciliadosCliente(cConciliacion Conciliacion, Conexion _conexion);

	}//end Cliente

}//end namespace ReglasDeNegocio