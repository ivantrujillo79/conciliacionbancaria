using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Conciliacion_FlujoEfectivo_FlujoEfectivoReal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //Llena el dropDown de  paginacion para Conciliados
    protected void paginasDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList oIraPag = sender as DropDownList;
        int iNumPag;

        grvFlujoReal.PageIndex = int.TryParse(oIraPag.Text.Trim(), out iNumPag) && iNumPag > 0 &&
                                   iNumPag <= grvFlujoReal.PageCount
                                       ? iNumPag - 1
                                       : 0;

        //LlenaGridViewFlujoProyectado();
    }

}