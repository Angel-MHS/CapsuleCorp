using System;

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
}