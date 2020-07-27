using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Acceso_Acceso : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
        objApp.ImplementadorMensajes.ContenedorActual = this;
        if (HttpContext.Current.Request.UrlReferrer != null)
        {
            if ((!HttpContext.Current.Request.UrlReferrer.AbsoluteUri.Contains("SitioConciliacion")) || (HttpContext.Current.Request.UrlReferrer.AbsoluteUri.Contains("Acceso.aspx")))
            {
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
                HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
                Response.Cache.SetExpires(DateTime.Now);
            }
        }

        if (Session.Count > 0)
        {
            Session.Clear();
            Session.Abandon();
            Page.Application.RemoveAll();
        }

        CargaScriptsIniciales();
        Login.CargaScriptsIniciales();
        Page.ClientScript.RegisterOnSubmitStatement(this.GetType(), "", "return ValidaEnvio();");
       
        this.Master.MenuVisible = false;
        this.Master.smPathVisible = false;

        this.Master.lblInformacionGeneral = "© 2013 Grupo Metropolitano - Transforma Consultoria";

        
    }

    private void CargaScriptsIniciales()
    {
        if (!Page.ClientScript.IsStartupScriptRegistered("Common"))
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Common", "<script languaje = JavaScript; src=../App_Scripts/Common.js></script>");
    }
    protected void Login_Load(object sender, EventArgs e)
    {
         
    }
   
}