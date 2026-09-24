using System;
using System.Collections.Generic;

namespace Instagram.Models;

public partial class Chat
{
    public int IdChat { get; set; }

    public string IdUsuEnv { get; set; } = null!;

    public string IdUsuRec { get; set; } = null!;

    public string? MenEnv { get; set; }

    public DateTime FecHorMen { get; set; }

    public int? IdPubComp { get; set; }

    public virtual Publicacione? IdPubCompNavigation { get; set; }

    public virtual Usuario IdUsuEnvNavigation { get; set; } = null!;

    public virtual Usuario IdUsuRecNavigation { get; set; } = null!;
}
