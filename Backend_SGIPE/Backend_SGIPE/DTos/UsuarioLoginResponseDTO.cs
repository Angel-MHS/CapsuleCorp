namespace Backend_SGIPE.DTos
{
    public class UsuarioLoginResponseDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public int RolId { get; set; }

        public string? RolNombre { get; set; }
    }
}
