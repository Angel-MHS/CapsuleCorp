using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGIPE_Frontend.Models
{
    public class MovimientoStockDTO
    {
        public int Id { get; set; }

        public int ProductoId { get; set; }

        public string? ProductoNombre { get; set; }

        public int UsuarioId { get; set; }

        public string? UsuarioNombre { get; set; }

        public string Tipo { get; set; } = string.Empty;

        public int Cantidad { get; set; }

        public string? Motivo { get; set; }

        public DateTime Fecha { get; set; }
    }
}
