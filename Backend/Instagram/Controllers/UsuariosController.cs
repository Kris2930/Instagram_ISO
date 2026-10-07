using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Instagram.Models;

namespace Instagram.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly InstagramDbContext _context;

        public UsuariosController(InstagramDbContext context)
        {
            _context = context;
        }

        // POST api/Usuarios/registro
        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return Ok(usuario);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] loginDto datos)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.CorUsu == datos.CorUsu || u.AliasUsu == datos.CorUsu);

            if (usuario == null)
            {
                return Unauthorized(new { mensaje = "Usuario no encontrado." });
            }

            if (usuario.ConUsu != datos.ConUsu)
            {
                return Unauthorized(new { mensaje = "Contraseña incorrecta." });
            }

            return Ok(new
            {
                idUsu = usuario.IdUsu,
                nomUsu = usuario.NomUsu,
                aliasUsu = usuario.AliasUsu
            });
        }

    }


}