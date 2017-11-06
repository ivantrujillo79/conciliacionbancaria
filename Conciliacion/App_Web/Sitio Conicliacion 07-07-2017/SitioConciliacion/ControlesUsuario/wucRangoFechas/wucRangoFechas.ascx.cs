using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ControlesUsuario_wucRangoFechas_wucRangoFechas : System.Web.UI.UserControl
{
    public string TextoDespliega;
    public string CampoFiltrar;

    GridView _GridViewFiltrar;
    public GridView GridViewFiltrar
    {
        get { return _GridViewFiltrar; }
        set { _GridViewFiltrar = GridViewFiltrar; }
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
        lbTextoDespliega.Text = TextoDespliega;
    }


}