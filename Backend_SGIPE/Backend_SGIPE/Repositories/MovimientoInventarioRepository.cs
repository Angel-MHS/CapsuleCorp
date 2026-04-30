using Backend_SGIPE.Models;
using Backend_SGIPE.Data;
using Microsoft.EntityFrameworkCore;
using Backend_SGIPE.DTos;

namespace Backend_SGIPE.Repositories;
public class MovimientoInventarioRepository : IMovimientoInventarioRepository
{
    private readonly AppDbContext _context;

    public MovimientoInventarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<MovimientoInventarioResponseDTO>> ObtenerTodos()
    {
        return await _context.MovimientosInventario
        .Include(m => m.Producto)
        .Include(m => m.Usuario)
        .Select(m => new MovimientoInventarioResponseDTO
        {
            Id = m.Id,

            ProductoId = m.ProductoId,
            ProductoNombre = m.Producto != null ? m.Producto.Nombre : string.Empty,

            UsuarioId = m.UsuarioId,
            UsuarioNombre = m.Usuario != null ? m.Usuario.Nombre : string.Empty,

            Tipo = (int)m.Tipo,
            Cantidad = m.Cantidad,
            Motivo = m.Motivo,
            Fecha = m.Fecha
        })
        .ToListAsync();
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