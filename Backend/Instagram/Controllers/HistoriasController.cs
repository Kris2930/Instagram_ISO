using Instagram.Models;
using Instagram.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HistoriasController : ControllerBase
{
    private readonly InstagramDbContext _db;
    private readonly ImagenService _img;
    private const string EstadoAceptado = "Aceptado";

    public HistoriasController(InstagramDbContext db, ImagenService img)
    {
        _db = db;
        _img = img;
    }

    // POST api/historias (multipart/form-data)
    [HttpPost]
    public async Task<IActionResult> Crear([FromForm] string usuario, IFormFile imagen)
    {
        if (imagen == null || imagen.Length == 0) return BadRequest("Falta la imagen");
        if (await _db.Usuarios.FindAsync(usuario) == null) return BadRequest("Usuario no existe");

        var url = await _img.Guardar(imagen, "historias");
        var ahora = DateTime.Now;
        var h = new Historia
        {
            IdUsuHis = usuario,
            UrlHis = url,
            FecHorHisSub = ahora,
            FecHorHisExp = ahora.AddHours(24)
        };
        _db.Historias.Add(h);
        await _db.SaveChangesAsync();
        return Ok(new { h.IdHis, h.UrlHis, h.FecHorHisExp });
    }

    // GET api/historias/activas?usuarioActual=U204
    // Quiénes tienen historias vigentes entre yo y los que sigo
    [HttpGet("activas")]
    public async Task<IActionResult> Activas([FromQuery] string usuarioActual)
    {
        var ahora = DateTime.Now;

        var siguiendo = _db.Seguidores
            .Where(s => s.IdUsuSig == usuarioActual && s.EstSeg == EstadoAceptado)
            .Select(s => s.IdUsuSeg);

        var lista = await _db.Historias
            .Where(h => h.FecHorHisExp > ahora
                     && (h.IdUsuHis == usuarioActual || siguiendo.Contains(h.IdUsuHis)))
            .GroupBy(h => new { h.IdUsuHis, h.IdUsuHisNavigation.AliasUsu })
            .Select(g => new
            {
                Usuario = g.Key.IdUsuHis,
                Alias = g.Key.AliasUsu,
                Cantidad = g.Count(),
                Ultima = g.Max(h => h.FecHorHisSub)
            })
            .OrderByDescending(x => x.Ultima)
            .ToListAsync();

        return Ok(lista);
    }

    // GET api/historias/usuario/U204?usuarioActual=U100
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

        if (!puedeVer) return Ok(Array.Empty<object>());

        return Ok(await _db.Historias
            .Where(h => h.IdUsuHis == usuario && h.FecHorHisExp > DateTime.Now)
            .OrderBy(h => h.FecHorHisSub)
            .Select(h => new { h.IdHis, h.UrlHis, h.FecHorHisSub })
            .ToListAsync());
    }
}