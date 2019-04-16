<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="EdoCtaPalabrasClave.aspx.cs" Inherits="EdoCtaPalabrasClave" %>

<%@ Register Src="~/ControlesUsuario/EstadoCuentaPalabrasClave/wucEdoCtaPalabrasClave.ascx" TagPrefix="uc1" TagName="wucEdoCtaPalabrasClave" %>




<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" Runat="Server" >


    <uc1:wucEdoCtaPalabrasClave runat="server" ID="wucEdoCtaPalabrasClave" />





     </asp:Content>

