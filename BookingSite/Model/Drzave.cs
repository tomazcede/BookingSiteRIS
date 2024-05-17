using System;
using System.Collections.Generic;

namespace BookingSite.Model;

public partial class Drzave
{
    public int DrzavaId { get; set; }

    public string? Ime { get; set; }

    public virtual ICollection<Neprimicnine> Neprimicnines { get; set; } = new List<Neprimicnine>();
}
