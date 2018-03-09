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
    private List<Caja> listaCajas;
    private List<Caja> listaCajasSeleccionadas;

    #region Propiedades

    public List<Caja> ListaCajas
    {
        get { return listaCajas; }
        set { listaCajas = value; }
        //get
        //{
        //    if (ViewState["listaCajas"] == null)
        //        return null;
        //    else
        //        return (List<Caja>)ViewState["listaCajas"];
        //}
        //set { ViewState["listaCajas"] = value; }
    }

    public List<Caja> ListaCajasSeleccionadas
    {
        get { return listaCajasSeleccionadas; }
        set { listaCajasSeleccionadas = value; }
        //get
        //{
        //    if (ViewState["listaCajasSeleccionadas"] == null)
        //        return null;
        //    else
        //        return (List<Caja>)ViewState["listaCajasSeleccionadas"];
        //}
        //set { ViewState["listaCajasSeleccionadas"] = value; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                CargarListaCajas();
            }
            else if (Page.IsPostBack)
            {
                //ActualizarListaCajas();
                //ActualizarSeleccionadosRepetidor();
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", 
                "alertify.alert('Conciliaci&oacute;n bancaria','Error: " + ex.Message + 
                "', function(){ alertify.error('Error en la solicitud'); });", true);
        }
    }

    private void CargarListaCajas()
    {
        try
        {
            HttpContext.Current.Session["LISTA_CAJAS"] = ListaCajas;
            repCajas.DataSource = ListaCajas;
            repCajas.DataBind();
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    private void ActualizarListaCajas()
    {
        try
        {
            ListaCajas = HttpContext.Current.Session["LISTA_CAJAS"] as List<Caja>;
            repCajas.DataSource = ListaCajas;
            repCajas.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ActualizarCajasSeleccionadas()
    {
        List<int> lstIndices = new List<int>();
        CheckBox chkElemento = new CheckBox();
        int id = 0;
        StringBuilder sbMensaje = new StringBuilder();

        foreach (RepeaterItem riElemento in repCajas.Items)
        {
            chkElemento = (CheckBox)riElemento.FindControl("chkElementoCaja");

            if (chkElemento.Checked)
            {
                id = Int32.Parse(((HiddenField)riElemento.FindControl("hdfIndiceCaja")).Value.ToString());
                ListaCajasSeleccionadas.Add(ListaCajas.First(x => x.ID == id));
            }
            HttpContext.Current.Session["LISTA_CAJAS_SELECCIONADAS"] = ListaCajasSeleccionadas;
        }

        foreach (Caja cCaja in ListaCajasSeleccionadas)
        {
            sbMensaje.Append("Caja: " + cCaja.Descripcion + "\n");
        }

        App.ImplementadorMensajes.MostrarMensaje(sbMensaje.ToString());
    }

    private void ActualizarSeleccionadosRepetidor()
    {
        CheckBox chkElemento = new CheckBox();
        int id = 0;
        try
        {
            ListaCajasSeleccionadas = HttpContext.Current.Session["LISTA_CAJAS_SELECCIONADAS"] as List<Caja>;

            foreach (RepeaterItem riElemento in repCajas.Items)
            {
                id = Int32.Parse(((HiddenField)riElemento.FindControl("hdfIndiceCaja")).Value.ToString());

                if (ListaCajasSeleccionadas.Exists(caja => caja.ID == id))
                {
                    chkElemento = (CheckBox)riElemento.FindControl("chkElementoCaja");
                    chkElemento.Checked = true;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void chkElementoCaja_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            ActualizarCajasSeleccionadas();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}