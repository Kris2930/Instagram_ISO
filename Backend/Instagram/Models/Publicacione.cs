using System;
using System.Collections.Generic;

namespace Instagram.Models;

public partial class Publicacione
{
    public int IdPub { get; set; }

    public string UsuPubPer { get; set; } = null!;

    public string UrlImgPub { get; set; } = null!;

    public string? DesImgPub { get; set; }

    public DateTime FecHorPub { get; set; }

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual ICollection<CompartidosExterno> CompartidosExternos { get; set; } = new List<CompartidosExterno>();

    public virtual ICollection<Guardado> Guardados { get; set; } = new List<Guardado>();

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual Usuario UsuPubPerNavigation { get; set; } = null!;
}
