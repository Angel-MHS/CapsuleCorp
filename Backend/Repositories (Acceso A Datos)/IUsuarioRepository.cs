public interface IUsuarioRepository
{
    Task<Usuario?> ObtenerPorId(int id);
    Task<List<Usuario>> ObtenerTodos();
}