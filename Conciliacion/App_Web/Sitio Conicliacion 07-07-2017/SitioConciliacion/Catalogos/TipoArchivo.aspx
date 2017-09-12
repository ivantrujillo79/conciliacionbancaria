<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true"
    CodeFile="TipoArchivo.aspx.cs" Inherits="ImportacionArchivos_TipoArchivo" Title="Tipo Archivo"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ MasterType TypeName="Principal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ToolkitScriptManager>
    <script type="text/javascript" src="../App_Scripts/jsUpdateProgress.js"></script>
    <script type="text/javascript" src="../App_Scripts/FuncionesGenerales.js"></script>
    <script type="text/javascript" language="javascript">
        var ModalProgress = '<%= ModalProgress.ClientID %>';        
    </script>
    <asp:UpdatePanel ID="uppPrincipal" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td style="width: 300px; vertical-align: top" class="Filtrado">
                        <div class="tiraAmarilla">
                        </div>
                        <div class="titulo">
                            Datos Generales
                        </div>
                        <div class="datos-estilo">
                            <div class="etiqueta">
                                Descripción</div>
                            <asp:TextBox ID="txtDescripcion" runat="server" Width="96%" CssClass="cajaTexto"></asp:TextBox>
                            <br />
                            <div class="etiqueta">
                                Formato Fecha</div>
                            <asp:TextBox ID="txtFormatoFecha" runat="server" Width="96%" CssClass="cajaTexto"></asp:TextBox>
                            <br />
                            <div class="etiqueta">
                                Formato Moneda</div>
                            <asp:TextBox ID="txtFormatoMoneda" runat="server" Width="96%" CssClass="cajaTexto"></asp:TextBox>
                            <br />
                            <div class="etiqueta">
                                Separador</div>
                            <asp:DropDownList ID="cboSeparador" runat="server" CssClass="dropDown" Width="100%"
                                OnDataBound="cboSeparador_DataBound">
                            </asp:DropDownList>
                            <br /><br />
                            <div class="centradoMedio">
                                <asp:Button ID="btnCancelarDatos" runat="server" CssClass="boton fg-color-blanco bg-color-grisClaro"
                                    OnClick="btnCancelarDatos_Click" Text="CANCELAR" ToolTip="Cancelar el guardado de datos" />
                                <asp:Button ID="btnGuardarDatos" runat="server" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                    OnClick="btnGuardarDatos_Click" Text="GUARDAR" ToolTip="Guardar Tipo Archivo"
                                    ValidationGroup="DatosGral" />
                            </div>
                        </div>
                    </td>
                    <td style="vertical-align: top">
                        <div class="datos-estilo">
                            <div class="titulo lineaHorizontal" style="margin-left: 0px">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 95%">
                                            Catalogo: Tipo Archivo
                                        </td>
                                        <td>
                                            <img src="../App_Themes/GasMetropolitanoSkin/Imagenes/grid.png" alt="Consulta" />
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <asp:GridView ID="grvTipoArchivo" runat="server" AutoGenerateColumns="false" Width="100%"
                                CssClass="grvResultadoConsultaCss" AllowPaging="True" PageSize="6" OnPageIndexChanging="grvTipoArchivo_PageIndexChanging"
                                OnRowDataBound="grvTipoArchivo_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" SortExpression="colIdTipoArchivo" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGVIdTipoArchivo" runat="server" Text="<%# Bind('IdTipoArchivo') %>"
                                                Width="50%"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Descripcion" SortExpression="colDescripcion">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGVDescripcionr" runat="server" Text="<%# Bind('Descripcion') %>"
                                                Width="50%"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="F. Fecha" SortExpression="colFFecha">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGVFFechar" runat="server" Text="<%# Bind('FormatoFecha') %>" Width="50%"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="F.Moneda" SortExpression="colFMoneda">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGVFMoneda" runat="server" Text="<%# Bind('FormatoMoneda') %>" Width="50%"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Separador" SortExpression="colDescripcion">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGVSeparador" runat="server" Text="<%# Bind('Separador') %>" Width="50%"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" ImageAlign="Middle" runat="server" OnClick="btnDelete_Click"
                                                OnClientClick='<%# "return confirm(\"¿Desea eliminar el registro,  "+ Eval("Descripcion").ToString() +  "  ?\");" %>'
                                                ImageUrl="../App_Themes/GasMetropolitano/Imagenes/quitar.png" EnableViewState="true"/>
                                        </ItemTemplate>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="panelBloqueo" runat="server" AssociatedUpdatePanelID="uppPrincipal">
        <ProgressTemplate>
            <asp:Image ID="imgLoad" runat="server" CssClass="icono bg-color-blanco" Height="40px"
                ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/LoadPage.gif" Width="40px" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ModalPopupExtender ID="ModalProgress" runat="server" PopupControlID="panelBloqueo"
        BackgroundCssClass="ModalBackground" TargetControlID="panelBloqueo">
    </asp:ModalPopupExtender>
</asp:Content>
