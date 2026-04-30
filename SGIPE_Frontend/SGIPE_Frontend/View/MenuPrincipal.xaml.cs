using SGIPE_Frontend.View;
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
    public partial class MenuPrincipal : Window
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void BtnConsulta_Click(object sender, RoutedEventArgs e)
        {
            new Consulta().Show();
            this.Close();
        }

        private void BtnAlta_Click(object sender, RoutedEventArgs e)
        {
            new AltaProducto().Show();
            this.Close();
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            new BajaProducto().Show();
            this.Close();
        }

        private void BtnInventario_Click(object sender, RoutedEventArgs e)
        {
            new Inventario().Show();
            this.Close();
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            new Login().Show();
            this.Close();
        }
        private void BtnCalcular_Click(object sender, RoutedEventArgs e)
        {
            new CalcularInventario().Show();
        }
        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            new ModificarProducto().Show();
        }
    }
}
