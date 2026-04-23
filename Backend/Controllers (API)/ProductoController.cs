using System;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductoController : ControllerBase
{
    private readonly ProductoService _productoService;

    public ProductoController(ProductoService productoService)
    {
        _productoService = productoService;
    }

    // 🔹 GET: api/producto
    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var productos = await _productoService.ObtenerProductos();
        return Ok(productos);
    }

    // 🔹 GET: api/producto/5
    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var producto = await _productoService.ObtenerPorId(id);

        if (producto == null)
            return NotFound();

        return Ok(producto);
    }

    // 🔹 POST: api/producto
    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] Producto producto)
    {
        await _productoService.CrearProducto(producto);
        return Ok();
    }

    // 🔹 PUT: api/producto
    [HttpPut]
    public async Task<IActionResult> Actualizar([FromBody] Producto producto)
    {
        await _productoService.ActualizarProducto(producto);
        return Ok();
    }

    // 🔹 DELETE: api/producto/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _productoService.EliminarProducto(id);
        return Ok();
    }

    // 🔹 POST: api/producto/stock
    [HttpPost("stock")]
    public async Task<IActionResult> AjustarStock(
        int productoId,
        int cantidad,
        string tipo,
        int usuarioId,
        string motivo)
    {
        await _productoService.AjustarStock(productoId, cantidad, tipo, usuarioId, motivo);
        return Ok();
    }
}