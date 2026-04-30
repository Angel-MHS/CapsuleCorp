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
using SGIPE_Frontend.Models;
using SGIPE_Frontend.Services;
using SGIPE_Frontend.View;

namespace SGIPE_Frontend.Views
{
    public partial class Login : Window
    {
        private readonly ApiService _apiService;

        public Login()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsuario.Text.Trim();
            string password = txtPassword.Password.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(
                    "Ingrese usuario y contraseña.",
                    "Campos requeridos",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return;
            }

            try
            {
                btnLogin.IsEnabled = false;

                UsuarioLoginResponse? usuario = await _apiService.Login(username, password);

                if (usuario == null)
                {
                    MessageBox.Show(
                        "Usuario o contraseña incorrectos.",
                        "Login inválido",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );

                    return;
                }

                MenuPrincipal menu = new MenuPrincipal();
                menu.Show();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"No se pudo conectar con el backend.\n\nDetalle: {ex.Message}",
                    "Error de conexión",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
            finally
            {
                btnLogin.IsEnabled = true;
            }
        }
    }
}