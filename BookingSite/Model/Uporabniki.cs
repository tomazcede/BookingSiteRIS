using System;
using System.Collections.Generic;

namespace BookingSite.Model;

public partial class Uporabniki
{
    public int UporabnikId { get; set; }

    public string? Ime { get; set; }

    public string? Priimek { get; set; }

    public string? DatumRojstva { get; set; }

    public string? Email { get; set; }

    public string? Geslo { get; set; }

    public int? TipUporabnikaId { get; set; }

    public virtual ICollection<Listingi> Listingis { get; set; } = new List<Listingi>();

    public virtual ICollection<Neprimicnine> Neprimicnines { get; set; } = new List<Neprimicnine>();

    public virtual ICollection<Rezervacije> Rezervacijes { get; set; } = new List<Rezervacije>();

    public virtual TipiUporabnika? TipUporabnika { get; set; }
}
