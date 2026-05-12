using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SGIPE_Frontend.Models;

namespace SGIPE_Frontend.Services;

public class ApiService
{
    private readonly HttpClient _http;

    public string UltimoError { get; private set; } = string.Empty;

    public ApiService()
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        _http = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://localhost:7019/")
        };
    }

    private async Task<bool> RespuestaExitosa(HttpResponseMessage response)
    {
        UltimoError = string.Empty;

        if (response.IsSuccessStatusCode)
            return true;

        string contenido = await response.Content.ReadAsStringAsync();

        UltimoError = !string.IsNullOrWhiteSpace(contenido)
            ? contenido
            : $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}";

        return false;
    }

    // =========================
    // LOGIN
    // =========================

    public async Task<UsuarioLoginResponse?> Login(string username, string password)
    {
        UltimoError = string.Empty;

        try
        {
            var request = new LoginRequest
            {
                Username = username,
                Password = password
            };

            var response = await _http.PostAsJsonAsync("api/usuario/login", request);

            if (!response.IsSuccessStatusCode)
            {
                await RespuestaExitosa(response);
                return null;
            }

            return await response.Content.ReadFromJsonAsync<UsuarioLoginResponse>();
        }
        catch (Exception ex)
        {
            UltimoError = ex.Message;
            return null;
        }
    }

    // =========================
    // PRODUCTOS
    // =========================

    public async Task<List<ProductoResponseDTO>> ObtenerProductos()
    {
        UltimoError = string.Empty;

        try
        {
            var result = await _http.GetFromJsonAsync<List<ProductoResponseDTO>>("api/producto");
            return result ?? new List<ProductoResponseDTO>();
        }
        catch (Exception ex)
        {
            UltimoError = ex.Message;
            return new List<ProductoResponseDTO>();
        }
    }

    public async Task<ProductoResponseDTO?> ObtenerProductoPorId(int id)
    {
        UltimoError = string.Empty;

        try
        {
            return await _http.GetFromJsonAsync<ProductoResponseDTO>($"api/producto/{id}");
        }
        catch (Exception ex)
        {
            UltimoError = ex.Message;
            return null;
        }
    }

    public async Task<bool> CrearProducto(ProductoCreateDTO producto)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/producto", producto);
            return await RespuestaExitosa(response);
        }
        catch (Exception ex)
        {
            UltimoError = ex.Message;
            return false;
        }
    }

    public async Task<bool> ActualizarProducto(ProductoUpdateDTO producto)
    {
        try
        {
            var response = await _http.PutAsJsonAsync("api/producto", producto);
            return await RespuestaExitosa(response);
        }
        catch (Exception ex)
        {
            UltimoError = ex.Message;
            return false;
        }
    }

    public async Task<bool> EliminarProducto(int id)
    {
        try
        {
            var response = await _http.DeleteAsync($"api/producto/{id}");
            return await RespuestaExitosa(response);
        }
        catch (Exception ex)
        {
            UltimoError = ex.Message;
            return false;
        }
    }

    public async Task<bool> AjustarStock(MovimientoInventarioRequest movimiento)
    {
        try
        {
            var url =
                $"api/producto/stock?productoId={movimiento.ProductoId}" +
                $"&cantidad={movimiento.Cantidad}" +
                $"&tipo={Uri.EscapeDataString(movimiento.Tipo)}" +
                $"&usuarioId={movimiento.UsuarioId}" +
                $"&motivo={Uri.EscapeDataString(movimiento.Motivo ?? string.Empty)}";

            var response = await _http.PostAsync(url, null);
            return await RespuestaExitosa(response);
        }
        catch (Exception ex)
        {
            UltimoError = ex.Message;
            return false;
        }
    }

    // =========================
    // CATEGORÍAS
    // =========================

    public async Task<List<CategoriaResponseDTO>> ObtenerCategorias()
    {
        UltimoError = string.Empty;

        try
        {
            var result = await _http.GetFromJsonAsync<List<CategoriaResponseDTO>>("api/categoria");
            return result ?? new List<CategoriaResponseDTO>();
        }
        catch (Exception ex)
        {
            UltimoError = ex.Message;
            return new List<CategoriaResponseDTO>();
        }
    }

    public async Task<CategoriaResponseDTO?> ObtenerCategoriaPorId(int id)
    {
        UltimoError = string.Empty;

        try
        {
            return await _http.GetFromJsonAsync<CategoriaResponseDTO>($"api/categoria/{id}");
        }
        catch (Exception ex)
        {
            UltimoError = ex.Message;
            return null;
        }
    }

    public async Task<bool> CrearCategoria(CategoriaCreateDTO categoria)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/categoria", categoria);
            return await RespuestaExitosa(response);
        }
        catch (Exception ex)
        {
            UltimoError = ex.Message;
            return false;
        }
    }

    public async Task<bool> ActualizarCategoria(CategoriaUpdateDTO categoria)
    {
        try
        {
            var response = await _http.PutAsJsonAsync("api/categoria", categoria);
            return await RespuestaExitosa(response);
        }
        catch (Exception ex)
        {
            UltimoError = ex.Message;
            return false;
        }
    }

    public async Task<bool> EliminarCategoria(int id)
    {
        try
        {
            var response = await _http.DeleteAsync($"api/categoria/{id}");
            return await RespuestaExitosa(response);
        }
        catch (Exception ex)
        {
            UltimoError = ex.Message;
            return false;
        }
    }

    // =========================
    // MOVIMIENTOS
    // =========================

    public async Task<List<MovimientoStockDTO>> ObtenerMovimientos()
    {
        UltimoError = string.Empty;

        try
        {
            var result = await _http.GetFromJsonAsync<List<MovimientoStockDTO>>("api/movimientoinventario");
            return result ?? new List<MovimientoStockDTO>();
        }
        catch (Exception ex)
        {
            UltimoError = ex.Message;
            return new List<MovimientoStockDTO>();
        }
    }

    public async Task<List<MovimientoStockDTO>> ObtenerMovimientosPorProducto(int productoId)
    {
        UltimoError = string.Empty;

        try
        {
            var result = await _http.GetFromJsonAsync<List<MovimientoStockDTO>>(
                $"api/movimientoinventario/producto/{productoId}");

            return result ?? new List<MovimientoStockDTO>();
        }
        catch (Exception ex)
        {
            UltimoError = ex.Message;
            return new List<MovimientoStockDTO>();
        }
    }
}