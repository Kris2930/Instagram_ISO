using System;
using System.Collections.Generic;

namespace Instagram.Models;

public partial class CompartidosExterno
{
    public int IdComp { get; set; }

    public string IdUsuComp { get; set; } = null!;

    public int IdPubComp { get; set; }

    public DateTime FecHorComp { get; set; }

    public virtual Publicacione IdPubCompNavigation { get; set; } = null!;

    public virtual Usuario IdUsuCompNavigation { get; set; } = null!;
}
