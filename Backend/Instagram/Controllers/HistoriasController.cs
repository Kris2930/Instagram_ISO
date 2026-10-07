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
    public HistoriasController(InstagramDbContext db, ImagenService img) { _db = db; _img = img; }

    // POST api/historias
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

    // GET api/historias/usuario/kris → historias vigentes (no expiradas) de una persona
    [HttpGet("usuario/{usuario}")]
    public async Task<IActionResult> PorUsuario(string usuario) =>
        Ok(await _db.Historias
            .Where(h => h.IdUsuHis == usuario && h.FecHorHisExp > DateTime.Now)
            .OrderBy(h => h.FecHorHisSub)
            .Select(h => new { h.IdHis, h.UrlHis, h.FecHorHisSub })
            .ToListAsync());
}