using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SGIPE_Frontend.Models;
using SGIPE_Frontend.Services;
using SGIPE_Frontend.Views;

namespace SGIPE_Frontend.View
{
    public partial class CalcularInventario : Window
    {
        private readonly ApiService _apiService;
        private List<ProductoResponseDTO> _productos = new();

        public CalcularInventario()
        {
            InitializeComponent();
            _apiService = new ApiService();

            Loaded += CalcularInventario_Loaded;
        }

        private async void CalcularInventario_Loaded(object sender, RoutedEventArgs e)
        {
            await CargarProductos();
        }

        private async Task CargarProductos()
        {
            try
            {
                _productos = await _apiService.ObtenerProductos();

                cmbProducto.ItemsSource = _productos;
                cmbProducto.DisplayMemberPath = "Nombre";
                cmbProducto.SelectedValuePath = "Id";
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

        private void cmbProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbProducto.SelectedItem is not ProductoResponseDTO producto)
                return;

            txtStock.Text = producto.Stock.ToString();
            txtResultado.Text = "Producto seleccionado. Capture la cantidad y calcule el nuevo total.";
        }

        private void BtnCalcular_Click(object sender, RoutedEventArgs e)
        {
            CalcularNuevoStock(mostrarResultado: true);
        }

        private async void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            if (cmbProducto.SelectedItem is not ProductoResponseDTO producto)
            {
                MessageBox.Show(
                    "Seleccione un producto.",
                    "Dato requerido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                cmbProducto.Focus();
                return;
            }

            if (!int.TryParse(txtCantidad.Text.Trim(), out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show(
                    "Ingrese una cantidad válida mayor a cero.",
                    "Dato inválido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                txtCantidad.Focus();
                return;
            }

            if (!int.TryParse(txtUsuarioId.Text.Trim(), out int usuarioId) || usuarioId <= 0)
            {
                MessageBox.Show(
                    "Ingrese un ID de usuario válido.",
                    "Dato inválido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                txtUsuarioId.Focus();
                return;
            }

            if (cmbTipo.SelectedItem is not ComboBoxItem itemSeleccionado)
            {
                MessageBox.Show(
                    "Seleccione el tipo de movimiento.",
                    "Dato requerido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                cmbTipo.Focus();
                return;
            }

            string tipoMovimiento = itemSeleccionado.Content.ToString() ?? string.Empty;
            string motivo = txtMotivo.Text.Trim();

            if (string.IsNullOrWhiteSpace(motivo))
            {
                MessageBox.Show(
                    "Ingrese el motivo del movimiento.",
                    "Dato requerido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                txtMotivo.Focus();
                return;
            }

            int nuevoStock = CalcularNuevoStock(mostrarResultado: false);

            if (nuevoStock < 0)
                return;

            MessageBoxResult confirmacion = MessageBox.Show(
                $"¿Desea registrar este movimiento?\n\n" +
                $"Producto: {producto.Nombre}\n" +
                $"Tipo: {tipoMovimiento}\n" +
                $"Cantidad: {cantidad}\n" +
                $"Nuevo stock: {nuevoStock}",
                "Confirmar movimiento",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (confirmacion != MessageBoxResult.Yes)
                return;

            try
            {
                btnRegistrar.IsEnabled = false;

                MovimientoInventarioRequest movimiento = new MovimientoInventarioRequest
                {
                    ProductoId = producto.Id,
                    Cantidad = cantidad,
                    Tipo = tipoMovimiento,
                    UsuarioId = usuarioId,
                    Motivo = motivo
                };

                bool registrado = await _apiService.AjustarStock(movimiento);

                if (!registrado)
                {
                    MessageBox.Show(
                        "No se pudo registrar el movimiento de inventario.",
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                    return;
                }

                MessageBox.Show(
                    "Movimiento registrado correctamente.",
                    "Éxito",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );

                await CargarProductos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al registrar el movimiento.\n\nDetalle: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
            finally
            {
                btnRegistrar.IsEnabled = true;
            }
        }

        private int CalcularNuevoStock(bool mostrarResultado)
        {
            if (cmbProducto.SelectedItem is not ProductoResponseDTO producto)
            {
                MessageBox.Show(
                    "Seleccione un producto.",
                    "Dato requerido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                cmbProducto.Focus();
                return -1;
            }

            if (!int.TryParse(txtCantidad.Text.Trim(), out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show(
                    "Ingrese una cantidad válida mayor a cero.",
                    "Dato inválido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                txtCantidad.Focus();
                return -1;
            }

            if (cmbTipo.SelectedItem is not ComboBoxItem itemSeleccionado)
            {
                MessageBox.Show(
                    "Seleccione el tipo de movimiento.",
                    "Dato requerido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                cmbTipo.Focus();
                return -1;
            }

            string tipoMovimiento = itemSeleccionado.Content.ToString() ?? string.Empty;

            int stockActual = producto.Stock;
            int nuevoStock;

            if (tipoMovimiento == "Entrada")
            {
                nuevoStock = stockActual + cantidad;
            }
            else if (tipoMovimiento == "Salida")
            {
                if (cantidad > stockActual)
                {
                    MessageBox.Show(
                        "La cantidad de salida no puede ser mayor al stock actual.",
                        "Stock insuficiente",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning
                    );

                    txtCantidad.Focus();
                    return -1;
                }

                nuevoStock = stockActual - cantidad;
            }
            else
            {
                MessageBox.Show(
                    "Tipo de movimiento no válido.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );

                return -1;
            }

            if (mostrarResultado)
            {
                txtResultado.Text =
                    $"Producto: {producto.Nombre}\n" +
                    $"Stock actual: {stockActual}\n" +
                    $"Movimiento: {tipoMovimiento} de {cantidad}\n" +
                    $"Nuevo stock: {nuevoStock}";
            }

            return nuevoStock;
        }

        private void LimpiarCampos()
        {
            cmbProducto.SelectedIndex = -1;
            txtStock.Clear();
            txtCantidad.Clear();
            txtMotivo.Clear();
            txtResultado.Text = "Seleccione un producto y capture los datos del movimiento.";
        }

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            MenuPrincipal menu = new MenuPrincipal();
            menu.Show();
            Close();
        }
    }
}