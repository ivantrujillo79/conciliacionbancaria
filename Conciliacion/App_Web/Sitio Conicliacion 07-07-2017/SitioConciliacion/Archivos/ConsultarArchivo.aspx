<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true"
    CodeFile="ConsultarArchivo.aspx.cs" Inherits="Archivos_Consultar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    CONSULTAR ARCHIVOS
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <link href="../App_Themes/GasMetropolitanoSkin/Sitio.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager runat="server" ID="smArchivos">
    </asp:ScriptManager>
    <script src="../App_Scripts/jsUpdateProgress.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        var ModalProgress = '<%= ModalProgress.ClientID %>';        
    </script>
    <asp:UpdatePanel ID="updPrincipal" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td style="width: 300px; vertical-align: top" class="Filtrado">
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
                                Empresa</div>
                            <asp:DropDownList ID="ddlEmpresa" runat="server" Width="100%" CssClass="dropDown"
                                AutoPostBack="True" OnDataBound="ddlEmpresa_DataBound" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvEmpresa" runat="server" ErrorMessage=" Especifique la empresa. "
                                ControlToValidate="ddlEmpresa" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="Archivos" Display="Dynamic"></asp:RequiredFieldValidator>
                            <div class="etiqueta">
                                Sucursal</div>
                            <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="dropDown" Width="100%">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvSucursal" runat="server" ErrorMessage=" Especifique la sucursal. "
                                ControlToValidate="ddlSucursal" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="Archivos" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                            <div class="etiqueta">
                                Banco</div>
                            <asp:DropDownList ID="cboBancoFinanciero" runat="server" CssClass="dropDown" OnSelectedIndexChanged="cboBancoFinanciero_SelectedIndexChanged"
                                Width="100%" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvBanco" runat="server" ErrorMessage=" Especifique la Banco. "
                                ControlToValidate="cboBancoFinanciero" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="Archivos" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                            <div class="etiqueta">
                                Cuenta</div>
                            <asp:DropDownList ID="cboCuentaFinanciero" runat="server" CssClass="dropDown" Width="100%">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvCuenta" runat="server" ErrorMessage=" Especifique la Cuenta bancaría. "
                                ControlToValidate="cboCuentaFinanciero" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="Archivos" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                            <div class="etiqueta">
                                Tipo Fuente Información</div>
                            <asp:DropDownList ID="ddlTipoFuente" runat="server" CssClass="dropDown" Width="100%">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvTipoFuente" runat="server" ErrorMessage=" Especifique el Tipo Fuente de Información. "
                                ControlToValidate="ddlTipoFuente" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="Archivos" Display="Dynamic"></asp:RequiredFieldValidator>
                            <div class="etiqueta">
                                Año</div>
                            <asp:DropDownList ID="ddlAnio" runat="server" CssClass="dropDown" Width="97%">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvAñoConciliacion" runat="server" ErrorMessage=" Especifique el Año de la Conciliación. "
                                ControlToValidate="ddlAnio" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="Archivos" Display="Dynamic"></asp:RequiredFieldValidator>
                            <div class="etiqueta">
                                Mes</div>
                            <asp:DropDownList ID="ddlMes" runat="server" CssClass="dropDown" Width="97%">
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
                            <div class="centradoMedio">
                                <asp:Button ID="btnConsultar" runat="server" Text="CONSULTAR" ValidationGroup="Archivos"
                                    CssClass="boton fg-color-blanco bg-color-azulClaro" ToolTip="CONSULTAR" OnClick="btnConsultar_Click" />
                            </div>
                        </div>
                    </td>
                    <td style="vertical-align: top">
                        <div class="datos-estilo" style="margin-right: 15px">
                            <div class="titulo" style="margin-left: 0px">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 95%" class="lineaHorizontal">
                                            Archivos
                                        </td>
                                        <td>
                                            <img src="../App_Themes/GasMetropolitanoSkin/Imagenes/grid.png" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <div class="centradoMedio">
                                <asp:GridView ID="grvTablaDestino" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                    Width="100%" DataKeyNames="Identificador" AllowSorting="True" CssClass="grvResultadoConsultaCss"
                                    PageSize="12" AllowPaging="True" OnPageIndexChanging="grvTablaDestino_PageIndexChanging"
                                    OnSelectedIndexChanging="grvTablaDestino_SelectedIndexChanging">
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="No existe ninguna conciliación de acuerdo a los Parámetros del Filtrado. "></asp:Label>
                                    </EmptyDataTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <RowStyle CssClass="bg-color-blanco fg-color-negro" />
                                    <SelectedRowStyle CssClass="bg-color-grisClaro fg-color-blanco"></SelectedRowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Descripción" SortExpression="Descripcion">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSecuencia" runat="server" Text='<%# Eval("Descripcion").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50%"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30%"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Detalle">
                                            <ItemTemplate>
                                                <asp:Button ID="btnVerDetalle" runat="server" CssClass="botonPequeño bg-color-azulClaro fg-color-blanco"
                                                    Text="DETALLE" CommandName="Select" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="10%"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="grvPaginacionScroll" />
                                </asp:GridView>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hdfDetalleOculto" runat="server" />
    <asp:ModalPopupExtender ID="mpeDetalleDestino" runat="server" TargetControlID="hdfDetalleOculto"
        PopupControlID="pnlDestinoDetalle" EnableViewState="False" BackgroundCssClass="ModalBackground"
        Enabled="True" BehaviorID="ModalBehaviour">
    </asp:ModalPopupExtender>
    <asp:Panel runat="server" ID="pnlDestinoDetalle" HorizontalAlign="Center" CssClass="ModalPopup"
        Style="display: none">
        <asp:UpdatePanel ID="upDestinoDetalle" runat="server">
            <ContentTemplate>
                <table style="width: 595px;">
                    <tr class="bg-color-grisOscuro">
                        <td colspan="6" style="padding: 5px 5px 5px 5px" class="etiqueta">
                            <div class="floatDerecha bg-color-grisClaro01">
                                <asp:ImageButton runat="server" ID="btnCerrarDetalle" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png"
                                    CssClass="iconoPequeño bg-color-rojo" OnClick="btnCerrarDetalle_Click" />
                            </div>
                            <div class="fg-color-blanco">
                                DETALLE FOLIO SELECCIONADO [<asp:Label runat="server" ID="lblFLSelec"></asp:Label>]
                            </div>
                        </td>
                    </tr>
                    <tr class="etiqueta">
                        <td class="centradoMedio" style="width: 5%">
                            Ver
                        </td>
                        <td class="centradoIzquierda" style="width: 95%">
                            <asp:DropDownList runat="server" ID="ddlConfiguracionDetalle" CssClass="dropDown"
                                AutoPostBack="True" Width="100px" OnSelectedIndexChanged="ddlConfiguracionDetalle_SelectedIndexChanged">
                                <asp:ListItem Value="0">10</asp:ListItem>
                                <asp:ListItem Value="1">Todos</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="centradoMedio bg-color-grisClaro fg-color-blanco ">
                            Total Deposito:
                        </td>
                        <td class="centradoMedio bg-color-naranja fg-color-blanco">
                            <b>
                                <asp:Label ID="lblTotalDeposito" runat="server"></asp:Label></b>
                        </td>
                        <td class="centradoMedio bg-color-grisClaro fg-color-blanco">
                            Total Retiro:
                        </td>
                        <td class="centradoMedio bg-color-naranja fg-color-blanco">
                            <b>
                                <asp:Label ID="lblTotalRetiro" runat="server"></asp:Label></b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <asp:GridView ID="grvDestinoDetalle" runat="server" AutoGenerateColumns="False" CssClass="grvResultadoConsultaCss"
                                ShowHeaderWhenEmpty="False" PageSize="15" GridLines="Horizontal" Width="100%"
                                DataKeyNames="Folio" AllowPaging="True" OnPageIndexChanging="grvDestinoDetalle_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="Referencia" SortExpression="Referencia">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReferencia" runat="server" Text="<%# Bind('Referencia') %>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                        <ItemStyle HorizontalAlign="Center" Width="100px" BackColor="Silver" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FOperacion" SortExpression="FOperacion">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFOperacion" runat="server" Text='<%# Eval("FOperacion","{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FMovimiento" SortExpression="FMovimiento">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFMovimiento" runat="server" Text='<%# Eval("FMovimiento","{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Deposito" SortExpression="Deposito">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeposito" runat="server" Text='<%# Eval("Deposito","{0:c2}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Retiro" SortExpression="Retiro">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRetiro" runat="server" Text='<%# Eval("Retiro","{0:c2}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Concepto" SortExpression="Concepto">
                                        <ItemTemplate>
                                            <div class="parrafoTexto" style="width: 400px">
                                                <asp:Label ID="lblConcepto" runat="server" Text='<%# Eval("Concepto") %>'></asp:Label>
                                            </div>
                                            <asp:HoverMenuExtender ID="hmeConcepto" runat="server" TargetControlID="lblConcepto"
                                                PopupControlID="pnlPopUpConcepto" PopDelay="20" OffsetX="-10" OffsetY="0">
                                            </asp:HoverMenuExtender>
                                            <asp:Panel ID="pnlPopUpConcepto" runat="server" CssClass="grvResultadoConsultaCss ocultar"
                                                Width="400px" Style="padding: 5px 5px 5px 5px" BackColor="White" Wrap="True">
                                                <asp:Label ID="lblToolTipConcepto" runat="server" Text='<%# Eval("Concepto") %>'
                                                    CssClass="etiqueta " Font-Size="10px" />
                                            </asp:Panel>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Justify" Width="400px"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Width="400px"></HeaderStyle>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="grvPaginacionScroll" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:UpdateProgress ID="panelBloqueo" runat="server" AssociatedUpdatePanelID="updPrincipal">
        <ProgressTemplate>
            <asp:Image ID="imgLoad" runat="server" CssClass="icono bg-color-blanco" Height="40px"
                ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/LoadPage.gif" Width="40px" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ModalPopupExtender ID="ModalProgress" runat="server" PopupControlID="panelBloqueo"
        BackgroundCssClass="ModalBackground" TargetControlID="panelBloqueo">
    </asp:ModalPopupExtender>
</asp:Content>
