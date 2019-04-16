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
    //private int clienteSeleccionado;

    #region Propiedades

    public List<Cliente> ClientesEncontrados
    {
        get { return clientesEncontrados; }
        set { clientesEncontrados = value; }
    }

    public string ClienteSeleccionado
    {
        get
        {
            if (ViewState["clienteSeleccionado"] == null)
                return "";
            else
                return (string)ViewState["clienteSeleccionado"];
        }
        //set { ViewState["clienteSeleccionado"] = value; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "WucCDBErrorMsg", @"alertify.alert('Conciliaci&oacute;n bancaria','Error: " + ex.Message + "', function(){ alertify.error('Error en la solicitud'); });", true);
        }
    }

    public void CargarClientes()
    {
        ViewState["clienteSeleccionado"] = "";
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

            GuardarCliente(row.RowIndex);

            QuitarSeleccionRadio(rdb);

            SeleccionarRadio(rdb, row.RowIndex);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg",
                @"alertify.alert('Conciliaci&oacute;n bancaria','Error: "
                + ex.Message + "', function(){ alertify.error('Error en la solicitud'); });", true);
        }
    }

    private void GuardarCliente(int indiceFila)
    {
        string cliente = ((Label)grvClientes.Rows[indiceFila].FindControl("lblCliente")).Text;
        ViewState["clienteSeleccionado"] = cliente;
    }
    
    private void QuitarSeleccionRadio(RadioButton rbNuevo)
    {
        foreach (RadioButton rb in
                from GridViewRow row in grvClientes.Rows
                select (RadioButton)grvClientes.Rows[row.RowIndex].FindControl("rbSeleccion"))
        {
            if (rb.Checked && rb != rbNuevo)
            {
                rb.Checked = false;
                DespintarFila(((GridViewRow)rb.Parent.Parent).RowIndex);
            }
        }
    }

    private void SeleccionarRadio(RadioButton rb, int indiceFila)
    {
        rb.Checked = true;
        PintarFila(indiceFila);
    }

    private void PintarFila(int fila)
    {
        grvClientes.Rows[fila].CssClass = "bg-color-azulClaro01 fg-color-blanco";
    }

    private void DespintarFila(int fila)
    {
        grvClientes.Rows[fila].CssClass = "bg-color-blanco fg-color-negro";
    }
}