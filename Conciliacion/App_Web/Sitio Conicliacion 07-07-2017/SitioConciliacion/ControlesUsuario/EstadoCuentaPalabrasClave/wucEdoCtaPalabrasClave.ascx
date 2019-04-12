<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucEdoCtaPalabrasClave.ascx.cs" Inherits="ControlesUsuario_EstadoCuentaPalabrasClave_wucEdoCtaPalabrasClave" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>



<script type="text/javascript">

    $(document).ready(function(){
        $("#<%=ddlBanco.ClientID%>").change(function () {
            CargaCuentasBanco();
            CargaPalabrasClave();
        });

           $("#<%=ddlCuenta .ClientID%>").change(function () {
            CargaPalabrasClave();           
        });

        $("#<%=ddlTipoCobro.ClientID%>").change(function () {
            CargaPalabrasClave();           
        });

        $("#btnAceptar").click(function(){
            GuardarPalabrasClave();
     });

       
});



     $.ajax({
                type: "POST",
                url: "EdoCtaPalabrasClave.aspx/CargaBancos",
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {// función que va a ejecutar si el pedido fue exitoso
            
              var datos=JSON.parse(data.d)             
              var s = '<option value="-1">Seleccione</option>';  
                                
               for (var i = 0; i < datos.length; i++) {  
                   s += '<option value="' + datos[i].Banco + '">' + datos[i].Descripcion + '</option>';  
                    }                 

            
                    $("#<%=ddlBanco.ClientID%>").html(s); 
                },
                error: function (r) {
                    alert(r.responseText);
                },
                failure: function (r) {
                    alert(r.responseText);
                }
    });

    
     $.ajax({
                type: "POST",
                url: "EdoCtaPalabrasClave.aspx/CargaTipoCobro",
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {// función que va a ejecutar si el pedido fue exitoso
            
              var datos=JSON.parse(data.d)             
              var s = '<option value="-1">Seleccione</option>';  
                                
               for (var i = 0; i < datos.length; i++) {  
                   s += '<option value="' + datos[i].IdTipoCobro + '">' + datos[i].Descripcion + '</option>';  
                    }                 

            
                    $("#<%=ddlTipoCobro.ClientID%>").html(s); 
                },
                error: function (r) {
                    alert(r.responseText);
                },
                failure: function (r) {
                    alert(r.responseText);
                }
    });


        
     $.ajax({
                type: "POST",
                url: "EdoCtaPalabrasClave.aspx/CargaColumnaDestino",
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {// función que va a ejecutar si el pedido fue exitoso
            
              var datos=JSON.parse(data.d)             
              var s = '<option value="-1">Seleccione</option>';  
                                
               for (var i = 0; i < datos.length; i++) {  
                   s += '<option value="' + datos[i].DscColumnaDestino + '">' + datos[i].DscColumnaDestino + '</option>';  
                    }                 

            
                    $("#<%=ddlColumna.ClientID%>").html(s); 
                },
                error: function (r) {
                    alert(r.responseText);
                },
                failure: function (r) {
                    alert(r.responseText);
                }
    });



    

    function CargaCuentasBanco() {

      
        
      $.ajax({
                type: "POST",
                url: "EdoCtaPalabrasClave.aspx/CargaCuentasBanco",
                data: "{Banco: '" + $("#<%=ddlBanco.ClientID%>").val() + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {// función que va a ejecutar si el pedido fue exitoso
            
              var datos=JSON.parse(data.d)             
              var s = '<option value="-1">Seleccione</option>';  
                                
               for (var i = 0; i < datos.length; i++) {  
                   s += '<option value="' + datos[i].NumeroCuenta + '">' + datos[i].NumeroCuenta + '</option>';  
                    }  
 
                   

            
                    $("#<%=ddlCuenta.ClientID%>").html(s); 
                },
                error: function (r) {
                    alert(r.responseText);
                },
                failure: function (r) {
                    alert(r.responseText);
                }
            });
    }

    function CargaPalabrasClave() {

        if ($("#<%=ddlBanco.ClientID%>").val() == "-1")
            {
            
              return;
        }

        if ($("#<%=ddlCuenta.ClientID%>").val() == "-1")
        {
             
              return;
            }

           
        if ($("#<%=ddlTipoCobro.ClientID%>").val() == "-1")
        {
             
              return;
            }

         $("#<%=TxtPalabrasClave.ClientID%>").val(''); 


        var objParametros = {};
        objParametros.banco = $.trim($("#<%=ddlBanco.ClientID%>").val());
        objParametros.cuentabanco = $.trim($("#<%=ddlCuenta.ClientID%>").val());
        objParametros.tipocobro = $.trim($("#<%=ddlTipoCobro.ClientID%>").val());

         $.ajax({
                type: "POST",
                url: "EdoCtaPalabrasClave.aspx/ConsultarPalabrasClave",
                data: JSON.stringify(objParametros),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {// función que va a ejecutar si el pedido fue exitoso
            
              var datos=JSON.parse(data.d)             
              var s = '';  
                                
               for (var i = 0; i < datos.length; i++) {  
                   s += ' ' + datos[i].PalabraClave ;  
                    }  
 
                   
                    if (s!= '')
                    {
                        $("#<%=TxtPalabrasClave.ClientID%>").val(s); 
                    }
                    else
                    {
                        alert('¡No existe información con los datos proporcionados!');
                    }

                },
                error: function (r) {
                    alert(r.responseText);
                },
                failure: function (r) {
                    alert(r.responseText);
                }
            });

    }

    function GuardarPalabrasClave() {

             if ($("#<%=ddlBanco.ClientID%>").val() == "-1")
            {
            alert('¡Seleccione un Banco!');
              return;
        }

        if ($("#<%=ddlCuenta.ClientID%>").val() == "-1")
        {
             alert('¡Seleccione un cuenta!');
              return;
            }

           
        if ($("#<%=ddlTipoCobro.ClientID%>").val() == "-1")
        {
             alert('¡Seleccione un tipo de cobro!');
              return;
        }

           if ($("#<%=ddlColumna.ClientID%>").val() == "-1")
        {
             alert('¡Seleccione una columna!');
              return;
        }

       if ($("#<%=TxtPalabrasClave.ClientID%>").val() == "")
        {
             alert('¡Capture palabras clave!');
              return;
            }





        var objParametros = {};
        objParametros.banco = $.trim($("#<%=ddlBanco.ClientID%>").val());
        objParametros.cuentabanco = $.trim($("#<%=ddlCuenta.ClientID%>").val());
        objParametros.tipocobro = $.trim($("#<%=ddlTipoCobro.ClientID%>").val());
        objParametros.palabraclave = $.trim($("#<%=TxtPalabrasClave.ClientID%>").val());
        objParametros.columnadestino = $.trim($("#<%=ddlColumna.ClientID%>").val());

         $.ajax({
                type: "POST",
                url: "EdoCtaPalabrasClave.aspx/GuardarPalabrasClave",
                data: JSON.stringify(objParametros),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {// función que va a ejecutar si el pedido fue exitoso
            
                    var res = JSON.parse(data.d)   

                    if (res = 'true')
                        alert('Palabra clave registrada exitosamente');  
 
                   

                },
                error: function (r) {
                    alert(r.responseText);
                },
                failure: function (r) {
                    alert(r.responseText);
                }
            });


    }




    





</script>


<style type="text/css">
    .auto-style1 {
        height: 24px;
    }
    .auto-style2 {
        font-weight: bold;
        font-size: medium;
        color: #1D1D1D;
        vertical-align: middle;
    }
    .auto-style3 {
        width: 308px;
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
    <asp:Label ID="Label1" runat="server" Text="Asociación de forma de pago a palabras clave" CssClass="etiqueta fg-color-negro centradoMedio" Font-Bold="True" Font-Size="Medium"></asp:Label>
        
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
            <td class="auto-style1" style="text-align:right"><asp:Label ID="Label2" runat="server" Text="Banco" CssClass="etiqueta fg-color-negro centradoMedio" Font-Bold="True" Font-Size="Medium"></asp:Label></td>
            <td style="text-align:left" class="auto-style1">
                <asp:DropDownList ID="ddlBanco" runat="server" AutoPostBack="false" Width="150px"  >
                   
                </asp:DropDownList>
            </td>
            <td style="text-align:right" class="auto-style1"><asp:Label ID="Label5" runat="server" Text="Cuenta" CssClass="etiqueta fg-color-negro centradoMedio" Font-Bold="True" Font-Size="Medium"></asp:Label></td>
            <td style="text-align:left" class="auto-style1">
                <asp:DropDownList ID="ddlCuenta" runat="server"  AutoPostBack="false" Width="150px" >
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
            <td style="text-align:right" ><asp:Label ID="Label3" runat="server" Text="Tipo Cobro" CssClass="etiqueta fg-color-negro centradoMedio" Font-Bold="True" Font-Size="Medium"></asp:Label></td>
            <td style="text-align:left">
                <asp:DropDownList ID="ddlTipoCobro" runat="server"  AutoPostBack="false" Width="150px" >
                    
                </asp:DropDownList>
            </td>
            <td style="text-align:right"><asp:Label ID="Label4" runat="server" Text="Columna" CssClass="etiqueta fg-color-negro centradoMedio" Font-Bold="True" Font-Size="Medium"></asp:Label></td>
            <td style="text-align:left">
                <asp:DropDownList ID="ddlColumna" runat="server" AutoPostBack="false" Width="150px">
                   
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
            
                    <td style="text-align:center" colspan="4"><asp:Label ID="Label6" runat="server" Text="Forma de Pago Elegida:Efectivo" CssClass="etiqueta fg-color-negro centradoMedio" Font-Bold="True" Font-Size="Medium"></asp:Label></td>
             
       </tr>
         <tr>
            <td colspan="4" ></td>
                
                   
       </tr>

         <tr>
            <td colspan="4" >&nbsp;</td>
                
                   
       </tr>

        <tr>
            <td style="text-align:center" colspan="4" >
                  <span class="auto-style2">Palabra Clave Elegida:</span>
                  <input  type="text" name="name" runat="server"  value="" data-role="tagsinput" id="TxtPalabrasClave" class="auto-style3"  />           
            </td>          
                   
       </tr>
        
        
         <tr>
            
                    <td >
                        &nbsp;</td>
               <td > &nbsp;</td>
              <td >&nbsp;</td>
              <td >&nbsp;</td>
             
       </tr>
        
        
         <tr>
            
                    <td colspan="2" style="text-align:right"  >
                        <input type="button" name="btnAceptar" value="Aceptar" style="width:150px" onclick="return GuardarPalabrasClave();" /> </td>
              <td colspan="2"  style="text-align:left" > <asp:Button ID="btnCancelar" runat="server" AutoPostBack="false" Text="Cancelar" Width="150px"  /> </td>
             
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