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
    public PublicacionesController(InstagramDbContext db, ImagenService img) { _db = db; _img = img; }

    // POST api/publicaciones (multipart/form-data)
    [HttpPost]
    public async Task<IActionResult> Crear([FromForm] string usuario, [FromForm] string? descripcion, IFormFile imagen)
    {
        if (imagen == null || imagen.Length == 0) return BadRequest("Falta la imagen");

        // FindAsync busca por la llave primaria, sea cual sea su nombre
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
    [HttpGet("{id}")]
    public async Task<IActionResult> Obtener(int id)
    {
        var p = await _db.Publicaciones.FindAsync(id);
        return p == null ? NotFound()
            : Ok(new { p.IdPub, p.UsuPubPer, p.UrlImgPub, p.DesImgPub, p.FecHorPub });
    }

    // GET api/publicaciones (temporal, para que Bonilla y Vale vean qué hay)
    [HttpGet]
    public async Task<IActionResult> Listar() =>
        Ok(await _db.Publicaciones.OrderByDescending(p => p.FecHorPub)
            .Select(p => new { p.IdPub, p.UsuPubPer, p.UrlImgPub, p.DesImgPub, p.FecHorPub })
            .ToListAsync());
}