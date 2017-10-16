<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testUpload.aspx.cs" Inherits="Conciliacion_testUpload" %>

<%@ Register Src="~//ControlesUsuario/CargaManualExcelCyC/wucCargaManualExcelCyC.ascx" TagPrefix="uc1" TagName="WebUserControl" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WebUserControl runat="server" ID="wucCargaManualExcelCyC" />
    </div>
    </form>
</body>
</html>
