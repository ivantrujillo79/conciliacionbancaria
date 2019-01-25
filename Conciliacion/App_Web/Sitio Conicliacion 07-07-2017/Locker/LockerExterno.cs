using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker
{
    public static class LockerExterno
    {
        public static List<RegistroExternoBloqueado> ExternoBloqueado { get; set; }

        public static void EliminarBloqueos(string IDSesion)
        {
            if (ExternoBloqueado == null)
                return;
            else
                ExternoBloqueado.RemoveAll(x => x.SessionID == IDSesion);
        }

        public static void EliminarBloqueos(string IDSesion, string FormaConciliacion)
        {
            if (ExternoBloqueado == null)
                return;
            else
                ExternoBloqueado.RemoveAll(x => x.SessionID == IDSesion && x.FormaConciliacion == FormaConciliacion);
        }
    }

    public class RegistroExternoBloqueado
    {
        public string FormaConciliacion { get; set; }
        public string SessionID { get; set; }
        public int Año { get; set; }
        public int Folio { get; set; }
        public int Consecutivo { get; set; }
        public int Corporativo { get; set; }
        public int Sucursal { get; set; }
        public string Usuario { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        public DateTime InicioBloqueo { get; set; }
        public int Secuencia { get; set; }
    }
}
