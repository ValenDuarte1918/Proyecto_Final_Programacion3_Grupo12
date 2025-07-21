using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Medico
    {
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Sexo { get; set; }
        public string Nacionalidad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public int IdProvincia { get; set; }
        public int IdLocalidad { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public int IdEspecialidad { get; set; }
    }

}
