using Datos;
using Entidades;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http.Headers;

namespace Servicios
{
    public class GestionRegistros
    {
        DaoPacientes dpaciente = new DaoPacientes();
        DaoMedicos dmedico = new DaoMedicos();
        DaoTurnos dTurnos = new DaoTurnos();
        AccesoDatos AccesoDatos = new AccesoDatos();

        public bool RegistrarPaciente(Paciente paciente)
        {
            int pacientesRegistrados = dpaciente.registroPaciente(paciente);
            return pacientesRegistrados > 0;
        }

        public bool RegistarMedico(Medico medico)
        {
            int medicosRegistrados = dmedico.registroMedico(medico);
            return medicosRegistrados > 0;
        }

        public string ObtenerProxLegajo()
        {
            return dmedico.ObtenerProxLegajo();
        }

        public bool VerificarSiExiste(string DNI)
        {
            return dpaciente.verificarSiExistePaciente(DNI);
        }

        public int RegistrarTurno(Turno turno)
        {
            int turnosRegistrados = dTurnos.registrarTurno(turno);
            return turnosRegistrados;
        }

        public DataTable InformeDeAsistencia(DateTime Desde, DateTime Hasta, string tipo)
        {
            DataTable InformeFunciona = dTurnos.InformeAsistencia(Desde, Hasta, tipo);
            return InformeFunciona;
        }

        public bool RegistrarSeguimiento(Turno TurnoACerrar, string observacion)
        {
            int seguimientosIngresados = dpaciente.RegistrarSeguimiento(TurnoACerrar, observacion);

            if (seguimientosIngresados > 0)
            {
                dTurnos.registrarTurno(TurnoACerrar, true);
            }
            return seguimientosIngresados > 0;
        }

        public DataTable ListarHistorialDelPaciente(string DniPaciente)
        {
            return AccesoDatos.EjecutarConsultaSelectDataAdapter(DniPaciente);
        }

        public bool ActualizarPaciente(string dni, string nombre, string apellido, string telefono, string correo) {



            return dpaciente.ActualizarPacienteEdit(dni, nombre, apellido, telefono, correo);
        }

        public bool BajaPaciente(string dni) {


            return dpaciente.BajaPaciente(dni);
        }
    } 
}
