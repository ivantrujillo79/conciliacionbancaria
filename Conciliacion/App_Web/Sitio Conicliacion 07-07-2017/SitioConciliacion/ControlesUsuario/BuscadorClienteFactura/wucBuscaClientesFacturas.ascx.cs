﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

public partial class ControlesUsuario_BuscadorClienteFactura_wucBuscaClientesFacturas : System.Web.UI.UserControl
{

    private GridView _GridRelacionado = new GridView();
    public GridView GridRelacionado
    {
      get { return _GridRelacionado; }
      set { _GridRelacionado = GridRelacionado; }
    }
    public string HtmlIdGridRelacionado;
    //private string _HtmlIdGridRelacionado;// = "ctl00_contenidoPrincipal_grvAgregadosPedidos";
    //public string HtmlIdGridRelacionado
    //{
    //    get { return _HtmlIdGridRelacionado; }
    //    set { _HtmlIdGridRelacionado = HtmlIdGridRelacionado; }
    //}

    private string NumeroClienteFiltrar;
    private string NumeroFacturaResaltar;
    private DataTable dtOriginal;
    private DataTable dtFiltado;

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

    }

    public DataTable FiltraCliente(GridView GridView)
    {
        //Método privado en el que durante su ejecución los registros asignados en el datasource del grid asignado por medio de 
        //la propiedad "GridRelacionado", serán filtrados para sólo mostrar los pedidos relacionados con el cliente provisto por la 
        //propiedad "NumeroClienteFiltrar".

        DataView dv = null;
        DataTable dtDatos = (DataTable)GridView.DataSource;
        NumeroClienteFiltrar = txtCliente.Text.Trim();
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
    
    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {        
        //if (Session["TABLADEAGREGADOS"] != null)
        //{
        //    GridView grvAgregadosPedidosPrima = (GridView)Session["TABLADEAGREGADOS"];
        //    _GridRelacionado.DataSource = FiltraCliente(grvAgregadosPedidosPrima);
        //    _GridRelacionado.DataBind();
        //    _GridRelacionado.DataBind();
        //}
    }

}