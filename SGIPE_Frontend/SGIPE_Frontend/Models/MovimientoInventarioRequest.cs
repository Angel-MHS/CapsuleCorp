using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGIPE_Frontend.Models
{
    public class MovimientoInventarioRequest
    {
        public int ProductoId { get; set; }

        public int Cantidad { get; set; }

        public string Tipo { get; set; } = string.Empty;

        public int UsuarioId { get; set; }

        public string? Motivo { get; set; }
    }
}
