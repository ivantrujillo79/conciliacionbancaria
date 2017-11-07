<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true"
    CodeFile="ImportacionArchivos.aspx.cs" Inherits="ImportacionArchivos_ImportacionArchivos"
    Title="Importación Archivos" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ MasterType TypeName="Principal" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager runat="server" ID="ScriptManager1" AsyncPostBackTimeout="14400">
    </asp:ScriptManager>
    <script src="../App_Scripts/jQueryScripts/jquery.min.js" type="text/javascript"></script>
    <script src="../App_Scripts/jQueryScripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../App_Scripts/jQueryScripts/jquery.ui.datepicker-es.js" type="text/javascript"></script>
    <link href="../App_Scripts/jQueryScripts/css/custom-theme/jquery-ui-1.10.2.custom.min.css"
        rel="stylesheet" type="text/css" />
    <script src="../App_Scripts/FuncionesGenerales.js" type="text/javascript"></script>
    <script src="../App_Scripts/Validaciones.js" type="text/javascript"></script>
    <!-- Script se utiliza para el Scroll del GridView-->
    <link href="../App_Scripts/ScrollGridView/GridviewScroll.css" rel="stylesheet" type="text/css" />
    <script src="../App_Scripts/ScrollGridView/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../App_Themes/GasMetropolitanoSkin/Sitio.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function pageLoad() {
            //            gridviewScroll();
            $("#<%= txtFInicial.ClientID%>").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                dateFormat: 'mm/dd/yy',
                numberOfMonths: 2,
                onClose: function (selectedDate) {
                    $("#<%=txtFFinal.ClientID%>").datepicker("option", "minDate", selectedDate);
                }
            });
            $("#<%=txtFFinal.ClientID%>").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                dateFormat: 'mm/dd/yy',
                changeYear: true,
                numberOfMonths: 2,
                onClose: function (selectedDate) {
                    $("#<%=txtFInicial.ClientID%>").datepicker("option", "maxDate", selectedDate);
                }
            });
        }
    </script>
    <script src="../App_Scripts/jsUpdateProgress.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        var ModalProgress = '<%= ModalProgress.ClientID %>';        
    </script>
    <asp:UpdatePanel ID="updPrincipal" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td style="vertical-align: top; width: 50%">
                        <div class="datos-estilo">
                            <div class="titulo" style="margin-left: 0px">
                                <table width="100%" class="lineaHorizontal">
                                    <tr>
                                        <td style="width: 95%">
                                            Importación de Archivos
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/atras.png"
                                                PostBackUrl="~/Inicio.aspx" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="Detalle">
                                <div id="Columna1" style="float: left; width: 50%;" >
                                    <div class="etiqueta">
                                        Corporativo
                                    </div>
                                    <asp:DropDownList ID="cboCorporativo" runat="server" CssClass="dropDown" Font-Size="12px"
                                        AutoPostBack="True" OnSelectedIndexChanged="cboCorporativo_SelectedIndexChanged"
                                        Width="90%" OnDataBound="cboCorporativo_DataBound">
                                    </asp:DropDownList>
                                    <br />
                                    <div class="etiqueta">
                                        Banco
                                    </div>
                                    <asp:DropDownList ID="cboBancoFinanciero" runat="server" CssClass="dropDown" Font-Size="12px"
                                        AutoPostBack="True" OnSelectedIndexChanged="cboBancoFinanciero_SelectedIndexChanged"
                                        OnDataBound="cboBancoFinanciero_DataBound" Width="90%">
                                    </asp:DropDownList>
                                    <br />
                                    <div class="etiqueta">
                                        Cuenta
                                    </div>
                                    <asp:DropDownList ID="cboCuentaFinanciero" runat="server" CssClass="dropDown" Font-Size="12px"
                                        AutoPostBack="True" OnSelectedIndexChanged="cboCuentaFinanciero_SelectedIndexChanged"
                                        Width="90%">
                                    </asp:DropDownList>
                                    <br />
                                    <div class="etiqueta">
                                        Tipo Fuente
                                    </div>
                                    <asp:DropDownList ID="cboTipoFuenteInformacion" runat="server" CssClass="dropDown"
                                        Font-Size="12px" Width="90%">
                                    </asp:DropDownList>
                                    <div class="etiqueta">
                                        Sucursal
                                    </div>
                                    <asp:DropDownList ID="cboSucursal" runat="server" CssClass="dropDown" Font-Size="12px"
                                        OnDataBound="cboSucursal_DataBound" Width="90%" AutoPostBack="True"
                                        OnSelectedIndexChanged="cboSucursal_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <br />
                                    <div class="etiqueta">
                                        Año
                                    </div>
                                    <asp:DropDownList ID="cboAnio" runat="server" CssClass="dropDown" Font-Size="12px"
                                        OnDataBound="cboAnio_DataBound" Width="90%" AutoPostBack="True">
                                    </asp:DropDownList>
                                    <br />
                                     <br />
                                    <div class="centradoMedio" style="width: 100%">
                                        <asp:Button ID="btnCancelar" runat="server" CssClass="boton fg-color-blanco bg-color-grisClaro"
                                            Text="CANCELAR" ToolTip="Cancelar el guardado de datos" OnClick="btnCancelarDatos_Click" />
                                        <asp:Button ID="btnGuardar" runat="server" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                            Text="GUARDAR" ToolTip="Importar Archivo." OnClick="btnGuardarDatos_Click" />
                                        <asp:Button ID="btnGuardarAplicacion" runat="server" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                            Text="GUARDAR" ToolTip="Importar Archivo." OnClick="btnGuardarAplicacion_Click" />
                                    </div>
                                </div>
                                <div id="Columna2" style="float: right; width: 48%;" >
                                    <div class="etiqueta">
                                        Importar desde:
                                    </div>
                                    <div class="etiqueta">
                                        <asp:RadioButtonList ID="rdbSubirDesde" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                            CssClass="etiqueta" OnSelectedIndexChanged="rdbSubirDesde_SelectedIndexChanged">
                                            <asp:ListItem Selected="True">Archivo</asp:ListItem>
                                            <asp:ListItem Value="Aplicacion">Aplicación</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="Filtrado" style="width: 100%">
                                        <div class="datos-estilo">
                                            <asp:AsyncFileUpload ID="uploadFile" runat="server" ThrobberID="Throbber" ErrorBackColor="Red"
                                                UploadingBackColor="#66CCFF" OnUploadedComplete="Archivo_UploadedComplete" Width="485px" />
                                            <asp:Image ID="Throbber" runat="server" CssClass="icono bg-color-blanco" Height="40px"
                                                ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/LoadPage.gif" Width="40px"
                                                Style="display: none" />
                                            <div style="width: 100%"  id ="divAplicacion" runat="server">
                                                <table style="width: 100%" id="tblFIFF" runat="server">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblFInicial" runat="server" Text="Fecha Inicial" CssClass="etiqueta"></asp:Label>
                                                            <asp:TextBox ID="txtFInicial" runat="server" CssClass="cajaTexto" Style="width: 97%"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 1%"></td>
                                                        <td>
                                                            <asp:Label ID="lblFFinal" runat="server" Text="Fecha Final" CssClass="etiqueta"></asp:Label>
                                                            <asp:TextBox ID="txtFFinal" runat="server" CssClass="cajaTexto" Style="width: 97%"></asp:TextBox>
                                                        </td>
                                                    </tr>                                            
                                                    
                                                </table>
                                                <div style="width: 100%">
                                                    <asp:Label  Width="97%" ID="lblOrigenAplicacion" runat="server" Text="Origen de Información" CssClass="etiqueta"></asp:Label>
                                                    <br />
                                                    <asp:GridView Width="97%"  ID="listadoOrigenes" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None"  >
                                                        
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Seleccionar">
                                                                    <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSeleccionar"  runat="server" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="20px"/>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                                                        </Columns>
                                                        
                                                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                                                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                                        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                        <SortedAscendingHeaderStyle BackColor="#594B9C" />
                                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                        <SortedDescendingHeaderStyle BackColor="#33276A" />
                                                        
                                                    </asp:GridView>
                                                    
                                                </div>
                                            </div>
                                    </div>
                                    
                                        
                                    
                                    <br />
                                </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="panelBloqueo" runat="server" AssociatedUpdatePanelID="updPrincipal">
        <ProgressTemplate>
            <asp:Image ID="imgLoad" runat="server" CssClass="icono bg-color-blanco" Height="40px"
                ImageUrl="~/App_Themes/GasMetropolitanoSkin/Imagenes/LoadPage.gif" Width="40px" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ModalPopupExtender ID="ModalProgress" runat="server" PopupControlID="panelBloqueo"
        BackgroundCssClass="ModalBackground" TargetControlID="panelBloqueo">
    </asp:ModalPopupExtender>
</asp:Content>
