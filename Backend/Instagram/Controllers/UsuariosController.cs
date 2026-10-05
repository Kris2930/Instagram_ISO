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
        public async Task<ActionResult<Usuario>> Registrar(Usuario nuevoUsuario)
        {
            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            return Ok(nuevoUsuario);
        }
    }
}