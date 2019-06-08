using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using SeguridadCB.Public;
using Conciliacion.RunTime.ReglasDeNegocio;
using CatalogoConciliacion;


public partial class Catalogos_TipoMovimientoPorCuenta : System.Web.UI.Page
{
    CatalogoConciliacion.App objAppCat = new CatalogoConciliacion.App();
    Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();

    #region "Propiedades Globales"
    private SeguridadCB.Public.Usuario usuario;
    #endregion

    #region "Propiedades Globales"
    private SeguridadCB.Public.Operaciones operaciones;
    #endregion

    CatalogoConciliacion.ReglasNegocio.ParametroAplicacion objParametro;



    protected override void OnPreInit(EventArgs e)
    {
        if (HttpContext.Current.Session["Operaciones"] == null)
            Response.Redirect("~/Acceso/Acceso.aspx", true);
        else
            operaciones = (SeguridadCB.Public.Operaciones)HttpContext.Current.Session["Operaciones"];
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        objApp.ImplementadorMensajes.ContenedorActual = this;
        try
        {
           
            //Llamamos a la clase app perteneciente a libreria de clases donde estamos apuntando
            
            if (!IsPostBack)
            {
                usuario = (SeguridadCB.Public.Usuario)HttpContext.Current.Session["Usuario"];
                objParametro = objAppCat.Parametro.CrearObjeto();

                objParametro.Parametro = "NumeroDocumentosTRANSBAN";

                cargaValores();                      
            }
            
            this.tbValor.Focus();

        }
        catch (Exception ex)
        {
           objApp.ImplementadorMensajes.MostrarMensaje("Error\n"+ex.Message);
        }

    }

    protected void cargaValores()
    {
        if (objParametro.Consultar())
        {
            lbObservaciones.Text = objParametro.Observaciones + ": " + objParametro.Valor;
        }
    }

    protected void btnModificar_Click1(object sender, EventArgs e)
    {

        try
        {
            // if (cboCorporativo.Items.Count == 0 && cboCorporativoDestino_.Items.Count == 0)
            objParametro = objAppCat.Parametro.CrearObjeto();

            objParametro.Parametro = "NumeroDocumentosTRANSBAN";
            objParametro.Valor = tbValor.Text;
            if(objParametro.Modificar())
            {

                cargaValores();

                tbValor.Text = String.Empty;
            }
                
           
        }
        catch (Exception ex)
        {

        }
    }
    
}