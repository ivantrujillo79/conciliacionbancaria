using Estructural.Clases.Conciliacion.Migracion.Runtime.Validacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClientePagaraNoDisponibleException
/// </summary>
public class ClientePagaraNoDisponibleException
{

    private DetalleValidacion resultadovalidacion;

    ~ClientePagaraNoDisponibleException()
    {

    }

    public virtual void Dispose()
    {

    }

    public DetalleValidacion ResultadoValidacion
    {
        get
        {
            return resultadovalidacion;
        }
        set
        {
            ResultadoValidacion = value;
        }
    }

    public ClientePagaraNoDisponibleException()
    {
        ResultadoValidacion.CodigoError = 1;
        ResultadoValidacion.Mensaje = "No existe cliente al cual asignar el pago.";
        ResultadoValidacion.VerificacionValida = false;
        //throw new Exception("No existe cliente al cual asignar el pago.");

    }
}