using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Datos
{
    public class AccesoDatos
    {
        private string connectionString = "Data Source=localhost\\sqlexpress;Initial Catalog=ClinicaMedica;Integrated Security=True";
        //cambie a public
        public SqlConnection connection()
        {
            SqlConnection cnxn = null;
            try
            {
                cnxn = new SqlConnection(connectionString);
                cnxn.Open();
                return cnxn;
            }
            catch (Exception ex)
            {
                try
                {
                    string AuxConnectionString = connectionString.Replace("\\sqlexpress", "");
                    cnxn = new SqlConnection(AuxConnectionString);
                    cnxn.Open();
                    return cnxn;
                }
                catch
                {
                    return null;
                }
            }
        }
        private SqlCommand sqlCommand(string ProcedimientoAlmacenado, SqlConnection conexion)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(ProcedimientoAlmacenado, conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                return cmd;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable EjecutarConsultaSelectDataAdapter(string ProcedimientoAlmacenado , SqlParameter[] parametro = null)
        {
            try
            {
                using (SqlCommand cmd = sqlCommand(ProcedimientoAlmacenado, connection()))
                {
                    if (cmd == null) return null;
                    if (parametro != null)
                    {
                        cmd.Parameters.AddRange(parametro);
                    }
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd)) {
                        DataTable tabla = new DataTable();
                        adapter.Fill(tabla);
                        return tabla;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int EjecutarProcedimientoAlmacenado(SqlCommand cmd, String Nombre)
        {
            int FilasAfectadas;
            SqlConnection Conexion = connection();
            cmd.Connection = Conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Nombre;
            FilasAfectadas = cmd.ExecuteNonQuery();
            Conexion.Close();
            return FilasAfectadas;
        }
    }
}