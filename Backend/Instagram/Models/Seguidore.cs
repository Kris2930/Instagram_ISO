using System;
using System.Collections.Generic;

namespace Instagram.Models;

public partial class Seguidore
{
    public int NumSeg { get; set; }

    public string IdUsuSig { get; set; } = null!;

    public string IdUsuSeg { get; set; } = null!;

    public string EstSeg { get; set; } = null!;

    public DateOnly? FecSeg { get; set; }

    public virtual Usuario IdUsuSegNavigation { get; set; } = null!;

    public virtual Usuario IdUsuSigNavigation { get; set; } = null!;
}
