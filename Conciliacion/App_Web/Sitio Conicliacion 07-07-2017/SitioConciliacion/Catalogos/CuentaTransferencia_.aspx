<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true"
    CodeFile="CuentaTransferencia_.aspx.cs" Inherits="Catalogos_CuentaTransferencia_" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style2
        {
            width: 238px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../App_Scripts/jsUpdateProgress.js" type="text/javascript"></script>
    <%--<script src="../../App_Scripts/jsUpdateProgress.js" type="text/javascript"></script>--%>
    <script type="text/javascript" language="javascript">
        var ModalProgress = '<%= mpeLoading.ClientID %>';   
    </script>
    <asp:UpdatePanel runat="server" ID="upCuentaTransferencia" UpdateMode="Always">
        <ContentTemplate>
            <script type="text/javascript">

                function ShowModalPopup() {
                    $find("ModalBehaviour").show();
                }
                function HideModalPopup() {
                    $find("ModalBehaviour").hide();
                }
                function HideModalPopupInterno() {
                    $find("ModalBehaviourInterno").hide();
                }
            </script>
            <table style="width: 100%">
                <tr>
                    <td style="vertical-align: top" class="style2">
                        <br />
                        <div class="Filtrado">
                            <%-- </ContentTemplate>
                    </asp:UpdatePanel>--%>
                            <div class="tiraVerdeClaro">
                            </div>
                            <div class="titulo">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 85%">
                                            Filtro <small style="font-size: 10px">Transferencia</small>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgNuevoMNC" runat="server" CssClass="icono bg-color-verdeClaro" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/Filtro.png"
                                                Width="20px" Heigth="20px" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="datos-estilo">
                                <div class="etiqueta">
                                    Corporativo Origen</div>
                                <div class="etiqueta">
                                    <asp:DropDownList ID="cboCorporativoOrigen" runat="server" CssClass="dropDown" AutoPostBack="True"
                                        Width="255px" OnDataBound="cboCorporativoOrigen_DataBound" OnSelectedIndexChanged="cboCorporativoOrigen_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="frvCorporativoOrigen" runat="server" ErrorMessage=" Seleccione un corporativo."
                                        ControlToValidate="cboCorporativoOrigen" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                        ValidationGroup="CuentaTransferencia"></asp:RequiredFieldValidator>
                                    <div class="etiqueta">
                                        Sucursal Origen
                                    </div>
                                    <asp:DropDownList ID="cboSucursalOrigen" runat="server" CssClass="dropDown" Width="255px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="frvSucursalOrigen" runat="server" ErrorMessage=" Seleccione una sucursal."
                                        ControlToValidate="cboSucursalOrigen" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                        ValidationGroup="CuentaTransferencia"></asp:RequiredFieldValidator>
                                    <div class="etiqueta">
                                        Banco Origen</div>
                                    <asp:DropDownList ID="cboNombreBancoOrigen" runat="server" CssClass="dropDown" Width="255px"
                                        AutoPostBack="True" OnSelectedIndexChanged="cboNombreBancoOrigen_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBancoOrigen" runat="server" ErrorMessage=" Seleccione un banco."
                                        ControlToValidate="cboNombreBancoOrigen" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                        ValidationGroup="CuentaTransferencia"></asp:RequiredFieldValidator>
                                    <div class="etiqueta">
                                        Cuenta Banco Origen</div>
                                    <asp:DropDownList ID="cboCuentaBancoOrigen" runat="server" CssClass="dropDown" Width="255px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="frvCuentaOrigen" runat="server" ErrorMessage=" Seleccione un numero de cuenta."
                                        ControlToValidate="cboCuentaBancoOrigen" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                        ValidationGroup="CuentaTransferencia"></asp:RequiredFieldValidator>
                                    <div class="centradoMedio">
                                        <br />
                                        <asp:Button ID="btnConsultar" runat="server" Text="CONSULTAR" ToolTip="Consultar las transferencias entre cuentas "
                                            ValidationGroup="CuentaTransferencia" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                            OnClick="btnConsultar_Click1" />
                                        <asp:Button ID="btnAgregar" runat="server" Text="AGREGAR" ToolTip="Agregar nuevas transferencias entre cuentas "
                                            CssClass="boton fg-color-blanco bg-color-naranja" OnClick="btnAgregar_Click1" />
                                    </div>
                                </div>
                                <%--Style="display: none"--%>
                            </div>
                        </div>
                    </td>
                    <td style="vertical-align: top">
                        <div class="datos-estilo" style="margin-right: 15px; margin-left: 15px">
                            <div class="titulo" style="margin-left: 0px">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 95%">
                                            Catálogo: Cuentas para Transferencias
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

                            <asp:GridView ID="grdTranferencias" runat="server" AutoGenerateColumns="False" Width="100%"
                                BorderStyle="Dotted" AllowPaging="True" PageSize="10" ShowHeaderWhenEmpty="True"
                                DataKeyNames="CuentaTransferenciaId" CssClass="grvResultadoConsultaCss" OnRowCommand="grdTranferencias_RowCommand"
                                OnRowDataBound="grdTranferencias_RowDataBound" OnPageIndexChanging="grdTranferencias_PageIndexChanging">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblvacio" runat="server" Font-Bold="True" Font-Overline="False" ForeColor="#CC3300"
                                        Text="No existen cuentas para consultar de acuerdo a los Parámetros del Filtrado"></asp:Label>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="GridHeader" />
                                <RowStyle CssClass="GridRow" />
                                <AlternatingRowStyle CssClass="bg-color-grisClaro01" />
                                <Columns>
                                    <%--<asp:TemplateField HeaderText="Cuenta Transferencia" SortExpression="colCuentaTransferencia">
                                <ItemTemplate>
                                    <asp:Label ID="lblCuentaTransferencia" runat="server" Text="<%# Bind('CuentaTransferenciaId') %>"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="5%" />
                            </asp:TemplateField>--%>
                                   <%-- <asp:TemplateField HeaderText="Coporativo Origen" SortExpression="colCorporativoOrigen">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCorporativoOrigen" runat="server" Text="<%# Bind('CorporativoOrigenDesc') %>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="True" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sucursal Origen" SortExpression="colSucursalOrigen">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSucursalOrigen" runat="server" Text="<%# Bind('SucursalOrigenDesc') %>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="True" Width="15%" />
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Banco Origen" SortExpression="colBancoNombreOrigen">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBancoNombreOrigen" runat="server" Text="<%# Bind('BancoNombreOrigen') %>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="True" Width="9%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cuenta Origen" SortExpression="colCuentaBancoOrigen">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCuentaBancoOrigen" runat="server" Text="<%# Bind('CuentaBancoOrigen') %>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="True" Width="12%" />
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Banco Origen" SortExpression="colBancoOrigen">
                                <ItemTemplate>
                                    <asp:Label ID="lblBancoOrigen" runat="server" Text="<%# Bind('BancoOrigen') %>"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="15%" />
                            </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Corporativo Destino" SortExpression="colCorporativoDestino">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCorporativoDestino" runat="server" Text="<%# Bind('CorporativoDestinoDesc') %>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="True" Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sucursal Destino" SortExpression="colSucursalDestino">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSucursalDestino" runat="server" Text="<%# Bind('SucursalDestinoDesc') %>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="True" Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Banco Destino" SortExpression="colBancoNombreDestino">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBancoNombreDestino" runat="server" Text="<%# Bind('BancoNombreDestino') %>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="True" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cuenta Destino" SortExpression="colCuentaBancoDestino">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCuentaBancoDestino" runat="server" Text="<%# Bind('CuentaBancoDestino') %>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="True" Width="12%" />
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Banco Destino" SortExpression="colBancoDestino">
                                <ItemTemplate>
                                    <asp:Label ID="lblBancoDestino" runat="server" Text="<%# Bind('BancoDestino') %>"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="15%" />
                            </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Status" SortExpression="colStatus">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text="<%# Bind('Status') %>" Visible="False"></asp:Label>
                                            <asp:Button runat="server" ID="btnStatus" Width="15px" CommandName="CAMBIARSTATUS"
                                                Height="15px" Style="padding: 0px 0px 0px 0px" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="True" Width="5%" />
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
                        <br />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="panelBloqueo" runat="server" AssociatedUpdatePanelID="upCuentaTransferencia">
        <ProgressTemplate>
            <asp:Image ID="imgLoad" runat="server" CssClass="icono bg-color-blanco" Height="40px"
                ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/LoadPage.gif" Width="40px" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ModalPopupExtender ID="mpeLoading" runat="server" BackgroundCssClass="ModalBackground"
        PopupControlID="panelBloqueo" TargetControlID="panelBloqueo">
    </asp:ModalPopupExtender>
    <asp:HiddenField runat="server" ID="hdfAgregarTransferencia" />
    <asp:ModalPopupExtender ID="popUpAgregarTransferencia" runat="server" PopupControlID="pnlAgregarTransferencia"
        TargetControlID="hdfAgregarTransferencia" BehaviorID="ModalBehaviour" BackgroundCssClass="ModalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlAgregarTransferencia" runat="server" BackColor="#FFFFFF" Width="50%">
        <%--Style="display: none"--%>
        <asp:UpdatePanel ID="upAgregarTransferencia" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr class="bg-color-grisOscuro">
                        <td colspan="4" style="padding: 5px 5px 5px 5px" class="etiqueta">
                            <div class="floatDerecha">
                                <asp:ImageButton runat="server" ID="imgCerrarImportar" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png"
                                    CssClass="iconoPequeño bg-color-rojo" OnClientClick="HideModalPopup();" /><%--OnClick="imgCerrarImportar_Click"--%>
                            </div>
                            <div class="fg-color-blanco">
                                AGREGAR NUEVA TRANSFERENCIA
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="datos-estilo" style="padding: 10px 10px 10px 10px">
                            <div class="etiqueta" align="center">
                                ORIGEN
                            </div>
                            <div class="etiqueta">
                                Corporativo
                            </div>
                            <asp:DropDownList ID="cboCorporativoOrigen_" runat="server" AutoPostBack="True" Width="100%"
                                CssClass="dropDown" OnDataBound="cboCorporativoOrigen__DataBound" OnSelectedIndexChanged="cboCorporativoOrigen__SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvCorporativoOrigen_" runat="server" ControlToValidate="cboCorporativoOrigen_"
                                ErrorMessage="Seleccione un corporativo." Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="AgregarCtaTransf"></asp:RequiredFieldValidator>
                            <div class="etiqueta">
                                Sucursal
                            </div>
                            <asp:DropDownList ID="cboSucursalOrigen_" runat="server" AutoPostBack="True" Width="100%"
                                CssClass="dropDown">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvSucursalOrigen_" runat="server" ControlToValidate="cboSucursalOrigen_"
                                ErrorMessage="Seleccione una Sucursal." Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="AgregarCtaTransf"></asp:RequiredFieldValidator>
                            <div class="etiqueta">
                                Nombre Banco
                            </div>
                            <asp:DropDownList ID="cboNombreBancoOrigen_" runat="server" AutoPostBack="True" Width="100%"
                                CssClass="dropDown" OnSelectedIndexChanged="cboNombreBancoOrigen__SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvBancoNombreOrigen_" runat="server" ControlToValidate="cboNombreBancoOrigen_"
                                ErrorMessage="Seleccione una nombre de banco origen." Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="AgregarCtaTransf"></asp:RequiredFieldValidator>
                            <div class="etiqueta">
                                Cuenta Banco
                            </div>
                            <asp:DropDownList ID="cboCuentaBancoOrigen_" runat="server" AutoPostBack="True" Width="100%"
                                CssClass="dropDown">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvCuentaBancoOrigen_" runat="server" ControlToValidate="cboCuentaBancoOrigen_"
                                ErrorMessage="Seleccione una cuenta de banco." Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="AgregarCtaTransf"></asp:RequiredFieldValidator>
                        </td>
                        <td class="datos-estilo" style="padding: 10px 10px 10px 10px; width: 50%">
                            <div class="etiqueta" align="center">
                                DESTINO
                            </div>
                            <div class="etiqueta">
                                Corporativo
                            </div>
                            <asp:DropDownList ID="cboCorporativoDestino_" runat="server" AutoPostBack="True"
                                Width="100%" CssClass="dropDown" OnDataBound="cboCorporativoDestino__DataBound"
                                OnSelectedIndexChanged="cboCorporativoDestino__SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvCorporativoDestino_" runat="server" ControlToValidate="cboCorporativoDestino_"
                                ErrorMessage="Seleccione un corporativo destino." Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="AgregarCtaTransf"></asp:RequiredFieldValidator>
                            <div class="etiqueta">
                                Sucursal
                            </div>
                            <asp:DropDownList ID="cboSucursalDestino_" runat="server" AutoPostBack="True" Width="100%"
                                CssClass="dropDown">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvSucursalDestino" runat="server" ControlToValidate="cboSucursalDestino_"
                                ErrorMessage="Seleccione una sucursal destino." Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="AgregarCtaTransf"></asp:RequiredFieldValidator>
                            <div class="etiqueta">
                                Nombre Banco
                            </div>
                            <asp:DropDownList ID="cboNombreBancoDestino_" runat="server" AutoPostBack="True"
                                Width="100%" CssClass="dropDown" OnSelectedIndexChanged="cboNombreBancoDestino__SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvNombreBancoDestino_" runat="server" ControlToValidate="cboNombreBancoDestino_"
                                ErrorMessage="Seleccione un banco." Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="AgregarCtaTransf"></asp:RequiredFieldValidator>
                            <div class="etiqueta">
                                Cuenta Banco
                            </div>
                            <asp:DropDownList ID="cboCuentaBancoDestino_" runat="server" AutoPostBack="True"
                                Width="100%" CssClass="dropDown">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="frvCuentaBancoDestino_" runat="server" ControlToValidate="cboCuentaBancoDestino_"
                                ErrorMessage="Seleccione una cuenta de banco." Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="AgregarCtaTransf"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="centradoMedio">
                            <asp:Button ID="btnGuardarInterno" runat="server" Text="GUARDAR" ToolTip="GUARDAR"
                                ValidationGroup="AgregarCtaTransf" CssClass="boton fg-color-blanco bg-color-verdeClaro"
                                OnClick="btnGuardarInterno_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="bg-color-grisClaro01" colspan="2">
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
