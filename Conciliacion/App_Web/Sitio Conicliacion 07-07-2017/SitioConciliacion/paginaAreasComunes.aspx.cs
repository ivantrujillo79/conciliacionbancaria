using Conciliacion.RunTime.DatosSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class paginaAreasComunes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnBuscaClientePadre_Click(object sender, EventArgs e)
    {

        Conexion conexion = new Conexion();

        wuAreascomunes.inicializa(int.Parse(txtClientePadre.Text), Convert.ToDecimal(txtmonto.Text));
        wuAreascomunes.cargaDatos();
        mpeAreasComunes.Show();
        
    }
}