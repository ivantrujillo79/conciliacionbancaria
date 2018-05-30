<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="ReporteSaldoAnticiposxCliente.aspx.cs" Inherits="ReportesConciliacion_ReporteSaldoAnticiposxCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
    Reporte Saldo de Anticipos de Cliente
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function pageLoad() {
            
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
        function openWindow() {
            window.open('http://www.google.com/', '_blank');
        }
    </script>
    <input type="button" value="Click Me" onclick="openWindow()" />
    <asp:UpdatePanel runat="server" ID="upConciliacionCompartida" UpdateMode="Always">
        <ContentTemplate>
            <table id="BarraHerramientas" class="bg-color-grisClaro01" style="width: 100%; vertical-align: top">
                <tr>
                    <td style="padding: 3px 3px 3px 0px; vertical-align: top; width: 70%">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion  bg-color-azulClaro" rowspan="2">
                                    <asp:ImageButton ID="btnActualizarConfig" runat="server" Height="25px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/ActualizarConfig.png"
                                        ToolTip="ACTUALIZAR CONFIGURACION" Width="25px" ValidationGroup="Configuracion" OnClick="btnActualizarConfig_Click" />
                                </td>
                                <td class="lineaVertical" style="width: 25%">
                                    Empresa
                                </td>
                                <td class="lineaVertical" style="width: 20%">
                                    Sucursal
                                </td>
                                <td class="lineaVertical" style="width: 20%">
                                    Banco
                                </td>
                                <td class="lineaVertical" style="width: 15%">
                                    Cuenta Bancaria
                                </td>
                                <td class="lineaVertical" style="width: 10%">
                                    Fecha Inicial
                                </td>
                                <td style="width: 10%">
                                    Fecha Final
                                </td>
                            </tr>
                            <tr>
                                <td class="lineaVertical" style="width: 25%">
                                    <asp:DropDownList ID="DropDownList1" runat="server" Width="100%" CssClass="dropDown" AutoPostBack="True"></asp:DropDownList>
                                </td>
                                <td class="lineaVertical" style="width: 20%">
                                    <asp:DropDownList ID="DropDownList2" runat="server" Width="100%" CssClass="dropDown" AutoPostBack="True"></asp:DropDownList>
                                </td>
                                <td class="lineaVertical" style="width: 20%">
                                    <asp:DropDownList ID="DropDownList3" runat="server" Width="100%" CssClass="dropDown" AutoPostBack="True"></asp:DropDownList>
                                </td>
                                <td class="lineaVertical" style="width: 15%">
                                    <asp:DropDownList ID="DropDownList4" runat="server" Width="100%" CssClass="dropDown" AutoPostBack="True"></asp:DropDownList>
                                </td>
                                <td class="lineaVertical" style="width: 10%">
                                    <asp:TextBox runat="server" ID="txtFInicio" CssClass="cajaTexto" Font-Size="10px" Width="85%"></asp:TextBox>
                                </td>
                                <td style="width: 10%">
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="cajaTexto" Font-Size="10px" Width="85%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 1%; padding: 3px 3px 3px 0px; vertical-align: top">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion bg-color-purpura" style="height: 30px" rowspan="2">
                                    <asp:ImageButton ID="ImageButton1" runat="server" Height="25px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Filtrar.png"
                                        ToolTip="FILTRAR" Width="25px" Style="height: 30px" />
                                </td>
                                <td>
                                    Filtrar en
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlFiltrarEn" runat="server" CssClass="etiqueta dropDownPequeño"
                                        Style="margin-bottom: 3px; margin-right: 3px" Width="85px">
                                        <asp:ListItem Value="Externos" Selected="True">Externos</asp:ListItem>
                                        <asp:ListItem Value="Internos">Internos</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="padding: 3px 3px 3px 0px; vertical-align: top; width: 1%">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion bg-color-naranja" style="height: 30px">
                                    <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Buscar.png"
                                        ToolTip="BUSCAR" Width="25px"/>
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
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion bg-color-azulOscuro" style="height: 30px">
                                    <asp:ImageButton ID="imgGuardar" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Guardar.png"
                                        ToolTip="GUARDAR VISTA" Width="25px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 1%; padding: 3px 3px 3px 0px; vertical-align: top">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion bg-color-grisOscuro" style="height: 30px">
                                    <asp:ImageButton ID="imgCerrarMesConciliacion" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png"
                                        ToolTip="CERRAR MES" Width="25px" />
                                </td>
                            </tr>
                        </table>
                    </td>

                </tr>
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

