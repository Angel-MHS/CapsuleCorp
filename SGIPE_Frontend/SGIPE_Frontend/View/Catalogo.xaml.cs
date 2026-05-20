using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SGIPE_Frontend.Models;
using SGIPE_Frontend.Services;
using SGIPE_Frontend.Views;

namespace SGIPE_Frontend.View
{
    public partial class Catalogo : Window
    {
        private readonly ApiService _apiService;
        private readonly string _rolUsuario;

        public Catalogo(string rolUsuario)
        {
            InitializeComponent();

            _apiService = new ApiService();
            _rolUsuario = rolUsuario;

            Loaded += Catalogo_Loaded;
        }

        private async void Catalogo_Loaded(object sender, RoutedEventArgs e)
        {
            await CargarCatalogo();
        }

        private async Task CargarCatalogo()
        {
            try
            {
                List<ProductoResponseDTO> productos = await _apiService.ObtenerProductos();

                var catalogoGrid = productos.Select(p => new CatalogoGridItem
                {
                    id = p.Id,
                    nombre = p.Nombre,
                    precioVenta = p.PrecioVenta
                }).ToList();

                dataGridCatalogo.ItemsSource = catalogoGrid;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"No se pudo cargar el catálogo de productos.\n\nDetalle: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            RegresarAlMenu();
        }

        private void RegresarAlMenu()
        {
            if (_rolUsuario.Equals("Administrador", StringComparison.OrdinalIgnoreCase))
            {
                MenuPrincipal menu = new MenuPrincipal();
                menu.Show();
            }
            else if (_rolUsuario.Equals("Empleado", StringComparison.OrdinalIgnoreCase))
            {
                MenuUsuario menu = new MenuUsuario();
                menu.Show();
            }
            else
            {
                MessageBox.Show(
                    "No se pudo determinar a qué menú regresar.",
                    "Error de navegación",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return;
            }

            Close();
        }

        private class CatalogoGridItem
        {
            public int id { get; set; }
            public string nombre { get; set; } = string.Empty;
            public decimal precioVenta { get; set; }
        }
    }
}