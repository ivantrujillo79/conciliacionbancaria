<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucClienteDatosBancarios.ascx.cs" Inherits="ControlesUsuario_ClienteDatosBancarios_wucClienteDatosBancarios" %>

<div style="max-height:300px;overflow:auto;">
    <table style="width:100%;">
        <tr class="etiqueta centradoJustificado">
            <td style="width:100%;">
                <asp:GridView runat="server" ID="grvClientes" AutoGenerateColumns="false" ShowHeader="true" 
                    ShowHeaderWhenEmpty="true" CssClass="grvResultadoConsultaCss" ShowFooter="false" Width="100%">
                    <HeaderStyle HorizontalAlign="Center" />
                    <Columns>                    
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:RadioButton ID="rbSeleccion" runat="server" OnCheckedChanged="rbSeleccion_CheckedChanged"
                                    AutoPostBack="true"/> 
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" BackColor="#ebecec"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cliente" SortExpression="Cliente">
                            <ItemTemplate>
                                <asp:Label ID="lblCliente" runat="server" Text='<%# Eval("NumCliente") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre" SortExpression="Nombre">
                            <ItemTemplate>
                                <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("Nombre") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Empresa" SortExpression="Empresa">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpresa" runat="server" Text='<%# Eval("RazonSocial") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</div>
