<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListadoPacientes.aspx.cs" Inherits="ClinicaMedica.ListadoPacientes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listado de Pacientes</title>
    <link rel="stylesheet" type="text/css" href="Estilo/EstiloClinica.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,500,700&display=swap" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            <section style="display: flex; justify-content: flex-end; padding: 10px;">
                <asp:Menu ID="MenuUsuario" runat="server" CssClass="menu-derecha"
                    Orientation="Horizontal"
                    StaticDisplayLevels="1"
                    DynamicHorizontalOffset="0"
                    Font-Names="Verdana" Font-Size="0.8em"
                    ForeColor="#7C6F57"
                    BackColor="#F7F6F3"
                    StaticSubMenuIndent="10px"
                    OnMenuItemClick="btnUnlogin_Click">
                    <Items>
                        <asp:MenuItem Text="Menú" Value="cuenta">
                            <asp:MenuItem Text="Cambiar contraseña" NavigateUrl="~/CambiarContraseña.aspx" />
                            <asp:MenuItem Text="Cerrar sesión" Value="cerrarSesion" />
                        </asp:MenuItem>
                    </Items>

                    <StaticMenuItemStyle HorizontalPadding="10px" VerticalPadding="5px" />
                    <StaticHoverStyle BackColor="#7C6F57" ForeColor="White" />
                    <DynamicMenuStyle BackColor="#F7F6F3" />
                    <DynamicHoverStyle BackColor="#7C6F57" ForeColor="White" />
                    <DynamicMenuItemStyle HorizontalPadding="10px" VerticalPadding="5px" />
                </asp:Menu>
                <asp:Label ID="lblBienvenidoUsuario" runat="server" Style="float: right; margin-right: 10px;" Font-Bold="True"></asp:Label>
                <asp:ImageButton ID="btnUserImg" runat="server" ImageUrl="Estilo/user.png" CssClass="user-image" Visible="False" Style="transform: translateY(-3px); margin-left: 2px" />
            </section>
            <div class="titulo-header">
                <h1>Clinica Medica</h1>
                <img src="Estilo/logoClinica.png" class="header-image" alt="Logo Clinica" />
                <div class="header-links">
                    <asp:HyperLink ID="hlListarTurnos" runat="server" CssClass="header-link" NavigateUrl="ListadoTurnos.aspx" Text="Listado de Turnos"></asp:HyperLink>
                    <%--<asp:HyperLink ID="hlSeguimientoPaciente" runat="server" CssClass="header-link" Text="Seguimiento Paciente" TabIndex="4" NavigateUrl="~/SeguimientosPacientes.aspx"></asp:HyperLink>--%>
                    <asp:HyperLink ID="hlAgregarMedico" runat="server" CssClass="header-link" NavigateUrl="RegistrarMedico.aspx" Text="Agregar Medico"></asp:HyperLink>
                    <asp:HyperLink ID="hlAsignarTurnos" runat="server" CssClass="header-link" NavigateUrl="AsignacionTurnos.aspx" Text="Asignar Turnos"></asp:HyperLink>
                    <asp:HyperLink ID="hListarMedicos" runat="server" CssClass="header-link" NavigateUrl="ListadoDeMedicos.aspx" Text="Listar Medicos"></asp:HyperLink>
                    <asp:HyperLink ID="HlListarPacientes" runat="server" CssClass="header-link-active" NavigateUrl="ListadoPacientes.aspx" Text="Listar Pacientes"></asp:HyperLink>
                    <asp:HyperLink ID="hlInformes" runat="server" CssClass="header-link" NavigateUrl="Informes.aspx" Text="Informes"></asp:HyperLink>
                </div>
            </div>
        </div>
        <div class="contenido">
            <div class="solapa">
                Listado de Pacientes
            </div>
            <div class="caja">
                <div style="display: flex; align-items: center; gap: 20px;">
                    <asp:Button ID="btnBuscarPacientes" runat="server" CssClass="button" Text="Buscar Pacientes" Width="210" Height="40" OnClick="btnBuscarPac_Click" />
                    <asp:TextBox ID="txtBuscadorPac" runat="server" CssClass="txtBox-caja" placeholder="Ingrese Dni" Style="margin-top: 18px;"></asp:TextBox>
                    <asp:TextBox ID="txtBuscadorNombre" runat="server" CssClass="txtBox-caja" placeholder="Ingrese Nombre" Style="margin-top: 18px;"></asp:TextBox>
                    <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="#CC0000"></asp:Label>

                </div>

                <asp:GridView ID="gvPacientes" runat="server" AutoGenerateColumns="False" CssClass="tabla-turnos"
                    OnRowCommand="gvPacientes_RowCommand"
                    AllowPaging="True"
                    OnPageIndexChanging="gvPacientes_PageIndexChanging"
                    PageSize="5"
                    DataKeyNames="DNI"
                    OnRowEditing="gvPacientes_RowEditing"
                    OnRowUpdating="gvPacientes_RowUpdating"
                    OnRowCancelingEdit="gvPacientes_RowCancelingEdit" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="gvPacientes_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
    <asp:TemplateField HeaderText="Dni">
        <ItemTemplate>
            <asp:Label ID="lbl_it_Dni" runat="server" Text='<%# Bind("DNI") %>'></asp:Label>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Nombre">
    <ItemTemplate>
        <asp:Label ID="lbl_it_Nombre" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:TextBox ID="txt_et_Nombre" runat="server" Text='<%# Bind("Nombre") %>'></asp:TextBox>
    </EditItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Apellido">
    <ItemTemplate>
        <asp:Label ID="lbl_it_Apellido" runat="server" Text='<%# Bind("Apellido") %>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:TextBox ID="txt_et_Apellido" runat="server" Text='<%# Bind("Apellido") %>'></asp:TextBox>
    </EditItemTemplate>
</asp:TemplateField>
    <asp:TemplateField HeaderText=" Fecha de Nacimiento">
        <ItemTemplate>
            <asp:Label ID="lbl_it_Fecha" runat="server" Text='<%# Bind("FechaNacimiento") %>'></asp:Label>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Sexo">
        <ItemTemplate>
            <asp:Label ID="lbl_it_Sexo" runat="server" Text='<%# Bind("Sexo") %>'></asp:Label>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Nacionalidad">
        <ItemTemplate>
            <asp:Label ID="lbl_it_Nacionalidad" runat="server" Text='<%# Bind("Nacionalidad") %>'></asp:Label>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Numero de Telefono">
    <ItemTemplate>
        <asp:Label ID="lbl_it_Telefono" runat="server" Text='<%# Bind("Telefono") %>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:TextBox ID="txt_et_Telefono" runat="server" Text='<%# Bind("Telefono") %>'></asp:TextBox>
    </EditItemTemplate>
</asp:TemplateField>
    <asp:TemplateField HeaderText="Provincia">
        <ItemTemplate>
            <asp:Label ID="lbl_it_Provincia" runat="server" Text='<%# Bind("Provincia") %>'></asp:Label>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Correo Electronico">
    <ItemTemplate>
        <asp:Label ID="lbl_it_Correo" runat="server" Text='<%# Bind("Correo") %>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:TextBox ID="txt_et_Correo" runat="server" Text='<%# Bind("Correo") %>'></asp:TextBox>
    </EditItemTemplate>
</asp:TemplateField>
                        <asp:TemplateField HeaderText="Dar de Baja">
    <ItemTemplate>
        <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("DNI") %>'
            Text="Eliminar" OnClientClick="return confirm('¿Seguro que desea eliminar el médico?');" />
    </ItemTemplate>
</asp:TemplateField>
    <asp:CommandField ShowEditButton="True" />
</Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
                <div style="margin: 25px 10px 5px 0px;">
                    <asp:Button ID="btnMostrarTodo" runat="server" CssClass="button" Text="Mostrar Todo" Width="202px" OnClick="btnMostrarTodo_Click" />
                </div>

            </div>
        </div>
    </form>
</body>
</html>
