using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Conciliacion.RunTime;
using Conciliacion.RunTime.ReglasDeNegocio;

public partial class ControlesUsuario_wuCuentasBancarias_WUCListadoCuentasBancarias : System.Web.UI.UserControl
{
    private List<Cuenta> cuentas;
    private List<Cuenta> cuentasSeleccionadas;

    #region Propiedades

    public List<Cuenta> Cuentas
    {
        
        get
        {
            if (ViewState["listaCuentas"] == null)
                return null;
            else
                return (List<Cuenta>)ViewState["listaCuentas"];
        }
        set { ViewState["listaCuentas"] = value; }
    }

    public List<Cuenta> CuentasSeleccionadas
    {
       
        get
        {
            if (ViewState["cuentasSeleccionadas"] == null)
                return null;
            else
                return (List<Cuenta>)ViewState["cuentasSeleccionadas"];
        }
        set { ViewState["cuentasSeleccionadas"] = value; }
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
        catch (Exception ex)
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
            repCuentas.DataSource = Cuentas;
            repCuentas.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ActualizarCuentasSeleccionadas()
    {
        try
        {
            List<int> lstIDs = new List<int>();
            CheckBox chkElemento = new CheckBox();
            int id = 0;

            // Limpiar lista antes de agregar instancias
            CuentasSeleccionadas = new List<Cuenta>();

            foreach (RepeaterItem riElemento in repCuentas.Items)
            {
                chkElemento = (CheckBox)riElemento.FindControl("chkElementoCuenta");

                if (chkElemento.Checked)
                {
                    id = Int32.Parse(((HiddenField)riElemento.FindControl("hdfIndiceCuentaa")).Value.ToString());
                    lstIDs.Add(id);
                }
            }

            foreach (int idx in lstIDs)
            {
                CuentasSeleccionadas.Add(Cuentas.First(cuenta => cuenta.ID == idx));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SeleccionarTodos(bool seleccionados)
    {
        try
        {
            if (seleccionados)
            {
                foreach (RepeaterItem riElemento in repCuentas.Items)
                {
                    ((CheckBox)riElemento.FindControl("chkElementoCuenta")).Checked = true;
                }

                CuentasSeleccionadas = Cuentas;
            }
            else
            {
                foreach (RepeaterItem riElemento in repCuentas.Items)
                {
                    ((CheckBox)riElemento.FindControl("chkElementoCuenta")).Checked = false;
                }

                CuentasSeleccionadas.Clear();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }// end SeleccionarTodos()

    protected void chkElementoCuenta_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            ActualizarCuentasSeleccionadas();
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