<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucCargaManualExcelCyC.ascx.cs" Inherits="Conciliacion_WebUserControl" %>

<!--    POPUP CARGA ARCHIVO    -->
<asp:HiddenField runat="server" ID="hdfCargaArchivo" />
                     
    <table>
        <tr class="bg-color-grisOscuro">
            <td style="padding: 5px 5px 5px 5px" class="etiqueta">
                <div class="floatIzquierda">
                    <asp:ImageButton runat="server" ID="btnCerrarCargaArchivo" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png"
                        CssClass="iconoPequeño bg-color-rojo" />
                </div>
            </td>
        </tr>
        <tr>
            <td style="padding:10px 0px 0px 10px;">
                <asp:FileUpload ID="fupSeleccionar" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="padding-left:6px;"> 
                <asp:Button ID="btnSubirArchivo" runat="server" CssClass="boton fg-color-blanco bg-color-azulClaro"
                    Text="Subir archivo" OnClick="btnSubirArchivo_Click" />               
            </td>
        </tr>
        <tr>
            <td style="padding-left:10px;">
                <asp:Label ID="lblArchivo" runat="server" CssClass="etiqueta " Font-Size="1em" Text="Archivo: " />
            </td>
        </tr>
        <tr>
            <td class="bg-color-grisClaro01" style="padding-left:10px;">
                <asp:Label ID="lblRegistros" runat="server" CssClass="etiqueta " Font-Size="1em" 
                    Text="Total de registros a cargar: " />
            </td>
        </tr>
        <tr>
            <td style="padding: 5px 5px 5px 5px; width: 100%; text-align: center">
                <asp:GridView ID="grvDetalleConciliacionManual" runat="server" >
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="padding-left:7px;">
                <asp:Button ID="btnCargaArchivoAceptar" runat="server" CssClass="boton fg-color-blanco bg-color-azulClaro"
                    Text="Aceptar" />
                <asp:Button ID="btnCargaArchivoCancelar" runat="server" CssClass="boton fg-color-blanco bg-color-grisClaro"
                    Text="Cancelar" OnClick="btnCargaArchivoCancelar_Click"/>                                        
            </td>
        </tr>
    </table>

<!--    FIN POPUP CARGA ARCHIVO    -->




