using Conciliacion.RunTime;
using Conciliacion.RunTime.ReglasDeNegocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Conciliacion_ConsultarDocumentos : System.Web.UI.Page
{
    private DataTable tblConsultarDocumentos = new DataTable("ConsultarDocumentos");
    public List<ConsultarMultiplesDocumentosTransBan> listaDocumentos = new List<ConsultarMultiplesDocumentosTransBan>();
    private ClaseFiltros filtros;
    private SeguridadCB.Public.Operaciones operaciones;
    private SeguridadCB.Public.Usuario usuario;

    protected void Nueva_Ventana(string Pagina, string Titulo, int Ancho, int Alto, int X, int Y)
    {
        ScriptManager.RegisterClientScriptBlock(this.upInicio,
                                            upInicio.GetType(),
                                            "ventana",
                                            "ShowWindow('" + Pagina + "','" + Titulo + "'," + Ancho + "," + Alto + "," + X + "," + Y + ")",
                                            true);
    }

    private void Consultar_Documentos()
    {
        try
        {
            listaDocumentos = Conciliacion.RunTime.App.Consultas.ConsultaConsultarMultiplesDocumentosTransBan(filtros.Conciliacion.Corporativo,filtros.Conciliacion.Sucursal,filtros.Conciliacion.Año,filtros.Conciliacion.Mes,filtros.Folio);
            HttpContext.Current.Session["LISTA_DOCUMENTOS"] = listaDocumentos;
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }
    private void GenerarTabla()
    {
        //string clave, DateTime fMovimiento, string cajaDescripcion, int caja, DateTime fOperacion, string tipoMovimientoCajaDescripcion, double total
        tblConsultarDocumentos.Columns.Add("Clave", typeof(string));
        tblConsultarDocumentos.Columns.Add("FMovimiento", typeof(DateTime));
        tblConsultarDocumentos.Columns.Add("CajaDescripcion", typeof(string));
        tblConsultarDocumentos.Columns.Add("Caja", typeof(int));
        tblConsultarDocumentos.Columns.Add("FOperacion", typeof(DateTime));
        tblConsultarDocumentos.Columns.Add("TipoMovimientoCajaDescripcion", typeof(string));
        tblConsultarDocumentos.Columns.Add("Total", typeof(decimal));

        foreach (ConsultarMultiplesDocumentosTransBan c in listaDocumentos)
        {
            tblConsultarDocumentos.Rows.Add(c.Clave,c.FMovimiento,c.CajaDescripcion,c.Caja,c.FOperacion,c.TipoMovimientoCajaDescripcion,c.Total);
        }
        HttpContext.Current.Session["TBL_CONSULTARDOCUMENTOSTRANSBAN"] = tblConsultarDocumentos;
    }

    private void VisualizarGrid()
    {
        DataTable tablaDocumentos = (DataTable)HttpContext.Current.Session["TBL_CONSULTARDOCUMENTOSTRANSBAN"];
        grid_cmdtb.DataSource = tablaDocumentos;
        grid_cmdtb.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            filtros = new ClaseFiltros();
            filtros = (ClaseFiltros)HttpContext.Current.Session["filtros"];
            //Label1.Text = "Empresa: " + filtros.Empresa+". Sucursal: "+filtros.Sucursal+". Grupo: " + filtros.Grupo+".";

            Consultar_Documentos();
            GenerarTabla();
            VisualizarGrid();

            miMenu.Visible = true;
            lnkReporteM.Attributes.Add("onclick", "return fnReporte()");
            lnkReporteM.Attributes.CssStyle.Add("opacity", "1");

        }            
    }

    protected void grid_cmdtb_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;
        e.Row.Attributes["onmouseover"] = string.Format("RowMouseOver({0});", e.Row.RowIndex);
        e.Row.Attributes["onmouseout"] = string.Format("RowMouseOut({0});", e.Row.RowIndex);
        e.Row.Attributes["onclick"] = string.Format("RowSelect({0});", e.Row.RowIndex);
    }

    protected void grid_cmdtb_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label l = e.Row.FindControl("lblIndice") as Label;
            l.Text = e.Row.RowIndex.ToString();
        }
        if (e.Row.RowType == DataControlRowType.Pager && (grid_cmdtb.DataSource != null))
        {

            Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumPaginas");
            _TotalPags.Text = grid_cmdtb.PageCount.ToString();

            DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
            for (int i = 1; i <= Convert.ToInt32(grid_cmdtb.PageCount); i++)
            {
                list.Items.Add(i.ToString());
            }
            list.SelectedValue = (grid_cmdtb.PageIndex + 1).ToString();
        }
    }

    protected void paginasDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag;
        grid_cmdtb.PageIndex = int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 &&
                                    iNumPag <= grid_cmdtb.PageCount
                                        ? iNumPag - 1
                                        : 0;
        VisualizarGrid();
    }

    protected void grid_cmdtb_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grid_cmdtb.PageIndex = e.NewPageIndex;
            VisualizarGrid();
        }
        catch (Exception)
        {

        }
    }

    protected void lnkReporte_Click(object sender, EventArgs e)
    {
        AppSettingsReader settings = new AppSettingsReader();
        
        ClaseFiltros filtro = new ClaseFiltros();
        filtro = (ClaseFiltros)HttpContext.Current.Session["filtros"];
        string indice = fldIndiceConcilacion.Value.Trim();
        string clave = Convert.ToString(grid_cmdtb.DataKeys[Convert.ToInt32(indice)].Value);

        List<ConsultarMultiplesDocumentosTransBan> lista = (List<ConsultarMultiplesDocumentosTransBan>)HttpContext.Current.Session["LISTA_DOCUMENTOS"];

        ConsultarMultiplesDocumentosTransBan dato = lista.Find(x => x.Clave.Equals(clave));

        string strReporte = Server.MapPath("~/") + settings.GetValue("RutaComprobanteDeCaja", typeof(string));
        if (!File.Exists(strReporte)) return;
        try
        {
            string strServer = settings.GetValue("Servidor", typeof(string)).ToString();
            string strDatabase = settings.GetValue("Base", typeof(string)).ToString();
            usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];

            string strUsuario = usuario.IdUsuario.Trim();
            string strPW = usuario.ClaveDesencriptada;
            ArrayList Par = new ArrayList();

            Par.Add("@Consecutivo=" + dato.Consecutivo);
            Par.Add("@Folio=" + dato.Folio);
            Par.Add("@Caja=" + dato.Caja);
            Par.Add("@FOperacion=" + dato.FOperacion.ToString("dd/MM/yyyy HH:mm:ss"));

            ClaseReporte Reporte = new ClaseReporte(strReporte, Par, strServer, strDatabase, strUsuario, strPW);
            HttpContext.Current.Session["RepDoc"] = Reporte.RepDoc;
            HttpContext.Current.Session["ParametrosReporte"] = Par;
            Nueva_Ventana("../Reporte/Reporte.aspx", "Carta", 0, 0, 0, 0);
            Reporte = null;
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error: " + ex.Message);
        }
    }
}