using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_SGIPE.Models;
public class Rol
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    public bool Activo { get; set; } = true;

    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    //public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
