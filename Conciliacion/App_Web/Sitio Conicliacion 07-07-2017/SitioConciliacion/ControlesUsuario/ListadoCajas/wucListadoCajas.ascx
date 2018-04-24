<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucListadoCajas.ascx.cs" Inherits="ControlesUsuario_ListadoCajas_wucListadoCajas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<div style="height: 250px; padding:5px; overflow:auto;" class="etiqueta centradoJustificado">
    <asp:Repeater ID="repCajas" runat="server" >
        <HeaderTemplate>
            <table class="usercontrolCajas"style="width: 100%; color:white;font-weight: bold;">
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
                        <asp:CheckBox ID="chkElementoCaja" runat="server" Text='<%# Eval("Descripcion") %>'
                            OnCheckedChanged="chkElementoCaja_CheckedChanged" AutoPostBack="true" />
                        <asp:HiddenField ID="hdfIndiceCaja" runat="server" Value='<%# Eval("ID") %>' />
                    </td>
                </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</div>