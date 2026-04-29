using Backend_SGIPE.Models;

namespace Backend_SGIPE.Repositories
{
    public interface ICategoriaRepository
    {
        Task<List<Categoria>> ObtenerTodos();
        Task<Categoria?> ObtenerPorId(int id);
        Task Agregar(Categoria categoria);
        Task Actualizar(Categoria categoria);
        Task Eliminar(int id);
    }
}
