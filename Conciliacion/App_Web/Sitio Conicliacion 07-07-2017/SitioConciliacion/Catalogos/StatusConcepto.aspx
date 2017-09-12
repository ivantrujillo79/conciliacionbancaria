<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true"
    CodeFile="StatusConcepto.aspx.cs" Inherits="Catalogos_StatusConcepto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    Catalogo Status Concepto
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ToolkitScriptManager>
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
        var ModalProgress = '<%= ModalProgress.ClientID %>';        
    </script>
    <asp:UpdatePanel ID="upStatusConcepto" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td style="width: 300px; vertical-align: top">
                        <asp:Panel runat="server" ID="pnlNuevoStatusConcepto">
                            <div class="Filtrado">
                                <div class="tiraAmarilla">
                                </div>
                                <div class="titulo">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 85%">
                                                Nuevo <small style="font-size: 10px">Status Concepto</small>
                                            </td>
                                            <td>
                                                <asp:Image ID="imgNuevoSC" runat="server" CssClass="icono bg-color-amarilloClaro"
                                                    ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Agregar.png" Width="20px"
                                                    Heigth="20px" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="datos-estilo">
                                    <div class="etiqueta">
                                        Descripción
                                    </div>
                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="cajaTexto" Width="95%"></asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ErrorMessage=" Especifique una descripción para el nuevo Status Concepto."
                                        ControlToValidate="txtDescripcion" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                        ValidationGroup="NuevoStatusConcepto"></asp:RequiredFieldValidator>
                                    <div class="centradoMedio">
                                        <asp:Button ID="btnGuardarStatusConcepto" runat="server" Text="GUARDAR" ToolTip="Guardar Nuevo Concepto"
                                            ValidationGroup="NuevoStatusConcepto" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                            OnClick="btnGuardarStatusConcepto_Click" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </td>
                    <td style="vertical-align: top">
                        <div class="datos-estilo" style="margin-right: 15px; margin-left: 15px">
                            <div class="titulo lineaHorizontal" style="margin-left: 0px">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 95%">
                                            Catalogo : Status Concepto
                                        </td>
                                        <td>
                                            <img src="../App_Themes/GasMetropolitanoSkin/Imagenes/grid.png" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="floatDerecha">
                                <asp:Button ID="btnModificar" runat="server" EnableViewState="true" CssClass="botonPequeño bg-color-azulClaro fg-color-blanco"
                                    Text="EDITAR ETIQUETAS" OnClick="btnModificar_Click" />
                            </div>
                            <br />
                            <br />
                            <asp:Panel runat="server" ID="pnlStatusConcepto">
                                <asp:GridView ID="grvStatusConcepto" runat="server" AutoGenerateColumns="False" Width="100%"
                                    BorderStyle="Dotted" AllowPaging="True" PageSize="10" CssClass="grvResultadoConsultaCss"
                                    ShowHeaderWhenEmpty="True" DataKeyNames="Id, Descripcion, Status" OnPageIndexChanging="grvStatusConcepto_PageIndexChanging"
                                    OnRowDataBound="grvStatusConcepto_RowDataBound" OnRowCommand="grvStatusConcepto_RowCommand">
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="No existen ningun Status Concepto"></asp:Label>
                                    </EmptyDataTemplate>
                                    <HeaderStyle CssClass="GridHeader" />
                                    <RowStyle CssClass="GridRow" />
                                    <AlternatingRowStyle CssClass="bg-color-grisClaro01" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Descripcion" SortExpression="Descripcion">
                                            <ItemTemplate>
                                                <asp:Literal ID="rbElegirStatusConcepto" runat="server"></asp:Literal>
                                                <asp:Label ID="lblDescripcion" runat="server" Text="<%# Bind('Descripcion') %>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="40%" />
                                            <ItemStyle Width="40%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UsuarioAlta" SortExpression="UsuarioAlta">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUsuario" runat="server" Text="<%# Bind('Usuario') %>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FAlta" SortExpression="FAlta">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFAlta" runat="server" Text="<%# Bind('FAlta','{0:d}') %>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblStatus" Text="<%# Bind('Status') %>" Visible="False"></asp:Label>
                                                <asp:Button runat="server" ID="btnStatus" Width="16px" CommandName="CAMBIARSTATUS"
                                                    Height="16px" Style="padding: 0px 0px 0px 0px" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
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
                            </asp:Panel>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hfEditarEtiquetas" runat="server" />
    <asp:Panel ID="pnlPopUpEtiquetas" runat="server" BackColor="#FFFFFF" Width="30%"
        Style="display: none">
        <asp:UpdatePanel ID="upEtiquetas" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr class="bg-color-grisOscuro">
                        <td colspan="2" class="etiqueta" style="padding: 5px 5px 5px 5px">
                            <div class="floatDerecha">
                                <asp:ImageButton runat="server" ID="imgCerrarImportar" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png"
                                    CssClass="iconoPequeño bg-color-rojo" OnClientClick="HideModalPopup();" />
                            </div>
                            <div class="fg-color-blanco">
                                ETIQUETAS A STATUS:
                                <asp:Label ID="lblStatusActual" runat="server"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="datos-estilo lineaHorizontal etiqueta" style="padding: 5px 5px 5px 5px">
                            <b>No Asignadas</b>
                        </td>
                    </tr>
                    <tr>
                        <td class="datos-estilo" style="padding: 5px 5px 5px 5px">
                            <div class="etiqueta">
                                Corporativo</div>
                            <asp:DropDownList ID="ddlCorporativo" runat="server" CssClass="dropDown" Width="100%"
                                AutoPostBack="True" OnDataBound="ddlCorpotativo_DataBound" OnSelectedIndexChanged="ddlCorpotativo_SelectedIndexChanged">
                            </asp:DropDownList>
                            <div class="etiqueta">
                                Banco</div>
                            <asp:DropDownList ID="ddlBancoFinanciero" runat="server" CssClass="dropDown" Width="100%"
                                AutoPostBack="True" OnDataBound="ddlBancoFinanciero_DataBound" OnSelectedIndexChanged="ddlBancoFinanciero_SelectedIndexChanged">
                            </asp:DropDownList>
                            <div class="etiqueta">
                                Etiqueta</div>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 98%">
                                        <asp:DropDownList ID="ddlEtiquetasBanco" runat="server" CssClass="dropDown" Width="100%"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 1%">
                                    </td>
                                    <td style="width: 1%">
                                        <asp:ImageButton ID="btnAgregarEtiqueta" ImageAlign="Middle" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/AgregarActivo.png"
                                            CssClass="icono bg-color-grisClaro01" Width="15px" Heigth="15px" EnableViewState="true"
                                            OnClick="btnAgregarEtiqueta_Click" ValidationGroup="NuevoEtiquetaStatus" />
                                    </td>
                                </tr>
                            </table>
                            <asp:RequiredFieldValidator ID="rfvEtiqueta" runat="server" ErrorMessage=" Especifique una Etiqueta."
                                ControlToValidate="ddlEtiquetasBanco" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="NuevoEtiquetaStatus"></asp:RequiredFieldValidator>
                        </td>
                        <tr>
                            <td class="datos-estilo lineaHorizontal etiqueta" style="padding: 5px 5px 5px 5px">
                                <b>Asignadas</b>
                            </td>
                        </tr>
                        <tr>
                            <td class="datos-estilo etiqueta" style="padding: 5px 5px 5px 5px">
                                <asp:GridView ID="grvEtiquetaStatusConcepto" runat="server" AutoGenerateColumns="false"
                                    Width="100%" CssClass="grvResultadoConsultaCss" DataKeyNames="Id" AllowPaging="True"
                                    PageSize="8" OnPageIndexChanging="grvEtiquetaStatusConcepto_PageIndexChanging"
                                    OnRowDataBound="grvEtiquetaStatusConcepto_RowDataBound">
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="No existen ningun Etiqueta"></asp:Label>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Descripcion" SortExpression="colDescripcion">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGVDescripcion" runat="server" Text="<%# Bind('Descripcion') %>"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" />
                                            <HeaderStyle Width="200px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="T.Dato" SortExpression="colTipoDato">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGVFFechar" runat="server" Text="<%# Bind('TipoDato') %>" Width="70%"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="25px" HorizontalAlign="Center" />
                                            <HeaderStyle Width="25px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick='<%# "return confirm(\"¿Desea quitar la etiqueta,  "+ Eval("Descripcion").ToString() +  " para este Status Concepto?\");" %>'
                                                    ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/QuitarActivo.png" CssClass="icono bg-color-grisClaro01"
                                                    Width="15px" Heigth="15px" EnableViewState="true" />
                                            </ItemTemplate>
                                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                                            <HeaderStyle Width="20px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerTemplate>
                                        Página
                                        <asp:DropDownList ID="paginasDropDownListEtiquetas" Font-Size="12px" AutoPostBack="true"
                                            runat="server" OnSelectedIndexChanged="paginasDropDownListEtiquetas_SelectedIndexChanged"
                                            CssClass="dropDown" Width="60px">
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
                            </td>
                        </tr>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:ModalPopupExtender ID="popUpEtiquetas" runat="server" PopupControlID="pnlPopUpEtiquetas"
        TargetControlID="hfEditarEtiquetas" BehaviorID="ModalBehaviour" BackgroundCssClass="ModalBackground">
    </asp:ModalPopupExtender>
    <asp:UpdateProgress ID="panelBloqueo" runat="server" AssociatedUpdatePanelID="upStatusConcepto">
        <ProgressTemplate>
            <asp:Image ID="imgLoad" runat="server" CssClass="icono bg-color-blanco" Height="40px"
                ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/LoadPage.gif" Width="40px" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ModalPopupExtender ID="ModalProgress" runat="server" PopupControlID="panelBloqueo"
        BackgroundCssClass="ModalBackground" TargetControlID="panelBloqueo">
    </asp:ModalPopupExtender>
</asp:Content>
