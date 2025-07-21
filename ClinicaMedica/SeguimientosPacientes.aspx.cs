using Entidades;
using Servicios;
using System;
using System.Data;
using System.Web.UI;


namespace ClinicaMedica
{
    public partial class SeguimientosPacientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["TurnoSeleccionado"] != null)
            {
               
                if (Session["UsuarioActivo"] != null)
                {
                    btnUserImg.Visible = true;

                    var turno = (Turno)Session["TurnoSeleccionado"];
                    Usuario usuario = (Usuario)Session["UsuarioActivo"];
                    ListarHistorialDelPaciente(turno.DniPaciente);
                    lblBienvenidoUsuario.Text = usuario.NombreUsuario + " " + usuario.ApellidoUsuario;

                    DateTime Fecha = DateTime.Now;

                    lblDniPaciente.Text = turno.DniPaciente;
                    lblFechaTurno.Text = Fecha.ToString("dd/MM/yyyy");
                    lblNombrePaciente.Text = turno.NombrePaciente;
                }
                else
                {
                    Response.Redirect("ListadoTurnos.aspx");
                }
            }
        }

        protected void btnFinalizarConsulta_Click(object sender, EventArgs e)
        {
            btnFinalizarConsulta.Enabled = false;

            btnConfirmar.Visible = true;
            btnCancelar.Visible = true;

            txtComentario.EnableViewState = false;
          

            lblMensaje.Text = "Esta seguro de terminar la consulta?";
        }

        protected void btnUnlogin_Click(object sender, EventArgs e)
        {
            Session["UsuarioActivo"] = null;
            Response.Redirect("ListadoTurnos.aspx");
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            Turno turnoIniciado = (Turno)Session["TurnoSeleccionado"];

            string comentario = txtComentario.Value;

            GestionRegistros gestion = new GestionRegistros();
            bool comentarioRegistrado = gestion.RegistrarSeguimiento(turnoIniciado, comentario);

            if (comentarioRegistrado)
            {
                lblMensaje.Text = "Comentario registrado correctamente.";
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                ListarHistorialDelPaciente(turnoIniciado.DniPaciente);
            }
            else
            {
                lblMensaje.Text = "Error al guardar el comentario.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }

            btnConfirmar.Visible = false;
            btnCancelar.Visible = false;

            btnAceptar.Visible = true;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            btnFinalizarConsulta.Enabled = true;

            btnConfirmar.Visible = false;
            btnCancelar.Visible = false;

            txtComentario.EnableViewState = true;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Session["TurnoSeleccionado"] = null;
            Response.Redirect("ListadoTurnos.aspx");
        }

        protected void ListarHistorialDelPaciente(string dni)
        {
            GestionRegistros gestionRegistros = new GestionRegistros();
            DataTable HistorialPorPersona = gestionRegistros.ListarHistorialDelPaciente(dni);
           
            if (HistorialPorPersona != null && HistorialPorPersona.Rows.Count > 0)//veo que hay registros 
            {
                lvHistorial.DataSource = HistorialPorPersona;
                lvHistorial.DataBind();
            }
           
        }

        protected void btnUserImg_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/CambiarContraseña.aspx");
        }
    }
}