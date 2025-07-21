<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AsignacionTurnos.aspx.cs" Inherits="ClinicaMedica.AsignacionTurnos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Asignación de Turnos</title>
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
                    <asp:HyperLink ID="hlCrearCuentaAdmin" runat="server" CssClass="header-link-active" NavigateUrl="~/CreacionCuentaAdmin.aspx" Text="Crear Cuenta Admin"></asp:HyperLink>
                    <asp:HyperLink ID="hlAsignarTurnos" runat="server" CssClass="header-link" NavigateUrl="AsignacionTurnos.aspx" Text="Asignar Turnos"></asp:HyperLink>
                    <asp:HyperLink ID="HlListarPacientes" runat="server" CssClass="header-link" NavigateUrl="ListadoPacientes.aspx" Text="Listar Pacientes"></asp:HyperLink>
                    <asp:HyperLink ID="hlListarMedicos" runat="server" CssClass="header-link" NavigateUrl="ListadoDeMedicos.aspx" Text="Listar Medicos"></asp:HyperLink>
                    <asp:HyperLink ID="hlAgregarMedico" runat="server" CssClass="header-link-active" NavigateUrl="RegistrarMedico.aspx" Text="Agregar Medico"></asp:HyperLink>
                    <asp:HyperLink ID="hlInformes" runat="server" CssClass="header-link" NavigateUrl="Informes.aspx" Text="Informes"></asp:HyperLink>
                </div>
            </div>
        </div>
        <div class="contenido">
            <div class="solapa">
                Asiganción de Turno
            </div>
            <div class="caja">
                <div style="margin: 15px 10px 20px 20px;">
                    <span style="color: white; font-size: 20px;">DNI paciente:</span>
                    &nbsp;<asp:TextBox ID="txtDni" runat="server" CssClass="txtBox-caja"></asp:TextBox>
                </div>

                <div style="margin: 5px 10px 20px 20px;">
                    <asp:Label ID="lblDatos" runat="server" Text="Datos" Font-Size="20px" ForeColor="White" Font-Underline="True"></asp:Label>
                </div>

                <div style="margin: 15px 10px 20px 20px;" id="ddlEspecialidad">
                    <span style="color: white; font-size: 20px;">Especialidad:</span>
                    <asp:DropDownList ID="ddlEspecialidades" AutoPostBack="true" runat="server" CssClass="txtBox-caja" OnSelectedIndexChanged="ddlEspecilidad_SelectedIndexChanged">
                    </asp:DropDownList>
                    <br />
                    <span style="color: white; font-size: 20px;">Médico:</span>
                    <asp:DropDownList ID="ddlMedicos" AutoPostBack="true" runat="server" CssClass="txtBox-caja" OnSelectedIndexChanged="ddlMedico_SelectedIndexChanged" Enabled="False"></asp:DropDownList>
                    <br />
                    <span style="color: white; font-size: 20px;">Fecha:</span>
                    <asp:DropDownList ID="ddlFechas" AutoPostBack="true" runat="server" CssClass="txtBox-caja" OnSelectedIndexChanged="ddlFecha_SelectedIndexChanged" Enabled="False"></asp:DropDownList>
                    <br />
                    <span style="color: white; font-size: 20px;">Hora:</span>
                    <asp:DropDownList ID="ddlHoras" AutoPostBack="true" runat="server" CssClass="txtBox-caja" Enabled="False"></asp:DropDownList>
                </div>
                <div style="margin: 25px 10px 5px 20px;">
                    <asp:Button ID="btnAsignarTurno" runat="server" CssClass="button" Text="Asignar Turno" Width="210px" Height="40" OnClick="btnAsignarTurno_Click" />

                    <asp:Panel ID="pnlConfirmacion" runat="server" Visible="false" CssClass="panel-confirmacion" Style="margin-top: 15px;">
                        <asp:Label ID="lblConfirmacion" runat="server" Text="¿Está seguro de que desea asignar este turno?" />
                        <br />
                        <br />
                        <asp:Button ID="btnConfirmar" runat="server" Text="Sí" OnClick="btnConfirmar_Click" CssClass="button" />
                        <asp:Button ID="btnCancelar" runat="server" Text="No" OnClick="btnCancelar_Click" CssClass="button" />
                        <br />
                    </asp:Panel>

                    <asp:Label ID="lblMensaje" runat="server" Visible="False" Font-Bold="True" ForeColor="#94FF6C"></asp:Label>
                </div>


            </div>
        </div>
    </form>
</body>
</html>
