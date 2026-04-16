using MySql.Data.MySqlClient;
using System;

namespace SGIPE_Backend.Datos
{
    public class ConexionBD
    {
        // Datos de conexión REALES (de Edwin)
        private static string server = "localhost";
        private static string database = "papeleria_isalag";
        private static string user = "pape_isalag";
        private static string password = "isalagcpejav";
        
        private static string connectionString = $"Server={server};Database={database};Uid={user};Pwd={password};";
        
        public static MySqlConnection GetConnection()
        {
            try
            {
                return new MySqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al conectar: " + ex.Message);
            }
        }
        
        public static bool ProbarConexion()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}