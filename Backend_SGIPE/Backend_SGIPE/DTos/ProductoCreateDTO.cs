namespace Backend_SGIPE.DTos
{
    public class ProductoCreateDTO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int CategoriaId { get; set; }
        public int Stock { get; set; }
        public decimal PrecioVenta { get; set; }
    }
}
