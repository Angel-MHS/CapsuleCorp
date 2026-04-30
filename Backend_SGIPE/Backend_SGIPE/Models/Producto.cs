using System;

namespace Backend_SGIPE.Models;
using System.ComponentModel.DataAnnotations.Schema;

[Table("productos")]
public class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    [Column("precio_venta")]
    public decimal Precio { get; set; }

    [Column("stock")]
    public int Stock { get; set; }

    [Column("activo")]
    public bool Activo { get; set; } = true;

    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    
    [Column("categoria_id")]
    public int CategoriaId { get; set; }

    [NotMapped]
    public Categoria? Categoria { get; set; }

    //public List<MovimientoInventario> Movimientos { get; set; } = new List<MovimientoInventario>();
}