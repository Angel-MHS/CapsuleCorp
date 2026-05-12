namespace Backend_SGIPE.DTos
{
    public class ProductoResponseDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public int CategoriaId { get; set; }

        public string CategoriaNombre { get; set; } = string.Empty;

        public int Stock { get; set; }

        public decimal PrecioVenta { get; set; }
    }
}
