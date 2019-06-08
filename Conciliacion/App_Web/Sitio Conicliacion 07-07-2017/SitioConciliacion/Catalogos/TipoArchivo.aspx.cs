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

public partial class ImportacionArchivos_TipoArchivo : System.Web.UI.Page
{
    #region "Propiedades Globales"
    private SeguridadCB.Public.Operaciones operaciones;
    #endregion

    StringBuilder mensaje;
    Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();

    protected override void OnPreInit(EventArgs e)
    {
        if (HttpContext.Current.Session["Operaciones"] == null)
            Response.Redirect("~/Acceso/Acceso.aspx", true);
        else
            operaciones = (SeguridadCB.Public.Operaciones)HttpContext.Current.Session["Operaciones"];
    }
    protected void Page_Load(object sender, EventArgs e)
    {
      
        objApp.ImplementadorMensajes.ContenedorActual = this;
        if (!IsPostBack)
        {
            //HttpContext.Current.Session["Usuario"] = HttpContext.Current.Session["Usuario"].ToString();
            this.cboSeparador.DataValueField = "Descripcion";
            this.cboSeparador.DataTextField = "Descripcion";
            this.cboSeparador.DataSource = Conciliacion.Migracion.Runtime.App.Consultas.ObtieneListaSeparador();
            this.cboSeparador.DataBind();
            this.grvTipoArchivo.DataSource = Conciliacion.Migracion.Runtime.App.Consultas.ObtieneListaTipoArchivo();
            this.grvTipoArchivo.DataBind();

        }
    }

    protected void btnGuardarDatos_Click(object sender, EventArgs e)
    {
        if (ValidarDatos())
        {
            TipoArchivo tipoArchivo = Conciliacion.Migracion.Runtime.App.TipoArchivo;
            tipoArchivo.IdTipoArchivo = Conciliacion.Migracion.Runtime.App.Consultas.ObtieneTipoArchivoNumeroMaximo() + 1;
            tipoArchivo.Descripcion = this.txtDescripcion.Text;
            tipoArchivo.FormatoFecha = this.txtFormatoFecha.Text;
            tipoArchivo.FormatoMoneda = this.txtFormatoMoneda.Text;
            tipoArchivo.Separador = this.cboSeparador.SelectedValue.ToString();
            tipoArchivo.Usuario = ((SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"]).IdUsuario;
            tipoArchivo.Status = "ACTIVO";
            tipoArchivo.FAlta = DateTime.Now;
            if (tipoArchivo.Guardar())
            {
                this.grvTipoArchivo.DataSource = Conciliacion.Migracion.Runtime.App.Consultas.ObtieneListaTipoArchivo();
                this.grvTipoArchivo.DataBind();
                Limpiar();
            }
        }
        else
        {

            //ScriptManager.RegisterStartupScript(this.uppPrincipal,
            //                                 uppPrincipal.GetType(),
            //                                    Guid.NewGuid().ToString(),
            //                                    "alert('" + LimpiarTexto(mensaje.ToString()) + "')",
            //                                    true);

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('" + LimpiarTexto(mensaje.ToString()) + "')", true);

        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton imgButton = (sender as ImageButton);
        GridViewRow row = imgButton.Parent.Parent as GridViewRow;
        Label lblReferencia = (Label)row.FindControl("lblGVIdTipoArchivo");
        TipoArchivo tipoArchivo = Conciliacion.Migracion.Runtime.App.Consultas.ObtieneTipoArchivoPorId(Convert.ToInt32(lblReferencia.Text.Trim()));
        tipoArchivo.Status = "INACTIVO";

        if (tipoArchivo.Actualizar())
        {
            this.grvTipoArchivo.DataSource = Conciliacion.Migracion.Runtime.App.Consultas.ObtieneListaTipoArchivo();
            this.grvTipoArchivo.DataBind();
            Limpiar();
        }
    }

    private bool ValidarDatos()
    {
        mensaje = new StringBuilder();
        bool resultado = true;
        mensaje.Append("Se requiere  el campo ");
        if (string.IsNullOrEmpty(this.txtDescripcion.Text))
        {
            mensaje.Append(" Descripcion");
            resultado = false;
        }
        else if (string.IsNullOrEmpty(this.txtFormatoFecha.Text))
        {
            mensaje.Append(" Formato Fecha");
            resultado = false;
        }
        else if (string.IsNullOrEmpty(this.txtFormatoMoneda.Text))
        {
            mensaje.Append(" Formato Moneda");
            resultado = false;
        }
        else if (cboSeparador.SelectedIndex == 0)
        {
            mensaje.Append(" Separador");
            resultado = false;
        }
        else if (!ValidarFormatoFecha())
        {
            mensaje.Clear();
            mensaje.Append("Escriba un formato de fecha valido.");
            resultado = false;
        }
        else if (!ValidarFormatoMoneda())
        {
            mensaje.Clear();
            mensaje.Append("Escriba un formato de moneda valido.");
            resultado = false;
        }
        else
            resultado = true;

        return resultado;
    }

    //private bool ValidarFormatoFecha()
    //{
    //    bool resultado = false;
    //    try
    //    {
    //        string formatoFecha = this.txtFormatoFecha.Text;
    //        string fecha = DateTime.Now.ToString(formatoFecha);
    //        resultado = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        resultado = false;
    //    }
    //    return resultado;
    //}

    //private bool ValidarFormatoMoneda()
    //{
    //    bool resultado = false;
    //    try
    //    {
    //        string formatoMoneda = this.txtFormatoMoneda.Text;
    //        string moneda = double.Parse("100000.0000000").ToString(formatoMoneda);
    //        resultado = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        resultado = false;
    //    }
    //    return resultado;
    //}

    private bool ValidarFormatoFecha()
    {
        bool resultado = false;
        try
        {
            string formatoFecha = this.txtFormatoFecha.Text;
            DateTime fecha = Convert.ToDateTime(DateTime.Now.ToString(formatoFecha));
            resultado = true;
        }
        catch (Exception ex)
        {
            resultado = false;
        }
        return resultado;
    }

    private bool ValidarFormatoMoneda()
    {
        bool resultado = false;
        try
        {
            string formatoMoneda = this.txtFormatoMoneda.Text;
            string valor = double.Parse("100000.0000000").ToString(formatoMoneda);
            double moneda = double.Parse(valor, System.Globalization.NumberStyles.Number | System.Globalization.NumberStyles.AllowCurrencySymbol);
            resultado = true;
        }
        catch (Exception ex)
        {
            resultado = false;
        }
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
        this.txtDescripcion.Text = string.Empty;
        this.txtFormatoFecha.Text = string.Empty;
        this.txtFormatoMoneda.Text = string.Empty;
        this.cboSeparador.SelectedIndex = 0;
    }

    protected void btnCancelarDatos_Click(object sender, EventArgs e)
    {
        Limpiar();
    }

    protected void cboSeparador_DataBound(object sender, EventArgs e)
    {
        this.cboSeparador.Items.Insert(0, new ListItem(" ", "0"));

    }
    protected void grvTipoArchivo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvTipoArchivo.PageIndex = e.NewPageIndex;
            this.grvTipoArchivo.DataSource = Conciliacion.Migracion.Runtime.App.Consultas.ObtieneListaTipoArchivo();
            this.grvTipoArchivo.DataBind();
        }
        catch (Exception)
        {

        }
    }
    protected void grvTipoArchivo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager && (grvTipoArchivo.DataSource != null))
        {

            Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumPaginas");
            _TotalPags.Text = grvTipoArchivo.PageCount.ToString();

            DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
            for (int i = 1; i <= Convert.ToInt32(grvTipoArchivo.PageCount); i++)
            {
                list.Items.Add(i.ToString());
            }
            list.SelectedValue = (grvTipoArchivo.PageIndex + 1).ToString();
        }
    }
    protected void paginasDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag = 0;

        if (int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 && iNumPag <= grvTipoArchivo.PageCount)
            grvTipoArchivo.PageIndex = iNumPag - 1;
        else
            grvTipoArchivo.PageIndex = 0;

        this.grvTipoArchivo.DataSource = Conciliacion.Migracion.Runtime.App.Consultas.ObtieneListaTipoArchivo();
        this.grvTipoArchivo.DataBind();
    }
}
