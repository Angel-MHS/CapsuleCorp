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
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtPassword.Password))
            {
                MessageBox.Show("Llena todos los campos");
                return;
            }


            if (txtUsuario.Text == "admin" && txtPassword.Password == "1234")
            {
                new MenuPrincipal().Show();
                this.Close();
            }
            else if (txtUsuario.Text == "user" && txtPassword.Password == "1234")
            {
                new MenuUsuario().Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Datos incorrectos");
            }
        }
    }
}