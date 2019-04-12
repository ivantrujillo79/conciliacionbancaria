using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using rg= CatalogoConciliacion.ReglasNegocio;

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

    [System.Web.Services.WebMethod]
    public static string CargaTipoCobro()
    {
        List<CatalogoConciliacion.ReglasNegocio.TipoCobro> ListaTipoCobro = new List<CatalogoConciliacion.ReglasNegocio.TipoCobro>();

        ListaTipoCobro = CatalogoConciliacion.App.Consultas.ObtieneTipoCobro();

        System.Web.Script.Serialization.JavaScriptSerializer jSearializer =
             new System.Web.Script.Serialization.JavaScriptSerializer();
        return jSearializer.Serialize(ListaTipoCobro);


    }

    [System.Web.Services.WebMethod]
    public static string CargaColumnaDestino()
    {
        List<CatalogoConciliacion.ReglasNegocio.ColumnaDestino> ListaColumnaDestino = new List<CatalogoConciliacion.ReglasNegocio.ColumnaDestino>();

        ListaColumnaDestino = CatalogoConciliacion.App.Consultas.ObtieneColumnaDestino();

        System.Web.Script.Serialization.JavaScriptSerializer jSearializer =
             new System.Web.Script.Serialization.JavaScriptSerializer();
        return jSearializer.Serialize(ListaColumnaDestino);


    }

    [System.Web.Services.WebMethod]
    public static string ConsultarPalabrasClave(string banco,string cuentabanco, string tipocobro)
    {
        List<CatalogoConciliacion.ReglasNegocio.PalabrasClave> ListaPalabrasClave = new List<CatalogoConciliacion.ReglasNegocio.PalabrasClave>();

        ListaPalabrasClave = CatalogoConciliacion.App.Consultas.ConsultarPalabrasClave(int.Parse(banco), cuentabanco, int.Parse(tipocobro));

        System.Web.Script.Serialization.JavaScriptSerializer jSearializer =
             new System.Web.Script.Serialization.JavaScriptSerializer();
        return jSearializer.Serialize(ListaPalabrasClave);


    }

    [System.Web.Services.WebMethod]
    public static string GuardarPalabrasClave(string banco, string cuentabanco, string tipocobro,string palabraclave,string columnadestino)
    {
        bool res = false;
        CatalogoConciliacion.Datos.PalabrasClaveDatos pc = new CatalogoConciliacion.Datos.PalabrasClaveDatos(int.Parse(banco),cuentabanco,int.Parse(tipocobro), palabraclave, columnadestino);
        res = pc.Guardar();
 
        System.Web.Script.Serialization.JavaScriptSerializer jSearializer =
             new System.Web.Script.Serialization.JavaScriptSerializer();
        return jSearializer.Serialize(res);


    }

}