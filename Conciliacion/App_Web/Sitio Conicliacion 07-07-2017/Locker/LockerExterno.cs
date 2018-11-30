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
    }

    public class RegistroExternoBloqueado
    {
        public string SessionID { get; set; }
        public int Año { get; set; }
        public int Folio { get; set; }
        public int Consecutivo { get; set; }
    }
}
