using System;
using System.Collections.Generic;

namespace BookingSite.Model;

public partial class Neprimicnine
{
    public int NepremicninaId { get; set; }

    public string? Naslov { get; set; }

    public string? Kraj { get; set; }

    public string? PostnaSt { get; set; }

    public int? UporabnikId { get; set; }

    public string? Nadstropje { get; set; }

    public string? StevilkaSobe { get; set; }

    public int? DrzavaId { get; set; }

    public virtual Drzave? Drzava { get; set; }

    public virtual ICollection<Listingi> Listingis { get; set; } = new List<Listingi>();

    public virtual Uporabniki? Uporabnik { get; set; }
}
