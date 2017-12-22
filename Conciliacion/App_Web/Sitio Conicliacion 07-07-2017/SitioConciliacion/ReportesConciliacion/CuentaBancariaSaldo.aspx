<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true"
    CodeFile="CuentaBancariaSaldo.aspx.cs" Inherits="ReportesConciliacion_CuentaBancariaSaldo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    CUENTA BANCARIA SALDO
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <!--Libreria jQuery-->
    <script src="../App_Scripts/jQueryScripts/jquery-3.2.1.min.js" type="text/javascript"></script>
    <script src="../App_Scripts/jQueryScripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../App_Scripts/jQueryScripts/jquery.ui.datepicker-es.js" type="text/javascript"></script>
    <link href="../App_Scripts/jQueryScripts/css/custom-theme/jquery-ui-1.10.2.custom.min.css"
        rel="stylesheet" type="text/css" />
    <!-- Script se utiliza para el Scroll del GridView-->
    <link href="../App_Scripts/ScrollGridView/GridviewScroll.css" rel="stylesheet" type="text/css" />
    <script src="../App_Scripts/ScrollGridView/gridviewScroll.min.js" type="text/javascript"></script>
    <script src="../App_Scripts/Common.js" type="text/javascript"></script>
    <script type="text/javascript">

        function pageLoad() {
            var gridviewID = "<%=grvCuentaBancoSaldoFinalDia.ClientID%>";
            gridviewScroll();
            activarDatePickers();
            gridview = $('#' + gridviewID);
        }

        function activarDatePickers() {
            //DatePicker FOperacion
            $("#<%= txtFConsulta.ClientID%>").datepicker({
                changeMonth: true,
                changeYear: true
            });
        }
        function gridviewScroll() {
            $('#<%=grvCuentaBancoSaldoFinalDia.ClientID%>').gridviewScroll({
                width: 1200,
                height: 500,
                freezesize: 5,
                arrowsize: 30,
                varrowtopimg: '../App_Scripts/ScrollGridView/Images/arrowvt.png',
                varrowbottomimg: '../App_Scripts/ScrollGridView/Images/arrowvb.png',
                harrowleftimg: '../App_Scripts/ScrollGridView/Images/arrowhl.png',
                harrowrightimg: '../App_Scripts/ScrollGridView/Images/arrowhr.png',
                headerrowcount: 1,
                startVertical: $("#<%=hfCompartidaSV.ClientID%>").val(),
                startHorizontal: $("#<%=hfCompartidaSH.ClientID%>").val(),
                onScrollVertical: function (delta) { $("#<%=hfCompartidaSV.ClientID%>").val(delta); },
                onScrollHorizontal: function (delta) { $("#<%=hfCompartidaSH.ClientID%>").val(delta); }
            });

        }

    </script>
    <script src="../App_Scripts/jsHoverGridView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager runat="server" ID="smDetalleConciliacion" AsyncPostBackTimeout="600"
        EnableScriptGlobalization="True">
    </asp:ScriptManager>
    <script src="../App_Scripts/jsUpdateProgress.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        function ShowModalPopup() {
            $find("ModalBehaviour").show();
        }

        function HideModalPopup() {
            $find("ModalBehaviour").hide();
        }
    </script>
    <script type="text/javascript" language="javascript">
        var ModalProgress = '<%=mpeLoading.ClientID%>';        
    </script>
    <asp:UpdatePanel runat="server" ID="upCuentaBancoSaldo" UpdateMode="Always">
         <Triggers>
            <asp:PostBackTrigger ControlID="imgExportar" />
        </Triggers>
        <ContentTemplate>
            <table id="BarraHerramientas" class="bg-color-grisClaro01" style="width: 100%; vertical-align: top">
                <tr>
                    <td style="padding: 3px 3px 3px 0px; vertical-align: top; width: 70%">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion  bg-color-azulClaro" rowspan="2">
                                    <asp:ImageButton ID="btnActualizarConfig" runat="server" Height="25px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/ActualizarConfig.png"
                                        ToolTip="ACTUALIZAR CONFIGURACION" Width="25px" ValidationGroup="Configuracion"
                                        OnClick="btnActualizarConfig_Click" />
                                </td>
                                <td class="lineaVertical" style="width: 25%">
                                    Empresa
                                    <asp:HiddenField ID="hdfCorporativo" runat="server" />
                                </td>
                                <td class="lineaVertical" style="width: 20%">
                                    Sucursal
                                    <asp:HiddenField ID="hdfSucursal" runat="server" />
                                </td>
                                <td class="lineaVertical" style="width: 20%">
                                    Banco
                                </td>
                                <td class="lineaVertical" style="width: 20%">
                                    Cuenta Bancaria
                                    <asp:HiddenField ID="hdfCuentaBancaria" runat="server" />
                                </td>
                                <td class="lineaVertical" style="width: 15%">
                                    Fecha Consulta
                                    <asp:HiddenField ID="hdfConsulta" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="lineaVertical" style="width: 25%">
                                    <asp:DropDownList ID="ddlEmpresa" runat="server" Width="100%" SkinID="DropDownList"
                                        CssClass="dropDown" AutoPostBack="True" OnDataBound="ddlEmpresa_DataBound" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:RequiredFieldValidator ID="rfvEmpresa" runat="server" ErrorMessage=" Especifique la empresa. "
                                        ControlToValidate="ddlEmpresa" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                        ValidationGroup="Configuracion" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                                <td class="lineaVertical" style="width: 20%">
                                    <asp:DropDownList ID="ddlSucursal" runat="server" Width="100%" CssClass="dropDown"
                                        AutoPostBack="False" OnDataBound="ddlSucursal_DataBound">
                                    </asp:DropDownList>
                                </td>
                                <td class="lineaVertical" style="width: 20%">
                                    <asp:DropDownList ID="ddlBanco" runat="server" Width="100%" CssClass="dropDown" AutoPostBack="True"
                                        OnDataBound="ddlBanco_DataBound" OnSelectedIndexChanged="ddlBanco_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="lineaVertical" style="width: 20%">
                                    <asp:DropDownList ID="ddlCuentaBancaria" runat="server" Width="100%" CssClass="dropDown"
                                        AutoPostBack="False" OnDataBound="ddlCuentaBancaria_DataBound">
                                    </asp:DropDownList>
                                </td>
                                <td class="lineaVertical" style="width: 15%">
                                    <asp:TextBox runat="server" ID="txtFConsulta" CssClass="cajaTexto" Font-Size="10px"
                                        Width="85%"></asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator ID="rfvFechaConsulta" runat="server" ErrorMessage=" Especifique la fecha de Consulta "
                                        ControlToValidate="txtFConsulta" Font-Size="10px" CssClass="etiqueta fg-color-amarillo"
                                        ValidationGroup="Configuracion" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="padding: 3px 3px 3px 0px; vertical-align: top; width: 1%">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion bg-color-naranja" style="height: 30px">
                                    <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Buscar.png"
                                        ToolTip="BUSCAR" Width="25px" OnClick="imgBuscar_Click" />
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
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="vertical-align: middle; padding: 5px 5px 5px 5px" class="etiqueta centradoJustificado fg-color-blanco bg-color-azulClaro">
                        SALDO FINAL POR DIA
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td colspan="2">
                        <asp:HiddenField ID="hfCompartidaSV" runat="server" />
                        <asp:HiddenField ID="hfCompartidaSH" runat="server" />
                        <asp:GridView ID="grvCuentaBancoSaldoFinalDia" runat="server" AutoGenerateColumns="False"
                            Width="100%" AllowPaging="False" ShowHeaderWhenEmpty="True" CssClass="grvResultadoConsultaCss"
                            DataKeyNames="Corporativo,Sucursal,Banco,CuentaBancaria" PageSize="12" OnRowDataBound="grvCuentaBancoSaldoFinalDia_RowDataBound"
                            AllowSorting="True" OnSorting="grvCuentaBancoSaldoFinalDia_Sorting" ShowFooter="True">
                            <%--<EmptyDataTemplate>
                                <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="No existe ninguna conciliación de acuerdo a los Parámetros del Filtrado. "></asp:Label>
                            </EmptyDataTemplate>--%>
                            <HeaderStyle HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField HeaderText="Banco" SortExpression="Banco">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBanco" runat="server" Text='<%# Eval("BancoDes") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="centradoMedio"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center" Font-Size="13px" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="False" Font-Size="13px" BackColor="#ebecec"
                                        CssClass="centradoMedio" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cuenta Banc." SortExpression="CuentaBancaria">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCuentaBancaria" runat="server" Text='<%# Eval("CuentaBancaria") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="centradoMedio"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center" Font-Size="13px" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="True" Font-Size="13px" BackColor="#ebecec"
                                        CssClass="centradoMedio" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Saldo Inicial Mes" SortExpression="SaldoInicialMes">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaldoInicialMes" runat="server" Text='<%# Eval("SaldoInicialMes", "{0:c2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalDia" runat="server" Text=""></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Size="13px" />
                                    <ItemStyle HorizontalAlign="Center" Font-Size="13px" Wrap="False" />
                                    <FooterStyle CssClass="centradoMedio bg-color-grisOscuro fg-color-blanco" Font-Size="14px"></FooterStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Saldo Final Dia" SortExpression="SaldoFinalDia">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaldoFinalDia" runat="server" Text='<%# Eval("SaldoFinalDia","{0:c2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalSaldoFinalDia" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Size="13px" />
                                    <ItemStyle HorizontalAlign="Center" Font-Size="13px" Wrap="False" />
                                    <FooterStyle CssClass="centradoMedio bg-color-grisOscuro fg-color-blanco" Font-Size="14px"></FooterStyle>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="estiloPaginacion bg-color-grisOscuro fg-color-blanco" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="panelBloqueo" runat="server" AssociatedUpdatePanelID="upCuentaBancoSaldo">
        <ProgressTemplate>
            <asp:Image ID="imgLoad" runat="server" CssClass="icono bg-color-blanco" Height="40px"
                ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/LoadPage.gif" Width="40px" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ModalPopupExtender ID="mpeLoading" runat="server" BackgroundCssClass="ModalBackground"
        PopupControlID="panelBloqueo" TargetControlID="panelBloqueo">
    </asp:ModalPopupExtender>
</asp:Content>
