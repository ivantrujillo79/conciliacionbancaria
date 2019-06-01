using AjaxControlToolkit;
using Conciliacion.RunTime.ReglasDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ControlesUsuario_ClientePago_wucBuscadorPagoEstadoCuenta : System.Web.UI.UserControl
{
    private int corporativo;
    private int sucursal;
    private int año;
    private int mes;
    private int folio;
    //public ListaPago;
    private bool activaEstaConciliacion;

    public int Corporativo
    {
        get { return corporativo; }
        set { corporativo = value; }
    }
    public int Sucursal
    {
        get { return sucursal; }
        set { sucursal = value; }
    }
    public int Año
    {
        get { return año; }
        set { año = value; }
    }
    public int Mes
    {
        get { return mes; }
        set { mes = value; }
    }
    public int Folio
    {
        get { return folio; }
        set { folio = value; }
    }

    public object Contenedor { get; set; }

    public bool ActivaEstaConciliacion
    {
        get { return activaEstaConciliacion; }
        set { activaEstaConciliacion = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarGrid();
            txtFinicio.Text = (DateTime.Today.AddMonths(-1)).ToShortDateString();
            txtFfinal.Text = DateTime.Today.ToShortDateString();
            grvPagoEstadoCuenta.DataSource = null;
            chkBuscaEnRetiros.Checked = false;
            chkBuscarEnDepositos.Checked = false;
            chkBuscarEnEsta.Checked = false;
        }
        chkBuscarEnEsta.Enabled = true; // ActivaEstaConciliacion;
    }

    public void CargarGrid()
    {

    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        ModalPopupExtender objContenedor;
        objContenedor = (ModalPopupExtender)this.Contenedor;
        objContenedor.Hide();
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        if (txtMonto.Text.Trim() == "")
            txtMonto.Text = "0";
        List<EstadoDeCuenta> listaEstadoCuenta;
        if (activaEstaConciliacion == true)
            listaEstadoCuenta = Conciliacion.RunTime.App.Consultas.BuscarPagoEstadoCuenta(DateTime.Parse(txtFinicio.Text), DateTime.Parse(txtFfinal.Text),
                decimal.Parse(txtMonto.Text),chkBuscaEnRetiros.Checked,chkBuscarEnDepositos.Checked,
                corporativo,
                sucursal,
                año,
                mes,
                folio);
        else
            listaEstadoCuenta = Conciliacion.RunTime.App.Consultas.BuscarPagoEstadoCuenta(DateTime.Parse(txtFinicio.Text), DateTime.Parse(txtFfinal.Text),
                decimal.Parse(txtMonto.Text), chkBuscaEnRetiros.Checked, chkBuscarEnDepositos.Checked,
                0,
                0,
                0,
                0,
                0);

        grvPagoEstadoCuenta.DataSource = listaEstadoCuenta;
        if (listaEstadoCuenta.Count > 0)
            grvPagoEstadoCuenta.DataBind();
        else
        {
            ModalPopupExtender objContenedor;
            objContenedor = (ModalPopupExtender)this.Contenedor;
            objContenedor.Hide();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Mensaje",
                    @"alertify.alert('Conciliaci&oacute;n bancaria','No se encontr&oacute; registro que coincida con los par&aacute;metros de b&uacute;squeda proporcionados.', function(){ });", true);

        }

    }
}
