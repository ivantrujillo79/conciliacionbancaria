<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="ReporteSaldoAnticiposxCliente.aspx.cs" Inherits="ReportesConciliacion_ReporteSaldoAnticiposxCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
    Reporte Saldo de Anticipos de Cliente
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!--Libreria jQuery-->
    <script src="../App_Scripts/jQueryScripts/jquery-3.2.1.min.js" type="text/javascript"></script>
    <script src="../App_Scripts/jQueryScripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../App_Scripts/jQueryScripts/jquery.ui.datepicker-es.js" type="text/javascript"></script>
    <link href="../App_Scripts/jQueryScripts/css/custom-theme/jquery-ui-1.10.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script src="../../App_Scripts/Common.js" type="text/javascript"></script>
    <script type="text/javascript">

        function pageLoad() {
            activarDatePickers();
        }
        function activarDatePickers() {
            //DatePicker FOperacion
            $("#<%= txtFInicio.ClientID%>").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                onClose: function (selectedDate) {
                    $("#<%=txtFFInal.ClientID%>").datepicker("option", "minDate", selectedDate);
                }
            });
            $("#<%=txtFFInal.ClientID%>").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                onClose: function (selectedDate) {
                    $("#<%=txtFInicio.ClientID%>").datepicker("option", "maxDate", selectedDate);
                }
            });
        }

        function ValidNum(e) {
            var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
            return ((tecla > 47 && tecla < 58));
        }

    </script>        
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">
    <asp:ScriptManager runat="server" ID="smDetalleConciliacion" AsyncPostBackTimeout="600"
        EnableScriptGlobalization="True">
    </asp:ScriptManager>

    <!--      Script animación de carga      -->
    <script src="../App_Scripts/jsUpdateProgress.js" type="text/javascript"></script>    

<%--    <script type="text/javascript" language="javascript">
        var ModalProgress = '<%=mpeLoading.ClientID%>';        
    </script>--%>
    <script type="text/javascript" language="javascript">
        
    </script>

    <asp:UpdatePanel runat="server" ID="upConciliacionCompartida" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdfCliente" runat="server" />
            <asp:HiddenField ID="hdfTodos" runat="server" />
            <asp:HiddenField ID="hdfFechaIni" runat="server" />
            <asp:HiddenField ID="hdfFechaFin" runat="server" />

            <table id="BarraHerramientas" class="bg-color-grisClaro01" style="width: 100%; vertical-align: top">
                <tr>
                    <td style="padding: 3px 3px 3px 0px; vertical-align: top; width: 70%">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion  bg-color-azulClaro" rowspan="2">
                                    <%--<asp:ImageButton ID="btnActualizarConfig" runat="server" Height="25px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/ActualizarConfig.png"
                                        ToolTip="ACTUALIZAR CONFIGURACION" Width="25px" ValidationGroup="Configuracion" OnClick="btnActualizarConfig_Click" />--%>
                                </td>
                                <td class="lineaVertical" style="width: 15%">
                                    Cliente
                                </td>
                                <td class="lineaVertical" style="width: 10%">
                                    Clientes con Saldo
                                </td>
                                <td class="lineaVertical" style="width: 15%">
                                    Fecha Inicial
                                </td>
                                <td style="width: 15%">
                                    Fecha Final
                                </td>
                                <td class="lineaVertical" style="width: 45%">
                                </td>
                            </tr>
                            <tr>
                                <td class="lineaVertical" style="width: 15%">
                                    <asp:TextBox ID="txtClienteID" runat="server" onkeypress="return ValidNum(event)" CssClass="cajaTexto" Font-Size="10px" Width="90%"></asp:TextBox>
                                </td>
                                <td class="lineaVertical" style="width: 10%">
                                    <asp:DropDownList ID="ddlClientesConSaldo" runat="server" Width="90%" CssClass="etiqueta dropDownPequeño" AutoPostBack="True">
                                        <asp:ListItem Selected="True" Value="SI">SI</asp:ListItem>
                                        <asp:ListItem Value="NO">NO</asp:ListItem>
                                        <asp:ListItem Value="TODOS">TODOS</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="lineaVertical" style="width: 15%">
                                    <asp:TextBox ID="txtFInicio" runat="server" CssClass="cajaTexto" Font-Size="10px" Width="90%"></asp:TextBox>
                                </td>
                                <td style="width: 15%">
                                    <asp:TextBox ID="txtFFInal" runat="server" CssClass="cajaTexto" Font-Size="10px" Width="90%"></asp:TextBox>
                                </td>
                                <td class="lineaVertical" style="width: 45%">
                                </td>
                            </tr>
                        </table>
                    </td>
                    
                    <td style="width: 1%; padding: 3px 3px 3px 0px; vertical-align: top">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td id="tdExportar" runat="server" class="iconoOpcion bg-color-rojo" style="height: 30px">
                                    <asp:ImageButton ID="imgExportar" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Exportar.png"
                                        ToolTip="EXPORTAR RESULTADOS" Width="25px" OnClick="imgExportar_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    
                    <td style="width: 1%; padding: 3px 3px 3px 0px; vertical-align: top">
                        <table class="bg-color-grisClaro01">
                            <tr>
                                <td class="iconoOpcion bg-color-grisOscuro" style="height: 30px">
                                   <%-- <asp:ImageButton ID="imgCerrarMesConciliacion" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png"
                                        ToolTip="CERRAR MES" Width="25px" />--%>
                                </td>
                            </tr>
                        </table>
                    </td>

                </tr>
            </table>

            <table style="width: 100%">
	            <tbody>
		            <tr>
                        <td style="vertical-align: middle; padding: 5px 5px 5px 5px" class="etiqueta centradoJustificado fg-color-blanco bg-color-azulClaro">
                            Saldos de Anticipos de Clientes
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td colspan="2">
                            <div style="width:1200px; height:450px; overflow:auto;">
				    <div>
				    </div>
                            </div>
                        </td>
                    </tr>
	            </tbody>
            </table>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdateProgress ID="panelBloqueo" runat="server" AssociatedUpdatePanelID="upConciliacionCompartida">
        <ProgressTemplate>
            <asp:Image ID="imgLoad" runat="server" CssClass="icono bg-color-blanco" Height="40px"
                ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/LoadPage.gif" Width="40px" />
        </ProgressTemplate>
    </asp:UpdateProgress>

<%--    <ajaxToolkit:ModalPopupExtender ID="mpeLoading" runat="server" BackgroundCssClass="ModalBackground"
        PopupControlID="panelBloqueo" TargetControlID="panelBloqueo">
    </ajaxToolkit:ModalPopupExtender>--%>

</asp:Content>

