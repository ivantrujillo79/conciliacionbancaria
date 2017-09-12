<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="ReporteConciliacionII.aspx.cs" Inherits="ReportesConciliacion_ReporteConciliacionII" %>


<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="Server">
    Reporte Tesoreria II
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <%--<asp:UpdatePanel ID="upReportes">
        <ContentTemplate>--%>
    <table style="width: 100%">
        <tr>
            <td style="width: 250px; vertical-align: top" class="Filtrado">
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
                        ValidationGroup="Reporte"></asp:RequiredFieldValidator>
                    <div class="etiqueta">
                        Sucursal
                    </div>
                    <asp:DropDownList ID="ddlSucursal" runat="server" Width="100%" CssClass="dropDown"
                        AutoPostBack="False">
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="rfSucursal" runat="server" ErrorMessage=" Especifique la sucursal. "
                        ControlToValidate="ddlSucursal" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                        ValidationGroup="Reporte"></asp:RequiredFieldValidator>
                    <div class="etiqueta">
                        Banco
                    </div>
                    <asp:DropDownList ID="ddlBanco" runat="server" Width="100%" CssClass="dropDown" AutoPostBack="True"
                        OnDataBound="ddlBanco_DataBound" OnSelectedIndexChanged="ddlBanco_SelectedIndexChanged">
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvBanco" runat="server" ErrorMessage=" Especifique el Banco. "
                        ControlToValidate="ddlBanco" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                        ValidationGroup="Reporte"></asp:RequiredFieldValidator>
                    <div class="etiqueta">
                        Cuenta Bancaría
                    </div>
                    <asp:DropDownList ID="ddlCuentaBancaria" runat="server" Width="100%" CssClass="dropDown"
                        AutoPostBack="False">
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvCuentaBancaria" runat="server" ErrorMessage=" Especifique la Cuenta Bancaría. "
                        ControlToValidate="ddlCuentaBancaria" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                        ValidationGroup="Reporte"></asp:RequiredFieldValidator>
                    <div class="etiqueta">
                        Año
                    </div>
                    <asp:DropDownList ID="ddlAño" runat="server" CssClass="dropDown" Width="100%" AutoPostBack="False">
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvAño" runat="server" ErrorMessage=" Especifique el Año. "
                        ControlToValidate="ddlAño" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                        ValidationGroup="Reporte"></asp:RequiredFieldValidator>
                    <div class="etiqueta">
                        Mes
                    </div>
                    <asp:DropDownList ID="ddlMes" runat="server" Width="100%" CssClass="dropDown" AutoPostBack="False">
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
                    <asp:RequiredFieldValidator ID="rfvMes" runat="server" ErrorMessage=" Especifique el Mes."
                        ControlToValidate="ddlMes" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                        ValidationGroup="Reporte"></asp:RequiredFieldValidator>
                    <div class="centradoMedio">
                        <asp:Button ID="btnConsultar" runat="server" CssClass="boton fg-color-blanco bg-color-azulClaro"
                            Text="CONSULTAR" ToolTip="CONSULTAR" ValidationGroup="Reporte" />
                    </div>
                </div>
            </td>
            <td style="vertical-align: top">
                <div class="datos-estilo">
                    <div class="titulo" style="margin-left: 0px">
                        <table width="100%">
                            <tr>
                                <td style="width: 95%">
                                    Resultado
                                </td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" CssClass="boton fg-color-blanco bg-color-verdeFuerte"
                                        Text="EXPORTAR" ToolTip="EXPORTAR" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="lineaHorizontal">
                        
                    </div>
                    <br />
                    <asp:GridView ID="grvReporte" runat="server" AutoGenerateColumns="False" Width="100%"
                        AllowPaging="True" ShowHeaderWhenEmpty="True" CssClass="grvResultadoConsultaCss"
                        DataKeyNames="FolioConciliacion" PageSize="12" OnPageIndexChanging="grvReporten_PageIndexChanging"
                        OnRowDataBound="grvReporte_RowDataBound" AllowSorting="True" OnSorting="grvReporte_Sorting">
                        <EmptyDataTemplate>
                            <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="No existe ninguna conciliación de acuerdo a los Parámetros del Filtrado. "></asp:Label>
                        </EmptyDataTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:TemplateField HeaderText="Secuencia" SortExpression="Secuencia">
                                <ItemTemplate>
                                    <asp:Label ID="lblSecuencia" runat="server" Text='<%# Eval("Secuencia") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle CssClass="centradoMedio"></ControlStyle>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" BackColor="#ebecec" ForeColor="Black"
                                    CssClass="centradoMedio" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha" SortExpression="Fecha">
                                <ItemTemplate>
                                    <asp:Label ID="lblFecha" runat="server" Text='<%# Eval("Fecha", "{0:d}") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle CssClass="centradoMedio"></ControlStyle>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" CssClass="centradoMedio" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo" SortExpression="Tipo">
                                <ItemTemplate>
                                    <asp:Label ID="lblTipo" runat="server" Text='<%# Eval("Tipo") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle CssClass="centradoMedio"></ControlStyle>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="True" CssClass="centradoMedio" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Autorizacion" SortExpression="Autorizacion">
                                <ItemTemplate>
                                    <asp:Label ID="lblAutorizacion" runat="server" Text='<%# Eval("Autorizacion") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Caja" SortExpression="Caja">
                                <ItemTemplate>
                                    <asp:Label ID="lblSucursalDes" runat="server" Text='<%# Eval("Caja") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sucursal" SortExpression="Sucursal">
                                <ItemTemplate>
                                    <asp:Label ID="lblSucursalDes" runat="server" Text='<%# Eval("Sucursal") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Referencia" SortExpression="Referencia">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblStatusConciliacion" Text='<%# Bind("Referencia") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Retiro" SortExpression="Retiro">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblRetiro" Text='<%# Bind("Retiro") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Deposito" SortExpression="Deposito">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDeposito" Text='<%# Bind("Deposito") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Concepto Interno" SortExpression="ConceptoInterno">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblConceptoInterno" Text='<%# Eval("Concepto Interno") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status Concepto" SortExpression="Status Concepto">
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="ddlStatusConcepto" Width="100%" CssClass="dropDown"
                                        AutoPostBack="True" />
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
    <%--  </ContentTemplate>
    </asp:UpdatePanel>   --%>
</asp:Content>