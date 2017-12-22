<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true"
    CodeFile="FlujoEfectivoProyectado.aspx.cs" Inherits="Conciliacion_FlujoEfectivo_FlujoEfectivoProyectado" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    FLUJO DE EFECTIVO PROYECTADO
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <!--Libreria jQuery-->
    <script src="../../App_Scripts/jQueryScripts/jquery-3.2.1.min.js" type="text/javascript"></script>
    <script src="../../App_Scripts/jQueryScripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../App_Scripts/jQueryScripts/jquery.ui.datepicker-es.js" type="text/javascript"></script>
    <link href="../../App_Scripts/jQueryScripts/css/custom-theme/jquery-ui-1.10.2.custom.min.css"
        rel="stylesheet" type="text/css" />
    <!-- Script se utiliza para el Scroll del GridView-->
    <script src="../../App_Scripts/ScrollGridView/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../../App_Scripts/ScrollGridView/GridviewScroll.css" rel="stylesheet"
        type="text/css" />
    <script src="../../App_Scripts/Common.js" type="text/javascript"></script>
    <script type="text/javascript">
        function pageLoad() {
            //gridview
            gridsviewScroll();
            //Activar DatePickers
            // var meses = "<% = mesesMaximo.ToString() %>"
            activarDatePickers(1);
        }

        function gridsviewScroll() {
            $('#<%=grvFlujoEfectivoEntrada.ClientID%>').gridviewScroll({
                width: 1200,
                height: 250,
                freezesize: 1,
                arrowsize: 30,
                varrowtopimg: '../../App_Scripts/ScrollGridView/Images/arrowvt.png',
                varrowbottomimg: '../../App_Scripts/ScrollGridView/Images/arrowvb.png',
                harrowleftimg: '../../App_Scripts/ScrollGridView/Images/arrowhl.png',
                harrowrightimg: '../../App_Scripts/ScrollGridView/Images/arrowhr.png',
                headerrowcount: 1,
                startVertical: $("#<%=hfFlujoESV.ClientID%>").val(),
                startHorizontal: $("#<%=hfFlujoESH.ClientID%>").val(),
                onScrollVertical: function (delta) { $("#<%=hfFlujoESV.ClientID%>").val(delta); },
                onScrollHorizontal: function (delta) { $("#<%=hfFlujoESH.ClientID%>").val(delta); },
                scrollAssociate: { mode: "horizontal", target: "<%=grvFlujoEfectivoSalida.ClientID%>" }
            });

            $('#<%=grvFlujoEfectivoSalida.ClientID%>').gridviewScroll({
                width: 1200,
                height: 250,
                freezesize: 1,
                arrowsize: 30,
                varrowtopimg: '../../App_Scripts/ScrollGridView/Images/arrowvt.png',
                varrowbottomimg: '../../App_Scripts/ScrollGridView/Images/arrowvb.png',
                harrowleftimg: '../../App_Scripts/ScrollGridView/Images/arrowhl.png',
                harrowrightimg: '../../App_Scripts/ScrollGridView/Images/arrowhr.png',
                startVertical: $("#<%=hfFlujoSSV.ClientID%>").val(),
                startHorizontal: $("#<%=hfFlujoSSH.ClientID%>").val(),
                onScrollVertical: function (delta) { $("#<%=hfFlujoSSV.ClientID%>").val(delta); },
                onScrollHorizontal: function (delta) { $("#<%=hfFlujoSSH.ClientID%>").val(delta); },
                scrollAssociate: { mode: "horizontal", target: "<%=grvFlujoEfectivoEntrada.ClientID%>" }
            });
            $('#<%=grvFlujoEfectivoSaldos.ClientID%>').gridviewScroll({
                width: 1200,
                height: 250,
                freezesize: 1,
                arrowsize: 30,
                varrowtopimg: '../../App_Scripts/ScrollGridView/Images/arrowvt.png',
                varrowbottomimg: '../../App_Scripts/ScrollGridView/Images/arrowvb.png',
                harrowleftimg: '../../App_Scripts/ScrollGridView/Images/arrowhl.png',
                harrowrightimg: '../../App_Scripts/ScrollGridView/Images/arrowhr.png',
                startVertical: $("#<%=hfFlujoSSLV.ClientID%>").val(),
                startHorizontal: $("#<%=hfFlujoSSLH.ClientID%>").val(),
                onScrollVertical: function (delta) { $("#<%=hfFlujoSSLV.ClientID%>").val(delta); },
                onScrollHorizontal: function (delta) { $("#<%=hfFlujoSSLH.ClientID%>").val(delta); },
                scrollAssociate: { mode: "horizontal", target: "<%=grvFlujoEfectivoEntrada.ClientID%>" }
            });
        }

    </script>
    <script language="javascript" type="text/javascript">
        function activarDatePickers(meses) {

            //DatePicker FInicio
            $("#<%= txtFInicial.ClientID%>").datepicker({

                changeMonth: true,
                dateFormat: 'dd-mm-yy',
                onClose: function (selectedDate, instance) {
                    if (selectedDate != '') {
                        $("#<%=txtFFinal.ClientID%>").datepicker("option", "minDate", selectedDate);
                        var date = $.datepicker.parseDate(instance.settings.dateFormat, selectedDate, instance.settings);
                        date.setMonth(date.getMonth() + meses);
                        console.log(selectedDate, date);
                        $("#<%=txtFFinal.ClientID%>").datepicker("option", "minDate", selectedDate);
                        $("#<%=txtFFinal.ClientID%>").datepicker("option", "maxDate", date);
                    }
                }
            });
            //DatePicker FFin
            $("#<%=txtFFinal.ClientID%>").datepicker({

                minDate: "dateToday",
                changeMonth: true,
                dateFormat: 'dd-mm-yy',
                onClose: function (selectedDate) {
                    $("#<%=txtFFinal.ClientID%>").datepicker("option", "maxDate", selectedDate);
                }
            });
            return false;
        }
    </script>
    <script language="javascript" type="text/javascript">
        function CalculateExpense(obj) {
            var row = obj.parentNode.parentNode; var inputs = row.getElementsByTagName("input");

            var sum = 0;
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "text") {

                    if (inputs[i].id.indexOf("txtTotal") == -1) //Here give the id of the Total textbox
                    {
                        //Calculate Total
                        if (inputs[i].value != null && inputs[i].value != "")
                            sum += parseInt(inputs[i].value);
                    }
                    else {
                        //Store the Total
                        inputs[i].value = sum;
                    }
                }
            }
        }
    </script>
    <!-- Validar: solo numeros -->
    <script type="text/javascript">
        function ValidNum(e) {
            var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
            return ((tecla > 47 && tecla < 58));
        }
        function ValidNumDecimal(event, _float) {
            event = event || window.event;
            var charCode = event.keyCode || event.which;
            var first = ((charCode <= 57 && charCode >= 48) || charCode == 8);
            if (_float) {
                var element = event.srcElement || event.target;
                return first || (element.value.indexOf('.') == -1 ? charCode == 46 : false);
            }
            return first;
        }
    </script>
    <%--  <script language="javascript" type="text/javascript">

        function validateTextBox() {
          // Get The base and Child controls
            var targetBaseControl = document.getElementById("<%=grvFlujoProyectado.ClientID%>");
          // Get the all the control of the type Input in the basse contrl
            var inputs = targetBaseControl.getElementsByTagName("input");
          // loop thorught the all textboxes
            for (var n = 0; n < inputs.length; ++n) {
                if (inputs[n].type == 'text') {
                    // Validate for input
                    if (inputs[n].value != "")
                        return true; 
                    alert('Ingrese un valor valido.');
                    return false;
                }
            }
            return true;
        }
 
         
    </script>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager runat="server" ID="smFlujoProyectado" AsyncPostBackTimeout="600"
        EnableScriptGlobalization="True">
    </asp:ScriptManager>
    <script src="../../App_Scripts/jsUpdateProgress.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        function ShowModalPopup() {
            $find("ModalBehaviour").show();
        }

        function HideModalPopup() {
            $find("ModalBehaviour").hide();
        }
       
    </script>
    <script type="text/javascript" language="javascript">
        var ModalProgress = '<%=mpeLoading.ClientID%>';        
    </script>
    <asp:UpdatePanel runat="server" ID="upFlujoProyectado">
        <Triggers>
            <asp:PostBackTrigger ControlID="imgExportar" />
        </Triggers>
        <ContentTemplate>
         <table id="BarraHerramientas" class="bg-color-grisClaro01" style="width: 100%; vertical-align: top">
                <tr>
                    <td style="padding: 3px 3px 3px 0px; vertical-align: top; width: 70%">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion  bg-color-azulClaro" rowspan="2" style="width: 1%">
                                    <asp:ImageButton ID="btnActualizarConfig" runat="server" Height="25px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/ActualizarConfig.png"
                                        ToolTip="ACTUALIZAR CONFIGURACION" Width="25px" ValidationGroup="Configuracion"
                                        OnClick="btnActualizarConfig_Click" />
                                </td>
                                <td class="lineaVertical" style="width: 30%">
                                    Empresa
                                </td>
                                <td class="lineaVertical" style="width: 30%">
                                    Sucursal
                                </td>
                                <td class="lineaVertical" style="width: 20%">
                                    Fecha Inicial
                                </td>
                                <td class="lineaVertical" style="width: 20%">
                                    Fecha Final
                                </td>
                            </tr>
                            <tr>
                                <td class="lineaVertical" style="width: 30%">
                                    <asp:DropDownList ID="ddlEmpresa" runat="server" Width="100%" SkinID="DropDownList"
                                        CssClass="dropDown" AutoPostBack="True" OnDataBound="ddlEmpresa_DataBound" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="lineaVertical" style="width: 30%">
                                    <asp:DropDownList ID="ddlSucursal" runat="server" Width="100%" CssClass="dropDown"
                                        AutoPostBack="False">
                                    </asp:DropDownList>
                                </td>
                                <td class="lineaVertical" style="width: 20%">
                                    <asp:TextBox runat="server" ID="txtFInicial" CssClass="cajaTexto" Width="90%"></asp:TextBox>
                                    <asp:HiddenField ID="hdfFConsulta" runat="server" />
                                </td>
                                <td class="lineaVertical" style="width: 20%">
                                    <asp:TextBox runat="server" ID="txtFFinal" CssClass="cajaTexto" Width="90%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 1%; padding: 3px 3px 3px 0px; vertical-align: top">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion bg-color-purpura" style="height: 30px">
                                    <asp:ImageButton ID="btnFiltrar" runat="server" Height="25px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Filtrar.png"
                                        ToolTip="FILTRAR" Width="25px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="padding: 3px 3px 3px 0px; vertical-align: top; width: 1%">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion bg-color-naranja" style="height: 30px">
                                    <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Buscar.png"
                                        ToolTip="BUSCAR" Width="25px" />
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
                                <td class="iconoOpcion bg-color-azulOscuro" style="height: 30px">
                                    <asp:ImageButton ID="imgGuardar" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Guardar.png"
                                        ToolTip="GUARDAR" Width="25px" OnClick="imgGuardar_Click" OnClientClick="return validateTextBox();" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 1%; padding: 3px 3px 3px 0px; vertical-align: top">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion bg-color-negro" style="height: 30px">
                                    <asp:ImageButton ID="imgCerrarConciliacion" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png"
                                        ToolTip="CERRAR FLUJO EFECTIVO DEL MES" Width="25px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 1%; padding: 3px 3px 3px 0px; vertical-align: top">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion bg-color-rojo" style="height: 30px">
                                    <asp:ImageButton ID="imgCancelarConciliacion" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cancelar.png"
                                        ToolTip="CANCELAR CONCILIACION" Width="25px" OnClientClick="return confirm('¿Esta seguro de CANCELAR la conciliación.?')" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="vertical-align: middle; padding: 5px 5px 5px 5px" class="etiqueta centradoJustificado fg-color-blanco bg-color-azulClaro">
                        FLUJO DE EFECTIVO REAL/PROYECTADO
                        <asp:HiddenField ID="CorporativoConsulta" runat="server" />
                        <asp:HiddenField ID="SucursalConsulta" runat="server" />
                        <asp:HiddenField ID="FInicioConsulta" runat="server" />
                        <asp:HiddenField ID="FFinConsulta" runat="server" />
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td colspan="2">
                        <asp:HiddenField ID="hfFlujoESV" runat="server" />
                        <asp:HiddenField ID="hfFlujoESH" runat="server" />
                        <asp:GridView ID="grvFlujoEfectivoEntrada" runat="server" AutoGenerateColumns="False"
                            Width="100%" ShowHeaderWhenEmpty="True" CssClass="grvResultadoConsultaCss" AllowSorting="True"
                            OnRowDataBound="grvFlujoEfectivoEntrada_RowDataBound" ViewStateMode="Enabled"
                            ShowFooter="False" DataKeyNames="Concepto">
                            <EmptyDataTemplate>
                                <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="No existe ninguna conciliación de acuerdo a los Parámetros del Filtrado. "></asp:Label>
                            </EmptyDataTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <RowStyle HorizontalAlign="Center" Wrap="False"></RowStyle>
                            <FooterStyle HorizontalAlign="Center" Wrap="False" CssClass="bg-color-grisOscuro fg-color-blanco">
                            </FooterStyle>
                            <PagerStyle CssClass="estiloPaginacion bg-color-grisOscuro fg-color-blanco" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td colspan="2">
                        <asp:HiddenField ID="hfFlujoSSV" runat="server" />
                        <asp:HiddenField ID="hfFlujoSSH" runat="server" />
                        <asp:GridView ID="grvFlujoEfectivoSalida" runat="server" AutoGenerateColumns="False"
                            ShowHeader="True" Width="100%" ShowHeaderWhenEmpty="False" CssClass="grvResultadoConsultaCss"
                            AllowSorting="True" OnRowDataBound="grvFlujoEfectivoSalida_RowDataBound" ViewStateMode="Enabled"
                            ShowFooter="False" DataKeyNames="Concepto">
                            <EmptyDataTemplate>
                                <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="No existe ninguna conciliación de acuerdo a los Parámetros del Filtrado. "></asp:Label>
                            </EmptyDataTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <RowStyle HorizontalAlign="Center" Wrap="False"></RowStyle>
                            <FooterStyle HorizontalAlign="Center" Wrap="False" CssClass="bg-color-grisOscuro fg-color-blanco">
                            </FooterStyle>
                            <PagerStyle CssClass="estiloPaginacion bg-color-grisOscuro fg-color-blanco" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td colspan="2">
                        <asp:HiddenField ID="hfFlujoSSLV" runat="server" />
                        <asp:HiddenField ID="hfFlujoSSLH" runat="server" />
                        <asp:GridView ID="grvFlujoEfectivoSaldos" runat="server" AutoGenerateColumns="False"
                            ShowHeader="True" Width="100%" ShowHeaderWhenEmpty="False" CssClass="grvResultadoConsultaCss"
                            AllowSorting="True" OnRowDataBound="grvFlujoEfectivoSaldos_RowDataBound" ViewStateMode="Enabled"
                            ShowFooter="False" DataKeyNames="Concepto">
                            <EmptyDataTemplate>
                                <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="No existe ninguna conciliación de acuerdo a los Parámetros del Filtrado. "></asp:Label>
                            </EmptyDataTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <RowStyle HorizontalAlign="Center" Wrap="False"></RowStyle>
                            <FooterStyle HorizontalAlign="Center" Wrap="False" CssClass="bg-color-grisOscuro fg-color-blanco">
                            </FooterStyle>
                            <PagerStyle CssClass="estiloPaginacion bg-color-grisOscuro fg-color-blanco" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="panelBloqueo" runat="server" AssociatedUpdatePanelID="upFlujoProyectado">
        <ProgressTemplate>
            <asp:Image ID="imgLoad" runat="server" CssClass="icono bg-color-blanco" Height="40px"
                ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/LoadPage.gif" Width="40px" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ModalPopupExtender ID="mpeLoading" runat="server" BackgroundCssClass="ModalBackground"
        PopupControlID="panelBloqueo" TargetControlID="panelBloqueo">
    </asp:ModalPopupExtender>
</asp:Content>
