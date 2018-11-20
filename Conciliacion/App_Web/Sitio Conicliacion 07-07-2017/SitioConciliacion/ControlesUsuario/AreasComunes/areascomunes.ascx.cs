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
    private decimal _monto;


    public int ClientePadre
    {
        get
        {
            return Convert.ToInt32(Session["acClientePadre"]);
        }
    }

    public decimal Monto
    {
        get
        {
            return Convert.ToDecimal(Session["acMonto"]);
        }       
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       // cargaDatos();

        
    }
       
    public void inicializa(int clientePadre, decimal monto)
    {
        Session["acClientePadre"] = clientePadre;
        Session["acMonto"] = monto;

        txtTotalAbono.Text = Monto.ToString("C");
        txtTotalSeleccionado.Text = 0.ToString("C");
        txtTotalResto.Text = 0.ToString("C");

    }


    public void consulta(short opcion, string fInicio, string fFinal, string pReferencia)
    {
        Conexion conexion = new Conexion();
        try
        {
            
            conexion.AbrirConexion(false);
            PagoAreasComunes objAC = Conciliacion.RunTime.App.PagoAreasComunes.CrearObjeto();
            objAC.ClientePadre = ClientePadre;

            switch (opcion)
            {
                case 1: objAC.FSuministroInicio = Convert.ToDateTime(fInicio);
                        objAC.FSuministroFin = Convert.ToDateTime(fFinal);
                        break;
                case 2:
                        objAC.PedidoReferencia = pReferencia;
                    break;
            }
            
            objAC.consulta(conexion);
           

            lblClientePadre.Text = "Cliente padre " + ClientePadre + " " + objAC.NombreClientePadre;

            if (objAC.TienePagos)
            {
                grvPedidosEmparentados.DataSource = objAC.Pagos;
                grvPedidosEmparentados.DataBind();
            }
            else
            {
                grvPedidosEmparentados.DataSource = null;
                grvPedidosEmparentados.DataBind();
            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Error:"+ex.Message+");", true);

        }
        finally
        {
            conexion.CerrarConexion();
            
        }

       
    }
    

    public void cargaDatos()
    {
        consulta(0, "", "", "");        
    }

    protected void rbSelector_CheckedChanged(object sender, System.EventArgs e)
    {
        decimal montoSeleccionado;
        decimal resto;
        foreach (GridViewRow oldrow in gwPagos.Rows)
        {
            ((RadioButton)oldrow.FindControl("RadioButton1")).Checked = false;
        }

        RadioButton rb = (RadioButton)sender;
        GridViewRow row = (GridViewRow)rb.NamingContainer;

        decimal.TryParse(row.Cells[2].Text, System.Globalization.NumberStyles.Currency, null, out montoSeleccionado);
        resto = montoSeleccionado - Monto;

        if (resto <0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('El monto del pedido debe ser mayor o igual a la suma de los pagos');", true);
        }
        else
        {
            ((RadioButton)row.FindControl("RadioButton1")).Checked = true;
            txtTotalSeleccionado.Text = montoSeleccionado.ToString("C");
            txtTotalResto.Text = resto.ToString("C");
            txtTotalAbono.Text = Monto.ToString("C");
        }
        
      
       
    }


    protected void imgBuscarFechas_Click(object sender, ImageClickEventArgs e)
    {

        consulta(1, txtFSuministroInicio.Text, txtFSuministroFin.Text, "");

    }

    protected void imgBuscarReferencia_Click(object sender, ImageClickEventArgs e)
    {
        consulta(2, "", "", txtPedidoReferencia.Text);

    }
}