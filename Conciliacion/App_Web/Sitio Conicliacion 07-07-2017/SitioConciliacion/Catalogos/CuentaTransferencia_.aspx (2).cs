using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CatalogoConciliacion;
using CatalogoConciliacion.ReglasNegocio;
using System.Data;
using Conciliacion.RunTime.ReglasDeNegocio;
using SeguridadCB.Public;



public partial class Catalogos_CuentaTransferencia_ : System.Web.UI.Page
{

    #region "Propiedades Globales"
    private SeguridadCB.Public.Usuario usuario;
    #endregion

    #region "Propiedades Globales"
    private SeguridadCB.Public.Operaciones operaciones;
    #endregion

    private List<ListaCombo> listSucursales = new List<ListaCombo>();
    private List<ListaCombo> listCuentaBancaria = new List<ListaCombo>();
    private List<ListaCombo> listBancos = new List<ListaCombo>();

    public static List<CuentaTransferencia> ListConsultaTransferencia = new List<CuentaTransferencia>();

    private DataTable tblCuentaTransferencia = new DataTable("Transferencia");
    //private DataTable tblDestinoDetalle = new DataTable("VistaDestinoDetalle");

    protected override void OnPreInit(EventArgs e)
    {
        if (HttpContext.Current.Session["Operaciones"] == null)
            Response.Redirect("~/Acceso/Acceso.aspx", true);
        else
            operaciones = (SeguridadCB.Public.Operaciones)HttpContext.Current.Session["Operaciones"];
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Conciliacion.RunTime.App.ImplementadorMensajes.ContenedorActual = this;
        try
        {
           
            //Llamamos a la clase app perteneciente a libreria de clases donde estamos apuntando
            CatalogoConciliacion.App.ImplementadorMensajes.ContenedorActual = this;
            if (!IsPostBack)
            {
                usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
                if (cboCorporativoOrigen.Items.Count == 0) Carga_CorporativoOrigen();
                Carga_BancoNombreOrigen(Convert.ToInt16(cboCorporativoOrigen.SelectedItem.Value));
                Carga_CuentaBancariaOrigen(Convert.ToInt16(cboCorporativoOrigen.SelectedItem.Value), Convert.ToSByte(cboNombreBancoOrigen.SelectedItem.Value));
                
                this.grdTranferencias.DataSource = tblCuentaTransferencia;
                this.grdTranferencias.DataBind();
                
                //Parametros para generar consultas
                Consulta_TablaCuentasTranferencia(Convert.ToInt16(cboCuentaBancoOrigen.SelectedItem.Value),
                                Convert.ToInt16(cboSucursalOrigen.SelectedItem.Value),
                                cboCuentaBancoOrigen.SelectedItem.Text.Trim());
                GenerarTablaCuentasTransferencia();
                LlenaGridViewTablaCuentasTransferencias();
            }
            
            this.cboCorporativoOrigen.Focus();

        }
        catch (Exception ex)
        {
           //App.ImplementadorMensajes.MostrarMensaje("Error\n"+ex.Message);
        }

    }

    //Consulta tabla principal CuentaTransferencias
    public void Consulta_TablaCuentasTranferencia(short corporativoOrigen, int sucursalOrigen, string cuentaBancoOrigen)
    {
        System.Data.SqlClient.SqlConnection Connection = SeguridadCB.Seguridad.Conexion;
        if (Connection.State == ConnectionState.Closed)
        {
            SeguridadCB.Seguridad.Conexion.Open();
            Connection = SeguridadCB.Seguridad.Conexion;
        }
        try
        {
            ListConsultaTransferencia = CatalogoConciliacion.App.Consultas.ObtenieneCuentasTransferenciaOrigenDestino(0,corporativoOrigen, sucursalOrigen, cuentaBancoOrigen,0);
            
            
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error\n" + ex.Message);
        }
    }

    //Consulta tabla principal CuentaTransferencias
    public void Consulta_TablaCuentasTranferenciaTodas(short corporativoOrigen, int sucursalOrigen, int cuentaBancoOrigen)
    {
        System.Data.SqlClient.SqlConnection Connection = SeguridadCB.Seguridad.Conexion;
        if (Connection.State == ConnectionState.Closed)
        {
            SeguridadCB.Seguridad.Conexion.Open();
            Connection = SeguridadCB.Seguridad.Conexion;
        }
        try
        {
            ListConsultaTransferencia = CatalogoConciliacion.App.Consultas.ObtenieneCuentasTransferenciaOrigenDestinoTodas(1, corporativoOrigen,sucursalOrigen,"", cuentaBancoOrigen);
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error\n" + ex.Message);
        }
    }

    //Genera la tabla en CuentaTransferencias
    public void GenerarTablaCuentasTransferencia()
    {
        tblCuentaTransferencia.Columns.Add("CuentaTransferenciaId", typeof(int));
        tblCuentaTransferencia.Columns.Add("CorporativoOrigenDesc", typeof(string));
        tblCuentaTransferencia.Columns.Add("CorporativoOrigen", typeof(int));
        tblCuentaTransferencia.Columns.Add("SucursalOrigenDesc", typeof (string));
        tblCuentaTransferencia.Columns.Add("SucursalOrigen", typeof(int));
        tblCuentaTransferencia.Columns.Add("CuentaBancoOrigen", typeof(string));
        tblCuentaTransferencia.Columns.Add("BancoOrigen", typeof(int));
        tblCuentaTransferencia.Columns.Add("BancoNombreOrigen", typeof(string));
        tblCuentaTransferencia.Columns.Add("CorporativoDestinoDesc", typeof (string));
        tblCuentaTransferencia.Columns.Add("CorporativoDestino", typeof(int));
        tblCuentaTransferencia.Columns.Add("SucursalDestinoDesc", typeof (string));
        tblCuentaTransferencia.Columns.Add("SucursalDestino", typeof(int));
        tblCuentaTransferencia.Columns.Add("CuentaBancoDestino", typeof(string));
        tblCuentaTransferencia.Columns.Add("BancoDestino", typeof(int));
        tblCuentaTransferencia.Columns.Add("BancoNombreDestino", typeof(string));
        tblCuentaTransferencia.Columns.Add("Status", typeof(string));

        foreach (CuentaTransferencia  t in ListConsultaTransferencia)
        {
            tblCuentaTransferencia.Rows.Add(
                t.CuentaTransferencia_,
                t.CorporativoOrigenDesc,
                t.CorporativoOrigen,
                t.SucursalOrigenDesc,
                t.SucursalOrigen,
                t.CuentaBancoOrigen,
                t.BancoOrigen,
                t.BancoNombreOrigen,
                t.CorporativoDestinoDesc,
                t.CorporativoDestino,
                t.SucursalDestinoDesc,
                t.SucursalDestino,
                t.CuentaBancoDestino,
                t.BancoDestino,
                t.BancoNombreDestino,
                t.Status);
        }
        //Variable de sesion creada para asumir datos
        HttpContext.Current.Session["TAB_CuentaTransferencia"] = tblCuentaTransferencia;

    }
    //Llena el Gridview Transacciones Concilidadas
    private void LlenaGridViewTablaCuentasTransferencias()
    {
        DataTable tablaCuentaTransferencia = (DataTable)HttpContext.Current.Session["TAB_CuentaTransferencia"];
        grdTranferencias.DataSource = tablaCuentaTransferencia;
        grdTranferencias.DataBind();
    }

    //Metodos para proporcionar valores a los combos pagina principal Destinos
    #region Metodos Pagina CuentaTransferencia
    public void Carga_CorporativoOrigen()
    {
        try
        {
            DataTable dtEmpresas = new DataTable();
            Usuario usuario;
            usuario = (Usuario)HttpContext.Current.Session["Usuario"];
            dtEmpresas = usuario.CorporativoAcceso;
            this.cboCorporativoOrigen.DataSource = dtEmpresas;
            this.cboCorporativoOrigen.DataValueField = "Corporativo";
            this.cboCorporativoOrigen.DataTextField = "NombreCorporativo";
            this.cboCorporativoOrigen.DataBind();
            dtEmpresas.Dispose();

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error\n" + ex.Message);
        }
    }

   public void Carga_SucursalCorporativoOrigen(int corporativo)
    {
        try
        {
            listSucursales = Conciliacion.RunTime.App.Consultas.ConsultaSucursales(Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionIden0.Sin0, corporativo);
            this.cboSucursalOrigen.DataSource = listSucursales;
            this.cboSucursalOrigen.DataValueField = "Identificador";
            this.cboSucursalOrigen.DataTextField = "Descripcion";
            this.cboSucursalOrigen.DataBind();
            this.cboSucursalOrigen.Dispose();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error\n" + ex.Message);
        }
    }

   
   public void Carga_BancoNombreOrigen(int corporativo)
   {
       try
       {

           listBancos = Conciliacion.RunTime.App.Consultas.ConsultaBancos(corporativo);
           this.cboNombreBancoOrigen.DataSource = listBancos;
           this.cboNombreBancoOrigen.DataValueField = "Identificador";
           this.cboNombreBancoOrigen.DataTextField = "Descripcion";
           this.cboNombreBancoOrigen.DataBind();
           this.cboNombreBancoOrigen.Dispose();
       }
       catch (Exception ex)
       {
           App.ImplementadorMensajes.MostrarMensaje("Error\n" + ex.Message);
       }
   }
    
   public void Carga_CuentaBancariaOrigen(int corporativo, short banco)
   {
       try
       {

           listCuentaBancaria = Conciliacion.RunTime.App.Consultas.ConsultaCuentasBancaria(corporativo, banco);
           
           ListaCombo dato = new ListaCombo(0, "VER TODOS", "TODAS LAS CUENTAS");
           listCuentaBancaria.Insert(0,dato);

           this.cboCuentaBancoOrigen.DataSource = listCuentaBancaria;
           this.cboCuentaBancoOrigen.DataValueField = "Identificador";
           this.cboCuentaBancoOrigen.DataTextField = "Descripcion";
           this.cboCuentaBancoOrigen.DataBind();
           this.cboCuentaBancoOrigen.Dispose();


       }
       catch (Exception)
       {

           this.cboCuentaBancoOrigen.DataSource = new List<ListaCombo>();
           this.cboCuentaBancoOrigen.DataBind();
           this.cboCuentaBancoOrigen.Dispose();
       }
   }
    #endregion

   //Metodos para proporcionar valores a los combos pop UP Origen
   #region Metodos Pop Up Origen
    
    public void Carga_CorporativoOrigen_()
    {
        try
        {
            DataTable dtEmpresas = new DataTable();
            Usuario usuario;
            usuario = (Usuario)HttpContext.Current.Session["Usuario"];
            dtEmpresas = usuario.CorporativoAcceso;
            this.cboCorporativoOrigen_.DataSource = dtEmpresas;
            this.cboCorporativoOrigen_.DataValueField = "Corporativo";
            this.cboCorporativoOrigen_.DataTextField = "NombreCorporativo";
            this.cboCorporativoOrigen_.DataBind();
            dtEmpresas.Dispose();

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error\n" + ex.Message);
        }
    }

    public void Carga_SucursalCorporativoOrigen_(int corporativo)
    {
        try
        {
            listSucursales = Conciliacion.RunTime.App.Consultas.ConsultaSucursales(Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionIden0.Sin0, corporativo);
            this.cboSucursalOrigen_.DataSource = listSucursales;
            this.cboSucursalOrigen_.DataValueField = "Identificador";
            this.cboSucursalOrigen_.DataTextField = "Descripcion";
            this.cboSucursalOrigen_.DataBind();
            this.cboSucursalOrigen_.Dispose();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error\n" + ex.Message);
        }
    }

    public void Carga_BancoNombreOrigen_(int corporativo)
    {
        try
        {

            listBancos = Conciliacion.RunTime.App.Consultas.ConsultaBancos(corporativo);
            this.cboNombreBancoOrigen_.DataSource = listBancos;
            this.cboNombreBancoOrigen_.DataValueField = "Identificador";
            this.cboNombreBancoOrigen_.DataTextField = "Descripcion";
            this.cboNombreBancoOrigen_.DataBind();
            this.cboNombreBancoOrigen_.Dispose();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error\n" + ex.Message);
        }
    }

    //Carga CTADestino 2
    public void Carga_CuentaBancariaOrigen_(int corporativo, short banco)
    {
        try
        {

            listCuentaBancaria = Conciliacion.RunTime.App.Consultas.ConsultaCuentasBancaria(corporativo, banco);
            this.cboCuentaBancoOrigen_.DataSource = listCuentaBancaria;
            this.cboCuentaBancoOrigen_.DataValueField = "Identificador";
            this.cboCuentaBancoOrigen_.DataTextField = "Descripcion";
            this.cboCuentaBancoOrigen_.DataBind();
            this.cboCuentaBancoOrigen_.Dispose();


        }
        catch (Exception)
        {

            this.cboCuentaBancoOrigen_.DataSource = new List<ListaCombo>();
            this.cboCuentaBancoOrigen_.DataBind();
            this.cboCuentaBancoOrigen_.Dispose();
        }
    }
   #endregion

    //Metodos para proporcionar valores a los combos pop UP Derstino
    #region Metodos Pop Up Destino
    
    public void Carga_CorporativoDestino_()
    {
        try
        {
            DataTable dtEmpresas = new DataTable();
            Usuario usuario;
            usuario = (Usuario)HttpContext.Current.Session["Usuario"];
            dtEmpresas = usuario.CorporativoAcceso;
            this.cboCorporativoDestino_.DataSource = dtEmpresas;
            this.cboCorporativoDestino_.DataValueField = "Corporativo";
            this.cboCorporativoDestino_.DataTextField = "NombreCorporativo";
            this.cboCorporativoDestino_.DataBind();
            dtEmpresas.Dispose();

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error\n" + ex.Message);
        }
    }

    public void Carga_SucursalCorporativoDestino_(int corporativo)
    {
        try
        {
            listSucursales = Conciliacion.RunTime.App.Consultas.ConsultaSucursales(Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionIden0.Sin0, corporativo);
            this.cboSucursalDestino_.DataSource = listSucursales;
            this.cboSucursalDestino_.DataValueField = "Identificador";
            this.cboSucursalDestino_.DataTextField = "Descripcion";
            this.cboSucursalDestino_.DataBind();
            this.cboSucursalDestino_.Dispose();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error\n" + ex.Message);
        }
    }

    //Carga nombrebancoDestino2
    public void Carga_BancoNombreDestino_(int corporativo)
    {
        try
        {

            listBancos = Conciliacion.RunTime.App.Consultas.ConsultaBancos(corporativo);
            this.cboNombreBancoDestino_.DataSource = listBancos;
            this.cboNombreBancoDestino_.DataValueField = "Identificador";
            this.cboNombreBancoDestino_.DataTextField = "Descripcion";
            this.cboNombreBancoDestino_.DataBind();
            this.cboNombreBancoDestino_.Dispose();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error\n" + ex.Message);
        }
    }

    //Carga CTAOrigen
    public void Carga_CuentaBancariaDestino_(int corporativo, short banco)
    {
        try
        {

            listCuentaBancaria = Conciliacion.RunTime.App.Consultas.ConsultaCuentasBancaria(corporativo, banco);
            this.cboCuentaBancoDestino_.DataSource = listCuentaBancaria;
            this.cboCuentaBancoDestino_.DataValueField = "Identificador";
            this.cboCuentaBancoDestino_.DataTextField = "Descripcion";
            this.cboCuentaBancoDestino_.DataBind();
            this.cboCuentaBancoDestino_.Dispose();


        }
        catch (Exception)
        {

            this.cboCuentaBancoDestino_.DataSource = new List<ListaCombo>();
            this.cboCuentaBancoDestino_.DataBind();
            this.cboCuentaBancoDestino_.Dispose();
        }
    }
    #endregion



    //Nuevo codigo
    protected void cboCorporativoOrigen_DataBound(object sender, EventArgs e)
    {
        Carga_SucursalCorporativoOrigen(Convert.ToInt32(cboCorporativoOrigen.SelectedItem.Value));
    }
    protected void cboCorporativoOrigen_SelectedIndexChanged(object sender, EventArgs e)
    {
        Carga_SucursalCorporativoOrigen(Convert.ToInt32(cboCorporativoOrigen.SelectedItem.Value));
        Carga_BancoNombreOrigen(Convert.ToInt16(cboCorporativoOrigen.SelectedItem.Value));
    }
    protected void cboNombreBancoOrigen_SelectedIndexChanged(object sender, EventArgs e)
    {
        Carga_CuentaBancariaOrigen(Convert.ToInt16(cboCorporativoOrigen.SelectedItem.Value), Convert.ToSByte(cboNombreBancoOrigen.SelectedItem.Value));
    }


    protected void cboCorporativoOrigen__DataBound(object sender, EventArgs e)
    {
        Carga_SucursalCorporativoOrigen_(Convert.ToInt32(cboCorporativoOrigen_.SelectedItem.Value));
    }
    protected void cboCorporativoOrigen__SelectedIndexChanged(object sender, EventArgs e)
    {
        Carga_SucursalCorporativoOrigen_(Convert.ToInt32(cboCorporativoOrigen_.SelectedItem.Value));
        Carga_BancoNombreOrigen_(Convert.ToInt16(cboCorporativoOrigen_.SelectedItem.Value));
    }
    protected void cboNombreBancoOrigen__SelectedIndexChanged(object sender, EventArgs e)
    {
        Carga_CuentaBancariaOrigen_(Convert.ToInt16(cboCorporativoOrigen_.SelectedItem.Value), Convert.ToByte(cboNombreBancoOrigen_.SelectedItem.Value));
    }


    protected void cboCorporativoDestino__DataBound(object sender, EventArgs e)
    {
        Carga_SucursalCorporativoDestino_(Convert.ToInt32(cboCorporativoOrigen.SelectedItem.Value));
    }
    protected void cboCorporativoDestino__SelectedIndexChanged(object sender, EventArgs e)
    {
        Carga_SucursalCorporativoDestino_(Convert.ToInt32(cboCorporativoOrigen.SelectedItem.Value));
        Carga_BancoNombreDestino_(Convert.ToInt16(cboCorporativoOrigen.SelectedItem.Value));
    }
    protected void cboNombreBancoDestino__SelectedIndexChanged(object sender, EventArgs e)
    {
        Carga_CuentaBancariaDestino_(Convert.ToInt16(cboCorporativoDestino_.SelectedItem.Value), Convert.ToByte(cboNombreBancoDestino_.SelectedItem.Value));
    }

    
    
    protected void btnConsultar_Click1(object sender, EventArgs e)
    {
        try
        {
            if (cboCuentaBancoOrigen.SelectedItem.Text.Trim().Equals("VER TODOS"))
            {
                Consulta_TablaCuentasTranferenciaTodas(Convert.ToInt16(cboCorporativoOrigen.SelectedItem.Value),
                                            Convert.ToInt16(cboSucursalOrigen.SelectedItem.Value),
                                            Convert.ToInt16(cboNombreBancoOrigen.SelectedItem.Value)
                                            );
            }

            else 
            {
                Consulta_TablaCuentasTranferencia(Convert.ToInt16(cboCorporativoOrigen.SelectedItem.Value),
                                            Convert.ToInt16(cboSucursalOrigen.SelectedItem.Value),
                                            cboCuentaBancoOrigen.SelectedItem.Text.Trim()
                                            );
            }
            
            GenerarTablaCuentasTransferencia();
            LlenaGridViewTablaCuentasTransferencias();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }
    }
    protected void btnAgregar_Click1(object sender, EventArgs e)
    {
        popUpAgregarTransferencia.Show();

        try
        {
           // if (cboCorporativoOrigen.Items.Count == 0 && cboCorporativoDestino_.Items.Count == 0)
            Carga_CorporativoOrigen_();
            Carga_CorporativoDestino_();
            Carga_BancoNombreOrigen_(Convert.ToInt16(cboCorporativoOrigen_.SelectedItem.Value));
            Carga_CuentaBancariaOrigen_(Convert.ToInt16(cboCorporativoOrigen_.SelectedItem.Value),
                                       Convert.ToSByte(cboNombreBancoOrigen_.SelectedItem.Value));
            Carga_BancoNombreDestino_(Convert.ToInt16(cboCorporativoDestino_.SelectedItem.Value));
            Carga_CuentaBancariaDestino_(Convert.ToInt16(cboCorporativoDestino_.SelectedItem.Value),
                                         Convert.ToSByte(cboNombreBancoDestino_.SelectedItem.Value));
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnGuardarInterno_Click(object sender, EventArgs e)
    {
    
        usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
        CuentaTransferencia cta = CatalogoConciliacion.App.ReferenciaCuentaTransferencia.CrearObjeto();
        cta.CuentaTransferencia_ = 0;
        cta.CorporativoOrigen = Convert.ToInt16(cboCorporativoOrigen_.SelectedItem.Value);
        cta.SucursalOrigen = Convert.ToInt16(cboSucursalOrigen_.SelectedItem.Value);
        cta.CuentaBancoOrigen = cboCuentaBancoOrigen_.SelectedItem.Text.Trim();
        cta.BancoOrigen = Convert.ToInt16(cboNombreBancoOrigen_.SelectedItem.Value);
        cta.BancoNombreOrigen = cboNombreBancoOrigen_.SelectedItem.Text.Trim();
        cta.CorporativoDestino = Convert.ToInt16(cboCorporativoDestino_.SelectedItem.Value);
        cta.SucursalDestino = Convert.ToInt16(cboSucursalDestino_.SelectedItem.Value);
        cta.CuentaBancoDestino = cboCuentaBancoDestino_.SelectedItem.Text.Trim();
        cta.BancoDestino = Convert.ToInt16(cboNombreBancoDestino_.SelectedItem.Value);
        cta.BancoNombreDestino = cboNombreBancoDestino_.SelectedItem.Text.Trim();
        cta.Status = "ACTIVO";
        cta.UsuarioAlta = usuario.IdUsuario;
        cta.FAlta_ = DateTime.Now;
        if (cta.Registrar())
        {
            try
            {
                //Refrescamos el DGV
                Consulta_TablaCuentasTranferencia(Convert.ToInt16(cboCorporativoOrigen.SelectedItem.Value),
                                    Convert.ToInt16(cboSucursalOrigen.SelectedItem.Value),
                                    cboCuentaBancoOrigen.SelectedItem.Text.Trim());
                GenerarTablaCuentasTransferencia();
                LlenaGridViewTablaCuentasTransferencias();

            }
            catch (Exception)
            {

                this.grdTranferencias.DataSource = tblCuentaTransferencia;
                this.grdTranferencias.DataBind();
            }
            popUpAgregarTransferencia.Hide();
            popUpAgregarTransferencia.Dispose();
            
        }
        
    }
    protected void grdTranferencias_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int status;
            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            Button btn = (Button)(e.Row.FindControl("btnStatus"));
            if (lblStatus.Text.Equals("ACTIVO"))
            {
                btn.CssClass = "boton activo";
                btn.ToolTip = "DESACTIVAR";
            }
            else
            {
                btn.CssClass = "boton inactivo";
                btn.ToolTip = "ACTIVAR";
            }
        }
        //Total de paginas
        if (e.Row.RowType == DataControlRowType.Pager && (grdTranferencias.DataSource != null))
        {
            //TRAE EL TOTAL DE PAGINAS
            Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumPaginas");
            _TotalPags.Text = grdTranferencias.PageCount.ToString();

            //LLENA LA LISTA CON EL NUMERO DE PAGINAS
            DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
            for (int i = 1; i <= Convert.ToInt32(grdTranferencias.PageCount); i++)
            {
                list.Items.Add(i.ToString());
            }
            list.SelectedValue = (grdTranferencias.PageIndex + 1).ToString();
        }
    }
    //Muestra Total de paginas
    protected void paginasDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag = 0;

        if (int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 && iNumPag <= grdTranferencias.PageCount)
        {
            grdTranferencias.PageIndex = iNumPag - 1;
        }
        else
        {
            grdTranferencias.PageIndex = 0;
        }

        LlenaGridViewTablaCuentasTransferencias();
    }

    protected void grdTranferencias_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("CAMBIARSTATUS"))
        {
            GridViewRow gRow = (GridViewRow)(e.CommandSource as Button).Parent.Parent;
            CatalogoConciliacion.App.ReferenciaCuentaTransferencia.CambiarStatus(Convert.ToInt16(grdTranferencias.DataKeys[gRow.RowIndex].Values["CuentaTransferenciaId"]));
            
            //Refrescamos el DGV
            Consulta_TablaCuentasTranferencia(Convert.ToInt16(cboCorporativoOrigen.SelectedItem.Value),
                                Convert.ToInt16(cboSucursalOrigen.SelectedItem.Value),
                                cboCuentaBancoOrigen.SelectedItem.Text.Trim());
            GenerarTablaCuentasTransferencia();
            LlenaGridViewTablaCuentasTransferencias();
        }
    }
    protected void grdTranferencias_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdTranferencias.PageIndex = e.NewPageIndex;
            LlenaGridViewTablaCuentasTransferencias();
        }
        catch (Exception)
        {

        }
    }
}