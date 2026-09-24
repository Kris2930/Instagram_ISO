using System;
using System.Collections.Generic;

namespace Instagram.Models;

public partial class Like
{
    public int IdLike { get; set; }

    public int IdPubLike { get; set; }

    public string IdUsuLike { get; set; } = null!;

    public DateTime FecHorLike { get; set; }

    public virtual Publicacione IdPubLikeNavigation { get; set; } = null!;

    public virtual Usuario IdUsuLikeNavigation { get; set; } = null!;
}
