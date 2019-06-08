using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CatalogoConciliacion;
using CatalogoConciliacion.ReglasNegocio;

using System.Data;
using SeguridadCB.Public;
using Conciliacion.RunTime.ReglasDeNegocio;

public partial class Catalogos_TipoMovimientoPorCuenta : System.Web.UI.Page
{
    Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
    CatalogoConciliacion.App objAppCat = new CatalogoConciliacion.App();
    #region "Propiedades Globales"
    private SeguridadCB.Public.Usuario usuario;
    #endregion

    #region "Propiedades Globales"
    private SeguridadCB.Public.Operaciones operaciones;
    #endregion

    private List<ListaCombo> listSucursales = new List<ListaCombo>();
    private List<ListaCombo> listCuentaBancaria = new List<ListaCombo>();
    private List<ListaCombo> listBancos = new List<ListaCombo>();

    public static List<Conciliacion.RunTime.ReglasDeNegocio.ImportacionAplicacion> ListExtractores = new List<Conciliacion.RunTime.ReglasDeNegocio.ImportacionAplicacion>();

    private DataTable tblImportacionAplicacion = new DataTable("ImportacionAplicacion");
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
        objApp.ImplementadorMensajes.ContenedorActual = this;
        try
        {
           
            //Llamamos a la clase app perteneciente a libreria de clases donde estamos apuntando
            objAppCat.ImplementadorMensajes.ContenedorActual = this;
            if (!IsPostBack)
            {
                usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
                if (cboCorporativo.Items.Count == 0) Carga_Corporativo();
                Carga_BancoNombre(Convert.ToInt16(cboCorporativo.SelectedItem.Value));
                Carga_CuentaBancaria(Convert.ToInt16(cboCorporativo.SelectedItem.Value), Convert.ToSByte(cboNombreBanco.SelectedItem.Value));
                
                this.grdExtractores.DataSource = tblImportacionAplicacion;
                this.grdExtractores.DataBind();

                if (cboCuentaBanco.SelectedItem == null || cboSucursal.SelectedItem == null)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "alertify.alert('Conciliaci&oacute;n bancaria','Error: La sucursal o la cuenta de banco por defecto no están configuradas.', function(){ alertify.error('Error en la solicitud'); });", true);
                    return;
                }
                else
                {
                    //Parámetros para generar consultas
                    Consulta_TablaImportacionAplicacionCuenta(Convert.ToInt16(cboCuentaBanco.SelectedItem.Value),
                                Convert.ToInt16(cboSucursal.SelectedItem.Value),
                                cboCuentaBanco.SelectedItem.Text.Trim());
                }
                GenerarTablaImportacionAplicacion();
                LlenaGridViewTablaCuentasTransferencias();
            }
            
            this.cboCorporativo.Focus();

        }
        catch (Exception ex)
        {
           objApp.ImplementadorMensajes.MostrarMensaje("Error\n"+ex.Message);
        }

    }

    //Consulta tabla principal CuentaTransferencias
    public void Consulta_TablaImportacionAplicacionCuenta(short corporativo, int sucursal, string cuentaBanco)
    {
        SeguridadCB.Seguridad seguridad = new SeguridadCB.Seguridad();
        System.Data.SqlClient.SqlConnection Connection = seguridad.Conexion;
        if (Connection.State == ConnectionState.Closed)
        {
            seguridad.Conexion.Open();
            Connection = seguridad.Conexion;
        }
        try
        {
            ListExtractores = objApp.Consultas.ConsultaImportacionesAplicacion( sucursal, cuentaBanco);
            
            
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error\n" + ex.Message);
        }
    }

    

    //Genera la tabla en CuentaTransferencias
    public void GenerarTablaImportacionAplicacion()
    {
        tblImportacionAplicacion.Columns.Add("Identificador", typeof(int));
        tblImportacionAplicacion.Columns.Add("Descripcion", typeof(string));
        /*tblImportacionAplicacion.Columns.Add("TipoFuenteInformacion", typeof(int));
        tblImportacionAplicacion.Columns.Add("Procedimiento", typeof (string));
        tblImportacionAplicacion.Columns.Add("Servidor", typeof(string));
        tblImportacionAplicacion.Columns.Add("BaseDeDatos", typeof(string));
        tblImportacionAplicacion.Columns.Add("UsuarioConsulta", typeof(string));*/

        foreach (ImportacionAplicacion  t in ListExtractores)
        {
            tblImportacionAplicacion.Rows.Add(
                t.Identificador,
               t.Descripcion);
           /* tblImportacionAplicacion.Rows.Add(
                t.Identificador,
                t.Descripcion,
                t.TipoFuenteInformacion,
                t.Procedimiento,
                t.Servidor,
                t.BaseDeDatos,
                t.UsuarioConsulta);*/
        }
        //Variable de sesion creada para asumir datos
        HttpContext.Current.Session["TAB_ImportacionAplicacion"] = tblImportacionAplicacion;

    }
    //Llena el Gridview Transacciones Concilidadas
    private void LlenaGridViewTablaCuentasTransferencias()
    {
        DataTable tablaImportacionAplicacion = (DataTable)HttpContext.Current.Session["TAB_ImportacionAplicacion"];
        grdExtractores.DataSource = tablaImportacionAplicacion;
        grdExtractores.DataBind();
    }

    //Metodos para proporcionar valores a los combos pagina principal Destinos
    #region Metodos Pagina CuentaTransferencia
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
            objApp.ImplementadorMensajes.MostrarMensaje("Error\n" + ex.Message);
        }
    }

   public void Carga_SucursalCorporativo(int corporativo)
    {
        try
        {
            listSucursales = objApp.Consultas.ConsultaSucursales(Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionIden0.Sin0, corporativo);
            this.cboSucursal.DataSource = listSucursales;
            this.cboSucursal.DataValueField = "Identificador";
            this.cboSucursal.DataTextField = "Descripcion";
            this.cboSucursal.DataBind();
            this.cboSucursal.Dispose();
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error\n" + ex.Message);
        }
    }

   
   public void Carga_BancoNombre(int corporativo)
   {
       try
       {

           listBancos = objApp.Consultas.ConsultaBancos(corporativo);
           this.cboNombreBanco.DataSource = listBancos;
           this.cboNombreBanco.DataValueField = "Identificador";
           this.cboNombreBanco.DataTextField = "Descripcion";
           this.cboNombreBanco.DataBind();
           this.cboNombreBanco.Dispose();
       }
       catch (Exception ex)
       {
           objApp.ImplementadorMensajes.MostrarMensaje("Error\n" + ex.Message);
       }
   }
    
   public void Carga_CuentaBancaria(int corporativo, short banco)
   {
       try
       {

           listCuentaBancaria = objApp.Consultas.ConsultaCuentasBancaria(corporativo, banco);
           
          // ListaCombo dato = new ListaCombo(0, "VER TODOS", "TODAS LAS CUENTAS");
          // listCuentaBancaria.Insert(0,dato);

           this.cboCuentaBanco.DataSource = listCuentaBancaria;
           this.cboCuentaBanco.DataValueField = "Identificador";
           this.cboCuentaBanco.DataTextField = "Descripcion";
           this.cboCuentaBanco.DataBind();
           this.cboCuentaBanco.Dispose();


       }
       catch (Exception)
       {

           this.cboCuentaBanco.DataSource = new List<ListaCombo>();
           this.cboCuentaBanco.DataBind();
           this.cboCuentaBanco.Dispose();
       }
   }
    #endregion

   //Metodos para proporcionar valores a los combos pop UP 
   #region Metodos Pop Up 
    
    public void Carga_TiposMovimientosFaltantes()
    {
        try
        {
           
            List<Conciliacion.RunTime.ReglasDeNegocio.ImportacionAplicacion> listImportacionAplicacionTemp1 = objApp.Consultas.ConsultaImportacionesAplicacion(Convert.ToInt16(cboSucursal.SelectedItem.Value), cboCuentaBanco.SelectedItem.Text.Trim()); ;

            for (int i = 0; i < listImportacionAplicacionTemp1.Count; i++)
            {
                listImportacionAplicacionTemp1[i].EsConfiguracion = true;
            }



            List<Conciliacion.RunTime.ReglasDeNegocio.ImportacionAplicacion> listImportacionAplicacionTemp2 = objApp.Consultas.ConsultaImportacionAplicacion(Convert.ToInt16(cboSucursal.SelectedItem.Value)); ;
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

            this.cboTipoMovimientoNuevo.DataSource = listImportacionAplicacionTemp2;
            this.cboTipoMovimientoNuevo.DataValueField = "Identificador";
            this.cboTipoMovimientoNuevo.DataTextField = "Descripcion";
            this.cboTipoMovimientoNuevo.DataBind();
            

        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error\n" + ex.Message);
        }
    }

   

    

    //Carga CTADestino 2
    
   #endregion





    //Nuevo codigo
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
        Carga_CuentaBancaria(Convert.ToInt16(cboCorporativo.SelectedItem.Value), Convert.ToSByte(cboNombreBanco.SelectedItem.Value));
    }

       
    protected void btnConsultar_Click1(object sender, EventArgs e)
    {
        try
        {
          
            Consulta_TablaImportacionAplicacionCuenta(Convert.ToInt16(cboCorporativo.SelectedItem.Value),
                                            Convert.ToInt16(cboSucursal.SelectedItem.Value),
                                            cboCuentaBanco.SelectedItem.Text.Trim()
                                            );
           
            
            GenerarTablaImportacionAplicacion();
            LlenaGridViewTablaCuentasTransferencias();
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }
    }
    protected void btnAgregar_Click1(object sender, EventArgs e)
    {
        popUpAgregarTransferencia.Show();

        try
        {
           // if (cboCorporativo.Items.Count == 0 && cboCorporativoDestino_.Items.Count == 0)
            Carga_TiposMovimientosFaltantes();
           
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnGuardarInterno_Click(object sender, EventArgs e)
    {

        //CuentaTransferencia cta =  objAppCat.ReferenciaCuentaTransferencia.CrearObjeto();
        TipoMovimientoCuenta movimiento =  objAppCat.TipoMovimientoCuenta.CrearObjeto();

        movimiento.Cuenta = cboCuentaBanco.SelectedItem.Text.Trim();
        movimiento.TipoMovimiento = Convert.ToInt16(cboTipoMovimientoNuevo.SelectedItem.Value);



        if (movimiento.Guardar())
        {
            try
            {
                //Refrescamos el DGV
                Consulta_TablaImportacionAplicacionCuenta(Convert.ToInt16(cboCorporativo.SelectedItem.Value),
                                            Convert.ToInt16(cboSucursal.SelectedItem.Value),
                                            cboCuentaBanco.SelectedItem.Text.Trim()
                                            );

                GenerarTablaImportacionAplicacion();
                LlenaGridViewTablaCuentasTransferencias();

            }
            catch (Exception)
            {

                this.grdExtractores.DataSource = tblImportacionAplicacion;
                this.grdExtractores.DataBind();
            }
            popUpAgregarTransferencia.Hide();
            popUpAgregarTransferencia.Dispose();

        }
       

        // objAppCat.ConsultasDos.

        /*   usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
           CuentaTransferencia cta =  objAppCat.ReferenciaCuentaTransferencia.CrearObjeto();
           cta.CuentaTransferencia_ = 0;
           cta.Corporativo = Convert.ToInt16(cboCorporativo_.SelectedItem.Value);
           cta.Sucursal = Convert.ToInt16(cboSucursal_.SelectedItem.Value);
           cta.CuentaBanco = cboCuentaBanco_.SelectedItem.Text.Trim();
           cta.Banco = Convert.ToInt16(cboNombreBanco_.SelectedItem.Value);
           cta.BancoNombre = cboNombreBanco_.SelectedItem.Text.Trim();
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
                   Consulta_TablaCuentasTranferencia(Convert.ToInt16(cboCorporativo.SelectedItem.Value),
                                       Convert.ToInt16(cboSucursal.SelectedItem.Value),
                                       cboCuentaBanco.SelectedItem.Text.Trim());
                   GenerarTablaImportacionAplicacion();
                   LlenaGridViewTablaCuentasTransferencias();

               }
               catch (Exception)
               {

                   this.grdExtractores.DataSource = tblCuentaTransferencia;
                   this.grdExtractores.DataBind();
               }
               popUpAgregarTransferencia.Hide();
               popUpAgregarTransferencia.Dispose();

           }*/

    }
    protected void grdExtractores_RowDataBound(object sender, GridViewRowEventArgs e)
    {
     /*   if (e.Row.RowType == DataControlRowType.DataRow)
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
        }*/
        //Total de paginas
        if (e.Row.RowType == DataControlRowType.Pager && (grdExtractores.DataSource != null))
        {
            //TRAE EL TOTAL DE PAGINAS
            Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumPaginas");
            _TotalPags.Text = grdExtractores.PageCount.ToString();

            //LLENA LA LISTA CON EL NUMERO DE PAGINAS
            DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
            for (int i = 1; i <= Convert.ToInt32(grdExtractores.PageCount); i++)
            {
                list.Items.Add(i.ToString());
            }
            list.SelectedValue = (grdExtractores.PageIndex + 1).ToString();
        }
    }
    //Muestra Total de paginas
    protected void paginasDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag = 0;

        if (int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 && iNumPag <= grdExtractores.PageCount)
        {
            grdExtractores.PageIndex = iNumPag - 1;
        }
        else
        {
            grdExtractores.PageIndex = 0;
        }

        LlenaGridViewTablaCuentasTransferencias();
    }

    protected void grdExtractores_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        /* aqui if (e.CommandName.Equals("CAMBIARSTATUS"))
         {
             GridViewRow gRow = (GridViewRow)(e.CommandSource as Button).Parent.Parent;
              objAppCat.ReferenciaCuentaTransferencia.CambiarStatus(Convert.ToInt16(grdExtractores.DataKeys[gRow.RowIndex].Values["CuentaTransferenciaId"]));

             //Refrescamos el DGV
             Consulta_TablaImportacionAplicacionCuenta(Convert.ToInt16(cboCorporativo.SelectedItem.Value),
                                 Convert.ToInt16(cboSucursal.SelectedItem.Value),
                                 cboCuentaBanco.SelectedItem.Text.Trim());
             GenerarTablaImportacionAplicacion();
             LlenaGridViewTablaCuentasTransferencias();
         }*/

        if (e.CommandName == "EliminaRegistro")
        {
            GridViewRow gRow = (GridViewRow)(e.CommandSource as Button).Parent.Parent;
            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = grdExtractores.Rows[index];

           if(objAppCat.TipoMovimientoCuenta.Eliminar(Convert.ToInt16(grdExtractores.DataKeys[gRow.RowIndex].Values["Identificador"]),

                cboCuentaBanco.SelectedItem.Text.Trim()
                ))
            {
                Consulta_TablaImportacionAplicacionCuenta(Convert.ToInt16(cboCorporativo.SelectedItem.Value),
                                Convert.ToInt16(cboSucursal.SelectedItem.Value),
                                cboCuentaBanco.SelectedItem.Text.Trim()
                                );

                GenerarTablaImportacionAplicacion();
                LlenaGridViewTablaCuentasTransferencias();

            }




        }

    }
    protected void grdExtractores_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdExtractores.PageIndex = e.NewPageIndex;
            LlenaGridViewTablaCuentasTransferencias();
        }
        catch (Exception)
        {

        }
    }

    protected void grdExtractores_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}