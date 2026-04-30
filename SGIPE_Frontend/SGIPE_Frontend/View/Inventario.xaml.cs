using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SGIPE_Frontend.Models;
using SGIPE_Frontend.Services;

namespace SGIPE_Frontend.Views
{
    public partial class Inventario : Window
    {
        private readonly ApiService _apiService;

        public Inventario()
        {
            InitializeComponent();
            _apiService = new ApiService();

            Loaded += Inventario_Loaded;
        }

        private async void Inventario_Loaded(object sender, RoutedEventArgs e)
        {
            await CargarMovimientos();
        }

        private async Task CargarMovimientos()
        {
            try
            {
                List<MovimientoStockDTO> movimientos = await _apiService.ObtenerMovimientos();

                var movimientosGrid = movimientos.Select(m => new MovimientoGridItem
                {
                    producto = !string.IsNullOrWhiteSpace(m.ProductoNombre)
                ? m.ProductoNombre
                : $"Producto ID {m.ProductoId}",
                    tipo = ConvertirTipoMovimiento(m.Tipo),
                    cantidad = m.Cantidad,
                    motivo = m.Motivo ?? string.Empty
                }).ToList();

                dataGridMovimientos.ItemsSource = movimientosGrid;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"No se pudieron cargar los movimientos de inventario.\n\nDetalle: {ex.Message}",
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

        private class MovimientoGridItem
        {
            public string producto { get; set; } = string.Empty;
            public string tipo { get; set; } = string.Empty;
            public int cantidad { get; set; }
            public string motivo { get; set; } = string.Empty;
        }

        private string ConvertirTipoMovimiento(int tipo)
        {
            return tipo switch
            {
                1 => "Entrada",
                2 => "Salida",
                _ => $"Tipo {tipo}"
            };
        }
    }
}