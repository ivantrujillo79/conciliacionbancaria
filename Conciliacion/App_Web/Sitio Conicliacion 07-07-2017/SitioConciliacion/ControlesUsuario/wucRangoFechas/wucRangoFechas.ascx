<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucRangoFechas.ascx.cs" Inherits="ControlesUsuario_wucRangoFechas_wucRangoFechas" %>

<script type="text/javascript">

    function pageLoad() {
        //FInicio - FFinal
        activarDatePickers();
    }

    function activarDatePickers() {
        $("#<%= txtFechaInicial.ClientID%>").datepicker({
            defaultDate: "+1w",
            changeMonth: true,
            changeYear: true,
            onClose: function (selectedDate) {
                $("#<%=txtFechaInicial.ClientID%>").datepicker("option", "minDate", selectedDate);
            }
        });

        $("#<%=txtFechaFinal.ClientID%>").datepicker({
            defaultDate: "+1w",
            changeMonth: true,
            changeYear: true,
            onClose: function (selectedDate) {
                $("#<%=txtFechaFinal.ClientID%>").datepicker("option", "maxDate", selectedDate);
            }
        });
    }

    function OnChangetxtFecha(valor) {
        if (valor.value.trim() = "") {
            valor.style.backgroundColor = "yellow";
        }
        else {
            valor.style.backgroundColor = "";
        }
    }

</script>

 <table style="width: 100%">
    <tr>
        <asp:Label ID="lbTextoDespliega" runat="server" Text=""></asp:Label>
        <td style="width: 50%;">
            <asp:TextBox ID="txtFechaInicial" runat="server" CssClass="cajaTexto" Font-Size="10px" Width="85%"
                onchange="javascript:OnChangetxtFecha(this)"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator1" runat="server" 
                Type="Date"
                Operator="DataTypeCheck"
                ControlToValidate="txtFechaInicial"
                ErrorMessage="Capture una fecha valida">
            </asp:CompareValidator>
        </td>
        <td style="width: 50%;">
            <asp:TextBox ID="txtFechaFinal" runat="server" CssClass="cajaTexto" Font-Size="10px" Width="85%"
                onchange="javascript:OnChangetxtFecha(this)"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator2" runat="server" 
                Type="Date"
                Operator="DataTypeCheck"
                ControlToValidate="txtFechaFinal"
                ErrorMessage="Capture una fecha valida">
            </asp:CompareValidator>
        </td>
    </tr>
</table>
