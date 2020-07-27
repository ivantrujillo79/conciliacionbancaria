using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Locker;
using System.Data;
using Conciliacion.RunTime.ReglasDeNegocio;
using SeguridadCB.Public;
using System.ComponentModel;
using System.Text;

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
        Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
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
                foreach (RegistroExternoBloqueado obj in LockerExterno.ExternoBloqueado)
                {
                    dtFiltros.Rows.Add(obj.Corporativo, obj.Sucursal, obj.Año, obj.Folio, obj.Secuencia, obj.Usuario);
                }
            }

            DataTable dtCorporativosDatos = dtFiltros.DefaultView.ToTable(true, "Corporativo");
            if (dtCorporativosDatos.Rows.Count > 1)
            { 
                DataTable dtCorporativosNombre = new DataTable();
                Usuario usuario;
                usuario = (Usuario)HttpContext.Current.Session["Usuario"];
                dtCorporativosNombre = usuario.CorporativoAcceso;

                IEnumerable<DataRow> query =
                    from nombre in dtCorporativosNombre.AsEnumerable()
                    join datos in dtCorporativosDatos.AsEnumerable()
                    on nombre.Field<short>("Corporativo").ToString() equals datos.Field<string>("Corporativo")
                    select nombre;
                DataTable dtCorporativos = query.CopyToDataTable();

                DataRow row = dtCorporativos.NewRow();
                row[0] = -1; row[1] = "Seleccionar"; row[2] = true;
                dtCorporativos.Rows.InsertAt(row, 0);

                ddlCorporativo.DataSource = dtCorporativos;
                ddlCorporativo.DataValueField = "Corporativo";
                ddlCorporativo.DataTextField = "NombreCorporativo";
                ddlCorporativo.DataBind();
                ddlCorporativo.SelectedIndex = -1;
            }

            DataTable dtSucursalesDatos = dtFiltros.DefaultView.ToTable(true, "Sucursal");
            if (dtSucursalesDatos.Rows.Count > 1)
            { 
                List<ListaCombo> listSucursalesNombre = objApp.Consultas.ConsultaSucursales(Conciliacion.RunTime.ReglasDeNegocio.Consultas.ConfiguracionIden0.Sin0, 1);
                DataTable dtSucursalesNombre = ConvertToDataTable(listSucursalesNombre);

                IEnumerable<DataRow> querySucursales =
                    from nombre in dtSucursalesNombre.AsEnumerable()
                    join datos in dtSucursalesDatos.AsEnumerable()
                    on nombre.Field<int>("Identificador").ToString() equals datos.Field<string>("Sucursal")
                    select nombre;
                DataTable dtSucursales = querySucursales.CopyToDataTable();

                DataRow rowSuc = dtSucursales.NewRow();
                rowSuc[0] = -1; rowSuc[1] = "Seleccionar"; 
                dtSucursales.Rows.InsertAt(rowSuc, 0);

                ddlSucursal.DataSource = dtSucursales;
                ddlSucursal.DataValueField = "Identificador";
                ddlSucursal.DataTextField = "Descripcion";
                ddlSucursal.DataBind();
            }

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

    public DataTable ConvertToDataTable<T>(IList<T> data)
    {
        PropertyDescriptorCollection properties =
           TypeDescriptor.GetProperties(typeof(T));
        DataTable table = new DataTable();
        foreach (PropertyDescriptor prop in properties)
            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        foreach (T item in data)
        {
            DataRow row = table.NewRow();
            foreach (PropertyDescriptor prop in properties)
                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            table.Rows.Add(row);
        }
        return table;

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

                    dtBloqueos.Rows.Add(obj.SessionID, obj.Corporativo, obj.Sucursal, obj.Año, obj.Folio, obj.Secuencia, obj.Descripcion, obj.Monto, obj.InicioBloqueo, obj.Usuario);
                    
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
                        //for (int i = 0; i <= J - 1; i++)
                        //{
                            //if (fila.Cells[1].Text == )
                            Locker.LockerExterno.ExternoBloqueado.Remove(Locker.LockerExterno.ExternoBloqueado.
                                Where<Locker.RegistroExternoBloqueado>(s => s.SessionID == fila.Cells[1].Text && s.Año.ToString() == fila.Cells[4].Text && s.Folio.ToString() == fila.Cells[5].Text && s.Secuencia.ToString() == fila.Cells[6].Text).ToList()[0]);
                        //}
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

    protected void grdBloqueos_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void grdBloqueos_PageIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlCorporativo_TextChanged(object sender, EventArgs e)
    {

    }

    private void AplicaFiltros()
    {
        StringBuilder filtro = new StringBuilder();

        if (ddlCorporativo.SelectedValue.ToString() != "-1")
            filtro.Append("Corporativo='" + ddlCorporativo.SelectedValue.ToString() + "'");

        if (ddlSucursal.SelectedValue.ToString() != "-1")
        {
            if (filtro.ToString() != string.Empty)
                filtro.Append(" AND ");
            filtro.Append(" Sucursal='" + ddlSucursal.SelectedValue.ToString() + "'");
        }

        if (ddlAnio.SelectedValue.ToString() != "Seleccionar")
        {
            if (filtro.ToString() != string.Empty)
                filtro.Append(" AND ");
            filtro.Append("Año='" + ddlAnio.SelectedValue.ToString() + "'");
        }

        if (ddlFolio.SelectedValue.ToString() != "Seleccionar")
        {
            if (filtro.ToString() != string.Empty)
                filtro.Append(" AND ");
            filtro.Append("Folio='" + ddlFolio.SelectedValue.ToString() + "'");
        }

        if (ddlSecuencia.SelectedValue.ToString() != "Seleccionar")
        {
            if (filtro.ToString() != string.Empty)
                filtro.Append(" AND ");
            filtro.Append("Secuencia ='" + ddlSecuencia.SelectedValue.ToString() + "'");
        }

        if (ddlUsuario.SelectedValue.ToString() != "Seleccionar")
        {
            if (filtro.ToString() != string.Empty)
                filtro.Append(" AND ");
            filtro.Append("Usuario='" + ddlUsuario.SelectedValue.ToString() + "'");
        }

        if (filtro.ToString() != string.Empty)
        {
            DataRow[] dr = CargaBloqueos().Select(filtro.ToString());
            grdBloqueos.DataSource = dr.CopyToDataTable();
        }
        else
        {
            grdBloqueos.DataSource = CargaBloqueos();
        }
        grdBloqueos.DataBind();
    }

    protected void ddlCorporativo_SelectedIndexChanged(object sender, EventArgs e)
    {
        AplicaFiltros();
    }

    protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
    {
        AplicaFiltros();
    }

    protected void ddlAnio_SelectedIndexChanged(object sender, EventArgs e)
    {
        AplicaFiltros();
    }

    protected void ddlFolio_SelectedIndexChanged(object sender, EventArgs e)
    {
        AplicaFiltros();
    }

    protected void ddlSecuencia_SelectedIndexChanged(object sender, EventArgs e)
    {
        AplicaFiltros();
    }

    protected void ddlUsuario_SelectedIndexChanged(object sender, EventArgs e)
    {
        AplicaFiltros();
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
        grdBloqueos.PageIndex = e.NewPageIndex;
        grdBloqueos.DataSource = CargaBloqueos();
        grdBloqueos.DataBind();
    }
}
