using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Conciliacion.RunTime;
using Conciliacion.RunTime.ReglasDeNegocio;
using System.Data;
using SeguridadCB.Public;
using Conciliacion.RunTime.DatosSQL;

public partial class Conciliacion_NuevaConciliacion : System.Web.UI.Page
{
    Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
    #region "Propiedades Globales"
    private SeguridadCB.Public.Usuario usuario;
    private SeguridadCB.Public.Parametros parametros;
    //private Conciliacion.RunTime.ReglasDeNegocio.cConciliacion conciliacion = App.Conciliacion;
    private Conciliacion.RunTime.ReglasDeNegocio.DatosArchivo datosArchivoExterno = null;
    private Conciliacion.RunTime.ReglasDeNegocio.DatosArchivo datosArchivoInterno = null;
    #endregion

    #region "Propiedades privadas"
    private List<ListaCombo> listCuentaBancariaExternoInterno = new List<ListaCombo>();
    private List<ListaCombo> listSucursales = new List<ListaCombo>();
    private List<ListaCombo> listTipoFuenteInformacionExternoInterno = new List<ListaCombo>();
    private List<ListaCombo> listBancos = new List<ListaCombo>();
    private List<ListaCombo> listCuentaBancaria = new List<ListaCombo>();
    private List<ListaCombo> listTipoConciliacion = new List<ListaCombo>();
    private List<ListaCombo> listGrupoConciliacion = new List<ListaCombo>();
    public List<ListaCombo> listFoliosExterno = new List<ListaCombo>();
    public List<ListaCombo> listFoliosInterno = new List<ListaCombo>();
    private List<DatosArchivoDetalle> listaDestinoDetalleExterno = new List<DatosArchivoDetalle>();
    private List<DatosArchivoDetalle> listaDestinoDetalleInterno = new List<DatosArchivoDetalle>();
    private DataTable tblDestinoDetalleExterno = new DataTable("VistaFoliosExterno");
    private DataTable tblDestinoDetalleInterno = new DataTable("VistaFoliosInterno");
    public string NumMesesAnterior;
    private SeguridadCB.Seguridad seguridad;
    #endregion



    protected void Page_Load(object sender, EventArgs e)
    {
        seguridad = new SeguridadCB.Seguridad();
        objApp.ImplementadorMensajes.ContenedorActual = this;
        try
        {
            if (HttpContext.Current.Request.UrlReferrer != null)
            {
                if ((!HttpContext.Current.Request.UrlReferrer.AbsoluteUri.Contains("SitioConciliacion")) || (HttpContext.Current.Request.UrlReferrer.AbsoluteUri.Contains("Acceso.aspx")))
                {
                    HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
                    HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
                    Response.Cache.SetExpires(DateTime.Now);
                }
            }
            parametros = (SeguridadCB.Public.Parametros)HttpContext.Current.Session["Parametros"];
            AppSettingsReader settings = new AppSettingsReader();
            NumMesesAnterior = parametros.ValorParametro(Convert.ToSByte(settings.GetValue("Modulo", typeof(sbyte))), "NunMesesAnterior");
            if (!IsPostBack)
            {
                usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
                if (ddlEmpresa.Items.Count == 0) Carga_Corporativo();
                Carga_Banco(Convert.ToInt32(ddlEmpresa.SelectedItem.Value));
                Carga_CuentaBancaria(Convert.ToInt32(ddlEmpresa.SelectedItem.Value),
                    Convert.ToSByte(ddlBanco.SelectedItem.Value));
                Carga_TipoConciliacion(usuario.IdUsuario);
                Carga_GrupoConciliacion(Consultas.ConfiguracionGrupo.ConAccesoTotal, usuario.IdUsuario);
                Carga_SucursalCorporativo(Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionIden0.Sin0,
                    Convert.ToInt32(ddlEmpresa.SelectedItem.Value));
                //Inhabilitar las pestaña de EXTERNOS E INTERNOS al cargar la VISTA
                tabNuevaConciliacion.Tabs[1].Enabled = false;
                tabNuevaConciliacion.Tabs[2].Enabled = false;
                Carga_TipoFuenteInformacionExternoInterno(
                    Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionTipoFuente.TipoFuenteInformacionExterno);
                Carga_TipoFuenteInformacionExternoInterno(
                    Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionTipoFuente.TipoFuenteInformacionInterno);
            }

        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error: Cargar Vista" + ex.Message);
        }
    }
    //Limpian variables de Session
    public void limpiarVariablesSession()
    {
        //Eliminar las variables de Session utilizadas en la Vista
        HttpContext.Current.Session["DETALLEEXTERNO"] = null;
        HttpContext.Current.Session["DETALLEINTERNO"] = null;

        HttpContext.Current.Session.Remove("DETALLEEXTERNO");
        HttpContext.Current.Session.Remove("DETALLEINTERNO");
    }

    #region Funciones Privadas
    public string fechaMaximaConciliacion()
    {
        return objApp.Consultas.ConsultaFechaActualInicial();
    }
    public string fechaMinimaConciliacion(string numMesesAnterior, string fechaMaximaActual)
    {
        return objApp.Consultas.ConsultaFechaPermitidoConciliacion(numMesesAnterior, fechaMaximaActual);
    }
    public string leerMes()
    {
        int p = txtMes.Text.IndexOf("/");
        return txtMes.Text.Substring(0, p);

    }
    public string leerAño()
    {
        int p = txtMes.Text.IndexOf("/");
        return txtMes.Text.Substring(p + 1);

    }

    /// <summary>
    /// Consulta si la cuenta seleccionado pertenece a las cuentas que solo 
    /// requieren el Archivo Externo para crear la Conciliacion

    public bool CuentaSoloExternoConciliacion(string cuentaBancaria)
    {
        parametros = Session["Parametros"] as Parametros;
        AppSettingsReader settings = new AppSettingsReader();
        short modulo = Convert.ToSByte(settings.GetValue("Modulo", typeof(string)).ToString());
        string parametro = parametros.ValorParametro(modulo, "Cuentas Solo Externos");

        return Array.FindAll(parametro.Split(new[] { ',' }), s => s.Trim().Equals(cuentaBancaria.Trim())).Length > 0;
        //return parametro.Split(new Char[] {','}).ToString().Contains(cuentaBancaria);
    }
    /// <summary>
    /// Llena el Combo de las Empresas por Usuario
    /// </summary>
    public void Carga_Corporativo()
    {
        try
        {
            DataTable dtEmpresas = new DataTable();
            Usuario usuario;
            usuario = (Usuario)HttpContext.Current.Session["Usuario"];
            dtEmpresas = usuario.CorporativoAcceso;
            this.ddlEmpresa.DataSource = dtEmpresas;
            this.ddlEmpresa.DataValueField = "Corporativo";
            this.ddlEmpresa.DataTextField = "NombreCorporativo";
            this.ddlEmpresa.DataBind();
            dtEmpresas.Dispose();

        }
        catch
        {
        }
    }
    /// <summary>
    /// Llena el Combo de Sucurales según Corporativo para Interno
    /// </summary>
    public void Carga_SucursalCorporativo(Consultas.ConfiguracionIden0 config, int corporativo)
    {
        try
        {
            listSucursales = objApp.Consultas.ConsultaSucursales(config, corporativo);

            this.ddlSucursalInterno.DataSource = listSucursales;
            this.ddlSucursalInterno.DataValueField = "Identificador";
            this.ddlSucursalInterno.DataTextField = "Descripcion";
            this.ddlSucursalInterno.DataBind();
            this.ddlSucursalInterno.Dispose();
        }
        catch
        {
        }
    }
    /// <summary>
    /// Llena el Combo de Bancos
    /// </summary>
    public void Carga_Banco(int corporativo)
    {
        try
        {

            listBancos = objApp.Consultas.ConsultaBancos(corporativo);
            this.ddlBanco.DataSource = listBancos;
            this.ddlBanco.DataValueField = "Identificador";
            this.ddlBanco.DataTextField = "Descripcion";
            this.ddlBanco.DataBind();
            this.ddlBanco.Dispose();
        }
        catch
        {
        }
    }
    /// <summary>
    /// Llena el Combo de Cuenta Bancaria
    /// </summary>
    public void Carga_CuentaBancaria(int corporativo, short banco)
    {
        try
        {

            listCuentaBancaria = objApp.Consultas.ConsultaCuentasBancaria(corporativo, banco);
            this.ddlCuentaBancaria.DataSource = listCuentaBancaria;
            this.ddlCuentaBancaria.DataValueField = "Identificador";
            this.ddlCuentaBancaria.DataTextField = "Descripcion";
            this.ddlCuentaBancaria.DataBind();
            this.ddlCuentaBancaria.Dispose();


        }
        catch (Exception)
        {

            this.ddlCuentaBancaria.DataSource = new List<ListaCombo>();
            this.ddlCuentaBancaria.DataBind();
            this.ddlCuentaBancaria.Dispose();
        }
    }
    /// <summary>
    /// Llena el Combo de Tipo Conciliacion
    /// </summary>
    public void Carga_TipoConciliacion(string usuario)
    {
        try
        {
            listTipoConciliacion = objApp.Consultas.ConsultaTipoConciliacion(Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionGrupo.Asignados, usuario);
            this.ddlTipoConciliacion.DataSource = listTipoConciliacion;
            this.ddlTipoConciliacion.DataValueField = "Identificador";
            this.ddlTipoConciliacion.DataTextField = "Descripcion";
            this.ddlTipoConciliacion.DataBind();
            this.ddlTipoConciliacion.Dispose();
        }
        catch
        {
        }
    }
    /// <summary>
    /// Llena el Combo de Tipo Conciliacion
    /// </summary>
    public void Carga_GrupoConciliacion(Consultas.ConfiguracionGrupo configuracion, string usuario)
    {
        try
        {
            listGrupoConciliacion = objApp.Consultas.ConsultaGruposConciliacion(configuracion, usuario);
            this.ddlGrupoConciliacion.DataSource = listGrupoConciliacion;
            this.ddlGrupoConciliacion.DataValueField = "Identificador";
            this.ddlGrupoConciliacion.DataTextField = "Descripcion";
            this.ddlGrupoConciliacion.DataBind();
            this.ddlGrupoConciliacion.Dispose();
        }
        catch (Exception)
        {
        }
    }
    /// <summary>
    /// Llena el Combo de Tipo Fuente Informacion Externo e Interno
    /// </summary>
    public void Carga_TipoFuenteInformacionExternoInterno(Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionTipoFuente tipo)
    {
        try
        {
            switch (tipo)
            {
                case Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionTipoFuente.TipoFuenteInformacionExterno:

                    listTipoFuenteInformacionExternoInterno = objApp.Consultas.ConsultaTipoInformacionDatos(tipo);
                    this.ddlTipoFuenteInfoExterno.DataSource = listTipoFuenteInformacionExternoInterno;
                    this.ddlTipoFuenteInfoExterno.DataValueField = "Identificador";
                    this.ddlTipoFuenteInfoExterno.DataTextField = "Descripcion";
                    this.ddlTipoFuenteInfoExterno.DataBind();
                    this.ddlTipoFuenteInfoExterno.Dispose();
                    break;

                case Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionTipoFuente.TipoFuenteInformacionInterno:

                    listTipoFuenteInformacionExternoInterno = objApp.Consultas.ConsultaTipoInformacionDatos(tipo);
                    this.ddlTipoFuenteInfoInterno.DataSource = listTipoFuenteInformacionExternoInterno;
                    this.ddlTipoFuenteInfoInterno.DataValueField = "Identificador";
                    this.ddlTipoFuenteInfoInterno.DataTextField = "Descripcion";
                    this.ddlTipoFuenteInfoInterno.DataBind();
                    this.ddlTipoFuenteInfoInterno.Dispose();
                    break;
            }

        }
        catch
        {
        }
    }
    /// <summary>
    /// Llena el Combo de Folios Externos según parametros de filtro.
    /// </summary>
    public void carga_FoliosExternos(int corporativo, int sucursal, int añoF, short mesF, string cuentabancaria, short tipofuenteinformacion)
    {
        try
        {
            listFoliosExterno = objApp.Consultas.ConsultaFoliosTablaDestino(corporativo, sucursal, añoF, mesF, cuentabancaria, tipofuenteinformacion);
            //HttpContext.Current.Session["listFoliosExternos"] = listFoliosExterno;

        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:CargaExterno\n " + ex.Message);
        }
    }

    /// <summary>
    /// Enlaza el combo con los Folio Externos
    /// </summary>
    public void enlazarFolioExternos()
    {
        try
        {
            this.ddlFolioExterno.DataSource = listFoliosExterno;
            this.ddlFolioExterno.DataValueField = "Identificador";
            this.ddlFolioExterno.DataTextField = "Descripcion";
            this.ddlFolioExterno.DataBind();
            this.ddlFolioExterno.Dispose();
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:Enlace con FolioExterno\n " + ex.Message);
        }
    }
    /// <summary>
    /// Llena el GridView de Folios Internos Agregados
    /// </summary>
    private void LlenaGridViewFoliosAgregados()
    {
        this.grvAgregados.DataSource = objApp.Conciliacion.ListaArchivos;
        this.grvAgregados.DataBind();

    }
    /// <summary>
    ///Consulta el detalle del Folio Externo
    /// </summary>
    public void Consulta_TablaDestinoDetalleExterno(Conciliacion.RunTime.ReglasDeNegocio.Consultas.Configuracion configuracion, int empresa, int sucursal, int año, int folioExterno)//Lee el metodo que llena la lista con las conciliaciones
    {
        System.Data.SqlClient.SqlConnection Connection = seguridad.Conexion;
        if (Connection.State == ConnectionState.Closed)
        {
            seguridad.Conexion.Open();
            Connection = seguridad.Conexion;
        }
        try
        {
            listaDestinoDetalleExterno = objApp.Consultas.ConsultaTablaDestinoDetalle(
                        configuracion,
                        empresa,
                        sucursal,
                        año,
                        folioExterno);
        }
        catch (Exception)
        {

        }
        finally
        {

        }

    }
    /// <summary>
    /// Llena el VistaRapida[TablaDestinoDetalleExterno] x Folio
    /// </summary>
    public void GenerarTablaDestinoDetalleExterno()//Genera la tabla de destino detalle[Vista Rapida]
    {
        tblDestinoDetalleExterno.Columns.Add("Folio", typeof(int));
        tblDestinoDetalleExterno.Columns.Add("FOperacion", typeof(DateTime));
        tblDestinoDetalleExterno.Columns.Add("FMovimiento", typeof(DateTime));
        tblDestinoDetalleExterno.Columns.Add("Referencia", typeof(string));
        tblDestinoDetalleExterno.Columns.Add("Descripcion", typeof(string));
        tblDestinoDetalleExterno.Columns.Add("Deposito", typeof(float));
        tblDestinoDetalleExterno.Columns.Add("Retiro", typeof(float));
        tblDestinoDetalleExterno.Columns.Add("Concepto", typeof(string));

        foreach (DatosArchivoDetalle da in listaDestinoDetalleExterno)
        {
            tblDestinoDetalleExterno.Rows.Add(
                da.Folio,
                da.FOperacion,
                da.FMovimiento,
                da.Referencia,
                da.Descripcion,
                da.Deposito,
                da.Retiro,
                da.Concepto);
        }
        HttpContext.Current.Session["DETALLEEXTERNO"] = tblDestinoDetalleExterno;
    }
    private void LlenaGridViewDestinoDetalleExterno()//Llena el gridview con las conciliaciones antes leídas
    {
        DataTable tablaDestinoDetalleExterno = (DataTable)HttpContext.Current.Session["DETALLEEXTERNO"];
        this.grvVistaRapidaExterno.DataSource = tblDestinoDetalleExterno;
        this.grvVistaRapidaExterno.DataBind();
    }
    /// <summary>
    /// Consulta Folios Internos según parametros de filtro.
    /// </summary>
    public void carga_FoliosInternos(int corporativo, int sucursal, int añoF, short mesF, string cuentabancaria, short tipofuenteinformacion)
    {
        try
        {
             listFoliosInterno = objApp.Consultas.ConsultaFoliosTablaDestino(corporativo, sucursal, añoF, mesF, cuentabancaria, tipofuenteinformacion);
            //HttpContext.Current.Session["listFoliosInterno"] = listFoliosInterno;
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:FolioIn\n" + ex.Message);
        }
    }

    /// <summary>
    /// Consulta Folios Internos según parametros de filtro.
    /// </summary>
    public void enlazarFoliosInternos()
    {
        this.ddlFolioInterno.DataSource = listFoliosInterno;
        this.ddlFolioInterno.DataValueField = "Identificador";
        this.ddlFolioInterno.DataTextField = "Descripcion";
        this.ddlFolioInterno.DataBind();
        this.ddlFolioInterno.Dispose();
    }
    /// <summary>
    ///Consulta el detalle del Folio Interno
    /// </summary>
    public void Consulta_TablaDestinoDetalleInterno(Conciliacion.RunTime.ReglasDeNegocio.Consultas.Configuracion configuracion, int empresa, int sucursal, int año, int folioInterno)//Lee el metodo que llena la lista con las conciliaciones
    {
        System.Data.SqlClient.SqlConnection Connection = seguridad.Conexion;
        if (Connection.State == ConnectionState.Closed)
        {
            seguridad.Conexion.Open();
            Connection = seguridad.Conexion;
        }
        try
        {
            listaDestinoDetalleInterno = objApp.Consultas.ConsultaTablaDestinoDetalle(
                        configuracion,
                        empresa,
                        sucursal,
                        año,
                        folioInterno);
        }
        catch (Exception)
        {

        }

    }
    /// <summary>
    /// Llena el VistaRapida[TablaDestinoDetalleExterno] x Folio
    /// </summary>
    public void GenerarTablaDestinoDetalleInterno()
    {
        tblDestinoDetalleInterno.Columns.Add("Folio", typeof(int));
        tblDestinoDetalleInterno.Columns.Add("FOperacion", typeof(DateTime));
        tblDestinoDetalleInterno.Columns.Add("FMovimiento", typeof(DateTime));
        tblDestinoDetalleInterno.Columns.Add("Referencia", typeof(string));
        tblDestinoDetalleInterno.Columns.Add("Descripcion", typeof(string));
        tblDestinoDetalleInterno.Columns.Add("Deposito", typeof(float));
        tblDestinoDetalleInterno.Columns.Add("Retiro", typeof(float));
        tblDestinoDetalleInterno.Columns.Add("Concepto", typeof(string));

        foreach (DatosArchivoDetalle da in listaDestinoDetalleInterno)
        {
            tblDestinoDetalleInterno.Rows.Add(
                da.Folio,
                da.FOperacion.ToShortDateString(),
                da.FMovimiento.ToShortDateString(),
                da.Referencia,
                da.Descripcion,
                da.Deposito,
                da.Retiro,
                da.Concepto);
        }
        HttpContext.Current.Session["DETALLEINTERNO"] = tblDestinoDetalleInterno;
    }
    /// <summary>
    ///Genera la tabla de destino detalle[Vista Rapida]
    ///  
    private void LlenaGridViewDestinoDetalleInterno()
    {
        DataTable tablaDestinoDetalleInterno = (DataTable)HttpContext.Current.Session["DETALLEINTERNO"];
        this.grvVistaRapidaInterno.DataSource = tblDestinoDetalleInterno;
        this.grvVistaRapidaInterno.DataBind();
    }
    /// <summary>
    /// Llena el gridview con las conciliaciones antes leídas
    /// </summary>
    public bool AgregarArchivoExterno()
    {
        carga_FoliosExternos(Convert.ToInt32(ddlEmpresa.SelectedItem.Value),
            Convert.ToInt32(0),
            Convert.ToInt32(leerAño()),
            Convert.ToSByte(leerMes()),
            ddlCuentaBancaria.SelectedItem.Text,
            Convert.ToSByte(ddlTipoFuenteInfoExterno.SelectedItem.Value));

        datosArchivoExterno = objApp.DatosArchivo;
        datosArchivoExterno.Folio = Convert.ToInt32(ddlFolioExterno.SelectedItem.Text);
        datosArchivoExterno.Sucursal = Convert.ToInt32(listFoliosExterno[ddlFolioExterno.SelectedIndex].Campo2);
        return objApp.Conciliacion.AgregarArhivoExterno(datosArchivoExterno);
    }
    /// <summary>
    /// Guardar Conciliacion Creada
    /// </summary>
    public bool GuardarConciliacion(string usuarioID)
    {
        objApp.Conciliacion.Corporativo = Convert.ToInt32(ddlEmpresa.SelectedItem.Value);
        objApp.Conciliacion.Sucursal = datosArchivoExterno.Sucursal;
        objApp.Conciliacion.CuentaBancaria = ddlCuentaBancaria.SelectedItem.Text.Trim();
        objApp.Conciliacion.TipoConciliacion = Convert.ToSByte(ddlTipoConciliacion.SelectedItem.Value);
        objApp.Conciliacion.GrupoConciliacion = Convert.ToInt32(ddlGrupoConciliacion.SelectedItem.Value);
        objApp.Conciliacion.Mes = Convert.ToSByte(leerMes());
        objApp.Conciliacion.Año = Convert.ToInt32(leerAño());

        return objApp.Conciliacion.Guardar(usuarioID) ? BorrarTransaccionesNoCorrespondidas(objApp.Conciliacion) : false;

    }
    /// <summary>
    /// Guardar Conciliacion SobreGiro
    /// </summary>
    public bool GuardarConciliacionSinInterno(string usuarioID)
    {
        objApp.Conciliacion.Corporativo = Convert.ToInt32(ddlEmpresa.SelectedItem.Value);
        objApp.Conciliacion.Sucursal = datosArchivoExterno.Sucursal;
        objApp.Conciliacion.CuentaBancaria = ddlCuentaBancaria.SelectedItem.Text.Trim();
        objApp.Conciliacion.TipoConciliacion = Convert.ToSByte(ddlTipoConciliacion.SelectedItem.Value);
        objApp.Conciliacion.GrupoConciliacion = Convert.ToInt32(ddlGrupoConciliacion.SelectedItem.Value);
        objApp.Conciliacion.Mes = Convert.ToSByte(leerMes());
        objApp.Conciliacion.Año = Convert.ToInt32(leerAño());

        return objApp.Conciliacion.GuardarSinInterno(usuarioID) ? BorrarTransaccionesNoCorrespondidas(objApp.Conciliacion) : false;

    }
    public bool BorrarTransaccionesNoCorrespondidas(cConciliacion conciliacion)
    {
        System.Data.SqlClient.SqlConnection Connection = seguridad.Conexion;
        if (Connection.State == ConnectionState.Closed)
        {
            seguridad.Conexion.Open();
            Connection = seguridad.Conexion;
        }
        try
        {
            return objApp.Consultas.BorrarTransaccionesNoCorrespondidas(
                objApp.Conciliacion.Corporativo, Convert.ToInt16(objApp.Conciliacion.Sucursal), objApp.Conciliacion.Año, objApp.Conciliacion.Mes, objApp.Conciliacion.Folio);
        }
        catch (Exception)
        {
            return false;
        }
        return false;
    }
    /// <summary>
    /// Mensajes
    /// </summary>
    protected void Mensaje(string titulo, string texto)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "alert('" + texto + "');", true);
    }
    #endregion
    #region Funciones de Forma
    protected void ddlSucursalInterno_SelectedIndexChanged(object sender, EventArgs e)
    {
        carga_FoliosInternos(Convert.ToInt32(ddlEmpresa.SelectedItem.Value), Convert.ToInt32(ddlSucursalInterno.SelectedItem.Value), Convert.ToInt32(leerAño()), Convert.ToSByte(leerMes()), Convert.ToString(ddlCuentaBancaria.SelectedItem.Text), Convert.ToSByte(ddlTipoFuenteInfoInterno.SelectedItem.Value));
        enlazarFoliosInternos();
    }
    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {

        Carga_SucursalCorporativo(Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionIden0.Sin0, Convert.ToInt32(ddlEmpresa.SelectedItem.Value));
        Carga_Banco(Convert.ToInt32(ddlEmpresa.SelectedItem.Value));
        try
        {
            Carga_CuentaBancaria(Convert.ToInt32(ddlEmpresa.SelectedItem.Value), Convert.ToSByte(ddlBanco.SelectedItem.Value));
        }
        catch (Exception)
        {
            Carga_CuentaBancaria(Convert.ToInt32(ddlEmpresa.SelectedItem.Value), -1);
        }



    }
    protected void ddlBanco_SelectedIndexChanged(object sender, EventArgs e)
    {
        Carga_CuentaBancaria(Convert.ToInt32(ddlEmpresa.SelectedItem.Value), Convert.ToSByte(ddlBanco.SelectedItem.Value));
    }
    protected void btnSiguiente_Click(object sender, EventArgs e)
    {
        tabNuevaConciliacion.Tabs[0].Enabled = false;
        tabNuevaConciliacion.Tabs[1].Enabled = true;
        tabNuevaConciliacion.ActiveTab = tabNuevaConciliacion.Tabs[1];
        carga_FoliosExternos(Convert.ToInt32(ddlEmpresa.SelectedItem.Value), Convert.ToInt32(0), Convert.ToInt32(leerAño()), Convert.ToSByte(leerMes()), ddlCuentaBancaria.SelectedItem.Text.Trim(), Convert.ToSByte(ddlTipoFuenteInfoExterno.SelectedItem.Value));
        enlazarFolioExternos();

    }
    protected void btnSiguienteExterno_Click(object sender, EventArgs e)
    {

        tabNuevaConciliacion.Tabs[1].Enabled = false;
        tabNuevaConciliacion.Tabs[2].Enabled = true;
        tabNuevaConciliacion.ActiveTab = tabNuevaConciliacion.Tabs[2];

        carga_FoliosInternos(
                       Convert.ToInt32(ddlEmpresa.SelectedItem.Value),
                       Convert.ToInt32(ddlSucursalInterno.SelectedItem.Value),
                       Convert.ToInt32(leerAño()),
                       Convert.ToSByte(leerMes()),
                       Convert.ToString(ddlCuentaBancaria.SelectedItem.Text),
                       Convert.ToSByte(ddlTipoFuenteInfoInterno.SelectedItem.Value)
                       );
        enlazarFoliosInternos();
        LlenaGridViewFoliosAgregados();
    }
    protected void ddlTipoFuenteInfoInterno_SelectedIndexChanged(object sender, EventArgs e)
    {
        carga_FoliosInternos(
                       Convert.ToInt32(ddlEmpresa.SelectedItem.Value),
                       Convert.ToInt32(ddlSucursalInterno.SelectedItem.Value),
                       Convert.ToInt32(leerAño()),
                       Convert.ToSByte(leerMes()),
                       Convert.ToString(ddlCuentaBancaria.SelectedItem.Text),
                       Convert.ToSByte(ddlTipoFuenteInfoInterno.SelectedItem.Value)
                       );
        enlazarFoliosInternos();
    }
    protected void ddlTipoFuenteInfoExterno_SelectedIndexChanged(object sender, EventArgs e)
    {
        carga_FoliosExternos(Convert.ToInt32(ddlEmpresa.SelectedItem.Value), Convert.ToInt32(0), Convert.ToInt32(leerAño()), Convert.ToSByte(leerMes()), ddlCuentaBancaria.SelectedItem.Text, Convert.ToSByte(ddlTipoFuenteInfoExterno.SelectedItem.Value));
        enlazarFolioExternos();
    }
    protected void btnAtrasExterno_Click(object sender, EventArgs e)
    {
        if (objApp.Conciliacion.ListaArchivos.Count > 0)
        {
            mpeMensageRegreso.Show();
        }
        else
        {
            tabNuevaConciliacion.Tabs[0].Enabled = true;
            tabNuevaConciliacion.Tabs[1].Enabled = false;
            tabNuevaConciliacion.ActiveTab = tabNuevaConciliacion.Tabs[0];
        }
    }
    protected void ddlFolioExterno_SelectedIndexChanged(object sender, EventArgs e)
    {
        carga_FoliosExternos(Convert.ToInt32(ddlEmpresa.SelectedItem.Value),
            Convert.ToInt32(0),
            Convert.ToInt32(leerAño()),
            Convert.ToSByte(leerMes()),
            ddlCuentaBancaria.SelectedItem.Text,
            Convert.ToSByte(ddlTipoFuenteInfoExterno.SelectedItem.Value));

        this.lblStatusFolioExterno.Text = listFoliosExterno[ddlFolioExterno.SelectedIndex].Campo1;
        this.lblUsuarioAltaEx.Text = listFoliosExterno[ddlFolioExterno.SelectedIndex].Campo3;
    }
    protected void ddlFolioInterno_SelectedIndexChanged(object sender, EventArgs e)
    {
        carga_FoliosInternos(Convert.ToInt32(ddlEmpresa.SelectedItem.Value),
            Convert.ToInt32(ddlSucursalInterno.SelectedItem.Value),
            Convert.ToInt32(leerAño()), Convert.ToSByte(leerMes()),
            Convert.ToString(ddlCuentaBancaria.SelectedItem.Text),
            Convert.ToSByte(ddlTipoFuenteInfoInterno.SelectedItem.Value));

        this.lblStatusFolioInterno.Text = listFoliosInterno[ddlFolioInterno.SelectedIndex].Campo1;
        this.lblUsuarioAlta.Text = listFoliosInterno[ddlFolioInterno.SelectedIndex].Campo3;
    }
    protected void ddlFolioExterno_DataBound(object sender, EventArgs e)
    {
        if (ddlFolioExterno.Items.Count <= 0)
        {
            this.lblStatusFolioExterno.Text = this.lblUsuarioAltaEx.Text = String.Empty;
        }
        else
        {
            this.lblStatusFolioExterno.Text = listFoliosExterno[ddlFolioExterno.SelectedIndex].Campo1;
            this.lblUsuarioAltaEx.Text = listFoliosExterno[ddlFolioExterno.SelectedIndex].Campo3;
        }
    }
    protected void ddlFolioInterno_DataBound(object sender, EventArgs e)
    {
        if (ddlFolioInterno.Items.Count <= 0)
        {
            this.lblStatusFolioInterno.Text = this.lblUsuarioAlta.Text = String.Empty;
        }
        else
        {
            this.lblStatusFolioInterno.Text = listFoliosInterno[ddlFolioInterno.SelectedIndex].Campo1;
            this.lblUsuarioAlta.Text = listFoliosInterno[ddlFolioInterno.SelectedIndex].Campo3;

        }
    }
    protected void btnAtrasInterno_Click(object sender, EventArgs e)
    {
        tabNuevaConciliacion.Tabs[1].Enabled = true;
        tabNuevaConciliacion.Tabs[2].Enabled = false;
        tabNuevaConciliacion.ActiveTab = tabNuevaConciliacion.Tabs[1];
    }
    protected void btnGuardarConciliacion_Click(object sender, EventArgs e)
    {
        usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
        parametros = Session["Parametros"] as Parametros;
        AppSettingsReader settings = new AppSettingsReader();
        short modulo = Convert.ToSByte(settings.GetValue("Modulo", typeof(string)).ToString());
        string parametro = parametros.ValorParametro(modulo, "Cuenta Sobregiro");

        if (ddlTipoConciliacion.SelectedItem.Text.Equals("CONCILIACION DE INGRESOS POR COBRANZA A ABONAR PEDIDO") || ddlTipoConciliacion.SelectedItem.Text.Equals("CONCILIACION DE INGRESOS Y EGRESOS SIN ARCHIVO") || ddlTipoConciliacion.SelectedItem.Text.Equals("CONCILIACION DE INGRESOS POR COBRANZA A ABONAR") || ddlCuentaBancaria.SelectedItem.Text.Trim().Equals(parametro.Trim()))
        {
            if (AgregarArchivoExterno())
            {
                if (GuardarConciliacionSinInterno(usuario.IdUsuario))
                {
                    limpiarVariablesSession();
                    Response.Redirect("~/Inicio.aspx");
                }
            }
        }
        else
        {
            if (objApp.Conciliacion.ListaArchivos.Count < 1)
            {
                objApp.ImplementadorMensajes.MostrarMensaje("Aún no ha agregado ningún archivo interno.");
            }
            else
            {
                if (AgregarArchivoExterno())
                {
                    if (GuardarConciliacion(usuario.IdUsuario))
                    {
                        objApp.Conciliacion.ListaArchivos.Clear();
                        limpiarVariablesSession();
                        Response.Redirect("~/Inicio.aspx");
                    }
                }
            }
        }
    }
    protected void btnVerDetalleExterno_Click(object sender, ImageClickEventArgs e)
    {
        carga_FoliosExternos(Convert.ToInt32(ddlEmpresa.SelectedItem.Value),
            Convert.ToInt32(0),
            Convert.ToInt32(leerAño()),
            Convert.ToSByte(leerMes()),
            ddlCuentaBancaria.SelectedItem.Text,
            Convert.ToSByte(ddlTipoFuenteInfoExterno.SelectedItem.Value));

        Consulta_TablaDestinoDetalleExterno(
                Consultas.Configuracion.Previo,
                Convert.ToInt32(ddlEmpresa.SelectedItem.Value),
                Convert.ToInt32(listFoliosExterno[ddlFolioExterno.SelectedIndex].Campo2),
                Convert.ToInt32(leerAño()),
                Convert.ToInt32(ddlFolioExterno.SelectedItem.Value)
            );
        GenerarTablaDestinoDetalleExterno();
        LlenaGridViewDestinoDetalleExterno();
        lblFESelec.Text = ddlFolioExterno.SelectedItem.Value;
        grvVistaRapidaExterno_ModalPopupExtender.Show();
    }
    protected void btnVerDetalleInterno_Click(object sender, ImageClickEventArgs e)
    {
        Consulta_TablaDestinoDetalleInterno(Consultas.Configuracion.Previo,
            Convert.ToInt32(ddlEmpresa.SelectedItem.Value),
            Convert.ToInt32(ddlSucursalInterno.SelectedItem.Value),
            Convert.ToInt32(leerAño()),
            Convert.ToInt32(ddlFolioInterno.SelectedItem.Value));

        GenerarTablaDestinoDetalleInterno();
        LlenaGridViewDestinoDetalleInterno();
        lblFISelec.Text = ddlFolioInterno.SelectedItem.Text;
        grvVistaRapidaInterno_ModalPopupExtender.Show();
    }
    protected void btnAñadirFolio_Click(object sender, ImageClickEventArgs e)
    {
        datosArchivoInterno = objApp.DatosArchivo.CrearObjeto();//new DatosArchivoDatos(App.ImplementadorMensajes); //App.DatosArchivo
        datosArchivoInterno.Folio = Convert.ToInt32(ddlFolioInterno.SelectedItem.Value);
        datosArchivoInterno.Corporativo = Convert.ToInt32(ddlEmpresa.SelectedItem.Value);
        datosArchivoInterno.Sucursal = Convert.ToInt32(ddlSucursalInterno.SelectedItem.Value);
        datosArchivoInterno.Año = Convert.ToInt32(leerAño());
        datosArchivoInterno.MesConciliacion = Convert.ToSByte(leerMes());
        objApp.Conciliacion.AgregarArchivo(datosArchivoInterno, cConciliacion.Operacion.Nueva);
        LlenaGridViewFoliosAgregados();
    }
    protected void grvAgregados_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        objApp.Conciliacion.QuitarArchivo(objApp.Conciliacion.ListaArchivos.Single(x => x.Folio == Convert.ToInt32(grvAgregados.DataKeys[e.RowIndex].Value)), cConciliacion.Operacion.Nueva);
        LlenaGridViewFoliosAgregados();
    }
    protected void btnAceptarRegreso_Click(object sender, EventArgs e)
    {
        objApp.Conciliacion.ListaArchivos.Clear();
        tabNuevaConciliacion.Tabs[0].Enabled = true;
        tabNuevaConciliacion.Tabs[1].Enabled = false;
        tabNuevaConciliacion.ActiveTab = tabNuevaConciliacion.Tabs[0];
    }
    protected void ddlTipoConciliacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
        parametros = Session["Parametros"] as Parametros;
        AppSettingsReader settings = new AppSettingsReader();
        short modulo = Convert.ToSByte(settings.GetValue("Modulo", typeof(string)).ToString());
        string parametro = parametros.ValorParametro(modulo, "Cuenta Sobregiro");

        if (ddlTipoConciliacion.SelectedItem.Text.Equals("CONCILIACION DE INGRESOS POR COBRANZA A ABONAR") || ddlCuentaBancaria.SelectedItem.Text.Trim().Equals(parametro.Trim()) || ddlTipoConciliacion.SelectedItem.Text.Equals("CONCILIACION DE INGRESOS Y EGRESOS SIN ARCHIVO"))
        //if (ddlTipoConciliacion.SelectedItem.Text.Equals("CONCILIACION DE INGRESOS POR COBRANZA A ABONAR"))
        {
            tabNuevaConciliacion.Tabs[2].Visible = false;
            btnGuardarConciliacionTipo2.Visible = true;
            btnSiguienteExterno.Visible = false;
        }
        else
        {
            tabNuevaConciliacion.Tabs[2].Visible = true;
            btnGuardarConciliacionTipo2.Visible = false;
            btnSiguienteExterno.Visible = true;
        }

        if(ddlTipoConciliacion.SelectedItem.Text.Equals("CONCILIACION DE INGRESOS POR COBRANZA A ABONAR PEDIDO"))
        {
            tabNuevaConciliacion.Tabs[2].Visible = false;
            btnGuardarConciliacionTipo2.Visible = true;
            btnSiguienteExterno.Visible = false;
        }

    }

    protected void grvAgregados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager && (grvAgregados.DataSource != null))
        {
            Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumPaginas");
            _TotalPags.Text = grvAgregados.PageCount.ToString();

            DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
            for (int i = 1; i <= Convert.ToInt32(grvAgregados.PageCount); i++)
            {
                list.Items.Add(i.ToString());
            }
            list.SelectedValue = (grvAgregados.PageIndex + 1).ToString();
        }
    }
    protected void paginasDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag;
        grvAgregados.PageIndex = int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 &&
                                    iNumPag <= grvAgregados.PageCount
                                        ? iNumPag - 1
                                        : 0;
        LlenaGridViewFoliosAgregados();
    }
    protected void grvAgregados_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try { grvAgregados.PageIndex = e.NewPageIndex; LlenaGridViewFoliosAgregados(); }
        catch (Exception ex) { }
    }
    #endregion
    protected void ddlCuentaBancaria_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            parametros = Session["Parametros"] as Parametros;
            AppSettingsReader settings = new AppSettingsReader();
            short modulo = Convert.ToSByte(settings.GetValue("Modulo", typeof(string)).ToString());
            string parametro = parametros.ValorParametro(modulo, "Cuenta Sobregiro");
            if (ddlCuentaBancaria.SelectedItem.Text.Trim().Equals(parametro.Trim()))
            {
                tabNuevaConciliacion.Tabs[2].Visible = false;
                btnGuardarConciliacionTipo2.Visible = true;
                btnSiguienteExterno.Visible = false;
            }
            else
            {
                tabNuevaConciliacion.Tabs[2].Visible = true;
                btnGuardarConciliacionTipo2.Visible = false;
                btnSiguienteExterno.Visible = true;
            }
        }
        catch (Exception ex)
        {

            objApp.ImplementadorMensajes.MostrarMensaje("Error: Seleccion de Cuenta Bancaria\n" + ex.Message);
        }
    }
    protected void ddlCuentaBancaria_DataBound(object sender, EventArgs e)
    {
        try
        {
            parametros = Session["Parametros"] as Parametros;
            AppSettingsReader settings = new AppSettingsReader();
            short modulo = Convert.ToSByte(settings.GetValue("Modulo", typeof(string)).ToString());
            string parametro = parametros.ValorParametro(modulo, "Cuenta Sobregiro");
            //if (ddlCuentaBancaria.SelectedItem == null) return;
            if (ddlCuentaBancaria.SelectedItem.Text.Trim().Equals(parametro.Trim()))
            {
                tabNuevaConciliacion.Tabs[2].Visible = false;
                btnGuardarConciliacionTipo2.Visible = true;
                btnSiguienteExterno.Visible = false;
            }
            else
            {
                tabNuevaConciliacion.Tabs[2].Visible = true;
                btnGuardarConciliacionTipo2.Visible = false;
                btnSiguienteExterno.Visible = true;
            }
        }
        catch (NullReferenceException ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("El Banco Seleccionado no tiene asociada ninguna Cuenta Bancaria.");
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error Cargar Cuenta Bancaria:\n" + ex.Message);
        }

    }
}