using System;
using Microsoft.EntityFrameworkCore;

public interface IMovimientoInventarioRepository
{
    Task<List<MovimientoInventario>> ObtenerTodos();

    Task<List<MovimientoInventario>> ObtenerPorProducto(int productoId);

    Task Agregar(MovimientoInventario movimiento);
}
