<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucListadoCajas.ascx.cs" Inherits="ControlesUsuario_ListadoCajas_wucListadoCajas" %>

<div style="height: 250px; padding:5px; overflow:auto;" class="centradoJustificado">
    <asp:Repeater ID="repCajas" runat="server" ViewStateMode="Enabled">
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
                        <asp:CheckBox ID="chkElementoCaja" runat="server" Text='<%# Eval("Descripcion") %>'
                            OnCheckedChanged="chkElementoCaja_CheckedChanged" AutoPostBack="true" EnableViewState="true" ViewStateMode="Enabled"/>
                        <asp:HiddenField ID="hdfIndiceCaja" runat="server" Value='<%# Eval("ID") %>' />
                    </td>
                </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</div>