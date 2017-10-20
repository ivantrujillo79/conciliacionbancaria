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

public partial class Conciliacion_WebUserControl : System.Web.UI.UserControl
{
    #region Propiedades
    private int totalRegistrosCargados;
    public int TotalRegistrosCargados {
        get { return totalRegistrosCargados; }
    }
    public int RegistrosCargados { get; set; }
    public List<ValidacionArchivosConciliacion.DetalleValidacion> DetalleProcesoDeCarga { get; set; }
    #endregion

    private const string ARCHIVO   = "Archivo: ";
    private const string REGISTROS = "Total de registros a cargar: ";

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
        string[] extensiones = {".xls", ".xlsx"};
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
                        iValidador.CuentaBancaria = 7703;
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

    private void LimpiarCampos()
    {
        grvDetalleConciliacionManual.DataSource = null;
        grvDetalleConciliacionManual.DataBind();
        lblArchivo.Text = ARCHIVO;
        lblRegistros.Text = REGISTROS;
    }
}
