using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Locker;

public partial class POCA : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LockerExterno.ExternoBloqueado != null)
        {
            if (LockerExterno.ExternoBloqueado.Count > 0)
            {
                Label1.Text = "Registros: " + LockerExterno.ExternoBloqueado.Count.ToString() + " registro 0: " + LockerExterno.ExternoBloqueado[0].SessionID.ToString() + " " + LockerExterno.ExternoBloqueado[0].Folio.ToString() + " " + LockerExterno.ExternoBloqueado[0].Consecutivo.ToString();
                foreach (RegistroExternoBloqueado obj in LockerExterno.ExternoBloqueado)
                {
                    Response.Write(obj.SessionID.ToString() + " " + obj.Folio.ToString() + " " + obj.Consecutivo.ToString() + "<br/>");
                }
            }
        }
    }
}