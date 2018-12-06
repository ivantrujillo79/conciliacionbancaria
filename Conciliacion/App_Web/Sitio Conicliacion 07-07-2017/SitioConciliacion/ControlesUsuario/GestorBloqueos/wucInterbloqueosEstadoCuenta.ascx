<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucInterbloqueosEstadoCuenta.ascx.cs" Inherits="ControlesUsuario_GestorBloqueos_wucInterbloqueosEstadoCuenta" %>
 
<script type="text/javascript">

     function checkAll(objRef)

{

    var GridView = objRef.parentNode.parentNode.parentNode;

    var inputList = GridView.getElementsByTagName("input");

    for (var i=0;i<inputList.length;i++)

    {

        //Get the Cell To find out ColumnIndex

        var row = inputList[i].parentNode.parentNode;

        if(inputList[i].type == "checkbox"  && objRef != inputList[i])

        {

            if (objRef.checked)

            {

                //If the header checkbox is checked

                //check all checkboxes

                //and highlight all rows

                row.style.backgroundColor = "aqua";

                inputList[i].checked=true;

            }

            else

            {

                //If the header checkbox is checked

                //uncheck all checkboxes

                //and change rowcolor back to original

                if(row.rowIndex % 2 == 0)

                {

                   //Alternating Row Color

                   row.style.backgroundColor = "#C2D69B";

                }

                else

                {

                   row.style.backgroundColor = "white";

                }

                inputList[i].checked=false;

            }

        }

    }

}
</script>


<style type="text/css">
    .auto-style3 {
        width: 1%;
        height: 24px;
    }
</style>
 

    <fieldset style="border:double">
    <legend style="font:bold;" >Filtros:</legend>
   
  

    <table style="width: 100%">
    <tr>
        <td style="padding-left:3px" class="auto-style3"> 
            <asp:Label ID="lblArchivo" runat="server" CssClass="etiqueta fg-color-negro" Font-Size="0.97em" Text="Corporativo: "
                        style="display:inline-block;" Font-Bold="True"/>
            &nbsp;</td>
        <td style="padding-left:3px" class="auto-style3"> 

            <asp:DropDownList ID="ddlCorporativo" runat="server" OnSelectedIndexChanged="ddlCorporativo_SelectedIndexChanged" AutoPostBack="True" OnTextChanged="ddlCorporativo_TextChanged" >

            </asp:DropDownList>

           </td>
        <td td style="padding-left:3px"  class="auto-style3"> 
            <asp:Label ID="lblArchivo0" runat="server" CssClass="etiqueta fg-color-negro" Font-Size="0.97em" style="display:inline-block;" Text="Sucursal: " Font-Bold="True" />
        </td>
        <td style="padding-left:3px" class="auto-style3">
            <asp:DropDownList ID="ddlSucursal" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td  style="padding-left:3px"  class="auto-style3">
            <asp:Label ID="lblArchivo1" runat="server" CssClass="etiqueta fg-color-negro" Font-Size="0.97em" style="display:inline-block;" Text="Año: " Font-Bold="True" />
        </td>

         <td style="padding-left:3px" class="auto-style3">
            <asp:DropDownList ID="ddlAnio" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAnio_SelectedIndexChanged">
            </asp:DropDownList>
        </td>

                <td  style="padding-left:3px"  class="auto-style3">
            <asp:Label ID="Label3" runat="server" CssClass="etiqueta fg-color-negro" Font-Size="0.97em" style="display:inline-block;" Text="Folio: " Font-Bold="True" />
        </td>

         <td style="padding-left:3px" class="auto-style3">
              <asp:DropDownList ID="ddlFolio" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFolio_SelectedIndexChanged">
            </asp:DropDownList>
        </td>

          <td  style="padding-left:3px"  class="auto-style3">
            <asp:Label ID="Label1" runat="server" CssClass="etiqueta fg-color-negro" Font-Size="0.97em" style="display:inline-block;" Text="Secuencia: " Font-Bold="True" />
        </td>

          <td style="padding-left:3px" class="auto-style3">
            <asp:DropDownList ID="ddlSecuencia" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSecuencia_SelectedIndexChanged">
            </asp:DropDownList>
        </td>

            <td  style="padding-left:3px"  class="auto-style3">
            <asp:Label ID="Label2" runat="server" CssClass="etiqueta fg-color-negro" Font-Size="0.97em" style="display:inline-block;" Text="Usuario: " Font-Bold="True" />
        </td>


             <td style="padding-left:3px" class="auto-style3">
            <asp:DropDownList ID="ddlUsuario" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUsuario_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>


        <tr>
            <td style="padding-left: 3px" class="auto-style3" colspan="12">


            </td>



        </tr>
          <tr>
            <td style="padding-left: 3px" class="auto-style3" colspan="12">


                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Liberar Bloqeos Seleccionados" Width="228px" />


            </td>



        </tr>

</table>

          </fieldset>

    <table style="width: 100%">
          <tr>
            <td style="padding-left: 3px;align-content:inherit; left: auto;text-align:left" class="auto-style3" colspan="12"  >


                <asp:CheckBox ID="ChkSelTodos" runat="server" Font-Bold="True" Text="Seleccionar todos los bloqueos" OnCheckedChanged="ChkSelTodos_CheckedChanged" AutoPostBack="True" />


            </td>



        </tr>

          <tr>
            <td style="padding-left: 3px" class="auto-style3" colspan="12">
                <asp:GridView runat="server" ID="grdBloqueos" AutoGenerateColumns="False"  AllowPaging="True" 
                ShowHeaderWhenEmpty="True" CssClass="grvResultadoConsultaCss" Width="100%" OnPageIndexChanged="grdBloqueos_PageIndexChanged" OnSelectedIendexChanged="grdBloqueos_SelectedIndexChanged"
                  pagesize="30" 
                    >
                <HeaderStyle HorizontalAlign="Center" />
                <Columns>                    
                    <asp:TemplateField HeaderText="Eliminar">
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkAgregar" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" BackColor="#ebecec"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="SessionId" HeaderText="SessionId" SortExpression="SessionId" Visible="False" />


                    <asp:BoundField DataField="Corporativo" HeaderText="Corporativo" SortExpression="Corporativo" />
                    <asp:BoundField DataField="Sucursal" HeaderText="Sucursal" SortExpression="Sucursal" />
                    <asp:BoundField DataField="Año" HeaderText="Año" SortExpression="Año" />
                    <asp:BoundField DataField="Folio" HeaderText="Folio" SortExpression="Año" />
                    <asp:BoundField DataField="Secuencia" HeaderText="Secuencia" SortExpression="Secuencia" />
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" />
                       <asp:BoundField DataField="Monto" HeaderText="Monto" SortExpression="Monto" />
                       <asp:BoundField DataField="InicioBloqueo" HeaderText="InicioBloqueo" SortExpression="InicioBloqueo" />
                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario" />

                     
                </Columns>
            </asp:GridView>

            </td>



        </tr>



</table>

      

    


 
