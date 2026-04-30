using System;
using Backend_SGIPE.Models;
using Backend_SGIPE.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend_SGIPE.Repositories;

public interface IMovimientoInventarioRepository
{
    Task<List<MovimientoInventario>> ObtenerTodos();

    Task<List<MovimientoInventario>> ObtenerPorProducto(int productoId);

    Task Agregar(MovimientoInventario movimiento);
}