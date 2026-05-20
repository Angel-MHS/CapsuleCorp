using System.Windows;
using SGIPE_Frontend.View;

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
            Consulta ventana = new Consulta();
            ventana.Show();
            Close();
        }

        private void BtnAlta_Click(object sender, RoutedEventArgs e)
        {
            AltaProducto ventana = new AltaProducto();
            ventana.Show();
            Close();
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            BajaProducto ventana = new BajaProducto();
            ventana.Show();
            Close();
        }

        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            ModificarProducto ventana = new ModificarProducto();
            ventana.Show();
            Close();
        }

        private void BtnInventario_Click(object sender, RoutedEventArgs e)
        {
            Inventario ventana = new Inventario();
            ventana.Show();
            Close();
        }

        private void BtnCalcular_Click(object sender, RoutedEventArgs e)
        {
            CalcularInventario inventario = new CalcularInventario("Administrador");
            inventario.Show();
            Close();
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Login ventana = new Login();
            ventana.Show();
            Close();
        }
    }
}