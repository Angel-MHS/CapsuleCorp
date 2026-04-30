using System;
using System.Collections.Generic;
using System.Text;

namespace SGIPE_Frontend.Models
{
     public class Producto
    {
        public int id { get; set; }
        public string nombre { get; set; } = "";
        public string descripcion { get; set; } = "";
        public int categoriaId { get; set; }
        public int stock { get; set; }
        public double precioCosto { get; set; }
        public decimal precioVenta { get; set; }

    }
}