using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.IO;
using System.Data;
using System.Text;

using Conciliacion.RunTime;
using Conciliacion.RunTime.ReglasDeNegocio;

public partial class wucCargaManualExcelCyC : System.Web.UI.UserControl
{
    #region Propiedades
    private int totalRegistrosCargados;
    public int TotalRegistrosCargados
    {
        get { return totalRegistrosCargados; }
    }

    public int RegistrosCargados { get; set; }

    public List<ValidacionArchivosConciliacion.DetalleValidacion> DetalleProcesoDeCarga { get; set; }

    private int anio;
    public int Anio
    {
        get
        {
            if (ViewState["anio"] == null)
                return int.MinValue;
            else
                return (int)ViewState["anio"];
        }
        set { ViewState["anio"] = value; }
    }

    private string clienteReferencia;
    public string ClienteReferencia
    {
        get { return clienteReferencia; }
        set
        {
            clienteReferencia = value;
            lblReferencia.Text = clienteReferencia;
        }
    }

    private int corporativo;
    public int Corporativo
    {
        get
        {
            if (ViewState["corporativo"] == null)
                return int.MinValue;
            else
                return (int)ViewState["corporativo"];
        }
        set { ViewState["corporativo"] = value; }
    }

    private int cuentaBancaria;
    public int CuentaBancaria
    {
        get
        {
            if (ViewState["cuentaBancaria"] == null)
                return int.MinValue;
            else
                return (int)ViewState["cuentaBancaria"];
        }
        set { ViewState["cuentaBancaria"] = value; }
    }

    private int folio;
    public int Folio
    {
        get
        {
            if (ViewState["folio"] == null)
                return int.MinValue;
            else
                return (int)ViewState["folio"];
        }
        set { ViewState["folio"] = value; }
    }

    private sbyte mes;
    public sbyte Mes
    {
        get
        {
            if (ViewState["mes"] == null)
                return mes;
            else
                return (sbyte)ViewState["mes"];
        }
        set { ViewState["mes"] = value; }
    }

    private decimal montoPago;
    public decimal MontoPago
    {
        get { return montoPago; }
        set
        {
            montoPago = value;
            lblMontoPago.Text = MONTO + String.Format("{0:C}", montoPago);
        }
    }

    private Int16 sucursal;
    public Int16 Sucursal
    {
        get
        {
            if (ViewState["sucursal"] == null)
                return Int16.MinValue;
            else
                return (Int16)ViewState["sucursal"];
        }
        set { ViewState["sucursal"] = value; }
    }

    private List<ReferenciaNoConciliada> referenciasPorConciliarExcel;
    public List<ReferenciaNoConciliada> ReferenciasPorConciliarExcel
    {
        //get { return referenciasPorConciliarExcel; }
        get
        {
            if (ViewState["referenciasPorConciliarExcel"] == null)
                return null;
            else
                return (List<ReferenciaNoConciliada>)ViewState["referenciasPorConciliarExcel"];
        }
    }

    private bool recuperoNoConciliados;
    public bool RecuperoNoConciliados
    {
        get
        {
            if (ViewState["recuperoNoConciliados"] == null)
                return false;
            else
                return (bool)ViewState["recuperoNoConciliados"];
        }
    }
    #endregion

    private const string ARCHIVO = "Archivo: ";
    private const string REGISTROS = "Total de registros a cargar: ";
    private const string MONTO = "Monto pago: ";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            grvDetalleConciliacionManual.DataSource = null;
            grvDetalleConciliacionManual.DataBind();
        }
    }

    protected void btnCargaArchivoCancelar_Click(object sender, EventArgs e)
    {
        //mpeCargaArchivoConciliacionManual.Hide();
    }

    protected void btnSubirArchivo_Click(object sender, EventArgs e)
    {
        DataTable dtTabla = new DataTable();
        ValidacionArchivosConciliacion.ValidadorCyC Validador = new ValidacionArchivosConciliacion.ValidadorCyC();
        ValidacionArchivosConciliacion.IValidadorExcel iValidador = Validador;
        string sArchivo;
        string sRutaArchivo;
        string sExt;
        StringBuilder sbMensaje;
        string[] extensiones = { ".xls", ".xlsx" };
        string[] MIME = {"application/vnd.ms-excel" ,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" };

        LimpiarCampos();
        sExt = Path.GetExtension(fupSeleccionar.FileName).ToLower();
        try
        {
            if (fupSeleccionar.HasFile)
            {
                if (extensiones.Contains(sExt))
                {
                    /*      Subir archivo       */
                    sRutaArchivo = System.IO.Path.GetFullPath(Server.MapPath("~/ControlesUsuario/CargaManualExcelCyC/Excel/"));
                    sArchivo = System.IO.Path.GetFullPath(Server.MapPath("~/ControlesUsuario/CargaManualExcelCyC/Excel/"))
                        + fupSeleccionar.FileName;
                    fupSeleccionar.SaveAs(sArchivo);

                    if (File.Exists(sArchivo))
                    {
                        iValidador.CuentaBancaria = this.CuentaBancaria;
                        //iValidador.DocumentoReferencia = 2;
                        iValidador.RutaArchivo = sRutaArchivo;
                        iValidador.NombreArchivo = Path.GetFileName(sArchivo);
                        iValidador.TipoMIME = (sExt == ".xls" ? MIME[0] : MIME[1]);

                        if (iValidador.ArchivoValido(sRutaArchivo, Path.GetFileName(sArchivo)))
                        {
                            dtTabla = iValidador.CargaArchivo(sRutaArchivo, Path.GetFileName(sArchivo));
                            DetalleProcesoDeCarga = iValidador.ValidacionCompleta();

                            if (DetalleProcesoDeCarga.Where(x => x.CodigoError != 0).Count() == 0)
                            {
                                grvDetalleConciliacionManual.DataSource = dtTabla.DefaultView;
                                grvDetalleConciliacionManual.DataBind();
                                totalRegistrosCargados = grvDetalleConciliacionManual.Rows.Count;

                                lblArchivo.Text = ARCHIVO + sArchivo;
                                lblRegistros.Text = REGISTROS + totalRegistrosCargados.ToString();
                            }

                            //grvDetalleConciliacionManual.DataSource = dtTabla.DefaultView;
                            //grvDetalleConciliacionManual.DataBind();

                            sbMensaje = new StringBuilder();
                            foreach (ValidacionArchivosConciliacion.DetalleValidacion detalle in DetalleProcesoDeCarga)
                            {
                                if (!detalle.VerificacionValida)
                                {
                                    if (detalle.CodigoError > 0)
                                    {
                                        sbMensaje.Append(detalle.Mensaje + "\n");
                                    }
                                    else
                                    {
                                        sbMensaje.Append("ERROR: el código de error y la validación no concuerdan: " + detalle.Mensaje + "\n");
                                    }
                                }
                            }
                            if (sbMensaje.Length > 0)
                            {
                                App.ImplementadorMensajes.MostrarMensaje(sbMensaje.ToString());
                            }
                        } // if ArchivoValido
                    } // if File.Exists
                }
                else
                {
                    App.ImplementadorMensajes.MostrarMensaje("El archivo a cargar debe ser de formato Excel, con extensión de archivo XLS o XLSX");
                }
            }// fupSeleccionar.HasFile
        }
        catch (Exception ex)
        {
            App.ImplementadorMensajes.MostrarMensaje(ex.ToString());
        }
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        RecuperaReferenciasNoConciliadas();
    }

    private void LimpiarCampos()
    {
        grvDetalleConciliacionManual.DataSource = null;
        grvDetalleConciliacionManual.DataBind();
        lblArchivo.Text = ARCHIVO;
        lblRegistros.Text = REGISTROS;
    }

    private bool RecuperaReferenciasNoConciliadas()
    {
        string sDocumento;
        decimal dMonto;
        bool recupero = false;
        ReferenciaNoConciliada RefNoConciliada;
        referenciasPorConciliarExcel = new List<ReferenciaNoConciliada>();  /*      Inicializar campo de la propiedad     */

        try
        {
            if (grvDetalleConciliacionManual.Rows.Count > 0)
            {
                foreach (GridViewRow row in grvDetalleConciliacionManual.Rows)
                {
                    sDocumento = row.Cells[1].Text;
                    dMonto = Convert.ToDecimal(row.Cells[3].Text);

                    //if (App.Consultas.ValidaPedidoEspecifico(Corporativo, Sucursal, sDocumento))
                    //{
                        RefNoConciliada = App.ReferenciaNoConciliada.CrearObjeto();
                        RefNoConciliada.Referencia = sDocumento;
                        RefNoConciliada.Monto = dMonto;
                        //RefNoConciliada.Folio = 1;
                        //RefNoConciliada.Secuencia = 1;
                        referenciasPorConciliarExcel.Add(RefNoConciliada);

                        recupero = true;
                    //}
                }
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
        //ViewState["referenciasPorConciliarExcel"] = referenciasPorConciliarExcel;

        HttpContext.Current.Session["referenciasPorConciliarExcel"] = referenciasPorConciliarExcel;

        recuperoNoConciliados = recupero;
        ViewState["recuperoNoConciliados"] = recuperoNoConciliados;
        return recupero;
    }// FIN Recupera Referencias No Conciliadas
}
