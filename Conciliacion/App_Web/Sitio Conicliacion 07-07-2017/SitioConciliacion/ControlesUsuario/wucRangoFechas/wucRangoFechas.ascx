<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucRangoFechas.ascx.cs" Inherits="ControlesUsuario_wucRangoFechas_wucRangoFechas" %>

<script type="text/javascript">
    <%--function pageLoad() {
        //FInicio - FFinal
        activarDatePickers();
    }--%>
    
    function RF_DatePickers() {
        $("#<%= txtFechaInicial.ClientID%>").datepicker({
            defaultDate: "+1w",
            changeMonth: true,
            changeYear: true,
            onClose: function (selectedDate) {
                $("#<%=txtFechaFinal.ClientID%>").datepicker("option", "minDate", selectedDate);
            }
        });

        $("#<%=txtFechaFinal.ClientID%>").datepicker({
            defaultDate: "+1w",
            changeMonth: true,
            changeYear: true,
            onClose: function (selectedDate) {
                $("#<%=txtFechaInicial.ClientID%>").datepicker("option", "maxDate", selectedDate);
            }
        });
    }
    
    function txtFecha_onblur(obj) {
        //debugger;
        if (document.getElementById("ctl00_contenidoPrincipal_wucRangoFechas_txtFechaInicial").value.trim() == ""
            &&
            document.getElementById("ctl00_contenidoPrincipal_wucRangoFechas_txtFechaFinal").value.trim() != "") {
            obj.style.backgroundColor = "orange";
        }
        else
            if (document.getElementById("ctl00_contenidoPrincipal_wucRangoFechas_txtFechaFinal").value.trim() == ""
                &&
                document.getElementById("ctl00_contenidoPrincipal_wucRangoFechas_txtFechaInicial").value.trim() != "") {
                obj.style.backgroundColor = "orange";
            }
            else {
                document.getElementById("ctl00_contenidoPrincipal_wucRangoFechas_txtFechaInicial").style.backgroundColor = "";
                document.getElementById("ctl00_contenidoPrincipal_wucRangoFechas_txtFechaFinal").style.backgroundColor = "";
            }
    }

    function ValidaFiltro()
    {
        if (document.getElementById("ctl00_contenidoPrincipal_wucRangoFechas_txtFechaInicial").value.trim() == ""
            &&
            document.getElementById("ctl00_contenidoPrincipal_wucRangoFechas_txtFechaFinal").value.trim() != "") {
            alert("Capture un periodo de fechas valido");
            document.getElementById("ctl00_contenidoPrincipal_wucRangoFechas_txtFechaInicial").focus();
            return false
        }
        else
            if (document.getElementById("ctl00_contenidoPrincipal_wucRangoFechas_txtFechaFinal").value.trim() == ""
                &&
                document.getElementById("ctl00_contenidoPrincipal_wucRangoFechas_txtFechaInicial").value.trim() != "") {
                alert("Capture un periodo de fechas valido");
                document.getElementById("ctl00_contenidoPrincipal_wucRangoFechas_txtFechaFinal").focus();
                return false
            }
            else 
                return true;
    }

    function RF_LimpiarCampos() {
        $('#<%= txtFechaInicial.ClientID %>').val("");
        $('#<%= txtFechaFinal.ClientID %>').val("");
    }

</script>

 <table style="width: 100%">
    <tr>
        <asp:Label ID="lbTextoDespliega" runat="server" Text=""></asp:Label>
        <td style="width: 40%;">
            <asp:TextBox ID="txtFechaInicial" runat="server" CssClass="cajaTexto" Font-Size="10px" Width="85%"
                onblur="txtFecha_onblur(this)"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator1" runat="server" 
                Type="Date"
                Operator="DataTypeCheck"
                ControlToValidate="txtFechaInicial"
                ErrorMessage="Capture una fecha valida">
            </asp:CompareValidator>
        </td>
        <td style="width: 40%;">
            <asp:TextBox ID="txtFechaFinal" runat="server" CssClass="cajaTexto" Font-Size="10px" Width="85%"
                onblur="txtFecha_onblur(this)"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator2" runat="server" 
                Type="Date"
                Operator="DataTypeCheck"
                ControlToValidate="txtFechaFinal"
                ErrorMessage="Capture una fecha valida">
            </asp:CompareValidator>
        </td>
        <td style="width: 20%; vertical-align:top; padding-top:7px;">
            <asp:Button ID="btAplicarFiltro" runat="server" 
                Text="Filtrar" 
                OnClientClick="return ValidaFiltro();" 
                OnClick="btAplicarFiltro_Click" 
                ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Filtrar.png"/>
        </td>

    </tr>
</table>
