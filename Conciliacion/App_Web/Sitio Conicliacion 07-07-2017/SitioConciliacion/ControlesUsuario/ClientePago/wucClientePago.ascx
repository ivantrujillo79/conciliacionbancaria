﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucClientePago.ascx.cs" Inherits="ControlesUsuario_ClientePago_wucClientePago" %>

    <!-- Script se utiliza para el Scroll del GridView-->
<%--    <link href="../../App_Scripts/ScrollGridView/GridviewScroll.css" rel="stylesheet"type="text/css" />
    <script src="../../App_Scripts/ScrollGridView/gridviewScroll.min.js" type="text/javascript"></script>--%>

     <script type="text/javascript">
        <%--function gridviewScroll() {
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
        }--%>

         function SelectSingleRadiobutton(rdbtnid) {
             debugger;
             var button = $('#grvClientes').find('RadioButton1');

             //var rdBtn = document.getElementById(rdbtnid);
             //var rdBtnList = document.getElementsByTagName("input");
             //for (i = 0; i < rdBtnList.length; i++) {
             //    if (rdBtnList[i].type == "radio" && rdBtnList[i].id != rdBtn.id) {
             //        rdBtnList[i].checked = false;
             //    }
             //}
         }

         function pageLoad() {
             SelectSingleRadiobutton(0);
        }

    </script>


<%--
    El gridview deberá mostrar sólo cinco elementos y si el dataset tiene más elementos el grid deberá mostrar un scrollbar automáticamente
    
    --%>
    <asp:HiddenField ID="hdfIndiceFila" runat="server" />
    <asp:HiddenField ID="hdfClienteSeleccionado" runat="server" />

    <table>
        <tr>
            <td class="etiqueta centradoMedio" style="width: 100%;">
            </td>
        </tr>

        <tr>
            <td class="etiqueta centradoMedio" style="width: 100%;">

                <div class="etiqueta centradoMedio" style="height:170px;width:800px;overflow:auto;">
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
                                        GroupName="Clientes"
                                        AutoPostBack="true"
                                        ToolTip="SELECCIONAR CLIENTE"
                                        OnCheckedChanged="RadioButton1_CheckedChanged" Checked="true"
                                        />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="25px" BackColor="#ebecec"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="25px"></HeaderStyle>
                            </asp:TemplateField>                                
                        </Columns>
                        <PagerStyle CssClass="grvPaginacionScroll" />
                    </asp:GridView>
                </div>

                <asp:Button ID="btnAceptar" runat="server"
                    CssClass="boton bg-color-azulOscuro fg-color-blanco"
                    Text="ACEPTAR" Style="margin: 0 0 0 0;" ToolTip="GUARDAR" OnClick="btnAceptar_Click"/>
                
                <asp:Button ID="btnCancelar" runat="server"
                    CssClass="boton bg-color-azulOscuro fg-color-blanco"
                    Text="CANCELAR" Style="margin: 0 0 0 0;" ToolTip="CANCELAR" OnClick="btnCancelar_Click" />

            </td>
        </tr>
    </table>
