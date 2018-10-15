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
    protected string MyString = "";

    private GridView _GridRelacionado = new GridView();
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

    private string _Factura;
    public string Factura {
        get { return txtFactura.Text.Trim(); }
        set { _Factura = value; }
    }

    private GridView grvpedidos;
    public GridView grvPedidos
    { 
        get { return grvpedidos; }
        set { grvpedidos = value; }
    }

    private DataTable _TablaFacturas;
    public DataTable TablaFacturas {
        get
        {
            BuscarFactura(this.Factura); return _TablaFacturas; }
        set { _TablaFacturas = value; }
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
        catch(Exception ex)
        {
            throw new Exception("Ingrese un número de cliente válido. " + ex.Message);
        }
    }

    public DataTable BuscaCliente()
    {
        try
        {
            DataTable dtClientePedidos = null;
            NumeroClienteFiltrar = txtCliente.Text.Trim();

            if (txtCliente.Text.Trim() != "")
            {
                Cliente cliente = App.Cliente.CrearObjeto();

                Conexion conexion = new Conexion();
                conexion.AbrirConexion(true);

                dtClientePedidos = cliente.ObtienePedidosCliente(Convert.ToInt64(NumeroClienteFiltrar), conexion);
            }

            return dtClientePedidos;
        }
        catch(Exception ex)
        {
            throw new Exception("Ingrese un número de cliente válido. " + ex.Message);
        }

    }

    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {        
        
    }


    protected void btnBuscaFactura_Click(object sender, ImageClickEventArgs e)
    {
        BuscarFactura(txtFactura.Text);
    }

    private void BuscarFactura(string NumeroFactura)
    {
        _Factura = NumeroFactura;
        DataTable tbPedidosPorFactura = null;
        if (NumeroFactura != string.Empty)
            tbPedidosPorFactura = App.Consultas.CBPedidosPorFactura(NumeroFactura);
        Session["CBPedidosPorFactura"] = tbPedidosPorFactura;
        _TablaFacturas = tbPedidosPorFactura;

        if (grvpedidos != null)
        {
            grvpedidos.DataSource = tbPedidosPorFactura;
            grvpedidos.DataBind();
            grvpedidos.DataBind();
        }
    }
    
}