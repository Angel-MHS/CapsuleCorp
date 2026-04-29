using SGIPE_Frontend.Services;
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
    /// Lógica de interacción para Catalogo.xaml
    /// </summary>
    public partial class Catalogo : Window
    {
        public Catalogo()
        {
            InitializeComponent();
            CargarProductos();

        }

        private async void CargarProductos()
        {
            ProductoService servicio = new ProductoService();
            var lista = await servicio.ObtenerProductos();

            dataGridCatalogo.ItemsSource = lista;
        }

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            new MenuUsuario().Show();
            this.Close();
        }
    }
}
