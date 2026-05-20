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
    /// Lógica de interacción para MenuUsuario.xaml
    /// </summary>
    public partial class MenuUsuario : Window
    {
        public MenuUsuario()
        {
            InitializeComponent();
        }

        private void BtnCatalogo_Click(object sender, RoutedEventArgs e)
        {
            Catalogo catalogo = new Catalogo("Empleado");
            catalogo.Show();
            Close();
        }

        private void BtnCalcular_Click(object sender, RoutedEventArgs e)
        {
            CalcularInventario inventario = new CalcularInventario("Empleado");
            inventario.Show();
            Close();
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            new Login().Show();
            this.Close();
        }
    }
}