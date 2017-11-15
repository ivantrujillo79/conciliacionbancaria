<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" EnableViewState="true"
    CodeFile="ConsultarDocumentos.aspx.cs" Inherits="Conciliacion_ConsultarDocumentos" Debug="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    CONSULTAR DOCUMENTOS TRANSBAN
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <script src="../App_Scripts/jQueryScripts/jquery.min.js" type="text/javascript"></script>
    <script src="../App_Scripts/jQueryScripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../App_Scripts/Common.js" type="text/javascript"></script>
    <link href="../App_Scripts/MenuContextual/css/Estilo.css" rel="stylesheet" type="text/css" />
    <!--  MenuContextual -->
    <script type="text/javascript">
        function pageLoad() {
            var hiddenField = document.getElementById("<%=fldIndiceConcilacion.ClientID %>");
            var gridviewID = "<%=grid_cmdtb.ClientID%>";
            var rowid = 0;

            //Funciones Click Derecho sobre GridView
            $("#<%=miMenu.ClientID%>").hide();
            $("table[id$='grid_cmdtb'] > tbody > tr").bind('contextmenu', function (e) {
                $("#<%=miMenu.ClientID%>").hide();
                e.preventDefault();
                rowid = $(this).children(':first-child').text();
                if (!isNaN(rowid)) {
                    hiddenField.value = rowid;
                    $("#<%=miMenu.ClientID%>").css({
                        top: e.pageY + "px",
                        left: e.pageX + "px",
                        position: 'absolute'
                    });
                    $("#<%=miMenu.ClientID%>").show();
                }
            });
            $(document).bind('click', function (e) {
                $("#<%=miMenu.ClientID%>").hide();
            });
            gridview = $('#' + gridviewID);
        }
    </script>
    <script src="../App_Scripts/jsHoverGridView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager ID="smActualizar" runat="server">
    </asp:ScriptManager>
    <script src="../App_Scripts/jsUpdateProgress.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        var ModalProgress = '<%= mpeLoading.ClientID %>';
    </script>
    <asp:UpdatePanel runat="server" ID="upInicio" UpdateMode="Always" >
        <ContentTemplate>
            <script type="text/javascript">
                function fnReporte() {
                    var lnkReporte = document.getElementById("<%=lnkReporte.ClientID %>");
                    lnkReporte.click();
                }
            </script>
            <table style="width: 100%">
                <tr>
                    <td style="vertical-align: top">
                        <div class="datos-estilo">
                            <div class="titulo" style="margin-left: 0px">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 95%">
                                            Documentos
                                        </td>
                                        <td>
                                            <img src="../App_Themes/GasMetropolitanoSkin/Imagenes/grid.png" alt="Consulta" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="lineaHorizontal">
                            </div>
                            <br />
                            <asp:GridView ID="grid_cmdtb" runat="server" CssClass="grvResultadoConsultaCss" PageSize="12" AutoGenerateColumns="false"
                ShowHeaderWhenEmpty="true" Width="100%" OnRowCreated="grid_cmdtb_RowCreated" AllowPaging="true" 
                OnRowDataBound="grid_cmdtb_RowDataBound" OnPageIndexChanging="grid_cmdtb_PageIndexChanging">
                                <EmptyDataTemplate>
                    <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="No existe ningún documento a visualizar de acuerdo a los Parámetros del Filtrado. "></asp:Label>
                </EmptyDataTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblIndice" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" CssClass="ocultar" Width="10px" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" CssClass="ocultar" Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Clave" SortExpression="Clave">
                        <ItemTemplate>
                            <asp:Label ID="lblClave" runat="server" Text='<%# Eval("Clave") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle CssClass="centradoMedio"></ControlStyle>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Wrap="True" BackColor="#ebecec" ForeColor="Black"
                            CssClass="centradoMedio" />
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="FMovimiento" SortExpression="FMovimiento">
                        <ItemTemplate>
                            <asp:Label ID="lblFMovimiento" runat="server" Text='<%# Eval("FMovimiento") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle CssClass="centradoMedio"></ControlStyle>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" CssClass="centradoMedio" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CajaDescripcion" SortExpression="CajaDescripcion">
                        <ItemTemplate>
                            <asp:Label ID="lblCajaDescripcion" runat="server" Text='<%# Eval("CajaDescripcion") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle CssClass="centradoMedio" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Wrap="false" CssClass="centradoMedio" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Caja" SortExpression="Caja">
                        <ItemTemplate>
                            <asp:Label ID="lblCaja" runat="server" Text='<%# Eval("Caja") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle CssClass="centradoMedio" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Wrap="false" CssClass="centradoMedio" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FOperacion" SortExpression="FOperacion">
                        <ItemTemplate>
                            <asp:Label ID="lblFOperacion" runat="server" Text='<%# Eval("FOperacion") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle CssClass="centradoMedio" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Wrap="false" CssClass="centradoMedio" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TipoMovimientoCajaDescripcion" SortExpression="TipoMovimientoCajaDescripcion">
                        <ItemTemplate>
                            <asp:Label ID="lblTipoMovimientoCajaDescripcion" runat="server" Text='<%# Eval("TipoMovimientoCajaDescripcion") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle CssClass="centradoMedio" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Wrap="false" CssClass="centradoMedio" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total" SortExpression="Total">
                        <ItemTemplate>
                            <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle CssClass="centradoMedio" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Wrap="false" CssClass="centradoMedio" />
                    </asp:TemplateField>
                                </Columns>
                                <PagerTemplate>
                                    Página
                                    <asp:DropDownList ID="paginasDropDownList" Font-Size="12px" AutoPostBack="true" runat="server"
                                        OnSelectedIndexChanged="paginasDropDownList_SelectedIndexChanged" CssClass="dropDown"
                                        Width="60px">
                                    </asp:DropDownList>
                                    de
                                    <asp:Label ID="lblTotalNumPaginas" runat="server" CssClass="etiqueta fg-color-blanco" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnInicial" runat="server" CommandName="Page" ToolTip="Prim. Pag"
                                        CommandArgument="First" CssClass="boton pagInicial" />
                                    <asp:Button ID="btnAnterior" runat="server" CommandName="Page" ToolTip="Pág. anterior"
                                        CommandArgument="Prev" CssClass="boton pagAnterior" />
                                    <asp:Button ID="btnSiguiente" runat="server" CommandName="Page" ToolTip="Sig. página"
                                        CommandArgument="Next" CssClass="boton pagSiguiente" />
                                    <asp:Button ID="btnUltima" runat="server" CommandName="Page" ToolTip="Últ. Pag" CommandArgument="Last"
                                        CssClass="boton pagUltima" />
                                </PagerTemplate>
                                <PagerStyle CssClass="estiloPaginacion bg-color-grisOscuro fg-color-blanco" />
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
            <!-- Menu Contextual -->                                   
            <asp:LinkButton ID="lnkReporte" Style="display: none" runat="server" OnClick="lnkReporte_Click"/>
            <asp:HiddenField ID="fldIndiceConcilacion" runat="server" />
            <ul id="miMenu" class="contextMenu" runat="server" >
                <li class="conciliar"><a runat="server" id="lnkReporteM">Reporte</a></li> 
              <%--  <li class="informe"><a runat="server" id="lnkInformeM">Informe</a></li>--%>
            </ul>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="panelBloqueo" runat="server" AssociatedUpdatePanelID="upInicio">
        <ProgressTemplate>
            <asp:Image ID="imgLoad" runat="server" CssClass="icono bg-color-blanco" Height="40px"
                ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/LoadPage.gif" Width="40px" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ModalPopupExtender ID="mpeLoading" runat="server" BackgroundCssClass="ModalBackground"
        PopupControlID="panelBloqueo" TargetControlID="panelBloqueo">
    </asp:ModalPopupExtender>
</asp:Content>
