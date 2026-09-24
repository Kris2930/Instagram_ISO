using System;
using System.Collections.Generic;

namespace Instagram.Models;

public partial class Guardado
{
    public int IdGua { get; set; }

    public string IdUsuGua { get; set; } = null!;

    public int IdPubGua { get; set; }

    public DateOnly FecGua { get; set; }

    public virtual Publicacione IdPubGuaNavigation { get; set; } = null!;

    public virtual Usuario IdUsuGuaNavigation { get; set; } = null!;
}
