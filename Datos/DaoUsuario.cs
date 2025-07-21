using System.Data;
using System.Data.SqlClient;
using Entidades;

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
    }
} 

