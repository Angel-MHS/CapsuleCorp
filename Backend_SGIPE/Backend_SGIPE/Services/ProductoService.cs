using System;
using Backend_SGIPE.Data;
using Backend_SGIPE.Models;
using Backend_SGIPE.Repositories;
using static Backend_SGIPE.Models.MovimientoInventario;

namespace Backend_SGIPE.Services;

public class ProductoService
{
    private readonly IProductoRepository _productoRepo;
    private readonly IMovimientoInventarioRepository _movimientoRepo;
    private readonly ICategoriaRepository _categoriaRepo;
    private readonly AppDbContext _context;

    public ProductoService(
     IProductoRepository productoRepo,
     IMovimientoInventarioRepository movimientoRepo,
     ICategoriaRepository categoriaRepo,
     AppDbContext context)
    {
        _productoRepo = productoRepo;
        _movimientoRepo = movimientoRepo;
        _categoriaRepo = categoriaRepo; 
        _context = context;
    }

    public async Task<List<Producto>> ObtenerProductos()
    {
        return await _productoRepo.ObtenerTodos();
    }

    public async Task<Producto?> ObtenerPorId(int id)
    {
        return await _productoRepo.ObtenerPorId(id);
    }

    public async Task CrearProducto(Producto producto)
    {
        if (string.IsNullOrWhiteSpace(producto.Nombre))
            throw new Exception("El nombre es obligatorio");

        if (producto.Precio <= 0)
            throw new Exception("El precio debe ser mayor a 0");

        if (producto.Stock < 0)
            throw new Exception("El stock no puede ser negativo");

        var categoria = await _categoriaRepo.ObtenerPorId(producto.CategoriaId);

        if (categoria == null || !categoria.Activo)
            throw new Exception("La categoría no existe o está inactiva");

        producto.Activo = true;
        producto.FechaCreacion = DateTime.Now;

        await _productoRepo.Agregar(producto);
    }

    public async Task ActualizarProducto(Producto producto)
    {
        var existente = await _productoRepo.ObtenerPorId(producto.Id);

        if (existente == null)
            throw new Exception("Producto inexistente");

        var categoria = await _categoriaRepo.ObtenerPorId(producto.CategoriaId);

        if (categoria == null || !categoria.Activo)
            throw new Exception("La categoría no existe o está inactiva");

        await _productoRepo.Actualizar(producto);
    }

    public async Task EliminarProducto(int id)
    {
        await _productoRepo.Eliminar(id);
    }
    public async Task AjustarStock(
        int productoId,
        int cantidad,
        string tipo,
        int usuarioId,
        string motivo)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            if (cantidad <= 0)
                throw new Exception("La cantidad debe ser mayor a 0");

            var producto = await _productoRepo.ObtenerPorId(productoId);

            if (producto == null)
                throw new Exception("Producto no encontrado");

            // Validación usando string (como antes)
            if (tipo == "Salida" && producto.Stock < cantidad)
                throw new Exception("Stock insuficiente");

            // Ajuste de stock (como antes)
            if (tipo == "Entrada")
                producto.Stock += cantidad;
            else if (tipo == "Salida")
                producto.Stock -= cantidad;

            await _productoRepo.Actualizar(producto);

            // Crear movimiento     
            var movimiento = new MovimientoInventario
            {
                ProductoId = productoId,
                Cantidad = cantidad,
                Tipo = Enum.Parse<TipoMovimiento>(tipo, true), 
                UsuarioId = usuarioId,
                Motivo = motivo,
                Fecha = DateTime.Now
            };

            await _movimientoRepo.Agregar(movimiento);

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}