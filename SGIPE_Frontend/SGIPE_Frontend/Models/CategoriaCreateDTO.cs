using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGIPE_Frontend.Models
{
    public class CategoriaCreateDTO
    {
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }
    }
}
