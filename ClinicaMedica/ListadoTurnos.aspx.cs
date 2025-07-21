using Entidades;
using Servicios;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;



namespace ClinicaMedica
{
    public partial class ListadoTurnos : System.Web.UI.Page
    {
        private GestionTablas gestionTablas = new GestionTablas();
        private GestionDdl gestionDdl = new GestionDdl();
        private Turno ConfiguracionTurno = new Turno();
        private GestionRegistros GestorRegistros = new GestionRegistros();
        private CommandField ColumnaOpciones = new CommandField();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gestionDdl.CargarFechas(ddlFechas);
                gestionDdl.CargarEstados(ddlEstados);

                ComprobacionDeSesion();
                HabilitacionDeAcceso();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Session["UsuarioActivo"] == null)
            {
                string user = txtUsuario.Text;
                string pass = txtContrasenia.Text;

                GestionUsuario gestionUsuario = new GestionUsuario();


                Usuario usuario = gestionUsuario.Loguear(user, pass);
                

                if (usuario != null)
                {
                    Session["UsuarioActivo"] = usuario;
                    ComprobacionDeSesion();
                }
                else
                {
                    lblBienvenidoUsuario.ForeColor = System.Drawing.Color.Red;
                    lblBienvenidoUsuario.Text = "Usuario o contraseña incorrectos";
                }
            }
        }

        protected void ComprobacionDeSesion()
        {
            if (Session["UsuarioActivo"] != null)
            {
                btnLogin.Visible = false;
                txtContrasenia.Visible = false;
                txtUsuario.Visible = false;
                lblContrasenia.Visible = false;
                lblNombreUsuario.Visible = false;
                btnUserImg.Visible = true;
                lblBienvenidoUsuario.ForeColor = System.Drawing.Color.White;
                lblBienvenidoUsuario.Text = ((Usuario)Session["UsuarioActivo"]).NombreUsuario + " " + ((Usuario)Session["UsuarioActivo"]).ApellidoUsuario;
                HabilitacionDeAcceso();
            }
            else
            {
                btnLogin.Visible = true;
                txtContrasenia.Visible = true;
                txtUsuario.Visible = true;
                lblContrasenia.Visible = true;
                lblNombreUsuario.Visible = true;
                lblBienvenidoUsuario.Text = string.Empty;
                gvTurnos.DataSource = null;
                gvTurnos.DataBind();
                HabilitacionDeAcceso();
            }
        }

        protected void btnUnlogin_Click(object sender, EventArgs e)
        {
            Session["UsuarioActivo"] = null;
            ComprobacionDeSesion();
        }

        protected void HabilitacionDeAcceso()
        {
            if (Session["UsuarioActivo"] != null)
            {
                ColumnaOpciones = (CommandField)gvTurnos.Columns[7];


                if (((Usuario)Session["UsuarioActivo"]).TipoUsuario > 1)
                {
                    hlAgregarMedico.Visible = true;

                    hlAsignarTurnos.Visible = true;
                    hlInformes.Visible = true;
                    hlListarMedicos.Visible = true;
                    HlListarPacientes.Visible = true;

                    if (ddlFechas.SelectedItem != null 
                        && ddlFechas.SelectedValue != "0" 
                        && !string.IsNullOrWhiteSpace(ddlFechas.SelectedItem.Text))
                    {
                        ConfiguracionTurno.Fecha = DateTime.ParseExact(ddlFechas.SelectedItem.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    
                    ColumnaOpciones.ShowSelectButton = false;
                    ColumnaOpciones.ShowEditButton = false;
                    ColumnaOpciones.ShowDeleteButton = true;
                }

                if (((Usuario)Session["UsuarioActivo"]).TipoUsuario >= 1)
                {
                    btnUserImg.Visible = true;
                    MenuUsuario.Visible = true;
                    hlListarTurnos.Visible = true;
                    lblFecha.Visible = true;
                    ddlFechas.Visible = true;
                    btnMostrarTodo.Visible = true;
                    btnConsultarEstado.Visible = true;
                    ddlEstados.Visible = true;
                    gvTurnos.Columns[7].Visible = true;

                    gestionDdl.CargarFechas(ddlFechas);

                    ConfiguracionTurno = new Turno();

                    if (((Usuario)Session["UsuarioActivo"]).TipoUsuario == 1)
                    {
                        gestionDdl.CargarFechas(ddlFechas, null, ((Usuario)Session["UsuarioActivo"]).LegajoDoctor);

                        ColumnaOpciones.ShowSelectButton = true;
                        ColumnaOpciones.ShowEditButton = false;
                        ColumnaOpciones.ShowDeleteButton = false;

                        ddlEstados.SelectedValue = "2"; // visualisacion de estado en 'Tomados'

                        ConfiguracionTurno.LegajoMed = Convert.ToInt32(((Usuario)Session["UsuarioActivo"]).LegajoDoctor);
                    }
                    else
                    {
                        ddlEstados.SelectedValue = "1";
                    }


                    if (ddlFechas.SelectedItem != null 
                        && ddlFechas.SelectedValue != "0" 
                        && !string.IsNullOrWhiteSpace(ddlFechas.SelectedItem.Text))
                    {
                        ConfiguracionTurno.Fecha = DateTime.Parse(ddlFechas.SelectedItem.Text);
                    }

                    gvTurnos.DataSource = gestionTablas.ObtenerTablaTurnos(ConfiguracionTurno, Convert.ToInt32(ddlEstados.SelectedValue));
                    gvTurnos.DataBind();
                }
            }
            else
            {
                btnUserImg.Visible = false;
                MenuUsuario.Visible = false;
                gvTurnos.Columns[7].Visible = false;
                hlAgregarMedico.Visible = false;
                hlAsignarTurnos.Visible = false;
                hlInformes.Visible = false;
                hlListarMedicos.Visible = false;
                HlListarPacientes.Visible = false;
                hlListarTurnos.Visible = false;
                lblFecha.Visible = false;
                ddlFechas.Visible = false;
                btnMostrarTodo.Visible = false;
                btnConsultarEstado.Visible = false;
                ddlEstados.Visible = false;
            }
        }
        
        protected void gvTurnos_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            string lblNumeroTurno = ((Label)gvTurnos.Rows[e.NewSelectedIndex].FindControl("lbl_it_NumeroTurno")).Text;
            string lblDniPaciente = ((Label)gvTurnos.Rows[e.NewSelectedIndex].FindControl("lbl_it_DniPaciente")).Text;
            string lblNombrePaciente = ((Label)gvTurnos.Rows[e.NewSelectedIndex].FindControl("lbl_it_NombrePaciente")).Text;
            string lblFecha = ((Label)gvTurnos.Rows[e.NewSelectedIndex].FindControl("lbl_it_Fecha")).Text;
            string lblHora = ((Label)gvTurnos.Rows[e.NewSelectedIndex].FindControl("lbl_it_Horario")).Text;

            string[] Division = lblNumeroTurno.Split('-');
            int idEspecialidad = int.Parse(Division[2]);

            ConfiguracionTurno = new Turno();

            ConfiguracionTurno.LegajoMed = ((Usuario)Session["UsuarioActivo"]).LegajoDoctor;
            ConfiguracionTurno.DniPaciente = lblDniPaciente;
            ConfiguracionTurno.IDEspecialidad = idEspecialidad;
            ConfiguracionTurno.NombrePaciente = lblNombrePaciente;
            ConfiguracionTurno.Fecha = Convert.ToDateTime(lblFecha);
            ConfiguracionTurno.Hora = TimeSpan.Parse(lblHora);

            Session["TurnoSeleccionado"] = ConfiguracionTurno;

            Response.Redirect("SeguimientosPacientes.aspx");
        }

        protected void ddlEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfiguracionTurno.LegajoMed = ((Usuario)Session["UsuarioActivo"]).LegajoDoctor;

            gestionDdl.CargarFechas(ddlFechas, null, ConfiguracionTurno.LegajoMed, ddlEstados.SelectedValue);

            if (((Usuario)Session["UsuarioActivo"]).TipoUsuario == 1)
            {
                    gestionDdl.CargarFechas(ddlFechas, null, ((Usuario)Session["UsuarioActivo"]).LegajoDoctor);
                    ConfiguracionTurno.LegajoMed = Convert.ToInt32(((Usuario)Session["UsuarioActivo"]).LegajoDoctor);

                if (ddlEstados.SelectedValue == "2")
                {
                    gvTurnos.Columns[7].Visible = true;
                }
                else
                {
                    gvTurnos.Columns[7].Visible = false;
                }

            }

            if (((Usuario)Session["UsuarioActivo"]).TipoUsuario == 2)
            {
                ColumnaOpciones = gvTurnos.Columns[7] as CommandField;

                if (ddlEstados.SelectedValue == "2")
                {
                    ColumnaOpciones.ShowEditButton = true;
                }
                else
                {
                    ColumnaOpciones.ShowEditButton = false;
                }
            }

            gvTurnos.DataSource = gestionTablas.ObtenerTablaTurnos(ConfiguracionTurno, Convert.ToInt32(ddlEstados.SelectedValue));
            gvTurnos.DataBind();
        }

        protected void ddlFechas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFechas.SelectedItem != null && ddlFechas.SelectedValue != "0")
            {
                ConfiguracionTurno.Fecha = DateTime.ParseExact(ddlFechas.SelectedItem.Text,"dd/MM/yyyy",CultureInfo.InvariantCulture);
            }

            if (((Usuario)Session["UsuarioActivo"]).TipoUsuario == 1)
            {
                ConfiguracionTurno.LegajoMed = ((Usuario)Session["UsuarioActivo"]).LegajoDoctor;
            }

            // Guarda el filtro en Session
            Session["FiltroTurno"] = ConfiguracionTurno;

            gvTurnos.DataSource = gestionTablas.ObtenerTablaTurnos(ConfiguracionTurno, Convert.ToInt32(ddlEstados.SelectedValue));
            gvTurnos.DataBind();
        }

        protected void gvTurnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTurnos.PageIndex = e.NewPageIndex;
            Turno configuracionTurnos = Session["FiltroTurno"] as Turno ?? new Turno();

            gvTurnos.DataSource = gestionTablas.ObtenerTablaTurnos(configuracionTurnos, Convert.ToInt32(ddlEstados.SelectedValue)); // se cambio el filtro
            gvTurnos.DataBind();
        }

        protected void gvTurnos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTurnos.EditIndex = e.NewEditIndex;            
            gvTurnos.DataSource = gestionTablas.ObtenerTablaTurnos(ConfiguracionTurno, Convert.ToInt32(ddlEstados.SelectedValue));
            gvTurnos.DataBind();
        }

        protected void gvTurnos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Aqui se desglosa Numero de turno, y se pueden sacar varios datos desde ahi

            string NumeroTurno = gvTurnos.DataKeys[e.RowIndex].Value.ToString();
            string[] Division = NumeroTurno.Split('-');
            int semana = int.Parse(Division[0]);
            int idDia = int.Parse(Division[1]);
            int idEspecialidad = int.Parse(Division[2]);
            int legajo = int.Parse(Division[3]);


            GridViewRow fila = gvTurnos.Rows[e.RowIndex];
            //Logico de eliminar va aqui o el llamado a eliminar el turno, se hace a traves del mismo metodo de asignar turno,
            //pasando los datos del turno sin el dni(dni null)
            ConfiguracionTurno = new Turno(
                dnipaciente: null,
                nombrePaciente: null,
                iDEspecialidad: idEspecialidad,
                legajoMed: legajo,
                fecha: Convert.ToDateTime(((System.Web.UI.WebControls.Label)fila.FindControl("lbl_it_Fecha")).Text),
                hora: TimeSpan.Parse(((System.Web.UI.WebControls.Label)fila.FindControl("lbl_it_Horario")).Text)
            );

           

            int Retorno = GestorRegistros.RegistrarTurno(ConfiguracionTurno);

            ConfiguracionTurno = new Turno();
            Session["FiltroTurno"] = ConfiguracionTurno;

            gvTurnos.DataSource = gestionTablas.ObtenerTablaTurnos(ConfiguracionTurno, Convert.ToInt32(ddlEstados.SelectedValue));
            gvTurnos.DataBind();
        }
        
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ConfiguracionTurno.DniPaciente = txtBuscador.Text.Trim();
            if (Session["UsuarioActivo"] != null)
            {
                ConfiguracionTurno.LegajoMed = ((Usuario)Session["UsuarioActivo"]).LegajoDoctor;
            }

            if (!string.IsNullOrEmpty(txtBuscador.Text))
            {
                lblMensaje.Text = string.Empty;

                DataTable tablaFiltrada = gestionTablas.ObtenerTablaTurnos(ConfiguracionTurno, 2);//El estado se coloca en '2' por que seria un Turno Tomado.
                gvTurnos.DataSource = tablaFiltrada;
                gvTurnos.DataBind();
            }
            else
            {
                lblMensaje.Text = "Ingrese DNI a buscar.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnMostrarTodo_Click(object sender, EventArgs e)
        {
            ConfiguracionTurno.LegajoMed = ((Usuario)Session["UsuarioActivo"]).LegajoDoctor;

            gvTurnos.DataSource = gestionTablas.ObtenerTablaTurnos(ConfiguracionTurno, Convert.ToInt32(ddlEstados.SelectedValue));
            gvTurnos.DataBind();
            txtBuscador.Text = string.Empty;
            ddlFechas.SelectedIndex = 0;
        }

        protected void gvTurnos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTurnos.EditIndex = -1;//Salir del Edit
            gvTurnos.DataSource = gestionTablas.ObtenerTablaTurnos(ConfiguracionTurno, Convert.ToInt32(ddlEstados.SelectedValue));
            gvTurnos.DataBind();
        }

        protected void gvTurnos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //No estoy completamente seguro de que mas se deberia poder subir o actualizar desde aqui, asi que por el momento solo esta el DNI,
            //aunque hay un txt para el nombre del paciente es temporal ya que depende del dni asi que no tiene mucho sentido
            string NumeroTurno = gvTurnos.DataKeys[e.RowIndex].Value.ToString();
            string[] Division = NumeroTurno.Split('-');

            int idEspecialidad = int.Parse(Division[2]);
            int legajo = int.Parse(Division[3]);

            GridViewRow fila = gvTurnos.Rows[e.RowIndex];

            ConfiguracionTurno = new Turno();
            ConfiguracionTurno.IDEspecialidad = idEspecialidad;
            ConfiguracionTurno.LegajoMed = legajo;
            ConfiguracionTurno.Fecha = Convert.ToDateTime(((System.Web.UI.WebControls.Label)fila.FindControl("lbl_it_Fecha")).Text);
            ConfiguracionTurno.Hora = TimeSpan.Parse(((System.Web.UI.WebControls.Label)fila.FindControl("lbl_it_Horario")).Text);

            int Resulado = GestorRegistros.RegistrarTurno(ConfiguracionTurno);

            ConfiguracionTurno = new Turno();

            Session.Remove("FiltroTurno");

            gvTurnos.EditIndex = -1;//Salir del Edit
            gvTurnos.DataSource = gestionTablas.ObtenerTablaTurnos(ConfiguracionTurno, Convert.ToInt32(ddlEstados.SelectedValue));
            gvTurnos.DataBind();
        }

        protected void btnUserImg_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect("~/CambiarContraseña.aspx");
        }
    }
}
