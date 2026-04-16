using MySql.Data.MySqlClient;
using System;

namespace SGIPE_Backend.Datos
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
        public DateTime CreadoEn { get; set; }
    }
    
    public class UsuarioDAO
    {
        public Usuario ValidarUsuario(string nombre, string password)
        {
            Usuario usuario = null;
            
            using (var conn = ConexionBD.GetConnection())
            {
                conn.Open();
                string query = "SELECT id, nombre, email, rol, creado_en FROM usuarios WHERE nombre = @nombre AND password = @password";
                
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@password", password);
                    
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new Usuario
                            {
                                Id = reader.GetInt32("id"),
                                Nombre = reader.GetString("nombre"),
                                Email = reader.GetString("email"),
                                Rol = reader.GetString("rol"),
                                CreadoEn = reader.GetDateTime("creado_en")
                            };
                        }
                    }
                }
            }
            
            return usuario;
        }
        
        public Usuario ObtenerUsuarioPorId(int id)
        {
            Usuario usuario = null;
            
            using (var conn = ConexionBD.GetConnection())
            {
                conn.Open();
                string query = "SELECT id, nombre, email, rol, creado_en FROM usuarios WHERE id = @id";
                
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new Usuario
                            {
                                Id = reader.GetInt32("id"),
                                Nombre = reader.GetString("nombre"),
                                Email = reader.GetString("email"),
                                Rol = reader.GetString("rol"),
                                CreadoEn = reader.GetDateTime("creado_en")
                            };
                        }
                    }
                }
            }
            
            return usuario;
        }
    }
}