using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Locker;

public partial class poc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        int c = 0;
        
        if (LockerExterno.ExternoBloqueado == null)
        {
            LockerExterno.ExternoBloqueado = new List<RegistroExternoBloqueado>();
            LockerExterno.ExternoBloqueado.Add(new RegistroExternoBloqueado { SessionID = Session.SessionID, Año = 2018, Folio = 5050, Consecutivo = c++,Usuario="ROPIMA",Corporativo=1,Sucursal=1,Monto=100,InicioBloqueo=DateTime.Today.Date,Descripcion="PRUEBAS" });
        }
        else
        {
            LockerExterno.ExternoBloqueado.Add(new RegistroExternoBloqueado { SessionID = Session.SessionID, Año = 2018, Folio = 5050, Consecutivo = c++,Usuario="ROPIMA",Corporativo=1,Sucursal=1,Monto=100, InicioBloqueo = DateTime.Today.Date, Descripcion = "PRUEBAS" });
        }


    }
}