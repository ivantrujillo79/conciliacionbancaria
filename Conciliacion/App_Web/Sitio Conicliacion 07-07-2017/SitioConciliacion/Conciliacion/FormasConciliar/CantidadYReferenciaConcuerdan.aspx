<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true"
    CodeFile="CantidadYReferenciaConcuerdan.aspx.cs" Inherits="Conciliacion_FormasConciliar_CantidadYReferenciaConcuerdan"
    Debug="true" EnableEventValidation="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="server">
    CANTIDAD Y REFERENCIA CONCUERDAN
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <!--Libreria jQuery-->
    <script src="../../App_Scripts/jQueryScripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../App_Scripts/jQueryScripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../App_Scripts/Common.js" type="text/javascript"></script>
    <!-- Script se utiliza para el Scroll del GridView-->
    <link href="../../App_Scripts/ScrollGridView/GridviewScroll.css" rel="stylesheet"
        type="text/css" />
    <script src="../../App_Scripts/ScrollGridView/gridviewScroll.min.js" type="text/javascript"></script>
    <!-- ScrollBar GridView -->
    <script type="text/javascript">
        //        $(document).ready(function () {
        function pageLoad() {
            gridviewScroll();
        }
        //        );
        
        function gridviewScroll() {
            if (<%= tipoConciliacion %> == 2) {
                $('#<%=grvCantidadReferenciaConcuerdanPedido.ClientID%>').gridviewScroll({
                    width: 1200,
                    height: 500,
                    arrowsize: 30,
                    varrowtopimg: '../../App_Scripts/ScrollGridView/Images/arrowvt.png',
                    varrowbottomimg: '../../App_Scripts/ScrollGridView/Images/arrowvb.png',
                    harrowleftimg: '../../App_Scripts/ScrollGridView/Images/arrowhl.png',
                    harrowrightimg: '../../App_Scripts/ScrollGridView/Images/arrowhr.png',
                    headerrowcount: 1,
                    wheelstep: 20,
                    verticalbar:"auto",
                    horizontalbar:"auto"               
                });
            } else {
                $('#<%=grvCantidadReferenciaConcuerdanArchivos.ClientID%>').gridviewScroll({
                    width: 1200,
                    height:500,
                    arrowsize: 30,
                    varrowtopimg: '../../App_Scripts/ScrollGridView/Images/arrowvt.png',
                    varrowbottomimg: '../../App_Scripts/ScrollGridView/Images/arrowvb.png',
                    harrowleftimg: '../../App_Scripts/ScrollGridView/Images/arrowhl.png',
                    harrowrightimg: '../../App_Scripts/ScrollGridView/Images/arrowhr.png',
                    headerrowcount: 1,
                    wheelstep: 20,
                    verticalbar:"auto",
                    horizontalbar:"auto" 
                }); 
            } 
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" runat="server">
    <asp:ScriptManager ID="smCantidadReferenciaConcuerda" runat="server" EnableScriptGlobalization="True"
        AsyncPostBackTimeout="14400">
    </asp:ScriptManager>
    <script src="../../App_Scripts/jsUpdateProgress.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        
        //M ó O
        function ModalPopupBuscar(operacion) {
            if (operacion == 'M') {
                var varBuscar=document.getElementById("<%=txtBuscar.ClientID%>");
                $find("mpeBuscar").show();
                varBuscar.value = "";
            } else {
                $find("mpeBuscar").hide();
            }
        }

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
    <script type="text/javascript" language="javascript">
        var ModalProgress = '<%= mpeLoading.ClientID %>';        
    </script>

    <asp:UpdatePanel runat="server" ID="upBarraEstado" UpdateMode="Always">
        <ContentTemplate>
            <table id="BarraEstado" class="BarraEstado bg-color-grisOscuro">
                <tr>
                    <td class="DatoConciliacion lineaVertical" rowspan="2" style="vertical-align: middle">
                        <asp:Image ID="imgStatusConciliacion" runat="server" CssClass="icono Principal" Height="35px"
                            Width="35px" />
                        <div class="FuenteDato">
                            <div class="InfoPrincipal">
                                Folio
                                <asp:Label ID="lblFolio" runat="server"></asp:Label>
                            </div>
                            <div class="Descripcion">
                                <asp:Label ID="lblStatusConciliacion" runat="server"></asp:Label>
                            </div>
                        </div>
                    </td>
                    <td class="Info Normal lineaHorizontal" style="vertical-align: top">
                        <asp:Image ID="imgBanco" runat="server" CssClass="icono Secundario" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Banco.png"
                            Width="20px" />
                        <div class="FuenteDato">
                            <div class="InfoSecundaria">
                                <asp:Label ID="lblBanco" runat="server"></asp:Label>
                            </div>
                            <div class="Descripcion">
                                Banco
                            </div>
                        </div>
                    </td>
                    <td class="Info Normal lineaHorizontal" style="vertical-align: top">
                        <asp:Image ID="imgSucursal" runat="server" CssClass="icono Secundario" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Sucursal.png"
                            Width="20px" />
                        <div class="FuenteDato">
                            <div class="InfoSecundaria">
                                <asp:Label ID="lblSucursal" runat="server"></asp:Label>
                            </div>
                            <div class="Descripcion">
                                Sucursal
                            </div>
                        </div>
                    </td>
                    <td class="Info Grande lineaHorizontal" style="vertical-align: top">
                        <asp:Image ID="imgTipoCon" runat="server" CssClass="icono Secundario" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/TipoConciliacion.png"
                            Width="20px" />
                        <div class="FuenteDato">
                            <div class="InfoSecundaria">
                                <asp:Label ID="lblTipoCon" runat="server"></asp:Label>
                            </div>
                            <div class="Descripcion">
                                Tipo Conciliación
                            </div>
                        </div>
                    </td>
                    <td class="Info Estadistica lineaHorizontal" style="vertical-align: top">
                        <asp:Image ID="imgConciliadasExt" runat="server" CssClass="icono Secundario" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Enfasis.png"
                            Width="20px" />
                        <div class="FuenteDato">
                            <div class="InfoSecundaria">
                                <asp:Label ID="lblConciliadasExt" runat="server"></asp:Label>
                            </div>
                            <div class="Descripcion">
                                Conciliadas Externas
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="Info Normal" style="vertical-align: top">
                        <asp:Image ID="imgCuentaBancaria" runat="server" CssClass="icono Secundario" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cuenta.png"
                            Width="20px" />
                        <div class="FuenteDato">
                            <div class="InfoSecundaria">
                                <asp:Label ID="lblCuenta" runat="server"></asp:Label>
                            </div>
                            <div class="Descripcion">
                                Cuenta Bancaría
                            </div>
                        </div>
                    </td>
                    <td class="Info Normal " style="vertical-align: top">
                        <asp:Image ID="imgMesAño" runat="server" CssClass="icono Secundario" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/AñoMes.png"
                            Width="20px" />
                        <div class="FuenteDato">
                            <div class="InfoSecundaria">
                                <asp:Label ID="lblMesAño" runat="server"></asp:Label>
                            </div>
                            <div class="Descripcion">
                                Mes/Año
                            </div>
                        </div>
                    </td>
                    <td class="Info Normal " style="vertical-align: top">
                        <asp:Image ID="imgGrupoConciliacion" runat="server" CssClass="icono Secundario" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/GrupoConciliacion.png"
                            Width="20px" />
                        <div class="FuenteDato">
                            <div class="InfoSecundaria">
                                <asp:Label ID="lblGrupoCon" runat="server"></asp:Label>
                            </div>
                            <div class="Descripcion">
                                Grupo Conciliación
                            </div>
                        </div>
                    </td>
                    <td class="Info Estadistica " style="vertical-align: top">
                        <asp:Image ID="imgConciliadasInt" runat="server" CssClass="icono Secundario" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Enfasis.png"
                            Width="20px" />
                        <div class="FuenteDato">
                            <div class="InfoSecundaria">
                                <asp:Label ID="lblConciliadasInt" runat="server"></asp:Label>
                            </div>
                            <div class="Descripcion">
                                Conciliadas Internas
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ID="upBarraHerramientas" UpdateMode="Always">
        <ContentTemplate>
            <table id="BarraHerramientas" class="bg-color-grisClaro01" style="width: 100%; vertical-align: top">
                <tr>
                    <td style="padding: 3px 3px 3px 3px; vertical-align: top; width: 1%">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion bg-color-verdeClaro" rowspan="2">
                                    <asp:ImageButton ID="imgAutomatica" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Automatica.png"
                                        ToolTip="CONSULTAR FORMA AUTOMATICA" Width="25px" OnClick="imgAutomatica_Click" />
                                </td>
                                <td>Conciliación Automatica
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlCriteriosConciliacion" CssClass="etiqueta dropDownPequeño"
                                        Width="150px" Style="margin-bottom: 3px; margin-right: 3px" AutoPostBack="False"
                                        OnSelectedIndexChanged="ddlCriteriosConciliacion_SelectedIndexChanged" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="padding: 3px 3px 3px 0px; vertical-align: top; width: 1%">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion  bg-color-azulClaro" rowspan="2">
                                    <asp:ImageButton ID="btnActualizarConfig" runat="server" Height="25px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/ActualizarConfig.png"
                                        ToolTip="ACTUALIZAR CONFIGURACION" Width="25px" OnClick="btnActualizarConfig_Click" />
                                </td>
                                <td class="lineaVertical">Dias
                                </td>
                                <td class="lineaVertical">Diferencia
                                </td>
                                <td class="lineaVertical">Status Concepto
                                </td>
                                <td>Sucursal&nbsp; Interna
                                </td>
                            </tr>
                            <tr>
                                <td id="tdDias" runat="server" class="lineaVertical">
                                    <asp:TextBox ID="txtDias" runat="server" CssClass="cajaTextoPequeño" Font-Size="12px"
                                        MaxLength="2" Width="30px" onkeypress="return ValidNum(event)" Style="margin-bottom: 3px; margin-right: 3px"></asp:TextBox>
                                </td>
                                <td class="lineaVertical">
                                    <asp:TextBox ID="txtDiferencia" runat="server" CssClass="cajaTextoPequeño" Font-Size="12px"
                                        onkeypress="return ValidNumDecimal(event)" Style="margin-bottom: 3px; margin-right: 3px"
                                        Width="40px"></asp:TextBox>
                                </td>
                                <td class="lineaVertical">
                                    <asp:DropDownList ID="ddlStatusConcepto" runat="server" AppendDataBoundItems="True"
                                        CssClass="etiqueta dropDownPequeño" Style="margin-bottom: 3px; margin-right: 3px"
                                        Width="130px" OnSelectedIndexChanged="ddlStatusConcepto_SelectedIndexChanged">
                                        <asp:ListItem Text="NINGUNO" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSucursal" runat="server" AutoPostBack="False" CssClass="etiqueta dropDownPequeño"
                                        Enabled="False" Style="margin-bottom: 3px; margin-right: 3px" Width="115px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr class="bg-color-blanco centradoIzquierda">
                                <td></td>
                                <td colspan="4">
                                    <b>
                                        <asp:RangeValidator ID="rvDias" runat="server" ControlToValidate="txtDias" CssClass="etiqueta fg-color-rojo"
                                            Display="Dynamic" EnableClientScript="True" Font-Size="10px" Type="Integer" ValidationGroup="CantidadReferencia">
                                        </asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvDiasVacio" runat="server" ControlToValidate="txtDias"
                                            CssClass="etiqueta fg-color-rojo" Display="Dynamic" EnableClientScript="True"
                                            ErrorMessage="Especifique los dias. " Font-Size="10px" ValidationGroup="CantidadReferencia"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="rvDiferencia" runat="server" ControlToValidate="txtDiferencia"
                                            CssClass="etiqueta fg-color-rojo" Display="Dynamic" EnableClientScript="True"
                                            Font-Size="10px" Type="Double" ValidationGroup="CantidadReferencia">
                                        </asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvDiferenciaVacio" runat="server" ControlToValidate="txtDiferencia"
                                            CssClass="etiqueta fg-color-rojo" Display="Dynamic" EnableClientScript="True"
                                            ErrorMessage="Especifique la diferencia " Font-Size="10px" ValidationGroup="CantidadReferencia"></asp:RequiredFieldValidator>
                                    </b>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="padding: 3px 3px 3px 0px; vertical-align: top; width: 1%">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion bg-color-purpura" rowspan="2">
                                    <asp:ImageButton ID="imgFiltrar" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Filtrar.png"
                                        ToolTip="FILTRAR" Width="25px" OnClick="imgFiltrar_Click" />
                                </td>
                                <td>Filtrar en
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlFiltrarEn" runat="server" CssClass="etiqueta dropDownPequeño"
                                        Style="margin-bottom: 3px; margin-right: 3px" Width="85px">
                                        <asp:ListItem Value="Externos" Selected="True">Externos</asp:ListItem>
                                        <asp:ListItem Value="Internos">Internos</asp:ListItem>
                                        <asp:ListItem Value="Conciliados"> Conciliados</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="padding: 3px 3px 3px 0px; vertical-align: top; width: 1%">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion bg-color-naranja" rowspan="2">
                                    <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Buscar.png"
                                        ToolTip="BUSCAR" Width="25px" OnClientClick="ModalPopupBuscar('M');" />
                                    <%--OnClientClick="ShowModalPopupBuscar();" OnClick="imgBuscar_Click"--%>
                                </td>
                                <td>Buscar en
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlBuscarEn" runat="server" CssClass="etiqueta dropDownPequeño"
                                        Style="margin-bottom: 3px; margin-right: 3px" Width="85px">
                                        <asp:ListItem Value="Externos" Selected="True">Externos</asp:ListItem>
                                        <asp:ListItem Value="Internos">Internos</asp:ListItem>
                                        <asp:ListItem Value="Conciliados">Conciliados</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 1%; padding: 3px 3px 3px 0px; vertical-align: top">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td id="tdExportar" runat="server" class="iconoOpcion bg-color-rojo" style="height: 30px">
                                    <asp:ImageButton ID="imgExportar" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Exportar.png"
                                        ToolTip="EXPORTAR" Width="25px" OnClick="imgExportar_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 1%; padding: 3px 3px 3px 0px; vertical-align: top">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td id="tdImportar" class="iconoOpcion bg-color-verdeFuerte" style="height: 30px"
                                    runat="server">
                                    <asp:ImageButton ID="imgImportar" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Importar.png"
                                        ToolTip="IMPORTAR" Width="25px" OnClick="imgImportar_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 1%; padding: 3px 3px 3px 0px; vertical-align: top">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion bg-color-naranjaOscuro" style="height: 30px">
                                    <asp:ImageButton ID="imgBitacora" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Bitacora.png"
                                        ToolTip="BITACORA AUDITORIA" Width="25px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 1%; padding: 3px 3px 3px 0px; vertical-align: top">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion bg-color-azulOscuro" style="height: 30px">
                                    <asp:ImageButton ID="btnGuardar" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Guardar.png"
                                        ToolTip="GUARDAR" Width="25px" OnClick="btnGuardar_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 1%; padding: 3px 3px 3px 0px; vertical-align: top">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion bg-color-negro" style="height: 30px">
                                    <asp:ImageButton ID="imgCerrarConciliacion" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png"
                                        ToolTip="CERRAR CONCILIACION" Width="25px" OnClientClick="return confirm('¿Esta seguro de CERRAR la conciliación.?')"
                                        OnClick="imgCerrarConciliacion_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 1%; padding: 3px 3px 3px 0px; vertical-align: top">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion bg-color-rojo" style="height: 30px">
                                    <asp:ImageButton ID="imgCancelarConciliacion" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cancelar.png"
                                        ToolTip="CANCELAR CONCILIACION" Width="25px" OnClientClick="return confirm('¿Esta seguro de CANCELAR la conciliación.?')"
                                        OnClick="imgCancelarConciliacion_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>


    <table style="width: 100%">
        <tr>
            <td style="width: 100%; vertical-align: top; padding: 5px 5px 5px 5px" class="etiqueta fg-color-blanco bg-color-azulClaro">Transacciones Conciliadas
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel runat="server" ID="upGrvConciliadas" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:GridView ID="grvConciliadas" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            PageSize="5" Width="100%" CssClass="grvResultadoConsultaCss" ShowHeaderWhenEmpty="True"
                            OnPageIndexChanging="grvConciliadas_PageIndexChanging" OnRowDataBound="grvConciliadas_RowDataBound"
                            DataKeyNames="CorporativoConciliacion, SucursalConciliacion, AñoConciliacion, MesConciliacion, FolioConciliacion, FolioExt, Secuencia"
                            OnRowCommand="grvConciliadas_RowCommand" OnSelectedIndexChanging="grvConciliadas_SelectedIndexChanging"
                            OnRowCreated="grvConciliadas_RowCreated" AllowSorting="True" OnSorting="grvConciliadas_Sorting">
                            <EmptyDataTemplate>
                                <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="No se han conciliado ninguna transacción."></asp:Label>
                            </EmptyDataTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField HeaderText="Folio" SortExpression="FolioExt">
                                    <ItemTemplate>
                                        <asp:Label ID="lFolio" runat="server" Text='<%# resaltarBusqueda(Eval("FolioExt").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="centradoMedio" />
                                    <ItemStyle BackColor="#ebecec" ForeColor="Black" HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sec." SortExpression="Secuencia">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSecuencia" runat="server" Text='<%# resaltarBusqueda(Eval("Secuencia").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="centradoMedio" />
                                    <ItemStyle HorizontalAlign="Center" Width="20px" BackColor="#ebecec" ForeColor="Black"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="F. Mov." SortExpression="FMovimiento">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFMovimiento" runat="server" Text='<%# resaltarBusqueda(Eval("FMovimiento","{0:d}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="centradoMedio" />
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="F. Op." SortExpression="FOperacion">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFOperacion" runat="server" Text='<%# resaltarBusqueda(Eval("FOperacion","{0:d}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="centradoMedio" />
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mont. Conciliado" SortExpression="MontoConciliado">
                                    <ItemTemplate>
                                        <b>
                                            <asp:Label ID="lblDeposito" runat="server" Text='<%# resaltarBusqueda(Eval("MontoConciliado","{0:c2}").ToString()) %>'></asp:Label></b>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Concepto" SortExpression="Concepto">
                                    <ItemTemplate>
                                        <div class="parrafoTexto" style="width: 400px">
                                            <asp:Label ID="lblConceptoExt" runat="server" Text='<%# resaltarBusqueda(Eval("Concepto").ToString()) %>'></asp:Label>
                                        </div>
                                        <asp:HoverMenuExtender ID="hmeConceptoExt" runat="server" TargetControlID="lblConceptoExt"
                                            PopupControlID="pnlPopUpConceptoExt" PopDelay="20" OffsetX="0" OffsetY="0">
                                        </asp:HoverMenuExtender>
                                        <asp:Panel ID="pnlPopUpConceptoExt" runat="server" CssClass="grvResultadoConsultaCss ocultar"
                                            Width="500px" Style="padding: 5px 5px 5px 5px" BackColor="White" Wrap="True">
                                            <asp:Label ID="lblToolTipConceptoExt" runat="server" Text='<%# resaltarBusqueda(Eval("Concepto").ToString()) %>'
                                                CssClass="etiqueta " Font-Size="10px" />
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Justify" Width="400px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="400px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripcion" SortExpression="Descripcion">
                                    <ItemTemplate>
                                        <div class="parrafoTexto" style="width: 200px">
                                            <asp:Label ID="lblDescripcion" runat="server" Text='<%# resaltarBusqueda(Eval("Descripcion").ToString()) %>'></asp:Label>
                                        </div>
                                        <asp:HoverMenuExtender ID="hmeDescripcionExt" runat="server" TargetControlID="lblDescripcion"
                                            PopupControlID="pnlPopUpDescripcionExt" PopDelay="20" OffsetX="0" OffsetY="0">
                                        </asp:HoverMenuExtender>
                                        <asp:Panel ID="pnlPopUpDescripcionExt" runat="server" CssClass="grvResultadoConsultaCss ocultar"
                                            Width="200px" Style="padding: 5px 5px 5px 5px" BackColor="White" Wrap="True">
                                            <asp:Label ID="lblToolTipDescripcionExt" runat="server" Text='<%# resaltarBusqueda(Eval("Descripcion").ToString()) %>'
                                                CssClass="etiqueta " Font-Size="10px" />
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Justify" Width="200px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Factura" SortExpression="SerieFactura">
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label runat="server" ID="lblSerieFactura" Text='<%# resaltarBusqueda(Eval("SerieFactura").ToString()) %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cliente" SortExpression="ClienteReferencia">
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label runat="server" ID="lblClienteReferencia" Text='<%# resaltarBusqueda(Eval("ClienteReferencia").ToString()) %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button runat="server" ID="imgDesconciliar" CssClass="Desconciliar centradoMedio boton"
                                            ToolTip="DESCONCILIAR" Width="20px" Height="20px" OnClientClick='<%# "return confirm(\"¿Esta seguro de DESCONCILIAR la Transacción: " + Eval("FolioExt").ToString() + ": Secuencia: "+ Eval("Secuencia").ToString() + "?¡Se actualizará la conciliacion y su detalle! ?\");" %>'
                                            CommandName="DESCONCILIAR" />
                                        <asp:Button runat="server" ID="imgDetalleConciliado" CssClass="Detalle centradoMedio boton"
                                            ToolTip="VER DETALLE" Width="20px" Height="20px" CommandName="Select" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="80px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
                                </asp:TemplateField>
                            </Columns>
                            <PagerTemplate>
                                Página
                                <asp:DropDownList ID="paginasDropDownListConciliadas" Font-Size="12px" AutoPostBack="true"
                                    runat="server" OnSelectedIndexChanged="paginasDropDownListConciliadas_SelectedIndexChanged"
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>

    <asp:UpdatePanel runat="server" ID="upConciliar" UpdateMode="Always">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td style="width: 100%; vertical-align: top; padding: 5px 5px 5px 5px" class="etiqueta fg-color-blanco bg-color-verdeFuerte"
                        colspan="3">Transacciones Por Conciliar
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%" class="centradoMedio">
                        <table style="width: 100%">
                            <tr>
                                <td class="bg-color-grisOscuro fg-color-blanco" style="width: 15%">Total Externo
                                </td>
                                <td class="bg-color-verdeClaro" style="width: 15%">
                                    <b>
                                        <asp:Label ID="lblMontoTotalExterno" runat="server" CssClass="etiqueta fg-color-blanco"></asp:Label></b>
                                </td>
                                <td class="bg-color-grisClaro02" style="width: 60%">
                                    <b>Archivos Externos</b>
                                </td>
                                <td class="bg-color-azulClaro" style="width: 5%">
                                    <asp:Image ID="imgGuardarParcial" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Exito.png"
                                        CssClass="icono" Width="20px"></asp:Image>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td rowspan="3" style="width: 1%"></td>
                    <td style="width: 50%;" class="centradoMedio">
                        <table style="width: 100%; height: 20px">
                            <tr>
                                <td class="bg-color-grisOscuro fg-color-blanco" style="width: 15%" id="tdEtiquetaMontoIn"
                                    runat="server">Total Interno
                                </td>
                                <td class="bg-color-amarillo" style="width: 15%" id="tdMontoIn" runat="server">
                                    <b>
                                        <asp:Label ID="lblMontoTotalInterno" runat="server" CssClass="etiqueta fg-color-negro"></asp:Label></b>
                                </td>
                                <td class="bg-color-grisClaro02" style="width: 64%">
                                    <b>
                                        <asp:Label ID="lblArchivosInternos" Text="Archivos Internos" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblPedidos" Text="Pedidos" runat="server" Visible="false"></asp:Label>
                                    </b>
                                </td>
                                <td class="bg-color-grisClaro fg-color-amarillo" style="width: 1%">
                                    <asp:Image ID="imgInt" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Exito.png"
                                        CssClass="icono" Width="20px"></asp:Image>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <div id="configuracionExternos" class="bg-color-grisClaro">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 50%; vertical-align: top">
                                        <table style="width: 300px">
                                            <tr>
                                                <td class="etiqueta fg-color-blanco" style="width: 30%">Campo Externo
                                                </td>
                                                <td style="width: 70%">
                                                    <asp:DropDownList runat="server" ID="ddlCampoExterno" CssClass="dropDownPequeño"
                                                        Width="125px" OnDataBound="ddlCampoExterno_DataBound" OnSelectedIndexChanged="ddlCampoExterno_SelectedIndexChanged"
                                                        AutoPostBack="True" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:RequiredFieldValidator ID="rfvCampoExterno" runat="server" ControlToValidate="ddlCampoExterno"
                                                        CssClass="etiqueta fg-color-amarillo" Display="Dynamic" EnableClientScript="True"
                                                        ErrorMessage="Especifique la referencia externa" Font-Size="10px" ValidationGroup="CantidadReferencia"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 1%"></td>
                                    <td style="width: 50%; vertical-align: top" class="centradoJustificado">
                                        <table style="width: 300px">
                                            <tr>
                                                <td class="etiqueta fg-color-blanco" style="width: 30%">Campo Interno
                                                </td>
                                                <td style="width: 70%">
                                                    <asp:DropDownList runat="server" ID="ddlCampoInterno" CssClass="dropDown" Width="125px"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlCampoInterno_SelectedIndexChanged" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:RequiredFieldValidator ID="rfvCampoInterno" runat="server" ControlToValidate="ddlCampoInterno"
                                                        CssClass="etiqueta fg-color-amarillo" Display="Dynamic" EnableClientScript="True"
                                                        ErrorMessage="Especifique la referencia interna" Font-Size="10px" ValidationGroup="CantidadReferencia"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="grvCantidadReferenciaConcuerdanArchivos" runat="server" AutoGenerateColumns="False"
                            AllowPaging="True" ShowHeader="True" Width="100%" CssClass="grvResultadoConsultaCss"
                            ShowFooter="false" PageSize="100" OnPageIndexChanging="grvCantidadReferenciaConcuerdanArchivos_PageIndexChanging"
                            OnRowDataBound="grvCantidadReferenciaConcuerdanArchivos_RowDataBound" ShowHeaderWhenEmpty="True"
                            OnRowCreated="grvCantidadReferenciaConcuerdanArchivos_RowCreated" DataKeyNames="FolioExt,SecuenciaExt,FolioInt,SecuenciaInt"
                            AllowSorting="True" OnSorting="grvCantidadReferenciaConcuerdanArchivos_Sorting">
                            <%-- <EmptyDataTemplate>
                                <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="No existen referencias con cantidades concordantes."></asp:Label></EmptyDataTemplate>--%>
                            <HeaderStyle HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField HeaderText="F. Ext" SortExpression="FolioExt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFolioExt" runat="server" Text='<%# resaltarBusqueda(Eval("FolioExt").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle BackColor="#99b433" ForeColor="White" HorizontalAlign="Center" Wrap="True"
                                        Width="30px" />
                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" Width="30px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sec. Ext." SortExpression="SecuenciaExt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSecuencia" runat="server" Text='<%# resaltarBusqueda(Eval("SecuenciaExt").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="40px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="F. Mov. Ext." SortExpression="FMovimientoExt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFechaMovimientoExt" runat="server" Text='<%# resaltarBusqueda(Eval("FMovimientoExt","{0:d}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="F. Op. Ext." SortExpression="FOperacionExt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFechaOperacionExt" runat="server" Text='<%# resaltarBusqueda(Eval("FOperacionExt","{0:d}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="M. Conciliado" SortExpression="MontoConciliadoExt">
                                    <ItemTemplate>
                                        <b>
                                            <asp:Label ID="lblMontoConciliado" runat="server" Text='<%# resaltarBusqueda(Eval("MontoConciliadoExt","{0:c2}").ToString()) %>'></asp:Label></b>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Concepto" SortExpression="ConceptoExt">
                                    <ItemTemplate>
                                        <div class="parrafoTexto">
                                            <asp:Label ID="lblConceptoExt" runat="server" Text='<%# resaltarBusqueda(Eval("ConceptoExt").ToString()) %>'></asp:Label>
                                        </div>
                                        <asp:HoverMenuExtender ID="hmeConceptoExt" runat="server" TargetControlID="lblConceptoExt"
                                            PopupControlID="pnlPopUpConceptoExt" PopDelay="20" OffsetX="-100" OffsetY="-5">
                                        </asp:HoverMenuExtender>
                                        <asp:Panel ID="pnlPopUpConceptoExt" runat="server" CssClass="grvResultadoConsultaCss ocultar"
                                            Width="300px" Wrap="True" Style="padding: 5px 5px 5px 5px" BackColor="White">
                                            <asp:Label ID="lblToolTipConceptoExt" runat="server" Text='<%# resaltarBusqueda(Eval("ConceptoExt").ToString()) %>'
                                                CssClass="etiqueta centradoJustificado" Font-Size="10px" />
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px"></ItemStyle>
                                    <HeaderStyle Width="150px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descrip. Ext." SortExpression="DescripcionExt">
                                    <ItemTemplate>
                                        <div class="parrafoTexto" style="width: 120px">
                                            <asp:Label ID="lblDescripcionExt" runat="server" Text='<%# resaltarBusqueda(Eval("DescripcionExt").ToString()) %>'></asp:Label>
                                        </div>
                                        <asp:HoverMenuExtender ID="hmeDescripcionExt" runat="server" TargetControlID="lblDescripcionExt"
                                            PopupControlID="pnlPopUpDescripcionExt" PopDelay="20" OffsetX="0" OffsetY="0">
                                        </asp:HoverMenuExtender>
                                        <asp:Panel ID="pnlPopUpDescripcionExt" runat="server" CssClass="grvResultadoConsultaCss ocultar"
                                            Width="100px" Wrap="True" BackColor="White" Style="padding: 5px 5px 5px 5px">
                                            <asp:Label ID="lblToolTipDescripcionExt" runat="server" Text='<%# resaltarBusqueda(Eval("DescripcionExt").ToString()) %>'
                                                CssClass="etiqueta" Font-Size="10px" />
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <ItemStyle Width="120px"></ItemStyle>
                                    <HeaderStyle Width="120px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="F. Int" SortExpression="FolioInt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFolioInt" runat="server" Text='<%# resaltarBusqueda(Eval("FolioInt").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="centradoMedio"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" BackColor="#d9b335" ForeColor="White" Width="30px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sec. Int." SortExpression="SecuenciaInt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSecuenciaInt" runat="server" Text='<%# resaltarBusqueda(Eval("SecuenciaInt").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="centradoMedio"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="40px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="F. Mov. Int." SortExpression="FMovimientoInt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFechaMovimientoInt" runat="server" Text='<%# resaltarBusqueda(Eval("FMovimientoInt","{0:d}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="F. Op. Int." SortExpression="FOperacionInt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFechaOperacionInt" runat="server" Text='<%# resaltarBusqueda(Eval("FOperacionInt","{0:d}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="M. Int" SortExpression="MontoInt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMontoInt" runat="server" Text='<%# resaltarBusqueda(Eval("MontoInt","{0:c2}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Wrap="True" Width="70px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" Width="70px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Concepto" SortExpression="ConceptoInt">
                                    <ItemTemplate>
                                        <div class="parrafoTexto" style="width: 100px">
                                            <asp:Label ID="lblConceptoInt" runat="server" Text='<%# resaltarBusqueda(Eval("ConceptoInt").ToString()) %>'></asp:Label>
                                        </div>
                                        <asp:HoverMenuExtender ID="hmeConceptoInt" runat="server" TargetControlID="lblConceptoInt"
                                            PopupControlID="pnlPopUpConceptoInt" PopDelay="20" OffsetX="0" OffsetY="0">
                                        </asp:HoverMenuExtender>
                                        <asp:Panel ID="pnlPopUpConceptoInt" runat="server" CssClass="grvResultadoConsultaCss ocultar"
                                            Width="100px" Wrap="True" BackColor="White" Style="padding: 5px 5px 5px 5px">
                                            <asp:Label ID="lblToolTipConceptoInt" runat="server" Text='<%# resaltarBusqueda(Eval("ConceptoInt").ToString()) %>'
                                                CssClass="etiqueta" Font-Size="10px" />
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Justify" Width="100px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripcion" SortExpression="DescripcionInt">
                                    <ItemTemplate>
                                        <div class="parrafoTexto" style="width: 120px">
                                            <asp:Label ID="lblDescripcionInt" runat="server" Text='<%# resaltarBusqueda(Eval("DescripcionInt").ToString()) %>'></asp:Label>
                                        </div>
                                        <asp:HoverMenuExtender ID="hmeDescripcionInt" runat="server" TargetControlID="lblDescripcionInt"
                                            PopupControlID="pnlPopUpDescripcionInt" PopDelay="20" OffsetX="-50" OffsetY="0">
                                        </asp:HoverMenuExtender>
                                        <asp:Panel ID="pnlPopUpDescripcionInt" runat="server" CssClass="grvResultadoConsultaCss ocultar"
                                            Width="150px" Wrap="True" BackColor="White" Style="padding: 5px 5px 5px 5px">
                                            <asp:Label ID="lblToolTipDescripcionInt" runat="server" Text='<%# resaltarBusqueda(Eval("DescripcionInt").ToString()) %>'
                                                CssClass="etiqueta" Font-Size="10px" />
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                                </asp:TemplateField>
                            </Columns>
                            <%-- <PagerTemplate>
                                Página
                                <asp:DropDownList ID="paginasDropDownList" Font-Size="12px" AutoPostBack="true" runat="server"
                                    OnSelectedIndexChanged="paginasDropDownList_SelectedIndexChanged" CssClass="dropDown"
                                    Width="60px">
                                </asp:DropDownList>
                                de
                                <asp:Label ID="lblTotalNumPaginas" runat="server" CssClass="etiqueta" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnInicial" runat="server" CommandName="Page" ToolTip="Prim. Pag"
                                    CommandArgument="First" CssClass="boton pagInicial" />
                                <asp:Button ID="btnAnterior" runat="server" CommandName="Page" ToolTip="Pág. anterior"
                                    CommandArgument="Prev" CssClass="boton pagAnterior" />
                                <asp:Button ID="btnSiguiente" runat="server" CommandName="Page" ToolTip="Sig. página"
                                    CommandArgument="Next" CssClass="boton pagSiguiente" />
                                <asp:Button ID="btnUltima" runat="server" CommandName="Page" ToolTip="Últ. Pag" CommandArgument="Last"
                                    CssClass="boton pagUltima" />
                            </PagerTemplate>--%>
                            <PagerStyle CssClass="grvPaginacionScroll" />
                        </asp:GridView>
                        <asp:GridView ID="grvCantidadReferenciaConcuerdanPedido" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" CssClass="grvResultadoConsultaCss" PageSize="100"
                            OnPageIndexChanging="grvCantidadReferenciaConcuerdanPedido_PageIndexChanging"
                            OnRowDataBound="grvCantidadReferenciaConcuerdanPedido_RowDataBound" ShowHeader="True"
                            ShowHeaderWhenEmpty="True" Width="100%" DataKeyNames="Secuencia,FolioExt,Pedido,Celula,AñoPed"
                            AllowSorting="True" OnRowCreated="grvCantidadReferenciaConcuerdanPedido_RowCreated"
                            OnSorting="grvCantidadReferenciaConcuerdanPedido_Sorting">
                            <%--<EmptyDataTemplate>
                                <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="No existen conciliaciones con Montos Concordantes"></asp:Label>
                            </EmptyDataTemplate>--%>
                            <HeaderStyle HorizontalAlign="Center" />
                            <AlternatingRowStyle BackColor="#CCCCCC" />
                            <Columns>
                                <asp:TemplateField HeaderText="F. Ext" SortExpression="FolioExt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFolioExt" runat="server" Text='<%# resaltarBusqueda(Eval("FolioExt").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle BackColor="#99b433" ForeColor="White" HorizontalAlign="Center" Wrap="True"
                                        Width="30px" />
                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" Width="30px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sec. Ext." SortExpression="Secuencia">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSecuencia" runat="server" Text='<%# resaltarBusqueda(Eval("Secuencia").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="40px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="F. Mov. Ext." SortExpression="FMovimientoExt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFechaMovimientoExt" runat="server" Text='<%# resaltarBusqueda(Eval("FMovimiento","{0:d}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="F. Op. Ext." SortExpression="FOperacionExt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFechaOperacionExt" runat="server" Text='<%# resaltarBusqueda(Eval("FOperacion","{0:d}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="M. Conciliado" SortExpression="MontoConciliadoExt">
                                    <ItemTemplate>
                                        <b>
                                            <asp:Label ID="lblMontoConciliado" runat="server" Text='<%# resaltarBusqueda(Eval("MontoConciliado","{0:c2}").ToString()) %>'></asp:Label></b>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Concepto" SortExpression="ConceptoExt">
                                    <ItemTemplate>
                                        <div class="parrafoTexto">
                                            <asp:Label ID="lblConceptoExt" runat="server" Text='<%# resaltarBusqueda(Eval("Concepto").ToString()) %>'></asp:Label>
                                        </div>
                                        <asp:HoverMenuExtender ID="hmeConceptoExt" runat="server" TargetControlID="lblConceptoExt"
                                            PopupControlID="pnlPopUpConceptoExt" PopDelay="20" OffsetX="-100" OffsetY="-5"
                                            EnableViewState="True">
                                        </asp:HoverMenuExtender>
                                        <asp:Panel ID="pnlPopUpConceptoExt" runat="server" CssClass="grvResultadoConsultaCss ocultar"
                                            Width="300px" Wrap="True" Style="padding: 5px 5px 5px 5px" BackColor="White">
                                            <asp:Label ID="lblToolTipConceptoExt" runat="server" Text='<%# resaltarBusqueda(Eval("Concepto").ToString()) %>'
                                                CssClass="etiqueta centradoJustificado" Font-Size="10px" />
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descrip. Ext." SortExpression="DescripcionExt">
                                    <ItemTemplate>
                                        <div class="parrafoTexto" style="width: 120px">
                                            <asp:Label ID="lblDescripcionExt" runat="server" Text='<%# resaltarBusqueda(Eval("Descripcion").ToString()) %>'></asp:Label>
                                        </div>
                                        <asp:HoverMenuExtender ID="hmeDescripcionExt" runat="server" TargetControlID="lblDescripcionExt"
                                            PopupControlID="pnlPopUpDescripcionExt" PopDelay="20" OffsetX="0" OffsetY="0">
                                        </asp:HoverMenuExtender>
                                        <asp:Panel ID="pnlPopUpDescripcionExt" runat="server" CssClass="grvResultadoConsultaCss ocultar"
                                            Width="100px" Wrap="True" BackColor="White" Style="padding: 5px 5px 5px 5px">
                                            <asp:Label ID="lblToolTipDescripcionExt" runat="server" Text='<%# resaltarBusqueda(Eval("Descripcion").ToString()) %>'
                                                CssClass="etiqueta" Font-Size="10px" />
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <ItemStyle Width="120px"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Documento" SortExpression="PedidoReferencia">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPedidoReferencia" runat="server" Text='<%# resaltarBusqueda(Eval("PedidoReferencia").ToString()) %>' />
                                    </ItemTemplate>
                                    <ControlStyle CssClass="centradoMedio" />
                                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="40px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Celula" SortExpression="Celula">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCelula" runat="server" Text='<%# resaltarBusqueda(Eval("Celula").ToString()) %>' />
                                    </ItemTemplate>
                                    <ControlStyle CssClass="centradoMedio" />
                                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="40px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Pedido" SortExpression="Total">
                                    <ItemTemplate>
                                        <b>
                                            <asp:Label ID="lblMontoPedido" runat="server" Text='<%# resaltarBusqueda(Eval("Total","{0:c2}").ToString()) %>'></asp:Label></b>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label runat="server" ID="lblTotalMontoPedido"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Concepto" SortExpression="ConceptoPedido">
                                    <ItemTemplate>
                                        <div class="parrafoTexto" style="width: 120px">
                                            <asp:Label ID="lblConceptoPedido" runat="server" Text='<%# resaltarBusqueda(Eval("ConceptoPedido").ToString()) %>'></asp:Label>
                                        </div>
                                        <asp:HoverMenuExtender ID="hmeConceptoPedido" runat="server" TargetControlID="lblConceptoPedido"
                                            PopupControlID="pnlPopUpConceptoPedido" PopDelay="20" OffsetX="0" OffsetY="0">
                                        </asp:HoverMenuExtender>
                                        <asp:Panel ID="pnlPopUpConceptoPedido" runat="server" CssClass="grvResultadoConsultaCss ocultar"
                                            Width="100px" Wrap="True" BackColor="White" Style="padding: 5px 5px 5px 5px">
                                            <asp:Label ID="lblToolTipConceptoPedido" runat="server" Text='<%# resaltarBusqueda(Eval("ConceptoPedido").ToString()) %>'
                                                CssClass="etiqueta" Font-Size="10px" />
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nom. Cliente" SortExpression="Nombre">
                                    <ItemTemplate>
                                        <div class="parrafoTexto" style="width: 150px">
                                            <asp:Label ID="lblCliente" runat="server" Text='<%# resaltarBusqueda(Eval("Nombre").ToString()) %>'></asp:Label>
                                        </div>
                                        <asp:HoverMenuExtender ID="hmeCliente" runat="server" TargetControlID="lblCliente"
                                            PopupControlID="pnlPopUpCliente" PopDelay="20" OffsetX="0" OffsetY="0">
                                        </asp:HoverMenuExtender>
                                        <asp:Panel ID="pnlPopUpCliente" runat="server" CssClass="grvResultadoConsultaCss ocultar"
                                            Width="150px" Wrap="True" BackColor="White" Style="padding: 5px 5px 5px 5px">
                                            <asp:Label ID="lblToolTipCliente" runat="server" Text='<%# resaltarBusqueda(Eval("Nombre").ToString()) %>'
                                                CssClass="etiqueta" Font-Size="10px" />
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="150px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="SerieFactura" SortExpression="SerieFactura" >
                                    <ItemTemplate>
                                        <div class="parrafoTexto" style="width: 80px">
                                            <asp:Label runat="server" ID="lblSerieFactura" Text="SERIEFACTURA"> </asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="ClienteReferencia" SortExpression="ClienteReferencia" >
                                    <ItemTemplate>
                                        <div class="parrafoTexto" style="width: 80px">
                                            <asp:Label runat="server" ID="lblClienteReferencia" Text="CLIENTEREFERENCIA"> </asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <%--   <PagerTemplate>
                                Página
                                <asp:DropDownList ID="paginasDropDownListPedidos" runat="server" AutoPostBack="true"
                                    CssClass="dropDown" Font-Size="12px" Width="60px" OnSelectedIndexChanged="paginasDropDownListPedidos_SelectedIndexChanged">
                                </asp:DropDownList>
                                de
                                <asp:Label ID="lblTotalNumPaginas" runat="server" CssClass="etiqueta"></asp:Label>
                                &nbsp;&nbsp;
                                <asp:Button ID="btnInicial" runat="server" CommandArgument="First" CommandName="Page"
                                    CssClass="boton pagInicial" ToolTip="Prim. Pag" />
                                <asp:Button ID="btnAnterior" runat="server" CommandArgument="Prev" CommandName="Page"
                                    CssClass="boton pagAnterior" ToolTip="Pág. anterior" />
                                <asp:Button ID="btnSiguiente" runat="server" CommandArgument="Next" CommandName="Page"
                                    CssClass="boton pagSiguiente" ToolTip="Sig. página" />
                                <asp:Button ID="btnUltima" runat="server" CommandArgument="Last" CommandName="Page"
                                    CssClass="boton pagUltima" ToolTip="Últ. Pag" />
                            </PagerTemplate>--%>
                            <PagerStyle CssClass="grvPaginacionScroll" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:HiddenField runat="server" ID="hdfCerrarBuscar" />
    <asp:ModalPopupExtender ID="mpeBuscar" runat="server" BackgroundCssClass="ModalBackground"
        DropShadow="False" EnableViewState="false" PopupControlID="pnlBuscar" TargetControlID="hdfCerrarBuscar"
        CancelControlID="btnCerrarBuscar" BehaviorID="mpeBuscar">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlBuscar" runat="server" CssClass="ModalPopup" Width="400px" Style="display: none">
        <asp:UpdatePanel runat="server" ID="upBuscar">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr class="bg-color-grisOscuro">
                        <td colspan="4" style="padding: 5px 5px 5px 5px" class="etiqueta">
                            <div class="floatDerecha">
                                <asp:ImageButton runat="server" ID="btnCerrarBuscar" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png"
                                    CssClass="iconoPequeño bg-color-rojo" OnClientClick="ModalPopupBuscar('O');" />
                            </div>
                            <div class="fg-color-blanco">
                                Buscar
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 5px 5px 5px 5px; width: 85%">
                            <div class="etiqueta">
                                Valor
                            </div>
                            <asp:TextBox ID="txtBuscar" runat="server" CssClass="cajaTexto" Font-Size="12px"
                                Width="95%">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5%">
                            <asp:Button ID="btnIrBuscar" runat="server" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                Text="BUSCAR" OnClick="btnIrBuscar_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="bg-color-grisClaro01" colspan="4"></td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <asp:UpdatePanel runat="server" ID="upModalesOpciones">
        <ContentTemplate>

            <%--No puede ser manejado desde JavaScript--%>
            <asp:HiddenField runat="server" ID="hdfCerrar" />
            <asp:ModalPopupExtender ID="mpeFiltrar" runat="server" BackgroundCssClass="ModalBackground"
                DropShadow="False" EnableViewState="false" PopupControlID="pnlFiltrar" TargetControlID="hdfCerrar">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlFiltrar" runat="server" CssClass="ModalPopup" Width="500px" Style="display: none">
                <table style="width: 100%;">
                    <tr class="bg-color-grisOscuro">
                        <td colspan="4" style="padding: 5px 5px 5px 5px" class="etiqueta">
                            <div class="floatDerecha">
                                <asp:ImageButton runat="server" ID="btnCerrar" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png"
                                    CssClass="iconoPequeño bg-color-rojo" />
                            </div>
                            <div class="fg-color-blanco">
                                FILTRAR
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 5px 5px 5px 5px; width: 50%">
                            <div class="etiqueta">
                                Campo
                            </div>
                            <asp:DropDownList runat="server" ID="ddlCampoFiltrar" CssClass="dropDown etiqueta"
                                ClientIDMode="Static" ValidationGroup="Filtrar" Width="100%" Font-Size="10px"
                                AutoPostBack="False" />
                            <%--OnSelectedIndexChanged="ddlCampoFiltrar_SelectedIndexChanged"--%>
                        </td>
                        <td style="padding: 5px 5px 5px 5px; width: 30%">
                            <div class="etiqueta">
                                Operacion
                            </div>
                            <asp:DropDownList runat="server" ID="ddlOperacion" CssClass="dropDown centradoMedio etiqueta"
                                ValidationGroup="Filtrar" Width="100%" Font-Size="10px">
                                <asp:ListItem Selected="True" Value="=">IGUAL</asp:ListItem>
                                <asp:ListItem Value="&gt;">MAYOR</asp:ListItem>
                                <asp:ListItem Value="&lt;">MENOR</asp:ListItem>
                                <asp:ListItem Value="&gt;=">MAYOR O IGUAL</asp:ListItem>
                                <asp:ListItem Value="&lt;=">MENOR O IGUAL</asp:ListItem>
                                <asp:ListItem Value="&lt;&gt;">DIFERENTE</asp:ListItem>
                                <asp:ListItem Value="LIKE">LIKE</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 5px 5px 5px 5px; width: 15%" colspan="2">
                            <div class="etiqueta">
                                Valor
                            </div>
                            <asp:TextBox ID="txtValor" runat="server" CssClass="cajaTexto" Font-Size="12px" Width="98%">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%;" class="centradoDerecha" colspan="3">
                            <asp:Button ID="btnIrFiltro" runat="server" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                Text="Filtrar" ValidationGroup="Filtro" OnClick="btnIrFiltro_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="bg-color-grisClaro01" colspan="4"></td>
                    </tr>
                </table>
            </asp:Panel>


            <asp:HiddenField runat="server" ID="hdfCerrarDetalle" />
            <asp:ModalPopupExtender ID="mpeLanzarDetalle" runat="server" BackgroundCssClass="ModalBackground"
                DropShadow="False" EnableViewState="false" PopupControlID="pnlDetalle" TargetControlID="hdfCerrarDetalle"
                CancelControlID="btnCerrarDetalle">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlDetalle" runat="server" CssClass="ModalPopup" EnableViewState="false"
                Width="880px" Style="display: none">
                <table style="width: 100%;">
                    <tr class="bg-color-grisOscuro">
                        <td colspan="5" style="padding: 5px 5px 5px 5px" class="etiqueta">
                            <div class="floatDerecha bg-color-grisClaro01">
                                <asp:ImageButton runat="server" ID="btnCerrarDetalle" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png"
                                    CssClass="iconoPequeño bg-color-rojo" />
                            </div>
                            <div class="fg-color-blanco centradoJustificado">
                                DETALLE : TRANSACCIÓN CONCILIADA :
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 5px 5px 5px 5px; width: 100%; text-align: center">
                            <asp:GridView ID="grvDetalleArchivoInterno" runat="server" AutoGenerateColumns="False"
                                ShowHeader="True" CssClass="grvResultadoConsultaCss" ShowHeaderWhenEmpty="True"
                                ShowFooter="False" DataKeyNames="SecuenciaInterno, FolioInterno">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblvacio" runat="server" Font-Bold="True" Font-Overline="False" ForeColor="#CC3300"
                                        Text="No se encontraron referencias internas"></asp:Label>
                                </EmptyDataTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Image ID="img" runat="server" CssClass="icono" Height="15px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Exito.png"
                                                Width="15px"></asp:Image>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" CssClass="bg-color-verdeClaro centradoMedio"
                                            Width="20px"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Secuencia" SortExpression="secuenciaInt">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSecuenciaInt" runat="server" Text='<%# Bind("SecuenciaInterno") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="centradoMedio" />
                                        <ItemStyle HorizontalAlign="Center" BackColor="#ebecec" Width="50px"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Folio" SortExpression="folioInterno">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFolioInterno" runat="server" Text='<%# Bind("FolioInterno") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="centradoMedio" />
                                        <ItemStyle HorizontalAlign="Center" BackColor="#ebecec" Width="100px"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FMovimiento" SortExpression="fMovimiento">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFMovimiento" runat="server" Text='<%# Bind("FMovimientoInt","{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="centradoMedio" />
                                        <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FOperacion" SortExpression="fOperacion">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFOperacion" runat="server" Text='<%# Bind("FOperacionInt","{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="centradoMedio" />
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Monto" SortExpression="monto">
                                        <ItemTemplate>
                                            <b>
                                                <asp:Label ID="lblMonto" runat="server" Text='<%# Bind("MontoInterno","{0:c2}") %>'></asp:Label></b>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Concepto" SortExpression="concepto">
                                        <ItemTemplate>
                                            <div class="parrafoTexto" style="width: 500px">
                                                <asp:Label ID="lblConceptoInt" runat="server" Text='<%# Bind("ConceptoInterno") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="500px"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Width="500px"></HeaderStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:GridView ID="grvDetallePedidoInterno" runat="server" AutoGenerateColumns="False"
                                ShowHeader="True" Width="100%" CssClass="grvResultadoConsultaCss" ShowHeaderWhenEmpty="True"
                                DataKeyNames="Celula,Pedido,AñoPed">
                                <HeaderStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Image ID="img" runat="server" CssClass="icono" Height="15px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Exito.png"
                                                Width="15px"></asp:Image>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" CssClass="bg-color-verdeClaro centradoMedio"
                                            Width="30px"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ped." SortExpression="Pedido">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPedido" runat="server" Text='<%# Eval("Pedido") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" BackColor="#d9b335" ForeColor="White" Width="50px"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Celula" SortExpression="Celula">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCelula" runat="server" Text='<%# Eval("Celula") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Width="40px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Pedido" SortExpression="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMontoPedido" runat="server" Text='<%# Eval("Total","{0:c2}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Concepto" SortExpression="ConceptoPedido">
                                        <ItemTemplate>
                                            <div class="parrafoTexto" style="width: 350px">
                                                <asp:Label ID="lblConceptoPedido" runat="server" Text='<%# Eval("ConceptoPedido") %>'></asp:Label>
                                            </div>
                                            <asp:HoverMenuExtender ID="hmeConceptoPedido" runat="server" TargetControlID="lblConceptoPedido"
                                                PopupControlID="pnlPopUpConceptoPedido" PopDelay="20" OffsetX="-20" OffsetY="-10">
                                            </asp:HoverMenuExtender>
                                            <asp:Panel ID="pnlPopUpConceptoPedido" runat="server" CssClass="grvResultadoConsultaCss ocultar"
                                                Width="400px" Wrap="True" BackColor="White" Style="padding: 5px 5px 5px 5px">
                                                <asp:Label ID="lblToolTipConceptoPedido" runat="server" Text='<%# Eval("ConceptoPedido") %>'
                                                    CssClass="etiqueta" Font-Size="10px" />
                                            </asp:Panel>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Justify" Width="400px"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Width="400px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nom. Cliente" SortExpression="Nombre">
                                        <ItemTemplate>
                                            <div class="parrafoTexto" style="width: 350px">
                                                <asp:Label ID="lblCliente" runat="server" Text='<%# Eval("Nombre") %>'></asp:Label>
                                            </div>
                                            <asp:HoverMenuExtender ID="hmeCliente" runat="server" TargetControlID="lblCliente"
                                                PopupControlID="pnlPopUpCliente" PopDelay="20" OffsetX="-20" OffsetY="-10">
                                            </asp:HoverMenuExtender>
                                            <asp:Panel ID="pnlPopUpCliente" runat="server" CssClass="grvResultadoConsultaCss ocultar"
                                                Width="300px" Wrap="True" BackColor="White" Style="padding: 5px 5px 5px 5px">
                                                <asp:Label ID="lblToolTipCliente" runat="server" Text='<%# Eval("Nombre") %>' CssClass="etiqueta"
                                                    Font-Size="10px" />
                                            </asp:Panel>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Justify" Width="350px"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Width="350px"></HeaderStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" class="bg-color-grisClaro01">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <%--No puede ser manejado desde JavaScript--%>


            <%--No se utilizan--%>
            <asp:HiddenField runat="server" ID="hdfDesconciliarConfirmar" />
            <asp:ModalPopupExtender ID="mpeConfirmarDesconciliado" runat="server" BackgroundCssClass="ModalBackground"
                DropShadow="False" EnableViewState="false" PopupControlID="pnlConfirmarDesconciliar"
                TargetControlID="hdfDesconciliarConfirmar" CancelControlID="btnCancelarConfirmarDesconciliar">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlConfirmarDesconciliar" runat="server" CssClass="ModalPopup" EnableViewState="false"
                Width="350px" Style="display: none">
                <table style="width: 100%;">
                    <tr class="bg-color-grisOscuro">
                        <td colspan="5" style="padding: 5px 5px 5px 5px" class="etiqueta">
                            <div class="lineaHorizontal fg-color-blanco">
                                CONFIRMAR DESCONCILIACIÓN Folio:
                                <asp:Label runat="server" ID="lblFolioTC"></asp:Label>
                                :Secuencia
                                <asp:Label runat="server" ID="lblSecuenciaTC"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 5px 5px 5px 5px; width: 10%" class="lineaVertical">
                            <asp:Image runat="server" ID="imgAdvertencia" CssClass="icono bg-color-amarillo"
                                ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Advertencia.png" />
                        </td>
                        <td style="padding: 5px 5px 5px 5px; width: 90%; vertical-align: top">
                            <div class="etiqueta">
                                ¿Esta seguro de <b class="fg-color-rojo">DESCONCILIAR</b> la transaccion externa?<br />
                                ¡Se actualizará la conciliacion y su detalle!
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="centradoMedio" colspan="2">
                            <div class="lineaHorizontal">
                            </div>
                            <asp:Button runat="server" ID="btnAceptarConfirmar" Text="ACEPTAR" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                OnClick="btnAceptarConfirmarDesconciliar_Click" />
                            <asp:Button runat="server" ID="btnCancelarConfirmarDesconciliar" Text="CANCELAR"
                                CssClass="boton fg-color-blanco bg-color-grisClaro" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="bg-color-grisClaro01">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>


            <asp:HiddenField ID="hdfCerrarConfirmar" runat="server" />
            <asp:ModalPopupExtender ID="mpeConfirmarCerrar" runat="server" BackgroundCssClass="ModalBackground"
                CancelControlID="btnCancelarConfirmarCerrar" DropShadow="False" EnableViewState="false"
                PopupControlID="pnlConfirmarCerrar" TargetControlID="hdfCerrarConfirmar">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlConfirmarCerrar" runat="server" CssClass="ModalPopup" EnableViewState="false"
                Style="display: none" Width="350px">
                <table style="width: 100%;">
                    <tr class="bg-color-grisOscuro">
                        <td class="etiqueta" colspan="5" style="padding: 5px 5px 5px 5px">
                            <div class="fg-color-blanco">
                                CONFIRMAR CERRADO: CONCILIACIÓN:
                                <asp:Label ID="lblFolioConciliacionCerrar" runat="server"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="lineaVertical" style="padding: 5px 5px 5px 5px; width: 10%">
                            <asp:Image ID="imgAdvertenciaCerrar" runat="server" CssClass="icono bg-color-amarillo"
                                ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Advertencia.png" />
                        </td>
                        <td style="padding: 5px 5px 5px 5px; width: 90%; vertical-align: top">
                            <div class="etiqueta">
                                ¿Esta seguro de <b class="fg-color-rojo">CERRAR</b> la conciliación actual?<br />
                                ¡Se dara de baja definitiva dicha conciliacion, para efectos de seguridad!
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="centradoMedio" colspan="2">
                            <div class="lineaHorizontal">
                            </div>
                            <asp:Button ID="btnAceptarConfirmarCerrar" runat="server" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                OnClick="btnAceptarConfirmarCerrar_Click" Text="ACEPTAR" />
                            <asp:Button ID="btnCancelarConfirmarCerrar" runat="server" CssClass="boton fg-color-blanco bg-color-grisClaro"
                                Text="CANCELAR" />
                        </td>
                    </tr>
                    <tr>
                        <td class="bg-color-grisClaro01" colspan="2">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>


            <asp:HiddenField ID="hdfCancelarConfirmar" runat="server" />
            <asp:ModalPopupExtender ID="mpeConfirmarCancelar" runat="server" BackgroundCssClass="ModalBackground"
                CancelControlID="btnCancelarConfirmarCancelar" DropShadow="False" EnableViewState="false"
                PopupControlID="pnlConfirmarCancelar" TargetControlID="hdfCancelarConfirmar">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlConfirmarCancelar" runat="server" CssClass="ModalPopup" EnableViewState="false"
                Style="display: none" Width="350px">
                <table style="width: 100%;">
                    <tr class="bg-color-grisOscuro">
                        <td class="etiqueta" colspan="5" style="padding: 5px 5px 5px 5px">
                            <div class="fg-color-blanco">
                                CONFIRMAR CANCELACIÓN: CONCILIACIÓN:
                                <asp:Label ID="lblFolioConciliacionCancelacion" runat="server"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="lineaVertical" style="padding: 5px 5px 5px 5px; width: 10%">
                            <asp:Image ID="imgAdvertenciaCancelar" runat="server" CssClass="icono bg-color-amarillo"
                                ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Advertencia.png" />
                        </td>
                        <td style="padding: 5px 5px 5px 5px; width: 90%; vertical-align: top">
                            <div class="etiqueta">
                                ¿Esta seguro de <b class="fg-color-rojo">CANCELAR</b> la conciliación actual?<br />
                                ¡Se dara de baja definitiva dicha conciliacion, para efectos de seguridad!
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="centradoMedio" colspan="2">
                            <div class="lineaHorizontal">
                            </div>
                            <asp:Button ID="btnAceptarConfirmarCancelar" runat="server" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                OnClick="btnAceptarConfirmarCancelar_Click" Text="ACEPTAR" />
                            <asp:Button ID="btnCancelarConfirmarCancelar" runat="server" CssClass="boton fg-color-blanco bg-color-grisClaro"
                                Text="CANCELAR" />
                        </td>
                    </tr>
                    <tr>
                        <td class="bg-color-grisClaro01" colspan="2">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <%--No se utilizan--%>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%--No puede ser manejado desde JavaScript--%>
    <asp:HiddenField runat="server" ID="hdfImportarArchivos" />
    <asp:ModalPopupExtender ID="popUpImportarArchivos" runat="server" PopupControlID="pnlImportarArchivos"
        TargetControlID="hdfImportarArchivos" BehaviorID="ModalBehaviour" BackgroundCssClass="ModalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlImportarArchivos" runat="server" BackColor="#FFFFFF" Width="50%"
        Style="display: none">
        <asp:UpdatePanel ID="upImportarArchivos" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr class="bg-color-grisOscuro">
                        <td colspan="4" style="padding: 5px 5px 5px 5px" class="etiqueta">
                            <div class="floatDerecha">
                                <asp:ImageButton runat="server" ID="imgCerrarImportar" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png"
                                    CssClass="iconoPequeño bg-color-rojo" OnClick="imgCerrarImportar_Click" />
                            </div>
                            <div class="fg-color-blanco">
                                IMPORTAR ARCHIVO
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="datos-estilo" style="padding: 10px 10px 10px 10px">
                            <div class="etiqueta">
                                Tipo Fuente Informacion
                            </div>
                            <asp:DropDownList ID="ddlTipoFuenteInfoInterno" runat="server" AutoPostBack="True"
                                Width="100%" CssClass="dropDown" OnSelectedIndexChanged="ddlTipoFuenteInfoInterno_SelectedIndexChanged"
                                OnDataBound="ddlTipoFuenteInfoInterno_DataBound">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvTipoFuenteInfoInterno" runat="server" ControlToValidate="ddlTipoFuenteInfoInterno"
                                ErrorMessage="Especifique el Tipo Fuente Información." Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="Interno"></asp:RequiredFieldValidator>
                            <div class="etiqueta">
                                Sucursal
                            </div>
                            <asp:DropDownList ID="ddlSucursalInterno" runat="server" AutoPostBack="True" Width="100%"
                                CssClass="dropDown" OnSelectedIndexChanged="ddlSucursalInterno_SelectedIndexChanged">
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
                                            OnDataBound="ddlFolioInterno_DataBound">
                                        </asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvFolioInterno" runat="server" ControlToValidate="ddlFolioInterno"
                                            ErrorMessage="Especifique el Folio." Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                            ValidationGroup="Interno"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="vertical-align: top; padding: 6px;">
                                        <asp:ImageButton ID="btnAñadirFolio" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Agregar.png"
                                            Width="16px" Height="16px" ValidationGroup="Interno" CssClass="icono bg-color-verdeClaro"
                                            OnClick="btnAñadirFolio_Click" />
                                    </td>
                                    <td style="vertical-align: top; padding: 6px;">
                                        <asp:ImageButton ID="btnVerDatalleInterno" runat="server" ImageAlign="AbsMiddle"
                                            ValidationGroup="Interno" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/buscar.png"
                                            Width="16px" Height="16px" CssClass="icono bg-color-azulClaro" OnClick="btnVerDatalleInterno_Click" />
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 40%">
                                        <div class="etiqueta">
                                            Usuario Alta
                                        </div>
                                        <asp:TextBox ID="lblUsuarioAltaEx" runat="server" Width="95%" CssClass="cajaTexto"
                                            Enabled="False"></asp:TextBox>
                                    </td>
                                    <td style="width: 1%"></td>
                                    <td style="width: 60%">
                                        <div class="etiqueta">
                                            Status
                                        </div>
                                        <asp:TextBox ID="lblStatusFolioInterno" runat="server" Width="100%" CssClass="cajaTexto"
                                            Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="datos-estilo" style="padding: 10px 10px 10px 10px; width: 50%">
                            <div class="etiqueta">
                                Folios Agregados
                            </div>
                            <asp:GridView ID="grvAgregados" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                BorderStyle="Dotted" CssClass="grvResultadoConsultaCss" Font-Size="12px" ShowHeaderWhenEmpty="True"
                                Width="90%" ShowHeader="False" BorderColor="White" DataKeyNames="Folio" PageSize="6"
                                OnRowDeleting="grvAgregados_RowDeleting" OnPageIndexChanging="grvAgregados_PageIndexChanging">
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
                                <PagerStyle CssClass="grvPaginacionScroll" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="centradoMedio">
                            <asp:Button ID="btnGuardarInterno" runat="server" Text="GUARDAR" ToolTip="GUARDAR"
                                CssClass="boton fg-color-blanco bg-color-verdeClaro" OnClick="btnGuardarInterno_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="bg-color-grisClaro01" colspan="2"></td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <asp:HiddenField ID="hdfDetalleInternoOculto" runat="server" />
    <asp:ModalPopupExtender ID="grvVistaRapidaInterno_ModalPopupExtender" runat="server"
        PopupControlID="pnlVistaRapidaInterno" TargetControlID="hdfDetalleInternoOculto"
        BackgroundCssClass="ModalBackground" BehaviorID="ModalBehaviourInterno">
    </asp:ModalPopupExtender>
    <asp:Panel runat="server" ID="pnlVistaRapidaInterno" HorizontalAlign="Center" CssClass="ModalPopup"
        EnableViewState="False" Style="display: none">
        <asp:UpdatePanel ID="upDetalleInterno" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr class="bg-color-grisOscuro">
                        <td style="padding: 5px 5px 5px 5px" class="etiqueta">
                            <div class="floatDerecha bg-color-grisClaro01">
                                <asp:ImageButton runat="server" ID="imgCerrarDetalleInterno" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png"
                                    CssClass="iconoPequeño bg-color-rojo" OnClientClick="HideModalPopupInterno();" />
                            </div>
                            <div class="fg-color-blanco">
                                DETALLE FOLIO INTERNO SELECCIONADO [<asp:Label runat="server" ID="lblFolioInterno"></asp:Label>]
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
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <%--No puede ser manejado desde JavaScript--%>

    <asp:UpdateProgress ID="panelBloqueo" runat="server">
        <ProgressTemplate>
            <asp:Image ID="imgLoad" runat="server" CssClass="icono bg-color-blanco" Height="40px"
                ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/LoadPage.gif" Width="40px" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ModalPopupExtender ID="mpeLoading" runat="server" BackgroundCssClass="ModalBackground"
        PopupControlID="panelBloqueo" TargetControlID="panelBloqueo">
    </asp:ModalPopupExtender>
</asp:Content>
