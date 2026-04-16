using System;

public interface IProductoRepository
{
    Task<List<Producto>> ObtenerTodos();

    Task<Producto?> ObtenerPorId(int id);

    Task Agregar(Producto producto);

    Task Actualizar(Producto producto);

    Task Eliminar(int id);
}