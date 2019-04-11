using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EdoCtaPalabrasClave : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    [System.Web.Services.WebMethod]
    public static string CargaBancos()
    {
        List< CatalogoConciliacion.ReglasNegocio.Bancos> ListaBancos = new List<CatalogoConciliacion.ReglasNegocio.Bancos>();

        ListaBancos = CatalogoConciliacion.App.Consultas.ObtieneBancos();

        System.Web.Script.Serialization.JavaScriptSerializer jSearializer =
             new System.Web.Script.Serialization.JavaScriptSerializer();
        return jSearializer.Serialize(ListaBancos);

       


    }

    [System.Web.Services.WebMethod]
    public static string CargaCuentasBanco(string Banco)
    {
        List<CatalogoConciliacion.ReglasNegocio.CuentaContableBanco> ListaCuentaContableBanco = new List<CatalogoConciliacion.ReglasNegocio.CuentaContableBanco>();

        ListaCuentaContableBanco = CatalogoConciliacion.App.Consultas.ObtieneCuentaContableBanco(int.Parse(Banco));

        System.Web.Script.Serialization.JavaScriptSerializer jSearializer =
             new System.Web.Script.Serialization.JavaScriptSerializer();
        return jSearializer.Serialize(ListaCuentaContableBanco);




    }

}