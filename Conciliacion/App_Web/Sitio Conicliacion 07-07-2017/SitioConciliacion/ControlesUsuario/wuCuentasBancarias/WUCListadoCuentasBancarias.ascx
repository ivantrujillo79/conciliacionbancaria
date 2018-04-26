<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCListadoCuentasBancarias.ascx.cs" Inherits="ControlesUsuario_wuCuentasBancarias_WUCListadoCuentasBancarias" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<div style="height: 250px; padding:5px; overflow:auto;" class="etiqueta centradoJustificado">
    <asp:Repeater ID="repCuentas" runat="server" >
        <HeaderTemplate>
            <table style="width: 100%; color:white; font-weight: bold; ">
                <tr>
                    <td>
                        <asp:CheckBox ID="chkTodos" runat="server" Text="Todos" OnCheckedChanged="chkTodos_CheckedChanged"
                            AutoPostBack="true"/>
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkElementoCuenta" runat="server" Text='<%# Eval("Descripcion") %>'
                            OnCheckedChanged="chkElementoCuenta_CheckedChanged" AutoPostBack="true" />
                        <asp:HiddenField ID="hdfIndiceCuentaa" runat="server" Value='<%# Eval("ID") %>' />
                    </td>
                </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</div>