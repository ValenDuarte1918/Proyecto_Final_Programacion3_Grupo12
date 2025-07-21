<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeguimientosPacientes.aspx.cs" Inherits="ClinicaMedica.SeguimientosPacientes" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="Estilo/EstiloClinica.css" />
    <title>Consulta</title>
    <style type="text/css">
        #TextArea1 {
            width: 750px;
        }
    </style>
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
                    <asp:HyperLink ID="hlSeguimientoPaciente" runat="server" CssClass="header-link" Text="Listado de Turnos" Visible="True" TabIndex="5" NavigateUrl="~/ListadoTurnos.aspx"></asp:HyperLink>
                </div>
            </div>
        </div>
        <div class="contenido">

            <div class="caja-historial">
                <!-- Secciones en paralelo -->
                <div style="display: flex; justify-content: center; gap: 200px; margin-top: 30px;">

                    <!-- HISTORIAL -->
                    <div style="background-color: #f1f7ff; padding: 20px; border-radius: 10px; box-shadow: 0 0 15px rgba(0,0,0,0.2); width: 300px;">
                        <h2 class="titulo-informe" style="text-align: center; color: black;">Historial</h2>
                        <ul style="list-style-type: disc; padding-left: 20px; color: black;">
                            <li>
                                <asp:ListView ID="lvHistorial" runat="server">
                                    <AlternatingItemTemplate>
                                        <li style="">Observacion:
                                        <asp:Label ID="ObservacionLabel" runat="server" Text='<%# Eval("Observacion") %>' />
                                            <br />
                                        </li>
                                    </AlternatingItemTemplate>
                                    <EditItemTemplate>
                                        <li style="">Observacion:
                                        <asp:TextBox ID="ObservacionTextBox" runat="server" Text='<%# Bind("Observacion") %>' />
                                            <br />
                                            <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                                        </li>
                                    </EditItemTemplate>
                                    <EmptyDataTemplate>
                                        No data was returned.
                                    </EmptyDataTemplate>
                                    <InsertItemTemplate>
                                        <li style="">Observacion:
                                        <asp:TextBox ID="ObservacionTextBox" runat="server" Text='<%# Bind("Observacion") %>' />
                                            <br />
                                            <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                                        </li>
                                    </InsertItemTemplate>
                                    <ItemSeparatorTemplate>
                                        <br />
                                    </ItemSeparatorTemplate>
                                    <ItemTemplate>
                                        <li style="">Observacion:
                                        <asp:Label ID="ObservacionLabel" runat="server" Text='<%# Eval("Observacion") %>' />
                                            <br />
                                        </li>
                                    </ItemTemplate>
                                    <LayoutTemplate>
                                        <ul id="itemPlaceholderContainer" runat="server" style="">
                                            <li runat="server" id="itemPlaceholder" />
                                        </ul>
                                        <div style="">
                                        </div>
                                    </LayoutTemplate>
                                    <SelectedItemTemplate>
                                        <li style="">Observacion:
                                        <asp:Label ID="ObservacionLabel" runat="server" Text='<%# Eval("Observacion") %>' />
                                            <br />
                                        </li>
                                    </SelectedItemTemplate>
                                </asp:ListView>
                                <asp:SqlDataSource ID="ClinicaMedica" runat="server" ConnectionString="<%$ ConnectionStrings:ClinicaMedicaConnectionString2 %>" ProviderName="<%$ ConnectionStrings:ClinicaMedicaConnectionString2.ProviderName %>" SelectCommand="SELECT [Observacion] FROM [SeguimientoPaciente]"></asp:SqlDataSource>
                            </li>
                        </ul>
                    </div>



                    <div class="caja-informe">
                        <h2 class="titulo-informe">Comentario</h2>

                        <div class="caja-informe" style="background: #f1f7ff; margin-bottom: 20px;">
                            <div style="text-align: center; margin-bottom: 10px;">
                                <asp:Label ID="lblNombre" runat="server" Text="Paciente: " Font-Bold="True" />
                                <asp:Label ID="lblNombrePaciente" runat="server" Font-Bold="True" Style="margin-right: 15px;" />

                                <asp:Label ID="lblDNI" runat="server" Text="DNI: " Font-Bold="True" />
                                <asp:Label ID="lblDniPaciente" runat="server" Font-Bold="True" Style="margin-right: 15px;" />

                                <asp:Label ID="lblFecha" runat="server" Text="Fecha: " Font-Bold="True" />

                                <asp:Label ID="lblFechaTurno" runat="server" Font-Bold="True" />
                            </div>
                            <div style="text-align: center;">
                                <textarea id="txtComentario" runat="server" name="S1" rows="2" style="width: 750px;" enableviewstate="True"></textarea>
                            </div>

                            <div style="text-align: right; margin-top: 10px;">
                                <br />
                                <asp:RequiredFieldValidator ID="validarQueHayaComentario" runat="server" ControlToValidate="txtComentario" ErrorMessage="Debe ingresar un comentario" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblMensaje" runat="server" Font-Bold="true" />
                                <br />
                                <br />
                                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" Visible="False" OnClick="btnAceptar_Click" />
                                <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" Visible="False" Style="margin-right: 15px;" OnClick="btnConfirmar_Click" />
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Visible="False" OnClick="btnCancelar_Click" />
                                <asp:Button ID="btnFinalizarConsulta" runat="server"
                                    Style="margin-right: 5px; height: 28px;"
                                    Text="Finalizar consulta"
                                    OnClick="btnFinalizarConsulta_Click" />
                            </div>

                            <div style="text-align: right; margin-top: 5px;">
                            </div>


                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
