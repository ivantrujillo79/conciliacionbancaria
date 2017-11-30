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
                                    <table>
                                        <tr>
                                            <td>
                                                Valor para el parámetro
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="tbValor" runat="server" style="" Width="100px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>
                                                <asp:RegularExpressionValidator ID="revtbValor"
                                        ControlToValidate="tbValor" runat="server"
                                        ErrorMessage="Introduzca sólo valores numéricos"
                                        ValidationExpression="\d+" >
                                    </asp:RegularExpressionValidator>
                                                    </strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>
                                                    <asp:RequiredFieldValidator runat="server" id="reqParametro" controltovalidate="tbValor" errormessage="Por favor ingrese un valor" />
                                                </strong>
                                            </td>
                                        </tr>
                                    </table>
                                 </div>
                                    
                                    
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
                                    <asp:label  iD="lbObservaciones" runat ="server" Text="Número de documentos que integran una TRANSBAN en una conciliación"> </asp:label>                                   

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
