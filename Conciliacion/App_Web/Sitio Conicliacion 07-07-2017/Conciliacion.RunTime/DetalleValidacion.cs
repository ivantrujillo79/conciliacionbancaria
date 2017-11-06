

namespace Estructural.Clases.Conciliacion.Migracion.Runtime.Validacion {

    public class DetalleValidacion {

		bool verificacionvalida;
		int codigoerror;
		string mensaje;

		public DetalleValidacion(){

		}

        public DetalleValidacion(bool VerificacionValida, int CodigoError, string Mensaje)
        {
            this.VerificacionValida = VerificacionValida;
            this.CodigoError = CodigoError;
            this.Mensaje = Mensaje;
        }

        ~DetalleValidacion(){

		}

		public virtual void Dispose(){

		}

        #region Propiedades

        public bool VerificacionValida
        {
            get { return verificacionvalida; }
            set { verificacionvalida = value; }
        }
        public int CodigoError
        {
            get { return codigoerror; }
            set { codigoerror = value; }
        }

        public string Mensaje
        {
            get { return mensaje; }
            set { mensaje = value; }
        }
        
        #endregion
    }

}