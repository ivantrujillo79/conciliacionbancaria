<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true"
    CodeFile="MotivoNoConcilidado.aspx.cs" Inherits="Catalogos_MotivoNoConcilidado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="width: 300px; vertical-align: top">
                <br />
                <div class="Filtrado">
                    <%--<asp:UpdatePanel ID="upConsultaConciliacion" runat="server">
                        <ContentTemplate>--%>
                    <div class="tiraVerdeClaro">
                    </div>
                    <div class="titulo">
                        <table width="100%">
                            <tr>
                                <td style="width: 85%">
                                    Nuevo <small style="font-size: 10px">Motivo No Conciliado</small>
                                </td>
                                <td>
                                    <asp:Image ID="imgNuevoMNC" runat="server" CssClass="icono bg-color-verdeClaro" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Agregar.png"
                                        Width="20px" Heigth="20px" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="datos-estilo">
                        <div class="etiqueta">
                            Descripción
                        </div>
                        <asp:TextBox ID="txtDescripcion" runat="server" MaxLength="100" TextMode="MultiLine"
                            Rows="4" CssClass="cajaTexto" Width="95%" Style="resize: none"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ErrorMessage=" Especifique alguna descripción para el nuevo motivo no conciliado."
                            ControlToValidate="txtDescripcion" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                            ValidationGroup="MotivoNoConciliado"></asp:RequiredFieldValidator>
                        <div class="centradoMedio">
                            <br />
                            <asp:Button ID="btnGuardar" runat="server" Text="GUARDAR" ToolTip="Guardar el nuevo motivo "
                                ValidationGroup="MotivoNoConciliado" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                OnClick="btnAgregar_Click1" />
                        </div>
                    </div>
                    <%-- </ContentTemplate>
                    </asp:UpdatePanel>--%>
                </div>
            </td>
            <td style="vertical-align: top">
                <div class="datos-estilo" style="margin-right: 15px; margin-left: 15px">
                    <div class="titulo" style="margin-left: 0px">
                        <table width="100%">
                            <tr>
                                <td style="width: 95%">
                                    Catalogo : Motivos No Conciliados
                                </td>
                                <td>
                                    <img src="../App_Themes/GasMetropolitanoSkin/Imagenes/grid.png" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="lineaHorizontal">
                    </div>
                    <br />
                    <asp:GridView ID="grdMotivos" runat="server" AutoGenerateColumns="False" Width="100%"
                        BorderStyle="Dotted" AllowPaging="True" OnRowDataBound="grdMotivos_RowDataBound"
                        PageSize="10" OnRowCommand="grdMotivos_RowCommand" OnPageIndexChanging="grdMotivos_PageIndexChanging"
                        ShowHeaderWhenEmpty="True" DataKeyNames="MotivoNoConciliadoId, Status" CssClass="grvResultadoConsultaCss">
                        <EmptyDataTemplate>
                            <asp:Label ID="lblvacio" runat="server" Font-Bold="True" Font-Overline="False" ForeColor="#CC3300"
                                Text="No existen motivos de no conciliacion"></asp:Label>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="GridHeader" />
                        <RowStyle CssClass="GridRow" />
                        <AlternatingRowStyle CssClass="bg-color-grisClaro01" />
                        <Columns>
                            <asp:TemplateField HeaderText="Clave" SortExpression="colClave">
                                <ItemTemplate>
                                    <asp:Label ID="lblClave" runat="server" Text="<%# Bind('MotivoNoConciliadoId') %>"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Descripción" SortExpression="colDescripcion">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescripcion" runat="server" Text="<%# Bind('Descripcion') %>"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="65%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" SortExpression="colStatus">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text="<%# Bind('Status') %>" Visible="False"></asp:Label>
                                    <asp:Button runat="server" ID="btnStatus" Width="15px" CommandName="CAMBIARSTATUS"
                                        Height="15px" Style="padding: 0px 0px 0px 0px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="20%" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerTemplate>
                            Página
                            <asp:DropDownList ID="paginasDropDownList" Font-Size="12px" AutoPostBack="true" runat="server"
                                OnSelectedIndexChanged="paginasDropDownList_SelectedIndexChanged" CssClass="dropDown"
                                Width="60px">
                            </asp:DropDownList>
                            de
                            <asp:Label ID="lblTotalNumPaginas" runat="server" CssClass="etiqueta fg-color-blanco"  />
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
                <br />
            </td>
        </tr>
    </table>
</asp:Content>
