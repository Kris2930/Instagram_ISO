using Instagram.Models;
using Instagram.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PublicacionesController : ControllerBase
{
    private readonly InstagramDbContext _db;
    private readonly ImagenService _img;
    private const string EstadoAceptado = "Aceptado";

    public PublicacionesController(InstagramDbContext db, ImagenService img)
    {
        _db = db;
        _img = img;
    }

    // POST api/publicaciones (multipart/form-data)
    [HttpPost]
    public async Task<IActionResult> Crear([FromForm] string usuario, [FromForm] string? descripcion, IFormFile imagen)
    {
        if (imagen == null || imagen.Length == 0) return BadRequest("Falta la imagen");
        if (await _db.Usuarios.FindAsync(usuario) == null) return BadRequest("Usuario no existe");

        var url = await _img.Guardar(imagen, "publicaciones");
        var pub = new Publicacione
        {
            UsuPubPer = usuario,
            UrlImgPub = url,
            DesImgPub = descripcion,
            FecHorPub = DateTime.Now
        };
        _db.Publicaciones.Add(pub);
        await _db.SaveChangesAsync();
        return Ok(new { pub.IdPub, pub.UrlImgPub });
    }

    // GET api/publicaciones/5
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Obtener(int id)
    {
        var p = await _db.Publicaciones.FindAsync(id);
        return p == null
            ? NotFound()
            : Ok(new { p.IdPub, p.UsuPubPer, p.UrlImgPub, p.DesImgPub, p.FecHorPub });
    }

    // GET api/publicaciones/feed?usuarioActual=U204&pagina=1&tamano=10
    [HttpGet("feed")]
    public async Task<IActionResult> Feed([FromQuery] string usuarioActual,
        [FromQuery] int pagina = 1, [FromQuery] int tamano = 10)
    {
        if (pagina < 1) pagina = 1;
        if (tamano < 1 || tamano > 50) tamano = 10;

        var siguiendo = _db.Seguidores
            .Where(s => s.IdUsuSig == usuarioActual && s.EstSeg == EstadoAceptado)
            .Select(s => s.IdUsuSeg);

        var lista = await _db.Publicaciones
            .Where(p => p.UsuPubPer == usuarioActual || siguiendo.Contains(p.UsuPubPer))
            .OrderByDescending(p => p.FecHorPub)
            .Skip((pagina - 1) * tamano)
            .Take(tamano)
            .Select(p => new
            {
                p.IdPub,
                p.UsuPubPer,
                Alias = p.UsuPubPerNavigation.AliasUsu,
                p.UrlImgPub,
                p.DesImgPub,
                p.FecHorPub
            })
            .ToListAsync();

        return Ok(lista);
    }

    // GET api/publicaciones/usuario/U204?usuarioActual=U100
    [HttpGet("usuario/{usuario}")]
    public async Task<IActionResult> PorUsuario(string usuario, [FromQuery] string usuarioActual)
    {
        var dueno = await _db.Usuarios.FindAsync(usuario);
        if (dueno == null) return NotFound("Usuario no existe");

        bool puedeVer = !dueno.EsPriv
            || usuario == usuarioActual
            || await _db.Seguidores.AnyAsync(s => s.IdUsuSig == usuarioActual
                                               && s.IdUsuSeg == usuario
                                               && s.EstSeg == EstadoAceptado);

        if (!puedeVer)
            return Ok(new { privado = true, publicaciones = Array.Empty<object>() });

        var pubs = await _db.Publicaciones
            .Where(p => p.UsuPubPer == usuario)
            .OrderByDescending(p => p.FecHorPub)
            .Select(p => new { p.IdPub, p.UrlImgPub, p.DesImgPub, p.FecHorPub })
            .ToListAsync();

        return Ok(new { privado = false, publicaciones = pubs });
    }

    // GET api/publicaciones (temporal, para que Bonilla y Vale vean qué hay)
    [HttpGet]
    public async Task<IActionResult> Listar() =>
        Ok(await _db.Publicaciones
            .OrderByDescending(p => p.FecHorPub)
            .Select(p => new { p.IdPub, p.UsuPubPer, p.UrlImgPub, p.DesImgPub, p.FecHorPub })
            .ToListAsync());

    // DELETE api/publicaciones/5?usuarioActual=U204
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id, [FromQuery] string usuarioActual)
    {
        var p = await _db.Publicaciones.FindAsync(id);
        if (p == null) return NotFound();
        if (p.UsuPubPer != usuarioActual) return StatusCode(403, "Solo el dueño puede eliminar");

        _db.Publicaciones.Remove(p);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}