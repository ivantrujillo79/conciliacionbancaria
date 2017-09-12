<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="ConsultarTransban.aspx.cs" Inherits="Conciliacion_Pagos_ConsultarTransban" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
    CONSULTAR TRANSBAN
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
     <!--Libreria jQuery-->
    <script src="../../App_Scripts/jQueryScripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../App_Scripts/jQueryScripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../App_Scripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">
        <asp:ScriptManager runat="server" ID="smCantidadConcuerda" AsyncPostBackTimeout="600"
        EnableScriptGlobalization="True">
    </asp:ScriptManager>
    <script src="../../App_Scripts/jsUpdateProgress.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        var ModalProgress = '<%= mpeLoading.ClientID %>';        
    </script>
    <asp:UpdatePanel runat="server" ID="upLanzarReporte" UpdateMode="Always">
        <ContentTemplate>
            <div style="text-align: left">
                <div>
                    Caja
                </div>
                <asp:TextBox runat="server" ID="txtCaja" CssClass="cajaTexto"></asp:TextBox>
                <div>
                    Consecutivo
                </div>
                <asp:TextBox runat="server" ID="txtConsecutivo" CssClass="cajaTexto"></asp:TextBox>
                <div>
                    Folio
                </div>
                <asp:TextBox runat="server" ID="txtFolio" CssClass="cajaTexto"></asp:TextBox>
                <div>
                    FOperacion
                </div>
                <asp:TextBox runat="server" ID="txtFOperacion" CssClass="cajaTexto"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="txtFOperacion_CalendarExtender" runat="server"
                    TargetControlID="txtFOperacion" Format="dd/MM/yy">
                </ajaxToolkit:CalendarExtender>
                <br />
                <asp:ImageButton ID="imgExportar" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Exportar.png"
                    ToolTip="EXPORTAR" Width="25px" CssClass="iconoBoton" OnClick="imgExportar_Click" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="panelBloqueo" runat="server" AssociatedUpdatePanelID="upLanzarReporte">
        <ProgressTemplate>
            <asp:Image ID="imgLoad" runat="server" CssClass="icono bg-color-blanco" Height="40px"
                ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/LoadPage.gif" Width="40px" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ModalPopupExtender ID="mpeLoading" runat="server" BackgroundCssClass="ModalBackground"
        PopupControlID="panelBloqueo" TargetControlID="panelBloqueo">
    </asp:ModalPopupExtender>
</asp:Content>

