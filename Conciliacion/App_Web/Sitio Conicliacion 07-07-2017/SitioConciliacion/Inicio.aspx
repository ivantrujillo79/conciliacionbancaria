<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" EnableViewState="true"
    CodeFile="Inicio.aspx.cs" Inherits="Inicio" Debug="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    CONCILIACIÓN
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <script src="App_Scripts/jQueryScripts/jquery.min.js" type="text/javascript"></script>
    <script src="App_Scripts/jQueryScripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="App_Scripts/Common.js" type="text/javascript"></script>
    <link href="App_Scripts/MenuContextual/css/Estilo.css" rel="stylesheet" type="text/css" />
    <!--  MenuContextual -->
    <script type="text/javascript">
        function pageLoad() {
            var hiddenField = document.getElementById("<%=fldIndiceConcilacion.ClientID %>");
            var gridviewID = "<%=grvConciliacion.ClientID%>";
            var rowid = 0;

            //Funciones Click Derecho sobre GridView
            $("#<%=miMenu.ClientID%>").hide();
            $("table[id$='grvConciliacion'] > tbody > tr").bind('contextmenu', function (e) {
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
    <script src="App_Scripts/jsHoverGridView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager ID="smActualizar" runat="server">
    </asp:ScriptManager>
    <script src="App_Scripts/jsUpdateProgress.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        var ModalProgress = '<%= mpeLoading.ClientID %>';        
    </script>
    <asp:UpdatePanel runat="server" ID="upInicio" UpdateMode="Always" >
        <ContentTemplate>
            <script type="text/javascript">
                function fnVerConciliacion() {
                    var lnkVer = document.getElementById("<%=lnkVer.ClientID %>");
                    lnkVer.click();
                }
                function fnDetalle() {
                    var lnkDetalle = document.getElementById("<%=lnkDetalle.ClientID %>");
                    lnkDetalle.click();
                }
                function fnPagos() {
                    var lnkPagos = document.getElementById("<%=lnkPagos.ClientID %>");
                    lnkPagos.click();
                }

                function fnInforme() {
                    var lnkInforme = document.getElementById("<%=lnkInforme.ClientID %>");
                    lnkInforme.click();
                }
              
            </script>
            <table style="width: 100%">
                <tr>
                    <td style="width: 300px; vertical-align: top" class="Filtrado">
                        <div class="tiraAmarilla">
                        </div>
                        <div class="titulo">
                            Filtro
                        </div>
                        <div class="datos-estilo">
                            <div class="etiqueta">
                                Empresa
                            </div>
                            <asp:DropDownList ID="ddlEmpresa" runat="server" Width="100%" SkinID="DropDownList"
                                CssClass="dropDown" AutoPostBack="True" OnDataBound="ddlEmpresa_DataBound" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvEmpresa" runat="server" ErrorMessage=" Especifique la empresa. "
                                ControlToValidate="ddlEmpresa" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="Conciliacion"></asp:RequiredFieldValidator>
                            <div class="etiqueta">
                                Sucursal
                            </div>
                            <asp:DropDownList ID="ddlSucursal" runat="server" Width="100%" CssClass="dropDown"
                                AutoPostBack="False">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="rfSucursal" runat="server" ErrorMessage=" Especifique la sucursal. "
                                ControlToValidate="ddlSucursal" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="Conciliacion"></asp:RequiredFieldValidator>
                            <div class="etiqueta">
                                Grupo Conciliación
                            </div>
                            <asp:DropDownList ID="ddlGrupo" runat="server" Width="100%" CssClass="dropDown" AutoPostBack="False">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvGrupo" runat="server" ErrorMessage=" Especifique el grupo. "
                                ControlToValidate="ddlGrupo" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="Conciliacion"></asp:RequiredFieldValidator>
                            <div class="etiqueta">
                                Tipo Conciliación
                            </div>
                            <asp:DropDownList ID="ddlTipoConciliacion" runat="server" Width="100%" CssClass="dropDown"
                                AutoPostBack="False">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvTipoConciliacion" runat="server" ErrorMessage=" Especifique el Tipo Conciliación. "
                                ControlToValidate="ddlTipoConciliacion" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="Conciliacion"></asp:RequiredFieldValidator>
                            <div class="etiqueta">
                                Status Conciliación
                            </div>
                            <asp:DropDownList ID="ddlStatusConciliacion" Width="100%" CssClass="dropDown" AutoPostBack="False"
                                runat="server">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ErrorMessage=" Especifique el Status. "
                                ControlToValidate="ddlStatusConciliacion" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="Conciliacion"></asp:RequiredFieldValidator>
                            <div class="etiqueta">
                                Año
                            </div>
                            <asp:DropDownList ID="ddlAñoConciliacion" runat="server" CssClass="dropDown" Width="100%"
                                AutoPostBack="False">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvAñoConciliacion" runat="server" ErrorMessage=" Especifique el Año de la Conciliación. "
                                ControlToValidate="ddlAñoConciliacion" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="Conciliacion"></asp:RequiredFieldValidator>
                            <div class="etiqueta">
                                Mes
                            </div>
                            <asp:DropDownList ID="ddlMesConciliacion" runat="server" Width="100%" CssClass="dropDown"
                                AutoPostBack="False">
                                <asp:ListItem Value="1">ENERO</asp:ListItem>
                                <asp:ListItem Value="2">FEBRERO</asp:ListItem>
                                <asp:ListItem Value="3">MARZO</asp:ListItem>
                                <asp:ListItem Value="4">ABRIL</asp:ListItem>
                                <asp:ListItem Value="5">MAYO</asp:ListItem>
                                <asp:ListItem Value="6">JUNIO</asp:ListItem>
                                <asp:ListItem Value="7">JULIO</asp:ListItem>
                                <asp:ListItem Value="8">AGOSTO</asp:ListItem>
                                <asp:ListItem Value="9">SEPTIEMBRE</asp:ListItem>
                                <asp:ListItem Value="10">OCTUBRE</asp:ListItem>
                                <asp:ListItem Value="11">NOVIEMBRE</asp:ListItem>
                                <asp:ListItem Value="12">DICIEMBRE</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvMes" runat="server" ErrorMessage=" Especifique el Mes de la Conciliación. "
                                ControlToValidate="ddlMesConciliacion" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="Conciliacion"></asp:RequiredFieldValidator>
                            <div class="centradoMedio">
                                <asp:Button ID="btnConsultar" runat="server" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                    Text="CONSULTAR" ToolTip="CONSULTAR" ValidationGroup="Conciliacion" OnClick="btnConsultar_Click" />
                                <asp:Button ID="btnNuevaConciliacion" runat="server" CssClass="boton fg-color-blanco bg-color-naranja"
                                    Text="NUEVA CONCILIACIÓN" ToolTip="NUEVA CONCILIACIÓN" OnClick="btnNuevaConciliacion_Click" />
                            </div>
                        </div>
                    </td>
                    <td style="vertical-align: top">
                        <div class="datos-estilo">
                            <div class="titulo" style="margin-left: 0px">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 95%">
                                            Conciliaciones
                                        </td>
                                        <td>
                                            <img src="App_Themes/GasMetropolitanoSkin/Imagenes/grid.png" alt="Consulta" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="lineaHorizontal">
                            </div>
                            <br />
                            <asp:GridView ID="grvConciliacion" runat="server" AutoGenerateColumns="False" Width="100%"
                                AllowPaging="True" ShowHeaderWhenEmpty="True" CssClass="grvResultadoConsultaCss"
                                DataKeyNames="FolioConciliacion" PageSize="12" OnPageIndexChanging="grvConciliacion_PageIndexChanging"
                                OnRowDataBound="grvConciliacion_RowDataBound" OnRowCreated="grvConciliacion_RowCreated"
                                AllowSorting="True" OnSorting="grvConciliacion_Sorting" >
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="No existe ninguna conciliación de acuerdo a los Parámetros del Filtrado. "></asp:Label>
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
                                    <asp:TemplateField HeaderText="F. Con." SortExpression="FolioConciliacion">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFolioConciliacion" runat="server" Text='<%# Eval("FolioConciliacion") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="centradoMedio"></ControlStyle>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="True" BackColor="#ebecec" ForeColor="Black"
                                            CssClass="centradoMedio" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Banco" SortExpression="Banco">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBanco" runat="server" Text='<%# Eval("Banco") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="centradoMedio"></ControlStyle>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" CssClass="centradoMedio" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="C. Banc." SortExpression="CuentaBancaria">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCuentaBancaria" runat="server" Text='<%# Eval("CuentaBancaria") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="centradoMedio"></ControlStyle>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="True" CssClass="centradoMedio" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="F. Inicial" SortExpression="FInicial">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFechaInicial" runat="server" Text='<%# Eval("FInicial", "{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="F. Final" SortExpression="FFinal">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFechaFinal" runat="server" Text='<%# Eval("FFinal","{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="T. Tran. Int." SortExpression="TransaccionesInternas">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransaccionesInternas" runat="server" Text='<%# Eval("TransaccionesInternas") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="T. Tran. Ext." SortExpression="TransaccionesExternas">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransaccionesExternas" runat="server" Text='<%# Eval("TransaccionesExternas") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Con. Ext" SortExpression="ConciliacionesExternas">
                                        <ItemTemplate>
                                            <asp:Label ID="lblConciliacionesExternas" runat="server" Text='<%# Eval("ConciliacionesExternas") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Con. Int." SortExpression="ConciliacionesInternas">
                                        <ItemTemplate>
                                            <asp:Label ID="lblConciliacionesInternas" runat="server" Text='<%# Eval("ConciliacionesInternas") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Grp. Con." SortExpression="GrupoConciliacionstr">
                                        <ItemTemplate>
                                            <div class="parrafoTexto">
                                                <asp:Label ID="lblGrupo" runat="server" Text='<%# Eval("GrupoConciliacionstr") %>'></asp:Label>
                                            </div>
                                            <asp:HoverMenuExtender ID="hmeGrupo" runat="server" TargetControlID="lblGrupo" PopupControlID="pnlPopUpConceptoExt"
                                                PopDelay="20" OffsetX="0" OffsetY="0">
                                            </asp:HoverMenuExtender>
                                            <asp:Panel ID="pnlPopUpConceptoExt" runat="server" CssClass="grvResultadoConsultaCss ocultar"
                                                Width="150px" Style="padding: 5px 5px 5px 5px" BackColor="White" Wrap="True">
                                                <asp:Label ID="lblToolTipConceptoExt" runat="server" Text='<%# Eval("GrupoConciliacionstr") %>'
                                                    CssClass="etiqueta " Font-Size="10px" />
                                            </asp:Panel>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sucursal" SortExpression="SucursalDes">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSucursalDes" runat="server" Text='<%# Eval("SucursalDes") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sts." SortExpression="StatusConciliacion">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblStatusConciliacion" Text='<%# Bind("StatusConciliacion") %>'
                                                Style="display: none"></asp:Label>
                                            <asp:Image runat="server" ID="imgStatusConciliacion" ImageUrl='<%# Bind("UbicacionIcono") %>'
                                                Width="15px" Height="15px" CssClass="icono border-color-grisOscuro centradoMedio"
                                                ToolTip='<%# Bind("StatusConciliacion") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
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
            <asp:LinkButton ID="lnkVer" Style="display: none" runat="server" OnClick="lnkVer_Click" />
            <asp:LinkButton ID="lnkDetalle" Style="display: none" runat="server" OnClick="lnkDetalle_Click" />
            <asp:LinkButton ID="lnkPagos" Style="display: none" runat="server" OnClick="lnkPagos_Click" />
            <asp:LinkButton ID="lnkInforme" Style="display: none" runat="server" OnClick="lnkInforme_Click" />
            <asp:HiddenField ID="fldIndiceConcilacion" runat="server" />
            <ul id="miMenu" class="contextMenu" runat="server" >
                <li class="conciliar"><a runat="server" id="lnkVerM">Conciliar</a></li>
                <li class="detalle"><a onclick="fnDetalle();">Detalle</a></li>
                <li class="separator"></li>
                <li class="pagos"><a runat="server" id="lnkPagosM">Pagos</a></li>
                <li class="informe"><a runat="server" id="lnkInformeM">Informe</a></li> 
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
