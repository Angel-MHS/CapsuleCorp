using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SGIPE_Frontend.Models;
using SGIPE_Frontend.Services;

namespace SGIPE_Frontend.Views
{
    public partial class Consulta : Window
    {
        private readonly ApiService _apiService;

        public Consulta()
        {
            InitializeComponent();
            _apiService = new ApiService();

            Loaded += Consulta_Loaded;
        }

        private async void Consulta_Loaded(object sender, RoutedEventArgs e)
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