using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public interface IConciliacion
    {
        bool AgregarReferencia(cReferencia referenciaexterna, cReferencia referenciainterna);
        bool Guardar(string usuario);
        IConciliacion CrearObjeto();
        int Corporativo { get; set; }
        int Sucursal {get; set;}
        int Año { get; set; }
        short Mes { get; set; }
        int Folio { get; set; }
        DatosArchivo ArchivoExterno { get; set; }
        DateTime FInicial { get; set; }
        DateTime FFinal { get; set; }
        String StatusConciliacion {get; set;}
        int GrupoConciliacion {get; set;}
        short TipoConciliacion {get; set;}
        int TransaccionesInternas { get; set; }
        int ConciliadasInternas {get; set;}
        int TransaccionesExternas { get; set; }
        int ConciliadasExternas { get; set; }
        string GrupoConciliacionStr {get; set;}
                        
        List<ReferenciaConciliada> ListaReferenciaConciliada { get; }
        List<DatosArchivo> ListaArchivos {get;}
        List<cReferencia> ListaReferenciaExterna { get; }
    }
}
