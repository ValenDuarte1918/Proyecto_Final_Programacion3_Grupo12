<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreacionCuentaAdmin.aspx.cs" Inherits="ClinicaMedica.CreacionCuentaAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" type="text/css" href="Estilo/EstiloClinica.css" />
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
            Creacion de cuenta de administrador</div>
        <div class="caja">
            <div class="form-row">
                <div class="form-col" style="display: inline;">
                    <div class="form-group">
                        <label class="form-label">Nombre:</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="txtBox-caja" placeholder="Claudio"></asp:TextBox>

                        <label class="form-label">Apellido:</label>
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="txtBox-caja" placeholder="Fernandez"></asp:TextBox>

                        <label class="form-label">Sexo:</label>
                        <asp:DropDownList ID="ddlSexo" runat="server" CssClass="txtBox-caja">
                            <asp:ListItem Text="Seleccione Sexo"></asp:ListItem>
                            <asp:ListItem Text="Masculino" Value="Masculino"></asp:ListItem>
                            <asp:ListItem Text="Femenino" Value="Femenino"></asp:ListItem>
                            <asp:ListItem Text="Otro" Value="Otro"></asp:ListItem>
                        </asp:DropDownList>

                        <label class="form-label">Nacionalidad:</label>
                        <asp:TextBox ID="txtNacionalidad" runat="server" CssClass="txtBox-caja" placeholder="Argentina"></asp:TextBox>

                        <label class="form-label">Fecha de nacimiento:</label>
                        <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="txtBox-caja" TextMode="Date" placeholder="1/1/1992"></asp:TextBox>

                        <label class="form-label">DNI:</label>
                        <asp:TextBox ID="txtDni" runat="server" CssClass="txtBox-caja" placeholder="12345678" MaxLength="10"></asp:TextBox>

                    </div>
                </div>
                <div class="form-col" style="display: inline;">

                    <label class="form-label">Provincia:</label>
                    <asp:DropDownList ID="ddlProvincias" AutoPostBack="true" OnSelectedIndexChanged="ddlProvincias_OnSelectedIndexChanged" runat="server" CssClass="txtBox-caja"></asp:DropDownList>

                    <label class="form-label">Localidad:</label>
                    <asp:DropDownList ID="ddlLocalidades" runat="server" CssClass="txtBox-caja"></asp:DropDownList>

                    <label class="form-label">Direccion:</label>
                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="txtBox-caja" placeholder="Hipólito Yrigoyen 288"></asp:TextBox>

                    <label class="form-label">Correo electrónico:</label>
                    <asp:TextBox ID="txtCorreoElectronico" runat="server" CssClass="txtBox-caja" placeholder="ejemplo@correo.com" TextMode="Email"></asp:TextBox>

                    <label class="form-label">Numero de telefono:</label>
                    <asp:TextBox ID="txtNumeroTelefono" runat="server" CssClass="txtBox-caja" placeholder="1512345678" TextMode="Phone"></asp:TextBox>

                    &nbsp;</div>
            </div>
            <div style="display: flex; flex-direction: column; align-items: center;">
                <br />
                <asp:Label ID="lblMensaje" runat="server" Style="font-weight: 700; font-size: 15px;"></asp:Label>
            </div>
            <div class="form-actions" style="margin-top: 15px;">
                <asp:Button ID="btnCrear" runat="server" Text="Crear" CssClass="btn-aceptar" OnClick="btnCrearCuenta_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn-cancelar" OnClick="btnCancelar_Click" />
            </div>
        </div>
    </div>
</form>
</body>
</html>
