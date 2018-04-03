<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="PosicionDiariaBancos.aspx.cs" Inherits="ReportesConciliacion_PosicionDiariaBancos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
    Posici&oacute;n Diaria de Bancos
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    
    <!--Libreria jQuery-->
    <script src="../App_Scripts/jQueryScripts/jquery-3.2.1.min.js" type="text/javascript"></script>
    <script src="../App_Scripts/jQueryScripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../App_Scripts/jQueryScripts/jquery.ui.datepicker-es.js" type="text/javascript"></script>
    <link href="../App_Scripts/jQueryScripts/css/custom-theme/jquery-ui-1.10.2.custom.min.css" rel="stylesheet" type="text/css" />
    
    <!--MsDropdown CSS-->
    <link href="../App_Scripts/msdropdown/dd.css" rel="stylesheet" type="text/css" />
    <script src="../App_Scripts/msdropdown/js/jquery.dd.js" type="text/javascript"></script>

    <!-- Estilo de AJAX Accordion-->
    <link rel="stylesheet" href="../App_Themes/GasMetropolitanoSkin/Accordion/css/accordion.css" />

    <!-- Script se utiliza para el Scroll del GridView-->
    <link href="../App_Scripts/ScrollGridView/GridviewScroll.css" rel="stylesheet" type="text/css" />
    <script src="../App_Scripts/ScrollGridView/gridviewScroll.min.js" type="text/javascript"></script>
    <script src="../App_Scripts/Common.js" type="text/javascript"></script>

    <script type="text/javascript">
    
        function pageLoad() {
            activarDatePickers();
        }

        function activarDatePickers() {
            //DatePicker FOperacion
            $("#<%= txtFInicial.ClientID%>").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                onClose: function (selectedDate) {
                    $("#<%=txtFFinal.ClientID%>").datepicker("option", "minDate", selectedDate);
                }
            });
            $("#<%=txtFFinal.ClientID%>").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                onClose: function (selectedDate) {
                    $("#<%=txtFInicial.ClientID%>").datepicker("option", "maxDate", selectedDate);
                }
            });       
        }

        function ValidaFiltro() {
            //debugger;

            if (document.getElementById("ctl00_contenidoPrincipal_txtFInicial").value.trim() == ""
                &&
                document.getElementById("ctl00_contenidoPrincipal_txtFFinal").value.trim() != "") {
                alert("Capture un periodo de fechas valido");
                document.getElementById("ctl00_contenidoPrincipal_txtFInicial").focus();
                return false
            }
            else
                if (document.getElementById("ctl00_contenidoPrincipal_txtFFinal").value.trim() == ""
                    &&
                    document.getElementById("ctl00_contenidoPrincipal_txtFInicial").value.trim() != "") {
                    alert("Capture un periodo de fechas valido");
                    document.getElementById("ctl00_contenidoPrincipal_txtFFinal").focus();
                    return false
                }
                else {
                    var finicial = document.getElementById("ctl00_contenidoPrincipal_txtFInicial").value;
                    var ffinal = document.getElementById("ctl00_contenidoPrincipal_txtFFinal").value;
                    var mesini = parseInt(finicial.substr(3, 2));
                    var mesfin = parseInt(ffinal.substr(3, 2));
                    var anoini = parseInt(finicial.substr(6, 4));
                    var anofin = parseInt(ffinal.substr(6, 4));
                    if (mesini != mesfin || anoini != anofin)
                    {
                        alertify.alert(
                            'Conciliaci&oacute;n bancaria', 'Error: Debe especificar una fecha inicial y final y las fechas deben corresponder al mismo mes y año, por favor corrija su entrada.',
                            function () { alertify.error('Error en la solicitud'); }
                            );
                    }
                    else
                        return true;
                }
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">
 
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="600"
        EnableScriptGlobalization="True">
    </asp:ScriptManager>

            <table id="BarraHerramientas" class="bg-color-grisClaro01" style="width: 100%; vertical-align: top">
                <tr>
                    <td style="padding: 3px 3px 3px 0px; vertical-align: top; width: 1%">                      
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion  bg-color-azulClaro" rowspan="2" style="width: 100%">
                                    <asp:ImageButton ID="btnBuscar" runat="server" Height="25px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/ActualizarConfig.png"
                                        ToolTip="CONSULTAR" Width="25px" ValidationGroup="Configuracion"
                                        OnClientClick="return ValidaFiltro();"
                                        />
                                </td>
                            </tr>
                        </table>
                    </td>

                    <td style="padding: 3px 3px 3px 0px; vertical-align: top; width: 59%">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table class="etiqueta opcionBarra">
                                    <tr>
                                        <td class="lineaVertical" style="width: 30%">
                                        </td>
                                        <td class="lineaVertical" style="width: 10%">
                                             Fecha Inicial
                                        </td>
                                        <td class="lineaVertical" style="width: 20%">
                                            <asp:TextBox runat="server" ID="txtFInicial" CssClass="cajaTexto" Font-Size="10px" Width="85%" ReadOnly="true"></asp:TextBox>
                                            <asp:HiddenField ID="hdfFFinal" runat="server" />
                                        </td>
                                        <td class="lineaVertical" style="width: 15%">
                                            Caja
                                        </td>
                                        <td class="lineaVertical" style="width: 15%">
                                        </td>
                                        <td class="lineaVertical" style="width: 10%">
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        

                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="lineaVertical" style="width: 30%">
                                </td>
                                <td class="lineaVertical" style="width: 10%">
                                     Fecha Final
                                </td>
                                <td class="lineaVertical" style="width: 20%">
                                    <asp:TextBox runat="server" ID="txtFFinal" CssClass="cajaTexto" Font-Size="10px" Width="85%" ReadOnly="true"></asp:TextBox>
                                    <asp:HiddenField ID="HiddenField2" runat="server" />
                                </td>
                                <td class="lineaVertical" style="width: 15%">
                                    <asp:CheckBox ID="CheckBox1" Text="Todos" runat="server" />
                                    <asp:CheckBox ID="CheckBox2" Text="Caja 1" runat="server" />
                                </td>
                                <td class="lineaVertical" style="width: 15%">
                                    <asp:CheckBox ID="CheckBox3" Text="Caja 2" runat="server" />
                                    <asp:CheckBox ID="CheckBox4" Text="Caja 5" runat="server" />
                                </td>
                                <td class="lineaVertical" style="width: 10%">
                                    <asp:CheckBox ID="CheckBox5" Text="Caja 6" runat="server" />
                                </td>
                            </tr>
                        </table>

                    </td>

                   <%-- <td style="padding: 3px 3px 3px 0px; vertical-align: top; width: 1%">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion bg-color-naranja" style="height: 30px">
                                    <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Buscar.png"
                                        ToolTip="BUSCAR" Width="25px" 
                                        />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 1%; padding: 3px 3px 3px 0px; vertical-align: top">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td id="tdExportar" runat="server" class="iconoOpcion bg-color-rojo" style="height: 30px">
                                    <asp:ImageButton ID="imgExportar" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Exportar.png"
                                        ToolTip="EXPORTAR RESULTADOS" Width="25px" 
                                        />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 1%; padding: 3px 3px 3px 0px; vertical-align: top">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion bg-color-azulOscuro" style="height: 30px">
                                    <asp:ImageButton ID="imgGuardar" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Guardar.png"
                                        ToolTip="GUARDAR VISTA" Width="25px" 
                                         />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 1%; padding: 3px 3px 3px 0px; vertical-align: top">
                        <table class="etiqueta opcionBarra">
                            <tr>
                                <td class="iconoOpcion bg-color-grisOscuro" style="height: 30px">
                                    <asp:ImageButton ID="imgCerrarMesConciliacion" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png"
                                        ToolTip="CERRAR MES" Width="25px" 
                                        OnClientClick="return confirm('¿Esta seguro de CERRAR el MES de Conciliaciones.?\nNota: Verificar antes de que no existan CONCILIACIONES abiertas de este mes.')" />
                                </td>
                            </tr>
                        </table>
                    </td>--%>

                </tr>
            </table>

</asp:Content>

