using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class DaoUsuario
    {
        AccesoDatos accesoDatos = new AccesoDatos();
        public DataTable SolicitudLogin(string user, string pass) {

            DataTable DatosDeUsuario = new DataTable();

            SqlParameter[] parametros = new SqlParameter[] {
                new SqlParameter("@User", user),
                new SqlParameter("@Pass", pass)
            };

            DatosDeUsuario = accesoDatos.EjecutarConsultaSelectDataAdapter("SP_LoginUsuario", parametros);

            return DatosDeUsuario;
        }
        public bool CambiarContrasenia(int idUsuario, string nuevaClave)
        {
            SqlCommand command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "SP_CambiarContraseñaUsuario"
            };
            command.Parameters.AddWithValue("@IdUsuario", idUsuario);
            command.Parameters.AddWithValue("@NuevaContraseña", nuevaClave);
            return accesoDatos.EjecutarProcedimientoAlmacenado(command, "SP_CambiarContraseñaUsuario") > 0;
        }
        public bool CrearCuenta(string Nombre, string Apellido, string Dni)
        {
            SqlCommand command = new SqlCommand();

            command.Parameters.AddWithValue("@Nombre", Nombre);
            command.Parameters.AddWithValue("@Apellido", Apellido);
            command.Parameters.AddWithValue("@DniUsuario", Dni);

            return accesoDatos.EjecutarProcedimientoAlmacenado(command, "SP_RegistrarCuenta") > 0;
        }
        public string ObtenerNombreUsuario(string DniUsuario)
        {
            SqlParameter[] parametros = new SqlParameter[] {
                new SqlParameter("@DniUsuario", DniUsuario)
            };

            DataTable nombreCuenta = accesoDatos.EjecutarConsultaSelectDataAdapter("SP_ObtenerNombreUsuario" , parametros);
            if (nombreCuenta.Rows.Count > 0)
            {
                return nombreCuenta.Rows[0]["NombreUsuario"].ToString();
            }
            return string.Empty;
        }
    }
} 

