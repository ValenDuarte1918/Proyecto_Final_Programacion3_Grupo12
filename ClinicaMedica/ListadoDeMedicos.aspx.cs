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
    public partial class ListadoDeMedicos : System.Web.UI.Page
    {
        private GestionTablas gestorTablas = new GestionTablas();
        private GestionDdl gestorDdl = new GestionDdl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnUserImg.Visible = true;

                llenarGrillaMedicos();
                gestorDdl.CargarEspecialidades(ddlEspecialidades);

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
        private void llenarGrillaMedicos(DataTable tabla = null)
        {
            if (tabla == null)
            {
               tabla = gestorTablas.ObtenerTablaMedicos();
            }
            gvMedicos.DataSource = tabla;
            gvMedicos.DataBind();
        }
        protected void btnBuscarMeds_Click(object sender, EventArgs e)
        {
            string legajo = txtBuscadorMeds.Text.Trim();
            string nombre = txtBuscadorNombre.Text.Trim();
            DataTable tablaFiltrada = null;

            // Validación: solo uno de los campos debe estar completo
            if (!string.IsNullOrEmpty(legajo) && !string.IsNullOrEmpty(nombre))
            {
                lblMensaje.Text = "Por favor, complete solo uno de los campos de búsqueda.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                llenarGrillaMedicos();
                return;
            }
            // si el campo de legajo está completo, filtramos por legajo
            if (!string.IsNullOrEmpty(legajo))
            {
                tablaFiltrada = gestorTablas.ObtenerTablaMedicosPorLegajo(legajo);
            }
            // si el campo de nombre está completo, filtramos por nombre
            else if (!string.IsNullOrEmpty(nombre))
            {
                tablaFiltrada = gestorTablas.ObtenerTablaMedicosPorNombre(nombre);
            }
            // si ninguno de los campos está completo, mostramos todos los médicos
            if (tablaFiltrada != null && tablaFiltrada.Rows.Count > 0)
            {
                llenarGrillaMedicos(tablaFiltrada);
                lblMensaje.Text = "";
            }
            else
            {
                lblMensaje.Text = "No se encontraron médicos con esos criterios.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                llenarGrillaMedicos();
            }
        }
        protected void btnFiltrarEspecialidad_Click(object sender, EventArgs e)
        {
            string idEspecialidad = ddlEspecialidades.SelectedValue;
            DataTable tablaFiltrada = gestorTablas.ObtenerTablaMedicosPorIdEspecialidad(idEspecialidad);
            if (tablaFiltrada.Rows.Count > 0)
            {
                llenarGrillaMedicos(tablaFiltrada);
            }
            else
            {
                lblMensaje.Text = "No se encontraron médicos con esa Especialidad.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                llenarGrillaMedicos();
            }
        }
        protected void btnMostrarTodo_Click(object sender, EventArgs e)
        {
            llenarGrillaMedicos(null);
        }
        protected void gvMedicos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int legajo = Convert.ToInt32(e.CommandArgument);

                var gestorMedicos = new GestionTablas.GestionMedicos();
                bool exito = gestorMedicos.DarDeBajaMedico(legajo);

                if (exito)
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Text = "Médico dado de baja exitosamente.";
                    llenarGrillaMedicos();
                }
                else
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = "No se pudo dar de baja al médico.";
                }
            }
        }
        protected void btnUnlogin_Click(object sender, EventArgs e)
        {
            Session["UsuarioActivo"] = null;
            Response.Redirect("ListadoTurnos.aspx");
        }
        protected void gvMedicos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMedicos.PageIndex = e.NewPageIndex;
            llenarGrillaMedicos();
        }

        // Permite poner la fila en modo edición
        protected void gvMedicos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMedicos.EditIndex = e.NewEditIndex;
            llenarGrillaMedicos();
        }

        // Cancela la edición
        protected void gvMedicos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMedicos.EditIndex = -1;
            llenarGrillaMedicos();
        }

        // Actualiza los datos editados
        protected void gvMedicos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Obtiene la fila que se está editando
            GridViewRow row = gvMedicos.Rows[e.RowIndex];
            // Obtiene los valores de los controles dentro de la fila
            string dni = ((Label)row.FindControl("lblDni")).Text;
            string nombre = ((TextBox)row.FindControl("txtNombre")).Text.Trim();
            string apellido = ((TextBox)row.FindControl("txtApellido")).Text.Trim();
            string telefono = ((TextBox)row.FindControl("txtTelefono")).Text.Trim();
            string correo = ((TextBox)row.FindControl("txtCorreo")).Text.Trim();
            // aca creamos una instancia del gestor de médicos
            var gestorMedicos = new GestionTablas.GestionMedicos();
            // Llama al método para actualizar el médico
            bool exito = gestorMedicos.ActualizarMedico(dni, nombre, apellido, telefono, correo);

            if (exito)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "Médico actualizado correctamente.";
            }
            else
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "No se pudo actualizar el médico.";
            }
            // Vuelve a cargar la grilla de médicos
            gvMedicos.EditIndex = -1;
            llenarGrillaMedicos();
        }

        protected void btnUserImg_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/CambiarContraseña.aspx");
        }
    }
}