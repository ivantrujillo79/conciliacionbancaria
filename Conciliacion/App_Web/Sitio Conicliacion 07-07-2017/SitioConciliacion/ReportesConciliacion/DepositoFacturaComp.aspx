<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true"
    CodeFile="DepositoFacturaComp.aspx.cs" Inherits="ReportesConciliacion_CuentaBancariaSaldo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    CUENTA BANCARIA SALDO
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <!--Libreria jQuery-->
    <script src="../App_Scripts/jQueryScripts/jquery.min.js" type="text/javascript"></script>
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

        function oncheckedbox() {
            if (document.getElementById('chkFConciliacion').checked) {
                document.getElementById('chkFDeposito').checked = false;
                document.getElementById('chkFTimbrado').checked = false;
            }
            if (document.getElementById('chkFDeposito').checked) {
                document.getElementById('chkFConciliacion').checked = false;
                document.getElementById('chkFTimbrado').checked = false;
            }
            if (document.getElementById('chkFTimbrado').checked) {
                document.getElementById('chkFConciliacion').checked = false;
                document.getElementById('chkFDeposito').checked = false;
            }
        }

        function activarDatePickers() {
            //DatePicker FOperacion
            $("#<%= txtFConsultaIni.ClientID%>").datepicker({
                changeMonth: true,
                changeYear: true
            });
            $("#<%= txtFConsultaFin.ClientID%>").datepicker({
                changeMonth: true,
                changeYear: true
            });
            $("#<%= txtFDepositoIni.ClientID%>").datepicker({
                changeMonth: true,
                changeYear: true
            });
            $("#<%= txtFDepositoFin.ClientID%>").datepicker({
                changeMonth: true,
                changeYear: true
            });
            $("#<%= txtFTimbradoIni.ClientID%>").datepicker({
                changeMonth: true,
                changeYear: true
            });
            $("#<%= txtFTimbradoFin.ClientID%>").datepicker({
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
                                </td>
<%--                                <td class="lineaVertical" style="width: 15%">
                                    Empresa
                                    <asp:HiddenField ID="hdfCorporativo" runat="server" />
                                </td>
                                <td class="lineaVertical" style="width: 15%">
                                    Sucursal
                                    <asp:HiddenField ID="hdfSucursal" runat="server" />
                                </td>
                                <td class="lineaVertical" style="width: 15%">
                                    Banco
                                </td>
                                <td class="lineaVertical" style="width: 15%">
                                    Cuenta Bancaria
                                    <asp:HiddenField ID="hdfCuentaBancaria" runat="server" />
                                </td>--%>
                                <td style="width: 20%">
                                    <asp:CheckBox ID="chkFConciliacion" Text="Periodo Conciliacion" runat="server" OnClientClick="javascript:return oncheckedbox();"/>
                                    <asp:HiddenField ID="hdfConciliacionIni" runat="server" />
                                    <asp:HiddenField ID="hdfConciliacionFin" runat="server" />
                                </td>
                                <td class="lineaVertical" style="width: 20%">
                                    
                                </td>
                                <td style="width: 15%">
                                    <asp:CheckBox ID="chkFDeposito" Text="Fecha Deposito" runat="server" />
                                    <asp:HiddenField ID="hdfDeposito" runat="server" />
                                </td>
                                <td class="lineaVertical" style="width: 15%">
                                    
                                </td>
                                <td style="width: 15%">
                                    <asp:CheckBox ID="chkFTimbrado" Text="Fecha Timbrado" runat="server" />
                                    <asp:HiddenField ID="hdfTibrado" runat="server" />
                                </td>
                                <td class="lineaVertical" style="width: 15%">
                                    
                                </td>
                            </tr>
                            <tr>
<%--                                
                            <td class="lineaVertical" style="width: 15%">
                                    <asp:DropDownList ID="ddlEmpresa" runat="server" Width="100%" SkinID="DropDownList"
                                        CssClass="dropDown" AutoPostBack="True" OnDataBound="ddlEmpresa_DataBound" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:RequiredFieldValidator ID="rfvEmpresa" runat="server" ErrorMessage=" Especifique la empresa. "
                                        ControlToValidate="ddlEmpresa" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                        ValidationGroup="Configuracion" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                                <td class="lineaVertical" style="width: 15%">
                                    <asp:DropDownList ID="ddlSucursal" runat="server" Width="100%" CssClass="dropDown"
                                        AutoPostBack="False" OnDataBound="ddlSucursal_DataBound">
                                    </asp:DropDownList>
                                </td>
                                <td class="lineaVertical" style="width: 15%">
                                    <asp:DropDownList ID="ddlBanco" runat="server" Width="100%" CssClass="dropDown" AutoPostBack="True"
                                        OnDataBound="ddlBanco_DataBound" OnSelectedIndexChanged="ddlBanco_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="lineaVertical" style="width: 15%">
                                    <asp:DropDownList ID="ddlCuentaBancaria" runat="server" Width="100%" CssClass="dropDown"
                                        AutoPostBack="False" OnDataBound="ddlCuentaBancaria_DataBound">
                                    </asp:DropDownList>
                                </td>
--%>
                                <td class="lineaVertical" style="width: 20%">
                                    <asp:TextBox runat="server" ID="txtFConsultaIni" CssClass="cajaTexto" Font-Size="10px"
                                        Width="100%"></asp:TextBox>
                                    <br />
                                </td>
                                <td class="lineaVertical" style="width: 20%">
                                    <asp:TextBox runat="server" ID="txtFConsultaFin" CssClass="cajaTexto" Font-Size="10px"
                                        Width="100%"></asp:TextBox>
                                    <br />
                                </td>
                                <td class="lineaVertical" style="width: 15%">
                                    <asp:TextBox runat="server" ID="txtFDepositoIni" CssClass="cajaTexto" Font-Size="10px"
                                        Width="100%"></asp:TextBox>
                                    <br />
                                </td>
                                <td class="lineaVertical" style="width: 15%">
                                    <asp:TextBox runat="server" ID="txtFDepositoFin" CssClass="cajaTexto" Font-Size="10px"
                                        Width="100%"></asp:TextBox>
                                    <br />
                                </td>
                                <td class="lineaVertical" style="width: 15%">
                                    <asp:TextBox runat="server" ID="txtFTimbradoIni" CssClass="cajaTexto" Font-Size="10px"
                                        Width="100%"></asp:TextBox>
                                    <br />
                                </td>
                                <td class="lineaVertical" style="width: 15%">
                                    <asp:TextBox runat="server" ID="txtFTimbradoFin" CssClass="cajaTexto" Font-Size="10px"
                                        Width="100%"></asp:TextBox>
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="padding: 3px 3px 3px 0px; vertical-align: top; width: 1%">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion bg-color-naranja" style="height: 30px">
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
                        <asp:GridView ID="grvCuentaBancoSaldoFinalDia" runat="server" AutoGenerateColumns="False" Visible="false"
                            Width="100%" AllowPaging="False" ShowHeaderWhenEmpty="True" CssClass="grvResultadoConsultaCss"
                            DataKeyNames="CuentaBancoFinanciero" PageSize="12" OnRowDataBound="grvCuentaBancoSaldoFinalDia_RowDataBound"
                            AllowSorting="True" OnSorting="grvCuentaBancoSaldoFinalDia_Sorting" ShowFooter="True">
                            <%--<EmptyDataTemplate>
                                <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="No existe ninguna conciliación de acuerdo a los Parámetros del Filtrado. "></asp:Label>
                            </EmptyDataTemplate>--%>
                            <HeaderStyle HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField HeaderText="Banco" SortExpression="Banco">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBanco" runat="server" Text='<%# Eval("cuentabancofinanciero") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="centradoMedio"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center" Font-Size="13px" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="False" Font-Size="13px" BackColor="#ebecec"
                                        CssClass="centradoMedio" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cuenta Banc." SortExpression="cuentabanco">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCuentaBancaria" runat="server" Text='<%# Eval("cuentabanco") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="centradoMedio"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center" Font-Size="13px" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="True" Font-Size="13px" BackColor="#ebecec"
                                        CssClass="centradoMedio" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Saldo Inicial Mes" SortExpression="fdeposito">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaldoInicialMes" runat="server" Text='<%# Eval("fdeposito") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalDia" runat="server" Text="Total Saldo Final Dia"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Size="13px" />
                                    <ItemStyle HorizontalAlign="Center" Font-Size="13px" Wrap="False" />
                                    <FooterStyle CssClass="centradoMedio bg-color-grisOscuro fg-color-blanco" Font-Size="14px"></FooterStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Saldo Final Dia" SortExpression="deposito">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaldoFinalDia" runat="server" Text='<%# Eval("deposito") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalSaldoFinalDia" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Size="13px" />
                                    <ItemStyle HorizontalAlign="Center" Font-Size="13px" Wrap="False" />
                                    <FooterStyle CssClass="centradoMedio bg-color-grisOscuro fg-color-blanco" Font-Size="14px"></FooterStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Saldo Final Dia" SortExpression="foliocomple">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaldoFinalDia" runat="server" Text='<%# Eval("foliocomple") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalSaldoFinalDia" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Size="13px" />
                                    <ItemStyle HorizontalAlign="Center" Font-Size="13px" Wrap="False" />
                                    <FooterStyle CssClass="centradoMedio bg-color-grisOscuro fg-color-blanco" Font-Size="14px"></FooterStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Saldo Final Dia" SortExpression="seriecomple">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaldoFinalDia" runat="server" Text='<%# Eval("seriecomple") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalSaldoFinalDia" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Size="13px" />
                                    <ItemStyle HorizontalAlign="Center" Font-Size="13px" Wrap="False" />
                                    <FooterStyle CssClass="centradoMedio bg-color-grisOscuro fg-color-blanco" Font-Size="14px"></FooterStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Saldo Final Dia" SortExpression="ftimbradocomple">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaldoFinalDia" runat="server" Text='<%# Eval("ftimbradocomple") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalSaldoFinalDia" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Size="13px" />
                                    <ItemStyle HorizontalAlign="Center" Font-Size="13px" Wrap="False" />
                                    <FooterStyle CssClass="centradoMedio bg-color-grisOscuro fg-color-blanco" Font-Size="14px"></FooterStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Saldo Final Dia" SortExpression="totalcomple">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaldoFinalDia" runat="server" Text='<%# Eval("totalcomple") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalSaldoFinalDia" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Size="13px" />
                                    <ItemStyle HorizontalAlign="Center" Font-Size="13px" Wrap="False" />
                                    <FooterStyle CssClass="centradoMedio bg-color-grisOscuro fg-color-blanco" Font-Size="14px"></FooterStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Saldo Final Dia" SortExpression="uuidcomple">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaldoFinalDia" runat="server" Text='<%# Eval("uuidcomple") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalSaldoFinalDia" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Size="13px" />
                                    <ItemStyle HorizontalAlign="Center" Font-Size="13px" Wrap="False" />
                                    <FooterStyle CssClass="centradoMedio bg-color-grisOscuro fg-color-blanco" Font-Size="14px"></FooterStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Saldo Final Dia" SortExpression="folio">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaldoFinalDia" runat="server" Text='<%# Eval("folio") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalSaldoFinalDia" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Size="13px" />
                                    <ItemStyle HorizontalAlign="Center" Font-Size="13px" Wrap="False" />
                                    <FooterStyle CssClass="centradoMedio bg-color-grisOscuro fg-color-blanco" Font-Size="14px"></FooterStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Saldo Final Dia" SortExpression="serie">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaldoFinalDia" runat="server" Text='<%# Eval("serie") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalSaldoFinalDia" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Size="13px" />
                                    <ItemStyle HorizontalAlign="Center" Font-Size="13px" Wrap="False" />
                                    <FooterStyle CssClass="centradoMedio bg-color-grisOscuro fg-color-blanco" Font-Size="14px"></FooterStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Saldo Final Dia" SortExpression="ftimbrado">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaldoFinalDia" runat="server" Text='<%# Eval("ftimbrado") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalSaldoFinalDia" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Size="13px" />
                                    <ItemStyle HorizontalAlign="Center" Font-Size="13px" Wrap="False" />
                                    <FooterStyle CssClass="centradoMedio bg-color-grisOscuro fg-color-blanco" Font-Size="14px"></FooterStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Saldo Final Dia" SortExpression="total">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaldoFinalDia" runat="server" Text='<%# Eval("total") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalSaldoFinalDia" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Size="13px" />
                                    <ItemStyle HorizontalAlign="Center" Font-Size="13px" Wrap="False" />
                                    <FooterStyle CssClass="centradoMedio bg-color-grisOscuro fg-color-blanco" Font-Size="14px"></FooterStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Saldo Final Dia" SortExpression="uuid">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaldoFinalDia" runat="server" Text='<%# Eval("uuid") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalSaldoFinalDia" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Size="13px" />
                                    <ItemStyle HorizontalAlign="Center" Font-Size="13px" Wrap="False" />
                                    <FooterStyle CssClass="centradoMedio bg-color-grisOscuro fg-color-blanco" Font-Size="14px"></FooterStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Saldo Final Dia" SortExpression="rfccliente">
                                    <ItemTemplate>
                                        <asp:Label ID="rfcliente" runat="server" Text='<%# Eval("rfccliente") %>'></asp:Label>
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
