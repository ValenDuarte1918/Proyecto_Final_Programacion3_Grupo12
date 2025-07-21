<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Informes.aspx.cs" Inherits="ClinicaMedica.Informes" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="Estilo/EstiloClinica.css" />
    <title>Informes</title>
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
                    <asp:HyperLink ID="hlListarTurnos" runat="server" CssClass="header-link" NavigateUrl="~/ListadoTurnos.aspx" Text="Listado de Turnos"></asp:HyperLink>
                    <asp:HyperLink ID="hlAgregarMedico" runat="server" CssClass="header-link" NavigateUrl="RegistrarMedico.aspx" Text="Agregar Medico"></asp:HyperLink>
                    <asp:HyperLink ID="hlAsignarTurnos" runat="server" CssClass="header-link" NavigateUrl="AsignacionTurnos.aspx" Text="Asignar Turnos"></asp:HyperLink>
                    <asp:HyperLink ID="hListarMedicos" runat="server" CssClass="header-link" NavigateUrl="ListadoDeMedicos.aspx" Text="Listar Medicos"></asp:HyperLink>
                    <asp:HyperLink ID="HlListarPacientes" runat="server" CssClass="header-link" NavigateUrl="ListadoPacientes.aspx" Text="Listar Pacientes"></asp:HyperLink>
                    <asp:HyperLink ID="hlInformes" runat="server" CssClass="header-link-active" NavigateUrl="Informes.aspx" Text="Informes"></asp:HyperLink>
                </div>
            </div>
        </div>
        <div class="contenido">
            <div class="caja-informe">
                <h2 class="titulo-informe">Informe de Asistencia</h2>
                <div style="margin-bottom: 25px; display: flex; align-items: center; justify-content: center;">
                    <asp:Label ID="lblFechaDesde" runat="server" Text="Desde:" CssClass="label-fecha" />
                    <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="input-fecha" TextMode="Date" />
                    <asp:Label ID="lblFechaHasta" runat="server" Text="Hasta:" CssClass="label-fecha" Style="margin-left: 20px;" />
                    <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="input-fecha" TextMode="Date" />
                    <asp:Button ID="btnGenerarInforme" runat="server" Text="Generar Informe" CssClass="boton-hover" Style="margin-left: 30px;" OnClick="btnGenerarInforme_Click" />
                </div>

                <!-- Estadísticas generales -->
                <div class="caja-informe" style="background: #f1f7ff; margin-bottom: 20px;">
                    <h3>Resumen Estadístico</h3>
                    Los presentes este mes alcanzan el porcentaje de:
                <asp:Label ID="lblPresentes" runat="server"></asp:Label>
                    <asp:Label ID="lblInformePresentismo" runat="server" CssClass="form-label" />
                    Los ausentes este mes alcanzan el porcentaje de:
                <asp:Label ID="lblAusentes" runat="server"></asp:Label>
                    <asp:Label ID="lblInformeAusentismo" runat="server" CssClass="form-label" />
                    con respecto al mes anterior se ve una diferencia en atencion del:<asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    &nbsp;<asp:Label ID="lblInformeComparacionMesAnterior" runat="server" CssClass="form-label" />
                </div>

            </div>
        </div>
    </form>
</body>
</html>
