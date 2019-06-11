using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Locker;

public partial class Principal : System.Web.UI.MasterPage
{

    private SeguridadCB.Public.Operaciones operaciones;

    #region "Propiedades"
    public bool MenuVisible
    {
        set
        {
            menuPrincipal.Visible = value;
        }
    }

    public bool smPathVisible
    {
        set
        {
            smPath.Visible = value;
        }
    }

    //public bool pnlTituloVisible
    //{
    //    set
    //    {
    //        if (pnlTitulo.Visible) pnlTitulo.Visible = value;
    //    }
    //}

    //public string lblTituloPrincipal
    //{
    //    set
    //    {
    //        lblTitulo.Text = value;
    //    }
    //}

    public string lblInformacionGeneral
    {
        set
        {
            lblInformacion.Text = value;
            lblInformacion.EnableViewState = true;
        }
    }
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if ((Session.Count > 0) && (operaciones == null))
            {
                operaciones = (SeguridadCB.Public.Operaciones)HttpContext.Current.Session["Operaciones"];
                //this.pnlTituloVisible = true;
                this.lblInformacionGeneral = (string)HttpContext.Current.Session["PiePagina"];
                this.smPathVisible = true;
            }
        
            if (!Page.IsPostBack)
            {
                if (this.Request.UrlReferrer != null &&
                    (this.Request.UrlReferrer.ToString().ToUpper().Contains("MANUAL.ASPX") ||
                    this.Request.UrlReferrer.ToString().ToUpper().Contains("UNOAVARIOS.ASPX") ||
                    this.Request.UrlReferrer.ToString().ToUpper().Contains("CANTIDADCONCUERDA.ASPX") ||
                    this.Request.UrlReferrer.ToString().ToUpper().Contains("CANTIDADYREFERENCIACONCUERDA.ASPX") ||
                    this.Request.UrlReferrer.ToString().ToUpper().Contains("UNOAVARIOS.ASPX") ||
                    this.Request.UrlReferrer.ToString().ToUpper().Contains("VARIOSAUNO.ASPX")
                    ))
                {
                    if (! this.Request.UrlReferrer.ToString().Contains(Request.Url.AbsolutePath.ToString()))
                    {
                        //if (this.Request.UrlReferrer.ToString().ToUpper().Contains("MANUAL.ASPX"))
                        //    LockerExterno.EliminarBloqueos(Session.SessionID, "MANUAL");
                        //else
                        //if (this.Request.UrlReferrer.ToString().ToUpper().Contains("UNOAVARIOS.ASPX"))
                        //    LockerExterno.EliminarBloqueos(Session.SessionID, "UNOAVARIOS");
                        //else
                        //if (this.Request.UrlReferrer.ToString().ToUpper().Contains("CANTIDADCONCUERDA.ASPX"))
                        //    LockerExterno.EliminarBloqueos(Session.SessionID, "CANTIDADCONCUERDA");
                        //else
                        //if (this.Request.UrlReferrer.ToString().ToUpper().Contains("CANTIDADYREFERENCIACONCUERDA.ASPX"))
                        //    LockerExterno.EliminarBloqueos(Session.SessionID, "CANTIDADYREFERENCIACONCUERDA");
                        //else
                        //if (this.Request.UrlReferrer.ToString().ToUpper().Contains("UNOAVARIOS.ASPX"))
                        //    LockerExterno.EliminarBloqueos(Session.SessionID, "UNOAVARIOS");
                        //else
                        //if (this.Request.UrlReferrer.ToString().ToUpper().Contains("VARIOSAUNO.ASPX"))
                        //    LockerExterno.EliminarBloqueos(Session.SessionID, "VARIOSAUNO");
                        //else
                        LockerExterno.EliminarBloqueos(Session.SessionID);
                    }
                }

            }
        }
        catch (Exception)
        {
            throw;
        }
    }



    protected void menuPrincipal_MenuItemDataBound(object sender, MenuEventArgs e)
    {
        try
        {
            AppSettingsReader settings = new AppSettingsReader();
            short modulo = Convert.ToSByte(settings.GetValue("Modulo", typeof(string)).ToString());

            if (Session.Count > 0)
            {
                SiteMapNode currentnode = (SiteMapNode)e.Item.DataItem;

                //SeguridadCorporativo.Public.Operaciones operaciones = (SeguridadCorporativo.Public.Operaciones)HttpContext.Current.Session["Operaciones"];
                int moduloopcion = 0;

                if (currentnode.ResourceKey != null)
                {
                    moduloopcion = Convert.ToInt32(currentnode.ResourceKey);
                }

                switch (moduloopcion)
                {
                    case 1:
                        e.Item.Enabled = operaciones.EstaHabilitada(modulo, "Acceso");
                        break;
                    case 2:
                        e.Item.Enabled = operaciones.EstaHabilitada(modulo, "Nueva conciliacion");
                        break;
                    case 3:
                        e.Item.Enabled = operaciones.EstaHabilitada(modulo, "Subir archivo");
                        break;
                    case 4:
                        e.Item.Enabled = operaciones.EstaHabilitada(modulo, "Consultar archivos");
                        break;
                    case 5:
                        e.Item.Enabled = operaciones.EstaHabilitada(modulo, "Nueva plantilla");
                        break;
                    case 6:
                        e.Item.Enabled = operaciones.EstaHabilitada(modulo, "Consultar plantillas");
                        break;
                    case 7:
                        e.Item.Enabled = operaciones.EstaHabilitada(modulo, "Catalogo de Motivos");
                        break;
                    case 8:
                        e.Item.Enabled = operaciones.EstaHabilitada(modulo, "Catalogo Grupo");
                        break;
                    case 14:
                        e.Item.Enabled = operaciones.EstaHabilitada(modulo, "Catalogo Tipo conciliaciòn/Usuario");
                        break;
                    case 15:
                        e.Item.Enabled = operaciones.EstaHabilitada(modulo, "Catalogo referencia");
                        break;
                    case 16:
                        e.Item.Enabled = operaciones.EstaHabilitada(modulo, "Catalogo separadores");
                        break;
                    case 17:
                        e.Item.Enabled = operaciones.EstaHabilitada(modulo, "Catalogo tipo archivo");
                        break;
                    case 18:
                        e.Item.Enabled = operaciones.EstaHabilitada(modulo, "Catalogo etiqueta");
                        break;
                    case 20:
                        e.Item.Enabled = operaciones.EstaHabilitada(modulo, "Importar de aplicación");
                        break;
                    case 21:
                        e.Item.Enabled = operaciones.EstaHabilitada(modulo, "Catalogo status concepto");
                        break;
                    case 22:
                        e.Item.Enabled = operaciones.EstaHabilitada(modulo, "Catalogo Cuenta Transferencia");
                        break;
                    case 23:
                        e.Item.Enabled = operaciones.EstaHabilitada(modulo, "Catálogos de Configuraciòn");
                        break;
                    case 24:
                        e.Item.Enabled = operaciones.EstaHabilitada(modulo, "Catálogos de Importaciòn de Archivos");
                        break;
                    case 25:
                        e.Item.Enabled = operaciones.EstaHabilitada(modulo, "Catálogos de Seguridad");
                        break;
                    case 27:
                        e.Item.Enabled = operaciones.EstaHabilitada(modulo, "Informe Posicion Bancos");
                        break;
                    case 28:
                        e.Item.Enabled = operaciones.EstaHabilitada(modulo, "Informe Estados Cuenta");
                        break;
                    case 29:
                        e.Item.Enabled = operaciones.EstaHabilitada(modulo, "Informe Estados Cuenta Dia");
                        break;
                    case 30:
                        e.Item.Enabled = operaciones.EstaHabilitada(modulo, "Informe Estado Cuenta Conciliado");
                        break;
                    default:
                        e.Item.Enabled = false;
                        break;
                }

            }
            else
            {
                Response.Redirect("~/Acceso/Acceso.aspx", true);
            }
        }
        catch (Exception) { Response.Redirect("~/Acceso/Acceso.aspx", true); }
    }
    protected void menuPrincipal_MenuItemClick(object sender, MenuEventArgs e)
    {

        SiteMapNode currentnode = (SiteMapNode)e.Item.DataItem;
        //currentnode.Url.Split("?");

    }
}