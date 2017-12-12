<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsultaSaldoAFavor.aspx.cs" Inherits="ReportesConciliacion_ConsultaSaldoAFavor" MasterPageFile="~/Principal.master" %>
<%@ Register Src="~/ControlesUsuario/SaldosAFavor/wucSaldoAFavor.ascx" TagPrefix="uc1" TagName="wucSaldoAFavor" %>


        
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    Reporte Tesoreria I
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager runat="server" ID="smDetalleConciliacion" AsyncPostBackTimeout="600"
        EnableScriptGlobalization="True">
    </asp:ScriptManager>
    
    <uc1:wucSaldoAFavor runat="server" ID="wucSaldoAFavor" />

</asp:Content>


    


