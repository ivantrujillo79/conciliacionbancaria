<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucInterbloqueosEstadoCuenta.ascx.cs" Inherits="ControlesUsuario_GestorBloqueos_wucInterbloqueosEstadoCuenta" %>
 
<style type="text/css">
    .auto-style3 {
        width: 1%;
        height: 24px;
    }
</style>
 
<asp:Panel ID="Panel1" runat="server" GroupingText="Filtros" Width="1201px" BorderStyle="Solid">
    <table style="width: 100%">
    <tr>
        <td style="padding-left:3px" class="auto-style3"> 
            <asp:Label ID="lblArchivo" runat="server" CssClass="etiqueta fg-color-negro" Font-Size="0.97em" Text="Corporativo: "
                        style="display:inline-block;" Font-Bold="True"/>
            &nbsp;</td>
        <td style="padding-left:3px" class="auto-style3"> 

            <asp:DropDownList ID="ddlCorporativo" runat="server">
            </asp:DropDownList>

           </td>
        <td td style="padding-left:3px"  class="auto-style3"> 
            <asp:Label ID="lblArchivo0" runat="server" CssClass="etiqueta fg-color-negro" Font-Size="0.97em" style="display:inline-block;" Text="Sucursal: " Font-Bold="True" />
        </td>
        <td style="padding-left:3px" class="auto-style3">
            <asp:DropDownList ID="ddlSucursal" runat="server">
            </asp:DropDownList>
        </td>
        <td  style="padding-left:3px"  class="auto-style3">
            <asp:Label ID="lblArchivo1" runat="server" CssClass="etiqueta fg-color-negro" Font-Size="0.97em" style="display:inline-block;" Text="Año: " Font-Bold="True" />
        </td>

         <td style="padding-left:3px" class="auto-style3">
            <asp:DropDownList ID="ddlAnio" runat="server">
            </asp:DropDownList>
        </td>

                <td  style="padding-left:3px"  class="auto-style3">
            <asp:Label ID="Label3" runat="server" CssClass="etiqueta fg-color-negro" Font-Size="0.97em" style="display:inline-block;" Text="Folio: " Font-Bold="True" />
        </td>

         <td style="padding-left:3px" class="auto-style3">
              <asp:DropDownList ID="ddFolio" runat="server">
            </asp:DropDownList>
        </td>

          <td  style="padding-left:3px"  class="auto-style3">
            <asp:Label ID="Label1" runat="server" CssClass="etiqueta fg-color-negro" Font-Size="0.97em" style="display:inline-block;" Text="Secuencia: " Font-Bold="True" />
        </td>

          <td style="padding-left:3px" class="auto-style3">
            <asp:DropDownList ID="ddlSecuencia" runat="server">
            </asp:DropDownList>
        </td>

            <td  style="padding-left:3px"  class="auto-style3">
            <asp:Label ID="Label2" runat="server" CssClass="etiqueta fg-color-negro" Font-Size="0.97em" style="display:inline-block;" Text="Usuario: " Font-Bold="True" />
        </td>


             <td style="padding-left:3px" class="auto-style3">
            <asp:DropDownList ID="ddlUsuario" runat="server">
            </asp:DropDownList>
        </td>
    </tr>


        <tr>
            <td style="padding-left: 3px" class="auto-style3" colspan="12">


            </td>



        </tr>
          <tr>
            <td style="padding-left: 3px" class="auto-style3" colspan="12">


            </td>



        </tr>


          <tr>
            <td style="padding-left: 3px;align-content:inherit; left: auto;text-align:left" class="auto-style3" colspan="12"  >


                <asp:CheckBox ID="ChkSelTodos" runat="server" Font-Bold="True" Text="Seleccionar todos los bloqueos" />


            </td>



        </tr>

          <tr>
            <td style="padding-left: 3px" class="auto-style3" colspan="12">
                <asp:GridView runat="server" ID="grvPagares" AutoGenerateColumns="False" 
                ShowHeaderWhenEmpty="True" CssClass="grvResultadoConsultaCss" Width="100%">
                <HeaderStyle HorizontalAlign="Center" />
                <Columns>                    
                    <asp:TemplateField HeaderText="Eliminar">
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkAgregar" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" BackColor="#ebecec"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Corporativo" SortExpression="Corporativo">
                        <ItemTemplate>
                            <asp:Label ID="lblObservacion" runat="server" Text='<%# Eval("") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Sucursal" SortExpression="Sucursal">
                        <ItemTemplate>
                            <asp:Label ID="lblObservacion" runat="server" Text='<%# Eval("") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:TemplateField>


                          <asp:TemplateField HeaderText="Año" SortExpression="Año">
                        <ItemTemplate>
                            <asp:Label ID="lblObservacion" runat="server" Text='<%# Eval("") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:TemplateField>


                        <asp:TemplateField HeaderText="Folio" SortExpression="Folio">
                        <ItemTemplate>
                            <asp:Label ID="lblObservacion" runat="server" Text='<%# Eval("") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:TemplateField>


                   <asp:TemplateField HeaderText="Secuencia" SortExpression="Secuencia">
                        <ItemTemplate>
                            <asp:Label ID="lblObservacion" runat="server" Text='<%# Eval("") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:TemplateField>


                    
                   <asp:TemplateField HeaderText="Descripción" SortExpression="Descripcion">
                        <ItemTemplate>
                            <asp:Label ID="lblObservacion" runat="server" Text='<%# Eval("") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:TemplateField>


                         <asp:TemplateField HeaderText="Monto" SortExpression="Monto">
                        <ItemTemplate>
                            <asp:Label ID="lblObservacion" runat="server" Text='<%# Eval("") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:TemplateField>

                              <asp:TemplateField HeaderText="Inicio Bloqueo" SortExpression="InicioBloqueo">
                        <ItemTemplate>
                            <asp:Label ID="lblObservacion" runat="server" Text='<%# Eval("") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:TemplateField>


                   <asp:TemplateField HeaderText="Usuario" SortExpression="Usuario">
                        <ItemTemplate>
                            <asp:Label ID="lblObservacion" runat="server" Text='<%# Eval("") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:TemplateField>
                  
                </Columns>
            </asp:GridView>

            </td>



        </tr>


</table>

    
</asp:Panel>

 
