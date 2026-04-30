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
    /// Lógica de interacción para AltaProducto.xaml
    /// </summary>
    public partial class AltaProducto : Window
    {
        public AltaProducto()
        {
            InitializeComponent();
        }
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            new MenuPrincipal().Show();
            this.Close();
        }
    }
}
