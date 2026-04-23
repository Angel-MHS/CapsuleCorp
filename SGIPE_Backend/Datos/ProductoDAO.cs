using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace SGIPE_Backend.Datos
{
    // Clase Producto (coordinada con Víctor y Edwin)
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }  // ← NUEVO
        public int CategoriaId { get; set; }
        public string CategoriaNombre { get; set; }
        public int Stock { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }  // ← NUEVO
        
        public decimal ValorTotal => Stock * PrecioVenta;
    }
    
    // Clase DAO para Producto (TÚ - Jonathan)
    public class ProductoDAO
    {
        /// <summary>
        /// Obtener TODOS los productos activos
        /// </summary>
        public List<Producto> ObtenerTodos()
        {
            List<Producto> productos = new List<Producto>();
            
            using (var conn = ConexionBD.GetConnection())
            {
                conn.Open();
                string query = @"SELECT p.*, c.nombre as categoria_nombre 
                                FROM productos p
                                JOIN categorias c ON p.categoria_id = c.id
                                WHERE p.activo = 1";
                
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productos.Add(MapProducto(reader));
                    }
                }
            }
            
            return productos;
        }
        
        /// <summary>
        /// Obtener un producto por su ID
        /// </summary>
        public Producto ObtenerPorId(int id)
        {
            using (var conn = ConexionBD.GetConnection())
            {
                conn.Open();
                string query = @"SELECT p.*, c.nombre as categoria_nombre 
                                FROM productos p
                                JOIN categorias c ON p.categoria_id = c.id
                                WHERE p.id = @id";
                
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapProducto(reader);
                        }
                    }
                }
            }
            
            return null;
        }
        
        /// <summary>
        /// Insertar un NUEVO producto
        /// </summary>
        public bool Insertar(Producto producto)
        {
            using (var conn = ConexionBD.GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO productos (nombre, descripcion, categoria_id, stock, precio_costo, precio_venta, activo)
                                VALUES (@nombre, @descripcion, @categoria_id, @stock, @precio_costo, @precio_venta, 1)";
                
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion ?? "");
                    cmd.Parameters.AddWithValue("@categoria_id", producto.CategoriaId);
                    cmd.Parameters.AddWithValue("@stock", producto.Stock);
                    cmd.Parameters.AddWithValue("@precio_costo", producto.PrecioCosto);
                    cmd.Parameters.AddWithValue("@precio_venta", producto.PrecioVenta);
                    
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        
        /// <summary>
        /// ACTUALIZAR un producto existente
        /// </summary>
        public bool Actualizar(Producto producto)
        {
            using (var conn = ConexionBD.GetConnection())
            {
                conn.Open();
                string query = @"UPDATE productos 
                                SET nombre = @nombre, 
                                    categoria_id = @categoria_id, 
                                    stock = @stock, 
                                    precio_costo = @precio_costo, 
                                    precio_venta = @precio_venta
                                WHERE id = @id";
                
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", producto.Id);
                    cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@categoria_id", producto.CategoriaId);
                    cmd.Parameters.AddWithValue("@stock", producto.Stock);
                    cmd.Parameters.AddWithValue("@precio_costo", producto.PrecioCosto);
                    cmd.Parameters.AddWithValue("@precio_venta", producto.PrecioVenta);
                    
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        
        /// <summary>
        /// ELIMINAR (baja lógica) - solo desactiva, no borra físicamente
        /// </summary>
        public bool EliminarLogico(int id)
        {
            using (var conn = ConexionBD.GetConnection())
            {
                conn.Open();
                string query = "UPDATE productos SET activo = 0 WHERE id = @id";
                
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        
        /// <summary>
        /// Filtrar productos por CATEGORÍA
        /// </summary>
        public List<Producto> FiltrarPorCategoria(int categoriaId)
        {
            List<Producto> productos = new List<Producto>();
            
            using (var conn = ConexionBD.GetConnection())
            {
                conn.Open();
                string query = @"SELECT p.*, c.nombre as categoria_nombre 
                                FROM productos p
                                JOIN categorias c ON p.categoria_id = c.id
                                WHERE p.activo = 1 AND p.categoria_id = @categoriaId";
                
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@categoriaId", categoriaId);
                    
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productos.Add(MapProducto(reader));
                        }
                    }
                }
            }
            
            return productos;
        }
        
        /// <summary>
        /// Filtrar productos por RANGO DE PRECIO
        /// </summary>
        public List<Producto> FiltrarPorPrecio(decimal precioMin, decimal precioMax)
        {
            List<Producto> productos = new List<Producto>();
            
            using (var conn = ConexionBD.GetConnection())
            {
                conn.Open();
                string query = @"SELECT p.*, c.nombre as categoria_nombre 
                                FROM productos p
                                JOIN categorias c ON p.categoria_id = c.id
                                WHERE p.activo = 1 AND p.precio_venta BETWEEN @min AND @max";
                
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@min", precioMin);
                    cmd.Parameters.AddWithValue("@max", precioMax);
                    
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productos.Add(MapProducto(reader));
                        }
                    }
                }
            }
            
            return productos;
        }
        
        /// <summary>
        /// Calcular el VALOR TOTAL del inventario (suma de stock * precio_venta)
        /// </summary>
        public decimal CalcularValorTotalInventario()
        {
            using (var conn = ConexionBD.GetConnection())
            {
                conn.Open();
                string query = "SELECT SUM(stock * precio_venta) as total FROM productos WHERE activo = 1";
                
                using (var cmd = new MySqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();
                    return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                }
            }
        }
        
        /// <summary>
        /// Mapear datos del lector SQL a objeto Producto
        /// </summary>
        private Producto MapProducto(MySqlDataReader reader)
        {
            return new Producto
            {
                Id = reader.GetInt32("id"),
                Nombre = reader.GetString("nombre"),
                Descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion")) ? "" : reader.GetString("descripcion"),
                CategoriaId = reader.GetInt32("categoria_id"),
                CategoriaNombre = reader.GetString("categoria_nombre"),
                Stock = reader.GetInt32("stock"),
                PrecioCosto = reader.GetDecimal("precio_costo"),
                PrecioVenta = reader.GetDecimal("precio_venta"),
                Activo = reader.GetBoolean("activo"),
                FechaCreacion = reader.GetDateTime("fecha_creacion")
            };
        }
    }
}
