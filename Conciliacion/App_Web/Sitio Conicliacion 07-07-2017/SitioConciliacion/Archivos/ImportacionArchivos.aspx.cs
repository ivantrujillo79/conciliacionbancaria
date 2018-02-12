using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Collections.Generic;
using Conciliacion.Migracion.Runtime;
using Conciliacion.Migracion.Runtime.ReglasNegocio;
using System.Text;
using System.IO;
using Conciliacion.RunTime.ReglasDeNegocio;
using System.Globalization;

public partial class ImportacionArchivos_ImportacionArchivos : System.Web.UI.Page
{
    private SeguridadCB.Public.Operaciones operaciones;
    private SeguridadCB.Public.Usuario usuario;

    FuenteInformacion finformacion;
    ImportacionController iController;
    public static List<Conciliacion.RunTime.ReglasDeNegocio.ImportacionAplicacion> listImportacionAplicacion = new List<Conciliacion.RunTime.ReglasDeNegocio.ImportacionAplicacion>();
    public static List<ListaCombo> listFoliosExternoInternos = new List<ListaCombo>();
    public static List<DatosArchivoDetalle> listaDestinoDetalle = new List<DatosArchivoDetalle>();
    private DataTable tblDestino = new DataTable("VistaFoliosExternoInterno");
    private DataTable tblDestinoDetalle = new DataTable("VistaDestinoDetalle");
    public static decimal totalDeposito, totalRetiro;
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
            Server.ScriptTimeout = 500;
            Session.Timeout = 500;
            LlenarCombos();
            //this.btnGuardar.Attributes.Add("onclick", "return ValidaFechas();");
            ActualizarSubirDesde();
            Carga_Extractores();

        }
    }
    StringBuilder mensaje;
    /// <summary>
    /// Lee mes actual
    /// </summary>
    public string leerMes()
    {
        DateTime fechaActual = Convert.ToDateTime(fechaMaximaConciliacion());
        return fechaActual.Month.ToString();
    }
    /// <summary>
    /// Obtiene Fecha Maxima Conciliacion
    /// </summary>
    public string fechaMaximaConciliacion()
    {
        return Conciliacion.RunTime.App.Consultas.ConsultaFechaActualInicial();
    }
    private bool ValidarDatos()
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
        else if (string.IsNullOrEmpty(this.cboTipoFuenteInformacion.Text) && rdbSubirDesde.SelectedItem.Value.Equals("Archivo"))
        {
            mensaje.Append(" Tipo Fuente.");
            resultado = false;
        }
        else if (this.cboSucursal.SelectedIndex == 0)
        {
            mensaje.Append(" Sucursal.");
            resultado = false;
        }
        else if (this.cboAnio.SelectedIndex == 0)
        {
            mensaje.Append(" A�o.");
            resultado = false;
        }
        //if (string.IsNullOrEmpty(this.dpFInicial.Text))
        //{
        //    mensaje.Append(" Fecha inicial.");
        //    resultado = false;
        //}
        //else if (string.IsNullOrEmpty(this.dpFInicial.Text))
        //{
        //    mensaje.Append(" Fecha final.");
        //    resultado = false;
        //}
        else
            resultado = true;

        return resultado;
    }

    private bool ValidarAplicacion()
    {
        mensaje = new StringBuilder();
        bool resultado = true;
        mensaje.Append("Se requiere  el campo ");

        /*if (this.ddlSelecAplicacion.SelectedValue.Equals(""))
        {
            mensaje.Append(" Origen informaci�n.");
            resultado = false;
        }
        else */

        if (txtFInicial.Text.Equals(""))
        {
            mensaje.Append(" Fecha Inicial.");
            resultado = false;
        }
        else if (txtFFinal.Text.Equals(""))
        {
            mensaje.Append(" Fecha Final.");
            resultado = false;
        }
        else
            resultado = true;

        return resultado;
    }



    public void ConfigurarImportacion()
    {
        HttpContext.Current.Session["FInformacion"] = null;
        HttpContext.Current.Session["IController"] = null;
        if (this.cboTipoFuenteInformacion.Items.Count > 0)
        {
            finformacion = App.Consultas.ObtieneFuenteInformacionPorId(Convert.ToInt32(this.cboBancoFinanciero.SelectedValue), cboCuentaFinanciero.SelectedValue.ToString(), Convert.ToInt32(this.cboTipoFuenteInformacion.SelectedValue));
            HttpContext.Current.Session["FInformacion"] = finformacion;
        }
    }


    void LlenarComboConsecutivo()
    {

        cboTipoFuenteInformacion.Items.Clear();
        List<FuenteInformacion> fuenteInformacion = App.Consultas.ObtieneListaFuenteInformacionPorBancoCuenta(Convert.ToInt32(this.cboBancoFinanciero.SelectedValue), cboCuentaFinanciero.SelectedValue.ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Esta Cuenta no tiene configurado un  archivo.');", true);
        }
    }


    private void PosicionarComboCorporativo()
    {
        byte corporativo = ((SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"]).Corporativo;
        cboCorporativo.SelectedIndex = buscarItem(cboCorporativo.Items, corporativo.ToString());
        cboCorporativo_SelectedIndexChanged(null, null);
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



    private void LlenarCombos()
    {
        this.cboCorporativo.DataValueField = "Id";
        this.cboCorporativo.DataTextField = "Nombre";
        this.cboCorporativo.DataSource = App.Consultas.ObtieneListaCorporativo(((SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"]).IdUsuario);
        this.cboCorporativo.DataBind();
        this.PosicionarComboCorporativo();

        this.cboCuentaFinanciero.DataValueField = "Descripcion";
        this.cboCuentaFinanciero.DataTextField = "Descripcion";

        this.cboAnio.DataSource = App.Consultas.ObtieneListaAnios();
        this.cboAnio.DataBind();
        this.cboAnio.SelectedIndex = 0;

    }

    protected void btnGuardarDatos_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidarDatos())
            {
                ConfigurarImportacion();
                finformacion = (FuenteInformacion)HttpContext.Current.Session["FInformacion"];
                string ruta = "~/Archivos/ArchivosTemporales/";
                string rutaCompleta = MapPath(ruta) + finformacion.RutaArchivo;
                if (HttpContext.Current.Session["Archivo"] != null)
                {
                    SubirArchivo(rutaCompleta);
                    if (finformacion.FuenteInformacionDetalle.Count > 0)
                    {
                        if (SubirArchivo(rutaCompleta))
                        {
                            //CultureInfo culture = new CultureInfo("en-US");
                            TablaDestino tablaDestino = (TablaDestino)App.TablaDestino.CrearObjeto();
                            tablaDestino.IdCorporativo = Convert.ToInt32(this.cboCorporativo.SelectedValue);
                            tablaDestino.IdSucursal = Convert.ToInt32(this.cboSucursal.SelectedValue);
                            tablaDestino.Anio = Convert.ToInt32(this.cboAnio.SelectedValue);
                            tablaDestino.Folio = App.Consultas.ObtieneTablaDestinoNumeroMaximo(Convert.ToInt32(this.cboCorporativo.SelectedValue), Convert.ToInt32(this.cboSucursal.SelectedValue), Convert.ToInt32(this.cboAnio.SelectedValue)) + 1;
                            tablaDestino.IdFrecuencia = 1;
                            //tablaDestino.FInicial = Convert.ToDateTime(dpFInicial.Text);
                            //tablaDestino.FFinal = Convert.ToDateTime(dpFFinal.Text);
                            tablaDestino.IdStatusConciliacion = "CONCILIACION ABIERTA";
                            tablaDestino.FAlta = DateTime.Now; //DateTb{bime.ParseExact(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss"), "MM/dd/yyyy hh:mm:ss", culture); ;
                            tablaDestino.CuentaBancoFinanciero = cboCuentaFinanciero.SelectedValue.ToString();
                            tablaDestino.Usuario = ((SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"]).IdUsuario;
                            tablaDestino.IdTipoFuenteInformacion = finformacion.IdTipoFuenteInformacion;
                            ImportacionController importador = new ImportacionController(finformacion, rutaCompleta);
                            if (importador.ImportarArchivo(tablaDestino))
                                Limpiar();
                            else
                                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                                    "alert('Ocurrieron errores al importar el archivo);",
                                                                    true);
                            if (File.Exists(rutaCompleta))
                                File.Delete(rutaCompleta);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Ocurrier�n errores al tratar de cargar el archivo.');", true);
                        }
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('La Cuenta no tiene configurada una plantilla de Mapeo.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Seleccione un archivo.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('" + LimpiarTexto(mensaje.ToString()) + "');", true);
            }
            HttpContext.Current.Session["Archivo"] = null;
            HttpContext.Current.Session["NombreArchivo"] = null;
        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert(" + ex.Message + "');", true);
            App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }
    }

    private string LimpiarTexto(string texto)
    {
        texto = texto.Replace("\b", "\\b");    //Retroceso [Backspace] 
        texto = texto.Replace("\f", "\\f");    //[Form feed] 
        texto = texto.Replace("\n", "\\n");    //Nueva l�nea 
        texto = texto.Replace("\r", "\\r");    //Retorno de carro [Carriage return] 
        texto = texto.Replace("\t", "\\t");    //Tabulador [Tab] 
        texto = texto.Replace("\v", "\\v");    //Tabulador vertical 
        texto = texto.Replace("\'", "\\'");    //Ap�strofe o comilla simple 
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
        this.cboCuentaFinanciero.Items.Clear();
        this.cboTipoFuenteInformacion.Items.Clear();
        this.cboSucursal.SelectedIndex = 0;
        this.cboAnio.SelectedIndex = 0;
        //this.dpFInicial.Text = string.Empty;
        //this.dpFFinal.Text = string.Empty;
        HttpContext.Current.Session["Archivo"] = null;
        HttpContext.Current.Session["NombreArchivo"] = null;
        PosicionarComboCorporativo();

    }


    protected void btnCancelarDatos_Click(object sender, EventArgs e)
    {
        Limpiar();
    }

    protected void cboBancoFinanciero_SelectedIndexChanged(object sender, EventArgs e)
    {

        cboCuentaFinanciero.DataSource = App.Consultas.ObtieneListaCuentaFinancieroPorBanco(Convert.ToInt32(this.cboCorporativo.SelectedValue), Convert.ToInt32(cboBancoFinanciero.SelectedValue));
        cboCuentaFinanciero.DataBind();
        LlenarComboConsecutivo();
        ConfigurarImportacion();
    }

    protected void cboCuentaFinanciero_SelectedIndexChanged(object sender, EventArgs e)
    {
        LlenarComboConsecutivo();
        ConfigurarImportacion();
        Carga_Extractores();
    }

    protected void cboBancoFinanciero_DataBound(object sender, EventArgs e)
    {
        cboBancoFinanciero.Items.Insert(0, new ListItem(" ", "0"));
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
        this.cboSucursal.DataTextField = "Descripcion";
        this.cboSucursal.DataSource = App.Consultas.ObtieneListaSucursalPorCorporativo(Convert.ToInt32(this.cboCorporativo.SelectedValue));
        cboSucursal.DataBind();
        int sucursal = ((SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"]).Sucursal;
        cboSucursal.SelectedIndex = buscarItem(cboSucursal.Items, sucursal.ToString());


        this.cboCuentaFinanciero.Items.Clear();
        this.cboTipoFuenteInformacion.Items.Clear();


    }

    protected void cboSucursal_DataBound(object sender, EventArgs e)
    {
        cboSucursal.Items.Insert(0, new ListItem(" ", "0"));
        Carga_Extractores();
    }
    protected void cboAnio_DataBound(object sender, EventArgs e)
    {
        cboAnio.Items.Insert(0, new ListItem(" "));
    }


    protected void Archivo_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        HttpContext.Current.Session["Archivo"] = null;
        if (uploadFile.HasFile)
        {
            try
            {
                byte[] bytes = uploadFile.FileBytes;
                HttpContext.Current.Session["Archivo"] = bytes;
            }
            catch (Exception ex)
            {
                string mns = ex.InnerException.ToString();
                HttpContext.Current.Session["Archivo"] = null;
            }
        }
    }
    public void ActualizarSubirDesde()
    {
        bool seleccion = rdbSubirDesde.SelectedValue.Equals("Archivo");
        uploadFile.Visible = btnGuardar.Visible = seleccion;
        divAplicacion.Visible = btnGuardarAplicacion.Visible = !seleccion;
    }
    protected void rdbSubirDesde_SelectedIndexChanged(object sender, EventArgs e)
    {
        ActualizarSubirDesde();
        Carga_Extractores();
    }
    protected void ddlSelecAplicacion_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlSelecAplicacion_DataBound(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Llena el Combo de Importacion de Aplicaciones
    /// </summary>
    /// 
    public void Carga_Extractores()
    {

        string valor = "";

        if (!cboCuentaFinanciero.Text.Equals(""))
        {
            valor = cboCuentaFinanciero.SelectedValue.ToString();
        }


        Carga_ImportacionAplicaciones(Convert.ToInt16(cboSucursal.SelectedItem.Value), valor);



    }

    public void Carga_ImportacionAplicaciones(int sucursal, string cuentaBancaria)
    {
        int j = 0;
        try
        {

            listImportacionAplicacion = new List<Conciliacion.RunTime.ReglasDeNegocio.ImportacionAplicacion>();
            List<Conciliacion.RunTime.ReglasDeNegocio.ImportacionAplicacion> listImportacionAplicacionTemp1 = Conciliacion.RunTime.App.Consultas.ConsultaImportacionesAplicacion(sucursal, cuentaBancaria); ;

            for (int i = 0; i < listImportacionAplicacionTemp1.Count; i++)
            {
                listImportacionAplicacionTemp1[i].EsConfiguracion = true;
            }



            List<Conciliacion.RunTime.ReglasDeNegocio.ImportacionAplicacion> listImportacionAplicacionTemp2 = Conciliacion.RunTime.App.Consultas.ConsultaImportacionAplicacion(sucursal); ;
            for (int i = 0; i < listImportacionAplicacionTemp2.Count; i++)
            {
                foreach (Conciliacion.RunTime.ReglasDeNegocio.ImportacionAplicacion importacionObjeto in listImportacionAplicacionTemp1)
                {
                    if (importacionObjeto.Identificador == listImportacionAplicacionTemp2[i].Identificador)
                    {
                        listImportacionAplicacionTemp2[i] = null;
                        break;
                    }
                }

            }

            listImportacionAplicacionTemp2.RemoveAll(x => x == null);

            listImportacionAplicacion.AddRange(listImportacionAplicacionTemp1);
            listImportacionAplicacion.AddRange(listImportacionAplicacionTemp2);

            listadoOrigenes.DataSource = listImportacionAplicacion;
            listadoOrigenes.DataBind();



            foreach (Conciliacion.RunTime.ReglasDeNegocio.ImportacionAplicacion importacionObjeto2 in listImportacionAplicacion)
            {
                //CheckBox Sel = ((CheckBox)listadoOrigenes.Rows[fila.RowIndex].FindControl("chkSeleccionar"));

                if (importacionObjeto2.EsConfiguracion)
                {
                    ((CheckBox)listadoOrigenes.Rows[j].FindControl("chkSeleccionar")).Checked = true;
                    ((CheckBox)listadoOrigenes.Rows[j].FindControl("chkSeleccionar")).Enabled = false;
                }
                j = j + 1;
            }

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }
    protected void btnGuardarAplicacion_Click(object sender, EventArgs e)
    {
        int i = 0;
        List<Conciliacion.RunTime.ReglasDeNegocio.ImportacionAplicacion> listadoExtractores = new List<Conciliacion.RunTime.ReglasDeNegocio.ImportacionAplicacion>();
        if (ValidarDatos())
        {
            // Conciliacion.RunTime.ReglasDeNegocio.ImportacionAplicacion lcs = listImportacionAplicacion[ddlSelecAplicacion.SelectedIndex];

            foreach (GridViewRow linea in listadoOrigenes.Rows)
            {
                CheckBox chkRow = (linea.Cells[0].FindControl("chkSeleccionar") as CheckBox);
                if (chkRow.Checked)
                {
                    listadoExtractores.Add(listImportacionAplicacion[i]);
                }
                i = i + 1;
            }

            Conciliacion.RunTime.ReglasDeNegocio.ImportacionAplicacion lcs = listImportacionAplicacion[1];

            if (ValidarAplicacion())
            {
                CultureInfo culture = new CultureInfo("en-US");
                DateTime fi = DateTime.ParseExact(txtFInicial.Text, "MM/dd/yyyy", culture);
                DateTime ff = DateTime.ParseExact(txtFFinal.Text, "MM/dd/yyyy", culture);
                //DateTime fa = DateTime.ParseExact(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss"), "MM/dd/yyyy hh:mm:ss", culture);
                DateTime fa = DateTime.Now;
                int folio = App.Consultas.ObtieneTablaDestinoNumeroMaximo(Convert.ToInt32(this.cboCorporativo.SelectedValue), Convert.ToInt32(this.cboSucursal.SelectedValue), Convert.ToInt32(this.cboAnio.SelectedValue)) + 1;
                string usuario = ((SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"]).IdUsuario.Trim();
                string pass = SeguridadCB.Seguridad.DesencriptaClave(lcs.Pass);
                Conciliacion.Migracion.Runtime.ReglasNegocio.ImportacionAplicacion ia = null;

                /*ia = Conciliacion.Migracion.Runtime.App.ImportacionAplicacion(Convert.ToInt32(this.cboCorporativo.SelectedValue),
                                                                              Convert.ToInt32(this.cboSucursal.SelectedValue),
                                                                              Convert.ToInt32(this.cboAnio.SelectedValue),
                                                                              this.cboCuentaFinanciero.SelectedValue,
                                                                              lcs.TipoFuenteInformacion,
                                                                              fi,
                                                                              ff,
                                                                              fa,
                                                                              lcs.Procedimiento,
                                                                              usuario,
                                                                              "CONCILIACION ABIERTA",
                                                                              folio,
                                                                              lcs.Servidor,
                                                                              lcs.BaseDeDatos,
                                                                              lcs.UsuarioConsulta,
                                                                              pass
                                                                              );*/

                ia = Conciliacion.Migracion.Runtime.App.ImportacionAplicacion(Convert.ToInt32(this.cboCorporativo.SelectedValue),
                                                                              Convert.ToInt32(this.cboSucursal.SelectedValue),
                                                                              Convert.ToInt32(this.cboAnio.SelectedValue),
                                                                              this.cboCuentaFinanciero.SelectedValue,
                                                                              fi,
                                                                              ff,
                                                                              fa,
                                                                              usuario,
                                                                              "CONCILIACION ABIERTA",
                                                                              folio,
                                                                              pass, listadoExtractores
                                                                              );

                if (! ia.PeriodoFechasOcupado(fi, ff, this.cboCuentaFinanciero.SelectedValue))
                {
                    if (ia.GuardaEnTablaDestinoDetalle())
                    {
                        App.ImplementadorMensajes.MostrarMensaje("Guardado con Exito.");
                        Limpiar();
                        txtFInicial.Text = txtFFinal.Text = String.Empty;
                        ia = null;
                    }
                    else { App.ImplementadorMensajes.MostrarMensaje("Ocurrieron conflictos al Guardar desde Aplicaci�n. \n Posibles Razones:\n1. Tabla Destino no fue encontrada. \n2. El extractor no ha devuelto ningun registro."); }
                }
                else { App.ImplementadorMensajes.MostrarMensaje("El periodo de fecha ya esta ocupado."); }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('" + LimpiarTexto(mensaje.ToString()) + "');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('" + LimpiarTexto(mensaje.ToString()) + "');", true);
        }
        HttpContext.Current.Session["Archivo"] = null;
        HttpContext.Current.Session["NombreArchivo"] = null;
    }


    protected void cboSucursal_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Carga_Extractores();
        }
        catch (Exception ex)
        {

            App.ImplementadorMensajes.MostrarMensaje("Error: \n" + ex.Message);
        }

    }


}
