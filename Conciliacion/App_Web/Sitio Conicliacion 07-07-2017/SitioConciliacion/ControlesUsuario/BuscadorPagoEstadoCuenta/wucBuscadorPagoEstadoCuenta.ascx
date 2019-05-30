<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucBuscadorPagoEstadoCuenta.ascx.cs" Inherits="ControlesUsuario_ClientePago_wucBuscadorPagoEstadoCuenta" %>
    
    <script src="/App_Scripts/jQueryScripts/jquery-3.2.1.min.js" type="text/javascript"></script>
    <script src="/App_Scripts/jQueryScripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="/../../App_Scripts/jQueryScripts/jquery.ui.datepicker-es.js" type="text/javascript"></script>
    <link href="/App_Scripts/jQueryScripts/css/custom-theme/jquery-ui-1.10.2.custom.min.css" rel="stylesheet" type="text/css" />

    <%--<script src="../App_Scripts/FuncionesGenerales.js" type="text/javascript"></script>
    <script src="../App_Scripts/Validaciones.js" type="text/javascript"></script>--%>
    <!-- Script se utiliza para el Scroll del GridView-->
    <%--<link href="../App_Scripts/ScrollGridView/GridviewScroll.css" rel="stylesheet" type="text/css" />
    <script src="../App_Scripts/ScrollGridView/gridviewScroll.min.js" type="text/javascript"></script>--%>
    <link href="/App_Themes/GasMetropolitanoSkin/Sitio.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        function pageLoad() {

            $("#<%= txtFinicio.ClientID%>").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd/mm/yy',
                numberOfMonths: 1,
                onClose: function (selectedDate) {
                    $("#<%=txtFfinal.ClientID%>").datepicker("option", "minDate", selectedDate);
                }
            });
            $("#<%=txtFfinal.ClientID%>").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                dateFormat: 'dd/mm/yy',
                changeYear: true,
                numberOfMonths: 1,
                onClose: function (selectedDate) {
                    $("#<%=txtFinicio.ClientID%>").datepicker("option", "maxDate", selectedDate);
                }
            });
        
        }

        function ValidNumDecimal(e) {
            var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
            return ((tecla > 47 && tecla < 58) || tecla == 46);
        }

        function CP_gridviewScroll() {
            $('#<%=grvPagoEstadoCuenta.ClientID%>').gridviewScroll({
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

        function activarDatePickers() {
            $("#<%= txtFinicio.ClientID%>").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                dateFormat: 'mm/dd/yy',
                numberOfMonths: 2,
                onClose: function (selectedDate) {
                    $("#<%= txtFfinal.ClientID%>").datepicker("option", "minDate", selectedDate);
                }
            });
            $("#<%= txtFfinal.ClientID%>").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                dateFormat: 'mm/dd/yy',
                numberOfMonths: 2,
                onClose: function (selectedDate) {
                    $("#<%= txtFinicio.ClientID%>").datepicker("option", "maxDate", selectedDate);
                }
            });
        }

    </script>

    <table style="width: 100%">
        <tr>
            <td style="width: 1%; padding-left:3px"> 
                <asp:CheckBox ID="chkBuscarEnEsta" Text="Buscar en esta conciliación" runat="server" />
            </td>
            <td style="width: 1%;"> 
                <asp:Label ID="Label2" runat="server" Text="Monto" CssClass="etiqueta fg-color-negro centradoMedio"></asp:Label>
                <asp:TextBox ID="txtMonto" runat="server" Width="150px" CssClass="cajaTextoPequeño" onkeypress="return ValidNumDecimal(event)" ></asp:TextBox>
            </td>
            <td style="width: 1%;"> 
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Width="150px" OnClick="btnBuscar_Click" />
            </td>
            <td style="width: 1%; padding-left:3px">
            </td>
            <td style="width: 1%;">
            </td>
        </tr>
        <tr>
            <td style="width: 1%; padding-left:3px"> 
                <asp:CheckBox ID="chkBuscaEnRetiros" Text="Buscar en retiros" runat="server" />
            </td>
            <td style="width: 1%;"> 
                <asp:Label ID="Label1" runat="server" Text="F.Inicio" CssClass="etiqueta fg-color-negro centradoMedio"></asp:Label>
                <asp:TextBox ID="txtFinicio" runat="server" Width="150px" CssClass="cajaTextoPequeño"></asp:TextBox>
            </td>
            <td style="width: 1%;"> 
            </td>
            <td style="width: 1%; padding-left:3px">
            </td>
            <td style="width: 1%;">
            </td>
        </tr>
        <tr>
            <td style="width: 1%; padding-left:3px"> 
                <asp:CheckBox ID="chkBuscarEnDepositos" Text="Buscar en depósitos" runat="server" />
            </td>
            <td style="width: 1%;"> 
                <asp:Label ID="Label3" runat="server" Text="F. Final" CssClass="etiqueta fg-color-negro centradoMedio"></asp:Label>
                <asp:TextBox ID="txtFfinal" runat="server" Width="150px" CssClass="cajaTextoPequeño"></asp:TextBox>
            </td>
            <td style="width: 1%;"> 
            </td>
            <td style="width: 1%; padding-left:3px">
            </td>
            <td style="width: 1%;">
            </td>
        </tr>
    </table>

    <table style="width:100%">
        <tr>
            <td class="etiqueta centradoMedio" style="width: 100%;">

                <div class="etiqueta centradoMedio" style="height:170px;overflow:auto;"> <!--width:800px-->
                    <asp:GridView ID="grvPagoEstadoCuenta" runat="server" 
                        AutoGenerateColumns="False" 
                        ShowHeader="True"
                        ShowHeaderWhenEmpty="True"
                        AllowSorting="True" 
                        CssClass="grvResultadoConsultaCss" 
                        ShowFooter="False" 
                        Width="100%"
                        AllowPaging="False"
                        PageSize="5"
                        DataKeyNames="AñoConciliacion,MesConciliacion,FolioConciliacion">
                        <EmptyDataTemplate>
                            <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="No se ha conciliado ninguna transacción."></asp:Label>
                        </EmptyDataTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:TemplateField HeaderText="AñoConciliacion" >
                                <ItemTemplate>
                                    <asp:Label ID="lblAñoConciliacion" runat="server" Text='<%# Eval("AñoConciliacion").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="MesConciliacion" >
                                <ItemTemplate>
                                    <asp:Label ID="lblMesConciliacion" runat="server" Text='<%# Eval("MesConciliacion").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="FolioConciliacion" >
                                <ItemTemplate>
                                    <asp:Label ID="lblFolioConciliacion" runat="server" Text='<%# Eval("FolioConciliacion").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="FolioExterno" >
                                <ItemTemplate>
                                    <asp:Label ID="lblFolioExterno" runat="server" Text='<%# Eval("FolioExterno").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Documento" >
                                <ItemTemplate>
                                    <asp:Label ID="lblDocumento" runat="server" Text='<%# Eval("Documento").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TransBan" >
                                <ItemTemplate>
                                    <asp:Label ID="lblTransBan" runat="server" Text='<%# Eval("TransBan").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FMovTransban" >
                                <ItemTemplate>
                                    <asp:Label ID="lblFMovTransban" runat="server" Text='<%# Eval("FMovTransban").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FOperacion" >
                                <ItemTemplate>
                                    <asp:Label ID="lblFOperacion" runat="server" Text='<%# Eval("FOperacion").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Retiro" >
                                <ItemTemplate>
                                    <asp:Label ID="lblRetiro" runat="server" Text='<%# Eval("Retiro").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Deposito" >
                                <ItemTemplate>
                                    <asp:Label ID="lblDeposito" runat="server" Text='<%# Eval("Deposito").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Concepto" >
                                <ItemTemplate>
                                    <asp:Label ID="lblConcepto" runat="server" Text='<%# Eval("Concepto").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Descripcion" >
                                <ItemTemplate>
                                    <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("Descripcion").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>
                            
                        </Columns>
                        <PagerStyle CssClass="grvPaginacionScroll" />
                    </asp:GridView>
                </div>

                <asp:Button ID="btnAceptar" runat="server"
                    CssClass="boton bg-color-azulOscuro fg-color-blanco"
                    Text="ACEPTAR" Style="margin: 0 0 0 0;" ToolTip="GUARDAR" OnClick="btnAceptar_Click"/>
                
            </td>
        </tr>
    </table>
