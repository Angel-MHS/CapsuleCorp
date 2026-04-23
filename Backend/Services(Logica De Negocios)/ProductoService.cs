using System;

public class ProductoService
{
    private readonly IProductoRepository _productoRepo;
    private readonly IMovimientoInventarioRepository _movimientoRepo;

    public ProductoService(
        IProductoRepository productoRepo,
        IMovimientoInventarioRepository movimientoRepo)
    {
        _productoRepo = productoRepo;
        _movimientoRepo = movimientoRepo;
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

        producto.Activo = true;
        producto.FechaCreacion = DateTime.Now;

        await _productoRepo.Agregar(producto);
    }

    public async Task ActualizarProducto(Producto producto)
    {
        await _productoRepo.Actualizar(producto);
    }

    public async Task EliminarProducto(int id)
    {
        await _productoRepo.EliminarLogico(id);
    }

    public async Task AjustarStock(
        int productoId,
        int cantidad,
        string tipo,
        int usuarioId,
        string? motivo = null)
    {
        if (cantidad <= 0)
            throw new Exception("La cantidad debe ser mayor a 0");

        // 1. Obtener producto
        var producto = await _productoRepo.ObtenerPorId(productoId);

        if (producto == null)
            throw new Exception("Producto no encontrado");

        // 2. Validar salida
        if (tipo == "Salida" && producto.Stock < cantidad)
            throw new Exception("Stock insuficiente");

        // 3. Actualizar stock
        if (tipo == "Entrada")
            producto.Stock += cantidad;
        else if (tipo == "Salida")
            producto.Stock -= cantidad;

        // 4. Crear movimiento
        var movimiento = new MovimientoInventario
        {
            ProductoId = productoId,
            Cantidad = cantidad,
            Tipo = tipo,
            UsuarioId = usuarioId,
            Motivo = motivo
            Fecha = DateTime.Now
        };

        // 5. Guardar todo (luego repository)
        await _productoRepo.Actualizar(producto);
        await _movimientoRepo.Agregar(movimiento);
    }
}

