namespace Instagram.Services
{
    public class ImagenService
    {
        private readonly IWebHostEnvironment _env;
        public ImagenService(IWebHostEnvironment env) => _env = env;

        public async Task<string> Guardar(IFormFile file, string carpeta)
        {
            var ext = Path.GetExtension(file.FileName).ToLower();
            if (ext is not (".jpg" or ".jpeg" or ".png" or ".webp"))
                throw new ArgumentException("Formato no permitido");

            var raiz = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
            var dir = Path.Combine(raiz, "uploads", carpeta);
            Directory.CreateDirectory(dir);

            var nombre = $"{Guid.NewGuid()}{ext}";
            using var fs = new FileStream(Path.Combine(dir, nombre), FileMode.Create);
            await file.CopyToAsync(fs);
            return $"/uploads/{carpeta}/{nombre}";
        }
    }
}
