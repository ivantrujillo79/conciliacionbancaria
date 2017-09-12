<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true"
    CodeFile="TipoConciliacionUsuario.aspx.cs" Inherits="Catalogos_TipoConciliacionUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="width: 33%;vertical-align: top" >
                <div class="Filtrado" style="width: 100%">
                    <div class="tiraAmarilla">
                    </div>
                    <div class="titulo">
                        Usuario</div>
                    <div class="datos-estilo">
                        <div class="etiqueta">
                            Empleado
                        </div>
                        <asp:DropDownList ID="ddlEmpleado" runat="server" CssClass="dropDown" Width="100%"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlEmpleado_SelectedIndexChanged">
                        </asp:DropDownList>
                        <div class="etiqueta">
                            Usuario</div>
                        <asp:TextBox ID="txtUsuario" runat="server" Enabled="False" CssClass="cajaTexto"
                            Width="98%" BackColor="white"></asp:TextBox>
                        <br />
                        <br />
                    </div>
                </div>
            </td>
            <td style="vertical-align: top">
                <div class="titulo lineaHorizontal ">
                    Tipos de Conciliación</div>
                <div class="datos-estilo">
                    <br />
                    <div class="etiqueta">
                        Asignados</div>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 90%">
                                <asp:DropDownList ID="ddlAsignados" runat="server" Width="100%" CssClass="dropDown"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlAsignados_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 10%">
                                <asp:Button ID="btnDesasignar" runat="server" OnClick="btnDesasignar_Click" Text="DESASIGNAR"
                                    CssClass="boton fg-color-blanco bg-color-grisClaro" ToolTip="Desasigna el tipo seleccionado al usuario"
                                    Style="margin: 0px 0px 0px 10px" Width="100px" />
                            </td>
                        </tr>
                    </table>
                    <div class="etiqueta">
                        No Asignados</div>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 90%">
                                <asp:DropDownList ID="ddlNoAsignados" runat="server" CssClass="dropDown" Width="100%"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlNoAsignados_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 10%">
                                <asp:Button ID="btnAsignar" runat="server" OnClick="btnAsignar_Click" Text="ASIGNAR"
                                    CssClass="boton fg-color-blanco bg-color-azulClaro" ToolTip="Asigna el tipo seleccionado al usuario"
                                    Style="margin: 0px 0px 0px 10px" Width="100px" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <div class="centradoDerecha">
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
