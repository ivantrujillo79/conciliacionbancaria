<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.master" AutoEventWireup="true" CodeFile="InterBloqueo.aspx.cs" Inherits="InterBloqueo" %>

<%@ Register Src="~/ControlesUsuario/GestorBloqueos/wucInterbloqueosEstadoCuenta.ascx" TagPrefix="uc1" TagName="wucInterbloqueosEstadoCuenta" %>


<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">
    <uc1:wucInterbloqueosEstadoCuenta runat="server" ID="wucInterbloqueosEstadoCuenta" />

</asp:Content>

