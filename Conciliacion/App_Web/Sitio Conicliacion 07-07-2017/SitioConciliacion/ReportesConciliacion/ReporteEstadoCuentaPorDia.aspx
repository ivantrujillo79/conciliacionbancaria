<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="ReporteEstadoCuentaPorDia.aspx.cs" 
    Inherits="ReportesConciliacion_ReporteEstadoCuentaPorDia" %>

<%@ Register Src="~/ControlesUsuario/wuCuentasBancarias/WUCListadoCuentasBancarias.ascx" TagName="WUCCuentasBancarias"
    TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    
    <!-- #region Librerias -->

    <!-- Libreria jQuery-->
    <script src="../App_Scripts/jQueryScripts/jquery-3.2.1.min.js" type="text/javascript"></script>
    <script src="../App_Scripts/jQueryScripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../App_Scripts/jQueryScripts/jquery.ui.datepicker-es.js" type="text/javascript"></script>
    <link href="../App_Scripts/jQueryScripts/css/custom-theme/jquery-ui-1.10.2.custom.min.css" rel="stylesheet" type="text/css" />

    <!-- #endregion -->

    <script type="text/javascript">
        //function pageLoad() {
        //    console.log('pageLoad');
        //    ActivarDatePickers();
        //}

        $(document).ready(function () {
            ActivarDatePickers();
        });

        function ActivarDatePickers() {
            $("#<%= txtFechaInicial.ClientID%>").datepicker({
                changeMonth: true,
                changeYear: true,
                onClose: function (selectedDate) {
                    $("#<%=txtFechaFinal.ClientID%>").datepicker("option", "minDate", selectedDate);
                    $("#<%=txtFechaFinal.ClientID%>").datepicker("option", "defaultDate", selectedDate);
                }
            });
            $("#<%=txtFechaFinal.ClientID%>").datepicker({
                changeMonth: true,
                changeYear: true,
                onClose: function (selectedDate) {
                    $("#<%=txtFechaInicial.ClientID%>").datepicker("option", "maxDate", selectedDate);
                    $("#<%=txtFechaInicial.ClientID%>").datepicker("option", "defaultDate", selectedDate);
                }
            });
        }
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">

    <table width="100%">
        <tr>
            <td style="padding: 5px;">
                <asp:Label ID="lblTitulo" runat="server" Text="Estado de cuenta por d&iacute;a" />
            </td>
        </tr>
        <tr>
            <td>
                <div>
                    <table width="100%">
                        <tr>
                            <!--        Etiquetas       -->
                            <td style="vertical-align:top;">
                                <div class="etiqueta centradoDerecha">
                                    <table width="100%" >
                                        <tr>
                                            <td style="padding:5px;height:45px">
                                                <asp:Label runat="server" Text="Fecha inicial:"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding:5px;height:45px">
                                                <asp:Label runat="server" Text="Fecha final:"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding:5px;height:45px">
                                                <asp:Label runat="server" Text="Banco:"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding:5px;height:45px">
                                                <asp:Label runat="server" Text="Cuenta bancaria:"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <!--        Controles       -->
                            <td style="vertical-align:top" >
                                <div class="etiqueta centradoIzquierda">
                                    <table width="100%">
                                        <tr>
                                            <td style="padding:5px;height:45px">
                                                <asp:TextBox ID="txtFechaInicial" runat="server" CssClass="cajaTexto" Font-Size="11px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding:5px;height:45px">
                                                <asp:TextBox ID="txtFechaFinal" runat="server" CssClass="cajaTexto" Font-Size="11px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding:5px;height:45px">
                                                <asp:DropDownList ID="ddlBanco" runat="server" Width="200px" CssClass="dropDown" Font-Size="11px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left:1px; padding-top:5px">
                                                <div style="width:200px">
                                                    <uc1:WUCCuentasBancarias ID="WUCCuentasBancarias" runat="server"/>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding:5px;" class="centradoMedio">
                                <asp:Button ID="btnConsultar" Text="CONSULTAR" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                    runat="server"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>

</asp:Content>

