using System;
using Backend_SGIPE.Models;
using Backend_SGIPE.Data;
using Microsoft.EntityFrameworkCore;
using Backend_SGIPE.DTos;

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
        return await _context.Productos
            .Include(p => p.Categoria)
            .ToListAsync();
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
        var productoExistente = await _context.Productos.FindAsync(producto.Id);

        if (productoExistente == null)
            throw new Exception("Producto no encontrado.");

        productoExistente.Nombre = producto.Nombre;
        productoExistente.Stock = producto.Stock;
        productoExistente.Precio = producto.Precio;
        productoExistente.Descripcion = producto.Descripcion;
        productoExistente.CategoriaId = producto.CategoriaId;

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