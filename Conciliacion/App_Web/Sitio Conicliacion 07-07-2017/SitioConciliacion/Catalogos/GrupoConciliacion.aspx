<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true"
    CodeFile="GrupoConciliacion.aspx.cs" Debug="true" Inherits="Catalogos_GrupoConciliacion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    GRUPO CONCILICIÓN
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <!-- Validar: solo numeros-->
    <script type="text/javascript">
        function ValidNum(e) {
            var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
            return ((tecla > 47 && tecla < 58) || tecla == 8);
        }
        function ValidNumDecimal(e) {
            var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
            return ((tecla > 47 && tecla < 58) || tecla == 46 || tecla == 8);
        }
    </script>
    <script type="text/javascript" language="javascript">

        function HideModalGrupoStatusConcepto() {
            $find("ModalBehaviourStatusConcepto").hide();
        }

        
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager runat="server" ID="spManager" EnableScriptGlobalization="True">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upGrupoConciliacion" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td style="width: 300px; vertical-align: top">
                        <asp:Panel runat="server" ID="pnlNuevoGrupo">
                            <div class="Filtrado">
                                <div class="tiraAmarilla">
                                </div>
                                <div class="titulo">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 85%">
                                                Nuevo <small style="font-size: 10px">Grupo Conciliación</small>
                                            </td>
                                            <td>
                                                <asp:Image ID="imgNuevoMNC" runat="server" CssClass="icono bg-color-amarilloClaro"
                                                    ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Agregar.png" Width="20px"
                                                    Heigth="20px" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="datos-estilo">
                                    <div class="etiqueta">
                                        Descripción
                                    </div>
                                    <asp:TextBox ID="txtDescripcion" runat="server" MaxLength="100" TextMode="MultiLine"
                                        Rows="4" CssClass="cajaTexto" Width="95%" Style="resize: none"></asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ErrorMessage=" Especifique alguna descripción para el nuevo Grupo."
                                        ControlToValidate="txtDescripcion" Font-Size="10px" CssClass="etiqueta fg-color-rojo"
                                        ValidationGroup="NuevoGrupo"></asp:RequiredFieldValidator>
                                    <div class="etiqueta">
                                        Dias
                                    </div>
                                    <table style="width: 100%" class="centradoMedio">
                                        <tr class="etiqueta">
                                            <td class="lineaVertical">
                                                Mínima
                                            </td>
                                            <td class="lineaVertical">
                                                Máxima
                                            </td>
                                            <td class="lineaVertical">
                                                Default
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lineaVertical">
                                                <asp:TextBox ID="txtDiasMinima" runat="server" onkeypress="return ValidNum(event)"
                                                    CssClass="cajaTexto" Width="50px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvDiasMinimo" runat="server" ControlToValidate="txtDiasMinima"
                                                    CssClass="etiqueta fg-color-rojo" ErrorMessage="*" Font-Size="10px" ValidationGroup="NuevoGrupo"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="lineaVertical">
                                                <asp:TextBox ID="txtDiasMaxima" runat="server" onkeypress="return ValidNum(event)"
                                                    CssClass="cajaTexto" Width="50px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvDiasMaximo"
                                                        runat="server" ControlToValidate="txtDiasMaxima" CssClass="etiqueta fg-color-rojo"
                                                        ErrorMessage="*" Font-Size="10px" ValidationGroup="NuevoGrupo"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="lineaVertical">
                                                <asp:TextBox ID="txtDiasDefault" runat="server" onkeypress="return ValidNum(event)"
                                                    CssClass="cajaTexto" Width="50px" Enabled="false"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvDiasDefault" runat="server" ControlToValidate="txtDiasDefault" CssClass="etiqueta fg-color-rojo"
                                                        ErrorMessage="*" Font-Size="10px" ValidationGroup="NuevoGrupo"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:RangeValidator ID="rgvDias" runat="server" ControlToValidate="txtDiasDefault"
                                                    CssClass="etiqueta fg-color-rojo" Display="Dynamic" EnableClientScript="True"
                                                    Font-Size="10px" Type="Integer" ValidationGroup="NuevoGrupo"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="etiqueta">
                                        Diferencia ($)
                                    </div>
                                    <table style="width: 100%" class="centradoMedio">
                                        <tr class="etiqueta">
                                            <td class="lineaVertical">
                                                Mínima
                                            </td>
                                            <td class="lineaVertical">
                                                Máxima
                                            </td>
                                            <td class="lineaVertical">
                                                Default
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lineaVertical">
                                                <asp:TextBox ID="txtDiferenciaMinima" runat="server" onkeypress="return ValidNumDecimal(event)"
                                                    CssClass="cajaTexto" Width="50px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvDiferenciaMinima"
                                                        runat="server" ControlToValidate="txtDiferenciaMinima" CssClass="etiqueta fg-color-rojo"
                                                        ErrorMessage="*" Font-Size="10px" ValidationGroup="NuevoGrupo"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="lineaVertical">
                                                <asp:TextBox ID="txtDiferenciaMaxima" runat="server" onkeypress="return ValidNumDecimal(event)"
                                                    CssClass="cajaTexto" Width="50px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvDiferenciaMaxima"
                                                        runat="server" ControlToValidate="txtDiferenciaMaxima" CssClass="etiqueta fg-color-rojo"
                                                        ErrorMessage="*" Font-Size="10px" ValidationGroup="NuevoGrupo"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="lineaVertical">
                                                <asp:TextBox ID="txtDiferenciaDefault" runat="server" onkeypress="return ValidNumDecimal(event)"
                                                    CssClass="cajaTexto" Width="50px" Enabled="false"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvDiferenciaDefault" runat="server" ControlToValidate="txtDiferenciaDefault"
                                                        CssClass="etiqueta fg-color-rojo" ErrorMessage="*" Font-Size="10px" ValidationGroup="NuevoGrupo"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:RangeValidator ID="rgvDiferencia" ControlToValidate="txtDiferenciaDefault" Type="Double"
                                                    EnableClientScript="True" runat="server" CssClass="etiqueta fg-color-rojo" Display="Dynamic"
                                                    Font-Size="10px" ValidationGroup="NuevoGrupo" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" class="centradoJustificado">
                                                <asp:CheckBox ID="chkValidarRangos" runat="server" Text="Validar Rangos" CssClass="etiqueta"
                                                    AutoPostBack="True" OnCheckedChanged="chkValidarRangos_CheckedChanged" />
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="centradoMedio">
                                        <br />
                                        <asp:Button ID="btnGuardarGrupoConciliacion" runat="server" Text="GUARDAR" ToolTip="Guardar el grupo nuevo"
                                            ValidationGroup="NuevoGrupo" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                            OnClick="btnGuardarGrupoConciliacion_Click" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlNuevoUsuario" Visible="False">
                            <div class="Filtrado">
                                <div class="tiraVerdeClaro">
                                </div>
                                <div class="titulo">
                                    Nuevo <small style="font-size: 10px">Usuario</small></div>
                                <div class="datos-estilo">
                                    <div class="etiqueta">
                                        Empleado</div>
                                    <asp:DropDownList ID="ddlEmpleado" runat="server" Width="100%" CssClass="dropDown"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlEmpleado_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <div class="etiqueta">
                                        Usuario</div>
                                    <asp:TextBox ID="txtUsuario" CssClass="cajaTexto" Width="95%" runat="server" Enabled="False"></asp:TextBox>
                                    <div class="etiqueta">
                                        <asp:CheckBox ID="ckbAccesoTotal" runat="server" Text="Acceso total" /></div>
                                    <br />
                                    <div class="centradoMedio">
                                        <asp:Button ID="btnGuardarUsuario" runat="server" Text="GUARDAR" CssClass="boton fg-color-blanco bg-color-azulClaro"
                                            OnClick="btnGuardarUsuario_Click" ToolTip="Agrega el usuario al grupo" />
                                        <asp:Button ID="btnCancelarUsuario" runat="server" OnClick="btnCancelarUsuario_Click"
                                            Text="CANCELAR" CssClass="boton fg-color-blanco bg-color-grisOscuro" ToolTip="Cancela la captura del usuario" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </td>
                    <td style="vertical-align: top">
                        <div class="datos-estilo" style="margin-right: 15px; margin-left: 15px">
                            <div class="titulo" style="margin-left: 0px">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 95%">
                                            Catalogo : Grupo de Conciliacion
                                            <asp:Label ID="lblGrupoConciliacion" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <img src="../App_Themes/GasMetropolitanoSkin/Imagenes/grid.png" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="lineaHorizontal">
                            </div>
                            <br />
                            <asp:Panel runat="server" ID="pnlGruposconciliacion">
                                <asp:GridView ID="grdGrupos" runat="server" AutoGenerateColumns="False" Width="100%"
                                    BorderStyle="Dotted" AllowPaging="True" PageSize="10" CssClass="grvResultadoConsultaCss"
                                    ShowHeaderWhenEmpty="True" DataKeyNames="GrupoConciliacionId, Descripcion, Status"
                                    OnPageIndexChanging="grdGrupos_PageIndexChanging" OnRowDataBound="grdGrupos_RowDataBound"
                                    OnRowCommand="grdGrupos_RowCommand" OnSelectedIndexChanging="grdGrupos_SelectedIndexChanging">
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="No existen grupos de conciliacion"></asp:Label>
                                    </EmptyDataTemplate>
                                    <HeaderStyle CssClass="GridHeader" />
                                    <RowStyle CssClass="GridRow" />
                                    <AlternatingRowStyle CssClass="bg-color-grisClaro01" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Clave" SortExpression="colClave">
                                            <ItemTemplate>
                                                <asp:Literal ID="rbElegirGrupo" runat="server"></asp:Literal>
                                                <asp:Label ID="lblClave" runat="server" Text="<%# Bind('GrupoConciliacionId') %>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Justify" Wrap="True" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripción" SortExpression="colDescripcion">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescripcion" runat="server" Text="<%# Bind('Descripcion') %>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" Wrap="True" Width="40%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UsuarioAlta" SortExpression="colUsuarioAlta">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUsuario" runat="server" Text="<%# Bind('Usuario') %>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="True" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha alta" SortExpression="colFechaAlta">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFalta" runat="server" Text="<%# Bind('FAlta','{0:d}') %>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="True" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dias Default" SortExpression="colDiasDefault">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDiasDefault" runat="server" Text="<%# Bind('DiasDefault') %>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="True" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Diferencia Default" SortExpression="colDiferenciaDefault">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDiferenciaDefault" runat="server" Text="<%# Bind('DiferenciaDefault') %>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="True" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" SortExpression="colStatus">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblStatus" Text="<%# Bind('Status') %>" Visible="False"></asp:Label>
                                                <asp:Button runat="server" ID="btnStatus" Width="15px" CommandName="CAMBIARSTATUS"
                                                    Height="15px" Style="padding: 0px 0px 0px 0px" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="True" Width="15%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerTemplate>
                                        Página
                                        <asp:DropDownList ID="paginasDropDownList" Font-Size="12px" AutoPostBack="true" runat="server"
                                            OnSelectedIndexChanged="paginasDropDownList_SelectedIndexChanged" CssClass="dropDown"
                                            Width="60px">
                                        </asp:DropDownList>
                                        de
                                        <asp:Label ID="lblTotalNumPaginas" runat="server" CssClass="etiqueta fg-color-blanco" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnInicial" runat="server" CommandName="Page" ToolTip="Prim. Pag"
                                            CommandArgument="First" CssClass="boton pagInicial" />
                                        <asp:Button ID="btnAnterior" runat="server" CommandName="Page" ToolTip="Pág. anterior"
                                            CommandArgument="Prev" CssClass="boton pagAnterior" />
                                        <asp:Button ID="btnSiguiente" runat="server" CommandName="Page" ToolTip="Sig. página"
                                            CommandArgument="Next" CssClass="boton pagSiguiente" />
                                        <asp:Button ID="btnUltima" runat="server" CommandName="Page" ToolTip="Últ. Pag" CommandArgument="Last"
                                            CssClass="boton pagUltima" />
                                    </PagerTemplate>
                                    <PagerStyle CssClass="estiloPaginacion bg-color-grisOscuro fg-color-blanco" />
                                </asp:GridView>
                                <div class="centradoDerecha">
                                    <asp:ImageButton runat="server" ID="btnUsuarios" Width="32px" Height="32px" ImageUrl="../App_Themes/GasMetropolitanoSkin/Imagenes/usuarios.png"
                                        ToolTip="VER USUARIOS" OnClick="btnUsuarios_Click" />
                                    <asp:Button runat="server" ID="btnVerUsuarios" CssClass="boton fg-color-blanco bg-color-verdeAgua"
                                        Text="VER USUARIOS" Style="margin: -25px 0px 0px 0px" OnClick="btnVerUsuarios_Click" />
                                    <asp:Button runat="server" ID="btnStatusConcepto" CssClass="boton fg-color-blanco bg-color-naranja"
                                        Text="VER STATUS CONCEPTO" Style="margin: -25px 0px 0px 0px" OnClick="btnVerStatusConcepto_Click" />
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnlUsuariosGrupo" Visible="False">
                                <asp:GridView ID="grdUsuarios" runat="server" AutoGenerateColumns="False" Width="100%"
                                    AllowPaging="True" OnPageIndexChanging="grdUsuarios_PageIndexChanging" ShowHeaderWhenEmpty="True"
                                    Caption="" DataKeyNames="Usuario" PageSize="10" CssClass="grvResultadoConsultaCss"
                                    OnRowDeleting="grdUsuarios_RowDeleting" OnRowDataBound="grdUsuarios_RowDataBound">
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="No existen usuarios en el grupo"></asp:Label>
                                    </EmptyDataTemplate>
                                    <HeaderStyle CssClass="GridHeader" />
                                    <RowStyle CssClass="GridRow" />
                                    <AlternatingRowStyle CssClass="GridAlternateRow" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Usuario" SortExpression="colUsuario">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUsuario" runat="server" Text="<%# Bind('Usuario') %>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="True" Width="20%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Acceso Total" SortExpression="colAccesototal">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkAccesoTotal" runat="server" Checked="<%# Bind('AccesoTotal') %>"
                                                    Enabled="False"></asp:CheckBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="True" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Eliminar" SortExpression="colEliminar">
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="btnEliminarUsuario" ImageUrl="../App_Themes/GasMetropolitanoSkin/Imagenes/borrar.png"
                                                    Width="15px" CommandName="delete" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="True" Width="15%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerTemplate>
                                        Página
                                        <asp:DropDownList ID="paginasDropDownList" Font-Size="12px" AutoPostBack="true" runat="server"
                                            OnSelectedIndexChanged="paginasDropDownListUsuarios_SelectedIndexChanged" CssClass="dropDown"
                                            Width="60px">
                                        </asp:DropDownList>
                                        de
                                        <asp:Label ID="lblTotalNumPaginas" runat="server" CssClass="etiqueta fg-color-blanco" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnInicial" runat="server" CommandName="Page" ToolTip="Prim. Pag"
                                            CommandArgument="First" CssClass="boton pagInicial" />
                                        <asp:Button ID="btnAnterior" runat="server" CommandName="Page" ToolTip="Pág. anterior"
                                            CommandArgument="Prev" CssClass="boton pagAnterior" />
                                        <asp:Button ID="btnSiguiente" runat="server" CommandName="Page" ToolTip="Sig. página"
                                            CommandArgument="Next" CssClass="boton pagSiguiente" />
                                        <asp:Button ID="btnUltima" runat="server" CommandName="Page" ToolTip="Últ. Pag" CommandArgument="Last"
                                            CssClass="boton pagUltima" />
                                    </PagerTemplate>
                                    <PagerStyle CssClass="estiloPaginacion bg-color-grisOscuro fg-color-blanco" />
                                </asp:GridView>
                                <br />
                                <br />
                            </asp:Panel>
                        </div>
                    </td>
                </tr>
            </table>
      </ContentTemplate>
    </asp:UpdatePanel>
    <!-- PANEL : AGREGAR STATUS CONCEPTO POR GRUPO DE CONCILIACION -->
    <asp:HiddenField runat="server" ID="hdfCerrarGrupoStatusConcepto" />
    <asp:ModalPopupExtender ID="mpeGrupoConciliacionStatus" runat="server" BackgroundCssClass="ModalBackground"
        DropShadow="False" EnableViewState="false" BehaviorID="ModalBehaviourStatusConcepto"
        PopupControlID="pnlGrupoStatusConcepto" TargetControlID="hdfCerrarGrupoStatusConcepto"
        CancelControlID="btnCerrar">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlGrupoStatusConcepto" runat="server" CssClass="ModalPopup" Width="400px">
        <asp:UpdatePanel runat="server" ID="upGrupoStatus" UpdateMode="Always">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr class="bg-color-grisOscuro">
                        <td colspan="3" style="padding: 5px 5px 5px 5px" class="etiqueta">
                            <div class="floatDerecha">
                                <asp:ImageButton runat="server" ID="btnCerrar" ImageUrl="~/App_Themes/GasMetropolitanoSkin/Iconos/Cerrar.png"
                                    CssClass="iconoPequeño bg-color-rojo" OnClientClick="HideModalGrupoStatusConcepto();" />
                            </div>
                            <div class="fg-color-blanco">
                                <b>Grupo :
                                    <asp:Label runat="server" ID="lblGrupoConciliacionSC" CssClass="etiqueta fg-color-blanco"></asp:Label></b>
                                <asp:HiddenField ID="hdfGrupoConciliacionSeleccionado" runat="server" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 5px 5px 5px 5px; width: 30%" class="etiqueta">
                            Status Concepto
                        </td>
                        <td style="padding: 5px 5px 5px 5px; width: 60%">
                            <asp:DropDownList ID="ddlStatusConcepto" runat="server" CssClass="dropDown" Width="100%">
                            </asp:DropDownList>
                            
                        </td>
                        <td style="padding: 5px 5px 5px 5px; width: 10%" class="lineaHorizontal">
                            <asp:Button ID="btnAgregarStatusConcepto" runat="server" Text="AÑADIR" ToolTip="AÑADIR"
                                CssClass="botonPequeño fg-color-blanco bg-color-verdeClaro" Width="50px" OnClick="btnAgregarStatusConcepto_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 5px 5px 5px 5px; width: 15%" colspan="3">
                            <asp:GridView ID="grvGrupoStatusConcepto" runat="server" AutoGenerateColumns="False"
                                Width="100%" AllowPaging="True" ShowHeaderWhenEmpty="True" DataKeyNames="StatusConcepto"
                                PageSize="10" CssClass="grvResultadoConsultaCss" 
                                OnRowDeleting="grvGrupoStatusConcepto_RowDeleting" 
                                onpageindexchanging="grvGrupoStatusConcepto_PageIndexChanging">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblvacio" runat="server" CssClass="etiqueta fg-color-rojo" Text="No existen status conceptos asocidados a este Grupo"></asp:Label>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="GridHeader" />
                                <RowStyle CssClass="GridRow" />
                                <AlternatingRowStyle CssClass="GridAlternateRow" />
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" SortExpression="colId">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatusConceptoID" runat="server" Text="<%# Bind('StatusConcepto') %>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="True" Width="20%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status Concepto" SortExpression="colStatusConcepto">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatusConceptoDes" runat="server" Text="<%# Bind('StatusConceptoDes') %>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="True" Width="60%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="colEliminar">
                                        <ItemTemplate>
                                            <asp:Button ID="btnEliminarStatusConcepto" runat="server" Text="QUITAR" CssClass="botonPequeño fg-color-blanco bg-color-rojo"
                                                Width="50px" CommandName="DELETE" ToolTip="QUITAR" OnClientClick='<%# "return confirm(\"¿Desea quitar el Status Concepto : "+ Eval("StatusConceptoDes").ToString() +  " del Grupo de Conciliación Asociado.?\");" %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="True" Width="5%" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%;" class="centradoMedio" colspan="3">
                            <asp:Button ID="btnCerrarGrupoStatus" runat="server" CssClass="boton fg-color-blanco bg-color-grisClaro"
                                Text="CERRAR" OnClientClick="HideModalGrupoStatusConcepto();" />
                        </td>
                    </tr>
                    <tr>
                        <td class="bg-color-grisClaro01" colspan="4">
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>  
</asp:Content>
