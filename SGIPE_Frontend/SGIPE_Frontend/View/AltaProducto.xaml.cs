using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using SGIPE_Frontend.Models;
using SGIPE_Frontend.Services;

namespace SGIPE_Frontend.Views
{
    public partial class AltaProducto : Window
    {
        private readonly ApiService _apiService;

        public AltaProducto()
        {
            InitializeComponent();
            _apiService = new ApiService();

            Loaded += AltaProducto_Loaded;
        }

        private async void AltaProducto_Loaded(object sender, RoutedEventArgs e)
        {
            await CargarCategorias();
        }

        private async Task CargarCategorias()
        {
            try
            {
                var categorias = await _apiService.ObtenerCategorias();

                cmbCategoria.ItemsSource = categorias;

                // Ajusta estos nombres si tu CategoriaResponseDTO usa otros.
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

        private async void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
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

            if (!int.TryParse(stockTexto, out int stockInicial) || stockInicial < 0)
            {
                MessageBox.Show("Ingrese un stock inicial válido.");
                txtStock.Focus();
                return;
            }

            if (!decimal.TryParse(precioTexto, NumberStyles.Number, CultureInfo.CurrentCulture, out decimal precio) || precio < 0)
            {
                MessageBox.Show("Ingrese un precio de venta válido.");
                txtPrecio.Focus();
                return;
            }

            try
            {
                btnGuardar.IsEnabled = false;

                int categoriaId = Convert.ToInt32(cmbCategoria.SelectedValue);

                var producto = new ProductoCreateDTO
                {
                    Nombre = nombre,
                    Descripcion = descripcion,
                    Stock = stockInicial,
                    PrecioVenta = precio,
                    CategoriaId = categoriaId
                };

                bool guardado = await _apiService.CrearProducto(producto);

                if (!guardado)
                {
                    MessageBox.Show(
                        "No se pudo guardar el producto.",
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                    return;
                }

                MessageBox.Show(
                    "Producto guardado correctamente.",
                    "Éxito",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );

                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al guardar el producto.\n\nDetalle: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
            finally
            {
                btnGuardar.IsEnabled = true;
            }
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtStock.Clear();
            txtPrecio.Clear();
            cmbCategoria.SelectedIndex = -1;
            txtNombre.Focus();
        }

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            MenuPrincipal menu = new MenuPrincipal();
            menu.Show();
            Close();
        }
    }
}