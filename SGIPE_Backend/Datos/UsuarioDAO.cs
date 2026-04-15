using System;
using System.Collections.Generic;

namespace SGIPE_Backend.Datos
{
    // Clase que representa un Usuario
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Contrasena { get; set; }
        public string Rol { get; set; }
        
        // Constructor vacío
        public Usuario() { }
        
        // Constructor con parámetros
        public Usuario(int id, string nombre, string contrasena, string rol)
        {
            Id = id;
            Nombre = nombre;
            Contrasena = contrasena;
            Rol = rol;
        }
    }
    
    // Clase DAO (Data Access Object) para Usuario
    public class UsuarioDAO
    {
        // Lista simulada de usuarios (mientras no hay BD real)
        private List<Usuario> usuariosSimulados;
        
        // Constructor: carga usuarios de prueba
        public UsuarioDAO()
        {
            usuariosSimulados = new List<Usuario>();
            
            // Agregar 2 usuarios como pide el proyecto
            usuariosSimulados.Add(new Usuario(1, "admin", "admin123", "Administrador"));
            usuariosSimulados.Add(new Usuario(2, "empleado", "emp456", "Empleado"));
        }
        
        /// <summary>
        /// Valida si un usuario existe con las credenciales dadas
        /// </summary>
        /// <param name="nombre">Nombre de usuario</param>
        /// <param name="contrasena">Contraseña</param>
        /// <returns>El usuario si es válido, null si no</returns>
        public Usuario ValidarUsuario(string nombre, string contrasena)
        {
            foreach (Usuario u in usuariosSimulados)
            {
                if (u.Nombre == nombre && u.Contrasena == contrasena)
                {
                    return u;
                }
            }
            return null; // Credenciales incorrectas
        }
        
        /// <summary>
        /// Obtiene un usuario por su ID
        /// </summary>
        /// <param name="id">ID del usuario</param>
        /// <returns>Usuario encontrado o null</returns>
        public Usuario ObtenerUsuarioPorId(int id)
        {
            foreach (Usuario u in usuariosSimulados)
            {
                if (u.Id == id)
                {
                    return u;
                }
            }
            return null;
        }
        
        /// <summary>
        /// Obtiene todos los usuarios
        /// </summary>
        public List<Usuario> ObtenerTodosLosUsuarios()
        {
            return usuariosSimulados;
        }
    }
}