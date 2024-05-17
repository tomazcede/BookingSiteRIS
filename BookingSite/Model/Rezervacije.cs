using System;
using System.Collections.Generic;

namespace BookingSite.Model;

public partial class Rezervacije
{
    public int RezervacijaId { get; set; }

    public DateTime? DatumOd { get; set; }

    public DateTime? DatumDo { get; set; }

    public int? ListingId { get; set; }

    public int? UporabnikId { get; set; }

    public virtual Listingi? Listing { get; set; }

    public virtual Uporabniki? Uporabnik { get; set; }
}
