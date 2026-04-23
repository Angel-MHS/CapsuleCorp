using System;

public class ProductoRepository : IProductoRepository
{
    private readonly AppDbContext _context;

    public ProductoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Producto>> ObtenerTodos()
    {
        return await _context.Productos.ToListAsync();
    }

    public async Task<Producto?> ObtenerPorId(int id)
    {
        return await _context.Productos.FindAsync(id);
    }

    public async Task Agregar(Producto producto)
    {
        await _context.Productos.AddAsync(producto);
        await _context.SaveChangesAsync();
    }

    public async Task Actualizar(Producto producto)
    {
        _context.Productos.Update(producto);
        await _context.SaveChangesAsync();
    }

    public async Task Eliminar(int id)
    {
        var producto = await _context.Productos.FindAsync(id);

        if (producto != null)
        {
            producto.Activo = false;
            await _context.SaveChangesAsync();
        }
    }
}