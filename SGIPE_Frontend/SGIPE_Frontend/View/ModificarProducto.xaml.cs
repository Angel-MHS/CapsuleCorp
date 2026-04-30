using SGIPE_Frontend.Models;
using SGIPE_Frontend.Services;
using SGIPE_Frontend.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SGIPE_Frontend.View
{
    /// <summary>
    /// Lógica de interacción para ModificarProducto.xaml
    /// </summary>
    public partial class ModificarProducto : Window
    {
        private ProductoResponseDTO productoSeleccionado;

        public ModificarProducto()
        {
            InitializeComponent();
            CargarProductos();

        }

        private async void CargarProductos()
        {
            ProductoService servicio = new ProductoService();
            var lista = await servicio.ObtenerProductos();
            dataGridProductos.ItemsSource = lista;
        }

        private void dataGridProductos_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dataGridProductos.SelectedItem is ProductoResponseDTO producto)
            {
                productoSeleccionado = producto;

                txtNombre.Text = producto.nombre;
                txtStock.Text = producto.stock.ToString();
                txtPrecio.Text = producto.precioVenta.ToString();
            }
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (productoSeleccionado == null)
            {
                MessageBox.Show("Selecciona un producto");
                return;
            }

            productoSeleccionado.nombre = txtNombre.Text;
            productoSeleccionado.stock = int.Parse(txtStock.Text);
            productoSeleccionado.precioVenta = decimal.Parse(txtPrecio.Text);

            MessageBox.Show("Producto actualizado (simulado)");
        }

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            new MenuPrincipal().Show();
            this.Close();
        }
    }
}
