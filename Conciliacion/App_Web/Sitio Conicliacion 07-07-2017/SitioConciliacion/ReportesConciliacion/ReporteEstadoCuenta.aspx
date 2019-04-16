﻿<%@ Page Title="Reportecuenta" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="ReporteEstadoCuenta.aspx.cs" Inherits="ReportesConciliacion_ReporteEstadoCuenta" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="~/ControlesUsuario/wuCuentasBancarias/WUCListadoCuentasBancarias.ascx" tagname="WUCListadoCuentasBancarias" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
    Reporte Estado Cuenta
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!--Libreria jQuery-->
     <!--Libreria jQuery-->
    <script src="../../App_Scripts/jQueryScripts/jquery-3.2.1.min.js" type="text/javascript"></script>
    <script src="../../App_Scripts/jQueryScripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../App_Scripts/jQueryScripts/jquery.ui.datepicker-es.js" type="text/javascript"></script>
    <link href="../../App_Scripts/jQueryScripts/css/custom-theme/jquery-ui-1.10.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script src="../../App_Scripts/Common.js" type="text/javascript"></script>
    
    <!--MsDropdown CSS-->
    <link href="../App_Scripts/msdropdown/dd.css" rel="stylesheet" type="text/css" />
    <%--<a href="ReporteConciliacionI.aspx">ReporteConciliacionI.aspx</a>--%>
    <script src="../App_Scripts/msdropdown/js/jquery.dd.js" type="text/javascript"></script>

    <!-- Estilo de AJAX Accordion-->
    <link rel="stylesheet" href="../App_Themes/GasMetropolitanoSkin/Accordion/css/accordion.css" />

    <!-- Script se utiliza para el Scroll del GridView-->
    <link href="../App_Scripts/ScrollGridView/GridviewScroll.css" rel="stylesheet" type="text/css" />
    <script src="../App_Scripts/ScrollGridView/gridviewScroll.min.js" type="text/javascript"></script>
    <script src="../App_Scripts/Common.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        $(document).ready(function () {
            var d = new Date();

            var month = d.getMonth() + 1;
            var day = d.getDate();

            var output = (('' + day).length < 2 ? '0' : '') + day + '/' +
                (('' + month).length < 2 ? '0' : '') + month + '/' +
                d.getFullYear();

            $('#ctl00_contenidoPrincipal_txtFInicial').val(output);
            $('#ctl00_contenidoPrincipal_txtFFinal').val(output);
        });

         function pageLoad() {
            activarDatePickers();
        }

        function activarDatePickers() {
          
            //DatePicker FOperacion
            $('#<%=txtFInicial.ClientID%>').datepicker({
                defaultDate: "+1w", dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                showOn: "button",
                buttonImage: "../App_Themes/GasMetropolitanoSkin/Iconos/AñoMes.png",             
                buttonImageOnly: true,
                onClose: function (selectedDate) {
                    $("#<%=txtFFinal.ClientID%>").datepicker("option", "minDate", selectedDate);
                    $(".ui-datepicker-trigger").css("width", "24px");
                }
            });
            $("#<%=txtFFinal.ClientID%>").datepicker({
                defaultDate: "+1w", dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                showOn: "button",
                buttonImage: "../App_Themes/GasMetropolitanoSkin/Iconos/AñoMes.png",                
                buttonImageOnly: true,                
                onClose: function (selectedDate) {
                    $("#<%=txtFInicial.ClientID%>").datepicker("option", "maxDate", selectedDate);
                    $(".ui-datepicker-trigger").css("width", "24px");
                }
            });
            $(".ui-datepicker-trigger").css("width", "24px");
        }

        function ValidarFechas() {
            try {
                var valido = false;
                var fecha1 = document.getElementById('<%=txtFInicial.ClientID%>').value;
                var fecha2 = document.getElementById('<%=txtFFinal.ClientID%>').value;

                var fechaInicial = ParseDate(fecha1);
                var fechaFinal = ParseDate(fecha2);

                if (fechaInicial.getMonth() === fechaFinal.getMonth() && fechaInicial.getFullYear() === fechaFinal.getFullYear()) {
                    valido = true;
                }
                else {
                    MostrarMensajeError();
                }
                return valido;
            }
            catch (err) {
                MostrarMensajeError();
                return false;
            }
        }

        // Cambiar formato de fecha a 'MM/DD/YYYY'
        function ParseDate(input) {
            var parts = input.match(/(\d+)/g);
            // note parts[1]-1
            return new Date(parts[2], parts[1] - 1, parts[0]);
        }

        function MostrarMensajeError() {
            var mensaje = document.getElementById('<%= dvAlertaError.ClientID %>');
            mensaje.hidden = false;
        }

        function LinkNombreArchivoDescarga() {
            var finicial = document.getElementById("ctl00_contenidoPrincipal_txtFInicial").value;
            var ffinal = document.getElementById("ctl00_contenidoPrincipal_txtFFinal").value;
            var mesini = parseInt(finicial.substr(3, 2));
            var anoini = parseInt(finicial.substr(6, 4));
            var cero;
            if (mesini < 10) {
                cero = "0";
            }
            else {
                cero = "";
            }
            //debe de agregar el nombre del archivo como debe de quedar
            // document.getElementById("LigaDescarga").href = "../InformesExcel/PosicionDiariaGM" + cero + mesini + anoini + ".xlsx";            
            
            let params = (new URL(document.location)).searchParams;
            let Pagina = params.get("Reporte");
            console.log(Pagina);

            if (Pagina == '2') {
           
                document.getElementById("LigaDescarga").href = "../InformesExcel/EdoCta"+'<%= hdfIniEmpresa.Value %>'+ cero + mesini + anoini+".xlsx";
            }
            if(Pagina == '3'){
                document.getElementById("LigaDescarga").href = "../InformesExcel/EdoCtaDia"+'<%= hdfIniEmpresa.Value %>' + cero + mesini + anoini + ".xlsx";

            }
            if (Pagina == '4') {
                document.getElementById("LigaDescarga").href = "../InformesExcel/InternosConciliarFuturo" + '<%= DateTime.Now.ToString("ddMMyyyy")%>' + ".xlsx";

            }

        }
        
    </script>


    <style type="text/css">
        .auto-style4 {
            height: 250px;
            width: 150px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">

     <asp:ScriptManager runat="server" ID="spManager" EnableScriptGlobalization="True"
        AsyncPostBackTimeout="14400">    
     </asp:ScriptManager>
    <script src="../../App_Scripts/jsUpdateProgress.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        var ModalProgress = '<%=mpeLoading.ClientID%>';        
    </script>

    <asp:UpdatePanel runat="server" ID="upInicio" UpdateMode="Always" >
        <ContentTemplate>
            <div runat="server" ID="dvAlertaError" class="alert alert-danger alert-dismissible fade show" hidden="true"
                  style="margin:5px 5px 0px 7px; box-sizing:border-box; font-size:15px">
                <strong>Error: </strong>
                <asp:Label runat="server" ID="lblMensajeError" Text="Debe especificar una fecha inicial y final 
                y las fechas deben corresponder al mismo mes y año, por favor corrija su entrada." />
     </div> 
            <table id="BarraHerramientas" class="bg-color-grisClaro01" style="width: 100%; vertical-align: top;">
                <tr>
                    <td style="padding: 3px 3px 3px 50px; vertical-align: top; width: 1%">                      
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion  bg-color-grisClaro01" rowspan="2" style="width: 100%">
                                    <asp:ImageButton ID="ImageButton1" runat="server"  Height="100px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/ImgInformes.png"
                                        ToolTip="CONSULTAR"   Width="80px" ValidationGroup="Configuracion"
                                         />
                                </td>
                            </tr>
                        </table>
                    </td>

                    <td style="padding: 3px 3px 3px 0px; vertical-align: top; width: 59%">
                        
                        <table class="etiqueta opcionBarra " style="width:30%;">
                            <tr>
                                
                                <td class="" style="width: 6%; text-align:center;font-weight: bold; color:white;text-align:left;padding: 3px 3px 3px 3px;">
                                     Fecha Inicial
                                </td>
                                <td class="lineaVertical" style="width: 16%">
                                    <asp:TextBox runat="server" ID="txtFInicial" CssClass="cajaTexto" Font-Size="10px" Width="65%"></asp:TextBox>                                    
                                    <asp:HiddenField ID="hdfFFinal" runat="server" />                           
                               </td>                             
                            </tr>
                        </table>

                        <table class="etiqueta opcionBarra" style="width:30%;">
                            <tr>                               
                                <td class="" style="width: 6.1%; text-align:center;font-weight: bold; color:white;text-align:left;padding: 3px 3px 3px 3px;">
                                     Fecha Final
                                </td>
                                <td class="lineaVertical" style="width: 16%">
                                    <asp:TextBox runat="server" ID="txtFFinal" CssClass="cajaTexto" Font-Size="10px" Width="65%"></asp:TextBox>
                                    <asp:HiddenField ID="HiddenField2" runat="server" />
                                    <asp:HiddenField ID="hdfIniEmpresa" runat="server" />
                                </td>                               
                             
                            </tr>
                        </table>

                         <table class="etiqueta opcionBarra" style="width:29.9%;">
                            <tr>
                               
                                <td class="" style="width: 5.9%; text-align:center;font-weight: bold; color:white;text-align:left;padding: 3px 3px 3px 3px; ">
                                <p >Banco</p>
                                </td>
                                <td class="" style="width: 16%">
                                    <asp:DropDownList runat="server" ID="btnlista" CssClass="cajaTexto" Font-Size="10px" Width="65%" Height="26px" AutoPostBack="True" OnSelectedIndexChanged="btnlista_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:HiddenField ID="HiddenField3" runat="server" />
                                </td>                               
                             
                            </tr>
                        </table>

                        <table class="etiqueta opcionBarra" style="width:30%;">
                            <caption>
                               
                                <tr>
                                    <td class="" style="width:30%; text-underline-position:below; text-align:center;font-weight: bold; color:white;text-align:left;padding: 3px 3px 3px 3px;">Cuenta Bancaria </td>
                                    <td class="lineaVertical">
                                        <div class="auto-style4" style="width: 100%">
                                            <uc1:WUCListadoCuentasBancarias ID="WUCListadoCuentasBancarias1" runat="server" />
                                        </div>
                                    </td>
                                </tr>
                            </caption>
                          </table>   
                        <asp:Button ID="btnConsultar" Text="CONSULTAR" CssClass="boton fg-color-blanco bg-color-azulClaro"
                             runat="server" OnClick="btnConsultar_Click"   OnClientClick="return ValidarFechas();" />       
                        <a  id="LigaDescarga" onclick="return LinkNombreArchivoDescarga() " ></a>                            
                    </td>           
                </tr>
            </table>

            <table style="width: 100%">
	            <tbody>
		            <tr>
                    <td style="vertical-align: middle; padding: 5px 5px 5px 5px" class="etiqueta centradoJustificado fg-color-blanco bg-color-azulClaro">
                        Estado de Cuenta
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td colspan="2">
                        <div style="width:1200px; height:110px; overflow:auto;">
				<div>
				</div>
                        </div>
                    </td>
                </tr>
	            </tbody>
            </table>

        </ContentTemplate>
    </asp:UpdatePanel>

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

