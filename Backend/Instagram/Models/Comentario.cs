using System;
using System.Collections.Generic;

namespace Instagram.Models;

public partial class Comentario
{
    public int IdCom { get; set; }

    public string UsuCom { get; set; } = null!;

    public int IdPubCom { get; set; }

    public string DesCom { get; set; } = null!;

    public int? IdComRes { get; set; }

    public DateTime FecHorCom { get; set; }

    public virtual Comentario? IdComResNavigation { get; set; }

    public virtual Publicacione IdPubComNavigation { get; set; } = null!;

    public virtual ICollection<Comentario> InverseIdComResNavigation { get; set; } = new List<Comentario>();

    public virtual Usuario UsuComNavigation { get; set; } = null!;
}
