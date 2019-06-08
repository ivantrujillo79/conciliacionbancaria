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
using System.IO;

public partial class ImportacionArchivos_FuenteInformacion : System.Web.UI.Page
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
            LlenarCombos();
        }
    }

    private bool ValidarDatos()
    {
        int prueba;

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
        else if (this.cboTipoFuenteInformacion.SelectedIndex == 0)
        {
            mensaje.Append(" Tipo Fuente de Informacion.");
            resultado = false;
        }
        else if (this.cboSucursal.SelectedIndex == 0)
        {
            mensaje.Append(" Sucursal.");
            resultado = false;
        }
        else if (this.cboTipoArchivo.SelectedIndex == 0)
        {
            mensaje.Append(" Tipo Archivo.");
            resultado = false;
        }
        //else if (string.IsNullOrEmpty(this.txtNumColumnas.Text))
        //{
        //    mensaje.Append(" Número de Columnas.");
        //    resultado = false;
        //}
        //else if (!int.TryParse(this.txtNumColumnas.Text, out prueba))
        //{
        //    mensaje.Append(" Número de Columnas de tipo Numérico.");
        //    resultado = false;
        //}
        else
            resultado = true;

        return resultado;
    }

    private void  PosicionarComboCorporativo()
    {
        byte corporativo = ((SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"]).Corporativo;
        cboCorporativo.SelectedIndex = buscarItem(cboCorporativo.Items, corporativo.ToString());
        cboCorporativo_SelectedIndexChanged(null, null);
    }

    private void LlenarCombos()
    {

        this.cboCorporativo.DataValueField = "Id";
        cboCorporativo.DataTextField = "Nombre";
        cboCorporativo.DataSource = App.Consultas.ObtieneListaCorporativo( ((SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"]).IdUsuario);
        cboCorporativo.DataBind();
        PosicionarComboCorporativo();


        cboTipoFuenteInformacion.DataValueField = "Id";
        cboTipoFuenteInformacion.DataTextField = "Descripcion";
        cboTipoFuenteInformacion.DataSource = App.Consultas.ObtieneListaTipoFuenteDeInformacion();
        cboTipoFuenteInformacion.DataBind();
        cboTipoFuenteInformacion.SelectedIndex = 0;

        cboTipoArchivo.DataValueField = "IdTipoArchivo";
        cboTipoArchivo.DataTextField = "Descripcion";
        cboTipoArchivo.DataSource = App.Consultas.ObtieneListaTipoArchivo();
        cboTipoArchivo.DataBind();
        cboTipoArchivo.SelectedIndex = 0;

      
       

    }

    private int buscarItem(ListItemCollection items,string valor)
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

    protected void btnGuardarDatos_Click(object sender, EventArgs e)
    {
        if (ValidarDatos())
        {
            string ruta = "~/Plantillas/ImportacionArchivos/Muestras/";
            string RUTA = base.MapPath(ruta);
            string sName = this.cboCorporativo.SelectedItem.Text.Trim().Replace(' ', '_').Replace('.', '_') + "_" + this.cboBancoFinanciero.SelectedItem.Text.Trim().Replace(' ', '_').Replace('.', '_') + "_" + this.cboCuentaFinanciero.SelectedItem.Text.Trim().Replace(' ', '_').Replace('.', '_') + "_" + this.cboTipoFuenteInformacion.SelectedItem.Text.Trim().Replace(' ', '_').Replace('.', '_') + Path.GetExtension(HttpContext.Current.Session["NombreArchivo"].ToString());
            string rutaCompleta = MapPath(ruta) + sName;
            if (HttpContext.Current.Session["Archivo"] != null)
            {
                if (SubirArchivo(rutaCompleta))
                {
                    FuenteInformacion fuenteInofrmacion = (FuenteInformacion)App.FuenteInformacion.CrearObjeto();
                    fuenteInofrmacion.IdFuenteInformacion = Convert.ToInt32(cboTipoFuenteInformacion.SelectedValue); //App.Consultas.ObtieneFuenteInformacionNumeroMaximo(Convert.ToInt32(this.cboBancoFinanciero.SelectedValue.ToString()), cboCuentaFinanciero.SelectedValue.ToString()) + 1;
                    fuenteInofrmacion.BancoFinanciero = Convert.ToInt32(this.cboBancoFinanciero.SelectedValue.ToString());
                    fuenteInofrmacion.CuentaBancoFinanciero = cboCuentaFinanciero.SelectedValue.ToString();
                    fuenteInofrmacion.RutaArchivo = sName;
                    fuenteInofrmacion.IdTipoFuenteInformacion = Convert.ToInt32(cboTipoFuenteInformacion.SelectedValue);
                    fuenteInofrmacion.IdSucursal = Convert.ToInt32(this.cboSucursal.SelectedValue.ToString());
                    fuenteInofrmacion.IdTipoArchivo = Convert.ToInt32(this.cboTipoArchivo.SelectedValue);
                    fuenteInofrmacion.NumColumnas = verificarColumnas(fuenteInofrmacion, rutaCompleta);//Convert.ToInt32(this.txtNumColumnas.Text);
                    if (fuenteInofrmacion.Guardar())
                    {
                        Limpiar();
                    }
                    else
                    {
                        if (File.Exists(rutaCompleta))
                        {
                            File.Delete(rutaCompleta);
                        }
                    }

                }
                else
                    ScriptManager.RegisterStartupScript(this.uppPrincipal,
                                                        uppPrincipal.GetType(),
                                                        Guid.NewGuid().ToString(),
                                                        "alert('Ocurrierón errores al tratar de cargar el archivo.');",
                                                        true);

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Ocurrierón errores al tratar de cargar el archivo.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.uppPrincipal,
                                    uppPrincipal.GetType(),
                                    Guid.NewGuid().ToString(),
                                    "alert('Seleccione un archivo.');",
                                    true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Seleccione un archivo.');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.uppPrincipal,
                                    uppPrincipal.GetType(),
                                    Guid.NewGuid().ToString(),
                                    "alert('" + LimpiarTexto(mensaje.ToString()) + "');",
                                    true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('" + LimpiarTexto(mensaje.ToString()) + "');", true);
        }

        HttpContext.Current.Session["Archivo"] = null;
        HttpContext.Current.Session["NombreArchivo"] = null;
    }

    private int  verificarColumnas(FuenteInformacion fuente, string ruta)
    { 
        ImportacionController controller = new ImportacionController(fuente,ruta,0);
        int columnas = controller.ObtenerColumnasEstadoCuenta().Length;
        return columnas;

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

    private bool SubirArchivo(string ruta)
    {
        bool resultado = false;
        try
        {
            if (HttpContext.Current.Session["Archivo"] != null)
            {
             
                byte[] bytes = (byte[])HttpContext.Current.Session["Archivo"];
                using (FileStream file = File.Create(ruta))
                {
                    file.Write(bytes, 0, bytes.Length);
                    file.Flush();
                    file.Close();
                }
                resultado = true;
            }
        }
        catch (Exception ex)
        {
            resultado = false;

        }
        return resultado;
    }
    
    private void Limpiar()
    {
        this.cboCorporativo.SelectedIndex = 0;
        this.cboBancoFinanciero.Items.Clear();
        this.cboBancoFinanciero.DataBind();
        this.cboCuentaFinanciero.Items.Clear();
        this.cboCuentaFinanciero.DataBind();
        this.cboTipoFuenteInformacion.SelectedIndex = 0;
        this.cboSucursal.SelectedIndex = 0;
        this.cboTipoArchivo.SelectedIndex = 0;
        this.txtDescripcionTipoArchivo.Text = string.Empty;
        //this.txtNumColumnas.Text = string.Empty;
        HttpContext.Current.Session["Archivo"] = null;
        HttpContext.Current.Session["NombreArchivo"] = null;
        this.grvFuenteInformacion.DataBind();
        PosicionarComboCorporativo();
    }

    protected void btnCancelarDatos_Click(object sender, EventArgs e)
    {
        Limpiar();
    }

    protected void cboTipoArchivo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (cboTipoArchivo.)
        //{
            TipoArchivo ta = App.Consultas.ObtieneTipoArchivoPorId(Convert.ToInt32(cboTipoArchivo.SelectedValue.ToString()));
            this.txtDescripcionTipoArchivo.Text = GenerarDescripcionTipoArchivo(ta);
        //}
    }

    private string GenerarDescripcionTipoArchivo(TipoArchivo ta)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Descripcion: \"" + ta.Descripcion + "\"");
        sb.AppendLine("Formato Fecha: \"" + ta.FormatoFecha + "\"");
        sb.AppendLine("Formato Moneda: \"" + ta.FormatoMoneda + "\"");
        sb.AppendLine("Separador: \"" + ta.Separador + "\"");
        return sb.ToString();
    }

    protected void cboBancoFinanciero_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (this.cboBancoFinanciero.Focused)
        //{
        cboCuentaFinanciero.DataValueField = "Descripcion";
        cboCuentaFinanciero.DataTextField = "Descripcion";
        cboCuentaFinanciero.DataSource = App.Consultas.ObtieneListaCuentaFinancieroPorBanco(Convert.ToInt32(this.cboCorporativo.SelectedValue), Convert.ToInt32(cboBancoFinanciero.SelectedValue));
        cboCuentaFinanciero.DataBind();

        cboCuentaFinanciero.SelectedIndex = 0;


        this.grvFuenteInformacion.DataBind();

        //}
    }

    protected void cboBancoFinanciero_DataBound(object sender, EventArgs e)
    {
        this.cboBancoFinanciero.Items.Insert(0, new ListItem(" ", "0"));
    }

    protected void cboTipoFuenteInformacion_DataBound(object sender, EventArgs e)
    {
        this.cboTipoFuenteInformacion.Items.Insert(0, new ListItem(" ", "0"));
    }

    protected void cboTipoArchivo_DataBound(object sender, EventArgs e)
    {
        this.cboTipoArchivo.Items.Insert(0, new ListItem(" ", "0"));
    }

    protected void cboSucursal_DataBound(object sender, EventArgs e)
    {
        this.cboSucursal.Items.Insert(0, new ListItem(" ", "0"));
    }

    protected void cboCorporativo_DataBound(object sender, EventArgs e)
    {
        this.cboCorporativo.Items.Insert(0, new ListItem(" ", "0"));
    }

    protected void cboCorporativo_SelectedIndexChanged(object sender, EventArgs e)
    {
       
            cboBancoFinanciero.DataValueField = "Id";
            cboBancoFinanciero.DataTextField = "Descripcion";
            cboBancoFinanciero.DataSource = App.Consultas.ObtieneListaBancoFinanciero(Convert.ToInt32(cboCorporativo.SelectedValue));
            cboBancoFinanciero.DataBind();
            cboBancoFinanciero.SelectedIndex = 0;
       
            this.cboSucursal.DataValueField = "Id";
            cboSucursal.DataTextField = "Descripcion";
            cboSucursal.DataSource = App.Consultas.ObtieneListaSucursalPorCorporativo (Convert.ToInt32(cboCorporativo.SelectedValue));
            cboSucursal.DataBind();
            int sucursal = ((SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"]).Sucursal;
            cboSucursal.SelectedIndex = buscarItem(cboSucursal.Items, sucursal.ToString());

            cboCuentaFinanciero.Items.Clear();
            cboTipoFuenteInformacion.SelectedIndex = 0;
            this.grvFuenteInformacion.DataBind();
    }
    protected void cboCuentaFinanciero_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.grvFuenteInformacion.DataSource = App.Consultas.ObtieneListaFuenteInformacionPorBancoCuenta(Convert.ToInt32(cboBancoFinanciero.SelectedValue), cboCuentaFinanciero.SelectedValue.ToString());
        this.grvFuenteInformacion.DataBind();
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton imgButton = (sender as ImageButton);
        GridViewRow row = imgButton.Parent.Parent as GridViewRow;
        Label lblCuenta = (Label)row.FindControl("colCuentaBancoFinancieroo");
        Label lblBanco = (Label)row.FindControl("colBancoFinanciero");
        Label lblFuenteInformacion = (Label)row.FindControl("colIdFuenteInformacion");


        FuenteInformacion fid = (FuenteInformacion)App.FuenteInformacion.CrearObjeto();
        fid.CuentaBancoFinanciero = lblCuenta.Text;
        fid.BancoFinanciero = Convert.ToInt32(lblBanco.Text);
        fid.IdFuenteInformacion = Convert.ToInt32(lblFuenteInformacion.Text);
        if (fid.Eliminar())
        {
            this.grvFuenteInformacion.DataSource = App.Consultas.ObtieneListaFuenteInformacionPorBancoCuenta(Convert.ToInt32(cboBancoFinanciero.SelectedValue), cboCuentaFinanciero.SelectedValue.ToString());
            this.grvFuenteInformacion.DataBind();
        }
    }

    protected void cboCuentaFinanciero_DataBound(object sender, EventArgs e)
    {
        this.cboCuentaFinanciero.Items.Insert(0, new ListItem(" ", "0"));
    }


    protected void Archivo_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        HttpContext.Current.Session["Archivo"] = null;
        HttpContext.Current.Session["NombreArchivo"] = null;
        if (uploadFile.HasFile)
        {
            try
            {
                byte[] bytes = uploadFile.FileBytes;
                HttpContext.Current.Session["NombreArchivo"] = uploadFile.FileName;
                HttpContext.Current.Session["Archivo"] = bytes;
            }
            catch (Exception ex)
            {
                string mns = ex.InnerException.ToString();
                HttpContext.Current.Session["Archivo"] = null;
                HttpContext.Current.Session["NombreArchivo"] = null;
            }
        }
    }

    protected void btnGuardarArchivo_Click(object sender, EventArgs e)
    {
        FuenteInformacion fuenteInofrmacion = (FuenteInformacion)HttpContext.Current.Session["FuneteInformacion"]; // Conciliacion.Migracion.Runtime.App.Consultas.ObtieneFuenteInformacionPorId(banco, cuenta,FI);
        int columnas = fuenteInofrmacion.NumColumnas;
        string ruta = "~/Plantillas/ImportacionArchivos/Muestras/";
        string RUTA = base.MapPath(ruta);
        string rutaCompleta = MapPath(ruta) + fuenteInofrmacion.RutaArchivo;
        if (HttpContext.Current.Session["Archivo"] != null)
        {

            if (File.Exists(rutaCompleta))
            {
                File.Delete(rutaCompleta);
            }
            rutaCompleta = Path.ChangeExtension(rutaCompleta, Path.GetExtension(HttpContext.Current.Session["NombreArchivo"].ToString()));
            if (SubirArchivo(rutaCompleta))
            {

                fuenteInofrmacion.RutaArchivo = Path.ChangeExtension(fuenteInofrmacion.RutaArchivo, Path.GetExtension(HttpContext.Current.Session["NombreArchivo"].ToString()));
                fuenteInofrmacion.IdTipoArchivo = Convert.ToInt32(this.cboPopUpTipoArchivo.SelectedValue);
                fuenteInofrmacion.NumColumnas = verificarColumnas(fuenteInofrmacion, rutaCompleta);//Convert.ToInt32(this.txtNumColumnas.Text);
                if (fuenteInofrmacion.Actualizar())
                {
                    this.popUpEtiquetas.Hide();
                    if (columnas != fuenteInofrmacion.NumColumnas)
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('El número de columnas cambio.Verifique el mapeo de este archivo.');", true);
                    this.grvFuenteInformacion.DataSource = App.Consultas.ObtieneListaFuenteInformacionPorBancoCuenta(Convert.ToInt32(cboBancoFinanciero.SelectedValue), cboCuentaFinanciero.SelectedValue.ToString());
                    this.grvFuenteInformacion.DataBind();
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Seleccione un archivo.');", true);
        }

    }
    protected void btnAdd_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton imgButton = (sender as ImageButton);
        GridViewRow row = imgButton.Parent.Parent as GridViewRow;




        string cuenta = ((Label)row.FindControl("colCuentaBancoFinancieroo")).Text;
        int banco = Convert.ToInt32(((Label)row.FindControl("colBancoFinanciero")).Text);
        int FI = Convert.ToInt32(((Label)row.FindControl("colIdFuenteInformacion")).Text);





        FuenteInformacion fuenteInofrmacion = Conciliacion.Migracion.Runtime.App.Consultas.ObtieneFuenteInformacionPorId(banco, cuenta, FI);
        HttpContext.Current.Session["FuneteInformacion"] = fuenteInofrmacion;


        cboPopUpTipoArchivo.DataValueField = "IdTipoArchivo";
        cboPopUpTipoArchivo.DataTextField = "Descripcion";
        cboPopUpTipoArchivo.DataSource = App.Consultas.ObtieneListaTipoArchivo();
        cboPopUpTipoArchivo.DataBind();
        cboPopUpTipoArchivo.SelectedIndex = PosisionarCombo(fuenteInofrmacion.IdTipoArchivo, cboPopUpTipoArchivo);


        this.popUpEtiquetas.Show();
    }

    private int PosisionarCombo(int valor, DropDownList combo)
    {
        int indice = 0;
        bool encontrado = false;
        foreach (ListItem item in combo.Items)
        {

            if (Convert.ToInt32(item.Value) == valor)
            {
                encontrado = true;
                break;
            }
            indice++;
        }
        return encontrado ? indice : 0;
    }

    protected void Archivo_UploadedComplete2(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        HttpContext.Current.Session["Archivo"] = null;
        HttpContext.Current.Session["NombreArchivo"] = null;
        if (this.AsyncFileUpload1.HasFile)
        {
            try
            {
                byte[] bytes = AsyncFileUpload1.FileBytes;
                HttpContext.Current.Session["NombreArchivo"] = AsyncFileUpload1.FileName;
                HttpContext.Current.Session["Archivo"] = bytes;
            }
            catch (Exception ex)
            {
                string mns = ex.InnerException.ToString();
                HttpContext.Current.Session["Archivo"] = null;
                HttpContext.Current.Session["NombreArchivo"] = null;
                throw (ex);
            }
        }
    }
    private bool ValidarDatosPopUp()
    {
        int prueba;

        mensaje = new StringBuilder();
        bool resultado = true;
        mensaje.Append("Se requiere  el campo ");
        if (this.cboTipoArchivo.SelectedIndex == 0)
        {
            mensaje.Append(" Tipo Archivo.");
            resultado = false;
        }

        else
            resultado = true;

        return resultado;
    }
}
