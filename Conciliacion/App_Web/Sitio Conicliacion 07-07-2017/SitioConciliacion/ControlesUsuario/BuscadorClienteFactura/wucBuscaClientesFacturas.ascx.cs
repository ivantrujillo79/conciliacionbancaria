using System;
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
    private UpdatePanel _updatep;
    public UpdatePanel UpdatePanelConciliacion
    {
        get { return _updatep; }
        set { _updatep = UpdatePanelConciliacion; }
    }

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

    public GridView ResaltaFactura(GridView GridView)
    {
        /*Método que resaltará la columna Factura (efecto de  marca textos) en el grid provisto por la propiedad "GridRelacionado" 
         * para todos los registros que cuenten con la cadena provista por el usuario a través de la propiedad "NumeroFacturaResaltar". 
         * IMPORTANTE La factura se compone por una Serie consistente en una cadena de caracteres (máximo 10) 
         * y un Folio de tipo entero según se define en la tabla Factura de la base de datos del sistema Sigamet.*/

        NumeroFacturaResaltar = txtFactura.Text.Trim();
        //GridRelacionado.RowDataBound += new GridViewRowEventHandler(GridRelacionado_RowDataBound);

        DataTable dt = (DataTable)GridView.DataSource;
        if (NumeroFacturaResaltar.Trim() != string.Empty)
        {
            //int columna = 1; //revisar la columna con # factura
            //CssStyleCollection Style;

            foreach (DataRow gvRow in dt.Rows)
            {
                //if (gvRow.RowType == DataControlRowType.DataRow)
                //{
                    //Style = gvRow.Cells[columna].Style;
                    //if (gvRow.Cells[columna].Value.ToString().Equals(NumeroFacturaResaltar))
                    //    Style.ForeColor = Color.FromArgb(204, 255, 0);
                    //else
                    //    Style.ForeColor = Color.FromArgb(255, 255, 255);

                    //gvRow.Cells[1].CssClass = "color: yellow;";

                    //gvRow.Cells[1].Text = "<FONT COLOR='blue'>Texto AZUL </FONT>";

                    DataColumn dc = (DataColumn)gvRow.Table.Columns[1];
                    string s = Convert.ToString(gvRow[dc.ColumnName]);
                    s = s.Replace(NumeroFacturaResaltar.Trim(), "<span style='color:RED'>" + NumeroFacturaResaltar.Trim() + "</span>");
                    gvRow[dc.ColumnName] = s;

                //}
            }
        }

        //GridRelacionado.Rows[0].Cells[0].Style.Add("color", "blue");

        GridView.DataSource = dt;
        GridView.DataBind();
        return GridView;
    }

    protected void GridRelacionado_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = e.Row.DataItem as DataRowView;
            if (drv["ColumnName"].ToString().Equals("Something"))
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }
            else
            {
                e.Row.BackColor = System.Drawing.Color.Green;
            }
        }
    }

    protected void txtCliente_TextChanged(object sender, EventArgs e)
    {
        NumeroClienteFiltrar = txtCliente.Text;
    }

    protected void txtFactura_TextChanged(object sender, EventArgs e)
    {
        NumeroFacturaResaltar = txtFactura.Text;
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