<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true"
    CodeFile="FuenteInformacion.aspx.cs" Inherits="ImportacionArchivos_FuenteInformacion"
    Title="Fuente de Información" EnableEventValidation="false" %>

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
                                Corporativo</div>
                            <asp:DropDownList ID="cboCorporativo" runat="server" CssClass="dropDown" Width="100%"
                                AutoPostBack="True" OnDataBound="cboCorporativo_DataBound" OnSelectedIndexChanged="cboCorporativo_SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                            <div class="etiqueta">
                                Sucursal</div>
                            <asp:DropDownList ID="cboSucursal" runat="server" CssClass="dropDown" Width="100%"
                                AutoPostBack="True" OnDataBound="cboSucursal_DataBound">
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
                                OnSelectedIndexChanged="cboCuentaFinanciero_SelectedIndexChanged" AutoPostBack="True"
                                OnDataBound="cboCuentaFinanciero_DataBound">
                            </asp:DropDownList>
                            <br />
                            <div class="etiqueta">
                                Tipo Fuente Información</div>
                            <asp:DropDownList ID="cboTipoFuenteInformacion" runat="server" CssClass="dropDown"
                                Width="100%" OnDataBound="cboTipoFuenteInformacion_DataBound">
                            </asp:DropDownList>
                            <br />
                            <div class="etiqueta">
                                Tipo Archivo</div>
                            <asp:DropDownList ID="cboTipoArchivo" runat="server" CssClass="dropDown" Width="100%"
                                AutoPostBack="True" OnSelectedIndexChanged="cboTipoArchivo_SelectedIndexChanged"
                                OnDataBound="cboTipoArchivo_DataBound">
                            </asp:DropDownList>
                            <br />
                            <div class="etiqueta">
                                Descripción</div>
                            <asp:TextBox ID="txtDescripcionTipoArchivo" Rows="4" CssClass="cajaTexto" Font-Size="12px"
                                Width="96%" Style="resize: none" runat="server" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
                            <br />
                            <div class="etiqueta">
                                Archivo</div>
                            <asp:AsyncFileUpload ID="uploadFile" runat="server" ThrobberID="Throbber" OnUploadedComplete="Archivo_UploadedComplete"
                                UploadingBackColor="#3399FF" CssClass="etiqueta fg-color-grisOscuro" ErrorBackColor="#990000"
                                Width="100%" CompleteBackColor="#66CCFF" />
                            <asp:Image ID="Throbber" runat="server" CssClass="icono bg-color-blanco" Height="40px"
                                ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/LoadPage.gif" Width="40px"
                                Style="display: none" />
                            <div class="centradoMedio">
                                <asp:Button ID="btnCancelarDatos" runat="server" CssClass="boton fg-color-blanco bg-color-grisClaro"
                                    Text="CANCELAR" ToolTip="Cancelar el guardado de datos" OnClick="btnCancelarDatos_Click" />
                                <asp:Button ID="btnGuardarDatos" runat="server" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                    Text="GUARDAR" ToolTip="Guardar Fuente de Información" OnClick="btnGuardarDatos_Click" />
                            </div>
                        </div>
                    </td>
                    <td style="vertical-align: top">
                        <div class="datos-estilo">
                            <div class="titulo lineaHorizontal" style="margin-left: 0px">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 95%">
                                            Fuente Información
                                        </td>
                                        <td>
                                            <img alt="" src="../../App_Themes/GasMetropolitanoSkin/Imagenes/grid.png" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <asp:GridView ID="grvFuenteInformacion" runat="server" AutoGenerateColumns="false"
                                Width="100%" CssClass="grvResultadoConsultaCss">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" SortExpression="colBancoFinancieron" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="colBancoFinanciero" runat="server" Text="<%# Bind('BancoFinanciero') %>"
                                                Width="50%"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Id" SortExpression="colCuentaBancoFinanciero" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="colCuentaBancoFinancieroo" runat="server" Text="<%# Bind('CuentaBancoFinanciero') %>"
                                                Width="50%"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Id" SortExpression="colIdFuenteInformacion" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="colIdFuenteInformacion" runat="server" Text="<%# Bind('IdFuenteInformacion') %>"
                                                Width="50%"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="RutaArchivo" SortExpression="colRutaArchivo" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGVRutaArchivo" runat="server" Text="<%# Bind('RutaArchivo') %>"
                                                Width="50%"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tipo F.Información" SortExpression="colTipoFuenteInformacion_Descripcion">
                                        <ItemTemplate>
                                            <asp:Label ID="colTipoFuenteInformacion_Descripcion" runat="server" Text="<%# Bind('TipoFuenteInformacion.Descripcion') %>"
                                                Width="50%"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Justify" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Justify" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tipo Archivo." SortExpression="colTipoArchivo_Descripcion">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTipoArchivo_Descripcion" runat="server" Text="<%# Bind('TipoArchivo.Descripcion') %>"
                                                Width="50%"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" ImageAlign="Middle" runat="server" OnClick="btnDelete_Click"
                                                OnClientClick='<%# "return confirm(\"¿Desea eliminar el registro  "+ Eval("IdFuenteInformacion").ToString() +  "  ?\");" %>'
                                                ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png" CssClass="icono bg-color-rojo"
                                                EnableViewState="true" Width="15px" Heigth="15px" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnModificar" ImageAlign="Middle" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Editar.png"
                                                OnClick="btnAdd_Click" EnableViewState="true" CssClass="icono bg-color-azulClaro"
                                                Width="15px" Heigth="15px" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center " Width="40px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hfCargarArchivo" runat="server" />
    <asp:Panel ID="pnlPopUpArchivo" runat="server" BackColor="#FFFFFF" Width="35%" Style="display: none">
        <asp:UpdatePanel ID="UpdArchivo" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr class="bg-color-grisOscuro">
                        <td colspan="2" class="etiqueta" style="padding: 5px 5px 5px 5px">
                            <div class="floatDerecha">
                                <asp:ImageButton runat="server" ID="imgCerrarImportar" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png"
                                    CssClass="iconoPequeño bg-color-rojo" OnClientClick="HideModalPopup();" />
                            </div>
                            <div class="fg-color-blanco">
                                CARGAR ARCHIVO
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="datos-estilo lineaVertical" style="padding: 5px 5px 5px 5px">
                            <div class="etiqueta">
                                Tipo de Archivo</div>
                            <asp:DropDownList ID="cboPopUpTipoArchivo" runat="server" CssClass="dropDown" Width="100%"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td class="datos-estilo lineaVertical" style="padding: 5px 5px 5px 5px">
                            <div class="etiqueta">
                                Nuevo Archivo</div>
                            <asp:AsyncFileUpload ID="AsyncFileUpload1" runat="server" ThrobberID="Throbber2"
                                OnUploadedComplete="Archivo_UploadedComplete2" UploadingBackColor="#3399FF" CssClass="etiqueta fg-color-grisOscuro"
                                ErrorBackColor="#990000" Width="100%" CompleteBackColor="#66CCFF" />
                            <asp:Image ID="Throbber2" runat="server" CssClass="icono bg-color-blanco" Height="40px"
                                ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/LoadPage.gif" Width="40px"
                                Style="display: none" />
                        </td>
                    </tr>
                    <tr>
                        <td class="centradoMedio" colspan="2">
                            <asp:Button ID="btnGuardarEtiqueta" runat="server" Text="ACEPTAR" CssClass="boton bg-color-azulClaro fg-color-blanco"
                                OnClick="btnGuardarArchivo_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:ModalPopupExtender ID="popUpEtiquetas" runat="server" PopupControlID="pnlPopUpArchivo"
        TargetControlID="hfCargarArchivo" BehaviorID="ModalBehaviour" BackgroundCssClass="ModalBackground">
    </asp:ModalPopupExtender>
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
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <link href="../../App_Themes/GasMetropolitanoSkin/TabPane.css" rel="stylesheet" type="text/css" />
</asp:Content>
