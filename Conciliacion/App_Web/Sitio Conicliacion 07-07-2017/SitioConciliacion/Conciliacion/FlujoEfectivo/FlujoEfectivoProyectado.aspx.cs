using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Conciliacion.Migracion.Runtime.ReglasNegocio;
using Conciliacion.RunTime;
using Conciliacion.RunTime.ReglasDeNegocio;
using SeguridadCB.Public;
using WebGridViewTemplateField;
using Consultas = Conciliacion.RunTime.ReglasDeNegocio.Consultas;
using DataGrid = System.Web.UI.WebControls.DataGrid;
using TextBox = System.Web.UI.WebControls.TextBox;


public partial class Conciliacion_FlujoEfectivo_FlujoEfectivoProyectado : System.Web.UI.Page
{
    #region "Propiedades Globales"
    private SeguridadCB.Public.Operaciones operaciones;
    private SeguridadCB.Public.Usuario usuario;
    #endregion
    private StringBuilder mensaje;
    private List<ListaCombo> listSucursales = new List<ListaCombo>();
    private List<FlujoProyectado> listFlujoRealProyectado = new List<FlujoProyectado>();
    public string mesesMaximo;
    DataTable dtbFlujoRealProyectado = new DataTable();
    DataTable tablaFlujoEfectivoEntrada = new DataTable();
    DataTable tablaFlujoEfectivoSalida = new DataTable();
    DataTable tablaFlujoEfectivoSaldos = new DataTable();
    int corporativo, sucursal;
    DateTime finicio, ffin;
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
                if ((!HttpContext.Current.Request.UrlReferrer.AbsoluteUri.Contains("SitioConciliacion")) || (HttpContext.Current.Request.UrlReferrer.AbsoluteUri.Contains("Inicio.aspx")))
                {
                    HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
                    HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
                    Response.Cache.SetExpires(DateTime.Now);
                }
            }
            if (!Page.IsPostBack)
            {

                usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
                if (ddlEmpresa.Items.Count == 0) Carga_Corporativo();
                this.ddlEmpresa.Focus();
                //ActivarDataPicker();
            }
            else
            {
                /////////////////////////////
                tablaFlujoEfectivoEntrada = (DataTable)HttpContext.Current.Session["TAB_FLUJO_E"];
                ////ENTRADA
                if (tablaFlujoEfectivoEntrada != null && tablaFlujoEfectivoEntrada.Rows.Count > 0)
                    LlenaGridViewFlujoEfectivo(Consultas.TipoTransferencia.Entrada);
                ////SALIDA
                tablaFlujoEfectivoSalida = (DataTable)HttpContext.Current.Session["TAB_FLUJO_S"];
                if (tablaFlujoEfectivoSalida != null && tablaFlujoEfectivoSalida.Rows.Count > 0)
                    LlenaGridViewFlujoEfectivo(Consultas.TipoTransferencia.Salida);
                /////////////////////////////
            }

            //ActivarDataPicker();
            Parametros p = Session["Parametros"] as Parametros;
            AppSettingsReader settings = new AppSettingsReader();
            short modulo = Convert.ToSByte(settings.GetValue("Modulo", typeof(string)));
            mesesMaximo = p.ValorParametro(modulo, "NumMesesFlujo");

        }

        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error: Cargar la Pagina\n" + ex.Message);
        }
    }


    public void ActivarDataPicker()
    {
        Parametros p = Session["Parametros"] as Parametros;
        AppSettingsReader settings = new AppSettingsReader();
        short modulo = Convert.ToSByte(settings.GetValue("Modulo", typeof(string)));

        string mesesMaximo = p.ValorParametro(modulo, "NumMesesFlujo");


        string key = "DatePickerFechas";
        string javascript = String.Format("return activarDatePickers({0});", mesesMaximo);

        ScriptManager.RegisterClientScriptBlock(this.upFlujoProyectado, this.upFlujoProyectado.GetType(), key, javascript, true);
    }

    private bool FiltroCorrecto()
    {
        bool resultado = true;
        mensaje = new StringBuilder();
        if (ddlEmpresa.Equals(null) || ddlEmpresa.Items.Count == 0)
        {
            mensaje.Append("Corporativo");
            resultado = false;
        }
        else if (ddlSucursal.Equals(null) || ddlSucursal.Items.Count == 0)
        {
            mensaje.Append("Sucursal");
            resultado = false;
        }
        else if (String.IsNullOrEmpty(txtFInicial.Text))
        {
            mensaje.Append("Fecha Inicial");
            resultado = false;
        }
        else if (String.IsNullOrEmpty(txtFFinal.Text))
        {
        }
        return resultado;
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
            Conciliacion.Migracion.Runtime.App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }
    /// <summary>
    /// Llena el Combo de Sucurales según Corporativo
    /// </summary>
    public void Carga_SucursalCorporativo(int corporativo)
    {
        try
        {
            listSucursales = Conciliacion.RunTime.App.Consultas.ConsultaSucursales(Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionIden0.Sin0, corporativo);
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
    protected void ddlEmpresa_DataBound(object sender, EventArgs e)
    {
        if (ddlEmpresa.Items.Count > 0)
        {
            Carga_SucursalCorporativo(Convert.ToInt32(ddlEmpresa.SelectedItem.Value));

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

        }
        else
        {
            ddlEmpresa.Items.Clear();
            ddlEmpresa.DataBind();
        }
    }



    //public DataTable TablaCruzada(DataTable table)
    //{
    //    DataTable newTable = new DataTable();

    //    for (int r = 0; r <= table.Rows.Count; r++)
    //    {
    //        newTable.Columns.Add(r.ToString());
    //    }

    //    for (int c = 0; c < table.Columns.Count; c++)
    //    {
    //        DataRow row = newTable.NewRow();
    //        object[] rowValues = new object[table.Rows.Count + 1];

    //        rowValues[0] = table.Columns[c].Caption;

    //        for (int r = 1; r <= table.Rows.Count; r++)
    //        {
    //            rowValues[r] = table.Rows[r - 1][c].ToString();
    //        }

    //        row.ItemArray = rowValues;
    //        newTable.Rows.Add(row);
    //    }

    //    return newTable;
    //}
    private void ConsultaFlujoEfectivo(int corporativo, int sucursal, Consultas.TipoTransferencia tipotransferencia, DateTime fInicial, DateTime fFinal)
    {
        System.Data.SqlClient.SqlConnection connection = SeguridadCB.Seguridad.Conexion;
        if (connection.State == ConnectionState.Closed)
        {
            SeguridadCB.Seguridad.Conexion.Open();
        }
        try
        {
            listFlujoRealProyectado =
                Conciliacion.RunTime.App.Consultas.ConsultaFlujoEfectivo(corporativo, sucursal, tipotransferencia, fInicial, fFinal);

            switch (tipotransferencia)
            {
                case Consultas.TipoTransferencia.Entrada:
                    Session["FLUJO_EFECTIVO_E"] = listFlujoRealProyectado;
                    break;
                case Consultas.TipoTransferencia.Salida:
                    Session["FLUJO_EFECTIVO_S"] = listFlujoRealProyectado;
                    break;
                default:
                    Session["FLUJO_EFECTIVO_SL"] = listFlujoRealProyectado;
                    break;
            }
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }
    public void GenerarTablaFlujoRealProyectado(Consultas.TipoTransferencia tipotransferencia)
    {
        dtbFlujoRealProyectado = new DataTable("FlujoEfectivo");

        dtbFlujoRealProyectado.Columns.Add("Corporativo", typeof(int));
        dtbFlujoRealProyectado.Columns.Add("Año", typeof(int));
        dtbFlujoRealProyectado.Columns.Add("Mes", typeof(int));
        dtbFlujoRealProyectado.Columns.Add("StatusConcepto", typeof(int));
        dtbFlujoRealProyectado.Columns.Add("Concepto", typeof(string));
        dtbFlujoRealProyectado.Columns.Add("TipoTransferencia", typeof(int));
        dtbFlujoRealProyectado.Columns.Add("FFlujo", typeof(string));
        //dtbFlujoProyectado.Columns.Add("ImporteProyectado", typeof(decimal));
        dtbFlujoRealProyectado.Columns.Add("Importe", typeof(decimal));

        foreach (FlujoProyectado rcc in listFlujoRealProyectado)
            dtbFlujoRealProyectado.Rows.Add(
                 rcc.Corporativo,
                 rcc.Año,
                 rcc.Mes,
                 rcc.StatusConcepto,
                 String.Format("{0}.{1}", rcc.StatusConcepto, rcc.StatusConceptoDes),//Solo para poder leer el StatusConcepto al guardar
                 rcc.TipoTransferencia,
                 rcc.FFlujo.ToString("d").ToUpper(),
                 rcc.TipoFlujo.Equals("N/A") ? rcc.ImporteReal :
                    rcc.TipoFlujo.Equals("REAL") ? rcc.ImporteReal : rcc.ImporteProyectado //Aqui la validacion para ver que Importe se Mostrara al Usuario
                //dependiendo del que tipo de Flujo es y  segun la fecha Actual
                 );

        switch (tipotransferencia)
        {
            case Consultas.TipoTransferencia.Entrada:
                HttpContext.Current.Session["TAB_FLUJO_E"] = GetInversedDataTable(dtbFlujoRealProyectado, "FFlujo", "Concepto", "Importe", "0.00", true);
                break;
            case Consultas.TipoTransferencia.Salida:
                HttpContext.Current.Session["TAB_FLUJO_S"] = GetInversedDataTable(dtbFlujoRealProyectado, "FFlujo", "Concepto", "Importe", "0.00", true);
                break;
            default:
                HttpContext.Current.Session["TAB_FLUJO_SL"] = GetInversedDataTable(dtbFlujoRealProyectado, "FFlujo", "Concepto", "Importe", "0.00", true);
                break;
        }

    }

    public void GenerarGridViewDinamicoFlujoRealProyectado(Consultas.TipoTransferencia tipo, DataTable dt, GridView grdDinamico)
    {
        try
        {
            //grvFlujoEfectivoEntrada.Columns.Clear();
            grdDinamico.Columns.Clear();
            string key = "onkeypress";
            string funcionJS = "return ValidNumDecimal(event, true)";
            BoundField tempConcepto = new BoundField();
            tempConcepto.DataField = "Concepto";
            tempConcepto.HeaderText = "Concepto";
            tempConcepto.ItemStyle.HorizontalAlign = HorizontalAlign.Justify;
            tempConcepto.ItemStyle.Width = new Unit(200);
            //grvFlujoEfectivoEntrada.Columns.Add(tempConcepto);
            grdDinamico.Columns.Add(tempConcepto);
            if (String.IsNullOrEmpty(CorporativoConsulta.Value)) return; corporativo = Convert.ToInt16(CorporativoConsulta.Value);
            foreach (TemplateField tmfField in from
                                               DataColumn col in dt.Columns
                                               where !col.ColumnName.Equals("Concepto") && !col.ColumnName.Equals("Total")
                                               let enabled = !(Convert.ToDateTime(col.ColumnName) < DateTime.Now)
                                               let año = (Convert.ToDateTime(col.ColumnName)).Year
                                               let mes = (Convert.ToDateTime(col.ColumnName)).Month
                                               let cierremes = CierreMes(tipo, corporativo, año, mes)
                                               let css = string.Format("cajaTextoEditar centradoDerecha {0}", (tipo != Consultas.TipoTransferencia.EntradaSalida ? (enabled ? "border-color-azulClaro" : cierremes ? "border-color-verdeClaro exito" : "border-color-amarillo espera") : "border-color-verdeOscuro"))
                                               select new TemplateField
            {
                HeaderText = col.ColumnName,
                ItemTemplate = new TextBoxTemplate(ListItemType.Item, col.ColumnName, (tipo != Consultas.TipoTransferencia.EntradaSalida ? enabled : false), css, key, funcionJS)
                //, String.Format("txt{0}", col.ColumnName
                //FooterTemplate = new TextBoxTemplate(ListItemType.Footer, col.ColumnName, false, "cajaTextoEditar centradoDerecha bg-color-grisOscuro fg-color-blanco", key, funcionJS)//, String.Format("ft{0}", col.ColumnName)
            })
            {
                grdDinamico.Columns.Add(tmfField);
            }
            //Total Fila
            BoundField bfTotal = new BoundField();
            bfTotal.HeaderText = "Total";
            bfTotal.DataField = "Total";
            bfTotal.ItemStyle.HorizontalAlign = HorizontalAlign.Justify;
            bfTotal.ItemStyle.Width = new Unit(120);
            bfTotal.ItemStyle.Font.Size = new FontUnit(10);
            bfTotal.ItemStyle.CssClass = "cajaTextoEditar centradoDerecha bg-color-grisOscuro fg-color-blanco";


            //bfTotal.FooterStyle.Width = new Unit(120);
            //bfTotal.FooterStyle.Font.Size = new FontUnit(10);
            //bfTotal.FooterStyle.CssClass = "cajaTextoEditar centradoDerecha bg-color-grisOscuro fg-color-blanco";


            grdDinamico.Columns.Add(bfTotal);
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error al Crear las Columnas Dinamicas.\nError:" + ex.Message);
        }
    }

    public bool CierreMes(Consultas.TipoTransferencia tipo, int corporativo, int año, int mes)
    {
        return tipo == Consultas.TipoTransferencia.Entrada
            ? (Session["FLUJO_EFECTIVO_E"] as List<FlujoProyectado>).Find(
                x => x.Corporativo == corporativo && x.Año == año && x.Mes == mes)
                .StatusMes.Equals("CONCILIACION CERRADA")
            : tipo == Consultas.TipoTransferencia.Salida ?
                (Session["FLUJO_EFECTIVO_S"] as List<FlujoProyectado>).Find(
                x => x.Corporativo == corporativo && x.Año == año && x.Mes == mes)
                .StatusMes.Equals("CONCILIACION CERRADA")
                : true;
    }

    private void LlenaGridViewFlujoEfectivo(Consultas.TipoTransferencia tipotrasnferencia)
    {
        switch (tipotrasnferencia)
        {
            case Consultas.TipoTransferencia.Entrada:
                tablaFlujoEfectivoEntrada = (DataTable)HttpContext.Current.Session["TAB_FLUJO_E"];
                GenerarGridViewDinamicoFlujoRealProyectado(tipotrasnferencia, tablaFlujoEfectivoEntrada, grvFlujoEfectivoEntrada);
                grvFlujoEfectivoEntrada.DataSource = tablaFlujoEfectivoEntrada;
                grvFlujoEfectivoEntrada.DataBind();
                break;
            case Consultas.TipoTransferencia.Salida:
                tablaFlujoEfectivoSalida = (DataTable)HttpContext.Current.Session["TAB_FLUJO_S"];
                GenerarGridViewDinamicoFlujoRealProyectado(tipotrasnferencia, tablaFlujoEfectivoSalida, grvFlujoEfectivoSalida);
                grvFlujoEfectivoSalida.DataSource = tablaFlujoEfectivoSalida;
                grvFlujoEfectivoSalida.DataBind();
                break;
            default:
                tablaFlujoEfectivoSaldos = (DataTable)HttpContext.Current.Session["TAB_FLUJO_SL"];
                GenerarGridViewDinamicoFlujoRealProyectado(tipotrasnferencia, tablaFlujoEfectivoSaldos, grvFlujoEfectivoSaldos);
                grvFlujoEfectivoSaldos.DataSource = tablaFlujoEfectivoSaldos;
                grvFlujoEfectivoSaldos.DataBind();
                break;
        }



    }
    protected void btnActualizarConfig_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (FiltroCorrecto())
            {
                //Leer valores del filtro
                corporativo = Convert.ToInt16(ddlEmpresa.SelectedItem.Value);
                sucursal = Convert.ToInt16(ddlSucursal.SelectedItem.Value);
                finicio = Convert.ToDateTime(txtFInicial.Text);
                ffin = Convert.ToDateTime(txtFFinal.Text);
                hdfFConsulta.Value = finicio.ToShortDateString();
                //Guardar Valores de la Consulta
                CorporativoConsulta.Value = corporativo.ToString();
                SucursalConsulta.Value = sucursal.ToString();
                FInicioConsulta.Value = finicio.ToString();
                FFinConsulta.Value = ffin.ToString();

                ////////////////////////ENTRADA(INGRESOS)

                ConsultaFlujoEfectivo(
                          corporativo,
                          sucursal,
                          Consultas.TipoTransferencia.Entrada,
                          finicio,
                          ffin
                          );
                GenerarTablaFlujoRealProyectado(Consultas.TipoTransferencia.Entrada);

                CalcularTotalesDiasDT(Consultas.TipoTransferencia.Entrada);
                CalcularTotalesFilasDT(Consultas.TipoTransferencia.Entrada);
                LlenaGridViewFlujoEfectivo(Consultas.TipoTransferencia.Entrada);
                //CalcularTotalesColumnas(Consultas.TipoTransferencia.Entrada);
                //CalcularTotalesFilas(Consultas.TipoTransferencia.Entrada);
                //////////////////////////SALIDA(EGRESOS)

                ConsultaFlujoEfectivo(
                          corporativo,
                          sucursal,
                          Consultas.TipoTransferencia.Salida,
                          finicio,
                          ffin
                          );
                GenerarTablaFlujoRealProyectado(Consultas.TipoTransferencia.Salida);
                CalcularTotalesDiasDT(Consultas.TipoTransferencia.Salida);
                CalcularTotalesFilasDT(Consultas.TipoTransferencia.Salida);
                LlenaGridViewFlujoEfectivo(Consultas.TipoTransferencia.Salida);
                //CalcularTotalesColumnas(Consultas.TipoTransferencia.Salida);
                //CalcularTotalesFilas(Consultas.TipoTransferencia.Salida);
                ///////////////////////////SALDOS
                ConsultaFlujoEfectivo(
                          corporativo,
                          sucursal,
                          Consultas.TipoTransferencia.EntradaSalida,
                          finicio,
                          ffin
                          );
                GenerarTablaFlujoRealProyectado(Consultas.TipoTransferencia.EntradaSalida);
                //CalcularTotalesDiasDT(Consultas.TipoTransferencia.EntradaSalida);
                 CalcularSaldoPeriodoDiaDT();
                CalcularTotalesFilasDT(Consultas.TipoTransferencia.EntradaSalida);
                LlenaGridViewFlujoEfectivo(Consultas.TipoTransferencia.EntradaSalida);
                //CalcularTotalesColumnas(Consultas.TipoTransferencia.Salida);
                //CalcularTotalesFilas(Consultas.TipoTransferencia.EntradaSalida);
               
                //CalcularSaldoPeriodoDia();
            }

            else
                App.ImplementadorMensajes.MostrarMensaje("Dato Incorrecto: " + mensaje + ".\nVerifique su Selección");

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }


    }

    public void CalcularTotalesDiasDT(Consultas.TipoTransferencia tipotransferencia)
    {
        try
        {
            DataTable dtFlujo = tipotransferencia == Consultas.TipoTransferencia.Entrada ?

                                                                    (DataTable)HttpContext.Current.Session["TAB_FLUJO_E"] :
                                                                    (DataTable)HttpContext.Current.Session["TAB_FLUJO_S"];


            DataRow newRow = dtFlujo.NewRow();
            newRow["Concepto"] = tipotransferencia == Consultas.TipoTransferencia.Entrada
                                                                           ? "SUMA INGRESOS"
                                                                           : "SUMA EGRESOS";
            for (int c = 1; c < dtFlujo.Columns.Count; c++)
            {
                decimal total = dtFlujo.Rows.Cast<DataRow>().Aggregate<DataRow, decimal>(0, (x, dr) => x + Convert.ToDecimal(dr[dtFlujo.Columns[c].ColumnName].ToString()));
                newRow[dtFlujo.Columns[c].ColumnName] = total;
            }
            dtFlujo.Rows.Add(newRow);

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error en el momento del calculo de los TOTALES X DIA: \nError:" + ex.Source + ":" + ex.StackTrace);
        }
    }

    public void CalcularTotalesFilasDT(Consultas.TipoTransferencia tipotransferencia)
    {
        try
        {
            DataTable dtFlujo = tipotransferencia == Consultas.TipoTransferencia.Entrada ?
                                                                    (DataTable)HttpContext.Current.Session["TAB_FLUJO_E"] :
                                                                    tipotransferencia == Consultas.TipoTransferencia.Salida ?
                                                                    (DataTable)HttpContext.Current.Session["TAB_FLUJO_S"] : (DataTable)HttpContext.Current.Session["TAB_FLUJO_SL"];


            dtFlujo.Columns.Add("Total", typeof(string));
            decimal totalINGRESOSEGRESOS = 0;
            int columnaFinalTotalFila = dtFlujo.Columns.Count - 1;
            for (int r = 0; r < dtFlujo.Rows.Count; r++)
            {
                decimal total = (from DataColumn col in dtFlujo.Columns
                                 where !col.ColumnName.Equals("Concepto") && !col.ColumnName.Equals("Total")
                                 select Convert.ToDecimal(dtFlujo.Rows[r][col.ColumnName].ToString())).Aggregate<decimal, decimal>(0, (x, valor) => x + valor);
                totalINGRESOSEGRESOS = totalINGRESOSEGRESOS + total;
                //grvFlujoEfectivo.Rows[r].Cells[columnaFinalTotalFila].Text = total.ToString("C2");
                dtFlujo.Rows[r][columnaFinalTotalFila] = total.ToString("C2");
            }


        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error en el momento del calculo de los TOTALES X DIA: \nError:" + ex.Source + ":" + ex.StackTrace);
        }
    }

    public void CalcularTotalesColumnas(Consultas.TipoTransferencia tipotransferencia)
    {
        try
        {
            GridView grvFlujoEfectivo = tipotransferencia == Consultas.TipoTransferencia.Entrada
                                                                ? grvFlujoEfectivoEntrada
                                                                : grvFlujoEfectivoSalida;
            grvFlujoEfectivo.FooterRow.Cells[0].Text = tipotransferencia == Consultas.TipoTransferencia.Entrada
                                                                            ? "SUMA INGRESOS"
                                                                            : "SUMA EGRESOS";
            DataTable dtFlujo = tipotransferencia == Consultas.TipoTransferencia.Entrada ?
                                                        (DataTable)HttpContext.Current.Session["TAB_FLUJO_E"] :
                                                        (DataTable)HttpContext.Current.Session["TAB_FLUJO_S"];

            for (int c = 1; c < dtFlujo.Columns.Count; c++)
            {
                TextBox txtFooter = grvFlujoEfectivo.FooterRow.Cells[c].Controls[0] as TextBox;
                decimal total = dtFlujo.Rows.Cast<DataRow>().Aggregate<DataRow, decimal>(0, (x, dr) => x + Convert.ToDecimal(dr[dtFlujo.Columns[c].ColumnName].ToString()));
                if (txtFooter != null) txtFooter.Text = total.ToString("C2");
            }
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error en el momento del calculo de los TOTALES X DIA: \nError:" + ex.Source + ":" + ex.StackTrace);
        }
    }

    public void CalcularTotalesFilas(Consultas.TipoTransferencia tipotransferencia)
    {
        try
        {
            GridView grvFlujoEfectivo = tipotransferencia == Consultas.TipoTransferencia.Entrada
                                                                ? grvFlujoEfectivoEntrada
                                                                : tipotransferencia == Consultas.TipoTransferencia.Salida ?
                                                                grvFlujoEfectivoSalida : grvFlujoEfectivoSaldos;
            DataTable dtFlujo = tipotransferencia == Consultas.TipoTransferencia.Entrada ?
                                                        (DataTable)HttpContext.Current.Session["TAB_FLUJO_E"] :
                                                        tipotransferencia == Consultas.TipoTransferencia.Salida ?
                                                        (DataTable)HttpContext.Current.Session["TAB_FLUJO_S"] :
                                                        (DataTable)HttpContext.Current.Session["TAB_FLUJO_SL"];
            int columnaFinalTotalFila = grvFlujoEfectivo.Columns.Count - 1;
            decimal totalINGRESOSEGRESOS = 0;
            for (int r = 0; r < dtFlujo.Rows.Count; r++)
            {
                decimal total = (from DataColumn col in dtFlujo.Columns
                                 where !col.ColumnName.Equals("Concepto") && !col.ColumnName.Equals("Total")
                                 select Convert.ToDecimal(dtFlujo.Rows[r][col.ColumnName].ToString())).Aggregate<decimal, decimal>(0, (x, valor) => x + valor);
                totalINGRESOSEGRESOS = totalINGRESOSEGRESOS + total;
                grvFlujoEfectivo.Rows[r].Cells[columnaFinalTotalFila].Text = total.ToString("C2");

            }
            //Coloca el Total / Total
            grvFlujoEfectivo.FooterRow.Cells[columnaFinalTotalFila].Text = totalINGRESOSEGRESOS.ToString("C2");
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error en el momento del calculo de los TOTALES X CONCEPTO: \nError:" + ex.Source + ":" + ex.StackTrace + "\nMensaje:" + ex.Message);
        }
    }

    public void CalcularSaldoPeriodoDia()
    {
        try
        {
            if (grvFlujoEfectivoEntrada.Rows.Count <= 0 || grvFlujoEfectivoSalida.Rows.Count <= 0) return;
            DataTable dtFlujoSaldo = (DataTable)HttpContext.Current.Session["TAB_FLUJO_SL"];

            for (int c = 1; c < dtFlujoSaldo.Columns.Count; c++)
            {
                decimal totalIngresoDia =
                    ObtenerValorDecimal(
                        (grvFlujoEfectivoEntrada.Rows[grvFlujoEfectivoEntrada.Rows.Count - 1].Cells[c].Controls[0] as
                            TextBox).Text);
                decimal totalEgresosDia =
                  ObtenerValorDecimal(
                       (grvFlujoEfectivoSalida.Rows[grvFlujoEfectivoSalida.Rows.Count - 1].Cells[c].Controls[0] as
                           TextBox).Text);

                (grvFlujoEfectivoSaldos.Rows[0].Cells[c].Controls[0] as TextBox).Text = (totalIngresoDia -
                                                                                        totalEgresosDia).ToString("C2");

            }
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(
                "Error en el momento del calculo del Saldo del DIA \nError:" + ex.Source);
        }

    }
    public void CalcularSaldoPeriodoDiaDT()
    {
        try
        {
            if (grvFlujoEfectivoEntrada.Rows.Count <= 0 || grvFlujoEfectivoSalida.Rows.Count <= 0) return;
            DataTable dtFlujoSaldo = (DataTable)HttpContext.Current.Session["TAB_FLUJO_SL"];
            

            for (int c = 1; c < dtFlujoSaldo.Columns.Count; c++)
            {
                decimal totalIngresoDia =
                    ObtenerValorDecimal(
                        (grvFlujoEfectivoEntrada.Rows[grvFlujoEfectivoEntrada.Rows.Count - 1].Cells[c].Controls[0] as
                            TextBox).Text);
                decimal totalEgresosDia =
                  ObtenerValorDecimal(
                       (grvFlujoEfectivoSalida.Rows[grvFlujoEfectivoSalida.Rows.Count - 1].Cells[c].Controls[0] as
                           TextBox).Text);

                //(grvFlujoEfectivoSaldos.Rows[0].Cells[c].Controls[0] as TextBox).Text = (totalIngresoDia -
                //                                                                        totalEgresosDia).ToString("C2");

                dtFlujoSaldo.Rows[0][c]=(totalIngresoDia -totalEgresosDia);
            }
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(
                "Error en el momento del calculo del Saldo del DIA \nError:" + ex.Source);
        }

    }

    /// <summary>
    /// Obtiene el DataTable Cruzado 
    /// </summary>
    /// <param name="Tabla">DataTable parametro</param>
    /// <param name="columnX">Clumna Eje X</param>
    /// <param name="columnY">Clumna Eje Y</param>
    /// <param name="columnZ">Clumna Eje Z(valores)</param>
    /// <param name="columnsAIgnorar">Si se desea ingnorar alguna columan, este parametro lo admite
    /// provided here</param>
    /// <param name="nullValue">Si es NULL que valor colocara en su lugar</param> 
    /// <returns>C# Metodo Tabla Pivite - Felipe Sabino</returns>
    public static DataTable GetInversedDataTable(DataTable table, string columnX,
         string columnY, string columnZ, string nullValue, bool sumValues)
    {
        //Crear un DataTable
        DataTable returnTable = new DataTable();

        if (columnX == "")
            columnX = table.Columns[0].ColumnName;

        //Agregar una columna al comienzo de la tabla
        returnTable.Columns.Add(columnY);



        //Leer todos los valores distintos de Columna Columnx en el DataTale proporcionado
        List<string> columnXValues = new List<string>();

        foreach (DataRow dr in table.Rows)
        {

            string columnXTemp = dr[columnX].ToString();
            if (columnXValues.Contains(columnXTemp)) continue;
            // Leer cada valor de la fila, si es diferente de los demás previstos, se suman a
            // la lista de valores y crea una nueva columna con su valor.
            columnXValues.Add(columnXTemp);
            returnTable.Columns.Add(columnXTemp);
        }

        // Verifique si Y y Z del Eje columnas  existen en el DateTable
        if (columnY != "" && columnZ != "")
        {
            //Leer los Valores Distintos para Columba Y Axis
            List<string> columnYValues = new List<string>();

            foreach (DataRow dr in table.Rows)
            {
                if (!columnYValues.Contains(dr[columnY].ToString()))
                {
                    columnYValues.Add(dr[columnY].ToString());

                }
            }
            //Recorrer los valores Distintos de Y
            foreach (string columnYValue in columnYValues)
            {
                //Crear una nueva Fila
                DataRow drReturn = returnTable.NewRow();
                drReturn[0] = columnYValue;
                //foreach column Y value, The rows are selected distincted
                DataRow[] rows = table.Select(columnY + "='" + columnYValue + "'");

                //Read each row to fill the DataTable
                foreach (DataRow dr in rows)
                {
                    string rowColumnTitle = dr[columnX].ToString();

                    //Read each column to fill the DataTable
                    foreach (DataColumn dc in returnTable.Columns)
                    {
                        if (dc.ColumnName == rowColumnTitle)
                        {
                            //If Sum of Values is True it try to perform a Sum
                            //If sum is not possible due to value types, the value 
                            // displayed is the last one read
                            if (sumValues)
                            {
                                try
                                {
                                    drReturn[rowColumnTitle] =
                                         Convert.ToDecimal(drReturn[rowColumnTitle]) +
                                         Convert.ToDecimal(dr[columnZ]);
                                }
                                catch
                                {
                                    drReturn[rowColumnTitle] = dr[columnZ];
                                }
                            }
                            else
                            {
                                drReturn[rowColumnTitle] = dr[columnZ];
                            }
                        }
                    }
                }
                returnTable.Rows.Add(drReturn);
            }
        }
        else
        {
            App.ImplementadorMensajes.MostrarMensaje("Las columnas proporcionadas no existen en la estructura de la Tabla");
        }

        //if a nullValue is provided, fill the datable with it
        if (nullValue != "")
        {
            foreach (DataRow dr in returnTable.Rows)
            {
                foreach (DataColumn dc in returnTable.Columns)
                {
                    if (dr[dc.ColumnName].ToString() == "")
                        dr[dc.ColumnName] = nullValue;
                }
            }
        }

        return returnTable;
    }
    protected void grvFlujoEfectivoEntrada_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string statusconceptodes = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Concepto"));
                e.Row.Cells[0].CssClass = statusconceptodes.Equals("SUMA INGRESOS") ? "bg-color-grisOscuro fg-color-blanco" : "bg-color-verdeClaro fg-color-blanco";
                
                //switch (fp.TipoTransferencia)
                //{
                //    case 2:
                //        e.Row.Cells[0].CssClass = "bg-color-azulClaro01 fg-color-blanco";
                //        break;
                //    case 3:
                //        e.Row.Cells[0].CssClass = "bg-color-verdeClaro fg-color-blanco";
                //        break;
                //    default:
                //        e.Row.CssClass = "bg-color-grisOscuro fg-color-blanco";
                //        break;
                //}
            }
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }
    protected void grvFlujoEfectivoSalida_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string statusconceptodes = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Concepto"));
                e.Row.Cells[0].CssClass = statusconceptodes.Equals("SUMA EGRESOS") ? "bg-color-grisOscuro fg-color-blanco" : "bg-color-azulClaro01 fg-color-blanco";

                //string statusconceptodes = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Concepto"));
                //short statusconcepto = Convert.ToSByte(statusconceptodes.Substring(0, statusconceptodes.IndexOf('.')));
                //listFlujoProyectado = Session["FLUJO_EFECTIVO_E"] as List<FlujoProyectado>;
                //FlujoProyectado fp = listFlujoProyectado.Find(x => x.StatusConcepto == statusconcepto);

                //switch (fp.TipoTransferencia)
                //{
                //    case 2:
                //        e.Row.Cells[0].CssClass = "bg-color-azulClaro01 fg-color-blanco";
                //        break;
                //    case 3:
                //        e.Row.Cells[0].CssClass = "bg-color-verdeClaro fg-color-blanco";
                //        break;
                //    default:
                //        e.Row.CssClass = "bg-color-grisOscuro fg-color-blanco";
                //        break;
                //}
            }
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }
    protected void grvFlujoEfectivoSaldos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                string statusconceptodes = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Concepto"));
                short statusconcepto = Convert.ToSByte(statusconceptodes.Substring(0, statusconceptodes.IndexOf('.')));

                //statusconceptodes = statusconceptodes.Substring(statusconceptodes.IndexOf('.')+1,statusconceptodes.Length - 3);
                e.Row.CssClass = statusconcepto == 1 ? "bg-color-amarillo fg-color-blanco" : "bg-color-verdeFE fg-color-blanco";
                e.Row.Cells[0].CssClass = "bg-color-grisOscuro fg-color-blanco";
            }

        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error:\n" + ex.Message);
        }
    }
    public bool GuardarFlujoRealProyectado(Consultas.TipoTransferencia tipoTransferencia, GridView grvFlujoEfectivo)
    {
        bool resultado = true;
        try
        {

            if (grvFlujoEfectivo.Rows.Count > 0)
            {
                int corporativo = Convert.ToInt16(CorporativoConsulta.Value);
                short mes;
                int año;
                string statusconceptodes = null;
                short statusconcepto;
                DateTime fflujo;
                FlujoProyectado fp;
                listFlujoRealProyectado = tipoTransferencia == Consultas.TipoTransferencia.Entrada
                    ? Session["FLUJO_EFECTIVO_E"] as List<FlujoProyectado>
                    : Session["FLUJO_EFECTIVO_S"] as List<FlujoProyectado>
                    ;

                usuario = Session["Usuario"] as Usuario;
                int columnaTotalesFila = grvFlujoEfectivo.Columns.Count - 1;
                foreach (GridViewRow row in grvFlujoEfectivo.Rows)
                {
                    if (!resultado) break;
                    var dataKey = grvFlujoEfectivo.DataKeys[row.RowIndex];

                    if (dataKey != null)
                        statusconceptodes = Convert.ToString(dataKey.Values["Concepto"]);
                    statusconcepto = Convert.ToSByte(statusconceptodes.Substring(0, statusconceptodes.IndexOf('.')));
                    if (statusconcepto == 0)
                        continue;
                    if (row.RowType != DataControlRowType.DataRow)
                        continue;
                    for (int celda = 1; celda < row.Cells.Count; celda++)
                    {
                        if (!resultado) break;
                        if (columnaTotalesFila == celda)
                            continue;
                        string fechaColumna = grvFlujoEfectivo.Columns[celda].HeaderText;
                        fflujo = Convert.ToDateTime(fechaColumna);
                        año = fflujo.Year;
                        mes = Convert.ToSByte(fflujo.Month);
                        fp = listFlujoRealProyectado.Find(x => x.Corporativo == corporativo
                                                               && x.Año == año
                                                               && x.Mes == mes
                                                               && x.StatusConcepto == statusconcepto
                                                               && x.FFlujo == fflujo
                            );
                        //Asignar Importes segun TipoFlujo
                        if (fp.TipoFlujo.Equals("PROYECTADO"))
                        {
                            try
                            {
                                fp.ImporteProyectado = ObtenerValorDecimal((row.Cells[celda].Controls[0] as TextBox).Text);
                            }
                            catch (Exception ex)
                            {
                                App.ImplementadorMensajes.MostrarMensaje(
                                    "Ha ocurrido un erro al intentar el leer el valor introducido.\nError:" + ex.Message);

                            }

                        }
                        //Actualizar el Flujo
                        resultado = fp.ActualizarFlujoEfectivo(usuario.IdUsuario.Trim());

                    }
                }
            }
            else
                App.ImplementadorMensajes.MostrarMensaje("Conceptos por" +
                                                            (tipoTransferencia == Consultas.TipoTransferencia.Entrada
                                                            ? "INGRESOS"
                                                            : "EGRESOS")
                                                          + " sin ningún registro.");
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Ha ocurrido un error al intentar guardar el FLUJO DE EFECTIVO.\nEl proceso se detendra, recargue la vista.\n" + ex.Message + "\nSource:" + ex.StackTrace);
        }
        return resultado;

    }
    //Exportar a EXCEL
    public void ExportToExcel(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            string filename = "DownloadFlujoExcel.xls";
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();
            dgGrid.CssClass = "grvResultadoConsultaCss";
            dgGrid.DataSource = dt;
            dgGrid.DataBind();

            //Get the HTML for the control.
            dgGrid.RenderControl(hw);
            //Write the HTML back to the browser.
            //Response.ContentType = application/vnd.ms-excel;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            this.EnableViewState = false;
            Response.Write(tw.ToString());
            Response.End();
        }
    }
    //private void ExporttoExcel(DataTable table)
    //{
    //    HttpContext.Current.Response.Clear();
    //    HttpContext.Current.Response.ClearContent();
    //    HttpContext.Current.Response.ClearHeaders();
    //    HttpContext.Current.Response.Buffer = true;
    //    HttpContext.Current.Response.ContentType = "application/ms-excel";
    //    HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
    //    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=ReporteFlujoEfectivo.xls");

    //    HttpContext.Current.Response.Charset = "utf-8";
    //    HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");

    //    HttpContext.Current.Response.Write("<B style='font-size:12.0pt; font-family: Calibri;'>");
    //    HttpContext.Current.Response.Write(ddlEmpresa.SelectedItem.Text);
    //    HttpContext.Current.Response.Write("<BR>");
    //    HttpContext.Current.Response.Write(String.Format("FLUJO DE EFECTIVO {0} / {1}", obtenerNombreMesNumero((Convert.ToDateTime(hdfFConsulta.Value)).Month), (Convert.ToDateTime(hdfFConsulta.Value)).Year));
    //    HttpContext.Current.Response.Write("</B>");

    //    //sets font

    //    HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family: Calibri;'>");
    //    HttpContext.Current.Response.Write("<BR><BR><BR>");
    //    //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
    //    HttpContext.Current.Response.Write("<TABLE border='1' " +
    //      "style='font-size:10.0pt; font-family: Calibri; background:white;'> <TR> ");
    //    //am getting my grid's column headers
    //    int columnscount = grvFlujoEfectivoEntrada.Columns.Count;

    //    for (int j = 0; j < columnscount; j++)
    //    {      //write in new column
    //        HttpContext.Current.Response.Write("<TD style='background: #ebecec;font-size: 10.0pt;font-style: normal;padding: 2px;cursor: pointer;text-align: center;'> ");
    //        //Get column headers  and make it as bold in excel columns
    //        HttpContext.Current.Response.Write("<B> ");
    //        HttpContext.Current.Response.Write(grvFlujoEfectivoEntrada.Columns[j].HeaderText);
    //        HttpContext.Current.Response.Write(" </B>");
    //        HttpContext.Current.Response.Write("</TD>");
    //    }
    //    HttpContext.Current.Response.Write("</TR>");
    //    foreach (DataRow row in table.Rows)
    //    {//write in new row
    //        HttpContext.Current.Response.Write("<TR>");
    //        for (int i = 0; i < table.Columns.Count; i++)
    //        {
    //            //HttpContext.Current.Response.Write(String.Format("<Td {0}>", obtenerStyleTable(table.Columns[i].ColumnName, row[i].ToString(), row[0].ToString())));
    //            HttpContext.Current.Response.Write("<TD> ");
    //            HttpContext.Current.Response.Write(i > 0 ? Convert.ToDecimal( row[i].ToString()).ToString("C2") : row[i].ToString());
    //            HttpContext.Current.Response.Write(" </TD>");
    //        }

    //        HttpContext.Current.Response.Write("</TR>");
    //    }
    //    HttpContext.Current.Response.Write("</TABLE>");
    //    HttpContext.Current.Response.Write("</font>");
    //    HttpContext.Current.Response.Flush();
    //    HttpContext.Current.Response.End();
    //}
    private void ExporttoExcel(DataTable table)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");

        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR>");
        //TITULO DEL REPORTE
        HttpContext.Current.Response.Write("<B style='font-size:12.0pt; font-family: Calibri;'>");
        HttpContext.Current.Response.Write(ddlEmpresa.SelectedItem.Text);
        HttpContext.Current.Response.Write("<BR>");
        HttpContext.Current.Response.Write(String.Format("FLUJO DE EFECTIVO {0} / {1}", obtenerNombreMesNumero((Convert.ToDateTime(hdfFConsulta.Value)).Month), (Convert.ToDateTime(hdfFConsulta.Value)).Year));
        HttpContext.Current.Response.Write("</B>");
        //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
        //am getting my grid's column headers
        int columnscount = grvFlujoEfectivoEntrada.Columns.Count;

        for (int j = 0; j < columnscount; j++)
        {      //write in new column
            HttpContext.Current.Response.Write("<Td style='background: #ebecec;font-size: 10.0pt;font-style: normal;padding: 2px;cursor: pointer;text-align: center;'>");
            //HttpContext.Current.Response.Write("<TD style='background: #ebecec;font-size: 10.0pt;font-style: normal;padding: 2px;cursor: pointer;text-align: center;'> ");
            //Get column headers  and make it as bold in excel columns
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(grvFlujoEfectivoEntrada.Columns[j].HeaderText.ToString());
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");
        }
        HttpContext.Current.Response.Write("</TR>");
        foreach (DataRow row in table.Rows)
        {//write in new row
            HttpContext.Current.Response.Write("<TR>");
            for (int i = 0; i < table.Columns.Count; i++)
            {
                //HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write(String.Format("<Td {0}>", obtenerStyleTable(table.Columns[i].ColumnName, row[i].ToString(), row[0].ToString())));
                HttpContext.Current.Response.Write(row[i].ToString());
                //try
                //{
                //    string valor = (Convert.ToDecimal(row[i].ToString()).ToString("C2"));
                //    HttpContext.Current.Response.Write(valor);
                //}
                //catch (Exception ex)
                //{
                //    HttpContext.Current.Response.Write(row[i].ToString());
                //}
                HttpContext.Current.Response.Write(row[i].ToString());
                //HttpContext.Current.Response.Write((i > 1 ? (Convert.ToDecimal(row[i].ToString()).ToString("C2")) : row[i].ToString()));
                HttpContext.Current.Response.Write("</Td>");
            }

            HttpContext.Current.Response.Write("</TR>");
        }
        HttpContext.Current.Response.Write("</Table>");
        HttpContext.Current.Response.Write("</font>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }
    public string obtenerStyleTable(string columNombre, string dato, string datoInicio)
    {
        string resultado = "style=' ";
        try
        {


            if (columNombre.Equals("Concepto"))
            {
                resultado = resultado +
                            (dato.Equals("SUMA INGRESOS") || dato.Equals("SUMA EGRESOS")
                             || dato.Contains("SALDO DEL PERIODO") ||
                             dato.Contains("SALDO INICIAL BANCOS")
                             || dato.Contains("SALDO DISPONIBLE")
                    ? "background: #4e4e4e;color:#FFFFFF"
                    : "background: #d1d0d0");


            }
            else if (columNombre.Equals("Total"))
            {
                resultado = resultado + "background: #4e4e4e;color:#FFFFFF";
            }
            else
            {

                switch (datoInicio)
                {
                    case "SUMA INGRESOS":
                        resultado = resultado + "background: #4e4e4e;color:#FFFFFF";
                        break;
                    case "SUMA EGRESOS":
                        resultado = resultado + "background: #4e4e4e;color:#FFFFFF";
                        break;
                    case "1.SALDO DEL PERIODO":
                        resultado = resultado + "background: #fbefa9";
                        break;
                    case "2.SALDO INICIAL BANCOS":
                        resultado = resultado + "background: #00B12C;color:#FFFFFF";
                        break;
                    case "3.SALDO DISPONIBLE":
                        resultado = resultado + "background: #00B12C;color:#FFFFFF";
                        break;
                    default:
                        resultado = resultado + "background: " +
                            (Convert.ToDateTime(columNombre) < DateTime.Now ? "#b0e8a1" : "#a1d8e8");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje("Error al generar el estilo por columna.\nError: " + ex.Message);
        }
        return resultado + "';";
    }

    protected void imgExportar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //ExportToExcel((DataTable)Session["TAB_FLUJO_E"]); 
            DataTable dtEntrada = (DataTable)Session["TAB_FLUJO_E"];
            DataTable dtSalida = (DataTable)Session["TAB_FLUJO_S"];
            DataTable dtSaldos = (DataTable)Session["TAB_FLUJO_SL"];
            DataTable dtExportar = dtEntrada.Copy();
            dtExportar.Merge(dtSalida);
            dtExportar.Merge(dtSaldos);
            ExporttoExcel(dtExportar);
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }

    }
    private string obtenerNombreMesNumero(int numeroMes)
    {
        try
        {
            DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
            return formatoFecha.GetMonthName(numeroMes).ToUpper();
        }
        catch
        {
            return "Desconocido";
        }
    }
    protected void imgGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //Leer datos de Consulta
            corporativo = Convert.ToInt16(CorporativoConsulta.Value);
            sucursal = Convert.ToInt16(SucursalConsulta.Value);
            finicio = Convert.ToDateTime(FInicioConsulta.Value);
            ffin = Convert.ToDateTime(FFinConsulta.Value);

            bool resultado = false;
            resultado = GuardarFlujoRealProyectado(Consultas.TipoTransferencia.Entrada, grvFlujoEfectivoEntrada);
            if (resultado)
            {
                ConsultaFlujoEfectivo(corporativo,
                          sucursal,
                          Consultas.TipoTransferencia.Entrada,
                          finicio,
                          ffin
                           );
                GenerarTablaFlujoRealProyectado(Consultas.TipoTransferencia.Entrada);
                CalcularTotalesDiasDT(Consultas.TipoTransferencia.Entrada);
                LlenaGridViewFlujoEfectivo(Consultas.TipoTransferencia.Entrada);
                //CalcularTotalesColumnas(Consultas.TipoTransferencia.Entrada);
                CalcularTotalesFilas(Consultas.TipoTransferencia.Entrada);
                App.ImplementadorMensajes.MostrarMensaje("Movimientos de ENTRADA guardados existosamente.");
            }

            if (resultado)
                resultado = GuardarFlujoRealProyectado(Consultas.TipoTransferencia.Salida, grvFlujoEfectivoSalida);
            if (resultado)
            {

                ConsultaFlujoEfectivo(corporativo,
                         sucursal,
                         Consultas.TipoTransferencia.Salida,
                         finicio,
                         ffin
                          );
                GenerarTablaFlujoRealProyectado(Consultas.TipoTransferencia.Salida);
                CalcularTotalesDiasDT(Consultas.TipoTransferencia.Salida);
                LlenaGridViewFlujoEfectivo(Consultas.TipoTransferencia.Salida);
                //CalcularTotalesColumnas(Consultas.TipoTransferencia.Salida);
                CalcularTotalesFilas(Consultas.TipoTransferencia.Salida);
                App.ImplementadorMensajes.MostrarMensaje("Movimientos de SALIDA guardados existosamente.");
            }


        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.Message);
        }

    }

    public decimal ObtenerValorDecimal(string texto)
    {
        return texto.Contains("$")
            ? decimal.Parse(texto, NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US"))
            : Convert.ToDecimal(texto);
    }

}