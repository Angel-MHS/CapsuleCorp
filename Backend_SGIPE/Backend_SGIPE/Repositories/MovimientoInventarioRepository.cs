using Backend_SGIPE.Models;
using Backend_SGIPE.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend_SGIPE.Repositories;
public class MovimientoInventarioRepository : IMovimientoInventarioRepository
{
    private readonly AppDbContext _context;

    public MovimientoInventarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<MovimientoInventario>> ObtenerTodos()
    {
        return await _context.MovimientosInventario.ToListAsync();
    }

    public async Task<List<MovimientoInventario>> ObtenerPorProducto(int productoId)
    {
        return await _context.MovimientosInventario
            .Where(m => m.ProductoId == productoId)
            .ToListAsync();
    }

    public async Task Agregar(MovimientoInventario movimiento)
    {
        await _context.MovimientosInventario.AddAsync(movimiento);
        await _context.SaveChangesAsync();
    }
}