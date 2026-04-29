namespace Backend_SGIPE.DTos
{
    public class ProductoResponseDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string CategoriaNombre { get; set; }
        public int Stock { get; set; }
        public decimal PrecioVenta { get; set; }
    }
}
