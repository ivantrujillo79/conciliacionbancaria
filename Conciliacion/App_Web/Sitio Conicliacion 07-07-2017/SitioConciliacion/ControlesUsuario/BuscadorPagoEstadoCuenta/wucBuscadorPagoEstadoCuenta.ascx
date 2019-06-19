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
                <asp:CheckBox ID="chkBuscarEnDepositos" Text="Buscar en interno" runat="server" />
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
                            <asp:TemplateField HeaderText="AñoCon" >
                                <ItemTemplate>
                                    <asp:Label ID="lblAñoConciliacion" runat="server" Text='<%# Eval("AñoConciliacion").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="MesCon" >
                                <ItemTemplate>
                                    <asp:Label ID="lblMesConciliacion" runat="server" Text='<%# Eval("MesConciliacion").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="FolioCon" >
                                <ItemTemplate>
                                    <asp:Label ID="lblFolioConciliacion" runat="server" Text='<%# Eval("FolioConciliacion").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Banco" >
                                <ItemTemplate>
                                    <asp:Label ID="lblBanco" runat="server" Text='<%# Eval("Banco").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="CuentaBancaria" >
                                <ItemTemplate>
                                    <asp:Label ID="lblCuentaBancaria" runat="server" Text='<%# Eval("CuentaBancaria").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="FolioExt" >
                                <ItemTemplate>
                                    <asp:Label ID="lblFolioExt" runat="server" Text='<%# Eval("FolioExterno").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="SecuenciaExt" >
                                <ItemTemplate>
                                    <asp:Label ID="lblSecuenciaExt" runat="server" Text='<%# Eval("SecuenciaExterno").ToString() %>'></asp:Label>
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
                            
                            <asp:TemplateField HeaderText="FOperacion Ext" >
                                <ItemTemplate>
                                    <asp:Label ID="lblFOperacionExt" runat="server" Text='<%# Eval("FOperacionExt").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Retiro Ext" >
                                <ItemTemplate>
                                    <asp:Label ID="lblRetiroExt" runat="server" Text='<%# Eval("RetiroExt").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Deposito Ext" >
                                <ItemTemplate>
                                    <asp:Label ID="lblDepositoExt" runat="server" Text='<%# Eval("DepositoExt").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ConceptoExt" >
                                <ItemTemplate>
                                    <asp:Label ID="lblConceptoExt" runat="server" Text='<%# Eval("ConceptoExt").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DescripcionExt" >
                                <ItemTemplate>
                                    <asp:Label ID="lblDescripcionExt" runat="server" Text='<%# Eval("DescripcionExt").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="FOperacion Int" >
                                <ItemTemplate>
                                    <asp:Label ID="lblFOperacionInt" runat="server" Text='<%# Eval("FOperacionInt").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Retiro Int" >
                                <ItemTemplate>
                                    <asp:Label ID="lblRetiroExt" runat="server" Text='<%# Eval("RetiroInt").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Deposito Int" >
                                <ItemTemplate>
                                    <asp:Label ID="lblDepositoInt" runat="server" Text='<%# Eval("DepositoInt").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ConceptoInt" >
                                <ItemTemplate>
                                    <asp:Label ID="lblConceptoInt" runat="server" Text='<%# Eval("ConceptoInt").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DescripcionInt" >
                                <ItemTemplate>
                                    <asp:Label ID="lblDescripcionInt" runat="server" Text='<%# Eval("DescripcionInt").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="MotivoNoConciliado" >
                                <ItemTemplate>
                                    <asp:Label ID="lblMotivoNoConciliado" runat="server" Text='<%# Eval("MotivoNoConciliado").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                            </asp:TemplateField>--%>
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
