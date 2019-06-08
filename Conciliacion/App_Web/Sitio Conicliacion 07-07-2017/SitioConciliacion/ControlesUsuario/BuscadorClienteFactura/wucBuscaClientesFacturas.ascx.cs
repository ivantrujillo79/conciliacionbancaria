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
using System.Configuration;

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
    private List<RTGMCore.DireccionEntrega> listaDireccinEntrega = new List<RTGMCore.DireccionEntrega>();
    private bool validarPeticion = false;
    private List<int> listaClientesEnviados;
    private List<int> listaClientes = new List<int>();
    private string _URLGateway;
    Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();

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

    private GridView grvagregados;
    public GridView grvAgregados
    {
        get { return grvagregados; }
        set { grvagregados = value; }
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
        if (IsPostBack)
        {
            string entrar = ViewState["Entrar"] as string;
            if (entrar == "Ok")
            {
                if (Page.Request.Params["__EVENTTARGET"] == "miPostBack")
                {
                    validarPeticion = false;
                    listaDireccinEntrega = ViewState["LISTAENTREGA"] as List<RTGMCore.DireccionEntrega>;
                    listaClientes = ViewState["LISTACLIENTES"] as List<int>;
                    //ViewState["TBL_REFCON_CANTREF"] = tblReferenciasAConciliar;
                    string dat = Page.Request.Params["__EVENTARGUMENT"].ToString();
                    if (dat == "1")
                    {
                        ObtieneNombreCliente(listaClientes);
                    }
                    else if (dat == "2")
                    {
                        llenarListaEntrega();
                    }
                }
            }
        }
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
                Cliente cliente = objApp.Cliente.CrearObjeto();

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
        { 
            tbPedidosPorFactura = objApp.Consultas.CBPedidosPorFactura(NumeroFactura);
            if (Session["TABLADEAGREGADOS"] != null && tbPedidosPorFactura.Rows.Count > 0)
            {   //quita pedidos que ya estan en el grid de preconciliados
                GridView grvAgregados = (GridView)Session["TABLADEAGREGADOS"];
                DataTable tableAgregados = (DataTable)grvAgregados.DataSource;
                var tableOrigen = tbPedidosPorFactura;
                //var tableResult = tbPedidosPorFactura.Clone();
                foreach (DataRow row in tableAgregados.Rows)
                { 
                    var rows = tableOrigen.AsEnumerable().Where(x => x.Field<int>("Pedido") != int.Parse(row["Pedido"].ToString()));
                    var dt = rows.Any() ? rows.CopyToDataTable() : tableOrigen.Clone();
                    tableOrigen.Clear();
                    foreach (DataRow r in dt.Rows)
                        tableOrigen.ImportRow(r);
                }
                tbPedidosPorFactura = tableOrigen; // tableResult;
            }
        }
        ViewState["POR_CONCILIAR"] = tbPedidosPorFactura;
        ViewState["Entrar"] = "Ok";
        List<int> listadistintos = new List<int>();
        listaClientesEnviados = new List<int>();
        try
        {
            listaDireccinEntrega = ViewState["LISTAENTREGA"] as List<RTGMCore.DireccionEntrega>;
            if (listaDireccinEntrega == null)
            {
                listaDireccinEntrega = new List<RTGMCore.DireccionEntrega>();
            }
        }
        catch (Exception)
        {

        }
        foreach (DataRow item in tbPedidosPorFactura.Rows)
        {
            if (!listaDireccinEntrega.Exists(x => x.IDDireccionEntrega == int.Parse(item["Cliente"].ToString())))
            {
                if (!listadistintos.Exists(x => x == int.Parse(item["Cliente"].ToString())))
                {
                    listadistintos.Add(int.Parse(item["Cliente"].ToString()));
                }
            }
        }
        try
        {
            if (listadistintos.Count > 0)
            {
                validarPeticion = true;
                ObtieneNombreCliente(listadistintos);
            }
            else
            {
                llenarListaEntrega();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        //Session["CBPedidosPorFactura"] = tbPedidosPorFactura;
        //_TablaFacturas = tbPedidosPorFactura;

        //if (grvpedidos != null)
        //{
        //    grvpedidos.DataSource = tbPedidosPorFactura;
        //    grvpedidos.DataBind();
        //    grvpedidos.DataBind();
        //}
    }
    private void llenarListaEntrega()
    {
        DataTable tbPedidosPorFactura = ViewState["POR_CONCILIAR"] as DataTable;
        try
        {
            foreach (DataRow item in tbPedidosPorFactura.Rows)
            {
                try
                {
                    RTGMCore.DireccionEntrega cliente = listaDireccinEntrega.FirstOrDefault(x => x.IDDireccionEntrega == int.Parse(item["Cliente"].ToString()));
                    if (cliente != null)
                    {
                        item["Nombre"] = cliente.Nombre;
                    }
                    else
                    {
                        item["Nombre"] = "No encontrado";
                    }
                }
                catch(Exception Ex)
                {
                    item["Nombre"] = Ex.Message;
                }
            }

           
            Session["CBPedidosPorFactura"] = tbPedidosPorFactura;
            _TablaFacturas = tbPedidosPorFactura;

            if (grvpedidos != null)
            {
                grvpedidos.DataSource = tbPedidosPorFactura;
                grvpedidos.DataBind();
                grvpedidos.DataBind();
            }
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
        finally
        {
            ViewState["Entrar"] = "No";
        }
    }

    public void completarListaEntregas(List<RTGMCore.DireccionEntrega> direccionEntregas)
    {
        RTGMCore.DireccionEntrega direccionEntrega;
        RTGMCore.DireccionEntrega direccionEntregaTemp;
        bool errorConsulta = false;
        try
        {
            foreach (var item in direccionEntregas)
            {
                try
                {
                    if (item != null)
                    {
                        if (item.Message != null)
                        {
                            direccionEntrega = new RTGMCore.DireccionEntrega();
                            direccionEntrega.IDDireccionEntrega = item.IDDireccionEntrega;
                            direccionEntrega.Nombre = item.Message;
                            listaDireccinEntrega.Add(direccionEntrega);
                        }
                        else if (item.IDDireccionEntrega == -1)
                        {
                            errorConsulta = true;
                        }
                        else if (item.IDDireccionEntrega >= 0)
                        {
                            listaDireccinEntrega.Add(item);
                        }
                    }
                    else
                    {
                        direccionEntrega = new RTGMCore.DireccionEntrega();
                        direccionEntrega.IDDireccionEntrega = item.IDDireccionEntrega;
                        direccionEntrega.Nombre = "No se encontró cliente";
                        listaDireccinEntrega.Add(direccionEntrega);
                    }
                }
                catch (Exception ex)
                {
                    direccionEntrega = new RTGMCore.DireccionEntrega();
                    direccionEntrega.IDDireccionEntrega = item.IDDireccionEntrega;
                    direccionEntrega.Nombre = ex.Message;
                    listaDireccinEntrega.Add(direccionEntrega);
                }
            }
            if (validarPeticion && errorConsulta)
            {
                validarPeticion = false;
                listaClientes = new List<int>();
                foreach (var item in listaClientesEnviados)
                {
                    direccionEntregaTemp = listaDireccinEntrega.FirstOrDefault(x => x.IDDireccionEntrega == item);
                    if (direccionEntregaTemp == null)
                    {
                        listaClientes.Add(item);
                    }
                }
                ViewState["LISTAENTREGA"] = listaDireccinEntrega;
                ViewState["LISTACLIENTES"] = listaClientes;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Mensaje", " mensajeAsincrono(" + listaClientes.Count + ");", true);
            }
            else
            {
                llenarListaEntrega();
            }
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    private void ObtieneNombreCliente(List<int> listadistintos)
    {
        RTGMGateway.RTGMGateway oGateway;
        RTGMGateway.SolicitudGateway oSolicitud;
        AppSettingsReader settings = new AppSettingsReader();
        string cadena = objApp.CadenaConexion;
        try
        {
            SeguridadCB.Public.Parametros parametros;
            parametros = (SeguridadCB.Public.Parametros)HttpContext.Current.Session["Parametros"];
            _URLGateway = parametros.ValorParametro(Convert.ToSByte(settings.GetValue("Modulo", typeof(sbyte))), "URLGateway").Trim();
            SeguridadCB.Public.Usuario user = (SeguridadCB.Public.Usuario)Session["Usuario"];
            oGateway = new RTGMGateway.RTGMGateway(byte.Parse(settings.GetValue("Modulo", typeof(string)).ToString()), objApp.CadenaConexion);//,_URLGateway);
            oGateway.ListaCliente = listadistintos;
            oGateway.URLServicio = _URLGateway;//"http://192.168.1.21:88/GasMetropolitanoRuntimeService.svc";//URLGateway;
            oGateway.eListaEntregas += completarListaEntregas;
            oSolicitud = new RTGMGateway.SolicitudGateway();
            listaClientesEnviados = listadistintos;
            foreach (var item in listadistintos)
            {
                oSolicitud.IDCliente = item;
                oGateway.busquedaDireccionEntregaAsync(oSolicitud);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}