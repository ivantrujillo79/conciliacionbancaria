using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Locker;
using System.Data;

public partial class ControlesUsuario_GestorBloqueos_wucInterbloqueosEstadoCuenta : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {

            CargaFiltros();

            grdBloqueos.DataSource = CargaBloqueos();
            grdBloqueos.DataBind();
        }

    }

    #region Metodos

    private void CargaFiltros()
    {
        DataTable dtFiltros = new DataTable("Folios");

        dtFiltros.Columns.Add("Corporativo", typeof(string));
        dtFiltros.Columns.Add("Sucursal", typeof(string));
        dtFiltros.Columns.Add("Año", typeof(string));
        dtFiltros.Columns.Add("Folio", typeof(string));
        dtFiltros.Columns.Add("Secuencia", typeof(string));
        dtFiltros.Columns.Add("Usuario", typeof(string));

        dtFiltros.Rows.Add("Seleccionar", "Seleccionar", "Seleccionar", "Seleccionar", "Seleccionar", "Seleccionar");

        if (LockerExterno.ExternoBloqueado != null)
        {
            if (LockerExterno.ExternoBloqueado.Count > 0)
            {
                //Label1.Text = "Registros: " + LockerExterno.ExternoBloqueado.Count.ToString() + " registro 0: " + LockerExterno.ExternoBloqueado[0].SessionID.ToString() + " " + LockerExterno.ExternoBloqueado[0].Folio.ToString() + " " + LockerExterno.ExternoBloqueado[0].Consecutivo.ToString();
                foreach (RegistroExternoBloqueado obj in LockerExterno.ExternoBloqueado)
                {
                    //' Response.Write(obj.SessionID.ToString() + " " + obj.Folio.ToString() + " " + obj.Consecutivo.ToString() + "<br/>");
                    dtFiltros.Rows.Add(obj.Corporativo, obj.Sucursal, obj.Año, obj.Folio, obj.Consecutivo, obj.Usuario);

                }

            }


            ddlCorporativo.DataSource = dtFiltros.DefaultView.ToTable(true, "Corporativo");
            ddlCorporativo.DataTextField = "Corporativo";
            ddlCorporativo.DataValueField = "Corporativo";
            ddlCorporativo.DataBind();
            ddlCorporativo.SelectedIndex = -1;

            ddlSucursal.DataSource = dtFiltros.DefaultView.ToTable(true, "Sucursal");
            ddlSucursal.DataTextField = "Sucursal";
            ddlSucursal.DataValueField = "Sucursal";
            ddlSucursal.DataBind();
            ddlSucursal.SelectedIndex = -1;



            ddlAnio.DataSource = dtFiltros.DefaultView.ToTable(true, "Año");
            ddlAnio.DataTextField = "Año";
            ddlAnio.DataValueField = "Año";
            ddlAnio.DataBind();
            ddlAnio.SelectedIndex = -1;



            ddlFolio.DataSource = dtFiltros.DefaultView.ToTable(true, "Folio");
            ddlFolio.DataTextField = "Folio";
            ddlFolio.DataValueField = "Folio";
            ddlFolio.DataBind();
            ddlFolio.SelectedIndex = -1;



            ddlSecuencia.DataSource = dtFiltros.DefaultView.ToTable(true, "Secuencia");
            ddlSecuencia.DataTextField = "Secuencia";
            ddlSecuencia.DataValueField = "Secuencia";
            ddlSecuencia.DataBind();
            ddlSecuencia.SelectedIndex = -1;



            ddlUsuario.DataSource = dtFiltros.DefaultView.ToTable(true, "Usuario");
            ddlUsuario.DataTextField = "Usuario";
            ddlUsuario.DataValueField = "Usuario";
            ddlUsuario.DataBind();
            ddlUsuario.SelectedIndex = -1;

        }



    }

    private DataTable CargaBloqueos()
    {

        DataTable dtBloqueos = new DataTable("Bloqueos");

        dtBloqueos.Columns.Add("SessionId", typeof(string));
        dtBloqueos.Columns.Add("Corporativo", typeof(string));
        dtBloqueos.Columns.Add("Sucursal", typeof(string));
        dtBloqueos.Columns.Add("Año", typeof(string));
        dtBloqueos.Columns.Add("Folio", typeof(string));
        dtBloqueos.Columns.Add("Secuencia", typeof(string));
        dtBloqueos.Columns.Add("Descripcion", typeof(string));
        dtBloqueos.Columns.Add("Monto", typeof(decimal));
        dtBloqueos.Columns.Add("InicioBloqueo", typeof(DateTime));
        dtBloqueos.Columns.Add("Usuario", typeof(string));




        if (LockerExterno.ExternoBloqueado != null)
        {
            if (LockerExterno.ExternoBloqueado.Count > 0)
            {
               
                foreach (RegistroExternoBloqueado obj in LockerExterno.ExternoBloqueado)
                {                   

                    dtBloqueos.Rows.Add(obj.SessionID, obj.Corporativo, obj.Sucursal, obj.Año, obj.Folio, obj.Consecutivo, obj.Descripcion, obj.Monto, obj.InicioBloqueo, obj.Usuario);
                    
                }


            }

        }

        return dtBloqueos;

    }

    #endregion

    protected void Button1_Click(object sender, EventArgs e)
    {
        int Seleccionados = 0;

        foreach (
               GridViewRow fila in
                   grdBloqueos.Rows.Cast<GridViewRow>()
                                                .Where(fila => fila.RowType == DataControlRowType.DataRow))
        {

            if (fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked == true)
            {
                Seleccionados = Seleccionados+1;
            }
        }



        if (Seleccionados >0)
        {
                CheckBox chk = (sender as CheckBox);
            foreach (
                   GridViewRow fila in
                       grdBloqueos.Rows.Cast<GridViewRow>()
                                                    .Where(fila => fila.RowType == DataControlRowType.DataRow))
            {

                if (fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked == true)
                {
                    if (Locker.LockerExterno.ExternoBloqueado != null)
                    {
                        int J = Locker.LockerExterno.ExternoBloqueado.Count;
                        for (int i = 0; i <= J-1; i++)
                        {
                            Locker.LockerExterno.ExternoBloqueado.Remove(Locker.LockerExterno.ExternoBloqueado.Where<Locker.RegistroExternoBloqueado>(s => s.SessionID == fila.Cells[1].Text).ToList()[0]);
                        }
                    }
                }

            }

            grdBloqueos.DataSource = CargaBloqueos();
            grdBloqueos.DataBind();
        }

        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "alert('" + "Por favor seleccione los bloqueos que desea eliminar use las cajas de la columna eliminar" + "');", true);
        }
    }

    protected void ddlCorporativo_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlCorporativo.SelectedValue.ToString() != "Seleccionar")
        {
            DataRow[] dr = CargaBloqueos().Select("Corporativo='" + ddlCorporativo.SelectedValue.ToString() + "'");

            grdBloqueos.DataSource = dr.CopyToDataTable();
        }

    }

    protected void grdBloqueos_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void grdBloqueos_PageIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlCorporativo_TextChanged(object sender, EventArgs e)
    {

    }

    protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSucursal.SelectedValue.ToString() != "Seleccionar")
        {
            DataRow[] dr = CargaBloqueos().Select("Sucursal='" + ddlSucursal.SelectedValue.ToString() + "'");
            grdBloqueos.DataSource = dr.CopyToDataTable();
        }
    }

    protected void ddlAnio_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAnio.SelectedValue.ToString() != "Seleccionar")
        {
            DataRow[] dr = CargaBloqueos().Select("Año='" + ddlAnio.SelectedValue.ToString() + "'");
            grdBloqueos.DataSource = dr.CopyToDataTable();
        }
    }

    protected void ddlFolio_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCorporativo.SelectedValue.ToString() != "Seleccionar")
        {
            DataRow[] dr = CargaBloqueos().Select("Folio='" + ddlFolio.SelectedValue.ToString() + "'");
            grdBloqueos.DataSource = dr.CopyToDataTable();
        }
    }

    protected void ddlSecuencia_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSecuencia.SelectedValue.ToString() != "Seleccionar")
        {
            DataRow[] dr = CargaBloqueos().Select("Secuencia='" + ddlSecuencia.SelectedValue.ToString() + "'");
            grdBloqueos.DataSource = dr.CopyToDataTable();
        }
    }

    protected void ddlUsuario_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUsuario.SelectedValue.ToString() != "Seleccionar")
        {
            DataRow[] dr = CargaBloqueos().Select("Usuario='" + ddlUsuario.SelectedValue.ToString() + "'");
            grdBloqueos.DataSource = dr.CopyToDataTable();
        }

    }

    protected void ChkSelTodos_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (sender as CheckBox);

        if( ChkSelTodos.Checked == true)
        {
            foreach (
                GridViewRow fila in
                    grdBloqueos.Rows.Cast<GridViewRow>()
                                                 .Where(fila => fila.RowType == DataControlRowType.DataRow))
                fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked = chk.Checked;
        }

        else
        {
            foreach (
               GridViewRow fila in
                   grdBloqueos.Rows.Cast<GridViewRow>()
                                                .Where(fila => fila.RowType == DataControlRowType.DataRow))
                fila.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked = false;
        }

    }

    protected void grdBloqueos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
}