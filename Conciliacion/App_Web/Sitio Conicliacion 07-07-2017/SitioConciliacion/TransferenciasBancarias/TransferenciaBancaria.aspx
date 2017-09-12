<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true"
    CodeFile="TransferenciaBancaria.aspx.cs" Inherits="TransferenciasBancarias_TransferenciaBancaria" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <%--Agregado--%>
    <script src="../App_Scripts/jQueryScripts/jquery.min.js" type="text/javascript"></script>
    <script src="../App_Scripts/jQueryScripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../App_Scripts/jQueryScripts/css/custom-theme/jquery-ui-1.10.2.custom.css"
        rel="stylesheet" type="text/css" />
    <script src="../App_Scripts/jQueryScripts/jquery.ui.datepicker-es.js" type="text/javascript"></script>
    <script type="text/javascript">
        function pageLoad() {
            activarDatePickers();
        }

        function activarDatePickers() {

            //DatePicker Fecha
            //            $("#<%= txtFecha.ClientID%>").datepicker({
            //                defaultDate: "+1w",
            //                changeMonth: true,
            //                changeYear: true,
            //                numberOfMonths: 1
            //            });
            //Mes/Año
            $("#<%=txtFecha.ClientID%>").datepicker({
                dateFormat: 'MM/yy',
                changeMonth: true,
                changeYear: true,
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
    <script type="text/javascript" language="javascript">
        function HideModalPopupCapturar() {
            $find("ModalBehaviourCapturar").hide();
        }
    </script>
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
    <script type="text/javascript" language="javascript">
        var ModalProgress = '<%= mpeLoading.ClientID %>';   
    </script>
    <asp:UpdatePanel runat="server" ID="upTransferenciaBancaria" UpdateMode="Always">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td style="vertical-align: top" class="style2">
                        <br />
                        <div class="Filtrado">
                            <div class="tiraVerdeClaro">
                            </div>
                            <div class="titulo">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 85%">
                                            Filtro <small style="font-size: 10px">Transferencia Bancaria</small>
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
                                        ValidationGroup="TransferenciaBancaria"></asp:RequiredFieldValidator>
                                    <div class="etiqueta">
                                        Sucursal
                                    </div>
                                    <asp:DropDownList ID="cboSucursal" runat="server" CssClass="dropDown" Width="255px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="frvSucursal" runat="server" ErrorMessage=" Seleccione una sucursal."
                                        ControlToValidate="cboSucursal" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                        ValidationGroup="TransferenciaBancaria"></asp:RequiredFieldValidator>
                                    <div class="etiqueta">
                                        Banco</div>
                                    <asp:DropDownList ID="cboNombreBanco" runat="server" CssClass="dropDown" Width="255px"
                                        AutoPostBack="True" OnSelectedIndexChanged="cboNombreBanco_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBancoDestino" runat="server" ErrorMessage=" Seleccione un banco."
                                        ControlToValidate="cboNombreBanco" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                        ValidationGroup="TransferenciaBancaria"></asp:RequiredFieldValidator>
                                    <div class="etiqueta">
                                        Cuenta</div>
                                    <asp:DropDownList ID="cboCuentaBancoOrigen" runat="server" CssClass="dropDown" Width="255px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="frvCuentaDestino" runat="server" ErrorMessage=" Seleccione un numero de cuenta."
                                        ControlToValidate="cboCuentaBancoOrigen" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                        ValidationGroup="TransferenciaBancaria"></asp:RequiredFieldValidator>
                                    <div class="etiqueta">
                                        Mes/Año</div>
                                    <asp:TextBox runat="server" ID="txtFecha" CssClass="cajaTexto" Width="250px"></asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator ID="rfvFC" runat="server" ErrorMessage="Indique el Mes/Año de Consulta."
                                        ControlToValidate="txtFecha" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                        ValidationGroup="TransferenciaBancaria" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <br />
                                    <div class="etiqueta">
                                        Status</div>
                                    <asp:DropDownList ID="cboStatus" runat="server" CssClass="dropDown" Width="255px">
                                        <asp:ListItem>CAPTURADA</asp:ListItem>
                                        <asp:ListItem>AUTORIZADA</asp:ListItem>
                                        <asp:ListItem>CANCELADO</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ErrorMessage=" Seleccione un satus."
                                        ControlToValidate="cboStatus" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                        ValidationGroup="TransferenciaBancaria"></asp:RequiredFieldValidator>
                                    <div class="centradoMedio">
                                        <br />
                                        <asp:Button ID="btnConsultar" runat="server" Text="CONSULTAR" ToolTip="Consultar las transferencias bancarias"
                                            ValidationGroup="TransferenciaBancaria" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                            OnClick="btnConsultar_Click" />
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
                                            Transferencia entre Cuentas
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
                            <asp:GridView ID="grdTranferenciBancaria" runat="server" AutoGenerateColumns="False"
                                Width="100%" BorderStyle="Dotted" AllowPaging="True" ShowHeaderWhenEmpty="True"
                                DataKeyNames="CorporativoId,SucursalId,AñoId,FolioId,Status,Monto,NombreCorporativo,NombreSucursal"
                                CssClass="grvResultadoConsultaCss" OnRowCommand="grdTranferenciBancaria_RowCommand"
                                OnRowDataBound="grdTranferenciBancaria_RowDataBound">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblvacio" runat="server" Font-Bold="True" Font-Overline="False" ForeColor="#CC3300"
                                        Text="No existen transferencias para consultar de acuerdo a los Parámetros del Filtrado"></asp:Label>
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
                                    <%--<asp:TemplateField HeaderText="Banco Origen" SortExpression="colBancoOrigen">
                                <ItemTemplate>
                                    <asp:Label ID="lblBancoOrigen" runat="server" Text="<%# Bind('BancoOrigen') %>"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="15%" />
                            </asp:TemplateField>--%>
                                    <%--<asp:TemplateField HeaderText="Banco Destino" SortExpression="colBancoDestino">
                                <ItemTemplate>
                                    <asp:Label ID="lblBancoDestino" runat="server" Text="<%# Bind('BancoDestino') %>"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="15%" />
                            </asp:TemplateField>--%>
                                    <%--<asp:TemplateField HeaderText="Folio" SortExpression="colFolio">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFolio" runat="server" Text="<%# Bind('FolioId') %>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" CssClass="ocultar" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="True" Width="10%" CssClass="ocultar"/>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Banco consulta" SortExpression="colBanco">
                                        <ItemTemplate>
                                        <table class="bg-color-grisClaro03">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblBanco" runat="server" Text="<%# Bind('BancoNombreOrigen') %>"></asp:Label>
                                            </td>
                                            <td style="width: 5%">
                                                    <asp:Image ID="imgTipo" ImageUrl='<%# String.Format("~/App_Themes/GasMetropolitanoSkin/Iconos/In_out/{0}.png", (Eval("Entrada").ToString().Equals("0")?"SALIDA":"ENTRADA")) %>'
                                                    runat="server" ToolTip='<%# String.Format("{0}",Eval("Entrada")).Equals("0")?"SALIDA":"ENTRADA"%>' 
                                                    Width="15px" Heigth="15px" CssClass="icono" ImageAlign="Middle" />
                                            </td>
                                            </tr>
                                        </table>
                                        </ItemTemplate>
                                      
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="True" CssClass="centradoMedio" />
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Tipo Movimiento" SortExpression="colEntrada">
                                        <ItemTemplate>
                                             <asp:Label ID="lblMovimiento" runat="server" 
                                        Text="<%# Bind('Entrada') %>"></asp:Label>
                                            <asp:Image ID="imgTipo" ImageUrl='<%# String.Format("~/App_Themes/GasMetropolitanoSkin/Iconos/In_out/{0}.png", Eval("Entrada")) %>'
                                                runat="server" AlternateText='<%# Eval("Entrada") %>' ToolTip='<%# Eval("Entrada")%>' 
                                                Width="15px" Heigth="15px" CssClass="icono bg-color-grisClaro03" ImageAlign="Middle" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                                        <ItemStyle HorizontalAlign="Center" Wrap="True" Width="10%" />
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Corporativo " SortExpression="colCorporativoOrigDest">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCorporativoOrigDest" runat="server" Text="<%# Bind('NombreCorporativo') %>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" Width="150px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sucursal" SortExpression="colSucursalOrigDest">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSucursalOrigDest" runat="server" Text="<%# Bind('NombreSucursal') %>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                                        <ItemStyle HorizontalAlign="Center" Wrap="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Banco " SortExpression="colBancoOrigenDest">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNombreBanco" runat="server" Text="<%# Bind('BancoNombreDestino') %>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cuenta " SortExpression="colCtaBancoOrigDest">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNumeroCuenta" runat="server" Text="<%# Bind('CuentaBancoDestino') %>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                                        <ItemStyle HorizontalAlign="Center" Wrap="True" Width="13%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Monto" SortExpression="colMonto">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMonto" runat="server" Font-Bold="True" Text='<%# Eval("Monto","{0:c2}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                                        <ItemStyle HorizontalAlign="Center" Wrap="True" Width="13%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnCancelar" ImageAlign="Middle" runat="server" OnClientClick='<%# "return confirm(\"¿Desea cancelar el status del banco  "+ Eval("BancoNombreDestino").ToString() +  " con cuenta "+Eval("CuentaBancoDestino").ToString() +" ?\");" %>'
                                                ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/borrar.png" CssClass="icono  bg-color-grisClaro03"
                                                EnableViewState="true" Width="15px" Heigth="15px" CommandName="CANCELARSTATUS"
                                                ToolTip="CANCELAR" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnAutorizado" ImageAlign="Middle" runat="server" OnClientClick='<%# "return confirm(\"¿Desea autorizar el status del banco  "+ Eval("BancoNombreDestino").ToString() +  " con cuenta "+Eval("CuentaBancoDestino").ToString() +" ?\");" %>'
                                                ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/paloma.png" CssClass="icono  bg-color-grisClaro03"
                                                EnableViewState="true" Width="15px" Heigth="15px" CommandName="AUTORIZARSTATUS"
                                                ToolTip="AUTORIZAR" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center " Width="30px" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerTemplate>
                                    Página
                                    <asp:DropDownList ID="paginasDropDownList" Font-Size="12px" AutoPostBack="true" runat="server"
                                        CssClass="dropDown" Width="60px">
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
    <asp:UpdateProgress ID="panelBloqueo" runat="server" AssociatedUpdatePanelID="upTransferenciaBancaria">
        <ProgressTemplate>
            <asp:Image ID="imgLoad" runat="server" CssClass="icono bg-color-blanco" Height="40px"
                ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/LoadPage.gif" Width="40px" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ModalPopupExtender ID="mpeLoading" runat="server" BackgroundCssClass="ModalBackground"
        PopupControlID="panelBloqueo" TargetControlID="panelBloqueo">
    </asp:ModalPopupExtender>
</asp:Content>
