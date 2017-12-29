<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucSaldoAFavor.ascx.cs" Inherits="ControlesUsuario_SaldosAFavor_wucSaldoAFavor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<script type="text/javascript">

    function SAF_DatePickers() {
        $("#<%= txtFechaInicio.ClientID%>").datepicker({
            defaultDate: "+1w",
            changeMonth: true,
            changeYear: true,
            onClose: function(selectedDate) {
                $("#<%=txtFechaFin.ClientID%>").datepicker("option", "minDate", selectedDate);
            }
        });
        $("#<%=txtFechaFin.ClientID%>").datepicker({
            defaultDate: "+1w",
            changeMonth: true,
            changeYear: true,
            onClose: function(selectedDate) {
                $("#<%=txtFechaInicio.ClientID%>").datepicker("option", "maxDate", selectedDate);
            }
        });
    }

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
        <td colspan="3">
            Fecha de saldos
        </td>
        <td></td>
        <td></td>
        <td>Conciliada</td>
        <td>Cliente</td>
        <td>Monto</td>
        <td></td>
    </tr>
    <tr  class="etiqueta centradoJustificado fg-color-blanco bg-color-azulClaro">
        <td colspan="3">
            <asp:CheckBox ID="cbTodos" runat="server" Text="Todos"/>
        </td>
        <td>
            <asp:TextBox runat="server" ID="txtFechaInicio" CssClass="cajaTexto" ToolTip="Fecha inicio" ValidationGroup="vgFecha" Width="90px" Font-Size="11px"/>
        </td>
        <td>
            <asp:TextBox runat="server" ID="txtFechaFin" CssClass="cajaTexto" ToolTip="Fecha fin" ValidationGroup="vgFecha" Width="90px" Font-Size="11px"/>
        </td>
        <td>
            <asp:DropDownList ID="ddStatusConciliacion" runat="server"></asp:DropDownList>
        </td>      
        <td>
            <asp:TextBox ID="txtCliente" runat="server" onkeypress="return ValidaNumero(event)"></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="txtMonto" runat="server" onkeypress="return ValidaMoneda(event)"></asp:TextBox>
            <br />

        </td>  
        <td>            
            <asp:ImageButton ID="imgBuscaPagares" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Buscar.png"
                ToolTip="BUSCAR" style="padding: 10px 4px 7px 5px;" 
                ValidationGroup="vgFecha, vgMoneda" OnClick="imgBuscaPagares_Click"/>
        </td>       
    </tr>
    <tr>
        <td class="auto-style1"></td>
        <td class="auto-style1"></td>
        <td class="auto-style1"></td>
        <td class="auto-style1"></td>
        <td class="auto-style1"></td>
        <td class="auto-style1">
            <asp:CompareValidator id="cvMonto" runat="server" 
                ControlToValidate="txtMonto" 
                Operator="DataTypeCheck"
                Type="Currency" ErrorMessage="Formato incorrecto" ValidationGroup="vgMoneda" />
        </td>
        <td class="auto-style1"></td>
    </tr>
    <tr>
        <td colspan="7">
            <asp:GridView ID="gvSaldosAFavor" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:CheckBoxField AccessibleHeaderText="Seleccionar" />
                    <asp:BoundField AccessibleHeaderText="Folio" HeaderText="Folio" />
                    <asp:BoundField AccessibleHeaderText="Cliente" HeaderText="Cliente" />
                    <asp:BoundField AccessibleHeaderText="Nombre Cliente" HeaderText="Nombre Cliente" />
                    <asp:BoundField AccessibleHeaderText="Banco" HeaderText="Banco" />
                    <asp:BoundField AccessibleHeaderText="Sucursal" HeaderText="Sucursal" />
                    <asp:BoundField AccessibleHeaderText="Tipo Cargo" HeaderText="Tipo Cargo" />
                    <asp:BoundField AccessibleHeaderText="Global" HeaderText="Global" />
                    <asp:BoundField AccessibleHeaderText="Fsaldo" HeaderText="Fsaldo" />
                    <asp:BoundField AccessibleHeaderText="Importe" HeaderText="Importe" />
                    <asp:BoundField AccessibleHeaderText="Conciliada" HeaderText="Conciliada" />
                </Columns>
                
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="7"></td>
    </tr>
        <tr>
        <td colspan="7">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
        </td>
    </tr>
</table>