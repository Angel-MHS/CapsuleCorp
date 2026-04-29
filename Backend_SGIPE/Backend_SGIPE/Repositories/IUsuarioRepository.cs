using System;
using Backend_SGIPE.Models;
using Backend_SGIPE.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend_SGIPE.Repositories;
public interface IUsuarioRepository
{
    Task<Usuario?> ObtenerPorId(int id);
    Task<List<Usuario>> ObtenerTodos();
}