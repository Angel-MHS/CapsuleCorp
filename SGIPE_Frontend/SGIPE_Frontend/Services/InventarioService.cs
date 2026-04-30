using SGIPE_Frontend.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGIPE_Frontend.Services
{
    public class InventarioService
    {
        public async Task<List<Movimiento>> ObtenerMovimientos()
        {
            return new List<Movimiento>
        {
            new Movimiento { producto = "Cuaderno", tipo = "Entrada", cantidad = 10, motivo = "Compra" },
            new Movimiento { producto = "Lápiz", tipo = "Salida", cantidad = 5, motivo = "Venta" },
            new Movimiento { producto = "Borrador", tipo = "Entrada", cantidad = 20, motivo = "Stock inicial" }
        };
        }
    }
}
