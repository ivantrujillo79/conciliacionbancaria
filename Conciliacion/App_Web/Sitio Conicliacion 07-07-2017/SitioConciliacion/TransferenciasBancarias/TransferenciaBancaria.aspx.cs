using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Conciliacion.RunTime.ReglasDeNegocio;
using System.Data;
using SeguridadCB.Public;
using Conciliacion.RunTime;

public partial class TransferenciasBancarias_TransferenciaBancaria: System.Web.UI.Page
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

    public List<TransferenciaBancarias> listTransferenciasBancarias =new List<TransferenciaBancarias>();

    private DataTable tblTransferenciaBancarias = new DataTable("TransferenciaBancarias");

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
           Conciliacion.RunTime.App.ImplementadorMensajes.ContenedorActual = this;
            
            if (!IsPostBack)
            {
                usuario = (SeguridadCB.Public.Usuario) HttpContext.Current.Session["Usuario"];
                if (cboCorporativo.Items.Count == 0) Carga_Corporativo();
                Carga_BancoNombre(Convert.ToInt16(cboCorporativo.SelectedItem.Value));
                Carga_CuentaBancariaDestino(Convert.ToInt16(cboCorporativo.SelectedItem.Value),
                                            Convert.ToSByte(cboNombreBanco.SelectedItem.Value));
                this.grdTranferenciBancaria.DataSource = tblTransferenciaBancarias;
                this.grdTranferenciBancaria.DataBind();

                Consulta_TablaTranferenciaBancaria(Convert.ToInt16(cboCorporativo.SelectedItem.Value),
                                           Convert.ToInt16(cboSucursal.SelectedItem.Value),
                                           cboCuentaBancoOrigen.SelectedItem.Text.Trim(),
                                           Convert.ToInt32(leerAño()),
                                           Convert.ToInt16(leerMes()),
                                           cboStatus.SelectedItem.Text.Trim());

                GenerarTablaTransferenciaBancarias();
                LlenaGridViewTablaTransferenciasBancarias();

            }
            this.cboCorporativo.Focus();

        }
        catch (Exception ex)
        {
            //App.ImplementadorMensajes.MostrarMensaje("Error\n"+ex.Message);
        }

    }




    //Consulta tabla principal CuentaTransferencias
    public void Consulta_TablaTranferenciaBancaria(short corporativoorigen, int sucursalorigen, 
                                                                                      string cuentabancoorigen, int año, short mes, string status)
    {
        System.Data.SqlClient.SqlConnection Connection = SeguridadCB.Seguridad.Conexion;
        if (Connection.State == ConnectionState.Closed)
        {
            SeguridadCB.Seguridad.Conexion.Open();
            Connection = SeguridadCB.Seguridad.Conexion;
        }
        try
        {
            listTransferenciasBancarias = Conciliacion.RunTime.App.Consultas.ObtenieneTransferenciasBancarias(corporativoorigen, sucursalorigen,  cuentabancoorigen, año, mes, status);


        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error\n" + ex.Message);
        }
    }


    //Genera la tabla en CuentaTransferencias
    public void GenerarTablaTransferenciaBancarias()
    {
        tblTransferenciaBancarias.Columns.Add("CorporativoId", typeof(short));
        tblTransferenciaBancarias.Columns.Add("SucursalId", typeof(int));
        tblTransferenciaBancarias.Columns.Add("AñoId", typeof(int));
        tblTransferenciaBancarias.Columns.Add("FolioId", typeof(int));
        tblTransferenciaBancarias.Columns.Add("NombreCorporativo", typeof(string));
        tblTransferenciaBancarias.Columns.Add("NombreSucursal", typeof(string));
        tblTransferenciaBancarias.Columns.Add("TipoTransferencia", typeof(short));
        tblTransferenciaBancarias.Columns.Add("Referencia", typeof(string));
        tblTransferenciaBancarias.Columns.Add("FMovimiento", typeof(DateTime));
        tblTransferenciaBancarias.Columns.Add("FAplicacion", typeof(DateTime));
        tblTransferenciaBancarias.Columns.Add("UsuarioCaptura", typeof(string));
        tblTransferenciaBancarias.Columns.Add("FCaptura", typeof(DateTime));
        tblTransferenciaBancarias.Columns.Add("UsuarioAprobo", typeof(string));
        tblTransferenciaBancarias.Columns.Add("FAprobado", typeof(DateTime));
        tblTransferenciaBancarias.Columns.Add("Status", typeof(string));
        tblTransferenciaBancarias.Columns.Add("Descripcion", typeof (string));
        tblTransferenciaBancarias.Columns.Add("BancoNombreOrigen", typeof(string));
        tblTransferenciaBancarias.Columns.Add("CuentaBancoOrigen", typeof(string));
        tblTransferenciaBancarias.Columns.Add("BancoNombreDestino", typeof(string));
        tblTransferenciaBancarias.Columns.Add("CuentaBancoDestino", typeof(string));
        tblTransferenciaBancarias.Columns.Add("Monto", typeof(decimal));
        tblTransferenciaBancarias.Columns.Add("Entrada", typeof(short));
        //tblTransferenciaBancarias.Columns.Add("Abono", typeof(decimal));
        //tblTransferenciaBancarias.Columns.Add("Cargo", typeof(decimal));

        foreach (TransferenciaBancarias t in listTransferenciasBancarias)
        {
            tblTransferenciaBancarias.Rows.Add(
                t.Corporativo,
                t.Sucursal,
                t.Año,
                t.Folio,
                t.NombreCorporativo,
                t.NombreSucursal,
                t.TipoTransferencia,
                t.Referencia,
                t.FMovimiento,
                t.FAplicacion,
                t.UsuarioCaptura,
                t.FCaptura,
                t.UsuariooAprobo,
                t.FAprobado,
                t.Status,
                t.Descripcion,
                t.BancoNombreOrigen,
                t.CuentaBancoOrigen,
                t.BancoNombreDestino,
                t.CuentaBancoDestino,
                t.Monto,
                t.Entrada
                //t.Abono,t.Cargo,
                );
        }
        //Variable de sesion creada para asumir datos
        HttpContext.Current.Session["TAB_TransferenciaBancaria"] = tblTransferenciaBancarias;

    }
    //Llena el Gridview Transacciones Concilidadas
    private void LlenaGridViewTablaTransferenciasBancarias()
    {
        DataTable tablaTransferenciaBancaria = (DataTable)HttpContext.Current.Session["TAB_TransferenciaBancaria"];
        grdTranferenciBancaria.DataSource = tablaTransferenciaBancaria;
        grdTranferenciBancaria.DataBind();
    }




    
    #region Metodos Pagina CuentaTransferencia

     public string leerMes()
    {
        int p = txtFecha.Text.IndexOf("/");
        return txtFecha.Text.Substring(0, p);

    }

    public string leerAño()
    {
        int p = txtFecha.Text.IndexOf("/");
        return txtFecha.Text.Substring(p + 1);
    }
    
    public void Carga_Corporativo()
    {
        try
        {
            DataTable dtEmpresas = new DataTable();
            Usuario usuario;
            usuario = (Usuario)HttpContext.Current.Session["Usuario"];
            dtEmpresas = usuario.CorporativoAcceso;
            this.cboCorporativo.DataSource = dtEmpresas;
            this.cboCorporativo.DataValueField = "Corporativo";
            this.cboCorporativo.DataTextField = "NombreCorporativo";
            this.cboCorporativo.DataBind();
            dtEmpresas.Dispose();

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error\n" + ex.Message);
        }
    }

    public void Carga_SucursalCorporativo(int corporativo)
    {
        try
        {
            listSucursales = Conciliacion.RunTime.App.Consultas.ConsultaSucursales(Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionIden0.Sin0, corporativo);
            this.cboSucursal.DataSource = listSucursales;
            this.cboSucursal.DataValueField = "Identificador";
            this.cboSucursal.DataTextField = "Descripcion";
            this.cboSucursal.DataBind();
            this.cboSucursal.Dispose();
        }
        catch (Exception ex)
        {
            //App.ImplementadorMensajes.MostrarMensaje("Error\n" + ex.Message);
        }
    }


    public void Carga_BancoNombre(int corporativo)
    {
        try
        {

            listBancos = Conciliacion.RunTime.App.Consultas.ConsultaBancos(corporativo);
            this.cboNombreBanco.DataSource = listBancos;
            this.cboNombreBanco.DataValueField = "Identificador";
            this.cboNombreBanco.DataTextField = "Descripcion";
            this.cboNombreBanco.DataBind();
            this.cboNombreBanco.Dispose();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error\n" + ex.Message);
        }
    }

    public void Carga_CuentaBancariaDestino(int corporativo, short banco)
    {
        try
        {

            listCuentaBancaria = Conciliacion.RunTime.App.Consultas.ConsultaCuentasBancaria(corporativo, banco);
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


    protected void cboCorporativo_DataBound(object sender, EventArgs e)
    {
        Carga_SucursalCorporativo(Convert.ToInt32(cboCorporativo.SelectedItem.Value));
    }
    protected void cboCorporativo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Carga_SucursalCorporativo(Convert.ToInt32(cboCorporativo.SelectedItem.Value));
        Carga_BancoNombre(Convert.ToInt16(cboCorporativo.SelectedItem.Value));
    }

    protected void cboNombreBanco_SelectedIndexChanged(object sender, EventArgs e)
    {
        Carga_CuentaBancariaDestino(Convert.ToInt16(cboCorporativo.SelectedItem.Value), Convert.ToByte(cboNombreBanco.SelectedItem.Value));
    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {

        try
        {
            Consulta_TablaTranferenciaBancaria(Convert.ToInt16(cboCorporativo.SelectedItem.Value),
                                            Convert.ToInt16(cboSucursal.SelectedItem.Value),
                                            cboCuentaBancoOrigen.SelectedItem.Text.Trim(),
                                            Convert.ToInt32(leerAño()),
                                            Convert.ToInt16(leerMes()),
                                            cboStatus.SelectedItem.Text.Trim());

            GenerarTablaTransferenciaBancarias();
            LlenaGridViewTablaTransferenciasBancarias();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }

    }
   

    protected void grdTranferenciBancaria_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
        if (e.CommandName.Equals("CANCELARSTATUS"))
        {
            GridViewRow gRow = (GridViewRow)(e.CommandSource as ImageButton).Parent.Parent;
            Conciliacion.RunTime.App.TransferenciaBancarias.CambiarStatus
                (Convert.ToInt16(grdTranferenciBancaria.DataKeys[gRow.RowIndex].Values["CorporativoId"]), 
                Convert.ToInt16(grdTranferenciBancaria.DataKeys[gRow.RowIndex].Values["SucursalId"]),
                Convert.ToInt32(grdTranferenciBancaria.DataKeys[gRow.RowIndex].Values["AñoId"]),
                Convert.ToInt32(grdTranferenciBancaria.DataKeys[gRow.RowIndex].Values["FolioId"]),
                usuario.IdUsuario.Trim(),
               "CANCELADO");
        }
        else if (e.CommandName.Equals("AUTORIZARSTATUS"))
        {
            GridViewRow gRow = (GridViewRow)(e.CommandSource as ImageButton).Parent.Parent;
            Conciliacion.RunTime.App.TransferenciaBancarias.CambiarStatus
                (Convert.ToInt16(grdTranferenciBancaria.DataKeys[gRow.RowIndex].Values["CorporativoId"]),
                Convert.ToInt16(grdTranferenciBancaria.DataKeys[gRow.RowIndex].Values["SucursalId"]),
                Convert.ToInt32(grdTranferenciBancaria.DataKeys[gRow.RowIndex].Values["AñoId"]),
                Convert.ToInt32(grdTranferenciBancaria.DataKeys[gRow.RowIndex].Values["FolioId"]),
                usuario.IdUsuario.Trim(),
               "AUTORIZADA");
            
        }
        this.grdTranferenciBancaria.DataSource = tblTransferenciaBancarias;
        this.grdTranferenciBancaria.DataBind();
    }
    protected void grdTranferenciBancaria_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //Solo ejecuta la busqueda en el contenido
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = Convert.ToString(grdTranferenciBancaria.DataKeys[e.Row.RowIndex].Values["Status"]);
               
                if (status == "AUTORIZADA" || status == "CANCELADO")
                {
                    ImageButton img1 = e.Row.FindControl("btnCancelar") as ImageButton;
                    ImageButton img2 = e.Row.FindControl("btnAutorizado") as ImageButton;
                    e.Row.Cells[6].Controls.Remove(img1);
                    e.Row.Cells[7].Controls.Remove(img2);
                }
            }
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error\n" + ex.StackTrace);
        }
    }
}