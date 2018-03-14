<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="ReporteEstadoCuentaPorDia.aspx.cs" 
    Inherits="ReportesConciliacion_ReporteEstadoCuentaPorDia" %>

<%@ Register Src="~/ControlesUsuario/wuCuentasBancarias/WUCListadoCuentasBancarias.ascx" TagName="WUCCuentasBancarias"
    TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">

    <table width="100%">
        <tr>
            <td style="padding: 5px;">
                <asp:Label ID="lblTitulo" runat="server" Text="Estado de cuenta por d&iacute;a" />
            </td>
        </tr>
        <tr>
            <td>
                <div>
                    <table width="100%">
                        <tr>
                            <!--        Etiquetas       -->
                            <td style="vertical-align:top;">
                                <div class="etiqueta centradoDerecha">
                                    <table width="100%" >
                                        <tr>
                                            <td style="padding:5px;height:35px">
                                                <asp:Label runat="server" Text="Fecha inicial:"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding:5px;height:35px">
                                                <asp:Label runat="server" Text="Fecha final:"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding:5px;height:35px">
                                                <asp:Label runat="server" Text="Banco:"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding:5px;height:35px">
                                                <asp:Label runat="server" Text="Cuenta bancaria:"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <!--        Controles       -->
                            <td style="vertical-align:top" >
                                <div class="etiqueta centradoIzquierda">
                                    <table width="100%">
                                        <tr>
                                            <td style="padding:5px;height:35px">
                                                <asp:TextBox ID="txtFechaInicial" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding:5px;height:35px">
                                                <asp:TextBox ID="TextBox1" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding:5px;height:35px">
                                                <asp:DropDownList ID="ddlBanco" runat="server" Width="200px"/> <%--CssClass="dropDown" --%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <uc1:WUCCuentasBancarias ID="WUCCuentasBancarias" runat="server"/>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding:5px;" class="centradoMedio">
                                <asp:Button ID="btnConsultar" Text="CONSULTAR" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                    runat="server"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>

</asp:Content>

