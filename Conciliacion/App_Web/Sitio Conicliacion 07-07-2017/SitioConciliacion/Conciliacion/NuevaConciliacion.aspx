<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true"
    CodeFile="NuevaConciliacion.aspx.cs" Inherits="Conciliacion_NuevaConciliacion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    NUEVA CONCILIACIÓN
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <script src="../App_Scripts/jQueryScripts/jquery-3.2.1.min.js" type="text/javascript"></script>
    <script src="../App_Scripts/jQueryScripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../App_Scripts/jQueryScripts/css/custom-theme/jquery-ui-1.10.2.custom.css"
        rel="stylesheet" type="text/css" />
    <script src="../App_Scripts/jQueryScripts/jquery.ui.datepicker-es.js" type="text/javascript"></script>
    <script type="text/javascript">

        function pageLoad(sender, args) {
            var numMeses = "<% = NumMesesAnterior.ToString() %>";
            var maxFecha = "<% = fechaMaximaConciliacion() %>";
            $("#<%=txtMes.ClientID%>").datepicker({
                dateFormat: 'mm/yy',
                changeMonth: true,
                changeYear: true,
                setDate: new Date(maxFecha),
                minDate: '-' + numMeses + 'm',
                maxDate: new Date(maxFecha),
                showButtonPanel: true,
                onClose: function (dateText, inst) {
                    var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                    var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                    $(this).val($.datepicker.formatDate('mm/yy', new Date(year, month, 1)));
                    inst.dpDiv.removeClass('monthonly');
                    window.__doPostBack();
                },
                beforeShow: function (input, inst) {
                    inst.dpDiv.addClass('monthonly');
                }
            });
        }
    </script>
    <style type="text/css">
        /*Ocultar el Calendario . solo mostrar año y Mes*/
        .ui-datepicker-calendar
        {
            display: none;
        }
    </style>
    <link href="../App_Themes/GasMetropolitanoSkin/TabPane.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager ID="smActualizar" runat="server" EnableScriptGlobalization="True">
    </asp:ScriptManager>
    <script src="../App_Scripts/jsUpdateProgress.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        var ModalProgress = '<%= mpeLoading.ClientID %>';        
    </script>
    <asp:UpdatePanel runat="server" ID="upInicio" UpdateMode="Always">
        <ContentTemplate>
            <div class="datos-estilo" style="margin-right: 15px">
                <div class="titulo">
                    <table>
                        <tr>
                            <td style="width: 10%; vertical-align: top">
                                <asp:LinkButton ID="lkbAtras" runat="server" CssClass="atras-boton" PostBackUrl="~/Inicio.aspx"></asp:LinkButton>
                            </td>
                            <td style="width: 90%; vertical-align: top">
                                Nueva Conciliación
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="lineaHorizontal">
                </div>
                <br />
                <asp:TabContainer ID="tabNuevaConciliacion" TabStripPlacement="Top" runat="server"
                    ActiveTabIndex="2" Width="100%" CssClass="tabEstilo">
                    <asp:TabPanel runat="server" HeaderText="DATOS GENERALES" ID="tabDatosGrales" Width="100%">
                        <ContentTemplate>
                            <br />
                            <div class="datos-estilo" style="width: 100%">
                                <div class="etiqueta">
                                    Empresa</div>
                                <asp:DropDownList ID="ddlEmpresa" runat="server" Width="50%" AutoPostBack="True" 
                                    Font-Size="14px" CssClass="dropDown" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvEmpresa" runat="server" ErrorMessage="Especifique el Corporativo."
                                    ControlToValidate="ddlEmpresa" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                    ValidationGroup="DatosGral"></asp:RequiredFieldValidator>
                                <div class="etiqueta">
                                    Banco</div>
                                <asp:DropDownList ID="ddlBanco" runat="server" Width="50%" Font-Size="14px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlBanco_SelectedIndexChanged" CssClass="dropDown">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvBanco" runat="server" ErrorMessage="Especifique el Banco."
                                    ControlToValidate="ddlBanco" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                    ValidationGroup="DatosGral"></asp:RequiredFieldValidator>
                                <div class="etiqueta">
                                    Cuenta Bancaria</div>
                                <asp:DropDownList ID="ddlCuentaBancaria" runat="server" Font-Size="14px" Width="50%"
                                    ValidationGroup="DatosGral" AutoPostBack="True" CssClass="dropDown" 
                                    ondatabound="ddlCuentaBancaria_DataBound" 
                                    onselectedindexchanged="ddlCuentaBancaria_SelectedIndexChanged">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvCuentaBancaria" runat="server" ErrorMessage="Especifique la Cuenta Bancaría."
                                    ControlToValidate="ddlCuentaBancaria" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                    ValidationGroup="DatosGral"></asp:RequiredFieldValidator>
                                <div class="etiqueta">
                                    Tipo Conciliación</div>
                                <asp:DropDownList ID="ddlTipoConciliacion" runat="server" Width="50%" CssClass="dropDown"
                                    AutoPostBack="True" Font-Size="14px" OnSelectedIndexChanged="ddlTipoConciliacion_SelectedIndexChanged">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvTipoConciliacion" runat="server" ErrorMessage="Especifique el Tipo de Conciliación."
                                    ControlToValidate="ddlTipoConciliacion" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                    ValidationGroup="DatosGral"></asp:RequiredFieldValidator>
                                <div class="etiqueta">
                                    Grupo Conciliación</div>
                                <asp:DropDownList ID="ddlGrupoConciliacion" runat="server" Font-Size="14px" Width="50%"
                                    AutoPostBack="True" CssClass="dropDown">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvGrupoConciliacion" runat="server" ErrorMessage="Especifique el Grupo Conciliación."
                                    ControlToValidate="ddlGrupoConciliacion" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                    ValidationGroup="DatosGral"></asp:RequiredFieldValidator>
                                <div class="etiqueta">
                                    Mes/Año</div>
                                <asp:TextBox ID="txtMes" runat="server" Width="49%" CssClass="cajaTexto"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvMesAño" runat="server" ErrorMessage="Especifique el Mes y Año de las conciliaciaciones."
                                    ControlToValidate="txtMes" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                    ValidationGroup="DatosGral"></asp:RequiredFieldValidator>
                                <div class="centradoDerecha" style="width: 50%">
                                    <asp:Button ID="btnSiguiente" runat="server" Text="SIGUIENTE" ToolTip="SIGUIENTE"
                                        CssClass="boton fg-color-blanco bg-color-verdeClaro" ValidationGroup="DatosGral"
                                        OnClick="btnSiguiente_Click" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tabArchivoExterno" runat="server" HeaderText="ARCHIVO EXTERNO">
                        <ContentTemplate>
                            <br />
                            <div class="datos-estilo">
                                <div class="etiqueta">
                                    Tipo Fuente Información
                                </div>
                                <asp:DropDownList ID="ddlTipoFuenteInfoExterno" runat="server" Width="49%" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlTipoFuenteInfoExterno_SelectedIndexChanged" CssClass="dropDown"
                                    Font-Size="14px">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvTipoFuenteInfoExterno" runat="server" ErrorMessage="Especifique el Tipo Fuente Información."
                                    ControlToValidate="ddlTipoFuenteInfoExterno" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                    ValidationGroup="Externo"></asp:RequiredFieldValidator>
                                <div class="etiqueta">
                                    Folio
                                </div>
                                <table style="width: 50%">
                                    <tr>
                                        <td style="width: 95%; vertical-align: top">
                                            <asp:DropDownList ID="ddlFolioExterno" runat="server" Width="98%" AutoPostBack="True"
                                                CssClass="dropDown" OnSelectedIndexChanged="ddlFolioExterno_SelectedIndexChanged"
                                                Font-Size="14px" OnDataBound="ddlFolioExterno_DataBound">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvFolio" runat="server" ErrorMessage="Especifique el Folio."
                                                ControlToValidate="ddlFolioExterno" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                                ValidationGroup="Externo"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="vertical-align: top; padding: 6px">
                                            <asp:ImageButton ID="btnVerDetalleExterno" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/buscar.png"
                                                OnClick="btnVerDetalleExterno_Click" CssClass="icono bg-color-azulClaro" ValidationGroup="Externo"
                                                Width="16px" Heigth="16px" />
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 50%">
                                    <tr>
                                        <td style="width: 50%">
                                            <div class="etiqueta">
                                                Status</div>
                                            <asp:TextBox ID="lblStatusFolioExterno" runat="server" Width="95%" CssClass="cajaTexto"
                                                Enabled="False"></asp:TextBox>
                                        </td>
                                        <td style="width: 50%">
                                            <div class="etiqueta">
                                                Usuario Alta</div>
                                            <asp:TextBox ID="lblUsuarioAltaEx" runat="server" Width="95%" CssClass="cajaTexto"
                                                Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hdfDetalleOculto" runat="server" />
                                <asp:HiddenField ID="hdfRegresoOculto" runat="server" />
                                <asp:ModalPopupExtender ID="grvVistaRapidaExterno_ModalPopupExtender" runat="server"
                                    BehaviorID="idmpeLoading" TargetControlID="hdfDetalleOculto" PopupControlID="pnlVistaRapidaExterno"
                                    EnableViewState="False" BackgroundCssClass="ModalBackground" Enabled="True" CancelControlID="btnCerrarDetalle"
                                    DynamicServicePath="">
                                </asp:ModalPopupExtender>
                                <asp:Panel runat="server" ID="pnlVistaRapidaExterno" HorizontalAlign="Center" CssClass="ModalPopup"
                                    EnableViewState="False" Style="display: none">
                                    <table style="width: 100%;">
                                        <tr class="bg-color-grisOscuro">
                                            <td colspan="5" style="padding: 5px 5px 5px 5px" class="etiqueta">
                                                <div class="floatDerecha bg-color-grisClaro01">
                                                    <asp:ImageButton runat="server" ID="btnCerrarDetalle" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png"
                                                        CssClass="iconoPequeño bg-color-rojo" />
                                                </div>
                                                <div class="fg-color-blanco">
                                                    DETALLE FOLIO EXTERNO SELECCIONADO [<asp:Label runat="server" ID="lblFESelec"></asp:Label>]
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="grvVistaRapidaExterno" runat="server" AutoGenerateColumns="False"
                                                    BorderStyle="Dotted" Font-Size="12px" CssClass="grvResultadoConsultaCss" ShowHeaderWhenEmpty="True"
                                                    Width="100%" GridLines="Horizontal">
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="Sin detalle del folio de la conciliacion."></asp:Label>
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Referencia" SortExpression="referencia">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblReferencia" runat="server" Text="<%# Bind('Referencia') %>"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" BackColor="#D5E684" VerticalAlign="Middle" Wrap="True"
                                                                Width="100px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="FOperacion" SortExpression="fOperacion">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFOperracion" runat="server" Text="<%# Bind('FOperacion', '{0:d}') %>"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="FMovimiento" SortExpression="fMovimiento">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFMovimiento" runat="server" Text="<%# Bind('FMovimiento','{0:d}') %>"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Descripcion" SortExpression="descripcion">
                                                            <ItemTemplate>
                                                                <div class="parrafoTexto">
                                                                    <asp:Label ID="lblDescripcion" runat="server" Text="<%# Bind('Descripcion') %>"></asp:Label>
                                                                </div>
                                                                <asp:HoverMenuExtender ID="hmeDescripcion" runat="server" TargetControlID="lblDescripcion"
                                                                    PopupControlID="pnlPopUpDescripcion" PopDelay="20" OffsetX="0" OffsetY="0">
                                                                </asp:HoverMenuExtender>
                                                                <asp:Panel ID="pnlPopUpDescripcion" runat="server" CssClass="grvResultadoConsultaCss ocultar"
                                                                    Width="150px" Style="padding: 5px 5px 5px 5px" BackColor="White" Wrap="True">
                                                                    <asp:Label ID="lblToolTipDescripcion" runat="server" Text='<%# Eval("Descripcion") %>'
                                                                        CssClass="etiqueta " Font-Size="10px" />
                                                                </asp:Panel>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Justify" VerticalAlign="Middle" Wrap="True" Width="150px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Deposito" SortExpression="deposito">
                                                            <ItemTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblDeposito" runat="server" Font-Size="10px" Text="<%# Bind('Deposito','{0:c}') %>"></asp:Label></b>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" Width="50px"
                                                                CssClass="fg-color-blanco bg-color-grisClaro centradoMedio" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Retiro" SortExpression="retiro">
                                                            <ItemTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblRetiro" runat="server" Font-Size="10px" Text="<%# Bind('Retiro','{0:c2}') %>"></asp:Label></b>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="True" Width="50px"
                                                                CssClass="fg-color-blanco bg-color-grisClaro centradoMedio" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Concepto" SortExpression="concepto">
                                                            <ItemTemplate>
                                                                <div class="parrafoTexto" style="width: 500px">
                                                                    <asp:Label ID="lblConceptoExt" runat="server" Text='<%# Eval("Concepto") %>'></asp:Label>
                                                                </div>
                                                                <asp:HoverMenuExtender ID="hmeConceptoExt" runat="server" TargetControlID="lblConceptoExt"
                                                                    PopupControlID="pnlPopUpConceptoExt" PopDelay="20" OffsetX="0" OffsetY="0">
                                                                </asp:HoverMenuExtender>
                                                                <asp:Panel ID="pnlPopUpConceptoExt" runat="server" CssClass="grvResultadoConsultaCss ocultar"
                                                                    Width="500px" Style="padding: 5px 5px 5px 5px" BackColor="White" Wrap="True">
                                                                    <asp:Label ID="lblToolTipConceptoExt" runat="server" Text='<%# Eval("Concepto") %>'
                                                                        CssClass="etiqueta " Font-Size="10px" />
                                                                </asp:Panel>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Justify" Wrap="True" Width="500px" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:ModalPopupExtender ID="mpeMensageRegreso" runat="server" PopupControlID="pnlMensageRegreso"
                                    TargetControlID="hdfRegresoOculto" EnableViewState="False" Enabled="True" BackgroundCssClass="ModalBackground"
                                    CancelControlID="btnCancelarRegreso" DynamicServicePath="">
                                </asp:ModalPopupExtender>
                                <asp:Panel runat="server" ID="pnlMensageRegreso" HorizontalAlign="Center" CssClass="ModalPopup"
                                    EnableViewState="False" Width="350px" Style="display: none">
                                    <div class="datos-estilo">
                                        <table>
                                            <tr>
                                                <td style="width: 30%">
                                                    <img alt="" src="../App_Themes/GasMetropolitanoSkin/Imagenes/warning.png" width="30px" />
                                                </td>
                                                <td>
                                                    <div class="titulo fg-color-rojo">
                                                        ¡Importante!</div>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="etiqueta centradoJustificado">
                                            Si ha detectado que ya ha agregado Archivos Internos. Si regresa a &quot;DATOS GENERALES&quot;
                                            se borraran los dichos archivos para efectos de seguridad.
                                            <br />
                                            ¿Esta seguro de realizar la acción?
                                        </div>
                                        <div class="lineaHorizontal">
                                        </div>
                                        <br />
                                        <asp:Button runat="server" Text="ACEPTAR" ID="btnAceptarRegreso" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                            OnClick="btnAceptarRegreso_Click" />
                                        <asp:Button runat="server" Text="CANCELAR" ID="btnCancelarRegreso" CssClass="boton fg-color-blanco bg-color-grisOscuro" />
                                    </div>
                                </asp:Panel>
                                <br />
                                <br />
                                <div class="centradoMedio" style="width: 50%">
                                    <asp:Button ID="btnAtrasExterno" runat="server" CssClass="boton fg-color-blanco bg-color-grisClaro"
                                        Text="ATRAS" ToolTip="ATRAS" OnClick="btnAtrasExterno_Click" />
                                    <asp:Button ID="btnSiguienteExterno" runat="server" OnClick="btnSiguienteExterno_Click"
                                        CssClass="boton fg-color-blanco bg-color-verdeClaro" Text="SIGUIENTE" ToolTip="SIGUIENTE"
                                        ValidationGroup="Externo" />
                                    <asp:Button ID="btnGuardarConciliacionTipo2" runat="server" CssClass="boton fg-color-blanco bg-color-verdeClaro"
                                        ValidationGroup="Externo" OnClick="btnGuardarConciliacion_Click" Text="GUARDAR"
                                        ToolTip="GUARDAR" Visible="False" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tabArchivoInterno" runat="server" HeaderText="ARCHIVO INTERNO">
                        <ContentTemplate>
                            <br />
                            <table style="width: 100%">
                                <tr>
                                    <td style="vertical-align: top; width: 50%">
                                        <div class="datos-estilo">
                                            <div class="etiqueta">
                                                Tipo Fuente Informacion
                                            </div>
                                            <asp:DropDownList ID="ddlTipoFuenteInfoInterno" runat="server" AutoPostBack="True"
                                                Font-Size="14px" Width="100%" CssClass="dropDown" OnSelectedIndexChanged="ddlTipoFuenteInfoInterno_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvTipoFuenteInfoInterno" runat="server" ControlToValidate="ddlTipoFuenteInfoInterno"
                                                ErrorMessage="Especifique el Tipo Fuente Información." Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                                ValidationGroup="Interno"></asp:RequiredFieldValidator>
                                            <div class="etiqueta">
                                                Sucursal
                                            </div>
                                            <asp:DropDownList ID="ddlSucursalInterno" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSucursalInterno_SelectedIndexChanged"
                                                Width="100%" CssClass="dropDown" Font-Size="14px">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvSucursalInterno" runat="server" ControlToValidate="ddlSucursalInterno"
                                                ErrorMessage="Especifique una Sucursal." Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                                ValidationGroup="Interno"></asp:RequiredFieldValidator>
                                            <div class="etiqueta">
                                                Folio
                                            </div>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 90%; vertical-align: top">
                                                        <asp:DropDownList ID="ddlFolioInterno" runat="server" AutoPostBack="True" Width="100%"
                                                            CssClass="dropDown" OnSelectedIndexChanged="ddlFolioInterno_SelectedIndexChanged"
                                                            OnDataBound="ddlFolioInterno_DataBound" Font-Size="14px">
                                                        </asp:DropDownList>
                                                        <br />
                                                        <asp:RequiredFieldValidator ID="rfvFolioInterno" runat="server" ControlToValidate="ddlFolioInterno"
                                                            ErrorMessage="Especifique el Folio." Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                                            ValidationGroup="Interno"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td style="vertical-align: top; padding: 6px;">
                                                        <asp:ImageButton ID="btnAñadirFolio" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Agregar.png"
                                                            OnClick="btnAñadirFolio_Click" Width="16px" Height="16px" ValidationGroup="Interno"
                                                            CssClass="icono bg-color-verdeClaro" />
                                                    </td>
                                                    <td style="vertical-align: top; padding: 6px;">
                                                        <asp:ImageButton ID="btnVerDetalleInterno" runat="server" ImageAlign="AbsMiddle"
                                                            ValidationGroup="Interno" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/buscar.png"
                                                            OnClick="btnVerDetalleInterno_Click" Width="16px" Height="16px" CssClass="icono bg-color-azulClaro" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td class="etiqueta" style="width: 50%">
                                                        Status
                                                        <asp:TextBox ID="lblStatusFolioInterno" runat="server" CssClass="cajaTexto" Enabled="False"
                                                            Width="95%"></asp:TextBox>
                                                    </td>
                                                    <td class="etiqueta" style="width: 50%">
                                                        Usuario Alta
                                                        <asp:TextBox ID="lblUsuarioAlta" runat="server" CssClass="cajaTexto" Enabled="False"
                                                            Width="95%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                    <td style="vertical-align: top; width: 50%">
                                        <div class="datos-estilo">
                                            <div class="etiqueta" style="margin-bottom: 5px">
                                                Folios Agregados
                                            </div>
                                            <asp:GridView ID="grvAgregados" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                BorderStyle="Dotted" CssClass="grvResultadoConsultaCss" Font-Size="12px" ShowHeaderWhenEmpty="True"
                                                Width="90%" ShowHeader="False" OnRowDeleting="grvAgregados_RowDeleting" BorderColor="White"
                                                OnPageIndexChanging="grvAgregados_PageIndexChanging" OnRowDataBound="grvAgregados_RowDataBound"
                                                DataKeyNames="Folio" PageSize="6">
                                                <EmptyDataTemplate>
                                                    <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="Aún no se ha agregado ningún folio Interno."></asp:Label>
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Image ID="imgBien" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Exito.png"
                                                                Width="15px" Heigth="15px" CssClass="icono bg-color-verdeClaro" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Wrap="True" Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Folias Agregados" SortExpression="fAgregados">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFoliosAgregados" runat="server" Text="<%# Bind('Folio') %>"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imbQuitar" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/DesconciliarActivo.png"
                                                                Width="15px" Heigth="15px" CommandName="Delete" CssClass="icono bg-color-grisClaro01" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Wrap="True" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerTemplate>
                                                    Página
                                                    <asp:DropDownList ID="paginasDropDownList" Font-Size="12px" AutoPostBack="true" runat="server"
                                                        OnSelectedIndexChanged="paginasDropDownList_SelectedIndexChanged" CssClass="dropDownPequeño"
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
                                <tr>
                                    <td colspan="2">
                                        <div class="centradoMedio">
                                            <br />
                                            <asp:Button ID="btnAtrasInterno" runat="server" CssClass="boton fg-color-blanco bg-color-grisClaro"
                                                Text="ATRAS" ToolTip="ATRAS" OnClick="btnAtrasInterno_Click" />
                                            <asp:Button ID="btnGuardarConciliacion" runat="server" Text="GUARDAR" ToolTip="GUARDAR"
                                                CssClass="boton fg-color-blanco bg-color-verdeClaro" OnClick="btnGuardarConciliacion_Click" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <asp:HiddenField ID="hdfDetalleInternoOculto" runat="server" />
                            <asp:ModalPopupExtender ID="grvVistaRapidaInterno_ModalPopupExtender" runat="server"
                                PopupControlID="pnlVistaRapidaInterno" TargetControlID="hdfDetalleInternoOculto"
                                EnableViewState="False" Enabled="True" BackgroundCssClass="ModalBackground" CancelControlID="imgCerrarDetalleInterno"
                                DynamicServicePath="">
                            </asp:ModalPopupExtender>
                            <asp:Panel runat="server" ID="pnlVistaRapidaInterno" HorizontalAlign="Center" CssClass="ModalPopup"
                                EnableViewState="False" Style="display: none">
                                <div class="datos-estilo">
                                    <table style="width: 100%;">
                                        <tr class="bg-color-grisOscuro">
                                            <td style="padding: 5px 5px 5px 5px" class="etiqueta">
                                                <div class="floatDerecha bg-color-grisClaro01">
                                                    <asp:ImageButton runat="server" ID="imgCerrarDetalleInterno" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png"
                                                        CssClass="iconoPequeño bg-color-rojo" />
                                                </div>
                                                <div class="fg-color-blanco">
                                                    DETALLE FOLIO INTERNO SELECCIONADO [<asp:Label runat="server" ID="lblFISelec"></asp:Label>]
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px 5px 5px 5px">
                                                <asp:GridView ID="grvVistaRapidaInterno" runat="server" AutoGenerateColumns="False"
                                                    BorderStyle="Dotted" Font-Size="12px" CssClass="grvResultadoConsultaCss" ShowHeaderWhenEmpty="True"
                                                    Width="100%" GridLines="Horizontal">
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="Sin detalle del folio de la conciliacion."></asp:Label>
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Referencia" SortExpression="referencia">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblReferencia" runat="server" Text="<%# Bind('Referencia') %>"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Justify" VerticalAlign="Middle" Wrap="True" Width="100px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="FOperacion" SortExpression="fOperacion">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFOperracion" runat="server" Text="<%# Bind('FOperacion', '{0:d}') %>"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Justify" VerticalAlign="Middle" Wrap="True" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="FMovimiento" SortExpression="fMovimiento">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFMovimiento" runat="server" Text="<%# Bind('FMovimiento','{0:d}') %>"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Justify" VerticalAlign="Middle" Wrap="True" Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Descripcion" SortExpression="descripcion">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDescripcion" runat="server" Text="<%# Bind('Descripcion') %>"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Justify" VerticalAlign="Middle" Wrap="True" Width="150px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Deposito" SortExpression="deposito">
                                                            <ItemTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblDeposito" runat="server" Font-Size="10px" Width="100px" Text="<%# Bind('Deposito','{0:c2}') %>"></asp:Label></b>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="True" Width="100px"
                                                                CssClass="fg-color-blanco bg-color-grisClaro" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Retiro" SortExpression="retiro">
                                                            <ItemTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblRetiro" runat="server" Font-Size="10px" Width="100px" Text="<%# Bind('Retiro','{0:c2}') %>"></asp:Label></b>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="True" Width="100px"
                                                                CssClass="fg-color-blanco bg-color-grisClaro" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Concepto" SortExpression="concepto">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblConcepto" runat="server" Text="<%# Bind('Concepto') %>"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Justify" Wrap="True" Width="500px" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                    </table>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
                <br />
                <br />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="panelBloqueo" runat="server" AssociatedUpdatePanelID="upInicio">
        <ProgressTemplate>
            <asp:Image ID="imgLoad" runat="server" CssClass="icono bg-color-blanco" Height="40px"
                ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/LoadPage.gif" Width="40px" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ModalPopupExtender ID="mpeLoading" runat="server" BackgroundCssClass="ModalBackground"
        PopupControlID="panelBloqueo" TargetControlID="panelBloqueo">
    </asp:ModalPopupExtender>
</asp:Content>
