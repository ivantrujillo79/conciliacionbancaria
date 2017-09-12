<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true"
    CodeFile="Etiqueta.aspx.cs" Inherits="ImportacionArchivos_Etiqueta" Title="Etiquetas"
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
                                Banco</div>
                            <asp:DropDownList ID="cboBancoFinanciero" runat="server" Width="100%" CssClass="dropDown"
                                AutoPostBack="True" OnDataBound="cboBancoFinanciero_DataBound">
                            </asp:DropDownList>
                            <br />
                            <div class="etiqueta">
                                Tipo Fuente Info.</div>
                            <asp:DropDownList ID="cboTipoFuenteInformacion" runat="server" Width="100%" CssClass="dropDown"
                                OnDataBound="cboTipoFuenteInformacion_DataBound">
                            </asp:DropDownList>
                            <br />
                            <div class="etiqueta">
                                Descripción</div>
                            <asp:TextBox ID="txtDescripcion" runat="server" Width="96%" CssClass="cajaTexto"></asp:TextBox>
                            <br />
                            <div class="etiqueta">
                                Tipo Dato</div>
                            <asp:DropDownList ID="cboTipoDato" runat="server" Width="100%" CssClass="dropDown"
                                OnDataBound="cboTipoDato_DataBound">
                            </asp:DropDownList>
                            <br />
                            <div class="etiqueta">
                                Tabla</div>
                            <asp:DropDownList ID="cboTabla" runat="server" BorderStyle="Solid" Width="100%" CssClass="dropDown"
                                AutoPostBack="True" OnSelectedIndexChanged="cboTabla_SelectedIndexChanged" OnDataBound="cboTabla_DataBound">
                            </asp:DropDownList>
                            <br />
                            <div class="etiqueta">
                                Campo Destino</div>
                            <asp:DropDownList ID="cboColumna" runat="server" Width="100%" CssClass="dropDown"
                                OnDataBound="cboColumna_DataBound">
                            </asp:DropDownList>
                            <br />
                            <br />
                            <div class="etiqueta">
                                <asp:CheckBox runat="server" Text="Concatenar Etiqueta" ID="chkConcatenar"/>
                            </div>
                            <div class="centradoMedio">
                                <asp:Button ID="btnCancelarDatos" runat="server" BackColor="WhiteSmoke" CssClass="boton fg-color-blanco bg-color-grisClaro"
                                    Text="CANCELAR" ToolTip="Cancelar el guardado de datos" OnClick="btnCancelarDatos_Click" />
                                <asp:Button ID="btnGuardarDatos" runat="server" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                    Text="GUARDAR" ToolTip="Guardar Etiqueta" OnClick="btnGuardarDatos_Click" />
                            </div>
                        </div>
                    </td>
                    <td style="vertical-align: top">
                        <div class="datos-estilo">
                            <div class="titulo lineaHorizontal" style="margin-left: 0px">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 95%">
                                            Catalgos: Etiquetas
                                        </td>
                                        <td>
                                            <img src="../App_Themes/GasMetropolitanoSkin/Imagenes/grid.png" alt="Consulta" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:GridView ID="grvEtiquetas" runat="server" AutoGenerateColumns="false" Width="100%"
                                CssClass="grvResultadoConsultaCss" AllowPaging="True" PageSize="8" OnPageIndexChanging="grvEtiquetas_PageIndexChanging"
                                OnRowDataBound="grvEtiquetas_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" SortExpression="colIdEtiqueta" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGVIdEtiqueta" runat="server" Text="<%# Bind('Id') %>" Width="50%"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Banco" SortExpression="colBanco">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGVBancor" runat="server" Text="<%# Bind('BancoDes') %>"
                                                Width="50%"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Descripcion" SortExpression="colDescripcion">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGVDescripcion" runat="server" Text="<%# Bind('Descripcion') %>"
                                                Width="50%"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tipo Dato" SortExpression="colTipoDato">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGVFFechar" runat="server" Text="<%# Bind('TipoDato') %>" Width="50%"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Columna Destino" SortExpression="colColumnaDestino">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGVColumnaDestino" runat="server" Text="<%# Bind('ColumnaDestino') %>" Width="50%"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Concatena" SortExpression="colConcatena">
                                        <ItemTemplate>
                                               <asp:CheckBox runat="server" ID="chkGVConcatena" Checked="<%# Bind('ConcatenaEtiqueta') %>" Enabled="False"/>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" ImageAlign="Middle" runat="server" OnClick="btnDelete_Click"
                                                OnClientClick='<%# "return confirm(\"¿Desea eliminar el registro,  "+ Eval("Descripcion").ToString() +  "  ?\");" %>'
                                                ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/DesconciliarActivo.png" CssClass="icono bg-color-grisClaro01"
                                                Width="15px" Heigth="15px" EnableViewState="true" ToolTip="ELIMINAR" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
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
