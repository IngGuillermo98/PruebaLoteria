using System;
using System.Collections.Generic;

namespace LoteriaMexicana.Models.Entities;

public partial class CartasPorTabla
{
    public int IdCartasPorTabla { get; set; }

    public int? IdCarta { get; set; }

    public int? IdTabla { get; set; }

    public int? PosicionF { get; set; }

    public int? PosicionC { get; set; }

    public virtual Cartas? IdCartaNavigation { get; set; }

    public virtual Tablas? IdTablaNavigation { get; set; }
}
