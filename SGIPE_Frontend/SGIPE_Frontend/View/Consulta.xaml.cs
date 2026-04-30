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

namespace SGIPE_Frontend.Views
{
    /// <summary>
    /// Lógica de interacción para Consulta.xaml
    /// </summary>
    public partial class Consulta : Window
    {
        public Consulta()
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

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            new MenuPrincipal().Show();
            this.Close();
        }
    }
}
