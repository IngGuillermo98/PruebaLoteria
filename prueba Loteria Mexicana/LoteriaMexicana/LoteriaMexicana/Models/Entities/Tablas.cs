using System;
using System.Collections.Generic;

namespace LoteriaMexicana.Models.Entities;

public partial class Tablas
{
    public int IdTabla { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<CartasPorTabla> CartasPorTabla { get; } = new List<CartasPorTabla>();
}
