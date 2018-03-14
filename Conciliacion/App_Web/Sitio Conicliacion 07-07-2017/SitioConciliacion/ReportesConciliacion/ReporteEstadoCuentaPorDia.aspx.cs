using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Conciliacion.RunTime.ReglasDeNegocio;

public partial class ReportesConciliacion_ReporteEstadoCuentaPorDia : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        InicializarCuentas();
    }

    private void InicializarCuentas()
    {
        List<Cuenta> ListaCuentas = new List<Cuenta>();
        try
        {
            for (int i = 1; i <= 6; i++)
            {
                Cuenta cuenta = new Cuenta(i, "Banco " + i);
                ListaCuentas.Add(cuenta);
            }

            WUCCuentasBancarias.Cuentas = ListaCuentas;
        }
        catch (Exception ex)
        {
            throw ex;
        }      
    }
}