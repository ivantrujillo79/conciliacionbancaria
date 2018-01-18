<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucSaldoAFavor.ascx.cs" Inherits="ControlesUsuario_SaldosAFavor_wucSaldoAFavor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<script type="text/javascript">
    function ValidaNumero(e) {
        var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
        return ((tecla > 47 && tecla < 58));
    }
    function ValidaMoneda(e) {
        var charCode = (e.which) ? e.which : e.keyCode;
        if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
</script>


<style type="text/css">
    .auto-style1 {
        height: 19px;
    }
</style>

<table style="width: 100%;">
    <tr>
        <td colspan="7">
            <div style="width:900px; height:300px; overflow:auto; align-content:center;">
                <asp:GridView ID="grvSaldosAFavor" runat="server" AutoGenerateColumns="False" CssClass="grvResultadoConsultaCss" OnRowDataBound="gvSaldoAFavor_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <input type="checkbox" runat="server" id="cbSAF"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField AccessibleHeaderText="Folio" HeaderText="Folio" DataField="Folio" />
                        <asp:BoundField AccessibleHeaderText="Cliente" HeaderText="Cliente" DataField="Cliente" />
                        <asp:BoundField AccessibleHeaderText="Nombre Cliente" HeaderText="Nombre Cliente" DataField="NombreCliente" />
                        <asp:BoundField AccessibleHeaderText="Banco" HeaderText="Banco" DataField="Banco"/>
                        <asp:BoundField AccessibleHeaderText="Sucursal" HeaderText="Sucursal" DataField="Sucursal" />
                        <asp:BoundField AccessibleHeaderText="Tipo Cargo" HeaderText="Tipo Cargo" DataField ="TipoCargo" />
                        <asp:BoundField AccessibleHeaderText="Global" HeaderText="Global" DataField = "Global" />
                        <asp:BoundField AccessibleHeaderText="Fsaldo" HeaderText="Fsaldo" DataField = "Fsaldo"  />
                        <asp:BoundField AccessibleHeaderText="Importe" HeaderText="Importe" DataField = "Importe"  />
                        <asp:BoundField AccessibleHeaderText="Conciliada" HeaderText="Conciliada" DataField = "Conciliada"  />
                    </Columns>
                </asp:GridView>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="7"></td>
    </tr>
    <tr>
        <td colspan="7">
            <div class="centradoMedio">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CssClass="boton fg-color-blanco bg-color-azulClaro"/>
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" CssClass="boton fg-color-blanco bg-color-grisClaro" />
            </div>
        </td>
    </tr>
</table>