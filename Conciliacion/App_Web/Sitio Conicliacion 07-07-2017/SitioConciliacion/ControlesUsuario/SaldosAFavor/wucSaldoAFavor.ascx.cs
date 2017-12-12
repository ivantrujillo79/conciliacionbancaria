using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Conciliacion.RunTime.ReglasDeNegocio;

public partial class ControlesUsuario_SaldosAFavor_wucSaldoAFavor : System.Web.UI.UserControl
{
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.TipoOperacion == 1)
        {
            ddStatusConciliacion.Visible = true;

            List<Conciliacion.RunTime.ReglasDeNegocio.OpcionSaldoAFavor> ListaOpciones = new List<Conciliacion.RunTime.ReglasDeNegocio.OpcionSaldoAFavor>();
            ListaOpciones.Add(new OpcionSaldoAFavor{IDOpcion = 0, OpcionConciliacion = "Pendiente"});
            ListaOpciones.Add(new OpcionSaldoAFavor { IDOpcion = 1, OpcionConciliacion = "Conciliado" });

            ddStatusConciliacion.DataSource = ListaOpciones;
            ddStatusConciliacion.DataValueField = "IDOpcion";
            ddStatusConciliacion.DataTextField = "OpcionConciliacion";
            ddStatusConciliacion.DataBind();
        }
        else
        {
            ddStatusConciliacion.Visible = false;
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
    }
}