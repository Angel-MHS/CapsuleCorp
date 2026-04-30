using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SGIPE_Frontend.Models;
using SGIPE_Frontend.Services;

namespace SGIPE_Frontend.Views
{
    public partial class BajaProducto : Window
    {
        private readonly ApiService _apiService;

        public BajaProducto()
        {
            InitializeComponent();
            _apiService = new ApiService();

            Loaded += BajaProducto_Loaded;
        }

        private async void BajaProducto_Loaded(object sender, RoutedEventArgs e)
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
                    stock = p.Stock
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

        private async void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridProductos.SelectedItem == null)
            {
                MessageBox.Show(
                    "Seleccione un producto para eliminar.",
                    "Producto requerido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return;
            }

            ProductoGridItem productoSeleccionado = (ProductoGridItem)dataGridProductos.SelectedItem;

            MessageBoxResult confirmacion = MessageBox.Show(
                $"¿Está seguro de que desea eliminar el producto \"{productoSeleccionado.nombre}\"?",
                "Confirmar eliminación",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (confirmacion != MessageBoxResult.Yes)
                return;

            try
            {
                bool eliminado = await _apiService.EliminarProducto(productoSeleccionado.id);

                if (!eliminado)
                {
                    MessageBox.Show(
                        "No se pudo eliminar el producto.",
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                    return;
                }

                MessageBox.Show(
                    "Producto eliminado correctamente.",
                    "Éxito",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );

                await CargarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al eliminar el producto.\n\nDetalle: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
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
        }
    }
}