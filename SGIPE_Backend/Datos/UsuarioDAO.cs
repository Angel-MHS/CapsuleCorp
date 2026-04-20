using MySql.Data.MySqlClient;
using System;

namespace SGIPE_Backend.Datos
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }  // ← cambió de "Password"
        public int RolId { get; set; }            // ← NUEVO (antes era string Rol)
        public string RolNombre { get; set; }     // ← NUEVO (para mostrar el nombre del rol)
        public bool Activo { get; set; }          // ← NUEVO
        public DateTime FechaCreacion { get; set; } // ← cambió de "CreadoEn"
    }
    
    public class UsuarioDAO
    {
        /// <summary>
        /// Validar credenciales (usando password_hash)
        /// </summary>
        public Usuario ValidarUsuario(string nombre, string password)
        {
            Usuario usuario = null;
            
            using (var conn = ConexionBD.GetConnection())
            {
                conn.Open();
                string query = @"SELECT u.id, u.nombre, u.email, u.password_hash, u.rol_id, u.activo, u.fecha_creacion,
                                       r.nombre as rol_nombre
                                FROM usuarios u
                                JOIN roles r ON u.rol_id = r.id
                                WHERE u.nombre = @nombre AND u.password_hash = @password AND u.activo = 1";
                
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@password", password); // En producción, aquí va el hash
                    
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new Usuario
                            {
                                Id = reader.GetInt32("id"),
                                Nombre = reader.GetString("nombre"),
                                Email = reader.GetString("email"),
                                PasswordHash = reader.GetString("password_hash"),
                                RolId = reader.GetInt32("rol_id"),
                                RolNombre = reader.GetString("rol_nombre"),
                                Activo = reader.GetBoolean("activo"),
                                FechaCreacion = reader.GetDateTime("fecha_creacion")
                            };
                        }
                    }
                }
            }
            
            return usuario;
        }
        
        /// <summary>
        /// Obtener usuario por ID (con su rol)
        /// </summary>
        public Usuario ObtenerUsuarioPorId(int id)
        {
            Usuario usuario = null;
            
            using (var conn = ConexionBD.GetConnection())
            {
                conn.Open();
                string query = @"SELECT u.id, u.nombre, u.email, u.password_hash, u.rol_id, u.activo, u.fecha_creacion,
                                       r.nombre as rol_nombre
                                FROM usuarios u
                                JOIN roles r ON u.rol_id = r.id
                                WHERE u.id = @id";
                
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
                                PasswordHash = reader.GetString("password_hash"),
                                RolId = reader.GetInt32("rol_id"),
                                RolNombre = reader.GetString("rol_nombre"),
                                Activo = reader.GetBoolean("activo"),
                                FechaCreacion = reader.GetDateTime("fecha_creacion")
                            };
                        }
                    }
                }
            }
            
            return usuario;
        }
        
        /// <summary>
        /// Obtener TODOS los usuarios activos
        /// </summary>
        public List<Usuario> ObtenerTodos()
        {
            List<Usuario> usuarios = new List<Usuario>();
            
            using (var conn = ConexionBD.GetConnection())
            {
                conn.Open();
                string query = @"SELECT u.id, u.nombre, u.email, u.password_hash, u.rol_id, u.activo, u.fecha_creacion,
                                       r.nombre as rol_nombre
                                FROM usuarios u
                                JOIN roles r ON u.rol_id = r.id
                                WHERE u.activo = 1";
                
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuarios.Add(new Usuario
                        {
                            Id = reader.GetInt32("id"),
                            Nombre = reader.GetString("nombre"),
                            Email = reader.GetString("email"),
                            PasswordHash = reader.GetString("password_hash"),
                            RolId = reader.GetInt32("rol_id"),
                            RolNombre = reader.GetString("rol_nombre"),
                            Activo = reader.GetBoolean("activo"),
                            FechaCreacion = reader.GetDateTime("fecha_creacion")
                        });
                    }
                }
            }
            
            return usuarios;
        }
    }
}
