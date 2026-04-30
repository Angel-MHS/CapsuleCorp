namespace Backend_SGIPE.DTos
{
    public class MovimientoInventarioResponseDTO
    {
        public int Id { get; set; }

        public int ProductoId { get; set; }
        public string ProductoNombre { get; set; } = string.Empty;

        public int UsuarioId { get; set; }
        public string UsuarioNombre { get; set; } = string.Empty;

        public int Tipo { get; set; }

        public int Cantidad { get; set; }

        public string? Motivo { get; set; }

        public DateTime Fecha { get; set; }
    }
}
