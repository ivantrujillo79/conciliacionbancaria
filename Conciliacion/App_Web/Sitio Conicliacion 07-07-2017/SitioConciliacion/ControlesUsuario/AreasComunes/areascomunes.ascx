<%@ Control Language="C#" AutoEventWireup="true" CodeFile="areascomunes.ascx.cs" Inherits="ControlesUsuario_AreasComunes_areascomunes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<div style="max-height:500px;overflow:auto; text-align:left">
    <p style ="height:20px; border-style:solid;  border-width: 1px; vertical-align:middle" >
        Listado de documentos por cliente padre
    </p>
    <p style ="height:30px">
        <asp:Label ID ="lblClientePadre" runat="server">Prueba</asp:Label>
    </p>
     <p style ="height:50px">
        <asp:Label ID ="Label1" runat="server">Prueba</asp:Label>
    </p>
    <p>
        <asp:HiddenField ID="rgSeleccionado" runat="server" />
        <asp:GridView ID="gwPagos" runat="server" CellPadding="20" ForeColor="#333333" GridLines="None"
            CssClass="grvResultadoConsultaCss" BorderStyle="Solid" EmptyDataText="Sin pagos" AutoGenerateColumns="False">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                 <asp:TemplateField>
                   <ItemTemplate>
                        <asp:RadioButton ID="RadioButton1" Text="" OnCheckedChanged="rbSelector_CheckedChanged" AutoPostBack="true" GroupName="Apply" runat="server"></asp:RadioButton>
                   </ItemTemplate>
                  </asp:TemplateField>
                <asp:BoundField DataField="FSuministro" HeaderText="FSuministro" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="Monto" DataFormatString="{0:c}" HeaderText="Monto" />
                <asp:BoundField DataField="Nombre" HeaderText="Nom. Cliente" />
                <asp:BoundField DataField="Concepto" HeaderText="Concepto" />
                <asp:BoundField DataField="Factura" HeaderText="Factura" />
                <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                <asp:BoundField DataField="pedidoreferencia" HeaderText="P. Referencia" />
            </Columns>
           
        </asp:GridView>

     


    </p>
</div>
