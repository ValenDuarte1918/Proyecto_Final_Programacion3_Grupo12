using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DaoMedicos
    {
        AccesoDatos ds = new AccesoDatos();

        public DataTable ListarMedicos(string legajo = null, string nombre = null, int? idEspecialidad = null)
        {
            SqlParameter[] parametros = new SqlParameter[] {
                new SqlParameter("@Legajo", legajo),
                new SqlParameter("@BusquedaGeneral", nombre),
                new SqlParameter("@IdEspecialidad", idEspecialidad)
            };
            return ds.EjecutarConsultaSelectDataAdapter("SP_RetornarListaMedicos", parametros);
        }

        public int registroMedico(Medico medico)
        {
            SqlCommand command = new SqlCommand();

            command.Parameters.AddWithValue("@Dni", medico.Dni); // Asegúrate de que medico.Dni tenga valor
            command.Parameters.AddWithValue("@Nombre", medico.Nombre);
            command.Parameters.AddWithValue("@Apellido", medico.Apellido);
            command.Parameters.AddWithValue("@Sexo", medico.Sexo);
            command.Parameters.AddWithValue("@Nacionalidad", medico.Nacionalidad);
            if (medico.FechaNacimiento == null || medico.FechaNacimiento < new DateTime(1753, 1, 1))
                command.Parameters.AddWithValue("@FechaNacimiento", DBNull.Value);
            else
                command.Parameters.AddWithValue("@FechaNacimiento", medico.FechaNacimiento);
            command.Parameters.AddWithValue("@Direccion", medico.Direccion);
            command.Parameters.AddWithValue("@IdLocalidad", medico.IdLocalidad);
            command.Parameters.AddWithValue("@IdProvincia", medico.IdProvincia);
            command.Parameters.AddWithValue("@Correo", medico.Correo);
            command.Parameters.AddWithValue("@Telefono", medico.Telefono);
            command.Parameters.AddWithValue("@IdEspecialidad", medico.IdEspecialidad);

            return ds.EjecutarProcedimientoAlmacenado(command, "SP_RegistrarMedico");
        }
        public string ObtenerProxLegajo()
        {
            DataTable dt = ds.EjecutarConsultaSelectDataAdapter("SP_ObtenerProxLegajo", null);

            if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["ProximoLegajo"] != DBNull.Value)
            {
                return dt.Rows[0]["ProximoLegajo"].ToString();
            }
            else
            {
                return "1";
            }
        }
        public bool DarDeBajaMedicoPorLegajo(int legajo)
        {
            using (SqlConnection conn = ds.connection())
            {
                SqlCommand cmd = new SqlCommand("SP_BajaMedico", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Legajo_Me", legajo);

                SqlParameter retorno = new SqlParameter();
                retorno.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(retorno);

                // Solo abrir si está cerrada
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                cmd.ExecuteNonQuery();
                conn.Close();

                return Convert.ToInt32(retorno.Value) == 0; // true = éxito
            }
        }

        public bool ActualizarMedico(Medico medico)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@Dni", medico.Dni);
            command.Parameters.AddWithValue("@Nombre", medico.Nombre);
            command.Parameters.AddWithValue("@Apellido", medico.Apellido);
            command.Parameters.AddWithValue("@Telefono", medico.Telefono);
            command.Parameters.AddWithValue("@Correo", medico.Correo);

            int filasAfectadas = ds.EjecutarProcedimientoAlmacenado(command, "SP_ActualizarMedico");
            return filasAfectadas > 0;
        }
    }
}
