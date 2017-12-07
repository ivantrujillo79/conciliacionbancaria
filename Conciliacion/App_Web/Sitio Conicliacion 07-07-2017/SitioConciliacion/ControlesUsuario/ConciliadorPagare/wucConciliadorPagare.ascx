<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucConciliadorPagare.ascx.cs" Inherits="wucConciliadorPagare" %>

<table style="width: 100%;">
    <tr class="etiqueta centradoJustificado fg-color-blanco bg-color-azulClaro">
        <td style="width:60%; vertical-align:top;">
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

        <td style="width:35%; vertical-align:top;">
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
        <td style="width:5%" align="left" class="iconoOpcion bg-color-naranja">
            <asp:ImageButton ID="imgBuscaPagares" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Buscar.png"
                ToolTip="BUSCAR" style="padding: 10px 4px 7px 5px;" 
                ValidationGroup="vgFecha"/>
        </td>
    </tr>
    <tr>
        <td class="etiqueta lineaVertical centradoMedio" style="width:50%; padding:5px" >
            Monto conciliar:
        </td>
    </tr>
</table>