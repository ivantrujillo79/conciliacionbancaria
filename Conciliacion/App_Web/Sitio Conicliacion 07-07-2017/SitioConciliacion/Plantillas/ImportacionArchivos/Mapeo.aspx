<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true"
    CodeFile="Mapeo.aspx.cs" Inherits="ImportacionArchivos_Mapeo" Title="Mapeo" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ MasterType TypeName="Principal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ToolkitScriptManager>
    <script type="text/javascript" src="../../App_Scripts/jsUpdateProgress.js"></script>
    <script type="text/javascript" language="javascript">
        function ShowModalPopup() {
            $find("ModalBehaviour").show();
        }
        function HideModalPopup() {
            $find("ModalBehaviour").hide();
        }
    </script>
    <script type="text/javascript" language="javascript">
        var ModalProgress = '<%:ModalProgress.ClientID %>';       
    </script>
    <asp:UpdatePanel ID="uppPrincipal" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td style="width: 300px; vertical-align: top" class="Filtrado">
                        <div class="tiraAmarilla">
                        </div>
                        <div class="titulo">
                            Datos Generales</div>
                        <div class="datos-estilo">
                            <div class="etiqueta">
                                Coporativo</div>
                            <asp:DropDownList ID="cboCorporativo" runat="server" CssClass="dropDown" Width="100%"
                                AutoPostBack="True" OnDataBound="cboCorporativo_DataBound" OnSelectedIndexChanged="cboCorporativo_SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                            <div class="etiqueta">
                                Banco</div>
                            <asp:DropDownList ID="cboBancoFinanciero" runat="server" CssClass="dropDown" Width="100%"
                                AutoPostBack="True" OnSelectedIndexChanged="cboBancoFinanciero_SelectedIndexChanged"
                                OnDataBound="cboBancoFinanciero_DataBound">
                            </asp:DropDownList>
                            <br />
                            <div class="etiqueta">
                                Cuenta</div>
                            <asp:DropDownList ID="cboCuentaFinanciero" runat="server" CssClass="dropDown" Width="100%"
                                AutoPostBack="True" OnSelectedIndexChanged="cboCuentaFinanciero_SelectedIndexChanged">
                            </asp:DropDownList>
                            <div class="etiqueta">
                                Tipo Fuente</div>
                            <asp:DropDownList ID="cboTipoFuenteInformacion" runat="server" CssClass="dropDown"
                                Width="100%" AutoPostBack="True" OnSelectedIndexChanged="cboConsecutivo_SelectedIndexChanged"
                                OnDataBound="cboTipoFuenteInformacion_DataBound">
                            </asp:DropDownList>
                            <br />
                            <div class="etiqueta">
                                Tabla</div>
                            <asp:DropDownList ID="cboTabla" runat="server" CssClass="dropDown" Width="100%" AutoPostBack="True"
                                OnSelectedIndexChanged="cboTabla_SelectedIndexChanged" OnDataBound="cboTabla_DataBound">
                            </asp:DropDownList>
                            <br />
                            <div class="etiqueta" style="margin-bottom: 5px">
                                Mapeo</div>
                            <asp:TabContainer ID="tabNuevaConciliacion" TabStripPlacement="Top" runat="server"
                                ActiveTabIndex="0" Width="100%">
                                <asp:TabPanel runat="server" HeaderText="NUEVO" ID="tabNuevo" Width="100%">
                                    <ContentTemplate>
                                        <div class="etiqueta">
                                            Campo Destino</div>
                                        <asp:DropDownList ID="cboColumna" runat="server" CssClass="dropDown" Width="100%"
                                            OnDataBound="cboColumna_DataBound">
                                        </asp:DropDownList>
                                        <br />
                                        <div class="etiqueta">
                                            Campo Origen</div>
                                        <asp:DropDownList ID="cboColumnaOrigen" runat="server" CssClass="dropDown" Width="100%"
                                            OnDataBound="cboColumnaOrigen_DataBound">
                                        </asp:DropDownList>
                                        <br />
                                        <asp:CheckBox ID="chkTipoFecha" runat="server" Text="Fecha Documento" CssClass="etiqueta fg-color-rojo"
                                            Font-Bold="true" />
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel runat="server" HeaderText="EXISTENTE" ID="tabExistente" Width="100%">
                                    <ContentTemplate>
                                        <div class="etiqueta">
                                            Cuenta Bancaria Fuente</div>
                                        <asp:DropDownList ID="ddlCuentaBancariaFuente" runat="server" CssClass="dropDown"
                                            Width="100%" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:TabPanel>
                            </asp:TabContainer>
                            <div class="centradoMedio">
                                <asp:Button ID="btnCancelarDatos" runat="server" CssClass="boton fg-color-blanco bg-color-grisClaro"
                                    Text="CANCELAR" ToolTip="Cancelar el guardado de datos" OnClick="btnCancelarDatos_Click" />
                                <asp:Button ID="btnGuardarDatos" runat="server" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                    Text="GUARDAR" ToolTip="Guardar datos" OnClick="btnGuardarDatos_Click" />
                            </div>
                        </div>
                    </td>
                    <td style="vertical-align: top">
                        <div class="datos-estilo">
                            <div class="titulo lineaHorizontal" style="margin-left: 0px">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 95%">
                                            Mapeo
                                        </td>
                                        <td>
                                            <img src="../../App_Themes/GasMetropolitanoSkin/Imagenes/grid.png" alt="Consulta" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:GridView ID="grvMapeos" runat="server" AutoGenerateColumns="false" Width="100%"
                                CssClass="grvResultadoConsultaCss" PageSize="9" AllowPaging="True" OnPageIndexChanging="grvMapeos_PageIndexChanging"
                                OnRowDataBound="grvMapeos_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" SortExpression="colIdCuentaBanco" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="colIdCuentaBanco" runat="server" Text="<%# Bind('CuentaBancoFinanciero') %>"
                                                Width="50%"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Id" SortExpression="colIdBanco" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="colIdBanco" runat="server" Text="<%# Bind('BancoFinanciero') %>" Width="50%"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Id" SortExpression="colIdFuenteInformacion" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="colIdFuenteInformacion" runat="server" Text="<%# Bind('IdFuenteInformacion') %>"
                                                Width="50%"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Secuencia" SortExpression="colSecuencia">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGVSecuencia" runat="server" Text="<%# Bind('Secuencia') %>" Width="50%"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Origen" SortExpression="colOrigen">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGVOrigen" runat="server" Text="<%# Bind('ColumnaOrigen') %>" Width="50%"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tipo Fecha">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkEsTipoFecha" Checked="<%# Bind('EsTipoFecha') %>" Enabled="false"
                                                runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tabla Dest." SortExpression="coltablaDestino">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGVFTablaDestino" runat="server" Text="<%# Bind('TablaDestino') %>"
                                                Width="50%"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Destino" SortExpression="colDestino">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGVDestinor" runat="server" Text="<%# Bind('ColumnaDestino') %>"
                                                Width="50%"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnAgregarPatron" ImageAlign="Middle" runat="server" ImageUrl="../../App_Themes/GasMetropolitano/Imagenes/agregar.png"
                                                OnClick="btnAdd_Click" EnableViewState="true" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                        <ItemStyle HorizontalAlign="Center" Width="35px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" ImageAlign="Middle" runat="server" OnClick="btnDelete_Click"
                                                OnClientClick='<%# "return confirm(\"¿Desea eliminar el registro  "+ Eval("Secuencia").ToString() +  "  ?\");" %>'
                                                ImageUrl="../../App_Themes/GasMetropolitano/Imagenes/quitar.png" EnableViewState="true" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                        <ItemStyle HorizontalAlign="Center" Width="35px" />
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
                            <asp:Button ID="btnCerrarMapeo" runat="server" CssClass="boton fg-color-blanco bg-color-grisOscuro"
                                Text="CERRAR MAPEO" ToolTip="Cerrar mapeo" OnClick="btnCerrarMapeo_Click" Enabled="False" />
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField runat="server" ID="hdfDetalleEtiquetas" />
    <asp:ModalPopupExtender ID="popUpEtiquetas" runat="server" PopupControlID="pnlPopUpEtiquetas"
        TargetControlID="hdfDetalleEtiquetas" BehaviorID="ModalBehaviour" BackgroundCssClass="ModalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopUpEtiquetas" runat="server" BackColor="#FFFFFF" Width="50%"
        Style="display: none">
        <asp:UpdatePanel ID="UpdEtiquetas" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr class="bg-color-grisOscuro">
                        <td colspan="5" style="padding: 5px 5px 5px 5px" class="etiqueta">
                            <div class="fg-color-blanco centradoJustificado">
                                ETIQUETA
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 5px 5px 5px 5px; width: 100%">
                            <table id="tblEtiquetas" border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr style="height: 26px; vertical-align: middle;">
                                    <td style="text-indent: 10px; width: 10%;" align="center">
                                        <asp:Label ID="lblEtiqueta" runat="server" Text="Etiqueta:" EnableViewState="false"
                                            CssClass="etiqueta"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cboEtiqueta" runat="server" CssClass="dropDown" Width="100%"
                                            OnDataBound="cboEtiqueta_DataBound">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr style="height: 26px; vertical-align: middle;">
                                    <td style="text-indent: 10px; width: 10%;" align="center">
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkLongitud" runat="server" Text="Sin Longitud Fija:" CssClass="etiqueta fg-color-rojo"
                                            Font-Bold="true" AutoPostBack="True" OnCheckedChanged="chkLongitud_CheckedChanged" />
                                    </td>
                                </tr>
                                <tr style="height: 26px; vertical-align: middle;">
                                    <td style="text-indent: 10px; width: 10%;" align="center">
                                        <asp:Label ID="lblLongitud" runat="server" Text="Longitud:" CssClass="etiqueta" EnableViewState="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLongitud" runat="server" Width="99%" CssClass="cajaTexto"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="height: 26px; vertical-align: middle;">
                                    <td style="text-indent: 10px; width: 10%;" align="center">
                                        <asp:Label ID="lblFinaliza" runat="server" Text="Finaliza:" EnableViewState="False"
                                            Visible="False" CssClass="etiqueta"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFinaliza" runat="server" Width="99%" Visible="False" CssClass="cajaTexto"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="centradoMedio" colspan="2">
                                        <asp:Button ID="btnGuardarEtiqueta" runat="server" Text="ACEPTAR" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                            OnClick="btnGuardarEtiqueta_Click"></asp:Button>
                                        <asp:Button ID="btnCancelar" runat="server" Text="CERRAR" CssClass="boton fg-color-blanco bg-color-grisClaro"
                                            OnClientClick="HideModalPopup();" OnClick="btnCancelarCargaPedidos_Click"></asp:Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:GridView ID="GridViewEtiquetas" runat="server" AutoGenerateColumns="false" Width="100%"
                                            CssClass="grvResultadoConsultaCss">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Id" SortExpression="colIdEtiqueta" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGVIdEtiqueta" runat="server" Text="<%# Bind('IdEtiqueta') %>" Width="50%"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Etiqueta" SortExpression="colEtiqueta">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGVEtiqueta" runat="server" Text="<%# Bind('Etiqueta.Descripcion') %>"
                                                            Width="50%"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Longitud" SortExpression="colLongitud">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGVLongitud" runat="server" Text="<%# Bind('LongitudFija') %>" Width="50%"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Finaliza" SortExpression="colFinaliza" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGVFinaliza" runat="server" Text="<%# Bind('Finaliza') %>" Width="50%"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDeleteEtiqueta" ImageAlign="Middle" runat="server" OnClick="btnDeleteEtiquetae_Click"
                                                            OnClientClick='<%# "return confirm(\"¿Desea eliminar el registro  "+ Eval("Etiqueta.Descripcion").ToString() +  "  ?\");" %>'
                                                            ImageUrl="../../App_Themes/GasMetropolitano/Imagenes/quitar.png" EnableViewState="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" class="bg-color-grisClaro01">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
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
