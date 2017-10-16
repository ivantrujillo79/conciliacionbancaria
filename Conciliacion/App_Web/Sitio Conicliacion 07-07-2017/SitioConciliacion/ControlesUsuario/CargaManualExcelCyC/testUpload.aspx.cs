using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Conciliacion.RunTime;

public partial class Conciliacion_testUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.ImplementadorMensajes.ContenedorActual = this;
    }
}