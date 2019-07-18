<%@Page Title="" Language="C#" MasterPageFile="~/Principal.master" 
    AutoEventWireup="true"
    CodeFile="UnoAVarios.aspx.cs" 
    Inherits="Conciliacion_FormasConciliar_UnoAVarios"
    MaintainScrollPositionOnPostback="false" 
    EnableEventValidation="false" 
    Async="true" %>
    
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~//ControlesUsuario/CargaManualExcelCyC/wucCargaManualExcelCyC.ascx" TagPrefix="uc1" TagName="WebUserControl" %>
<%@ Register Src="~/ControlesUsuario/SaldosAFavor/wucSaldoAFavor.ascx" TagPrefix="uc1" TagName="wucSaldoAFavor" %>
<%@ Register Src="~/ControlesUsuario/BuscadorClienteFactura/wucBuscaClientesFacturas.ascx" TagPrefix="uc1" TagName="wucBuscaClientesFacturas" %>
<%@ Register Src="~/ControlesUsuario/ConciliadorPagare/wucConciliadorPagare.ascx" TagPrefix="uc1" TagName="wucConciliadorPagare" %>
<%@ Register Src="~/ControlesUsuario/ClientePago/wucClientePago.ascx" TagPrefix="uc1" TagName="wucClientePago" %>
<%@ Register Src="~/ControlesUsuario/ClienteDatosBancarios/wucClienteDatosBancarios.ascx" TagPrefix="uc1" TagName="wucClienteDatosBancarios" %>
<%@ Register Src="~/ControlesUsuario/BuscadorPagoEstadoCuenta/wucBuscadorPagoEstadoCuenta.ascx" TagPrefix="uc1" TagName="wucBuscadorPagoEstadoCuenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="server">
    UNO A VARIOS</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <!--Libreria jQuery-->

<%--    <script src="../../App_Scripts/jQueryScripts/jquery-3.2.1.min.js" type="text/javascript"></script>
    <script src="../../App_Scripts/jQueryScripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../../App_Scripts/jQueryScripts/css/custom-theme/jquery-ui-1.10.2.custom.css" rel="stylesheet" type="text/css" />
    <script src="../../App_Scripts/Common.js" type="text/javascript"></script>--%>

    <script src="/App_Scripts/jQueryScripts/jquery-3.2.1.min.js" type="text/javascript"></script>
    <script src="/App_Scripts/jQueryScripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="/../../App_Scripts/jQueryScripts/jquery.ui.datepicker-es.js" type="text/javascript"></script>
    <link href="/App_Scripts/jQueryScripts/css/custom-theme/jquery-ui-1.10.2.custom.min.css" rel="stylesheet" type="text/css" />

    <%--    <script src="../../App_Scripts/jQueryScripts/jquery.ui.datepicker-es.js" type="text/javascript"></script>--%>
<%--    <script src="../../App_Scripts/jQueryScripts/jquery.ui.datepicker-es.js"></script>--%>

    <style type="text/css">
        .select-css {
	        display: block;
	        font-size: 8px;
	        font-family: sans-serif;
	        font-weight: 700;
	        color: #000;
	        line-height: 1.3;
	        padding: .6em 1.4em .5em .8em;
	        width: 100px;
	        max-width: 100%; 
	        box-sizing: border-box;
	        margin: 0;
	        border: 1px solid #aaa;
	        box-shadow: 0 1px 0 1px rgba(0,0,0,.04);
	        border-radius: .5em;
	        -moz-appearance: none;
	        -webkit-appearance: none;
	        appearance: none;
	        background-color: #fff;
	        background-image: url('data:image/svg+xml;charset=US-ASCII,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20width%3D%22292.4%22%20height%3D%22292.4%22%3E%3Cpath%20fill%3D%22%23007CB2%22%20d%3D%22M287%2069.4a17.6%2017.6%200%200%200-13-5.4H18.4c-5%200-9.3%201.8-12.9%205.4A17.6%2017.6%200%200%200%200%2082.2c0%205%201.8%209.3%205.4%2012.9l128%20127.9c3.6%203.6%207.8%205.4%2012.8%205.4s9.2-1.8%2012.8-5.4L287%2095c3.5-3.5%205.4-7.8%205.4-12.8%200-5-1.9-9.2-5.5-12.8z%22%2F%3E%3C%2Fsvg%3E'),
	          linear-gradient(to bottom, #ffffff 0%,#00ff00 100%);
	        background-repeat: no-repeat, repeat;
	        background-position: right .7em top 50%, 0 0;
	        background-size: .65em auto, 100%;
        }
        .select-css::-ms-expand {
	        display: none;
        }
        .select-css:hover {
	        border-color: #888;
        }
        .select-css:focus {
	        border-color: #aaa;
	        box-shadow: 0 0 1px 3px rgba(59, 153, 252, .7);
	        box-shadow: 0 0 0 3px -moz-mac-focusring;
	        color: #222; 
	        outline: none;
        }
        .select-css option {
	        font-weight:normal;
        }
        .select-css-rojo {
	        display: block;
	        font-size: 8px;
	        font-family: sans-serif;
	        font-weight: 700;
	        color: #000;
	        line-height: 1.3;
	        padding: .6em 1.4em .5em .8em;
	        width: 100px;
	        max-width: 100%; 
	        box-sizing: border-box;
	        margin: 0;
	        border: 1px solid #aaa;
	        box-shadow: 0 1px 0 1px rgba(0,0,0,.04);
	        border-radius: .5em;
	        -moz-appearance: none;
	        -webkit-appearance: none;
	        appearance: none;
	        background-color: #fff;
	        background-image: url('data:image/svg+xml;charset=US-ASCII,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20width%3D%22292.4%22%20height%3D%22292.4%22%3E%3Cpath%20fill%3D%22%23007CB2%22%20d%3D%22M287%2069.4a17.6%2017.6%200%200%200-13-5.4H18.4c-5%200-9.3%201.8-12.9%205.4A17.6%2017.6%200%200%200%200%2082.2c0%205%201.8%209.3%205.4%2012.9l128%20127.9c3.6%203.6%207.8%205.4%2012.8%205.4s9.2-1.8%2012.8-5.4L287%2095c3.5-3.5%205.4-7.8%205.4-12.8%200-5-1.9-9.2-5.5-12.8z%22%2F%3E%3C%2Fsvg%3E'),
	          linear-gradient(to bottom, #ffffff 0%,#ff0000 100%);
	        background-repeat: no-repeat, repeat;
	        background-position: right .7em top 50%, 0 0;
	        background-size: .65em auto, 100%;
        }
        .select-css::-ms-expand {
	        display: none;
        }
        .select-css:hover {
	        border-color: #888;
        }
        .select-css:focus {
	        border-color: #aaa;
	        box-shadow: 0 0 1px 3px rgba(59, 153, 252, .7);
	        box-shadow: 0 0 0 3px -moz-mac-focusring;
	        color: #222; 
	        outline: none;
        }
        .select-css option {
	        font-weight:normal;
        }

        .button {
          padding: 3px 10px;
          font-size: 12px;
          text-align: center;
          cursor: pointer;
          outline: none;
          color: #fff;
          background-color: #e3a21a;
          border: none;
          border-radius: 5px;
          box-shadow: 0 4px #333333;
        }

        .blue {
          background-color: #2d89ef;
        }

        .button:hover {background-color: #da532c}

        .button:active {
          background-color: #da532c;
          box-shadow: 0 1px #666;
          transform: translateY(4px);
        }
    </style>

    <script type="text/javascript">

        var sumapreconciliadas = 0;
        var TipoCobroSeleccionado = 0;

        $( document ).ready(function() {
            Calendarios();
        });

        function Calendarios() {

             $("#<%= txtAFuturo_FInicioInternos.ClientID%>").datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 1,
                    onClose: function(selectedDate) {
                        $("#<%=txtAFuturo_FFInalInternos.ClientID%>").datepicker("option", "minDate", selectedDate);
                    }
                });
                $("#<%=txtAFuturo_FFInalInternos.ClientID%>").datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 1,
                    onClose: function(selectedDate) {
                        $("#<%=txtAFuturo_FInicioInternos.ClientID%>").datepicker("option", "maxDate", selectedDate);
                    }
                });
            
             $("#<%= txtAFuturo_FInicio.ClientID%>").datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 1,
                    onClose: function(selectedDate) {
                        $("#<%=txtAFuturo_FFInal.ClientID%>").datepicker("option", "minDate", selectedDate);
                    }
                });
                $("#<%=txtAFuturo_FFInal.ClientID%>").datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 1,
                    onClose: function(selectedDate) {
                        $("#<%=txtAFuturo_FInicio.ClientID%>").datepicker("option", "maxDate", selectedDate);
                    }
                });
            
            // DatePickers SALDO A FAVOR
            $("#<%= txtFechaInicioSAF.ClientID%>").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                onClose: function (selectedDate) {
                    $("#<%= txtFechaFinSAF.ClientID%>").datepicker("option", "minDate", selectedDate);
                    //$("#ctl00_contenidoPrincipal_txtFechaFinSAF").datepicker("option", "minDate", selectedDate);
                }
            });
            $("#<%= txtFechaFinSAF.ClientID%>").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                onClose: function (selectedDate) {
                    $("#<%= txtFechaInicioSAF.ClientID%>").datepicker("option", "maxDate", selectedDate);
                    //$("#ctl00_contenidoPrincipal_txtFechaInicioSAF").datepicker("option", "maxDate", selectedDate);
                }
            });

            //DataPicker Rango-Fechas 
            //if (<%= tipoConciliacion %> != 2) {
                //DatePicker FOperacion
                $("#<%= txtFOInicio.ClientID%>").datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 2,
                    onClose: function(selectedDate) {
                        $("#<%=txtFOTermino.ClientID%>").datepicker("option", "minDate", selectedDate);
                    }
                });
                $("#<%=txtFOTermino.ClientID%>").datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 2,
                    onClose: function(selectedDate) {
                        $("#<%=txtFOInicio.ClientID%>").datepicker("option", "maxDate", selectedDate);
                    }
                });
                //DatePicker FMovimiento
                $("#<%= txtFMInicio.ClientID%>").datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 2,
                    onClose: function(selectedDate) {
                        $("#<%=txtFMTermino.ClientID%>").datepicker("option", "minDate", selectedDate);
                    }
                });
                $("#<%=txtFMTermino.ClientID%>").datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 2,
                    onClose: function(selectedDate) {
                        $("#<%=txtFMInicio.ClientID%>").datepicker("option", "maxDate", selectedDate);
                    }
                });
            //} else {
                //DatePicker FSuministro
                $("#<%= txtFSInicio.ClientID%>").datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 2,
                    onClose: function(selectedDate) {
                        $("#<%=txtFSTermino.ClientID%>").datepicker("option", "minDate", selectedDate);
                    }
                });
                $("#<%=txtFSTermino.ClientID%>").datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 2,
                    onClose: function(selectedDate) {
                        $("#<%=txtFSInicio.ClientID%>").datepicker("option", "maxDate", selectedDate);
                    }
                });
        }

        function PageLoad() {
            //debugger;
            activarDatePickers();
            MuestraSaldoAFavor();
            MostrarTxtComisionInicio();
            document.getElementById("divExternos").scrollTop = document.getElementById('ctl00_contenidoPrincipal_hfDivExternosScrollPos').value;
            if (document.getElementById('ctl00_contenidoPrincipal_ddlTiposDeCobro') != null)
                document.getElementById('ctl00_contenidoPrincipal_ddlTiposDeCobro').value = document.getElementById('ctl00_contenidoPrincipal_ddlTiposDeCobro').value;
            gridviewScroll();
        }
          
        $(document).keypress(function (e) {
            if (e.which == 2) {
                console.log("ctrl B");
                var hoy = new Date();
                var dd = String(hoy.getDate()).padStart(2, '0');
                var mm = String(hoy.getMonth()).padStart(2, '0'); 
                var yyyy = hoy.getFullYear();
                document.getElementById("ctl00_contenidoPrincipal_wucBuscadorPagoEstadoCuenta_txtFinicio").value = dd + '/' + mm + '/' + yyyy;
                var mesAnterior = String(hoy.getMonth() + 1).padStart(2, '0'); 
                document.getElementById("ctl00_contenidoPrincipal_wucBuscadorPagoEstadoCuenta_txtFfinal").value = dd + '/' + mesAnterior + '/' + yyyy;
                $('#ctl00_contenidoPrincipal_wucBuscadorPagoEstadoCuenta_grvPagoEstadoCuenta').remove();
                document.getElementById("ctl00_contenidoPrincipal_wucBuscadorPagoEstadoCuenta_txtMonto").value = "0.00";
                document.getElementById("ctl00_contenidoPrincipal_wucBuscadorPagoEstadoCuenta_chkBuscaEnRetiros").checked = false;
                document.getElementById("ctl00_contenidoPrincipal_wucBuscadorPagoEstadoCuenta_chkBuscarEnDepositos").checked = false;
                document.getElementById("ctl00_contenidoPrincipal_wucBuscadorPagoEstadoCuenta_chkBuscarEnEsta").checked = false;
                $find('ModalBehaviorBuscadorPagoEdoCta').show();
            }
        });

        function clickBotonMuestraAFuturo() {
            $('#dvMuestraAFuturo').slideToggle();
            Calendarios();
        }
        function clickBotonMuestraAFuturoInterno() {
            $('#dvMuestraAFuturoInternos').slideToggle();
        }
        function rdbSecuencia_scrollpos() {
            document.getElementById('ctl00_contenidoPrincipal_hfDivExternosScrollPos').value = document.getElementById("divExternos").scrollTop;
        }

        function ValidaSumaPreconciliados(sumapreconciliadas, dSeleccion, dAbono, dDiferencia) {
            dResto = parseFloat(document.getElementById('ctl00_contenidoPrincipal_lblResto').innerHTML.replace(',', '').replace('$', '').trim());
            if (dResto > 0)
                return true;
            else
                return false;
        }

        function chkSeleccionaTodosPedido() {
            //debugger;
            if (document.getElementById('ctl00_contenidoPrincipal_grvPedidos') != null) {
                grv = document.getElementById('ctl00_contenidoPrincipal_grvPedidos');
                chkVal = document.getElementById('ctl00_contenidoPrincipal_chkSeleccionarInternosTodos').checked;
                for (indice = 1; indice < grv.rows.length; indice++) {
                    if (grv.rows[indice].cells[1] != undefined) {
                        grv.rows[indice].cells[1].children[0].checked = chkVal;
                        res = btnAgregarPedidoConciliacion(grv, indice);
                        if (res == false) {
                            document.getElementById('ctl00_contenidoPrincipal_grvPedidos').rows[indice].cells[1].children[0].checked = false;
                            break;
                        }

                    }
                }
            }
            if (document.getElementById('ctl00_contenidoPrincipal_grvInternos') != null) {
                grv = document.getElementById('ctl00_contenidoPrincipal_grvInternos');
                chkVal = document.getElementById('ctl00_contenidoPrincipal_chkSeleccionarInternosTodos').checked;
                for (indice = 1; indice < grv.rows.length; indice++) {
                    if (grv.rows[indice].cells[1] != undefined)
                        grv.rows[indice].cells[1].children[0].checked = chkVal;
                }
            }
        }

        function btnAgregarPedidoConciliacion(grid, fila) {
            //debugger;
            dResto = parseFloat(document.getElementById('ctl00_contenidoPrincipal_lblResto').innerHTML);
            //var total = parseFloat(0).toFixed(2);
            //var dRespaldoAbono = parseFloat(0.0);
            //var comisionSeleccionada = 0;
            var dComision = 0;
            if (document.getElementById('<%= chkComision.ClientID %>') != null) {
                comisionSeleccionada = document.getElementById('<%= chkComision.ClientID %>').checked;
                dComision = parseFloat(document.getElementById('<%= txtComision.ClientID %>').value);
                dComision = (isNaN(dComision) ? 0 : dComision);
            }

            //debugger;
            //var dAcumulado = parseFloat(document.getElementById('ctl00_contenidoPrincipal_lblMontoAcumuladoInterno').innerHTML.replace(',', '').replace('$', '').trim());
            var dAgregados = 0.0;
            if (document.getElementById('ctl00_contenidoPrincipal_grvAgregadosPedidos') != null) {
                grvPreCon = document.getElementById('ctl00_contenidoPrincipal_grvAgregadosPedidos');
                for (i = 0; i < grvPreCon.rows.length; i++) {
                    if (grvPreCon.rows[i].cells[5] != undefined)
                        dAgregados = parseFloat(dAgregados) + parseFloat(grvPreCon.rows[i].cells[5].innerText.replace(',', '').replace('$','').trim());
                }
            }

            var dChequeados = 0.0;
            grv = document.getElementById('ctl00_contenidoPrincipal_grvPedidos');
            for (i = 1; i < grv.rows.length; i++) {
                if (grv.rows[i].cells[1] != undefined && grv.rows[i].cells[1].children[0].checked == true)
                    dChequeados = parseFloat(dChequeados) + parseFloat(grv.rows[i].cells[3].innerText.replace(',', '').replace('$','').trim());
            }

            var dResto = 0.0;
            sumapreconciliadas = dAgregados; //+ dChequeados;
            if (parseFloat(dChequeados) > 0.01)
            {    
                
                //if (dChequeados < sumapreconciliadas) {                    
                //    sumapreconciliadas = dChequeados;
                //}
                var strAbono = document.getElementById('ctl00_contenidoPrincipal_lblAbono').innerHTML.replace('$', '').trim();
                do
                    strAbono = strAbono.replace(',', '');
                while (strAbono.search(',') > -1);
                var dAbono = parseFloat(strAbono);
                var dSeleccion = parseFloat(grv.rows[fila].cells[3].innerText.replace(',', '').replace('$', '').trim());

                //        0                           595.40                     34.87
                //    34.87                           861.04                     34.87
                if (document.getElementById('ctl00_contenidoPrincipal_txtDiferencia').innerText.trim() == "")
                    dDiferencia = 0;
                else
                    dDiferencia = parseFloat(document.getElementById('ctl00_contenidoPrincipal_txtDiferencia').innerText.trim());
                if (ValidaSumaPreconciliados(sumapreconciliadas, dSeleccion, dAbono, dDiferencia) == true || grv.rows[fila].cells[1].children[0].checked == false)
                {
                    sumapreconciliadas = parseFloat(sumapreconciliadas) + parseFloat(dChequeados);    
                    if (dAbono > 0)
                        dResto      = parseFloat(dAbono - sumapreconciliadas);
                    if (dResto <= 0)
                        dResto      = 0;
                    document.getElementById('ctl00_contenidoPrincipal_lblResto').innerHTML = currencyFormat(dResto); //dResto.toFixed(2);
                    document.getElementById('ctl00_contenidoPrincipal_lblMontoAcumuladoInterno').innerHTML = currencyFormat(sumapreconciliadas); //sumapreconciliadas.toFixed(2);
                    return true; 
                }
                else
                {
                    if (grid.parentNode.parentNode.children[fila] != null)
                        (grid.parentNode.parentNode.children[fila].children[0]).checked = false;
                    alert('El total acumulado es mayor a monto del pedido.');
                    return false;
                }
            }
            else
            {
                //No se seleccionó nada
                document.getElementById('ctl00_contenidoPrincipal_lblResto').innerHTML = document.getElementById('ctl00_contenidoPrincipal_lblAbono').innerHTML;
                document.getElementById('ctl00_contenidoPrincipal_lblMontoAcumuladoInterno').innerHTML = currencyFormat(dAgregados); //dAgregados.toFixed(2);
                sumapreconciliadas = 0.0;
            }

        }

        function currencyFormat(num) {
            return '$' + num.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,')
        }

        var dAbonoSel;
        function ActualizaMonto(){
            //debugger;
            var dAbono = 0;
            var dComision = 0;
            var chkComisionActivado = document.getElementById('<%= chkComision.ClientID %>').checked;
            if (document.getElementById('<%= txtComision.ClientID %>').value == "")
                dComision = 0;
            else
            if (chkComisionActivado)
                dComision = parseFloat(document.getElementById('<%= txtComision.ClientID %>').value);
            else
                dComision = 0;

            dAbono = parseFloat(document.getElementById('<%= hdfAbonoSeleccionado.ClientID %>').value);
            dAbono = parseFloat(dAbono) + parseFloat(dComision);

            //PENDIENTE ACTUALIZAR VARIABLE SUMAPRE CONCILIADAS
            var sumapreconciliadas = parseFloat(document.getElementById('ctl00_contenidoPrincipal_lblMontoAcumuladoInterno').innerHTML.replace(',', '').replace('$', '').trim());
            var dResto = 0;
            
            document.getElementById('ctl00_contenidoPrincipal_lblAbono').innerHTML = currencyFormat(dAbono); //dAbono.toFixed(2);
            if (dAbono > 0)
                dResto      = parseFloat(dAbono - sumapreconciliadas);
            if (dResto <= 0)
                dResto      = 0;
            document.getElementById('ctl00_contenidoPrincipal_lblResto').innerHTML = currencyFormat(dResto); //dResto.toFixed(2);            

        }

        function MuestraSaldoAFavor() {
            //if ( $('#hfMuestraSeccionSaldoAFavor')[0].value == 1 ) {
            if ($('#<%= hfMuestraSeccionSaldoAFavor.ClientID %>').val() == "1") {
                $('#configuracionInternosPedidos').hide();
                $('#seccionGridPedidos').hide();
                $('#seccionFiltrosSaldoAFavor').show();
                <%--console.log('Mostrar');
                console.log($('#<%= hfMuestraSeccionSaldoAFavor.ClientID %>').val());--%>
            }
            else {
                $('#seccionFiltrosSaldoAFavor').hide();
                $('#configuracionInternosPedidos').show();
                $('#seccionGridPedidos').show();
                <%--console.log('Ocultar');
                console.log($('#<%= hfMuestraSeccionSaldoAFavor.ClientID %>').val());--%>
            }
        }

        function clickBotonMuestraSaldoAFavor(Tipo)
        {            
            //if ($('#hfMuestraSeccionSaldoAFavor')[0].value == 0) {
            if ( $('#<%= hfMuestraSeccionSaldoAFavor.ClientID %>').val() == "0" ) {
                $('#configuracionInternosPedidos').hide(500);
                $('#seccionFiltrosSaldoAFavor').show(500);
                //$('#seccionSaldoAFavor').show(500);
                $('#seccionGridPedidos').hide(500);                
                //$('#hfMuestraSeccionSaldoAFavor').val('1');
                $('#<%= hfMuestraSeccionSaldoAFavor.ClientID %>').val("1");
                $('#<%= hfActivaPagoOSaldo.ClientID %>').val(Tipo);
                $('#btnMuestraSaldoAFavor').css("background-color", "yellow");
                $('#btnMuestraSaldoAFavor').html('Ocultar');
                
                console.log($('#<%= hfActivaPagoOSaldo.ClientID %>').val());
            }
            else {
                $('#configuracionInternosPedidos').show(500);
                $('#seccionFiltrosSaldoAFavor').hide(500);
                //$('#seccionSaldoAFavor').hide(500);
                $('#seccionGridPedidos').show(500);
                //$('#hfMuestraSeccionSaldoAFavor').val('0');
                $('#<%= hfMuestraSeccionSaldoAFavor.ClientID %>').val("0");
                $('#btnMuestraSaldoAFavor').css("background-color", "green");
                $('#btnMuestraSaldoAFavor').html('Mostrar');
            }
            
        }

        /**
         * Muestra el TextBox una vez que se oculta después del Postback
         */
        function MostrarTxtComisionInicio() {
            if ($('#<%= hfTxtComisionVisible.ClientID %>').val() == "1") {
                $('#<%= txtComision.ClientID %>').show();
            }
        }

        function MostrarTxtComision() {
            if ($('#<%= hfTxtComisionVisible.ClientID %>').val() == "0") {
                $('#<%= hfTxtComisionVisible.ClientID %>').val('1');
                $('#<%= txtComision.ClientID %>').show(250);

            }
            else {
                $('#<%= hfTxtComisionVisible.ClientID %>').val('0');
                $('#<%= txtComision.ClientID %>').hide(250);
            }
        }

        //Funcion para mostrar el calendar
        function datapicker_modal(fDiaMin, fMesMin, fAñoMin, fDiaMax, fMesMax, fAñoMax) {
            var cadenaMin = fDiaMin+'/'+fMesMin+'/'+fAñoMin;
            var cadenaMax = fDiaMax+'/'+fMesMax+'/'+fAñoMax;
            $("#<%=txtFechaAplicacion.ClientID%>").datepicker({
                dateFormat: 'dd/mm/yy',
                changeYear: true,
                changeMonth: true,
                minDate: cadenaMin,
                maxDate: cadenaMax
            });
        }

        function registroElegido(Registro) {
            try {
                var tabla = document.getElementById('ctl00_contenidoPrincipal_wucSaldoAFavor_grvSaldosAFavor');
                console.log(tabla);
                var filas = tabla.rows;
                console.log(filas);
                var fila = tabla.rows[Registro + 1];
                console.log(fila);
                console.log(fila.cells[9].innerHTML);

                var acumulado = parseFloat(0).toFixed(2);
                acumulado = parseFloat($("#cellResto")[0].innerHTML.replace('<span id="ctl00_contenidoPrincipal_wucSaldoAFavor_lblResto">', '').replace('</span>', '').replace('NaN', '').replace('Resto:', '').replace('$','').trim()).toFixed(2);

                var elegido = fila.cells[0].innerHTML;
                
                if (elegido)
                {
                    var nuevoMonto = parseFloat(acumulado) + parseFloat(fila.cells[9].innerHTML);
                }
                else
                {
                    var nuevoMonto = parseFloat(acumulado) - parseFloat(fila.cells[9].innerHTML);
                }
                
                $("#cellResto").hide(500);
                $("#cellResto")[0].innerHTML = "Resto: " + nuevoMonto;
                $("#cellResto").show(500);
                $("#cellResto")[0].style.backgroundColor = "yellow"; 
            }
            catch (err)
            {
                console.log('Error en función JS registroElegido: ' + err);
            }
        }

        /*function pageLoad() {
            //gridviewScroll();
            // Script se utiliza para llamar a  la funcion de jQuery desplegable
            $("#btnMostrarAgregados").click(function () {
                $("#dvAgregados").slideToggle();
            });
            activarDatePickers();
            //SAF_DatePickers();
        }*/

        function OcultarPopUpConciliacionManual() {
            $find("mpeCargaArchivo").hide();
        }

        function OcultarPopUpConciliarPagares() {
            $find("mpeConciliarPagares").hide();
        }

        function OcultarPopUpClientePago() {
            btnClientePagoCancelar_Click();
            $find("ModalBehaviorClientePago").hide();
        }

        function OcultarPopUpBuscadorPagoEdoCta() {
            $find("ModalBehaviorBuscadorPagoEdoCta").hide();
        }

        function OcultarPopUpClienteDatosBancarios() {
            $find("ModalBehavior").hide();
        }
        
        function popUpVisible() {
            $('#<%= hdfVisibleCargaArchivo.ClientID %>').val("1");
        }

        function popUpNoVisible() {
            $('#<%= hdfVisibleCargaArchivo.ClientID %>').val("0");
        }
        
        
        /*              Botones del control wucCargaManualExcel         */
        function btnCargaManualCancelar_Click(){
            $('#<%= hdfCargaAgregado.ClientID %>').val("0");
            popUpNoVisible();
        }
        function btnCargaManualAceptar_Click(){
            $("#<%= hdfCargaAgregado.ClientID %>").val("1");
            popUpNoVisible();
        }

        /*              Botones del control wucConciliadorPagare         */
        function btnConciliadorPagareCancelar_Click() {
            OcultarPopUpConciliarPagares();
        }

        /*              Botones del control wucClientePago         */
        function btnClientePagoAceptar_Click() {
            $("#<%= hdfClientePagoAceptar.ClientID %>").val("1");
        }

        /*              Botones del control wucClientePago         */
        function btnClientePagoCancelar_Click() {
            $("#<%= hdfClientePagoCancelar.ClientID %>").val("1");
        }
        
        function activarDatePickers() {
            console.writeline('ejectuando la funcion de los calendatios');


        }

        function ConfirmarSaldoAFavor() {
            //debugger;
            var numAgregados = document.getElementById('ctl00_contenidoPrincipal_lblAgregadosInternos').innerHTML
            if (numAgregados === "0" || numAgregados  === "undefined") {
                alert("No se ha agregado ninguna referencia interna.");
                return false;
            }
            else {                
                var MontoSAF = document.getElementById('ctl00_contenidoPrincipal_lblResto').innerHTML;
                var ValorParametro = document.getElementById('ctl00_contenidoPrincipal_hdfSaldoAFavor').value;
                console.log(parseFloat(MontoSAF.replace('$', '').replace(',', '').trim()).toFixed(2));
                console.log(parseFloat(ValorParametro).toFixed(2));
                console.log(parseFloat(MontoSAF.replace('$', '').trim()).toFixed(2) >= parseFloat(ValorParametro).toFixed(2));
                if ($('#<%= hdfEsPedido.ClientID %>').val() == "1") {
                    if (parseFloat(MontoSAF.replace('$', '').replace(',', '').trim()).toFixed(2) >= parseFloat(ValorParametro).toFixed(2)) {
                        var r = confirm('El monto depositado genera un saldo a favor por ' + MontoSAF + '\n¿Desea generar el saldo a favor?');
                        if (r == true) {
                            document.getElementById('ctl00_contenidoPrincipal_hdfAceptaAplicarSaldoAFavor').value = 'Aceptado';
                        }
                        else {
                            document.getElementById('ctl00_contenidoPrincipal_hdfAceptaAplicarSaldoAFavor').value = 'Rechazado';
                            if ($('#<%= hdfEsPedido.ClientID %>').val() == "1") {
                                $('#<%= hdfCambiarEstatusPedido.ClientID %>').val("1");
                                alert("Se guardará el saldo restante para conciliación a futuro.");
                            }
                        }
                    }
                }
                return true;
            }
        }

        function PedidoMultipleSI() {
            alertify.success('Seleccionó: Pre-conciliar.');
        }

        function PedidoMultipleNO() {
            if ($('#<%= hdfEsPedido.ClientID %>').val() == "1") {
                if (document.getElementById('ctl00_contenidoPrincipal_grvAgregadosPedidos') != null && $('#<%= hdfPedidoMultipleSeleccionado.ClientID %>').val() != "") {
                    grvPreCon = document.getElementById('ctl00_contenidoPrincipal_grvAgregadosPedidos');
                    for (i = 0; i < grvPreCon.rows.length; i++) {
                        if (grvPreCon.rows[i].cells[1].innerText.trim() == $('#<%= hdfPedidoMultipleSeleccionado.ClientID %>').val()) {
                            grvPreCon.rows[i].cells[0].childNodes['1'].click();
                            break;
                        }
                    }
                }
                $('#<%= hdfPedidoMultipleSeleccionado.ClientID %>').val("");
                alertify.error('Seleccionó: No pre-conciliar.');
            }
        }

        function gridviewScroll() {
            $('#<%=grvExternos.ClientID%>').gridviewScroll({
                width: 595,
                height: 389,
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
                    height: 300,
                    freezesize: 0,
                    arrowsize: 30,
                    varrowtopimg: '../../App_Scripts/ScrollGridView/Images/arrowvt.png',
                    varrowbottomimg: '../../App_Scripts/ScrollGridView/Images/arrowvb.png',
                    harrowleftimg: '../../App_Scripts/ScrollGridView/Images/arrowhl.png',
                    harrowrightimg: '../../App_Scripts/ScrollGridView/Images/arrowhr.png',
                    headerrowcount: 1,
                    <%--startVertical: $("#<%=hfInternosSV.ClientID%>").val(), 
                    startHorizontal: $("#<%=hfInternosSH.ClientID%>").val(), 
                    onScrollVertical: function (delta) { $("#<%=hfInternosSV.ClientID%>").val(delta); }, 
                    onScrollHorizontal: function (delta) { $("#<%=hfInternosSH.ClientID%>").val(delta);}--%>
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
            <%--$('#<%=grvPedidos.ClientID%>').gridviewScroll({
                    width: 595,
                    height: 250,
                    freezesize: 0,
                    arrowsize: 30,
                    varrowtopimg: '../../App_Scripts/ScrollGridView/Images/arrowvt.png',
                    varrowbottomimg: '../../App_Scripts/ScrollGridView/Images/arrowvb.png',
                    harrowleftimg: '../../App_Scripts/ScrollGridView/Images/arrowhl.png',
                    harrowrightimg: '../../App_Scripts/ScrollGridView/Images/arrowhr.png',
                    headerrowcount: 1,--%>

                    <%--startVertical: $("#<%=hfInternosSV.ClientID%>").val(), 
                    startHorizontal: $("#<%=hfInternosSH.ClientID%>").val(), 
                    onScrollVertical: function (delta) { $("#<%=hfInternosSV.ClientID%>").val(delta); }, 
                    onScrollHorizontal: function (delta) { $("#<%=hfInternosSH.ClientID%>").val(delta);}--%>
            <%--
            });
            --%>
        }
        function mensajeAsincrono(faltante) {
            var pre = document.createElement('pre');pre.style.maxHeight = '400px';
                pre.style.margin = '0';
                pre.style.padding = '24px';
                pre.style.whiteSpace = 'pre-wrap';
                pre.style.textAlign = 'justify';
            pre.appendChild(document.createTextNode('No fue posible encontrar información para ' + faltante + ' clientes de la solicitud ¿desea reintentar?')); alertify.confirm('Conciliaci&oacute;n bancaria',pre, function () {  __doPostBack('miPostBack', "1"); }, function () { __doPostBack('miPostBack',"2");}).set({ labels: { ok: 'Si', cancel: 'No' }, padding: false });
        }
    </script>
    <!-- Validar: numeros, moneda y alfanuméricos -->
    <script type="text/javascript">
        function ValidNum(e) {
            var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
            return ((tecla > 47 && tecla < 58));
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
<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" runat="server">
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

        
        function ShowModalPopupCargar(operacion) {
            if (operacion == 'M') {
                var varBuscar=document.getElementById("<%=txtBuscar.ClientID%>");
                $find("mpeBuscar").show();
                varBuscar.value = "";
            } else {
                $find("mpeBuscar").hide();
            }
        }

            </script>
    <script type="text/javascript" language="javascript">
        var ModalProgress = '<%=mpeLoading.ClientID%>';        
    </script>

    <asp:UpdatePanel runat="server" ID="upBarraEstado" UpdateMode="Always">
        <ContentTemplate>

            <!-- Controles INDICES -->
            <asp:HiddenField ID="hdfIndiceExterno" runat="server" />
            <asp:HiddenField ID="hdfIndiceInterno" runat="server" />

            <asp:HiddenField ID="hdfEsPedido" runat="server" />
            <asp:HiddenField ID="hdfCambiarEstatusPedido" runat="server" />

            <asp:HiddenField ID="hdfUltimoBotonPresionado" runat="server" />
            
            <asp:HiddenField ID="hdfPedidoMultipleSeleccionado" runat="server" />

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
                                        ToolTip="CONSULTAR FORMA AUTOMATICA" Width="25px" OnClick="imgAutomatica_Click" />
                                   
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
                                        OnClick="btnActualizarConfig_Click" ToolTip="ACTUALIZAR CONFIGURACION" Width="25px" />
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
                                        Style="margin-bottom: 3px; margin-right: 3px" Width="115px"
                                        Visible="False">
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
                                            Display="Dynamic" EnableClientScript="True" Font-Size="10px" Type="Integer" ValidationGroup="UnoVarios"></asp:RangeValidator>
                                        <asp:RangeValidator ID="rvDiferencia" runat="server" ControlToValidate="txtDiferencia"
                                            CssClass="etiqueta fg-color-rojo" Display="Dynamic" EnableClientScript="True"
                                            Font-Size="10px" Type="Double" ValidationGroup="UnoVarios" />
                                        <asp:RequiredFieldValidator ID="rfvDiasVacio" runat="server" ControlToValidate="txtDias"
                                            CssClass="etiqueta fg-color-rojo" Display="Dynamic" EnableClientScript="True"
                                            ErrorMessage="Especifique los dias. " Font-Size="10px" ValidationGroup="UnoVarios"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvDiferenciaVacio" runat="server" ControlToValidate="txtDiferencia"
                                            CssClass="etiqueta fg-color-rojo" Display="Dynamic" EnableClientScript="True"
                                            ErrorMessage="Especifique la diferencia. " ValidationGroup="UnoVarios" Font-Size="10px"></asp:RequiredFieldValidator>
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
                                        ToolTip="IMPORTAR" Width="25px" OnClick="imgImportar_Click"/>
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
                    <td style="width: 50%; vertical-align: top; padding: 5px 5px 5px 5px" class="etiqueta fg-color-blanco bg-color-azulClaro">
                        Transacciones Conciliadas
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel runat="server" ID="upGrvConciliadas" UpdateMode="Always">
                            <ContentTemplate>
                                <div style="width:1200px; height:230px; overflow:auto;">
                                    <asp:GridView ID="grvConciliadas" runat="server" AutoGenerateColumns="False" 
                                    AllowPaging='<%# ActivePaging %>' PageSize="5"
                                    Width="100%" CssClass="grvResultadoConsultaCss" ShowHeaderWhenEmpty="True"
                                    OnPageIndexChanging="grvConciliadas_PageIndexChanging" OnRowDataBound="grvConciliadas_RowDataBound"
                                    DataKeyNames="CorporativoConciliacion,SucursalConciliacion,AñoConciliacion,MesConciliacion,FolioConciliacion,FolioExterno,SecuenciaExterno,Pedido"
                                    OnRowCommand="grvConciliadas_RowCommand" OnSelectedIndexChanging="grvConciliadas_SelectedIndexChanging"
                                    OnRowCreated="grvConciliadas_RowCreated" AllowSorting="True" OnSorting="grvConciliadas_Sorting">
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="No se ha conciliado ninguna transacción."></asp:Label>
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
                                        <asp:TemplateField HeaderText="$Conciliado" SortExpression="MontoConciliado">
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
                                                    Width="20.5cm" Style="padding: 5px 5px 5px 5px" BackColor="White" Wrap="True">
                                                    <asp:Label ID="lblToolTipConceptoExt" runat="server" Text='<%# resaltarBusqueda(Eval("Concepto").ToString()) %>'
                                                        CssClass="etiqueta " Width="20cm" Font-Size="10px" />
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
                                        <asp:TemplateField HeaderText="Factura" SortExpression="Seriefactura">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label runat="server" ID="lblSerieFactura" Text='<%# resaltarBusqueda(Eval("Seriefactura").ToString()) %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>                            
                                        <asp:TemplateField HeaderText="Cliente" SortExpression="ClienteReferencia">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label runat="server" ID="lblCliente" Text='<%# resaltarBusqueda(Eval("ClienteReferencia").ToString()) %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                       <asp:TemplateField HeaderText="Pedido" SortExpression="Pedido">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label runat="server" ID="lblPedido" Text='<%# resaltarBusqueda(Eval("Pedido").ToString()) %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>                           
                                       <asp:TemplateField HeaderText="TipoCobro" SortExpression="TipoCobro">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label runat="server" ID="lblTipoCobro" Text='<%# resaltarBusqueda(Eval("TipoCobro").ToString()) %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>                           

                                       <asp:TemplateField Visible="false" HeaderText="TipoCobroImg" SortExpression="TipoCobroImg">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Image ID="imgTipoCobro" runat="server" ImageUrl="" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>                           

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button runat="server" ID="imgDesconciliar" CssClass="Desconciliar centradoMedio boton"
                                                    ToolTip="DESCONCILIAR" Width="20px" Height="20px" OnClientClick='<%# "return confirm(\"¿Esta seguro de DESCONCILIAR la Transacción: " + Eval("FolioExterno").ToString() + ": Secuencia: "+ Eval("SecuenciaExterno").ToString() + "?¡Se actualizará la conciliacion y su detalle! ?\");" %>'
                                                    CommandName="DESCONCILIAR" />
                                                <asp:Button runat="server" ID="imgDetalleConciliado" CssClass="Detalle centradoMedio boton"
                                                    ToolTip="VER DETALLE" Width="20px" Height="20px" CommandName="Select" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="90px"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center" Width="90px"></HeaderStyle>
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
                                </div>
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
                        <table style="width: 100%;" class="grids">
                            <tr>
                                <td class="bg-color-grisOscuro fg-color-blanco" style="width: 15%">Total Externo
                                </td>
                                <td class="bg-color-verdeClaro" style="width: 15%">
                                    <b>
                                        <asp:Label ID="lblMontoTotalExterno" runat="server" CssClass="etiqueta fg-color-blanco"></asp:Label></b>
                                </td>

                                <td class="icono bg-color-grisClaro02 fg-color-amarillo" style="width: 1%">
                                    <%--123--%>
                                    <input type="button" name="btnMuestraAFuturo" value="Futuro" class="button blue" onclick="clickBotonMuestraAFuturo();"
                                         runat="server" ID="btnMuestraAFuturo"/>
                                </td>

                                <td class="bg-color-grisClaro02" style="width: 60%">
                                    <b>Archivos Externos</b>
                                </td>
                                <td class="bg-color-azulClaro" style="width: 5%">
                                    <asp:Image ID="imgGuardarParcial" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Exito.png"
                                        CssClass="icono" Width="25px"></asp:Image>
                                </td>
                            </tr>
                        </table>
                    </td>

                    <td rowspan="3" style="width: 1%"></td>
                    <td style="width: 50%;" class="centradoMedio">
                        <table style="width: 100%;" class="grids">
                            <tr>
                                <td class="bg-color-grisOscuro fg-color-blanco" style="width: 15%" id="tdEtiquetaMontoIn"
                                    runat="server">Total Interno
                                </td>
                                <td class="bg-color-amarillo" style="width: 15%" id="tdMontoIn" runat="server">
                                    <b>
                                        <asp:Label ID="lblMontoTotalInterno" runat="server" CssClass="etiqueta fg-color-negro"></asp:Label></b>
                                </td>

                                <td class="icono bg-color-grisClaro02 fg-color-amarillo" style="width: 1%">
                                    <%--123--%>
                                    <input type="button" name="btnMuestraAFuturoInterno" value="Futuro" class="button blue" onclick="clickBotonMuestraAFuturoInterno();"
                                         runat="server" ID="btnMuestraAFuturoInterno"/>
                                </td>

                                <td class="bg-color-grisClaro02" style="width: 64%">
                                    <b>
                                        <asp:Label ID="lblArchivosInternos" Text="Archivos Internos" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblPedidos" Text="Pedidos" runat="server" Visible="false"></asp:Label>
                                    </b>
                                </td>
                                <td class="icono bg-color-grisClaro02 fg-color-amarillo" style="width: 1%">
                                    <input type="hidden" id="hfActivaPagoOSaldo" value="" runat="server"/>
                                    <input type="button" name="btnMuestraPagare" value="Pagaré" class="button blue" onclick="clickBotonMuestraSaldoAFavor('PAGARE');"
                                        style="visibility:hidden" runat="server" ID="btnMuestraPagareID"/>
                                </td>
                                <td class="icono bg-color-grisClaro02 fg-color-amarillo" style="width: 1%">
                                    <input type="hidden" id="hfMuestraSeccionSaldoAFavor" value="0" runat="server"/>
                                    <input type="button" name="btnMuestraSaldoAFavor" value="Saldo a favor" class="button" onclick="clickBotonMuestraSaldoAFavor('SALDO');"
                                        style="visibility:hidden" runat="server" ID="btnMuestraSaldoAFavorID"/>
                                </td>
                                <td class="icono bg-color-grisClaro02 fg-color-amarillo" style="width: 1%">
                                    <asp:ImageButton ID="imgPagare" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Pagare.png" 
                                        ToolTip="CONCILIAR PAGARE" Width="25px" Height="25px" OnClick="imgPagare_Click" Visible="false"/>
                                </td>
                                <td class="icono bg-color-grisClaro02 fg-color-amarillo" style="width: 1%">
                                    <asp:ImageButton ID="imgCargar" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/FormatosExp/EXCEL.png"
                                        ToolTip="CARGAR ARCHIVO" Width="25px" Height="25px" OnClick="imgCargar_Click"  OnClientClick="popUpVisible();"
                                        Visible="false"></asp:ImageButton>
                                </td>
                                <td class="bg-color-grisClaro fg-color-amarillo" style="width: 5%">
                                    <asp:Image ID="imgInt" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Exito.png"
                                        CssClass="icono" Width="25px" Height="25px"></asp:Image>
                                </td>
                            </tr>
                        </table>
                    </td>

                </tr>
                <tr>
                    <td style="vertical-align: top">
                        <div id="configuracionExternos" class="bg-color-grisClaro">
                            
                            <div id="dvMuestraAFuturo" style="display: none">
                                <div style="width:450px; height:40px; overflow:auto;">
                                <table width="100%">
                                    <tr>
                                        <td rowspan="2" style="vertical-align: top; width: 12.5%;">
                                            <asp:Label ID="Label1" runat="server" CssClass="etiqueta fg-color-blanco centradoMedio"
                                                Text=" Fecha"></asp:Label>
                                        </td>
                                        <td style="width: 12.5%;">
                                            <asp:TextBox ID="txtAFuturo_FInicio" runat="server" CssClass="cajaTextoPequeño" ToolTip="F Inicio"
                                                ValidationGroup="vgFOperacion" Width="80px"></asp:TextBox>
                                            <asp:TextBox ID="txtAFuturo_FFInal" runat="server" CssClass="cajaTextoPequeño" ToolTip="F Final"
                                                Width="80px"></asp:TextBox>
                                        </td>
                                        <td rowspan="2" style="vertical-align: top; width: 12.5%;">
                                            <asp:ImageButton ID="btnFiltraAFuturo" runat="server" CssClass="icono bg-color-azulClaro"
                                                Height="25px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Buscar.png"
                                                OnClick="btnFiltraAFuturo_Click" ToolTip="FILTRAR Transacciones a Futuro" 
                                                Width="25px" />
                                        </td>
                                    </tr>
                                </table>
                                </div>
                            </div>
                            
                            <table width="100%">
                                <tr>
                                    <td class="centradoJustificado" style="width: 15%;">
                                        <asp:CheckBox ID="chkReferenciaEx" runat="server" Text="Documento" CssClass="etiqueta fg-color-blanco centradoMedio"
                                            AutoPostBack="True" OnCheckedChanged="chkReferenciaEx_CheckedChanged" />
                                    </td>
                                    <td class="centradoJustificado" style="width: 30%;">
                                        <asp:RadioButtonList ID="rdbVerDepositoRetiro" runat="server" CssClass="etiqueta fg-color-blanco"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="DEPOSITOS">DEPOSITOS</asp:ListItem>
                                            <asp:ListItem Value="RETIROS">RETIROS</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="centradoDerecha" style="width: 15%;">
                                        <asp:ImageButton runat="server" ID="btnENPROCESOEXTERNO" ToolTip="EN PROCESO" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Conciliar.png"
                                            CssClass="icono bg-color-verdeClaro" OnClick="btnENPROCESOEXTERNO_Click" />
                                        <asp:ImageButton runat="server" ID="btnCANCELAREXTERNO" ToolTip="CANCELAR" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/NoConciliar.png"
                                            CssClass="icono bg-color-grisOscuro" OnClick="btnCANCELAREXTERNO_Click" />
                                    </td>
                                    <%--Nuevo--%>
                                    <td class="centradoDerecha; bg-color-naranjaOscuro" style="width: 11%;">
                                        <div class="floatDerecha">
                                            <table id="Table1" runat="server">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton runat="server" ID="ImageButton1" ToolTip="TRASPASO ENTRE CUENTAS"
                                                            ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Traspaso.png" CssClass="icono"
                                                            OnClick="btnAgregar_Click" />
                                                    </td>
                                                    <td class="icono etiqueta fg-color-blanco">TRASPASO
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                    <td class="centradoDerecha" style="width: 45%;">
                                        <div class="floatDerecha">
                                            <table id="statusGridExternos" runat="server">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton runat="server" ID="btnHistorialPendientesExterno" ToolTip="VER EXTERNOS CANCELADOS"
                                                            ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Pendientes.png" CssClass="icono"
                                                            OnClick="btnHistorialPendientesExterno_Click" />
                                                        <asp:ImageButton runat="server" ID="btnRegresarExterno" ToolTip="REGRESAR" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/RegresarIn.png"
                                                            CssClass="icono" OnClick="btnRegresarExterno_Click" />
                                                    </td>
                                                    <td class="icono etiqueta fg-color-blanco">EXTERNOS
                                                        <asp:Label runat="server" ID="lblStatusGridExternos"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:HiddenField ID="hdfExternosControl" runat="server" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div id="tipoCobros" class="bg-color-grisClaro">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTiposdeCobro" Text="Tipos de Cobro" runat="server" CssClass="etiqueta fg-color-blanco" ToolTip="Seleccione el tipo de cobro para asignarlo al guardar"></asp:Label>
                                        <asp:DropDownList ID="ddlTiposDeCobro" runat="server" AutoPostBack="False"
                                            CssClass="etiqueta dropDownPequeño" Style="margin-bottom: 3px; margin-right: 3px"
                                            Width="150px"
                                            ToolTip="Seleccione el tipo de cobro para asignarlo al guardar"
                                            >
                                            </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div id="divExternos" style="width:600px; height:350px; overflow:auto;" onscroll="rdbSecuencia_scrollpos();" >
                            <asp:GridView ID="grvExternos" runat="server" 
                                AllowPaging='<%# ActivePaging %>' PageSize="10" 
                                AutoGenerateColumns="False" ViewStateMode="Enabled"
                                OnRowDataBound ="grvExternos_RowDataBound" ShowHeaderWhenEmpty="True" Width="100%"
                                AllowSorting="True" CssClass="grvResultadoConsultaCss"
                            DataKeyNames="Corporativo,Sucursal,Año,Secuencia,Folio,StatusConciliacion,Referencia,Deposito"
                            OnSorting="grvExternos_Sorting" OnPageIndexChanging="grvExternos_PageIndexChanging">
                                <HeaderStyle HorizontalAlign="Center" />
                                <RowStyle CssClass="bg-color-blanco fg-color-negro" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkExterno" />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:CheckBox runat="server" ID="chkTodosExternos" AutoPostBack="True" OnCheckedChanged="OnCheckedChangedExternos" />
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" BackColor="#ebecec"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Secuencia" SortExpression="Secuencia">
                                        <ItemTemplate>
                                            <asp:RadioButton ID="rdbSecuencia" runat="server" GroupName="GrupoExternos" AutoPostBack="True"
                                                Text='<%# resaltarBusqueda(Eval("Secuencia").ToString()) %>' OnCheckedChanged="rdbSecuencia_CheckedChanged" />
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
                                    <asp:TemplateField HeaderText="Documento" SortExpression="Referencia">
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
                                <PagerStyle CssClass="grvPaginacionScroll" />
                                <SelectedRowStyle BackColor="#66CCFF" ForeColor="Black" />
                            </asp:GridView>
                        </div>


                        <asp:HiddenField ID="hfExternosSV" runat="server" />
                        <asp:HiddenField ID="hfExternosSH" runat="server" />
                        <asp:HiddenField ID="hfDivExternosScrollPos" runat="server" />
                        <asp:HiddenField ID="hfTipoCobroSeleccionado" runat="server" />
                    </td>
                    <td style="vertical-align: top" colspan="2">
                        <div id="configuracionInternosPedidos" class="bg-color-grisClaro">
<%--123--%>
                           <div id="dvMuestraAFuturoInternos" style="display: none">
                                <div style="width:450px; height:40px; overflow:auto;">
                                    <table width="100%">
                                        <tr>
                                                <td rowspan="2" style="vertical-align: top; width: 12.5%;">
                                                    <asp:Label ID="Label2" runat="server" CssClass="etiqueta fg-color-blanco centradoMedio"
                                                        Text=" Fecha"></asp:Label>
                                                </td>
                                                <td style="width: 12.5%;">
                                                    <asp:TextBox ID="txtAFuturo_FInicioInternos" runat="server" CssClass="cajaTextoPequeño" ToolTip="F Inicio"
                                                        ValidationGroup="vgFOperacion" Width="80px"></asp:TextBox>
                                                    <asp:TextBox ID="txtAFuturo_FFInalInternos" runat="server" CssClass="cajaTextoPequeño" ToolTip="F Final"
                                                        Width="80px"></asp:TextBox>
                                                </td>
                                                <td rowspan="2" style="vertical-align: top; width: 12.5%;">
                                                    <asp:ImageButton ID="btnFiltraAFuturoInterno" runat="server" CssClass="icono bg-color-azulClaro"
                                                        Height="25px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Buscar.png"
                                                        OnClick="btnFiltraAFuturoInterno_Click" ToolTip="FILTRAR Transacciones a Futuro" 
                                                        Width="25px" />
                                                </td>

                                        </tr>
                                    </table>
                                </div>
                            </div>

                            <table width="100%">
                                <tr>
                                    <td style="width: 15%" class="centradoJustificado">
                                        <asp:CheckBox ID="chkReferenciaIn" runat="server" Text="Documento" CssClass="etiqueta fg-color-blanco"
                                            ToolTip="COMPARAR REFERENCIA" AutoPostBack="True" OnCheckedChanged="chkReferenciaIn_CheckedChanged" />
                                    </td>
                                    <td style="width: 5%" class="etiqueta fg-color-blanco">
                                        <asp:Label runat="server" ID="lblVer" Text="Ver:" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 23%">
                                        <asp:RadioButtonList ID="rdbTodosMenoresIn" runat="server" RepeatDirection="Horizontal"
                                            Visible="False" CssClass="etiqueta fg-color-blanco" AutoPostBack="True" OnSelectedIndexChanged="rdbTodosMenoresIn_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="MENORES">Menores</asp:ListItem>
                                            <asp:ListItem Value="TODOS">Todos</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <%--    Controles busqueda pedidos  --%>    <!-- RM 04_05_2018 -->
                                    <td class="lineaVertical" id="tdPedidosLinea" runat="server" style="padding-left:7px"></td>
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
                                        <asp:RequiredFieldValidator ID="rfTxtBusquedaPedidos" ControlToValidate="txtBusquedaPedidos" 
                                            ValidationGroup="vgTxtBusquedaPedidos" runat="server"/>
                                    </td>
                                    <td class="centradoDerecha">
                                        <asp:ImageButton ID="imbBusquedaPedidos" ToolTip="Buscar pedidos" CssClass="icono bg-color-verdeClaro" 
                                            ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Buscar.png" Height="25px" Width="25px"
                                            style="margin-left:2px" Visible="false" OnClick="imbBusquedaPedidos_Click" 
                                            ValidationGroup="vgTxtBusquedaPedidos" runat="server"/>
                                    </td>
                                    <%--    Fin controles busqueda pedidos  --%>

                                    <td class="centradoDerecha" style="width: 10%;">
                                        <asp:ImageButton runat="server" ID="btnENPROCESOINTERNO" ToolTip="EN PROCESO" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Conciliar.png"
                                            CssClass="icono bg-color-verdeClaro" OnClick="btnENPROCESOINTERNO_Click" />
                                        <asp:ImageButton runat="server" ID="btnCANCELARINTERNO" ToolTip="CANCELAR" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/NoConciliar.png"
                                            CssClass="icono bg-color-grisOscuro" OnClick="btnCANCELARINTERNO_Click" />
                                    </td>
                                    <td class="centradoDerecha" style="width: 30%;">
                                        <div class="floatDerecha">
                                            <table id="statusGridInternos" runat="server">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton runat="server" ID="btnHistorialPendientesInterno" ToolTip="VER INTERNOS CANCELADOS"
                                                            ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Pendientes.png" CssClass="icono"
                                                            OnClick="btnHistorialPendientesInterno_Click" />
                                                        <asp:ImageButton runat="server" ID="btnRegresarInterno" ToolTip="REGRESAR INTERNOS PENDIENTES"
                                                            ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/RegresarEx.png" CssClass="icono"
                                                            OnClick="btnRegresarInterno_Click" />
                                                    </td>
                                                    <td class="icono etiqueta fg-color-blanco">
                                                        <asp:Label ID="lblGridAP" runat="server"></asp:Label>
                                                        <asp:Label runat="server" ID="lblStatusGridInternos"></asp:Label>
                                                        <asp:HiddenField ID="hdfInternosControl" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
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
                                        <asp:Label ID="lblPedidoDirecto" runat="server" CssClass="etiqueta fg-color-blanco centradoMedio"
                                            Text="Documento"></asp:Label>
                                    </td>
                                    <td style="width: 12.5%;">
                                        <asp:TextBox ID="txtFMInicio" runat="server" CssClass="cajaTextoPequeño" ToolTip="FMov Inicio"
                                            Width="80px"></asp:TextBox>
                                        <asp:TextBox ID="txtPedido" runat="server" CssClass="cajaTextoPequeño" ToolTip="Pedido"
                                            Width="190px" ValidationGroup="vgAgregarPedidoDirecto"></asp:TextBox>
                                        <%--<asp:TextBoxWatermarkExtender ID="txtWMFMInicio" runat="server" TargetControlID="txtFMInicio"
                                            WatermarkText="FMov Inicio" WatermarkCssClass="cajaTextoPequeño marcaAgua">
                                        </asp:TextBoxWatermarkExtender>--%>
                                    </td>
                                    <td style="width: 12.5%;">
                                        <asp:TextBox ID="txtFMTermino" runat="server" CssClass="cajaTextoPequeño" ToolTip="FMov Fin"
                                            Width="80px"></asp:TextBox>
                                        <%-- <asp:TextBoxWatermarkExtender ID="txtWMFMTermino" runat="server" TargetControlID="txtFMTermino"
                                            WatermarkText="FMov Termino" WatermarkCssClass="cajaTextoPequeño marcaAgua">
                                        </asp:TextBoxWatermarkExtender>--%>
                                    </td>
                                    <td rowspan="2" style="vertical-align: top; width: 12.5%;">
                                        <asp:ImageButton ID="btnRangoFechasFM" runat="server" CssClass="icono bg-color-azulClaro"
                                            Height="25px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Filtrar.png"
                                            OnClick="btnRangoFechasFM_Click" ToolTip="FILTRAR FMovimiento" ValidationGroup="vgFMovimiento"
                                            Width="25px"  />
                                        <asp:ImageButton ID="btnAgregarPedidoDirecto" runat="server" CssClass="icono bg-color-verdeClaro"
                                            Height="25px" Width="25px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Agregar.png"
                                            ToolTip="FILTRAR FMovimiento" ValidationGroup="vgAgregarPedidoDirecto"
                                            OnClick="btnAgregarPedidoDirecto_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:RangeValidator ID="rvFOInicio" runat="server" ControlToValidate="txtFOInicio"
                                            CssClass="etiqueta fg-color-amarillo" Display="Dynamic" ErrorMessage="Por favor insertar una fecha valida"
                                            MaximumValue="28/12/9999" MinimumValue="28/12/1000" Type="Date" ValidationGroup="vgFOperacion"
                                            Font-Size="10px"></asp:RangeValidator>
                                        <asp:RangeValidator ID="rvFOTermino" runat="server" ControlToValidate="txtFOTermino"
                                            CssClass="etiqueta fg-color-amarillo" Display="Dynamic" ErrorMessage="Por favor insertar una fecha valida"
                                            MaximumValue="28/12/9999" MinimumValue="28/12/1000" Type="Date" ValidationGroup="vgFOperacion"
                                            Font-Size="10px"></asp:RangeValidator>
                                        <asp:RangeValidator ID="rvFSInicio" runat="server" ControlToValidate="txtFSInicio"
                                            CssClass="etiqueta fg-color-amarillo" Display="Dynamic" ErrorMessage="Por favor insertar una fecha valida"
                                            MaximumValue="28/12/9999" MinimumValue="28/12/1000" Type="Date" ValidationGroup="vgFSuministro"
                                            Font-Size="10px"></asp:RangeValidator>
                                        <asp:RangeValidator ID="rvFSTermino" runat="server" ControlToValidate="txtFSTermino"
                                            CssClass="etiqueta fg-color-amarillo" Display="Dynamic" ErrorMessage="Por favor insertar una fecha valida"
                                            MaximumValue="28/12/9999" MinimumValue="28/12/1000" Type="Date" ValidationGroup="vgFSuministro"
                                            Font-Size="10px"></asp:RangeValidator>
                                    </td>
                                    <td colspan="2">
                                        <asp:RangeValidator ID="rvFMInicio" runat="server" ControlToValidate="txtFMInicio"
                                            CssClass="etiqueta fg-color-amarillo" Display="Dynamic" ErrorMessage="Por favor insertar una fecha valida"
                                            MaximumValue="28/12/9999" MinimumValue="28/12/1000" Type="Date" ValidationGroup="vgFMovimiento"
                                            Font-Size="10px"></asp:RangeValidator>
                                        <asp:RangeValidator ID="rvFMTermino" runat="server" ControlToValidate="txtFMTermino"
                                            CssClass="etiqueta fg-color-amarillo" Display="Dynamic" ErrorMessage="Por favor insertar una fecha valida"
                                            MaximumValue="28/12/9999" MinimumValue="28/12/1000" Type="Date" ValidationGroup="vgFMovimiento"
                                            Font-Size="10px"></asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPedido" runat="server" ControlToValidate="txtPedido"
                                            CssClass="etiqueta fg-color-amarillo" Display="Dynamic" EnableClientScript="True"
                                            ErrorMessage="Especifique el pedido. " Font-Size="10px" ValidationGroup="vgAgregarPedidoDirecto"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revPedido" runat="server" ControlToValidate="txtPedido"
                                            ErrorMessage="Por favor solo numeros" Font-Size="10px" CssClass="etiqueta fg-color-amarillo"
                                            ValidationExpression="^\d+$" ValidationGroup="vgAgregarPedidoDirecto" Display="Dynamic"
                                            EnableClientScript="True"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </table>
							<div class="lineaHorizontal">
                            </div>

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 25%;">
                                                <uc1:wucBuscaClientesFacturas runat="server" ID="wucBuscaClientesFacturas" />
                                            </td>
                                            <td style="width: 1%;">
                                                <asp:ImageButton ID="btnFiltraCliente" runat="server" CssClass="icono bg-color-verdeClaro"
                                                    Height="25px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Buscar.png"
                                                    ToolTip="Buscar cliente" Width="25px"
                                                    OnClick="btnFiltraCliente_Click"/>
                                            </td>
                                            <td class="centradoIzquierda" style="width: 1%; padding-left:3px">
                                                <asp:Label ID="LabelPedidoReferencia" runat="server" Text="PedidoReferencia" CssClass="etiqueta fg-color-blanco"></asp:Label>
                                            </td>
                                            <td class="centradoIzquierda" style="width: 1%;">
                                                <asp:TextBox ID="txtPedidoReferencia" runat="server" Width="70px" CssClass="cajaTextoPequeño"></asp:TextBox>
                                            </td>
                                            <td class="centradoIzquierda" style="width: 1%;">
                                                <asp:ImageButton ID="btnFiltraPedidoReferencia" runat="server" CssClass="icono bg-color-verdeClaro"
                                                    Height="25px" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Buscar.png"
                                                    ToolTip="Buscar pedido referencia" Width="25px"
                                                    OnClick="btnFiltraPedidoReferencia_Click" />
                                            </td>
                                            <%--    Comisiones  --%>
                                            <td style="width:1%; white-space:nowrap; height:27px; padding-left:1px"
                                                runat="server" class="centradoIzquierda">
                                                <asp:CheckBox ID="chkComision" Text="Comisión:" CssClass="etiqueta fg-color-blanco" runat="server"
                                                    style="margin-left:3px;" Visible="false" onclick="MostrarTxtComision();"
                                                    onchange="ActualizaMonto();"/>
                                            </td>
                                            <td class="centradoIzquierda" style="width:1%;">
                                                <asp:TextBox ID="txtComision" runat="server" Width="70px" CssClass="cajaTextoPequeño" 
                                                    style="margin-left:3px; display:none;" 
                                                    onkeypress="return ValidNumDecimal(event);" 
                                                    onchange="ActualizaMonto();" />
                                                <asp:HiddenField ID="hfTxtComisionVisible" runat="server" Value="0"/>
                                            </td>
                                            <%--    Fin comisiones  --%>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%" class="centradoJustificado">
                                                <asp:CheckBox ID="chkSeleccionarInternosTodos" runat="server" Text="Selecciona Todos" CssClass="etiqueta fg-color-blanco"
                                                    ToolTip="SELECCIONA TODOS LOS INTERNOS"
                                                    OnClick="chkSeleccionaTodosPedido();" />
                                            </td>
                                        </tr>
                                    </table>		
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        

                        <div id="seccionFiltrosSaldoAFavor" style="display:none; padding:3px;" class="etiqueta centradoJustificado fg-color-blanco bg-color-azulClaro">
                            <table style="width: 100%;" >
                                <tr>
                                    <td>
                                        FECHA DE SALDOS
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td id="ColConciliada">Conciliada</td>
                                    <td>Cliente</td>
                                    <td>Monto</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="cbTodos" runat="server" Text="Todos"/>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtFechaInicioSAF" CssClass="cajaTexto" ToolTip="Fecha inicio" ValidationGroup="vgFecha" Width="70px" Font-Size="11px"/>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtFechaFinSAF" CssClass="cajaTexto" ToolTip="Fecha fin" ValidationGroup="vgFecha" Width="70px" Font-Size="11px"/>
                                    </td>
                                    <td id="ColConciliada1">
                                        <asp:DropDownList ID="ddStatusConciliacion" runat="server"></asp:DropDownList>
                                    </td>      
                                    <td>
                                        <asp:TextBox ID="txtClienteSAF" runat="server" Width="90px"
                                            CssClass="cajaTexto" Font-Size="11px"></asp:TextBox><%-- onkeypress="return ValidaNumero(event)"--%>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMontoSAF" runat="server" Width="90px" 
                                            CssClass="cajaTexto" Font-Size="11px"></asp:TextBox><%--onkeypress="return ValidaMoneda(event)"--%>
                                    </td>  
                                    <td>            
                                        <div class="bg-color-naranja fg-color-blanco"> <%--bg-color-grisClaro fg-color-amarillo--%>
                                            <asp:ImageButton ID="imgBuscaSaldoAFavor" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Buscar.png"
                                                ToolTip="Buscar saldos a favor" Width="30px" Height="30px" style="padding: 5px 5px 5px 5px;" 
                                                ValidationGroup="vgFecha, vgMoneda, vgEntero" OnClick="imgBuscaSaldoAFavor_Click"
                                                CausesValidation="true"/>
                                        </div>
                                    </td>       
                                </tr>
                                <tr>
                                    <td class="auto-style1"><asp:CheckBox ID="cbMontosIguales" runat="server" Text="Montos iguales"/></td>
                                    <td class="auto-style1"></td>
                                    <td class="auto-style1"><asp:Label ID="lblMontoConciliar" runat="server"/>Monto a conciliar:</td>
                                    <td id="ColConciliada2" class="auto-style1"></td>
                                    <%--<td id="cellResto" class="auto-style1"><asp:Label ID="Label1" runat="server"/></td>--%>
                                    <td>
                                        <asp:CompareValidator id="cvCliente" runat="server" 
                                            ControlToValidate="txtClienteSAF" 
                                            Operator="DataTypeCheck"
                                            Type="Integer" ErrorMessage="Formato incorrecto" ValidationGroup="vgEntero" Font-Size="12px"/>
                                    </td>
                                    <td>
                                        <asp:CompareValidator id="cvMonto" runat="server" 
                                            ControlToValidate="txtMontoSAF" 
                                            Operator="DataTypeCheck"
                                            Type="Currency" ErrorMessage="Formato incorrecto" ValidationGroup="vgMoneda" Font-Size="12px"/>
                                    </td>
                                    <td class="auto-style1"></td>
                                    <%--<td class="auto-style1"></td>--%>
                                </tr>
                                <tr>
                                    <td colspan="7"></td>
                                </tr>
                            </table>
                        </div>


                        <table style="width: 100%;" class="lineaVertical bg-color-grisClaro01">
                            <tr>
                                <td class="etiqueta lineaVertical centradoMedio" style="width: 1%; padding: 5px 5px 5px 5px">
                                    <img src="../../App_Themes/GasMetropolitanoSkin/Imagenes/grid.png" id="btnMostrarAgregados"
                                        alt="MOSTRAR AGREGADOS" class="icono bg-color-blanco" style="width: 25px; height: 25px; cursor: pointer"
                                        title="MOSTRAR AGREGADOS" onclick="$('#dvAgregados').slideToggle();" />
                                </td>
                                <td class="etiqueta lineaVertical centradoMedio" style="width: 15%; padding: 5px 5px 5px 5px">Agregados:
                                </td>
                                <td class="etiqueta lineaVertical centradoMedio bg-color-grisOscuro fg-color-blanco"
                                    style="width: 10%; padding: 5px 5px 5px 5px">
                                    <asp:Label runat="server" ID="lblAgregadosInternos" Text="0"></asp:Label>
                                </td>
                                <td class="etiqueta lineaVertical centradoMedio" style="width: 10%; padding: 5px 5px 5px 5px">Acumulado:
                                </td>
                                <td class="etiqueta lineaVertical centradoMedio bg-color-grisOscuro fg-color-blanco"
                                    style="width: 15%; padding: 5px 5px 5px 5px">
                                    <asp:Label runat="server" ID="lblMontoAcumuladoInterno" Text="$ 0.00"></asp:Label>
                                </td>
                                <td class="etiqueta lineaVertical centradoMedio" style="width: 10%; padding: 5px 5px 5px 5px">Abono:
                                </td>
                                <asp:HiddenField ID="hdfAbonoSeleccionado" runat="server" />
                                <td class="etiqueta lineaVertical centradoMedio bg-color-azul fg-color-blanco" style="width: 15%; padding: 5px 5px 5px 5px">
                                    <asp:Label runat="server" ID="lblAbono" Text="$ 0.00"></asp:Label>
                                </td>
                                <td class="etiqueta lineaVertical centradoMedio" style="width: 10%; padding: 5px 5px 5px 5px">Resto:
                                </td>
                                <td class="etiqueta lineaVertical centradoMedio bg-color-purpura fg-color-blanco" style="width: 15%; padding: 5px 5px 5px 5px">
                                    <asp:Label runat="server" ID="lblResto" Text="$ 0.00"></asp:Label>
                                </td>
                                <td class="etiqueta centradoMedio" style="width: 15%;">
                                    <asp:Button runat="server" ID="btnGuardarUnoAVarios" CssClass="boton bg-color-azulOscuro fg-color-blanco"
                                        Text="GUARDAR" Style="margin: 0 0 0 0;" ToolTip="GUARDAR" OnClick="btnGuardarUnoAVarios_Click" 
                                        OnClientClick="return ConfirmarSaldoAFavor();"/>
                                </td>
                            </tr>
                        </table>
                        
                        <div id="dvAgregados" style="display: none">
                            <div style="width:600px; height:100px; overflow:auto;">
                                <asp:GridView ID="grvAgregadosPedidos" runat="server" AutoGenerateColumns="False"
                                    AllowPaging='<%# ActivePaging %>' PageSize="5" 
                                    ShowHeader="False" Width="100%" CssClass="grvResultadoConsultaCss" ShowHeaderWhenEmpty="False"
                                    ShowFooter="False" DataKeyNames="Celula,Pedido,AñoPed,Cliente" ViewStateMode="Enabled"
                                    OnRowDataBound="grvAgregadosPedidos_RowDataBound" 
                                    OnRowCreated="grvAgregadosPedidos_RowCreated" 
                                    OnPageIndexChanging="grvAgregadosPedidos_PageIndexChanging">
                                <HeaderStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button runat="server" ID="btnQuitarInterno" CssClass="Quitar centradoMedio boton"
                                                ToolTip="QUITAR" Width="20px" Height="20px" OnClick="btnQuitarPedidoInterno_Click" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Width="10px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pedido" SortExpression="pedido">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPedido" runat="server" Text='<%# Bind("Pedido") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="centradoMedio" />
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cliente" SortExpression="cliente">
                                        <ItemTemplate>
                                            <div class="parrafoTexto" style="width: 250px">
                                                <asp:Label ID="lblClientePed" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                                            </div>
                                            <asp:HoverMenuExtender ID="hmeClientePed" runat="server" TargetControlID="lblClientePed"
                                                PopupControlID="pnlPopUpClientePed" PopDelay="20" OffsetX="-30" OffsetY="-10">
                                            </asp:HoverMenuExtender>
                                            <asp:Panel ID="pnlPopUpClientePed" runat="server" CssClass="grvResultadoConsultaCss ocultar"
                                                Width="250px" Style="padding: 5px 5px 5px 5px" BackColor="White" Wrap="True">
                                                <asp:Label ID="lblToolTipClientePed" runat="server" Text='<%# Eval("Nombre") %>'
                                                    CssClass="etiqueta " Font-Size="10px" />
                                            </asp:Panel>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Justify" Width="250px"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Width="250px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FMovimiento" SortExpression="fMovimiento">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFMovimiento" runat="server" Text='<%# Bind("FMovimiento","{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="centradoMedio" />
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FOperacion" SortExpression="fOperacion">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFOperacion" runat="server" Text='<%# Bind("FOperacion","{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="centradoMedio" />
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Monto<br/>Pedido" SortExpression="montoInterno">
                                        <ItemTemplate>
                                            <b>
                                                <asp:Label ID="lblMontoPedido" runat="server" Text='<%# Bind("Monto","{0:c2}") %>'></asp:Label></b>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Concepto<br/>Pedido" SortExpression="conceptoPedido">
                                        <ItemTemplate>
                                            <div class="parrafoTexto" style="width: 100px">
                                                <asp:Label ID="lblConceptoPed" runat="server" Text='<%# Eval("Concepto") %>'></asp:Label>
                                            </div>
                                            <asp:HoverMenuExtender ID="hmeConceptoPed" runat="server" TargetControlID="lblConceptoPed"
                                                PopupControlID="pnlPopUpConceptoPed" PopDelay="20" OffsetX="-40" OffsetY="-10">
                                            </asp:HoverMenuExtender>
                                            <asp:Panel ID="pnlPopUpConceptoPed" runat="server" CssClass="grvResultadoConsultaCss ocultar"
                                                Width="130px" Style="padding: 5px 5px 5px 5px" BackColor="White" Wrap="True">
                                                <asp:Label ID="lblToolTipConceptoPed" runat="server" Text='<%# Eval("Concepto") %>'
                                                    CssClass="etiqueta " Font-Size="10px" />
                                            </asp:Panel>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:TemplateField>
                                </Columns>
                                </asp:GridView>
    						    <asp:GridView ID="grvAgregadosInternos" runat="server" AutoGenerateColumns="False"
                                    AllowPaging='<%# ActivePaging %>' PageSize="5" ShowHeader="True" Width="100%" CssClass="grvResultadoConsultaCss"
                                    ShowHeaderWhenEmpty="False" ShowFooter="False" DataKeyNames="Folio,Secuencia,Año,Sucursal"
                                    OnRowCreated="grvAgregadosInternos_RowCreated"
                                    OnPageIndexChanging="grvAgregadosInternos_PageIndexChanging" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button runat="server" ID="btnQuitarArchivoInterno" CssClass="Quitar centradoMedio boton"
                                                    ToolTip="QUITAR" Width="20px" Height="20px" OnClick="btnQuitarArchivoInterno_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center" Width="10px"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Secuencia" SortExpression="secuenciaInt">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSecuenciaInt" runat="server" Text='<%# Eval("Secuencia").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle CssClass="centradoMedio" />
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Folio" SortExpression="folioInterno">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFolioInterno" runat="server" Text='<%# Eval("Folio").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle CssClass="centradoMedio" />
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FMovimiento" SortExpression="fMovimiento">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFMovimiento" runat="server" Text='<%# Eval("FMovimiento","{0:d}").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle CssClass="centradoMedio" />
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FOperacion" SortExpression="fOperacion">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFOperacion" runat="server" Text='<%# Eval("FOperacion","{0:d}").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle CssClass="centradoMedio" />
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Concepto" SortExpression="concepto">
                                            <ItemTemplate>
                                                <div class="parrafoTexto" style="width: 100px">
                                                    <asp:Label ID="lblConceptoInt" runat="server" Text='<%# resaltarBusqueda(Eval("Concepto").ToString()) %>'></asp:Label>
                                                </div>
                                                <asp:HoverMenuExtender ID="hmeConcepto" runat="server" TargetControlID="lblConceptoInt"
                                                    PopupControlID="pnlPopUpConceptoInt" PopDelay="20" OffsetX="-40" OffsetY="-10">
                                                </asp:HoverMenuExtender>
                                                <asp:Panel ID="pnlPopUpConceptoInt" runat="server" CssClass="grvResultadoConsultaCss ocultar"
                                                    Width="130px" Style="padding: 5px 5px 5px 5px" BackColor="White" Wrap="True">
                                                    <asp:Label ID="lblToolTipConceptoInt" runat="server" Text='<%# resaltarBusqueda(Eval("Concepto").ToString()) %>'
                                                        CssClass="etiqueta " Font-Size="10px" />
                                                </asp:Panel>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Justify" Width="100px"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Monto" SortExpression="monto">
                                            <ItemTemplate>
                                                <b>
                                                    <asp:Label ID="lblMonto" runat="server" Text='<%# Eval("Monto","{0:c2}").ToString() %>'></asp:Label></b>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerTemplate>
                                        Página
                                        <asp:DropDownList ID="paginasDropDownListAgregadosInternos" Font-Size="12px" AutoPostBack="true"
                                            runat="server" OnSelectedIndexChanged="paginasDropDownListAgregadosInternos_SelectedIndexChanged"
                                            CssClass="dropDownPequeño" Width="60px">
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
                                    </PagerTemplate>
                                    <PagerStyle CssClass="estiloPaginacion bg-color-grisClaro fg-color-blanco" />
                                </asp:GridView>
                            </div>
                        </div>
                        <div style="width:588px; height:250px; overflow:auto;">
                            <asp:GridView ID="grvInternos" runat="server" AutoGenerateColumns="False" ShowHeader="True"
                                AllowPaging='<%# ActivePaging %>' PageSize="3" CssClass="grvResultadoConsultaCss" AllowSorting="True" OnRowDataBound="grvInternos_RowDataBound"
                                ShowHeaderWhenEmpty="False" ShowFooter="False" Width="100%" DataKeyNames="Secuencia, Folio, Sucursal"
                                OnSorting="grvInternos_Sorting" OnPageIndexChanging="grvInternos_PageIndexChanging">
                                <HeaderStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button runat="server" ID="btnAgregarArchivo" CssClass="Agregar centradoMedio boton"
                                                ToolTip="AGREGAR" Width="20px" Height="20px" OnClick="btnAgregarArchivo_Click" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="30px" BackColor="#ebecec"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkInterno" />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:CheckBox runat="server" ID="chkTodosInternos" Visible="false"  AutoPostBack="True" OnCheckedChanged="OnCheckedChangedInternos" />
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" BackColor="#ebecec"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Secuencia" SortExpression="Secuencia">
                                        <ItemTemplate>
                                            <asp:RadioButton ID="rdbSecuenciaIn" runat="server" GroupName="GrupoArchivosIn" AutoPostBack="True"
                                                Text='<%# resaltarBusqueda(Eval("Secuencia").ToString()) %>' OnCheckedChanged="rdbSecuenciaIn_CheckedChanged" />
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
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
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
                                    <asp:TemplateField HeaderText="Documento" SortExpression="Referencia">
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
                                    <asp:TemplateField HeaderText="Cliente" SortExpression="Cliente">
                                        <ItemTemplate>
                                            <div class="parrafoTexto">
                                                <asp:Label ID="lblCliente" runat="server" Text='<%# resaltarBusqueda(Eval("Cliente").ToString()) %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Factura" SortExpression="SerieFactura">
                                        <ItemTemplate>
                                            <div class="parrafoTexto">
                                                <asp:Label runat="server" ID="lblSerieFactura" Text='<%# resaltarBusqueda(Eval("SerieFactura").ToString()) %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cliente" SortExpression="ClienteReferencia">
                                        <ItemTemplate>
                                            <div class="parrafoTexto">
                                                <asp:Label runat="server" ID="lblClienteReferencia" Text='<%# resaltarBusqueda(Eval("ClienteReferencia").ToString()) %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="grvPaginacionScroll" />
                            </asp:GridView>
                            <%-- <br />--%>
                            <div id="seccionGridPedidos">
                                <asp:GridView ID="grvPedidos" runat="server" 
                                    AllowPaging='<%# ActivePaging %>' PageSize="3" AutoGenerateColumns="False" ShowHeader="True"
                                    CssClass="grvResultadoConsultaCss" AllowSorting="True" ShowFooter="False" Width="100%"
                                    ShowHeaderWhenEmpty="True" 
                                    DataKeyNames="Celula,Pedido,AñoPed,Cliente"
                                    OnSorting="grvPedidos_Sorting"
                                    OnRowCommand="grvPedidos_RowCommand" 
                                    OnPageIndexChanging="grvPedidos_PageIndexChanging"> 
                                <HeaderStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnAgregarPedido" runat="server" CssClass="Agregar" Height="20px" 
                                                OnClick="btnAgregarPedido_Click"
                                                ToolTip="AGREGAR" Width="20px"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                CommandName="AgregarPedidoAConciliacion" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="25px" BackColor="#ebecec"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Width="25px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkPedido" OnClick="return btnAgregarPedidoConciliacion(this,this.parentNode.parentNode.rowIndex);" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" BackColor="#ebecec"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FSuministro" SortExpression="FSuministro">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFSuministro" runat="server" Text='<%# Eval("FSuministro","{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="centradoMedio" />
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Monto" SortExpression="Total">
                                        <ItemTemplate>
                                            <b><asp:Label ID="lblMontoPedido" runat="server" Text='<%# Eval("Total","{0:C}") %>'></asp:Label></b>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nom. Cliente" SortExpression="Nombre">
                                        <ItemTemplate>
                                                <asp:Label ID="lblCliente" runat="server" Text='<%# Eval("Nombre") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Concepto" SortExpression="Concepto">
                                        <ItemTemplate>
                                                <asp:Label ID="lblConcepto" runat="server" Text='<%# Eval("Concepto") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Factura" SortExpression="SerieFactura">
                                        <ItemTemplate>
                                                <asp:Label ID="lblFacturaPED" runat="server" Text='<%# Eval("FolioFactura") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cliente" SortExpression="ClienteReferencia">
                                        <ItemTemplate>
                                                <asp:Label ID="lblFacturaPEDCliente" runat="server" Text='<%# Eval("Cliente") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:TemplateField>
                                
                                    <asp:TemplateField HeaderText="Pedido" SortExpression="Pedido" Visible="True">
                                        <ItemTemplate>
                                                <asp:Label ID="lblPedidoPedido" runat="server" Text='<%# Eval("Pedido") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Celula" SortExpression="Celula" Visible="False">
                                        <ItemTemplate>
                                                <asp:Label ID="lblCelulaPedido" runat="server" Text='<%# Eval("Celula") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:TemplateField>
                                
                                    <asp:TemplateField HeaderText="AñoPed" SortExpression="AñoPed" Visible="False">
                                        <ItemTemplate>
                                                <asp:Label ID="lblAñoPedPedido" runat="server" Text='<%# Eval("AñoPed") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:TemplateField>


                                </Columns>
                                <PagerStyle CssClass="grvPaginacionScroll" />
                            </asp:GridView>
                            </div>
                            <asp:HiddenField ID="hfInternosSV" runat="server" />
                            <asp:HiddenField ID="hfInternosSH" runat="server" />

                        </div>
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
        <asp:UpdatePanel runat="server" ID="upBuscar" >
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
            <asp:Panel ID="pnlDetalle" runat="server" CssClass="ModalPopup" EnableViewState="true"
                Width="900px" Style="display: none;">
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
                            <div style="width:100%; height:240px; max-height:480px; overflow: auto;">
                                <asp:GridView ID="grvDetalleArchivoInterno" PageSize="5" runat="server" 
                                    AllowPaging='<%# ActivePaging %>' AutoGenerateColumns="False"
                                    ShowHeader="True" CssClass="grvResultadoConsultaCss" ShowHeaderWhenEmpty="True"
                                    ShowFooter="False" DataKeyNames="SecuenciaInterno, FolioInterno"
                                    OnPageIndexChanging="grvDetalleArchivoInterno_PageIndexChanging">
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
                                <asp:GridView ID="grvDetallePedidoInterno" runat="server" 
                                    AllowPaging='<%# ActivePaging %>' PageSize="5" AutoGenerateColumns="False"
                                    ShowHeader="True" Width="100%" CssClass="grvResultadoConsultaCss" ShowHeaderWhenEmpty="True"
                                    DataKeyNames="Celula,Pedido,AñoPed"
                                    OnPageIndexChanging="grvDetallePedidoInterno_PageIndexChanging" >
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
                            </div>
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
                            <asp:Button runat="server" ID="btnAceptarStatusInterno" Text="ACEPTAR" CssClass="boton fg-color-blanco bg-color-azulClaro"
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
                                    CssClass="iconoPequeño bg-color-rojo" OnClientClick="HideModalPopup();"  /><%--OnClick="imgCerrarImportar_Click" --%>
                            </div>
                            <div class="fg-color-blanco">
                                AGREGAR ARCHIVO
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
                                            Width="25px" Height="25px" ValidationGroup="Interno" CssClass="icono bg-color-verdeClaro"
                                            OnClick="btnAñadirFolio_Click" />
                                    </td>
                                    <td style="vertical-align: top; padding: 6px;">
                                        <asp:ImageButton ID="btnVerDatalleInterno" runat="server" ImageAlign="AbsMiddle"
                                            ValidationGroup="Interno" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/buscar.png"
                                            Width="25px" Height="25px" CssClass="icono bg-color-azulClaro" OnClick="btnVerDatalleInterno_Click" />
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
                            <asp:GridView ID="grvAgregados" runat="server" 
                                AllowPaging='<%# ActivePaging %>' AutoGenerateColumns="False"
                                BorderStyle="Dotted" CssClass="grvResultadoConsultaCss" Font-Size="12px" ShowHeaderWhenEmpty="True"
                                Width="90%" ShowHeader="True" BorderColor="White" DataKeyNames="Folio" PageSize="6"
                                OnRowDeleting="grvAgregados_RowDeleting" OnPageIndexChanging="grvAgregados_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Image ID="imgBien" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Exito.png"
                                                Width="15px" Heigth="15px" CssClass="icono bg-color-verdeClaro" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Wrap="True" Width="30px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Folios Agregados" SortExpression="fAgregados">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFoliosAgregados" runat="server" Text = '<%# Eval("Folio")%>'></asp:Label>
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
                            <div style="width:1000px; height:340px; overflow:auto;">
                                <asp:GridView ID="grvVistaRapidaInterno" runat="server"
                                    AllowPaging='<%# ActivePaging %>' PageSize="5" AutoGenerateColumns="False"
                                    BorderStyle="Dotted" Font-Size="12px" CssClass="grvResultadoConsultaCss" ShowHeaderWhenEmpty="True"
                                    Width="100%" GridLines="Horizontal"
                                    OnPageIndexChanging="grvVistaRapidaInterno_PageIndexChanging" >
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="Sin detalle del folio de la conciliacion."></asp:Label>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Documento" SortExpression="referencia">
                                            <ItemTemplate>
                                                <asp:Label ID="lblReferencia" runat="server" Text='<%# Eval("Referencia") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Justify" VerticalAlign="Middle" Wrap="True" Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FOperacion" SortExpression="fOperacion">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFOperracion" runat="server" Text='<%# Eval("FOperacion", "{0:d}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Justify" VerticalAlign="Middle" Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FMovimiento" SortExpression="fMovimiento">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFMovimiento" runat="server" Text='<%# Eval("FMovimiento","{0:d}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Justify" VerticalAlign="Middle" Wrap="True" Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripcion" SortExpression="descripcion">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("Descripcion") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Justify" VerticalAlign="Middle" Wrap="True" Width="150px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Deposito" SortExpression="deposito">
                                            <ItemTemplate>
                                                <b>
                                                    <asp:Label ID="lblDeposito" runat="server" Font-Size="10px" Width="100px" Text='<%# Eval("Deposito") %>'></asp:Label></b>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="True" Width="100px"
                                                CssClass="fg-color-blanco bg-color-grisClaro" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Retiro" SortExpression="retiro">
                                            <ItemTemplate>
                                                <b>
                                                    <asp:Label ID="lblRetiro" runat="server" Font-Size="10px" Width="100px" Text='<%# Bind("Retiro","{0:c2}") %>'></asp:Label></b>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="True" Width="100px"
                                                CssClass="fg-color-blanco bg-color-grisClaro" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Concepto" SortExpression="concepto">
                                            <ItemTemplate>
                                                <asp:Label ID="lblConcepto" runat="server" Text=<%# Bind("Concepto") %>></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Justify" Wrap="True" Width="500px" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <asp:HiddenField runat="server" ID="hdfAgregarTransfBancaria" />
    <asp:ModalPopupExtender ID="popUpAgregarTransfBancaria" runat="server" PopupControlID="pnlAgregarTransfBancaria"
        TargetControlID="hdfAgregarTransfBancaria" BehaviorID="ModalBehaviourTransferenciaBancaria"
        BackgroundCssClass="ModalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlAgregarTransfBancaria" runat="server" BackColor="#FFFFFF" Width="50%" style="display: none">
        <%--Style="display: none"--%>
        <asp:UpdatePanel ID="upAgregarTransferenciaBancaria" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr class="bg-color-grisOscuro">
                        <td colspan="4" style="padding: 5px 5px 5px 5px" class="etiqueta">
                            <div class="floatDerecha">
                                <asp:ImageButton runat="server" ID="img_cerrarTransfbancaria" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png"
                                    CssClass="iconoPequeño bg-color-rojo" OnClientClick="HideModalPopup();" OnClick="img_cerrarTransfbancaria_Click" /><%--OnClick="imgCerrarImportar_Click"--%>
                            </div>
                            <div class="fg-color-blanco">
                                AGREGAR NUEVA TRANSFERENCIA ENTRE CUENTAS
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="datos-estilo" style="padding: 10px 10px 10px 10px">
                            <div class="etiqueta" align="center">
                                <asp:Label ID="lbDireccion1" runat="server" CssClass="fg-color-negro"></asp:Label>
                            </div>
                            <br />
                            <div class="etiqueta">
                                Corporativo
                            </div>
                            <asp:TextBox ID="lbCorporativo" runat="server" Width="95%" CssClass="cajaTexto" Enabled="False"></asp:TextBox>
                            <br />
                            <div class="etiqueta">
                                Sucursal
                            </div>
                            <asp:TextBox ID="lbSucursal" runat="server" Width="95%" CssClass="cajaTexto" Enabled="False"></asp:TextBox>
                            <br />
                            <div class="etiqueta">
                                Nombre Banco
                            </div>
                            <asp:TextBox ID="lbNombreBanco" runat="server" Width="95%" CssClass="cajaTexto" Enabled="False"></asp:TextBox>
                            <br />
                            <div class="etiqueta">
                                Cuenta Banco
                            </div>
                            <asp:TextBox ID="lbCuentaBanco" runat="server" Width="95%" CssClass="cajaTexto" Enabled="False"></asp:TextBox>
                            <br />
                            <div class="etiqueta">
                                Fecha Movimiento
                            </div>
                            <asp:TextBox ID="lbFMovimiento" runat="server" Width="95%" CssClass="cajaTexto" Enabled="False"></asp:TextBox>
                            <br />
                            <div class="etiqueta">
                                Documento
                            </div>
                            <asp:TextBox ID="txtReferencia" runat="server" Width="95%" CssClass="cajaTexto" Enabled="True"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvReferencia" runat="server" ControlToValidate="txtReferencia"
                                ErrorMessage="Inserte una Referencia." Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="AgregarCtaTransf"></asp:RequiredFieldValidator>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                        <td class="datos-estilo" style="padding: 10px 10px 10px 10px; width: 50%">
                            <div class="etiqueta" align="center">
                                <asp:Label ID="lbDireccion2" runat="server" CssClass="fg-color-negro"></asp:Label>
                            </div>
                            <br />
                             <div class="etiqueta">
                                Corporativo
                            </div>
                           <asp:DropDownList ID="cboCorporativo" runat="server" AutoPostBack="True" Width="100%"
                                CssClass="dropDown" ondatabound="cboCorporativo_DataBound" 
                                onselectedindexchanged="cboCorporativo_SelectedIndexChanged" >
                            </asp:DropDownList>
                             <br />
                             <asp:RequiredFieldValidator ID="rfvCorporativo" runat="server" ControlToValidate="cboCorporativo"
                                ErrorMessage="Seleccione una corporativo." Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="AgregarCtaTransf"></asp:RequiredFieldValidator>
                          <%--  <div class="etiqueta">
                                Corporativo
                            </div>
                            <asp:TextBox ID="lbCorporativo_" runat="server" Width="95%" CssClass="cajaTexto"
                                Enabled="False"></asp:TextBox>--%>
                            <br />
                            <div class="etiqueta">
                                Sucursal
                            </div>
                           <asp:DropDownList ID="cboSucursal" runat="server" AutoPostBack="True" Width="100%"
                                CssClass="dropDown" 
                                onselectedindexchanged="cboSucursal_SelectedIndexChanged" >
                            </asp:DropDownList>
                             <br />
                             <asp:RequiredFieldValidator ID="rfvSucursal" runat="server" ControlToValidate="cboSucursal"
                                ErrorMessage="Seleccione una sucursal." Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="AgregarCtaTransf"></asp:RequiredFieldValidator>
                            <%--<div class="etiqueta">
                                Sucursal
                            </div>
                            <asp:TextBox ID="lbSucursal_" runat="server" Width="95%" CssClass="cajaTexto" Enabled="False"></asp:TextBox>
                            --%>
                            <br />
                            <div class="etiqueta">
                                Nombre Banco
                            </div>
                           <asp:DropDownList ID="cboNombreBanco" runat="server" AutoPostBack="True" Width="100%"
                                CssClass="dropDown"  
                                onselectedindexchanged="cboNombreBanco_SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                             <asp:RequiredFieldValidator ID="rfvNombreBanco" runat="server" ControlToValidate="cboCuentaBanco"
                                ErrorMessage="Seleccione un banco." Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="AgregarCtaTransf"></asp:RequiredFieldValidator>
                            <div class="etiqueta">
                                Cuenta Banco
                            </div>
                            <asp:DropDownList ID="cboCuentaBanco" runat="server" AutoPostBack="True" Width="100%"
                                CssClass="dropDown" 
                                onselectedindexchanged="cboCuentaBanco_SelectedIndexChanged" >
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvCuentaBanco" runat="server" ControlToValidate="cboCuentaBanco"
                                ErrorMessage="Seleccione una cuenta de banco." Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="AgregarCtaTransf"></asp:RequiredFieldValidator>
                            <div class="etiqueta">
                                Fecha Aplicacion</div>
                            <asp:TextBox runat="server" ID="txtFechaAplicacion" CssClass="cajaTexto" Width="96%"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvFCA" runat="server" ErrorMessage="Indique la fecha de aplicación."
                                ControlToValidate="txtFechaAplicacion" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="AgregarCtaTransf" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                            <div class="etiqueta">
                                Descripción</div>
                            <asp:TextBox ID="txtDescripcion" runat="server" MaxLength="100" TextMode="MultiLine"
                                Rows="4" CssClass="cajaTexto" Width="95%" Style="resize: none"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="datos-estilo; bg-color-verdeClaro" style="padding: 10px 10px 10px 10px">
                            <div class="etiqueta; fg-color-blanco">
                                Cargo
                            </div>
                          <%--  <asp:Label ID="lbCargo" runat="server" CssClass="fg-color-negro"></asp:Label>
                            <br />--%>
                            
                             <asp:TextBox runat="server" ID="txtCargo" onkeypress="return ValidNumDecimal(event)" CssClass="cajaTexto" Width="96%"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="frvCargo" runat="server" ErrorMessage="Indique el monto del cargo."
                                ControlToValidate="txtCargo" Font-Size="10px"  CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="AgregarCtaTransf" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td class="datos-estilo; bg-color-amarillo" style="padding: 10px 10px 10px 10px;
                            width: 50%">
                            <div class="etiqueta; fg-color-blanco">
                                Abono
                            </div>
                           <%-- <asp:Label ID="lbAbono" runat="server" CssClass="fg-color-negro"></asp:Label>--%>
                            <asp:TextBox runat="server" ID="txtAbono" onkeypress="return ValidNumDecimal(event)" CssClass="cajaTexto" Width="96%"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="frvAbono" runat="server" ErrorMessage="Indique el monto del abono."
                                ControlToValidate="txtAbono" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                ValidationGroup="AgregarCtaTransf" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="centradoMedio">
                            <asp:Button ID="btnguardarTransfBancaria" runat="server" Text="GUARDAR" ToolTip="GUARDAR"
                                ValidationGroup="AgregarCtaTransf" CssClass="boton fg-color-blanco bg-color-verdeClaro"
                                OnClick="btnGuardar__Click" />
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

    <!--        INICIO DE POPUP CARGA ARCHIVO     -->
    <asp:HiddenField runat="server" ID="hdfCargaAgregado" value="1"/>
    <asp:HiddenField runat="server" ID="hdfCargaArchivo" />
    <asp:HiddenField runat="server" ID="hdfVisibleCargaArchivo" value="0"/>
    <asp:ModalPopupExtender ID="mpeCargaArchivoConciliacionManual" runat="server" BackgroundCssClass="ModalBackground"
        DropShadow="False" PopupControlID="pnlCargaArchivo" TargetControlID="hdfCargaArchivo"
        BehaviorID="mpeCargaArchivo" CancelControlID="btnCerrarCargaArchivo"> <%--EnableViewState="false"--%>
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlCargaArchivo" runat="server" CssClass="ModalPopup" width="700px" style="display: none;">  
    <asp:UpdatePanel ID="UPCargaArchivoConciliacionManual" runat="server">
        <ContentTemplate>
            <div>
                <table style="width:100%; box-sizing:border-box;">
                    <tr class="bg-color-grisOscuro">
                        <td style="padding: 5px 5px 5px 5px; box-sizing:border-box;" class="etiqueta">
                            <div class="floatDerecha">
                                <asp:ImageButton runat="server" ID="btnCerrarCargaArchivo" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png"
                                    CssClass="iconoPequeño bg-color-rojo" Width="20px" Height="20px" OnClientClick="btnCargaManualCancelar_Click();"
                                    OnClick="btnCerrarCargaArchivo_Click"/> <%--OnClientClick="popUpNoVisible(); OcultarPopUpConciliacionManual();"--%>
                            </div>
                            <div class="fg-color-blanco centradoJustificado">
                                CARGAR ARCHIVO
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:WebUserControl ID="wucCargaExcelCyC" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Panel>
    <!--        FIN POPUP CARGA ARCHIVO     -->

    <!--        INICIO DE POPUP CONCILIAR PAGARES     -->
    <asp:HiddenField runat="server" ID="hdfConciliarPagares" />
    <asp:ModalPopupExtender ID="mpeConciliarPagares" runat="server" BackgroundCssClass="ModalBackground"
        DropShadow="False" PopupControlID="pnlConciliarPagares" TargetControlID="hdfConciliarPagares"
        BehaviorID="mpeConciliarPagares" CancelControlID="imgCerrar_ConciliarPagares"> <%-- BehaviorID="ModalBehaviour" EnableViewState="false"--%>
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlConciliarPagares" runat="server" CssClass="ModalPopup" width="700px" style="display: none;">  
    <asp:UpdatePanel ID="upConciliarPagares" runat="server">
        <ContentTemplate>
            <div>
                <table style="width:100%;">
                    <tr class="bg-color-grisOscuro">
                        <td style="padding: 5px 5px 5px 5px;" class="etiqueta">
                            <div class="floatDerecha bg-color-grisClaro01">
                                <asp:ImageButton runat="server" ID="imgCerrar_ConciliarPagares" CssClass="iconoPequeño bg-color-rojo" 
                                    ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png" Width="20px" Height="20px" 
                                    OnClientClick="OcultarPopUpConciliarPagares();"/>
                            </div>
                            <div class="fg-color-blanco centradoJustificado">
                                CONCILIAR PAGARES
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:wucConciliadorPagare ID="wucConciliadorPagare" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Panel>
    <!--        FIN POPUP CONCILIAR PAGARES     -->

    <!--        INICIO DE POPUP CLIENTE PAGO     -->
    <asp:HiddenField runat="server" ID="hdfClientePago" />
    
    <asp:ModalPopupExtender ID="mpeClientePago" runat="server" BackgroundCssClass="ModalBackground"
        DropShadow="False" PopupControlID="pnlClientePago" TargetControlID="hdfClientePago"
        BehaviorID="ModalBehaviorClientePago" CancelControlID="btnCerrar_ClientePago">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlClientePago" runat="server" CssClass="ModalPopup" width="700px" style="display: none;">  
    <asp:UpdatePanel ID="upClientePago" runat="server">
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="hdfClientePagoAnio"         />
            <asp:HiddenField runat="server" ID="hdfClientePagoCorporativo"  />
            <asp:HiddenField runat="server" ID="hdfClientePagoFolio"        />
            <asp:HiddenField runat="server" ID="hdfClientePagoSecuencia"    />
            <asp:HiddenField runat="server" ID="hdfClientePagoSucursal"/>
            <asp:HiddenField runat="server" ID="hdfClientePagoMostrar" Value=""/>
            <asp:HiddenField runat="server" ID="hdfClientePagoAceptar" Value=""/>
            <asp:HiddenField runat="server" ID="hdfClientePagoCancelar" Value=""/>
            <div>
                <table style="width:100%;">
                    <tr class="bg-color-grisOscuro">
                        <td style="padding: 5px 5px 5px 5px;" class="etiqueta">
                            <div class="floatDerecha bg-color-grisClaro01">
                                <asp:ImageButton runat="server" ID="btnCerrar_ClientePago" CssClass="iconoPequeño bg-color-rojo" 
                                    ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png" Width="20px" Height="20px" 
                                    OnClientClick="OcultarPopUpClientePago();"/>
                            </div>
                            <div class="fg-color-blanco centradoJustificado">
                                CLIENTE PAGO
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:wucClientePago ID="wucClientePago" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Panel>
    <!--        FIN POPUP CLIENTE PAGO     -->

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
                                <uc1:wucBuscadorPagoEstadoCuenta runat="server" ID="wucBuscadorPagoEstadoCuenta" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <%--FIN POPUP BUSCADORPAGOESTADO DE CUENTA--%>

    <!--        INICIO DE POPUP CLIENTE DATOS BANCARIOS     -->
    <asp:HiddenField runat="server" ID="hdfClienteDatosBancarios" />
    <asp:ModalPopupExtender ID="mpeClienteDatosBancarios" runat="server" BackgroundCssClass="ModalBackground"
        DropShadow="False" PopupControlID="pnlClienteDatosBancarios" TargetControlID="hdfClienteDatosBancarios"
        BehaviorID="ModalBehavior" CancelControlID="imbClienteDatosBancarios_Cerrar">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlClienteDatosBancarios" runat="server" CssClass="ModalPopup" width="700px" style="display:none;">  
    <asp:UpdatePanel ID="upClienteDatosBancarios" runat="server">
        <ContentTemplate>
            <div>
                <table style="width:100%;">
                    <tr class="bg-color-grisOscuro">
                        <td style="padding: 5px 5px 5px 5px;" class="etiqueta">
                            <div class="floatDerecha bg-color-grisClaro01">
                                <asp:ImageButton runat="server" ID="imbClienteDatosBancarios_Cerrar" CssClass="iconoPequeño bg-color-rojo" 
                                    ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png" Width="20px" Height="20px" 
                                    OnClientClick="OcultarPopUpClienteDatosBancarios();"/>
                            </div>
                            <div class="fg-color-blanco centradoJustificado">
                                SELECCIONAR CLIENTE
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:wucClienteDatosBancarios ID="wucClienteDatosBancarios" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="centradoMedio">
                            <asp:Button ID="btnAceptarClienteDatosBancarios" runat="server" Text="ACEPTAR" 
                                CssClass="boton fg-color-blanco bg-color-azulClaro" 
                                OnClick="btnAceptarClienteDatosBancarios_Click" />
                            <asp:Button ID="btnCancelarClienteDatosBancarios" runat="server" Text="CANCELAR"
                                CssClass="boton fg-color-blanco bg-color-grisClaro"
                                OnClick="btnCancelarClienteDatosBancarios_Click"/>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Panel>
    <!--        FIN POPUP CLIENTE DATOS BANCARIOS     -->

    <asp:HiddenField ID="hdfAceptaAplicarSaldoAFavor" runat="server" /> 
    <asp:HiddenField ID="hdfSaldoAFavor" runat="server" />
    
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
