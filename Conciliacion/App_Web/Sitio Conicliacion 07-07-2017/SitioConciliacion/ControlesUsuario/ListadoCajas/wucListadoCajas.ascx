<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucListadoCajas.ascx.cs" Inherits="ControlesUsuario_ListadoCajas_wucListadoCajas" %>

<div style="height: 250px; padding:5px;" class="centradoJustificado">
    <asp:Repeater ID="repCajas" runat="server">
        <HeaderTemplate>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:CheckBox ID="chkTodos" runat="server" Text="Todos"/>
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkElementoCaja" runat="server" Text='<%# Eval("Descripcion") %>' />
                    </td>
                </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>

    <%--<table style="width: 100%;" class="centradoJustificado fg-color-blanco bg-color-azulClaro">
        <tr>
            <td style="padding:5px;">
                <asp:CheckBox ID="chkTodos" runat="server" Text="Todos"/>
            </td>
        </tr>
        <tr>
            <td style="padding:5px;">
                <asp:CheckBox ID="chkElemento" runat="server" Text="Caja1"/>
            </td>
        </tr>
    </table>--%>
</div>