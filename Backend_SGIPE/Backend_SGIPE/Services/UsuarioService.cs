 using System;
using Backend_SGIPE.Models;
using Backend_SGIPE.Repositories;

namespace Backend_SGIPE.Services;
public class UsuarioService
{
    private readonly IUsuarioRepository _usuarioRepo;

    public UsuarioService(IUsuarioRepository usuarioRepo)
    {
        _usuarioRepo = usuarioRepo;
    }

    public async Task<List<Usuario>> ObtenerUsuarios()
    {
        return await _usuarioRepo.ObtenerTodos();
    }

    public async Task<Usuario?> ObtenerUsuarioPorId(int id)
    {
        return await _usuarioRepo.ObtenerPorId(id);
    }

    public async Task<Usuario?> Login(string username, string password)
    {
        var usuario = await _usuarioRepo.ObtenerPorUsername(username);

        if (usuario == null)
            return null;

        if (usuario.Password != password)
            return null;

        return usuario;
    }
}