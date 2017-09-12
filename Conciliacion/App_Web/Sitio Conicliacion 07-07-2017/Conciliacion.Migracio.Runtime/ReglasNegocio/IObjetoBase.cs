using System;
namespace Conciliacion.Migracion.Runtime.ReglasNegocio
{
    public interface IObjetoBase
    {
        bool Actualizar();
        string CadenaConexion { get; }
        IObjetoBase CrearObjeto();
        bool Eliminar();
        bool Guardar();
    }
}
