﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true"
    CodeFile="VariosAUno.aspx.cs" Inherits="Conciliacion_FormasConciliar_VariosAUno"
    MaintainScrollPositionOnPostback="false" Async="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/ControlesUsuario/BuscadorClienteFactura/wucBuscaClientesFacturas.ascx" TagPrefix="uc1" TagName="wucBuscaClientesFacturas" %>
<%@ Register Src="~/ControlesUsuario/ClienteDatosBancarios/wucClienteDatosBancarios.ascx" TagPrefix="uc1" TagName="wucClienteDatosBancarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    VARIOS A UNO
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <!--Libreria jQuery-->
    <script src="../../App_Scripts/jQueryScripts/jquery-3.2.1.min.js" type="text/javascript"></script>
    <script src="../../App_Scripts/jQueryScripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../App_Scripts/jQueryScripts/jquery.ui.datepicker-es.js" type="text/javascript"></script>
    <link href="../../App_Scripts/jQueryScripts/css/custom-theme/jquery-ui-1.10.2.custom.min.css"
        rel="stylesheet" type="text/css" />
    <script src="../../App_Scripts/Common.js" type="text/javascript"></script>
    <!-- Script se utiliza para el Scroll del GridView-->
    <link href="../../App_Scripts/ScrollGridView/GridviewScroll.css" rel="stylesheet"
        type="text/css" />
    <script src="../../App_Scripts/ScrollGridView/gridviewScroll.min.js" type="text/javascript"></script>
    <!-- ScrollBar GridView -->
    <script type="text/javascript">
        function pageLoad() {
            // Script se utiliza para llamar a  la funcion de jQuery desplegable
            $("#btnMostrarAgregados").click(function () {
                $("#dvAgregados").slideToggle();
            });
            activarDatePickers();
            if (document.getElementById('ctl00_contenidoPrincipal_ddlTiposDeCobro') != null)
                document.getElementById('ctl00_contenidoPrincipal_ddlTiposDeCobro').value = document.getElementById('ctl00_contenidoPrincipal_ddlTiposDeCobro').value;
        }

        function activarDatePickers() {
            //DataPicker Rango-Fechas 
            //if (<%= tipoConciliacion %> != 2) {
                //DatePicker FOperacion
                $( "#<%= txtFOInicio.ClientID%>" ).datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 2,
                    onClose: function( selectedDate ) {
                        $( "#<%=txtFOTermino.ClientID%>" ).datepicker( "option", "minDate", selectedDate );
                    }
                });
                $( "#<%=txtFOTermino.ClientID%>" ).datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 2,
                    onClose: function( selectedDate ) {
                        $( "#<%=txtFOInicio.ClientID%>" ).datepicker( "option", "maxDate", selectedDate );
                    }
                });
                //DatePicker FMovimiento
                $( "#<%= txtFMInicio.ClientID%>" ).datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 2,
                    onClose: function( selectedDate ) {
                        $( "#<%=txtFMTermino.ClientID%>" ).datepicker( "option", "minDate", selectedDate );
                    }
                });
                $( "#<%=txtFMTermino.ClientID%>" ).datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 2,
                    onClose: function( selectedDate ) {
                        $( "#<%=txtFMInicio.ClientID%>" ).datepicker( "option", "maxDate", selectedDate );
                    }
                });
            //}
            //else{
                //DatePicker FSuministro
                $( "#<%= txtFSInicio.ClientID%>" ).datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 2,
                    onClose: function( selectedDate ) {
                        $( "#<%=txtFSTermino.ClientID%>" ).datepicker( "option", "minDate", selectedDate );
                    }
                });
                $( "#<%=txtFSTermino.ClientID%>" ).datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 2,
                    onClose: function( selectedDate ) {
                        $( "#<%=txtFSInicio.ClientID%>" ).datepicker( "option", "maxDate", selectedDate );
                    }
                });
            //}
        }
        function gridviewScroll() {
            $('#<%=grvExternos.ClientID%>').gridviewScroll({
                width: 595,
                height: 370,
                freezesize: 3,
                arrowsize: 30,
                varrowtopimg: '../../App_Scripts/ScrollGridView/Images/arrowvt.png',
                varrowbottomimg: '../../App_Scripts/ScrollGridView/Images/arrowvb.png',
                harrowleftimg: '../../App_Scripts/ScrollGridView/Images/arrowhl.png',
                harrowrightimg: '../../App_Scripts/ScrollGridView/Images/arrowhr.png',
                headerrowcount: 1,
                startVertical: $("#<%=hfExternosSV.ClientID%>").val(), 
                startHorizontal: $("#<%=hfExternosSH.ClientID%>").val(), 
                onScrollVertical: function (delta) { $("#<%=hfExternosSV.ClientID%>").val(delta); }, 
                onScrollHorizontal: function (delta) { $("#<%=hfExternosSH.ClientID%>").val(delta);}
            });
            if (<%= tipoConciliacion %> == 2) {
                $('#<%=grvPedidos.ClientID%>').gridviewScroll({
                    width: 595,
                    height: 372,
                    freezesize: 1,
                    arrowsize: 30,
                    varrowtopimg: '../../App_Scripts/ScrollGridView/Images/arrowvt.png',
                    varrowbottomimg: '../../App_Scripts/ScrollGridView/Images/arrowvb.png',
                    harrowleftimg: '../../App_Scripts/ScrollGridView/Images/arrowhl.png',
                    harrowrightimg: '../../App_Scripts/ScrollGridView/Images/arrowhr.png',
                    headerrowcount: 1,
                    startVertical: $("#<%=hfInternosSV.ClientID%>").val(), 
                    startHorizontal: $("#<%=hfInternosSH.ClientID%>").val(), 
                    onScrollVertical: function (delta) { $("#<%=hfInternosSV.ClientID%>").val(delta); }, 
                    onScrollHorizontal: function (delta) { $("#<%=hfInternosSH.ClientID%>").val(delta);}
                });
            } else {
                $('#<%=grvInternos.ClientID%>').gridviewScroll({
                    width: 595,
                    height: 300,
                    freezesize: 4,
                    arrowsize: 30,
                    varrowtopimg: '../../App_Scripts/ScrollGridView/Images/arrowvt.png',
                    varrowbottomimg: '../../App_Scripts/ScrollGridView/Images/arrowvb.png',
                    harrowleftimg: '../../App_Scripts/ScrollGridView/Images/arrowhl.png',
                    harrowrightimg: '../../App_Scripts/ScrollGridView/Images/arrowhr.png',
                    headerrowcount: 1,
                    startVertical: $("#<%=hfInternosSV.ClientID%>").val(), 
                    startHorizontal: $("#<%=hfInternosSH.ClientID%>").val(), 
                    onScrollVertical: function (delta) { $("#<%=hfInternosSV.ClientID%>").val(delta); }, 
                    onScrollHorizontal: function (delta) { $("#<%=hfInternosSH.ClientID%>").val(delta);}

                });
            }
            $('#<%=grvPedidos.ClientID%>').gridviewScroll({
                    width: 595,
                    height: 372,
                    freezesize: 0,
                    arrowsize: 30,
                    varrowtopimg: '../../App_Scripts/ScrollGridView/Images/arrowvt.png',
                    varrowbottomimg: '../../App_Scripts/ScrollGridView/Images/arrowvb.png',
                    harrowleftimg: '../../App_Scripts/ScrollGridView/Images/arrowhl.png',
                    harrowrightimg: '../../App_Scripts/ScrollGridView/Images/arrowhr.png',
                    headerrowcount: 1,
                    startVertical: $("#<%=hfInternosSV.ClientID%>").val(), 
                    startHorizontal: $("#<%=hfInternosSH.ClientID%>").val(), 
                    onScrollVertical: function (delta) { $("#<%=hfInternosSV.ClientID%>").val(delta); }, 
                    onScrollHorizontal: function (delta) { $("#<%=hfInternosSH.ClientID%>").val(delta);}
                });
        }
        function mensajeAsincrono(faltante) {
            var pre = document.createElement('pre');pre.style.maxHeight = '400px';
                pre.style.margin = '0';
                pre.style.padding = '24px';
                pre.style.whiteSpace = 'pre-wrap';
                pre.style.textAlign = 'justify';
            pre.appendChild(document.createTextNode('No fue posible encontrar información para ' + faltante + ' clientes de la solicitud ¿desea reintentar?')); alertify.confirm('Conciliaci&oacute;n bancaria',pre, function () { __doPostBack('miPostBack', "1"); }, function () { __doPostBack('miPostBack',"2");}).set({ labels: { ok: 'Si', cancel: 'No' }, padding: false });
        }
    </script>
    <!-- Validar: numeros, moneda y alfanuméricos -->
    <script type="text/javascript">
        function ValidNum(e) {
            var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
            return ((tecla > 47 && tecla < 58) || tecla == 8);
        }
        function ValidNumDecimal(e) {
            var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
            return ((tecla > 47 && tecla < 58) || tecla == 46);
        }
        function ValidAlfanumerico(e) {
            var key = document.all ? key = e.keyCode : key = e.which;

            if (/[^A-Za-z0-9 ]/.test(String.fromCharCode(key))) {
                return false;
            }
            return true;
        }
    </script>
    <!-- Controles busqueda pedidos -->
    <script type="text/javascript">
        /**
         * Cambiar la validación del TextBox txtBusquedaPedidos
         * dependiendo de la opción seleccionada en el DropDownList
         * @param valor
         */
        function ReasignarOnKeyPress(valor) {
            var textBox = document.getElementById('<%= txtBusquedaPedidos.ClientID %>');
            textBox.value = '';
            textBox.onkeypress = null;
            textBox.setAttribute("MaxLength", "20");

            if (valor == 1 || valor == 2) {
                textBox.onkeypress = ValidNum;
                if (valor == 2)
                    textBox.setAttribute("MaxLength", "18");
            }
            else if (valor == 3 || valor == 4) {
                textBox.onkeypress = ValidAlfanumerico;
                if (valor == 3)
                    textBox.setAttribute("MaxLength", "13");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager runat="server" ID="spManager" EnableScriptGlobalization="True"
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

        function OcultarPopUpClienteDatosBancarios() {
            $find("ModalBehavior").hide();
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
                        <asp:Image ID="imgStatusConciliacion" runat="server" CssClass="icono Principal" Width="35px"
                            Height="35px" />
                        <div class="FuenteDato">
                            <div class="InfoPrincipal">
                                Folio
                                <asp:Label runat="server" ID="lblFolio"></asp:Label>
                            </div>
                            <div class="Descripcion">
                                <asp:Label runat="server" ID="lblStatusConciliacion"></asp:Label>
                            </div>
                        </div>
                    </td>
                    <td class="Info Normal lineaHorizontal" style="vertical-align: top">
                        <asp:Image ID="imgBanco" runat="server" CssClass="icono Secundario" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Banco.png"
                            Width="20px" />
                        <div class="FuenteDato">
                            <div class="InfoSecundaria">
                                <asp:Label runat="server" ID="lblBanco"></asp:Label>
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
                                <asp:Label runat="server" ID="lblSucursal"></asp:Label>
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
                                <asp:Label runat="server" ID="lblTipoCon"></asp:Label>
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
                                <asp:Label runat="server" ID="lblConciliadasExt"></asp:Label>
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
                                <asp:Label runat="server" ID="lblCuenta"></asp:Label>
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
                                <asp:Label runat="server" ID="lblMesAño"></asp:Label>
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
                                <asp:Label runat="server" ID="lblGrupoCon"></asp:Label>
                            </div>
                            <div class="Descripcion">
                                Grupo Conciliación
                            </div>
                        </div>
                    </td>
                    <td class="Info Estadistica" style="vertical-align: top">
                        <asp:Image ID="imgConciliadasInt" runat="server" CssClass="icono Secundario" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Enfasis.png"
                            Width="20px" />
                        <div class="FuenteDato">
                            <div class="InfoSecundaria">
                                <asp:Label runat="server" ID="lblConciliadasInt"></asp:Label>
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
                                        ToolTip="CONSULTAR FORMA AUTOMATICA" Width="25px" OnClick="imgAutomatica_Click" 
                                        />
                                </td>
                                <td>Conciliación Automatica
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlCriteriosConciliacion" runat="server" AutoPostBack="False"
                                        CssClass="etiqueta dropDownPequeño" Style="margin-bottom: 3px; margin-right: 3px"
                                        Width="150px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="padding: 3px 3px 3px 0px; vertical-align: top; width: 1%">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion  bg-color-azulClaro" rowspan="2">
                                    <asp:ImageButton ID="btnActualizarConfig" runat="server" Height="25px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/ActualizarConfig.png"
                                        OnClick="btnActualizarConfig_Click" ToolTip="ACTUALIZAR CONFIGURACION" Width="25px"
                                        ValidationGroup="VariosUno" />
                                </td>
                                <td class="lineaVertical">Dias
                                </td>
                                <td class="lineaVertical">Diferencia
                                </td>
                                <td class="lineaVertical">Status Concepto
                                </td>
                                <td>
                                    <asp:Label ID="lblSucursalCelula" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td id="tdDias" runat="server" class="lineaVertical">
                                    <asp:TextBox ID="txtDias" runat="server" CssClass="cajaTextoPequeño" Font-Size="12px"
                                        MaxLength="2" onkeypress="return ValidNum(event)" Style="margin-bottom: 3px; margin-right: 3px"
                                        Width="30px"></asp:TextBox>
                                </td>
                                <td class="lineaVertical">
                                    <asp:TextBox ID="txtDiferencia" runat="server" CssClass="cajaTextoPequeño" Font-Size="12px"
                                        onkeypress="return ValidNumDecimal(event)" Style="margin-bottom: 3px; margin-right: 3px"
                                        Width="40px"></asp:TextBox>
                                </td>
                                <td class="lineaVertical">
                                    <asp:DropDownList ID="ddlStatusConcepto" runat="server" AppendDataBoundItems="True"
                                        CssClass="etiqueta dropDownPequeño" Style="margin-bottom: 3px; margin-right: 3px"
                                        Width="130px">
                                        <asp:ListItem Text="NINGUNO" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSucursal" runat="server" AutoPostBack="False" CssClass="etiqueta dropDownPequeño"
                                        Style="margin-bottom: 3px; margin-right: 3px" Width="115px" Visible="False">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlCelula" runat="server" AutoPostBack="False" CssClass="etiqueta dropDownPequeño"
                                        Style="margin-bottom: 3px; margin-right: 3px" Width="115px" Visible="False">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr class="bg-color-blanco centradoIzquierda">
                                <td></td>
                                <td colspan="4">
                                    <b>
                                        <asp:RangeValidator ID="rvDias" runat="server" ControlToValidate="txtDias" CssClass="etiqueta fg-color-rojo"
                                            Display="Dynamic" EnableClientScript="True" Font-Size="10px" Type="Integer" ValidationGroup="VariosUno"></asp:RangeValidator>
                                        <asp:RangeValidator ID="rvDiferencia" runat="server" ControlToValidate="txtDiferencia"
                                            CssClass="etiqueta fg-color-rojo" Display="Dynamic" EnableClientScript="True"
                                            Font-Size="10px" Type="Double" ValidationGroup="VariosUno" />
                                        <asp:RequiredFieldValidator ID="rfvDiasVacio" runat="server" ControlToValidate="txtDias"
                                            CssClass="etiqueta fg-color-rojo" Display="Dynamic" EnableClientScript="True"
                                            ErrorMessage="Especifique los dias. " Font-Size="10px" ValidationGroup="VariosUno"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvDiferenciaVacio" runat="server" ControlToValidate="txtDiferencia"
                                            CssClass="etiqueta fg-color-rojo" Display="Dynamic" EnableClientScript="True"
                                            ErrorMessage="Especifique la diferencia. " ValidationGroup="VariosUno" Font-Size="10px"></asp:RequiredFieldValidator>
                                    </b>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="padding: 3px 3px 3px 0px; vertical-align: top; width: 1%">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion bg-color-purpura" rowspan="2">
                                    <asp:ImageButton ID="imgFiltrar" runat="server" Height="25px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Filtrar.png"
                                        OnClick="imgFiltrar_Click" ToolTip="FILTRAR" Width="25px" />
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
                                        <asp:ListItem Value="Conciliados">Conciliados</asp:ListItem>
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
                                         OnClientClick="ModalPopupBuscar('M');" ToolTip="BUSCAR" Width="25px" />
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
                                <td id="tdImportar" runat="server" class="iconoOpcion bg-color-verdeFuerte" style="height: 30px">
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
                                <td class="iconoOpcion bg-color-grisClaro01" style="height: 30px">
                                    <asp:ImageButton ID="btnGuardar" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Guardar.png"
                                        ToolTip="GUARDAR" Width="25px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 1%; padding: 3px 3px 3px 0px; vertical-align: top">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion bg-color-negro" style="height: 30px">
                                    <asp:ImageButton ID="imgCerrarConciliacion" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png"
                                        OnClientClick="return confirm('¿Esta seguro de CERRAR la conciliación.?')" OnClick="imgCerrarConciliacion_Click"
                                        ToolTip="CERRAR CONCILIACION" Width="25px" />
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

    <table width="100%">
                <tr>
                    <td style="width: 100%; vertical-align: top; padding: 5px 5px 5px 5px" class="etiqueta fg-color-blanco bg-color-azulClaro">
                        Transacciones Conciliadas
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel runat="server" ID="upGrvConciliadas" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="grvConciliadas" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                    PageSize="5" Width="100%" CssClass="grvResultadoConsultaCss" ShowHeaderWhenEmpty="True"
                                    OnPageIndexChanging="grvConciliadas_PageIndexChanging" OnRowDataBound="grvConciliadas_RowDataBound"
                                    DataKeyNames="CorporativoConciliacion, SucursalConciliacion, AñoConciliacion, MesConciliacion, FolioConciliacion, FolioExterno, SecuenciaExterno"
                                    OnRowCommand="grvConciliadas_RowCommand" OnSelectedIndexChanging="grvConciliadas_SelectedIndexChanging"
                                    OnRowCreated="grvConciliadas_RowCreated" AllowSorting="True" OnSorting="grvConciliadas_Sorting">
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="No se han conciliado ninguna transacción."></asp:Label>
                                    </EmptyDataTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Folio" SortExpression="FolioExterno">
                                            <ItemTemplate>
                                                <asp:Label ID="lFolio" runat="server" Text='<%# resaltarBusqueda(Eval("FolioExterno").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle CssClass="centradoMedio" />
                                            <ItemStyle BackColor="#ebecec" ForeColor="Black" HorizontalAlign="Center" Width="50px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sec." SortExpression="SecuenciaExterno">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSecuencia" runat="server" Text='<%# resaltarBusqueda(Eval("SecuenciaExterno").ToString()) %>'></asp:Label>
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
                                            <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
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
                                                <asp:HoverMenuExtender ID="hmeDescripcionExt" runat="server" TargetControlID="lblConceptoExt"
                                                    PopupControlID="pnlPopUpConceptoExt" PopDelay="20" OffsetX="0" OffsetY="0">
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
                                       <asp:TemplateField HeaderText="TipoCobro" SortExpression="TipoCobro">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label runat="server" ID="lblTipoCobro" Text='<%# resaltarBusqueda(Eval("TipoCobro").ToString()) %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>                           
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button runat="server" ID="imgDesconciliar" CssClass="Desconciliar centradoMedio boton"
                                                    ToolTip="DESCONCILIAR" Width="20px" Height="20px" OnClientClick='<%# "return confirm(\"¿Esta seguro de DESCONCILIAR la Transacción: " + Eval("FolioExterno").ToString() + ", Secuencia: "+ Eval("SecuenciaExterno").ToString() + "? ¡Se actualizará la conciliacion y su detalle!\");" %>'
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
                        colspan="4">Transacciones Por Conciliar
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%" class="centradoMedio">
                        <table style="width: 100%" class="grids">
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
                                        CssClass="icono" Width="30px"></asp:Image>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td rowspan="3" style="width: 1%"></td>
                    <td style="width: 50%;" class="centradoMedio">
                        <table style="width: 100%;" class="grids"> <%--style=" height: 20px"--%>
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
                                        CssClass="icono" Width="30px"></asp:Image>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top">
                        <div id="configuracionExternos" class="bg-color-grisClaro">
                            <table width="100%">
                                <tr>
                                    <td class="centradoJustificado" style="width: 30%;">
                                        <asp:CheckBox ID="chkReferenciaEx" runat="server" Text="Referencia" CssClass="etiqueta fg-color-blanco centradoMedio"
                                            AutoPostBack="True" OnCheckedChanged="chkReferenciaEx_CheckedChanged" />
                                    </td>
                                    <td class="centradoDerecha" style="width: 20%;">
                                        <asp:ImageButton runat="server" ID="btnENPROCESOEXTERNO" ToolTip="EN PROCESO" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Conciliar.png"
                                            CssClass="icono bg-color-verdeClaro" OnClick="btnENPROCESOEXTERNO_Click" />
                                        <asp:ImageButton runat="server" ID="btnCANCELAREXTERNO" ToolTip="CANCELAR" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/NoConciliar.png"
                                            CssClass="icono bg-color-grisOscuro" OnClick="btnCANCELAREXTERNO_Click" />
                                    </td>
                                    <td class="centradoDerecha" style="width: 40%;">
                                        <asp:RadioButtonList ID="rdbVerDepositoRetiro" runat="server" CssClass="etiqueta fg-color-blanco"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="DEPOSITOS">DEPOSITOS</asp:ListItem>
                                            <asp:ListItem Value="RETIROS">RETIROS</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%;" class="lineaVertical bg-color-grisClaro01">
                                <tr>
                                    <td class="etiqueta lineaVertical centradoMedio" style="width: 1%; padding: 5px 5px 5px 5px">
                                        <asp:Image ID="imgMostrar" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/grid.png"
                                            Width="25px" Height="25px" CssClass="icono bg-color-blanco" />
                                    </td>
                                    <td class="etiqueta lineaVertical centradoMedio" style="width: 20%; padding: 5px 5px 5px 5px">Externos Agregados:
                                    </td>
                                    <td class="etiqueta lineaVertical centradoMedio bg-color-grisOscuro fg-color-blanco"
                                        style="width: 10%; padding: 5px 5px 5px 5px">
                                        <asp:Label runat="server" ID="lblAgregadosExternos" Text="0"></asp:Label>
                                    </td>
                                    <td class="etiqueta lineaVertical centradoMedio" style="width: 20%; padding: 5px 5px 5px 5px">Monto Acumulado:
                                    </td>
                                    <td class="etiqueta lineaVertical centradoMedio bg-color-grisOscuro fg-color-blanco"
                                        style="width: 25%; padding: 5px 5px 5px 5px">$<asp:Label runat="server" ID="lblMontoAcumuladoExterno" Text="0.00"></asp:Label>
                                    </td>
                                    <td class="etiqueta lineaVertical centradoMedio bg-color-naranja" style="width: 5%; padding: 5px 5px 5px 5px"
                                        id="tdVerINEX" runat="server">
                                        <asp:ImageButton runat="server" ID="btnVerInternos" ToolTip="VER INTERNOS" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Ir.png"
                                            Width="20px" Height="20px" OnClick="btnVerInternos_Click" ValidationGroup="VariosUno" />
                                        <asp:ImageButton runat="server" ID="btnRegresarExternos" ToolTip="REGRESAR EXTERNOS"
                                            ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Regresar.png" Width="30px"
                                            Visible="false" OnClick="btnRegresarExternos_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td class="etiqueta lineaVertical centradoMedio" style="width: 20%; padding: 5px 5px 5px 5px">Selecciona Todo:
                                        <asp:CheckBox ID="chkSeleccionaExternosTodos" runat="server" AutoPostBack="true"
                                            OnCheckedChanged="chkSeleccionaExternosTodos_CheckedChanged" 
                                            CssClass="etiqueta fg-color-blanco centradoMedio" />
                                    </td>
                                    <td class="etiqueta lineaVertical centradoMedio" style="width: 20%; padding: 5px 5px 5px 5px">Tipos de Cobro:
                                        <asp:Label ID="lblTiposdeCobro" runat="server" 
                                            CssClass="etiqueta fg-color-blanco" ></asp:Label>
                                    </td>
                                    <td class="etiqueta lineaVertical centradoMedio" style="width: 20%; padding: 5px 5px 5px 5px">
                                        <asp:DropDownList ID="ddlTiposDeCobro" runat="server" AutoPostBack="False"
                                                CssClass="etiqueta dropDownPequeño" Style="margin-bottom: 3px; margin-right: 3px"
                                                Width="150px">
                                                </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" class="centradoMedio bg-color-grisOscuro fg-color-blanco etiqueta"
                                        style="padding: 5px 5px 5px 5px">¡Las referencias externas CANCELADAS no pueden ser elegidas para CONCILIAR!
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="width:595px; height:370px; overflow:auto;">
                            <asp:GridView ID="grvExternos" runat="server" AutoGenerateColumns="False" ViewStateMode="Enabled"
                            OnRowDataBound="grvExternos_RowDataBound" ShowHeaderWhenEmpty="True" Width="100%"
                            DataKeyNames="Secuencia,Folio" AllowSorting="True" CssClass="grvResultadoConsultaCss"
                            OnSorting="grvExternos_Sorting" OnPageIndexChanging="grvExternos_PageIndexChanging"
                            PageSize="100" AllowPaging="False">
                            <%--<EmptyDataTemplate>
                                    <asp:Label ID="lblvacio" runat="server" Font-Bold="True" Font-Overline="False" ForeColor="#CC3300"
                                        Text="No se encontraron referencias externas."></asp:Label>
                                </EmptyDataTemplate>--%>
                            <HeaderStyle HorizontalAlign="Center" />
                            <RowStyle CssClass="bg-color-blanco fg-color-negro" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkExterno" OnCheckedChanged="chkExterno_CheckedChanged"
                                            AutoPostBack="True" Checked='<%# Bind("Selecciona") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" BackColor="#ebecec"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Secuencia" SortExpression="Secuencia">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSecuencia" runat="server" Text='<%# resaltarBusqueda(Eval("Secuencia").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" BackColor="#ebecec" Width="80px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" SortExpression="StatusConciliacion">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatusConciliacion" runat="server" Text='<%# Bind("StatusConciliacion") %>'
                                            Style="display: none"></asp:Label>
                                        <asp:Image runat="server" ID="imgStatusConciliacion" ImageUrl='<%# Bind("UbicacionIcono") %>'
                                            Width="27px" Height="27px" CssClass="icono border-color-grisOscuro centradoMedio"
                                            ToolTip='<%# Bind("StatusConciliacion") %>' AlternateText='<%# Bind("StatusConciliacion") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" BackColor="#ebecec" Width="35px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="35px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FolioExt" SortExpression="Folio">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFolio" runat="server" Text='<%# resaltarBusqueda(Eval("Folio").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FMovimiento" SortExpression="FMovimiento">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFMovimiento" runat="server" Text='<%# resaltarBusqueda(Eval("FMovimiento","{0:d}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FOperacion" SortExpression="FOperacion">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFOperacion" runat="server" Text='<%# resaltarBusqueda(Eval("FOperacion","{0:d}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Referencia" SortExpression="Referencia">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReferencia" runat="server" Text='<%# resaltarBusqueda(Eval("Referencia").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Retiro" SortExpression="Retiro">
                                    <ItemTemplate>
                                        <b>
                                            <asp:Label ID="lblRetiro" runat="server" Text='<%# resaltarBusqueda(Eval("Retiro","{0:c2}").ToString()) %>'></asp:Label></b>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deposito" SortExpression="Deposito">
                                    <ItemTemplate>
                                        <b>
                                            <asp:Label ID="lblDeposito" runat="server" Text='<%# resaltarBusqueda(Eval("Deposito","{0:c2}").ToString()) %>'></asp:Label></b>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Concepto" SortExpression="Concepto">
                                    <ItemTemplate>
                                        <div class="parrafoTexto" style="width:15cm">
                                            <asp:Label ID="lblConcepto" runat="server" Text='<%# resaltarBusqueda(Eval("Concepto").ToString()) %>'></asp:Label>
                                        </div>
                                        <asp:HoverMenuExtender ID="hmeConcepto" runat="server" TargetControlID="lblConcepto"
                                            PopupControlID="pnlPopUpConcepto" PopDelay="20" OffsetX="-70" OffsetY="-10">
                                        </asp:HoverMenuExtender>
                                        <asp:Panel ID="pnlPopUpConcepto" runat="server" CssClass="grvResultadoConsultaCss ocultar"
                                            Width="15cm" Style="padding: 5px 5px 5px 5px" BackColor="White" Wrap="True">
                                            <asp:Label ID="lblToolTipConcepto" runat="server" Text='<%# resaltarBusqueda(Eval("Concepto").ToString()) %>'
                                                CssClass="etiqueta " Font-Size="10px" />
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Justify" Width="150px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripcion" SortExpression="Descripcion">
                                    <ItemTemplate>
                                        <div class="parrafoTexto">
                                            <asp:Label ID="lblDescripcion" runat="server" Text='<%# resaltarBusqueda(Eval("Descripcion").ToString()) %>'></asp:Label>
                                        </div>
                                        <asp:HoverMenuExtender ID="hmeDescripcion" runat="server" TargetControlID="lblDescripcion"
                                            PopupControlID="pnlPopUpDescripcion" PopDelay="20" OffsetX="-30" OffsetY="-10">
                                        </asp:HoverMenuExtender>
                                        <asp:Panel ID="pnlPopUpDescripcion" runat="server" CssClass="grvResultadoConsultaCss ocultar"
                                            Width="150px" Style="padding: 5px 5px 5px 5px" BackColor="White" Wrap="True">
                                            <asp:Label ID="lblToolTipDescripcion" runat="server" Text='<%# resaltarBusqueda(Eval("Descripcion").ToString()) %>'
                                                CssClass="etiqueta " Font-Size="10px" />
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Justify" Width="150px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="150px"></HeaderStyle>
                                </asp:TemplateField>
                            </Columns>
                            <SelectedRowStyle BackColor="#66CCFF" ForeColor="Black" />
                            <PagerStyle CssClass="grvPaginacionScroll" />
                        </asp:GridView>
                        </div>
                        <asp:HiddenField ID="hfExternosSV" runat="server" />
                        <asp:HiddenField ID="hfExternosSH" runat="server" />
                        <asp:HiddenField ID="hfTipoCobroSeleccionado" runat="server" />
                    </td>
                    <td style="vertical-align: top" colspan="2">
                        <div id="configuracionInternosPedidos" class="bg-color-grisClaro">
                            <table width="100%">
                                <tr>
                                    <td style="width: 15%" class="centradoJustificado">
                                        <asp:CheckBox ID="chkReferenciaIn" runat="server" Text="Referencia" CssClass="etiqueta fg-color-blanco"
                                            ToolTip="COMPARAR REFERENCIA" AutoPostBack="True" OnCheckedChanged="chkReferenciaIn_CheckedChanged" />
                                    </td>
                                    <td style="width: 5%" class="etiqueta fg-color-blanco">
                                        <asp:Label runat="server" ID="lblVer" Text="Ver:" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:RadioButtonList ID="rdbTodosMenoresIn" runat="server" RepeatDirection="Horizontal"
                                            Visible="False" CssClass="etiqueta fg-color-blanco" AutoPostBack="True" OnSelectedIndexChanged="rdbTodosMenoresIn_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="MENORES">Menores</asp:ListItem>
                                            <asp:ListItem Value="TODOS">Todos</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <%--    Controles busqueda pedidos  --%>    <!-- RM 04_05_2018 -->
                                    <td class="lineaVertical" id="tdPedidosLinea" runat="server"></td>
                                    <td class="centradoDerecha">
                                        <asp:Label ID="lblBusquedaPedidos" Text="Busqueda:" CssClass="etiqueta fg-color-blanco" runat="server"
                                            style="margin-left:3px" Visible="false"/>
                                    </td>
                                    <td class="centradoDerecha">
                                        <asp:DropDownList ID="ddlBusquedaPedidos" CssClass="dropDownPequeño" Width="80px" runat="server"
                                            style="margin-left:3px;" Visible="false" onchange="ReasignarOnKeyPress(this.value)"/>
                                    </td>
                                    <td class="centradoDerecha">
                                        <asp:TextBox ID="txtBusquedaPedidos" CssClass="cajaTextoPequeño" Width="80px" runat="server"
                                            style="margin-left:2px; font-size:11px" Visible="false" onkeypress="return ValidNum(event)"
                                            MaxLength="20"/>
                                    </td>
                                    <td class="centradoDerecha">
                                        <asp:ImageButton ID="imbBusquedaPedidos" ToolTip="Buscar pedidos" CssClass="icono bg-color-verdeClaro" runat="server"
                                            ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Buscar.png" Height="25px" Width="25px"
                                            style="margin-left:2px" Visible="false"
                                            OnClick="imbBusquedaPedidos_Click" 
                                            />
                                    </td>
                                    <%--    Fin controles busqueda pedidos  --%>

                                    <td class="centradoDerecha" style="width: 10%;">
                                        <asp:ImageButton runat="server" ID="btnENPROCESOINTERNO" ToolTip="EN PROCESO" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Conciliar.png"
                                            CssClass="icono bg-color-verdeClaro" OnClick="btnENPROCESOINTERNO_Click" />
                                        <asp:ImageButton runat="server" ID="btnCANCELARINTERNO" ToolTip="CANCELAR" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/NoConciliar.png"
                                            CssClass="icono bg-color-grisOscuro" OnClick="btnCANCELARINTERNO_Click" />
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%;" class="lineaVertical bg-color-grisClaro01">
                                <tr>
                                    <td class="etiqueta lineaVertical centradoMedio" style="width: 1%; padding: 5px 5px 5px 5px">
                                        <asp:Image ID="imgMostrar01" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/grid.png"
                                            Width="25px" Height="25px" CssClass="icono bg-color-blanco" />
                                    </td>
                                    <td class="etiqueta lineaVertical centradoMedio" style="width: 25%; padding: 5px 5px 5px 5px">Monto Interno Seleccionado:
                                    </td>
                                    <td class="etiqueta lineaVertical centradoMedio bg-color-grisOscuro fg-color-blanco"
                                        style="width: 15%; padding: 5px 5px 5px 5px">
                                        <asp:Label runat="server" ID="lblMontoInterno" Text="$0.00"></asp:Label>
                                    </td>
                                    <td class="etiqueta lineaVertical centradoMedio" style="width: 1%;">
                                        <asp:Button runat="server" ID="btnGuardarVariosAUno" CssClass="boton bg-color-azulOscuro fg-color-blanco"
                                            Text="GUARDAR" Style="margin: 0 0 0 0;" ToolTip="GUARDAR" OnClick="btnGuardarVariosAUno_Click" />
                                    </td>
                                </tr>
                            </table>

                            <div class="lineaHorizontal">
                            </div>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 50%;">
                                        <uc1:wucBuscaClientesFacturas runat="server" ID="wucBuscaClientesFacturas" />
                                    </td>
                                    <td style="width: 50%;">
                                        <asp:ImageButton ID="btnFiltraCliente" runat="server" CssClass="icono bg-color-verdeClaro"
                                            Height="25px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Buscar.png"
                                            ToolTip="FILTRAR Cliente" Width="25px"
                                            OnClick="btnFiltraCliente_Click" />
                                    </td>
                                </tr>
                            </table>	

                            <div class="lineaHorizontal">
                            </div>
                            <table style="width: 100%">
                                <tr>
                                    <td rowspan="2" style="vertical-align: top; width: 12.5%;">
                                        <asp:Label ID="lblFOperacion" runat="server" CssClass="etiqueta fg-color-blanco centradoMedio"
                                            Text="FOperación"></asp:Label>
                                        <asp:Label ID="lblFSuminstro" runat="server" CssClass="etiqueta fg-color-blanco centradoMedio"
                                            Text="FSuminstro"></asp:Label>
                                    </td>
                                    <td style="width: 12.5%;">
                                        <asp:TextBox ID="txtFOInicio" runat="server" CssClass="cajaTextoPequeño" ToolTip="FOper Inicio"
                                            ValidationGroup="vgFOperacion" Width="80px"></asp:TextBox>
                                        <asp:TextBox ID="txtFSInicio" runat="server" CssClass="cajaTextoPequeño" ToolTip="FSum Inicio"
                                            ValidationGroup="vgFSuministro" Width="80px"></asp:TextBox>
                                    </td>
                                    <td style="width: 12.5%;">
                                        <asp:TextBox ID="txtFOTermino" runat="server" CssClass="cajaTextoPequeño" ToolTip="FOper Fin"
                                            Width="80px"></asp:TextBox>
                                        <asp:TextBox ID="txtFSTermino" runat="server" CssClass="cajaTextoPequeño" ToolTip="FSum Fin"
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td rowspan="2" style="vertical-align: top; width: 12.5%;">
                                        <asp:ImageButton ID="btnRangoFechasFO" runat="server" CssClass="icono bg-color-azulClaro"
                                            Height="25px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Filtrar.png"
                                            OnClick="btnRangoFechasFO_Click" ToolTip="FILTRAR FOperacion" ValidationGroup="vgFOperacion"
                                            Width="25px" />
                                        <asp:ImageButton ID="btnRangoFechasFS" runat="server" CssClass="icono bg-color-azulClaro"
                                            Height="25px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Filtrar.png"
                                            OnClick="btnRangoFechasFS_Click" ToolTip="FILTRAR FSuminstro" ValidationGroup="vgFSuministro"
                                            Width="25px" />
                                    </td>
                                    <td class="lineaVertical" rowspan="2"></td>
                                    <td rowspan="2" style="vertical-align: top; width: 12.5%;">
                                        <asp:Label ID="lblFMovimiento" runat="server" CssClass="etiqueta fg-color-blanco centradoMedio"
                                            Text="FMovimiento"></asp:Label>
                                    </td>
                                    <td style="width: 12.5%;">
                                        <asp:TextBox ID="txtFMInicio" runat="server" CssClass="cajaTextoPequeño" ToolTip="FMov Inicio"
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td style="width: 12.5%;">
                                        <asp:TextBox ID="txtFMTermino" runat="server" CssClass="cajaTextoPequeño" ToolTip="FMov Fin"
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td rowspan="2" style="vertical-align: top; width: 12.5%;">
                                        <asp:ImageButton ID="btnRangoFechasFM" runat="server" CssClass="icono bg-color-azulClaro"
                                            Height="25px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Filtrar.png"
                                            OnClick="btnRangoFechasFM_Click" ToolTip="FILTRAR FMovimiento" ValidationGroup="vgFMovimiento"
                                            Width="25px" />
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="2">
                                        <asp:RangeValidator ID="rvFOInicio" runat="server" ControlToValidate="txtFOInicio"
                                            CssClass="etiqueta fg-color-amarillo" Display="Dynamic" ErrorMessage="Porfavor insertar una fecha valida"
                                            MaximumValue="28/12/9999" MinimumValue="28/12/1000" Type="Date" ValidationGroup="vgFOperacion"
                                            Font-Size="10px"></asp:RangeValidator>
                                        <asp:RangeValidator ID="rvFOTermino" runat="server" ControlToValidate="txtFOTermino"
                                            CssClass="etiqueta fg-color-amarillo" Display="Dynamic" ErrorMessage="Porfavor insertar una fecha valida"
                                            MaximumValue="28/12/9999" MinimumValue="28/12/1000" Type="Date" ValidationGroup="vgFOperacion"
                                            Font-Size="10px"></asp:RangeValidator>
                                        <asp:RangeValidator ID="rvFSInicio" runat="server" ControlToValidate="txtFSInicio"
                                            CssClass="etiqueta fg-color-amarillo" Display="Dynamic" ErrorMessage="Porfavor insertar una fecha valida"
                                            MaximumValue="28/12/9999" MinimumValue="28/12/1000" Type="Date" ValidationGroup="vgFSuministro"
                                            Font-Size="10px"></asp:RangeValidator>
                                        <asp:RangeValidator ID="rvFSTermino" runat="server" ControlToValidate="txtFSTermino"
                                            CssClass="etiqueta fg-color-amarillo" Display="Dynamic" ErrorMessage="Porfavor insertar una fecha valida"
                                            MaximumValue="28/12/9999" MinimumValue="28/12/1000" Type="Date" ValidationGroup="vgFSuministro"
                                            Font-Size="10px"></asp:RangeValidator>
                                    </td>
                                    <td colspan="2">
                                        <asp:RangeValidator ID="rvFMInicio" runat="server" ControlToValidate="txtFMInicio"
                                            CssClass="etiqueta fg-color-amarillo" Display="Dynamic" ErrorMessage="Porfavor insertar una fecha valida"
                                            MaximumValue="28/12/9999" MinimumValue="28/12/1000" Type="Date" ValidationGroup="vgFMovimiento"
                                            Font-Size="10px"></asp:RangeValidator>
                                        <asp:RangeValidator ID="rvFMTermino" runat="server" ControlToValidate="txtFMTermino"
                                            CssClass="etiqueta fg-color-amarillo" Display="Dynamic" ErrorMessage="Porfavor insertar una fecha valida"
                                            MaximumValue="28/12/9999" MinimumValue="28/12/1000" Type="Date" ValidationGroup="vgFMovimiento"
                                            Font-Size="10px"></asp:RangeValidator>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="dvExpera" runat="server" class="Espera">
                            <asp:Image ID="imgEspera" runat="server" CssClass="icono imagenCentrada" Height="40px"
                                ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/LoadPage.gif" Width="40px"
                                Style="margin-top: 135px" />
                            <br />
                            <br />
                            <asp:Label ID="lblEspera" Text="...ESPERANDO SELECCIÓN DE EXTERNOS..." runat="server"
                                CssClass="etiqueta"></asp:Label>
                        </div>
                        <div style="width:595px; height:343px; overflow:auto;">
                            <asp:GridView ID="grvInternos" runat="server" AutoGenerateColumns="False" ShowHeader="True"
                            AllowPaging="False" PageSize="100" CssClass="grvResultadoConsultaCss" AllowSorting="True"
                            OnRowDataBound="grvInternos_RowDataBound" ShowHeaderWhenEmpty="True" ShowFooter="False"
                            Width="100%" DataKeyNames="Secuencia, Folio, Sucursal" OnRowCreated="grvInternos_RowCreated"
                            OnSorting="grvInternos_Sorting" OnDataBound="grvInternos_DataBound" OnPageIndexChanging="grvInternos_PageIndexChanging">
                            <HeaderStyle HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkInterno" />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:CheckBox runat="server" ID="chkTodosInternos" AutoPostBack="True" OnCheckedChanged="OnCheckedChangedInternos" />
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" BackColor="#ebecec"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Secuencia" SortExpression="Secuencia">
                                    <ItemTemplate>
                                        <asp:RadioButton ID="rdbSecuenciaIn" runat="server" GroupName="GrupoArchivosIn" AutoPostBack="True"
                                            Text='<%# resaltarBusqueda(Eval("Secuencia").ToString()) %>' OnCheckedChanged="rdbSecuenciaIn_CheckedChanged" />
                                        <%--<asp:Label ID="lblSecuenciaIn" runat="server" Text='<%# resaltarBusqueda(Eval("Secuencia").ToString()) %>' />--%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Wrap="True" BackColor="#ebecec"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" SortExpression="StatusConciliacion">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatusConciliacion" runat="server" Text='<%# Bind("StatusConciliacion") %>'
                                            Style="display: none"></asp:Label>
                                        <asp:Image runat="server" ID="imgStatusConciliacion" ImageUrl='<%# Bind("UbicacionIcono") %>'
                                            Width="27px" Height="27px" CssClass="icono border-color-grisOscuro centradoMedio"
                                            ToolTip='<%# Bind("StatusConciliacion") %>' AlternateText='<%# Bind("StatusConciliacion") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" BackColor="#ebecec"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Folio" SortExpression="Folio">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFolioIn" runat="server" Text='<%# resaltarBusqueda(Eval("Folio").ToString()) %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" BackColor="#ebecec"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FMovimiento" SortExpression="FMovimiento">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFMovimiento" runat="server" Text='<%# resaltarBusqueda(Eval("FMovimiento","{0:d}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FOperacion" SortExpression="FOperacion">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFOperacion" runat="server" Text='<%# resaltarBusqueda(Eval("FOperacion","{0:d}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Referencia" SortExpression="Referencia">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReferencia" runat="server" Text='<%# resaltarBusqueda(Eval("Referencia").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Retiro" SortExpression="Retiro">
                                    <ItemTemplate>
                                        <b>
                                            <asp:Label ID="lblRetiro" runat="server" Text='<%# resaltarBusqueda(Eval("Retiro","{0:c2}").ToString()) %>'></asp:Label></b>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deposito" SortExpression="Deposito">
                                    <ItemTemplate>
                                        <b>
                                            <asp:Label ID="lblDeposito" runat="server" Text='<%# resaltarBusqueda(Eval("Deposito","{0:c2}").ToString()) %>'></asp:Label></b>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Concepto" SortExpression="Concepto">
                                    <ItemTemplate>
                                        <div class="parrafoTexto">
                                            <asp:Label ID="lblConcepto" runat="server" Text='<%# resaltarBusqueda(Eval("Concepto").ToString()) %>'></asp:Label>
                                        </div>
                                        <asp:HoverMenuExtender ID="hmeConcepto" runat="server" TargetControlID="lblConcepto"
                                            PopupControlID="pnlPopUpConcepto" PopDelay="20" OffsetX="-30" OffsetY="0">
                                        </asp:HoverMenuExtender>
                                        <asp:Panel ID="pnlPopUpConcepto" runat="server" CssClass="grvResultadoConsultaCss ocultar"
                                            Width="150px" Style="padding: 5px 5px 5px 5px" BackColor="White" Wrap="True">
                                            <asp:Label ID="lblToolTipConcepto" runat="server" Text='<%# resaltarBusqueda(Eval("Concepto").ToString()) %>'
                                                CssClass="etiqueta " Font-Size="10px" />
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripcion" SortExpression="Descripcion">
                                    <ItemTemplate>
                                        <div class="parrafoTexto">
                                            <asp:Label ID="lblDescripcion" runat="server" Text='<%# resaltarBusqueda(Eval("Descripcion").ToString()) %>'></asp:Label>
                                        </div>
                                        <asp:HoverMenuExtender ID="hmeDescripcion" runat="server" TargetControlID="lblDescripcion"
                                            PopupControlID="pnlPopUpDescripcion" PopDelay="20" OffsetX="-30" OffsetY="0">
                                        </asp:HoverMenuExtender>
                                        <asp:Panel ID="pnlPopUpDescripcion" runat="server" CssClass="grvResultadoConsultaCss ocultar"
                                            Width="150px" Style="padding: 5px 5px 5px 5px" BackColor="White" Wrap="True">
                                            <asp:Label ID="lblToolTipDescripcion" runat="server" Text='<%# resaltarBusqueda(Eval("Descripcion").ToString()) %>'
                                                CssClass="etiqueta " Font-Size="10px" />
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <div class="parrafoTexto">
                                                <asp:Label ID="lblCliente" runat="server" Text='<%# resaltarBusqueda(Eval("Cliente").ToString()) %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="grvPaginacionScroll" />
                            </asp:GridView>
                        <%--</div>--%>
                        <asp:GridView ID="grvPedidos" runat="server" AutoGenerateColumns="False" ShowHeader="True"
                            CssClass="grvResultadoConsultaCss" AllowSorting="True" ShowFooter="False" Width="100%"
                            ShowHeaderWhenEmpty="True" DataKeyNames="Celula,Pedido,AñoPed,Cliente" OnSorting="grvPedidos_Sorting"
                            AllowPaging="True" PageSize="200" OnPageIndexChanging="grvPedidos_PageIndexChanging"
                            OnRowDataBound="grvPedidos_RowDataBound" OnDataBound="grvPedidos_DataBound">
                            <%-- <EmptyDataTemplate>
                                <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="No se encontraron información sobre pedidos."></asp:Label>
                            </EmptyDataTemplate>--%>
                            <HeaderStyle HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField HeaderText="Pedido" SortExpression="Pedido">
                                    <ItemTemplate>
                                        <asp:RadioButton ID="rdbPedido" runat="server" GroupName="GrupoPedidos" AutoPostBack="True"
                                            Text='<%# resaltarBusqueda(Eval("Pedido").ToString()) %>' OnCheckedChanged="rdbPedido_CheckedChanged" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="70px" BackColor="#ebecec"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FSuministro" SortExpression="FSuministro">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFSuministro" runat="server" Text='<%# resaltarBusqueda(Eval("FSuministro","{0:d}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="centradoMedio" />
                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Factura" SortExpression="SerieFactura">
                                    <ItemTemplate>
                                            <asp:Label ID="lblFacturaPED" runat="server" Text='<%# Eval("FolioFactura") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Monto" SortExpression="Total">
                                    <ItemTemplate>
                                        <b>
                                            <asp:Label ID="lblMontoPedido" runat="server" Text='<%# resaltarBusqueda(Eval("Total","{0:c2}").ToString()) %>'></asp:Label></b>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label runat="server" ID="lblTotalMontoPedido"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                                    <FooterStyle HorizontalAlign="Center" Width="120px" CssClass="fg-color-blanco bg-color-grisOscuro"></FooterStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nom. Cliente" SortExpression="Nombre">
                                    <ItemTemplate>
                                        <div class="parrafoTexto">
                                            <asp:Label ID="lblCliente" runat="server" Text='<%# resaltarBusqueda(Eval("Nombre").ToString()) %>'>
                                            </asp:Label>
                                        </div>
                                        <asp:HoverMenuExtender ID="hmeCliente" runat="server" TargetControlID="lblCliente"
                                            PopupControlID="pnlPopUpCliente" PopDelay="20" OffsetX="-30" OffsetY="-10">
                                        </asp:HoverMenuExtender>
                                        <asp:Panel ID="pnlPopUpCliente" runat="server" CssClass="grvResultadoConsultaCss ocultar"
                                            Width="250px" Style="padding: 5px 5px 5px 5px" BackColor="White" Wrap="True">
                                            <asp:Label ID="lblToolTipCliente" runat="server" Text='<%# resaltarBusqueda(Eval("Nombre").ToString()) %>'
                                                CssClass="etiqueta " Font-Size="10px" />
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Justify" Width="150px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="150px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Concepto" SortExpression="Concepto">
                                    <ItemTemplate>
                                        <div class="parrafoTexto">
                                            <asp:Label ID="lblConcepto" runat="server" Text='<%# resaltarBusqueda(Eval("Concepto").ToString()) %>'></asp:Label>
                                        </div>
                                        <asp:HoverMenuExtender ID="hmeConcepto" runat="server" TargetControlID="lblConcepto"
                                            PopupControlID="pnlPopUpConcepto" PopDelay="20" OffsetX="-30" OffsetY="-10">
                                        </asp:HoverMenuExtender>
                                        <asp:Panel ID="pnlPopUpConcepto" runat="server" CssClass="grvResultadoConsultaCss ocultar"
                                            Width="120px" Style="padding: 5px 5px 5px 5px" BackColor="White" Wrap="True">
                                            <asp:Label ID="lblToolTipConcepto" runat="server" Text='<%# resaltarBusqueda(Eval("Concepto").ToString()) %>'
                                                CssClass="etiqueta " Font-Size="10px" />
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="150px"></HeaderStyle>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="grvPaginacionScroll" />
                        </asp:GridView>
                        <asp:HiddenField ID="hfInternosSV" runat="server" />
                        <asp:HiddenField ID="hfInternosSH" runat="server" />
                        </div>
                    </div>
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
                                    CssClass="iconoPequeño bg-color-rojo" OnClientClick="ModalPopupBuscar('O');"  />
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
                                Width="98%">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5%" class="centradoDerecha">
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
            <asp:HiddenField runat="server" ID="hdfCerrarDetalle" />
            <asp:ModalPopupExtender ID="mpeLanzarDetalle" runat="server" BackgroundCssClass="ModalBackground"
                DropShadow="False" EnableViewState="false" PopupControlID="pnlDetalle" TargetControlID="hdfCerrarDetalle"
                CancelControlID="btnCerrarDetalle">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlDetalle" runat="server" CssClass="ModalPopup" EnableViewState="false"
                Width="900px" Style="display: none">
                <table style="width: 100%">
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
                        <td style="padding: 5px 5px 5px 5px; width: 100%">
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
                                        <ItemStyle HorizontalAlign="Center" BackColor="#d9b335" ForeColor="White" Width="50px">
                                        </ItemStyle>
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
                                    <%--                                    <asp:TemplateField HeaderText="Concepto" SortExpression="ConceptoPedido">
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
                                    </asp:TemplateField>--%>
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
                                        <ItemStyle HorizontalAlign="Justify" Width="550px"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Width="550px"></HeaderStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" class="bg-color-grisClaro01">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:HiddenField runat="server" ID="hdfStatusTransaccion" />
            <asp:ModalPopupExtender ID="mpeStatusTransaccion" runat="server" BackgroundCssClass="ModalBackground"
                DropShadow="False" EnableViewState="false" PopupControlID="pnlStatusTransaccion"
                TargetControlID="hdfStatusTransaccion">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlStatusTransaccion" runat="server" CssClass="ModalPopup" Width="350px"
                Style="display: none">
                <table style="width: 100%;">
                    <tr class="bg-color-grisOscuro">
                        <td colspan="5" style="padding: 5px 5px 5px 5px" class="etiqueta">
                            <div class="floatDerecha bg-color-grisClaro01">
                                <asp:ImageButton runat="server" ID="btnCerrarCambiar" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png"
                                    CssClass="iconoPequeño bg-color-rojo" />
                            </div>
                            <div class="fg-color-blanco centradoJustificado">
                                CAMBIAR STATUS TRANSACCIÓN</div>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 5px 5px 5px 5px; width: 90%; vertical-align: top">
                            <div class="etiqueta">
                                Motivo
                            </div>
                            <asp:DropDownList runat="server" ID="ddlMotivosNoConciliado" CssClass="dropDown"
                                Width="100%" />
                            <div class="etiqueta">
                                Comentario
                            </div>
                            <asp:TextBox runat="server" ID="txtComentario" CssClass="cajaTexto" TextMode="MultiLine"
                                Rows="4" Width="97%" Style="resize: none"></asp:TextBox>
                            <div class="etiqueta etiqueta">
                                <asp:RegularExpressionValidator ID="revtxtComentario" runat="server" CssClass="fg-color-rojo"
                                    ErrorMessage="Debe ingresar hasta un maximo de 250 caracteres" ValidationExpression="^([\S\s]{0,250})$"
                                    ControlToValidate="txtComentario" Display="Dynamic" ValidationGroup="CancelarPendiente"></asp:RegularExpressionValidator>
                            </div>
                            <div id="dvMensajeExterno" runat="server">
                                <table style="width: 100%" class="border-color-amarillo">
                                    <tr>
                                        <td style="width: 1%; padding: 5px 5px 5px 5px">
                                            <asp:Image ID="Image1" runat="server" CssClass="iconoPequeño bg-color-amarillo" Width="20px"
                                                Height="20px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Advertencia.png" />
                                        </td>
                                        <td class="etiqueta fg-color-grisoscuro" style="vertical-align: top; padding: 5px 5px 5px 5px">
                                            ¡Si cancela esta(s) Referencia(s) Externa(s) se quitaran todos los referencias internas
                                            agregadas a este(os) Folio(s) Externo(s)!
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="centradoDerecha" colspan="2">
                            <asp:Button runat="server" ID="btnAceptarStatusExterno" Text="ACEPTAR" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                OnClick="btnAceptarStatusExterno_Click" ValidationGroup="CancelarPendiente" />
                            <asp:Button runat="server" ID="btnAceptarStatusInterno" Text="ACEPTAR" CssClass="boton fg-color-blanco bg-color-azulOscuro"
                                OnClick="btnAceptarStatusInterno_Click" ValidationGroup="CancelarPendiente" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="bg-color-grisClaro01">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:HiddenField runat="server" ID="hdfCerrar" />
            <asp:ModalPopupExtender ID="mpeFiltrar" runat="server" BackgroundCssClass="ModalBackground"
                DropShadow="False" EnableViewState="false" PopupControlID="pnlFiltrar" TargetControlID="hdfCerrar"
                CancelControlID="btnCerrar">
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
                            <%-- OnSelectedIndexChanged="ddlCampoFiltrar_SelectedIndexChanged"--%>
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
                        <td class="bg-color-grisClaro01" colspan="4">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
             <%--No puede ser manejado desde JavaScript--%>
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
                                    CssClass="iconoPequeño bg-color-rojo" OnClientClick="HideModalPopup();" /><%-- OnClick="imgCerrarImportar_Click"--%>
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
                                Width="100%" CssClass="dropDown" OnSelectedIndexChanged="ddlTipoFuenteInfoInterno_SelectedIndexChanged">
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
                                            Usuario Alta</div>
                                        <asp:TextBox ID="lblUsuarioAltaEx" runat="server" Width="95%" CssClass="cajaTexto"
                                            Enabled="False"></asp:TextBox>
                                    </td>
                                    <td style="width: 1%">
                                    </td>
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
                        <td class="bg-color-grisClaro01" colspan="2">
                        </td>
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
    
    <!--        INICIO DE POPUP CLIENTE DATOS BANCARIOS     -->
    <asp:HiddenField runat="server" ID="hdfCerrarSeleccionCliente" />
    <asp:ModalPopupExtender ID="mpeLanzarSeleccionCliente" runat="server" BackgroundCssClass="ModalBackground"
        DropShadow="False" PopupControlID="pnlSeleccionCliente" TargetControlID="hdfCerrarSeleccionCliente"
        BehaviorID="ModalBehavior" CancelControlID="btnCerrarSeleccionCliente">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlSeleccionCliente" runat="server" CssClass="ModalPopup" Width="700px" style="display:none;">
    <asp:UpdatePanel ID="upClienteDatosBancarios" runat="server">
        <ContentTemplate>
                <table style="width: 100%;">
                    <tr class="bg-color-grisOscuro">
                        <td colspan="5" style="padding: 5px 5px 5px 5px" class="etiqueta">
                            <div class="floatDerecha bg-color-grisClaro01">
                                <asp:ImageButton runat="server" ID="btnCerrarSeleccionCliente" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png"
                                    CssClass="iconoPequeño bg-color-rojo"
                                    OnClientClick="OcultarPopUpClienteDatosBancarios();" />
                            </div>
                            <div class="fg-color-blanco centradoJustificado">
                                SELECCIONAR CLIENTE
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="etiqueta centradoMedio" style="width: 100%;">
                            <div class="etiqueta centradoMedio" style="height:170px;overflow:auto;">
                                <uc1:wucClienteDatosBancarios runat="server" ID="wucClienteDatosBancarios" />
                            </div>
                            <asp:Button ID="btnAceptarClienteDatosBancarios" runat="server" OnClick="btnAceptarClienteDatosBancarios_Click"
                                CssClass="boton bg-color-azulOscuro fg-color-blanco" 
                                Text="ACEPTAR" Style="margin: 0 0 0 0;" ToolTip="ACEPTAR" />
                            <asp:Button ID="btnCancelarClienteDatosBancario" runat="server"
                                CssClass="boton bg-color-azulOscuro fg-color-blanco" 
                                Text="CANCELAR" Style="margin: 0 0 0 0;" ToolTip="CANCELAR" OnClick="btnCancelarClienteDatosBancario_Click" />
                         </td>
                    </tr>
                </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Panel>

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
