using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Servicios
{
    public class GestionUsuario
    {
        private DaoUsuario dao;
        public Usuario Loguear(string User, string Pass)
        {
            DataTable DatosDeUsuario = new DataTable();

            dao = new DaoUsuario();
            DatosDeUsuario = dao.SolicitudLogin(User, Pass);
            
            Usuario usuarioLogueado = null;

            if (DatosDeUsuario != null && DatosDeUsuario.Rows.Count != 0)
            {
                DataRow row = DatosDeUsuario.Rows[0];
                //Usuario(user, pass, IdUsuario, int tipoUsuario, string nombreUsuario, string apellidoUsuario, int LegajoDoctor)
                usuarioLogueado = new Usuario(
                    User, 
                    Pass, 
                    Convert.ToInt32(row["IdUsuario"]), 
                    Convert.ToInt32(row["TipoUsuario"]),
                    row["Nombre_DP"].ToString(), 
                    row["Apellido_DP"].ToString());

                if (usuarioLogueado.TipoUsuario == 1) // en caso de ser Doctor se asigna el legajo
                {
                        usuarioLogueado.LegajoDoctor = Convert.ToInt32(row["LegajoDoctor"]); // si es doctor, el legajo no es nulo
                }
                return usuarioLogueado;
            }
            else
            {
                return null;
            }
        }
        public bool CambiarContrasenia(Usuario usuario, string nuevaClave)
        {
            if (usuario != null && !string.IsNullOrEmpty(nuevaClave))
            {
                dao = new DaoUsuario();
                return dao.CambiarContrasenia(usuario.Id, nuevaClave);
            }
            return false;
        }
    }
}
