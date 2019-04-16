using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ControlesUsuario_ClientePago_wucBuscadorPagoEstadoCuenta : System.Web.UI.UserControl
{
    private int corporativo;
    private int sucursal;
    private int año;
    private int mes;
    private int folio;
    //public ListaPago;
    private bool activaEstaConciliacion;

    private int Corporativo;
    private int Sucursal;
    private int Año;
    private int Mes;
    private int Folio;
    //public ListaPago;
    public bool ActivaEstaConciliacion;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarGrid();
        }
    }

    public void CargarGrid()
    {

    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {

    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {

    }
}