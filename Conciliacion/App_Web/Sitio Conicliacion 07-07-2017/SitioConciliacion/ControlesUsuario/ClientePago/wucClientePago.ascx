<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucClientePago.ascx.cs" Inherits="ControlesUsuario_ClientePago_wucClientePago" %>

    <!-- Script se utiliza para el Scroll del GridView-->
    <link href="../../App_Scripts/ScrollGridView/GridviewScroll.css" rel="stylesheet"type="text/css" />
    <script src="../../App_Scripts/ScrollGridView/gridviewScroll.min.js" type="text/javascript"></script>

     <script type="text/javascript">
        function gridviewScroll() {
                $('#<%=grvClientes.ClientID%>').gridviewScroll({
                    width: 595,
                    height: 180,
                    freezesize: 3,
                    arrowsize: 30,
                    varrowtopimg: '../../App_Scripts/ScrollGridView/Images/arrowvt.png',
                    varrowbottomimg: '../../App_Scripts/ScrollGridView/Images/arrowvb.png',
                    harrowleftimg: '../../App_Scripts/ScrollGridView/Images/arrowhl.png',
                    harrowrightimg: '../../App_Scripts/ScrollGridView/Images/arrowhr.png',
                    headerrowcount: 1
                });
        }

        function pageLoad() {
             //if(!isPostback)
             //gridviewScroll();
        }

    </script>


<%--
    El gridview deberá mostrar sólo cinco elementos y si el dataset tiene más elementos el grid deberá mostrar un scrollbar automáticamente
    
    --%>
    <asp:HiddenField ID="hdfIndiceFila" runat="server" />

                        <asp:GridView ID="grvClientes" runat="server" 
                            ShowHeader="True"
                            AllowSorting="True" 
                            CssClass="grvResultadoConsultaCss" 
                            ShowFooter="False" 
                            Width="100%"
                            ShowHeaderWhenEmpty="True" 
                            AllowPaging="False"
                            PageSize="5" 
                            OnRowCommand="grvClientes_RowCommand" CommandName="SeleccionarCliente"
                            OnRowDataBound="grvClientes_RowDataBound" >

                            <HeaderStyle HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:RadioButton ID="RadioButton1" runat="server" 
                                            AutoPostBack="true"
                                            ToolTip="SELECCIONAR CLIENTE"
                                            OnCheckedChanged="RadioButton1_CheckedChanged"
                                            />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="25px" BackColor="#ebecec"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="25px"></HeaderStyle>
                                </asp:TemplateField>
                                
                                <%--<asp:TemplateField HeaderText="Cliente" SortExpression="Cliente">
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
                                </asp:TemplateField>      --%>                          
                                
                            </Columns>
                            <PagerStyle CssClass="grvPaginacionScroll" />
                        </asp:GridView>

    <table>
        <tr>
            <td class="etiqueta centradoMedio" style="width: 15%;">

                <asp:Button ID="btnAceptar" runat="server"
                    CssClass="boton bg-color-azulOscuro fg-color-blanco"
                    Text="GUARDAR" Style="margin: 0 0 0 0;" ToolTip="GUARDAR" OnClick="btnAceptar_Click"/>
                
                <asp:Button ID="btnCancelar" runat="server"
                    CssClass="boton bg-color-azulOscuro fg-color-blanco"
                    Text="CANCELAR" Style="margin: 0 0 0 0;" ToolTip="CANCELAR" />

            </td>
        </tr>
    </table>
