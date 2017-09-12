<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true"
    CodeFile="ReferenciaAComparar.aspx.cs" Inherits="Catalogos_ReferenciaAComparar"
    EnableEventValidation="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    REFERENCIAS A COMPARAR
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager ID="spRefComp" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%">
        <tr>
            <td style="width: 300px; vertical-align: top">
                <div class="Filtrado">
                    <div class="tiraAmarilla">
                    </div>
                    <div class="titulo">
                        Datos Generales
                    </div>
                    <div class="datos-estilo">
                        <div class="etiqueta">
                            Tipo Conciliación</div>
                        <asp:DropDownList ID="ddlTipoConciliacion" runat="server" Width="100%" CssClass="dropDown">
                        </asp:DropDownList>
                        <br />
                        <div class="etiqueta">
                            Columna Destino Externa</div>
                        <asp:DropDownList ID="ddlColumnaDestExt" runat="server" Width="100%" CssClass="dropDown">
                        </asp:DropDownList>
                        <br />
                        <div class="etiqueta">
                            Columna Destino Interna</div>
                        <asp:DropDownList ID="ddlColumnaDestInt" runat="server" CssClass="dropDown" Width="100%">
                        </asp:DropDownList>
                        <br />
                        <div class="centradoMedio">
                            <asp:Button ID="btnGuardar" runat="server" OnClick="btnAgregar_Click1" Text="GUARDAR"
                                ToolTip="Guardar la referencia nueva" CssClass="boton fg-color-blanco bg-color-azulClaro" />
                        </div>
                    </div>
                </div>
            </td>
            <td style="vertical-align: top">
                <div class="datos-estilo">
                    <div class="titulo" style="margin-left: 0px">
                        <table width="100%">
                            <tr>
                                <td style="width: 95%">
                                    Catalogo: Referencias a Comparar
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
                    <asp:GridView ID="grdReferencias" runat="server" AutoGenerateColumns="False" Width="100%"
                        BorderStyle="Dotted" AllowPaging="True" OnPageIndexChanging="grdReferencias_PageIndexChanging"
                        ShowHeaderWhenEmpty="True" DataKeyNames="TipoConciliacion,Secuencia,Status" OnRowCommand="grdReferencias_RowCommand"
                        OnRowDataBound="grdReferencias_RowDataBound" CssClass="grvResultadoConsultaCss">
                        <EmptyDataTemplate>
                            <asp:Label ID="lblvacio" runat="server" Font-Bold="True" Font-Overline="False" ForeColor="#CC3300"
                                Text="No existen referencias"></asp:Label>
                        </EmptyDataTemplate>
                          <AlternatingRowStyle CssClass="bg-color-grisClaro01" />
                        <Columns>
                            <%-- <asp:CommandField SelectText="-&gt;" ShowSelectButton="True">
                            <ItemStyle Width="5%" />
                        </asp:CommandField>--%>
                            <asp:TemplateField HeaderText="TipoConciliacion" SortExpression="TipoConciliacionDescripcion">
                                <ItemTemplate>
                                    <asp:Label ID="lblClave" runat="server" Text="<%# Bind('TipoConciliacionDescripcion') %>"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="300px" />
                                <ItemStyle Width="300px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Secuencia" SortExpression="Secuencia">
                                <ItemTemplate>
                                    <asp:Label ID="lblClave2" runat="server" Text="<%# Bind('Secuencia') %>"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ColumnaDestinoExt" SortExpression="ColumnaDestinoExt">
                                <ItemTemplate>
                                    <asp:Label ID="lblColumnaDestinoExt" runat="server" Text="<%# Bind('ColumnaDestinoExt') %>"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center"  Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ColumnaDestinoInt" SortExpression="ColumnaDestinoInt">
                                <ItemTemplate>
                                    <asp:Label ID="lblColumnaDestinoInt" runat="server" Text="<%# Bind('ColumnaDestinoInt') %>"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text="<%# Bind('Status') %>" Style="display: none"></asp:Label>
                                    <asp:Button runat="server" ID="btnStatus" Width="15px" CommandName="CAMBIARSTATUS"
                                        Height="15px" Style="padding: 0px 0px 0px 0px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
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
</asp:Content>
