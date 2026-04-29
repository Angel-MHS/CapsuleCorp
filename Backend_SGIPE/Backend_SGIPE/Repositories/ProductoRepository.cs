using System;
using Backend_SGIPE.Models;
using Backend_SGIPE.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend_SGIPE.Repositories;
public class ProductoRepository : IProductoRepository
{
    private readonly AppDbContext _context;

    public ProductoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Producto>> ObtenerTodos()
    {
        return await _context.Productos.Include(p => p.Categoria).ToListAsync();
    }

    public async Task<Producto?> ObtenerPorId(int id)
    {
        return await _context.Productos.Include(p => p.Categoria).FirstOrDefaultAsync(p  => p.Id == id);
    }

    public async Task Agregar(Producto producto)
    {
        await _context.Productos.AddAsync(producto);
        await _context.SaveChangesAsync();
    }

    public async Task Actualizar(Producto producto)
    {
        // Desvincula la navegación para que EF no la toque
        if (producto.Categoria != null)
        {
            _context.Entry(producto.Categoria).State = EntityState.Unchanged;
        }

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