using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ControlesUsuario_ClientePago_wucClientePago : System.Web.UI.UserControl
{
    private object controlcontenedor;
    private List<String> clientes;
    private string clienteseleccionado;

    public List<String> Clientes
    {
        get
        {
            return clientes;
        }
        set
        {
            clientes = value;
        }
    }

    public object ControlContenedor
    {
        get
        {
            return controlcontenedor;
        }
        set
        {
            controlcontenedor = value;
        }
    }

    public string ClienteSeleccionado
    {
        get
        {
            return clienteseleccionado;
        }
    }

    private void CargarGrid()
    {

    }

    private void ObtieneDetalleClientes()
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}