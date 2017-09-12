<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true"
    CodeFile="ConsultarPlantilla.aspx.cs" Inherits="Plantillas_ConsultarPlantilla" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    CONSULTAR PLANTILLAS
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager runat="server" ID="smPlantillas">
    </asp:ScriptManager>
    <table style="width: 100%">
        <tr>
            <td style="width: 300px; vertical-align: top">
                <div class="Filrado">
                    <asp:UpdatePanel ID="upConsultaConciliacion" runat="server">
                        <ContentTemplate>
                            <div class="tiraAmarilla">
                            </div>
                            <div class="titulo">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 85%">
                                            Filtro
                                        </td>
                                        <td>
                                            <img src="../App_Themes/GasMetropolitanoSkin/Imagenes/Filtro.png" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="datos-estilo">
                                <div class="etiqueta">
                                    Empresa
                                </div>
                                <asp:DropDownList ID="ddlEmpresa" runat="server" Width="100%" CssClass="dropDown"
                                    AutoPostBack="True" OnDataBound="ddlEmpresa_DataBound" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvEmpresa" runat="server" ErrorMessage=" Especifique la empresa. "
                                    ControlToValidate="ddlEmpresa" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                    ValidationGroup="Conciliacion"></asp:RequiredFieldValidator>
                                <div class="etiqueta">
                                    Sucursal</div>
                                <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="dropDown" Width="100%"
                                    AutoPostBack="True" OnDataBound="ddlSucursal_DataBound" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvSucursal" runat="server" ControlToValidate="ddlSucursal"
                                    ErrorMessage=" Especifique la Sucursal." Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                    ValidationGroup="Plantilla"></asp:RequiredFieldValidator>
                                <div class="etiqueta">
                                    Banco</div>
                                <asp:DropDownList ID="ddlBanco" runat="server" Width="100%" AutoPostBack="True" CssClass="dropDown"
                                    OnDataBound="ddlBanco_DataBound" OnSelectedIndexChanged="ddlBanco_SelectedIndexChanged">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvBanco" runat="server" ControlToValidate="ddlBanco"
                                    ErrorMessage=" Especifique el Banco." Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                    ValidationGroup="Plantilla">
                                </asp:RequiredFieldValidator>
                                <div class="etiqueta">
                                    Cuenta Bancaria</div>
                                <asp:DropDownList ID="ddlCuentaBancaria" runat="server" Width="100%" AutoPostBack="True"
                                    CssClass="dropDown">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvCuentaBancaria" runat="server" ControlToValidate="ddlCuentaBancaria"
                                    ErrorMessage=" Especifique la Cuenta Bancaria." Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                    ValidationGroup="Plantilla">
                                </asp:RequiredFieldValidator>
                                <div class="etiqueta">
                                    Tipo Fuente Información</div>
                                <asp:DropDownList ID="ddlTipoFuenteInformacion" runat="server" Width="100%" AutoPostBack="True"
                                    CssClass="dropDown">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvTipoFuenteInfo" runat="server" ControlToValidate="ddlTipoFuenteInformacion"
                                    ErrorMessage=" Especifique el Tipo de Fuente de Información." Font-Size="10px"
                                    CssClass="etiqueta fg-color-rojo" ValidationGroup="Plantilla"></asp:RequiredFieldValidator>
                                <div class="centradoMedio">
                                    <asp:Button ID="btnConsultar" runat="server" Text="CONSULTAR" ValidationGroup="Plantilla"
                                        CssClass="boton fg-color-blanco bg-color-azulClaro" 
                                        onclick="btnConsultar_Click" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </td>
            <td style="vertical-align: top">
                <div class="datos-estilo" style="margin-right: 15px">
                    <div class="titulo" style="margin-left: 0px">
                        <table width="100%">
                            <tr>
                                <td style="width: 95%">
                                    Plantillas
                                </td>
                                <td>
                                    <img src="../App_Themes/GasMetropolitanoSkin/Imagenes/grid.png" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="linea">
                    </div>
                    <br />
                    <asp:GridView ID="grvFuenteInformacion" runat="server" AutoGenerateColumns="False"
                        Width="100%" BorderStyle="Dotted" AllowPaging="true" PageSize="10" PagerSettings-Mode="NextPreviousFirstLast"
                        PagerSettings-Position="Bottom" PagerSettings-Visible="true" PagerSettings-PageButtonCount="5"
                        PagerStyle-HorizontalAlign="Center" PagerSettings-FirstPageText="Página inicial"
                        PagerSettings-LastPageText="Página final" PagerSettings-NextPageText="Página siguiente"
                        PagerSettings-PreviousPageText="Página anterior" ShowHeader="true" ShowHeaderWhenEmpty="True"
                        CssClass="grvResultadoConsultaCss">
                        <EmptyDataTemplate>
                            <asp:Label ID="lblvacio" runat="server" Font-Bold="True" Font-Overline="False" ForeColor="#CC3300"
                                Text="No existe ninguna Fuente de Información de acuerdo a los parametros del filtro."></asp:Label>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="GridHeader" />
                        <RowStyle CssClass="GridRow" />
                        <AlternatingRowStyle CssClass="GridAlternateRow" BackColor="Silver" />
                        <PagerSettings FirstPageText="Página inicial" LastPageText="Página final" Mode="NextPreviousFirstLast"
                            NextPageText="Página siguiente" PageButtonCount="5" PreviousPageText="Página anterior" />
                        <PagerStyle CssClass="GridPager" />
                        <Columns>
                            <asp:TemplateField HeaderText="Folio" SortExpression="colFolio">
                                <ItemTemplate>
                                    <asp:Label ID="lblFolio" runat="server" Text="<%# Bind('RutaArchivo') %>"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cuenta Bancaria" SortExpression="CuentaBancaria">
                                <ItemTemplate>
                                    <asp:Label ID="lblCuentaBancaria" runat="server" Text="<%# Bind('CuentaBancoFinanciero') %>"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo</br>Fuente</br>Información" SortExpression="tipoFuenteInfo">
                                <ItemTemplate>
                                    <asp:Label ID="lblTipoFuenteInformacion" runat="server" Text="<%# Bind('TipoFuenteInformacionDes') %>"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo</br>Fuente" SortExpression="tipoFuente">
                                <ItemTemplate>
                                    <asp:Label ID="lblTipoFuente" runat="server" Text="<%# Bind('TipoFuenteDes') %>"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo</br>Archivo" SortExpression="tipoArchivo">
                                <ItemTemplate>
                                    <asp:Label ID="lblTipoArchivo" runat="server" Text="<%# Bind('TipoArchivo') %>"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
