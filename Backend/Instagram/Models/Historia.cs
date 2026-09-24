using System;
using System.Collections.Generic;

namespace Instagram.Models;

public partial class Historia
{
    public int IdHis { get; set; }

    public string IdUsuHis { get; set; } = null!;

    public string UrlHis { get; set; } = null!;

    public DateTime FecHorHisSub { get; set; }

    public DateTime? FecHorHisExp { get; set; }

    public virtual Usuario IdUsuHisNavigation { get; set; } = null!;
}
