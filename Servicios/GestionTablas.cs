using System.Data;
using Datos;
using System.Web.UI.WebControls;
using Entidades;
using System;

namespace Servicios
{
    public class GestionTablas
    {
        private DaoTurnos daoTurnos = new DaoTurnos();
        private DaoMedicos daoMedicos = new DaoMedicos();
        private DaoPacientes daoPacientes = new DaoPacientes();
        public GestionTablas() { 

        }
        public DataTable ObtenerTablaTurnos(Turno ConfiguracionTurno, int? ValorEstado = 1) {

            bool? Estado;

            switch (ValorEstado)
            {
                case 1:
                    Estado = true; // Disponibles
                    break;
                case 2:
                    Estado = null; // Tomados
                    break;
                case 0:
                    Estado = false; // Deshabilitados
                    break;

                default:
                    Estado = true; // Por si llega un valor inesperado
                    break;
            }
            return daoTurnos.ListadoTurnos(ConfiguracionTurno, Estado);
        }

        public DataTable ObtenerTablaMedicos()
        {
            return daoMedicos.ListarMedicos();
        }
        public DataTable ObtenerTablaMedicosPorLegajo(string legajo)
        {
            return daoMedicos.ListarMedicos(legajo);
        }
        public DataTable ObtenerTablaMedicosPorNombre(string nombre)
        {
            return daoMedicos.ListarMedicos(null,nombre);
        }
        public DataTable ObtenerTablaMedicosPorIdEspecialidad(string idEspecialidad)
        {
            return daoMedicos.ListarMedicos(null, null, Convert.ToInt32(idEspecialidad));
        }
        public DataTable ObtenerTablaPacientes()
        {
            return daoPacientes.ListarPacientes();
        }

        public DataTable ObtenerTablaPacientesPorNombre(string nombre)
        {
            return daoPacientes.BuscarPorNombre(nombre);
        }

        public DataTable ObtenerTablaPacientesPorDni(string dni)
        {
            return daoPacientes.BuscarPorDni(dni);
        }


        public class GestionMedicos 
        {
            DaoMedicos dao = new DaoMedicos();

            public bool DarDeBajaMedico(int legajo)
            {
                return dao.DarDeBajaMedicoPorLegajo(legajo);
            }
            public bool ActualizarMedico(string dni, string nombre, string apellido, string telefono, string correo)
            {
                Medico medico = new Medico
                {
                    Dni = dni,
                    Nombre = nombre,
                    Apellido = apellido,
                    Telefono = telefono,
                    Correo = correo
                };
                return dao.ActualizarMedico(medico);
            }
        }

    }
}
