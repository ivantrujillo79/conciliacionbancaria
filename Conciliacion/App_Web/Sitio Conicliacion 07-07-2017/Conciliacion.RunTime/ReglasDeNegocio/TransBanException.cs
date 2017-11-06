//using Estructural.Clases.Conciliacion.Migracion.Runtime.Validacion;
using ValidacionArchivosConciliacion;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    /// <summary>
    /// Clase genérica para el control de excepciones provenientes desde el proceso de
    /// generación de relaciones de cobranza en un subcomponente determinado, su
    /// objetivo es el de proveer un alto nivel de detalle en los mensajes de error y
    /// también el de facilitar la identificación de fuentes de incidentes.
    /// </summary>
    public class TransBanException
    {

        private DetalleValidacion detalleexcepcion = new DetalleValidacion();

        public TransBanException()
        {

        }

        ~TransBanException()
        {

        }

        public virtual void Dispose()
        {

        }

        public DetalleValidacion DetalleExcepcion
        {
            get
            {
                return detalleexcepcion;
            }
            set
            {
                detalleexcepcion = value;
            }
        }

    }//end RelacionCobranzaException

}//end namespace SitioConciliacion