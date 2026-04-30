using System;
using System.Collections.Generic;
using System.Text;

namespace SGIPE_Frontend.Models
{
    public class Movimiento
    {
        public string producto { get; set; }
        public string tipo { get; set; }
        public int cantidad { get; set; }
        public string motivo { get; set; }
    }
}
