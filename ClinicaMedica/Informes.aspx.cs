using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClinicaMedica
{
    public partial class Informes : System.Web.UI.Page
    {
        private GestionRegistros GestorRegistros = new GestionRegistros();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                btnUserImg.Visible = true;

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

        protected void btnGenerarInforme_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtFechaDesde.Text) && !string.IsNullOrWhiteSpace(txtFechaHasta.Text))
            {
                //Le paso los dateTime al respectivo metodo
                DateTime Desde = Convert.ToDateTime(txtFechaDesde.Text);
                DateTime Hasta = Convert.ToDateTime(txtFechaHasta.Text);

                //Para probar de momento hay turnos desde el 24/06 hasta el 30/06

                //Son los estados que agregue al sp
                string Presentes = "p", Ausentes = "a", Totales = "t";

                DataTable present = GestorRegistros.InformeDeAsistencia(Desde, Hasta, Presentes);
                DataTable ausent = GestorRegistros.InformeDeAsistencia(Desde, Hasta, Ausentes);
                DataTable total = GestorRegistros.InformeDeAsistencia(Desde, Hasta, Totales);

                //Contamos las filas para sacar porcentajes
                int totalTurns = total.Rows.Count;//Da Bien
                int totalPresent = present.Rows.Count;
                int totalAusent = ausent.Rows.Count;


                //lblMensaje.Text = totalPresent.ToString();

                if (totalTurns > 0)
                {

                    float porcentajePresentes = (float)(totalPresent*100)/totalTurns;
                    float porcentajeAusentes = (float)(totalAusent * 100) /totalTurns;


                    lblPresentes.Text = porcentajePresentes.ToString("0.00")+"%";
                    lblAusentes.Text = porcentajeAusentes.ToString("0.00")+"%";
                }

                lblDesde.Text = txtFechaDesde.Text;
                lblDesde2.Text = txtFechaDesde.Text;
                lblHasta.Text = txtFechaHasta.Text;
                lblHasta2.Text = txtFechaHasta.Text;

            }
        }

        protected void btnUserImg_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/CambiarContraseña.aspx");
        }
    }
}