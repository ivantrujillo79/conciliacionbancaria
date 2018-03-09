using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Conciliacion.RunTime.ReglasDeNegocio;

public partial class ControlesUsuario_ListadoCajas_wucListadoCajas : System.Web.UI.UserControl
{
    private List<Caja> lstCajas;
    private List<Caja> lstCajasSeleccionadas;

    #region Propiedades

    public List<Caja> LstCajas
    {
        get { return lstCajas; }
        set { lstCajas = value; }
    }

    public List<Caja> LstCajasSeleccionadas
    {
        get { return lstCajasSeleccionadas; }
        set { lstCajasSeleccionadas = value; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}