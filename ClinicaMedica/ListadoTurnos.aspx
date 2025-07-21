<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListadoTurnos.aspx.cs" Inherits="ClinicaMedica.ListadoTurnos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="Estilo/EstiloClinica.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Clinica Medica</title>
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
                <asp:Label ID="lblNombreUsuario" runat="server" Style="float: right; margin-right: 4px; font-size: 15px;" Text="Nombre de usuario:"></asp:Label>
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="txtBox-login" Style="float: right; margin-right: 10px; height: 20px;" ValidationGroup="GrupoInicioSesion" TabIndex="1"></asp:TextBox>
                <asp:Label ID="lblContrasenia" runat="server" Style="float: right; margin-right: 2px; font-size: 15px;" Text="Contraseña:"></asp:Label>
                <asp:TextBox ID="txtContrasenia" runat="server" CssClass="txtBox-login" Style="float: right; margin-right: 10px; height: 20px;" TextMode="Password" ValidationGroup="GrupoInicioSesion" TabIndex="2"></asp:TextBox>
                <asp:Button ID="btnLogin" runat="server" Style="float: right; margin-right: 5px; height: 28px; transform: translateY(-2px)" Text="Ingresar" CssClass="button" OnClick="btnLogin_Click" ValidationGroup="GrupoInicioSesion" TabIndex="3" />

            </section>
            <div class="titulo-header">
                <h1>Clinica Medica</h1>
                <img src="Estilo/logoClinica.png" class="header-image" alt="Logo Clinica" />
                <div class="header-links">
                    <asp:HyperLink ID="hlListarTurnos" runat="server" CssClass="header-link-active" NavigateUrl="ListadoTurnos.aspx" Text="Listado de Turnos" Visible="False" TabIndex="4" Height="16px"></asp:HyperLink>
                    <asp:HyperLink ID="hlAgregarMedico" runat="server" CssClass="header-link" NavigateUrl="RegistrarMedico.aspx" Text="Agregar Medico" Visible="False" TabIndex="6"></asp:HyperLink>
                    <asp:HyperLink ID="hlAsignarTurnos" runat="server" CssClass="header-link" NavigateUrl="AsignacionTurnos.aspx" Text="Asignar Turnos" Visible="False" TabIndex="7"></asp:HyperLink>
                    <asp:HyperLink ID="hlListarMedicos" runat="server" CssClass="header-link" NavigateUrl="ListadoDeMedicos.aspx" Text="Listar Medicos" Visible="False" TabIndex="8"></asp:HyperLink>
                    <asp:HyperLink ID="HlListarPacientes" runat="server" CssClass="header-link" NavigateUrl="ListadoPacientes.aspx" Text="Listar Pacientes" Visible="False" TabIndex="9"></asp:HyperLink>
                    <asp:HyperLink ID="hlInformes" runat="server" CssClass="header-link" NavigateUrl="Informes.aspx" Text="Informes" Visible="False" TabIndex="10"></asp:HyperLink>
                </div>
            </div>
        </div>
        <div class="contenido">
            <div class="solapa">
                Turnos Asignados
            </div>
            <div class="caja">
                <div style="display: flex; align-items: center; gap: 20px; padding-left: 10px;">
                    <asp:TextBox ID="txtBuscador" runat="server" placeholder="Ingrese Dni" CssClass="txtBox-caja" Style="margin-top: 18px;" TabIndex="10"> </asp:TextBox>
                    <asp:Button ID="btnBuscar" runat="server" Text="Consultar Turno" CssClass="button" Width="210px" Height="40" TabIndex="11" OnClick="btnBuscar_Click" />

                    <div style="display: flex; align-items: center; gap: 20px">
                        <br />
                        <br />
                        <asp:Label ID="lblFecha" runat="server" Text="Fecha" CssClass="button" Width="90px" Height="40" TabIndex="11" OnClick="lblFecha_Click" />

                        <asp:DropDownList ID="ddlFechas" runat="server" CssClass="txtBox-caja" Style="margin-top: 18px;" Visible="False" TabIndex="12" AutoPostBack="True" OnSelectedIndexChanged="ddlFechas_SelectedIndexChanged"></asp:DropDownList>

                    </div>
                </div>
                <div style="display: flex; align-items: center; gap: 20px; padding-left: 10px;">
                    <asp:DropDownList ID="ddlEstados" runat="server" CssClass="txtBox-caja" Style="margin-top: 18px; font-size: 14px;" Visible="False" TabIndex="13" OnSelectedIndexChanged="ddlEstados_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                    <asp:Label ID="btnConsultarEstado" runat="server" Text="Estado de Turnos" Style="width: 210px; height: 40px; font-size: 14px" CssClass="button" Visible="False" TabIndex="14" Height="40px" />
                    <asp:Button ID="btnMostrarTodo" runat="server" Text="Mostrar Todo" CssClass="button" Style="font-size: 14px; margin-left: auto;" Visible="False" TabIndex="15" OnClick="btnMostrarTodo_Click" />
                    <br />

                    <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="#CC0000"></asp:Label>

                    <br />
                    <br />
                </div>
                <asp:GridView ID="gvTurnos" runat="server"
                    AutoGenerateColumns="False"
                    CssClass="tabla-turnos"
                    AllowPaging="True"
                    PageSize="15"
                    DataKeyNames="NumeroTurno"
                    AutoPostBack="True"
                    OnPageIndexChanging="gvTurnos_PageIndexChanging"
                    OnRowDeleting="gvTurnos_RowDeleting"
                    OnRowEditing="gvTurnos_RowEditing"
                    OnRowUpdating="gvTurnos_RowUpdating"
                    OnRowCancelingEdit="gvTurnos_RowCancelingEdit" OnSelectedIndexChanging="gvTurnos_SelectedIndexChanging">
                    <Columns>

                        <asp:TemplateField HeaderText=" Numero de turno">
                            <ItemTemplate>
                                <asp:Label ID="lbl_it_NumeroTurno" runat="server" Text='<%# Bind("NumeroTurno") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Especialidad">
                            <ItemTemplate>
                                <asp:Label ID="lbl_it_Especialidad" runat="server" Text='<%# Bind("Especialidad") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Doctor(a)">
                            <ItemTemplate>
                                <asp:Label ID="lbl_it_DoctorNombre" runat="server" Text='<%# Bind("Doctor") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Fecha">
                            <ItemTemplate>
                                <asp:Label ID="lbl_it_Fecha" runat="server" Text='<%# Bind("Fecha") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Horario">
                            <ItemTemplate>
                                <asp:Label ID="lbl_it_Horario" runat="server" Text='<%# Bind("Horario") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Paciente">
                            <EditItemTemplate>
                                <asp:Label ID="lbl_et_NombrePaciente" runat="server" Text='<%# Bind("NombrePaciente") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbl_it_NombrePaciente" runat="server" Text='<%# Bind("NombrePaciente") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="DNI">
                            <ItemTemplate>
                                <asp:Label ID="lbl_it_DniPaciente" runat="server" Text='<%# Bind("DNIpaciente") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lbl_et_DniPaciente" runat="server" Text='<%# Bind("DNIpaciente") %>'></asp:Label>
                                <br />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:CommandField
                            HeaderText="Opciones"
                            ShowEditButton="true"
                            ShowDeleteButton="true"
                            ShowSelectButton="true"
                            EditText="Liberar Turno"
                            UpdateText="Desvincular"
                            CancelText="Cancelar"
                            DeleteText="Deshabilitar"
                            SelectText="Iniciar Turno" />

                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
