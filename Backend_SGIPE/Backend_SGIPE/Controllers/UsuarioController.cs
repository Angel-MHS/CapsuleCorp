using Backend_SGIPE.DTos;
using Backend_SGIPE.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend_SGIPE.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly UsuarioService _usuarioService;

    public UsuarioController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    // GET: api/usuario
    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var usuarios = await _usuarioService.ObtenerUsuarios();

        var response = usuarios.Select(u => new UsuarioLoginResponseDTO
        {
            Id = u.Id,
            Nombre = u.Nombre,
            Email = u.Correo,
            RolId = u.RolId,
            RolNombre = u.Rol != null ? u.Rol.Nombre : null
        });

        return Ok(response);
    }

    // GET: api/usuario/5
    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var usuario = await _usuarioService.ObtenerUsuarioPorId(id);

        if (usuario == null)
            return NotFound(new { mensaje = "Usuario no existe" });

        var response = new UsuarioLoginResponseDTO
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre,
            Email = usuario.Correo,
            RolId = usuario.RolId,
            RolNombre = usuario.Rol != null ? usuario.Rol.Nombre : null
        };

        return Ok(response);
    }

    // POST: api/usuario/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        var usuario = await _usuarioService.Login(dto.Username, dto.Password);

        if (usuario == null)
            return Unauthorized(new { mensaje = "Credenciales incorrectas" });

        var response = new UsuarioLoginResponseDTO
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre,
            Email = usuario.Correo,
            RolId = usuario.RolId,
            RolNombre = usuario.Rol != null ? usuario.Rol.Nombre : null
        };

        return Ok(response);
    }
}