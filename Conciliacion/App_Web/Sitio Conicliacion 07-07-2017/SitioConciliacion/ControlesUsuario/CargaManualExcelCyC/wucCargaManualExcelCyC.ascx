﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucCargaManualExcelCyC.ascx.cs" Inherits="wucCargaManualExcelCyC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<div style="width:inherit;">
    <table style="width:100%; box-sizing:border-box;">
        <%--<tr class="bg-color-grisOscuro">
            <td style="padding: 5px 5px 5px 5px; box-sizing:border-box;" class="etiqueta">
                <div class="floatDerecha">
                    <asp:ImageButton runat="server" ID="btnCerrarCargaArchivo" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png"
                        CssClass="iconoPequeño bg-color-rojo" />
                </div>
            </td>
        </tr>--%>
        <tr>
            <td style="padding:10px 0px 0px 10px; box-sizing:border-box;">
                <asp:FileUpload ID="fupSeleccionar" runat="server" ViewStateMode="Enabled"/>
            </td>
        </tr>
        <tr>
            <td style="padding-left:6px; box-sizing:border-box;"> 
                <asp:Button ID="btnSubirArchivo" runat="server" CssClass="boton fg-color-blanco bg-color-azulClaro"
                    Text="Subir archivo" OnClick="btnSubirArchivo_Click" />               
            </td>
        </tr>
        <tr>
            <td style="padding-left:10px; padding-right: 5px; box-sizing:border-box;">
                <div>
                    <asp:Label ID="lblArchivo" runat="server" CssClass="etiqueta fg-color-negro" Font-Size="1em" Text="Archivo: "
                        style="display:inline-block;"/> <!--   width:200px;     -->
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="bg-color-grisClaro01" style="margin-left:5px; padding-right: 5px; box-sizing:border-box;">
                    <asp:Label ID="lblRegistros" runat="server" CssClass="etiqueta " Font-Size="1em" 
                        Text="Total de registros a cargar: " style="padding-left:5px;" />
                </div>
            </td>
        </tr>
        <tr>
            <td style="padding-left:10px; padding-right: 5px; box-sizing:border-box;">
                <div>
                    <asp:Label ID="lblReferencia" runat="server" CssClass="etiqueta fg-color-negro" Text="Referencia: "
                        style="display:inline-block;"/> <!--    Font-Size="1em"     -->
                </div>
            </td>
        </tr>
        <tr>
            <td style="padding-left:10px; padding-right: 5px; box-sizing:border-box;">
                <div>
                    <asp:Label ID="lblMontoPago" runat="server" CssClass="etiqueta fg-color-negro" Text="Monto pago: "
                        style="display:inline-block;"/> <!--    Font-Size="1em"     -->
                </div>
            </td>
        </tr>
        <tr>
            <td style="padding-left:10px; padding-right: 5px; box-sizing:border-box;">
                <div>
                    <asp:Label ID="lblSaldo" runat="server" CssClass="etiqueta fg-color-negro" Text="Saldo a favor: "
                        style="display:inline-block;"/> <!--    Font-Size="1em"     -->
                </div>
            </td>
        </tr>
        <tr> 
            <td style="padding: 5px 5px 5px 5px; width: 100%; text-align: center; box-sizing:border-box;">
                <div style="margin: 5px 5px 5px 3px; max-height: 169px; overflow: auto;">
                    <asp:GridView ID="grvDetalleConciliacionManual" runat="server" style="align-content:center;"
                        CssClass="grvResultadoConsultaCss" ShowHeaderWhenEmpty="True" Width="100%" 
                        ViewStateMode="Enabled" BehaviourID="gridView1">
                        <PagerStyle CssClass="grvPaginacionScroll" />
                        <SelectedRowStyle BackColor="#66CCFF" ForeColor="Black" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkAplicarPago" />
                                    </ItemTemplate>
                                    <%--<HeaderTemplate>
                                        <asp:CheckBox runat="server" ID="chkTodosInternos" /> <!--AutoPostBack="True" OnCheckedChanged="OnCheckedChangedInternos" -->
                                    </HeaderTemplate>--%>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25px" BackColor="#ebecec"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
                                </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td style="padding-left:7px; box-sizing:border-box;">
                <div class="centradoMedio">
                    <asp:Button ID="btnCargaArchivoAceptar" style="margin-right:10px;" runat="server" OnClick="btnAceptar_Click" 
                        CssClass="boton fg-color-blanco bg-color-azulClaro" Text="Aceptar"  />
                    <asp:Button ID="btnCargaArchivoCancelar" runat="server" CssClass="boton fg-color-blanco bg-color-grisClaro"
                        Text="Cancelar" />                                        
                </div>
            </td>
        </tr>
    </table>

</div>
