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

namespace SGIPE_Frontend.Views
{
    /// <summary>
    /// Lógica de interacción para BajaProducto.xaml
    /// </summary>
    public partial class BajaProducto : Window
    {
        public BajaProducto()
        {
            InitializeComponent();
            CargarProductos();
        }
        private async void CargarProductos()
        {
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridProductos.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un producto");
                return;
            }

        }

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            new MenuPrincipal().Show();
            this.Close();
        }
    }
}
