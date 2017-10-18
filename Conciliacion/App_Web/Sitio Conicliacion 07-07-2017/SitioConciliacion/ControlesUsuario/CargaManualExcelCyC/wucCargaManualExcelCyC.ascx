<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucCargaManualExcelCyC.ascx.cs" Inherits="Conciliacion_WebUserControl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!-- Script GridView Scroll -->
<link href="../../App_Scripts/ScrollGridView/GridviewScroll.css" rel="stylesheet"
    type="text/css" />
<script src="../../App_Scripts/ScrollGridView/gridviewScroll.min.js" type="text/javascript"></script>

<script type="text/javascript">
    <%--function pageLoad() {
        gridviewScroll();
    }

    function gridviewScroll() {
        $('#<%=grvDetalleConciliacionManual.ClientID%>').gridviewScroll({
            //width: 595,
            //height: 370,
            freezesize: 3,
            arrowsize: 30,
            varrowtopimg: '../../App_Scripts/ScrollGridView/Images/arrowvt.png',
            varrowbottomimg: '../../App_Scripts/ScrollGridView/Images/arrowvb.png',
            //harrowleftimg: '../../App_Scripts/ScrollGridView/Images/arrowhl.png',
            //harrowrightimg: '../../App_Scripts/ScrollGridView/Images/arrowhr.png',
            headerrowcount: 1,
            startVertical: $("#<%=hfConciliacionManualSV.ClientID%>").val(),
            onScrollVertical: function (delta) { $("#<%=hfConciliacionManualSV.ClientID%>").val(delta); }
            
        });
    }--%>
</script>

<div style="width:inherit;">
    <table style="width:100%; box-sizing:border-box;">
        <%--<tr class="bg-color-grisOscuro">
            <td style="padding: 5px 5px 5px 5px; box-sizing:border-box;" class="etiqueta">
                <div class="floatDerecha">
                    <asp:ImageButton runat="server" ID="btnCerrarCargaArchivo" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png"
                        CssClass="iconoPequeño bg-color-rojo" />
                </div>
            </td>
        </tr>--%>
        <tr>
            <td style="padding:10px 0px 0px 10px; box-sizing:border-box;">
                <asp:FileUpload ID="fupSeleccionar" runat="server" ViewStateMode="Enabled"/>
            </td>
        </tr>
        <tr>
            <td style="padding-left:6px; box-sizing:border-box;"> 
                <asp:Button ID="btnSubirArchivo" runat="server" CssClass="boton fg-color-blanco bg-color-azulClaro"
                    Text="Subir archivo" OnClick="btnSubirArchivo_Click" />               
            </td>
        </tr>
        <tr>
            <td style="padding-left:10px; padding-right: 5px; box-sizing:border-box;">
                <div>
                    <asp:Label ID="lblArchivo" runat="server" CssClass="etiqueta" Font-Size="1em" Text="Archivo: "
                        style="color:red; display:inline-block;"/> <!--   width:200px;     -->
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="bg-color-grisClaro01" style="margin-left:5px; padding-right: 5px; box-sizing:border-box;">
                    <asp:Label ID="lblRegistros" runat="server" CssClass="etiqueta " Font-Size="1em" 
                        Text="Total de registros a cargar: " style="padding-left:5px;" />
                </div>
            </td>
        </tr>
        <tr> 
            <td style="padding: 5px 5px 5px 5px; width: 100%; text-align: center; box-sizing:border-box;">
                <div style="margin: 5px 5px 5px 3px;">
                    <%--    Barra de Scroll    --%>
                    <asp:HiddenField ID="hfConciliacionManualSV" runat="server" />
                    <asp:GridView ID="grvDetalleConciliacionManual" runat="server" style="align-content:center;"
                        CssClass="grvResultadoConsultaCss" ShowHeaderWhenEmpty="True" Width="100%" 
                        ViewStateMode="Enabled" >
                        <PagerStyle CssClass="grvPaginacionScroll" />
                        <SelectedRowStyle BackColor="#66CCFF" ForeColor="Black" />
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <%--<tr>
            <td style="padding-left:7px; box-sizing:border-box;">
                <asp:Button ID="btnCargaArchivoAceptar" runat="server" CssClass="boton fg-color-blanco bg-color-azulClaro"
                    Text="Aceptar" OnClientClick="Saluda();" OnClick="btnSubirArchivo_Click" />
                <asp:Button ID="btnCargaArchivoCancelar" runat="server" CssClass="boton fg-color-blanco bg-color-grisClaro"
                    Text="Cancelar" OnClick="btnCargaArchivoCancelar_Click"/>                                        
            </td>
        </tr>--%>
    </table>

</div>
