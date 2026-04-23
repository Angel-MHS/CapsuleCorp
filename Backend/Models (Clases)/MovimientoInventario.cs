using System;

public class MovimientoInventario
{
    public int Id { get; set; }

    public int ProductoId { get; set; }
    public Producto Producto { get; set; }

    public int Cantidad { get; set; }

    public enum TipoMovimiento {Entrada, Salida}

    public TipoMovimiento Tipo { get; set; }
    // "Entrada" o "Salida"

    public string? Motivo { get; set; }

    public DateTime Fecha { get; set; } = DateTime.Now;

    // Quién hizo el movimiento
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
}
