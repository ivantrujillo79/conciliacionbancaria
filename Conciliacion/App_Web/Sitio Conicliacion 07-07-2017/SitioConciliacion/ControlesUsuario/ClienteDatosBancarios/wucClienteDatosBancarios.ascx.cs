using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Conciliacion.RunTime.ReglasDeNegocio;

public partial class ControlesUsuario_ClienteDatosBancarios_wucClienteDatosBancarios : System.Web.UI.UserControl
{
    private List<Cliente> clientesEncontrados;
    private int clienteSeleccionado;

    #region Propiedades

    public List<Cliente> ClientesEncontrados
    {
        get { return clientesEncontrados; }
        set { clientesEncontrados = value; }
    }

    public int ClienteElegido
    {
        get { return clienteSeleccionado; }
        //set { clienteSeleccionado = value; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //if (!Page.IsPostBack)
            //{
            //    CargarClientes();
            //}
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "WucCDBErrorMsg", @"alertify.alert('Conciliaci&oacute;n bancaria','Error: " + ex.Message + "', function(){ alertify.error('Error en la solicitud'); });", true);
        }
    }

    public void CargarClientes()
    {
        if (clientesEncontrados != null && clientesEncontrados.Count > 0)
        {
            grvClientes.DataSource = clientesEncontrados;
            grvClientes.DataBind();
        }
    }

    protected void rbSeleccion_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            RadioButton rdb = sender as RadioButton;
            GridViewRow row = (GridViewRow)rdb.Parent.Parent;

            rdb.Checked = true;

            quitarSeleccionRadio();

            pintarFila(row.RowIndex);

            //indiceGridSeleccionado = grv.RowIndex;

            //int indexfila = 0;
            //GridViewRowCollection filas = grvClientes.Rows;
            //foreach (GridViewRow f in filas)
            //{
            //    if (indiceGridSeleccionado == indexfila)
            //    {//f.RowIndex
            //        hdfClienteSeleccionado.Value = f.Cells[1].Text;
            //        break;
            //    }
            //    indexfila = indexfila + 1;
            //}

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg",
                @"alertify.alert('Conciliaci&oacute;n bancaria','Error: "
                + ex.Message + "', function(){ alertify.error('Error en la solicitud'); });", true);
        }
    }
    
    public void quitarSeleccionRadio()
    {
        foreach (RadioButton rb in
                from GridViewRow row in grvClientes.Rows
                select (RadioButton)grvClientes.Rows[row.RowIndex].FindControl("rbSeleccion"))
        {
            if (rb.Checked)
            {
                rb.Checked = false;
                //despintarFila(((GridViewRow)rb.Parent.Parent).RowIndex);
            }
        }
    }

    public void pintarFila(int fila)
    {
        grvClientes.Rows[fila].CssClass = "bg-color-azulClaro01 fg-color-blanco";
        //grvClientes.Rows[fila].Cells[0].CssClass = "bg-color-azulClaro01";
        //grvClientes.Rows[fila].Cells[1].CssClass = "bg-color-azulClaro01";
        //grvClientes.Rows[fila].Cells[2].CssClass = "bg-color-azulClaro01";
    }
}