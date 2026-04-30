using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SGIPE_Frontend.Models;
using SGIPE_Frontend.Services;
using SGIPE_Frontend.Views;

namespace SGIPE_Frontend.View
{
    public partial class ModificarProducto : Window
    {
        private readonly ApiService _apiService;
        private ProductoGridItem? _productoSeleccionado;

        public ModificarProducto()
        {
            InitializeComponent();
            _apiService = new ApiService();

            Loaded += ModificarProducto_Loaded;
        }

        private async void ModificarProducto_Loaded(object sender, RoutedEventArgs e)
        {
            await CargarProductos();
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

        private void dataGridProductos_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dataGridProductos.SelectedItem is not ProductoGridItem producto)
                return;

            _productoSeleccionado = producto;

            txtNombre.Text = producto.nombre;
            txtStock.Text = producto.stock.ToString();
            txtPrecio.Text = producto.precioVenta.ToString(CultureInfo.CurrentCulture);
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
            string stockTexto = txtStock.Text.Trim();
            string precioTexto = txtPrecio.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Ingrese el nombre del producto.");
                txtNombre.Focus();
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
                    Stock = stock,
                    PrecioVenta = precioVenta
                };

                bool actualizado = await _apiService.ActualizarProducto(productoActualizado);

                if (!actualizado)
                {
                    MessageBox.Show(
                        "No se pudo modificar el producto.",
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

            txtNombre.Clear();
            txtStock.Clear();
            txtPrecio.Clear();
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
            public int stock { get; set; }
            public decimal precioVenta { get; set; }
        }
    }
}