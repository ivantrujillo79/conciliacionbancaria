<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucConciliadorPagare.ascx.cs" Inherits="wucConciliadorPagare" %>

<table style="width: 100%;">
    <tr class="etiqueta centradoJustificado fg-color-blanco bg-color-azulClaro">
        <td style="width:75%; vertical-align:top;" colspan="3">
            <table style="width: 100%">
                <tr>
                    <td style="padding: 5px;" align="left" colspan="3">
                        <asp:Label ID="lblFechaPagare" runat="server" CssClass="etiqueta fg-color-blanco centradoMedio" 
                            text="Fecha del pagaré:"/>
                    </td>
                </tr>
                <tr>        
                    <td style="padding: 5px;" align="left">
                        <asp:CheckBox runat="server" ID="chkTodos" CssClass="etiqueta fg-color-blanco centradoMedio"
                            text="Todos"/>
                    </td>
                    <td style="padding: 5px;" align="left">
                        <asp:TextBox runat="server" ID="txtFechaInicio" CssClass="cajaTexto" ToolTip="Fecha inicio"
                            ValidationGroup="vgFecha" Width="90px" Font-Size="11px"/>
                    </td>
                    <td style="padding: 5px;" align="left">
                        <asp:TextBox runat="server" ID="txtFechaFin" CssClass="cajaTexto" ToolTip="Fecha fin"
                            ValidationGroup="vgFecha" Width="90px" Font-Size="11px"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="1"></td>
                    <td>
                        <div style="min-height:18px;">
                            <asp:RangeValidator ID="rvFechaInicio" runat="server" ControlToValidate="txtFechaInicio"
                                CssClass="etiqueta fg-color-naranja" Display="Dynamic" ErrorMessage="Por favor insertar una fecha válida"
                                MinimumValue="28/12/1000" MaximumValue="28/12/9999" Type="Date" ValidationGroup="vgFecha"
                                Font-Size="12px"></asp:RangeValidator>
                        </div>
                    </td>
                    <td>
                        <div style="min-height:18px;">
                            <asp:RangeValidator ID="rvFechaFin" runat="server" ControlToValidate="txtFechaFin"
                                CssClass="etiqueta fg-color-naranja" Display="Dynamic" ErrorMessage="Por favor insertar una fecha válida"
                                MinimumValue="28/12/1000" MaximumValue="28/12/9999" Type="Date" ValidationGroup="vgFecha"
                                Font-Size="12px"></asp:RangeValidator>
                        </div>
                    </td>
                </tr>
            </table>    
        </td>
        <td style="width:20%; vertical-align:top;" colspan="1">
            <table style="width: 100%">
                <tr>
                    <td style="padding: 5px;" align="left">     
                        <asp:Label ID="lblMonto" runat="server" CssClass="etiqueta fg-color-blanco centradoMedio"
                            Text="Monto:"/>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 5px">
                        <asp:TextBox ID="txtMonto" runat="server" CssClass="cajaTexto" Width="100px" Font-Size="11px"/>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width:5%" align="left" class="iconoOpcion bg-color-naranja" colspan="1">
            <asp:ImageButton ID="imgBuscaPagares" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Buscar.png"
                ToolTip="BUSCAR" style="padding: 10px 4px 7px 5px;" 
                ValidationGroup="vgFecha"/>
        </td>
    </tr>
    <tr>
        <td style="width:75%; vertical-align:top;" colspan="4">            
            <table style="width:100%">
                <tr>
                    <td class="etiqueta lineaVertical centradoDerecha" style="width:25%; padding:5px" >
                        Monto conciliar:
                    </td>
                    <td class="etiqueta lineaVertical centradoIzquierda">
                        <div class="bg-color-grisOscuro fg-color-blanco" style="width: 25%; padding: 5px;">
                            <asp:Label runat="server" ID="lblMontoExterno" Text="$ 0.00"></asp:Label>
                        </div>
                    </td>
                    <td class="etiqueta lineaVertical centradoDerecha" style="width:25%; padding:5px" >
                        Resto:
                    </td>
                    <td class="etiqueta lineaVertical centradoIzquierda">
                        <div class="bg-color-azul fg-color-blanco" style="width: 25%; padding: 5px;">
                            <asp:Label runat="server" ID="lblResto" Text="$ 0.00"></asp:Label>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
        <td colspan="2"></td>
    </tr>
    <tr class="etiqueta centradoJustificado">
        <td style="width:100%;" colspan="5">
            <asp:GridView runat="server" ID="grvPagares" AutoGenerateColumns="false" ShowHeader="true" 
                ShowHeaderWhenEmpty="true" CssClass="grvResultadoConsultaCss" ShowFooter="false" Width="100%">
                <HeaderStyle HorizontalAlign="Center" />
                <Columns>                    
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkAgregar" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" BackColor="#ebecec"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Folio corte" SortExpression="Folio">
                        <ItemTemplate>
                            <asp:Label ID="lblFolio" runat="server" Text='<%# Eval("") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FOperacion" SortExpression="FOperacion">
                        <ItemTemplate>
                            <asp:Label ID="lblFOperacion" runat="server" Text='<%# Eval("", "{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Caja" SortExpression="Caja">
                        <ItemTemplate>
                            <asp:Label ID="lblCaja" runat="server" Text='<%# Eval("") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Consecutivo" SortExpression="Consecutivo">
                        <ItemTemplate>
                            <asp:Label ID="lblConsecutivo" runat="server" Text='<%# Eval("") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Tipo aplicación" SortExpression="TAplicacion">
                        <ItemTemplate>
                            <asp:Label ID="lblTApliacion" runat="server" Text='<%# Eval("") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Monto" SortExpression="Monto">
                        <ItemTemplate>
                            <asp:Label ID="lblMonto" runat="server" Text='<%# Eval("", "{0:C}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Observación" SortExpression="Observacion">
                        <ItemTemplate>
                            <asp:Label ID="lblObservacion" runat="server" Text='<%# Eval("") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td class="centradoMedio" colspan="5">
            <asp:Button ID="btnGuardar" runat="server" CssClass="boton bg-color-verdeClaro fg-color-blanco"
                Text="GUARDAR" Width="100px" />
            <asp:Button ID="btnCancelar" runat="server" CssClass="boton bg-color-grisClaro01 fg-color-blanco"
                Text="CANCELAR" Width="100px" />
        </td>
    </tr>
</table>