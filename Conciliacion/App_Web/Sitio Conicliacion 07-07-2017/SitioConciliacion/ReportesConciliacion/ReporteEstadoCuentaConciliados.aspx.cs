using CatalogoConciliacion;
using Conciliacion.RunTime.DatosSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportesConciliacion_ReporteEstadoCuentaConciliados : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Inicializarddlist();
        
    }

    private void Inicializarddlist()
    {        
        ddlEstatus.DataValueField = "Key";
        ddlEstatus.DataTextField = "Value";
        ddlEstatus.DataSource = CargarEstatus(); ;
        ddlEstatus.DataBind();

        ddlEstatusConcepto.DataValueField = "Key";
        ddlEstatusConcepto.DataTextField = "Value";
        ddlEstatusConcepto.DataSource = CargarEstatusConcepto(); ;
        ddlEstatusConcepto.DataBind();

        InicializarBanco();
    }

    private Dictionary<int,string> CargarEstatus()
    {
        Dictionary<int,string> ListaRetorno = new Dictionary<int,string>();
        ListaRetorno.Add(0,"Todo");
        ListaRetorno.Add(1, "Conciliado");
        ListaRetorno.Add(2, "No conciliado");
        return ListaRetorno;
    }

    private Dictionary<int, string> CargarEstatusConcepto()
    {
        Dictionary<int, string> ListaRetorno = new Dictionary<int, string>();
        ListaRetorno.Add(0, "Todos");
        ListaRetorno.Add(1, "Concepto1");
        ListaRetorno.Add(2, "Concepto2");
        return ListaRetorno;
    }

    private void InicializarBanco()
    {
        List<InformeBancarioDatos.DetalleBanco> lstDetalle = new List<InformeBancarioDatos.DetalleBanco>();
        lstDetalle = consultarBancos();
        Dictionary<int, string> ListaRetorno = new Dictionary<int, string>();
        ddlBanco.DataValueField = "IDBanco";
        ddlBanco.DataTextField = "Descripcion";
        ddlBanco.DataSource = lstDetalle;
        ddlBanco.DataBind();
        

    }

    private List<InformeBancarioDatos.DetalleBanco> consultarBancos()
    {
        Conexion conexion = new Conexion();
        var lstDetalle = new List<InformeBancarioDatos.DetalleBanco>();

        try
        {
            var informeBancario = new InformeBancarioDatos(App.ImplementadorMensajes);
            conexion.AbrirConexion(false);
            SeguridadCB.Public.Usuario Usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
            lstDetalle = informeBancario.consultarBancos(conexion, 1, Usuario.IdUsuario);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", @"alertify.alert('Conciliaci&oacute;n bancaria','Error: " + ex.Message.Replace("'", "") + "', function(){ alertify.error('Error en la solicitud'); });", true);
        }
        finally
        {
            conexion.CerrarConexion();
        }
        return lstDetalle;
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg",
        @"alertify.alert('Conciliaci&oacute;n bancaria','¡Informe Estado de cuenta conciliados generado con éxito!', function(){document.getElementById('LigaDescarga').click(); });", true);
    }
}