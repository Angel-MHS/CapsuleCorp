using System;
using Backend_SGIPE.Models;
using Backend_SGIPE.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend_SGIPE.Repositories;
public interface IProductoRepository
{
    Task<List<Producto>> ObtenerTodos();

    Task<Producto?> ObtenerPorId(int id);

    Task Agregar(Producto producto);

    Task Actualizar(Producto producto);

    Task Eliminar(int id);
}