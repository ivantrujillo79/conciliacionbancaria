using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;
using AjaxControlToolkit;
using Conciliacion.Migracion.Runtime;
using Conciliacion.Migracion.Runtime.ReglasNegocio;
using System.Text;

public partial class ImportacionArchivos_Etiqueta : System.Web.UI.Page
{
    #region "Propiedades Globales"
    private SeguridadCB.Public.Operaciones operaciones;
    private SeguridadCB.Public.Usuario usuario;
    #endregion

    StringBuilder mensaje;
    protected override void OnPreInit(EventArgs e)
    {
        if (HttpContext.Current.Session["Operaciones"] == null)
            Response.Redirect("~/Acceso/Acceso.aspx", true);
        else
            operaciones = (SeguridadCB.Public.Operaciones)HttpContext.Current.Session["Operaciones"];
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        App.ImplementadorMensajes.ContenedorActual = this;
        if (!IsPostBack)
        {
            LlenarCombos();
            this.grvEtiquetas.DataSource = App.Consultas.ObtieneListaEtiqueta();
            this.grvEtiquetas.DataBind();

        }
    }

    private void LlenarCombos()
    {

        this.cboBancoFinanciero.DataValueField = "Id";
        this.cboBancoFinanciero.DataTextField = "Descripcion";
        this.cboBancoFinanciero.DataSource = App.Consultas.ObtieneListaBancoFinanciero(((SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"]).Corporativo);
        this.cboBancoFinanciero.DataBind();
        this.cboBancoFinanciero.SelectedIndex = 0;



        this.cboTipoFuenteInformacion.DataValueField = "Id";
        this.cboTipoFuenteInformacion.DataTextField = "Descripcion";
        this.cboTipoFuenteInformacion.DataSource = App.Consultas.ObtieneListaTipoFuenteDeInformacion();
        this.cboTipoFuenteInformacion.DataBind();
        this.cboTipoFuenteInformacion.SelectedIndex = 0;

        this.cboTipoDato.DataValueField = "Descripcion";
        this.cboTipoDato.DataTextField = "Descripcion";
        this.cboTipoDato.DataSource = App.Consultas.ObtieneListaTipoDato();
        this.cboTipoDato.DataBind();
        this.cboTipoDato.SelectedIndex = 0;


        this.cboTabla.DataSource = App.Consultas.ObtieneListaDiferentesTablasDestino();
        this.cboTabla.DataBind();
        this.cboTabla.SelectedIndex = 0;



    }

    protected void cboTabla_SelectedIndexChanged(object sender, EventArgs e)
    {
        cboColumna.DataValueField = "ColumnaDestino";
        cboColumna.DataTextField = "ColumnaDestino";
        cboColumna.DataSource = App.Consultas.ObtieneDestinoPorTabla(this.cboTabla.SelectedValue.ToString());
        cboColumna.DataBind();
        cboColumna.SelectedIndex = 0;
    }


    protected void cboTabla_DataBound(object sender, EventArgs e)
    {
        cboTabla.Items.Insert(0, new ListItem(" ", "0"));
    }

    protected void cboColumna_DataBound(object sender, EventArgs e)
    {
        cboColumna.Items.Insert(0, new ListItem(" ", "0"));
    }

    protected void btnGuardarDatos_Click(object sender, EventArgs e)
    {
        if (ValidarDatos())
        {
            Etiqueta etiqueta = (Etiqueta)App.Etiqueta.CrearObjeto();
            etiqueta.Id = App.Consultas.ObtieneNumeroMaximoEtiqueta() + 1;
            etiqueta.IdBancoFinanciero = Convert.ToInt32(this.cboBancoFinanciero.SelectedValue);
            etiqueta.IdTipoFuenteInformacion = Convert.ToInt32(this.cboTipoFuenteInformacion.SelectedValue);
            etiqueta.Descripcion = this.txtDescripcion.Text;
            etiqueta.Status = "ACTIVO";
            etiqueta.TipoDato = this.cboTipoDato.SelectedValue.ToString();
            etiqueta.FAlta = DateTime.Now;
            etiqueta.UsuarioAlta = ((SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"]).IdUsuario;
            etiqueta.FBaja = null;
            etiqueta.UsuarioBaja = null;
            etiqueta.TablaDestino = cboTabla.SelectedValue.ToString();
            etiqueta.ColumnaDestino = cboColumna.SelectedValue.ToString();
            etiqueta.ConcatenaEtiqueta = chkConcatenar.Checked;
            if (etiqueta.Guardar())
            {
                this.grvEtiquetas.DataSource = App.Consultas.ObtieneListaEtiqueta();
                this.grvEtiquetas.DataBind();
                Limpiar();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('" + LimpiarTexto(mensaje.ToString()) + "')", true);

        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton imgButton = (sender as ImageButton);
        GridViewRow row = imgButton.Parent.Parent as GridViewRow;
        Label lblReferencia = (Label)row.FindControl("lblGVIdEtiqueta");
        Etiqueta etiqueta = App.Consultas.ObtieneEtiquetaPorId(Convert.ToInt32(lblReferencia.Text.Trim()));
        etiqueta.Status = "INACTIVO";
        if (etiqueta.Actualizar())
        {
            this.grvEtiquetas.DataSource = App.Consultas.ObtieneListaEtiqueta();
            this.grvEtiquetas.DataBind();
            Limpiar();
        }
    }

    private bool ValidarDatos()
    {
        mensaje = new StringBuilder();
        bool resultado = true;
        mensaje.Append("Se requiere  el campo ");
        if (cboBancoFinanciero.SelectedIndex == 0)
        {
            mensaje.Append(" Banco.");
            resultado = false;
        }
        else if (this.cboTipoFuenteInformacion.SelectedIndex == 0)
        {
            mensaje.Append(" Tipo Fuente Información.");
            resultado = false;
        }
        else if (string.IsNullOrEmpty(this.txtDescripcion.Text))
        {
            mensaje.Append(" Descripcion.");
            resultado = false;
        }
        else if (this.cboTipoDato.SelectedIndex == 0)
        {
            mensaje.Append(" Tipo Dato.");
            resultado = false;
        }
        else if (this.cboTabla.SelectedIndex == 0)
        {
            mensaje.Append(" Tabla.");
            resultado = false;
        }
        else if (this.cboColumna.SelectedIndex == 0)
        {
            mensaje.Append(" Columna.");
            resultado = false;
        }
        else
            resultado = true;

        return resultado;
    }

    private string LimpiarTexto(string texto)
    {
        texto = texto.Replace("\b", "\\b");    //Retroceso [Backspace] 
        texto = texto.Replace("\f", "\\f");    //[Form feed] 
        texto = texto.Replace("\n", "\\n");    //Nueva línea 
        texto = texto.Replace("\r", "\\r");    //Retorno de carro [Carriage return] 
        texto = texto.Replace("\t", "\\t");    //Tabulador [Tab] 
        texto = texto.Replace("\v", "\\v");    //Tabulador vertical 
        texto = texto.Replace("\'", "\\'");    //Apóstrofe o comilla simple 
        return texto;
    }

    private void Limpiar()
    {
        this.cboBancoFinanciero.SelectedIndex = 0;
        this.cboTipoFuenteInformacion.SelectedIndex = 0;
        this.cboTipoDato.SelectedIndex = 0;
        this.txtDescripcion.Text = string.Empty;
        this.cboTabla.SelectedIndex = 0;
        this.cboColumna.SelectedIndex = 0;
        this.chkConcatenar.Checked = false;
    }

    protected void btnCancelarDatos_Click(object sender, EventArgs e)
    {
        Limpiar();
    }


    protected void cboBancoFinanciero_DataBound(object sender, EventArgs e)
    {
        this.cboBancoFinanciero.Items.Insert(0, new ListItem(" ", "0"));
    }
    protected void cboTipoFuenteInformacion_DataBound(object sender, EventArgs e)
    {
        this.cboTipoFuenteInformacion.Items.Insert(0, new ListItem(" ", "0"));
    }
    protected void cboTipoDato_DataBound(object sender, EventArgs e)
    {
        this.cboTipoDato.Items.Insert(0, new ListItem(" ", "0"));
    }

    protected void grvEtiquetas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvEtiquetas.PageIndex = e.NewPageIndex;
            this.grvEtiquetas.DataSource = App.Consultas.ObtieneListaEtiqueta();
            this.grvEtiquetas.DataBind();
        }
        catch (Exception)
        {

        }
    }
    protected void grvEtiquetas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager && (grvEtiquetas.DataSource != null))
        {

            Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumPaginas");
            _TotalPags.Text = grvEtiquetas.PageCount.ToString();

            DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
            for (int i = 1; i <= Convert.ToInt32(grvEtiquetas.PageCount); i++)
            {
                list.Items.Add(i.ToString());
            }
            list.SelectedValue = (grvEtiquetas.PageIndex + 1).ToString();
        }
    }
    protected void paginasDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag;
        grvEtiquetas.PageIndex = int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 &&
                                    iNumPag <= grvEtiquetas.PageCount
                                        ? iNumPag - 1
                                        : 0;
        this.grvEtiquetas.DataSource = App.Consultas.ObtieneListaEtiqueta();
        this.grvEtiquetas.DataBind();
    }
}
