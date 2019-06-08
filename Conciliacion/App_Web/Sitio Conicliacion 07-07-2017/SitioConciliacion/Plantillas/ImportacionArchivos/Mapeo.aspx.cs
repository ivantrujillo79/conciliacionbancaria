using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

public partial class ImportacionArchivos_Mapeo : System.Web.UI.Page
{
    #region "Propiedades Globales"
    private SeguridadCB.Public.Operaciones operaciones;
    #endregion
    FuenteInformacion finformacion;
    ImportacionController iController;

    FuenteInformacion finformacionExistente;
    ImportacionController iControllerFuenteExistente;
    StringBuilder mensaje;

    List<FuenteInformacion> fuenteInformacion;

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
        try
        {
            if (!IsPostBack)
            {
                LlenarCombos();
            }
        }
        catch(Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    private bool ValidarDatos(int tipoOrigenFuente)
    {
        mensaje = new StringBuilder();
        bool resultado = true;
        mensaje.Append("Se requiere  el campo ");

        if (this.cboCorporativo.SelectedIndex == 0)
        {
            mensaje.Append(" Corporativo.");
            resultado = false;
        }
        else if (this.cboBancoFinanciero.SelectedIndex == 0)
        {
            mensaje.Append(" Banco.");
            resultado = false;
        }
        else if (string.IsNullOrEmpty(this.cboTipoFuenteInformacion.Text))
        {
            mensaje.Append(" Tipo Fuente.");
            resultado = false;
        }
        else if (tipoOrigenFuente != 0)
        {
            if (string.IsNullOrEmpty(this.ddlCuentaBancariaFuente.Text))
            {
                mensaje.Append(" Cuenta Bancaría Fuente.");
                resultado = false;
            }
            else resultado = true;
        }
        else if (this.cboTabla.SelectedIndex == 0)
        {
            mensaje.Append(" Tabla");
            resultado = false;
        }
        else if (this.cboColumna.SelectedIndex == 0)
        {
            mensaje.Append(" Destino.");
            resultado = false;
        }
        else if (this.cboColumnaOrigen.SelectedIndex == 0)
        {
            mensaje.Append(" Origen.");
            resultado = false;
        }
        else
            resultado = true;

        return resultado;
    }

    protected void btnAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton imgButton = (sender as ImageButton);
            GridViewRow row = imgButton.Parent.Parent as GridViewRow;
            Label lblCuenta = (Label)row.FindControl("colIdCuentaBanco");
            Label lblBanco = (Label)row.FindControl("colIdBanco");
            Label lblFuenteInformacion = (Label)row.FindControl("colIdFuenteInformacion");
            Label lblSecuencia = (Label)row.FindControl("lblGVSecuencia");
            FuenteInformacionDetalleEtiqueta etiqueta = (FuenteInformacionDetalleEtiqueta)App.FuenteInformacionDetalleEtiqueta.CrearObjeto();
            etiqueta.CuentaBancoFinanciero = lblCuenta.Text;
            etiqueta.IdBancoFinanciero = Convert.ToInt32(lblBanco.Text);
            etiqueta.IdFuenteInformacion = Convert.ToInt32(lblFuenteInformacion.Text);
            etiqueta.Secuencia = Convert.ToInt32(lblSecuencia.Text);
            HttpContext.Current.Session["Etiqueta"] = etiqueta;
            this.GridViewEtiquetas.DataSource = App.Consultas.ObtieneListaFuenteInformacionDetalleEtiqueta(etiqueta.CuentaBancoFinanciero, etiqueta.IdBancoFinanciero, etiqueta.IdFuenteInformacion, etiqueta.Secuencia); ;
            this.GridViewEtiquetas.DataBind();
            this.popUpEtiquetas.Show();
        }
        catch(Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton imgButton = (sender as ImageButton);
            GridViewRow row = imgButton.Parent.Parent as GridViewRow;
            Label lblCuenta = (Label)row.FindControl("colIdCuentaBanco");
            Label lblBanco = (Label)row.FindControl("colIdBanco");
            Label lblFuenteInformacion = (Label)row.FindControl("colIdFuenteInformacion");
            Label lblSecuencia = (Label)row.FindControl("lblGVSecuencia");

            FuenteInformacionDetalle fid = (FuenteInformacionDetalle)App.FuenteInformacionDetalle.CrearObjeto();
            fid.CuentaBancoFinanciero = lblCuenta.Text;
            fid.BancoFinanciero = Convert.ToInt32(lblBanco.Text);
            fid.IdFuenteInformacion = Convert.ToInt32(lblFuenteInformacion.Text);
            fid.Secuencia = Convert.ToInt32(lblSecuencia.Text);
            if (fid.Eliminar())
            {
                iController = (ImportacionController)HttpContext.Current.Session["IController"];
                if (iController != null)
                {
                    cboColumna.DataSource = iController.ObtenerColumnasDestino(this.cboTabla.Text.ToString());
                    cboColumna.DataBind();
                    this.grvMapeos.DataSource = iController.ObtieneCamposMapeados();
                    this.grvMapeos.DataBind();
                }
            }
        }
        catch(Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    protected void btnDeleteEtiquetae_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton imgButton = (sender as ImageButton);
            GridViewRow row = imgButton.Parent.Parent as GridViewRow;
            Label lblCuenta = (Label)row.FindControl("lblGVIdEtiqueta");
            FuenteInformacionDetalleEtiqueta etiqueta = (FuenteInformacionDetalleEtiqueta)HttpContext.Current.Session["Etiqueta"];
            etiqueta.IdEtiqueta = Convert.ToInt32(lblCuenta.Text);
            if (etiqueta.Eliminar())
            {
                this.GridViewEtiquetas.DataSource = App.Consultas.ObtieneListaFuenteInformacionDetalleEtiqueta(etiqueta.CuentaBancoFinanciero, etiqueta.IdBancoFinanciero, etiqueta.IdFuenteInformacion, etiqueta.Secuencia); ;
                this.GridViewEtiquetas.DataBind();
            }
        }
        catch(Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    private void PosicionarComboCorporativo()
    {
        byte corporativo = ((SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"]).Corporativo;
        cboCorporativo.SelectedIndex = buscarItem(cboCorporativo.Items, corporativo.ToString());
        cboCorporativo_SelectedIndexChanged(null, null);
    }

    private void LlenarCombos()
    {
        this.cboCorporativo.DataValueField = "Id";
        cboCorporativo.DataTextField = "Nombre";
        cboCorporativo.DataSource = App.Consultas.ObtieneListaCorporativo(((SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"]).IdUsuario);
        cboCorporativo.DataBind();
        PosicionarComboCorporativo();


        cboCuentaFinanciero.DataValueField = "Descripcion";
        cboCuentaFinanciero.DataTextField = "Descripcion";

        ddlCuentaBancariaFuente.DataValueField = "Descripcion";
        ddlCuentaBancariaFuente.DataTextField = "Descripcion";

        cboColumna.DataValueField = "ColumnaDestino";
        cboColumna.DataTextField = "ColumnaDestino";

        cboTabla.DataSource = App.Consultas.ObtieneListaDiferentesTablasDestino();
        cboTabla.DataBind();
        cboTabla.SelectedIndex = 0;


        this.cboEtiqueta.DataValueField = "Id";
        this.cboEtiqueta.DataTextField = "Descripcion";
        this.cboEtiqueta.DataSource = App.Consultas.ObtieneListaEtiqueta();
        this.cboEtiqueta.DataBind();
        cboEtiqueta.SelectedIndex = 0;


    }

    private int buscarItem(ListItemCollection items, string valor)
    {
        int resultado = 0;
        int index = 0;
        foreach (ListItem item in items)
        {
            if (item.Value.Trim() == valor.Trim())
            {
                resultado = index;
                break;
            }
            index++;
        }
        return resultado;
    }

    private bool verificarFecha(ImportacionController iController)
    {

        bool resultado = false;
        List<FuenteInformacionDetalle> lista = iController.ObtieneCamposMapeados();
        foreach (FuenteInformacionDetalle detalle in lista)
        {
            if (detalle.EsTipoFecha)
            {
                resultado = detalle.EsTipoFecha;
                break;
            }
        }
        return resultado;
    }

    protected void btnGuardarDatos_Click(object sender, EventArgs e)
    {
        try
        {
            if (tabNuevaConciliacion.ActiveTabIndex == 0)
            {
                if (ValidarDatos(tabNuevaConciliacion.ActiveTabIndex))
                {
                    finformacion = (FuenteInformacion)HttpContext.Current.Session["FInformacion"];
                    iController = (ImportacionController)HttpContext.Current.Session["IController"];

                    bool existeFecha = false;
                    if (chkTipoFecha.Checked)
                    {
                        existeFecha = verificarFecha(iController);
                    }
                    if (!existeFecha)
                    {
                        FuenteInformacionDetalle FID = App.FuenteInformacionDetalle;
                        FID.CuentaBancoFinanciero = cboCuentaFinanciero.SelectedValue.ToString();
                        FID.BancoFinanciero = Convert.ToInt32(cboBancoFinanciero.SelectedValue);
                        FID.IdFuenteInformacion = finformacion.IdFuenteInformacion;
                        FID.Secuencia = App.Consultas.ObtieneFuenteInformacionDetalleNumeroMaximo(finformacion.BancoFinanciero, finformacion.CuentaBancoFinanciero, finformacion.IdFuenteInformacion) + 1;
                        FID.ColumnaOrigen = cboColumnaOrigen.SelectedValue.ToString();
                        FID.IdConceptoBanco = 0;
                        FID.TablaDestino = cboTabla.SelectedValue.ToString();
                        FID.ColumnaDestino = cboColumna.SelectedValue.ToString();
                        FID.EsTipoFecha = chkTipoFecha.Checked;
                        if (FID.Guardar())
                        {
                            this.grvMapeos.DataSource = iController.ObtieneCamposMapeados();
                            this.grvMapeos.DataBind();
                            cboColumna.DataSource = iController.ObtenerColumnasDestino(this.cboTabla.SelectedValue.ToString());
                            cboColumna.DataBind();
                            cboColumnaOrigen.DataSource = iController.ObtenerCamposEstadoCuenta();
                            cboColumnaOrigen.DataBind();
                            Limpiar();
                            BloquearControles();

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Ya existe un campo de Fecha.');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('" + LimpiarTexto(mensaje.ToString()) + "');", true);
                }
            }
            else
            {
                if (ValidarDatos(tabNuevaConciliacion.ActiveTabIndex))
                {
                    iController = (ImportacionController)HttpContext.Current.Session["IController"];
                    finformacion = (FuenteInformacion)HttpContext.Current.Session["FInformacion"];

                    if (finformacion.CopiarFuenteInformacionDetalle(ddlCuentaBancariaFuente.SelectedValue))
                    {
                        this.grvMapeos.DataSource = iController.ObtieneCamposMapeados();
                        this.grvMapeos.DataBind();
                        cboColumna.DataSource = iController.ObtenerColumnasDestino(this.cboTabla.SelectedValue.ToString());
                        cboColumna.DataBind();
                        cboColumnaOrigen.DataSource = iController.ObtenerCamposEstadoCuenta();
                        cboColumnaOrigen.DataBind();
                        Limpiar();
                        BloquearControles();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('" + LimpiarTexto(mensaje.ToString()) + "');", true);
                }
            }
        }
        catch(Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
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
        if (cboColumnaOrigen.Items.Count > 0)
            this.cboColumnaOrigen.SelectedIndex = 0;
        if (this.cboColumna.Items.Count > 0)
            cboColumna.SelectedIndex = 0;

        this.chkTipoFecha.Checked = false;

    }

    private void BloquearControles()
    {
        this.cboCorporativo.Enabled = false;
        this.cboBancoFinanciero.Enabled = false;
        this.cboCuentaFinanciero.Enabled = false;
        this.cboTabla.Enabled = false;
        this.cboTipoFuenteInformacion.Enabled = false;
        this.btnCerrarMapeo.Enabled = true;
    }

    private void DesbloquearControles()
    {
        this.cboCorporativo.Enabled = true;
        this.cboBancoFinanciero.Enabled = true;
        this.cboCuentaFinanciero.Enabled = true;
        this.cboTabla.Enabled = true;
        this.cboTipoFuenteInformacion.Enabled = true;
        this.btnCerrarMapeo.Enabled = false;
    }

    private void LimpiezaTotal()
    {

        this.cboCorporativo.SelectedIndex = 0;
        this.cboBancoFinanciero.Items.Clear();
        this.cboCuentaFinanciero.Items.Clear();
        this.cboTabla.SelectedIndex = 0;
        this.cboTipoFuenteInformacion.Items.Clear();
        this.cboColumna.Items.Clear();
        this.cboColumnaOrigen.Items.Clear();
        this.grvMapeos.DataBind();
        this.chkTipoFecha.Checked = false;
        PosicionarComboCorporativo();

    }

    protected void btnCancelarDatos_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.btnCerrarMapeo.Enabled)
                Limpiar();
            else
                LimpiezaTotal();
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    protected void cboCorporativo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            cboBancoFinanciero.DataValueField = "Id";
            cboBancoFinanciero.DataTextField = "Descripcion";
            cboBancoFinanciero.DataSource = App.Consultas.ObtieneListaBancoFinanciero(Convert.ToInt32(cboCorporativo.SelectedValue));
            cboBancoFinanciero.DataBind();
            cboBancoFinanciero.SelectedIndex = 0;


            this.cboCuentaFinanciero.Items.Clear();
            this.ddlCuentaBancariaFuente.Items.Clear();
            this.cboTipoFuenteInformacion.Items.Clear();
            this.cboColumnaOrigen.Items.Clear();
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    protected void cboBancoFinanciero_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            cboCuentaFinanciero.DataSource = App.Consultas.ObtieneListaCuentaFinancieroPorBanco(Convert.ToInt32(this.cboCorporativo.SelectedValue), Convert.ToInt32(cboBancoFinanciero.SelectedValue));
            cboCuentaFinanciero.DataBind();

            LlenarComboConsecutivo();
            ConfigurarImportacion();
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg",
            //    @"alertify.alert('Conciliaci&oacute;n bancaria','Error: "
            //    + ex.Message + "', function(){ alertify.error('Error en la solicitud'); });", true);
        }
    }

    protected void cboCuentaFinanciero_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LlenarComboConsecutivo();
            ConfigurarImportacion();
        }
        catch(Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }

    }

    private void LlenarComboConsecutivo()
    {
        cboTipoFuenteInformacion.Items.Clear();
        ddlCuentaBancariaFuente.Items.Clear();
        fuenteInformacion = App.Consultas.ObtieneListaFuenteInformacionPorBancoCuenta(Convert.ToInt32(this.cboBancoFinanciero.SelectedValue), cboCuentaFinanciero.SelectedValue.ToString());
        if (fuenteInformacion.Count > 0)
        {
            cboTipoFuenteInformacion.DataValueField = "IdFuenteInformacion";
            cboTipoFuenteInformacion.DataTextField = "DesTipoFuenteInformacion";
            cboTipoFuenteInformacion.DataSource = fuenteInformacion;
            cboTipoFuenteInformacion.DataBind();
            cboTipoFuenteInformacion.SelectedIndex = 0;
        }
        else
        {
            this.grvMapeos.DataBind();
            cboTabla.SelectedIndex = 0;
            cboColumna.Items.Clear();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Esta Cuenta no tiene configurado un  archivo.');", true);
        }
    }

    private void ConfigurarImportacion()
    {
        try
        {
            HttpContext.Current.Session["FInformacion"] = null;
            HttpContext.Current.Session["IController"] = null;
            cboColumnaOrigen.Items.Clear();
            if (this.cboTipoFuenteInformacion.Items.Count > 0)
            {
                finformacion = App.Consultas.ObtieneFuenteInformacionPorId(Convert.ToInt32(this.cboBancoFinanciero.SelectedValue), cboCuentaFinanciero.SelectedValue.ToString(), Convert.ToInt32(this.cboTipoFuenteInformacion.SelectedValue));

                HttpContext.Current.Session["FInformacion"] = finformacion;
                string ruta = "~/Plantillas/ImportacionArchivos/Muestras/";
                string rutaCompleta = MapPath(ruta) + finformacion.RutaArchivo;
                iController = new ImportacionController(finformacion, rutaCompleta, 0);

                HttpContext.Current.Session["IController"] = iController;
                cboColumnaOrigen.DataSource = iController.ObtenerCamposEstadoCuenta();
                cboColumnaOrigen.DataBind();
                cboColumnaOrigen.SelectedIndex = 0;
                cboTabla.SelectedIndex = 0;
                cboColumna.Items.Clear();
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    protected void cboTabla_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            iController = (ImportacionController)HttpContext.Current.Session["IController"];
            if (iController != null)
            {
                cboColumna.DataSource = iController.ObtenerColumnasDestino(this.cboTabla.SelectedItem.Text);
                cboColumna.DataBind();
                this.grvMapeos.DataSource = iController.ObtieneCamposMapeados();
                this.grvMapeos.DataBind();
            }
        }
        catch(Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    protected void cboBancoFinanciero_DataBound(object sender, EventArgs e)
    {
        try
        {
            cboBancoFinanciero.Items.Insert(0, new ListItem(" ", "0"));
        }
        catch(Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    protected void cboTabla_DataBound(object sender, EventArgs e)
    {
        try
        {
            cboTabla.Items.Insert(0, new ListItem(" ", "0"));
        }
        catch(Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    protected void cboColumna_DataBound(object sender, EventArgs e)
    {
        try
        {
            cboColumna.Items.Insert(0, new ListItem(" ", "0"));
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    protected void cboCorporativo_DataBound(object sender, EventArgs e)
    {
        try
        {
            this.cboCorporativo.Items.Insert(0, new ListItem(" ", "0"));
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    protected void cboConsecutivo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.grvMapeos.DataBind();
            ConfigurarImportacion();
            ddlCuentaBancariaFuente.DataSource = App.Consultas.ConsultaCuentaExistenteFuenteInformacion(Convert.ToSByte(cboBancoFinanciero.SelectedItem.Value), cboCuentaFinanciero.SelectedItem.Value, Convert.ToSByte(cboTipoFuenteInformacion.SelectedItem.Value));
            ddlCuentaBancariaFuente.DataBind();
        }
        catch(Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    protected void btnCerrarMapeo_Click(object sender, EventArgs e)
    {
        try
        {
            finformacion = (FuenteInformacion)HttpContext.Current.Session["FInformacion"];
            iController = (ImportacionController)HttpContext.Current.Session["IController"];
            if (verificarFecha(iController))
            {
                DesbloquearControles();
                LimpiezaTotal();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('No existe ningún campo de tipo fecha, configure uno.');", true);
            }
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    protected void cboColumnaOrigen_DataBound(object sender, EventArgs e)
    {
        try
        {
            cboColumnaOrigen.Items.Insert(0, new ListItem(" ", "0"));
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    protected void chkLongitud_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            this.lblLongitud.Visible = !chkLongitud.Checked;
            this.txtLongitud.Visible = !chkLongitud.Checked;
            this.lblFinaliza.Visible = chkLongitud.Checked;
            this.txtFinaliza.Visible = chkLongitud.Checked;

            this.txtLongitud.Text = string.Empty;
            this.txtFinaliza.Text = string.Empty;
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    private void LimpiarControlesEtiquetas()
    {
        this.cboEtiqueta.SelectedIndex = 0;
        this.txtLongitud.Text = string.Empty;
        this.txtFinaliza.Text = string.Empty;
        this.chkLongitud.Checked = false;
        this.lblLongitud.Visible = !chkLongitud.Checked;
        this.txtLongitud.Visible = !chkLongitud.Checked;
        this.lblFinaliza.Visible = chkLongitud.Checked;
        this.txtFinaliza.Visible = chkLongitud.Checked;

        txtLongitud.Text = string.Empty;
        this.txtFinaliza.Text = string.Empty;

    }

    protected void cboEtiqueta_DataBound(object sender, EventArgs e)
    {
        try
        {
            cboEtiqueta.Items.Insert(0, new ListItem(" ", "0"));
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    private bool ValidarDatosEtiquetas()
    {
        int res;
        mensaje = new StringBuilder();
        bool resultado = true;
        mensaje.Append("Se requiere  el campo ");

        if (this.cboEtiqueta.SelectedIndex == 0)
        {
            mensaje.Append(" Etiqueta.");
            resultado = false;
        }
        else if ((!chkLongitud.Checked) && (string.IsNullOrEmpty(this.txtLongitud.Text)))
        {
            mensaje.Append(" Longitud Fija.");
            resultado = false;
        }
        else if ((!chkLongitud.Checked) && (!int.TryParse(this.txtLongitud.Text, out res)))
        {
            mensaje.Append(" Longitud Fija de tipo numérico.");
            resultado = false;
        }
        else if ((chkLongitud.Checked) && (string.IsNullOrEmpty(this.txtFinaliza.Text)))
        {
            mensaje.Append(" Finaliza.");
            resultado = false;
        }
        else
            resultado = true;

        return resultado;
        
    }

    protected void btnGuardarEtiqueta_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidarDatosEtiquetas())
            {
                FuenteInformacionDetalleEtiqueta etiqueta = (FuenteInformacionDetalleEtiqueta)HttpContext.Current.Session["Etiqueta"];
                etiqueta.IdEtiqueta = Convert.ToInt32(this.cboEtiqueta.SelectedValue);
                etiqueta.LongitudFija = !this.chkLongitud.Checked ? Convert.ToInt32(this.txtLongitud.Text) : 0;
                etiqueta.Finaliza = this.chkLongitud.Checked ? this.txtFinaliza.Text : null;
                if (etiqueta.Guardar())
                {
                    this.GridViewEtiquetas.DataSource = App.Consultas.ObtieneListaFuenteInformacionDetalleEtiqueta(etiqueta.CuentaBancoFinanciero, etiqueta.IdBancoFinanciero, etiqueta.IdFuenteInformacion, etiqueta.Secuencia); ;
                    this.GridViewEtiquetas.DataBind();
                    LimpiarControlesEtiquetas();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('" + LimpiarTexto(mensaje.ToString()) + "');", true);
            }
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    protected void btnCancelarCargaPedidos_Click(object sender, EventArgs e)
    {
        try
        {
            LimpiarControlesEtiquetas();
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    protected void grvMapeos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvMapeos.PageIndex = e.NewPageIndex;
            iController = (ImportacionController)HttpContext.Current.Session["IController"];
            this.grvMapeos.DataSource = iController.ObtieneCamposMapeados();
            this.grvMapeos.DataBind();
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    protected void grvMapeos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager && (grvMapeos.DataSource != null))
            {

                Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumPaginas");
                _TotalPags.Text = grvMapeos.PageCount.ToString();

                DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
                for (int i = 1; i <= Convert.ToInt32(grvMapeos.PageCount); i++)
                {
                    list.Items.Add(i.ToString());
                }
                list.SelectedValue = (grvMapeos.PageIndex + 1).ToString();
            }
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    protected void paginasDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList oIraPag = (DropDownList)sender;
            int iNumPag;
            grvMapeos.PageIndex = int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 &&
                                        iNumPag <= grvMapeos.PageCount
                                            ? iNumPag - 1
                                            : 0;
            iController = (ImportacionController)HttpContext.Current.Session["IController"];
            this.grvMapeos.DataSource = iController.ObtieneCamposMapeados();
            this.grvMapeos.DataBind();
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    protected void cboTipoFuenteInformacion_DataBound(object sender, EventArgs e)
    {
        try
        {
            ddlCuentaBancariaFuente.DataSource = App.Consultas.ConsultaCuentaExistenteFuenteInformacion(Convert.ToSByte(cboBancoFinanciero.SelectedValue), cboCuentaFinanciero.SelectedItem.Value, Convert.ToSByte(cboTipoFuenteInformacion.SelectedValue));
            ddlCuentaBancariaFuente.DataBind();
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }
}