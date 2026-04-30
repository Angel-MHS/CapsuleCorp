using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGIPE_Frontend.Models
{
    public class ProductoUpdateDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public int CategoriaId { get; set; }

        public int Stock { get; set; }

        public decimal PrecioVenta { get; set; }
    }
}
