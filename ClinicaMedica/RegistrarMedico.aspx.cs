using Entidades;
using Servicios;
using System;
//using System.Collections.Generic;
using System.Drawing;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClinicaMedica
{
    public partial class RegistrarMedico : System.Web.UI.Page
    {
        private GestionDdl gestorDdl = new GestionDdl();
        GestionRegistros registros = new GestionRegistros();
        private string legajo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnUserImg.Visible = true;

                gestorDdl.CargarProvincias(ddlProvincias);
                gestorDdl.CargarLocalidades(ddlLocalidades, 0);
                gestorDdl.CargarEspecialidades(ddlEspecialidades);
                CargarProxLegajo();


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

        protected void ddlProvincias_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int idProvincia = int.Parse(ddlProvincias.SelectedValue);

            if (idProvincia != 0)
            {
                gestorDdl.CargarLocalidades(ddlLocalidades, idProvincia);
            }
            else
            {
                ddlLocalidades.Items.Clear();
                ddlLocalidades.Items.Insert(0, new ListItem("-- Seleccione una provincia primero --", "0"));
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (CamposIncompletos())
            {
                lblMensaje.Text = "Por favor, complete todos los campos.";
                lblMensaje.Visible = true;
                lblMensaje.ForeColor = Color.Red;
                return;
            }

            DateTime fechaNacimiento;
            if (!DateTime.TryParse(txtFechaNacimiento.Text, out fechaNacimiento))
            {
                lblMensaje.Text = "La fecha de nacimiento no es válida.";
                lblMensaje.Visible = true;
                lblMensaje.ForeColor = Color.Red;
                return;
            }

            // Validar rango permitido
            if (fechaNacimiento < new DateTime(1930, 1, 1) || fechaNacimiento > new DateTime(2025, 12, 31))
            {
                lblMensaje.Text = "La fecha de nacimiento invalida. Debe estar entre 1930 y 2025.";
                lblMensaje.Visible = true;
                lblMensaje.ForeColor = Color.Red;
                return;
            }
            // validar que el DNI no exista y si existe, debe tener como máximo 8 dígitos
            if (txtDniMedico.Text.Length > 8 || txtDniMedico.Text.Length < 7)
            {
                lblMensaje.Text = "El DNI debe tener entre 7 y 8 dígitos.";
                lblMensaje.Visible = true;
                lblMensaje.ForeColor = Color.Red;
                return;
            }
            if (registros.VerificarSiExiste(txtDniMedico.Text.Trim()))
            {
                lblMensaje.Text = "El DNI ingresado ya está registrado.";
                lblMensaje.Visible = true;
                lblMensaje.ForeColor = Color.Red;
                return;
            }


            Medico nuevoMedico = new Medico
            {
                Dni = txtDniMedico.Text.Trim(),
                Nombre = txtNombre.Text.Trim(),
                Apellido = txtApellido.Text.Trim(),
                Sexo = ddlSexo.SelectedValue,
                Nacionalidad = txtNacionalidad.Text.Trim(),
                FechaNacimiento = fechaNacimiento,
                IdProvincia = int.Parse(ddlProvincias.SelectedValue),
                IdLocalidad = int.Parse(ddlLocalidades.SelectedValue),
                Direccion = txtDireccion.Text.Trim(),
                Correo = txtCorreoElectronico.Text.Trim(),
                Telefono = txtNumeroTelefono.Text.Trim(),
                IdEspecialidad = int.Parse(ddlEspecialidades.SelectedValue)
            };

            if (registros.RegistarMedico(nuevoMedico))
            {
                lblMensaje.Text = "Médico registrado correctamente.";
                lblMensaje.Visible = true;
                lblMensaje.ForeColor = System.Drawing.ColorTranslator.FromHtml("#94ff6c");
            }
            else
            {
                lblMensaje.Text = "Error al registrar médico";
                lblMensaje.Visible = true;
                lblMensaje.ForeColor = Color.Red;
            }
            CargarProxLegajo();
            LimpiarCasillas();
        }

        private void CargarProxLegajo()
        {
            legajo = registros.ObtenerProxLegajo();
            txtLegajo.Text = legajo;
        }
        private bool CamposIncompletos()
        {
            return string.IsNullOrWhiteSpace(txtNombre.Text) ||
                   string.IsNullOrWhiteSpace(txtApellido.Text) ||
                   string.IsNullOrWhiteSpace(txtDniMedico.Text) ||
                   string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                   string.IsNullOrWhiteSpace(txtFechaNacimiento.Text) ||
                   string.IsNullOrWhiteSpace(txtNacionalidad.Text) ||
                   ddlProvincias.SelectedValue == "0" ||
                   ddlLocalidades.SelectedValue == "0" ||
                   ddlEspecialidades.SelectedValue == "0" ||
                   string.IsNullOrWhiteSpace(txtCorreoElectronico.Text) ||
                   string.IsNullOrWhiteSpace(txtNumeroTelefono.Text);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = string.Empty;
            LimpiarCasillas();
        }

        private void LimpiarCasillas()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtDniMedico.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtFechaNacimiento.Text = string.Empty;
            txtNacionalidad.Text = string.Empty;
            txtCorreoElectronico.Text = string.Empty;
            txtNumeroTelefono.Text = string.Empty;

            ddlProvincias.SelectedIndex = 0;
            ddlLocalidades.SelectedIndex = 0;
            ddlEspecialidades.SelectedIndex = 0;
            ddlSexo.SelectedIndex = 0;

            //lblMensaje.Text = string.Empty;
        }

        protected void btnUnlogin_Click(object sender, EventArgs e)
        {
            Session["UsuarioActivo"] = null;
            Response.Redirect("ListadoTurnos.aspx");

        }

        protected void btnUserImg_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect("~/CambiarContraseña.aspx");
        }
    }
}