using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CatalogoConciliacion.ReglasNegocio;
using System.Data;
using Conciliacion.RunTime;
using Conciliacion.RunTime.ReglasDeNegocio;

public partial class Catalogos_TipoConciliacionUsuario : System.Web.UI.Page
{  
    #region "Propiedades Globales"
    private SeguridadCB.Public.Operaciones operaciones;
    #endregion

    private List<ListaCombo> listEmpleados = new List<ListaCombo>();
    private List<ListaCombo> listaAsignados = new List<ListaCombo>();
    private List<ListaCombo> listaNoAsignados = new List<ListaCombo>();

    protected override void OnPreInit(EventArgs e)
    {
        if (HttpContext.Current.Session["Operaciones"] == null)
            Response.Redirect("~/Acceso/Acceso.aspx", true);
        else
            operaciones = (SeguridadCB.Public.Operaciones)HttpContext.Current.Session["Operaciones"];
    }
    protected void Page_Load(object sender, EventArgs e)
    {
      
        CatalogoConciliacion.App.ImplementadorMensajes.ContenedorActual = this;

        if (!IsPostBack)
        { 
            CargarComboEmpleados();
        }
    }

    public void CargarComboEmpleados()
    {
        listEmpleados = CatalogoConciliacion.App.Consultas.ObtieneEmpleados(1,0);
        ddlEmpleado.DataSource = listEmpleados;
        this.ddlEmpleado.DataValueField = "Campo1"; //"Identificador";
        this.ddlEmpleado.DataTextField = "Descripcion";
        this.ddlEmpleado.DataBind();
        this.ddlEmpleado.Dispose();
        txtUsuario.Text = ddlEmpleado.SelectedItem.Value;
        
        ConsultaTipoConciliacionUsuario();
        ValidarBotones();

    }


    // Consulta los grupos en la base de datos
    public void ConsultaTipoConciliacionUsuario()
    {

        listaAsignados = Conciliacion.RunTime.App.Consultas.ConsultaTipoConciliacion(Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionGrupo.Asignados, ddlEmpleado.SelectedItem.Value);
        ddlAsignados.DataSource = listaAsignados;
        ddlAsignados.DataValueField = "Identificador";
        ddlAsignados.DataTextField = "Descripcion";
        ddlAsignados.DataBind();
        ddlAsignados.Dispose();

        listaNoAsignados = Conciliacion.RunTime.App.Consultas.ConsultaTipoConciliacion(Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionGrupo.NoAsignados, ddlEmpleado.SelectedItem.Value);
        ddlNoAsignados.DataSource = listaNoAsignados;
        ddlNoAsignados.DataValueField = "Identificador";
        ddlNoAsignados.DataTextField = "Descripcion";
        ddlNoAsignados.DataBind();
        ddlNoAsignados.Dispose();
        
    }


        protected void ddlEmpleado_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtUsuario.Text = ddlEmpleado.SelectedItem.Value;
        ConsultaTipoConciliacionUsuario();
        ValidarBotones();
    }

    protected void ddlNoAsignados_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlAsignados_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnDesasignar_Click(object sender, EventArgs e)
    {

        int a;
        a = Convert.ToInt32(ddlAsignados.SelectedItem.Value);

        CatalogoConciliacion.App.TipoConciliacionUsuario.DesasignarTipo(a, ddlEmpleado.SelectedItem.Value);
        ConsultaTipoConciliacionUsuario();
        ValidarBotones();

    }
    protected void btnAsignar_Click(object sender, EventArgs e)
    {
        int a;
        a = Convert.ToInt32(ddlNoAsignados.SelectedItem.Value);

        CatalogoConciliacion.App.TipoConciliacionUsuario.AsignarTipo(a, ddlEmpleado.SelectedItem.Value);
        ConsultaTipoConciliacionUsuario();
        ValidarBotones();

    }

    private void ValidarBotones()
    {
        if (ddlNoAsignados.Items.Count <= 0) {
           btnAsignar.Enabled = false;
        }
        else {
            btnAsignar.Enabled = true;
        }

        if (ddlAsignados.Items.Count <= 0)
        {
            btnDesasignar.Enabled = false;
        }
        else
        {
            btnDesasignar.Enabled = true;
        }
        
        
   

    }


}