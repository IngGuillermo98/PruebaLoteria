using System;
using System.Collections.Generic;

namespace LoteriaMexicana.Models.Entities;

public partial class Cartas
{
    public int IdCarta { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<CartasPorTabla> CartasPorTabla { get; } = new List<CartasPorTabla>();
}
