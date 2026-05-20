using System;
using System.Windows;
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

                if (usuario.RolNombre == "ADMIN")
                {
                    MenuPrincipal menu = new MenuPrincipal();
                    menu.Show();
                    this.Close();
                }
                else if (usuario.RolNombre == "EMPLEADO")
                {
                    MenuUsuario menuUsuario = new MenuUsuario();
                    menuUsuario.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        "El rol del usuario no es válido o no está registrado correctamente.",
                        "Rol no reconocido",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning
                    );
                }
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