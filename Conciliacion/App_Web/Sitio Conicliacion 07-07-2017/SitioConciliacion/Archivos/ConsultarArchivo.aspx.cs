using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SeguridadCB.Public;
using Conciliacion.RunTime;
using Conciliacion.RunTime.ReglasDeNegocio;


public partial class Archivos_Consultar : System.Web.UI.Page
{
    Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
    #region "Propiedades Globales"
    private SeguridadCB.Public.Usuario usuario;
    private SeguridadCB.Public.Operaciones operaciones;
    
    #endregion

    #region "Propiedades privadas"

    private List<ListaCombo> listSucursales = new List<ListaCombo>();
    private List<ListaCombo> listAñoConciliacion = new List<ListaCombo>();
    private List<ListaCombo> listTipoFuenteInformacion = new List<ListaCombo>();
    private List<ListaCombo> listCuentaBancaria = new List<ListaCombo>();
    private List<ListaCombo> listBancos = new List<ListaCombo>();
    public static List<ListaCombo> listFoliosExternoInternos = new List<ListaCombo>();
    public static List<DatosArchivoDetalle> listaDestinoDetalle = new List<DatosArchivoDetalle>();
    public static decimal totalDeposito, totalRetiro;

    private DataTable tblDestino = new DataTable("VistaFoliosExternoInterno");
    private DataTable tblDestinoDetalle = new DataTable("VistaDestinoDetalle");
    #endregion
    protected override void OnPreInit(EventArgs e)
    {
        if (HttpContext.Current.Session["Operaciones"] == null)
            Response.Redirect("~/Acceso/Acceso.aspx", true);
        else
            operaciones = (SeguridadCB.Public.Operaciones)HttpContext.Current.Session["Operaciones"];
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
            objApp.ImplementadorMensajes.ContenedorActual = this;
            if (!IsPostBack)
            {
                usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
                if (ddlEmpresa.Items.Count == 0) Carga_Corporativo();
                Carga_AñoConciliacion();
                Carga_TipoFuenteInformacion();
                Carga_Banco(Convert.ToInt16(ddlEmpresa.SelectedItem.Value));
                Carga_CuentaBancaria(Convert.ToInt16(ddlEmpresa.SelectedItem.Value), Convert.ToSByte(cboBancoFinanciero.SelectedItem.Value));

                Consulta_FoliosTablaDestino(Convert.ToInt16(ddlEmpresa.SelectedItem.Value),
                                Convert.ToInt16(ddlSucursal.SelectedItem.Value),
                                Convert.ToInt16(ddlAnio.SelectedItem.Value),
                                Convert.ToSByte(ddlMes.SelectedItem.Value),
                                cboCuentaFinanciero.SelectedItem.Text,
                                Convert.ToSByte(ddlTipoFuente.SelectedItem.Value));
                GenerarTablaFolios();
                LlenaGridViewTablaDestino();

            }

            this.ddlEmpresa.Focus();

        }
        catch (Exception ex)
        {
            //App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }
    }


    #region "Funciones privadas"

    //Consulta transacciones conciliadas
    public void Consulta_FoliosTablaDestino(int corporativo, int sucursal, int añoF, short mesF, string cuentabancaria, short tipofuenteinformacion)
    {
        Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
        SeguridadCB.Seguridad seguridad = new SeguridadCB.Seguridad();
        System.Data.SqlClient.SqlConnection Connection = seguridad.Conexion;
        if (Connection.State == ConnectionState.Closed)
        {
            seguridad.Conexion.Open();
            Connection = seguridad.Conexion;
        }
        try
        {
            listFoliosExternoInternos = objApp.Consultas.ConsultaFoliosTablaDestino(corporativo, sucursal, añoF, mesF, cuentabancaria, tipofuenteinformacion);
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error\n" + ex.Message);
        }
    }
    //Genera la tabla de transacciones Conciliadas
    public void GenerarTablaFolios()
    {
        tblDestino = new DataTable("TransaccionesConciladas");
        tblDestino.Columns.Add("Identificador", typeof(int));
        tblDestino.Columns.Add("Descripcion", typeof(string));
        tblDestino.Columns.Add("Status", typeof(string));
        tblDestino.Columns.Add("Campo2", typeof(string));

        foreach (ListaCombo rc in listFoliosExternoInternos)
        {
            tblDestino.Rows.Add(
                rc.Identificador,
                rc.Descripcion,
                rc.Campo1,
                rc.Campo2);
        }

        HttpContext.Current.Session["TAB_DESTINO"] = tblDestino;

    }
    //Llena el Gridview Transacciones Concilidadas
    private void LlenaGridViewTablaDestino()
    {
        DataTable tablaConciliadas = (DataTable)HttpContext.Current.Session["TAB_DESTINO"];
        grvTablaDestino.DataSource = tablaConciliadas;
        grvTablaDestino.DataBind();

    }


    /// <summary>
    ///Consulta el detalle del Folio Externo
    /// </summary>
    public void Consulta_TablaDestinoDetalle(Conciliacion.RunTime.ReglasDeNegocio.Consultas.Configuracion configuracion, int empresa, int sucursal, int año, int folioExterno)//Lee el metodo que llena la lista con las conciliaciones
    {
        Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
        SeguridadCB.Seguridad seguridad = new SeguridadCB.Seguridad();

        System.Data.SqlClient.SqlConnection Connection = seguridad.Conexion;
        if (Connection.State == ConnectionState.Closed)
        {
            seguridad.Conexion.Open();
            Connection = seguridad.Conexion;
        }
        try
        {
            listaDestinoDetalle = objApp.Consultas.ConsultaTablaDestinoDetalle(
                        configuracion,
                        empresa,
                        sucursal,
                        año,
                        folioExterno);
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }

    }
    /// <summary>
    /// Llena el VistaRapida[TablaDestinoDetalleExterno] x Folio
    /// </summary>
    public void GenerarTablaDestinoDetalle()//Genera la tabla de destino detalle[Vista Rapida]
    {
        tblDestinoDetalle.Columns.Add("Folio", typeof(int));
        tblDestinoDetalle.Columns.Add("FOperacion", typeof(DateTime));
        tblDestinoDetalle.Columns.Add("FMovimiento", typeof(DateTime));
        tblDestinoDetalle.Columns.Add("Referencia", typeof(string));
        tblDestinoDetalle.Columns.Add("Descripcion", typeof(string));
        tblDestinoDetalle.Columns.Add("Deposito", typeof(float));
        tblDestinoDetalle.Columns.Add("Retiro", typeof(float));
        tblDestinoDetalle.Columns.Add("Concepto", typeof(string));

        foreach (DatosArchivoDetalle da in listaDestinoDetalle)
        {
            tblDestinoDetalle.Rows.Add(
                da.Folio,
                da.FOperacion,
                da.FMovimiento,
                da.Referencia,
                da.Descripcion,
                da.Deposito,
                da.Retiro,
                da.Concepto);

            totalDeposito += da.Deposito;
            totalRetiro += da.Retiro;

        }
        asignarTotales();
        HttpContext.Current.Session["DESTINO_DETALLE"] = tblDestinoDetalle;
    }

    private void LlenaGridViewDestinoDetalle()//Llena el gridview con las conciliaciones antes leídas
    {
        DataTable tablaDestinoDetalle = (DataTable)HttpContext.Current.Session["DESTINO_DETALLE"];
        this.grvDestinoDetalle.DataSource = tablaDestinoDetalle;
        this.grvDestinoDetalle.DataBind();
    }

    public void asignarTotales()
    {
        lblTotalDeposito.Text = Decimal.Round(totalDeposito, 2).ToString("C2");
        lblTotalRetiro.Text = Decimal.Round(totalRetiro, 2).ToString("C2");
        totalDeposito = totalRetiro = 0.0M;
    }

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
    public void Carga_SucursalCorporativo(int corporativo)
    {
        try
        {
            Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
            listSucursales = objApp.Consultas.ConsultaSucursales(Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionIden0.Sin0, corporativo);
            this.ddlSucursal.DataSource = listSucursales;
            this.ddlSucursal.DataValueField = "Identificador";
            this.ddlSucursal.DataTextField = "Descripcion";
            this.ddlSucursal.DataBind();
            this.ddlSucursal.Dispose();
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
            Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
            listBancos = objApp.Consultas.ConsultaBancos(corporativo);
            this.cboBancoFinanciero.DataSource = listBancos;
            this.cboBancoFinanciero.DataValueField = "Identificador";
            this.cboBancoFinanciero.DataTextField = "Descripcion";
            this.cboBancoFinanciero.DataBind();
            this.cboBancoFinanciero.Dispose();
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
            Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
            listCuentaBancaria = objApp.Consultas.ConsultaCuentasBancaria(corporativo, banco);
            this.cboCuentaFinanciero.DataSource = listCuentaBancaria;
            this.cboCuentaFinanciero.DataValueField = "Identificador";
            this.cboCuentaFinanciero.DataTextField = "Descripcion";
            this.cboCuentaFinanciero.DataBind();
            this.cboCuentaFinanciero.Dispose();


        }
        catch (Exception)
        {

            this.cboCuentaFinanciero.DataSource = new List<ListaCombo>();
            this.cboCuentaFinanciero.DataBind();
            this.cboCuentaFinanciero.Dispose();
        }
    }
    public void Carga_AñoConciliacion()
    {
        try
        {
            Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
            listAñoConciliacion = objApp.Consultas.ConsultaAños();
            this.ddlAnio.DataSource = listAñoConciliacion;
            this.ddlAnio.DataValueField = "Identificador";
            this.ddlAnio.DataTextField = "Descripcion";
            this.ddlAnio.DataBind();
            this.ddlAnio.Dispose();
        }
        catch
        {
        }
    }
    public void Carga_TipoFuenteInformacion()
    {
        Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
        try
        {

            listTipoFuenteInformacion = objApp.Consultas.ConsultaTipoInformacionDatos(Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionTipoFuente.TipoFuenteInformacion);
            this.ddlTipoFuente.DataSource = listTipoFuenteInformacion;
            this.ddlTipoFuente.DataValueField = "Identificador";
            this.ddlTipoFuente.DataTextField = "Descripcion";
            this.ddlTipoFuente.DataBind();
            this.ddlTipoFuente.Dispose();
        }
        catch
        {
        }
    }

    #endregion

    #region "Funciones de formas"
    protected void ddlEmpresa_DataBound(object sender, EventArgs e)
    {
        Carga_SucursalCorporativo(Convert.ToInt32(ddlEmpresa.SelectedItem.Value));
    }
    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        Carga_SucursalCorporativo(Convert.ToInt32(ddlEmpresa.SelectedItem.Value));
        Carga_Banco(Convert.ToInt16(ddlEmpresa.SelectedItem.Value));
    }
    #endregion

    protected void grvTablaDestino_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvTablaDestino.PageIndex = e.NewPageIndex;
        LlenaGridViewTablaDestino();
    }
    protected void grvTablaDestino_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int folioSeleccionado = Convert.ToInt32(grvTablaDestino.DataKeys[e.NewSelectedIndex].Value);
        lblFLSelec.Text = folioSeleccionado.ToString();
        Consulta_TablaDestinoDetalle(ObtenerConfiguracionDestinoDetalle(), Convert.ToInt32(ddlEmpresa.SelectedItem.Value),
                                     Convert.ToInt32(ddlSucursal.SelectedItem.Value),
                                     Convert.ToInt16(ddlAnio.SelectedItem.Value), folioSeleccionado);
        GenerarTablaDestinoDetalle();
        LlenaGridViewDestinoDetalle();

        mpeDetalleDestino.Show();


    }
    protected void ddlConfiguracionDetalle_SelectedIndexChanged(object sender, EventArgs e)
    {
        Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
        try
        {
            Consulta_TablaDestinoDetalle(ObtenerConfiguracionDestinoDetalle(), Convert.ToInt32(ddlEmpresa.SelectedItem.Value),
                                            Convert.ToInt32(ddlSucursal.SelectedItem.Value),
                                            Convert.ToInt16(ddlAnio.SelectedItem.Value), Convert.ToInt16(lblFLSelec.Text));
            GenerarTablaDestinoDetalle();
            LlenaGridViewDestinoDetalle();

        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }

    }
    public Consultas.Configuracion ObtenerConfiguracionDestinoDetalle()
    {
        return Convert.ToInt16(ddlConfiguracionDetalle.SelectedItem.Value) == 0
                   ? Consultas.Configuracion.Previo
                   : Consultas.Configuracion.Todos;
    }

    protected void grvDestinoDetalle_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvDestinoDetalle.PageIndex = e.NewPageIndex;
        LlenaGridViewDestinoDetalle();
    }
    protected void cboBancoFinanciero_SelectedIndexChanged(object sender, EventArgs e)
    {
        Carga_CuentaBancaria(Convert.ToInt16(ddlEmpresa.SelectedItem.Value), Convert.ToSByte(cboBancoFinanciero.SelectedItem.Value));
    }

    protected void btnCerrarDetalle_Click(object sender, ImageClickEventArgs e)
    {
        mpeDetalleDestino.Hide();
        mpeDetalleDestino.Dispose();
    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        try
        {

            Consulta_FoliosTablaDestino(Convert.ToInt16(ddlEmpresa.SelectedItem.Value),
                                            Convert.ToInt16(ddlSucursal.SelectedItem.Value),
                                            Convert.ToInt16(ddlAnio.SelectedItem.Value),
                                            Convert.ToSByte(ddlMes.SelectedItem.Value),
                                            cboCuentaFinanciero.SelectedItem.Text,
                                            Convert.ToSByte(ddlTipoFuente.SelectedItem.Value));
            GenerarTablaFolios();
            LlenaGridViewTablaDestino();
        }
        catch (Exception ex)
        {
            //App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }
    }
}