<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="AplicarPago.aspx.cs" Inherits="Conciliacion_Pagos_AplicarPago" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>




<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    APLICAR PAGOS
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <!--Libreria jQuery-->
    <script src="../../App_Scripts/jQueryScripts/jquery-3.2.1.min.js" type="text/javascript"></script>
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
            //gridviewScroll();
        }
        //        );

        function gridviewScroll() {
            $('#<%=grvPagos.ClientID%>').gridviewScroll({
                width: 1200,
                height: 500,
                arrowsize: 30,
                varrowtopimg: '../../App_Scripts/ScrollGridView/Images/arrowvt.png',
                varrowbottomimg: '../../App_Scripts/ScrollGridView/Images/arrowvb.png',
                harrowleftimg: '../../App_Scripts/ScrollGridView/Images/arrowhl.png',
                harrowrightimg: '../../App_Scripts/ScrollGridView/Images/arrowhr.png',
                headerrowcount: 1,
                wheelstep: 20,
                verticalbar: "auto",
                horizontalbar: "auto"

            });
        }
    </script>
    <!-- Validar: solo numeros-->
    <script type="text/javascript">
        function ValidNum(e) {
            var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
            return ((tecla > 47 && tecla < 58) || tecla == 8);
        }
        function ValidNumDecimal(e) {
            var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
            return ((tecla > 47 && tecla < 58) || tecla == 46 || tecla == 8);
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager runat="server" ID="smCantidadConcuerda" AsyncPostBackTimeout="600"
        EnableScriptGlobalization="True">
    </asp:ScriptManager>
    <script src="../../App_Scripts/jsUpdateProgress.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        var ModalProgress = '<%= mpeLoading.ClientID %>';        
    </script>
    <asp:UpdatePanel runat="server" ID="upAplicarPagos" UpdateMode="Always">
        <ContentTemplate>
            <table id="BarraEstado" class="BarraEstado bg-color-grisOscuro">
                <tr>
                    <td class="DatoConciliacion lineaVertical" rowspan="2" style="vertical-align: middle">
                        <asp:Image ID="imgStatusConciliacion" runat="server" CssClass="icono Principal" Height="35px"
                            Width="35px" />
                        <div class="FuenteDato">
                            <div class="InfoPrincipal">
                                Folio
                                <asp:Label ID="lblFolio" runat="server"></asp:Label></div>
                            <div class="Descripcion">
                                <asp:Label ID="lblStatusConciliacion" runat="server"></asp:Label></div>
                        </div>
                    </td>
                    <td class="Info Normal lineaHorizontal" style="vertical-align: top">
                        <asp:Image ID="imgBanco" runat="server" CssClass="icono Secundario" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Banco.png"
                            Width="20px" />
                        <div class="FuenteDato">
                            <div class="InfoSecundaria">
                                <asp:Label ID="lblBanco" runat="server"></asp:Label></div>
                            <div class="Descripcion">
                                Banco</div>
                        </div>
                    </td>
                    <td class="Info Normal lineaHorizontal" style="vertical-align: top">
                        <asp:Image ID="imgSucursal" runat="server" CssClass="icono Secundario" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Sucursal.png"
                            Width="20px" />
                        <div class="FuenteDato">
                            <div class="InfoSecundaria">
                                <asp:Label ID="lblSucursal" runat="server"></asp:Label></div>
                            <div class="Descripcion">
                                Sucursal</div>
                        </div>
                    </td>
                    <td class="Info Grande lineaHorizontal" style="vertical-align: top">
                        <asp:Image ID="imgTipoCon" runat="server" CssClass="icono Secundario" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/TipoConciliacion.png"
                            Width="20px" />
                        <div class="FuenteDato">
                            <div class="InfoSecundaria">
                                <asp:Label ID="lblTipoCon" runat="server"></asp:Label></div>
                            <div class="Descripcion">
                                Tipo Conciliación</div>
                        </div>
                    </td>
                    <td class="Info Estadistica lineaHorizontal" style="vertical-align: top">
                        <asp:Image ID="imgConciliadasExt" runat="server" CssClass="icono Secundario" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Enfasis.png"
                            Width="20px" />
                        <div class="FuenteDato">
                            <div class="InfoSecundaria">
                                <asp:Label ID="lblConciliadasExt" runat="server"></asp:Label></div>
                            <div class="Descripcion">
                                Conciliadas Externas</div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="Info Normal" style="vertical-align: top">
                        <asp:Image ID="imgCuentaBancaria" runat="server" CssClass="icono Secundario" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cuenta.png"
                            Width="20px" />
                        <div class="FuenteDato">
                            <div class="InfoSecundaria">
                                <asp:Label ID="lblCuenta" runat="server"></asp:Label></div>
                            <div class="Descripcion">
                                Cuenta Bancaría</div>
                        </div>
                    </td>
                    <td class="Info Normal " style="vertical-align: top">
                        <asp:Image ID="imgMesAño" runat="server" CssClass="icono Secundario" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/AñoMes.png"
                            Width="20px" />
                        <div class="FuenteDato">
                            <div class="InfoSecundaria">
                                <asp:Label ID="lblMesAño" runat="server"></asp:Label></div>
                            <div class="Descripcion">
                                Mes/Año</div>
                        </div>
                    </td>
                    <td class="Info Normal " style="vertical-align: top">
                        <asp:Image ID="imgGrupoConciliacion" runat="server" CssClass="icono Secundario" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/GrupoConciliacion.png"
                            Width="20px" />
                        <div class="FuenteDato">
                            <div class="InfoSecundaria">
                                <asp:Label ID="lblGrupoCon" runat="server"></asp:Label></div>
                            <div class="Descripcion">
                                Grupo Conciliación</div>
                        </div>
                    </td>
                    <td class="Info Estadistica " style="vertical-align: top">
                        <asp:Image ID="imgConciliadasInt" runat="server" CssClass="icono Secundario" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Enfasis.png"
                            Width="20px" />
                        <div class="FuenteDato">
                            <div class="InfoSecundaria">
                                <asp:Label ID="lblConciliadasInt" runat="server"></asp:Label></div>
                            <div class="Descripcion">
                                Conciliadas Internas</div>
                        </div>
                    </td>
                </tr>
            </table>
            <table id="BarraHerramientas" class="bg-color-grisClaro01" style="width: 100%; vertical-align: top">
                <tr>
                    <td style="padding: 3px 3px 3px 3px; vertical-align: top; width: 1%">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion bg-color-grisClaro02" rowspan="2">
                                    <asp:ImageButton ID="imgAutomatica" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Automatica.png"
                                        ToolTip="CONSULTAR FORMA AUTOMATICA" Width="25px" Enabled="False" />
                                </td>
                                <td>
                                    Conciliación Automatica
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlCriteriosConciliacion" CssClass="etiqueta dropDownPequeño"
                                        Width="150px" Style="margin-bottom: 3px; margin-right: 3px" AutoPostBack="False"
                                        Enabled="False" />
                                    </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 1%; padding: 3px 3px 3px 0px; vertical-align: top">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion bg-color-azulClaro" style="height: 30px">
                                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/ActualizarConfig.png"
                                        OnClick="btnActualizar_Click" ToolTip="ACTUALIZAR" Width="25px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="padding: 3px 3px 3px 0px; vertical-align: top; width: 1%">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion bg-color-purpura" rowspan="2">
                                    <asp:ImageButton ID="btnFiltrar" runat="server" Height="25px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Filtrar.png"
                                        ToolTip="FILTRAR" Width="25px" OnClick="btnFiltrar_Click" />
                                </td>
                                <td class="lineaVertical">
                                    Campo
                                </td>
                                <td class="lineaVertical">
                                    Operación
                                </td>
                                <td class="lineaVertical">
                                    Valor
                                </td>
                                <td>
                                    Filtrar en
                                </td>
                            </tr>
                            <tr>
                                <td id="tdDias" runat="server" class="lineaVertical">
                                    <asp:DropDownList runat="server" ID="ddlCampoFiltrar" CssClass="dropDownPequeño etiqueta"
                                        ClientIDMode="Static" ValidationGroup="Filtrar" Width="150px" Font-Size="10px"
                                        Style="margin-bottom: 3px; margin-right: 3px" OnSelectedIndexChanged="ddlCampoFiltrar_SelectedIndexChanged"
                                        AutoPostBack="True" />
                                </td>
                                <td class="lineaVertical">
                                    <asp:DropDownList runat="server" ID="ddlOperacion" CssClass="etiqueta dropDownPequeño"
                                        ValidationGroup="Filtrar" Width="120px" Style="margin-bottom: 3px; margin-right: 3px">
                                        <asp:ListItem Selected="True" Value="=">IGUAL</asp:ListItem>
                                        <asp:ListItem Value="&gt;">MAYOR</asp:ListItem>
                                        <asp:ListItem Value="&lt;">MENOR</asp:ListItem>
                                        <asp:ListItem Value="&gt;=">MAYOR O IGUAL</asp:ListItem>
                                        <asp:ListItem Value="&lt;=">MENOR O IGUAL</asp:ListItem>
                                        <asp:ListItem Value="&lt;&gt;">DIFERENTE</asp:ListItem>
                                        <asp:ListItem Value="LIKE">LIKE</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="lineaVertical" style="vertical-align: bottom">
                                    <asp:TextBox ID="txtValorCadenaFiltro" runat="server" CssClass="etiqueta cajaTextoPequeño"
                                        Width="120px" Font-Size="12px" Style="margin-bottom: 3px; margin-right: 3px">
                                    </asp:TextBox>
                                    <asp:TextBox ID="txtValorFechaFiltro" runat="server" CssClass="etiqueta cajaTextoPequeño"
                                        Width="270px" Font-Size="12px" Style="margin-bottom: 3px; margin-right: 3px">
                                    </asp:TextBox>
                                    <asp:TextBox ID="txtValorNumericoFiltro" runat="server" CssClass="etiqueta cajaTextoPequeño"
                                        onkeypress="return ValidNumDecimal(event)" Width="270px" Font-Size="12px" Style="margin-bottom: 3px;
                                        margin-right: 3px"> 
                                    </asp:TextBox><asp:CalendarExtender ID="cleValorFechaFiltro" runat="server" TargetControlID="txtValorFechaFiltro"
                                        PopupButtonID="txtValorFechaFiltro" Format="dd/MM/yyyy" PopupPosition="BottomLeft"
                                        Enabled="true">
                                    </asp:CalendarExtender>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdbFiltrarEn" runat="server" AppendDataBoundItems="True"
                                        BorderStyle="None" CssClass="etiqueta dropDownPequeño fg-color-blanco" Height="20px"
                                        Style="margin: 0 0 0 0; padding: 0 0 0 0" Width="120px" RepeatDirection="Horizontal"
                                        AutoPostBack="True" OnSelectedIndexChanged="rdbFiltrarEn_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="Externos">Externos</asp:ListItem>
                                        <asp:ListItem Value="Internos">Pedidos</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 1%; padding: 3px 3px 3px 0px; vertical-align: top">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion bg-color-naranja" style="height: 30px">
                                    <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Buscar.png"
                                        ToolTip="BUSCAR" Width="25px" OnClick="imgBuscar_Click" />
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
                                <td class="iconoOpcion bg-color-grisClaro01" style="height: 30px" id="td1"
                                    runat="server">
                                    <asp:ImageButton ID="btnAreasComunes" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Detalle.png"
                                        ToolTip="PAGO DE AREAS COMUNES" Width="25px" OnClick="btnAreasComunes_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 1%; padding: 3px 3px 3px 0px; vertical-align: top;" >
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion bg-color-azulOscuro" style="height: 30px" id="tdAplicarPagos"
                                    runat="server">
                                    <asp:ImageButton ID="btnAplicarPagos" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Pagos.png"
                                        ToolTip="APLICAR PAGO A TODOS" Width="25px" OnClick="btnAplicarPagos_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="width: 100%; vertical-align: top; padding: 5px 5px 5px 5px" class="etiqueta fg-color-blanco bg-color-verdeFuerte"
                        colspan="3">
                        PAGOS
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%" class="centradoMedio">
                        <div class="etiqueta border-color-verdeClaro fg-color-verdeClaro" style="padding: 5px 5px 5px 5px">
                            <b>Referencias Externas</b></div>
                    </td>
                    <td rowspan="3" style="width: 1%">
                    </td>
                    <td style="width: 50%;" class="centradoMedio">
                        <div class="etiqueta border-color-amarillo fg-color-amarillo" style="padding:5px">
                            <b>Referencias Internas: Pedidos </b>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top" colspan="3">
                        <table class="grvResultadoConsultaCss" cellspacing="0" rules="all" border="1" style="border-collapse:collapse;">
	                        <tbody>
		                        <tr align="center">
			                        <th align="center" scope="col" style="width:25px;">&nbsp;</th>
                                    <th align="center" scope="col" style="width:40px;"><a href="javascript:__doPostBack('ctl00$contenidoPrincipal$grvPagos','Sort$Secuencia')">Sec. Ext.</a></th>
                                    <th align="center" scope="col" style="width:70px;"><a href="javascript:__doPostBack('ctl00$contenidoPrincipal$grvPagos','Sort$Referencia')">Referencia</a></th>
                                    <th align="center" scope="col" style="width:70px;"><a href="javascript:__doPostBack('ctl00$contenidoPrincipal$grvPagos','Sort$FMovimiento')">F. Mov. Ext.</a></th>
                                    <th align="center" scope="col" style="width:70px;"><a href="javascript:__doPostBack('ctl00$contenidoPrincipal$grvPagos','Sort$FOperacion')">F. Op. Ext.</a></th>
                                    <th align="center" scope="col" style="width:100px;"><a href="javascript:__doPostBack('ctl00$contenidoPrincipal$grvPagos','Sort$Diferencia')">Diferencia</a></th>
                                    <th scope="col" style="width:200px;"><a href="javascript:__doPostBack('ctl00$contenidoPrincipal$grvPagos','Sort$Concepto')">Concepto</a></th>
                                    <th align="center" scope="col" style="width:70px;"><a href="javascript:__doPostBack('ctl00$contenidoPrincipal$grvPagos','Sort$PedidoReferencia')">Documento</a></th>
                                    <th align="center" scope="col" style="width:100px;"><a href="javascript:__doPostBack('ctl00$contenidoPrincipal$grvPagos','Sort$RemisionPedido')">Remision Ped.</a></th>
                                    <th align="center" scope="col" style="width:40px;"><a href="javascript:__doPostBack('ctl00$contenidoPrincipal$grvPagos','Sort$SeriePedido')">Serie Ped.</a></th>
                                    <th align="center" scope="col" style="width:100px;"><a href="javascript:__doPostBack('ctl00$contenidoPrincipal$grvPagos','Sort$FolioSat')">Folio Sat</a></th>
                                    <th align="center" scope="col" style="width:100px;"><a href="javascript:__doPostBack('ctl00$contenidoPrincipal$grvPagos','Sort$SerieSat')">Serie Sat</a></th>
                                    <th align="center" scope="col" style="width:120px;"><a href="javascript:__doPostBack('ctl00$contenidoPrincipal$grvPagos','Sort$ConceptoPedido')">Concepto</a></th>
                                    <th align="center" scope="col" style="width:100px;"><a href="javascript:__doPostBack('ctl00$contenidoPrincipal$grvPagos','Sort$Total')">Total Pedido</a></th>
                                    <th align="center" scope="col" style="width:100px;"><a href="javascript:__doPostBack('ctl00$contenidoPrincipal$grvPagos','Sort$Cliente')">Cliente</a></th>
                                    <th align="center" scope="col" style="width:150px;"><a href="javascript:__doPostBack('ctl00$contenidoPrincipal$grvPagos','Sort$Nombre')">Nom. Cliente</a></th>
                                    <th align="center" scope="col" style="width:150px;"><a href="javascript:__doPostBack('ctl00$contenidoPrincipal$grvPagos','Sort$TipoCobro')">T. Cobro</a></th>
		                        </tr>
	                        </tbody>
                        </table>
                        <div style="width:1200px; height: 500px; overflow:auto;">
                            <asp:GridView ID="grvPagos" runat="server" AutoGenerateColumns="False" AllowPaging="False"
                            AllowSorting="True" ShowHeader="False" CssClass="grvResultadoConsultaCss" ShowHeaderWhenEmpty="True"
                            DataKeyNames="Secuencia,FolioExt,Pedido,Celula,AñoPed" OnRowCreated="grvPagos_RowCreated"
                            OnSorting="grvPagos_Sorting" OnRowDataBound="grvPagos_RowDataBound">
                            <%--<EmptyDataTemplate>
                                <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="No existen pedidos con cantidades concordantes."></asp:Label>
                            </EmptyDataTemplate>--%>
                            <HeaderStyle HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Image ID="imgStatusMovimiento" runat="server" CssClass="icono" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Exito.png"
                                            Width="25px" Height="25px" ToolTip='<%# Eval("StatusMovimiento").ToString() %>'
                                            AlternateText='<%# Eval("StatusMovimiento").ToString() %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="25px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="25px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sec. Ext." SortExpression="Secuencia">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSecuencia" runat="server" Text='<%# resaltarBusqueda(Eval("Secuencia").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="40px" BackColor="#99b433" ForeColor="White">
                                    </ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="40px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Referencia" SortExpression="Referencia">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReferencia" runat="server" Text='<%# resaltarBusqueda(Eval("Referencia","{0:d}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="F. Mov. Ext." SortExpression="FMovimiento">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFechaMovimientoExt" runat="server" Text='<%# resaltarBusqueda(Eval("FMovimiento","{0:d}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="F. Op. Ext." SortExpression="FOperacion">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFechaOperacionExt" runat="server" Text='<%# resaltarBusqueda(Eval("FOperacion","{0:d}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Diferencia" SortExpression="Diferencia">
                                    <ItemTemplate>
                                        <b>
                                            <asp:Label ID="lblDiferencia" runat="server" Text='<%# resaltarBusqueda(Eval("Diferencia","{0:c2}").ToString()) %>'></asp:Label></b>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Concepto" SortExpression="Concepto">
                                    <ItemTemplate>
                                        <div class="parrafoTexto" style="width: 200px">
                                            <asp:Label ID="lblConceptoExt" runat="server" Text='<%# resaltarBusqueda(Eval("Concepto").ToString()) %>'></asp:Label>
                                        </div>
                                        <asp:HoverMenuExtender ID="hmeConceptoExt" runat="server" TargetControlID="lblConceptoExt"
                                            PopupControlID="pnlPopUpConceptoExt" PopDelay="20" OffsetX="-70" OffsetY="-20"
                                            EnableViewState="True">
                                        </asp:HoverMenuExtender>
                                        <asp:Panel ID="pnlPopUpConceptoExt" runat="server" CssClass="grvResultadoConsultaCss ocultar"
                                            Width="300px" Wrap="True" Style="padding: 5px 5px 5px 5px" BackColor="White">
                                            <asp:Label ID="lblToolTipConceptoExt" runat="server" Text='<%# resaltarBusqueda(Eval("Concepto").ToString()) %>'
                                                CssClass="etiqueta centradoJustificado" Font-Size="10px" />
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <HeaderStyle Width="200px"></HeaderStyle>
                                    <ItemStyle Width="200px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Documento" SortExpression="PedidoReferencia">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPedidoReferencia" runat="server" Text='<%# resaltarBusqueda(Eval("PedidoReferencia").ToString()) %>' />
                                    </ItemTemplate>
                                    <ControlStyle CssClass="centradoMedio" />
                                    <ItemStyle HorizontalAlign="Center" Width="70px" BackColor="#d9b335" ForeColor="White">
                                    </ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remision Ped." SortExpression="RemisionPedido">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemisionPedido" runat="server" Text='<%# resaltarBusqueda(Eval("RemisionPedido").ToString()) %>' />
                                    </ItemTemplate>
                                    <ControlStyle CssClass="centradoMedio" />
                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Serie Ped." SortExpression="SeriePedido">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSeriePedido" runat="server" Text='<%# resaltarBusqueda(Eval("SeriePedido").ToString()) %>' />
                                    </ItemTemplate>
                                    <ControlStyle CssClass="centradoMedio" />
                                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="40px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Folio Sat" SortExpression="FolioSat">
                                    <ItemTemplate>
                                        <b>
                                            <asp:Label ID="lblFolioSat" runat="server" Text='<%# resaltarBusqueda(Eval("FolioSat","{0:c2}").ToString()) %>'></asp:Label>
                                        </b>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Serie Sat" SortExpression="SerieSat">
                                    <ItemTemplate>
                                        <b>
                                            <asp:Label ID="lblSerieSat" runat="server" Text='<%# resaltarBusqueda(Eval("SerieSat","{0:c2}").ToString()) %>'></asp:Label>
                                        </b>
                                    </ItemTemplate>
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
                                <asp:TemplateField HeaderText="Total Pedido" SortExpression="Total">
                                    <ItemTemplate>
                                        <b>
                                            <asp:Label ID="lblMontoPedido" runat="server" Text='<%# resaltarBusqueda(Eval("Total","{0:c2}").ToString()) %>'></asp:Label>
                                        </b>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cliente" SortExpression="Cliente">
                                    <ItemTemplate>
                                        <b>
                                            <asp:Label ID="lblCliente" runat="server" Text='<%# resaltarBusqueda(Eval("Cliente").ToString()) %>'></asp:Label>
                                        </b>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Nom. Cliente" SortExpression="Nombre">
                                    <ItemTemplate>
                                        <div class="parrafoTexto" style="width: 150px">
                                            <asp:Label ID="lblNombreCliente" runat="server" Text='<%# resaltarBusqueda(Eval("Nombre").ToString()) %>'></asp:Label>
                                        </div>
                                        <asp:HoverMenuExtender ID="hmeCliente" runat="server" TargetControlID="lblNombreCliente"
                                            PopupControlID="pnlPopUpCliente" PopDelay="20" OffsetX="-40" OffsetY="-20">
                                        </asp:HoverMenuExtender>
                                        <asp:Panel ID="pnlPopUpCliente" runat="server" CssClass="grvResultadoConsultaCss ocultar"
                                            Width="170px" Wrap="True" BackColor="White" Style="padding: 5px 5px 5px 5px">
                                            <asp:Label ID="lblToolTipCliente" runat="server" Text='<%# resaltarBusqueda(Eval("Nombre").ToString()) %>'
                                                CssClass="etiqueta" Font-Size="10px" />
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Justify" Width="150px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="150px"></HeaderStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="TipoCobro" SortExpression="TipoCobro">
                                    <ItemTemplate>
                                        <b>
                                            <asp:Label ID="lblTipoCobro" runat="server" Text='<%# resaltarBusqueda(Eval("TipoCobro").ToString()) %>'></asp:Label>
                                        </b>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HiddenField runat="server" ID="hdfCerrarBuscar" />
                        <asp:ModalPopupExtender ID="mpeBuscar" runat="server" BackgroundCssClass="ModalBackground"
                            DropShadow="False" EnableViewState="false" PopupControlID="pnlBuscar" TargetControlID="hdfCerrarBuscar"
                            CancelControlID="btnCerrarBuscar">
                        </asp:ModalPopupExtender>
                        <asp:Panel ID="pnlBuscar" runat="server" CssClass="ModalPopup" Width="400px" Style="display: none">
                            <table style="width: 100%;">
                                <tr class="bg-color-grisOscuro">
                                    <td colspan="4" style="padding: 5px 5px 5px 5px" class="etiqueta">
                                        <div class="floatDerecha">
                                            <asp:ImageButton runat="server" ID="btnCerrarBuscar" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png"
                                                CssClass="iconoPequeño bg-color-rojo" />
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
                                    <td class="bg-color-grisClaro01" colspan="4">
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <%--            Panel popup inicia --%>
           
            <%--            Panel popup termina --%>


        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="panelBloqueo" runat="server" AssociatedUpdatePanelID="upAplicarPagos">
        <ProgressTemplate>
            <asp:Image ID="imgLoad" runat="server" CssClass="icono bg-color-blanco" Height="40px"
                ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/LoadPage.gif" Width="40px" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ModalPopupExtender ID="mpeLoading" runat="server" BackgroundCssClass="ModalBackground"

        PopupControlID="panelBloqueo" TargetControlID="panelBloqueo">
    </asp:ModalPopupExtender>
</asp:Content>
