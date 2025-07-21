using Entidades;
using Servicios;
using System;

namespace ClinicaMedica
{
    public partial class CambiarContrasenia : System.Web.UI.Page
    {
        private GestionUsuario GestorUsuario = new GestionUsuario();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HabilitacionDeAcceso();

                if (Session["UsuarioActivo"] != null)
                {
                    Usuario usuario = (Usuario)Session["UsuarioActivo"];

                    lblBienvenidoUsuario.Text = usuario.NombreUsuario + " " + usuario.ApellidoUsuario;
                }
                else
                {
                    Response.Redirect("ListadoTurnos.aspx");
                }
            }
        }

        protected void HabilitacionDeAcceso()
        {
            if (Session["UsuarioActivo"] != null)
            {
                if (((Usuario)Session["UsuarioActivo"]).TipoUsuario > 1)
                {
                    hlAgregarMedico.Visible = true;

                    hlAsignarTurnos.Visible = true;
                    hlInformes.Visible = true;
                    hlListarMedicos.Visible = true;
                    HlListarPacientes.Visible = true;
                    hlCrearCuentaAdmin.Visible = true;
                }

                if (((Usuario)Session["UsuarioActivo"]).TipoUsuario >= 1)
                {
                    btnUserImg.Visible = true;
                    MenuUsuario.Visible = true;
                    hlListarTurnos.Visible = true;
                }
            }
        }
        protected void btnUnlogin_Click(object sender, EventArgs e)
        {
            Session["UsuarioActivo"] = null;
            Response.Redirect("ListadoTurnos.aspx");

        }
        protected void btnCambiarContrasenia_Click(object sender, EventArgs e)
        {
            string nuevaClave = txtContraseniaNueva.Text;
            string confirmarClave = txtConfirmarContraseniaNueva.Text;
            
                Usuario usuario = (Usuario)Session["UsuarioActivo"];
                if (usuario != null)
                {
                   GestionUsuario gestionUsuario = new GestionUsuario();
                    bool resultado = gestionUsuario.CambiarContrasenia(usuario, nuevaClave);
                    if (resultado)
                    {
                        lblMensaje.Text = "Contraseña cambiada exitosamente.";
                        txtContraseniaNueva.Text = string.Empty;
                        txtConfirmarContraseniaNueva.Text = string.Empty;
                    }
                    else
                    {
                        lblMensaje.Text = "Error al cambiar la contraseña";
                    }
                }
                else
                {
                    lblMensaje.Text = "error...";
                }
            
        }
    }
}