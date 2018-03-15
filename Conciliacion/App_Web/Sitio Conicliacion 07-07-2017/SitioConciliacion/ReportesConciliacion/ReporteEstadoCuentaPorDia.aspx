<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="ReporteEstadoCuentaPorDia.aspx.cs" 
    Inherits="ReportesConciliacion_ReporteEstadoCuentaPorDia" %>

<%@ Register Src="~/ControlesUsuario/wuCuentasBancarias/WUCListadoCuentasBancarias.ascx" TagName="WUCCuentasBancarias"
    TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
    Estado de cuenta por d&iacute;a
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
            <td style="padding:20px 5px 10px 7px;vertical-align:top; width:25%">
                <div >
                    <!-- Imagen -->
                    <table width="100%">
                        <tr>
                            <td class="centradoMedio" align="center">
                                <asp:Image runat="server" ID="imgStatusConciliacion" width="280px"
                                    ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/bkgWhite.png" style=""/>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td>
                <div style="width:100%">
                    <!-- Formulario -->
                    <table width="75%">
                        <tr>
                            <td>
                                <div runat="server" ID="dvAlertaError" class="alert alert-danger alert-dismissible fade show" Visible="false"
                                    style="margin:5px 5px 0px 7px; box-sizing:border-box; font-size:15px">
                                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                    <strong>Error: </strong>
                                    <asp:Label runat="server" ID="lblMensajeError" Text="Debe especificar una fecha inicial y final 
                                        y las fechas deben corresponder al mismo mes y año, por favor corrija su entrada." />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <table width="100%">
                                        <tr>
                                            <!--        Etiquetas       -->
                                            <td style="vertical-align:top; width:200px;">
                                                <div class="etiqueta centradoDerecha">
                                                    <table width="100%" >
                                                        <tr>
                                                            <td style="padding:5px;height:45px">
                                                                <asp:Label runat="server" Text="Fecha inicial:" Font-Size="12px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding:5px;height:45px">
                                                                <asp:Label runat="server" Text="Fecha final:" Font-Size="12px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding:5px;height:45px">
                                                                <asp:Label runat="server" Text="Banco:" Font-Size="12px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding:5px;height:45px">
                                                                <asp:Label runat="server" Text="Cuenta bancaria:" Font-Size="12px"></asp:Label>
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
                                                                <asp:TextBox ID="txtFechaInicial" runat="server" CssClass="cajaTexto" Font-Size="12px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding:5px;height:45px">
                                                                <asp:TextBox ID="txtFechaFinal" runat="server" CssClass="cajaTexto" Font-Size="12px" />
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
                                                    runat="server" OnClick="btnConsultar_Click"/>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>

</asp:Content>

