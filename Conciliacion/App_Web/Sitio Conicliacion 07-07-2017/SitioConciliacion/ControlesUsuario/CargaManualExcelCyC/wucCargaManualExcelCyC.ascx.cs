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

    /*      Componentes interfaz gráfica        */
    public FileUpload fuSeleccionar
    {
        get { return fupSeleccionar; }
    }
    public Button bnSubir
    {
        get { return btnSubirArchivo; }
    }
    #endregion

    string NombreArchivo;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (this.Session["FileUpload1"] == null && fupSeleccionar.HasFile)
        //{
        //    this.Session["FileUpload1"] = fupSeleccionar;
        //}
        //else if (this.Session["FileUpload1"] != null && fupSeleccionar.HasFile)
        //{
        //    fupSeleccionar = (FileUpload)this.Session["FileUpload1"];
        //}
        //else if (fupSeleccionar.HasFile)
        //{
        //    this.Session["FileUpload1"] = fupSeleccionar;
        //}

        if (Page.IsPostBack)
        {
            NombreArchivo = fupSeleccionar.HasFile ? fupSeleccionar.PostedFile.FileName : "";
        }
        else
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
        #region Código antigüo
        //OleDbConnection oledbConn = new OleDbConnection();
        //OleDbCommand cmd;
        //OleDbDataAdapter oleda;
        //DataSet ds;
        #endregion
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
                        lblArchivo.Text = "Archivo: " + sArchivo;

                        iValidador.CuentaBancaria = 7703;
                        //iValidador.DocumentoReferencia = 2;
                        iValidador.RutaArchivo = sRutaArchivo;
                        iValidador.NombreArchivo = Path.GetFileName(sArchivo);
                        iValidador.TipoMIME = (sExt == ".xls" ? MIME[0] : MIME[1]);

                        if (iValidador.ArchivoValido(sRutaArchivo, Path.GetFileName(sArchivo)))
                        {
                            dtTabla = iValidador.CargaArchivo(sRutaArchivo, Path.GetFileName(sArchivo));
                            DetalleProcesoDeCarga = iValidador.ValidacionCompleta();
                        }

                        #region Código antigüo
                        //lblExiste.Text = "Existe";
                        //if (ext == ".xls")
                        //{
                        //    oledbConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;" +
                        //        "Data Source = " + sArchivo +
                        //        ";Extended Properties =\"Excel 8.0;HDR=Yes;IMEX=2\"");
                        //}
                        //else if (ext == ".xlsx")
                        //{
                        //    oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;" +
                        //        "Data Source=" + sArchivo +
                        //        ";Extended Properties = 'Excel 12.0;HDR=YES;IMEX=1;READONLY=TRUE'; ");
                        //}
                        ///*      Cargar datos en el Grid     */
                        //oledbConn.Open();
                        //cmd = new OleDbCommand("SELECT * FROM [Hoja1$]", oledbConn);
                        //oleda = new OleDbDataAdapter();
                        //ds = new DataSet();

                        //oleda.SelectCommand = cmd;
                        //oleda.Fill(ds, "Registros");
                        //grvDetalleConciliacionManual.DataSource = ds.Tables[0].DefaultView;
                        #endregion

                        //if (DetalleProcesoDeCarga.Where(x => x.CodigoError != 0).Count() == 0)
                        //{
                        //    grvDetalleConciliacionManual.DataSource = dtTabla.DefaultView;
                        //    grvDetalleConciliacionManual.DataBind();
                        //}

                        grvDetalleConciliacionManual.DataSource = dtTabla.DefaultView;
                        grvDetalleConciliacionManual.DataBind();

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
                    }
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
}
