using Conciliacion.RunTime.DatosSQL;
using Conciliacion.RunTime.ReglasDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ControlesUsuario_AreasComunes_areascomunes : System.Web.UI.UserControl
{

    private int _clientePadre;

    public int ClientePadre
    {
        get
        {
            return _clientePadre;
        }

        set
        {
            _clientePadre = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       // cargaDatos();
    }

    public void cargaDatos()
    {
        

        Conexion conexion = new Conexion();
        conexion.AbrirConexion(false);
        PagoAreasComunes objAC = Conciliacion.RunTime.App.PagoAreasComunes.CrearObjeto();
        objAC.ClientePadre = _clientePadre;
        objAC.consulta(conexion);
        conexion.CerrarConexion();


        lblClientePadre.Text = "Cliente padre " + _clientePadre + " " + objAC.NombreClientePadre;

        if (objAC.TienePagos)
        {
            gwPagos.DataSource = objAC.Pagos;
            gwPagos.DataBind();
        }
        else
        {
            gwPagos.DataSource = null;
            gwPagos.DataBind();
        }
    }

    protected void rbSelector_CheckedChanged(object sender, System.EventArgs e)
    {
        foreach (GridViewRow oldrow in gwPagos.Rows)
        {
            ((RadioButton)oldrow.FindControl("RadioButton1")).Checked = false;
        }


        //Set the new selected row
        RadioButton rb = (RadioButton)sender;
        GridViewRow row = (GridViewRow)rb.NamingContainer;
        ((RadioButton)row.FindControl("RadioButton1")).Checked = true;
    }

}