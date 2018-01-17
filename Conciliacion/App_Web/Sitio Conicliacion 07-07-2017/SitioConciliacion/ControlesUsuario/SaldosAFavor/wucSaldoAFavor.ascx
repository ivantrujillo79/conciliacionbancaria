<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucSaldoAFavor.ascx.cs" Inherits="ControlesUsuario_SaldosAFavor_wucSaldoAFavor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<script type="text/javascript">
    function ValidaNumero(e) {
        var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
        return ((tecla > 47 && tecla < 58));
    }
    function ValidaMoneda(e) {
        var charCode = (e.which) ? e.which : e.keyCode;
        if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
</script>


<style type="text/css">
    .auto-style1 {
        height: 19px;
    }
</style>

<table style="width: 100%;">
    <tr class="etiqueta centradoJustificado fg-color-blanco bg-color-azulClaro">
        <td>
            Fecha de saldos
        </td>
        <td></td>
        <td></td>
        <td id="ColConciliada">Conciliada</td>
        <td>Cliente</td>
        <td>Monto</td>
        <td></td>
    </tr>
    <tr  class="etiqueta centradoJustificado fg-color-blanco bg-color-azulClaro">
        <td>
            <asp:CheckBox ID="cbTodos" runat="server" Text="Todos"/>
        </td>
        <td>
            <asp:TextBox runat="server" ID="txtFechaInicio" CssClass="cajaTexto" ToolTip="Fecha inicio" ValidationGroup="vgFecha" Width="90px" Font-Size="11px"/>
        </td>
        <td>
            <asp:TextBox runat="server" ID="txtFechaFin" CssClass="cajaTexto" ToolTip="Fecha fin" ValidationGroup="vgFecha" Width="90px" Font-Size="11px"/>
        </td>
        <td id="ColConciliada1">
            <asp:DropDownList ID="ddStatusConciliacion" runat="server"></asp:DropDownList>
        </td>      
        <td>
            <asp:TextBox ID="txtCliente" runat="server" onkeypress="return ValidaNumero(event)"></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="txtMonto" runat="server" onkeypress="return ValidaMoneda(event)"></asp:TextBox>
            <asp:CompareValidator id="cvMonto" runat="server" 
                ControlToValidate="txtMonto" 
                Operator="DataTypeCheck"
                Type="Currency" ErrorMessage="Formato incorrecto" ValidationGroup="vgMoneda" />

            <br />

        </td>  
        <td>            
            <div class="bg-color-grisClaro fg-color-amarillo">
                <asp:ImageButton ID="imgBuscaPagares" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Buscar.png"
                ToolTip="Buscar saldos a favor" Width="30px" Height="30px" style="padding: 5px 5px 5px 5px;" 
                ValidationGroup="vgFecha, vgMoneda" OnClick="imgBuscaPagares_Click"/>
            </div>
        </td>       
    </tr>
    <tr class="centradoJustificado">
        <td class="auto-style1"><asp:CheckBox ID="cbMontosIguales" runat="server" Text="Montos iguales"/></td>
        <td class="auto-style1"></td>
        <td class="auto-style1"><asp:Label ID="lblMontoConciliar" runat="server"/>Monto a conciliar:</td>
        <td id="ColConciliada2" class="auto-style1"></td>
        <td id="cellResto" class="auto-style1"><asp:Label ID="lblResto" runat="server"/></td>
        <td class="auto-style1"></td>
        <td class="auto-style1"></td>
    </tr>
    <tr>
        <td colspan="7"></td>
    </tr>
    <tr>
        <td colspan="7">
            <div style="width:900px; height:300px; overflow:auto;" >
                <asp:GridView ID="grvSaldosAFavor" runat="server" AutoGenerateColumns="False" CssClass="grvResultadoConsultaCss" OnRowDataBound="GVCity_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <input type="checkbox" runat="server" id="cbSAF"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField AccessibleHeaderText="Folio" HeaderText="Folio" DataField="Folio" />
                        <asp:BoundField AccessibleHeaderText="Cliente" HeaderText="Cliente" DataField="Cliente" />
                        <asp:BoundField AccessibleHeaderText="Nombre Cliente" HeaderText="Nombre Cliente" DataField="NombreCliente" />
                        <asp:BoundField AccessibleHeaderText="Banco" HeaderText="Banco" DataField="Banco"/>
                        <asp:BoundField AccessibleHeaderText="Sucursal" HeaderText="Sucursal" DataField="Sucursal" />
                        <asp:BoundField AccessibleHeaderText="Tipo Cargo" HeaderText="Tipo Cargo" DataField ="TipoCargo" />
                        <asp:BoundField AccessibleHeaderText="Global" HeaderText="Global" DataField = "Global" />
                        <asp:BoundField AccessibleHeaderText="Fsaldo" HeaderText="Fsaldo" DataField = "Fsaldo"  />
                        <asp:BoundField AccessibleHeaderText="Importe" HeaderText="Importe" DataField = "Importe"  />
                        <asp:BoundField AccessibleHeaderText="Conciliada" HeaderText="Conciliada" DataField = "Conciliada"  />
                    </Columns>
                </asp:GridView>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="7"></td>
    </tr>
        <tr>
        <td colspan="7">
            <div class="centradoMedio">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CssClass="boton fg-color-blanco bg-color-azulClaro"/>
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" CssClass="boton fg-color-blanco bg-color-grisClaro" />
            </div>
        </td>
    </tr>
</table>