<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" EnableViewState="true"
    CodeFile="Inicio.aspx.cs" Inherits="Inicio" Debug="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/ControlesUsuario/BuscadorPagoEstadoCuenta/wucBuscadorPagoEstadoCuenta.ascx" TagPrefix="uc1" TagName="wucBuscadorPagoEstadoCuenta" %>


<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    CONCILIACIÓN
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <script src="/App_Scripts/jQueryScripts/jquery-3.2.1.min.js" type="text/javascript"></script>
    <script src="/App_Scripts/jQueryScripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="/App_Scripts/Common.js" type="text/javascript"></script>

    <script src="/../../App_Scripts/jQueryScripts/jquery.ui.datepicker-es.js" type="text/javascript"></script>
    <link href="/App_Scripts/jQueryScripts/css/custom-theme/jquery-ui-1.10.2.custom.min.css" rel="stylesheet" type="text/css" />

    <link href="/App_Scripts/MenuContextual/css/Estilo.css" rel="stylesheet" type="text/css" />
    <!--  MenuContextual -->
    <script type="text/javascript">
        function pageLoad() {
            var hiddenField = document.getElementById("<%=fldIndiceConcilacion.ClientID %>");
            var gridviewID = "<%=grvConciliacion.ClientID%>";
            var rowid = 0;

            $("#<%= txtFinicio.ClientID%>").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd/mm/yy',
                numberOfMonths: 1,
                onClose: function (selectedDate) {
                    $("#<%=txtFfinal.ClientID%>").datepicker("option", "minDate", selectedDate);
                }
            });
            $("#<%=txtFfinal.ClientID%>").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                dateFormat: 'dd/mm/yy',
                changeYear: true,
                numberOfMonths: 1,
                onClose: function (selectedDate) {
                    $("#<%=txtFinicio.ClientID%>").datepicker("option", "maxDate", selectedDate);
                }
            });
        
            //Funciones Click Derecho sobre GridView
            $("#<%=miMenu.ClientID%>").hide();
            $("table[id$='grvConciliacion'] > tbody > tr").bind('contextmenu', function (e) {
                $("#<%=miMenu.ClientID%>").hide();
                e.preventDefault();
                rowid = $(this).children(':first-child').text();
                if (!isNaN(rowid)) {
                    hiddenField.value = rowid;
                    $("#<%=miMenu.ClientID%>").css({
                        top: e.pageY + "px",
                        left: e.pageX + "px",
                        position: 'absolute'
                    });
                    $("#<%=miMenu.ClientID%>").show();
                }
            });
            $(document).bind('click', function (e) {
                $("#<%=miMenu.ClientID%>").hide();
            });
            gridview = $('#' + gridviewID);

            //activarDatePickers();
        }

        function activarDatePickers() {
            // DatePickers BUSCA MONTO EN EDO CTA
            $("#<%= txtFinicio.ClientID%>").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                onClose: function (selectedDate) {
                    $("#<%= txtFfinal.ClientID%>").datepicker("option", "minDate", selectedDate);
                }
            });
            $("#<%= txtFfinal.ClientID%>").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                onClose: function (selectedDate) {
                    $("#<%= txtFinicio.ClientID%>").datepicker("option", "maxDate", selectedDate);
                }
            });
        }

    </script>
    <script src="App_Scripts/jsHoverGridView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager ID="smActualizar" runat="server">
    </asp:ScriptManager>
    <script src="App_Scripts/jsUpdateProgress.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        var ModalProgress = '<%= mpeLoading.ClientID %>';        
    </script>
    <asp:UpdatePanel runat="server" ID="upInicio" UpdateMode="Always" >
        <ContentTemplate>
            <script type="text/javascript">
                function fnVerConciliacion() {
                    var lnkVer = document.getElementById("<%=lnkVer.ClientID %>");
                    lnkVer.click();
                }
                function fnDetalle() {
                    var lnkDetalle = document.getElementById("<%=lnkDetalle.ClientID %>");
                    lnkDetalle.click();
                }
                function fnPagos() {
                    var lnkPagos = document.getElementById("<%=lnkPagos.ClientID %>");
                    lnkPagos.click();
                }

                function fnInforme() {
                    var lnkInforme = document.getElementById("<%=lnkInforme.ClientID %>");
                    lnkInforme.click();
                }
                function fnConsultar() {
                    var lnkConsultar = document.getElementById("<%=lnkConsultar.ClientID %>");
                    lnkConsultar.click();
                }

                $(document).keypress(function (e) {
                    if (e.which == 2) {
                        console.log("ctrl B");
                        $find('ModalBehaviorBuscadorPagoEdoCta').show();
                    }
                });

                function OcultarPopUpBuscadorPagoEdoCta() {
                    $find("ModalBehaviorBuscadorPagoEdoCta").hide();
                }


            </script>
            <table style="width: 100%">
                <tr>
                    <td style="width: 300px; vertical-align: top" class="Filtrado">
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
                                ValidationGroup="Conciliacion"></asp:RequiredFieldValidator>
                            <div class="etiqueta">
                                Sucursal
                            </div>
                            <asp:DropDownList ID="ddlSucursal" runat="server" Width="100%" CssClass="dropDown"
                                AutoPostBack="False">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="rfSucursal" runat="server" ErrorMessage=" Especifique la sucursal. "
                                ControlToValidate="ddlSucursal" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="Conciliacion"></asp:RequiredFieldValidator>
                            <div class="etiqueta">
                                Grupo Conciliación
                            </div>
                            <asp:DropDownList ID="ddlGrupo" runat="server" Width="100%" CssClass="dropDown" AutoPostBack="False">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvGrupo" runat="server" ErrorMessage=" Especifique el grupo. "
                                ControlToValidate="ddlGrupo" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="Conciliacion"></asp:RequiredFieldValidator>
                            <div class="etiqueta">
                                Tipo Conciliación
                            </div>
                            <asp:DropDownList ID="ddlTipoConciliacion" runat="server" Width="100%" CssClass="dropDown"
                                AutoPostBack="False">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvTipoConciliacion" runat="server" ErrorMessage=" Especifique el Tipo Conciliación. "
                                ControlToValidate="ddlTipoConciliacion" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="Conciliacion"></asp:RequiredFieldValidator>
                            <div class="etiqueta">
                                Status Conciliación
                            </div>
                            <asp:DropDownList ID="ddlStatusConciliacion" Width="100%" CssClass="dropDown" AutoPostBack="False"
                                runat="server">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ErrorMessage=" Especifique el Status. "
                                ControlToValidate="ddlStatusConciliacion" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="Conciliacion"></asp:RequiredFieldValidator>
                            <div class="etiqueta">
                                Año
                            </div>
                            <asp:DropDownList ID="ddlAñoConciliacion" runat="server" CssClass="dropDown" Width="100%"
                                AutoPostBack="False">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvAñoConciliacion" runat="server" ErrorMessage=" Especifique el Año de la Conciliación. "
                                ControlToValidate="ddlAñoConciliacion" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="Conciliacion"></asp:RequiredFieldValidator>
                            <div class="etiqueta">
                                Mes
                            </div>
                            <asp:DropDownList ID="ddlMesConciliacion" runat="server" Width="100%" CssClass="dropDown"
                                AutoPostBack="False">
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
                            <asp:RequiredFieldValidator ID="rfvMes" runat="server" ErrorMessage=" Especifique el Mes de la Conciliación. "
                                ControlToValidate="ddlMesConciliacion" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="Conciliacion"></asp:RequiredFieldValidator>
                            <div class="centradoMedio">
                                <asp:Button ID="btnConsultar" runat="server" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                    Text="CONSULTAR" ToolTip="CONSULTAR" ValidationGroup="Conciliacion" OnClick="btnConsultar_Click" />
                                <asp:Button ID="btnNuevaConciliacion" runat="server" CssClass="boton fg-color-blanco bg-color-naranja"
                                    Text="NUEVA CONCILIACIÓN" ToolTip="NUEVA CONCILIACIÓN" OnClick="btnNuevaConciliacion_Click" />
                            </div>
                        </div>
                    </td>
                    <td style="vertical-align: top">
                        <div class="datos-estilo">
                            <div class="titulo" style="margin-left: 0px">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 95%">
                                            Conciliaciones
                                        </td>
                                        <td>
                                            <img src="App_Themes/GasMetropolitanoSkin/Imagenes/grid.png" alt="Consulta" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="lineaHorizontal">
                            </div>
                            <br />
                            <asp:GridView ID="grvConciliacion" runat="server" AutoGenerateColumns="False" Width="100%"
                                AllowPaging="True" ShowHeaderWhenEmpty="True" CssClass="grvResultadoConsultaCss"
                                DataKeyNames="FolioConciliacion" PageSize="12" OnPageIndexChanging="grvConciliacion_PageIndexChanging"
                                OnRowDataBound="grvConciliacion_RowDataBound" OnRowCreated="grvConciliacion_RowCreated"
                                AllowSorting="True" OnSorting="grvConciliacion_Sorting" >
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="No existe ninguna conciliación de acuerdo a los Parámetros del Filtrado. "></asp:Label>
                                </EmptyDataTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="lblIndice" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" CssClass="ocultar" Width="10px" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" CssClass="ocultar" Width="10px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="F. Con." SortExpression="FolioConciliacion">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFolioConciliacion" runat="server" Text='<%# Eval("FolioConciliacion") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="centradoMedio"></ControlStyle>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="True" BackColor="#ebecec" ForeColor="Black"
                                            CssClass="centradoMedio" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Banco" SortExpression="Banco">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBanco" runat="server" Text='<%# Eval("Banco") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="centradoMedio"></ControlStyle>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" CssClass="centradoMedio" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="C. Banc." SortExpression="CuentaBancaria">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCuentaBancaria" runat="server" Text='<%# Eval("CuentaBancaria") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="centradoMedio"></ControlStyle>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="True" CssClass="centradoMedio" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="F. Inicial" SortExpression="FInicial">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFechaInicial" runat="server" Text='<%# Eval("FInicial", "{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="F. Final" SortExpression="FFinal">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFechaFinal" runat="server" Text='<%# Eval("FFinal","{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="T. Tran. Int." SortExpression="TransaccionesInternas">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransaccionesInternas" runat="server" Text='<%# Eval("TransaccionesInternas") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="T. Tran. Ext." SortExpression="TransaccionesExternas">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransaccionesExternas" runat="server" Text='<%# Eval("TransaccionesExternas") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Con. Ext" SortExpression="ConciliacionesExternas">
                                        <ItemTemplate>
                                            <asp:Label ID="lblConciliacionesExternas" runat="server" Text='<%# Eval("ConciliacionesExternas") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Con. Int." SortExpression="ConciliacionesInternas">
                                        <ItemTemplate>
                                            <asp:Label ID="lblConciliacionesInternas" runat="server" Text='<%# Eval("ConciliacionesInternas") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Grp. Con." SortExpression="GrupoConciliacionstr">
                                        <ItemTemplate>
                                            <div class="parrafoTexto">
                                                <asp:Label ID="lblGrupo" runat="server" Text='<%# Eval("GrupoConciliacionstr") %>'></asp:Label>
                                            </div>
                                            <asp:HoverMenuExtender ID="hmeGrupo" runat="server" TargetControlID="lblGrupo" PopupControlID="pnlPopUpConceptoExt"
                                                PopDelay="20" OffsetX="0" OffsetY="0">
                                            </asp:HoverMenuExtender>
                                            <asp:Panel ID="pnlPopUpConceptoExt" runat="server" CssClass="grvResultadoConsultaCss ocultar"
                                                Width="150px" Style="padding: 5px 5px 5px 5px" BackColor="White" Wrap="True">
                                                <asp:Label ID="lblToolTipConceptoExt" runat="server" Text='<%# Eval("GrupoConciliacionstr") %>'
                                                    CssClass="etiqueta " Font-Size="10px" />
                                            </asp:Panel>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sucursal" SortExpression="SucursalDes">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSucursalDes" runat="server" Text='<%# Eval("SucursalDes") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sts." SortExpression="StatusConciliacion">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblStatusConciliacion" Text='<%# Bind("StatusConciliacion") %>'
                                                Style="display: none"></asp:Label>
                                            <asp:Image runat="server" ID="imgStatusConciliacion" ImageUrl='<%# Bind("UbicacionIcono") %>'
                                                Width="25px" Height="25px" CssClass="icono border-color-grisOscuro centradoMedio"
                                                ToolTip='<%# Bind("StatusConciliacion") %>' />
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
            <!-- Menu Contextual -->
            <asp:LinkButton ID="lnkVer" Style="display: none" runat="server" OnClick="lnkVer_Click" />
            <asp:LinkButton ID="lnkDetalle" Style="display: none" runat="server" OnClick="lnkDetalle_Click" />
            <asp:LinkButton ID="lnkPagos" Style="display: none" runat="server" OnClick="lnkPagos_Click" />
            <asp:LinkButton ID="lnkInforme" Style="display: none" runat="server" OnClick="lnkInforme_Click" />            
            <asp:LinkButton ID="lnkConsultar" Style="display: none" runat="server" OnClick="lnkConsultar_Click"/>
            <asp:HiddenField ID="fldIndiceConcilacion" runat="server" />
            <ul id="miMenu" class="contextMenu" runat="server" >
                <li class="conciliar"><a runat="server" id="lnkVerM">Conciliar</a></li>
                <li class="detalle"><a onclick="fnDetalle();">Detalle</a></li>
                <li class="separator"></li>
                <li class="pagos"><a runat="server" id="lnkPagosM">Pagos</a></li>
                <li class="informe"><a runat="server" id="lnkInformeM">Informe</a></li> 
                <li class="editar"><a runat="server" id="lnkConsultarDoc">TransBan/Cob</a></li> 
              <%--  <li class="informe"><a runat="server" id="lnkInformeM">Informe</a></li>--%>
            </ul>
        </ContentTemplate>
    </asp:UpdatePanel>

            <%--INICIO POPUP BUSCADORPAGOESTADO DE CUENTA--%>
    <asp:HiddenField runat="server" ID="hdfBuscadorPagoEdoCta" />
    <asp:ModalPopupExtender ID="mpeBuscadorPagoEdoCta" runat="server" BackgroundCssClass="ModalBackground"
        DropShadow="false" PopupControlID="pnlBuscadorPagoEdoCta" TargetControlID="hdfBuscadorPagoEdoCta"
        BehaviorID="ModalBehaviorBuscadorPagoEdoCta" CancelControlID="btnCerrar_BuscadorPagoEdoCta">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlBuscadorPagoEdoCta" runat="server" CssClass="ModalPopup" Width="1200px" style="display: none;">
        <asp:UpdatePanel ID="upBuscadorPagoEdoCta" runat="server">
            <ContentTemplate>
                <asp:HiddenField runat="server" ID="hdfBuscadorPagoEdoCtaMostrar" Value=""/>
                <div>
                    <table style="width:100%;">
                        <tr class="bg-color-grisOscuro">
                            <td style="padding: 5px 5px 5px 5px;" class="etiqueta">
                                <div class="floatDerecha bg-color-grisClaro01">
                                    <asp:ImageButton runat="server" ID="btnCerrar_BuscadorPagoEdoCta" CssClass="iconoPequeño bg-color-rojo" 
                                        ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png" Width="20px" Height="20px" 
                                        OnClientClick="OcultarPopUpBuscadorPagoEdoCta();"/>
                                </div>
                                <div class="fg-color-blanco centradoJustificado">
                                    BUSQUEDA DE MONTO
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%--<uc1:wucBuscadorPagoEstadoCuenta runat="server" ID="wucBuscadorPagoEstadoCuenta" />--%>

                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 1%; padding-left:3px"> 
                                            <asp:CheckBox ID="chkBuscarEnEsta" Text="Buscar en esta conciliación" runat="server" Enabled="false" />
                                        </td>
                                        <td style="width: 1%;"> 
                                            <asp:Label ID="Label2" runat="server" Text="Monto" CssClass="etiqueta fg-color-negro centradoMedio"></asp:Label>
                                            <asp:TextBox ID="txtMonto" runat="server" Width="150px" CssClass="cajaTextoPequeño" onkeypress="return ValidNumDecimal(event)" ></asp:TextBox>
                                        </td>
                                        <td style="width: 1%;"> 
                                            <asp:Button ID="btnBuscaEdoCtaBuscar" runat="server" Text="Buscar" Width="150px" OnClick="btnBuscaEdoCtaBuscar_Click" />
                                        </td>
                                        <td style="width: 1%; padding-left:3px">
                                        </td>
                                        <td style="width: 1%;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 1%; padding-left:3px"> 
                                            <asp:CheckBox ID="chkBuscaEnRetiros" Text="Buscar en retiros" runat="server" />
                                        </td>
                                        <td style="width: 1%;"> 
                                            <asp:Label ID="Label1" runat="server" Text="F.Inicio" CssClass="etiqueta fg-color-negro centradoMedio"></asp:Label>
                                            <asp:TextBox ID="txtFinicio" runat="server" Width="150px" CssClass="cajaTextoPequeño"></asp:TextBox>
                                        </td>
                                        <td style="width: 1%;"> 
                                        </td>
                                        <td style="width: 1%; padding-left:3px">
                                        </td>
                                        <td style="width: 1%;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 1%; padding-left:3px"> 
                                            <asp:CheckBox ID="chkBuscarEnDepositos" Text="Buscar en depósitos" runat="server" />
                                        </td>
                                        <td style="width: 1%;"> 
                                            <asp:Label ID="Label3" runat="server" Text="F. Final" CssClass="etiqueta fg-color-negro centradoMedio"></asp:Label>
                                            <asp:TextBox ID="txtFfinal" runat="server" Width="150px" CssClass="cajaTextoPequeño"></asp:TextBox>
                                        </td>
                                        <td style="width: 1%;"> 
                                        </td>
                                        <td style="width: 1%; padding-left:3px">
                                        </td>
                                        <td style="width: 1%;">
                                        </td>
                                    </tr>
                                </table>

                                <table style="width:100%">
                                    <tr>
                                        <td class="etiqueta centradoMedio" style="width: 100%;">
                                            <div class="etiqueta centradoMedio" style="height:170px;overflow:auto;"> <!--width:800px-->
                                                <asp:GridView ID="grvPagoEstadoCuenta" runat="server" 
                                                    AutoGenerateColumns="False" 
                                                    ShowHeader="True"
                                                    ShowHeaderWhenEmpty="True"
                                                    AllowSorting="True" 
                                                    CssClass="grvResultadoConsultaCss" 
                                                    ShowFooter="False" 
                                                    Width="100%"
                                                    AllowPaging="False"
                                                    PageSize="5"
                                                    DataKeyNames="AñoConciliacion,MesConciliacion,FolioConciliacion">
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="No se ha conciliado ninguna transacción."></asp:Label>
                                                    </EmptyDataTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="AñoConciliacion" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAñoConciliacion" runat="server" Text='<%# Eval("AñoConciliacion").ToString() %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                                            <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="MesConciliacion" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMesConciliacion" runat="server" Text='<%# Eval("MesConciliacion").ToString() %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                                            <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="FolioConciliacion" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFolioConciliacion" runat="server" Text='<%# Eval("FolioConciliacion").ToString() %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                                            <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="FolioExterno" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFolioExterno" runat="server" Text='<%# Eval("FolioExterno").ToString() %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                                            <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Documento" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDocumento" runat="server" Text='<%# Eval("Documento").ToString() %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                                            <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TransBan" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTransBan" runat="server" Text='<%# Eval("TransBan").ToString() %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                                            <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="FMovTransban" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFMovTransban" runat="server" Text='<%# Eval("FMovTransban").ToString() %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                                            <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="FOperacion" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFOperacion" runat="server" Text='<%# Eval("FOperacion").ToString() %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                                            <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Retiro" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRetiro" runat="server" Text='<%# Eval("Retiro").ToString() %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                                            <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Deposito" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDeposito" runat="server" Text='<%# Eval("Deposito").ToString() %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                                            <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Concepto" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblConcepto" runat="server" Text='<%# Eval("Concepto").ToString() %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                                            <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Descripcion" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("Descripcion").ToString() %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                                            <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                                        </asp:TemplateField>
                            
                                                    </Columns>
                                                    <PagerStyle CssClass="grvPaginacionScroll" />
                                                </asp:GridView>
                                            </div>

                                            <asp:Button ID="btnBuscaPagoEdoCtaAceptar" runat="server"
                                                CssClass="boton bg-color-azulOscuro fg-color-blanco"
                                                Text="ACEPTAR" Style="margin: 0 0 0 0;" ToolTip="GUARDAR" OnClick="btnBuscaPagoEdoCtaAceptar_Click"/>
                
                                        </td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <%--FIN POPUP BUSCADORPAGOESTADO DE CUENTA--%>


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
