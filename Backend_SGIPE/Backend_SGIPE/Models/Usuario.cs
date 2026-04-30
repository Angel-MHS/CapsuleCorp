using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_SGIPE.Models;

[Table("usuarios")]
public class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    [Column("email")]
    public string Correo { get; set; } = string.Empty;

    [Column("password_hash")]
    public string Password { get; set; } = string.Empty;

    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [Column("rol_id")]
    public int RolId { get; set; }
    public Rol Rol { get; set; }
}
