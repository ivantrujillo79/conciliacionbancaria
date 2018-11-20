<%@ Page Title=""  Language="C#"  MasterPageFile="~/Principal.master"  AutoEventWireup="true" CodeFile="paginaAreasComunes.aspx.cs" Inherits="paginaAreasComunes" %>

<%@ Register Src="~/ControlesUsuario/AreasComunes/areascomunes.ascx" TagPrefix="ControlUsuario" TagName="AreasComunesControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>




<asp:Content ID="MainContent" ContentPlaceHolderID="contenidoPrincipal" runat="server">
        <script type="text/javascript">
            function OcultarPopUpAreasComunes() {
                $find("mpeAreasComunes").hide();
        }

        </script>

       <div><asp:TextBox ID="txtClientePadre" runat="server"></asp:TextBox>
             <asp:TextBox ID="txtmonto" runat="server"></asp:TextBox>
        <asp:Button ID="btnBuscaClientePadre" runat="server" Text="Button" OnClick="btnBuscaClientePadre_Click" />

             </div>
   
    <div>
        <!--        INICIO DE POPUP CONCILIAR PAGARES     -->
    <asp:HiddenField runat="server" ID="hdfAreasComunes" />
    <asp:ModalPopupExtender ID="mpeAreasComunes" runat="server" BackgroundCssClass="ModalBackground"
        DropShadow="False" PopupControlID="pnlAreasComunes" TargetControlID="hdfAreasComunes"
        BehaviorID="mpeAreasComunes" CancelControlID="imgCerrar_AreasComunes"> <%-- BehaviorID="ModalBehaviour" EnableViewState="false"--%>
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlAreasComunes" runat="server" CssClass="ModalPopup" width="700px" style="display: none;">  
    <asp:UpdatePanel ID="upAreasComunes" runat="server">    
        <ContentTemplate>
            <div>
                <table style="width:100%;">
                    <tr class="bg-color-grisOscuro">
                        <td style="padding: 5px 5px 5px 5px;" class="etiqueta">
                            <div class="floatDerecha bg-color-grisClaro01">
                                <asp:ImageButton runat="server" ID="imgCerrar_AreasComunes" CssClass="iconoPequeño bg-color-rojo" 
                                    ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png" Width="20px" Height="20px" 
                                    OnClientClick="OcultarPopUpAreasComunes();"/>
                            </div>
                            <div class="fg-color-blanco centradoJustificado">
                                CONCILIAR PAGARES
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>                         
                          <ControlUsuario:areascomunesControl runat="server" ID="wuAreascomunes" />   
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Panel>
    <!--        FIN POPUP CONCILIAR PAGARES     -->

<%--        <ControlUsuario:areascomunesControl runat="server" ID="pnlAreascomunes" />       --%>
    </div>

 
   
</asp:Content>
