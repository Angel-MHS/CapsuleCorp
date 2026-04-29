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

    public async Task Actualizar(Categoria categoria)
    {
        await _repo.Actualizar(categoria);
    }

    public async Task Eliminar(int id)
    {
        await _repo.Eliminar(id);
    }
}
