using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using SGIPE_Frontend.Models;

namespace SGIPE_Frontend.Services;

public class ApiService
{
    private readonly HttpClient _http;

    public ApiService()
    {
        var handler = new HttpClientHandler
        {
            // Solo para desarrollo local con HTTPS de localhost.
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        _http = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://localhost:7019/")
        };
    }

    // =========================
    // LOGIN
    // =========================

    public async Task<UsuarioLoginResponse?> Login(string username, string password)
    {
        var request = new LoginRequest
        {
            Username = username,
            Password = password
        };

        var response = await _http.PostAsJsonAsync("api/usuario/login", request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<UsuarioLoginResponse>();
    }

    // =========================
    // PRODUCTOS
    // =========================

    public async Task<List<ProductoResponseDTO>> ObtenerProductos()
    {
        var result = await _http.GetFromJsonAsync<List<ProductoResponseDTO>>("api/producto");
        return result ?? new List<ProductoResponseDTO>();
    }

    public async Task<ProductoResponseDTO?> ObtenerProductoPorId(int id)
    {
        return await _http.GetFromJsonAsync<ProductoResponseDTO>($"api/producto/{id}");
    }

    public async Task<bool> CrearProducto(ProductoCreateDTO producto)
    {
        var response = await _http.PostAsJsonAsync("api/producto", producto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ActualizarProducto(ProductoUpdateDTO producto)
    {
        var response = await _http.PutAsJsonAsync("api/producto", producto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> EliminarProducto(int id)
    {
        var response = await _http.DeleteAsync($"api/producto/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> AjustarStock(MovimientoInventarioRequest movimiento)
    {
        var url =
            $"api/producto/stock?productoId={movimiento.ProductoId}" +
            $"&cantidad={movimiento.Cantidad}" +
            $"&tipo={Uri.EscapeDataString(movimiento.Tipo)}" +
            $"&usuarioId={movimiento.UsuarioId}" +
            $"&motivo={Uri.EscapeDataString(movimiento.Motivo ?? string.Empty)}";

        var response = await _http.PostAsync(url, null);
        return response.IsSuccessStatusCode;
    }

    // =========================
    // CATEGORÍAS
    // =========================

    public async Task<List<CategoriaResponseDTO>> ObtenerCategorias()
    {
        var result = await _http.GetFromJsonAsync<List<CategoriaResponseDTO>>("api/categoria");
        return result ?? new List<CategoriaResponseDTO>();
    }

    public async Task<CategoriaResponseDTO?> ObtenerCategoriaPorId(int id)
    {
        return await _http.GetFromJsonAsync<CategoriaResponseDTO>($"api/categoria/{id}");
    }

    public async Task<bool> CrearCategoria(CategoriaCreateDTO categoria)
    {
        var response = await _http.PostAsJsonAsync("api/categoria", categoria);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ActualizarCategoria(CategoriaUpdateDTO categoria)
    {
        var response = await _http.PutAsJsonAsync("api/categoria", categoria);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> EliminarCategoria(int id)
    {
        var response = await _http.DeleteAsync($"api/categoria/{id}");
        return response.IsSuccessStatusCode;
    }

    // =========================
    // MOVIMIENTOS
    // =========================

    public async Task<List<MovimientoStockDTO>> ObtenerMovimientos()
    {
        var result = await _http.GetFromJsonAsync<List<MovimientoStockDTO>>("api/movimientoinventario");
        return result ?? new List<MovimientoStockDTO>();
    }

    public async Task<List<MovimientoStockDTO>> ObtenerMovimientosPorProducto(int productoId)
    {
        var result = await _http.GetFromJsonAsync<List<MovimientoStockDTO>>(
            $"api/movimientoinventario/producto/{productoId}");

        return result ?? new List<MovimientoStockDTO>();
    }
}