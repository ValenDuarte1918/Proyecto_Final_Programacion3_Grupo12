using Entidades;
using Servicios;
using System;
using System.Web.UI.WebControls;

namespace ClinicaMedica
{
    public partial class CreacionCuentaAdmin : System.Web.UI.Page
    {
        private GestionUsuario creacionUsuario = new GestionUsuario();
        private GestionRegistros gestionRegistros = new GestionRegistros();
        private GestionDdl gestorDdl = new GestionDdl();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gestorDdl.CargarProvincias(ddlProvincias);
                gestorDdl.CargarLocalidades(ddlLocalidades, 0);

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
        protected void btnUnlogin_Click(object sender, EventArgs e)
        {
            Session["UsuarioActivo"] = null;
            Response.Redirect("ListadoTurnos.aspx");

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
        protected void btnCrearCuenta_Click(object sender, EventArgs e)
        {
            Paciente DatosPersonales = new Paciente(txtDni.Text, txtNombre.Text, txtApellido.Text, 
                ddlSexo.SelectedItem.Text, txtNacionalidad.Text, Convert.ToDateTime(txtFechaNacimiento.Text), 
                txtDireccion.Text, Convert.ToInt32(ddlProvincias.SelectedValue), Convert.ToInt32(ddlLocalidades.SelectedValue), 
                txtCorreoElectronico.Text, txtNumeroTelefono.Text);

            if (gestionRegistros.VerificarSiExiste(txtDni.Text))
            {
                lblMensaje.Text = "ya existe una cuenta con ese DNI";
            }
            else 
            {
                bool RegistroDatos = gestionRegistros.RegistrarPaciente(DatosPersonales);

                bool exito = false;
                if (RegistroDatos) 
                {
                    exito = creacionUsuario.CrearNuevaCuenta(txtNombre.Text, txtApellido.Text, txtDni.Text);
                }
                    
                string nombreCuenta = creacionUsuario.ObtenerNombreUsuario(txtDni.Text);
                if (exito)
                {
                    lblMensaje.Text = "Nombre de cuenta: " + nombreCuenta;
                }else
                {
                    lblMensaje.Text = "Ha ocurrido un error";
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListadoTurnos.aspx");
        }
    }
}