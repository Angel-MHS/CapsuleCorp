using System;
using Microsoft.AspNetCore.Mvc;
using Backend_SGIPE.Services;
using Backend_SGIPE.Models;
using Backend_SGIPE.Repositories;

namespace Backend_SGIPE.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovimientoInventarioController : ControllerBase
{
    private readonly IMovimientoInventarioRepository _movimientoRepo;

    public MovimientoInventarioController(IMovimientoInventarioRepository movimientoRepo)
    {
        _movimientoRepo = movimientoRepo;
    }

    // 🔹 GET: api/movimientoinventario
    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var movimientos = await _movimientoRepo.ObtenerTodos();
        return Ok(movimientos);
    }

    // 🔹 GET: api/movimientoinventario/producto/5
    [HttpGet("producto/{productoId}")]
    public async Task<IActionResult> ObtenerPorProducto(int productoId)
    {
        var movimientos = await _movimientoRepo.ObtenerPorProducto(productoId);
        return Ok(movimientos);
    }
}