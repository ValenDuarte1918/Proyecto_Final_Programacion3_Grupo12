using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Entidades
{
    public enum TipoUsuario
    {
        ADMIN = 1,
        Doctor = 2
    }
    public class Usuario
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public int TipoUsuario { get; set; }
        public int? LegajoDoctor { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }

        public Usuario(string user, string pass, int IdUsuario, int tipoUsuario, string nombreUsuario, string apellidoUsuario, int? legajoDoctor = null)
        {
            User = user;
            Password = pass;
            Id = IdUsuario;
            TipoUsuario = tipoUsuario;
            NombreUsuario = nombreUsuario;
            ApellidoUsuario = apellidoUsuario;
            LegajoDoctor = legajoDoctor;
        }
    }
}
