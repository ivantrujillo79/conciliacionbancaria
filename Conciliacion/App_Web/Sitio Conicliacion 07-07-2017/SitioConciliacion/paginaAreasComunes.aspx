<%@ Page Title=""  Language="C#"  MasterPageFile="~/Principal.master"  AutoEventWireup="true" CodeFile="paginaAreasComunes.aspx.cs" Inherits="paginaAreasComunes" %>

<%@ Register Src="~/ControlesUsuario/AreasComunes/areascomunes.ascx" TagPrefix="ControlUsuario" TagName="AreasComunesControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="contenidoPrincipal" runat="server">

       <div><asp:TextBox ID="txtClientePadre" runat="server"></asp:TextBox>
        <asp:Button ID="btnBuscaClientePadre" runat="server" Text="Button" OnClick="btnBuscaClientePadre_Click" /></div>
   
    <div>
        <ControlUsuario:areascomunesControl runat="server" ID="pnlAreascomunes" />       
    </div>

<%--    <div>
        <ControlUsuario:wucAreasComunes runat="server" id="wucAreasComunes" />

    </div>--%>
 
   
</asp:Content>
