using SGIPE_Frontend.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SGIPE_Frontend.Services
{
    class ProductoService
    {
        private HttpClient httpClient;

        // 🔥 CAMBIADOR DE MODO
        private bool usarMock = true;

        public ProductoService()
        {
            httpClient = new HttpClient(
                new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (msg, cert, chain, err) => true
                }
            );
        }

        public async Task<List<Producto>> ObtenerProductos()
        {
            // 🔥 MODO PRUEBA (SIN BACKEND)
            if (usarMock)
            {
                return new List<Producto>
                {
                    new Producto { id = 1, nombre = "Cuaderno", stock = 10, precioVenta = 30 },
                    new Producto { id = 2, nombre = "Lápiz", stock = 50, precioVenta = 5 },
                    new Producto { id = 3, nombre = "Borrador", stock = 20, precioVenta = 8 }
                };
            }

            // 🔗 MODO REAL (CUANDO YA HAYA BACKEND)
            var response = await httpClient.GetAsync("https://localhost:7019/api/producto");

            if (!response.IsSuccessStatusCode)
                return new List<Producto>();

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Producto>>(json);
        }
    }
}

/*
using SGIPE_Frontend.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SGIPE_Frontend.Services
{
    class ProductoService
    {
        private HttpClient httpClient;

        public ProductoService()
        {
            httpClient = new HttpClient(
                new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (msg, cert, chain, err) => true
                }
            );
        }

        public async Task<List<Producto>> ObtenerProductos()
        {
         var response = await httpClient.GetAsync("https://localhost:7019/api/producto");


            if (!response.IsSuccessStatusCode)
                return new List<Producto>();

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Producto>>(json);

        }
    }
}
*/