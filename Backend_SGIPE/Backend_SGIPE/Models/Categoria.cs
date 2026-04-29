using System;

namespace Backend_SGIPE.Models;
using System.ComponentModel.DataAnnotations.Schema;

[Table("categorias")]
public class Categoria
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    public bool Activo { get; set; } = true;

    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    //public List<Producto> Productos { get; set; } = new List<Producto>();
}