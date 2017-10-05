using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.ValidacionArchivosConciliacion
{
    #region DetalleValidacion
    public class DetalleValidacion
    {

        public bool VerificacionValida;
        public int CodigoError;
        public string Mensaje;

        public DetalleValidacion()
        {

        }

        ~DetalleValidacion()
        {

        }

        public virtual void Dispose()
        {

        }

    }//end DetalleValidacion
    #endregion

    #region ValidadorCyC
    public class ValidadorCyC
    {

        public string rutaArchivo;
        public string nombreArchivo;

        public ValidadorCyC()
        {

        }

        ~ValidadorCyC()
        {

        }

        public virtual void Dispose()
        {

        }

        //public ValidadorCyC new(){
		//	return null;
		//}

		//public ValidadorCyC new(string RutaArchivo, string NombreArchivo)
        //{
		//	return null;
        //}

        public DetalleValidacion ValidacionCompleta()
        {

            return null;
        }

        private DetalleValidacion ValidaCuentaBancaria()
        {

            return null;
        }

        private DetalleValidacion ValidaDocumentoReferencia()
        {

            return null;
        }

        private DetalleValidacion ValidaMonto()
        {

            return null;
        }

        private DetalleValidacion ValidaEncabezado()
        {

            return null;
        }

        private DetalleValidacion ValidaFormatoExcel()
        {

            return null;
        }

        private DetalleValidacion ValidaLayout()
        {

            return null;
        }

        private List<DetalleValidacion> ValidaLineaVacia()
        {

            return null;
        }

    }//end ValidadorCyC
    #endregion

    #region IValidadorExcel
    public interface IValidadorExcel
    {

    }//end IValidadorExcel
    #endregion

}
