<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.master" AutoEventWireup="true"
    CodeFile="Acceso.aspx.cs" Inherits="Acceso_Acceso" %>

<%@ MasterType TypeName="Sitio" %>
<%@ Register Src="Login.ascx" TagName="Login" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    ACCESO
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <script src="../App_Scripts/jsModalUpdateProgress.js" type="text/javascript"></script>
    <script src="../App_Scripts/jQueryScripts/jquery-3.2.1.min.js" type="text/javascript"></script>
    
     <script type="text/javascript" language="javascript">
         var modalId = '<%:mpeLoading.ClientID%>';

         $(document).ready(function () {
             console.log('Documento cargado');
         });

         $(window).on("load", function () {
             var txtUsr = document.getElementById('ctl00_contenidoPrincipal_Login_txtUsuario');
             txtUsr.focus();
             console.log('Ventana cargada');
         });

    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager ID="smAcceso" runat="server" AsyncPostBackTimeout="600">
    </asp:ScriptManager>
    <div id="header-site" style="height: 30px; width: 100%">
    </div>
    <div>
        <asp:UpdatePanel ID="upAcceso" runat="server">
            <ContentTemplate>
                <div class="centradoPrincipal">
                    <div class="centro">
                        <div class="centradoPrincipal">
                            <div class="centro" style="margin-left: 250px">
                                <table>
                                    <tr>
                                        <td>
                                            <div style="width: 425px; margin-right: 50px; margin-left: 0px">
                                                <div style="width: 425px;">
                                                    <img alt="CONCILIACIÓN BANCARIA" src="../App_Themes/GasMetropolitanoSkin/Imagenes/conciliacionbanca.png" />
                                                </div>
                                                <div id="pie" class="pieBannerLogin">
                                                    <div class="encabezadoAcceso">
                                                        <h1>
                                                            Conciliación Bancaria</h1>
                                                    </div>
                                                    <p class="additional-info">
                                                        Le damos la bienvenida a una nueva, más rápida y más económica forma de realizar
                                                        la conciliación bancaria de sus cuentas</p>
                                                </div>
                                            </div>
                                        </td>
                                        <td style="vertical-align: top">
                                            <div style="width: 350px;">
                                                <div class="datos-estilo">
                                                    <asp:Image ID="imagenLogotipoEmpresa" SkinID="logotipoEmpresa" runat="server" AlternateText="Grupo Metropolitano" />
                                                </div>
                                                <div style="height: 30px;">
                                                </div>
                                                <div>
                                                    <uc1:Login ID="Login" runat="server" />
                                                </div>
                                                <br />
                                                <br />
                                                <div class="datos-estilo" style="margin-left: 40px">
                                                    <asp:ValidationSummary ID="valAcceso" runat="server" CssClass="fg-color-rojo" ShowMessageBox="True" />
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr style="margin-bottom: 50px">
                                        <td colspan="2">
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanelAnimationExtender ID="upae" runat="server" BehaviorID="animation"
            TargetControlID="upAcceso">
            <Animations>
                    <OnUpdating>
                        <Parallel duration="0">
                            <ScriptAction Script="accionActualizando();" />  
                 </Parallel>
                    </OnUpdating>
                    <OnUpdated>
                        <Parallel duration="0">
                       <ScriptAction Script="accionActualizado();" /> 
                        </Parallel> 
                    </OnUpdated>
            </Animations>
        </asp:UpdatePanelAnimationExtender>
        <asp:ModalPopupExtender ID="mpeLoading" runat="server" BackgroundCssClass="ModalBackground"
            DropShadow="False" EnableViewState="false" PopupControlID="imgLoad" TargetControlID="hdfLoad">
        </asp:ModalPopupExtender>
        <asp:HiddenField ID="hdfLoad" runat="server" />
        <asp:Image ID="imgLoad" runat="server" CssClass="icono bg-color-blanco" Height="40px"
            ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/LoadPage.gif" Width="40px" />
    </div>
</asp:Content>
