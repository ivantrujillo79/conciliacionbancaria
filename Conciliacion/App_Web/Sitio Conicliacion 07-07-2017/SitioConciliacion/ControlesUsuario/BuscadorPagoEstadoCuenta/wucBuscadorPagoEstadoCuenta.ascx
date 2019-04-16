<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucBuscadorPagoEstadoCuenta.ascx.cs" Inherits="ControlesUsuario_ClientePago_wucBuscadorPagoEstadoCuenta" %>
    
    <!-- Script se utiliza para el Scroll del GridView-->
    <link href="../../App_Scripts/ScrollGridView/GridviewScroll.css" rel="stylesheet"type="text/css" />
    <script src="../../App_Scripts/ScrollGridView/gridviewScroll.min.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        function CP_gridviewScroll() {
            $('#<%=grvPagoEstadoCuenta.ClientID%>').gridviewScroll({
                width: 595,
                height: 180,
                freezesize: 3,
                arrowsize: 30,
                varrowtopimg: '../../App_Scripts/ScrollGridView/Images/arrowvt.png',
                varrowbottomimg: '../../App_Scripts/ScrollGridView/Images/arrowvb.png',
                harrowleftimg: '../../App_Scripts/ScrollGridView/Images/arrowhl.png',
                harrowrightimg: '../../App_Scripts/ScrollGridView/Images/arrowhr.png',
                headerrowcount: 1
            });
        }

        function activarDatePickers() {
            $("#<%= txtFinicio.ClientID%>").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                onClose: function (selectedDate) {
                    $("#<%= txtFfinal.ClientID%>").datepicker("option", "minDate", selectedDate);
                }
            });
            $("#<%= txtFfinal.ClientID%>").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                onClose: function (selectedDate) {
                    $("#<%= txtFinicio.ClientID%>").datepicker("option", "maxDate", selectedDate);
                }
            });
        }

    </script>

    <table style="width: 100%">
        <tr>
            <td style="width: 1%; padding-left:3px"> 
                <asp:CheckBox ID="chkBuscarEnEsta" Text="Buscar en esta conciliacion" runat="server" />
            </td>
            <td style="width: 1%;"> 
                <asp:Label ID="Label2" runat="server" Text="Monto" CssClass="etiqueta fg-color-negro centradoMedio"></asp:Label>
                <asp:TextBox ID="txtMonto" runat="server" Width="150px" CssClass="cajaTextoPequeño"></asp:TextBox>
            </td>
            <td style="width: 1%;"> 
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Width="150px" OnClick="btnBuscar_Click" />
            </td>
            <td style="width: 1%; padding-left:3px">
            </td>
            <td style="width: 1%;">
            </td>
        </tr>
        <tr>
            <td style="width: 1%; padding-left:3px"> 
                <asp:CheckBox ID="chkBuscaEnRetiros" Text="Buscar en depositos" runat="server" />
            </td>
            <td style="width: 1%;"> 
                <asp:Label ID="Label1" runat="server" Text="F.Inicio" CssClass="etiqueta fg-color-negro centradoMedio"></asp:Label>
                <asp:TextBox ID="txtFinicio" runat="server" Width="150px" CssClass="cajaTextoPequeño"></asp:TextBox>
            </td>
            <td style="width: 1%;"> 
            </td>
            <td style="width: 1%; padding-left:3px">
            </td>
            <td style="width: 1%;">
            </td>
        </tr>
        <tr>
            <td style="width: 1%; padding-left:3px"> 
                <asp:CheckBox ID="chkBuscarEnDepositos" Text="Buscar en depositos" runat="server" />
            </td>
            <td style="width: 1%;"> 
                <asp:Label ID="Label3" runat="server" Text="F. Final" CssClass="etiqueta fg-color-negro centradoMedio"></asp:Label>
                <asp:TextBox ID="txtFfinal" runat="server" Width="150px" CssClass="cajaTextoPequeño"></asp:TextBox>
            </td>
            <td style="width: 1%;"> 
            </td>
            <td style="width: 1%; padding-left:3px">
            </td>
            <td style="width: 1%;">
            </td>
        </tr>
    </table>

    <table style="width:100%">
        <tr>
            <td class="etiqueta centradoMedio" style="width: 100%;">

                <div class="etiqueta centradoMedio" style="height:170px;overflow:auto;"> <!--width:800px-->
                    <asp:GridView ID="grvPagoEstadoCuenta" runat="server" 
                        ShowHeader="True"
                        AllowSorting="True" 
                        CssClass="grvResultadoConsultaCss" 
                        ShowFooter="False" 
                        Width="100%"
                        ShowHeaderWhenEmpty="True" 
                        AllowPaging="False"
                        PageSize="5">

                        <HeaderStyle HorizontalAlign="Center" />
                        <Columns>
                        </Columns>
                        <PagerStyle CssClass="grvPaginacionScroll" />
                    </asp:GridView>
                </div>

                <asp:Button ID="btnAceptar" runat="server"
                    CssClass="boton bg-color-azulOscuro fg-color-blanco"
                    Text="ACEPTAR" Style="margin: 0 0 0 0;" ToolTip="GUARDAR" OnClick="btnAceptar_Click"/>
                
            </td>
        </tr>
    </table>
