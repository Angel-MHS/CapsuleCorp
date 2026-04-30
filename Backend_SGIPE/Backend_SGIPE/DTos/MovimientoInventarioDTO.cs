namespace Backend_SGIPE.DTos
{
    public class MovimientoInventarioDTO
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public string Tipo { get; set; } // "entrada" o "salida"
        public int UsuarioId { get; set; }
        public string? Motivo { get; set; }
    }
}
