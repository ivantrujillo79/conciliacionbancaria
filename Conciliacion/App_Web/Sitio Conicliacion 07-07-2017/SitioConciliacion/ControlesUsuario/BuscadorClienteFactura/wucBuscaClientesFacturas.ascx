<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucBuscaClientesFacturas.ascx.cs" Inherits="ControlesUsuario_BuscadorClienteFactura_wucBuscaClientesFacturas" %>

<script type="text/javascript">

    function ResaltaFactura() {
        /*Método que resaltará la columna Factura (efecto de  marca textos) en el grid provisto por la propiedad "GridRelacionado" 
         * para todos los registros que cuenten con la cadena provista por el usuario a través de la propiedad "NumeroFacturaResaltar". 
         * IMPORTANTE La factura se compone por una Serie consistente en una cadena de caracteres (máximo 10) 
         * y un Folio de tipo entero según se define en la tabla Factura de la base de datos del sistema Sigamet.*/
        //var gv = document.getElementById("ctl00_contenidoPrincipal_grvAgregadosPedidos");        
        var gv = document.getElementById('<%= HtmlIdGridRelacionado %>');
        var numfactura = document.getElementById('ctl00_contenidoPrincipal_wucBuscaClientesFacturas_txtFactura').value;
        var celdaid = $('<%= HtmlIdGridCeldaID %>').selector;
        var cnodoid = $('<%= HtmlIdGridCNodoID %>').selector;

        if (gv != null && numfactura != "")
        {
            var gvRowCount = gv.rows.length;
            var rwIndex = 0;
            var encontrado = false;
            
            debugger; //IMPORTANTE QUITAR

            for (rwIndex; rwIndex <= gvRowCount - 1; rwIndex++) {
                //if (gv.rows[rwIndex].cells[1].childNodes[1].innerText.localeCompare(numfactura) == 0) {
                //  gv.rows[rwIndex].cells[4].childNodes[0].innerText // Tipo: != 2, Criterio:Uno a Varios -> ctl00_contenidoPrincipal_grvInternos
                gvnumfactura = "";
                gvnumfactura = gv.rows[rwIndex].cells[celdaid].innerText;
                gvnumfactura = gvnumfactura.trim();
                if (gvnumfactura.localeCompare(numfactura) == 0) {
                    encontrado = true;
                    if (window.getSelection) {  // all browsers, except IE before version 9
                        var selection = window.getSelection();
                        var rangeToSelect = document.createRange();
                        //rangeToSelect.selectNodeContents(gv.rows[rwIndex].cells[1].childNodes[1]);
                        rangeToSelect.selectNodeContents(gv.rows[rwIndex].cells[celdaid]);
                        selection.removeAllRanges();   // clears the current selection
                        selection.addRange(rangeToSelect);
                    }
                    else {  // Internet Explorer before version 9
                        if (document.selection) {
                            var rangeToSelect = document.body.createTextRange();
                            //rangeToSelect.moveToElementText(gv.rows[rwIndex].cells[1].childNodes[1]);
                            rangeToSelect.moveToElementText(gv.rows[rwIndex].cells[celdaid]);
                            rangeToSelect.select();
                        }
                    }
                }
            }
            if (encontrado == false) {
                alert("No se encontro Numero de Factura.");
            }
        }
        return false;
    }

</script>

<table style="width: 100%">
    <tr>
        <td style="width: 20%;">
            <asp:Label ID="Label2" runat="server" Text="Factura" CssClass="etiqueta fg-color-blanco centradoMedio"></asp:Label>
        </td>
        <td style="width: 20%;">
            <asp:TextBox ID="txtFactura" runat="server" Width="80px" CssClass="cajaTextoPequeño"></asp:TextBox>
        </td>
        <td style="width: 20%;">
            <asp:ImageButton ID="btnFiltraCliente" runat="server" 
                CssClass="icono bg-color-verdeClaro"
                Height="16px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Buscar.png"
                ToolTip="Resalta factura" Width="16px"
                    OnClientClick="javascript:ResaltaFactura(); return false;" 
                />
        </td>
        <td style="width: 20%;">
            <asp:Label ID="Label1" runat="server" Text="Cliente" CssClass="etiqueta fg-color-blanco centradoMedio"></asp:Label>
        </td>
        <td style="width: 20%;">
            <asp:TextBox ID="txtCliente" runat="server" Width="80px" CssClass="cajaTextoPequeño"></asp:TextBox>
        </td>
    </tr>
</table>