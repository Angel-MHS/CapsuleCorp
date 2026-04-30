using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_SGIPE.Models;
[Table("movimientos_inventario")]
public class MovimientoInventario
{
    public int Id { get; set; }

    [Column("producto_id")]
    public int ProductoId { get; set; }
    public Producto Producto { get; set; }

    public int Cantidad { get; set; }

    public enum TipoMovimiento { 
        ENTRADA = 1, 
        SALIDA = 2 }

    public TipoMovimiento Tipo { get; set; }
    // "Entrada" o "Salida"

    public string? Motivo { get; set; }

    [Column("fecha_creacion")]
    public DateTime Fecha { get; set; } = DateTime.Now;

    // Quién hizo el movimiento

    [Column("usuario_id")]
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
}