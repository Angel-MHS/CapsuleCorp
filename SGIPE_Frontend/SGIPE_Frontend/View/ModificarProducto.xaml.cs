using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SGIPE_Frontend.Models;
using SGIPE_Frontend.Services;
using SGIPE_Frontend.Views;

namespace SGIPE_Frontend.View
{
    public partial class ModificarProducto : Window
    {
        private readonly ApiService _apiService;

        private List<CategoriaResponseDTO> _categorias = new();
        private ProductoGridItem? _productoSeleccionado;

        public ModificarProducto()
        {
            InitializeComponent();
            _apiService = new ApiService();

            Loaded += ModificarProducto_Loaded;
        }

        private async void ModificarProducto_Loaded(object sender, RoutedEventArgs e)
        {
            await CargarCategorias();
            await CargarProductos();
        }

        private async Task CargarCategorias()
        {
            try
            {
                _categorias = await _apiService.ObtenerCategorias();

                cmbCategoria.ItemsSource = _categorias;
                cmbCategoria.DisplayMemberPath = "Nombre";
                cmbCategoria.SelectedValuePath = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"No se pudieron cargar las categorías.\n\nDetalle: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }

        private async Task CargarProductos()
        {
            try
            {
                List<ProductoResponseDTO> productos = await _apiService.ObtenerProductos();

                var productosGrid = productos.Select(p => new ProductoGridItem
                {
                    id = p.Id,
                    nombre = p.Nombre,
                    descripcion = p.Descripcion ?? string.Empty,
                    categoriaId = p.CategoriaId,
                    categoriaNombre = !string.IsNullOrWhiteSpace(p.CategoriaNombre)
                        ? p.CategoriaNombre
                        : ObtenerNombreCategoria(p.CategoriaId),
                    stock = p.Stock,
                    precioVenta = p.PrecioVenta
                }).ToList();

                dataGridProductos.ItemsSource = productosGrid;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"No se pudieron cargar los productos.\n\nDetalle: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }

        private string ObtenerNombreCategoria(int categoriaId)
        {
            CategoriaResponseDTO? categoria = _categorias.FirstOrDefault(c => c.Id == categoriaId);
            return categoria?.Nombre ?? $"Categoría ID {categoriaId}";
        }

        private void dataGridProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridProductos.SelectedItem is not ProductoGridItem producto)
                return;

            _productoSeleccionado = producto;

            txtId.Text = producto.id.ToString();
            txtNombre.Text = producto.nombre;
            txtDescripcion.Text = producto.descripcion;
            txtStock.Text = producto.stock.ToString();
            txtPrecio.Text = producto.precioVenta.ToString(CultureInfo.CurrentCulture);

            cmbCategoria.SelectedValue = producto.categoriaId;
        }

        private async void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (_productoSeleccionado == null)
            {
                MessageBox.Show(
                    "Seleccione un producto para modificar.",
                    "Producto requerido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return;
            }

            string nombre = txtNombre.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();
            string stockTexto = txtStock.Text.Trim();
            string precioTexto = txtPrecio.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Ingrese el nombre del producto.");
                txtNombre.Focus();
                return;
            }

            if (cmbCategoria.SelectedValue == null)
            {
                MessageBox.Show("Seleccione una categoría.");
                cmbCategoria.Focus();
                return;
            }

            if (!int.TryParse(stockTexto, out int stock) || stock < 0)
            {
                MessageBox.Show("Ingrese un stock válido.");
                txtStock.Focus();
                return;
            }

            if (!decimal.TryParse(precioTexto, NumberStyles.Number, CultureInfo.CurrentCulture, out decimal precioVenta) || precioVenta < 0)
            {
                MessageBox.Show("Ingrese un precio de venta válido.");
                txtPrecio.Focus();
                return;
            }

            int categoriaId = Convert.ToInt32(cmbCategoria.SelectedValue);

            MessageBoxResult confirmacion = MessageBox.Show(
                $"¿Desea guardar los cambios del producto \"{_productoSeleccionado.nombre}\"?",
                "Confirmar modificación",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (confirmacion != MessageBoxResult.Yes)
                return;

            try
            {
                ProductoUpdateDTO productoActualizado = new ProductoUpdateDTO
                {
                    Id = _productoSeleccionado.id,
                    Nombre = nombre,
                    Descripcion = descripcion,
                    CategoriaId = categoriaId,
                    Stock = stock,
                    PrecioVenta = precioVenta
                };

                bool actualizado = await _apiService.ActualizarProducto(productoActualizado);

                if (!actualizado)
                {
                    MessageBox.Show(
                        $"No se pudo modificar el producto.\n\nDetalle: {_apiService.UltimoError}",
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                    return;
                }

                MessageBox.Show(
                    "Producto modificado correctamente.",
                    "Éxito",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );

                LimpiarFormulario();

                await CargarCategorias();
                await CargarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al modificar el producto.\n\nDetalle: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }

        private void LimpiarFormulario()
        {
            _productoSeleccionado = null;
            dataGridProductos.SelectedItem = null;

            txtId.Clear();
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtStock.Clear();
            txtPrecio.Clear();
            cmbCategoria.SelectedIndex = -1;
        }

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            MenuPrincipal menu = new MenuPrincipal();
            menu.Show();
            Close();
        }

        private class ProductoGridItem
        {
            public int id { get; set; }
            public string nombre { get; set; } = string.Empty;
            public string descripcion { get; set; } = string.Empty;
            public int categoriaId { get; set; }
            public string categoriaNombre { get; set; } = string.Empty;
            public int stock { get; set; }
            public decimal precioVenta { get; set; }
        }
    }
}