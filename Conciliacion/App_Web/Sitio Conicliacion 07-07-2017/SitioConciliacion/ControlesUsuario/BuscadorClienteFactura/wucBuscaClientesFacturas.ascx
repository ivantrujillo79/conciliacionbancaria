<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucBuscaClientesFacturas.ascx.cs" Inherits="ControlesUsuario_BuscadorClienteFactura_wucBuscaClientesFacturas" %>

<script type="text/javascript">
    function doSearch(text) {
        if (window.find && window.getSelection) {
            document.designMode = "on";
            var sel = window.getSelection();
            sel.collapse(document.body, 0);

            while (window.find(text)) {
                document.execCommand("HiliteColor", false, "yellow");
                sel.collapseToEnd();
            }
            document.designMode = "off";
        }
        else
            if (document.body.createTextRange) {
                //var textRange = document.body.createTextRange();
                //var textRange = document.selectNodes('//h2').createTextRange();
                //var textRange = document.evaluate('//h2', document, null, XPathResult.ANY_TYPE, null).createTextRange();
                //var textRange = document.getElementById('<%=GridRelacionado.ClientID %>');
                
                //var grid = $find("ctl00_contenidoPrincipal_wucBuscaClientesFacturas_GridRelacionado");
                //var activeRow = grid.get_gridView().get_behaviors().get_activation().get_activeCell().get_row();
                var textRange = document.getElementsByName("ctl00_contenidoPrincipal_wucBuscaClientesFacturas_GridRelacionado");
                
                //Cell = grid.rows[0].cells[0];
                

                while (textRange.findText(text)) {
                    textRange.execCommand("BackColor", false, "yellow");
                    textRange.collapse(false);
                }
            }
    }

    function SelectRed(text) {
        //var redElement = document.getElementById("red");
        var redElement = document.getElementById("ctl00_contenidoPrincipal_grvAgregadosPedidos_ctl02_lblPedido"); //ctl00_contenidoPrincipal_grvAgregadosPedidos_ctl02_lblPedido
        if (redElement != null)
        {
            if (redElement.innerText.localeCompare(text) == 0) {
                if (window.getSelection) {  // all browsers, except IE before version 9
                    var selection = window.getSelection();
                    var rangeToSelect = document.createRange();
                    rangeToSelect.selectNodeContents(redElement);

                    selection.removeAllRanges();   // clears the current selection
                    selection.addRange(rangeToSelect);
                }
                else {  // Internet Explorer before version 9
                    if (document.selection) {
                        var rangeToSelect = document.body.createTextRange();
                        rangeToSelect.moveToElementText(redElement);
                        rangeToSelect.select();
                        //rangeToSelect.execCommand("BackColor", false, "yellow");
                        //rangeToSelect.collapse(false);
                    }
                }
            }
        }
    }

</script>

<table style="width: 100%">
    <tr>
        <td rowspan="2" style="vertical-align: top; width: 20%;">
            <asp:Label ID="Label2" runat="server" Text="Factura" CssClass="etiqueta fg-color-blanco centradoMedio"></asp:Label>
        </td>
        <td rowspan="2" style="vertical-align: top; width: 20%;">
            <asp:TextBox ID="txtFactura" runat="server" OnTextChanged="txtFactura_TextChanged" Width="80px" CssClass="cajaTextoPequeño"></asp:TextBox>
        </td>
        <td rowspan="2" style="vertical-align: top; width: 20%;">
            <img src="http://www.comacsis.com/imagenes/ok.png" alt="Smiley face" height="42" width="42"  
                onclick="SelectRed (document.getElementById('ctl00_contenidoPrincipal_wucBuscaClientesFacturas_txtFactura').value);" >
        </td>
        <td rowspan="2" style="vertical-align: top; width: 20%;">
            <asp:Label ID="Label1" runat="server" Text="Cliente" CssClass="etiqueta fg-color-blanco centradoMedio"></asp:Label>
        </td>
        <td rowspan="2" style="vertical-align: top; width: 20%;">
            <asp:TextBox ID="txtCliente" runat="server" OnTextChanged="txtCliente_TextChanged" Width="80px" CssClass="cajaTextoPequeño"></asp:TextBox>
        </td>
        <td rowspan="2" style="vertical-align: top; width: 20%;">
            <%--<asp:ImageButton ID="btnBuscar" runat="server" 
                CssClass="icono bg-color-verdeClaro"
                Height="16px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Buscar.png"
                ToolTip="FILTRAR Cliente" Width="16px"    
                 OnClick="btnBuscar_Click"/>    --%>

            <%--<img src="http://www.comacsis.com/imagenes/ok.png" alt="Smiley face" height="42" width="42"  
                onclick="SelectRed ();"
            >--%>

            <%--<button onclick="SelectRed ();">Prueba</button>--%>
            <%--<br /><br />
            <div id="red" style="color:red">prueba a buscar</div>--%>

        </td>
    </tr>

</table>