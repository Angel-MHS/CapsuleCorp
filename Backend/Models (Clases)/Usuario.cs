using System;

public class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Correo { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public bool Activo { get; set; } = true;

    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    public int RolId { get; set; }
    public Rol Rol { get; set; }
}
