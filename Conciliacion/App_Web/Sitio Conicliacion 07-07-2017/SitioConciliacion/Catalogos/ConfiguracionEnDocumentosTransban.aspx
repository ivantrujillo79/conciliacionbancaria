<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true"
    CodeFile="ConfiguracionEnDocumentosTransban.aspx.cs" Inherits="Catalogos_TipoMovimientoPorCuenta" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style2
        {
            width: 238px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../App_Scripts/jsUpdateProgress.js" type="text/javascript"></script>
    <%--<script src="../../App_Scripts/jsUpdateProgress.js" type="text/javascript"></script>--%>
    <script type="text/javascript" language="javascript">
        var ModalProgress = '<%= mpeLoading.ClientID %>';   
    </script>
    <asp:UpdatePanel runat="server" ID="upCuentaTransferencia" UpdateMode="Always">
        <ContentTemplate>
            <script type="text/javascript">

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
            <table style="width: 100%">
                <tr>
                    <td style="vertical-align: top" class="style2">
                        <br />
                        <div class="Filtrado">
                            <%-- </ContentTemplate>
                    </asp:UpdatePanel>--%>
                            <div class="tiraVerdeClaro">
                            </div>
                            <div class="titulo">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 85%">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </div>
                            <div class="Filtrado">
                                <br />
                                <br />
                                <br />
                                <br />
                                <div align="center">
                                    <div class="etiqueta">
                                        Valor del parámetro
                                    </div>
                                    <asp:TextBox ID="tbValor" runat="server" style="" Width="50%"></asp:TextBox>
                                </div>
                                <br />
                                <br />
                                <br />
                                <br />


                               
                                <div class="Filtrado">
   
                                    <div class="centradoMedio">
                                        <br />
                                        <asp:Button ID="btnAgregar" runat="server" Text="MODIFICAR" ToolTip="Modifica el valor del parametro"
                                            CssClass="boton fg-color-blanco bg-color-naranja" OnClick="btnModificar_Click1" />
                                    </div>
                                </div>
                                <%--Style="display: none"--%>
                            </div>
                        </div>
                    </td>
                    <td style="vertical-align: top">
                        <div class="datos-estilo" style="margin-right: 15px; margin-left: 15px">
                            <div class="titulo" style="margin-left: 0px">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 95%">
                                            Catálogo: No. Documentos por TransBan</td>
                                        <td>
                                            <img src="../App_Themes/GasMetropolitanoSkin/Imagenes/grid.png" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="datos-estilo">
                                <br />
                                <br />

                                
                                <br />
                                <br />
                                <div class="etiqueta" style="font-size: large">
                                    <asp:label  iD="lbObservaciones" runat ="server" Text="Numero de Documentos que integran una TRANSBAN en una Conciliacion"> </asp:label>                                   

                                </div>
                                <br />
                            </div>
                            <br />
                        </div>
                        <br />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="panelBloqueo" runat="server" AssociatedUpdatePanelID="upCuentaTransferencia">
        <ProgressTemplate>
            <asp:Image ID="imgLoad" runat="server" CssClass="icono bg-color-blanco" Height="40px"
                ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/LoadPage.gif" Width="40px" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ModalPopupExtender ID="mpeLoading" runat="server" BackgroundCssClass="ModalBackground"
        PopupControlID="panelBloqueo" TargetControlID="panelBloqueo">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlAgregarTransferencia" runat="server" BackColor="#FFFFFF" Width="30%">
        <%--Style="display: none"--%>
    </asp:Panel>
</asp:Content>
