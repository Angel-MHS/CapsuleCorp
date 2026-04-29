namespace Backend_SGIPE.DTos
{
    public class ProductoUpdateDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int CategoriaId { get; set; }
        public int Stock { get; set; }
        public decimal PrecioVenta { get; set; }
    }
}
