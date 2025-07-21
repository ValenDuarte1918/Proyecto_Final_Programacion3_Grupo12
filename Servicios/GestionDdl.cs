using Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Servicios
{
    public class GestionDdl
    {
        AccesoDatos acceso = new AccesoDatos();
        public void CargarProvincias(DropDownList ddlProvincias)
        {
            AccesoDatos acceso = new AccesoDatos();
            DataTable tablaProvincias = acceso.EjecutarConsultaSelectDataAdapter("SP_ObtenerProvincias");

            if (tablaProvincias != null)
            {
                ddlProvincias.DataSource = tablaProvincias;
                ddlProvincias.DataTextField = "NombreProvincia";
                ddlProvincias.DataValueField = "IdProvincia";
                ddlProvincias.DataBind();
                ddlProvincias.Items.Insert(0, new ListItem("Seleccione Provincia", "0"));
            }
        }
        public void CargarLocalidades(DropDownList ddlLocalidades, int idProvincia)
        {
            acceso = new AccesoDatos();
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@IdProvincia", idProvincia)
            };
            DataTable tablaLocalidades = acceso.EjecutarConsultaSelectDataAdapter("SP_ObtenerLocalidadesPorProvincia", parametros);

            if (tablaLocalidades != null)
            {
                ddlLocalidades.DataSource = tablaLocalidades;
                ddlLocalidades.DataTextField = "NombreLocalidad";
                ddlLocalidades.DataValueField = "IdLocalidad";
                ddlLocalidades.DataBind();
                ddlLocalidades.Items.Insert(0, new ListItem("Seleccione Localidad", "0"));
            }
        }
        public void CargarEspecialidades(DropDownList ddlEspecialidades)
        {
            acceso = new AccesoDatos();
            DataTable tablaEspecialidades = acceso.EjecutarConsultaSelectDataAdapter("SP_ObtenerEspecialidad");

            if (tablaEspecialidades != null)
            {
                ddlEspecialidades.DataSource = tablaEspecialidades;
                ddlEspecialidades.DataTextField = "Nombre_Esp";
                ddlEspecialidades.DataValueField = "Id_Esp";
                ddlEspecialidades.DataBind();
                ddlEspecialidades.Items.Insert(0, new ListItem("Seleccione Especialidad", "0"));
            }
        }
        public void CargarMedicos(DropDownList ddlMedicos, int idEspecialidad, int? idDia = null)
        {
            DataTable tablaMedicos = new DataTable();
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@IdEspecialidad", idEspecialidad),
                new SqlParameter("@IdDia", idDia)
            };
            tablaMedicos = acceso.EjecutarConsultaSelectDataAdapter("SP_RetornarListaMedicos", parametros);

            if (tablaMedicos != null)
            {
                ddlMedicos.DataSource = tablaMedicos;
                ddlMedicos.DataTextField = "Doctor";
                ddlMedicos.DataValueField = "Legajo";
                ddlMedicos.DataBind();
                ddlMedicos.Items.Insert(0, new ListItem("Seleccione Medico", "0"));
            }
        }
        public void CargarEstados(DropDownList ddlEstados)
        {
            DataTable tablaEstados = new DataTable();
            tablaEstados.Columns.Add("IdEstado", typeof(int));
            tablaEstados.Columns.Add("NombreEstado", typeof(string));
            tablaEstados.Rows.Add(1, "Disponibles");
            tablaEstados.Rows.Add(2, "Tomados");
            tablaEstados.Rows.Add(0, "Deshabilitados");
            if (tablaEstados != null)
            {
                ddlEstados.DataSource = tablaEstados;
                ddlEstados.DataTextField = "NombreEstado";
                ddlEstados.DataValueField = "IdEstado";
                ddlEstados.DataBind();
            }
        }
        public void CargarFechas(DropDownList ddlFechas, int? idEspecialidad = null, int? LegajoMedico = null, string Estado = "1")
        {
            int? estadoInterpretado;

            switch (Estado)
            {
                case "1": estadoInterpretado = 1; break;
                case "2": estadoInterpretado = null; break;
                case "0": estadoInterpretado = 0; break;
                default : estadoInterpretado = 1; break;
            }

            acceso = new AccesoDatos();
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@IdEspecialidad", idEspecialidad),
                new SqlParameter("@Legajo", LegajoMedico),
                new SqlParameter("@Estado", estadoInterpretado)
            };
            DataTable tablaFechas = acceso.EjecutarConsultaSelectDataAdapter("SP_RetornarFechasTurnos", parametros);
            if (tablaFechas != null && tablaFechas.Rows.Count > 0)
            {
                // Agregamos una columna solo para mostrar el texto formateado
                if (!tablaFechas.Columns.Contains("FechaTexto"))
                    tablaFechas.Columns.Add("FechaTexto", typeof(string));

                foreach (DataRow row in tablaFechas.Rows)
                {
                    if (row["Fecha"] != DBNull.Value)
                    {
                        DateTime fecha = (DateTime)row["Fecha"];
                        row["FechaTexto"] = fecha.ToString("dd/MM/yyyy");
                    }
                }

                //HttpContext.Current.Session["IDSemana"] = tablaFechas;

                ddlFechas.DataSource = tablaFechas;
                ddlFechas.DataTextField = "FechaTexto";
                ddlFechas.DataValueField = "IdDia";
                ddlFechas.DataBind();

                ddlFechas.Items.Insert(0, new ListItem("Seleccione Fecha", "0"));
            }
        }
        public void CargarHoras(DropDownList ddlHoras, int idEspecialidad, int? idDia = null, int? LegajoMedico = null)
        {
            acceso = new AccesoDatos();
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@IdEspecialidad", idEspecialidad),
                new SqlParameter("@Legajo", LegajoMedico),
                new SqlParameter("@IdDia", idDia)
            };
            DataTable tablaHoras = acceso.EjecutarConsultaSelectDataAdapter("SP_RetornarHorasTurnos", parametros);
            if (tablaHoras != null && tablaHoras.Rows.Count > 0)
            {
                ddlHoras.DataSource = tablaHoras;
                ddlHoras.DataTextField = "Hora";
                ddlHoras.DataValueField = "Hora";
                ddlHoras.DataBind();
                ddlHoras.Items.Insert(0, new ListItem("Seleccione Hora"));
            }
        }
    }
}
