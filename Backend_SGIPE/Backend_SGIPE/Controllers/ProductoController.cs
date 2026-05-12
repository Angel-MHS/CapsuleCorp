using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Backend_SGIPE.Services;
using Backend_SGIPE.Models;
using Backend_SGIPE.DTos;

namespace Backend_SGIPE.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductoController : ControllerBase
{
    private readonly ProductoService _productoService;

    private Producto MapToEntity(ProductoCreateDTO dto)
    {
        return new Producto
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            CategoriaId = dto.CategoriaId,
            Stock = dto.Stock,
            Precio = dto.PrecioVenta,
            Activo = true,
            FechaCreacion = DateTime.Now
        };
    }

    private Producto MapToEntity(ProductoUpdateDTO dto)
    {
        return new Producto
        {
            Id = dto.Id,
            Nombre = dto.Nombre,
            CategoriaId = dto.CategoriaId,
            Stock = dto.Stock,
            Precio = dto.PrecioVenta,
            Descripcion = dto.Descripcion
        };
    }
    private ProductoResponseDTO MapToDTO(Producto p)
    {
        return new ProductoResponseDTO
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Descripcion = p.Descripcion,
            CategoriaId = p.CategoriaId,
            CategoriaNombre = p.Categoria != null ? p.Categoria.Nombre : string.Empty,
            Stock = p.Stock,
            PrecioVenta = p.Precio
        };
    }

    private List<ProductoResponseDTO> MapToDTOList(List<Producto> productos)
    {
        return productos.Select(p => MapToDTO(p)).ToList();
    }

    public ProductoController(ProductoService productoService)
    {
        _productoService = productoService;
    }

    // GET: api/producto
    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var productos = await _productoService.ObtenerProductos();

        return Ok(productos);
    }

    // GET: api/producto/5
    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var producto = await _productoService.ObtenerPorId(id);

        if (producto == null)
            return NotFound();
        var response = MapToDTO(producto);

        return Ok(response);
    }

    // POST: api/producto
    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] ProductoCreateDTO dto)
    {
        if (dto.PrecioVenta <= 0)
            return BadRequest("El precio debe ser mayor a 0");
        var producto = MapToEntity(dto);

        await _productoService.CrearProducto(producto);

        return CreatedAtAction(nameof(ObtenerPorId), new {id = producto.Id}, null);
    }

    // PUT: api/producto
    [HttpPut]
    public async Task<IActionResult> Actualizar([FromBody] ProductoUpdateDTO dto)
    {   
        var existe = await _productoService.ObtenerPorId(dto.Id);
        if (existe == null)
            return NotFound(new { mensaje = "Producto no existe" });

        var producto = MapToEntity(dto);
        await _productoService.ActualizarProducto(producto);
        return Ok();
    }

    // DELETE: api/producto/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var existente = await _productoService.ObtenerPorId(id);
        if (existente == null)
            return NotFound(new { mensaje = "Producto no existe" });
        await _productoService.EliminarProducto(id);
        return Ok();
    }

    // POST: api/producto/stock
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