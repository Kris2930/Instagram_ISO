using System;
using System.Collections.Generic;



namespace Instagram.Models;

public partial class Usuario
{
    public string? IdUsu { get; set; }

    public string NomUsu { get; set; } = null!;

    public string ApeUsu { get; set; } = null!;

    public string CorUsu { get; set; } = null!;

    public string AliasUsu { get; set; } = null!;

    public string ConUsu { get; set; } = null!;

    public bool EsPriv { get; set; }

    public DateOnly FecReg { get; set; }

    public virtual ICollection<Chat> ChatIdUsuEnvNavigations { get; set; } = new List<Chat>();

    public virtual ICollection<Chat> ChatIdUsuRecNavigations { get; set; } = new List<Chat>();

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual ICollection<CompartidosExterno> CompartidosExternos { get; set; } = new List<CompartidosExterno>();

    public virtual ICollection<Guardado> Guardados { get; set; } = new List<Guardado>();

    public virtual ICollection<Historia> Historia { get; set; } = new List<Historia>();

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual ICollection<Publicacione> Publicaciones { get; set; } = new List<Publicacione>();

    public virtual ICollection<Seguidore> SeguidoreIdUsuSegNavigations { get; set; } = new List<Seguidore>();

    public virtual ICollection<Seguidore> SeguidoreIdUsuSigNavigations { get; set; } = new List<Seguidore>();
}
