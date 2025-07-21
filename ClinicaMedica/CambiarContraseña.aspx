<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CambiarContraseña.aspx.cs" Inherits="ClinicaMedica.CambiarContraseña" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="Estilo/EstiloClinica.css" />
    <title>Cambio de contraseña</title>
</head>
<body>
    <form id="form2" runat="server">
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

                    <asp:HyperLink ID="hlAgregarMedico" runat="server" CssClass="header-link" NavigateUrl="RegistrarMedico.aspx" Text="Agregar Medico"></asp:HyperLink>
                    <asp:HyperLink ID="hlAsignarTurnos" runat="server" CssClass="header-link" NavigateUrl="AsignacionTurnos.aspx" Text="Asignar Turnos"></asp:HyperLink>
                    <asp:HyperLink ID="hListarMedicos" runat="server" CssClass="header-link" NavigateUrl="ListadoDeMedicos.aspx" Text="Listar Medicos"></asp:HyperLink>
                    <asp:HyperLink ID="HlListarPacientes" runat="server" CssClass="header-link" NavigateUrl="ListadoPacientes.aspx" Text="Listar Pacientes"></asp:HyperLink>
                    <asp:HyperLink ID="hlInformes" runat="server" CssClass="header-link" NavigateUrl="Informes.aspx" Text="Informes"></asp:HyperLink>
                </div>
            </div>
        </div>
        <div class="contenido">
            <div class="caja-informe">
                <h2 class="titulo-informe">Cambiar Contraseña</h2>
                <div style="margin-bottom: 25px; display: flex; align-items: center; justify-content: center;">
                    <asp:TextBox ID="txtContraseniaNueva" runat="server" CssClass="input-fecha" TextMode="Password" Width="500px" placeholder="Contraseña nueva" />
                    <br />
                    <asp:RequiredFieldValidator ID="rfvContraseniaNueva1" runat="server" ControlToValidate="txtContraseniaNueva">Por favor rellene el campo</asp:RequiredFieldValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="revContraseniaNueva1" runat="server" ControlToValidate="txtContraseniaNueva" ValidationExpression="^(?=.*[A-Z]).{8,20}$">Se ingreso una contraseña muy larga o muy corta(8 a 20) Debe tener al menos una Mayuscula</asp:RegularExpressionValidator>
                    <br />
                </div>

                <div style="margin-bottom: 25px; display: flex; flex-direction: column; justify-content: flex-start;">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="White" Text="Seguridad de la contraseña:" Font-Overline="False" Font-Underline="True"></asp:Label>
                    <asp:Label ID="lblCondicion1" runat="server" Text="• minimo 8 y máximo 20 caracteres" CssClass="label" Style="margin-top: 5px; color: white;" />
                    <asp:Label ID="lblCondicion2" runat="server" Text="• debe poseer al menos un caracter en mayuscula y minuscula" CssClass="label" Style="margin-top: 5px; color: white;" />
                    <asp:Label ID="lblCondicion3" runat="server" Text="• no debe coincidir con alguno de sus datos registrados" CssClass="label" Style="margin-top: 5px; color: white;" />
                </div>

                <div style="margin-bottom: 25px; display: flex; align-items: center; justify-content: center;">
                    <asp:TextBox ID="txtConfirmarContraseniaNueva" runat="server" CssClass="input-fecha" TextMode="Password" Width="500px" placeholder="Confirma la nueva contraseña" />
                    <br />
                    <asp:RequiredFieldValidator ID="rfvContraseniaNueva2" runat="server" ControlToValidate="txtConfirmarContraseniaNueva">Por favor rellene el campo</asp:RequiredFieldValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="revContraseniaNueva2" runat="server" ControlToValidate="txtConfirmarContraseniaNueva" ValidationExpression="^(?=.*[A-Z]).{8,20}$">Se ingreso una contraseña muy larga o muy corta(8 a 20)Debe tener al menos una Mayuscula</asp:RegularExpressionValidator>
                </div>

                <div style="margin-bottom: 25px; display: flex; align-items: center; justify-content: center;">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CompareValidator ID="cvContrasenias" runat="server" ControlToCompare="txtContraseniaNueva" ControlToValidate="txtConfirmarContraseniaNueva" ErrorMessage="Las contraseñas no coinciden"></asp:CompareValidator>
                &nbsp;<asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </div>

                <div style="margin-bottom: 25px; display: flex; justify-content: flex-end;">
                    <asp:Button ID="btnCambiarContrasenia" runat="server" Text="Cambiar contraseña" CssClass="boton-hover" OnClick="btnCambiarContrasenia_Click" />
                </div>
            </div>
            <asp:Panel ID="crearAdmin" runat="server" Visible="false">
                <div class="caja-informe" style="margin-top: 10px;" visible="false">
                    <h2 class="titulo-informe">Crear Cuenta Administrador</h2>

                    <div style="margin-bottom: 25px; display: flex; align-items: center; justify-content: center;">
                        <asp:TextBox ID="txtUsuarioAdmin" runat="server" CssClass="input-fecha" Width="500px" placeholder="Nombre de usuario" />
                    </div>

                    <div style="margin-bottom: 25px; display: flex; align-items: center; justify-content: center;">
                        <asp:TextBox ID="txtDniAdmin" runat="server" CssClass="input-fecha" Width="500px" placeholder="DNI" />
                    </div>

                    <div style="margin-bottom: 25px; display: flex; align-items: center; justify-content: center;">
                        <asp:TextBox ID="txtContraseniaAdmin" runat="server" CssClass="input-fecha" TextMode="Password" Width="500px" placeholder="Contraseña" />
                    </div>

                    <div style="margin-bottom: 25px; display: flex; align-items: center; justify-content: center;">
                    </div>

                    <div style="margin-bottom: 25px; display: flex; justify-content: flex-end;">
                        <asp:Button ID="btnCrearCuentaAdmin" runat="server" Text="Crear Cuenta" CssClass="boton-hover" OnClick="btnCrearCuentaAdmin_Click" />
                    </div>
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
