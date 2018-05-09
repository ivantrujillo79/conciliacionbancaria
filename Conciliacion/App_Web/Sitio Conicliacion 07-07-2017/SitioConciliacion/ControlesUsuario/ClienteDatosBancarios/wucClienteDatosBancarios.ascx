<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucClienteDatosBancarios.ascx.cs" Inherits="ControlesUsuario_ClienteDatosBancarios_wucClienteDatosBancarios" %>

<table style="width:100%;">
    <tr class="etiqueta centradoJustificado">
        <td style="width:100%;">  <%--colspan="5"--%>
            <asp:HiddenField ID="hdfClienteSeleccionado" runat="server" />
            <asp:GridView runat="server" ID="grvClientes" AutoGenerateColumns="false" ShowHeader="true" 
                ShowHeaderWhenEmpty="true" CssClass="grvResultadoConsultaCss" ShowFooter="false" Width="100%"
                EnableViewState="true" ViewStateMode="Enabled">
                <HeaderStyle HorizontalAlign="Center" />
                <Columns>                    
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:RadioButton ID="rbSeleccion" runat="server" OnCheckedChanged="rbSeleccion_CheckedChanged"
                                AutoPostBack="true" EnableViewState="true" ViewStateMode="Enabled"/> 
                            <%--onclick="rbSeleccion_Click(this); CssClass="rbSeleccionClass" "--%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" BackColor="#ebecec"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cliente" SortExpression="Cliente">
                        <ItemTemplate>
                            <asp:Label ID="lblCliente" runat="server" Text='<%# Eval("NumCliente") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nombre" SortExpression="Nombre">
                        <ItemTemplate>
                            <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("Nombre") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Empresa" SortExpression="Empresa">
                        <ItemTemplate>
                            <asp:Label ID="lblEmpresa" runat="server" Text='<%# Eval("RazonSocial") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td class="centradoMedio" >
            <asp:Button ID="btnAceptar" runat="server" CssClass="boton bg-color-azulClaro fg-color-blanco"
                Text="ACEPTAR" Width="100px" />
            <asp:Button ID="btnCancelar" runat="server" CssClass="boton bg-color-grisClaro fg-color-blanco"
                Text="CANCELAR" Width="100px" />
        </td>
    </tr>
</table>

<script type="text/javascript">
    function rbSeleccion_Click(rb) {
        var gv = document.getElementById("<%= grvClientes.ClientID %>");
        var rbs = gv.getElementsByTagName("input");
        var row = rb.parentNode.parentNode.parentNode; 
        var labels = row.getElementsByTagName("span");

        for (var j = 0; j < labels.length; j++) {
            if (labels[j].id.indexOf("lblCliente") > -1) {
                $('#<%= hdfClienteSeleccionado.ClientID %>').val(labels[j].innerText);
            }

        }

        pintarFila(row);

        for (var i = 0; i < rbs.length; i++) {
            if (rbs[i].type == "radio") {
                if (rbs[i].checked && rbs[i] != rb) {
                    rbs[i].checked = false;
                    despintarFila(rbs[i].parentNode.parentNode.parentNode);
                    break;
                }
            }
        }
    }   

    function pintarFila(element) {
        element.style.backgroundColor = "rgb(78, 205, 194)";
        element.style.color = "rgb(255, 255, 255)";
    }

    function despintarFila(element) {
        element.style.backgroundColor = "rgb(255, 255, 255)";
        element.style.color = "rgb(29, 29, 29)";
    }
</script>
