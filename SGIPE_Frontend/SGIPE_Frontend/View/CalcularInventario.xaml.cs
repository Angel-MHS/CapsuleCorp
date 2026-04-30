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
    /// Lógica de interacción para CalcularInventario.xaml
    /// </summary>
    public partial class CalcularInventario : Window
    {
        public CalcularInventario()
        {
            InitializeComponent();
        }
        private void BtnCalcular_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int stock = int.Parse(txtStock.Text);
                int cantidad = int.Parse(txtCantidad.Text);

                string tipo = (cmbTipo.SelectedItem as ComboBoxItem).Content.ToString();

                int resultado = 0;

                txtResultado.Text = "Nuevo stock: " + resultado;
            }
            catch
            {
                MessageBox.Show("Ingresa valores válidos");
            }
        }

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            new MenuPrincipal().Show();
            this.Close();
        }
    }
}
