<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucCargaManualExcelCyC.ascx.cs" Inherits="wucCargaManualExcelCyC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<div style="width:inherit; padding-top:5px; box-sizing:border-box;">
    <table style="width:100%;">
        <tr>
            <td>
                <div runat="server" ID="dvAlertaError" class="alert alert-danger alert-dismissible fade show" Visible="false"
                    style="margin:5px 5px 0px 7px; box-sizing:border-box;">
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <strong>Error: </strong><asp:Label runat="server" ID="lblMensajeError"></asp:Label>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div runat="server" ID="dvMensajeExito" class="alert alert-success alert-dismissible fade show" Visible="false"
                    style="margin:5px 5px 0px 7px; box-sizing:border-box;">
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <strong>Éxito: </strong>El archivo se ha cargado satisfactoriamente
                </div>
            </td>
        </tr>
        <tr>
            <td style="padding:5px 0px 0px 7px; box-sizing:border-box;">
                <asp:FileUpload ID="fupSeleccionar" runat="server" ViewStateMode="Enabled"/>
            </td>
        </tr>
        <tr>
            <td style="padding-left:3px; box-sizing:border-box;"> 
                <asp:Button ID="btnSubirArchivo" runat="server" CssClass="boton fg-color-blanco bg-color-azulClaro"
                    Text="Subir archivo" OnClick="btnSubirArchivo_Click" />               
            </td>
        </tr>
        <tr>
            <td style="padding-left:10px; padding-right: 5px; box-sizing:border-box;">
                <div>
                    <asp:Label ID="lblArchivo" runat="server" CssClass="etiqueta fg-color-negro" Font-Size="0.97em" Text="Archivo: "
                        style="display:inline-block;"/> <!--   width:200px;     -->
                </div>
            </td>
        </tr>
        <tr>
            <td style="padding-left:10px; padding-right: 5px; box-sizing:border-box;">
                <div>
                    <asp:Label ID="lblReferencia" runat="server" CssClass="etiqueta fg-color-negro" Text="Cliente: "
                        style="display:inline-block;" Font-Size="0.97em"/>
                </div>
            </td>
        </tr>
        <tr>
            <td style="padding-left:10px; padding-right: 5px; box-sizing:border-box;">
                <div>
                    <asp:Label ID="lblMontoPago" runat="server" CssClass="etiqueta fg-color-negro" Text="Monto pago: "
                        style="display:inline-block;" Font-Size="0.97em"/>
                </div>
            </td>
        </tr>
        <tr>
            <td style="padding-left:10px; padding-right: 5px; box-sizing:border-box;">
                <div>
                    <asp:Label ID="lblSaldo" runat="server" CssClass="etiqueta fg-color-negro" Text="Saldo a favor: "
                        style="display:inline-block;" Font-Size="0.97em"/>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="bg-color-grisClaro01" style="margin-left:5px; padding-right: 5px; margin-right:5px; box-sizing:border-box;">
                    <asp:Label ID="lblRegistros" runat="server" CssClass="etiqueta " Font-Size="0.97em" 
                        Text="Total de registros a cargar: " style="padding-left:5px;" />
                </div>
            </td>
        </tr>
        <tr> 
            <td style="padding: 5px 5px 5px 5px; width: 100%; text-align: center; box-sizing:border-box;">
                <div style="margin: 5px 5px 5px 3px; max-height: 169px; overflow: auto;">
                    <!--        GRIDVIEW DETALLE CONCILIACION MANUAL       -->
                    <asp:GridView ID="grvDetalleConciliacionManual" runat="server" style="align-content:center;"
                        CssClass="grvResultadoConsultaCss" ShowHeaderWhenEmpty="True" Width="100%" 
                        ViewStateMode="Enabled" BehaviourID="gridView1" AutoGenerateColumns="False">
                        <PagerStyle CssClass="grvPaginacionScroll" />
                        <SelectedRowStyle BackColor="#66CCFF" ForeColor="Black" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:TemplateField HeaderText="Documento">
                            <ItemTemplate>
                                <asp:Label ID="lblDocumento" runat="server" Text='<%# Eval("Documento") %>'></asp:Label>
                            </ItemTemplate>
                            <ControlStyle CssClass="centradoMedio" />
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Center" Width="33%"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cuenta">
                            <ItemTemplate>
                                <asp:Label ID="lblCuenta" runat="server" Text='<%# Eval("Cuenta") %>'></asp:Label>
                            </ItemTemplate>
                            <ControlStyle CssClass="centradoMedio" />
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Center" Width="33%"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Monto">
                            <ItemTemplate>
                                <asp:Label ID="lblMonto" runat="server" Text='<%# Eval("Monto", "{0:C}") %>'></asp:Label>
                            </ItemTemplate>
                            <ControlStyle CssClass="centradoMedio" />
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Center" Width="33%"></HeaderStyle>
                        </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <!--        GRIDVIEW PAGOS PROPUESTOS       -->
                    <asp:GridView ID="grvPagosPropuestos" runat="server" style="align-content:center;"
                        CssClass="grvResultadoConsultaCss" ShowHeaderWhenEmpty="True" Width="100%" 
                        ViewStateMode="Enabled" BehaviourID="gridView1" Visible="false" AutoGenerateColumns="false">
                        <PagerStyle CssClass="grvPaginacionScroll" />
                        <SelectedRowStyle BackColor="#66CCFF" ForeColor="Black" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:TemplateField HeaderText="Aplicar pago">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkAplicarPago" Checked='<%# Convert.ToBoolean(Eval("AplicarPago")) %>' 
                                        Enabled="false"/>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#ebecec"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Documento">
                                <ItemTemplate>
                                    <asp:Label ID="lblPedidoReferencia" runat="server" Text='<%# Eval("PedidoReferencia") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle CssClass="centradoMedio" />
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cliente">
                                <ItemTemplate>
                                    <asp:Label ID="lblClienteReferencia" runat="server" Text='<%# Eval("ClienteReferencia") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle CssClass="centradoMedio" />
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="F. Suministro">
                                <ItemTemplate>
                                    <asp:Label ID="lblFSuministro" runat="server" Text='<%# Eval("FSuministro", "{0:d}") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle CssClass="centradoMedio" />
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Saldo">
                                <ItemTemplate>
                                    <asp:Label ID="lblSaldoPedido" runat="server" Text='<%# Eval("SaldoPedido", "{0:C}") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle CssClass="centradoMedio" />
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Monto propuesto">
                                <ItemTemplate>
                                    <asp:Label ID="lblMontoPropuesto" runat="server" Text='<%# Eval("MontoPropuesto", "{0:C}") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle CssClass="centradoMedio" />
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="F. Propuesta">
                                <ItemTemplate>
                                    <asp:Label ID="lblFechaPropuesta" runat="server" Text='<%# Eval("FechaPropuesta", "{0:d}") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle CssClass="centradoMedio" />
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td style="padding-left:7px; box-sizing:border-box;">
                <div class="centradoMedio">
                    <!--        Implementar btnCargaManualAceptar_Click(); en página contenedora       -->
                    <asp:Button ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" OnClientClick="btnCargaManualAceptar_Click();"
                        CssClass="boton fg-color-blanco bg-color-azulClaro" Text="ACEPTAR" style="margin-right:10px;" />
                    <!--        Implementar btnCargaManualCancelar_Click(); en página contenedora       -->
                    <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" OnClientClick="btnCargaManualCancelar_Click();" 
                        CssClass="boton fg-color-blanco bg-color-grisClaro" Text="CANCELAR" Visible="false" />                                        
                </div>
            </td>
        </tr>
    </table>

</div>
