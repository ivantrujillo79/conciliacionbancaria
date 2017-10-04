<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true"
    CodeFile="DetalleConciliacion.aspx.cs" Inherits="Conciliacion_DetalleConciliacion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    DETALLE CONCILIACIÓN
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <!--Libreria jQuery-->
    <script src="../App_Scripts/jQueryScripts/jquery.min.js" type="text/javascript"></script>
    <script src="../App_Scripts/jQueryScripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../App_Scripts/jQueryScripts/css/custom-theme/jquery-ui-1.10.2.custom.min.css"
        rel="stylesheet" type="text/css" />
    <script src="../App_Scripts/Common.js" type="text/javascript"></script>
    <script src="../App_Scripts/jQueryScripts/jquery.ui.datepicker-es.js" type="text/javascript"></script>
    <!-- Script se utiliza para el Scroll de l GridView-->
    <script src="../App_Scripts/ScrollGridView/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../App_Scripts/ScrollGridView/GridviewScroll.css" rel="stylesheet" type="text/css" />
    <!-- ScrollBar GridView -->
    <script type="text/javascript">
        function pageLoad() 
        {
            gridviewScroll();
            activarDatePickers();
        }

   function activarDatePickers() {
    //DataPicker Rango-Fechas 
               if (<%= tipoConciliacion %> != 2) {
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
               }
               else{
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
               }
   }

        function gridviewScroll() {
            $('#<%=grvConciliadas.ClientID%>').gridviewScroll({
                width: 1200,
                height: 200,
                freezesize: 4,
                arrowsize: 30,
                varrowtopimg: '../App_Scripts/ScrollGridView/Images/arrowvt.png',
                varrowbottomimg: '../App_Scripts/ScrollGridView/Images/arrowvb.png',
                harrowleftimg: '../App_Scripts/ScrollGridView/Images/arrowhl.png',
                harrowrightimg: '../App_Scripts/ScrollGridView/Images/arrowhr.png',
                headerrowcount: 1
            });

            $('#<%=grvExternos.ClientID%>').gridviewScroll({
                width: 595,
                height: 340,
                freezesize: 3,
                arrowsize: 30,
                varrowtopimg: '../App_Scripts/ScrollGridView/Images/arrowvt.png',
                varrowbottomimg: '../App_Scripts/ScrollGridView/Images/arrowvb.png',
                harrowleftimg: '../App_Scripts/ScrollGridView/Images/arrowhl.png',
                harrowrightimg: '../App_Scripts/ScrollGridView/Images/arrowhr.png',
                headerrowcount: 1,
                startVertical: $("#<%=hfExternosSV.ClientID%>").val(), 
                startHorizontal: $("#<%=hfExternosSH.ClientID%>").val(), 
                onScrollVertical: function (delta) { $("#<%=hfExternosSV.ClientID%>").val(delta); }, 
                onScrollHorizontal: function (delta) { $("#<%=hfExternosSH.ClientID%>").val(delta);}
            });
            
            if (<%= tipoConciliacion %> == 2) {
            $('#<%=grvPedidos.ClientID%>').gridviewScroll({
                width: 595,
                height: 340,
                freezesize: 1,
                arrowsize: 30,
                varrowtopimg: '../App_Scripts/ScrollGridView/Images/arrowvt.png',
                varrowbottomimg: '../App_Scripts/ScrollGridView/Images/arrowvb.png',
                harrowleftimg: '../App_Scripts/ScrollGridView/Images/arrowhl.png',
                harrowrightimg: '../App_Scripts/ScrollGridView/Images/arrowhr.png',
                headerrowcount: 1,
                startVertical: $("#<%=hfInternosSV.ClientID%>").val(), 
                startHorizontal: $("#<%=hfInternosSH.ClientID%>").val(), 
                onScrollVertical: function (delta) { $("#<%=hfInternosSV.ClientID%>").val(delta); }, 
                onScrollHorizontal: function (delta) { $("#<%=hfInternosSH.ClientID%>").val(delta);}
            });

            } else {
             
              $('#<%=grvInternos.ClientID%>').gridviewScroll({
                width: 595,
                height: 340,
                freezesize: 3,
                arrowsize: 30,
                varrowtopimg: '../App_Scripts/ScrollGridView/Images/arrowvt.png',
                varrowbottomimg: '../App_Scripts/ScrollGridView/Images/arrowvb.png',
                harrowleftimg: '../App_Scripts/ScrollGridView/Images/arrowhl.png',
                harrowrightimg: '../App_Scripts/ScrollGridView/Images/arrowhr.png',
                headerrowcount: 1,
                startVertical: $("#<%=hfInternosSV.ClientID%>").val(), 
                startHorizontal: $("#<%=hfInternosSH.ClientID%>").val(), 
                onScrollVertical: function (delta) { $("#<%=hfInternosSV.ClientID%>").val(delta); }, 
                onScrollHorizontal: function (delta) { $("#<%=hfInternosSH.ClientID%>").val(delta);}
            });
            }
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
    <asp:ScriptManager runat="server" ID="smDetalleConciliacion" AsyncPostBackTimeout="600"
        EnableScriptGlobalization="True">
    </asp:ScriptManager>
    <script src="../App_Scripts/jsUpdateProgress.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
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
    <asp:UpdatePanel runat="server" ID="upDetalleConciliacion">
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
                                Cuenta Bancaria</div>
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
                                <td class="iconoOpcion bg-color-verdeClaro" rowspan="2">
                                    <asp:ImageButton ID="imgAutomatica" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Automatica.png"
                                        ToolTip="CONSULTAR FORMA AUTOMATICA" Width="25px" OnClick="imgAutomatica_Click" />
                                </td>
                                <td>
                                    Conciliación Automatica
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlCriteriosConciliacion" CssClass="etiqueta dropDownPequeño"
                                        Width="150px" Style="margin-bottom: 3px; margin-right: 3px" AppendDataBoundItems="True">
                                        <asp:ListItem Value="0" Selected="True">TODAS</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="padding: 3px 3px 3px 0px; vertical-align: top; width: 10%">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion  bg-color-azulClaro" rowspan="2">
                                    <asp:ImageButton ID="btnActualizarConfig" runat="server" Height="25px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/ActualizarConfig.png"
                                        ToolTip="ACTUALIZAR CONFIGURACION" Width="25px" OnClick="btnActualizarConfig_Click"
                                        ValidationGroup="Configuracion" />
                                </td>
                                <td class="lineaVertical">
                                    Diferencia
                                </td>
                                <td class="lineaVertical">
                                    Status Concepto
                                </td>
                                <td>
                                    <asp:Label ID="lblSucursalCelula" runat="server"></asp:Label>
                                </td>
                                <td rowspan="2" style="vertical-align: top">
                                    <asp:RadioButtonList ID="ddlStatusEn" runat="server" BorderStyle="None" AppendDataBoundItems="True"
                                        CssClass="etiqueta dropDownPequeño fg-color-blanco" Style="margin: 0 0 0 0; padding: 0 0 0 0"
                                        Width="75px" Height="20px">
                                        <asp:ListItem Value="Externos" Selected="True">Externos</asp:ListItem>
                                        <asp:ListItem Value="Internos">Internos</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="lineaVertical">
                                    <asp:TextBox ID="txtDiferencia" runat="server" CssClass="cajaTextoPequeño etiqueta"
                                        Font-Size="12px" onkeypress="return ValidNumDecimal(event)" Style="margin-bottom: 3px;
                                        margin-right: 3px" Width="40px" ValidationGroup="Configuracion"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStatusConcepto" runat="server" AppendDataBoundItems="True"
                                        CssClass="etiqueta dropDownPequeño" Style="margin-bottom: 3px;" Width="150px">
                                        <asp:ListItem Text="NINGUNO" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="lineaVertical">
                                    <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="etiqueta dropDownPequeño"
                                        Style="margin-bottom: 3px; margin-right: 3px" Width="120px" Visible="False">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlCelula" runat="server" CssClass="etiqueta dropDownPequeño"
                                        Style="margin-bottom: 3px; margin-right: 3px" Width="120px" Visible="False">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td colspan="4">
                                    <asp:RangeValidator ID="rvDiferencia" runat="server" ControlToValidate="txtDiferencia"
                                        CssClass="etiqueta fg-color-amarillo" Display="Dynamic" EnableClientScript="True"
                                        Font-Size="10px" Type="Double" ValidationGroup="Configuracion">
                                    </asp:RangeValidator>
                                    <asp:RequiredFieldValidator ID="rfvDiferenciaVacio" runat="server" ControlToValidate="txtDiferencia"
                                        CssClass="etiqueta fg-color-amarillo" Display="Dynamic" EnableClientScript="True"
                                        ErrorMessage="Especifique la diferencia de centavos. " Font-Size="10px" ValidationGroup="Configuracion"></asp:RequiredFieldValidator>
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
                                <td>
                                    Filtrar en
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
                                        ToolTip="BUSCAR" Width="25px" OnClick="imgBuscar_Click" />
                                </td>
                                <td>
                                    Buscar en
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
                                        ToolTip="IMPORTAR" Width="25px"  OnClick="imgImportar_Click"/>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 1%; padding: 3px 3px 3px 0px; vertical-align: top">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion bg-color-naranjaOscuro" style="height: 30px">
                                    <asp:ImageButton ID="imgBitacora" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Bitacora.png"
                                        ToolTip="BITACORA AUDITORIA" Width="25px" OnClick="imgBitacora_Click" />
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
            <table style="width: 100%">
                <tr>
                    <td style="vertical-align: middle" class="etiqueta fg-color-blanco bg-color-azulClaro">
                        <div class="floatDerecha">
                            <asp:Button runat="server" ID="btnDesconciliar" Text="DESCONCILIAR" CssClass="boton bg-color-rojo fg-color-blanco"
                                OnClick="btnDesconciliar_Click" Style="margin: 0 0 0 0" OnClientClick="return confirm('¿Esta seguro de DESCONCILIAR las TRANSACCIONES EXTERNAS seleccionadas?')" />
                        </div>
                        <div style="padding: 5px 5px 5px 5px" class="centradoJustificado">
                            Transacciones Conciliadas</div>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td colspan="2">
                        <asp:GridView ID="grvConciliadas" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            PageSize="100" Width="100%" CssClass="grvResultadoConsultaCss" ShowHeaderWhenEmpty="True"
                            OnRowDataBound="grvConciliadas_RowDataBound" DataKeyNames="CorporativoConciliacion, SucursalConciliacion, AñoConciliacion, MesConciliacion, FolioConciliacion, Folio, Secuencia,Tipo"
                            OnSelectedIndexChanging="grvConciliadas_SelectedIndexChanging" OnRowCreated="grvConciliadas_RowCreated"
                            AllowSorting="True" OnSorting="grvConciliadas_Sorting" OnPageIndexChanging="grvConciliadas_PageIndexChanging">
                            <%-- <EmptyDataTemplate>
                        <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="No se han conciliado ninguna transacción."></asp:Label>
                    </EmptyDataTemplate>--%>
                            <HeaderStyle HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAllConciliados" runat="server" OnCheckedChanged="OnCheckedChangedConciliados"
                                            AutoPostBack="True" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkFolio" runat="server" AutoPostBack="False" />
                                    </ItemTemplate>
                                    <ControlStyle Width="100%"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Folio" SortExpression="Folio">
                                    <ItemTemplate>
                                        <asp:Label ID="lFolio" runat="server" Text='<%# resaltarBusqueda(Eval("Folio").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="centradoMedio" />
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sec." SortExpression="Secuencia">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSecuencia" runat="server" Text='<%# resaltarBusqueda(Eval("Secuencia").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="centradoMedio" />
                                    <ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" SortExpression="StatusConciliacion">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatusConciliacion" runat="server" Style="display: none" Text='<%# Bind("StatusConciliacion") %>'></asp:Label>
                                        <asp:Image ID="imgStatusConciliacion" runat="server" AlternateText='<%# Bind("StatusConciliacion") %>'
                                            CssClass="icono bg-color-blanco centradoMedio" Height="15px" ImageUrl='<%# Bind("UbicacionIcono") %>'
                                            ToolTip='<%# Bind("StatusConciliacion") %>' Width="15px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
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
                                <asp:TemplateField HeaderText="Deposito" SortExpression="Deposito">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeposito" runat="server" Text='<%# resaltarBusqueda(Eval("Deposito","{0:c2}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="centradoMedio" />
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Retiro" SortExpression="Retiro">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRetiro" runat="server" Text='<%# resaltarBusqueda(Eval("Retiro","{0:c2}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="centradoMedio" />
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Concepto" SortExpression="Concepto">
                                    <ItemTemplate>
                                        <div class="parrafoTexto" style="width: 400px">
                                            <asp:Label ID="lblConceptoExt" runat="server" Text='<%# resaltarBusqueda(Eval("Concepto").ToString()) %>'></asp:Label>
                                        </div>
                                        <asp:HoverMenuExtender ID="hmeConceptoExt" runat="server" TargetControlID="lblConceptoExt"
                                            PopupControlID="pnlPopUpConceptoExt" PopDelay="20" OffsetX="-30" OffsetY="-10">
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
                                            PopupControlID="pnlPopUpDescripcionExt" PopDelay="20" OffsetX="-30" OffsetY="-10">
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
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button runat="server" ID="imgDetalleConciliado" CssClass="Detalle centradoMedio boton bg-color-blanco"
                                            ToolTip="VER DETALLE" Width="20px" Height="20px" CommandName="Select" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="grvPaginacionScroll" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="width: 100%; vertical-align: top; padding: 5px 5px 5px 5px" class="etiqueta fg-color-blanco bg-color-verdeFuerte"
                        colspan="3">
                        Transacciones Por Conciliar
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%" class="centradoMedio">
                        <table style="width: 100%">
                            <tr>
                                <td class="bg-color-grisOscuro fg-color-blanco" style="width: 15%">
                                    Total Externo
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
                    <td rowspan="3" style="width: 1%">
                    </td>
                    <td style="width: 50%;" class="centradoMedio">
                        <table style="width: 100%; height: 20px">
                            <tr>
                                <td class="bg-color-grisOscuro fg-color-blanco" style="width: 15%" id="tdEtiquetaMontoIn"
                                    runat="server">
                                    Total Interno
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
                    <td style="vertical-align: top">
                        <div id="configuracionExternos" class="bg-color-grisClaro">
                            <table width="100%">
                                <tr>
                                    <td class="centradoDerecha" style="width: 20%;">
                                        <asp:ImageButton runat="server" ID="btnCANCELAREXTERNO" ToolTip="CANCELAR" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/NoConciliar.png"
                                            CssClass="icono bg-color-grisOscuro" OnClick="btnCANCELAREXTERNO_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:GridView ID="grvExternos" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            CssClass="grvResultadoConsultaCss" DataKeyNames="Secuencia,Folio" OnRowCreated="grvExternos_RowCreated"
                            OnRowDataBound="grvExternos_RowDataBound" OnSorting="grvExternos_Sorting" PageSize="100"
                            ShowHeaderWhenEmpty="True" Width="100%" AllowPaging="True" OnPageIndexChanging="grvExternos_PageIndexChanging">
                            <%--<EmptyDataTemplate>
                        <asp:Label ID="lblvacio" runat="server" Font-Bold="True" Font-Overline="False" ForeColor="#CC3300"
                            Text="No se encontraron referencias externas."></asp:Label>
                    </EmptyDataTemplate>--%>
                            <HeaderStyle HorizontalAlign="Center" />
                            <SortedAscendingHeaderStyle BackColor="Blue" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="LightBlue" />
                            <SortedDescendingHeaderStyle BackColor="Green" ForeColor="White" />
                            <SortedDescendingCellStyle BackColor="LightGreen" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkTodosExternos" runat="server" OnCheckedChanged="OnCheckedChangedExternos"
                                            AutoPostBack="True" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkFolioExt" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle BackColor="#ebecec" HorizontalAlign="Center" VerticalAlign="Top" Width="20px" />
                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sec." SortExpression="Secuencia">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSecuencia" runat="server" Text='<%# resaltarBusqueda(Eval("Secuencia").ToString()) %>' />
                                    </ItemTemplate>
                                    <ItemStyle BackColor="#ebecec" HorizontalAlign="Center" VerticalAlign="Top" Width="40px" />
                                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" SortExpression="StatusConciliacion">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatusConciliacion" runat="server" Style="display: none" Text='<%# Bind("StatusConciliacion") %>'></asp:Label>
                                        <asp:Image ID="imgStatusConciliacion" runat="server" AlternateText='<%# Bind("StatusConciliacion") %>'
                                            CssClass="icono border-color-grisOscuro centradoMedio" Height="15px" ImageUrl='<%# Bind("UbicacionIcono") %>'
                                            ToolTip='<%# Bind("StatusConciliacion") %>' Width="15px" />
                                    </ItemTemplate>
                                    <ItemStyle BackColor="#ebecec" HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FMovimiento" SortExpression="FMovimiento">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFMovimiento" runat="server" Text='<%# resaltarBusqueda(Eval("FMovimiento","{0:d}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FOperacion" SortExpression="FOperacion">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFOperacion" runat="server" Text='<%# resaltarBusqueda(Eval("FOperacion","{0:d}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
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
                                        <asp:Label ID="lblRetiro" runat="server" Text='<%# resaltarBusqueda(Eval("Retiro","{0:c2}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deposito" SortExpression="Deposito">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeposito" runat="server" Text='<%# resaltarBusqueda(Eval("Deposito","{0:c2}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Concepto" SortExpression="Concepto">
                                    <ItemTemplate>
                                        <div class="parrafoTexto">
                                            <asp:Label ID="lblConcepto" runat="server" Text='<%# resaltarBusqueda(Eval("Concepto").ToString()) %>'></asp:Label>
                                        </div>
                                        <asp:HoverMenuExtender ID="hmeConcepto" runat="server" OffsetX="-70" OffsetY="-10"
                                            PopDelay="20" PopupControlID="pnlPopUpConcepto" TargetControlID="lblConcepto">
                                        </asp:HoverMenuExtender>
                                        <asp:Panel ID="pnlPopUpConcepto" runat="server" BackColor="White" CssClass="grvResultadoConsultaCss ocultar"
                                            Style="padding: 5px 5px 5px 5px" Width="250px" Wrap="True">
                                            <asp:Label ID="lblToolTipConcepto" runat="server" CssClass="etiqueta " Font-Size="10px"
                                                Text='<%# resaltarBusqueda(Eval("Concepto").ToString()) %>'></asp:Label>
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Justify" Width="150px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripcion" SortExpression="Descripcion">
                                    <ItemTemplate>
                                        <div class="parrafoTexto">
                                            <asp:Label ID="lblDescripcion" runat="server" Text='<%# resaltarBusqueda(Eval("Descripcion").ToString()) %>'></asp:Label>
                                        </div>
                                        <asp:HoverMenuExtender ID="hmeDescripcion" runat="server" OffsetX="-30" OffsetY="-10"
                                            PopDelay="20" PopupControlID="pnlPopUpDescripcion" TargetControlID="lblDescripcion">
                                        </asp:HoverMenuExtender>
                                        <asp:Panel ID="pnlPopUpDescripcion" runat="server" BackColor="White" CssClass="grvResultadoConsultaCss ocultar"
                                            Style="padding: 5px 5px 5px 5px" Width="150px" Wrap="True">
                                            <asp:Label ID="lblToolTipDescripcion" runat="server" CssClass="etiqueta " Font-Size="10px"
                                                Text='<%# resaltarBusqueda(Eval("Descripcion").ToString()) %>'></asp:Label>
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Justify" Width="150px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="grvPaginacionScroll" />
                        </asp:GridView>
                        <asp:HiddenField ID="hfExternosSV" runat="server" />
                        <asp:HiddenField ID="hfExternosSH" runat="server" />
                    </td>
                    <td style="vertical-align: top" colspan="2">
                        <div id="configuracionInternosPedidos" class="bg-color-grisClaro">
                            <table width="100%">
                                <tr>
                                    <td rowspan="2" style="vertical-align: top; width: 12%;">
                                        <asp:Label ID="lblFOperacion" runat="server" Text="FOperación" CssClass="etiqueta fg-color-blanco centradoMedio"></asp:Label>
                                        <asp:Label ID="lblFSuminstro" runat="server" Text="FSuministro" CssClass="etiqueta fg-color-blanco centradoMedio"></asp:Label>
                                    </td>
                                    <td style="width: 12%;">
                                        <asp:TextBox ID="txtFOInicio" runat="server" Width="80px" CssClass="cajaTextoPequeño"
                                            ValidationGroup="vgFOperacion" ToolTip="FOper Inicio"></asp:TextBox>
                                        <asp:TextBox ID="txtFSInicio" runat="server" Width="80px" CssClass="cajaTextoPequeño"
                                            ValidationGroup="vgFSuministro" ToolTip="FSum Inicio"></asp:TextBox>
                                    </td>
                                    <td style="width: 12%;">
                                        <asp:TextBox ID="txtFOTermino" runat="server" Width="80px" CssClass="cajaTextoPequeño"
                                            ToolTip="FOper Fin"></asp:TextBox>
                                        <asp:TextBox ID="txtFSTermino" runat="server" Width="80px" CssClass="cajaTextoPequeño"
                                            ToolTip="FSum Fin"></asp:TextBox>
                                    </td>
                                    <td rowspan="2" style="vertical-align: top; width: 12%;">
                                        <asp:ImageButton ID="btnRangoFechasFO" runat="server" CssClass="icono bg-color-azulClaro"
                                            ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Filtrar.png" Height="16px"
                                            Width="16px" OnClick="btnRangoFechasFO_Click" ValidationGroup="vgFOperacion"
                                            ToolTip="FILTRAR FOperacion"></asp:ImageButton>
                                        <asp:ImageButton ID="btnRangoFechasFS" runat="server" CssClass="icono bg-color-azulClaro"
                                            ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Filtrar.png" Height="16px"
                                            Width="16px" ValidationGroup="vgFSuministro" OnClick="btnRangoFechasFS_Click"
                                            ToolTip="FILTRAR FSuminstro"></asp:ImageButton>
                                    </td>
                                    <td class="lineaVertical" rowspan="2">
                                    </td>
                                    <td rowspan="2" style="vertical-align: top; width: 10%;">
                                        <asp:Label ID="lblFMovimiento" runat="server" Text="FMovimiento" CssClass="etiqueta fg-color-blanco centradoMedio"></asp:Label>
                                    </td>
                                    <td style="width: 10%;">
                                        <asp:TextBox ID="txtFMInicio" runat="server" Width="80px" CssClass="cajaTextoPequeño"
                                            ToolTip="FMov Inicio"></asp:TextBox>
                                    </td>
                                    <td style="width: 10%;">
                                        <asp:TextBox ID="txtFMTermino" runat="server" Width="80px" CssClass="cajaTextoPequeño"
                                            ToolTip="FMov Fin"></asp:TextBox>
                                    </td>
                                    <td rowspan="2" style="vertical-align: top; width: 10%;">
                                        <asp:ImageButton ID="btnRangoFechasFM" runat="server" CssClass="icono bg-color-azulClaro"
                                            ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Filtrar.png" Height="16px"
                                            Width="16px" ValidationGroup="vgFMovimiento" OnClick="btnRangoFechasFM_Click"
                                            ToolTip="FILTRAR FMovimiento"></asp:ImageButton>
                                    </td>
                                    <td class="centradoDerecha" rowspan="2" style="vertical-align: top; width: 10%;">
                                        <asp:ImageButton runat="server" ID="btnCANCELARINTERNO" ToolTip="CANCELAR" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/NoConciliar.png"
                                            CssClass="icono bg-color-grisOscuro" OnClick="btnCANCELARINTERNO_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:RangeValidator ID="rvFOInicio" runat="server" ControlToValidate="txtFOInicio"
                                            ErrorMessage="Porfavor insertar una fecha valida" Type="Date" MinimumValue="28/12/1000"
                                            MaximumValue="28/12/9999" Display="Dynamic" ValidationGroup="vgFOperacion" CssClass="etiqueta">
                                        </asp:RangeValidator>
                                        <asp:RangeValidator ID="rvFOTermino" runat="server" ControlToValidate="txtFOTermino"
                                            ErrorMessage="Porfavor insertar una fecha valida" Type="Date" MinimumValue="28/12/1000"
                                            MaximumValue="28/12/9999" Display="Dynamic" ValidationGroup="vgFOperacion" CssClass="etiqueta">
                                        </asp:RangeValidator>
                                        <asp:RangeValidator ID="rvFSInicio" runat="server" ControlToValidate="txtFSInicio"
                                            ErrorMessage="Porfavor insertar una fecha valida" Type="Date" MinimumValue="28/12/1000"
                                            MaximumValue="28/12/9999" Display="Dynamic" ValidationGroup="vgFSuministro" CssClass="etiqueta">
                                        </asp:RangeValidator>
                                        <asp:RangeValidator ID="rvFSTermino" runat="server" ControlToValidate="txtFSTermino"
                                            ErrorMessage="Porfavor insertar una fecha valida" Type="Date" MinimumValue="28/12/1000"
                                            MaximumValue="28/12/9999" Display="Dynamic" ValidationGroup="vgFSuministro" CssClass="etiqueta">
                                        </asp:RangeValidator>
                                    </td>
                                    <td colspan="2">
                                        <asp:RangeValidator ID="rvFMInicio" runat="server" ControlToValidate="txtFMInicio"
                                            ErrorMessage="Porfavor insertar una fecha valida" Type="Date" MinimumValue="28/12/1000"
                                            MaximumValue="28/12/9999" Display="Dynamic" ValidationGroup="vgFMovimiento" CssClass="etiqueta">
                                        </asp:RangeValidator>
                                        <asp:RangeValidator ID="rvFMTermino" runat="server" ControlToValidate="txtFMTermino"
                                            ErrorMessage="Porfavor insertar una fecha valida" Type="Date" MinimumValue="28/12/1000"
                                            MaximumValue="28/12/9999" Display="Dynamic" ValidationGroup="vgFMovimiento" CssClass="etiqueta">
                                        </asp:RangeValidator>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:HiddenField ID="hfInternosSV" runat="server" />
                        <asp:HiddenField ID="hfInternosSH" runat="server" />
                        <asp:GridView ID="grvInternos" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" CssClass="grvResultadoConsultaCss" DataKeyNames="Secuencia, Folio"
                            OnPageIndexChanging="grvInternos_PageIndexChanging" OnRowCreated="grvInternos_RowCreated"
                            OnRowDataBound="grvInternos_RowDataBound" OnSorting="grvInternos_Sorting" PageSize="100"
                            ShowFooter="False" ShowHeader="True" ShowHeaderWhenEmpty="True" Width="550px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkTodosInternos" runat="server" AutoPostBack="True" OnCheckedChanged="OnCheckedChangedInternos" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSecuenciaInt" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle BackColor="#ebecec" HorizontalAlign="Center" VerticalAlign="Top" Width="20px" />
                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sec." SortExpression="Secuencia">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSecuenciaIn" runat="server" Text='<%# resaltarBusqueda(Eval("Secuencia").ToString()) %>' />
                                    </ItemTemplate>
                                    <ItemStyle BackColor="#ebecec" HorizontalAlign="Center" Width="40px" Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Center" Width="40px" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" SortExpression="StatusConciliacion">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatusConciliacion" runat="server" Style="display: none" Text='<%# Bind("StatusConciliacion") %>'></asp:Label>
                                        <asp:Image ID="imgStatusConciliacion" runat="server" AlternateText='<%# Bind("StatusConciliacion") %>'
                                            CssClass="icono border-color-grisOscuro centradoMedio" Height="15px" ImageUrl='<%# Bind("UbicacionIcono") %>'
                                            ToolTip='<%# Bind("StatusConciliacion") %>' Width="15px" />
                                    </ItemTemplate>
                                    <ControlStyle />
                                    <ItemStyle BackColor="#ebecec" HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Folio" SortExpression="Folio">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFolioIn" runat="server" Text='<%# resaltarBusqueda(Eval("Folio").ToString()) %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FMovimiento" SortExpression="FMovimiento">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFMovimiento" runat="server" Text='<%# resaltarBusqueda(Eval("FMovimiento","{0:d}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FOperacion" SortExpression="FOperacion">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFOperacion" runat="server" Text='<%# resaltarBusqueda(Eval("FOperacion","{0:d}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Referencia" SortExpression="Referencia">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReferencia" runat="server" Text='<%# resaltarBusqueda(Eval("Referencia").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Retiro" SortExpression="Retiro">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRetiro" runat="server" Text='<%# resaltarBusqueda(Eval("Retiro","{0:c2}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deposito" SortExpression="Deposito">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeposito" runat="server" Text='<%# resaltarBusqueda(Eval("Deposito","{0:c2}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Concepto" SortExpression="Concepto">
                                    <ItemTemplate>
                                        <div class="parrafoTexto">
                                            <asp:Label ID="lblConcepto" runat="server" Text='<%# resaltarBusqueda(Eval("Concepto").ToString()) %>'></asp:Label>
                                        </div>
                                        <asp:HoverMenuExtender ID="hmeConcepto" runat="server" OffsetX="-30" OffsetY="-10"
                                            PopDelay="20" PopupControlID="pnlPopUpConcepto" TargetControlID="lblConcepto">
                                        </asp:HoverMenuExtender>
                                        <asp:Panel ID="pnlPopUpConcepto" runat="server" BackColor="White" CssClass="grvResultadoConsultaCss ocultar"
                                            Style="padding: 5px 5px 5px 5px" Width="150px" Wrap="True">
                                            <asp:Label ID="lblToolTipConcepto" runat="server" CssClass="etiqueta " Font-Size="10px"
                                                Text='<%# resaltarBusqueda(Eval("Concepto").ToString()) %>' />
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Justify" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripcion" SortExpression="Descripcion">
                                    <ItemTemplate>
                                        <div class="parrafoTexto">
                                            <asp:Label ID="lblDescripcion" runat="server" Text='<%# resaltarBusqueda(Eval("Descripcion").ToString()) %>'></asp:Label>
                                        </div>
                                        <asp:HoverMenuExtender ID="hmeDescripcion" runat="server" OffsetX="-30" OffsetY="-10"
                                            PopDelay="20" PopupControlID="pnlPopUpDescripcion" TargetControlID="lblDescripcion">
                                        </asp:HoverMenuExtender>
                                        <asp:Panel ID="pnlPopUpDescripcion" runat="server" BackColor="White" CssClass="grvResultadoConsultaCss ocultar"
                                            Style="padding: 5px 5px 5px 5px" Width="150px" Wrap="True">
                                            <asp:Label ID="lblToolTipDescripcion" runat="server" CssClass="etiqueta " Font-Size="10px"
                                                Text='<%# resaltarBusqueda(Eval("Descripcion").ToString()) %>' />
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Justify" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="grvPaginacionScroll" />
                        </asp:GridView>

                        <!--    POPUP CARGA ARCHIVO    -->
                        <asp:HiddenField runat="server" ID="hdfCargaArchivo" />
                        <asp:ModalPopupExtender ID="mpeCargaArchivoConciliacionManual" runat="server" BackgroundCssClass="ModalBackground"
                            DropShadow="False" EnableViewState="false" PopupControlID="pnlCargaArchivo" TargetControlID="hdfCargaArchivo"
                            CancelControlID="btnCerrarCargaArchivo">
                        </asp:ModalPopupExtender>
                        <asp:Panel ID="pnlCargaArchivo" runat="server" CssClass="ModalPopup" Width="400px" Style="display: none">
                            <table style="width: 80%;">
                                <tr class="bg-color-grisOscuro">
                                    <td colspan="4" style="padding: 5px 5px 5px 5px" class="etiqueta">
                                        <div class="floatIzquierda">
                                            <asp:ImageButton runat="server" ID="btnCerrarCargaArchivo" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png"
                                                CssClass="iconoPequeño bg-color-rojo" />
                                        </div>
                                        <!--<div class="fg-color-blanco">
                                            Buscar
                                        </div>  -->
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 5px 5px 5px 5px; width: 85%">
                                        <!--<div class="etiqueta">
                                            Valor
                                        </div>
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="cajaTexto" Font-Size="12px"
                                            Width="95%">
                                        </asp:TextBox>-->
                                        <asp:FileUpload ID="fupSeleccionar" runat="server" />
                                        <asp:Button ID="btnSubirArchivo" runat="server" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                            Text="Seleccionar archivo..." OnClick="btnSubirArchivo_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>  <!--  style="width: 5%"   -->
                                        <!--<asp:Button ID="Button1" runat="server" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                            Text="BUSCAR" OnClick="btnIrBuscar_Click" /> -->
                                        <asp:Label ID="lblArchivo" runat="server" CssClass="etiqueta " Font-Size="10px" Text="Archivo: " />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="bg-color-grisClaro01" colspan="4">
                                        <asp:Label ID="lblRegistros" runat="server" CssClass="etiqueta " Font-Size="10px" 
                                            Text="Total de registros a cargar: " />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 5px 5px 5px 5px; width: 100%; text-align: center">
                                        <asp:GridView ID="grvDetalleConciliacionManual" runat="server" AutoGenerateColumns="False"
                                            AllowPaging="True" ShowHeader="True" Width="850px" CssClass="grvResultadoConsultaCss"
                                            PageSize="15" ShowHeaderWhenEmpty="True" ShowFooter="False" DataKeyNames="SecuenciaInterno, FolioInterno">
                                            <!--<EmptyDataTemplate>
                                                <asp:Label ID="lblvacio" runat="server" Font-Bold="True" Font-Overline="False" ForeColor="#CC3300"
                                                    Text="No se encontraron referencias internas"></asp:Label>
                                            </EmptyDataTemplate>-->
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Image ID="img" runat="server" CssClass="icono bg-color-verdeClaro centradoMedio"
                                                            Height="15px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Exito.png" Width="15px">
                                                        </asp:Image>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="bg-color-verdeClaro centradoMedio"
                                                        BackColor="#ebecec" Width="30px"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Documento" SortExpression="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocumento" runat="server" Text='<%# Bind("Documento") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ControlStyle CssClass="centradoMedio" />
                                                    <ItemStyle HorizontalAlign="Center" BackColor="#d9b335" ForeColor="White" Width="50px">
                                                    </ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cuenta" SortExpression="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCuenta" runat="server" Text='<%# Bind("Cuenta") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ControlStyle CssClass="centradoMedio" />
                                                    <ItemStyle HorizontalAlign="Center" BackColor="#ebecec"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Monto" SortExpression="monto">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMonto" runat="server" Text='<%# Bind("Monto","{0:c2}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnCargaArchivoAceptar" runat="server" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                            Text="Aceptar" OnClick="btnCargarArchivoAceptar_Click" />
                                        <asp:Button ID="btnCargaArchivCancelar" runat="server" CssClass="boton fg-color-blanco bg-color-grisClaro"
                                            Text="Cancelar" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <!--    FIN POPUP CARGA ARCHIVO    -->

                        <asp:GridView ID="grvPedidos" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            CssClass="grvResultadoConsultaCss" DataKeyNames="Celula,Pedido,AñoPed,Cliente"
                            AllowPaging="True" PageSize="200" OnPageIndexChanging="grvPedidos_PageIndexChanging"
                            OnRowCreated="grvPedidos_RowCreated" OnSorting="grvPedidos_Sorting" ShowHeader="True"
                            ShowHeaderWhenEmpty="True" Width="100%">
                            <%-- <EmptyDataTemplate>
                                <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="No se encontraron información sobre pedidos."></asp:Label>
                            </EmptyDataTemplate>--%>
                            <HeaderStyle HorizontalAlign="Center" />
                            <Columns>
                                 <%-- <asp:TemplateField HeaderText="Pedido" SortExpression="Pedido">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPedido" runat="server" Text='<%# resaltarBusqueda(Eval("Pedido").ToString()) %>' />
                                    </ItemTemplate>
                                    <ItemStyle BackColor="#ebecec" HorizontalAlign="Center" Width="100px" />
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                </asp:TemplateField> --%>
                                <%-- <asp:TemplateField HeaderText="Celula" SortExpression="Celula">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCelula" runat="server" Text='<%# resaltarBusqueda(Eval("Celula").ToString()) %>' />
                                    </ItemTemplate>
                                    <ItemStyle BackColor="#ebecec" HorizontalAlign="Center" Width="100px" />
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="FSuministro" SortExpression="FSuministro">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFSuministro" runat="server" Text='<%# resaltarBusqueda(Eval("FSuministro","{0:d}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Monto" SortExpression="Total">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMontoPedido" runat="server" Text='<%# resaltarBusqueda(Eval("Total","{0:c2}").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nom. Cliente" SortExpression="Nombre">
                                    <ItemTemplate>
                                        <div class="parrafoTexto">
                                            <asp:Label ID="lblCliente" runat="server" Text='<%# resaltarBusqueda(Eval("Nombre").ToString()) %>'>
                                            </asp:Label>
                                        </div>
                                        <asp:HoverMenuExtender ID="hmeCliente" runat="server" OffsetX="-30" OffsetY="-10"
                                            PopDelay="20" PopupControlID="pnlPopUpCliente" TargetControlID="lblCliente">
                                        </asp:HoverMenuExtender>
                                        <asp:Panel ID="pnlPopUpCliente" runat="server" BackColor="White" CssClass="grvResultadoConsultaCss ocultar"
                                            Style="padding: 5px 5px 5px 5px" Width="250px" Wrap="True">
                                            <asp:Label ID="lblToolTipCliente" runat="server" CssClass="etiqueta " Font-Size="10px"
                                                Text='<%# resaltarBusqueda(Eval("Nombre").ToString()) %>' />
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Justify" Width="150px" />
                                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Concepto" SortExpression="Concepto">
                                    <ItemTemplate>
                                        <div class="parrafoTexto">
                                            <asp:Label ID="lblConcepto" runat="server" Text='<%# resaltarBusqueda(Eval("Concepto").ToString()) %>'>
                                            </asp:Label>
                                        </div>
                                        <asp:HoverMenuExtender ID="hmeConcepto" runat="server" OffsetX="-30" OffsetY="-10"
                                            PopDelay="20" PopupControlID="pnlPopUpConcepto" TargetControlID="lblConcepto">
                                        </asp:HoverMenuExtender>
                                        <asp:Panel ID="pnlPopUpConcepto" runat="server" BackColor="White" CssClass="grvResultadoConsultaCss ocultar"
                                            Style="padding: 5px 5px 5px 5px" Width="120px" Wrap="True">
                                            <asp:Label ID="lblToolTipConcepto" runat="server" CssClass="etiqueta " Font-Size="10px"
                                                Text='<%# resaltarBusqueda(Eval("Concepto").ToString()) %>' />
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="grvPaginacionScroll" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
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
                        <td class="bg-color-grisClaro01" colspan="4">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
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
                                AllowPaging="True" ShowHeader="True" Width="850px" CssClass="grvResultadoConsultaCss"
                                PageSize="15" ShowHeaderWhenEmpty="True" ShowFooter="False" DataKeyNames="SecuenciaInterno, FolioInterno">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblvacio" runat="server" Font-Bold="True" Font-Overline="False" ForeColor="#CC3300"
                                        Text="No se encontraron referencias internas"></asp:Label>
                                </EmptyDataTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Image ID="img" runat="server" CssClass="icono bg-color-verdeClaro centradoMedio"
                                                Height="15px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Exito.png" Width="15px">
                                            </asp:Image>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" CssClass="bg-color-verdeClaro centradoMedio"
                                            BackColor="#ebecec" Width="30px"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Secuencia" SortExpression="secuenciaInt">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSecuenciaInt" runat="server" Text='<%# Bind("SecuenciaInterno") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="centradoMedio" />
                                        <ItemStyle HorizontalAlign="Center" BackColor="#d9b335" ForeColor="White" Width="50px">
                                        </ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Folio" SortExpression="folioInterno">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFolioInterno" runat="server" Text='<%# Bind("FolioInterno") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="centradoMedio" />
                                        <ItemStyle HorizontalAlign="Center" BackColor="#ebecec"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FMovimiento" SortExpression="fMovimiento">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFMovimiento" runat="server" Text='<%# Bind("FMovimientoInt","{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="centradoMedio" />
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FOperacion" SortExpression="fOperacion">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFOperacion" runat="server" Text='<%# Bind("FOperacionInt","{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="centradoMedio" />
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Monto" SortExpression="monto">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMonto" runat="server" Text='<%# Bind("MontoInterno","{0:c2}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
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
                                AllowPaging="True" ShowHeader="True" Width="100%" CssClass="grvResultadoConsultaCss"
                                PageSize="15" ShowHeaderWhenEmpty="True" DataKeyNames="Celula,Pedido,AñoPed">
                                <HeaderStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Image ID="img" runat="server" CssClass="icono" Height="15px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Exito.png"
                                                Width="15px"></asp:Image>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" CssClass="bg-color-verdeClaro centradoMedio"
                                            BackColor="#ebecec" Width="30px"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ped." SortExpression="Pedido">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPedido" runat="server" Text='<%# resaltarBusqueda(Eval("Pedido").ToString()) %>' />
                                        </ItemTemplate>
                                        <ControlStyle CssClass="centradoMedio" />
                                        <ItemStyle HorizontalAlign="Center" BackColor="#d9b335" ForeColor="White" Width="50px">
                                        </ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
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
                                            <asp:Label ID="lblMontoPedido" runat="server" Text='<%# resaltarBusqueda(Eval("Total","{0:c2}").ToString()) %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label runat="server" ID="lblTotalMontoPedido"></asp:Label>
                                        </FooterTemplate>
                                        <ControlStyle CssClass="centradoMedio" />
                                        <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nom. Cliente" SortExpression="Nombre">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCliente" runat="server" Text='<%# resaltarBusqueda(Eval("Nombre").ToString()) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Wrap="True"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
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
            <asp:HiddenField runat="server" ID="hdfDesconciliarConfirmar" />
            <asp:ModalPopupExtender ID="mpeConfirmarDesconciliado" runat="server" BackgroundCssClass="ModalBackground"
                DropShadow="False" EnableViewState="false" PopupControlID="pnlConfirmarDesconciliar"
                TargetControlID="hdfDesconciliarConfirmar" CancelControlID="btnCancelarConfirmar">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlConfirmarDesconciliar" runat="server" CssClass="ModalPopup" EnableViewState="false"
                Width="350px" Style="display: none">
                <table style="width: 100%;">
                    <tr class="bg-color-grisOscuro">
                        <td colspan="5" style="padding: 5px 5px 5px 5px" class="etiqueta">
                            <div class="lineaHorizontal fg-color-blanco">
                                CONFIRMAR DESCONCILIACIÓN
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
                                ¿Esta seguro de <b class="fg-color-rojo">DESCONCILIAR</b> la(s) transaccion(es)
                                conciliadas/externas/internas?<br />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="centradoMedio" colspan="2">
                            <div class="lineaHorizontal">
                            </div>
                            <asp:Button runat="server" ID="btnAceptarConfirmar" Text="ACEPTAR" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                OnClick="btnAceptarConfirmar_Click" />
                            <asp:Button runat="server" ID="btnCancelarConfirmar" Text="CANCELAR" CssClass="boton fg-color-blanco bg-color-grisClaro" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="bg-color-grisClaro01">
                            &nbsp;
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
                        <td class="bg-color-grisClaro01" colspan="2">
                            &nbsp;
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
                        <td class="bg-color-grisClaro01" colspan="2">
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
                                CANCELAR TRANSACCIÓN (ES)</div>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    
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

    <asp:UpdateProgress ID="panelBloqueo" runat="server" AssociatedUpdatePanelID="upDetalleConciliacion">
        <ProgressTemplate>
            <asp:Image ID="imgLoad" runat="server" CssClass="icono bg-color-blanco" Height="40px"
                ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/LoadPage.gif" Width="40px" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ModalPopupExtender ID="mpeLoading" runat="server" BackgroundCssClass="ModalBackground"
        PopupControlID="panelBloqueo" TargetControlID="panelBloqueo">
    </asp:ModalPopupExtender>
</asp:Content>
