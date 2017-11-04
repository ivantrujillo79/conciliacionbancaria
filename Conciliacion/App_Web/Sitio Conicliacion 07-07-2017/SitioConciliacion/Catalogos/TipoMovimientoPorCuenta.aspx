<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true"
    CodeFile="TipoMovimientoPorCuenta.aspx.cs" Inherits="Catalogos_TipoMovimientoPorCuenta" %>

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
                                    Corporativo</div>
                                <div class="etiqueta">
                                    <asp:DropDownList ID="cboCorporativo" runat="server" CssClass="dropDown" AutoPostBack="True"
                                        Width="255px" OnDataBound="cboCorporativo_DataBound" OnSelectedIndexChanged="cboCorporativo_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="frvCorporativo" runat="server" ErrorMessage=" Seleccione un corporativo."
                                        ControlToValidate="cboCorporativo" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                        ValidationGroup="CuentaTransferencia"></asp:RequiredFieldValidator>
                                    <div class="etiqueta">
                                        Sucursal
                                    </div>
                                    <asp:DropDownList ID="cboSucursal" runat="server" CssClass="dropDown" Width="255px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="frvSucursal" runat="server" ErrorMessage=" Seleccione una sucursal."
                                        ControlToValidate="cboSucursal" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                        ValidationGroup="CuentaTransferencia"></asp:RequiredFieldValidator>
                                    <div class="etiqueta">
                                        Banco</div>
                                    <asp:DropDownList ID="cboNombreBanco" runat="server" CssClass="dropDown" Width="255px"
                                        AutoPostBack="True" OnSelectedIndexChanged="cboNombreBanco_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBanco" runat="server" ErrorMessage=" Seleccione un banco."
                                        ControlToValidate="cboNombreBanco" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                        ValidationGroup="CuentaTransferencia"></asp:RequiredFieldValidator>
                                    <div class="etiqueta">
                                        Cuenta Banco</div>
                                    <asp:DropDownList ID="cboCuentaBanco" runat="server" CssClass="dropDown" Width="255px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="frvCuenta" runat="server" ErrorMessage=" Seleccione un numero de cuenta."
                                        ControlToValidate="cboCuentaBanco" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                        ValidationGroup="CuentaTransferencia"></asp:RequiredFieldValidator>
                                    <div class="centradoMedio">
                                        <br />
                                        <asp:Button ID="btnConsultar" runat="server" Text="CONSULTAR" ToolTip="Consultar los extractores por cuenta "
                                            ValidationGroup="CuentaTransferencia" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                            OnClick="btnConsultar_Click1" />
                                        <asp:Button ID="btnAgregar" runat="server" Text="AGREGAR" ToolTip="Agregar nuevo extractor a la cuentas "
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
                                            Catálogo: Tipo de Movimiento Por Cuenta
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
                            <asp:GridView ID="grdExtractores" runat="server" Width="100%"
                                BorderStyle="Dotted" AllowPaging="True" ShowHeaderWhenEmpty="True"
                                DataKeyNames="Identificador" CssClass="grvResultadoConsultaCss" OnRowCommand="grdExtractores_RowCommand"
                                OnRowDataBound="grdExtractores_RowDataBound" OnPageIndexChanging="grdExtractores_PageIndexChanging" AutoGenerateColumns="False" OnSelectedIndexChanged="grdExtractores_SelectedIndexChanged">
                                <Columns>
                                    <asp:BoundField DataField="Identificador" HeaderText="Identificador" />
                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                                    <asp:TemplateField HeaderText="Eliminar">
                                            
                                         <%-- <ItemTemplate>
                                            <asp:Button ID="btEliminar" width="80px"  Text="Eliminar" CommandName="EliminaRegistro" 
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server" />
                                            </ItemTemplate--%>
                                            <ItemTemplate>                                           
                                            <asp:Button ID="btEliminar" CssClass="boton activo" runat="server" Width="15px" CommandName="EliminaRegistro" 
                                                 CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" 
                                                Height="15px" Style=" padding: 0px 0px 0px 0px" />
                                                 </ItemTemplate>


                                            <ItemStyle HorizontalAlign="Center"   Width="100px"/>
                                    </asp:TemplateField>


                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblvacio" runat="server" Font-Bold="True" Font-Overline="False" ForeColor="#CC3300"
                                        Text="No existen extractores para consultar de acuerdo a los Parámetros del Filtrado"></asp:Label>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="GridHeader" />
                                <RowStyle CssClass="GridRow" />
                                <AlternatingRowStyle CssClass="bg-color-grisClaro01" />
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
    <asp:Panel ID="pnlAgregarTransferencia" runat="server" BackColor="#FFFFFF" Width="30%">
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
                                AGREGAR NUEVO TIPO MOVIMIENTO
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="datos-estilo" style="padding: 10px 10px 10px 10px">
                            <div class="etiqueta" align="center">
                                
                            </div>
                            <div class="etiqueta">
                                Tipo Movimiento
                            </div>
                            <asp:DropDownList ID="cboTipoMovimientoNuevo" runat="server" AutoPostBack="True" Width="100%"
                                CssClass="dropDown"  >
                            </asp:DropDownList>                           
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
