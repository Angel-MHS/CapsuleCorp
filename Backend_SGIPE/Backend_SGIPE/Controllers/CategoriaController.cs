using Backend_SGIPE.DTos;
using Backend_SGIPE.Models;
using Backend_SGIPE.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend_SGIPE.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly CategoriaService _service;

    public CategoriaController(CategoriaService service)
    {
        _service = service;
    }

    // ========================
    // MAPEOS
    // ========================

    private Categoria MapToEntity(CategoriaCreateDTO dto)
    {
        return new Categoria
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            Activo = true,
            FechaCreacion = DateTime.Now
        };
    }

    private Categoria MapToEntity(CategoriaUpdateDTO dto)
    {
        return new Categoria
        {
            Id = dto.Id,
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            Activo = dto.Activo
        };
    }

    private CategoriaResponseDTO MapToDTO(Categoria c)
    {
        return new CategoriaResponseDTO
        {
            Id = c.Id,
            Nombre = c.Nombre,
            Descripcion = c.Descripcion,
            Activo = c.Activo,
            FechaCreacion = c.FechaCreacion
        };
    }

    // ========================
    // ENDPOINTS
    // ========================

    // GET: api/categoria
    [HttpGet]
    public async Task<IActionResult> Obtener()
    {
        var categorias = await _service.ObtenerTodas();

        var response = categorias
            .Where(c => c.Activo) // 🔥 mantienes soft delete
            .Select(c => MapToDTO(c))
            .ToList();

        return Ok(response);
    }

    // GET: api/categoria/5
    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var categoria = await _service.ObtenerPorId(id);

        if (categoria == null || !categoria.Activo)
            return NotFound();

        var response = MapToDTO(categoria);

        return Ok(response);
    }

    // POST: api/categoria
    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CategoriaCreateDTO dto)
    {
        var categoria = MapToEntity(dto);

        await _service.Crear(categoria);

        return Ok();
    }

    // PUT: api/categoria
    [HttpPut]
    public async Task<IActionResult> Actualizar([FromBody] CategoriaUpdateDTO dto)
    {
        var categoria = MapToEntity(dto);

        var actualizado = await _service.Actualizar(categoria);

        if (!actualizado)
            return NotFound(new { mensaje = "Categoría no existe" });

        return Ok();
    }

    // DELETE: api/categoria/5 (Soft Delete)
    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var eliminado = await _service.Eliminar(id);

        if (!eliminado)
            return NotFound(new { mensaje = "Categoría no existe" });

        return Ok();
    }
}