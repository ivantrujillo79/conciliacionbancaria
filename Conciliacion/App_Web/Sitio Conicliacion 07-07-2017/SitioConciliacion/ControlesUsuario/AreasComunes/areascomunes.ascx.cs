using Conciliacion.RunTime.DatosSQL;
using Conciliacion.RunTime.ReglasDeNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ControlesUsuario_AreasComunes_areascomunes : System.Web.UI.UserControl
{

    private int _clientePadre;
    private decimal _monto;

    private class pedidoPagar : ICloneable
    {
        private int _celulapedido;
        private int _añopedido;
        private int _pedido;
        private string _pedidoReferencia;
        private decimal _monto;
        private int pedidoPadre;

        public pedidoPagar()
        {
            this.pedidoPadre = 0;
        }

        public int Celulapedido
        {
            get
            {
                return _celulapedido;
            }

            set
            {
                _celulapedido = value;
            }
        }

        public int Añopedido
        {
            get
            {
                return _añopedido;
            }

            set
            {
                _añopedido = value;
            }
        }

        public int Pedido
        {
            get
            {
                return _pedido;
            }

            set
            {
                _pedido = value;
            }
        }

        public string PedidoReferencia
        {
            get
            {
                return _pedidoReferencia;
            }

            set
            {
                _pedidoReferencia = value;
            }
        }

        public decimal Monto
        {
            get
            {
                return _monto;
            }

            set
            {
                _monto = value;
            }
        }

        public int PedidoPadre
        {
            get
            {
                return pedidoPadre;
            }

            set
            {
                pedidoPadre = value;
            }
        }

        public pedidoPagar Clonar()
        {
            return (pedidoPagar)this.MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            throw new NotImplementedException();
        }
    }

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

    public int CorporativoConciliacion
    {
        get { return Convert.ToInt32(Session["CorporativoConciliacion"]); }
        set { Session["CorporativoConciliacion"] = value; }
    }

    public int SucursalConciliacion
    {
        get { return Convert.ToInt32(Session["SucursalConciliacion"]); }
        set { Session["SucursalConciliacion"] = value; }
    }

    public int AnioConciliacion
    {
        get { return Convert.ToInt32(Session["AnioConciliacion"]); }
        set { Session["AnioConciliacion"] = value; }
    }

    public short MesConciliacion
    {
        get { return Convert.ToInt16(Session["MesConciliacion"]); }
        set { Session["MesConciliacion"] = value; }
    }

    public int FolioConciliacion
    {
        get { return Convert.ToInt32(Session["FolioConciliacion"]); }
        set { Session["FolioConciliacion"] = value; }
    }

    public DataTable TablaPagos
    {
        get { return (DataTable)(Session["TablaPagos"]); }
        set { Session["TablaPagos"] = value; }
    }

    public DataTable TablaPagosPadre
    {
        get { return (DataTable)(Session["TablaPagosPadre"]); }
        set { Session["TablaPagosPadre"] = value; }
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

            TablaPagosPadre = objAC.Pagos;
            

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

        RadioButton rb = (RadioButton)sender;
        GridViewRow row = (GridViewRow)rb.NamingContainer;

        foreach (GridViewRow oldrow in grvPedidosEmparentados.Rows)
        {
            if (row != oldrow)
            {
                if (((TextBox)oldrow.FindControl("TxtMontoPagar")).Text == "")
                {
                    ((TextBox)oldrow.FindControl("TxtMontoPagar")).Enabled = false;
                    ((TextBox)oldrow.FindControl("TxtMontoPagar")).Text = "";
                    ((RadioButton)oldrow.FindControl("RadioButton1")).Checked = false;
                }
            }            
        }

        //rb.Checked = !rb.Checked;
        ((TextBox)row.FindControl("TxtMontoPagar")).Enabled = rb.Checked;

        if (!rb.Checked)
        {
            ((TextBox)row.FindControl("TxtMontoPagar")).Text = "";
        }
        else
        {
            ((TextBox)row.FindControl("TxtMontoPagar")).Focus();
        }


        //decimal.TryParse(row.Cells[2].Text, System.Globalization.NumberStyles.Currency, null, out montoSeleccionado);

       

        //resto = montoSeleccionado - Monto;


        

        ////if (resto <0)
        ////{
        ////    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('El monto del pedido debe ser mayor o igual a la suma de los pagos');", true);
        ////}
        ////else
        ////{
        //    ((RadioButton)row.FindControl("RadioButton1")).Checked = true;
        //    txtTotalSeleccionado.Text = montoSeleccionado.ToString("C");
        //    txtTotalResto.Text = resto.ToString("C");
        //    txtTotalAbono.Text = Monto.ToString("C");
        ////}
        
      
       
    }


    protected void imgBuscarFechas_Click(object sender, ImageClickEventArgs e)
    {

        consulta(1, txtFSuministroInicio.Text, txtFSuministroFin.Text, "");

    }

    protected void imgBuscarReferencia_Click(object sender, ImageClickEventArgs e)
    {
        consulta(2, "", "", txtPedidoReferencia.Text);

    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        Conexion conexion = new Conexion();
        //GridViewRow filaGrid=null;
        //int pedidoTemp= 0;
        //int añopedido=0;
        //int pedido=0;
        //string PedidoReferencia;
        DataRow filaPadre;

        List<pedidoPagar> listaPedidos = new List<pedidoPagar>();
        List<pedidoPagar> listaPedidosFinal = new List<pedidoPagar>();

        TextBox txtMontoPagar;
        decimal montoPagar;
        decimal montoTotal=0;

        
       
        

        foreach (GridViewRow oldrow in grvPedidosEmparentados.Rows)
        {
            if (((RadioButton)oldrow.FindControl("RadioButton1")).Checked)
            {
                txtMontoPagar = (TextBox)oldrow.FindControl("TxtMontoPagar");

                decimal.TryParse(txtMontoPagar.Text, out montoPagar);

                if (montoPagar> 0)
                {

                    pedidoPagar pedidoTemp = new pedidoPagar();
                    pedidoTemp.PedidoReferencia = oldrow.Cells[7].Text;
                    pedidoTemp.Monto = montoPagar;
                    montoTotal = montoTotal + montoPagar;
                    listaPedidos.Add(pedidoTemp);
                }               

                //filaGrid = oldrow;
                //
                //break;
            }
        }

        if (listaPedidos.Count==0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Es necesario que seleccione un registro');", true);
            return;
        }

        if (Monto != montoTotal)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Favor de verificar el monto capturado. ');", true);
            return;
        }


        foreach (pedidoPagar pedidoTemp in listaPedidos)
        {
            filaPadre = TablaPagosPadre.Select("pedidoreferencia='" +pedidoTemp.PedidoReferencia + "'").FirstOrDefault();

            pedidoTemp.Celulapedido = Convert.ToInt32(filaPadre["celula"]);
            pedidoTemp.Añopedido = Convert.ToInt32(filaPadre["añoPed"]);
            pedidoTemp.Pedido = Convert.ToInt32(filaPadre["pedido"]);
        }

        List<decimal> ListaMontosPadre = new List<decimal>();

        ListaMontosPadre.Add(200);
        ListaMontosPadre.Add(400);
        ListaMontosPadre.Add(400);



        int i = 0;
        int j = 0;

        while (i < listaPedidos.Count())
        {
            if (listaPedidos[i].Monto <= ListaMontosPadre[j])
            {
                pedidoPagar pedidoTemp = listaPedidos[i].Clonar();
                pedidoTemp.PedidoPadre = j;
                listaPedidosFinal.Add(pedidoTemp);
                ListaMontosPadre[j] = ListaMontosPadre[j] - pedidoTemp.Monto;
                i = i + 1;
            }
            else
            {
                pedidoPagar pedidoTemp = listaPedidos[i].Clonar();
                pedidoTemp.PedidoPadre = j;
                pedidoTemp.Monto = ListaMontosPadre[j];

                listaPedidos[i].Monto= listaPedidos[i].Monto - ListaMontosPadre[j];
                ListaMontosPadre[j] = 0;
                listaPedidosFinal.Add(pedidoTemp);
            }

            if (ListaMontosPadre[j] == 0)
            {
                j = j + 1;
            }
        }




        //PedidoReferencia = filaGrid.Cells[7].Text;
        //filasPadre = TablaPagosPadre.Select("pedidoreferencia='" + PedidoReferencia+"'");


        //celulapedido = Convert.ToInt32(filasPadre[0]["celula"]);
        //añopedido = Convert.ToInt32(filasPadre[0]["añoPed"]);
        //pedido = Convert.ToInt32(filasPadre[0]["pedido"]);
       
        conexion.AbrirConexion(true, true);
        
        try
        {
            foreach(pedidoPagar pedidoAPagar in listaPedidosFinal)
            {
                DataRow filaTabla = TablaPagos.Rows[pedidoAPagar.PedidoPadre];
                ReferenciaConciliadaPedido objRCP = new ReferenciaConciliadaPedidoDatos(
                                                    CorporativoConciliacion,
                                                    AnioConciliacion,
                                                    MesConciliacion,
                                                    FolioConciliacion,
                                                    SucursalConciliacion,
                                                    "",
                                                    Convert.ToInt32(filaTabla["FolioExt"]),
                                                    Convert.ToInt32(filaTabla["Secuencia"]),
                                                    Convert.ToString(filaTabla["Concepto"]),
                                                    pedidoAPagar.Monto,
                                                    0,
                                                    Convert.ToInt16(filaTabla["FormaConciliacion"]),
                                                    0,
                                                    "",
                                                    DateTime.Now,
                                                    DateTime.Now,
                                                    "",
                                                    "",
                                                    "",
                                                    "",
                                                    "",
                                                    pedidoAPagar.Monto,
                                                    0,
                                                    Convert.ToInt32(filaTabla["SucursalPedido"]),
                                                    "",
                                                    pedidoAPagar.Celulapedido,
                                                    pedidoAPagar.Añopedido,
                                                    pedidoAPagar.Pedido,
                                                    Convert.ToInt32(filaTabla["RemisionPedido"]),
                                                    Convert.ToString(filaTabla["SeriePedido"]),
                                                    Convert.ToInt32(filaTabla["FolioSat"]),
                                                    Convert.ToString(filaTabla["SerieSat"]),
                                                    Convert.ToString(filaTabla["ConceptoPedido"]),
                                                    Convert.ToDecimal(filaTabla["Diferencia"]),
                                                    Convert.ToString(filaTabla["StatusMovimiento"]),
                                                    ClientePadre,
                                                    "",
                                                    Convert.ToInt32(filaTabla["AñoPed"]),
                                                    Conciliacion.RunTime.App.ImplementadorMensajes);

                objRCP.TipoCobro = Convert.ToInt32(filaTabla["IdTipoCobro"]);
                objRCP.Guardar2(conexion);

            }
            //foreach (DataRow filaTabla in TablaPagos.Rows)
            //{

            //    ReferenciaConciliadaPedido objRCP = new ReferenciaConciliadaPedidoDatos(
            //                                        CorporativoConciliacion,
            //                                        AnioConciliacion,
            //                                        MesConciliacion,
            //                                        FolioConciliacion,
            //                                        SucursalConciliacion,
            //                                        "",
            //                                        Convert.ToInt32(filaTabla["FolioExt"]),
            //                                        Convert.ToInt32(filaTabla["Secuencia"]),
            //                                        Convert.ToString(filaTabla["Concepto"]),
            //                                        Convert.ToDecimal(filaTabla["Diferencia"]),
            //                                        0,
            //                                        Convert.ToInt16(filaTabla["FormaConciliacion"]),
            //                                        0,
            //                                        "",
            //                                        DateTime.Now,
            //                                        DateTime.Now,
            //                                        "",
            //                                        "",
            //                                        "",
            //                                        "",
            //                                        "",
            //                                        Convert.ToDecimal(filaTabla["Deposito"]),
            //                                        0,
            //                                        Convert.ToInt32(filaTabla["SucursalPedido"]),
            //                                        "",
            //                                        celulapedido,
            //                                        añopedido,
            //                                        pedido,
            //                                        Convert.ToInt32(filaTabla["RemisionPedido"]),
            //                                        Convert.ToString(filaTabla["SeriePedido"]),
            //                                        Convert.ToInt32(filaTabla["FolioSat"]),
            //                                        Convert.ToString(filaTabla["SerieSat"]),
            //                                        Convert.ToString(filaTabla["ConceptoPedido"]),
            //                                        Convert.ToDecimal(filaTabla["Diferencia"]),
            //                                        Convert.ToString(filaTabla["StatusMovimiento"]),
            //                                        ClientePadre,
            //                                        "",
            //                                        Convert.ToInt32(filaTabla["AñoPed"]),
            //                                        Conciliacion.RunTime.App.ImplementadorMensajes);

            //    objRCP.TipoCobro = Convert.ToInt32(filaTabla["IdTipoCobro"]);
            //    objRCP.Guardar2(conexion);
            //}
           // throw new Exception("Hola");
            conexion.CommitTransaction();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Ocurrio un error:"+ex.Message+" ');", true);
            conexion.RollBackTransaction();
            return;
        }
        Response.Redirect(Request.Url.ToString());


    }

    
}