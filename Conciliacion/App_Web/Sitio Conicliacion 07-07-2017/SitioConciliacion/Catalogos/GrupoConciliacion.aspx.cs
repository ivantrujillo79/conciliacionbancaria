using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CatalogoConciliacion.ReglasNegocio;
using System.Data;
using Conciliacion.Migracion.Runtime.ReglasNegocio;
using Conciliacion.RunTime;
using Conciliacion.RunTime.ReglasDeNegocio;
using SeguridadCB;
using SeguridadCB.Public;
using System.Text;

public partial class Catalogos_GrupoConciliacion : System.Web.UI.Page
{
    Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
    #region "Propiedades Globales"
    private SeguridadCB.Public.Operaciones operaciones;
    #endregion

    private List<GrupoConciliacion> listaGrupos = new List<GrupoConciliacion>();
    private DataTable tblGrupos = new DataTable("Grupos");
    private List<GrupoConciliacionUsuario> listaUsuarios = new List<GrupoConciliacionUsuario>();
    private DataTable tblUsuarios = new DataTable("Usuarios");
    private DataTable tblStatusConceptoGrupo = new DataTable("StatusConcepto");
    private List<ListaCombo> listEmpleados = new List<ListaCombo>();
    private List<StatusConcepto> listStatusConceptosGrupo = new List<StatusConcepto>();
    private SeguridadCB.Public.Usuario usuario;
    StringBuilder mensaje;
    CatalogoConciliacion.App objAppCat;

    protected override void OnPreInit(EventArgs e)
    {
        if (HttpContext.Current.Session["Operaciones"] == null)
            Response.Redirect("~/Acceso/Acceso.aspx", true);
        else
            operaciones = (SeguridadCB.Public.Operaciones)HttpContext.Current.Session["Operaciones"];
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        objAppCat = new CatalogoConciliacion.App();

        objAppCat.ImplementadorMensajes.ContenedorActual = this;
        Conciliacion.Migracion.Runtime.App.ImplementadorMensajes.ContenedorActual = this;

        Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
        objApp.ImplementadorMensajes.ContenedorActual = this;
        if (!IsPostBack)
        {
            ConsultaGruposConciliacion();
            GenerarTablaGruposConciliacion();
            LlenaGridViewGrupos();

            rgvDias.MaximumValue = rgvDias.MinimumValue = rgvDiferencia.MaximumValue = rgvDiferencia.MinimumValue = "0";

        }
    }

    protected void Mensaje(string titulo, string texto)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "alert('" + texto + "');", true);
    }

    // Consulta los grupos en la base de datos
    public void ConsultaGruposConciliacion()
    {
        CatalogoConciliacion.App objApp = new CatalogoConciliacion.App();
        listaGrupos = objApp.Consultas.ObtieneGrupos(1, 0); // Configuracion 1 todos los grupos activos e inactivos 
        // GenerarTablaGruposConciliacion();

    }

    //Genera la estructura de la tabla temporal para guardar lo que devulva la conulsta
    public void GenerarTablaGruposConciliacion()
    {
        tblGrupos.Columns.Add("GrupoConciliacionId", typeof(int));
        tblGrupos.Columns.Add("Descripcion", typeof(string));
        tblGrupos.Columns.Add("Usuario", typeof(string));
        tblGrupos.Columns.Add("Status", typeof(string));
        tblGrupos.Columns.Add("FAlta", typeof(DateTime));
        tblGrupos.Columns.Add("DiasDefault", typeof(int));
        tblGrupos.Columns.Add("DiferenciaDefault", typeof(decimal));


        foreach (GrupoConciliacion g in listaGrupos)
        {
            tblGrupos.Rows.Add(
                g.GrupoConciliacionId,
                g.Descripcion,
                g.Usuario,
                g.Status,
                g.FAlta,
                g.DiasDefault,
                g.DiferenciaDefault
                );
        }
        HttpContext.Current.Session["TABLAGRUPOS"] = tblGrupos;
    }

    //Asigna la tabla temporal al gridview
    private void LlenaGridViewGrupos()
    {
        DataTable tablaGrupos = (DataTable)HttpContext.Current.Session["TABLAGRUPOS"];
        grdGrupos.DataSource = tablaGrupos;
        grdGrupos.DataBind();
    }


    // Navegar entre las paginas del grid
    protected void grdGrupos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdGrupos.PageIndex = e.NewPageIndex;
            // ConsultaGruposConciliacion();
            LlenaGridViewGrupos();

        }
        catch (Exception)
        {

        }

    }

    // Consulta la base de datos los usuarios del grupo seleccionado
    public void ConsultaUsuarioGrupo(int grupo)
    {
        CatalogoConciliacion.App objAppCat = new CatalogoConciliacion.App();
        listaUsuarios = objAppCat.Consultas.ObtieneUsuariosPorGrupo(0, grupo);
    }

    //Genera la estructura de la tabla temporal para guardar lo que devulva la consulta
    public void GenerarTablaUsuariosGrupo()
    {
        tblUsuarios.Columns.Add("GrupoConciliacionId", typeof(int));
        tblUsuarios.Columns.Add("Usuario", typeof(string));
        tblUsuarios.Columns.Add("AccesoTotal", typeof(bool));

        foreach (GrupoConciliacionUsuario u in listaUsuarios)
        {
            tblUsuarios.Rows.Add(
                u.GrupoConciliacionId,
                u.Usuario,
                u.AccesoTotal
                );
        }
        HttpContext.Current.Session["TABLAUSUARIOS"] = tblUsuarios;
    }

    // Asigna la tabla temporal al gridview de usuario
    private void LlenaGridViewUsuarios()
    {
        DataTable tablaUsuarios = (DataTable)HttpContext.Current.Session["TABLAUSUARIOS"];
        grdUsuarios.DataSource = tablaUsuarios;
        grdUsuarios.DataBind();
    }

    //Navega entre las paginas del gris de usuarios.
    protected void grdUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdUsuarios.PageIndex = e.NewPageIndex;
            LlenaGridViewUsuarios();
        }
        catch (Exception ex)
        {

        }

    }


    //cancelar agregar usuario
    protected void btnCancelarUsuario_Click(object sender, EventArgs e)
    {
        lblGrupoConciliacion.Text = String.Empty;
        pnlGruposconciliacion.Visible = true;
        pnlNuevoGrupo.Visible = true;
        pnlUsuariosGrupo.Visible = false;
        pnlNuevoUsuario.Visible = false;
        //VisibilidadNuevoUsuario(false);
    }


    // Llena el combo de empleados
    public void CargarComboEmpleados(int grupoId)
    {
        CatalogoConciliacion.App objAppCat = new CatalogoConciliacion.App();

        listEmpleados = objAppCat.Consultas.ObtieneEmpleados(0, grupoConciliacionId);
        ddlEmpleado.DataSource = listEmpleados;
        this.ddlEmpleado.DataValueField = "Campo1"; //"Identificador";
        this.ddlEmpleado.DataTextField = "Descripcion";
        this.ddlEmpleado.DataBind();
        this.ddlEmpleado.Dispose();
        txtUsuario.Text = ddlEmpleado.SelectedItem.Value;
    }

    //Actualiza el txt usuario dependiendo del usuario seleccionado en el combo
    protected void ddlEmpleado_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtUsuario.Text = ddlEmpleado.SelectedItem.Value;
    }

    //Guardar usuario Nuevo en la bd
    protected void btnGuardarUsuario_Click(object sender, EventArgs e)
    {
        CatalogoConciliacion.App objAppCat = new CatalogoConciliacion.App();
        objAppCat.GrupoConciliacionUsuario.AgregaUsuario(grupoConciliacionId, txtUsuario.Text, ckbAccesoTotal.Checked);
        ConsultaUsuarioGrupo(grupoConciliacionId);
        GenerarTablaUsuariosGrupo();
        LlenaGridViewUsuarios();

        CargarComboEmpleados(grupoConciliacionId);
        ckbAccesoTotal.Checked = false;

    }
    private int indiceSeleccionadoGrupo
    {
        get
        {
            if (string.IsNullOrEmpty(Request.Form["GrupoConciliacion"]))
                return -1;
            return Convert.ToInt32(Request.Form["GrupoConciliacion"]);
        }
    }
    protected void grdGrupos_RowDataBound(object sender, GridViewRowEventArgs e)
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
            // Graba una referencia a la control Literal
            Literal output = (Literal)e.Row.FindControl("rbElegirGrupo");

            // Crea un input tupo radio button 

            output.Text = string.Format(@"<input type=""radio"" name=""GrupoConciliacion"" 
                                        id=""rowGrupo{0}"" value=""{0}""",
                                        e.Row.RowIndex);

            if (indiceSeleccionadoGrupo == e.Row.RowIndex || (!Page.IsPostBack && e.Row.RowIndex == 0))
                output.Text += @" checked=""checked""";
            output.Text += " />";
        }
        if (e.Row.RowType == DataControlRowType.Pager && (grdGrupos.DataSource != null))
        {
            //TRAE EL TOTAL DE PAGINAS
            Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumPaginas");
            _TotalPags.Text = grdGrupos.PageCount.ToString();

            //LLENA LA LISTA CON EL NUMERO DE PAGINAS
            DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
            for (int i = 1; i <= Convert.ToInt32(grdGrupos.PageCount); i++)
            {
                list.Items.Add(i.ToString());
            }
            list.SelectedValue = (grdGrupos.PageIndex + 1).ToString();
        }
    }
    protected void paginasDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag = 0;

        grdGrupos.PageIndex = int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 &&
                              iNumPag <= grdGrupos.PageCount
            ? iNumPag - 1
            : 0;

        LlenaGridViewGrupos();
    }
    protected void paginasDropDownListUsuarios_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag = 0;

        grdUsuarios.PageIndex = int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 &&
                                iNumPag <= grdUsuarios.PageCount
            ? iNumPag - 1
            : 0;

        LlenaGridViewUsuarios();
    }

    protected void grdGrupos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("CAMBIARSTATUS"))
        {
            GridViewRow gRow = (GridViewRow)(e.CommandSource as Button).Parent.Parent;
            CatalogoConciliacion.App objAppCat = new CatalogoConciliacion.App();
            objAppCat.GrupoConciliacion.CambiarStatus(Convert.ToInt32(grdGrupos.DataKeys[gRow.RowIndex].Values["GrupoConciliacionId"]));
            ConsultaGruposConciliacion();
            GenerarTablaGruposConciliacion();
            LlenaGridViewGrupos();
        }
    }
    protected void grdGrupos_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }

    protected void grdUsuarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CatalogoConciliacion.App objAppCat = new CatalogoConciliacion.App();
        objAppCat.GrupoConciliacionUsuario.EliminaUsuario(grupoConciliacionId, Convert.ToString(grdUsuarios.DataKeys[e.RowIndex].Value));
        ConsultaUsuarioGrupo(grupoConciliacionId);
        GenerarTablaUsuariosGrupo();
        LlenaGridViewUsuarios();
        CargarComboEmpleados(grupoConciliacionId);

    }
    protected void grdUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager && (grdUsuarios.DataSource != null))
        {
            //TRAE EL TOTAL DE PAGINAS
            Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumPaginas");
            _TotalPags.Text = grdUsuarios.PageCount.ToString();

            //LLENA LA LISTA CON EL NUMERO DE PAGINAS
            DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
            for (int i = 1; i <= Convert.ToInt32(grdUsuarios.PageCount); i++)
            {
                list.Items.Add(i.ToString());
            }
            list.SelectedValue = (grdUsuarios.PageIndex + 1).ToString();
        }
    }

    public static int grupoConciliacionId;

    public void verUsuariosGrupo()
    {

        grupoConciliacionId = Convert.ToInt32(grdGrupos.DataKeys[indiceSeleccionadoGrupo].Values["GrupoConciliacionId"]);

        CargarComboEmpleados(grupoConciliacionId);
        ConsultaUsuarioGrupo(grupoConciliacionId);
        GenerarTablaUsuariosGrupo();
        LlenaGridViewUsuarios();

        lblGrupoConciliacion.Text = ":" + grdGrupos.DataKeys[indiceSeleccionadoGrupo].Values["Descripcion"].ToString();
        pnlGruposconciliacion.Visible = false;
        pnlNuevoGrupo.Visible = false;
        pnlNuevoUsuario.Visible = true;
        pnlUsuariosGrupo.Visible = true;
    }
    public void verStatusConceptoGrupo()
    {
        Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
        try
        {
            grupoConciliacionId = Convert.ToInt32(grdGrupos.DataKeys[indiceSeleccionadoGrupo].Values["GrupoConciliacionId"]);
            hdfGrupoConciliacionSeleccionado.Value = grupoConciliacionId.ToString();
            CargarComboStatusConcepto(grupoConciliacionId);
            ConsultaStatusConceptoGrupo(grupoConciliacionId);
            GenerarTablaStatusConceptoGrupo();
            LlenaGridViewStatusConceptoGrupo();
            lblGrupoConciliacionSC.Text = ":" + grdGrupos.DataKeys[indiceSeleccionadoGrupo].Values["Descripcion"].ToString();
            mpeGrupoConciliacionStatus.Show();
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }

    }

    // Consulta la base de datos los usuarios del grupo seleccionado
    public void ConsultaStatusConceptoGrupo(int grupo)
    {
        listStatusConceptosGrupo = Conciliacion.Migracion.Runtime.App.Consultas.ObtenieneStatusConceptosGrupoConciliacion(0, grupo);
       }

    //Genera la estructura de la tabla temporal para guardar lo que devulva la conulsta
    public void GenerarTablaStatusConceptoGrupo()
    {
        tblStatusConceptoGrupo.Columns.Add("StatusConcepto", typeof(int));
        tblStatusConceptoGrupo.Columns.Add("StatusConceptoDes", typeof(string));

        foreach (StatusConcepto u in listStatusConceptosGrupo)
        {
            tblStatusConceptoGrupo.Rows.Add(
                u.Id,
                u.Descripcion
                );
        }
        HttpContext.Current.Session["TABLASTATUSCONCEPTO"] = tblStatusConceptoGrupo;
    }

    // Asigna la tabla temporal al gridview de usuario
    private void LlenaGridViewStatusConceptoGrupo()
    {
        DataTable tablaStatusConcepto = (DataTable)HttpContext.Current.Session["TABLASTATUSCONCEPTO"];
        grvGrupoStatusConcepto.DataSource = tablaStatusConcepto;
        grvGrupoStatusConcepto.DataBind();
    }

    private void CargarComboStatusConcepto(int grupoConciliacionId)
    {
        Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
        try
        {

            listStatusConceptosGrupo = Conciliacion.Migracion.Runtime.App.Consultas.ObtenieneStatusConceptosGrupoConciliacion(1, grupoConciliacionId);
            ddlStatusConcepto.DataSource = listStatusConceptosGrupo;
            this.ddlStatusConcepto.DataValueField = "Id"; //"Identificador";
            this.ddlStatusConcepto.DataTextField = "Descripcion";
            this.ddlStatusConcepto.DataBind();
            this.ddlStatusConcepto.Dispose();
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error al Consultar los Status Concepto:\n" + ex.Message);
        }
    }
    protected void btnVerUsuarios_Click(object sender, EventArgs e)
    {
        Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
        if (indiceSeleccionadoGrupo >= 0)
            verUsuariosGrupo();
        else
            objApp.ImplementadorMensajes.MostrarMensaje("Seleccione un Grupo de Conciliación");

    }
    protected void btnVerStatusConcepto_Click(object sender, EventArgs e)
    {
        Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
        if (indiceSeleccionadoGrupo >= 0)
            verStatusConceptoGrupo();
        else
            objApp.ImplementadorMensajes.MostrarMensaje("Seleccione un Grupo de Conciliación");

    }
    protected void btnUsuarios_Click(object sender, ImageClickEventArgs e)
    {
        Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
        if (indiceSeleccionadoGrupo >= 0)
            verUsuariosGrupo();
        else
            objApp.ImplementadorMensajes.MostrarMensaje("Seleccione un Grupo de Conciliación");
    }

    protected void btnGuardarGrupoConciliacion_Click(object sender, EventArgs e)
    {
        usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
        CatalogoConciliacion.App objAppCat = new CatalogoConciliacion.App();
        objAppCat.GrupoConciliacion.Guardar(txtDescripcion.Text, usuario.IdUsuario.Trim(), Convert.ToInt32(txtDiasDefault.Text), Convert.ToInt32(txtDiasMaxima.Text), Convert.ToInt32(txtDiasMinima.Text), Convert.ToDecimal(txtDiferenciaDefault.Text), Convert.ToDecimal(txtDiferenciaMaxima.Text), Convert.ToDecimal(txtDiferenciaMinima.Text));
        ConsultaGruposConciliacion();
        GenerarTablaGruposConciliacion();
        LlenaGridViewGrupos();
        LimpiarTodo();
    }

    protected void chkValidarRangos_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkValidarRangos.Checked)
            {
                if (ValidarDatosRangos())
                {
                    if (ValidarRangos())
                    {

                        txtDiasMaxima.Enabled = txtDiasMinima.Enabled = txtDiferenciaMaxima.Enabled = txtDiferenciaMinima.Enabled = false;
                        txtDiasDefault.Enabled = txtDiferenciaDefault.Enabled = true;

                        rgvDias.MaximumValue = txtDiasMaxima.Text;
                        rgvDias.MinimumValue = txtDiasMinima.Text;
                        rgvDias.ErrorMessage = "Dias permitidos entre " + txtDiasMinima.Text + " - " + txtDiasMaxima.Text + " dias.";

                        rgvDiferencia.MaximumValue = txtDiferenciaMaxima.Text;
                        rgvDiferencia.MinimumValue = txtDiferenciaMinima.Text;
                        rgvDiferencia.ErrorMessage = "Diferencia permitidos entre " + txtDiferenciaMinima.Text + " - " + txtDiferenciaMaxima.Text + " pesos.";


                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "alert('" + LimpiarTexto(mensaje.ToString()) + "');", true);
                        chkValidarRangos.Checked = false;
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "alert('" + LimpiarTexto(mensaje.ToString()) + "');", true);
                    chkValidarRangos.Checked = false;
                }
            }
            else
            {
                txtDiasMaxima.Enabled = txtDiasMinima.Enabled = txtDiferenciaMaxima.Enabled = txtDiferenciaMinima.Enabled = true;
                txtDiasDefault.Enabled = txtDiferenciaDefault.Enabled = false;
                txtDiasDefault.Text = txtDiferenciaDefault.Text = String.Empty;
            }
        }
        catch (Exception ex)
        {
            chkValidarRangos.Checked = false;
            txtDiferenciaMaxima.Text = txtDiferenciaMinima.Text = String.Empty; ;
            Mensaje("Mensaje", ex.Message);

        }
    }

    public bool ValidarDatosRangos()
    {
        mensaje = new StringBuilder();
        bool resultado = true;
        mensaje.Append("Se requiere  el campo: ");

        if (this.txtDiasMinima.Text.Equals(""))
        {
            mensaje.Append(" Dias Minimo.");
            resultado = false;
        }
        else if (this.txtDiasMaxima.Text.Equals(""))
        {
            mensaje.Append(" Dias Maximo.");
            resultado = false;
        }
        else if (this.txtDiferenciaMinima.Text.Equals(""))
        {
            mensaje.Append(" Diferencia Minima.");
            resultado = false;
        }
        else if (this.txtDiferenciaMaxima.Text.Equals(""))
        {
            mensaje.Append(" Diferencia Maxima.");
            resultado = false;
        }
        else
            resultado = true;
        return resultado;
    }
    public bool ValidarRangos()
    {
        mensaje = new StringBuilder();
        bool resultado = true;
        mensaje.Append("Verifique:\n");

        if (Convert.ToInt32(txtDiasMinima.Text) >= Convert.ToInt32(txtDiasMaxima.Text))
        {
            mensaje.Append("- Dias Minimo no puedo ser igual o mayor que Dias Maximo.");
            resultado = false;
        }
        else if (Convert.ToInt32(txtDiasMaxima.Text) <= Convert.ToInt32(txtDiasMinima.Text))
        {
            mensaje.Append("- Dias Maximo no puedo ser igual o menor que Dias Minimo.");
            resultado = false;
        }
        else if (Convert.ToDecimal(txtDiferenciaMinima.Text) >= Convert.ToDecimal(txtDiferenciaMaxima.Text))
        {
            mensaje.Append("- La Diferencia Minima no puedo ser igual o mayor que la Diferencia Maxima.");
            resultado = false;
        }
        else if (Convert.ToDecimal(txtDiferenciaMaxima.Text) <= Convert.ToDecimal(txtDiferenciaMinima.Text))
        {
            mensaje.Append("- La Diferencia Maxima no puedo ser igual o menor que la Diferencia Minima.");
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
    private void LimpiarTodo()
    {
        txtDescripcion.Text = txtDiasDefault.Text = txtDiasMaxima.Text = txtDiasMinima.Text = txtDiferenciaDefault.Text = txtDiferenciaMaxima.Text = txtDiferenciaMinima.Text = String.Empty;
        chkValidarRangos.Checked = false;
    }
    protected void grvGrupoStatusConcepto_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
        if (hdfGrupoConciliacionSeleccionado.Value != null)
        {
            CatalogoConciliacion.App objAppCat = new CatalogoConciliacion.App();
            GrupoConciliacion gc =
                objAppCat.Consultas.ObtieneGrupoPorId(2,
                    Convert.ToInt32(hdfGrupoConciliacionSeleccionado.Value));
            int statusconcepto = Convert.ToInt32(grvGrupoStatusConcepto.DataKeys[e.RowIndex].Values["StatusConcepto"]);
            if (!gc.QuitarStatusConcepto(statusconcepto)) return;

            objApp.ImplementadorMensajes.MostrarMensaje("Registro Modificado Con éxito");
            CargarComboStatusConcepto(grupoConciliacionId);
            ConsultaStatusConceptoGrupo(gc.GrupoConciliacionId);
            GenerarTablaStatusConceptoGrupo();
            LlenaGridViewStatusConceptoGrupo();
        }
        else
            objApp.ImplementadorMensajes.MostrarMensaje("Error al leer el Grupo de Conciliacion Seleccionado. Recargue la vista.");

    }
    protected void btnAgregarStatusConcepto_Click(object sender, EventArgs e)
    {
        Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
        if (hdfGrupoConciliacionSeleccionado.Value != null)
        {
            CatalogoConciliacion.App objAppCat = new CatalogoConciliacion.App();
            GrupoConciliacion gc =
                objAppCat.Consultas.ObtieneGrupoPorId(2,
                    Convert.ToInt32(hdfGrupoConciliacionSeleccionado.Value));
            int statusconcepto = Convert.ToInt32(ddlStatusConcepto.SelectedItem.Value);
            if (!gc.AñadirStatusConcepto(statusconcepto)) return;

            objApp.ImplementadorMensajes.MostrarMensaje("Registro Modificado Con éxito");
            CargarComboStatusConcepto(grupoConciliacionId);
            ConsultaStatusConceptoGrupo(gc.GrupoConciliacionId);
            GenerarTablaStatusConceptoGrupo();
            LlenaGridViewStatusConceptoGrupo();
        }
        else
            objApp.ImplementadorMensajes.MostrarMensaje("Error al leer el Grupo de Conciliacion Seleccionado. Recargue la vista.");
    }
    protected void grvGrupoStatusConcepto_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
        try
        {
            grvGrupoStatusConcepto.PageIndex = e.NewPageIndex;
            LlenaGridViewStatusConceptoGrupo();
        }
        catch (Exception ex)
        {
            objApp.ImplementadorMensajes.MostrarMensaje("Error:\n"+ex.Message);
        }
    }
}
