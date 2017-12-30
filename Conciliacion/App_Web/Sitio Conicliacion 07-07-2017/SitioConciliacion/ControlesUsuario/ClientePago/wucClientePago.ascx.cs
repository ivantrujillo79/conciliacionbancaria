using Conciliacion.RunTime.ReglasDeNegocio;
using System;
using System.Collections.Generic;
using System.Data;
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

    DataTable dt = new DataTable();

    private int indiceExternoSeleccionado
    {
        get { return Convert.ToInt32(hdfIndiceFila.Value); }
        set { hdfIndiceFila.Value = value.ToString(); }
    }

    private void CargarGrid()
    {
        
        //seleccion de cliente: se selecciona el padre, si no hay padre se selecciona el hijo con el numero de cliente mas pequeno
        
        //remover duplidos
        Clientes = Clientes.Distinct().ToList();
        
        dt.Clear();
        dt.Columns.Clear();
        dt.Columns.Add("Cliente");
        dt.Columns.Add("Nombre");
        dt.Columns.Add("Tipo");

        DataRow row;
        foreach (string cli in Clientes)
        {
            //validar
            Cliente objCliente = Conciliacion.RunTime.App.Cliente.CrearObjeto();
            Conciliacion.RunTime.DatosSQL.Conexion conexion = new Conciliacion.RunTime.DatosSQL.Conexion();
            conexion.AbrirConexion(true);
            objCliente.Referencia = cli;
            if (objCliente.ValidaClienteExiste(conexion))
            {
                //asignar nombre y tipo 
                row = dt.NewRow();
                row["Cliente"] = cli;
                row["Nombre"] = objCliente.Nombre;
                row["Tipo"] = "SUCURSAL"; //FALTA
                dt.Rows.Add(row);
            }
        }

        //El gridview deberá mostrar los registros ordenados por número de Cliente de forma ascendente 
        dt.DefaultView.Sort = "Cliente";

        grvClientes.DataSource = dt;
        grvClientes.DataBind();
        
    }

    private void ObtieneDetalleClientes()
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            CargarGrid();
        }
    }

    protected void grvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Response.Write(e.CommandArgument);
        if (e.CommandName == "SeleccionarCliente")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            // Recupera la fila en la que se hizo clic al botón
            GridViewRow row = grvClientes.Rows[index];
        }
    }

    public void pintarFilaSeleccionada(int fila)
    {
        grvClientes.Rows[fila].CssClass = "bg-color-azulClaro01 fg-color-blanco";
        grvClientes.Rows[fila].Cells[0].CssClass = "bg-color-azulClaro01";
        grvClientes.Rows[fila].Cells[1].CssClass = "bg-color-azulClaro01";
        grvClientes.Rows[fila].Cells[2].CssClass = "bg-color-azulClaro01";
    }

    public void despintarFilaSeleccionada(int fila)
    {
        grvClientes.Rows[fila].CssClass = "bg-color-blanco fg-color-negro";
        grvClientes.Rows[fila].Cells[0].CssClass = "bg-color-grisClaro03";
        grvClientes.Rows[fila].Cells[1].CssClass = "bg-color-grisClaro03";
        grvClientes.Rows[fila].Cells[2].CssClass = "bg-color-grisClaro03";
    }

    public void quitarSeleccionRadio()
    {
        foreach (
            RadioButton rb in
                from GridViewRow gv in grvClientes.Rows
                select (RadioButton)grvClientes.Rows[gv.RowIndex].FindControl("RadioButton1"))
        {
            rb.Checked = false;
            despintarFilaSeleccionada(((GridViewRow)rb.Parent.Parent).RowIndex);
        }
    }

    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            quitarSeleccionRadio();
            RadioButton rdb = sender as RadioButton;
            rdb.Checked = true;
            GridViewRow grv = (GridViewRow)rdb.Parent.Parent;
            pintarFilaSeleccionada(grv.RowIndex);

            indiceExternoSeleccionado = grv.RowIndex;

            int indexfila = 0;
            GridViewRowCollection filas = grvClientes.Rows;
            foreach (GridViewRow f in filas)
            {
                if (indiceExternoSeleccionado == indexfila) //f.RowIndex
                    clienteseleccionado = f.Cells[1].Text; //break
                indexfila = indexfila + 1;
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg",
                @"alertify.alert('Conciliaci&oacute;n bancaria','Error: "
                + ex.Message + "', function(){ alertify.error('Error en la solicitud'); });", true);
        }
    }


    protected void grvClientes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            if (grvClientes.Rows.Count > 0)
            {
                RadioButton rdb = grvClientes.Rows[0].FindControl("RadioButton1") as RadioButton;
                rdb.Checked = true;
                indiceExternoSeleccionado = 0;
                pintarFilaSeleccionada(0);
            }
        }
    }

}