using System;
using Backend_SGIPE.Models;
using Backend_SGIPE.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend_SGIPE.Repositories;
public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> ObtenerPorId(int id)
    {
        return await _context.Usuarios
            .Include(u => u.Rol)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<List<Usuario>> ObtenerTodos()
    {
        return await _context.Usuarios
            .ToListAsync();
    }

    public async Task<Usuario?> ObtenerPorUsername(string username)
    {
        return await _context.Usuarios
            .Include(u => u.Rol)
            .FirstOrDefaultAsync(u => u.Nombre == username);
    }
}