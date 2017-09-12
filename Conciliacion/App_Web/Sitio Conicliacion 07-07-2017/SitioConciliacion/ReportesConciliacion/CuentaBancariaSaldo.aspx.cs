using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CatalogoConciliacion.ReglasNegocio;
using Conciliacion.Migracion.Runtime;
using Conciliacion.RunTime.ReglasDeNegocio;
using SeguridadCB.Public;

public partial class ReportesConciliacion_CuentaBancariaSaldo : System.Web.UI.Page
{
    #region "Propiedades Globales"

    private SeguridadCB.Public.Operaciones operaciones;
    private SeguridadCB.Public.Usuario usuario;

    #endregion
    private List<ListaCombo> listSucursales = new List<ListaCombo>();
    private List<CuentaBancoSaldo> listaCuentaBancoSaldo = new List<CuentaBancoSaldo>();
    private DataTable dtSaldoFinal;
    private decimal sumatotalsaldo = 0;

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
            Conciliacion.RunTime.App.ImplementadorMensajes.ContenedorActual = this;
            Conciliacion.Migracion.Runtime.App.ImplementadorMensajes.ContenedorActual = this;

            if (HttpContext.Current.Request.UrlReferrer != null)
            {
                if ((!HttpContext.Current.Request.UrlReferrer.AbsoluteUri.Contains("SitioConciliacion")) ||
                    (HttpContext.Current.Request.UrlReferrer.AbsoluteUri.Contains("Inicio.aspx")))
                {
                    HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
                    HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
                    Response.Cache.SetExpires(DateTime.Now);
                }
            }
            if (!Page.IsPostBack)
            {
                if (ddlEmpresa.Items.Count == 0) Carga_Corporativo();

            }
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
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
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    /// <summary>
    /// Llena el Combo de Sucurales según Corporativo
    /// </summary>
    public void Carga_SucursalCorporativo(int corporativo)
    {
        try
        {
            listSucursales =
                Conciliacion.RunTime.App.Consultas.ConsultaSucursales(
                    Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionIden0.Sin0, corporativo);

            this.ddlSucursal.DataSource = listSucursales;
            this.ddlSucursal.DataValueField = "Identificador";
            this.ddlSucursal.DataTextField = "Descripcion";
            this.ddlSucursal.DataBind();
            this.ddlSucursal.Dispose();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    /// <summary>
    /// Llena el Combo de Sucurales según Corporativo
    /// </summary>
    public void Carga_Banco(int corporativo)
    {
        try
        {

            ddlBanco.DataValueField = "Identificador";
            ddlBanco.DataTextField = "Descripcion";
            ddlBanco.DataSource = Conciliacion.RunTime.App.Consultas.ConsultaBancos(corporativo);
            ddlBanco.DataBind();
            //ddlBanco.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    /// <summary>
    /// Llena el Combo de Sucurales según Corporativo
    /// </summary>
    public void Carga_CuentaBancaria(int corporativo, int banco)
    {
        try
        {

            ddlCuentaBancaria.DataValueField = "Descripcion";
            ddlCuentaBancaria.DataTextField = "Descripcion";
            ddlCuentaBancaria.DataSource = App.Consultas.ObtieneListaCuentaFinancieroPorBanco(corporativo, banco);
            ddlCuentaBancaria.DataBind();

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }
    /// <summary>
    /// Consulta los Saldos Finales X Dia
    /// </summary>
    public void ConsultaCuentaBancariaSaldoFinal(int corporativo, int sucursal, int banco, string cuentabancaria, DateTime fconsula)//, int grupoconciliacion
    {
        try
        {
            listaCuentaBancoSaldo = CatalogoConciliacion.App.Consultas.ConsultaCuentaBancariaSaldoFinalDia(corporativo,
                sucursal, banco, cuentabancaria, fconsula);//,grupoconciliacion
            Session["SALDOFINAL"] = listaCuentaBancoSaldo;
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }
    public void GenerarTablaCuentaBancariaSaldoFinal()
    {
        dtSaldoFinal = new DataTable("CuentaBancariaSaldoFinal");
        dtSaldoFinal.Columns.Add("Corporativo", typeof(int));
        dtSaldoFinal.Columns.Add("Sucursal", typeof(int));
        dtSaldoFinal.Columns.Add("Banco", typeof(int));
        dtSaldoFinal.Columns.Add("BancoDes", typeof(string));
        dtSaldoFinal.Columns.Add("CuentaBancaria", typeof(string));
        dtSaldoFinal.Columns.Add("SaldoInicialMes", typeof(decimal));
        dtSaldoFinal.Columns.Add("SaldoFinalDia", typeof(decimal));

        foreach (CuentaBancoSaldo cs in listaCuentaBancoSaldo)
            dtSaldoFinal.Rows.Add(
                cs.Corporativo,
                cs.Sucursal,
                cs.Banco,
                cs.BancoDes,
                cs.CuentaBancaria,
                cs.SaldoInicialMes,
                cs.SaldoFinal
               );

        HttpContext.Current.Session["TAB_CUENTABANCOSALDO"] = dtSaldoFinal;

    }
    private void LlenarGridViewCuentaBancoSaldoFinal()
    {
        DataTable tablaCuentaBancoSaldo = (DataTable)HttpContext.Current.Session["TAB_CUENTABANCOSALDO"];
        grvCuentaBancoSaldoFinalDia.DataSource = tablaCuentaBancoSaldo;
        grvCuentaBancoSaldoFinalDia.DataBind();
    }
    /// <summary>
    /// Obtiene la direccion de ordebamiento ASC o DESC
    /// </summary>
    private string direccionOrdenarCadena(string columna)
    {
        string direccionOrden = "ASC";
        string expresionOrden = ViewState["ExpresionOrden"] as string;
        if (expresionOrden != null)
            if (expresionOrden == columna)
            {
                string direccionAnterior = ViewState["DireccionOrden"] as string;
                if ((direccionAnterior != null) && (direccionAnterior == "ASC"))
                    direccionOrden = "DESC";
            }

        ViewState["DireccionOrden"] = direccionOrden;
        ViewState["ExpresionOrden"] = columna;

        return direccionOrden;
    }

    private void ExporttoExcel(GridView grvResultados)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=ReporteaSaldoCuentaBanco.xls");

        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:13.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR>");
        //TITULO DEL REPORTE
        HttpContext.Current.Response.Write("<B style='font-size:13.0pt; font-family: Calibri;'>");
        HttpContext.Current.Response.Write(ddlEmpresa.SelectedItem.Text);
        HttpContext.Current.Response.Write("</B>");
        HttpContext.Current.Response.Write("<BR>");
        HttpContext.Current.Response.Write("CUENTA BANCARIA / SALDO FINAL");
        HttpContext.Current.Response.Write("<BR>");
        HttpContext.Current.Response.Write("<B style='font-size:12.0pt; font-family: Calibri;'>");
        HttpContext.Current.Response.Write("DIA : " + hdfConsulta.Value);
        HttpContext.Current.Response.Write("</B>");
        //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:12.0pt; font-family:Calibri; background:white;'> <TR>");
        //am getting my grid's column headers
        int columnscount = grvResultados.Columns.Count;

        for (int j = 0; j < columnscount; j++)
        {      //write in new column
            HttpContext.Current.Response.Write("<Td style='background: #ebecec;font-size: 11.0pt;font-style: normal;padding: 2px;cursor: pointer;text-align: center;'>");
            //HttpContext.Current.Response.Write("<TD style='background: #ebecec;font-size: 10.0pt;font-style: normal;padding: 2px;cursor: pointer;text-align: center;'> ");
            //Get column headers  and make it as bold in excel columns
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(grvResultados.Columns[j].HeaderText);
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");
        }
        HttpContext.Current.Response.Write("</TR>");
        foreach (GridViewRow row in grvResultados.Rows)
        {//write in new row
            HttpContext.Current.Response.Write("<TR>");
            for (int i = 0; i < grvResultados.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write(grvResultados.Columns[i].HeaderText.Equals("Cuenta Banc.") || grvResultados.Columns[i].HeaderText.Equals("Banco")
                    ? "<Td style='background: #ebecec;font-size: 11.0pt;font-style: normal;padding: 2px;cursor: pointer;text-align: center;'>"
                    : "<Td>");
                string dato = (row.Cells[i].Controls[1] as Label).Text;
                HttpContext.Current.Response.Write(dato);

                HttpContext.Current.Response.Write("</Td>");
            }

            HttpContext.Current.Response.Write("</TR>");
        }
        //Footer Row
        GridViewRow ft = grvResultados.FooterRow;
        HttpContext.Current.Response.Write("<TR>");
        for (int i = 0; i < grvResultados.Columns.Count; i++)
        {
            HttpContext.Current.Response.Write("<Td style='background: black;color:white;font-size: 11.0pt;'>");
            if (i > 1)
            {
                var label = ft.Cells[i].Controls[1] as Label;
                if (label != null)
                {
                    string dato = label.Text;
                    HttpContext.Current.Response.Write(dato);
                }
            }
            HttpContext.Current.Response.Write("</Td>");
        }

        HttpContext.Current.Response.Write("</TR>");
        // fin Fotter
        HttpContext.Current.Response.Write("</Table>");
        HttpContext.Current.Response.Write("</font>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }
    #region Eventos Formas

    protected void grvCuentaBancoSaldoFinalDia_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                sumatotalsaldo = sumatotalsaldo + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SaldoFinalDia"));
                break;
            case DataControlRowType.Footer:
                {
                    Label lblTotalSaldoFinal = (Label)e.Row.FindControl("lblTotalSaldoFinalDia");
                    lblTotalSaldoFinal.Text = sumatotalsaldo.ToString("C2");
                }
                break;
        }
    }

    protected void grvCuentaBancoSaldoFinalDia_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtTblOrdenada = (DataTable)HttpContext.Current.Session["TAB_CUENTABANCOSALDO"];
        if (dtTblOrdenada == null) return;
        string orden = direccionOrdenarCadena(e.SortExpression);
        dtTblOrdenada.DefaultView.Sort = e.SortExpression + " " + orden;
        HttpContext.Current.Session["TAB_CUENTABANCOSALDO"] = dtTblOrdenada;
        grvCuentaBancoSaldoFinalDia.DataSource = dtTblOrdenada;
        grvCuentaBancoSaldoFinalDia.DataBind();
    }

    protected void btnActualizarConfig_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        try
        {
            int corporativo = Convert.ToInt16(ddlEmpresa.SelectedItem.Value);
            int sucursal = Convert.ToInt16(ddlSucursal.SelectedItem.Value);
            int banco = Convert.ToInt16(ddlBanco.SelectedItem.Value);
            string cuentabancaria = Convert.ToString(ddlCuentaBancaria.SelectedItem.Text);
            DateTime fconsulta = Convert.ToDateTime(txtFConsulta.Text);
            hdfConsulta.Value = fconsulta.ToShortDateString();

            ConsultaCuentaBancariaSaldoFinal(corporativo, sucursal, banco, cuentabancaria, fconsulta);
            GenerarTablaCuentaBancariaSaldoFinal();
            LlenarGridViewCuentaBancoSaldoFinal();
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }

    protected void ddlBanco_SelectedIndexChanged(object sender, EventArgs e)
    {

        Carga_CuentaBancaria(Convert.ToInt16(ddlEmpresa.SelectedItem.Value),
            ddlBanco.Items.Count > 0 ? Convert.ToSByte(ddlBanco.SelectedItem.Value) : -1);
    }

    protected void ddlSucursal_DataBound(object sender, EventArgs e)
    {
        //ddlSucursal.Items.Insert(0, new ListItem("TODAS", "-1"));
    }

    protected void ddlBanco_DataBound(object sender, EventArgs e)
    {
        ddlBanco.Items.Insert(0, new ListItem("TODOS", "-1"));
        Carga_CuentaBancaria(Convert.ToInt16(ddlEmpresa.SelectedItem.Value),
            ddlBanco.Items.Count > 0 ? Convert.ToSByte(ddlBanco.SelectedItem.Value) : -1);
    }

    protected void ddlCuentaBancaria_DataBound(object sender, EventArgs e)
    {
        ddlCuentaBancaria.Items.Insert(0, new ListItem("TODAS", "-1"));
    }

    protected void ddlEmpresa_DataBound(object sender, EventArgs e)
    {
        if (ddlEmpresa.Items.Count > 0)
        {
            Carga_SucursalCorporativo(Convert.ToInt32(ddlEmpresa.SelectedItem.Value));
            Carga_Banco(Convert.ToInt32(ddlEmpresa.SelectedItem.Value));
        }
        else
        {
            ddlEmpresa.Items.Clear();
            ddlEmpresa.DataBind();
        }

    }

    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmpresa.Items.Count > 0)
        {
            Carga_SucursalCorporativo(Convert.ToInt32(ddlEmpresa.SelectedItem.Value));
            Carga_Banco(Convert.ToInt32(ddlEmpresa.SelectedItem.Value));
        }
        else
        {
            ddlEmpresa.Items.Clear();
            ddlEmpresa.DataBind();
        }
    }
    protected void grvCuentaBancoSaldoFinalDia_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;
        e.Row.Attributes["onmouseover"] = string.Format("RowMouseOver({0});", e.Row.RowIndex);
        e.Row.Attributes["onmouseout"] = string.Format("RowMouseOut({0});", e.Row.RowIndex);
        e.Row.Attributes["onclick"] = string.Format("RowSelect({0});", e.Row.RowIndex);
    }
    /////////////////////////////////////// EXPORTAR 
    protected void imgExportar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (grvCuentaBancoSaldoFinalDia.Rows.Count > 0)
                ExporttoExcel(grvCuentaBancoSaldoFinalDia);
            else
                App.ImplementadorMensajes.MostrarMensaje("No existe ningun resultado para exportar. Realice una consulta.");
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }
    }
    protected void imgBuscar_Click(object sender, ImageClickEventArgs e)
    {



    }
    #endregion
}