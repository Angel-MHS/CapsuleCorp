using System;
using System.Collections.Generic;
using System.Linq;

namespace SGIPE_Backend.Datos
{
    public class GestionInventario
    {
        private ProductoDAO productoDAO = new ProductoDAO();
        
        /// <summary>
        /// Calcular el valor total del inventario (desde la lista de productos)
        /// </summary>
        public decimal CalcularValorTotal(List<Producto> productos)
        {
            decimal total = 0;
            foreach (var p in productos)
            {
                total += p.ValorTotal;
            }
            return total;
        }
        
        /// <summary>
        /// Obtener productos con STOCK BAJO (menos de 5 unidades)
        /// </summary>
        public List<Producto> ObtenerProductosStockBajo(int limite = 5)
        {
            var productos = productoDAO.ObtenerTodos();
            return productos.Where(p => p.Stock < limite).ToList();
        }
        
        /// <summary>
        /// Filtrar productos por nombre (búsqueda)
        /// </summary>
        public List<Producto> BuscarPorNombre(string texto)
        {
            var productos = productoDAO.ObtenerTodos();
            return productos.Where(p => p.Nombre.ToLower().Contains(texto.ToLower())).ToList();
        }
        
        /// <summary>
        /// Obtener resumen del inventario
        /// </summary>
        public InventarioResumen ObtenerResumen()
        {
            var productos = productoDAO.ObtenerTodos();
            
            return new InventarioResumen
            {
                TotalProductos = productos.Count,
                ValorTotalInventario = CalcularValorTotal(productos),
                ProductosStockBajo = ObtenerProductosStockBajo().Count,
                Categorias = productos.Select(p => p.CategoriaNombre).Distinct().Count()
            };
        }
    }
    
    // Clase para el resumen del inventario
    public class InventarioResumen
    {
        public int TotalProductos { get; set; }
        public decimal ValorTotalInventario { get; set; }
        public int ProductosStockBajo { get; set; }
        public int Categorias { get; set; }
    }
}