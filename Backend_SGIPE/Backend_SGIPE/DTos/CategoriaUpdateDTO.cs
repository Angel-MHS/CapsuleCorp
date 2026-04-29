namespace Backend_SGIPE.DTos
{
    public class CategoriaUpdateDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }

    }
}
