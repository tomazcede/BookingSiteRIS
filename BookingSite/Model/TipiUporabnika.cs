using System;
using System.Collections.Generic;

namespace BookingSite.Model;

public partial class TipiUporabnika
{
    public int TipId { get; set; }

    public string? Naziv { get; set; }

    public virtual ICollection<Uporabniki> Uporabnikis { get; set; } = new List<Uporabniki>();
}
