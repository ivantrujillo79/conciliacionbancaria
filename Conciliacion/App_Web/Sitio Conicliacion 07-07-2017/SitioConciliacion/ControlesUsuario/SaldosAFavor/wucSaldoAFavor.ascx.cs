using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Conciliacion.RunTime.ReglasDeNegocio;
using System.Web.UI.HtmlControls;

public partial class ControlesUsuario_SaldosAFavor_wucSaldoAFavor : System.Web.UI.UserControl
{
    const string __strMontoAConciliar = "Monto a conciliar: ";
    const string __strResto = "Resto: ";

    #region Propiedades del control
    private byte _TipoOperacion;
    public byte TipoOperacion
    {
        get { return _TipoOperacion; }
        set
        {
            //1 = Consulta y 2 = Conciliación
            if (value == 1 || value == 2)
            {
                _TipoOperacion = value;
            }
            else
            {
                throw new Exception("El tipo de operación asignado no es válido");
            }
        }
    }
    public object Contenedor { get; set; }

    public decimal MontoAConciliar
    {
        get
        {
            return this.MontoAConciliar;
        }
        set
        {
            this.MontoAConciliar = value;
            lblMontoConciliar.Text = __strMontoAConciliar + this.MontoAConciliar.ToString();
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "Calendarios", "SAF_DatePickers();", true);
        lblResto.Text = __strResto + "$0.00";

        if (this.TipoOperacion == 1)
        {
            ddStatusConciliacion.Visible = true;

            List<Conciliacion.RunTime.ReglasDeNegocio.OpcionSaldoAFavor> ListaOpciones = new List<Conciliacion.RunTime.ReglasDeNegocio.OpcionSaldoAFavor>();
            ListaOpciones.Add(new OpcionSaldoAFavor { IDOpcion = 0, OpcionConciliacion = "Pendiente" });
            ListaOpciones.Add(new OpcionSaldoAFavor { IDOpcion = 1, OpcionConciliacion = "Conciliado" });

            ddStatusConciliacion.DataSource = ListaOpciones;
            ddStatusConciliacion.DataValueField = "IDOpcion";
            ddStatusConciliacion.DataTextField = "OpcionConciliacion";
            ddStatusConciliacion.DataBind();

            ddStatusConciliacion.Visible = true;
            btnCancelar.Visible = false;
            btnGuardar.Visible = false;
        }
        else
        {
            ddStatusConciliacion.Visible = false;
            btnCancelar.Visible = true;
            btnGuardar.Visible = true;
        }

    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        ModalPopupExtender objContenedor;
        objContenedor = (ModalPopupExtender)this.Contenedor;
        objContenedor.Hide();
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        ModalPopupExtender objContenedor;
        objContenedor = (ModalPopupExtender)this.Contenedor;
        objContenedor.Hide();
    }
    protected void imgBuscaPagares_Click(object sender, ImageClickEventArgs e)
    {
        List<DetalleSaldoAFavor> ListaSaldoAFavor = new List<DetalleSaldoAFavor>();
        ListaSaldoAFavor.Add(new DetalleSaldoAFavor { Folio = 1, Cliente = "763763", NombreCliente = "Marcos aurelio", Banco = "Banregio", Sucursal = "Monterrey", TipoCargo = "TipoA", Global = true, Fsaldo = DateTime.Now, Importe = Convert.ToDecimal("342.55"), Conciliada = "abc" });
        ListaSaldoAFavor.Add(new DetalleSaldoAFavor { Folio = 1, Cliente = "763763", NombreCliente = "Marcos aurelio", Banco = "Banregio", Sucursal = "Monterrey", TipoCargo = "TipoA", Global = true, Fsaldo = DateTime.Now, Importe = Convert.ToDecimal("342.55"), Conciliada = "abc" });
        ListaSaldoAFavor.Add(new DetalleSaldoAFavor { Folio = 1, Cliente = "763763", NombreCliente = "Marcos aurelio", Banco = "Banregio", Sucursal = "Monterrey", TipoCargo = "TipoA", Global = true, Fsaldo = DateTime.Now, Importe = Convert.ToDecimal("342.55"), Conciliada = "abc" });
        ListaSaldoAFavor.Add(new DetalleSaldoAFavor { Folio = 1, Cliente = "763763", NombreCliente = "Marcos aurelio", Banco = "Banregio", Sucursal = "Monterrey", TipoCargo = "TipoA", Global = true, Fsaldo = DateTime.Now, Importe = Convert.ToDecimal("342.55"), Conciliada = "abc" });
        grvSaldosAFavor.DataSource = ListaSaldoAFavor;
        grvSaldosAFavor.DataBind();
    }

    protected void GVCity_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlInputCheckBox cbElegido = (HtmlInputCheckBox)e.Row.FindControl("cbSAF");
            string Operacion = "SUMA";
            if(cbElegido.Checked)
            {
                Operacion = "SUMA";
            }
            else
            {
                Operacion = "RESTA";
            }
            cbElegido.Attributes["onchange"] = "javascript:return registroElegido("+ e.Row.RowIndex + ",'"+ Operacion + "');";
        }
    }
}