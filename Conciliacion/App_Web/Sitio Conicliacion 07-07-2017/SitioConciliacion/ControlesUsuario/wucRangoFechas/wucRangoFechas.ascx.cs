using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ControlesUsuario_wucRangoFechas_wucRangoFechas : System.Web.UI.UserControl
{
    public string TextoDespliega;
    public string CampoFiltrar;
    public string VarSesionGridNombre;

    private GridView gridviewFiltrar;
    public GridView GridViewFiltrar
    {
        get
        {
            return gridviewFiltrar;
        }
        set
        {
            gridviewFiltrar = value;
        }
    }

    public DateTime FInicial {
        get
        {
            if (txtFechaInicial.Text.Trim() != "")
                return DateTime.Parse(txtFechaInicial.Text);
            else
                return DateTime.Now;
        }
        set
        {
            if (FInicial != null)
                txtFechaInicial.Text = Convert.ToString(FInicial);
            else
                FInicial = DateTime.Now;
        }
    }
    public DateTime FFinal {
        get
        {
            if (txtFechaFinal.Text.Trim() != "")
                return DateTime.Parse(txtFechaFinal.Text);
            else
                return DateTime.Now;
        }
        set
        {
            if (FFinal != null)
                txtFechaFinal.Text = Convert.ToString(FFinal);
            else
                FFinal = DateTime.Now;                
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "Calendarios", "RF_DatePickers();", true);

        lbTextoDespliega.Text = TextoDespliega;
    }

    public void AplicarFiltro()
    {
        DataTable tablaReferenciasP = (DataTable)HttpContext.Current.Session[this.VarSesionGridNombre];

        DataView dv = null;
        DataTable dtDatos = tablaReferenciasP;
        dv = new DataView(dtDatos);
        int a; int m; int d;

        if (txtFechaInicial.Text.Trim() != string.Empty && txtFechaFinal.Text.Trim() != string.Empty)
        {
            a = Convert.ToDateTime(txtFechaInicial.Text).Year;
            m = Convert.ToDateTime(txtFechaInicial.Text).Month;
            d = Convert.ToDateTime(txtFechaInicial.Text).Day;
            string Desde = String.Format(CultureInfo.InvariantCulture.DateTimeFormat, CampoFiltrar + " >= #{0}#", new DateTime(a, m, d, 0, 0, 1));
            a = Convert.ToDateTime(txtFechaFinal.Text).Year;
            m = Convert.ToDateTime(txtFechaFinal.Text).Month;
            d = Convert.ToDateTime(txtFechaFinal.Text).Day;
            string Hasta = String.Format(CultureInfo.InvariantCulture.DateTimeFormat, CampoFiltrar + " <= #{0}#", new DateTime(a, m, d, 23, 59, 59));
            dv.RowFilter = Desde + " and " + Hasta;
        }
        else
            dv.RowFilter = "";

        GridViewFiltrar.DataSource = dv;
        GridViewFiltrar.DataBind();

    }

    protected void btAplicarFiltro_Click(object sender, EventArgs e)
    {
        AplicarFiltro();
    }
}   