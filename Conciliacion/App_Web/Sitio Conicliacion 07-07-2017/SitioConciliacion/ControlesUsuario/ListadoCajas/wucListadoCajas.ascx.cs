using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Conciliacion.RunTime;
using Conciliacion.RunTime.ReglasDeNegocio;

public partial class ControlesUsuario_ListadoCajas_wucListadoCajas : System.Web.UI.UserControl
{
    private List<Caja> cajas;
    private List<Caja> cajasSeleccionadas;

    #region Propiedades

    public List<Caja> Cajas
    {
        //get { return cajas; }
        //set { cajas = value; }
        get
        {
            if (ViewState["listaCajas"] == null)
                return null;
            else
                return (List<Caja>)ViewState["listaCajas"];
        }
        set { ViewState["listaCajas"] = value; }
    }

    public List<Caja> CajasSeleccionadas
    {
        //get { return cajasSeleccionadas; }
        //set { cajasSeleccionadas = value; }
        get
        {
            if (ViewState["cajasSeleccionadas"] == null)
                return null;
            else
                return (List<Caja>)ViewState["cajasSeleccionadas"];
        }
        set { ViewState["cajasSeleccionadas"] = value; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                CargarRepetidor();
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", 
                "alertify.alert('Conciliaci&oacute;n bancaria','Error: " + ex.Message + 
                "', function(){ alertify.error('Error en la solicitud'); });", true);
        }
    }// end Page_Load

    private void CargarRepetidor()
    {
        try
        {
            repCajas.DataSource = Cajas;
            repCajas.DataBind();
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
    
    private void ActualizarCajasSeleccionadas()
    {
        try
        {
            List<int> lstIDs = new List<int>();
            CheckBox chkElemento = new CheckBox();
            int id = 0;
            
            // Limpiar lista antes de agregar instancias
            CajasSeleccionadas = new List<Caja>();

            foreach (RepeaterItem riElemento in repCajas.Items)
            {
                chkElemento = (CheckBox)riElemento.FindControl("chkElementoCaja");

                if (chkElemento.Checked)
                {
                    id = Int32.Parse(((HiddenField)riElemento.FindControl("hdfIndiceCaja")).Value.ToString());
                    lstIDs.Add(id);
                }
            }

            foreach (int idx in lstIDs)
            {
                CajasSeleccionadas.Add(Cajas.First(caja => caja.ID == idx));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }// end ActualizarCajasSeleccionadas()

    private void SeleccionarTodos(bool seleccionados)
    {
        try
        {
            if (seleccionados)
            {
                foreach(RepeaterItem riElemento in repCajas.Items)
                {
                    ((CheckBox)riElemento.FindControl("chkElementoCaja")).Checked = true;
                }

                CajasSeleccionadas = Cajas;
            }
            else
            {
                foreach (RepeaterItem riElemento in repCajas.Items)
                {
                    ((CheckBox)riElemento.FindControl("chkElementoCaja")).Checked = false;
                }

                CajasSeleccionadas.Clear();
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }// end SeleccionarTodos()

    protected void chkElementoCaja_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            ActualizarCajasSeleccionadas();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", 
                @"alertify.alert('Conciliaci&oacute;n bancaria','Error: " + ex.Message + "', function(){ alertify.error('Error en la solicitud'); });", true);
        }
    }

    protected void chkTodos_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkTodos;
        try
        {
            chkTodos = sender as CheckBox;

            if (chkTodos.Checked)
            {
                SeleccionarTodos(true);
            }
            else
            {
                SeleccionarTodos(false);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg",
                @"alertify.alert('Conciliaci&oacute;n bancaria','Error: " + ex.Message + "', function(){ alertify.error('Error en la solicitud'); });", true);
        }
    }

}