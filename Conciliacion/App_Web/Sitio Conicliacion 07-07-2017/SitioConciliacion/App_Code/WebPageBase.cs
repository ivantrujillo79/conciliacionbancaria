using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.IO;

// creamos una clase base para todos nuestro formularios de nuestra aplicacion
public class WebPageBase : System.Web.UI.Page
{
	public WebPageBase()
	{
    }
    
    // definimos una constante de tipo string para almacenar el nombre 
    // de una entrada en el objeto session
    private string KEY_PERSISTENT_VIEWSTATENAME = "PageViewState";

    

    // creamos una propiedad de tipo boleana para habilitar el procesamiento de adminsitracion del viewstate
    // del lado de servidor. No todas las paginas queremos este comportamiento asi que mejor especificamos
    // cuando y que pagina requiere un comportamiento como tal
    private bool _viewStateOnServer = false;
    public bool ViewStateOnServer
    {
        get { return _viewStateOnServer; }
        set { _viewStateOnServer = value; }
    }

    // sobre escribimos SavePageStateToPersistenceMedium para atrapar personalizar el comportamiento. Aqui es
    // donde obtenemos el viewstate de nuestra pagina y almacenammos en algun medio la informacion.
    protected override void SavePageStateToPersistenceMedium(object state)
    {
        if (_viewStateOnServer)
        {
            // llamada al metodo encargado de almacenar en el objeto sesion
            ToSession(state);
        }
        else
        {
            base.SavePageStateToPersistenceMedium(state);
        }
    }

    // sobre escribimos LoadPageStateFromPersistenceMedium para atrapar y personazlziar el coportamiento cuando
    // vamos a enviear el viewstate a la pagina que mandaremos al cliente
    protected override object LoadPageStateFromPersistenceMedium()
    {
        if (_viewStateOnServer)
        {
            return FromSession();
        }
        else
        {
            return base.LoadPageStateFromPersistenceMedium();
        }
    }

    // este metodo obtiene del objeto sesion la informacion del viewstate para una pagina en cuestion. 
    public object FromSession()
    {
        Object viewState;
        String view;
        MemoryStream ms;
        LosFormatter lf;
        HttpContext ctx;
        StreamReader sr;

        try
        {

            ctx = HttpContext.Current;
            ms = ctx.Session[KEY_PERSISTENT_VIEWSTATENAME] as MemoryStream;
            ms.Position = 0;
            sr = new StreamReader(ms);
            view = sr.ReadToEnd();
            sr.Close();

            lf = new LosFormatter();
            viewState = lf.Deserialize(view);

            return viewState;


        }
        catch (Exception ex)
        { throw ex; }



    }
    
    // este metodo almacena en sesion el viewstate de una pagina en particular.
    public void ToSession(object viewState)
    {

        MemoryStream ms = new MemoryStream();
        LosFormatter lf;
        HttpContext ctx;

        try
        {
            ctx = HttpContext.Current;
            lf = new LosFormatter();
            lf.Serialize(ms, viewState);

            ctx.Session.Add(KEY_PERSISTENT_VIEWSTATENAME, ms);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }




}
