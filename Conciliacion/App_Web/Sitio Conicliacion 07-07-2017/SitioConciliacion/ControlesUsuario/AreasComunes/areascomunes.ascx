<%@ Control Language="C#" AutoEventWireup="true" CodeFile="areascomunes.ascx.cs" Inherits="ControlesUsuario_AreasComunes_areascomunes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<style type="text/css">
    .txtAlign {
        TEXT-ALIGN: right;
    }
</style>

<div style="max-height: 500px; overflow: auto; text-align: left">

    <p>
        <asp:Label ID="lblClientePadre" runat="server">Prueba</asp:Label>
    </p>
<%--    <asp:ScriptManager ID="ScriptManager1" runat="server"
        EnableScriptGlobalization="True">
    </asp:ScriptManager>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <table border="0" style="width: 635px;">
                    <tr>
                        <td style="vertical-align: middle;">
                            <asp:Label CssClass="miscLabel" ID="lblTagFecha" runat="server" Text="F. Suministro: "></asp:Label>
                        </td>
                        <td style="vertical-align: middle;">
                            <asp:TextBox Width="90px" ID="txtFSuministroInicio" runat="server" Text="" CssClass="calendarTextBox" Enabled="false"></asp:TextBox>
                            <cc2:CalendarExtender ID="txtFSuministroInicio_CalendarExtender" runat="server"
                                TargetControlID="txtFSuministroInicio" Format="dd/MM/yyyy" PopupButtonID="imgFSuministroInicio">
                            </cc2:CalendarExtender>
                            <asp:ImageButton runat="Server" ID="imgFSuministroInicio" ImageUrl="~/App_Themes/GasMetropolitano/Imagenes/Calendar.png" AlternateText="Muestra Calendario" />
                        </td>
                        <td style="vertical-align: middle;">
                            <asp:TextBox Width="90px" ID="txtFSuministroFin" runat="server" Text="" CssClass="calendarTextBox" Enabled="false"></asp:TextBox>
                            <cc2:CalendarExtender ID="txtFSuministroFin_CalendarExtender" runat="server"
                                TargetControlID="txtFSuministroFin" Format="dd/MM/yyyy" PopupButtonID="imgFSuministroFin">
                            </cc2:CalendarExtender>
                            <asp:ImageButton runat="Server" ID="imgFSuministroFin" ImageUrl="~/App_Themes/GasMetropolitano/Imagenes/Calendar.png" AlternateText="Muestra Calendario" />

                        </td>
                        <td style="vertical-align: middle;">
                            <asp:ImageButton class="iconoOpcion bg-color-naranja" ID="btnConsultaFsuministro" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Buscar.png" ToolTip="Busca Por Fechas" Width="25px" OnClick="imgBuscarFechas_Click" />
                        </td>
                        <td style="vertical-align: middle;">
                            <asp:Label CssClass="miscLabel" ID="Label1" runat="server" Text="P. Referencia: "></asp:Label>
                        </td>
                        <td style="vertical-align: middle;">
                            <asp:TextBox Width="150px" ID="txtPedidoReferencia" runat="server" Text=""></asp:TextBox>
                        </td>
                        <td style="vertical-align: middle;">
                            <asp:ImageButton class="iconoOpcion bg-color-naranja" ID="btnConsultaPedidoReferencia" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Buscar.png" ToolTip="Busca Por Referencia" Width="25px" OnClick="imgBuscarReferencia_Click" />
                        </td>

                    </tr>
                </table>
            </div>
            <div>
                <table border="0" style="width: 635px;">
                    <tr>
                        <td style="vertical-align: middle;">
                            <asp:Label ID="Label2" runat="server" Text="Seleccionado: "> </asp:Label>
                        </td>
                        <td style="vertical-align: middle;">
                            <asp:Label ID="txtTotalSeleccionado" runat="server" Text="0.00" BackColor="gray" ForeColor="white" Width="90px" Height="20px" CssClass="txtAlign"></asp:Label>
                        </td>
                        <td style="vertical-align: middle;">
                            <asp:Label ID="Label4" runat="server" Text="Abono: ">  </asp:Label>
                        </td>
                        <td style="vertical-align: middle;">
                            <asp:Label ID="txtTotalAbono" runat="server" Text="0.00" BackColor="blue" ForeColor="white" Width="90px" Height="20px" CssClass="txtAlign"></asp:Label>
                        </td>
                        <td style="vertical-align: middle;">
                            <asp:Label ID="Label6" runat="server" Text="Resto: "> </asp:Label>
                        </td>
                        <td style="vertical-align: middle; text-align=rigth">
                            <asp:Label ID="txtTotalResto" runat="server" Text="0.00" BackColor="purple" ForeColor="white" Width="90px" Height="20px" CssClass="txtAlign"></asp:Label>
                        </td>
                        <td style="vertical-align: middle;">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="boton fg-color-blanco bg-color-azulClaro" />
                        </td>
                    </tr>
                </table>
            </div>
            <asp:HiddenField ID="rgSeleccionado" runat="server" />
            <asp:GridView ID="grvPedidosEmparentados" runat="server" CellPadding="20" ForeColor="#333333" GridLines="None" Style="width: 635px;"
                CssClass="grvResultadoConsultaCss" BorderStyle="Solid" EmptyDataText="Sin pagos" AutoGenerateColumns="False">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField ItemStyle-Width="2px" ControlStyle-Width="22px" HeaderText="Sel" ShowHeader="False">
                        <ItemTemplate>
                            <asp:RadioButton ID="RadioButton1" Text="" OnCheckedChanged="rbSelector_CheckedChanged" AutoPostBack="true" GroupName="Apply" runat="server"></asp:RadioButton>
                        </ItemTemplate>
                        <ControlStyle Width="22px" />
                        <FooterStyle Width="22px" />
                        <HeaderStyle Width="22px" />
                        <ItemStyle Width="22px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="FSuministro" HeaderText="FSuministro" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="Monto" DataFormatString="{0:c}" HeaderText="Monto" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nom. Cliente" />
                    <asp:BoundField DataField="Concepto" HeaderText="Concepto" />
                    <asp:BoundField DataField="Factura" HeaderText="Factura" />
                    <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                    <asp:BoundField DataField="pedidoreferencia" HeaderText="P. Referencia" />
                </Columns>

            </asp:GridView>

        </ContentTemplate>
    </asp:UpdatePanel>
</div>
