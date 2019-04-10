<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucEdoCtaPalabrasClave.ascx.cs" Inherits="ControlesUsuario_EstadoCuentaPalabrasClave_wucEdoCtaPalabrasClave" %>
<style type="text/css">
    .auto-style1 {
        height: 24px;
    }
</style>
<div id="PalabrasCalve" >
    <table style="width: 100%">
        
        <tr>
<td colspan="4"  style="width: 8.3%; text-align:center; color:white;font-weight: bold;" >
    &nbsp;</td>
         </tr>

        <tr>
<td colspan="4"  style="width: 8.3%; text-align:center; color:white;font-weight: bold;" >
    <asp:Label ID="Label1" runat="server" Text="Asociación de forma de pago a palabras clave" CssClass="etiqueta fg-color-negro centradoMedio"></asp:Label>
        
        </td>
         </tr>

        <tr>
            <td>&nbsp;</td>
            <td style="align-items:flex-start">
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1"><asp:Label ID="Label2" runat="server" Text="Banco" CssClass="etiqueta fg-color-negro centradoMedio" Font-Bold="True" Font-Size="Medium"></asp:Label></td>
            <td style="align-items:flex-start" class="auto-style1">
                <asp:DropDownList ID="ddlBanco" runat="server" AutoPostBack="True" Width="150px" >
                    <asp:ListItem>Banamex</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="auto-style1"><asp:Label ID="Label5" runat="server" Text="Cuenta" CssClass="etiqueta fg-color-negro centradoMedio" Font-Bold="True" Font-Size="Medium"></asp:Label></td>
            <td class="auto-style1">
                <asp:DropDownList ID="ddlCuenta" runat="server"  AutoPostBack="True" Width="150px" >
                    <asp:ListItem>40981</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td><asp:Label ID="Label3" runat="server" Text="Tipo Cobro" CssClass="etiqueta fg-color-negro centradoMedio" Font-Bold="True" Font-Size="Medium"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlTipoCobro" runat="server"  AutoPostBack="True" Width="150px" >
                    <asp:ListItem>Efectivo</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td><asp:Label ID="Label4" runat="server" Text="Columna" CssClass="etiqueta fg-color-negro centradoMedio" Font-Bold="True" Font-Size="Medium"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlColumna" runat="server" AutoPostBack="True" Width="150px">
                    <asp:ListItem>Descripción</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            
                    <td colspan="4"></td>
             
       </tr>
        <tr>
            <td colspan="4" ></td>
                
                   
       </tr>
        <tr>
            <td colspan="4" >&nbsp;</td>
                
                   
       </tr>
        <tr>
            
                    <td colspan="4"><asp:Label ID="Label6" runat="server" Text="Forma de Pago Elejida:Efectivo" CssClass="etiqueta fg-color-negro centradoMedio" Font-Bold="True" Font-Size="Medium"></asp:Label></td>
             
       </tr>
         <tr>
            <td colspan="4" ></td>
                
                   
       </tr>

         <tr>
            <td colspan="4" >&nbsp;</td>
                
                   
       </tr>

        <tr>
            <td colspan="2" >
                  Palabras Clave:
                  <input type="text" name="name" value="PRUEBA" data-role="tagsinput"  />           
            </td>          
                   
            <td colspan="2" >
                  &nbsp;</td>          
                   
       </tr>
        
        
         <tr>
            
                    <td >
                        &nbsp;</td>
               <td > &nbsp;</td>
              <td >&nbsp;</td>
              <td >&nbsp;</td>
             
       </tr>
        
        
         <tr>
            
                    <td >
                        <asp:Button ID="btAceptar" runat="server" Text="Aceptar" Width="150px"  /></td>
               <td > <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Width="150px"  /> </td>
              <td ></td>
              <td ></td>
             
       </tr>
        
        
         <tr>
            
                    <td >
                        &nbsp;</td>
               <td > &nbsp;</td>
              <td >&nbsp;</td>
              <td >&nbsp;</td>
             
       </tr>
    </table>

    

</div>