using Backend_SGIPE.Models;
using Backend_SGIPE.Repositories;

namespace Backend_SGIPE.Services;

public class CategoriaService
{
    private readonly ICategoriaRepository _repo;

    public CategoriaService(ICategoriaRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Categoria>> ObtenerTodas()
    {
        return await _repo.ObtenerTodos();
    }

    public async Task<Categoria?> ObtenerPorId(int id)
    {
        return await _repo.ObtenerPorId(id);
    }

    public async Task Crear(Categoria categoria)
    {
        await _repo.Agregar(categoria);
    }

    public async Task<bool> Actualizar(Categoria categoria)
    {
        var existente = await _repo.ObtenerPorId(categoria.Id);

        if (existente == null)
            return false;

        await _repo.Actualizar(categoria);
        return true;
    }

    public async Task<bool> Eliminar(int id)
    {
        var categoria = await _repo.ObtenerPorId(id);

        if (categoria == null)
            return false;

        categoria.Activo = false;

        await _repo.Actualizar(categoria);
        return true;
    }

}
