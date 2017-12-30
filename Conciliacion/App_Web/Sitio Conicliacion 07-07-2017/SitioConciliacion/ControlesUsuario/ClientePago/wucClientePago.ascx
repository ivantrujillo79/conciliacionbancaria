<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucClientePago.ascx.cs" Inherits="ControlesUsuario_ClientePago_wucClientePago" %>


                        <asp:GridView ID="grvClientes" runat="server" ShowHeader="True"
                            CssClass="grvResultadoConsultaCss" ShowFooter="False" Width="100%"
                            ShowHeaderWhenEmpty="True" 
                            AllowPaging="False">
                            <HeaderStyle HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:RadioButton ID="RadioButton1" runat="server" 
                                            ToolTip="SELECCIONAR CLIENTE"
                                            />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="25px" BackColor="#ebecec"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="25px"></HeaderStyle>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Cliente" SortExpression="Cliente">
                                    <ItemTemplate>
                                            <asp:Label ID="lblCliente" runat="server" Text='<%# Eval("Cliente") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Nom. Cliente" SortExpression="Nombre">
                                    <ItemTemplate>
                                            <asp:Label ID="lblClienteNombre" runat="server" Text='<%# Eval("Nombre") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Tipo" SortExpression="Tipo">
                                    <ItemTemplate>
                                            <asp:Label ID="lblClienteTipo" runat="server" Text='<%# Eval("Tipo") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </asp:TemplateField>                                
                                
                            </Columns>
                            <PagerStyle CssClass="grvPaginacionScroll" />
                        </asp:GridView>
