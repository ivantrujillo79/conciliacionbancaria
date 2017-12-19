using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Conciliacion.RunTime;
using Conciliacion.RunTime.ReglasDeNegocio;
using Conciliacion.RunTime.DatosSQL;

public partial class ControlesUsuario_BuscadorClienteFactura_wucBuscaClientesFacturas : System.Web.UI.UserControl
{
    protected string MyString = "This is my string";

    private GridView _GridRelacionado = new GridView();
    //public GridView GridRelacionado { get { return _GridRelacionado; } set { _GridRelacionado = GridRelacionado; } }
    public string HtmlIdGridRelacionado;
    public string HtmlIdGridCeldaID;
    public string HtmlIdGridCNodoID;

    private string NumeroClienteFiltrar;
    private string NumeroFacturaResaltar;
    private DataTable dtOriginal;
    private DataTable dtFiltado;

    public string Cliente {
        get { return txtCliente.Text.Trim(); }
        set { NumeroClienteFiltrar = value; }
    } 

    private GridView grvpedidos;
    public GridView grvPedidos
    { 
        get { return grvpedidos; }
        set { grvpedidos = value; }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (this.DesignMode == true)
        {
            this.EnsureChildControls();
        }
        this.Page.RegisterRequiresControlState(this);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["CBPedidosPorFactura"] = null;
        if (HttpContext.Current.Session["wucBuscaClientesFacturasVisible"] != null)
        { 
            if (int.Parse(HttpContext.Current.Session["wucBuscaClientesFacturasVisible"].ToString()) == 1)
                Visible = true;
            else
                Visible = false;
        }
        else
            Visible = false;
    }

    public DataTable FiltraCliente(GridView GridView)
    {
        //Método privado en el que durante su ejecución los registros asignados en el datasource del grid asignado por medio de 
        //la propiedad "GridRelacionado", serán filtrados para sólo mostrar los pedidos relacionados con el cliente provisto por la 
        //propiedad "NumeroClienteFiltrar".

        try
        {
            DataView dv = null;
            NumeroClienteFiltrar = txtCliente.Text.Trim();
            if (txtCliente.Text.Trim() != "" && GridView != null && GridView.DataSource != null)
            {
                DataTable dtDatos = (DataTable)GridView.DataSource;
                if (NumeroClienteFiltrar != string.Empty)
                {
                    dv = new DataView(dtDatos);
                    dv.RowFilter = "Cliente = " + NumeroClienteFiltrar;
                }
                if (dv == null)
                    return dtDatos;
                else
                    return dv.ToTable();
            }
            else
                return null;
        }
        catch(Exception)
        {
            throw new Exception("Ingrese un número de cliente válido.");
        }
    }

    public DataTable BuscaCliente()
    {
        //Método privado en el que durante su ejecución los registros asignados en el datasource del grid asignado por medio de 
        //la propiedad "GridRelacionado", serán filtrados para sólo mostrar los pedidos relacionados con el cliente provisto por la 
        //propiedad "NumeroClienteFiltrar".

        //DataView dv = null;
        try
        {
            DataTable dtClientePedidos = null;
            NumeroClienteFiltrar = txtCliente.Text.Trim();

            if (txtCliente.Text.Trim() != "")
            {
                Cliente cliente = App.Cliente.CrearObjeto();

                Conexion conexion = new Conexion();
                conexion.AbrirConexion(true);

                dtClientePedidos = cliente.ObtienePedidosCliente(Convert.ToInt32(NumeroClienteFiltrar), conexion);
            }

            return dtClientePedidos;
        }
        catch(Exception)
        {
            throw new Exception("Ingrese un número de cliente válido.");
        }

    }

    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {        
        
    }


    protected void btnBuscaFactura_Click(object sender, ImageClickEventArgs e)
    {
        DataTable tbPedidosPorFactura = null;
        if (txtFactura.Text != string.Empty)
            tbPedidosPorFactura = App.Consultas.CBPedidosPorFactura(txtFactura.Text);
        Session["CBPedidosPorFactura"] = tbPedidosPorFactura;
        if (grvpedidos != null)
        { 
            grvpedidos.DataSource = tbPedidosPorFactura;
            grvpedidos.DataBind();
            grvpedidos.DataBind();
        }
    }
}