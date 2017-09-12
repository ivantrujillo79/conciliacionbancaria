<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true"
    CodeFile="Separadores.aspx.cs" Inherits="ImportacionArchivos_Separadores" Title="Separadores"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ MasterType TypeName="Principal" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ToolkitScriptManager>
    <script src="../App_Scripts/jsUpdateProgress.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        var ModalProgress = '<%= ModalProgress.ClientID %>';        
    </script>
    <div>
        <asp:UpdatePanel ID="updPrincipal" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 300px; vertical-align: top">
                            <div class="Filtrado">
                                <div class="tiraAmarilla">
                                </div>
                                <div class="titulo">
                                    Nuevo
                                </div>
                                <div class="datos-estilo">
                                    <div class="etiqueta">
                                        Separador
                                    </div>
                                    <asp:TextBox ID="txtSeparador" runat="server" Width="96%" CssClass="cajaTexto"></asp:TextBox>
                                    <br />
                                    <div class="centradoMedio">
                                        <asp:Button ID="btnCancelarDatos" runat="server" Text="CANCELAR" ToolTip="Cancelar el guardado de datos"
                                            OnClick="btnCancelarDatos_Click" CssClass="boton bg-color-grisClaro02 fg-color-blanco" />
                                        <asp:Button ID="btnGuardarDatos" runat="server" Text="GUARDAR" ToolTip="Guardar Separador"
                                            OnClick="btnGuardarDatos_Click" CssClass="boton bg-color-azulClaro fg-color-blanco" />
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </td>
                        <td style="vertical-align: top">
                            <div class="datos-estilo">
                                <div class="titulo lineaHorizontal" style="margin-left: 0px">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 95%">
                                               Catalogo: Separadores
                                            </td>
                                            <td>
                                                <img src="../App_Themes/GasMetropolitanoSkin/Imagenes/grid.png" alt="Consulta" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <br />
                                <asp:GridView ID="grvSeparadores" runat="server" AutoGenerateColumns="false" Width="100%"
                                    CssClass="grvResultadoConsultaCss" PageSize="10" BorderStyle="Dotted" AllowPaging="True" 
                                    onpageindexchanging="grvSeparadores_PageIndexChanging" 
                                    onrowdatabound="grvSeparadores_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Separador" SortExpression="colDescripcion">
                                            <ItemTemplate>
                                                <b>
                                                    <asp:Label ID="lblGVSeparador" runat="server" Text="<%# Bind('Descripcion') %>" Width="50%"
                                                        Font-Size="12px"></asp:Label></b>
                                            </ItemTemplate>
                                            <ItemStyle Width="80%" HorizontalAlign="Center" />
                                            <HeaderStyle Width="80%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" ImageAlign="Middle" runat="server" OnClick="btnDelete_Click"
                                                    OnClientClick='<%# "return confirm(\"¿Desea eliminar el registro  "+ Eval("Descripcion").ToString() +  "  ?\");" %>'
                                                    ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/DesconciliarActivo.png" CssClass="icono bg-color-grisClaro01"
                                                    EnableViewState="true" Width="15px" Heigth="15px" ToolTip="QUITAR" />
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" HorizontalAlign="Center" />
                                            <HeaderStyle Width="20%" HorizontalAlign="Center" />
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
                                <br />
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="panelBloqueo" runat="server" AssociatedUpdatePanelID="updPrincipal">
            <ProgressTemplate>
                <asp:Image ID="imgLoad" runat="server" CssClass="icono bg-color-blanco" Height="40px"
                    ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/LoadPage.gif" Width="40px" />
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:ModalPopupExtender ID="ModalProgress" runat="server" PopupControlID="panelBloqueo"
            BackgroundCssClass="ModalBackground" TargetControlID="panelBloqueo">
        </asp:ModalPopupExtender>
    </div>
</asp:Content>
