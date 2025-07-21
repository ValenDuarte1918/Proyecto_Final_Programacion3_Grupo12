using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Servicios.GestionTablas;

namespace ClinicaMedica
{
    public partial class ListadoPacientes : System.Web.UI.Page
    {
        private GestionTablas gestorTablas = new GestionTablas();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnUserImg.Visible = true;

                llenarGrillaPacientes();
                //gestorDdl.CargarEspecialidades(ddlEspecialidades);

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
        private void llenarGrillaPacientes(DataTable tabla = null)
        {
            if (tabla == null)
            {
               tabla = gestorTablas.ObtenerTablaPacientes();
            }
            gvPacientes.DataSource = tabla;
            gvPacientes.DataBind();
        }
        protected void btnBuscarPac_Click(object sender, EventArgs e)
        {
            string dni = txtBuscadorPac.Text.Trim();
            string nombre = txtBuscadorNombre.Text.Trim();
            DataTable tablaFiltrada = null;

            // Validación: solo uno de los campos debe estar completo
            if (!string.IsNullOrEmpty(dni) && !string.IsNullOrEmpty(nombre))
            {
                lblMensaje.Text = "Por favor, complete solo uno de los campos de búsqueda.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                llenarGrillaPacientes();
                return;
            }
            // si el campo de dni está completo, filtramos por dni
            if (!string.IsNullOrEmpty(dni))
            {
                tablaFiltrada = gestorTablas.ObtenerTablaPacientesPorDni(dni);
            }
            // si el campo de nombre está completo, filtramos por nombre
            else if (!string.IsNullOrEmpty(nombre))
            {
                tablaFiltrada = gestorTablas.ObtenerTablaPacientesPorNombre(nombre);
            }
            // si ninguno de los campos está completo, mostramos todos los Pacientes
            if (tablaFiltrada != null && tablaFiltrada.Rows.Count > 0)
            {
                llenarGrillaPacientes(tablaFiltrada);
                lblMensaje.Text = "";
            }
            else
            {
                lblMensaje.Text = "No se encontraron Pacientes con esos criterios.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                llenarGrillaPacientes();
            }
        }
        
        protected void btnMostrarTodo_Click(object sender, EventArgs e)
        {
            llenarGrillaPacientes(null);
        }
        protected void gvPacientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int dni = Convert.ToInt32(e.CommandArgument);

                var gestorPacientes = new GestionTablas.GestionMedicos();
                bool exito = gestorPacientes.DarDeBajaMedico(dni);//Hay que cambiar esto

                if (exito)
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Text = "Paciente dado de baja exitosamente.";
                    llenarGrillaPacientes();
                }
                else
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = "No se pudo dar de baja al Paciente.";
                }
            }
        }
        protected void btnUnlogin_Click(object sender, EventArgs e)
        {
            Session["UsuarioActivo"] = null;
            Response.Redirect("ListadoTurnos.aspx");
        }
        protected void gvPacientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPacientes.PageIndex = e.NewPageIndex;
            llenarGrillaPacientes();
        }

        // Permite poner la fila en modo edición
        protected void gvPacientes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPacientes.EditIndex = e.NewEditIndex;
            llenarGrillaPacientes();
        }

        // Cancela la edición
        protected void gvPacientes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPacientes.EditIndex = -1;
            llenarGrillaPacientes();
        }

        // Actualiza los datos editados
        protected void gvPacientes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Obtiene la fila que se está editando
            GridViewRow row = gvPacientes.Rows[e.RowIndex];
            // Obtiene los valores de los controles dentro de la fila
            string dni = ((Label)row.FindControl("lblDni")).Text;
            string nombre = ((TextBox)row.FindControl("txtNombre")).Text.Trim();
            string apellido = ((TextBox)row.FindControl("txtApellido")).Text.Trim();
            string telefono = ((TextBox)row.FindControl("txtTelefono")).Text.Trim();
            string correo = ((TextBox)row.FindControl("txtCorreo")).Text.Trim();
            // aca creamos una instancia del gestor de Paciente

            //Paciente 

            var gestorMedicos = new GestionTablas.GestionMedicos();
            // Llama al método para actualizar el Paciente
            bool exito = gestorMedicos.ActualizarMedico(dni, nombre, apellido, telefono, correo);

            if (exito)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "Paciente actualizado correctamente.";
            }
            else
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "No se pudo actualizar el Paciente.";
            }
            // Vuelve a cargar la grilla de Pacientes
            gvPacientes.EditIndex = -1;
            llenarGrillaPacientes();
        }

        protected void btnUserImg_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/CambiarContraseña.aspx");
        }

        protected void gvPacientes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}